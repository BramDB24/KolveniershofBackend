using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
using kolveniershofBackend.DTO.Picto;
using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using kolveniershofBackend.Extensions;

namespace kolveniershofBackend.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DagPlanningController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IDagPlanningTemplateRepository _dagPlanningTemplateRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IAtelierRepository _atelierRepository;
        /// <summary>
        /// gebruik repos
        /// </summary>
        /// <param name="dagPlanningTemplateRepository"></param>
        /// <param name="gebruikerRepository"></param>
        /// <param name="atelierRepository"></param>
        /// <param name="templateRepository"></param>
        /// 
        public DagPlanningController(IDagPlanningTemplateRepository dagPlanningTemplateRepository, IGebruikerRepository gebruikerRepository, IAtelierRepository atelierRepository, ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
            _dagPlanningTemplateRepository = dagPlanningTemplateRepository;
            _gebruikerRepository = gebruikerRepository;
            _atelierRepository = atelierRepository;
        }

        /// <summary>
        /// zoek dagplanning op datum
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        [HttpGet("{datum}")]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagplanningDTO> GetDagPlanning(string datum)
        {   
            VerwijderVerouderdeData();
            VulDagenAan();
            DateTime datumFormatted;
            try
            {
                datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }
            catch
            {
                return BadRequest();
            }

            return MaakDagPlanningDto(GeefDagPlanningVolgensDatum(datumFormatted));
        }

        /// <summary>
        /// Geeft alle aanwezige gebruikers terug volgens de dagplanning van vandaag
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        [HttpGet("{datum}/aanwezigen")]
        public IEnumerable<DagAtelierDTO> GetAanwezigeGebruikers(string datum)
        {
            DagplanningDTO huidigeDagPlanning = GetDagPlanning(datum).Value;
            AtelierType[] afwezigeAtelierTypes = new AtelierType[] { AtelierType.Afwezig, AtelierType.Thuis, AtelierType.Ziek };
            IEnumerable<DagAtelierDTO> afwezigeAteliers = huidigeDagPlanning.DagAteliers.Where(x => afwezigeAtelierTypes.Contains(x.Atelier.AtelierType));
            IEnumerable<DagAtelierDTO> normaleAteliers = huidigeDagPlanning.DagAteliers.Where(x => x.Atelier.AtelierType == AtelierType.Gewoon);
            IEnumerable<BasicGebruikerDTO> aanwezigenVoormiddag = normaleAteliers.Where(a => a.DagMoment == DagMoment.Voormiddag).Select(d => d.Gebruikers).SelectMany(g => g).Distinct();
            IEnumerable<BasicGebruikerDTO> aanwezigenNamiddag = normaleAteliers.Where(a => a.DagMoment == DagMoment.Namiddag).Select(d => d.Gebruikers).SelectMany(g => g).Distinct();
            return afwezigeAteliers.Append(new DagAtelierDTO()
            {
                Atelier = new AtelierDTO() { AtelierType = AtelierType.Gewoon, Naam = "Aanwezigen voormiddag" },
                DagMoment = DagMoment.Voormiddag,
                Gebruikers = aanwezigenVoormiddag,
            })
            .Append(new DagAtelierDTO()
            {
                Atelier = new AtelierDTO() { AtelierType = AtelierType.Gewoon, Naam = "Aanwezigen namiddag" },
                DagMoment = DagMoment.Namiddag,
                Gebruikers = aanwezigenVoormiddag,
            });
        }

        /// <summary>
        /// Geeft een pictodto terug met enkel de picto gegevens van de gegeven persoon.
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="gebruikerId"></param>
        /// <returns></returns>
        [HttpGet("{datum}/Gebruiker/{gebruikerId}")]
        public ActionResult<PictoDagDTO> GetPictoAgendaVanEenPersoon(string datum, string gebruikerId)
        {
            //Controle op overbodige data
            VerwijderVerouderdeData();

            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DagPlanning dagplanning = GeefDagPlanningVolgensDatum(datumFormatted);
            var pictoDagDTO = new PictoDagDTO()
            {
                Eten = dagplanning.Eten,
                Datum = datumFormatted,
                GebruikerId = gebruikerId,
                Ateliers = dagplanning.GetDagAteliersGebruiker(gebruikerId).Select(da => new PictoAtelierDTO()
                {
                    AtelierNaam = da.Atelier.Naam,
                    AtelierImg = da.Atelier.PictoURL,
                    BegeleiderImages = da.GeefAlleBegeleiders().Select(g => g.Foto),
                    AtelierType = da.Atelier.AtelierType.ToString(),
                    dagMoment = da.DagMoment.ToString()
                }) //?? new List<PictoAtelierDTO>()  --> null of new list teruggeven?
            };
            return pictoDagDTO;
        }

        [HttpGet("{datum}/pictoagenda")]
        public IEnumerable<PictoDagDTO> GetWeekPictoAgendasHuidigeGebruiker(string datum)
        {
            Gebruiker gebruiker = new Gebruiker();
            if (!_gebruikerRepository.TryGetGebruiker(User.Identity.Name, out gebruiker))
            {
                return null;
            }
            DateTime tempdate = GetEersteDagVanWeek(datum);
            var pictodtos = new List<PictoDagDTO>();

            for (int i = 0; i < 7; i++)
            {
                pictodtos.Add(GetPictoAgendaVanEenPersoon(tempdate.AddDays(i).ToString("yyyy/MM/dd"), gebruiker.Id).Value);
            }
            return pictodtos.AsEnumerable();
        }

        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        [HttpGet("{datum}/pictoagenda/client/{gebruikerId}")]
        public IEnumerable<PictoDagDTO> GetWeekPictoAgendasVanClient(string datum, string gebruikerId)
        {
            Gebruiker gebruiker = _gebruikerRepository.GetBy(gebruikerId);
            if (gebruiker == null)
                return null;

            DateTime tempdate = GetEersteDagVanWeek(datum);
            var pictodtos = new List<PictoDagDTO>();
            for (int i = 0; i < 7; i++)
            {
                pictodtos.Add(GetPictoAgendaVanEenPersoon(tempdate.AddDays(i).ToString("yyyy/MM/dd"), gebruiker.Id).Value);
            }
            return pictodtos.AsEnumerable();
        }

        /// <summary>
        /// aanpassingen aan dagplanningtemplate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dagPlanning"></param>
        /// <returns></returns>
        [HttpPut("template/{id}")]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagPlanningTemplate> PutDagPlanningTemplateAanpassingen(int id, DagPlanningTemplate dagPlanning)
        {
            if (id != dagPlanning.DagplanningId)
                return BadRequest();
            _dagPlanningTemplateRepository.Update(dagPlanning);
            _dagPlanningTemplateRepository.SaveChanges();
            return NoContent();
        }

        [HttpPut("updatecommentaar/{dagplanningId}/commentaar/{commentaar}")]
        public IActionResult PutDagplanningCommentaar(int dagplanningId, string commentaar)
        {
            var dag = _dagPlanningTemplateRepository.GetByIdDagPlanning(dagplanningId);
            if(dag == null)
            {
                return BadRequest();
            }
            dag.Commentaar = commentaar;
            _dagPlanningTemplateRepository.Update(dag);
            _dagPlanningTemplateRepository.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// update dagplanning met opgegeven dagplanning id en met template
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dagPlanning"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagPlanningTemplate> PutDagPlanningAanpassingen(int id, DagPlanning dagPlanning)
        {
            if (id != dagPlanning.DagplanningId)
                return BadRequest();
            _dagPlanningTemplateRepository.Update(dagPlanning);
            _dagPlanningTemplateRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// voeg maaltijd aan een dagplanning toe of vervang de huidige
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eten"></param>
        /// <returns></returns>
        [HttpPost("{id}/eten")]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagplanningDTO> PutEten(int id, String eten)
        {
            DagPlanningTemplate dpt = _dagPlanningTemplateRepository.GetByIdDagPlanningTemplate(id);
          
            if (dpt != null)
            {
                dpt.Eten = eten;
                _dagPlanningTemplateRepository.Update(dpt);
                _dagPlanningTemplateRepository.SaveChanges();
                 return Ok();
            }

            return BadRequest();

            
        }

        [HttpPost("{datumVanDagplanning}/dagateliers")]
        public ActionResult<DagPlanning> DeleteDagAtelierUitDagplanning(string datumVanDagplanning, DagAtelierDTO dagAtelier)
        {
            DateTime datumFormatted = DateTime.Parse(datumVanDagplanning, null, System.Globalization.DateTimeStyles.RoundtripKind);
            var planning = _dagPlanningTemplateRepository.GetByDatum(datumFormatted);
            DagAtelier da = planning.DagAteliers.FirstOrDefault(d => d.DagAtelierId == dagAtelier.DagAtelierId);
            planning.VerwijderDagAtlierVanDagPlanningTemplate(da);
            _dagPlanningTemplateRepository.Update(planning);
            _dagPlanningTemplateRepository.SaveChanges();
            return CreatedAtAction(nameof(GetDagPlanning), new { datum = planning.Datum }, planning);
        }

        [HttpPut("{id}/dagAtelier")]
        public ActionResult PutDagAtelier(int id, DagAtelierDTO dto)
        {
            var dagPlanning = _dagPlanningTemplateRepository.GetByIdDagPlanningTemplate(id);
            var atelier = _atelierRepository.getBy(dto.Atelier.AtelierId);

            if (dagPlanning == null)
            {
                return NotFound();
            }

            DagAtelier dagAtelier = new DagAtelier
            {
                Atelier = atelier,
                DagAtelierId = dto.DagAtelierId,
                DagMoment = dto.DagMoment
            };
            
            dto.Gebruikers.ToList().ForEach(e => dagAtelier.VoegGebruikerAanDagAtelierToe(_gebruikerRepository.GetBy(e.Id)));

            if (dto.DagAtelierId == 0)
            {
                dagPlanning.DagAteliers.Add(dagAtelier);
            }
            else
            {
                var temp = dagPlanning.DagAteliers.FirstOrDefault(t => t.DagAtelierId == dto.DagAtelierId);
                temp.Gebruikers = dagAtelier.Gebruikers;
                temp.Atelier = dagAtelier.Atelier;
                temp.DagMoment = dagAtelier.DagMoment;
            }
            _dagPlanningTemplateRepository.SaveChanges();

            return Ok();
        }


        //Helper methodes

        private DagplanningDTO MaakDagPlanningDto(DagPlanning dagPlanning)
        {
            return new DagplanningDTO()
            {
                DagplanningId = dagPlanning.DagplanningId,
                Eten = dagPlanning.Eten,
                Weekdag = dagPlanning.Weekdag,
                Weeknummer = dagPlanning.Weeknummer,
                Datum = dagPlanning.Datum,
                DagAteliers = SetDagAteliers(dagPlanning),
                Commentaar = dagPlanning.Commentaar
            };
        }
        private IEnumerable<DagAtelierDTO> SetDagAteliers(DagPlanningTemplate dagPlanning)
        {
            return dagPlanning.DagAteliers.Select(da => new DagAtelierDTO()
            {
                Atelier = new AtelierDTO()
                {
                    AtelierId = da.Atelier.AtelierId,
                    AtelierType = da.Atelier.AtelierType,
                    Naam = da.Atelier.Naam,
                    PictoURL = da.Atelier.PictoURL
                },
                DagAtelierId = da.DagAtelierId,
                DagMoment = da.DagMoment,
                Gebruikers = da.Gebruikers.Select(gda => new BasicGebruikerDTO()
                {
                    Id = gda.Gebruiker.Id,
                    Achternaam = gda.Gebruiker.Achternaam,
                    Voornaam = gda.Gebruiker.Voornaam,
                    Foto = gda.Gebruiker.Foto,
                    Type = gda.Gebruiker.Type,
                })
            });
        }

        /**
         * Geeft van een gegeven datum de maandag van de week waar deze datum in zit.
         * Dit wordt voornamelijk gebruikt om het verschil in weken te berekenen.
         */
        private DateTime DinsdagVanWeek(DateTime datum)
        {
            //-2 toegevoegd aangezien DayOfWeek begint op zondag en niet op dinsdag
            //+7 toegevoegd voor het geval dat datum een zondag is, zonder +7 is het resultaat -1 maar we hebben een 6 nodig
            int dagenVerwijderedVanDinsdag = ((int)datum.DayOfWeek - 2 + 7) % 7;
            return new DateTime(datum.Year, datum.Month, datum.Day).AddDays(dagenVerwijderedVanDinsdag * -1);
        }


        /**
         * Geeft de DagPlanningTemplate van een bepaald weeknummer en weekdag.
         * Als de DagPlanningTemplate niet bestaat in de databank dan wordt hij gegenereerd.
         * Deze actie wordt in verschillende api calls gebruikt, daarom dat het beschikbaar is in een private methode
         */
        private DagPlanningTemplate GeefDagPlanningTemplate(int weeknummer, int weekdag)
        {
            var dagplanning = _templateRepository.GetActiveDagTemplate(weeknummer, (Weekdag)weekdag);
            if (dagplanning == null)
                return new DagPlanningTemplate(weeknummer, (Weekdag)weekdag);
            return dagplanning;
            //if (_dagPlanningTemplateRepository.GetTemplateByWeeknummerEnDagnummerGeenInclude(weeknummer, (Weekdag)weekdag) == null)
            //{
            //    // Maak een nieuwe DagPlanningTemplate aan als er geen Template bestaat
            //    DagPlanningTemplate nieuwDagPlanningTemplate = new DagPlanningTemplate(weeknummer, (Weekdag)weekdag);
            //    _dagPlanningTemplateRepository.Add(nieuwDagPlanningTemplate);
            //    _dagPlanningTemplateRepository.SaveChanges();

            //}
            //return _dagPlanningTemplateRepository.GetTemplateByWeeknummerEnDagnummer(weeknummer, (Weekdag)weekdag);
        }


        /**
         * Deze methode geeft een DagPlanning volgens de gegeven datum.
         * Als deze niet bestaat dan wordt er een DagPlanning aangemaakt volgens een gepaste DagPlanningTemplate
         */
        private DagPlanning GeefDagPlanningVolgensDatum(DateTime datum)
        {
            DagPlanning dagplanning = _dagPlanningTemplateRepository.GetByDatum(datum);
            if (dagplanning != null)
            {
                return dagplanning;
            }

            // Eerst zoeken we naar de juiste template om te gebruiken. Hiervoor hebben we een weekdag en een weeknummer nodig.

            //Is nodig om te kunnen werken met de enums, de enum heeft (voor whatever reason) een undefined nodig die index 0 heeft, terwijl index 0 gewoon maandag moet zijn
            var weekdag = datum.DagVanWeek();
            //var weekdag = ((int)datum.DayOfWeek - 1 + 7) % 7;

            //if (weekdag == (int)Weekdag.Undefined)
            //{
            //    weekdag = (int)Weekdag.Maandag;
            //}

            /**
             * We kunnen een willekeurige, reeds bestaande DagPlanning nemen om te controleren hoe veel weken de datum van de parameter
             * verschilt met de datum van de willekeurige DagPlanning
             * Hiermee kunnen we het weeknummer voor de nieuwe DagPlanning bepalen.
             */
            DagPlanning controleDagPlanning = null;
            if (!_dagPlanningTemplateRepository.IsDagPlanningenLeeg())
            {
                controleDagPlanning = _dagPlanningTemplateRepository.GetEersteDagPlanning();
            }
            else
            {
                /**
                 * Als er nog geen dagplanningen zijn dan gaan we er één aanmaken voor de dag van vandaag met 
                 * de week waar we vandaag in zitten als de start van de 4 weekse planning
                 */
                // maak een nieuw dagplanning vandaag en start vanaf de eerste week
                // dit wordt ook aangemaakt aangezien we de dag van vandaag als referentie gebruiken om de juiste template te vinden
                DagPlanningTemplate dagPlanningTemplateVandaag = GeefDagPlanningTemplate(1, weekdag);
                controleDagPlanning = new DagPlanning(dagPlanningTemplateVandaag, DateTime.Today);
                _dagPlanningTemplateRepository.AddDagPlanning(controleDagPlanning);
            }

            ///**
            // * Om het aantal weken te berekenen converteren we eerst de datum van de parameter en de data van vandaag
            // * naar de maandag van de week waar ze in zitten.
            // * Hierdoor kunnen we het aantal weken berekenen door het verschil in dagen te nemen en dit te delen door 7.
            // */
            //var weeknummerControle = controleDagPlanning.Weeknummer;
            //var dinsdagControleWeek = DinsdagVanWeek(controleDagPlanning.Datum);
            //var dinsdagGegevenWeek = DinsdagVanWeek(datum);
            //var aantalWekenVerschil = (dinsdagGegevenWeek - dinsdagControleWeek).Days / 7;
            //int weeknummer = (((weeknummerControle - 1) + aantalWekenVerschil + 4) % 4) + 1;
            var weeknummer = datum.WeekNummer(controleDagPlanning.Datum, controleDagPlanning.Weeknummer);
            DagPlanningTemplate dagPlanningTemplate = GeefDagPlanningTemplate(weeknummer, weekdag);
            
            // Aangezien we nu de template hebben kunnen we een nieuwe dagplanning maken voor de opgegeven datum
            dagplanning = new DagPlanning(dagPlanningTemplate, datum);
            //_dagPlanningTemplateRepository.AddDagPlanning(dagplanning);
            //_dagPlanningTemplateRepository.SaveChanges();
            return dagplanning;
        }

        /**
         *  Voegt dagen in het verleden toe.
         **/
        private void VulDagenAan()
        {
            DagPlanning last = _dagPlanningTemplateRepository.GetLastDagPlanning();
            if(last == null)
            {
                var template = _templateRepository.GetActiveDagTemplate(1, (Weekdag)DateTime.Today.DagVanWeek());
                last = new DagPlanning(template, DateTime.Today);
                _dagPlanningTemplateRepository.AddDagPlanning(last);
            }
                
            DateTime tempdate = last.Datum;
            while (tempdate < DateTime.Today)
            {
                tempdate = tempdate.AddDays(1);
                var template = _templateRepository.GetActiveDagTemplate(tempdate.WeekNummer(last.Datum, last.Weeknummer), (Weekdag)tempdate.DagVanWeek());
                _dagPlanningTemplateRepository.AddDagPlanning(new DagPlanning(template, tempdate));
            }
            _dagPlanningTemplateRepository.SaveChanges();

        }


        /// <summary>
        /// Deze methode controleert of er verouderde data is in de databank en verwijdert die.
        /// Data is verouderd als het minstens twee jaar oud is (gebasseerd op jaar nummer).
        /// </summary>
        private void VerwijderVerouderdeData()
        {
            DagPlanning oudsteDagPlanning = _dagPlanningTemplateRepository.GetEersteDagPlanning();
            if (DateTime.Today.Year - oudsteDagPlanning.Datum.Year < 2)
            {
                return;
            }

            _dagPlanningTemplateRepository.DeleteOuderDanAantalJaar(DateTime.Today, 2);
            _dagPlanningTemplateRepository.SaveChanges();

        }

        private DateTime GetEersteDagVanWeek(string datum)
        {
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            int dayofweek = (int)datumFormatted.DayOfWeek - 1;
            if (dayofweek < 0)
                dayofweek = 6; //zondag als laatste dag nemen. (-1 -> 6)
            DateTime tempdate = datumFormatted.AddDays(dayofweek * -1);
            return tempdate;

        }
    }
}
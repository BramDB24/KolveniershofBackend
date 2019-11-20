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
        private readonly IDagPlanningTemplateRepository _dagPlanningTemplateRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IAtelierRepository _atelierRepository;
        /// <summary>
        /// gebruik repos
        /// </summary>
        /// <param name="dagPlanningTemplateRepository"></param>
        /// <param name="gebruikerRepository"></param>
        /// 
        public DagPlanningController(IDagPlanningTemplateRepository dagPlanningTemplateRepository, IGebruikerRepository gebruikerRepository, IAtelierRepository repo)
        {
            _dagPlanningTemplateRepository = dagPlanningTemplateRepository;
            _gebruikerRepository = gebruikerRepository;
            _atelierRepository = repo;
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
            //Controle op overbodige data
            VerwijderVerouderdeData();

            DateTime datumFormatted = DateTime.Now;
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

        /**
         * Geeft de DagPlanningTemplate van een bepaald weeknummer en weekdag.
         * Als de DagPlanningTemplate niet bestaat in de databank dan wordt hij gegenereerd.
         */
        [HttpGet("vanWeek/{weeknummer}/vanDag/{weekdag}")]
        public ActionResult<DagplanningDTO> GetDagPlanningTemplate(int weeknummer, int weekdag)
        {
            DagPlanningTemplate dagPlanningTemplate = null;
            try
            {
                dagPlanningTemplate = GeefDagPlanningTemplate(weeknummer, weekdag);
            }
            catch
            {
                return BadRequest();
            }
            DagplanningDTO dagPlanningTemplateDto = new DagplanningDTO()
            {
                DagplanningId = dagPlanningTemplate.DagplanningId,
                Eten = null,
                Weekdag = dagPlanningTemplate.Weekdag,
                Weeknummer = dagPlanningTemplate.Weeknummer,
                Datum = null,
                DagAteliers = SetDagAteliers(dagPlanningTemplate)
            };
            dagPlanningTemplateDto.DagplanningId = dagPlanningTemplate.DagplanningId;
            return dagPlanningTemplateDto;
        }

        /// <summary>
        /// Geeft een pictodto terug met enkel de picto gegevens van de gegeven persoon.
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="gebruikerId"></param>
        /// <returns></returns>
        [HttpGet("{datum}/Gebruiker/{gebruikerId}")]
        public ActionResult<PictoDagDTO> GetDagPlanningVanEenPersoon(string datum, string gebruikerId)
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
                    AtelierImg = da.Atelier.PictoURL,
                    BegeleiderImages = da.GeefAlleBegeleiders().Select(g => g.Foto),
                    AtelierType = da.Atelier.AtelierType.ToString(),
                    dagMoment = da.DagMoment.ToString()
                }) //?? new List<PictoAtelierDTO>()  --> null of new list teruggeven?
            };
            return pictoDagDTO;
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

        [HttpPost("week/{weeknr}/dag/{weekdag}/dagateliers")]
        public ActionResult<DagPlanning> DeleteDagAtelierUitDagplanningTemplate(int weeknr, int weekdag, DagAtelierDTO dagAtelier)
        {
            DagPlanningTemplate dagPlanningTemplate = _dagPlanningTemplateRepository.GetTemplateByWeeknummerEnDagnummer(weeknr, (Weekdag)weekdag);
            DagAtelier da = dagPlanningTemplate.DagAteliers.FirstOrDefault(d => d.DagAtelierId == dagAtelier.DagAtelierId);
            dagPlanningTemplate.VerwijderDagAtlierVanDagPlanningTemplate(da);
            _dagPlanningTemplateRepository.Update(dagPlanningTemplate);
            _dagPlanningTemplateRepository.SaveChanges();
            return CreatedAtAction(nameof(GetDagPlanningTemplate), new { week = dagPlanningTemplate.Weeknummer, weekdag = dagPlanningTemplate.Weekdag },
                dagPlanningTemplate);
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

            dto.Gebruikers.ToList().ForEach(e => dagAtelier.VoegGebruikerAanDagAtelierToe(_gebruikerRepository.GetBy(e.GebruikerId)));

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
                DagAteliers = SetDagAteliers(dagPlanning)
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
                    GebruikerId = gda.Gebruiker.Id,
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
            if (_dagPlanningTemplateRepository.GetTemplateByWeeknummerEnDagnummerGeenInclude(weeknummer, (Weekdag)weekdag) == null)
            {
                // Maak een nieuwe DagPlanningTemplate aan als er geen Template bestaat
                DagPlanningTemplate nieuwDagPlanningTemplate = new DagPlanningTemplate(weeknummer, (Weekdag)weekdag);
                _dagPlanningTemplateRepository.Add(nieuwDagPlanningTemplate);
                _dagPlanningTemplateRepository.SaveChanges();

            }
            return _dagPlanningTemplateRepository.GetTemplateByWeeknummerEnDagnummer(weeknummer, (Weekdag)weekdag);
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
            var weekdag = ((int)datum.DayOfWeek - 1 + 7) % 7;

            if (weekdag == (int)Weekdag.Undefined)
            {
                weekdag = (int)Weekdag.Maandag;
            }

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

                // bereken de weekdag enum voor de datum van vandaag
                var weekdagVandaag = (int)datum.DayOfWeek - 1;

                if (weekdagVandaag == (int)Weekdag.Undefined)
                {
                    weekdagVandaag = (int)Weekdag.Maandag;
                };
                // maak een nieuw dagplanning vandaag en start vanaf de eerste week
                // dit wordt ook aangemaakt aangezien we de dag van vandaag als referentie gebruiken om de juiste template te vinden
                DagPlanningTemplate dagPlanningTemplateVandaag = GeefDagPlanningTemplate(1, weekdagVandaag);
                controleDagPlanning = new DagPlanning(dagPlanningTemplateVandaag, DateTime.Today);
                _dagPlanningTemplateRepository.AddDagPlanning(controleDagPlanning);
            }

            /**
             * Om het aantal weken te berekenen converteren we eerst de datum van de parameter en de data van vandaag
             * naar de maandag van de week waar ze in zitten.
             * Hierdoor kunnen we het aantal weken berekenen door het verschil in dagen te nemen en dit te delen door 7.
             */
            var weeknummerControle = controleDagPlanning.Weeknummer;
            var dinsdagControleWeek = DinsdagVanWeek(controleDagPlanning.Datum);
            var dinsdagGegevenWeek = DinsdagVanWeek(datum);
            var aantalWekenVerschil = (dinsdagGegevenWeek - dinsdagControleWeek).Days / 7;
            int weeknummer = (((((weeknummerControle - 1) + aantalWekenVerschil + 4) % 4) + 4) % 4) + 1;

            DagPlanningTemplate dagPlanningTemplate = GeefDagPlanningTemplate(weeknummer, weekdag);


            // Aangezien we nu de template hebben kunnen we een nieuwe dagplanning maken voor de opgegeven datum
            dagplanning = new DagPlanning(dagPlanningTemplate, datum);
            _dagPlanningTemplateRepository.AddDagPlanning(dagplanning);
            _dagPlanningTemplateRepository.SaveChanges();
            return dagplanning;
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
    }
}
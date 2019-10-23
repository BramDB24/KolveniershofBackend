using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
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
        private readonly IDagPlanningTemplateRepository _dagPlanningRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IAtelierRepository _atelierRepository; 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dagPlanningRepository"></param>
        /// <param name="gebruikerRepository"></param>
        /// 
        public DagPlanningController(IDagPlanningTemplateRepository dagPlanningRepository, IGebruikerRepository gebruikerRepository, IAtelierRepository repo)
        {
            _dagPlanningRepository = dagPlanningRepository;
            _gebruikerRepository = gebruikerRepository;
            _atelierRepository = repo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        [HttpGet("{datum}")]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagplanningDTO> GetDagPlanning(string datum)
        {
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DagPlanning dagplanning = _dagPlanningRepository.GetBy(datumFormatted);

            //Is nodig om te kunnen werken met de enums, de enum heeft (voor whatever reason) een undefined nodig die index 0 heeft, terwijl index 0 gewoon maandag moet zijn
            var weekdag = (int)datumFormatted.DayOfWeek - 1;

            if (weekdag == (int)Weekdag.Undefined)
            {
                weekdag = (int)Weekdag.Maandag;
            };
            //Nodig om te berekenen in welke week en dag de dagplanning zit voor de opgegeven datum
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datumFormatted);
            if (day >= DayOfWeek.Tuesday && day <= DayOfWeek.Thursday)
            {
                datumFormatted = datumFormatted.AddDays(3);
            }

            //Herbereken de week met dinsdag als eerste dag
            var weeknummer = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(datumFormatted, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Tuesday);
            var weeknummerDagPlanning = weeknummer % 4;

            DagPlanningTemplate dagPlanningTemplate = _dagPlanningRepository.GetBy(weeknummerDagPlanning, (Weekdag)weekdag);


            //Als er geen dagplanning gevonden is voor de datum moet er een template gebruikt worden, daarvoor zijn allemaal de nullchecks nodig
            DagplanningDTO dto = new DagplanningDTO()
            {
                DagplanningId = dagplanning == null ? dagPlanningTemplate.DagplanningId : dagplanning.DagplanningId,
                Eten = dagplanning == null ? null : dagplanning.Eten,
                Weekdag = dagplanning == null ? dagPlanningTemplate.Weekdag : dagplanning.Weekdag,
                Weeknummer = dagplanning == null ? dagPlanningTemplate.Weeknummer : dagplanning.Weeknummer,
                Datum = dagplanning == null ? (DateTime?)null : dagplanning.Datum,
                DagAteliers = dagplanning == null ? setDagAteliersTemplate(dagPlanningTemplate) : setDagAteliers(dagplanning)
            };

            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dagPlanning"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagPlanningTemplate> PutDagPlanning(int id, DagPlanningTemplate dagPlanning)
        {
            if (id != dagPlanning.DagplanningId)
                return BadRequest();
            _dagPlanningRepository.Update(dagPlanning);
            _dagPlanningRepository.SaveChanges();
            return NoContent();
        }

        [HttpPost("{datumVanDagplanning}")]
        public ActionResult<DagPlanning> DeleteDagAtelierUitDagplanning(string datumVanDagplanning, DagAtelierDTO dagAtelier)
        {
            DateTime datumFormatted = DateTime.Parse(datumVanDagplanning, null, System.Globalization.DateTimeStyles.RoundtripKind);
            var planning = _dagPlanningRepository.GetBy(datumFormatted);
            DagAtelier da = planning.DagAteliers.FirstOrDefault(d => d.DagAtelierId == dagAtelier.DagAtelierId);
            planning.VerwijderDagAtlierVanDagPlanningTemplate(da);
            _dagPlanningRepository.Update(planning);
            _dagPlanningRepository.SaveChanges();
            return CreatedAtAction(nameof(GetDagPlanning), new { datum = planning.Datum }, planning);
        }

        [HttpPut("dagAtelier/{id}")]
        public ActionResult putDagPlanning(int id, DagAtelierDTO dto)
        {
            var dagPlanning = _dagPlanningRepository.GetById(id);
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
            } else
            {
                var temp = dagPlanning.DagAteliers.FirstOrDefault(t => t.DagAtelierId == dto.DagAtelierId);
                temp.Gebruikers = dagAtelier.Gebruikers;
                temp.Atelier = dagAtelier.Atelier;
                temp.DagMoment = dagAtelier.DagMoment;
            }
            _dagPlanningRepository.SaveChanges();

            return Ok();
        }

        //Helper methodes
        private IEnumerable<DagAtelierDTO> setDagAteliers(DagPlanning dagPlanning)
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

        private IEnumerable<DagAtelierDTO> setDagAteliersTemplate(DagPlanningTemplate dagPlanning)
        {
            return dagPlanning.DagAteliers.Select(t => new DagAtelierDTO()
            {
                Atelier = new AtelierDTO()
                {
                    AtelierType = t.Atelier.AtelierType,
                    Naam = t.Atelier.Naam,
                    PictoURL = t.Atelier.PictoURL
                },
                DagAtelierId = t.DagAtelierId,
                DagMoment = t.DagMoment,
                Gebruikers = t.Gebruikers.Select(gda => new BasicGebruikerDTO()
                {
                    GebruikerId = gda.Gebruiker.Id,
                    Achternaam = gda.Gebruiker.Achternaam,
                    Voornaam = gda.Gebruiker.Voornaam,
                    Foto = gda.Gebruiker.Foto,
                    Type = gda.Gebruiker.Type,
                })
            });

        }
    }
}
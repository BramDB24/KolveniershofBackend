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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DagPlanningController : ControllerBase
    {
        private readonly IDagPlanningTemplateRepository _dagPlanningRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dagPlanningRepository"></param>
        /// <param name="dagAtelierRepository"></param>
        /// 
        public DagPlanningController(IDagPlanningTemplateRepository dagPlanningRepository)
        {
            _dagPlanningRepository = dagPlanningRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        [HttpGet("{datum}")]
        [Authorize(Policy = "AdminOnly")]
        [Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagplanningDTO> GetDagPlanning(string datum)
        {
            //identity

            //date object kan niet meegegeven worden via parameter
            //dit zet string om naar datum --> zo doen of datum gewoon als string opslaan?
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DagPlanning dagplanning = _dagPlanningRepository.GetBy(datumFormatted);
            DagplanningDTO dto;
            var weekdag = (int)datumFormatted.DayOfWeek - 1;
            if (weekdag == (int)Weekdag.Undefined)
            {
                weekdag = (int)Weekdag.Maandag;
            };
            if (dagplanning == null)
            {
                DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datumFormatted);
                if (day >= DayOfWeek.Tuesday && day <= DayOfWeek.Thursday)
                {
                    datumFormatted = datumFormatted.AddDays(3);
                }

                // Return the week of our adjusted day
                var weeknummer = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(datumFormatted, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Tuesday);
                var weeknummerDagPlanning = weeknummer % 4;

                DagPlanningTemplate dagplaningTemplate = _dagPlanningRepository.GetBy(weeknummerDagPlanning, (Weekdag)weekdag);
                dto = new DagplanningDTO()
                {
                    DagplanningId = dagplaningTemplate.DagplanningId,
                    Eten = null,
                    Weekdag = dagplaningTemplate.Weekdag,
                    Weeknummer = dagplaningTemplate.Weeknummer,
                    DagAteliers = dagplaningTemplate.DagAteliers.Select(t => new DagAtelierDTO()
                    {
                        Atelier = new AtelierDTO()
                        {
                            AtelierType = t.Atelier.AtelierType,
                            Naam = t.Atelier.Naam,
                            PictoURL = t.Atelier.PictoURL
                        },
                        DagAtelierId = t.DagAtelierId,
                        DagMoment = t.DagMoment,
                        Gebruikers = t.GebruikerDagAteliers.Select(gda => new BasicGebruikerDTO()
                        {
                            Id = gda.Gebruiker.Id,
                            Achternaam = gda.Gebruiker.Achternaam,
                            Voornaam = gda.Gebruiker.Voornaam,
                            Foto = gda.Gebruiker.Foto,
                            Type = gda.Gebruiker.Type,
                        })
                    })
                };

            }
            else
            {
                dto = new DagplanningDTO()
                {
                    DagplanningId = dagplanning.DagplanningId,
                    Datum = dagplanning.Datum,
                    Eten = dagplanning.Eten,
                    Weekdag = dagplanning.Weekdag,
                    Weeknummer = dagplanning.Weeknummer,

                    DagAteliers = dagplanning.DagAteliers.Select(da => new DagAtelierDTO()
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
                        Gebruikers = da.GebruikerDagAteliers.Select(gda => new BasicGebruikerDTO()
                        {
                            Id = gda.Gebruiker.Id,
                            Achternaam = gda.Gebruiker.Achternaam,
                            Voornaam = gda.Gebruiker.Voornaam,
                            Foto = gda.Gebruiker.Foto,
                            Type = gda.Gebruiker.Type,
                        })
                    }),
                };
            }
            return dto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dagPlanning"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        [Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagPlanningTemplate> PutDagPlanning(int id, DagPlanningTemplate dagPlanning)
        {
            if (id != dagPlanning.DagplanningId)
                return BadRequest();
            _dagPlanningRepository.Update(dagPlanning);
            _dagPlanningRepository.SaveChanges();
            return NoContent();
        }

        [HttpPost("{datumVanDagplanning}")]
        public ActionResult<DagPlanning> DeleteDagAtelierUitDagplanning(string datumVanDagplanning, DagAtelier dagAtelier)
        {
            DateTime datumFormatted = DateTime.Parse(datumVanDagplanning, null, System.Globalization.DateTimeStyles.RoundtripKind);
            var planning = _dagPlanningRepository.GetBy(datumFormatted);
            DagAtelier da = planning.DagAteliers.FirstOrDefault(d => d.DagAtelierId == dagAtelier.DagAtelierId);
            planning.VerwijderDagAtlierVanDagPlanningTemplate(da);
            _dagPlanningRepository.Update(planning);
            _dagPlanningRepository.SaveChanges();
            return CreatedAtAction(nameof(GetDagPlanning), new { datum = planning.Datum }, planning);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dagPlanning"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<DagPlanningTemplate> PostDagplanning(DagPlanning dagPlanning)
        {
            //Wordt DTO wss

            //Identity
            _dagPlanningRepository.Add(dagPlanning);
            _dagPlanningRepository.SaveChanges();
            return CreatedAtAction(nameof(GetDagPlanning), new { datum = dagPlanning.Datum }, dagPlanning);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete("{id}")]
        //public ActionResult<DagPlanningTemplate> DeleteDagPlanning(int id)
        //{
        //    //identity
        //    DagPlanningTemplate dp = _dagPlanningRepository.GetBy(id);
        //    if (dp == null)
        //    {
        //        return NotFound();
        //    }
        //    _dagPlanningRepository.Delete(dp);
        //    _dagPlanningRepository.SaveChanges();
        //    return dp;
        //}
    }
}
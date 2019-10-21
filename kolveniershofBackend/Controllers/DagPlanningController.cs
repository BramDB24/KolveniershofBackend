﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
using kolveniershofBackend.Models;
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
    [AllowAnonymous]
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
        public ActionResult<DagplanningDTO> GetDagPlanning(string datum)
        {
            //identity

            //date object kan niet meegegeven worden via parameter
            //dit zet string om naar datum --> zo doen of datum gewoon als string opslaan?
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind); 
            DagPlanning dagplanning = _dagPlanningRepository.GetBy(datumFormatted);
            if (dagplanning == null)
                return NotFound();
            Console.WriteLine(dagplanning.DagplanningId);

            var dto = new DagplanningDTO()
            {
                DagplanningId = dagplanning.DagplanningId,
                Datum = dagplanning.Datum,
                Eten = dagplanning.Eten,
                Weekdag = dagplanning.Weekdag,
                Weeknummer = dagplanning.Weeknummer,

                DagAteliers = dagplanning.DagAteliers.Select(da => new DagAtelierDTO()
                {
                    Atelier = new AtelierDTO() {
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
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dagPlanning"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
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
        public ActionResult<DagPlanningTemplate> PostDagplanning(DagPlanning dagPlanning) {

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
        [HttpDelete("{id}")]
        public ActionResult<DagPlanningTemplate> DeleteDagPlanning(int id)
        {
            //identity
            DagPlanningTemplate dp = _dagPlanningRepository.GetBy(id);
            if (dp == null)
            {
                return NotFound();
            }
            _dagPlanningRepository.Delete(dp);
            _dagPlanningRepository.SaveChanges();
            return dp;
        }
    }
}
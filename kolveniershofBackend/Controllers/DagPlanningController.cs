﻿using System;
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
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DagPlanning dagplanning = _dagPlanningTemplateRepository.GetByDatum(datumFormatted);
            if (dagplanning != null)
            {
                return MaakDagPlanningDto(dagplanning);
            }

            // Eerst zoeken we naar de juiste template om te gebruiken. Hiervoor hebben we een weekdag en een weeknummer nodig.

            //Is nodig om te kunnen werken met de enums, de enum heeft (voor whatever reason) een undefined nodig die index 0 heeft, terwijl index 0 gewoon maandag moet zijn
            var weekdag = (int)datumFormatted.DayOfWeek - 1;

            if (weekdag == (int)Weekdag.Undefined)
            {
                weekdag = (int)Weekdag.Maandag;
            }
            
            /**
             * We kunnen de dagplanning van vandaag gebruiken om te controleren hoe veel weken de datum van de parameter
             * verschilt met de datum van vandaag
             */
            DagPlanning dagplanningVandaag = _dagPlanningTemplateRepository.GetByDatumGeenInclude(DateTime.Today);
            if (dagplanningVandaag == null)
            {
                /**
                 * Als er nog geen dagplanning is voor vandaag dan gaan we er een aanmaken met 
                 * de week waar we vandaag in zitten als de start van de 4 weekse planning
                 */

                // bereken de weekdag enum voor de datum van vandaag
                var weekdagVandaag = (int)datumFormatted.DayOfWeek - 1;

                if (weekdagVandaag == (int)Weekdag.Undefined)
                {
                    weekdagVandaag = (int)Weekdag.Maandag;
                };
                // maak een nieuw dagplanning vandaag en start vanaf de eerste week
                // dit wordt ook aangemaakt aangezien we de dag van vandaag als referentie gebruiken om de juiste template te vinden
                DagPlanningTemplate dagPlanningTemplateVandaag = GeefDagPlanningTemplate(1, weekdagVandaag);
                dagplanningVandaag = new DagPlanning(dagPlanningTemplateVandaag, DateTime.Today);
                _dagPlanningTemplateRepository.AddDagPlanning(dagplanningVandaag);
            }

            /**
             * Om het aantal weken te berekenen converteren we eerst de datum van de parameter en de data van vandaag
             * naar de maandag van de week waar ze in zitten.
             * Hierdoor kunnen we het aantal weken berekenen door het verschil in dagen te nemen en dit te delen door 7.
             */
            var weeknummerVandaag = dagplanningVandaag.Weeknummer;
            var maandagDezeWeek = MaandagVanWeek(DateTime.Today);
            var maandagGegevenWeek = MaandagVanWeek(datumFormatted);
            var aantalWekenVerschil = (maandagGegevenWeek - maandagDezeWeek).Days / 7;
            int weeknummer = (weeknummerVandaag + aantalWekenVerschil) % 4;

            DagPlanningTemplate dagPlanningTemplate = GeefDagPlanningTemplate(weeknummer, weekdag);
            
            
            // Aangezien we nu de template hebben kunnen we een nieuwe dagplanning voor de opgegeven datum
            dagplanning = new DagPlanning(dagPlanningTemplate, datumFormatted);
            _dagPlanningTemplateRepository.AddDagPlanning(dagplanning);
            _dagPlanningTemplateRepository.SaveChanges();
            return MaakDagPlanningDto(dagplanning);
        }

        /**
         * Geeft de DagPlanningTemplate van een bepaald weeknummer en weekdag.
         * Als de DagPlanningTemplate niet bestaat in de databank dan wordt hij gegenereerd.
         */
        [HttpGet("vanWeek/{weeknummer}/vanDag/{weekdag}")]
        public ActionResult<DagplanningDTO> GetDagPlanningTemplate(int weeknummer, int weekdag)
        {
            DagPlanningTemplate dagPlanningTemplate = GeefDagPlanningTemplate(weeknummer, weekdag);
            DagplanningDTO dagPlanningTemplateDto = MaakDagPlanningDto(new DagPlanning(dagPlanningTemplate, DateTime.Today));
            dagPlanningTemplateDto.DagplanningId = dagPlanningTemplate.DagplanningId;
            return dagPlanningTemplateDto;
        }

        /// <summary>
        /// 
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

        [HttpPost("{datumVanDagplanning}")]
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
        private IEnumerable<DagAtelierDTO> SetDagAteliers(DagPlanning dagPlanning)
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
        private DateTime MaandagVanWeek(DateTime datum)
        {
            //-1 toegevoegd aangezien DayOfWeek begint op zondag en niet op maandag
            //+7 toegevoegd voor het geval dat datum een zondag is, zonder +7 is het resultaat -1 maar we hebben een 6 nodig
            int dagenVerwijderedVanMaandag = ((int)datum.DayOfWeek - 1 + 7) % 7;
            return new DateTime(datum.Year, datum.Month, datum.Day - dagenVerwijderedVanMaandag);
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
    }
}
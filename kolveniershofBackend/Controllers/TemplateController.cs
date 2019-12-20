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
using Newtonsoft.Json.Linq;

namespace kolveniershofBackend.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    //[Authorize(Policy = "AdminOnly")]
    //[Authorize(Policy = "BegeleidersOnly")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TemplateController : ControllerBase
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
        public TemplateController(IDagPlanningTemplateRepository dagPlanningTemplateRepository, IGebruikerRepository gebruikerRepository, IAtelierRepository atelierRepository, ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
            _dagPlanningTemplateRepository = dagPlanningTemplateRepository;
            _gebruikerRepository = gebruikerRepository;
            _atelierRepository = atelierRepository;
        }


        [HttpGet]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public IEnumerable<Template> GetTemplates()
        {
            var templates = _templateRepository.GetAll();
            return templates;
        }


        [HttpGet("{id}")]
        public ActionResult<Template> GetTemplateById(int id)
        {
            var template = _templateRepository.GetTemplateById(id);
            if (template == null)
                return BadRequest();
            return template;
        }

        [HttpPost]
        public ActionResult<Template> PostTemplate(TemplateDTO templatedto)
        {
            var naam = templatedto.Naam;
            if(naam == null || naam == "")
            {
                return BadRequest();
            }
            var template = new Template(naam);
            _templateRepository.AddTemplate(template);
            _templateRepository.SaveChanges();
            return CreatedAtAction(nameof(GetTemplateById), new { id = template.Id }, GetTemplateById(template.Id).Value);
        }

        /**
         * Geeft de DagPlanningTemplate van een bepaald weeknummer en weekdag.
         * Als de DagPlanningTemplate niet bestaat in de databank dan wordt hij gegenereerd.
         */
        [HttpGet("{id}/Week/{weeknummer}/Dag/{weekdag}")]
        public ActionResult<DagplanningDTO> GetDagPlanningTemplate(int id, int weeknummer, int weekdag)
        {
            DagPlanningTemplate dagPlanningTemplate = null;
            dagPlanningTemplate = _templateRepository.GetDagTemplateById(id, weeknummer, (Weekdag)weekdag);
            if (dagPlanningTemplate == null)
                return BadRequest();
            DagplanningDTO dagPlanningTemplateDto = new DagplanningDTO()
            {
                DagplanningId = dagPlanningTemplate.DagplanningId,
                Eten = null,
                Weekdag = dagPlanningTemplate.Weekdag,
                Weeknummer = dagPlanningTemplate.Weeknummer,
                Datum = null,
                DagAteliers = dagPlanningTemplate.DagAteliers.Select(da => new DagAtelierDTO()
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
                })
            };
            return dagPlanningTemplateDto;
        }

        [HttpPut("{naam}/week/{weeknr}/dag/{weekdag}/dagateliers")]
        public ActionResult<DagPlanning> DeleteDagAtelierUitDagplanningTemplate(string naam, int weeknr, int weekdag, DagAtelierDTO dagAtelier)
        {
            DagPlanningTemplate dagPlanningTemplate = _templateRepository.GetDagTemplateByNaam(naam, weeknr, (Weekdag)weekdag);
            DagAtelier da = dagPlanningTemplate.DagAteliers.FirstOrDefault(d => d.DagAtelierId == dagAtelier.DagAtelierId);
            dagPlanningTemplate.VerwijderDagAtlierVanDagPlanningTemplate(da);
            _dagPlanningTemplateRepository.Update(dagPlanningTemplate);
            _dagPlanningTemplateRepository.SaveChanges();
            return CreatedAtAction(nameof(GetDagPlanningTemplate), new { week = dagPlanningTemplate.Weeknummer, weekdag = dagPlanningTemplate.Weekdag },
                dagPlanningTemplate);
        }

        [HttpDelete("{id}")]
        public ActionResult<Template> DeleteTemplate(int id)
        {
            Template template = _templateRepository.GetTemplateById(id);
            if (template == null && !template.IsActief)
            {
                return NotFound();
            }
            _templateRepository.Delete(template);
            _templateRepository.SaveChanges();
            return template;
        }

        [HttpPut("{id}")]
        public ActionResult<TemplateDTO> PutTemplate(int id, TemplateDTO template)
        {
            if (id != template.Id)
                return BadRequest();
            Template newTemplate = _templateRepository.GetFullTemplateById(id);
            if (newTemplate == null || newTemplate.IsActief)
            {
                return BadRequest();
            }
            Template oldTemplate = _templateRepository.GetFullActiveTemplate();
            if (oldTemplate != null)
            {
                oldTemplate.SwitchStatus();
                _templateRepository.Update(oldTemplate);
            }
            newTemplate.SwitchStatus();
            _templateRepository.Update(oldTemplate);
            _templateRepository.SaveChanges();
            return NoContent();
        }
    }
}
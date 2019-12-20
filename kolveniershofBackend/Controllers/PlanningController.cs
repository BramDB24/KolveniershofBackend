using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace kolveniershofBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PlanningController : ControllerBase
    {
        private readonly IDagPlanningTemplateRepository _dagPlanningTemplateRepository;
        private readonly IAtelierRepository _atelierRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public PlanningController(IDagPlanningTemplateRepository dagplanningRepository, IAtelierRepository atelierRepository, IGebruikerRepository gebruikerRepository)
        {
            _dagPlanningTemplateRepository = dagplanningRepository;
            _atelierRepository = atelierRepository;
            _gebruikerRepository = gebruikerRepository;
        }

        //Heeft deze methode nog nut?
        [HttpPut]
        public ActionResult PutDagplanning(DagplanningDTO dto)
        {
            var template = _dagPlanningTemplateRepository.GetTemplateByWeeknummerEnDagnummer(dto.Weeknummer, dto.Weekdag);

            dto.DagAteliers.ToList().ForEach(t =>
            {
                var atelier = _atelierRepository.getBy(t.Atelier.AtelierId);
                DagAtelier dagAtelier = new DagAtelier
                {
                    Atelier = atelier,
                    DagAtelierId = t.DagAtelierId,
                    DagMoment = t.DagMoment
                };

                t.Gebruikers.ToList().ForEach(e => dagAtelier.VoegGebruikerAanDagAtelierToe(_gebruikerRepository.GetBy(e.Id)));
                template.DagAteliers.Add(dagAtelier);
                var d = template.DagAteliers;
            });

            _dagPlanningTemplateRepository.SaveChanges();

            return Ok();
            
        }


    }
}
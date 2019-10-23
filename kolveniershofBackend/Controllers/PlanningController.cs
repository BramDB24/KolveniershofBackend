using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace kolveniershofBackend.Controllers
{
    public class PlanningController : ControllerBase
    {
        private readonly IDagPlanningTemplateRepository _dagPlanningTemplateRepository;

        public PlanningController(IDagPlanningTemplateRepository repo)
        {
            _dagPlanningTemplateRepository = repo;
        }

        public ActionResult PostDagplanning(ICollection<DagplanningDTO> dto)
        {
            dto.ToList().ForEach(e =>
            {
                var local = _dagPlanningTemplateRepository.GetBy(e.Weeknummer, e.Weekdag);
                e.DagAteliers.ToList().ForEach(t =>
                {
                    //local.VoegDagAtelierToeAanDagPlaningTemplate(t);
                    
                });
            });
            return Ok();
        }


    }
}
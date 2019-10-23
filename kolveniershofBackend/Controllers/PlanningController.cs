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

        public ActionResult PostDagplanning(DagplanningDTO dto)
        {

            var template = _dagPlanningTemplateRepository.GetBy(dto.Weeknummer, dto.Weekdag);
            //template.DagAteliers = dto.DagAteliers;
            return Ok();
        }


    }
}
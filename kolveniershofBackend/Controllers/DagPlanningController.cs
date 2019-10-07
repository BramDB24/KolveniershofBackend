using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kolveniershofBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DagPlanningController : ControllerBase
    {
        private readonly IDagPlanningTemplateRepository _dagPlanningRepository;

        public DagPlanningController(IDagPlanningTemplateRepository dagPlanningRepository)
        {
            _dagPlanningRepository = dagPlanningRepository;
        }

        [HttpGet("{datum}")]
        public ActionResult<DagPlanning> GetDagPlanning(string datum)
        {
            //identity

            //date object kan niet meegegeven worden via parameter
            //dit zet string om naar datum --> zo doen of datum gewoon als string opslaan?
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind); 
            DagPlanning dp = _dagPlanningRepository.GetBy(datumFormatted);
            if (dp == null)
                return NotFound();
            return dp;
        }

        [HttpPut("{id}")]
        public ActionResult<DagPlanningTemplate> PutDagPlanning(int id, DagPlanning dagPlanning)
        {
            if (id != dagPlanning.Id)
                return BadRequest();
            _dagPlanningRepository.Update(dagPlanning);
            _dagPlanningRepository.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public ActionResult<DagPlanningTemplate> PostDagplanning(DagPlanning dagPlanning) {
            //Wordt DTO wss

            //Identity
            _dagPlanningRepository.Add(dagPlanning);
            _dagPlanningRepository.SaveChanges();
            return CreatedAtAction(nameof(GetDagPlanning), new { datum = dagPlanning.Datum }, dagPlanning);
            
        }
       

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
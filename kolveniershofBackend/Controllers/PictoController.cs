using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.DTO.Picto;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kolveniershofBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictoController : ControllerBase
    {
        private readonly IDagPlanningTemplateRepository _dagPlanningTemplateRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public PictoController(IDagPlanningTemplateRepository dagPlanningTemplateRepository, IGebruikerRepository repo)
        {
            _dagPlanningTemplateRepository = dagPlanningTemplateRepository;
            _gebruikerRepository = repo;
        }

        [HttpGet]
        public ActionResult<object> GetWeekPicto(string gebruikerId, string time)
        {
            DateTime datumFormatted = DateTime.Parse(time, null, System.Globalization.DateTimeStyles.RoundtripKind);
            int daysBack = 0;
            int daysForward = 0;

            Gebruiker gebruiker = _gebruikerRepository.GetBy(gebruikerId);

            var obj = _dagPlanningTemplateRepository.GetByDatum(datumFormatted);
            switch (datumFormatted.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    daysForward = 5;
                    break;
                case DayOfWeek.Tuesday:
                    daysForward = 4;
                    daysBack = 1;
                    break;
                case DayOfWeek.Wednesday:
                    daysForward = 3;
                    daysBack = 2;
                    break;
                case DayOfWeek.Thursday:
                    daysForward = 1;
                    daysBack = 4;
                    break;
                case DayOfWeek.Friday:
                    daysBack = 5;
                    break;
                default: daysBack = 5; break;
            }

            List<DateTime> datesToCheck = new List<DateTime>();
            for (int i = 1; i <= daysBack; i++)
            {
                var dateStore = datumFormatted.AddDays(i * -1);
                datesToCheck.Add(dateStore);
            }

            datesToCheck.Add(datumFormatted);


            for (int i = 1; i < daysForward; i++)
            {
                var dateStore = datumFormatted.AddDays(i);
                datesToCheck.Add(dateStore);
            }

            List<DagPlanning> dagplanning = new List<DagPlanning>();
            datesToCheck.ForEach(date => {
                DagPlanning dp = _dagPlanningTemplateRepository.GetByDatum(date);
                if(dp == null)
                {
                    
                }
                dagplanning.Add(_dagPlanningTemplateRepository.GetByDatum(date));
            });

            List<DagAtelier> dagateliers = new List<DagAtelier>();
            dagplanning.ForEach(element => dagateliers.AddRange(element.GetDagAteliersGebruiker(gebruikerId)));

            List<Atelier> ateliers = new List<Atelier>();
            dagateliers.ForEach(element => ateliers.Add(element.Atelier));
            return new OkObjectResult(ateliers);
        }

        private DateTime DinsdagVanWeek(DateTime datum)
        {
            //-2 toegevoegd aangezien DayOfWeek begint op zondag en niet op dinsdag
            //+7 toegevoegd voor het geval dat datum een zondag is, zonder +7 is het resultaat -1 maar we hebben een 6 nodig
            int dagenVerwijderedVanDinsdag = ((int)datum.DayOfWeek - 2 + 7) % 7;
            return new DateTime(datum.Year, datum.Month, datum.Day).AddDays(dagenVerwijderedVanDinsdag * -1);
        }
    }
}
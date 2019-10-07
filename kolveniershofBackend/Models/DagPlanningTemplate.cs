using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagPlanningTemplate
    {
        public int Id { get; set; }
        public List<DagAtelier> DagAteliers { get; set; }
        public int Weeknummer { get; set; }
        public DayOfWeek Weekdag { get; set; }
        public bool IsTemplate { get; set; }


        public DagPlanningTemplate()
        {
            DagAteliers = new List<DagAtelier>();
        }
    }
}

using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public bool IsActief { get; set; }
        public IEnumerable<DagPlanningTemplate> DagPlanningTemplates { get; set; }

        public Template()
        {
            DagPlanningTemplates = new List<DagPlanningTemplate>();
        }

        public Template(string naam)
        {
            Naam = naam;
            GenereerLegePlanning();
        }

        private void GenereerLegePlanning()
        {
            List<DagPlanningTemplate> dagplanningen = new List<DagPlanningTemplate>();
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    dagplanningen.Add(new DagPlanningTemplate(i, (Weekdag)j));
                }
            }
            DagPlanningTemplates = dagplanningen.AsEnumerable();
        }
    }
}

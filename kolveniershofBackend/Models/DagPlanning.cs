using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagPlanning : DagPlanningTemplate
    {   
        public DateTime Datum { get; set; }
        public string Eten { get; set; }
        public List<Opmerking> Opmerkingen { get; set; }

        public DagPlanning()
        {
            Opmerkingen = new List<Opmerking>();
        }

        public DagPlanning(DateTime datum) : base()
        {
            this.Datum = datum;
        }
        
    }

    
}

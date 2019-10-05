using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagPlanning
    {
        public int Id { get; set; }
        public int Weeknummer { get; set; }
        public DateTime Datum { get; set; }
        public DayOfWeek Weekdag => Datum.DayOfWeek;
        public string Eten { get; set; }
        public List<Opmerking> Opmerkingen { get; set; }
        public List<DagAtelier> DagAteliers { get; set; }


        public DagPlanning()
        {
            Opmerkingen = new List<Opmerking>();
            DagAteliers = new List<DagAtelier>();
        }

        public DagPlanning(DateTime datum) : base()
        {
            this.Datum = datum;
        }
        
    }

    
}

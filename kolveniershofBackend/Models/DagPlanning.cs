using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagPlanning
    {
        public int Weeknummer { get; set; }
        public DateTime Datum { get; set; }
        public DayOfWeek weekdag { get; set; }
        public String Eten { get; set; }
        public List<Opmerking> Opmerkingen { get; set; }
        public List<DagAtelier> DagAteliers { get; set; }
    }
}

using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagAtelier
    {
        public int DagAtelierId { get; set; }
        public List<GebruikerAtelier> GebruikerAtelier { get; set; }
        public DagMoment DagMoment { get; set; }
        public Atelier Atelier { get; set; }

        public DagAtelier()
        {
            GebruikerAtelier = new List<GebruikerAtelier>();
        }
    }
}

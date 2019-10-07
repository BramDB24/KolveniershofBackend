using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class GebruikerAtelier
    {
        public string GebruikerId { get; set; }
        public int DagAtelierId { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public DagAtelier DagAtelier { get; set; }

        protected GebruikerAtelier()
        {

        }

        public GebruikerAtelier(Gebruiker gebruiker, DagAtelier dagAtelier)
        {
            Gebruiker = gebruiker;
            DagAtelier = dagAtelier;
            GebruikerId = gebruiker.GebruikerId;
            DagAtelierId = dagAtelier.DagAtelierId;
        }
    }


}

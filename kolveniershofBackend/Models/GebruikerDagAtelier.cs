using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class GebruikerDagAtelier
    {
        public string GebruikerId { get; set; }
        public int DagAtelierId { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public DagAtelier DagAtelier { get; set; }

        protected GebruikerDagAtelier()
        {

        }

        public GebruikerDagAtelier(Gebruiker gebruiker, DagAtelier dagAtelier)
        {
            Gebruiker = gebruiker;
            DagAtelier = dagAtelier;
            GebruikerId = gebruiker.GebruikerId;
            DagAtelierId = dagAtelier.DagAtelierId;
        }
    }
}



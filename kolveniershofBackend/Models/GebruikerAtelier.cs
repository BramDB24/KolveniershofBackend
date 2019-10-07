using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class GebruikerAtelier
    {
        public string GebruikerId { get; set; }
        public int AtelierId { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public Atelier Atelier { get; set; }

        protected GebruikerAtelier()
        {

        }

        public GebruikerAtelier(Gebruiker gebruiker, Atelier atelier)
        {
            Gebruiker = gebruiker;
            Atelier = atelier;
            GebruikerId = gebruiker.GebruikerId;
            AtelierId = atelier.AtelierId;
        }
    }


}

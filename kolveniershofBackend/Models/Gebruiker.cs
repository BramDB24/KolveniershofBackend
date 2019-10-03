using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Gebruiker
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public Gebruiker(String naam)
        {
            this.Naam = naam;
        }
    }
}

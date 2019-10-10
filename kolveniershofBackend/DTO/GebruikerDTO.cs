using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class GebruikerDTO
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public List<Commentaar> Commentaren { get; set; }
        public string Email { get; set; }
        public Sfeergroep Sfeergroep { get; set; }
        public string Straatnaam { get; set; }
        public string Huisnummer { get; set; }
        public string Busnummer { get; set; }
        public string Gemeente { get; set; }
        public string Postcode { get; set; }
        public GebruikerType Type { get; set; }

        public GebruikerDTO(Gebruiker g)
        {
            this.Voornaam = g.Voornaam;
            this.Achternaam = g.Achternaam;
            this.Commentaren = g.Commentaren;
            this.Email = g.Email;
            this.Sfeergroep = g.Sfeergroep;
            this.Straatnaam = g.Straatnaam;
            this.Huisnummer = g.Huisnummer;
            this.Busnummer = g.Busnummer;
            this.Gemeente = g.Gemeente;
            this.Postcode = g.Postcode;
            this.Type = g.Type;





        }
    }
}

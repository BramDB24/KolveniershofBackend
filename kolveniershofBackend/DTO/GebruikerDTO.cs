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
        public string Foto { get; set; }

        public GebruikerDTO(Gebruiker g)
        {
            Voornaam = g.Voornaam;
            Achternaam = g.Achternaam;
            Commentaren = g.Commentaren;
            Email = g.Email;
            Sfeergroep = g.Sfeergroep;
            Straatnaam = g.Straatnaam;
            Huisnummer = g.Huisnummer;
            Busnummer = g.Busnummer;
            Gemeente = g.Gemeente;
            Postcode = g.Postcode;
            Type = g.Type;
            Foto = g.Foto;
        }
    }
}

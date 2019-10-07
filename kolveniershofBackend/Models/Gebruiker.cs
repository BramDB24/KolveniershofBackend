using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Gebruiker
    {
        public string GebruikerId { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public Sfeergroep Sfeergroep { get; set; }
        public string Foto { get; set; }
        public string Straatnaam { get; set; }
        public int Huisnummer { get; set; }
        public string Busnummer { get; set; }
        public string Gemeente { get; set; }
        public int Postcode { get; set; }
        public List<Commentaar> Commentaren { get; set; }
        public GebruikerType GebruikerType { get; set; }

        public Gebruiker()
        {
            Commentaren = new List<Commentaar>();
        }

        public Gebruiker(string voornaam, string familienaam, string email, Sfeergroep sf, string foto, 
            string straat, int huisnr, string busnr, string gemeente, int postcode, GebruikerType gebruikerType):this()
        {
            Voornaam = voornaam;
            Achternaam = familienaam;
            Email = email;
            Sfeergroep = sf;
            Foto = foto;
            Straatnaam = straat;
            Huisnummer = huisnr;
            Busnummer = busnr;
            Gemeente = gemeente;
            Postcode = postcode;
            GebruikerType = gebruikerType;
        }
    }
}

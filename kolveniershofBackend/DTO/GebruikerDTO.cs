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
        public string GebruikerId { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public List<Commentaar> Commentaren { get; set; }
        public string Email { get; set; }
        public Sfeergroep Sfeergroep { get; set; }
        public GebruikerType Type { get; set; }
        public string Foto { get; set; }

        public GebruikerDTO(Gebruiker g)
        {
            GebruikerId = g.Id;
            Voornaam = g.Voornaam;
            Achternaam = g.Achternaam;
            Commentaren = g.Commentaren;
            Email = g.Email;
            Sfeergroep = g.Sfeergroep;
            Type = g.Type;
            Foto = g.Foto;
        }
    }
}

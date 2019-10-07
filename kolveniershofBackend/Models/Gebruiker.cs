﻿using kolveniershofBackend.Enums;
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
        public int Busnummer { get; set; }
        public string Gemeente { get; set; }
        public int Postcode { get; set; }
        public List<Commentaar> Commentaren { get; set; }

        public Gebruiker()
        {
        
        }

        public Gebruiker(string name)
        {
            this.Voornaam = name;
        }
    }
}

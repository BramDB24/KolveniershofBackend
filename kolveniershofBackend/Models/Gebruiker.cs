using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Gebruiker
    {

        private string _voornaam;
        private string _achternaam;
        private string _postcode;
        private string _email;

        public string GebruikerId { get; set; }
        public string Voornaam {
            get {
                return _voornaam;
            }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een voornaam hebben");
                _voornaam = value;
            }
        }
        public string Achternaam {
            get {
                return _achternaam;
            }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een achternaam hebben");
                _achternaam = value;
            }
        }


        public string Email {
            get {
                return _email;
            }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een email hebben");
                else if (!Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                    throw new ArgumentException("Ongeldig email formaat");
                _email = value;
            }
        }
        public Sfeergroep Sfeergroep { get; set; }
        public string Foto { get; set; }
        public string Straatnaam { get; set; }
        public string Huisnummer { get; set; }
        public string Busnummer { get; set; }
        public string Gemeente { get; set; }
        public string Postcode {
            get {
                return _postcode;
            }
            set {
                if (value.Length != 4)
                    throw new ArgumentException("Een postcode moet uit 4 getallen bestaan");
                _postcode = value;
            }
        }

        public GebruikerType Type { get; set; }

        public List<Commentaar> Commentaren { get; set; }

        public Gebruiker() {
            Commentaren = new List<Commentaar>();
        }

        public Gebruiker(string voornaam, string achternaam, string email, Sfeergroep sfeergroep, string foto, 
            string straatnaam, string huisnummer, string busnummer, string gemeente, string postcode, GebruikerType type) : this()
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            Email = email;
            Sfeergroep = sfeergroep;
            Foto = foto;
            Straatnaam = straatnaam;
            Huisnummer = huisnummer;
            Busnummer = busnummer;
            Gemeente = gemeente;
            Postcode = postcode;
            Type = type;
        }

        public void addCommentaar(Commentaar commentaar) //commentaar object meegeven of objecten om commentaar mee te creëren meegeven
        {
            Commentaren.Add(commentaar);
        }
    }
}

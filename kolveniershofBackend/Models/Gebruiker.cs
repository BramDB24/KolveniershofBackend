using kolveniershofBackend.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Gebruiker: IdentityUser
    {

        private string _voornaam;
        private string _achternaam;
        private string _email;
        private string _foto;
        private string _straatnaam;
        private string _huisnummer;
        private string _busnummer;
        private string _gemeente;
        private string _postcode;
        private GebruikerType _gebruikerType;

        public List<Commentaar> Commentaren { get; set; }

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


        public override string Email {
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

        public string Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een foto hebben");
                _foto = value;
            }
        }

        public string Straatnaam
        {
            get
            {
                return _straatnaam;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een straat opgeven");
                _straatnaam = value;
            }
        }

        public string Huisnummer
        {
            get
            {
                return _huisnummer;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[0-9]{1,4}[a-zA-Z]{0,2}$", RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException("Ongeldig huisnummer");
                }
                else
                {
                    _huisnummer = value;
                }
            }
        }

        public string Busnummer
        {
            get
            {
                return _busnummer;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _busnummer = null;
                }
                else if (!Regex.IsMatch(value, @"^$|^[A-Za-z0-9 \\.]*[A-Za-z0-9][A-Za-z0-9 \\.]*$", RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException("Ongeldig busnummer");
                }
                else
                {
                    _busnummer = value;
                }
            }
        }

        public string Gemeente
        {
            get
            {
                return _gemeente;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een gemeente opgeven");
                _gemeente = value;
            }
        }

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

        public GebruikerType Type
        {
            get { return _gebruikerType; }
            set
            {
                if (value == GebruikerType.Undefined)
                {
                    throw new ArgumentException("Selecteer het soort gebruiker");
                }
                else
                {
                    _gebruikerType = value;
                }
            }
        }


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
            UserName = email;
        }

        public void addCommentaar(Commentaar commentaar) //commentaar object meegeven of objecten om commentaar mee te creëren meegeven
        {
            Commentaren.Add(commentaar);
        }

        public IEnumerable<Commentaar> GeefCommentaarVanHuidigeGebruiker()
        {
            return Commentaren.OrderBy(c=>c.Datum);
        }
    }
}

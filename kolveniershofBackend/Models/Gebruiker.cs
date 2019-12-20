using kolveniershofBackend.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        private GebruikerType _gebruikerType;

        public List<Commentaar> Commentaren { get; set; }
        public IdentityUserClaim<string> Role { get; set; }

        
        override public string Id { get; set; }
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

        [JsonConverter(typeof(StringEnumConverter))]
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

        [JsonConverter(typeof(StringEnumConverter))]
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

        public Gebruiker(string voornaam, string achternaam, string email, Sfeergroep sfeergroep, string foto, GebruikerType type) : this()
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            Email = email;
            Sfeergroep = sfeergroep;
            Foto = foto;
            Type = type;
            UserName = email;
        }

        public void addCommentaar(Commentaar commentaar) //commentaar object meegeven of objecten om commentaar mee te creëren meegeven
        {
            Commentaren.Add(commentaar);
        }

    }
}

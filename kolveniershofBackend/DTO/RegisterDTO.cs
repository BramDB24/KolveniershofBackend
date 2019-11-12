using kolveniershofBackend.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        [StringLength(200)]
        public string Voornaam { get; set; }
        [Required]
        [StringLength(250)]
        public string Achternaam { get; set; }
        [Required(ErrorMessage = "Please enter your password again")]
        [Compare("Password", ErrorMessage = "Password and passwordconfirmation must be the same")]
        public string PasswordConfirmation { get; set; }
        [Required]
        [StringLength(200)]
        public string Gemeente { get; set; }
        [Required]
        [StringLength(4)]
        public string PostCode { get; set; }
        [Required]
        [StringLength(200)]
        public string Straatnaam { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression("^[0-9]{1,4}[a-zA-Z]{0,2}$", ErrorMessage = "Gelieve een geldig huisnummer op te geven")]
        public string Huisnummer { get; set; }
        [RegularExpression("^$|^[A-Za-z0-9 \\.]*[A-Za-z0-9][A-Za-z0-9 \\.]*$", ErrorMessage = "Gelieve een geldig huisnummer op te geven")]
        public string Busnummer { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Sfeergroep Sfeergroep { get; set; }
        [Required]
        public string Foto { get; set; }
        [Required]
        public GebruikerType Type {get; set; }
    }
}

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
        [JsonConverter(typeof(StringEnumConverter))]
        public Sfeergroep Sfeergroep { get; set; }
        [Required]
        public string Foto { get; set; }
        [Required]
        public GebruikerType Type {get; set; }
    }
}

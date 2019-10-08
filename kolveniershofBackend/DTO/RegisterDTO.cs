using kolveniershofBackend.Enums;
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
        [Required]
        [Compare("Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
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
        public Sfeergroep Sfeergroep { get; set; }
        [Required]
        public string Foto { get; set; }
        [Required]
        public GebruikerType Type {get; set; }
    }
}

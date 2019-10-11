using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class BasicGebruikerDTO
    {
        public string Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public GebruikerType Type { get; set; }
        public string Foto { get; set; }


        public BasicGebruikerDTO()
        {

        } 
    }
}

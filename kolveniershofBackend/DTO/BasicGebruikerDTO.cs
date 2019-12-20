using kolveniershofBackend.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        [JsonConverter(typeof(StringEnumConverter))]
        public GebruikerType Type { get; set; }
        public string Foto { get; set; }


        public BasicGebruikerDTO()
        {

        } 
    }
}

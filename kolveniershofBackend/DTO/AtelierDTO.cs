using kolveniershofBackend.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class AtelierDTO
    { 
        public int AtelierId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AtelierType AtelierType { get; set; }
        public string Naam { get; set; }
        public string PictoURL { get; set; }

        public AtelierDTO()
        {

        }
    }
}

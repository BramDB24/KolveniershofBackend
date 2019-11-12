using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class OpmerkingDTO
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public OpmerkingType OpmerkingType { get; set; }
        public string Tekst { get; set; }
        public DateTime Datum { get; set; }

        public OpmerkingDTO()
        {

        }

        public Opmerking getOpmerking()
        {
            return new Opmerking(OpmerkingType, Tekst, Datum);
        }
    }
}

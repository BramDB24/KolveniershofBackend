using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class DagplanningDTO
    {
        public int DagplanningId { get; set; }
        public string Eten;
        public DateTime? Datum;
        public int Weeknummer;
        [JsonConverter(typeof(StringEnumConverter))]
        public Weekdag Weekdag;
        public IEnumerable<DagAtelierDTO> DagAteliers { get; set; }
        public string Commentaar { get; set; }

        //public List<Opmerking> Opmerkingen { get; set; }

        public DagplanningDTO()
        {

        }




    }
}

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
    public class DagAtelierDTO
    {
        public int DagAtelierId;
        [JsonConverter(typeof(StringEnumConverter))]
        public DagMoment DagMoment;
        public AtelierDTO Atelier;
        public IEnumerable<BasicGebruikerDTO> Gebruikers;

        public DagAtelierDTO() {

        }


    }
}

using kolveniershofBackend.Enums;
using System;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace kolveniershofBackend.DTO
{
    public class CommentaarDTO
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CommentaarType CommentaarType { get; set; }
        public string Tekst { get; set; }
        public DateTime Datum { get; set; }
    }
}

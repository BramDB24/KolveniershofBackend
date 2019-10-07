using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Opmerking
    {
        public int OpmerkingId { get; set; }
        public OpmerkingType Opmerkingtype { get; set; }
        public string Tekst { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Opmerking
    {

        public int Id { get; set; }
        public int Type { get; set; }

        public string Tekst { get; set; }
    }
}

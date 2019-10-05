using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Commentaar
    {
        public DateTime Datum { get; set; }
        public String tekst { get; set; }
        public String Type { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}

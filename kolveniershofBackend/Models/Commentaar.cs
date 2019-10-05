using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Commentaar
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
        public string Type { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}

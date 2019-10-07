using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Commentaar
    {
        public int CommentaarId { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
        public CommentaarType CommentaarType { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}

using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class CommentaarDTO
    {
        public CommentaarType CommentaarType { get; set; }
        public string Tekst { get; set; }
        public DateTime Datum { get; set; }
    }
}

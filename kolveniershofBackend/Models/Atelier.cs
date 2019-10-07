using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Atelier
    {
        public int AtelierId { get; set; }
        public string Naam { get; set; }
        public string PictoURL { get; set; }
        public AtelierType AtelierType { get; set; }
    }
}

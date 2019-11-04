using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO.Picto
{
    public class PictoDagDTO
    {
        public string Eten;
        public DateTime Datum;
        public IEnumerable<PictoAtelierDTO> Ateliers { get; set; }
    }
}

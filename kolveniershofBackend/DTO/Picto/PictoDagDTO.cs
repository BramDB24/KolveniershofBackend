using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO.Picto
{
    public class PictoDagDTO
    {
        public string Eten { get; set; }
        public DateTime Datum { get; set; }
        public IEnumerable<PictoAtelierDTO> Ateliers { get; set; }
        public string GebruikerId { get; set; }
    }
}

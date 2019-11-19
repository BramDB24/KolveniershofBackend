using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO.Picto
{
    public class PictoGebruikerDTO
    {
        public string GebruikerId { get; set; }
        public string Naam { get; set; }
        public string Foto { get; set; }

        public PictoGebruikerDTO()
        {

        }
    }
}

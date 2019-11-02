using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO.Picto
{
    public class PictoAtelierDTO
    {
        public string AtelierType { get; set; }
        public string AtelierImg { get; set; }
        public IEnumerable<string> BegeleiderImages { get; set; }
        public string dagMoment { get; set; }
    }
}

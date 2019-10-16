using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class DagplanningDTO
    {
        public int DagplanningId { get; set; }
        public string Eten;
        public DateTime? Datum;
        public int Weeknummer;
        public Weekdag Weekdag;
        //public List<Opmerking> Opmerkingen { get; set; }
        public IEnumerable<DagAtelierDTO> DagAteliers { get; set; }

        public DagplanningDTO()
        {

        }




    }
}

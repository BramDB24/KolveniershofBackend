using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.DTO
{
    public class DagAtelierDTO
    {
        public int DagAtelierId;
        public DagMoment DagMoment;
        public AtelierDTO Atelier;
        public IEnumerable<BasicGebruikerDTO> Gebruikers;
        public DagAtelierDTO() {

        }


    }
}

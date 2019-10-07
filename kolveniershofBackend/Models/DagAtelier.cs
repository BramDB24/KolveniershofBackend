using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagAtelier
    {
        public int DagAtelierId { get; set; }
        public DagMoment DagMoment { get; set; }
    }
}

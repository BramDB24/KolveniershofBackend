using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagAtelier
    {
        #region Fields
        private DagMoment _dagMoment; 
        #endregion
      
        #region Properties
        public int DagAtelierId { get; set; }

        public Atelier Atelier { get; set; }
        public int AtelierId { get; set; }

        public DagMoment DagMoment
        {
            get { return _dagMoment; }
            set
            {
                if (_dagMoment == DagMoment.Undefined)
                {
                    throw new ArgumentException("Selecteer de soort opmerking");
                }
                else
                {
                    _dagMoment = value;
                }
            }
        }

        public List<GebruikerDagAtelier> GebruikerDagAteliers { get; set; }

        #endregion

        public DagAtelier()
        {
            GebruikerDagAteliers = new List<GebruikerDagAtelier>();
        }

        public DagAtelier(DagMoment dagMoment, Atelier atelier): this()
        {
            DagMoment = dagMoment;
            Atelier = atelier;
        }
    }
}

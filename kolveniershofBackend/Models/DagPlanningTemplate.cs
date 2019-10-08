using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagPlanningTemplate
    {

        private int _weeknummer;
        private Weekdag _weekdag;

        public int DagplanningId { get; set; }
        public List<DagAtelier> DagAteliers { get; set; }
        public bool IsTemplate { get; set; }

        public int Weeknummer
        {
            get { return _weeknummer; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Gelieve een weeknummer te kiezen");
                }
                else
                {
                    _weeknummer = value;
                }
            }

        }

        public Weekdag Weekdag
        {
            get { return _weekdag; }
            set
            {
                if (value == Weekdag.Undefined)
                {
                    throw new ArgumentException("Selecteer een weekdag");
                }
                else
                {
                    _weekdag = value;
                }
            }
        }

        public DagPlanningTemplate()
        {
            DagAteliers = new List<DagAtelier>();
        }

        public DagPlanningTemplate(int weeknr, Weekdag weekdag, bool template): this()
        {
            Weeknummer = weeknr;
            IsTemplate = template;
            Weekdag = weekdag;
        }
    }
}

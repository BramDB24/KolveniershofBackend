using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagPlanning : DagPlanningTemplate
    {
        #region Fields
        private DateTime _datum;
        #endregion

        #region Properties
        public DateTime Datum
        {
            get { return _datum; }
            set
            {
                if (value != null)
                {
                    _datum = value;
                }

                else
                {
                    throw new ArgumentException("Gelieve een datum in te vullen");
                }
            }
        }

        public string Commentaar { get; set; } = "";



        #endregion

        public DagPlanning()
        {
            DagAteliers = new List<DagAtelier>();
        }

        public DagPlanning(int weeknr, DateTime datum, string eten) : this()
        {
            Datum = datum;
            Eten = eten;
            Weeknummer = weeknr;
            IsTemplate = false;
            Weekdag = ZetDayOfWeekOmNaarWeekdag(datum);
        }

        public DagPlanning(DagPlanningTemplate template, DateTime datum) : this(template.Weeknummer, datum, template.Eten)
        {
            DagAteliers = template.DagAteliers;
        }

        public Weekdag ZetDayOfWeekOmNaarWeekdag(DateTime datum)
        {
            var dag = Weekdag.Undefined;

            switch (datum.DayOfWeek)
            {
                case DayOfWeek.Monday: dag = Weekdag.Maandag; break;
                case DayOfWeek.Tuesday: dag = Weekdag.Dinsdag; break;
                case DayOfWeek.Wednesday: dag = Weekdag.Woensdag; break;
                case DayOfWeek.Thursday: dag = Weekdag.Donderdag; break;
                case DayOfWeek.Friday: dag = Weekdag.Vrijdag; break;
                case DayOfWeek.Saturday: dag = Weekdag.Zaterdag; break;
                case DayOfWeek.Sunday: dag = Weekdag.Zondag; break;
            }
            return dag;
        }
    }
}

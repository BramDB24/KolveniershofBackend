using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class DagPlanning
    {
        #region Fields
        private int _weeknummer;
        private DateTime _datum;
        private string _eten;
        #endregion

        #region Properties
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

        public DateTime Datum
        {
            get { return _datum; }
            set
            {
                if (value != null && value >= DateTime.Today)
                {
                    _datum = value;
                }

                else
                {
                    throw new ArgumentException("Gelieve een datum in te vullen gelijk aan vandaag of later");
                }
            }
        }

        public string Eten
        {
            get { return _eten; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Gelieve de maaltijd in te geven");
                }
                else
                {
                    _eten = value;
                }
            }
        }

        public int DagplanningId { get; set; }
        public DayOfWeek Weekdag { get; set; }

        public List<Opmerking> Opmerkingen { get; set; }
        public List<DagAtelier> DagAteliers { get; set; }
        #endregion

        public DagPlanning()
        {
            Opmerkingen = new List<Opmerking>();
            DagAteliers = new List<DagAtelier>();
            Weekdag = Datum.DayOfWeek;
        }

        public DagPlanning(int weeknummer, DateTime datum, string eten): this()
        {
            Weeknummer = weeknummer;
            Datum = datum;
            Eten = eten;
        }
    }
}

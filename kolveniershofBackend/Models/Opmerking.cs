using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Opmerking
    {
        #region Fields
        private OpmerkingType _opmerkingtype;
        private string _tekst;
        private DateTime _datum;
        #endregion

        #region Properties
        public int OpmerkingId { get; set; }
        public OpmerkingType OpmerkingType
        {
            get { return _opmerkingtype; }
            set
            {
                if(value == OpmerkingType.Undefined)
                {
                    throw new ArgumentException("Selecteer de soort opmerking");
                }
                else
                {
                    _opmerkingtype = value;
                }
            }
        }

        public string Tekst
        {
            get { return _tekst; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Gelieve tekst in te vullen voor de opmerking");
                }
                else
                {
                    _tekst = value;
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
        #endregion

        protected Opmerking()
        {

        }

        public Opmerking(OpmerkingType opmerkingType, string tekst, DateTime dagVanOpmerking): this()
        {
            OpmerkingType = opmerkingType;
            Tekst = tekst;
            Datum = dagVanOpmerking;
        }
    }
}

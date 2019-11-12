using kolveniershofBackend.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        #endregion

        #region Properties
        public int OpmerkingId { get; set; }
        public string Tekst { get; set; }
        public DateTime Datum { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
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

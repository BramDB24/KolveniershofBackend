using kolveniershofBackend.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Atelier
    {
        #region Fields
        private AtelierType _ateliertype;
        private string _naam;
        private string _pictoURL;

        #endregion

        #region Properties
        public int AtelierId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AtelierType AtelierType
        {
            get { return _ateliertype; }
            set
            {
                if (value == AtelierType.Undefined)
                {
                    throw new ArgumentException("Selecteer het soort atelier");
                }
                else
                {
                    _ateliertype = value;
                }
            }
        }

        public string Naam
        {
            get { return _naam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Gelieve de naam van het atelier op te geven");
                }
                else
                {
                    _naam = value;
                }
            }
        }

        public string PictoURL
        {
            get { return _pictoURL; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Gelieve de picto van het atelier op te geven");
                }
                else
                {
                    _pictoURL = value;
                }
            }
        }

        [JsonIgnore]
        public List<DagAtelier> DagAteliers { get; set; }

        #endregion

        public Atelier()
        {
            DagAteliers = new List<DagAtelier>();
        }
    

        public Atelier(AtelierType atelierType, string naam, string picto)
        {
            AtelierType = atelierType;
            Naam = naam;
            PictoURL = picto;
        }
    }
}

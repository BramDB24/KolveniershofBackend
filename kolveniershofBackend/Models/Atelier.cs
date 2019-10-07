using kolveniershofBackend.Enums;
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

        public AtelierType AtelierType
        {
            get { return _ateliertype; }
            set
            {
                if (_ateliertype == AtelierType.Undefined)
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

        #endregion

        public Atelier()
        {

        }

        public Atelier(AtelierType atelierType, string naam, string picto)
        {
            AtelierType = atelierType;
            Naam = naam;
            PictoURL = picto;
        }
    }
}

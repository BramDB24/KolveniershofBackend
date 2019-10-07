﻿using kolveniershofBackend.Enums;
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
        #endregion

        #region Properties
        public int OpmerkingId { get; set; }

        public OpmerkingType OpmerkingType
        {
            get { return _opmerkingtype; }
            set
            {
                if(_opmerkingtype == OpmerkingType.Undefined)
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

        #endregion
    }
}

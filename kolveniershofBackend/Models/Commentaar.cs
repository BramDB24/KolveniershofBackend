using kolveniershofBackend.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public class Commentaar
    {
        #region Fields
        private CommentaarType _commentaarType;
        private string _tekst;
        #endregion

        #region Properties
        public int CommentaarId { get; set; }
        public DateTime Datum { get; set; }
        public string GebruikerId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CommentaarType CommentaarType
        {
            get { return _commentaarType; }
            set
            {
                if (value == CommentaarType.Undefined)
                {
                    throw new ArgumentException("Selecteer de soort opmerking");
                }
                else
                {
                    _commentaarType = value;
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
                    throw new ArgumentException("Gelieve tekst in te vullen voor de commentaar");
                }
                else
                {
                    _tekst = value;
                }
            }
        }
        #endregion

        public Commentaar()
        {
        }

        public Commentaar(string tekst, CommentaarType commentaarType, DateTime datum, string gebruikerid)
        {
            Tekst = tekst;
            CommentaarType = commentaarType;
            Datum = datum;
            GebruikerId = gebruikerid;
        }

    }
}

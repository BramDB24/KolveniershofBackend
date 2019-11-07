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

        public DagMoment DagMoment
        {
            get { return _dagMoment; }
            set
            {
                _dagMoment = value;
            }
        }

        public Atelier Atelier { get; set; }


        public List<GebruikerDagAtelier> Gebruikers { get; set; }

        #endregion

        public DagAtelier()
        {
            Gebruikers = new List<GebruikerDagAtelier>();
        }

        public DagAtelier(DagMoment dagMoment, Atelier atelier): this()
        {
            DagMoment = dagMoment;
            Atelier = atelier;
        }

        public void VoegGebruikersToe(List<Gebruiker> g)
        {
            g.ForEach(t => Gebruikers.Add(new GebruikerDagAtelier(t, this)));
        }

        public void VoegGebruikerAanDagAtelierToe(Gebruiker gebruiker)
        {
            Gebruikers.Add(new GebruikerDagAtelier(gebruiker, this));
        }

        public void VerwijderGebruikerUitAtelier(Gebruiker gebruiker)
        {
            Gebruikers.Remove(Gebruikers.FirstOrDefault(g => g.Gebruiker == gebruiker));
        }

        public IEnumerable<Gebruiker> GeefAlleGebruikersVanAtelier()
        {
            return Gebruikers.Select(g => g.Gebruiker).OrderBy(g => g.Achternaam);
        }

        public IEnumerable<Gebruiker> GeefAlleBegeleiders()
        {
            return Gebruikers.Select(g => g.Gebruiker).Where(g => g.Type == GebruikerType.Begeleider);
        }

    }
}

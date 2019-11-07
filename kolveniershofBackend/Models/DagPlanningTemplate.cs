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
                if (value <= 0 || value > 4)
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
                if (value == Weekdag.Undefined || !Enum.IsDefined(typeof(Weekdag), value))
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

        public DagPlanningTemplate(int weeknr, Weekdag weekdag): this()
        {
            Weeknummer = weeknr;
            IsTemplate = true;
            Weekdag = weekdag;
        }

        public void VoegDagAtelierToeAanDagPlanningTemplate(DagAtelier dagAtelier)
        {
            DagAteliers.Add(dagAtelier);
        }

        public DagAtelier VoegDagateliersToe(Atelier atelier)
        {
            Array values = Enum.GetValues(typeof(DagMoment));
            Random r = new Random();
            DagMoment randomMoment = (DagMoment)values.GetValue(r.Next(values.Length));
            DagAtelier dagatelier = new DagAtelier
            {
                Atelier = atelier,
                DagMoment = randomMoment,
            };
            DagAteliers.Add(dagatelier);
            return dagatelier;
        }

        public void VerwijderDagAtlierVanDagPlanningTemplate(DagAtelier dagAtelier)
        { 
            DagAteliers.Remove(dagAtelier);
        }

        public IEnumerable<DagAtelier> GetDagAteliersGebruiker(string gebruikerId)
        {
            return DagAteliers.Where(da => da.Gebruikers.Any(g => g.Id == gebruikerId));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface IDagPlanningTemplateRepository
    {
        DagPlanningTemplate GetBy(int weeknummer, int dagnummer);
        DagPlanning GetBy(DateTime date);
        void Add(DagPlanningTemplate dagPlanning);
        void Delete(DagPlanningTemplate dagPlanning);
        void Update(DagPlanningTemplate dagPlanning);
        void SaveChanges();
    }
}

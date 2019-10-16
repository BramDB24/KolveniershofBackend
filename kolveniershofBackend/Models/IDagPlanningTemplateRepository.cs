using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface IDagPlanningTemplateRepository
    {
        DagPlanning GetBy(DateTime date);
        DagPlanningTemplate GetBy(int week, Weekdag dag);
        void Add(DagPlanningTemplate dagPlanning);
        void Delete(DagPlanningTemplate dagPlanning);
        void Update(DagPlanningTemplate dagPlanning);
        void SaveChanges();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface IDagPlanningRepository
    {
        DagPlanning GetBy(int id);
        DagPlanning GetBy(DateTime date);
        void Add(DagPlanning dagPlanning);
        void Delete(DagPlanning dagPlanning);
        void Update(DagPlanning dagPlanning);
        void SaveChanges();
    }
}

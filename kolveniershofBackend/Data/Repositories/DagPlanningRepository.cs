using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Repositories
{
    public class DagPlanningRepository : IDagPlanningRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<DagPlanning> _dagen;

        public DagPlanningRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dagen = dbContext.Dagplanningen;
        }
        public void Add(DagPlanning dagPlanning)
        {
            _dagen.Add(dagPlanning);
        }

        public void Delete(DagPlanning dagPlanning)
        {
            _dagen.Remove(dagPlanning);
        }

        public DagPlanning GetBy(DateTime datum)
        {
            return _dagen.SingleOrDefault(d => d.Datum == datum);
        }

        public DagPlanning GetBy(int id)
        {
            return _dagen.SingleOrDefault(d => d.DagplanningId == id);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(DagPlanning dagPlanning)
        {
            _dagen.Update(dagPlanning);
        }
    }
}

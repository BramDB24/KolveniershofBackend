using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Repositories
{
    public class DagPlanningTemplateRepository : IDagPlanningTemplateRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<DagPlanningTemplate> _dagen;

        public DagPlanningTemplateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dagen = dbContext.DagPlanningen;
        }
        public void Add(DagPlanningTemplate dagPlanning)
        {
            _dagen.Add(dagPlanning);
        }

        public void Delete(DagPlanningTemplate dagPlanning)
        {
            _dagen.Remove(dagPlanning);
        }

        public DagPlanning GetBy(DateTime datum)
        {
            return (DagPlanning)_dagen.Where(d => d.GetType() == typeof(DagPlanning)).Include(d => d.DagAteliers).ThenInclude(a => a.Atelier)
                        .Include(d => d.DagAteliers).ThenInclude(a => a.GebruikerDagAteliers).ThenInclude(g => g.Gebruiker).ThenInclude(o => o.Commentaren).FirstOrDefault(d => ((DagPlanning)d).Datum == datum);
        }

        public  DagPlanningTemplate GetBy(int weeknummer, Weekdag dagnummer)
        {
            return _dagen.Include(d => d.DagAteliers).ThenInclude(a => a.Atelier)
                        .Include(d => d.DagAteliers).ThenInclude(a => a.GebruikerDagAteliers).ThenInclude(g => g.Gebruiker).FirstOrDefault(d => d.Weekdag == dagnummer && d.Weeknummer == weeknummer);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(DagPlanningTemplate dagPlanning)
        {
            _dagen.Update(dagPlanning);
        }
    }
}

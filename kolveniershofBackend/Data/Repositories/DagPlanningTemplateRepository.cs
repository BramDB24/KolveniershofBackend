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

        public void AddDagPlanning(DagPlanning dagPlanning)
        {
            _dagen.Add(dagPlanning);
        }

        public void Delete(DagPlanningTemplate dagPlanning)
        {
            _dagen.Remove(dagPlanning);
        }

        public void DeleteOuderDanAantalJaar(DateTime datum, int jarenVerschil)
        {
            IEnumerable<DagPlanning> gevondenDagPlanningen = _dagen.OfType<DagPlanning>().Include(d => d.DagAteliers).Where(d => datum.Year - d.Datum.Year >= jarenVerschil).AsEnumerable();
            _dagen.RemoveRange(gevondenDagPlanningen);
        }

        public DagPlanning GetByDatum(DateTime datum)
        {
            return _dagen.OfType<DagPlanning>().Include(d => d.DagAteliers).ThenInclude(a => a.Atelier)
                        .Include(d => d.DagAteliers).ThenInclude(a => a.Gebruikers).ThenInclude(g => g.Gebruiker).ThenInclude(o => o.Commentaren).FirstOrDefault(d => ((DagPlanning)d).Datum == datum);
        }

        public DagPlanning GetByDatumGeenInclude(DateTime datum)
        {
            return _dagen.OfType<DagPlanning>().FirstOrDefault(d => d.Datum == datum);
        }

        public DagPlanningTemplate GetTemplateByWeeknummerEnDagnummer(int weeknummer, Weekdag dagnummer)
        {
            return _dagen.Include(d => d.DagAteliers).ThenInclude(a => a.Atelier)
                        .Include(d => d.DagAteliers).ThenInclude(a => a.Gebruikers).ThenInclude(g => g.Gebruiker).FirstOrDefault(d => d.IsTemplate && d.Weekdag == dagnummer && d.Weeknummer == weeknummer);
        }

        public DagPlanningTemplate GetTemplateByWeeknummerEnDagnummerGeenInclude(int weeknummer, Weekdag dagnummer)
        {
            return _dagen.FirstOrDefault(d => d.IsTemplate && d.Weekdag == dagnummer && d.Weeknummer == weeknummer);
        }

        public DagPlanning GetByIdDagPlanning(int id)
        {
            return (DagPlanning)_dagen.Where(d => !d.IsTemplate).Include(d => d.DagAteliers).ThenInclude(a => a.Atelier)
                         .Include(d => d.DagAteliers).ThenInclude(a => a.Gebruikers).ThenInclude(g => g.Gebruiker).ThenInclude(o => o.Commentaren).FirstOrDefault(d => ((DagPlanning)d).DagplanningId == id);
        }

        public DagPlanningTemplate GetByIdDagPlanningTemplate(int id)
        {
            return _dagen.Include(d => d.DagAteliers).ThenInclude(a => a.Atelier)
                         .Include(d => d.DagAteliers).ThenInclude(a => a.Gebruikers).ThenInclude(g => g.Gebruiker).ThenInclude(o => o.Commentaren).FirstOrDefault(d => d.DagplanningId == id);
        }

        public DagPlanning GetEersteDagPlanning()
        {
            return _dagen.OfType<DagPlanning>().OrderBy(d => d.Datum).FirstOrDefault();
        }

        public bool IsDagPlanningenLeeg()
        {
            return !_dagen.Where(d => !d.IsTemplate).Any();
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

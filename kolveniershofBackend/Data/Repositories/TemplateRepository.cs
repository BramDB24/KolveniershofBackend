using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Template> _templates;

        public TemplateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _templates = dbContext.Templates;
        }

        public IEnumerable<Template> GetAll()
        {
            return _dbContext.Templates.AsEnumerable();
        }

        public Template GetTemplateById(int id)
        {
            return _dbContext.Templates.Where(t => t.Id == id).FirstOrDefault();
        }

        public Template GetActiveTemplate()
        {
            return _dbContext.Templates.Where(t => t.IsActief).FirstOrDefault();
        }

        public Template GetFullTemplateById(int id)
        {
            return _dbContext.Templates.Where(t => t.Id == id).Include(t => t.DagPlanningTemplates).ThenInclude(dt => dt.DagAteliers).ThenInclude(da => da.Atelier)
                .Include(t => t.DagPlanningTemplates).ThenInclude(dt => dt.DagAteliers).ThenInclude(da => da.Gebruikers).ThenInclude(g => g.Gebruiker).FirstOrDefault();
        }

        public Template GetFullActiveTemplate()
        {
            return _dbContext.Templates.Where(t => t.IsActief).Include(t => t.DagPlanningTemplates).ThenInclude(dt => dt.DagAteliers).ThenInclude(da => da.Atelier)
                .Include(t => t.DagPlanningTemplates).ThenInclude(dt => dt.DagAteliers).ThenInclude(da => da.Gebruikers).ThenInclude(g => g.Gebruiker).FirstOrDefault();
        }

        public DagPlanningTemplate GetActiveDagTemplate(int weeknr, Weekdag weekdag)
        {   
            return _templates.Where(t => t.IsActief).Include(t => t.DagPlanningTemplates).ThenInclude(dpt => dpt.DagAteliers).ThenInclude(da => da.Gebruikers).ThenInclude(g => g.Gebruiker).Include(t => t.DagPlanningTemplates).ThenInclude(dpt => dpt.DagAteliers).ThenInclude(da => da.Atelier).FirstOrDefault()?.DagPlanningTemplates.Where(d => d.Weeknummer == weeknr &&  d.Weekdag == weekdag).FirstOrDefault() ?? null;
        }

        public DagPlanningTemplate GetDagTemplateByNaam(string naam, int weeknr, Weekdag weekdag)
        {   
            return _templates.Where(t => t.Naam == naam).Include(t => t.DagPlanningTemplates).ThenInclude(dpt => dpt.DagAteliers).ThenInclude(da => da.Gebruikers).ThenInclude(g => g.Gebruiker).Include(t => t.DagPlanningTemplates).ThenInclude(dpt => dpt.DagAteliers).ThenInclude(da => da.Atelier).FirstOrDefault()?.DagPlanningTemplates.Where(d => d.Weeknummer == weeknr && d.Weekdag == weekdag).FirstOrDefault();
        }

        public DagPlanningTemplate GetDagTemplateById(int id, int weeknr, Weekdag weekdag)
        {
            return _templates.Where(t => t.Id == id).Include(t => t.DagPlanningTemplates).ThenInclude(dpt => dpt.DagAteliers).ThenInclude(da => da.Gebruikers).ThenInclude(g => g.Gebruiker).Include(t => t.DagPlanningTemplates).ThenInclude(dpt => dpt.DagAteliers).ThenInclude(da => da.Atelier).FirstOrDefault()?.DagPlanningTemplates.Where(d => d.Weeknummer == weeknr && d.Weekdag == weekdag).FirstOrDefault();
        }

        public void AddTemplate(Template template)
        {
            _templates.Add(template);
        }

        public void Update(Template template)
        {
            _templates.Update(template);
        }

        public void Delete(Template template)
        {
            _dbContext.Remove(template);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        
    }
}

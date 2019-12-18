using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface ITemplateRepository
    {
        DagPlanningTemplate GetDagTemplateByNaam(string name, int weeknr, Weekdag weekdag);
        DagPlanningTemplate GetDagTemplateById(int id, int weeknr, Weekdag weekdag);
        DagPlanningTemplate GetActiveDagTemplate(int weeknr, Weekdag weekdag);
        IEnumerable<Template> GetAll();
        Template GetTemplateById(int id);
        Template GetActiveTemplate();
        Template GetFullTemplateById(int id);
        Template GetFullActiveTemplate();
        void AddTemplate(Template template);
        void Update(Template template);
        void Delete(Template template);
        void SaveChanges();
    }
}

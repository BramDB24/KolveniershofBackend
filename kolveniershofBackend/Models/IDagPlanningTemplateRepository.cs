using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface IDagPlanningTemplateRepository
    {
        DagPlanning GetByDatum(DateTime date);
        DagPlanning GetByDatumGeenInclude(DateTime date);
        DagPlanning GetByIdDagPlanning(int id);
        DagPlanningTemplate GetByIdDagPlanningTemplate(int id);
        DagPlanningTemplate GetTemplateByWeeknummerEnDagnummer(int week, Weekdag dag);
        DagPlanningTemplate GetTemplateByWeeknummerEnDagnummerGeenInclude(int weeknummer, Weekdag dagnummer);
        DagPlanning GetEersteDagPlanning();
        DagPlanning GetLastDagPlanning();
        bool IsDagPlanningenLeeg();
        void Add(DagPlanningTemplate dagPlanning);
        void AddDagPlanning(DagPlanning dagPlanning);
        void Delete(DagPlanningTemplate dagPlanning);
        void DeleteOuderDanAantalJaar(DateTime datum, int jarenVerschil);
        void Update(DagPlanningTemplate dagPlanning);
        void SaveChanges();
    }
}

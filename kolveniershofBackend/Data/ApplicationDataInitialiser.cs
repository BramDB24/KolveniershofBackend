using kolveniershofBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data
{
    public class ApplicationDataInitialiser
    {

        private readonly ApplicationDbContext _dbContext;

        public ApplicationDataInitialiser(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //gebruikers   
                var user = new Gebruiker("lucas", "vermeulen", "lucas@gmail.com", Enums.Sfeergroep.Sfeergroep1, 
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Begeleider);
                _dbContext.Gebruikers.Add(user);
                

                //dagplanningen
                DateTime dt = DateTime.Today;
                var dagplanning = new DagPlanning(1, new DateTime(2020, 5, 12), "balletjes in tomatensaus en friet");
                _dbContext.DagPlanningen.Add(dagplanning);
                for(int i= 1; i<20; i++)
                {
                    var date = dt.AddDays(i);
                    var dp = new DagPlanning(2, date, "pizza");
                    _dbContext.DagPlanningen.Add(dp);
                }
                Console.WriteLine(dt);


                var template = new DagPlanningTemplate()
                {
                    IsTemplate = true,
                    Weeknummer = 1
                };

                _dbContext.SaveChanges();

            }
        }
    }
}

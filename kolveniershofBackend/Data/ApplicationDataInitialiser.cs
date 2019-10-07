using kolveniershofBackend.Enums;
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
                var lucas = new Gebruiker("lucas", "nogiets", "mail@iets.com", Sfeergroep.Sfeergroep1, "fotoURL", "straat", "11", "71", "Gent", "9000", GebruikerType.Cliënt);
                var bram = new Gebruiker("bram", "de bleecker", "email@iets.be", Sfeergroep.Sfeergroep2, "fotoURL", "straat", "200", "41", "Oudenaarde", "9700", GebruikerType.Admin);
                _dbContext.Gebruikers.AddRange(lucas, bram);
                

                //dagplanningen
                DateTime dt = DateTime.Today;
                var dagplanning = new DagPlanning(dt);
                _dbContext.Dagplanningen.Add(dagplanning);
                for(int i= 1; i<20; i++)
                {
                    var date = dt.AddDays(i);
                    var dp = new DagPlanning(date);
                    _dbContext.Dagplanningen.Add(dp);
                }

                var template = new DagPlanningTemplate()
                {
                    Weekdag = DayOfWeek.Monday,
                    Weeknummer = 1
                };


                _dbContext.SaveChanges();

            }
        }
    }
}

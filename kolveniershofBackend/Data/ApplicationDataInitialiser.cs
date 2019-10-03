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
                //seeding the database with recipes, see DBContext       
                var user = new Gebruiker("lucas");
                _dbContext.Gebruikers.Add(user);
                _dbContext.SaveChanges();
            }
        }
    }
}

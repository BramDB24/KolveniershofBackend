﻿using kolveniershofBackend.Models;
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
                var user = new Gebruiker("lucas");
                _dbContext.Gebruikers.Add(user);
                

                //dagplanningen
                DateTime dt = DateTime.Today;
                var dagplanning = new DagPlanning(1, new DateTime(2020, 5, 12), "broccoli met worst en patatten");
                _dbContext.Dagplanningen.Add(dagplanning);
                for(int i= 1; i<20; i++)
                {
                    var date = dt.AddDays(i);
                    var dp = new DagPlanning(2, date, "pizza");
                    _dbContext.Dagplanningen.Add(dp);
                }
                Console.WriteLine(dt);


                _dbContext.SaveChanges();

            }
        }
    }
}

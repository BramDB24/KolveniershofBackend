using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Gebruiker> _gebruikers;

        public GebruikerRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _gebruikers = dbContext.Gebruikers;
        }

        public void Add(Gebruiker gebruiker)
        {
            _gebruikers.Add(gebruiker);
        }

        public void Delete(Gebruiker gebruiker)
        {
            _gebruikers.Remove(gebruiker);
        }

        public IEnumerable<Gebruiker> GetAll()
        {
            return _gebruikers.ToList();
        }

        public Gebruiker GetBy(string id)
        {
            return _gebruikers.SingleOrDefault(r => r.GebruikerId == id);
        }

        public Gebruiker GetBySfeergroep(Sfeergroep sfeergroep)
        {
            return _gebruikers.SingleOrDefault(r => r.Sfeergroep == sfeergroep);
        }

        public Gebruiker GetByType(GebruikerType gebruikerType)
        {
            return _gebruikers.SingleOrDefault(r => r.Type == gebruikerType);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Gebruiker gebruiker)
        {
            _context.Update(gebruiker);
        }
    }
}

using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Repositories
{
    public class AtelierRepository : IAtelierRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Atelier> _ateliers;

        public AtelierRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _ateliers = dbContext.Ateliers;
        }

        public Atelier getBy(int id)
        {
            return _ateliers.FirstOrDefault(a => a.AtelierId == id);
        }

        public IEnumerable<Atelier> getAll()
        {
            return _ateliers.ToList();
        }

        public void Add(Atelier atelier)
        {
            _ateliers.Add(atelier);
        }

        public void Update(Atelier atelier)
        {
            _ateliers.Update(atelier);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

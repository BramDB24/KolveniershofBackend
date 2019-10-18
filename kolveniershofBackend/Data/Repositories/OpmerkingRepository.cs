using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Repositories
{
    public class OpmerkingRepository : IOpmerkingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Opmerking> _opmerkingen;

        public OpmerkingRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _opmerkingen = dbContext.Opmerkingen;
        }

        public Opmerking Add(Opmerking opmerking)
        {
            _opmerkingen.Add(opmerking);
            return opmerking;
        }

        public Opmerking Delete(Opmerking opmerking)
        {
            _opmerkingen.Remove(opmerking);
            return opmerking;
        }

        public IEnumerable<Opmerking> getAll()
        {
            return _opmerkingen.ToList();
        }

        public Opmerking getBy(int id)
        {
            return _opmerkingen.FirstOrDefault(a => a.OpmerkingId == id);
        }

        public IEnumerable<Opmerking> getByDateAndType(DateTime datum, OpmerkingType type)
        {
            return _opmerkingen.Where(a => a.Datum == datum && a.OpmerkingType == type).ToList();
        }

        public IEnumerable<Opmerking> getByDate(DateTime datum)
        {
            return _opmerkingen.Where(a => a.Datum == datum).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Opmerking opmerking)
        {
            _opmerkingen.Update(opmerking);
        }


    }
}

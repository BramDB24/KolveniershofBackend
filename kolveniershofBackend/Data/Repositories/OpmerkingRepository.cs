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

        public IEnumerable<Opmerking> GetAll()
        {
            return _opmerkingen.ToList();
        }

        public Opmerking GetBy(int id)
        {
            return _opmerkingen.FirstOrDefault(a => a.OpmerkingId == id);
        }

        public Opmerking GetEerste()
        {
            return _opmerkingen.OrderBy(o => o.Datum).FirstOrDefault();
        }

        public IEnumerable<Opmerking> GetByDateAndType(DateTime datum, OpmerkingType type)
        {
            return _opmerkingen.Where(a => a.Datum == datum && a.OpmerkingType == type).ToList();
        }

        public IEnumerable<Opmerking> GetByDate(DateTime datum)
        {
            return _opmerkingen.Where(a => a.Datum == datum).ToList();
        }

        public void DeleteOuderDanAantalJaar(DateTime datum, int jarenVerschil)
        {
            IEnumerable<Opmerking> gevondenOpmerkingen = _opmerkingen.Where(o => datum.Year - o.Datum.Year >= jarenVerschil).ToList();
            _opmerkingen.RemoveRange(gevondenOpmerkingen);
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

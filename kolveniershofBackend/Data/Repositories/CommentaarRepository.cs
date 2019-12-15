using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Repositories
{
    public class CommentaarRepository : ICommentaarRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Commentaar> _commentaar;

        public CommentaarRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _commentaar = dbContext.Commentaar;
        }

        public void Add(Commentaar commentaar)
        {
            _commentaar.Add(commentaar);
        }

        public Commentaar GetCommentaarByDatumEnGebruiker(string gebruikerId, DateTime datum)
        {
            return _commentaar.Where(t => t.GebruikerId.Equals(gebruikerId) && t.Datum == datum).FirstOrDefault();
        }

        public IEnumerable<Commentaar> GetByDatum(DateTime datum)
        {
            return _commentaar.AsNoTracking().Where(c => c.Datum == datum).ToList();
        }

        public void Update(Commentaar commentaar)
        {
            _commentaar.Update(commentaar);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Commentaar GetById(int commentaarId)
        {
            return _commentaar.FirstOrDefault(t => t.CommentaarId == commentaarId);
        }
    }
}

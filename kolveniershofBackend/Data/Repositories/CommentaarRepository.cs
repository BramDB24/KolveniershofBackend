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

        public IEnumerable<Commentaar> GetAll()
        {
            return _commentaar.ToList();
        }

        public Commentaar GetBy(int id)
        {
            return _commentaar.SingleOrDefault(c => c.CommentaarId == id);
        }

        public IEnumerable<Commentaar> GetByDatum(DateTime datum)
        {
            return _commentaar.Where(c => c.Datum == datum).ToList();
        }

        public void Update(Commentaar commentaar)
        {
            _commentaar.Update(commentaar);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(Commentaar commentaar)
        {
            _commentaar.Remove(commentaar);
        }
    }
}

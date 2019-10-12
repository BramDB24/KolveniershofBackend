using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface ICommentaarRepository
    {
        Commentaar GetBy(int id);
        IEnumerable<Commentaar> GetByDatum(DateTime datum);
        IEnumerable<Commentaar> GetAll();
        void Add(Commentaar commentaar);
        void Update(Commentaar commentaar);
        void SaveChanges();
        void Delete(Commentaar commentaar);
    }
}

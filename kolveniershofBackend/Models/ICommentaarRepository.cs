using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface ICommentaarRepository
    {
        void Add(Commentaar commentaar);
        void Update(Commentaar commentaar);
        void SaveChanges();
        Commentaar GetCommentaarByDatumEnGebruiker(string gebruikerId, DateTime datum);
        Commentaar GetById(int commentaarId);
    }
}

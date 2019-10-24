using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface IAtelierRepository
    {
        Atelier getBy(int id);
        IEnumerable<Atelier> getAll();
        void Add(Atelier atelier);
        void Update(Atelier atelier);
        void Delete(Atelier atelier);
        void SaveChanges();
    }
}

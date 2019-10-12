using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface IOpmerkingRepository
    {
        Opmerking getBy(int id);
        IEnumerable<Opmerking> getAll();
        Opmerking Add(Opmerking opmerking);
        void Update(Opmerking opmerking);
        void SaveChanges();
        Opmerking Delete(Opmerking opmerking);
    }
}

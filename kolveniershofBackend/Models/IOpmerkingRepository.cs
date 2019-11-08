using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface IOpmerkingRepository
    {
        Opmerking GetBy(int id);
        Opmerking GetEerste();
        IEnumerable<Opmerking> GetAll();
        IEnumerable<Opmerking> GetByDateAndType(DateTime date, OpmerkingType type);
        IEnumerable<Opmerking> GetByDate(DateTime date);
        Opmerking Add(Opmerking opmerking);
        void Update(Opmerking opmerking);
        void SaveChanges();
        Opmerking Delete(Opmerking opmerking);
        void DeleteOuderDanAantalJaar(DateTime datum, int jarenVerschil);
    }
}

using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Models
{
    public interface IGebruikerRepository
    {
            Gebruiker GetBy(string id);
            Gebruiker GetBySfeergroep(Sfeergroep sfeergroep);
            Gebruiker GetByType(GebruikerType gebruikerType);
            IEnumerable<Gebruiker> GetAll();
            void Add(Gebruiker gebruiker);
            void Delete(Gebruiker gebruiker);
            void Update(Gebruiker gebruiker);
            void SaveChanges();
    }
}

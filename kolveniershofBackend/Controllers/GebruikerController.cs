using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kolveniershofBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly IGebruikerRepository _gebruikerRepository;

        public GebruikerController(IGebruikerRepository context)
        {
            _gebruikerRepository = context;
        }

        [HttpGet("{id}")]
        public ActionResult<Gebruiker> GetGebruiker(string id)
        {
            Gebruiker g = _gebruikerRepository.GetBy(id);
            if (g == null) return NotFound();
            return g;
        }

        [HttpDelete("{id}")]
        public ActionResult<Gebruiker> VerwijderGebruiker(string id)
        {
            Gebruiker g = _gebruikerRepository.GetBy(id);
            if (g == null)
            {
                return NotFound();
            }
            _gebruikerRepository.Delete(g);
            _gebruikerRepository.SaveChanges();
            return g;
        }

        [HttpGet]
        public IEnumerable<Gebruiker> GetGebruikers()
        {
            return _gebruikerRepository.GetAll();
        }

        [HttpPut("{id}")]
        public ActionResult<Gebruiker> PutGebruiker(string id, Gebruiker gebruiker)
        {
            Gebruiker g = _gebruikerRepository.GetBy(id);
            if (!g.GebruikerId.Equals(id))
                return BadRequest();
            _gebruikerRepository.Update(gebruiker);
            _gebruikerRepository.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public ActionResult<Gebruiker> PostGebruiker(Gebruiker gebruiker)
        {
            _gebruikerRepository.Add(gebruiker);
            _gebruikerRepository.SaveChanges();
            return CreatedAtAction(nameof(GetGebruiker), gebruiker.GebruikerId);
        }
    }
}
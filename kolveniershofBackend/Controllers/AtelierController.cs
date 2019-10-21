using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kolveniershofBackend.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    //[Authorize(Policy = "AdminOnly")]
    //[Authorize(Policy = "BegeleidersOnly")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AtelierController : ControllerBase
    {
        private readonly IAtelierRepository _atelierRepository;

        public AtelierController(IAtelierRepository atelierRepository)
        {
            _atelierRepository = atelierRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Atelier> GetAtelier(int id)
        {
            var atelier = _atelierRepository.getBy(id);
            if (atelier == null)
                return NotFound();
            return atelier;
        }

        [HttpGet]
        public IEnumerable<Atelier> GetAteliers()
        {
            return _atelierRepository.getAll();
        }

        [HttpPost]
        public ActionResult<Atelier> PostAtelier(Atelier atelier) //dto?
        {
            _atelierRepository.Add(atelier);
            _atelierRepository.SaveChanges();
            return Redirect(nameof(GetAteliers));
        }

        [HttpPut("{id}")]
        public ActionResult<Atelier> PutAtelier(int id, Atelier atelier)
        {
            if (atelier.AtelierId != id)
                return BadRequest();
            _atelierRepository.Update(atelier);
            _atelierRepository.SaveChanges();
            return NoContent();
        } 

    }
}
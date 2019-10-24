using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
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
        public ActionResult<AtelierDTO> PostAtelier(AtelierDTO dto)
        {
            Atelier atelier = new Atelier
            {
                AtelierId = dto.AtelierId,
                AtelierType = dto.AtelierType,
                Naam = dto.Naam,
                PictoURL = dto.PictoURL
            };
            _atelierRepository.Add(atelier);
            _atelierRepository.SaveChanges();
            return Redirect(nameof(GetAteliers));
        }

        [HttpPut("{id}")]
        public ActionResult<AtelierDTO> PutAtelier(int id, AtelierDTO dto)
        {
            if (dto.AtelierId != id)
                return BadRequest();
            Atelier atelier = new Atelier
            {
                AtelierId = dto.AtelierId,
                AtelierType = dto.AtelierType,
                Naam = dto.Naam,
                PictoURL = dto.PictoURL
            };
            _atelierRepository.Update(atelier);
            _atelierRepository.SaveChanges();
            return NoContent();
        } 

        [HttpDelete]
        public ActionResult<Atelier> DeleteAtelier(int id)
        {
            Atelier atelier = _atelierRepository.getBy(id);
            if (atelier == null)
            {
                return NotFound();
            }
            _atelierRepository.Delete(atelier);
            _atelierRepository.SaveChanges();
            return atelier;
        }

    }
}
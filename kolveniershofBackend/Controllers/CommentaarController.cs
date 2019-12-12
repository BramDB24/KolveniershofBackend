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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentaarController : ControllerBase
    {
        private readonly ICommentaarRepository _commentaarRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public CommentaarController(ICommentaarRepository commentaarRepository, IGebruikerRepository gebruikerRepository)
        {
            _commentaarRepository = commentaarRepository;
            _gebruikerRepository = gebruikerRepository;
        }

        [HttpGet("{id}")]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<Commentaar> GetCommentaar(int id)
        {
            var commentaar = _commentaarRepository.GetBy(id);
            if (commentaar == null)
                return NotFound();
            return commentaar;
        }

        [HttpGet("huidigeGebruiker")]
        //Parameter moet meegegeven worden, naam van aangemelde gebruiker
        public ActionResult<IEnumerable<Commentaar>> GetCommentaarVanSpecifiekeGebruiker()
        {
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail(User.Identity.Name);
            return new OkObjectResult(huidigeGebruiker.GeefCommentaarVanHuidigeGebruiker());
        }

        [HttpGet("huidigeGebruiker/{datum}")]
        public ActionResult<IEnumerable<Commentaar>> GetCommentaarVanSpefiekeDagEnGebruiker(string datum)
        {
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail(User.Identity.Name);
            return new OkObjectResult(huidigeGebruiker.GeefCommentaarVanHuidigeGebruiker().Where(c=>c.Datum == datumFormatted).ToList());
        }

        [HttpGet]
        //[Authorize(Policy = "AdminOnly")]
        //[Authorize(Policy = "BegeleidersOnly")]
        public ActionResult<IEnumerable<Commentaar>> GetAlleCommentaar()
        {
            return new OkObjectResult(_commentaarRepository.GetAll());
        }

        [HttpPost]
        //[Authorize(Policy = "ClientOnly")]
        public ActionResult<CommentaarDTO> PostCommentaar(CommentaarDTO commentaardto) 
        {
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail(User.Identity.Name);
            Commentaar commentaar = new Commentaar(commentaardto.Tekst, commentaardto.CommentaarType, commentaardto.Datum);
            huidigeGebruiker.addCommentaar(commentaar);

            _gebruikerRepository.SaveChanges();
            _commentaarRepository.Add(commentaar);
            _commentaarRepository.SaveChanges();
            return Redirect(nameof(GetCommentaar));
        }

        [HttpPut("{id}")]
        //[Authorize(Policy = "ClientOnly")]
        public ActionResult<Commentaar> PutCommentaar(int id, Commentaar commentaar)
        {
            if (commentaar.CommentaarId != id)
                return BadRequest();
            _commentaarRepository.Update(commentaar);
            _commentaarRepository.SaveChanges();
            _gebruikerRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Policy = "ClientOnly")]
        public ActionResult<Commentaar> DeleteCommentaar(int id)
        {
            Commentaar commentaar = _commentaarRepository.GetBy(id);
            if(commentaar == null)
            {
                return NotFound();
            }
            _commentaarRepository.Delete(commentaar);
            _gebruikerRepository.SaveChanges();
            _commentaarRepository.SaveChanges();
            return commentaar;
        }
    }
}
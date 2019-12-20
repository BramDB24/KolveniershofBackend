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


        [HttpGet("huidigeGebruiker/zaterdag/{zaterdagDatum}/zondag/{zondagDatum}/{gebruikerId}")]
        public ActionResult<IEnumerable<Commentaar>> GetCommentaarVanSpefiekeDagEnGebruiker(string zaterdagDatum, string zondagDatum, string gebruikerId)
        {
            DateTime datumFormattedZat = DateTime.Parse(zaterdagDatum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DateTime datumFormattedZon = DateTime.Parse(zondagDatum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetBy(gebruikerId);

            ICollection<Commentaar> commentaarlijst = new List<Commentaar>();
            commentaarlijst.Add(_commentaarRepository.GetCommentaarByDatumEnGebruiker(huidigeGebruiker.Id, datumFormattedZat));
            commentaarlijst.Add(_commentaarRepository.GetCommentaarByDatumEnGebruiker(huidigeGebruiker.Id, datumFormattedZon));
            return new OkObjectResult(commentaarlijst.ToList());
        }

        [HttpPost]
        //[Authorize(Policy = "ClientOnly")]
        public ActionResult<CommentaarDTO> PostCommentaar(CommentaarDTO commentaardto) 
        {
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail(User.Identity.Name);
            Commentaar commentaar = new Commentaar(commentaardto.Tekst, commentaardto.CommentaarType, commentaardto.Datum, huidigeGebruiker.Id);

            _commentaarRepository.Add(commentaar);
            _commentaarRepository.SaveChanges();
            return Ok();
        }

        [HttpPut("{commentaarId}/{content}")]
        public ActionResult PutCommentaar(int commentaarId, string content)
        {
            Commentaar commentaar = _commentaarRepository.GetById(commentaarId);
            if(commentaar != null)
            {
                commentaar.Tekst = content;
            }
            _commentaarRepository.Update(commentaar);
            _commentaarRepository.SaveChanges();
            return Ok(); 
        }

       
    }
}
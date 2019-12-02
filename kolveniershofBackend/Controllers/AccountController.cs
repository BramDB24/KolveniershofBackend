using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace kolveniershofBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<Gebruiker> _signInManager;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IConfiguration _config;
        public AccountController(SignInManager<Gebruiker> signInManager, UserManager<Gebruiker> userManager, 
            IGebruikerRepository gebruikerRepository, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _gebruikerRepository = gebruikerRepository;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    string token = GetToken(user);
                    return Created("", new { token, user}); //returns only the token                   
                }
            }
            return BadRequest();
        }

        private string GetToken(Gebruiker g)
        {      // Createthetoken
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, g.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, g.UserName) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(null, null, 
                claims,
                expires: DateTime.Now.AddMinutes(30), 
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO model)
        {
            Gebruiker g = new Gebruiker { Email = model.Email, Voornaam = model.Voornaam, Achternaam = model.Achternaam,
            Sfeergroep = model.Sfeergroep, Foto = model.Foto, Type = model.Type,
            UserName = model.Email
            };

            var result = await _userManager.CreateAsync(g, model.Password);

            if (result.Succeeded)
            {
                _gebruikerRepository.SaveChanges();
                string token = GetToken(g);
                return Created("", token);
            }
            return BadRequest();
        }

        [HttpGet("{gebruikerId}")]
        public ActionResult<GebruikerDTO> GetGebruikerViaID(string gebruikerId)
        {
            Gebruiker g = _gebruikerRepository.GetBy(gebruikerId);
            if (g == null) return NotFound();
            return new GebruikerDTO(g);
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
        public IEnumerable<GebruikerDTO> GetGebruikers()
        {
            return _gebruikerRepository.GetAll().ToList().Select(g => new GebruikerDTO(g));
        }

        [HttpGet("sfeergroep/{sfeergroep}")]
        public IEnumerable<GebruikerDTO> GetGebruikersVanSpecifiekeSfeergroep(int sfeergroep)
        {
            return _gebruikerRepository.GetAll().Where(g=>g.Sfeergroep == (Sfeergroep)sfeergroep).
                ToList().Select(g => new GebruikerDTO(g));
        }

        [HttpPut("{id}")]
        public ActionResult<Gebruiker> PutGebruiker(string id, Gebruiker gebruiker)
        {
            Gebruiker g = _gebruikerRepository.GetBy(id);
            g.Achternaam = gebruiker.Achternaam;
            g.Voornaam = gebruiker.Voornaam;
            g.Commentaren = gebruiker.Commentaren;
            g.Email = gebruiker.Email;
            g.Sfeergroep = gebruiker.Sfeergroep;
            g.Type = gebruiker.Type;
            g.Foto = gebruiker.Foto;
            if (!g.Id.Equals(id))
                return BadRequest();
            _gebruikerRepository.Update(g);
            _gebruikerRepository.SaveChanges();
            return NoContent();
        }
    }

}
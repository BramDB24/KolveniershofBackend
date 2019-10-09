﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    string token = GetToken(user);
                    return Created("", token); //returns only the token                   
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO model)
        {
            //IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };

            Gebruiker g = new Gebruiker { Email = model.Email, Voornaam = model.Voornaam, Achternaam = model.Achternaam,
            Gemeente = model.Gemeente, Postcode = model.PostCode, Straatnaam = model.Straatnaam,
            Huisnummer = model.Huisnummer, Sfeergroep = model.Sfeergroep, Foto = model.Foto, Type = model.Type,
            UserName = model.Email
            };

            var result = await _userManager.CreateAsync(g, model.Password);

            if (result.Succeeded)
            {
                _gebruikerRepository.Add(g);
                _gebruikerRepository.SaveChanges();
                string token = GetToken(g);
                return Created("", token);
            }
            return BadRequest();
        }
    }

}
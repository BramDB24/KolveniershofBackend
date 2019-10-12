﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kolveniershofBackend.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class OpmerkingController : ControllerBase
    {
        private readonly IOpmerkingRepository _opmerkingRepository;

        public OpmerkingController(IOpmerkingRepository opmerkingRepository)
        {
            _opmerkingRepository = opmerkingRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Opmerking> GetOpmerking(int id)
        {
            var opmerking = _opmerkingRepository.getBy(id);
            if (opmerking == null)
                return NotFound();
            return opmerking;
        }

        [HttpGet]
        public IEnumerable<Opmerking> GetAteliers()
        {
            return _opmerkingRepository.getAll();
        }

        [HttpPost]
        public ActionResult<Opmerking> PostAtelier(OpmerkingDTO opmerkingData)
        {
            Opmerking opm = _opmerkingRepository.Add(opmerkingData.getOpmerking());
            _opmerkingRepository.SaveChanges();
            return Ok(opm);
        }

        [HttpPut("{id}")]
        public ActionResult<Atelier> PutAtelier(int id, OpmerkingDTO opmerkingData)
        {
            if (opmerkingData == null)
                return BadRequest();
            Opmerking opm = _opmerkingRepository.getBy(id);
            opm.OpmerkingType = opmerkingData.OpmerkingType;
            opm.Tekst = opmerkingData.Tekst;
            _opmerkingRepository.Update(opm);
            _opmerkingRepository.SaveChanges();
            return Ok(opm);
        }

        [HttpDelete("{id}")]
        public ActionResult<Opmerking> DeleteOpmerking(int id)
        {
            //identity
            Opmerking opm = _opmerkingRepository.getBy(id);
            if (opm == null)
            {
                return NotFound();
            }
            _opmerkingRepository.Delete(opm);
            _opmerkingRepository.SaveChanges();
            return opm;
        }
    }
}
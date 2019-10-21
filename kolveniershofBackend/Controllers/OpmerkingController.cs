using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.DTO;
using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kolveniershofBackend.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(Policy = "AdminOnly")]
    [Authorize(Policy = "BegeleidersOnly")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public IEnumerable<Opmerking> GetOpmerkingen()
        {
            return _opmerkingRepository.getAll();
        }

        [HttpGet("opmerkingOp/{datum}/typeOpmerking/{type}")]
        public IEnumerable<Opmerking> GetOpmerkingenVanSpecifiekeDagEnType(string datum, OpmerkingType type)
        {
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            return _opmerkingRepository.getByDateAndType(datumFormatted, type);
        }

        [HttpGet("opmerkingOp/{datum}")]
        public IEnumerable<Opmerking> GetOpmerkingenVanSpecifiekeDag(string datum)
        {
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            IEnumerable<Opmerking> opmerkingenVanDatum = _opmerkingRepository.getByDate(datumFormatted);
            ICollection<OpmerkingType> types = new List<OpmerkingType>() {OpmerkingType.AteliersEnWeekschema, OpmerkingType.Begeleiding, OpmerkingType.Cliënten,
                OpmerkingType.Stagiairs, OpmerkingType.UurRegistratie, OpmerkingType.Varia, OpmerkingType.Vervoer, OpmerkingType.Vrijwilligers };
            ICollection<Opmerking> nieuweOpmerkingen = new List<Opmerking>();

            if (opmerkingenVanDatum.Count() == 0)
            {
                foreach (var item in types)
                {
                    Opmerking o = new Opmerking(item, "", datumFormatted);
                    nieuweOpmerkingen.Add(o);
                    _opmerkingRepository.Add(o);
                    _opmerkingRepository.SaveChanges();
                } 
                return nieuweOpmerkingen;
            }
            else
            {
                return _opmerkingRepository.getByDate(datumFormatted);

            }
        }

        [HttpPost]
        public ActionResult<Opmerking> PostOpmerking(OpmerkingDTO opmerkingData)
        {
            Opmerking opm = _opmerkingRepository.Add(opmerkingData.getOpmerking());
            _opmerkingRepository.SaveChanges();
            return Ok(opm);
        }

        [HttpPut("{id}")]
        public ActionResult<Opmerking> PutOpmerking(int id, OpmerkingDTO opmerkingData)
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
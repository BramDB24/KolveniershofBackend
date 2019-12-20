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
    [Authorize(Policy = "Begeleider")]
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
            //Controle op overbodige data
            VerwijderVerouderdeData();

            var opmerking = _opmerkingRepository.GetBy(id);
            if (opmerking == null)
                return NotFound();
            return opmerking;
        }

        [HttpGet]
        public IEnumerable<Opmerking> GetOpmerkingen()
        {
            //Controle op overbodige data
            VerwijderVerouderdeData();

            return _opmerkingRepository.GetAll();
        }

        [HttpGet("opmerkingOp/{datum}/typeOpmerking/{type}")]
        public IEnumerable<Opmerking> GetOpmerkingenVanSpecifiekeDagEnType(string datum, OpmerkingType type)
        {
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            return _opmerkingRepository.GetByDateAndType(datumFormatted, type);
        }

        [HttpGet("opmerkingOp/{datum}")]
        public IEnumerable<Opmerking> GetOpmerkingenVanSpecifiekeDag(string datum)
        {
            DateTime datumFormatted = DateTime.Parse(datum, null, System.Globalization.DateTimeStyles.RoundtripKind);
            IEnumerable<Opmerking> opmerkingenVanDatum = _opmerkingRepository.GetByDate(datumFormatted);
            ICollection<OpmerkingType> types = new List<OpmerkingType>() {OpmerkingType.AteliersEnWeekschema, OpmerkingType.Begeleiding, OpmerkingType.Cliënten,
                OpmerkingType.Stagiairs, OpmerkingType.UurRegistratie, OpmerkingType.Varia, OpmerkingType.Vervoer, OpmerkingType.Vrijwilligers, OpmerkingType.Logistiek };
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
                return _opmerkingRepository.GetByDate(datumFormatted);

            }
        }

        [HttpPut("{id}")]
        public ActionResult<Opmerking> PutOpmerking(int id, OpmerkingDTO opmerkingData)
        {
            if (opmerkingData == null)
                return BadRequest();
            Opmerking opm = _opmerkingRepository.GetBy(id);
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
            Opmerking opm = _opmerkingRepository.GetBy(id);
            if (opm == null)
            {
                return NotFound();
            }
            _opmerkingRepository.Delete(opm);
            _opmerkingRepository.SaveChanges();
            return opm;
        }

        /// <summary>
        /// Deze methode controleert of er verouderde data is in de databank en verwijdert die.
        /// Data is verouderd als het minstens twee jaar oud is (gebasseerd op jaar nummer).
        /// </summary>
        private void VerwijderVerouderdeData()
        {
            Opmerking oudsteOpmerking = _opmerkingRepository.GetEerste();
            if (DateTime.Today.Year - oudsteOpmerking.Datum.Year < 2)
            {
                return;
            }

            _opmerkingRepository.DeleteOuderDanAantalJaar(DateTime.Today, 2);
            _opmerkingRepository.SaveChanges();

        }
    }
}
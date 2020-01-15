using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.Dependency;
using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using Newtonsoft.Json;
using NLog;

namespace ConsultantPunctualityApp.Controllers
{
    public class ConsultantsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ConsultantDB _db = new ConsultantDB();
        private IConsultant _consultant;
        public ConsultantsController(IConsultant consultant)
        {
            _consultant = consultant;
        }
        // GET: api/Consultants
        [HttpGet]
        [Route("api/Consultant/GetConsultants")]
        public List<ConsultantDTO> GetConsultants()
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetConsultants IHttpActionResult in the Consultants Controller");
            return _consultant.AllConsultant();
        }


        // GET: api/Consultants/5
        [Route("api/Consultant/GetConsultant/{id}")]
        [ResponseType(typeof(ConsultantDTO))]
        public  ConsultantDTO GetConsultant(string id)
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetConsultant IHttpActionResult in the Consultants Controller");
            return _consultant.GetConsultant(id);
        }

        // PUT: api/Consultants/5
        [ResponseType(typeof(void))]
        [Route("api/Consultant/PutConsultant/{id}")]
        public async Task<IHttpActionResult> PutConsultant(int id, Consultant consultant)
        {
            logger.Info(DateTime.Now + ":" + "Inside the PutConsultant IHttpActionResult in the Consultants Controller");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consultant.Id)
            {
                return BadRequest();
            }

            //_db.Entry(consultant).State = EntityState.Modified;
            var getConsultants = _db.Consultants.Where(c => c.Id == id).SingleOrDefault();
            if (string.IsNullOrEmpty(consultant.MobileNo))
            {
                getConsultants.MobileNo = getConsultants.MobileNo;
            }
            if (!string.IsNullOrEmpty(consultant.MobileNo))
            {
                getConsultants.MobileNo = consultant.MobileNo;
            }
            if (string.IsNullOrEmpty(consultant.RegID))
            {
                getConsultants.RegID = getConsultants.RegID;
            }
            if (!string.IsNullOrEmpty(consultant.RegID))
            {
                getConsultants.RegID = consultant.RegID;
            }
            if (string.IsNullOrEmpty(consultant.FullName))
            {
                getConsultants.FullName = getConsultants.FullName;
            }
            if (!string.IsNullOrEmpty(consultant.FullName))
            {
                getConsultants.FullName = consultant.FullName;
            }
            if (string.IsNullOrEmpty(consultant.EmailAddress))
            {
                getConsultants.EmailAddress = getConsultants.EmailAddress;
            }
            if (!string.IsNullOrEmpty(consultant.EmailAddress))
            {
                getConsultants.EmailAddress = consultant.EmailAddress;
            }
            if (string.IsNullOrEmpty(consultant.DOB))
            {
                getConsultants.DOB = getConsultants.DOB;
            }
            if (!string.IsNullOrEmpty(consultant.DOB))
            {
                getConsultants.DOB = consultant.DOB;
            }
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Consultants
        [ResponseType(typeof(ConsultantDTO))]
        [Route("api/Consultant/PostConsultant")]
        public async Task<IHttpActionResult> PostConsultant(Consultant consultant)
        {
            logger.Info(DateTime.Now + ":" + "Inside the PostConsultant IHttpActionResult in the Consultants Controller");
            if (!ModelState.IsValid)
            {
                logger.Info(DateTime.Now + ":" + "Inside the ModelState.IsValid IHttpActionResult in the Consultants Controller");
                return BadRequest(ModelState);
            }
            await _consultant.AddConsultant(consultant);
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(consultant));
            return CreatedAtRoute("DefaultApi", new { id = consultant.RegID }, consultant);
        }

        // DELETE: api/Consultants/5
        [ResponseType(typeof(Consultant))]
        public async Task<IHttpActionResult> DeleteConsultant(int id)
        {
            logger.Info(DateTime.Now + ":" + "Inside the DeleteConsultant IHttpActionResult in the Consultants Controller");
            Consultant consultant = await _db.Consultants.FindAsync(id);
            if (consultant == null)
            {
                return NotFound();
            }
            try
            {
                _db.Consultants.Remove(consultant);
                await _db.SaveChangesAsync();
                logger.Info("Response" + ":" + JsonConvert.SerializeObject(consultant));
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
            return Ok(consultant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsultantExists(int id)
        {
            return _db.Consultants.Count(e => e.Id == id) > 0;
        }
    }
}
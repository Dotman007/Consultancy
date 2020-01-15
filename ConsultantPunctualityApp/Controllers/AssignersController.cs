using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.Dependency;
using ConsultantPunctualityApp.Models;
using Newtonsoft.Json;
using NLog;

namespace ConsultantPunctualityApp.Controllers
{
    public class AssignersController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ConsultantDB _db;
        private IAssigner _assigner;
        public AssignersController(ConsultantDB db, IAssigner assigner)
        {
            _db = db;
            _assigner = assigner;
        }
        // GET: api/Assigners
        public IQueryable<Assigner> GetAssigners()
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetAssigners IHttpActionResult in the Assigners Controller");
            return _db.Assigners;
        }

        // GET: api/Assigners/5
        [ResponseType(typeof(Assigner))]
        public async Task<IHttpActionResult> GetAssigner(int id)
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetAssigner IHttpActionResult in the Assigners Controller");
            Assigner assigner = await _db.Assigners.FindAsync(id);
            if (assigner == null)
            {
                return NotFound();
            }
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(assigner));
            return Ok(assigner);
        }

        // PUT: api/Assigners/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAssigner(int id, Assigner assigner)
        {
            logger.Info(DateTime.Now + ":" + "Inside the PutAssigner IHttpActionResult in the Assigners Controller");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assigner.Id)
            {
                return BadRequest();
            }

            _db.Entry(assigner).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignerExists(id))
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

        // POST: api/Assigners
        [ResponseType(typeof(Assigner))]
        public async Task<IHttpActionResult> PostAssigner(Assigner assigner)
        {
            logger.Info(DateTime.Now + ":" + "Inside the PostAssigner IHttpActionResult in the Assigners Controller");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _assigner.Register(assigner);
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(assigner));
            return CreatedAtRoute("DefaultApi", new { id = assigner.Id }, assigner);
        }

        // DELETE: api/Assigners/5
        [ResponseType(typeof(Assigner))]
        public async Task<IHttpActionResult> DeleteAssigner(int id)
        {
            logger.Info(DateTime.Now + ":" + "Inside the PostAssigner IHttpActionResult in the Assigners Controller");
            Assigner assigner = await _db.Assigners.FindAsync(id);
            if (assigner == null)
            {
                return NotFound();
            }

            _db.Assigners.Remove(assigner);
            await _db.SaveChangesAsync();
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(assigner));
            return Ok(assigner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssignerExists(int id)
        {
            return _db.Assigners.Count(e => e.Id == id) > 0;
        }
    }
}
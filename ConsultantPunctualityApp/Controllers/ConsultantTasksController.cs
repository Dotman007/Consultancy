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
    public class ConsultantTasksController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ConsultantDB _db;
        private IConsultantTask _consultantTask;
        public ConsultantTasksController(ConsultantDB db, IConsultantTask consultantTask)
        {
            _db = db;
            _consultantTask = consultantTask;
        }
        // GET: api/ConsultantTasks
        public IQueryable<ConsultantTask> GetConsultantTasks()
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetConsultantTasks IHttpActionResult in the ConsultantTasks Controller");
            return _db.ConsultantTasks;
        }

        // GET: api/ConsultantTasks/5
        [ResponseType(typeof(ConsultantTask))]
        public async Task<IHttpActionResult> GetConsultantTask(int id)
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetConsultantTask IHttpActionResult in the ConsultantTasks Controller");
            ConsultantTask consultantTask = await _db.ConsultantTasks.FindAsync(id);
            if (consultantTask == null)
            {
                return NotFound();
            }
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(consultantTask));
            return Ok(consultantTask);
        }

        // PUT: api/ConsultantTasks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutConsultantTask(int id, ConsultantTask consultantTask)
        {
            logger.Info(DateTime.Now + ":" + "Inside the PutConsultantTask IHttpActionResult in the ConsultantTasks Controller");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consultantTask.TaskId)
            {
                return BadRequest();
            }

            _db.Entry(consultantTask).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultantTaskExists(id))
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

        // POST: api/ConsultantTasks
        [ResponseType(typeof(ConsultantTask))]
        public async Task<IHttpActionResult> PostConsultantTask(ConsultantTask consultantTask)
        {
            logger.Info(DateTime.Now + ":" + "Inside the PostConsultantTask IHttpActionResult in the ConsultantTasks Controller");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _consultantTask.AddTask(consultantTask);
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(consultantTask));
            return CreatedAtRoute("DefaultApi", new { id = consultantTask.TaskId }, consultantTask);
        }

        // DELETE: api/ConsultantTasks/5
        [ResponseType(typeof(ConsultantTask))]
        public async Task<IHttpActionResult> DeleteConsultantTask(int id)
        {
            ConsultantTask consultantTask = await _db.ConsultantTasks.FindAsync(id);
            if (consultantTask == null)
            {
                return NotFound();
            }

            _db.ConsultantTasks.Remove(consultantTask);
            await _db.SaveChangesAsync();

            return Ok(consultantTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsultantTaskExists(int id)
        {
            return _db.ConsultantTasks.Count(e => e.TaskId == id) > 0;
        }
    }
}
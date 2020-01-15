using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.Dependency;
using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using NLog;

namespace ConsultantPunctualityApp.Controllers
{
    public class AssignmentsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ConsultantDB _db;
        private IAssignment _assignment;
        public AssignmentsController(ConsultantDB db, IAssignment assignment)
        {
            _db = db;
            _assignment = assignment;

        }
        // GET: api/Assignments
        [Route("Assignment/GetAssignmentByRegNo/{regNo}")]
        [ResponseType(typeof(Assignment))]
        public async Task<IHttpActionResult> GetAssignmentByRegNo(string regNo)
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetAssignmentByRegNo IHttpActionResult in the Assignments Controller");
            return Ok(await _assignment.GetConsultantAssignmentsByPresentDate(regNo));
        }


        [Route("Assignment/GetConsultantAssignmentBySpecifiedDate/{regNo}/{specificDate}")]
        [ResponseType(typeof(AssignmentDateDTO))]
        public async Task<IHttpActionResult> GetConsultantAssignmentBySpecifiedDate(string regNo,string specificDate)
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetConsultantAssignmentBySpecifiedDate IHttpActionResult in the Assignments Controller");
            return Ok(await _assignment.GetConsultantAssignmentBySpecifiedDate(regNo,specificDate));
        }

        [Route("Assignment/SubmitTask/{regNo}/{consultantTaskId}")]
        [HttpGet]
        [ResponseType(typeof(Assignment))]
        public async Task<IHttpActionResult> SubmitTask(string regNo, int consultantTaskId)
        {
            logger.Info(DateTime.Now + ":" + "Inside the SubmitTask IHttpActionResult in the Assignments Controller");
            await _assignment.SubmitTask(regNo, consultantTaskId);
            return Ok();
        }
        [Route("Assignment/GetCompletedTask")]
        [HttpGet]
        [ResponseType(typeof(CompletedAssignmentDTO))]
        public async Task<IHttpActionResult> GetAllAcheivedTask()
        {
            //await _assignment.SubmitTask(regNo, consultantTaskId);
            logger.Info(DateTime.Now + ":" + "Inside the GetAllAcheivedTask IHttpActionResult in the Assignments Controller");
            return Ok( await _assignment.GetAllAcheivedTask());
        }

        [Route("Assignment/GetUnCompletedTask")]
        [HttpGet]
        [ResponseType(typeof(UnCompletedAssignmentDTO))]
        public async Task<IHttpActionResult> GetAllUnAchievedTask()
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetAllAcheivedTask IHttpActionResult in the Assignments Controller");
            return Ok( await _assignment.GetAllUnAchievedTask());
        }

        [Route("Assignment/GetAllAssignment")]
        [ResponseType(typeof(GetAllAssignmentDTO))]
        public async Task<IHttpActionResult> GetAllAssignment()
        {
            logger.Info(DateTime.Now + ":" + "Inside the GetAllAssignment IHttpActionResult in the Assignments Controller");
            return Ok(await _assignment.GetAssignments());
        }

        // GET: api/Assignments/5
        [Route("Assignment/GetAssignment/{id}")]
        [ResponseType(typeof(AssignmentDTO))]
        public async Task<IHttpActionResult> GetAssignment(int id)
        {
            logger.Info(DateTime.Now+ ":"+ "Inside the GetAssignment IHttpActionResult in the Assignments Controller");
            Assignment assignment = await _db.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment);
        }

        // POST: api/Assignments
        //[Route("Assignment/PostAssignment/{}")]
        [ResponseType(typeof(Assignment))]
        public async Task<IHttpActionResult> PostAssignment(Assignment assignment, string consultantId, int consultantTaskId, int assignerId)
        {
            logger.Info(DateTime.Now + ":" + "Inside the PostAssignment IHttpActionResult in the Assignments Controller");
            if (!ModelState.IsValid)
            {
                logger.Info(DateTime.Now + ":" + "Inside the ModelState.IsValid in the Assignments Controller");
                return BadRequest(ModelState);
            }
            try
            {
                logger.Info(DateTime.Now + ":" + "Inside the try block of the PostAssignment IHttpActionResult  in the Assignments Controller");
                await _assignment.AssignTaskToConsultant(assignment, consultantId, consultantTaskId, assignerId);
            }
            catch (DbEntityValidationException ex)
            {
                logger.Info(DateTime.Now + ":" + "The Error is :" + ex.Message);
                string message = ex.Message;
                throw new Exception(message);
            }
            return CreatedAtRoute("DefaultApi", new { id = assignment.Id }, assignment);
        }


        private bool AssignmentExists(int id)
        {
            return _db.Assignments.Count(e => e.Id == id) > 0;
        }
    }
}
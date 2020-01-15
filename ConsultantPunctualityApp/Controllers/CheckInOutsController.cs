using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.Dependency;
using NLog;

namespace ConsultantPunctualityApp.Controllers
{
    public class CheckInOutsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ConsultantDB _db;
        private ICheckInOut _checkIn;
        public CheckInOutsController(ConsultantDB db, ICheckInOut checkIn)
        {
            _db = db;
            _checkIn = checkIn;
        }
        [Route("api/CheckInOut/CheckIn/{regNo}")]
        public async Task<IHttpActionResult> Checkin(string regNo)
        {
            logger.Info(DateTime.Now + ":" + "Inside the Checkin IHttpActionResult in the CheckInOuts Controller");
            await _checkIn.CheckIn(regNo);
            return Ok();
        }

        [Route("api/CheckInOut/CheckOut/{regNo}")]
        public async Task<IHttpActionResult> CheckOut(string regNo)
        {
            logger.Info(DateTime.Now + ":" + "Inside the CheckOut IHttpActionResult in the CheckInOuts Controller");
            await _checkIn.CheckOut(regNo);
            return Ok();
        }

        // GET: api/CheckInOuts
        [Route("api/CheckInOut/GetCheckedInConsultant")]
        public async Task<IHttpActionResult> GetCheckedInConsultant(string regId)
        {
            logger.Info(DateTime.Now + ":" + "Inside the CheckOut GetCheckedInConsultant in the CheckInOuts Controller");
            var checkedInconsultant = await _checkIn.GetCheckedInConsultant(regId);
            return Ok(checkedInconsultant);
        }

        [Route("api/CheckInOut/GetCheckedOutConsultant")]
        public async Task<IHttpActionResult> GetCheckedOutConsultant(string regId)
        {
            logger.Info(DateTime.Now + ":" + "Inside the CheckOut GetCheckedOutConsultant in the CheckInOuts Controller");
            var checkedOutconsultant = await _checkIn.GetCheckedOutConsultant(regId);
            return Ok(checkedOutconsultant);
        }


        [Route("api/CheckInOut/GetCheckedInConsultants")]
        public async Task<IHttpActionResult> GetCheckedInConsultant()
        {
            logger.Info(DateTime.Now + ":" + "Inside the CheckOut GetCheckedInConsultant in the CheckInOuts Controller");
            var checkedInconsultants = await _checkIn.GetCheckedInConsultants();
            return Ok(checkedInconsultants);
        }

        [Route("api/CheckInOut/GetCheckedOutConsultants")]
        public async Task<IHttpActionResult> GetCheckedOutConsultants()
        {
            logger.Info(DateTime.Now + ":" + "Inside the CheckOut GetCheckedOutConsultants in the CheckInOuts Controller");
            var checkedOutconsultants = await _checkIn.GetCheckedOutConsultants();
            return Ok(checkedOutconsultants);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckinExists(int id)
        {
            return _db.CheckInOuts.Count(e => e.Id == id) > 0;
        }
    }
}
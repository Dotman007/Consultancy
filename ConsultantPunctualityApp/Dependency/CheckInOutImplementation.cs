using ConsultantPunctualityApp.DAL;
using ConsultantPunctualityApp.DTOs;
using ConsultantPunctualityApp.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsultantPunctualityApp.Dependency
{
    public class CheckInOutImplementation : ICheckInOut
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ConsultantDB _consultantdb = new ConsultantDB();
        public CheckInOutImplementation()
        {
            
        }
        public async Task CheckIn(string regId)
        {
            logger.Info("Inside the CheckIn Method");
            var consultant = await _consultantdb.Consultants.Where(c => c.RegID == regId).SingleOrDefaultAsync();
            if (consultant == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    Content = new StringContent(string.Format("The Registration Number do not exist")),
                    ReasonPhrase = "The Registration Number do not exist"
                };
                logger.Info("Logged Details :" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);
            }
            var checkin = new CheckInOut
            {
                ConsultantId = consultant.Id,
                ConsultantRegID = consultant.RegID,
                ChekedIn = true,
                CheckinDate = DateTime.Now.ToString("MM-dd-yyyy"),
                CheckinTime = DateTime.Now.ToString("hh:mm tt")
            };
            var getConsultantsId = await _consultantdb.CheckInOuts.ToListAsync();
            logger.Info("Logged Details :" + JsonConvert.SerializeObject(checkin));
            var checkedInBefore = _consultantdb.CheckInOuts.Where(c => c.CheckinDate == checkin.CheckinDate && c.ConsultantRegID == regId && c.ChekedIn == true).Count();
            if (checkedInBefore >= 1)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent(string.Format("You have already checked in today; Have a nice day at work")),
                    ReasonPhrase = "You have already checked in today; Have a nice day at work"
                };
                logger.Info("Logged Details :" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);

            }
            _consultantdb.CheckInOuts.Add(checkin);
            await _consultantdb.SaveChangesAsync();
            logger.Info("Saved Successfully" + ":" + JsonConvert.SerializeObject(checkin));
            var responseSuccess = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent(string.Format("Checkin was Successful; Welcome to work")),
                ReasonPhrase = "Checkin was Successful; Welcome to work"
            };
            logger.Info("Success Response" + ":" + JsonConvert.SerializeObject(responseSuccess));
            throw new HttpResponseException(responseSuccess);
            
        }

        public async Task CheckOut(string regId)
        {
            logger.Info("Inside the CheckOut Method");
            var consultant = await _consultantdb.Consultants.Where(c => c.RegID == regId).SingleOrDefaultAsync();
            if (consultant == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    Content = new StringContent(string.Format("The Registration Number do not exist")),
                    ReasonPhrase = "The Registration Number do not exist"
                };
                logger.Info("Logged Details :" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);
            }
            var today = DateTime.Now.ToString("MM-dd-yyyy");
            var checkinouts = _consultantdb.CheckInOuts.Where(c=>c.ConsultantRegID ==regId && c.CheckinDate == today).SingleOrDefault();
            if (checkinouts.ChekedIn == false)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {

                    Content = new StringContent(string.Format("You have already checkedin")),
                    ReasonPhrase = "You have already checkedout for today; See you tomorrow"
                };
                logger.Info("Response" + ":" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);
            }
            checkinouts.CheckedOut = true;
            checkinouts.CheckoutDate = DateTime.Now.ToString("MM-dd-yyyy");
            checkinouts.CheckoutTime = DateTime.Now.ToString("hh:mm tt");
            var checkedInBefore = _consultantdb.CheckInOuts.Where(c => c.CheckoutDate == checkinouts.CheckoutDate && c.ConsultantRegID == regId && c.CheckedOut == true).Count();
            if (checkedInBefore >= 1)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                   
                    Content = new StringContent(string.Format("You have already checkedout for today; See you tomorrow")),
                    ReasonPhrase = "You have already checkedout for today; See you tomorrow"
                };
                logger.Info("Response" + ":" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);
            }
            await _consultantdb.SaveChangesAsync();
            var responseSuccess = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent(string.Format("Checkout was Successful; Have a nice day ahead")),
                ReasonPhrase = "Checkout was Successful; Have a nice day ahead"
            };
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(responseSuccess));
            throw new HttpResponseException(responseSuccess);
        }

        public async Task<List<CheckinCheckoutDTO>> GetCheckedInConsultants()
        {
            logger.Info("Inside the GetCheckedInConsultants Method");
            var todaysDate = DateTime.Now.ToString("MM-dd-yyyy");
            var CheckIns = await _consultantdb.CheckInOuts.Where(ch => ch.ChekedIn == true && ch.CheckinDate ==  todaysDate).Select(c => new CheckinCheckoutDTO
            {
                ConsultantName = c.Consultant.FullName,
                ConsultantRegID = c.ConsultantRegID,
                CheckinTime = c.CheckinTime,
                CheckinDate = c.CheckinDate,
                CheckoutTime = "",
                CheckoutDate = ""
            }).ToListAsync();
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(CheckIns));
            if (CheckIns == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    Content = new StringContent(string.Format("No CheckedIn Consultant For Today")),
                    ReasonPhrase = "No CheckedIn Consultant For Today"
                };
                logger.Info("Response" + ":" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);
            }
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(CheckIns));
            return CheckIns;
        }


        public async Task<List<CheckinCheckoutDTO>> GetCheckedOutConsultants()
        {
            logger.Info("Inside the GetCheckedOutConsultants Method");
            var todaysDate = DateTime.Now.ToString("MM-dd-yyyy");
            var CheckOuts = await _consultantdb.CheckInOuts.Where(ch => ch.CheckedOut == true && ch.CheckoutDate == todaysDate).Select(c => new CheckinCheckoutDTO
            {
                ConsultantName = c.Consultant.FullName,
                ConsultantRegID = c.ConsultantRegID,
                CheckinTime = c.CheckinTime,
                CheckoutDate = c.CheckoutDate,
                CheckoutTime = c.CheckoutTime,
                CheckinDate = c.CheckinDate
            }).ToListAsync();
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(CheckOuts));
            if (CheckOuts == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    Content = new StringContent(string.Format("No CheckedOut Consultant For Today")),
                    ReasonPhrase = "No CheckedOut Consultant For Today"
                };
                logger.Info("Response" + ":" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);
                
            }
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(CheckOuts));
            return CheckOuts;
        }



        public async Task<CheckinCheckoutDTO> GetCheckedInConsultant(string regId)
        {
            logger.Info("Inside the GetCheckedInConsultant Method");
            var todaysDate = DateTime.Now.ToString("MM-dd-yyyy");
            var CheckIns = await _consultantdb.CheckInOuts.Where(ch => ch.ChekedIn == true && (ch.CheckedOut == true || ch.CheckedOut == false) && ch.CheckinDate == todaysDate).Select(c => new CheckinCheckoutDTO
            {
                ConsultantName = c.Consultant.FullName,
                ConsultantRegID = c.ConsultantRegID,
                CheckinTime = c.CheckinTime.ToString(),
                CheckinDate = c.CheckinDate.ToString(),
                CheckoutDate = c.CheckoutDate.ToString(),
                CheckoutTime = c.CheckoutTime.ToString()
            }).SingleOrDefaultAsync(c => c.ConsultantRegID == regId);
            if (CheckIns.ConsultantRegID == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    Content = new StringContent(string.Format("The Registration Number do not exist")),
                    ReasonPhrase = "The Registration Number do not exist"
                };
                logger.Info("Logged Details :" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);
            }
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(CheckIns));
            if (CheckIns == null)
            {
                throw new Exception("Empty List");
            }
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(CheckIns));
            return CheckIns;
        }

        public async Task<CheckinCheckoutDTO> GetCheckedOutConsultant(string regId)
        {
            logger.Info("Inside the GetCheckedOutConsultant Method");
            var todaysDate = DateTime.Now.ToString("MM-dd-yyyy");
            var CheckOuts = await _consultantdb.CheckInOuts.Where(ch => ch.ChekedIn == true && ch.CheckedOut == true && ch.CheckoutDate == todaysDate).Select(c => new CheckinCheckoutDTO
            {
                ConsultantName = c.Consultant.FullName,
                ConsultantRegID = c.ConsultantRegID,
                CheckinDate = c.CheckinDate,
                CheckinTime = c.CheckinTime,
                CheckoutTime = c.CheckoutTime,
                CheckoutDate = c.CheckoutDate
            }).SingleOrDefaultAsync(c => c.ConsultantRegID == regId);
            if (CheckOuts.ConsultantRegID == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    Content = new StringContent(string.Format("The Registration Number do not exist")),
                    ReasonPhrase = "The Registration Number do not exist"
                };
                logger.Info("Logged Details :" + JsonConvert.SerializeObject(response));
                throw new HttpResponseException(response);
            }
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(CheckOuts));
            if (CheckOuts == null)
            {
                throw new Exception("Empty List");
            }
            logger.Info("Response" + ":" + JsonConvert.SerializeObject(CheckOuts));
            return CheckOuts;
        }

        public HttpResponseMessage CannotCheckinTwice()
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            var response = message;
            return response;
        }
    }
}
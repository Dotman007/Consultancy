using ConsultantPunctualityApp.Models;
using ConsultantPunctualityAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ConsultantPunctualityAppMVC.Controllers
{
    public class ConsultantController : Controller
    {
        // GET: Consultant
        public ActionResult Index()
        {
            IQueryable<ConsultantMVC> consultantMVCs = null;
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50736/api/");
            var responseTask = client.GetAsync("consultants");
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IQueryable<ConsultantMVC>>();
                readTask.Wait();
                consultantMVCs = readTask.Result;
            }
            else
            {
                //consultantMVCs = Enumerable.Empty<ConsultantMVC>();
                ModelState.AddModelError(string.Empty, "Server error try after some time");
            }
            responseTask = client.GetAsync("consultants");
            return View(responseTask);
            //IEnumerable<ConsultantMVC> consultantMVCs;
            //HttpResponseMessage responseMessage = GlobalVariable.webApiClient.GetAsync("Consultant").Result;
            //consultantMVCs = responseMessage.Content.ReadAsAsync<IEnumerable<ConsultantMVC>>().Result;
            //return View(consultantMVCs);
        }
    }
}
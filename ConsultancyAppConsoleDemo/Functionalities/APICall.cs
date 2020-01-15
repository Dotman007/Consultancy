using ConsultantPunctualityApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsultancyAppConsoleDemo.Functionalities
{
    public class APICall
    {
        private RestClient Client = new RestClient("http://localhost:50736/api/");
        public List<Consultant> GetConsultants()
        {
            RestRequest request = new RestRequest("Consultant/GetConsultants", Method.GET);
            IRestResponse<List<Consultant>> response = Client.Execute<List<Consultant>>(request);
            var convertedResponse = JsonConvert.SerializeObject(response.Data);
            Console.WriteLine(convertedResponse);
            return response.Data;
        }
        public List<Consultant> GetConsultant(string id)
        {
            RestRequest request = new RestRequest("Consultant/GetConsultant/"+id, Method.GET);
            IRestResponse<List<Consultant>> response = Client.Execute<List<Consultant>>(request);
            var convertedResponse = JsonConvert.SerializeObject(response.Data);
            Console.WriteLine(convertedResponse + "\n");
            Console.ReadLine();
            return response.Data;
        }


        public void PutConsultant(int id, Consultant consultant)
        {
            RestRequest request = new RestRequest("Consultant/PutConsultant/" + id, Method.PUT);
            //IRestResponse<Task<IHttpActionResult>> response = Client.Execute<Task<IHttpActionResult>>(request);
            request.AddJsonBody(consultant);
            Client.Execute(request);
            Console.WriteLine("Updated Successfully" + "\n");
            Console.ReadLine();
        }


        public void PostConsultant(Consultant consultant)
        {
            RestRequest request = new RestRequest("Consultant/PostConsultant", Method.POST);
            //IRestResponse<Task<IHttpActionResult>> response = Client.Execute<Task<IHttpActionResult>>(request);
            request.AddJsonBody(consultant);
            Client.Execute(request);
            Console.WriteLine("Posted Successfully" + "\n");
            Console.ReadLine();
        }
    }
   
}



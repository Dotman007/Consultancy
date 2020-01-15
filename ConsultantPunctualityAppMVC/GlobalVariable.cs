using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ConsultantPunctualityAppMVC
{
    public static class GlobalVariable
    {
        public static HttpClient webApiClient = new HttpClient();
        static GlobalVariable()
        {
            webApiClient.BaseAddress = new Uri("http://localhost:50736/api/");
            webApiClient.DefaultRequestHeaders.Clear();
            webApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
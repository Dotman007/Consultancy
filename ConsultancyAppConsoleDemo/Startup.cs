//using System;
//using System.Collections.Generic;
//using System.Text;
//using Hangfire;
//using Microsoft.Owin;
//using Owin;
//[assembly: OwinStartup(typeof(HangfireTesting.Startup))]
//namespace ConsultancyAppConsoleDemo
//{
//    public class Startup
//    {
//        public void Configuration (IAppBuilder app)
//        {
//            GlobalConfiguration.Configuration.UseSqlServerStorage("");
//            var options = new DashboardOptions { AppPath = VirtualPathUtility}
//            app.UseHangfireDashboard("",options);
//            app.UseHangfireServer();
//        }
//    }
//}

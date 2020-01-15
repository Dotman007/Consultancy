using ConsultantPunctualityApp.Dependency;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ConsultantPunctualityApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IConsultant, ConsultantImplementation>();
            container.RegisterType<ICheckInOut, CheckInOutImplementation>();
            container.RegisterType<IAssigner, AssignerImplementation>();
            container.RegisterType<IConsultantTask, ConsultantTaskImplementation>();
            container.RegisterType<IAssignment, AssignmentImplementation>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
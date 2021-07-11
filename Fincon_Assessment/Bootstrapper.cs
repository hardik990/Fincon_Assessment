using System.Web.Mvc;

using Fincon_Assessment.Models;

using Microsoft.Practices.Unity;

using Unity.Mvc4;

namespace Fincon_Assessment
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<ILogin, Login>();
            container.RegisterType<IRegister, Register>();
            container.RegisterType<IDashBoard, DashBoard>();
            container.RegisterType<IGeneral, General>();
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
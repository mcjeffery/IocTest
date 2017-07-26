using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Movies.Services;
using SimpleContainer;
using SimpleContainer.Mvc;

namespace Movies
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = BuildContainer();
            DependencyResolver.SetResolver(new SimpleDependencyResolver(container, DependencyResolver.Current));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private Container BuildContainer()
        {
            var container = new Container();
            container.Register<IMovieDbService, MovieDbService>();

            container.Complete();
            return container;
        }
    }
}

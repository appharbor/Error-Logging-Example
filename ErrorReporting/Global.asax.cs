using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ErrorReporting
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			try
			{
				var exception = Server.GetLastError();
				var logEntry = new LogEntry
				{
					Date = DateTime.Now,
					Message = exception.Message,
					StackTrace = exception.StackTrace,
				};

				var datacontext = new LogDBDataContext();
				datacontext.LogEntries.InsertOnSubmit(logEntry);
				datacontext.SubmitChanges();
			}
			catch (Exception)
			{
				// failed to record exception
			}
		}
	}
}

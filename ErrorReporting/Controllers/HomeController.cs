using System;
using System.Web.Mvc;

namespace ErrorReporting.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			throw new Exception("Boom!");
		}
	}
}

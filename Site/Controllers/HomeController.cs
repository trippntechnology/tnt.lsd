using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TNT.LSD.Site.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult HomeOwners()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}

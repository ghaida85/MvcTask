using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTask.Controllers
{
    public class SidebarController : Controller
    {
        // GET: Sidebar
        public ActionResult Index()
        {
            return View();
        }
    }
}
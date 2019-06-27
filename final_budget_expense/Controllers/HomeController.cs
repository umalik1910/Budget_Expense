using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace budget_expense.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {

            return View();

        }
        public ActionResult SignIn()
        {

            return View();
        }


        public ActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

    }
}

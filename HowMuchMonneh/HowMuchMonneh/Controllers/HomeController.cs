using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HowMuchMonneh.WebService;

namespace HowMuchMonneh.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            new IncomeWebService().InvokeRequestResponseService().Wait();

            return View();
        }
    }
}
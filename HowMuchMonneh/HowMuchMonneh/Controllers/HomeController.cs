using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HowMuchMonneh.Models;
using HowMuchMonneh.WebService;
using System.Globalization;

namespace HowMuchMonneh.Controllers
{
    public class HomeController : Controller
    {
        private readonly IncomeWebService _incomeWebService;
        public HomeController()
        {
            _incomeWebService = new IncomeWebService();
        }

        public Person PersonResult { get; set; }

        [HttpPost]
        public ActionResult GetPredictionFromWebService()
        {
            var gender = Request.Form["gender"];
            var age = Request.Form["age"];

            if (!string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(age))
            {
                var resultResponse = _incomeWebService.InvokeRequestResponseService<ResultOutcome>(gender, age).Result;

                    if (resultResponse != null)
                    {
                        var result = resultResponse.Results.Output1.Value.Values;
                        PersonResult = new Person
                        {
                            Gender = result[0, 0],
                            Age = Int32.Parse(result[0, 1]),
                            Income = float.Parse(result[0, 3], CultureInfo.InvariantCulture.NumberFormat)
                    };
                }
            }

            ViewBag.myData = PersonResult.Income.ToString();
            return View("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
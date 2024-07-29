using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;
using TBAM.Models;

namespace TBAM.Controllers
{
    public class TestFormController : Controller
    {
        private readonly ILogger<TestFormController> _logger;

        public TestFormController(ILogger<TestFormController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new TestFormViewModel
            {
                LineItems = new List<LineItem>(),
                ProductCodes = new List<string> { "Code1", "Code2","Code3" /* add more product codes here */ },
                Workcentres = new List<string> { "Workcentre1", "Workcentre2" /* add more workcentres here */ },
                PurposesOfTesting = new List<string> { "MDR", "USFDA", "In-house validation", "Regulatory submission", "Audit query", "Alternate vendor", "Other" },
                Plants = new List<string> { "Plant1", "Plant2" , "Plant3"/* add more plants here */ },
                TestDetails = ""
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitTestForm(TestFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the form data
                // Save to database or perform other actions

                // Redirect to a success page or return a success message
                 return View("Success",model);

            }
            var message = model.LineItems == null? "No LineItems": "" + model.TestDetails == null? "No Test Details entered.": "";
            return View("/Home",model);
        }

        public IActionResult Success(TestFormViewModel model)
        {
            return View(model);
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

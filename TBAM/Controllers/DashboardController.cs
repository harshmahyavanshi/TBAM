using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security;
using TBAM.Data;
using TBAM.Models;

namespace TBAM.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        private readonly ApplicationDbContext _context;


        public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.Get("userId") != null)
            {
                var model = new TestFormViewModel
                {
                    LineItems = new List<LineItem>(),
                    Products = _context.ProductCodes.Select(p => new Product { ProductCode = p.ProductCode, ProductName = p.ProductName }).ToList(),
                    Workcentres = new List<string> { "Workcentre1", "Workcentre2" /* add more workcentres here */ },
                    PurposesOfTesting = _context.PurposesOfTesting.Select(p => p.Description).ToList(),//new List<string> { "MDR", "USFDA", "In-house validation", "Regulatory submission", "Audit query", "Alternate vendor", "Other" },
                    Plants = _context.Plants.Select(p => p.PlantId).ToList(),
                    TestDetails = ""
                };
                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult SubmitTestForm(TestFormViewModel model)
        {
            // if (ModelState.IsValid)
            // {
                // Process the form data
                // Save to database or perform other actions

                // Redirect to a success page or return a success message
                return RedirectToAction("TestBatchList","Dashboard", model);

            // }
            // var message = model.LineItems == null ? "No LineItems" : "" + model.TestDetails == null ? "No Test Details entered." : "";
            // return View("Index", model);
        }

        public IActionResult Success(TestFormViewModel model)
        {
            return View(model);
        }

        public IActionResult TestBatchList(TestFormViewModel model)
        {
            //  var model = new TestFormViewModel
            // {
            //     LineItems = new List<LineItem>{new LineItem { BatchNumber = "Batch1", ProductCode = "Code1" , Quantity=1, ProductName="Product name", Remarks="Remarks", Workcentre="Workcentre 1"} },
            //     ProductCodes = new List<string> { "Code1"},
            //     Workcentres = new List<string> { "Workcentre1"},
            //     PurposesOfTesting = new List<string> { "MDR" },
            //     Plants = new List<string> { "Plant1"},
            //     TestDetails = "Test"
            // };

            // var model =
            //     new TestBatchListModel 
            //     {
            //         ListOfBatch =
            //         {
            //             new TestBatchModel
            //             {
            //                 Id=1,
            //                 PurposesOfTesting = "Other",
            //                 Plant=1200,
            //                 TestDetails="Test",
            //                 Status="Initiator"
            //             }
            //         }
            //     };
            return View(model);
        }

        //Logout
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Index", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

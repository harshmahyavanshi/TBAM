using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security;
using TBAM.Data;
using TBAM.Models;

namespace TBAM.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DataService _dataService;
        private readonly ILogger<DashboardController> _logger;

        private readonly ApplicationDbContext _context;


        public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext context, DataService dataService)
        {
            _logger = logger;
            _context = context;
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? RefNo)
        {
            if (HttpContext.Session.Get("userId") != null)
            {
                if (RefNo == null)
                {
                    var model = new TestFormViewModel
                    {
                        LineItems = new List<LineItem>(),
                        Products = _context.ProductMaster.Select(p => new Product { ProductCode = p.ProductCode, ProductName = p.ProductName }).ToList(),
                        Workcentres = new List<string> { "Workcentre1", "Workcentre2" /* add more workcentres here */ },
                        PurposesOfTesting = _context.PurposesOfTesting.Select(p => p.Description).ToList(),//new List<string> { "MDR", "USFDA", "In-house validation", "Regulatory submission", "Audit query", "Alternate vendor", "Other" },
                        Plants = _context.Plants.Select(p => p.PlantId).ToList(),
                        TestDetails = "",
                        RefNo = ""
                    };
                    return View(model);
                }
                else
                {
                    var userId = HttpContext.Session.GetInt32("userId");
                    var lineItemList = await _dataService.GetTestBatchItemList(userId,RefNo);
                    //var testDetails =  _context.TestBatch.Select(p=>p).Where(p=>p.Refno == RefNo).FirstOrDefault().TestDetails;
                    var testBatch = await _dataService.GetTestBatch(userId,RefNo);
                    var model = new TestFormViewModel
                    {
                        LineItems = lineItemList,
                        Products = _context.ProductMaster.Select(p => new Product { ProductCode = p.ProductCode, ProductName = p.ProductName }).ToList(),
                        Workcentres = new List<string> { "Workcentre1", "Workcentre2" /* add more workcentres here */ },
                        PurposesOfTesting = _context.PurposesOfTesting.Select(p => p.Description).ToList(),//new List<string> { "MDR", "USFDA", "In-house validation", "Regulatory submission", "Audit query", "Alternate vendor", "Other" },
                        Plants = _context.Plants.Select(p => p.PlantId).ToList(),
                        TestDetails = testBatch.TestDetails,
                        SelectedPlant = testBatch.Plant,
                        SelectedPurposeofTesting = testBatch.PurposesOfTesting,
                        RefNo = RefNo

                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTestForm(TestFormViewModel model)
        {
            // Redirect to a success page or return a success message
            var userId = HttpContext.Session.GetInt32("userId");
            var isCreatedSuccessfully = await _dataService.CreateTestBatch(model, userId);

            if (isCreatedSuccessfully == true)
            {
                return RedirectToAction("TestBatchList", "Dashboard");
            }
            
            return View("Index", model);
        }

        public IActionResult Success(TestFormViewModel model)
        {
            return View(model);
        }

        public async Task<IActionResult> TestBatchList()
        {
            if (HttpContext.Session.Get("userId") != null)
            {
                var userId = HttpContext.Session.GetInt32("userId");
                var model = await _dataService.GetTestBatchList(userId);

                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult EditTestBatch(string RefNo)
        {
            if (HttpContext.Session.Get("userId") != null)
            {
                return RedirectToAction("Index", "Dashboard", new {RefNo});
            }
            return RedirectToAction("Index", "Login");
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

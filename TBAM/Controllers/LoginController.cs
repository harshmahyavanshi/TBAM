using Microsoft.AspNetCore.Mvc;
using TBAM.Data;
using TBAM.Models;
using Microsoft.AspNetCore.Http;


namespace TBAM.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _context ;


        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context )
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Login logic goes here
                // For demonstration purposes, just return a success message
                var data =_context.User.Where(s => s.Email.Equals(model.Email) && s.Password.Equals(model.Password)).ToList();
                var userRole = _context.UserRole.Where(s => s.LoginId == data.FirstOrDefault().Id);
                if (data.Count() > 0)
                {
                    //add session
                    // HttpContext.Session["FullName"] = data.FirstOrDefault().FirstName +" "+ data.FirstOrDefault().LastName;
                    // Session["Email"] = data.FirstOrDefault().Email;
                    // Session["idUser"] = data.FirstOrDefault().idUser;
                    var fullName = data.FirstOrDefault().FirstName +" "+ data.FirstOrDefault().LastName;
                    var email = data.FirstOrDefault().Email;
                    HttpContext.Session.SetString(fullName, "FullName");
                    HttpContext.Session.SetString(email, "Email");
                    HttpContext.Session.SetInt32("userRole", userRole.FirstOrDefault().RoleId); //Change Id to RoleId.
                    HttpContext.Session.SetInt32("userId", data.FirstOrDefault().Id);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
                // TempData["SuccessMessage"] = "Login successful!";
                // return RedirectToAction("Index", "Dashboard");
            }

            return View(model);
        }
    
    }
}


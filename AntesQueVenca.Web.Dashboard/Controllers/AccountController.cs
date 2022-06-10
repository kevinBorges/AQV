using Microsoft.AspNetCore.Mvc;

namespace AntesQueVenca.Web.Dashboard.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                TempData["LOGIN"] = "Atenção, preencha seu e-mail e senha!";
                return View();
            }
            else if (Email.ToLower().Equals("admin@takeo.com") && Password.ToLower().Equals("admin123takeo"))
            {
                return RedirectToAction("Dashboard1", "Dashboard");
            }
            else 
            {
                TempData["LOGIN"] = "Atenção, e-mail ou senha inválidos!";
                return View();
            }
        }
    }
}

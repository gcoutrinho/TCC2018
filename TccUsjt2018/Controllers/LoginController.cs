using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.ViewModels.Login;

namespace TccUsjt2018.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if(model.Email == null || model.Email.Length == 0 || !model.Email.Contains("@"))
            {
                ModelState.AddModelError("Email", "E-mail inválido!");
            }

            if(model.Senha == null || model.Senha.Length < 6)
            {
                ModelState.AddModelError("Senha", "Senha inválida!");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
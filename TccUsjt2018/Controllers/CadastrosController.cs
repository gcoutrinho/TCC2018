using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TccUsjt2018.Controllers
{
    public class CadastrosController : Controller
    {
        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }

        [Authorize]
        public ActionResult Lote()
        {
            return View();
        }

        [Authorize]
        public ActionResult Estoque()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.ViewModels.Filtros;

namespace TccUsjt2018.Controllers
{
    public class FiltroRelatorioController : Controller
    {
        // GET: FiltroRelatorio
        public ActionResult Index()
        {
            FiltrosViewModel model = new FiltrosViewModel();

            CategoriaDAO categoriaDAO = new CategoriaDAO();
            //model.Categorias = categoriaDAO.GetAll();

            return View(model);
        }
    }
}
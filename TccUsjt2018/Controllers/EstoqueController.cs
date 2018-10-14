using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels.Estoques;

namespace TccUsjt2018.Controllers
{
    public class EstoqueController : Controller
    {
        // GET: Estoque
        public ActionResult Index()
        {
            EstoqueDAO dao = new EstoqueDAO();
            var lotes = dao.GetAll();

            var model = lotes.Select(x => new EstoqueViewModel()
            {
                CodigoEstoque = x.CodigoEstoque,
                Descricao = x.DescricaoEstoque,
            });

            return View(model);

        }

        public IEnumerable<SelectListItem> GetEstoque()
        {
            var dao = new EstoqueDAO();
            var produtos = dao.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.CodigoEstoque.ToString(),
                    Text = x.DescricaoEstoque,
                });

            return new SelectList(produtos, "Value", "Text");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EstoqueViewModel model)
        {
            EstoqueDAO dao = new EstoqueDAO();
            if (ModelState.IsValid)
            {
                Estoque estoque = new Estoque()
                {
                    DescricaoEstoque = model.Descricao,
                };

                dao.Salva(estoque);

                return RedirectToAction("Index", "Estoque");

            }
            else
            {
                return View("Create");
            }
        }
    }
}
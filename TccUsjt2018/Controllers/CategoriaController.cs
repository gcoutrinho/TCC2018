using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels.ProdutoCategoria;

namespace TccUsjt2018.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();

            ViewBag.Categorias = listaCategoria;

            return View();
        }

        public ActionResult FormularioCategoria()
        {
            return View();
        }

        public ActionResult Adiciona(CategoriaProdutoViewModel model)
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();

            if (ModelState.IsValid)
            {
                CategoriaProduto categoriaProduto = new CategoriaProduto
                {
                    NomeCategoria = model.NomeCategoria,
                    DescricaoCategoria = model.DescricaoCategoria,
                };

                categoriaDAO.Salva(categoriaProduto);

                return RedirectToAction("Index", "Categoria");
            }
            else
            {
                return View("FormularioCategoria");
            }
        }
    }
}
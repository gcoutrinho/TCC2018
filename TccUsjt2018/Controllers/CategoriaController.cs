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
            var categorias = categoriaDAO.GetAll();

            var model = categorias.Select(x => new CategoriaProdutoViewModel()
            {
                CodigoCategoria = x.CodigoCategoria,
                NomeCategoria = x.NomeCategoria,
                DescricaoCategoria = x.DescricaoCategoria,            
            });

            return View(model);
        }

        public IEnumerable<SelectListItem> GetCategoria()
        {
            var dao = new CategoriaDAO();
            var categorias = dao.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.CodigoCategoria.ToString(),
                    Text = x.NomeCategoria,
                });

            return new SelectList(categorias, "Value", "Text");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoriaProdutoViewModel model)
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
                return View("Create");
            }
        }

        [HttpGet]
        public ActionResult Consultar(int codigoCategoria)
        {
            var categoriaDao = new CategoriaDAO();
            var listacategoria = categoriaDao.GetById(codigoCategoria);
            var model = new CategoriaProdutoViewModel()
            {
                CodigoCategoria = listacategoria.CodigoCategoria,
                DescricaoCategoria = listacategoria.DescricaoCategoria,
            };

            return View(model);
        }


    }
}
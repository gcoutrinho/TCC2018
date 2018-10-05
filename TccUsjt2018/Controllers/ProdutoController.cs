using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels;
using TccUsjt2018.ViewModels.ProdutoCategoria;

namespace TccUsjt2018.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ProdutoDAO dao = new ProdutoDAO();
            var produtos = dao.GetAll();

            var model = produtos.Select(x => new ProdutoViewModel()
            {
                CodigoProduto = x.CodigoProduto,
                NomeProduto = x.NomeProduto,
                DataCadastro = x.DataCadastro,
                CategoriaProduto = x.CategoriaProduto,
                MarcaProduto = x.MarcaProduto
            }).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            ViewBag.Categorias = listaCategoria;

            ProdutoViewModel model = new ProdutoViewModel();
            return View(model);
        }

        public ActionResult Edit(int codigo)
        {
            ProdutoDAO dao = new ProdutoDAO();
            var produto = dao.GetById(codigo);

            var model = new ProdutoViewModel() {
                CodigoProduto = produto.CodigoProduto,
                NomeProduto = produto.NomeProduto,
                DataCadastro = produto.DataCadastro,
                CategoriaProduto = produto.CategoriaProduto,
                MarcaProduto = produto.MarcaProduto
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProdutoViewModel model)
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ProdutoViewModel model)
        {
            ProdutoDAO dao = new ProdutoDAO();

            if (ModelState.IsValid)
            {
                Produto produto = new Produto
                {
                    NomeProduto = model.NomeProduto,
                    MarcaProduto = model.MarcaProduto,
                    Categoria_CodigoCategoria = model.CategoriaProduto.CodigoCategoria,
                    DataCadastro = DateTime.Now,
                };

                dao.Salva(produto);

                return RedirectToAction("Index", "Produto");
            }
            else
            {
                CategoriaDAO categoriaDao = new CategoriaDAO();
                return View("FormularioProduto");
            }
        }
    }
}
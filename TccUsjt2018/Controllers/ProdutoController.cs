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

        public IEnumerable<SelectListItem> GetProdutos()
        {
            var dao = new ProdutoDAO();
            var produtos = dao.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.CodigoProduto.ToString(),
                    Text = x.NomeProduto,
                });

            return new SelectList(produtos, "Value", "Text");
        }

        public ActionResult Create()
        {
            CategoriaController categoriaController = new CategoriaController();
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();  

            var model = new ProdutoViewModel()
            {
                Categorias = categoriaController.GetCategoria(),
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ProdutoViewModel model)
        {
            ProdutoDAO dao = new ProdutoDAO();
            ModelState.Remove("Categorias");

            if (ModelState.IsValid)
            {
                Produto produto = new Produto
                {
                    NomeProduto = model.NomeProduto,
                    MarcaProduto = model.MarcaProduto,
                    Categoria_CodigoCategoria = model.SelectItemCategoriaId,
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
        public ActionResult Excluir(int codigo)
        {
            ProdutoDAO dao = new ProdutoDAO();
            var model = dao.GetById(codigo);
            var produto = new Produto()
            {
                NomeProduto = model.NomeProduto,
                CodigoProduto = model.CodigoProduto,
                Categoria_CodigoCategoria = model.CategoriaProduto.CodigoCategoria,
                CategoriaProduto = model.CategoriaProduto,
                DataCadastro = model.DataCadastro,
                MarcaProduto = model.MarcaProduto,
            };
            dao.Remove(produto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Consultar(int codigoProduto)
        {
            var produtoDao = new ProdutoDAO();
            var produto = produtoDao.GetById(codigoProduto);
            var categoriaDao = new CategoriaDAO();
            var listacategoria = categoriaDao.GetById(produto.Categoria_CodigoCategoria);
            var model = new ProdutoViewModel()
            {
                CodigoProduto = produto.CodigoProduto,
                MarcaProduto = produto.MarcaProduto,
                CategoriaProduto = produto.CategoriaProduto,
            };

            return View(model);
        }
    }
}
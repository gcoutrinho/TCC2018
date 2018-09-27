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

            return View(produtos);
        }

        public ActionResult FormularioProduto()
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            ViewBag.Categorias = listaCategoria;
            
            return View();
        }
        
        [HttpPost]
        public ActionResult Adiciona(ProdutoViewModel model)
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
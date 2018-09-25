using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels.Lote;

namespace TccUsjt2018.Controllers
{
    public class LoteController : Controller
    {
        // GET: Lote
        public ActionResult Index()
        {
            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();

            ViewBag.Lotes = listaLote;
            return View();
        }

        public ActionResult FormularioLote()
        {
            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();

            EstoqueDAO estoqueDAO = new EstoqueDAO();
            var listaEstoque = estoqueDAO.GetAll();

            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();

            ViewBag.Lotes = listaLote;
            ViewBag.Estoques = listaEstoque;
            ViewBag.Produtos = listaProduto;
            return View();
        }

        public ActionResult Adiciona(LoteViewModel model)
        {
            LoteDAO loteDAO = new LoteDAO();

            if (ModelState.IsValid)
            {
                Lote lote = new Lote
                {
                    DescricaoLote = model.DescricaoLote,
                    Estoque_CodigoEstoque = model.Estoque.CodigoEstoque,
                    Produto_CodigoProduto = model.Produto.CodigoProduto,
                    QuantidadeProduto = model.QuantidadeProduto,
                    ValidadeLote = model.ValidadeLote,                    
                };

                loteDAO.Salva(lote);

                return RedirectToAction("Index", "Lote");
            }
            else
            {
                return View("FormularioLote");
            }
        }
    }
}
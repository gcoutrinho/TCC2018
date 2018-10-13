using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels;
using TccUsjt2018.ViewModels.Lote;

namespace TccUsjt2018.Controllers
{
    public class LoteController : Controller
    {
        // GET: Lote
        public ActionResult Index()
        {
            LoteDAO loteDAO = new LoteDAO();
            var lotes = loteDAO.GetAll();          

            var model = lotes.Select(x => new LoteViewModel()
            {
                CodigoLote = x.CodigoLote,
                DescricaoLote = x.DescricaoLote,
                Produto = x.Produto,
                ValidadeLote = x .ValidadeLote,
                QuantidadeProduto = x.QuantidadeProduto,
            });

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
            var model = new LoteViewModel()
            {
                Produtos = GetProdutos(),
                Estoques = GetEstoque(),

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LoteViewModel model)
        {
            LoteDAO loteDAO = new LoteDAO();

            if (ModelState.IsValid)
            {
                Lote lote = new Lote
                {
                    DescricaoLote = model.DescricaoLote,
                    Estoque_CodigoEstoque = (int)model.SelectItemEstoqueId,
                    Produto_CodigoProduto = (int)model.SelectItemProdutoId,
                    QuantidadeProduto = model.QuantidadeProduto,
                    ValidadeLote = model.ValidadeLote,                    
                };

                loteDAO.Salva(lote);

                return RedirectToAction("Index", "Lote");
            }
            else
            {
                return View("Create");
            }
        }
    }
}
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

        public IEnumerable<SelectListItem> GetLotes()
        {
            var dao = new LoteDAO();
            var lotes = dao.GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.CodigoLote.ToString(),
                    Text = x.DescricaoLote,
                });

            return new SelectList(lotes, "Value", "Text");
        }

        public ActionResult Create()
        {
            EstoqueController estoqueController = new EstoqueController();
            ProdutoController produtoController = new ProdutoController();
            var model = new LoteViewModel()
            {
                Produtos = produtoController.GetProdutos(),
                Estoques = estoqueController.GetEstoque(),

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

        public ActionResult BaixaLote(LoteViewModel model)
        {
            var loteDAO = new LoteDAO();
            var loteatual = loteDAO.GetById(model.CodigoLote);

            var lote = new Lote()
            {
                CodigoLote = model.CodigoLote,
                QuantidadeProduto = loteatual.QuantidadeProduto-model.QuantidadeBaixa, //Subtraindo a quantidade atual.
            };

            loteDAO.Update(lote);

            var baixaDAO = new BaixaDAO();
            var baixa = new Baixa()
            {
                DataBaixa = DateTime.Now,
                Lote_CodigoLote = model.CodigoLote,
                Produto_CodigoProduto = model.Produto_CodigoProduto,
                QuantidadeBaixa = model.QuantidadeBaixa,
            };

            baixaDAO.Salva(baixa);

            return RedirectToAction("Index");
        }

    }
}
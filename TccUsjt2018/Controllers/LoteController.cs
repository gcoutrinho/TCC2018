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
        [Authorize]
        public ActionResult Index()
        {
            LoteDAO loteDAO = new LoteDAO();
            var lotes = loteDAO.GetAll().Where(x => x.QuantidadeProduto > 0).OrderBy(x => x.ValidadeLote);

            EstoqueDAO estoqueDao = new EstoqueDAO();
            var estoque = estoqueDao.GetAll();

            var model = lotes.Select(x => new LoteViewModel()
            {
                CodigoLote = x.CodigoLote,
                DescricaoLote = x.DescricaoLote,
                Produto = x.Produto,
                ValidadeLote = x.ValidadeLote,
                QuantidadeProduto = x.QuantidadeProduto,
                LocalEstoque = estoqueDao.GetById(x.Estoque_CodigoEstoque).DescricaoEstoque,        
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

        [Authorize]
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
        [Authorize]
        public ActionResult Create(LoteViewModel model)
        {
            LoteDAO loteDAO = new LoteDAO();

            if (ModelState.IsValid && model.DescricaoLote != "" && model.DescricaoLote != null && model.QuantidadeProduto > 0 )
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
                ModelState.AddModelError("", "Quantidade invalida");
                return View("ErroQuantidade");
               
            }

        }

        [Authorize]
        public ActionResult BaixaLote(int codigoLote)
        {
            LoteDAO dao = new LoteDAO();
            var lote = dao.GetById(codigoLote);

            var model = new LoteViewModel()
            {
                CodigoLote = codigoLote,
                DescricaoLote = lote.DescricaoLote,
                QuantidadeProduto = lote.QuantidadeProduto,
                ValidadeLote = lote.ValidadeLote,
            };

            return View(model);
        }


        [HttpPost]
        [Authorize]
        public ActionResult BaixaLote(LoteViewModel model, int codigoLote)
        {
            var loteDAO = new LoteDAO();
            var loteAtual = loteDAO.GetById(codigoLote);

            if (model.QuantidadeBaixa > loteAtual.QuantidadeProduto)
            {
                ModelState.AddModelError("", "Quantidade invalida");
                return View("ErroQuantidade");
            }
            else
            {
                var lote = new Lote()
                {
                    CodigoLote = loteAtual.CodigoLote,
                    QuantidadeProduto = (loteAtual.QuantidadeProduto - model.QuantidadeBaixa), //Subtraindo a quantidade atual.
                };



                loteDAO.Update(lote);

                var baixaDAO = new BaixaDAO();
                var baixa = new Baixa()
                {
                    DataBaixa = DateTime.Now,
                    Lote_CodigoLote = loteAtual.CodigoLote,
                    Produto_CodigoProduto = loteAtual.Produto_CodigoProduto,
                    QuantidadeBaixa = model.QuantidadeBaixa,
                };

                baixaDAO.Salva(baixa);

                return RedirectToAction("Index");
            }

            
        }

        [HttpGet]
        [Authorize]
        public ActionResult Consultar(int codigoLote)
        {
            var dao = new LoteDAO();
            var lote = dao.GetById(codigoLote);
            var daoEstoque = new EstoqueDAO();
            var estoque = daoEstoque.GetById(lote.Estoque_CodigoEstoque);
            var produtoDao = new ProdutoDAO();
            var produto = produtoDao.GetById(lote.Produto_CodigoProduto);
            var model = new LoteViewModel()
            {
                CodigoLote = lote.CodigoLote,
                DescricaoLote = lote.DescricaoLote,
                QuantidadeProduto = lote.QuantidadeProduto,
                Estoque = estoque,
                Produto = produto,
                ValidadeLote = lote.ValidadeLote,
            };

            return View(model);
        }

    }
}
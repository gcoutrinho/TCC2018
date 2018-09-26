using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels;
using TccUsjt2018.ViewModels.Relatório;

namespace TccUsjt2018.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RelatorioProduto(string nomeProduto, string nomeCategoria, DateTime dataValidade)
        {
            RelatorioViewModel model = new RelatorioViewModel();
            model.ListaProdutoViewModel = new List<ProdutoViewModel>();

            ProdutoDAO produtoDAO = new ProdutoDAO();

            var listaProduto = produtoDAO.GetAll().Where(x => x.NomeProduto == nomeProduto 
                                                   && x.CategoriaProduto.NomeCategoria == nomeCategoria);

            LoteDAO loteDAO = new LoteDAO();

            var listaLote = loteDAO.GetAll().Where(x => x.ValidadeLote == dataValidade);           

            if (nomeProduto != null && nomeCategoria != null && dataValidade != null)
            {
                var resultQuery = from p in listaProduto
                                  join l in listaLote
                                  on p.CodigoProduto equals l.Produto.CodigoProduto
                                  select new
                                  {
                                      p.NomeProduto,
                                      p.CategoriaProduto.NomeCategoria,
                                      l.ValidadeLote,
                                  };

                foreach (var item in resultQuery)
                {
                    ProdutoViewModel modelproduto = new ProdutoViewModel();
                    modelproduto.NomeProduto = item.NomeProduto;
                    modelproduto.CategoriaProduto.NomeCategoria = item.NomeCategoria;
                    modelproduto.ValidadeLote = item.ValidadeLote;

                    model.ListaProdutoViewModel.Add(modelproduto);
                }

                return View(model);
                                 
            }

            return null;
        }
    }
}
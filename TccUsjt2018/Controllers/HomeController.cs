using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult RetornaRankingProdutos()
        {
            //Retorna Grafico de Colunas
            //Esse grafico retorna a quantidade de produtos totais.

            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();

            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();

            var resultQuery = from l in listaLote
                              join p in listaProduto
                              on l.Produto_CodigoProduto equals p.CodigoProduto
                              group l by new { p.NomeProduto } into g
                              select new
                              {
                                  NomeProduto = g.Key.NomeProduto,
                                  QuantidadeProduto = g.Sum(x => x.QuantidadeProduto)
                              };
            return View(resultQuery);
        }

        public HashSet<string> VerificaSituacaoLote()
        {
            var loteDAO = new LoteDAO();

            var baixaDAO = new BaixaDAO();

            //var produtoBaixado = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 1)&& x.Produto_CodigoProduto == codigo).Sum(x => x.QuantidadeBaixa);

            //var produtoBaixado = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 1));

            var loteAtual = loteDAO.GetAll();
            var produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            //.Where(x => x.ValidadeLote.Month == DateTime.Now.Month);
            HashSet<string> lista = new HashSet<string>();
            var texto = "";
            foreach (var aux in loteAtual)
            {
                //var loteAtualif = loteDAO.GetAll().Where(x => x.ValidadeLote.Month == DateTime.Now.Month && x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeProduto);
                var loteAtualif = loteDAO.GetAll().Where(x => x.ValidadeLote.Month == DateTime.Now.Month && x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeProduto);
                var produtoBaixadoif = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 1)&& x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeBaixa);
                var nomeproduto = produtoDAO.GetById(aux.Produto_CodigoProduto);

                if (loteAtualif > produtoBaixadoif)
                {                    
                    texto = "Ops, você não conseguira vender " + nomeproduto.NomeProduto + " " + (loteAtualif - produtoBaixadoif) + "produtos, faça uma promoção!";
                    lista.Add(texto);
                }
                else
                {
                    ViewBag.Mensagem = "";
                }
            }

            return lista;



            //var loteAtual = loteDAO.GetAll().Where(x => x.ValidadeLote.Month == DateTime.Now.Month && x.Produto_CodigoProduto == codigo).Sum(x => x.QuantidadeProduto);

            //if (loteAtual > produtoBaixado)
            //{
            //    ViewBag.Mensagem = "Ops, você não conseguira vender" + (loteAtual - produtoBaixado) + "produtos, faça uma promoção!";
            //}
            //else
            //{
            //    ViewBag.Mensagem = "";
            //}



            //return RedirectToAction("Index");
        }

        //public ActionResult BaixaTresMeses()
        //{
            

        //}


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels;
using TccUsjt2018.ViewModels.Lote;

namespace TccUsjt2018.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dados = VerificaSituacaoLote();
            return View(dados);
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
        [HttpGet]
        public JsonResult RetornaRankingProdutos()
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
            
            var lista = new List<RankingProdutoViewModel>();
            foreach (var item in resultQuery)
            {
                lista.Add(new RankingProdutoViewModel() {
                    NomeProduto = item.NomeProduto,
                    QuantidadeProduto = item.QuantidadeProduto
                });
            }

            var js = new JavaScriptSerializer();
            var json = js.Serialize(lista);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //public HashSet<string> VerificaSituacaoLote()
        //{
        //    var loteDAO = new LoteDAO();

        //    var baixaDAO = new BaixaDAO();

        //    //var produtoBaixado = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 1)&& x.Produto_CodigoProduto == codigo).Sum(x => x.QuantidadeBaixa);

        //    //var produtoBaixado = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 1));

        //    var loteAtual = loteDAO.GetAll();
        //    var produtoDAO = new ProdutoDAO();
        //    var listaProduto = produtoDAO.GetAll();
        //    //.Where(x => x.ValidadeLote.Month == DateTime.Now.Month);
        //    HashSet<string> lista = new HashSet<string>();
        //    var texto = "";
        //    foreach (var aux in loteAtual)
        //    {
        //        //var loteAtualif = loteDAO.GetAll().Where(x => x.ValidadeLote.Month == DateTime.Now.Month && x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeProduto);
        //        var loteAtualif = loteDAO.GetAll().Where(x => x.ValidadeLote.Month == DateTime.Now.Month && x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeProduto);
        //        var produtoBaixadoif = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 1)&& x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeBaixa);
        //        var nomeproduto = produtoDAO.GetById(aux.Produto_CodigoProduto);

        //        if (loteAtualif > produtoBaixadoif)
        //        {                    
        //            texto = "Ops, você não conseguira vender " + nomeproduto.NomeProduto + " " + (loteAtualif - produtoBaixadoif) + "produtos, faça uma promoção!";
        //            lista.Add(texto);
        //        }
        //        else
        //        {
        //            ViewBag.Mensagem = "";
        //        }
        //    }

        //    return lista;



        //    //var loteAtual = loteDAO.GetAll().Where(x => x.ValidadeLote.Month == DateTime.Now.Month && x.Produto_CodigoProduto == codigo).Sum(x => x.QuantidadeProduto);

        //    //if (loteAtual > produtoBaixado)
        //    //{
        //    //    ViewBag.Mensagem = "Ops, você não conseguira vender" + (loteAtual - produtoBaixado) + "produtos, faça uma promoção!";
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.Mensagem = "";
        //    //}



        //    //return RedirectToAction("Index");
        //}

        //public ActionResult BaixaTresMeses()
        //{


        //}

        public LoteViewModel VerificaSituacaoLote()
        {
            var loteDAO = new LoteDAO();
            var baixaDAO = new BaixaDAO();
            var listabaixa = baixaDAO.GetAll();
            var loteAtual = loteDAO.GetAll();
            var produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            var baixaTotal = 0;

            HashSet<string> lista = new HashSet<string>();
            var texto = "";
            foreach (var aux in loteAtual)
            {

                var lotemesatual = loteDAO.GetAll().Where(x => x.ValidadeLote.Month == DateTime.Now.Month && x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeProduto);
                var baixaprodmes1 = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 1) && x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeBaixa);
                var baixaprodmes2 = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 2) && x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeBaixa);
                var baixaprodmes3 = baixaDAO.GetAll().Where(x => x.DataBaixa.Month == (DateTime.Now.Month - 3) && x.Produto_CodigoProduto == aux.Produto_CodigoProduto).Sum(x => x.QuantidadeBaixa);
                var nomeproduto = produtoDAO.GetById(aux.Produto_CodigoProduto);

                var diasrestantes = aux.ValidadeLote.Day - DateTime.Now.Day;


                if (baixaprodmes1 > 0 && baixaprodmes2 > 0 && baixaprodmes3 > 0) // 3 meses
                {
                    baixaTotal = ((baixaprodmes1 + baixaprodmes2 + baixaprodmes3) / 3);
                }
                else if (baixaprodmes1 == 0 && baixaprodmes2 > 0 && baixaprodmes3 > 0) // 2 e 3 meses
                {
                    baixaTotal = (baixaprodmes2 + baixaprodmes3) / 2;
                }
                else if (baixaprodmes2 == 0 && baixaprodmes1 > 0 && baixaprodmes3 > 0) //1 e 3 meses
                {
                    baixaTotal = (baixaprodmes1 + baixaprodmes3) / 2;
                }
                else if (baixaprodmes3 == 0 && baixaprodmes1 > 0 && baixaprodmes2 > 0) // 1 e 2
                {
                    baixaTotal = (baixaprodmes1 + baixaprodmes2) / 2;
                }
                else if (baixaprodmes2 == 0 && baixaprodmes3 == 0 && baixaprodmes1 > 0)
                {
                    baixaTotal = baixaprodmes1;
                }
                else if (baixaprodmes1 == 0 && baixaprodmes3 == 0 && baixaprodmes2 > 0)
                {
                    baixaTotal = baixaprodmes2;
                }
                else if (baixaprodmes2 == 0 && baixaprodmes1 == 0 && baixaprodmes3 > 0)
                {
                    baixaTotal = baixaprodmes3;
                }
                else
                {
                    baixaTotal = 0;
                }
                //Verificação do warning de baixas
                if (lotemesatual > baixaTotal && baixaTotal > 0 && diasrestantes < 16)
                {
                    var diferençaresultado = lotemesatual - baixaTotal;
                    var resultadoporcentagem = (float)((diferençaresultado * 100) / lotemesatual);
                    texto = "Ops, você não conseguira vender: " + resultadoporcentagem + " % de " + nomeproduto.NomeProduto + " faça uma promoção!";
                    lista.Add(texto);
                }
                else
                {
                    ViewBag.Mensagem = "";
                }


            }
            var model = new LoteViewModel()
            {
                ListaAlerta = lista,
            };
            return model;
        }
    }
}
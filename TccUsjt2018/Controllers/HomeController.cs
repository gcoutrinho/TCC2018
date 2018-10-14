using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;

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

        public ActionResult BaixaTresMeses()
        {
            

        }


    }
}
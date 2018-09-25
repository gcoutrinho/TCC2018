using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;

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
            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();

            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();

            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();

            //if ()
            //{

            //}

            //var Resultado = from p in listaProduto
            //                join l in listaLote on p.CodigoProduto equals l.Produto_CodigoProduto
                            

            //                join c in listaCategoria on p.CategoriaProduto.CodigoCategoria equals c.CodigoCategoria
                            
            //                c.NomeCategoria = nomeCategoria

                            
            //                select new
            //                {
            //                    Nome = p.NomeProduto,
            //                    Animal = l.nome,
            //                    Tipo = _T.nome
            //                };








        }
    }
}
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
            var model = new RelatorioViewModel();
            model.ListaProdutoViewModel = new List<ProdutoViewModel>();
            return View(model);
        }

        public ActionResult RelatorioProduto(string nomeCategoria, DateTime dataValidade)
        {

            RelatorioViewModel model = new RelatorioViewModel
            {
                ListaProdutoViewModel = new List<ProdutoViewModel>()
            };

            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            var filtroCategoria = new List<CategoriaProduto>();
            foreach (var item in listaCategoria)
            {
                if (item.NomeCategoria.Equals(nomeCategoria))
                {
                    filtroCategoria.Add(item);
                }
            }

            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            //var filtroProduto = new List<Produto>();
            //foreach (var item in listaProduto)
            //{
            //    if (item.NomeProduto.Equals(nomeProduto))
            //    {
            //        filtroProduto.Add(item);
            //    }
            //}

            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();
            var filtroLote = new List<Lote>();
            foreach (var item in listaLote)
            {
                if (item.ValidadeLote.Equals(dataValidade))
                {
                    filtroLote.Add(item);
                }
            }
            if (nomeCategoria != null && dataValidade != null)
            {
                var resultQuery = from p in listaProduto
                                  join l in filtroLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  join c in filtroCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  select new
                                  {
                                      p.NomeProduto,
                                      c.NomeCategoria,
                                      l.ValidadeLote,
                                  };
                return View();
            }
            else if (nomeCategoria == null && dataValidade != null)
            {
                var resultQuery = from p in listaProduto
                                  join l in filtroLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  select new
                                  {
                                      p.NomeProduto,
                                      p.MarcaProduto,
                                      l.ValidadeLote,
                                  };
                return View();
            }
            else if (nomeCategoria == null && dataValidade == null)
            {
                var todosLote = loteDAO.GetAll();
                var todosProdutos = produtoDAO.GetAll();
                var todasCategorias = categoriaDAO.GetAll();

                var resultQuery = from p in todosProdutos
                                  join l in todosLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  join c in todasCategorias
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  select new
                                  {
                                      p.NomeProduto,
                                      c.NomeCategoria,
                                      p.MarcaProduto,
                                      l.ValidadeLote,
                                  };
                return View();
            }

            return null;
        }

        public ActionResult RelatorioLote(string nomeLote, string nomeCategoria, DateTime dataValidade)
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            var filtroCategoria = new List<CategoriaProduto>();
            foreach (var item in listaCategoria)
            {
                if (item.NomeCategoria.Equals(nomeCategoria))
                {
                    filtroCategoria.Add(item);
                }
            }

            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();
            var filtroLote = new List<Lote>();
            foreach (var item in listaLote)
            {
                if (item.ValidadeLote.Equals(dataValidade) && item.DescricaoLote.Equals(nomeLote))
                {
                    filtroLote.Add(item);
                }
            }

            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            var filtroProduto = new List<Produto>();


            if (nomeLote != null && nomeCategoria != null && dataValidade != null)
            {
                var resultQuery = from l in filtroLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in filtroCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  orderby l.ValidadeLote
                                  select new
                                  {
                                      l.DescricaoLote,
                                      p.NomeProduto,
                                      c.NomeCategoria,
                                      l.ValidadeLote,
                                      l.QuantidadeProduto,
                                  };
                return View();

            }
            else
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  orderby l.ValidadeLote
                                  select new
                                  {
                                      l.DescricaoLote,
                                      p.NomeProduto,
                                      c.NomeCategoria,
                                      l.ValidadeLote,
                                      l.QuantidadeProduto,
                                  };
                return View();

            }
        }
    }
}
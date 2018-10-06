using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels;
using TccUsjt2018.ViewModels.Filtros;
using TccUsjt2018.ViewModels.Relatório;

namespace TccUsjt2018.Controllers
{
    public class RelatorioController : Controller
    {
        public ActionResult Index()
        {
            FiltrosViewModel model = new FiltrosViewModel();
            CategoriaDAO categoria = new CategoriaDAO();
            model.Categorias = categoria.GetAll();

            return View(model);
        }

        public ActionResult RelatorioProduto(FiltrosViewModel filtro)
        {
           
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            var filtroCategoria = new List<CategoriaProduto>();
            foreach (var item in listaCategoria)
            {
                if (item.NomeCategoria.Equals(filtro.NomeCategoria))
                {
                    filtroCategoria.Add(item);
                }
            }

            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            var filtroProduto = new List<Produto>();
            foreach (var item in listaProduto)
            {
                if (item.NomeProduto.Equals(filtro.NomeCategoria))
                {
                    filtroProduto.Add(item);
                }
            }

            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();
            var filtroLote = new List<Lote>();
            foreach (var item in listaLote)
            {
                if (item.ValidadeLote.Equals(filtro.DataVencimento))
                {
                    filtroLote.Add(item);
                }
            }
            if (filtro.NomeCategoria != null && filtro.DataVencimento != null)
            {
                var resultQuery = from p in listaProduto
                                  join l in filtroLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  join c in filtroCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  select new RelatorioProdutoViewModel()
                                  {
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      DataVencimento=  l.ValidadeLote,
                                      Marca = p.MarcaProduto,
                                  };
                return View();
            }
            else if (filtro.NomeCategoria == null && filtro.DataVencimento != null)
            {
                var resultQuery = from p in listaProduto
                                  join l in filtroLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  join c in filtroCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  select new RelatorioProdutoViewModel
                                  {
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      DataVencimento = l.ValidadeLote,
                                      Marca = p.MarcaProduto,
                                  };
                return View();
            }
            else if (filtro.NomeCategoria == null && filtro.DataVencimento == null)
            {
                var todosLote = loteDAO.GetAll();
                var todosProdutos = produtoDAO.GetAll();
                var todasCategorias = categoriaDAO.GetAll();

                var resultQuery = from p in listaProduto
                                  join l in filtroLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  join c in filtroCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  select new RelatorioProdutoViewModel
                                  {
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      DataVencimento = l.ValidadeLote,
                                      Marca = p.MarcaProduto,
                                  };

                return View(resultQuery);
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
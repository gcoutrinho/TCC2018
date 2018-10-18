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
            CategoriaController categoriaController = new CategoriaController();
            ProdutoController produtoController = new ProdutoController();
            var model = new FiltrosViewModel
            {
                Categorias = categoriaController.GetCategoria(),
                Produtos = produtoController.GetProdutos(),
            };

            return View(model);
        }      

        public ActionResult RelatorioProduto(FiltrosViewModel filtro)
        {
            filtro.DataVencimento = null;

            // Retorna a lista de categoria de acordo com o filtro
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            var filtroCategoria = new List<CategoriaProduto>();
            foreach (var item in listaCategoria)
            {
                if (item.CodigoCategoria.Equals(filtro.SelectItemCategoriaId))
                {
                    filtroCategoria.Add(item);
                }
            }

            // Retorna a lista de produtos de acordo com o filtro
            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            var filtroProduto = new List<Produto>();
            foreach (var item in listaProduto)
            {
                if (item.CodigoProduto.Equals(filtro.SelectItemProdutoId))
                {
                    filtroProduto.Add(item);
                }
            }

            // Retorna a lista de lotes de acordo com o filtro
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
            if (filtro.SelectItemCategoriaId != null && filtro.DataVencimento != null && filtro.SelectItemProdutoId != null)
            {
                var resultQuery = from p in filtroProduto
                                  join l in filtroLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  join c in filtroCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria                      
                                  
                                  select new RelatorioProdutoViewModel()
                                  {
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      DataVencimento = l.ValidadeLote,
                                      Marca = p.MarcaProduto,
                                  };

                return View(resultQuery);
            }
            else if (filtro.SelectItemCategoriaId != null && filtro.DataVencimento == null && filtro.NomeProduto == null)
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
                return View(resultQuery);
            }
            else if (filtro.SelectItemCategoriaId == null && filtro.DataVencimento == null && filtro.NomeProduto == null)
            {
                var todosLote = loteDAO.GetAll();
                var todosProdutos = produtoDAO.GetAll();
                var todasCategorias = categoriaDAO.GetAll();

                var resultQuery = from p in todosProdutos
                                  join l in todosLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  join c in todasCategorias
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

        public ActionResult IndexRelatorioLote()
        {
            LoteController loteController = new LoteController();
            ProdutoController produtoController = new ProdutoController();
            CategoriaController categoriaController = new CategoriaController();

            var model = new FiltrosViewModel()
            {
                Lotes = loteController.GetLotes(),
                Categorias = categoriaController.GetCategoria(),
                Produtos = produtoController.GetProdutos(),

            };
  
            return View(model);
        }

        public ActionResult RelatorioLote(FiltrosViewModel filtro)
        {
            filtro.DataVencimento = null;
          
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            var filtroCategoria = new List<CategoriaProduto>();
            foreach (var item in listaCategoria)
            {
                if (item.CodigoCategoria.Equals(filtro.SelectItemCategoriaId))
                {
                    filtroCategoria.Add(item);
                }
            }

            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();
            var filtroLote = new List<Lote>();
            foreach (var item in listaLote)
            {
                if (item.CodigoLote.Equals(filtro.SelectItemLoteId))
                {
                    filtroLote.Add(item);
                }
            }

            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            var filtroProduto = new List<Produto>();
            foreach (var item in listaProduto)
            {
                if (item.CodigoProduto.Equals(filtro.SelectItemProdutoId))
                {
                    filtroProduto.Add(item);
                }
            }


            if (filtro.SelectItemLoteId != null && filtro.SelectItemCategoriaId != null && filtro.SelectItemProdutoId != null)
            {
                var resultQuery = from l in filtroLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in filtroCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  orderby l.ValidadeLote
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                  };
                return View(resultQuery);

            }
            else
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  orderby l.ValidadeLote
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                  };
                return View(resultQuery);

            }
        }
    }
}
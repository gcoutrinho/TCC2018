﻿using System;
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
        [Authorize]
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

        [Authorize]
        public ActionResult RelatorioProduto(FiltrosViewModel filtro)
        {            

            CategoriaDAO categoriaDAO = new CategoriaDAO();
            ProdutoDAO produtoDAO = new ProdutoDAO();
            LoteDAO loteDAO = new LoteDAO();

            var todosLote = loteDAO.GetAll();
            var todosProdutos = produtoDAO.GetAll();
            var todasCategorias = categoriaDAO.GetAll();

            if (filtro.SelectItemCategoriaId != null && filtro.SelectItemProdutoId != null)
            {
                var resultQuery = from p in todosProdutos
                                  join c in todasCategorias
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where p.Categoria_CodigoCategoria == filtro.SelectItemCategoriaId &&
                                  p.CodigoProduto == filtro.SelectItemProdutoId
                                  select new RelatorioProdutoViewModel
                                  {
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      Marca = p.MarcaProduto,
                                  };
                return View(resultQuery.Distinct());
            }
            else if (filtro.SelectItemCategoriaId != null && filtro.NomeProduto == null)
            {   
                var resultQuery = from p in todosProdutos                             
                                  join c in todasCategorias
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where p.Categoria_CodigoCategoria == filtro.SelectItemCategoriaId                 
                                  select new RelatorioProdutoViewModel
                                  {
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,                                 
                                      Marca = p.MarcaProduto,
                                  };
                return View(resultQuery.Distinct());
            }
            else if (filtro.SelectItemProdutoId == null && filtro.SelectItemCategoriaId != null)
            {
                var resultQuery = from p in todosProdutos
                                  join c in todasCategorias
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where p.CodigoProduto == filtro.SelectItemProdutoId
                                  select new RelatorioProdutoViewModel
                                  {
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      Marca = p.MarcaProduto,
                                  };
                return View(resultQuery.Distinct());
            }
            else
            {
                var resultQuery = from p in todosProdutos
                                  join c in todasCategorias
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria                                
                                  select new RelatorioProdutoViewModel
                                  {
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      Marca = p.MarcaProduto,
                                  };
                return View(resultQuery.Distinct());
            }
        }

        [Authorize]
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

        [Authorize]
        public ActionResult RelatorioLote(FiltrosViewModel filtro)
        {
            
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();
            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();

            //if (filtro.SelectItemLoteId != null && filtro.SelectItemCategoriaId != null && filtro.SelectItemProdutoId != null)
            //{
            //    var resultQuery = from l in filtroLote
            //                      join p in listaProduto
            //                      on l.Produto_CodigoProduto equals p.CodigoProduto
            //                      join c in filtroCategoria
            //                      on p.Categoria_CodigoCategoria equals c.CodigoCategoria
            //                      orderby l.ValidadeLote
            //                      select new RelatorioLoteViewModel
            //                      {
            //                          DescricaoLote = l.DescricaoLote,
            //                          NomeProduto = p.NomeProduto,
            //                          NomeCategoria = c.NomeCategoria,
            //                          ValidadeLote = l.ValidadeLote,
            //                          QuantidadeProduto = l.QuantidadeProduto,
            //                      };
            //    return View(resultQuery);

            //}
            if (filtro.SelectItemLoteId == null && filtro.SelectItemCategoriaId == null && filtro.SelectItemProdutoId != null)
            {
                //var resultQuery = from l in listaLote
                //                  join p in listaProduto
                //                  on l.Produto_CodigoProduto equals p.CodigoProduto
                //                  join c in listaCategoria
                //                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                //                  orderby l.ValidadeLote
                //                  select new RelatorioLoteViewModel
                //                  {
                //                      DescricaoLote = l.DescricaoLote,
                //                      NomeProduto = p.NomeProduto,
                //                      NomeCategoria = c.NomeCategoria,
                //                      ValidadeLote = l.ValidadeLote,
                //                      QuantidadeProduto = l.QuantidadeProduto,
                //                  };
                //return View(resultQuery);

                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where l.Produto_CodigoProduto == filtro.SelectItemProdutoId
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                  };
                return View(resultQuery.Distinct());
            }
            else
            {
                return null;
            }
        }
    }
}
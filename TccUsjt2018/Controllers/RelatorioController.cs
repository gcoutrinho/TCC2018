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
            else if (filtro.SelectItemCategoriaId == null && filtro.SelectItemProdutoId != null)
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
        public ActionResult IndexRelatorioBaixa()
        {
            BaixaController baixaController = new BaixaController();
            ProdutoController produtoController = new ProdutoController();
            CategoriaController categoriaController = new CategoriaController();

            var model = new FiltrosViewModel()
            {
                Baixas = baixaController.GetBaixas(),
                Categorias = categoriaController.GetCategoria(),
                Produtos = produtoController.GetProdutos(),

            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RelatorioLote(FiltrosViewModel filtro)
        {

            CategoriaDAO categoriaDAO = new CategoriaDAO();
            var listaCategoria = categoriaDAO.GetAll();
            LoteDAO loteDAO = new LoteDAO();
            var listaLote = loteDAO.GetAll();
            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            EstoqueDAO estoqueDao = new EstoqueDAO();
            //todos
            if (filtro.SelectItemLoteId != null && filtro.SelectItemCategoriaId != null && filtro.SelectItemProdutoId != null)
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where l.Produto_CodigoProduto == filtro.SelectItemProdutoId
                                  && p.CodigoProduto == filtro.SelectItemProdutoId
                                  && p.Categoria_CodigoCategoria == filtro.SelectItemCategoriaId
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                      LocalEstoque = estoqueDao.GetById(l.Estoque_CodigoEstoque).DescricaoEstoque,
                                  };
                return View(resultQuery.Distinct());

            }
            //seleciona produto e categoria aaaaa 
            else if (filtro.SelectItemLoteId == null && filtro.SelectItemCategoriaId != null && filtro.SelectItemProdutoId != null)
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where p.CodigoProduto == filtro.SelectItemProdutoId
                                  && c.CodigoCategoria == filtro.SelectItemCategoriaId
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                      LocalEstoque = estoqueDao.GetById(l.Estoque_CodigoEstoque).DescricaoEstoque,
                                  };
                return View(resultQuery.Distinct());
            }
            //seleciona produto e lote
            else if (filtro.SelectItemLoteId != null && filtro.SelectItemCategoriaId == null && filtro.SelectItemProdutoId != null)
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where p.CodigoProduto == filtro.SelectItemProdutoId
                                  && l.CodigoLote == filtro.SelectItemLoteId
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                      LocalEstoque = estoqueDao.GetById(l.Estoque_CodigoEstoque).DescricaoEstoque,
                                  };
                return View(resultQuery.Distinct());

            }
            //lote e categoria 
            else if (filtro.SelectItemLoteId != null && filtro.SelectItemCategoriaId != null && filtro.SelectItemProdutoId == null)
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where l.CodigoLote == filtro.SelectItemLoteId
                                  && c.CodigoCategoria == filtro.SelectItemCategoriaId
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                      LocalEstoque = estoqueDao.GetById(l.Estoque_CodigoEstoque).DescricaoEstoque,
                                  };
                return View(resultQuery.Distinct());
            }
            //produto
            else if (filtro.SelectItemLoteId == null && filtro.SelectItemCategoriaId == null && filtro.SelectItemProdutoId != null)
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where p.CodigoProduto == filtro.SelectItemProdutoId
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                      LocalEstoque = estoqueDao.GetById(l.Estoque_CodigoEstoque).DescricaoEstoque,
                                  };
                return View(resultQuery.Distinct());
            }
            //lote
            else if (filtro.SelectItemLoteId != null && filtro.SelectItemCategoriaId == null && filtro.SelectItemProdutoId == null)
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where l.CodigoLote == filtro.SelectItemLoteId
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                      LocalEstoque = estoqueDao.GetById(l.Estoque_CodigoEstoque).DescricaoEstoque,
                                  };
                return View(resultQuery.Distinct());
            }

            //categoria
            else if (filtro.SelectItemLoteId == null && filtro.SelectItemCategoriaId != null && filtro.SelectItemProdutoId == null)
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  where c.CodigoCategoria == filtro.SelectItemCategoriaId
                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                      LocalEstoque = estoqueDao.GetById(l.Estoque_CodigoEstoque).DescricaoEstoque,
                                  };
                return View(resultQuery.Distinct());
            }

            else
            {
                var resultQuery = from l in listaLote
                                  join p in listaProduto
                                  on l.Produto_CodigoProduto equals p.CodigoProduto
                                  join c in listaCategoria on
                                  p.Categoria_CodigoCategoria equals c.CodigoCategoria

                                  select new RelatorioLoteViewModel
                                  {
                                      DescricaoLote = l.DescricaoLote,
                                      NomeProduto = p.NomeProduto,
                                      NomeCategoria = c.NomeCategoria,
                                      ValidadeLote = l.ValidadeLote,
                                      QuantidadeProduto = l.QuantidadeProduto,
                                      LocalEstoque = estoqueDao.GetById(l.Estoque_CodigoEstoque).DescricaoEstoque,
                                  };
                return View(resultQuery.Distinct());
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult RelatorioBaixa(FiltrosViewModel filtro)
        {

            BaixaDAO baixaDAO = new BaixaDAO();
            var listaBaixa = baixaDAO.GetAll();
            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();
            //todos
            if (filtro.SelectItemProdutoId != null)
            {
                var resultQuery = from b in listaBaixa
                                  join p in listaProduto
                                  on b.Produto_CodigoProduto equals p.CodigoProduto
                                  where b.Produto_CodigoProduto == filtro.SelectItemProdutoId
                                  select new RelatorioBaixaViewModel
                                  {
                                      NomeProduto = p.NomeProduto,
                                      QuantidadeBaixa = b.QuantidadeBaixa,
                                      DataBaixa = b.DataBaixa,
                                  };

                return View(resultQuery.Distinct());

            }
            else
            {
                var resultQuery = from b in listaBaixa
                                  join p in listaProduto
                                  on b.Produto_CodigoProduto equals p.CodigoProduto
                                  select new RelatorioBaixaViewModel
                                  {
                                      NomeProduto = p.NomeProduto,
                                      QuantidadeBaixa = b.QuantidadeBaixa,
                                      DataBaixa = b.DataBaixa,
                                  };

                return View(resultQuery.Distinct());

            }
        }

    }

}
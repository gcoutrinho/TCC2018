using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.ViewModels;
using TccUsjt2018.ViewModels.Relatório;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            #region VARIAVEIS DE ENTRADA
            string nomeCategoria = null;
            Nullable<DateTime> dataValidade = null/*new DateTime(2018, 06, 01)*/;
            #endregion

            //METODOS QUE REQUISITAM DADOS NO BANCO E FILTRAM

            #region COLETORES DE DADOS DAO
            ProdutoDAO produtoDAO = new ProdutoDAO();
            var listaProduto = produtoDAO.GetAll();

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
            #endregion

            //Retorna Grafico de Colunas
            //Esse grafico retorna a quantidade de produtos totais.
            //Com ele podemos criar um ranking de produto para verificar o que mais é vendido, tornando mais dificil de vencer no estoque
            //Pois tem alta rotatividade

            #region DADOS GRAFICO DE RANKING DE PRODUTOS (BARRA)

            //var resultQuery = from l in listaLote
            //                  join p in listaProduto
            //                  on l.Produto_CodigoProduto equals p.CodigoProduto
            //                  group l by new { p.NomeProduto } into g
            //                  select new
            //                  {
            //                      NomeProduto = g.Key.NomeProduto,
            //                      QuantidadeProduto = g.Sum(x => x.QuantidadeProduto)
            //                  };

            //foreach (var item in resultQuery)
            //{
            //    Console.WriteLine("Produto: " + item.NomeProduto + " Quantidade: " + item.QuantidadeProduto);
            //}
            //Console.ReadLine();

            #endregion

            //Esse relatorio pode ser filtrado pelo nome da categoria do lote e pela data de validade
            //O Mesmo vai retornar o resultado de todos os lotes daquela categoria e com aquela data
            //Esse relatorio é importate para sabermos o nome o tipo de produto que tem dentro de cada lote, quantidade e se esta proximo do vencimento

            #region RELATORIO LOTE COM FILTRO DE DATA E NOMECATEGORIA
            //if (nomeCategoria != null && dataValidade != null)
            //{
            //    var resultQuery = from l in listaLote
            //                      join p in listaProduto
            //                      on l.Produto_CodigoProduto equals p.CodigoProduto
            //                      join c in filtroCategoria
            //                      on p.Categoria_CodigoCategoria equals c.CodigoCategoria
            //                      orderby l.ValidadeLote
            //                      select new
            //                      {
            //                          l.DescricaoLote,
            //                          p.NomeProduto,
            //                          c.NomeCategoria,
            //                          l.ValidadeLote,
            //                          l.QuantidadeProduto,
            //                      };
            //    foreach (var item in resultQuery)
            //    {
            //        Console.WriteLine("Lote: " + item.DescricaoLote + " Produto: " + item.NomeProduto + " Categoria: " + item.NomeCategoria + " Validade: " + item.ValidadeLote + " Quantidade: " + item.QuantidadeProduto);
            //    }
            //    Console.ReadLine();

            //}
            //else
            //{
            //    var resultQuery = from l in listaLote
            //                      join p in listaProduto
            //                      on l.Produto_CodigoProduto equals p.CodigoProduto
            //                      join c in listaCategoria
            //                      on p.Categoria_CodigoCategoria equals c.CodigoCategoria
            //                      orderby l.ValidadeLote
            //                      select new
            //                      {
            //                          l.DescricaoLote,
            //                          p.NomeProduto,
            //                          c.NomeCategoria,
            //                          l.ValidadeLote,
            //                          l.QuantidadeProduto,
            //                      };
            //    foreach (var item in resultQuery)
            //    {
            //        Console.WriteLine("Lote: " + item.DescricaoLote + " Produto: " + item.NomeProduto + " Categoria: " + item.NomeCategoria + " Validade: " + item.ValidadeLote + " Quantidade: " + item.QuantidadeProduto);
            //    }
            //    Console.ReadLine();
            //}
            #endregion


            //RELATORIO DE PRODUTO
            // Filtros: CATEGORIA/DATA ou DATA ou TODOS OS PRODUTOS
            #region RELATORIO DE PRODUTO
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
                                      p.MarcaProduto,
                                      l.ValidadeLote,
                                  };

                foreach (var item in resultQuery)
                {
                    Console.WriteLine("Produto: " + item.NomeProduto + " Categoria: " + item.NomeCategoria + " Marca: " + item.MarcaProduto + " Validade: " + item.ValidadeLote);
                }
                Console.ReadLine();

            }
            else if (nomeCategoria == null && dataValidade != null)
            {
                var resultQuery = from p in listaProduto
                                  join l in filtroLote
                                  on p.CodigoProduto equals l.Produto_CodigoProduto
                                  join c in listaCategoria
                                  on p.Categoria_CodigoCategoria equals c.CodigoCategoria
                                  select new
                                  {
                                      p.NomeProduto,
                                      c.NomeCategoria,
                                      p.MarcaProduto,
                                      l.ValidadeLote,
                                  };

                foreach (var item in resultQuery)
                {
                    Console.WriteLine("Produto: " + item.NomeProduto + " Categoria: " + item.NomeCategoria + " Marca: " + item.MarcaProduto + " Validade: " + item.ValidadeLote);
                }
                Console.ReadLine();

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

                foreach (var item in resultQuery)
                {
                    Console.WriteLine("Produto: " + item.NomeProduto + " Categoria: " + item.NomeCategoria + " Marca: " + item.MarcaProduto + " Validade: " + item.ValidadeLote );
                }
                Console.ReadLine();

            }
            #endregion



        }
    }
}

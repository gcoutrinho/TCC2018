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
            string nomeProduto = "Carne";
            string nomeCategoria = "TipoB";
            DateTime dataValidade = new DateTime(2018, 06, 01);

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
            var filtroProduto = new List<Produto>();
            foreach (var item in listaProduto)
            {
                if (item.NomeProduto.Equals(nomeProduto))
                {
                    filtroProduto.Add(item);
                }
            }
          
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

            if (nomeProduto != null && nomeCategoria != null && dataValidade != null)
            {
                var resultQuery = from p in filtroProduto
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

                foreach (var item in resultQuery)
                {
                    Console.WriteLine("Nome Produto " + item.NomeProduto + " --- " + " Nome Categoria " + item.NomeCategoria + " --- " + "Data Validade " + item.ValidadeLote);
                }
                Console.ReadLine();
            }
        }
    }
}

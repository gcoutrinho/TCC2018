using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.DAO;
using TccUsjt2018.Database.Entities;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            ProdutoDAO daoProduto = new ProdutoDAO();
            CategoriaDAO daoCategoria = new CategoriaDAO();
            LoteDAO loteDao = new LoteDAO();

            //CategoriaProduto c = new CategoriaProduto()
            //{
            //    DescricaoCategoria = "Laticinio",
            //    NomeCategoria = "Categori destinada a produtos ",
            //};

            //daoCategoria.Salva(c);

            var idcategoria = daoCategoria.GetById(2);

            Produto p = new Produto()
            {
                NomeProduto = "Danone",
                DataCadastro = DateTime.Now,
                Marca = "Frutap",
                ValorProduto = (decimal)1.59,
                CategoriaProduto = idcategoria,
            };
            daoProduto.Salva(p);

            Lote l = new Lote()
            {
                DataCadastro = DateTime.Now,
                DescricaoLote = "Lote de Leite",
                ProdutoId = p.CodigoProduto,
                QuantidadeProduto = 100,
                ValidadeLote = DateTime.Now.AddYears(2018).AddMonths(12).AddDays(30),
                Produto = p,            
            };

            loteDao.Salva(l);

            Console.WriteLine("Salvou o --  " + p.NomeProduto + "--  na categoria -- " + idcategoria.NomeCategoria);
            Console.ReadLine();



        }
    }
}

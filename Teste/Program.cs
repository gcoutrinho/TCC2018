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

            List<Produto> lista = daoProduto.GetAll();

            foreach (var item in lista)
            {
                Console.WriteLine("Nome Produto" + item.NomeProduto);
            }
            Console.ReadLine();
            



        }
    }
}

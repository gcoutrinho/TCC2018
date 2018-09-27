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
           


            foreach (var item in resultQuery)
            {
                Console.WriteLine("Produtio: " + item.NomeProduto + " Qtd " + item.QuantidadeProduto);
            }
            Console.ReadLine();

        }
    }
}

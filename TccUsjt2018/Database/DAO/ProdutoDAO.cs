using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.Database.EntitiesContext;

namespace TccUsjt2018.Database.DAO
{
    public class ProdutoDAO
    {
        private EntidadesContext contexto;

        public ProdutoDAO()
        {
            contexto = new EntidadesContext();
        }

        public void Salva(Produto produto)
        {
            //var categoriaProduto = produto.CategoriaProduto.CodigoCategoria;
            //produto.CategoriaId = categoriaProduto;
            contexto.Produtos.Add(produto);
            contexto.SaveChanges();
        }

        public Produto GetById(int id)
        {
            return contexto.Produtos.FirstOrDefault(x => x.CodigoProduto == id);
        }

        public void Remove(Produto produto)
        {
            contexto.Remove(produto);
            contexto.SaveChanges();
        }

        public List<Produto> GetAll()
        {
            return contexto.Produtos.ToList();
        }
      
    }
}
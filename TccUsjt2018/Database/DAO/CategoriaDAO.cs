using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.Database.EntitiesContext;

namespace TccUsjt2018.Database.DAO
{
    public class CategoriaDAO
    {
        private EntidadesContext contexto;

        public CategoriaDAO()
        {
            contexto = new EntidadesContext();
        }

        public void Salva(CategoriaProduto categoriaProduto)
        {
            contexto.Add(categoriaProduto);
            contexto.SaveChanges();
        } 

        public CategoriaProduto GetById(int id)
        {
            return contexto.Categorias.FirstOrDefault(x => x.CodigoCategoria == id);
        }

        public void Remove(CategoriaProduto categoriaProduto)
        {
            contexto.Remove(categoriaProduto);
            contexto.SaveChanges();
        }

        public List<CategoriaProduto> GetAll()
        {
            return contexto.Categorias.ToList();
        }
    }
}
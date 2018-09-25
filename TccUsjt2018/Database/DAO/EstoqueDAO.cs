using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.Database.EntitiesContext;

namespace TccUsjt2018.Database.DAO
{
    public class EstoqueDAO
    {
        private EntidadesContext contexto;

        public EstoqueDAO()
        {
            contexto = new EntidadesContext();
        }

        public void Salva(Estoque estoque)
        {
            contexto.Estoques.Add(estoque);
            contexto.SaveChanges();
        }

        public Estoque GetById(int id)
        {
            return contexto.Estoques.FirstOrDefault(x => x.CodigoEstoque == id);
        }

        public void Remove(Estoque estoque)
        {
            contexto.Estoques.Remove(estoque);
            contexto.SaveChanges();
        }

        public List<Estoque> GetAll()
        {
            return contexto.Estoques.ToList();
        }





    }
}
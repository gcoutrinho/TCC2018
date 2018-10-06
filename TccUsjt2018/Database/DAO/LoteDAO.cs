using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.Database.EntitiesContext;

namespace TccUsjt2018.Database.DAO
{
    public class LoteDAO
    {
        private EntidadesContext contexto;

        public LoteDAO()
        {
            contexto = new EntidadesContext();
        }

        public void Salva(Lote lote)
        {
            contexto.Lotes.Add(lote);
            contexto.SaveChanges();
        }

        public Lote GetById(int id)
        {
            return contexto.Lotes.FirstOrDefault(x => x.CodigoLote == id);
        }

        public List<Lote> GetAll()
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();
            var retorno =  contexto.Lotes.ToList();

            foreach (var produto in retorno)
            {
                produto.Produto = produtoDAO.GetById(produto.Produto_CodigoProduto);
            }

            return retorno;
        }

        public void Update(Lote lote)
        {
            contexto.Lotes.Update(lote);
            contexto.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;
using TccUsjt2018.Database.EntitiesContext;

namespace TccUsjt2018.Database.DAO
{
    public class BaixaDAO
    {
        private EntidadesContext contexto;

        public BaixaDAO()
        {
            contexto = new EntidadesContext();
        }

        public void Salva(Baixa baixa)
        {
            contexto.Add(baixa);
            contexto.SaveChanges();
        }

        public List<Baixa> GetAll()
        {
            return contexto.Baixas.ToList();
        }
    }
}
}
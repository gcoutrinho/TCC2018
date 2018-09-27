using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.ViewModels.Filtros
{
    public class FiltrosViewModel
    {
        public string NomeCategoria { get; set; }
        public DateTime DataVencimento { get; set; }
        public IEnumerable<CategoriaProduto> Categorias { get; set; }
    }
}
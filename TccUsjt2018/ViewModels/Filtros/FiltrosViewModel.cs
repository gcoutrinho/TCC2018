using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.ViewModels.Filtros
{
    public class FiltrosViewModel
    {
        public string NomeProduto { get; set; }
        public string NomeCategoria { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TccUsjt2018.ViewModels.Estoques
{
    public class EstoqueViewModel
    {
        [Required]
        public virtual int CodigoEstoque { get; set; }
        public virtual string Descricao { get; set; }
    }
}
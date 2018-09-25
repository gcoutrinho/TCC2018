using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TccUsjt2018.ViewModels.ProdutoCategoria
{
    public class CategoriaProdutoViewModel
    {
        [Required]
        public string NomeCategoria { get; set; }
        [Required]
        public string DescricaoCategoria { get; set; }
    }
}
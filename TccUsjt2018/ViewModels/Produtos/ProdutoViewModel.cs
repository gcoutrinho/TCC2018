using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.ViewModels
{
    public class ProdutoViewModel
    {
        public int CodigoProduto { get; set; }
        [Required]
        public string NomeProduto { get; set; }
        public DateTime DataCadastro { get; set; }
        [Required]
        public virtual CategoriaProduto CategoriaProduto { get; set; }     
        public int Categoria_CodigoCategoria { get; set; }
        public string MarcaProduto { get; set; }
    }
}
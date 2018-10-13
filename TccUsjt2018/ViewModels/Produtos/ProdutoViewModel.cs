using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.ViewModels
{
    public class ProdutoViewModel
    {
        public int CodigoProduto { get; set; }
        [Required]
        [Display(Name = "Nome")]
        public string NomeProduto { get; set; }
        [Display(Name = "Data Cadastro")]
        public DateTime DataCadastro { get; set; }
        [Display(Name = "Categoria")]
        public virtual CategoriaProduto CategoriaProduto { get; set; }
        [Display(Name = "Marca")]
        public string MarcaProduto { get; set; }
        [Display(Name = "Categoria")]
        public int? SelectItemCategoriaId { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.ViewModels.Filtros
{
    public class FiltrosViewModel
    { 
        [Display(Name = "Nome Produto")]
        public string NomeProduto { get; set; }
        [Display(Name = "Data Vencimento")]
        public DateTime? DataVencimento { get; set; }
        [Display(Name = "Categoria")]
        public int? SelectItemCategoriaId { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }
        [Display(Name = "Produto")]
        public int? SelectItemProdutoId { get; set; }
        public IEnumerable<SelectListItem> Produtos { get; set; }
        [Display(Name = "Lote")]
        public int? SelectItemLoteId { get; set; }
        public IEnumerable<SelectListItem> Lotes { get; set; }

    }
}
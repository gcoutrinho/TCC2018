using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.ViewModels.Baixa
{
    public class BaixaViewModel
    {
        [Display(Name = "Codigo Baixa")]
        public int CodigoBaixa { get; set; }
        [Required]
        [Display(Name = "Data Baixa")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataBaixa { get; set; }
        [Required]
        [Display(Name = "QuantidadeBaixa")]
        public int QuantidadeBaixa { get; set; }
        [Display(Name = "Produto")]
        public int? SelectItemProdutoId { get; set; }
        public IEnumerable<SelectListItem> Produtos { get; set; }
    }
}




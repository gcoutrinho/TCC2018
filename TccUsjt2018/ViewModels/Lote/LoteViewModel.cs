using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.ViewModels.Lote
{
    public class LoteViewModel
    {        
        public int CodigoLote { get; set; }
        [Required]
        [Display(Name = "Nome")]
        public string DescricaoLote { get; set; }
        [Display(Name = "Data Validade")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ValidadeLote { get; set; }
        [Required]
        [Display(Name = "Quantidade")]
        public int QuantidadeProduto { get; set; }
        public virtual Produto Produto { get; set; }
        public int Produto_CodigoProduto { get; set; }
        public virtual Estoque Estoque { get; set; }     
        public int Estoque_CodigoEstoque { get; set; }
        [Required]
        [Display(Name = "Produto")]     
        public int? SelectItemProdutoId { get; set; }
        public IEnumerable<SelectListItem> Produtos { get; set; }
        [Required]
        [Display(Name = "Local Estoque")]
        public int? SelectItemEstoqueId { get; set; }
        public IEnumerable<SelectListItem> Estoques { get; set; }
        [Display(Name = "Quantidade a Baixar")]
        public int QuantidadeBaixa { get; set; }
        public HashSet<string> ListaAlerta { get; set; }
    }
}
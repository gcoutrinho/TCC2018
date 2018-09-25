using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.ViewModels.Lote
{
    public class LoteViewModel
    {        
        public int CodigoLote { get; set; }
        [Required]
        public string DescricaoLote { get; set; }
        public DateTime ValidadeLote { get; set; }
        [Required]
        public int QuantidadeProduto { get; set; }
        [Required]
        public virtual Produto Produto { get; set; }
        public int Produto_CodigoProduto { get; set; }
        [Required]
        public virtual Estoque Estoque { get; set; }     
        public int Estoque_CodigoEstoque { get; set; }
    }
}
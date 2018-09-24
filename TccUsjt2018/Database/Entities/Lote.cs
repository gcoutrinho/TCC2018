using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TccUsjt2018.Database.Entities
{
    public class Lote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoLote { get; set; }
        public string DescricaoLote { get; set; }
        public DateTime ValidadeLote { get; set; }
        public int QuantidadeProduto { get; set; }    
        public virtual Produto Produto { get; set; }
        [ForeignKey("Produto")]
        public int Produto_CodigoProduto { get; set; }
        public virtual Estoque Estoque { get; set; }
        [ForeignKey("Estoque")]
        public int Estoque_CodigoEstoque { get; set; }



    }
}
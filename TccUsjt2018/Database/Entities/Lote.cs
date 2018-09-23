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
        public DateTime DataCadastro { get; set; }
        public DateTime ValidadeLote { get; set; }
        public int QuantidadeProduto { get; set; } 
        public virtual Produto Produto { get; set; }
        [ForeignKey("ProdutoId")]
        public int ProdutoId { get; set; }
        
    }
}
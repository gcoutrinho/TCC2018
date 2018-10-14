using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TccUsjt2018.Database.Entities
{
    public class Baixa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoBaixa { get; set; }
        public int QuantidadeBaixa { get; set; }
        public DateTime DataBaixa { get; set; }
        public virtual Lote Lote { get; set; }
        [ForeignKey("Lote")]
        public int Lote_CodigoLote { get; set; }
        public virtual Produto Produto { get; set; }
        [ForeignKey("Produto")]
        public int Produto_CodigoProduto { get; set; }

    }
}
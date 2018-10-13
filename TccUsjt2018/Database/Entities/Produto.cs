using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TccUsjt2018.Database.Entities
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public DateTime DataCadastro { get; set; }
        public virtual CategoriaProduto CategoriaProduto { get; set; }
        [ForeignKey("CategoriaProduto")]
        public int? Categoria_CodigoCategoria { get; set; }
        public string MarcaProduto { get; set; }
    }
}
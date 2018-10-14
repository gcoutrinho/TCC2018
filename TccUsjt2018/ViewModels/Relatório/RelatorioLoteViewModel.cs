using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TccUsjt2018.ViewModels.Relatório
{
    public class RelatorioLoteViewModel
    { 
        [Display(Name = "Nome Lote")]
        public virtual string DescricaoLote { get; set; }
        [Display(Name = "Produto")] 
        public string NomeProduto { get; set; }
        [Display(Name = "Data Vecimento")]
        public DateTime ValidadeLote { get; set; }
        [Display(Name = "Categoria")]
        public string NomeCategoria { get; set; }
        [Display(Name = "Quantidade")]
        public virtual int QuantidadeProduto { get; set; }   

    }
}
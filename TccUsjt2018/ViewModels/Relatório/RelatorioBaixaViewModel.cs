using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TccUsjt2018.ViewModels.Relatório
{
    public class RelatorioBaixaViewModel
    {
        [Display(Name = "Nome Produto")]
        public virtual string NomeProduto { get; set; }
        [Display(Name = "Quantidade Baixa")]
        public int QuantidadeBaixa{ get; set; }
        [Display(Name = "Data Baixa")]
        public virtual DateTime DataBaixa { get; set; }
    }
}
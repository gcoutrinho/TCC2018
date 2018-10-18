using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TccUsjt2018.ViewModels.Relatório
{
    public class RelatorioProdutoViewModel
    {
        public int CodigoProduto { get; set; }
        [Display(Name = "Nome Produto")]
        public virtual string NomeProduto { get; set; }
        [Display(Name = "Nome Categoria")]
        public virtual string NomeCategoria { get; set; }
        [Display(Name = "Data de Vencimento")]
        public virtual  DateTime DataVencimento { get; set; }
        [Display(Name = "Marca")]
        public virtual string Marca { get; set; }
    }
}
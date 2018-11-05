using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TccUsjt2018.ViewModels
{
    public class VerificaSituacaoEstoqueViewModel
    {
        public string NomeProduto { get; set; }
        public int MediaBaixa { get; set;}
        public int EstoqueAtual { get; set; }
    }
}
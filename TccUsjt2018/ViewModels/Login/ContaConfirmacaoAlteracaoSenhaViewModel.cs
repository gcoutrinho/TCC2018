using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TccUsjt2018.ViewModels.Login
{
    public class ContaConfirmacaoAlteracaoSenhaViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UsuarioId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string NovaSenha { get; set; }
    }
}
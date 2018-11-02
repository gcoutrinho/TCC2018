using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TccUsjt2018.ViewModels.Login
{
    public class ContaEsqueciSenhaViewModel
    {
        [Display(Name = "E-Mail")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
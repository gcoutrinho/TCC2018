using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TccUsjt2018.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required]
        public string Email;
        [Required]
        public string Senha;

    }
}
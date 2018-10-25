using Microsoft.AspNet.Identity.EntityFramework;

namespace TccUsjt2018.Models
{
    public class UsuarioAplicacao : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}
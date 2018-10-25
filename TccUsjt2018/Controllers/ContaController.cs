using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TccUsjt2018.Models;
using System.Data.Entity;

namespace TccUsjt2018
{
    public class ContaController : Controller
    {

        private UserManager<UsuarioAplicacao> _userManager;
        public UserManager<UsuarioAplicacao> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    var contextOwin = HttpContext.GetOwinContext();
                    _userManager = contextOwin.GetUserManager<UserManager<UsuarioAplicacao>>();
                }
                return _userManager;
            }
            set
            {
                _userManager = value;
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(ContaRegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var novoUsuario = new UsuarioAplicacao();

                novoUsuario.Email = modelo.Email;
                novoUsuario.UserName = modelo.UserName;
                novoUsuario.NomeCompleto = modelo.NomeCompleto;

                var usuario = UserManager.FindByEmail(modelo.Email);
                var usuarioJaExiste = usuario != null;

                if (usuarioJaExiste)
                    return RedirectToAction("Index", "Home");

                var resultado = await UserManager.CreateAsync(novoUsuario, modelo.Senha);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AdicionaErros(resultado);
                }
            }

            // Alguma coisa de errado aconteceu!
            return View(modelo);
        }

        public ActionResult Index()
        {
            var usuarios = UserManager.Users.ToList();
            var model = usuarios.Select(x => new ContaRegistrarViewModel
            {
                UserName = x.UserName,
                NomeCompleto = x.NomeCompleto,
                Email = x.Email,

            });
            return View(model);
        }

        private void AdicionaErros(IdentityResult resultado)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError("", erro);
        }

        public async Task<List<UsuarioAplicacao>> RetornaUsuarios()
        {
            List<UsuarioAplicacao> usuarios = await UserManager.Users.ToListAsync();
            return usuarios;
        }
    }
}
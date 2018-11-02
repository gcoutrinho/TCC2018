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
using TccUsjt2018.ViewModels.Login;
using Microsoft.Owin.Security;

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

        private SignInManager<UsuarioAplicacao, string> _signInManager;
        public SignInManager<UsuarioAplicacao, string> SignInManager
        {
            get
            {
                if (_signInManager == null)
                {
                    var contextOwin = HttpContext.GetOwinContext();
                    _signInManager = contextOwin.GetUserManager<SignInManager<UsuarioAplicacao, string>>();
                }
                return _signInManager;
            }
            set
            {
                _signInManager = value;
            }
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                var contextoOwin = Request.GetOwinContext();
                return contextoOwin.Authentication;
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EsqueciSenha(ContaEsqueciSenhaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = await UserManager.FindByEmailAsync(modelo.Email);

                if (usuario != null)
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(usuario.Id);

                    var linkDeCallback =
                       Url.Action(
                           "ConfirmacaoAlteracaoSenha",
                           "Conta",
                           new { usuarioId = usuario.Id, token = token },
                           Request.Url.Scheme);

                    await UserManager.SendEmailAsync(usuario.Id,
                        "Lgg Rastreablidade - Alteração de Senha",
                        $"Bem vindo a LGG Rastreabilidade , clique aqui {linkDeCallback} para alterar a sua senha!");
                }

                return View("EmailAlteracaoSenhaEnviado");
            }

            return View();
        }

        public ActionResult ConfirmacaoAlteracaoSenha(string usuarioId, string token)
        {
            var modelo = new ContaConfirmacaoAlteracaoSenhaViewModel()
            {
                UsuarioId = usuarioId,
                Token = token
            };

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmacaoAlteracaoSenha(ContaConfirmacaoAlteracaoSenhaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var resultadoAlteracao =
                    await UserManager.ResetPasswordAsync(
                        modelo.UsuarioId,
                        modelo.Token,
                        modelo.NovaSenha);

                if (resultadoAlteracao.Succeeded)
                {
                    return RedirectToAction("Login", "Conta");
                }
                AdicionaErros(resultadoAlteracao);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Conta");
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(ContaRegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var novoUsuario = new UsuarioAplicacao
                {
                    Email = modelo.Email,
                    UserName = modelo.UserName,
                    NomeCompleto = modelo.NomeCompleto
                };

                var usuario = await UserManager.FindByEmailAsync(modelo.Email);
                var usuarioJaExiste = usuario != null;

                if (usuarioJaExiste)
                    return View("AguardandoConfirmacao");

                var resultado = await UserManager.CreateAsync(novoUsuario, modelo.Senha);

                if (resultado.Succeeded)
                {
                    //Enviar email d confirmação
                    await EnviarEmailDeConfirmacaoAsync(novoUsuario);
                    return View("AguardandoConfirmacao");
                }
                else
                {
                    AdicionaErros(resultado);
                }
            }

            // Alguma coisa de errado aconteceu!
            return View(modelo);
        }

        public async Task<ActionResult> ConfirmacaoEmail(string usuarioId, string token)
        {
            if (usuarioId == null || token == null)
            {
                return View("Error");
            }

            var resultado = await UserManager.ConfirmEmailAsync(usuarioId, token);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }



            throw new NotImplementedException();
        }
        private async Task EnviarEmailDeConfirmacaoAsync(UsuarioAplicacao usuario)
        {
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(usuario.Id);

            var linkDeCallback =
                Url.Action(
                    "ConfirmacaoEmail",
                    "Conta",
                    new { usuarioId = usuario.Id, token = token },
                    Request.Url.Scheme);

            await UserManager.SendEmailAsync(usuario.Id,
                "Lgg Rastreablidade - Confirmação de Email",
                $"Bem vindo a LGG Rastreabilidade , clique aqui {linkDeCallback} para confirmar o seu e-mail");
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

        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(ContaLoginViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = await UserManager.FindByEmailAsync(modelo.Email);

                if (usuario == null)
                    return SenhaDoUsuarioInvalido();


                var signInResultado =
                    await SignInManager.PasswordSignInAsync(
                        usuario.UserName,
                        modelo.Senha,
                        isPersistent: modelo.ContinuarLogado,
                        shouldLockout: true);

                switch (signInResultado)
                {
                    case SignInStatus.Success:
                        if (!usuario.EmailConfirmed)
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            return View("AguardandoCOnfirmacao");
                        }
                        return RedirectToAction("Index", "Home");
                    case SignInStatus.LockedOut:
                        var senhaCorreta =
                            await UserManager.CheckPasswordAsync(
                                usuario,
                                modelo.Senha);

                        if (senhaCorreta)
                            ModelState.AddModelError("", "A conta esta temporariamente bloqueada!");
                        else
                            return SenhaDoUsuarioInvalido();
                        break;
                    default:
                        return SenhaDoUsuarioInvalido();

                }
            }

            return View(modelo);
        }

        private ActionResult SenhaDoUsuarioInvalido()
        {
            ModelState.AddModelError("", "Credenciais Invalidas!");
            return View("Login");
        }
    }
}
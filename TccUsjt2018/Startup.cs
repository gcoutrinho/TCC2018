using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TccUsjt2018.App_Start.Identity;
using TccUsjt2018.Models;

[assembly: OwinStartup(typeof(TccUsjt2018.Startup))]

namespace TccUsjt2018
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.CreatePerOwinContext<DbContext>(() =>
                new IdentityDbContext<UsuarioAplicacao>("banco23092018"));

            builder.CreatePerOwinContext<IUserStore<UsuarioAplicacao>>(
                (opcoes, contextoOwin) =>
                {
                    var dbContext = contextoOwin.Get<DbContext>();
                    return new UserStore<UsuarioAplicacao>(dbContext);
                });

            builder.CreatePerOwinContext<UserManager<UsuarioAplicacao>>(
                (opcoes, contextoOwin) =>
                {
                    var userStore = contextoOwin.Get<IUserStore<UsuarioAplicacao>>();
                    var userManager = new UserManager<UsuarioAplicacao>(userStore);

                    var userValidator = new UserValidator<UsuarioAplicacao>(userManager)
                    {
                        RequireUniqueEmail = true
                    };

                    userManager.UserValidator = userValidator;
                    userManager.PasswordValidator = new SenhaValidador()
                    {
                        TamanhoRequerido = 6,
                        ObrigatorioCaracteresEspeciais = true,
                        ObrigatorioDigitos = true,
                        ObrigatorioLowerCase = true,
                        ObrigatorioUpperCase = true
                    };

                    userManager.EmailService = new EmailServico();

                    var dataProtectionPriveder = opcoes.DataProtectionProvider;
                    var dataProtectionPrivederCreate = dataProtectionPriveder.Create("TccUsjt2018");

                    userManager.UserTokenProvider = new DataProtectorTokenProvider<UsuarioAplicacao>(dataProtectionPrivederCreate);

                    //Define que o maximo de tentativa de login por usuario é de 3, apos isso, bloqueia por 1 minuto
                    userManager.MaxFailedAccessAttemptsBeforeLockout = 3;
                    //Define que o tempo de bloqueio é de 1 minuto
                    userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(1);
                    //Define que a condição de bloqueio é valida para todas as classes de usuário
                    userManager.UserLockoutEnabledByDefault = true;
                    return userManager;
                });

            builder.CreatePerOwinContext<SignInManager<UsuarioAplicacao, string>>(
               (opcoes, contextoOwin) =>
               {
                   var userManager = contextoOwin.Get<UserManager<UsuarioAplicacao>>();
                   var signManager =
                        new SignInManager<UsuarioAplicacao, string>(
                            userManager,
                            contextoOwin.Authentication);
                   return signManager;
               });
            builder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            using (var dbContext = new IdentityDbContext<UsuarioAplicacao>("banco23092018"))
            {
                CriarRoles(dbContext);
                CriarAdministrador(dbContext);
            }

        }

        private void CriarRoles(IdentityDbContext<UsuarioAplicacao> dbContext)
        {
            using (var roleStore = new RoleStore<IdentityRole>(dbContext))
            using (var roleManager = new RoleManager<IdentityRole>(roleStore))
            {
                if (!roleManager.RoleExists(RolesNomes.GERENTE))                
                    roleManager.Create(new IdentityRole(RolesNomes.GERENTE));

                if(!roleManager.RoleExists(RolesNomes.ESTOQUISTA))               
                    roleManager.Create(new IdentityRole(RolesNomes.ESTOQUISTA));
            }
        }

        private void CriarAdministrador(IdentityDbContext<UsuarioAplicacao> dbContext)
        {
            using (var userStore = new UserStore<UsuarioAplicacao>(dbContext))
            using (var userManager = new UserManager<UsuarioAplicacao>(userStore))
            {
                var administradorEmail = ConfigurationManager.AppSettings["admin:email"];
                var administrador = userManager.FindByEmail(administradorEmail);

                if (administrador != null)
                    return;

                administrador = new UsuarioAplicacao
                {
                    Email = administradorEmail,
                    EmailConfirmed = true,
                    UserName = ConfigurationManager.AppSettings["admin:user_name"]
                };

                userManager.Create(administrador,
                    ConfigurationManager.AppSettings["admin:senha"]);

                userManager.AddToRole(administrador.Id, RolesNomes.GERENTE);
             
            }




        }

    }


}






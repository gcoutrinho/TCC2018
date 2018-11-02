using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace TccUsjt2018.App_Start.Identity
{
    public class EmailServico : IIdentityMessageService
    {
        private readonly string EMAIL_ORIGEM = ConfigurationManager.AppSettings["emailServico:email_remetente"];
        private readonly string EMAIL_SENHA = ConfigurationManager.AppSettings["emailServico:email_senha"];
        public async Task SendAsync(IdentityMessage message)
        {
            using (var mensagemDeEmail = new MailMessage())
            {
                //Seta o endereço de origem do email
                mensagemDeEmail.From = new MailAddress(EMAIL_ORIGEM);

                //Propriedade que armazena o assunto
                mensagemDeEmail.Subject = message.Subject;
                //Coleção de destinatarios = Enviar email pra muitasa pessoas
                mensagemDeEmail.To.Add(message.Destination);
                //Mensagem que vai no crpo do email
                mensagemDeEmail.Body = message.Body;

                //SMTP - Reponsavel por transportar o email
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(EMAIL_ORIGEM, EMAIL_SENHA);

                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;

                    smtpClient.Timeout = 20000;

                    await smtpClient.SendMailAsync(mensagemDeEmail);
                   
                }
            }                
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{

    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("nuevo30664@gmail.com", "kbxeavaldzanhwfi");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void ArmarCorreoFormContacto( string txt_nombreContacto, string txt_email, string txt_mensaje)

        {
            string micorreo = "cristianbernal83@hotmail.com";

            email = new MailMessage();
            email.From = new MailAddress(txt_email);
            email.To.Add (micorreo);
            email.Subject = txt_nombreContacto;
            email.Body = ("Enviado por ")+ txt_nombreContacto+("\nEmail: ")+ txt_email + ("\nMensaje:\n")+txt_mensaje;
            
        }

        public void ArmarCorreoAltaUser(string Mail,string Cuerpo)

        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@pokedex.com");
            email.To.Add (Mail);
            email.Subject = "Bienvenido Trainee";
            email.Body = Cuerpo;
            
        }

        public void EnviarMail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace CapaNegocios
{
    namespace Flujos
    {
        namespace Tareas
        {
            public class EnviarCorreo
            {
                public void EnviarHtml(Sesion session, string subject, string to, string body)
                {
                    //Se obtiene la configuración para acceder a la cuenta de correo
                    ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                    CN_Configuracion cn_configuracion = new CN_Configuracion();
                    try
                    {
                        configuracion.Id_Cd = session.Id_Cd_Ver;
                        configuracion.Id_Emp = session.Id_Emp;
                        cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener la configuración global.", ex);
                    }

                    try
                    {
                        SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                        sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                        MailMessage m = new MailMessage();
                        m.From = new MailAddress(configuracion.Mail_Remitente);
                        m.To.Add(new MailAddress(to));
                        m.Subject = subject;
                        m.IsBodyHtml = true;
                        AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                        m.AlternateViews.Add(vistaHtml);
                        sm.Send(m);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al enviar el mesanje de correo.", ex);
                    }
                }
            }
        }
    }
}

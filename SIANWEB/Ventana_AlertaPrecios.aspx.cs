using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Configuration;
using Telerik.Web.UI;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net.Mime;


namespace SIANWEB
{ 
    public partial class Ventana_AlertaPrecios : System.Web.UI.Page
    {
        #region Variables 

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    Inicializar();


                }
                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            try
            {

                EnviarCorreo();
             
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            try
            {
                CerrarVentana("CancelarAlertaPrecios");

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
               

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        #endregion

        #region Funciones
        private void Inicializar()
        {
            try
            {

                List<string> Productos = (List<string>) Session["ProdsConv" + Session.SessionID];
                List<string> Convenios = (List<string>) Session["ConvPrecios" + Session.SessionID];
                this.LblMensaje1.Text = "El precio a facturar de el/los producto(s) " +
                                       string.Join(", ", Productos) +
                                       " es diferente al precio de venta autorizado según el/los convenio(s) " +
                                       string.Join(", " ,Convenios) + 
                                       " si se factura a otro precio se afectará la utilidad del cliente. Favor de corregir.";

                //Session["ProdsConv" + Session.SessionID] = null;
                //Session["ConvPrecios" + Session.SessionID] = null;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void EnviarCorreo()
        {
            try
            {

                if (this.TxtMensaje.Text.Trim() == "")
                {
                    Alerta("Debe agregar comentarios");
                    return;
 
                }

                List<string> Productos = (List<string>)Session["ProdsConv" + Session.SessionID];
                List<string> Convenios = (List<string>)Session["ConvPrecios" + Session.SessionID];
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                string Id_Fac = (string)Session["Id_FacPrec" + Session.SessionID];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Excepción de aplicación de precios especiales en la factura #" +  Id_Fac +  " en el CDI " +  session.Cd_Nombre );
                cuerpo_correo.Append("</td></tr><tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se omitieron los precios especiales de el/los producto(s) " + string.Join(", ", Productos) +  " autorizados en/los convenio(s) " + string.Join(", ", Convenios) +".");
                cuerpo_correo.Append("</td></tr> <tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Comentarios: " + this.TxtMensaje.Text);
                cuerpo_correo.Append("</td></tr> <tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Usuario : " + session.U_Nombre);
                cuerpo_correo.Append("</td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                char[] splitchar = { ';' };
                string Correo = "juan.campos@key.com.mx";
                string[] Correos = Correo.Split(splitchar);

                foreach (string correo in Correos)
                {
                    m.To.Add(new MailAddress(correo));
                }

          
                m.Subject = "Excepción de aplicación de precios especiales - " + session.Cd_Nombre;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);

                }
                catch (Exception)
                {

                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }

                Session["ProdsConv" + Session.SessionID] = null;
                Session["ConvPrecios" + Session.SessionID] = null;
                Session["Id_FacPrec" + Session.SessionID] = null;

                CerrarVentana("AceptarPrecios");
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CerrarVentana(string param)
        {
            try
            {
                string funcion = "CloseAndRebind('" + param + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void ErrorManager()
        {
            try
            {
                this.LblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(string Message)
        {
            try
            {
                this.LblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.LblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.LblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}
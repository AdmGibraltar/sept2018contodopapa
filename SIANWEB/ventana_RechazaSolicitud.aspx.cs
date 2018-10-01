using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using System.Text;
using System.Xml;
using CapaDatos;
using System.Globalization;
using System.Threading;
using System.Net.Mail;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class ventana_RechazaSolicitud : System.Web.UI.Page
    {

 #region Variables

        string valor_retorno = "";

    #endregion

 #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
          
            {
               
                ErrorManager();
                if (!Page.IsPostBack)
                {
                   ConsultarDepuracionFactura();
                }

            }
            catch (Exception ex)
            {

                ErrorManager(ex, "Page_Load");
            }
          
 
        }


        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "save":
                        Guardar();
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }

        }



        

    #endregion

 #region Funciones


        private void ConsultarDepuracionFactura()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Factura factura = new Factura();
                factura.Id_Cd = Convert.ToInt32(Request.Params["Id_Cd"]);
                factura.Id_Emp = Convert.ToInt32(Request.Params["Id_Emp"]);
                factura.Id_Fac = Convert.ToInt32(Request.Params["Id_Fac"]);

                //CN_CapFactura CNCapFactura = new CN_CapFactura();
                //CNCapFactura.Factura_DepuracionConsulta(ref factura, Sesion.Emp_Cnx);

                this.LblIdCdi.Text = Request.Params["Id_Cd"];
                this.LblId_Fac.Text = Request.Params["Id_Fac"];
                  
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        protected void BtnRechazar_Click(object sender, EventArgs e)
        {

            //try
            //{
                //Sesion Sesion = new Sesion();
                //Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //int verificador = 0;
                //Factura factura = new Factura();
                //factura.Id_Cd = Convert.ToInt32(this.LblIdCdi.Text);
                //factura.Id_Emp = Convert.ToInt32(Sesion.Id_Emp);
                //factura.Id_Fac = Convert.ToInt32(this.LblId_Fac.Text);
                //factura.Fac_DepuracionMotivo = this.txtMotivo.Text;




                //CN_CapFactura CNCapFactura = new CN_CapFactura();
                ////CNCapFactura.Factura_DepuracionActualiza (factura, Sesion.Emp_Cnx, ref verificador);

                //if (verificador == 1)
                //{
                //    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Los datos se guardaron correctamente", "')"));
                //}
                //else
                //{

                //    Alerta("Error al intentar guardar los datos");
                //}
                //valor_retorno = "-1";
                //Response.Write(valor_retorno);

 
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                int Verificador = 0;


                cn_es.CapEntSalSolicitud_ModificarEstatus(sesion.Id_Cd_Ver, int.Parse(this.LblId_Fac.Text), "R", ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    string[] url = Request.Url.ToString().Split(new char[] { '/' });
                    string URL = Request.Url.ToString().Replace(url[url.Length - 1], "");


                    EnviarCorreoAtendio(0 /*Rechazado*/, int.Parse(this.LblId_Fac.Text),this.txtMotivo.Text);
                    // cn_es.CapEntSalSolicitud_CorreoAtendio(sesion.Id_Cd_Ver, int.Parse(this.txtFolio.Text), URL, ref Verificador, sesion.Emp_Cnx);
                    //Alerta("Se rechazo la solicitud con Folio: <b> #" + this.LblId_Fac.Text + "</b> , se envio el correo de notificación");
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Se rechazo la solicitud con Folio: <b> #" + this.LblId_Fac.Text + "</b> , se envio el correo de notificación", "')"));
                    
                }
                else
                {
                    Alerta("Error al tratar de rechazar la solicitud");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
        }

         private void EnviarCorreoAtendio(int Tipo, int Id_ESol,string motivo)
         {
             try
             {
                 Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                 CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                 EntSalSolicitud es = new EntSalSolicitud();

                 cn_es.CapEntSolicitud_ConsultaDatosEnvio(sesion.Id_Cd_Ver, Id_ESol, ref es, sesion.Emp_Cnx);

                 string[] url = Request.Url.ToString().Split(new char[] { '/' });
                 string URL = Request.Url.ToString().Replace(url[url.Length - 1], "");


                 ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                 configuracion.Id_Cd = sesion.Id_Cd_Ver;
                 configuracion.Id_Emp = sesion.Id_Emp;
                 CN_Configuracion cn_configuracion = new CN_Configuracion();
                 cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);

                 StringBuilder cuerpo_correo = new StringBuilder();
                 cuerpo_correo.Append("<Table> <tr><td><b></b><td></tr> <tr><td><b>");
                 if (Tipo == 0)
                 {
                     cuerpo_correo.Append("La solicitud #" + Id_ESol + " ha sido RECHAZADA");

                 }
                 else
                 {
                     cuerpo_correo.Append("La solicitud #" + Id_ESol + " ha sido APROBADA");
                 }
                 cuerpo_correo.Append("</b><td></tr><tr><td>&nbsp;<td></tr><tr><td><b>" + motivo);

                 cuerpo_correo.Append("</b><td></tr><tr><td>&nbsp;<td></tr><tr><td>");
                 cuerpo_correo.Append("<a href=" + URL + "CapEntSalAutorizacion_Lista.aspx?u=" + es.ESol_Unique + ">Ver solicitud de autorización</a>");
                 cuerpo_correo.Append("</td></tr></Table>");

                 SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                 sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                 //sm.EnableSsl = true;
                 MailMessage m = new MailMessage();
                 m.From = new MailAddress(configuracion.Mail_Remitente);
                 m.To.Add(new MailAddress(es.ESol_CorreoDest));
                 //m.To.Add(new MailAddress("jmartinez@axsistec.com"));
                 if (es.ESol_CorreoCC.Length > 1)
                 {
                     m.CC.Add(new MailAddress(es.ESol_CorreoCC));
                 }
                 //m.Subject = "Solicitud de autorización de precios especiales";
                 m.Subject = "Solicitud de movimiento de almacén #" + Id_ESol + " RECHAZADA";
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
                 catch (Exception ex)
                 {
                     throw ex;

                 }
             }
             catch (Exception ex)
             {

                 throw ex;
             }
         }

        private void Guardar()
        {

            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = 0;
                Factura factura = new Factura();
                factura.Id_Cd = Convert.ToInt32(this.LblIdCdi.Text);
                factura.Id_Emp = Convert.ToInt32(Sesion.Id_Emp);
                factura.Id_Fac = Convert.ToInt32(this.LblId_Fac.Text);
                factura.Fac_DepuracionMotivo = this.txtMotivo.Text;
                



                CN_CapFactura CNCapFactura = new CN_CapFactura();
                //CNCapFactura.Factura_DepuracionActualiza (factura, Sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Los datos se guardaron correctamente", "')"));
                } 
                else
                {

                    Alerta("Error al intentar guardar los datos");
                }
                valor_retorno = "-1";
                Response.Write(valor_retorno);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

 #endregion

 #region ErrorManager
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
        private void AlertaFocus2(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus2('" + mensaje + "','" + rtb + "');");
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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
                this.lblMensaje.Text = "";
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
                this.lblMensaje.Text = Message;
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
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
  
    }
}
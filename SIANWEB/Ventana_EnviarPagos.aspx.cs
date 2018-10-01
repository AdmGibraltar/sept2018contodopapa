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
    public partial class Ventana_EnviarPagos : System.Web.UI.Page
    {
        #region Variables

        string URLtempPDF;
        string URLtempXML;

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.LblTipo.Text = Page.Request.QueryString["Tipo"].ToString();
                    this.LblDocumento.Text = Page.Request.QueryString["Id_Pag"].ToString();
                    this.HFId_Emp.Value = Page.Request.QueryString["Id_Emp"].ToString();
                    this.HFId_Cte.Value = Page.Request.QueryString["Id_Cte"].ToString();
                    this.HFId_Cd.Value = Page.Request.QueryString["Id_Cd"].ToString();
                    this.HFId_Pag.Value = Page.Request.QueryString["Id_Pag"].ToString();
                    this.HFId_Fac.Value = Page.Request.QueryString["Id_Fac"].ToString();
                    this.HFId_PagDet.Value = Page.Request.QueryString["Id_PagDet"].ToString();
                    this.HFSerie.Value = Page.Request.QueryString["Serie"].ToString();
                    ConsultaCorreo();

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {

            try
            {
                ObtenerPDFXML();
                EnviarCorreo();

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
        private void ConsultaCorreo()
        {
            try
            {
                Clientes cte = new Clientes();
                CN_CatCliente cn_cte = new CN_CatCliente();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Cd = int.Parse(this.HFId_Cd.Value);
                if (sesion.Id_Cd_Ver != Id_Cd)
                {
                    cn_cte.ConsultaClienteCorrreosOtraBD(sesion.Id_Emp, int.Parse(this.HFId_Cd.Value), int.Parse(this.HFId_Fac.Value), HFSerie.Value.ToString(), ref cte, sesion.Emp_Cnx);
                }
                else
                {
                    cn_cte.ConsultaClienteCorrreos(int.Parse(this.HFId_Cd.Value), int.Parse(this.HFId_Fac.Value), ref cte, sesion.Emp_Cnx);
                }
                this.LblId_CteStr.Text = cte.Cte_NomComercial;
                this.TxtCorreos.Text = cte.Cte_Email;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void ObtenerPDFXML()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                object pagoPDF = null;
                pagoDetComplemento.Id_Emp = Convert.ToInt32(HFId_Emp.Value);
                pagoDetComplemento.Id_Cd = Convert.ToInt32(HFId_Cd.Value);
                pagoDetComplemento.Id_Pag = Convert.ToInt32(HFId_Pag.Value);
                pagoDetComplemento.Id_Cte = Convert.ToInt32(HFId_Cte.Value);
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(HFId_PagDet.Value);
                new CN_CapPago().ConsultaComplementoPago(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

                if (pagoDetComplemento == null)
                {
                    Alerta("El pago no ha sido timbrado");
                }
                else
                {
                    if (pagoDetComplemento.Pago_Xml == null)
                        Alerta("Los archivos no fueron generados al momento de timbrar. Favor de timbrar de nuevo");
                    else
                    {

                        byte[] archivoPdf = (byte[])pagoPDF;
                        byte[] archivoPdfCN = pagoPDF != System.DBNull.Value ? (byte[])pagoPDF : new byte[0];
                        if (archivoPdf.Length > 0)
                        {
                            string tempPDFname = "PAGO";
                            tempPDFname = tempPDFname + pagoDetComplemento.Id_Emp.ToString() + "_" + pagoDetComplemento.Id_Cd.ToString() + "_" + pagoDetComplemento.Id_PagComp.ToString() + ".pdf";
                            URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                            this.ByteToTempPDF(URLtempPDF, archivoPdf);
                            //RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFVarias('", WebURLtempPDF, "')"));
                        }

                        System.IO.StreamWriter sws = null;
                        URLtempXML = Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".txt";

                        if (File.Exists(URLtempXML))
                            File.Delete(URLtempXML);
                        if (File.Exists(Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".xml"))
                            File.Delete(Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");
                        sws = new System.IO.StreamWriter(URLtempXML, false, Encoding.UTF8);
                        sws.WriteLine(pagoDetComplemento.Pago_Xml.ToString());
                        sws.Close();
                        File.Move(URLtempXML, Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");
                        URLtempXML = Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".xml";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    filebytes = Convert.FromBase64String(data);
                }
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }
        private void EnviarCorreo()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

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
                cuerpo_correo.Append("Estimado cliente se le envian PDF y XML del pago #" + this.LblDocumento.Text);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                char[] splitchar = { ';' };
                string[] Correos = this.TxtCorreos.Text.Split(splitchar);

                foreach (string correo in Correos)
                {
                    m.To.Add(new MailAddress(correo));
                }

                m.Attachments.Add(new Attachment(URLtempPDF));
                m.Attachments.Add(new Attachment(URLtempXML));
                m.Subject = "PDF y XML Pago  #" + this.LblDocumento.Text;
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

                AlertaCerrar("Se han enviado el correo de manera exitosa");

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

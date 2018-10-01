using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;
using CapaNegocios;
using CapaEntidad;
using Telerik.Web.UI;

using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace SIANWEB
{
    public partial class EnviaCorreoInstitucional : System.Web.UI.Page
    {


        #region Variables

        string strEmpresa = "";
        string Cita = "0";

        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            

            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    ValidarPermisos();
                    if (sesion.Cu_Modif_Pass_Voluntario == false)
                    {
                        RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        return;
                    }
                    Cita = Request.Params["cita"].ToString();
                    LlenaCorreo();
                    Random randObj = new Random(DateTime.Now.Millisecond);
                    HF_ClvPag.Value = randObj.Next().ToString();
                    this.HF_Usuario.Value = sesion.Id_U.ToString();
                }
            }
            
            
        }


        private void Page_Init(object sender, EventArgs e)
        {

           
        }



        #region ProcesosVarios

        protected void btnEditaMail_OnClick(object sender, EventArgs e)
        {
            // mandar ejecutar el SP spCatMotivoCambioVisita_AgregarALog
            try
            {
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelaMail_OnClick(object sender, EventArgs e)
        {
            try
            {
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnEnviaMail_OnClick(object sender, EventArgs e)
        {
            // 
            try
            {
                EnviarCorreo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Funciones

        protected void LlenaCorreo()
        {
            try
            {
                lblFrom.Text = "";
                lblTo.Text = "";
                lblSubject.Text = "Contacto Comercial Key.";
                lblEmpresa.Text="";
                lblContacoEmpresa.Text = "";
                
                string EmpresaZ = "";
                string ContaZ = "";
                string FromZ = "";
                string Tooz = "";
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];

                CN_Citas.ObtieneDatosEmpresaCita(Convert.ToInt32(Cita), ref EmpresaZ, ref ContaZ, ref FromZ, ref Tooz, gSession.Emp_Cnx);
                lblEmpresa.Text = EmpresaZ;
                lblContacoEmpresa.Text = ContaZ;
                lblFrom.Text = FromZ;
                lblTo.Text = Tooz;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void EnviarCorreo()
        {
            try
            {

                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }

                int verificador = -1;
                int grabo = 0;
                string Contacto = lblContacoEmpresa.Text;
                string NombreEmpresa = lblEmpresa.Text;

                //// Envia el correo
                verificador = -1;
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri; color:black'>");
                cuerpo_correo.Append("<table width='800px' style='font-family: Verdana; font-size: 8pt;'>");
                cuerpo_correo.Append("<tr><td><IMG SRC=\"cid:companylogo\" ALIGN='left'></td></tr>");
                cuerpo_correo.Append("<tr><td>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2'>");
                cuerpo_correo.Append("Hola <b>" + Contacto + "</b>:</td></tr><tr><td>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Espero que este email te encuentre bien! Te escribo ya que dentro de Key se esta implementando una nueva campaña de acercamiento con sus clientes.</td></tr>");
                cuerpo_correo.Append("<tr><td>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Key tiene una nueva plataforma (<i>SIANWeb4Costumers</i>) que podría ayudar a  <b>" + NombreEmpresa + "</b> para lograr mejores beneficios, entre los cuales podemos listar:</td></tr>");
                cuerpo_correo.Append("<tr><td><ul style='list-style-type:square'>");
                cuerpo_correo.Append("<li>Envio y Recepcion de Pedidos al momento.</li>");
                cuerpo_correo.Append("<li>Seguimiento y Notificaciones del estatus del pedido, factura, nota de credito, etc.</li>");
                cuerpo_correo.Append("<li>Impresion de Facturas desde la aplicacion para celulares y tablets</li>");
                cuerpo_correo.Append("<li>Timbrado de incidencias sobre pedidos incompletos, sustituidos, cancelados, etc.</li>");
                cuerpo_correo.Append("<li>Confirmacion de entregas y seguimiento en ruta de los despachos.</li>");
                cuerpo_correo.Append("</ul></td></tr>");
                cuerpo_correo.Append("<tr><td>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Podriamos explorar estos beneficios que <i>SIANWeb4Costumers</i> brindaria especificamente a tu negocio si tienes tiempo para una llamada o incluso una visita personal en los siguientes dias.</td></tr>");
                cuerpo_correo.Append("<tr><td>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Quedo al pendiente de cualquier duda, comentario para con gusto ampliar la informacion.</td></tr>");
                cuerpo_correo.Append("<tr><td>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Saludos,</td></tr>");
                //cuerpo_correo.Append("<table width='90%' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri;color:black' border=0 cellpadding=1>");
                //cuerpo_correo.Append("<tr><td>Código del producto:</td><td colspan='4'>" + "TRES" + "</td></tr>");
                //cuerpo_correo.Append("<tr><td>Descripción del producto:</td><td colspan='4'>" + "CUATRO" + "</td></tr>");
                //cuerpo_correo.Append("<tr><td>Tipo de producto:</td><td colspan='3'>" + "CINCO" + "</td><td>&nbsp;</td></tr>");
                //cuerpo_correo.Append("<tr><td>Familia del producto:</td><td colspan='4'>" + "SEIS" + "</td></tr>");
                //cuerpo_correo.Append("<tr><td>SubFamilia del producto:</td><td colspan='4'>" + "SIETE" + "</td></tr>");
                //cuerpo_correo.Append("<tr><td>Precios:</td><td colspan='4'>&nbsp;</td></tr><tr><td colspan='4' align=center>");
                //string tablaprecios = "";
                //string color = "";
                //int reng = 1;
                //string AAAO = "";
                //string PublicoO = "";

                //  string[] url = Request.Url.ToString().Split(new char[] { '/' });

                //  cuerpo_correo.Append(tablaprecios);
                //  cuerpo_correo.Append("</td></tr>");
                //  cuerpo_correo.Append("</table>");
                //  cuerpo_correo.Append("</td></tr><tr><td colspan='2' align=center></td></tr>");
                //  cuerpo_correo.Append("<br>");
                /*
                cuerpo_correo.Append("<a style='font-family: verdana; font-size: 8pt;' href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProCompraLocal_Autorizacion.aspx?id1=" + hfNumSolicitudAbasto.Value + "&Id2=" + session.Id_Emp + "&Id3=" + session.Id_Cd_Ver + "'>");
                cuerpo_correo.Append("Solicitud de Compra Local</a>");
                */
                cuerpo_correo.Append("</table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = false;
                MailMessage m = new MailMessage();
                string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
                m.From = new MailAddress(configuracion.Mail_Remitente);

                m.To.Add(new MailAddress("luis.mendez@bsdenterprise.com"));

                //string correo = "";

                //this.CorreosAutorizadorxMotivoxApp(ref correo, famk);
                //string[] eVirtual2 = correo.Split(',');
                //reng = 0;

                //foreach (string core in eVirtual2)
                //{
                //    if (core != " ")
                //    {
                //        if (reng == 0)
                //        {
                //            m.To.Add(new MailAddress(core));
                //            reng = 1;
                //        }
                //        else
                //        {
                //            m.CC.Add(new MailAddress(core));
                //        }

                //    }
                //}

                m.Bcc.Add(new MailAddress("luis.mendez@bsdenterprise.com"));


                m.Subject = "Contacto Comercial Key"; //   +"UNO";
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();

                //  this.RespaldoCorreo(hfNumSolicitudAbasto.Value, body, correo);

                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"../Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
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
                    Alerta("Correo enviado correctamente");
                }
                catch (Exception exx)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema. " + exx.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region ErrorManager

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
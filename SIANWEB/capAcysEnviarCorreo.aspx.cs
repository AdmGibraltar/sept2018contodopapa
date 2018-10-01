using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using CapaEntidad;
using Telerik.Web.UI;
using System.Web.UI;
using CapaNegocios;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class capAcysEnviarCorreo : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    CerrarVentana();
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                           // RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }



                        if (Request.QueryString["Id_Acs"].ToString() != "-1")
                        {
                            Acys acys = new Acys();
                            acys.Id_Emp = Sesion.Id_Emp;
                            acys.Id_Cd = Sesion.Id_Cd_Ver;
                            acys.Id_Acs = Convert.ToInt32(Request.QueryString["Id_Acs"]);

                            CN_CapAcys cn_acys = new CN_CapAcys();
                            cn_acys.ConsultaUltimaVersion(ref acys, Sesion.Emp_Cnx);
                            cn_acys.Consultar(ref acys, Sesion.Emp_Cnx);                            
                            txtAsunto.Text = "KEY - Acuerdo Comercial y de Sevicio - Cliente #" + acys.Id_Cte;
                        }
                       

                       // Inicializar();

                      /*  double ancho = 0;
                        foreach (GridColumn gc in rgPedido.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        double extra = 0;
                        if (rgPedido.Items.Count > 10)
                        {
                            extra = 18;
                        }
                        rgPedido.Width = Unit.Pixel(Convert.ToInt32(ancho + extra));
                        rgPedido.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));*/
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
       
        


        private void EnviaEmail()
        {
            try
            {
                if (Request.QueryString["Id_Acs"].ToString() != "-1")
                {
                    int Id_Acys = Convert.ToInt32(Request.QueryString["Id_Acs"]);


                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    int verificador = -1;
                    Acys acys = new Acys();
                    acys.Id_Emp = session.Id_Emp;
                    acys.Id_Cd = session.Id_Cd_Ver;
                    acys.Id_Acs = Id_Acys;

                    CN_CapAcys cn_acys = new CN_CapAcys();
                    cn_acys.Consultar(ref acys, session.Emp_Cnx);

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
                    cuerpo_correo.Append(RadEditor1.Content);
                    cuerpo_correo.Append("</td></tr></table></div>");

                    SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                    sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                    //sm.EnableSsl = true;
                    MailMessage m = new MailMessage();
                    m.From = new MailAddress(configuracion.Mail_Remitente);
                    string correos = txtCorreos.Text;
                    char[] delimiterChars = { ' ', ',', ';', ':', '\t' };
                    string[] words = correos.Split(delimiterChars);

                    foreach (string s in words)
                    {
                        m.To.Add(new MailAddress(s));
                    }

                    m.Subject = txtAsunto.Text;
                    Attachment attachment;
                    attachment = new Attachment(Server.MapPath("Reportes") + "\\AcuerdoImpresion_" + acys.Id_Acs + ".pdf");
                    m.Attachments.Add(attachment);
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
                        Alerta("Mensaje enviado exitosamente");
                        CerrarVentana();
                        
                    }
                    catch (Exception)
                    {
                        Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "panel":
                      //  Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 380);

                       /* RadPane2.Height = altura;
                        RadSplitter2.Height = altura;
                        rgProductos.Height = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 390);*/
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
       
        #region funciones
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                    funcion = "CloseWindow()";
                else
                    funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Inicializar()
        {
          /*  HF_Ped.Value = Request.QueryString["Id_Prd"].ToString();
            txtProducto.Text = Request.QueryString["Id_Prd"].ToString();*/
            // Request.QueryString["Prd_Nom"].ToString();

          /*  CN_CatProducto cn_producto = new CN_CatProducto();*/

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
           /* Producto prd = new Producto();
            cn_producto.ConsultaProducto(ref prd, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(HF_Ped.Value));*/
           // txtProductoNombre.Text = prd.Prd_Descripcion;

            /*_PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);*/
           // rgPedido.Rebind();
            ValidarPermisos();
        }
      

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "mail")
                {
                    EnviaEmail();
                }
                
                else if (btn.CommandName == "undo")
                {
                    CerrarVentana();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
       
        private void ValidarPermisos()
        {
            try
            {
                /*if (_PermisoGuardar == false & _PermisoModificar == false)
                    this.rtb1.Items[5].Visible = false;
                //Nuevo
                this.rtb1.Items[6].Visible = false;
                //Regresar
                this.rtb1.Items[4].Visible = false;
                //Eliminar
                this.rtb1.Items[3].Visible = false;
                //Imprimir
                this.rtb1.Items[2].Visible = false;
                //Correo
                this.rtb1.Items[1].Visible = false;*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                //RAM1.ResponseScripts.Add("CloseAlert('" + mensaje + "');");
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
              //  RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
              //  this.lblMensaje.Text = "";
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
               // this.lblMensaje.Text = Message;
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
               // this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
    
}
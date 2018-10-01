using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using CapaDatos;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Net.Mime;
using System.Net;

namespace SIANWEB
{
    public partial class ProSolCompraLocal : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCompraLocal_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgCompraLocal.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                        //HiddenRebind.Value = "1";
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //CerrarVentana();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                double costo = !string.IsNullOrEmpty(txtCosto.Text) ? Convert.ToDouble(txtCosto.Text) : 0;
                if (costo > 0.00)
                {
                    DataRow[] Ar_dr;
                    Ar_dr = dt.Select("Num='" + txtProducto.Text + "'");
                    if (Ar_dr.Length == 0)
                    {
                        dt.Rows.Add(new object[] { txtProducto.Text, txt_Prd.Text, Convert.ToDouble(txtCosto.Text).ToString("#,##0.00"), 0, "Sin autorizar" });
                        rgCompraLocal.Rebind();
                        //cmbProducto.SelectedIndex = 0;
                        //cmbProducto.Text = "-- Seleccionar --";
                        txtProducto.Text = "";
                        txt_Prd.Text = "";
                        txtCosto.Value = 0;
                    }
                    else
                    {
                        Alerta("El producto ya fue incluido en la solicitud");
                    }
                }
                else
                    AlertaFocus("El costo a ingresar debe ser mayor a cero", txtCosto.ClientID);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCompraLocal_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rgCompraLocal.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCompraLocal_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    int Id_PedDet = 0;
                    DataRow[] Ar_dr;
                    GridItem gi = e.Item;

                    Id_PedDet = Convert.ToInt32(((Label)gi.FindControl("lblcve")).Text);


                    Ar_dr = dt.Select("Num='" + Id_PedDet + "'");
                    if (Ar_dr.Length > 0)
                    {
                        Ar_dr[0].Delete();
                        dt.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbSolicitud_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Solicitud_Cambio(Convert.ToInt32(cmbSolicitud.SelectedValue));
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Solicitud_Cambio(int solicitud)
        {
            Creardt();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CompraLocal cl = new CompraLocal();
            cl.Id_Emp = Sesion.Id_Emp;
            cl.Id_Cd = Sesion.Id_Cd_Ver;
            cl.Id_Comp = solicitud;

            CN_ProCompraLocal cn_procompralocal = new CN_ProCompraLocal();
            DataTable dt2 = dt;
            cn_procompralocal.ConsultarSolicitud(cl, ref dt2, Sesion.Emp_Cnx);
            dt = dt2;
            rgCompraLocal.Rebind();

            //cmbProducto.SelectedIndex = 0;
            //cmbProducto.Text = "-- Seleccionar --";
            txtProducto.Text = "";
            txt_Prd.Text = "";
            txtCosto.Value = 0;
            if (cmbSolicitud.SelectedIndex != 0)
            {
                rtb1.Items[5].Enabled = false;
                rgCompraLocal.Columns[5].Visible = false;
                tbProducto.Visible = false;
            }
            else
            {
                rtb1.Items[5].Enabled = true;
                rgCompraLocal.Columns[5].Visible = true;
                tbProducto.Visible = true;
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                CargarSolicitudes();
                //Creardt();
                cmbSolicitud.SelectedIndex = Request.QueryString["Id"] != null ? cmbSolicitud.FindItemIndexByValue(Request.QueryString["Id"].ToString()) : 0;
                Solicitud_Cambio(Request.QueryString["Id"] != null ? Convert.ToInt32(Request.QueryString["Id"]) : 0);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Creardt()
        {
            try
            {
                dt = new DataTable();

                dt.Columns.Add("Num");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Costo");
                dt.Columns.Add("Estatus");
                dt.Columns.Add("EstatusStr");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSolicitudes()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spProCompraLocal_Combo", ref this.cmbSolicitud);
                this.cmbSolicitud.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
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
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                if (dt.Rows.Count == 0)
                {
                    Alerta("No ha agregado ningún producto a la solicitud");
                    return;
                }

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Funciones funciones = new Funciones();

                CompraLocal cl = new CompraLocal();
                cl.Id_Emp = Sesion.Id_Emp;
                cl.Id_Cd = Sesion.Id_Cd_Ver;
                cl.FechaSol = funciones.GetLocalDateTime(Sesion.Minutos);
                cl.Comp_Solicito = Sesion.Id_U;
                int verificador = 0;
                CN_ProCompraLocal cn_procompralocal = new CN_ProCompraLocal();

                cn_procompralocal.InsertarSolicitud(cl, dt, Sesion.Emp_Cnx, ref verificador);
                if (verificador != 0)
                {
                    Alerta("Los datos se guardaron correctamente");
                    Nuevo();
                    EnviaEmail(verificador);
                }
                else
                {
                    Alerta("Ocurrió un error al intentar guardar los datos");
                }

                CargarSolicitudes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            cmbSolicitud.SelectedIndex = 0;
            //cmbProducto.SelectedIndex = 0;
            //cmbProducto.Text = "-- Seleccionar --";
            txtProducto.Text = "";
            txt_Prd.Text = "";
            txtCosto.Value = 0;
            rtb1.Items[5].Enabled = true;
            rgCompraLocal.Columns[5].Visible = true;
            tbProducto.Visible = true;
            Creardt();
            rgCompraLocal.Rebind();
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                //if (pag.Length > 1)
                //{
                //    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                //}
                //else
                //{
                pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                //}
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                    {
                        this.rtb1.Items[6].Visible = false;
                    }
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.rtb1.Items[5].Visible = false;
                    }
                    //if (Permiso.PEliminar == false)
                    //{
                    //    this.RadToolBar1.Items[3].Visible = false;
                    //}
                    //if(Permiso.PImprimir == false)
                    //{
                    //    this.RadToolBar1.Items[2].Visible = false;
                    //}

                    //Nuevo
                    //this.rtb1.Items[6].Visible = false;
                    //Guardar
                    //this.rtb1.Items[5].Visible = false;
                    //Regresar
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = false;
                    //Correo
                    this.rtb1.Items[1].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviaEmail(int solicitud)
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
                if (configuracion.Mail_CompLocal.Length == 0)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table style='font-family: verdana; font-size:9pt'><tr><td>");
                cuerpo_correo.Append("<img src=\"cid:companylogo\"></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de compra local con el número de folio <b>" + solicitud.ToString() + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("Centro de distribución: <b>" + session.Id_Cd_Ver + " - " + CmbCentro.SelectedItem.Text + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("Solicitó: <b>" + session.Id_U + " - " + session.U_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'><br>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br><a href='" + Request.Url.ToString().Replace((new FileInfo(Request.Url.AbsolutePath)).Name, "") + "ProCompraLocal_Autorizacion.aspx?id1=" + solicitud.ToString() + "&Id2=" + session.Id_Emp + "&Id3=" + session.Id_Cd_Ver + "'>Solicitud de autorización de compra local</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                string To = configuracion.Mail_CompLocal;
                for (int x = 0; x < To.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Length; x++)
                {
                    m.To.Add(new MailAddress(To.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[x]));
                }
                m.Subject = "Solicitud de autorización de compra local";
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
                sm.Send(m);

            }
            catch (Exception)
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                //Throw ex

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

        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int prd = !string.IsNullOrEmpty(txtProducto.Text) ? Convert.ToInt32(txtProducto.Text) : -1;
                if (prd != -1)
                {
                    List<ProductoLocal> local = new List<ProductoLocal>();
                    CN_ProCompraLocal compras = new CN_ProCompraLocal();
                    try
                    {
                        compras.ConsultarPrdCompraLocal(Sesion, prd, ref local);
                        if (local.Count > 0)
                            txt_Prd.Text = local[0].Descripcion;
                        else
                            txt_Prd.Text = "";
                    }
                    catch (Exception ex)
                    {
                        txt_Prd.Text = "";
                        AlertaFocus(ex.Message, txtProducto.ClientID);                        
                    }
                }
                else
                    txt_Prd.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
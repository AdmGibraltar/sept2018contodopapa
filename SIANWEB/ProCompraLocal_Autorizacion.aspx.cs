using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Data;
using CapaDatos;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class ProAutCompraLocal : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                    //Server.Transfer("Login.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {

                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
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
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetList();
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
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
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
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkAutoriza_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem gdi in rg1.Items)
                {
                    if ((gdi["Autoriza"].FindControl("chkAutoriza") as RadioButton).Enabled)
                    {
                        (gdi["Pendiente"].FindControl("chkPendiente") as RadioButton).Checked = false;
                        (gdi["Rechaza"].FindControl("chkRechaza") as RadioButton).Checked = false;
                        (gdi["Autoriza"].FindControl("chkAutoriza") as RadioButton).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void chkRechaza_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem gdi in rg1.Items)
                {
                    if ((gdi["Rechaza"].FindControl("chkRechaza") as RadioButton).Enabled)
                    {
                        (gdi["Pendiente"].FindControl("chkPendiente") as RadioButton).Checked = false;
                        (gdi["Autoriza"].FindControl("chkAutoriza") as RadioButton).Checked = false;
                        (gdi["Rechaza"].FindControl("chkRechaza") as RadioButton).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkPendiente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem gdi in rg1.Items)
                {
                    if ((gdi["Pendiente"].FindControl("chkPendiente") as RadioButton).Enabled)
                    {
                        (gdi["Autoriza"].FindControl("chkAutoriza") as RadioButton).Checked = false;
                        (gdi["Rechaza"].FindControl("chkRechaza") as RadioButton).Checked = false;
                        (gdi["Pendiente"].FindControl("chkPendiente") as RadioButton).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkEnfocada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem gdi in rg1.Items)
                {
                    (gdi["Autoriza"].FindControl("chkEnfocada") as CheckBox).Checked = (sender as CheckBox).Checked;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones

        private void Inicializar()
        {
            try
            {

                CargarSolicitud();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSolicitud()
        {
            try
            {
                Nuevo();

                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];

                string IdSol = Request.Params["Id1"] != null ? Request.Params["Id1"].ToString() : "";
                string IdEmp = Request.Params["Id2"] != null ? Request.Params["Id2"].ToString() : "";
                string IdCd = Request.Params["Id3"] != null ? Request.Params["Id3"].ToString() : "";


                if (session2.Id_Emp.ToString() == IdEmp)
                {
                    if (session2.Id_Cd_Ver.ToString() == IdCd)
                    {
                        if (IdSol != "")
                        {
                            lblFolio.Text = IdSol;

                            CompraLocal cl = new CompraLocal();
                            cl.Id_Emp = session2.Id_Emp;
                            cl.Id_Cd = session2.Id_Cd_Ver;
                            cl.Id_Comp = Convert.ToInt32(lblFolio.Text == "" ? "0" : lblFolio.Text);
                            CN_ProCompraLocal cn_procompralocal = new CN_ProCompraLocal();
                            int verificador = -1;
                            cn_procompralocal.ConsultarSolicitud(ref cl, session2.Emp_Cnx, ref verificador);

                            if (verificador == -1)
                            {
                                Alerta("No se encontro la solicitud");

                            }
                            else
                            {
                                lblSucursal.Text = cl.Id_Cd.ToString() + " - " + cl.Cd_Nombre.ToUpper();
                                lblSolicitaId.Text = cl.Comp_Solicito.ToString();
                                lblSolicitaNombre.Text = " - " + cl.Solicito_Nombre.ToUpper();
                                lblAutorizacion.Text = cl.FechaAut.ToLower();
                                lblFolio.Text = cl.Id_Comp.ToString();
                                lblFechaSol.Text = cl.FechaSol.ToString("dd/MM/yyyy hh:mm:ss tt").ToLower();
                            }
                        }
                        else
                        {
                            Alerta("No se encontro la solicitud");
                        }
                    }
                    else
                    {
                        Alerta("La solicitud no pertenece al centro de distribución en el que se encuentra");
                    }
                }
                else
                {
                    Alerta("La solicitud no pertenece a la empresa en la que inicio sesión");
                }

                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        List<ProductoLocal> GetList()
        {
            try
            {
                List<ProductoLocal> List = new List<ProductoLocal>();
                CN_ProCompraLocal clsProCompraLocal = new CN_ProCompraLocal();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                clsProCompraLocal.ConsultaCompraLocalList(session2.Id_Emp, session2.Id_Cd_Ver, Convert.ToInt32(lblFolio.Text == "" ? "0" : lblFolio.Text), session2.Emp_Cnx, ref List);
                return List;
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

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
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

                Session["Head" + Session.SessionID] = pagina.Descripcion;
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

                    //if (Permiso.PGrabar == false)
                    //{
                    //    this.rtb1.Items[6].Visible = false;
                    //}
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
                    this.rtb1.Items[6].Visible = false;
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

                //CN_Ctrl ctrl = new CN_Ctrl();
                //ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                //ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            try
            {

                //Modificar... (faltan)
                lblSucursal.Text = string.Empty;
                lblSolicitaId.Text = string.Empty;
                lblSolicitaNombre.Text = string.Empty;
                lblAutorizacion.Text = string.Empty;
                lblFolio.Text = string.Empty;
                lblFechaSol.Text = string.Empty;

                //rg1.Rebind();

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

                int CantidadAutorizados = 0;
                bool AutOld = false;
                bool RecOld = false;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Funciones funciones = new Funciones();
                CompraLocal cl = new CompraLocal();
                cl.Id_Emp = session.Id_Emp;
                cl.Id_Cd = session.Id_Cd_Ver;
                cl.Id_Comp = Convert.ToInt32(lblFolio.Text);
                List<ProductoLocal> list = new List<ProductoLocal>();
                ProductoLocal pl = default(ProductoLocal);
                CN_ProCompraLocal cn_procompralocal = new CN_ProCompraLocal();
                foreach (GridDataItem gi in rg1.Items)
                {
                    pl = new ProductoLocal();
                    pl.Id_Det = Convert.ToInt32(gi["Id_Det"].Text);
                    pl.Id_Prd = Convert.ToInt32(gi["Id_Prd"].Text);
                    pl.Costo = Convert.ToDouble(gi["Costo"].Text);
                    pl.Estatus = (gi["Autoriza"].FindControl("chkAutoriza") as CheckBox).Checked ? "A" : (gi["Rechaza"].FindControl("chkRechaza") as CheckBox).Checked ? "R" : "0";
                    AutOld = (gi["AutorizaOld"].FindControl("chkAutorizaOld") as CheckBox).Checked;
                    RecOld = (gi["RechazaOld"].FindControl("chkRechazaOld") as CheckBox).Checked;

                    if ((gi["Autoriza"].FindControl("chkAutoriza") as CheckBox).Checked != AutOld || (gi["Rechaza"].FindControl("chkRechaza") as CheckBox).Checked != RecOld)
                    {
                        CantidadAutorizados++;
                    }

                    pl.Autorizo = session.Id_U;
                    //if (pl.Autorizado)
                    //{
                    pl.FechaAut = funciones.GetLocalDateTime(session.Minutos).ToString();
                    //}
                    pl.CompraEnfocada = (gi["Enfocada"].FindControl("chkEnfocada") as CheckBox).Checked;
                    list.Add(pl);
                }

                //if (CantidadAutorizados == 0)
                //{
                //    Alerta("No ha autorizado/rechazado ningún registro");
                //    return;
                //}

                int verificador = -1;

                cn_procompralocal.ModificarCompraLocal(cl, list, session.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    Alerta("Los datos se guardaron correctamente");
                    EnviaEmail();
                    Inicializar();
                }
                else
                {
                    Alerta("Ocurrió un error al intentar guardar los datos");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviaEmail()
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
                cuerpo_correo.Append("<td colspan='2'><b><font face= 'Tahoma' size = '2'>");
                cuerpo_correo.Append("Se ha dado respuesta a la solicitud #" + lblFolio.Text + ".");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br><a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProCompraLocal.aspx?Id=" + lblFolio.Text + "'>Solicitud de autorización de compra local</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                string[] emails = configuracion.Mail_CompLocal.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string email in emails)
                {
                    m.To.Add(new MailAddress(email));
                }
                m.Subject = "Confirmación de autorización de compra local";
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

        private string CorreoSolicitante()
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            Usuario usu = new Usuario();
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            usu.Id_Emp = session.Id_Emp;
            usu.Id_Cd = session.Id_Cd_Ver;
            usu.Id_U = Convert.ToInt32(lblSolicitaId.Text);
            string Correo_Usuario = "";
            cn_catusuario.ConsultaCorreoUsuario(usu, session.Emp_Cnx, ref Correo_Usuario);
            return Correo_Usuario;
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
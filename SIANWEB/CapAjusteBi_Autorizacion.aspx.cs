using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class CapAjusteSolicitud : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private string htmlMensaje = string.Concat("<div style=\"background-color: #E9F0FC; border-color: #1287DE; border-style:dashed; border-width:1px; padding: 10px 10px 10px 10px; margin: 10px 10px 10px 10px ; font-family:Arial Tahoma\">@@mensaje</div>");
        //int Id_Folio;

        private List<AjusteBaseInstaladaDet> ListaAjusteBaseInstalada
        {
            get { return (List<AjusteBaseInstaladaDet>)Session["ListaAjusteBaseInstalada"]; }
            set { Session["ListaAjusteBaseInstalada"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        Session["ListaAjusteBaseInstalada"] = new List<AjusteBaseInstaladaDet>();
                        this.ValidarPermisos();
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

        #region Eventos
        protected void rgAutBaseInstalada_DataBound(object source, GridItemEventArgs e)
        {
            try
            {

                if (e.Item is GridDataItem)
                {
                    GridDataItem gdi = e.Item as GridDataItem;
                    if ((gdi.FindControl("lblEstatus") as Label).Text != "C")
                    {
                        (gdi["selectColumn"].Controls[0] as CheckBox).Checked = false;
                        (gdi["selectColumn"].Controls[0] as CheckBox).Visible = false;
                        (gdi["selectColumn"].Controls[0] as CheckBox).Enabled = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                this.Inicializar();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }

        protected void rgAutBaseInstalada_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                { //Llenar Grid
                    rgAutBaseInstalada.DataSource = this.ListaAjusteBaseInstalada;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgAutBaseInstalada_NeedDataSource"));
            }
        }

        protected void rgAutBaseInstalada_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgAutBaseInstalada.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = "CapAutBaseInstalada_autorizar_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        #endregion

        #region Funciones

        private void Guardar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                int verificador = 0;
                List<AjusteBaseInstaladaDet> listaAjusteBaseInstalada = new List<AjusteBaseInstaladaDet>();

                foreach (GridDataItem item in rgAutBaseInstalada.SelectedItems)
                {
                    if (item.OwnerTableView.Name == "Master")
                    {
                        CheckBox cb  = (item["selectColumn"].Controls[0] as CheckBox);
                        if (cb.Visible)
                        {
                            AjusteBaseInstaladaDet ajusteBaseInstaladaDet = new AjusteBaseInstaladaDet();
                            ajusteBaseInstaladaDet.Id_Emp = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Emp"]);
                            ajusteBaseInstaladaDet.Id_Cd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Cd"]);
                            ajusteBaseInstaladaDet.Id_Abi = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Abi"]);
                            ajusteBaseInstaladaDet.Id_AbiDet = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_AbiDet"]);
                            ajusteBaseInstaladaDet.Abi_Estatus = "A";
                            listaAjusteBaseInstalada.Add(ajusteBaseInstaladaDet);
                        }

                    }
                }
                if (listaAjusteBaseInstalada.Count > 0)
                {
                    if (this.HD_Guardar.Value == "0")
                    {
                        new CN_CapAjusteBaseInstalada().ModificarEstatusAjusteBaseInstalada(ref listaAjusteBaseInstalada, sesion.Emp_Cnx, ref verificador);
                        this.HD_Guardar.Value = "1";
                        EnviaEmail();
                        this.DisplayMensajeAlerta("rgAutBaseInstalada_update_ok");
                    }
                }
                else
                    this.DisplayMensajeAlerta("rgAutBaseInstalada_NoSelectItems");
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

                CN_Configuracion cn_configuracion = new CN_Configuracion();
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Emp = session.Id_Emp;
                configuracion.Id_Cd = session.Id_Cd_Ver;

                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face= 'Tahoma' size = '2'>");
                cuerpo_correo.Append("La solicitud #" + lblFolio.Text + " ha sido atendida.");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br><a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "CapAjusteBi.aspx'>Solicitud de autorización de ajuste de base instalada</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(ConsultarEmail(Convert.ToInt32(HF_ID.Value))));
                m.Subject = "Confirmación de autorización de ajuste de base instalada";
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
            }
        }

        private string ConsultarEmail(int id_u)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario u = new Usuario();
            u.Id_Emp = session.Id_Emp;
            u.Id_Cd = session.Id_Cd_Ver;
            u.Id_U = id_u;
            string correo = "";
            cn_catusuario.ConsultaCorreoUsuario(u, session.Emp_Cnx, ref correo);
            return correo;
        }

        private void Inicializar()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                string IdEmp = Request.Params["Id1"] != null ? Request.Params["Id1"].ToString() : "";
                string IdCd = Request.Params["Id2"] != null ? Request.Params["Id2"].ToString() : "";
                string unique = Request.Params["Id3"] != null ? Request.Params["Id3"].ToString() : "";

                if (session.Id_Emp.ToString() == IdEmp)
                {
                    if (session.Id_Cd_Ver.ToString() == IdCd)
                    {
                        if (!string.IsNullOrEmpty(unique))
                        {
                            bool encontrado = false;
                            AjusteBaseInstalada ajusteBaseInstalada = new AjusteBaseInstalada();
                            ajusteBaseInstalada.Id_Emp = Convert.ToInt32(IdEmp);
                            ajusteBaseInstalada.Id_Cd = Convert.ToInt32(IdCd);
                            ajusteBaseInstalada.Abi_Unique = unique;
                            ajusteBaseInstalada.ListaAjusteBaseInstalada = new List<AjusteBaseInstaladaDet>();

                            new CN_CapAjusteBaseInstalada().ConsultarAjusteBaseInstalada_PorUnique(ref ajusteBaseInstalada, session.Emp_Cnx, ref encontrado);

                            if (!encontrado)
                            {
                                this.divPrincipal.Style.Add("display", "none");
                                RadToolBar1.Items.FindItemByValue("save").Visible = false;
                                Alerta("No se encontro la solicitud");
                            }
                            else
                            {
                                lblSucursal.Text = string.Concat(ajusteBaseInstalada.Id_Cd, " - ", ajusteBaseInstalada.Cd_Nombre);
                                lblSolicita.Text = string.Concat(ajusteBaseInstalada.Id_U.ToString(), " - ", ajusteBaseInstalada.U_Nombre);
                                lblAutorizacion.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                                lblNumAut.Text = ajusteBaseInstalada.Abi_Unique;
                                lblFolio.Text = ajusteBaseInstalada.Id_Abi.ToString();
                                lblFecha.Text = ajusteBaseInstalada.Abi_Fecha.ToString("dd/MM/yyyy");
                                HF_ID.Value = ajusteBaseInstalada.Id_U.ToString();
                                this.ListaAjusteBaseInstalada = ajusteBaseInstalada.ListaAjusteBaseInstalada;
                                rgAutBaseInstalada.Rebind();
                                this.divPrincipal.Style.Add("display", "block");
                                RadToolBar1.Items.FindItemByValue("save").Visible = true;
                            }
                        }
                        else
                        {
                            this.divPrincipal.Style.Add("display", "none");
                            RadToolBar1.Items.FindItemByValue("save").Visible = false;
                            Alerta("No se encontro la solicitud");
                        }
                    }
                    else
                    {
                        this.divPrincipal.Style.Add("display", "none");
                        RadToolBar1.Items.FindItemByValue("save").Visible = false;
                        Alerta("La solicitud no pertenece al centro de distribución en el que se encuentra");
                    }
                }
                else
                {
                    this.divPrincipal.Style.Add("display", "none");
                    RadToolBar1.Items.FindItemByValue("save").Visible = false;
                    Alerta("La solicitud no pertenece a la empresa en la que inicio sesión");
                }
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
                    this.TblEncabezado.Rows[1].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
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
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);

                pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

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

                    if (Permiso.PGrabar == false)
                    {
                        //this.rtb1.Items[1].Visible = false;
                    }

                }
                else
                {
                    this.RadToolBar1.Visible = false;
                    lblEtiquetaCentro.Visible = false;
                    CmbCentro.Visible = false;
                    this.divPrincipal.Visible = false;
                    this.Alerta("No se cuenta con permisos suficientes para acceder a la página");
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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgAutBaseInstalada_NoSelectItems"))
                    Alerta("No ha seleccionado ningún registro");
                else
                    if (mensaje.Contains("rgAutBaseInstalada_update_ok"))
                        Alerta("La autorización se ha realizado correctamente");
                    else
                        if (mensaje.Contains("rgAutBaseInstalada_NeedDataSource"))
                            Alerta("Error al momento de llenar el Grid");
                        else
                            if (mensaje.Contains("CapAutBaseInstalada_autorizar_error"))
                                Alerta("Error al momento de autorizar la solicitud de ajuste de base instalada");
                            else
                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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
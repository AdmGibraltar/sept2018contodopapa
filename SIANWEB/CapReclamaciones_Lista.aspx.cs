using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;

namespace SIANWEB
{
    public partial class CapReclamaciones_Lista : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion Variables

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        Session["_IdProducto"] = 0;
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas "
                                + "return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                            return;
                        }
                        this.CargarCentros();
                        this.Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                this.rgReclamaciones.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "btnBuscar_error"));
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DateTime fechaPeriodoInicio = session2.CalendarioIni;
                DateTime fechaPeriodoFinal = session2.CalendarioFin;
                Int32 item = default(Int32);
                int Id_Rec = 0;
                int Id_Rec4 = 0;
                item = (e.Item == null ? -1 : e.Item.ItemIndex);
                if (item != -1)
                {
                    GridItem gi = e.Item;
                    List<string> statusPosibles;
                    if (item >= 0)
                    {
                        Id_Rec = Convert.ToInt32(rgReclamaciones.Items[item]["Id_Rec"].Text);
                        Id_Rec4 = Convert.ToInt32(gi.Cells[4].Text);
                    }
                    switch (e.CommandName)
                    {
                        case "Imprimir":
                            if (_PermisoImprimir)
                                this.Imprimir(Id_Rec);
                            else
                                Alerta("No tiene permiso para imprimir");
                            break;
                        case "Editar":
                            statusPosibles = new List<string>() { "C", "A" };
                            if (!statusPosibles.Contains(gi.Cells[11].Text.ToUpper()))
                            {

                                RAM1.ResponseScripts.Add("OpenAlert('Imposible modificar este documento; Se encuentra en estatus de conformidad','" + Id_Rec + "','False','False')");
                                e.Canceled = true;
                                return;
                            }
                            RAM1.ResponseScripts.Add("return AbrirVentana_Edicion('" + Id_Rec + "','" + _PermisoGuardar + "','" + _PermisoModificar + "')");
                            break;
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
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= dpFecha1.MinDate && sesion.CalendarioIni <= dpFecha1.MaxDate)
                {
                    dpFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= dpFecha2.MinDate && sesion.CalendarioFin <= dpFecha2.MaxDate)
                {
                    dpFecha2.DbSelectedDate = sesion.CalendarioFin;
                }

                Session["Sesion" + Session.SessionID] = sesion;
                rgReclamaciones.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "cmbCentro_SelectedIndexChanged1");
            }
        }
        private void Inicializar()
        {
            Sesion sesion2 = new Sesion();
            sesion2 = (Sesion)Session["Sesion" + Session.SessionID];

            if (sesion2.CalendarioIni >= dpFecha1.MinDate && sesion2.CalendarioIni <= dpFecha1.MaxDate)
            {
                dpFecha1.DbSelectedDate = sesion2.CalendarioIni;
            }
            if (sesion2.CalendarioFin >= dpFecha2.MinDate && sesion2.CalendarioFin <= dpFecha2.MaxDate)
            {
                dpFecha2.DbSelectedDate = sesion2.CalendarioFin;
            }

            this.CargaEstatus();
            this.CargaTipo();

            double ancho = 0;
            foreach (GridColumn gc in rgReclamaciones.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgReclamaciones.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgReclamaciones.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
            this.rgReclamaciones.Rebind();

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.dpFecha1.SelectedDate > this.dpFecha2.SelectedDate)
            {
                this.Alerta("La fecha inicial no debe ser mayor a la fecha final");
                this.dpFecha1.Focus();
                return;
            }
            try
            {
                this.rgReclamaciones.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgReclamaciones.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgReclamaciones_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = "";

                    Button = (WebControl)item["Imprimir"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Rec").ToString());
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
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        this.rgReclamaciones.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rgReclamaciones_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    this.rgReclamaciones.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgReclamaciones_NeedDataSource"));
            }
        }
        #endregion Eventos

        #region Funciones
        private bool ValidarSesion()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Reclamaciones> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<Reclamaciones> listaReclamaciones = new List<Reclamaciones>();
                Reclamaciones reclamaciones = new Reclamaciones();

                new CN_CapReclamaciones().BuscaReclamaciones(reclamaciones, sesion.Emp_Cnx, ref listaReclamaciones, sesion.Id_Emp, sesion.Id_Cd_Ver,
                    this.txtRec1.Text == string.Empty ? -1 : Convert.ToInt32(this.txtRec1.Text),
                    this.txtRec2.Text == string.Empty ? -1 : Convert.ToInt32(this.txtRec2.Text),
                    this.txtCliente1.Text == string.Empty ? -1 : Convert.ToInt32(this.txtCliente1.Text),
                    this.txtCliente2.Text == string.Empty ? -1 : Convert.ToInt32(this.txtCliente2.Text),
                    this.cmbEstatus.SelectedValue,
                    this.dpFecha1.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.dpFecha1.SelectedDate),
                    this.dpFecha2.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.dpFecha2.SelectedDate),
                    this.txtNombre.Text,
                    Convert.ToInt32(this.cmbTipo.SelectedValue));

                return listaReclamaciones;
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
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
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

                if (Permiso.PAccesar)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (!Permiso.PGrabar)
                        this.RadToolBar1.Items[3].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaTipo()
        {
            this.cmbTipo.Items.Insert(0, new RadComboBoxItem("-- Todos --", "-1"));
            this.cmbTipo.Items.Insert(1, new RadComboBoxItem("Producto", "1"));
            this.cmbTipo.Items.Insert(2, new RadComboBoxItem("Servicio administrativo/Operativo", "2"));
            this.cmbTipo.Items.Insert(3, new RadComboBoxItem("Servicio de asesoría", "3"));
        }
        private void CargaEstatus()
        {
            this.cmbEstatus.Items.Insert(0, new RadComboBoxItem("-- Todos --", ""));
            this.cmbEstatus.Items.Insert(1, new RadComboBoxItem("Acción", "A"));
            this.cmbEstatus.Items.Insert(2, new RadComboBoxItem("Capturado", "C"));
            this.cmbEstatus.Items.Insert(3, new RadComboBoxItem("Conformidad", "F"));
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
        private void Imprimir(int Id_Rec)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Id_Rec);

                Type instance = null;
                instance = typeof(LibreriaReportes.ReclamacionesImpresion);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
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
                if (mensaje.Contains("CapReclamaciones_Imprimir_Denegado"))
                    Alerta("Imposible imprimir el documento");
                else
                    if (mensaje.Contains("CapReclamaciones_Modificar_Denegado"))
                        Alerta("Imposible modificar el documento");
                    else
                        if (mensaje.Contains("CapReclamaciones_print_error"))
                            Alerta("Error al imprimir la reclamación");
                        else
                            if (mensaje.Contains("btnBuscar_error"))
                                Alerta("Error al momento de filtrar la información");
                            else
                                if (mensaje.Contains("RAM1_AjaxRequest"))
                                    Alerta("Error al momento de actualizar el Grid de ordenes de compra");
                                else
                                    if (mensaje.Contains("rgReclamaciones_NeedDataSource"))
                                        Alerta("Error al cargar el Grid de detalle de orden de compra");
                                    else
                                        if (mensaje.Contains("rgReclamaciones_ItemCommand"))
                                            Alerta("Error al ejecutar un evento (rgOrdCompra_ItemCommand) al cargar el Grid de orden de compra");
                                        else
                                            if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                Alerta("Error al cambiar de página");
                                            else
                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion Funciones

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
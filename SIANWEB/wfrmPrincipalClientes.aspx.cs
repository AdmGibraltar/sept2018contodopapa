using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CrmCatCliente : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }
        public int refrescar
        {
            get
            {
                int ret = Session["refrescarCl" + Session.SessionID] != null ? (int)Session["refrescarCl" + Session.SessionID] : 1;
                Session["refrescarCl" + Session.SessionID] = 2;
                return ret;

            }
            set { Session["refrescarCl" + Session.SessionID] = value; }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (session.Cu_Modif_Pass_Voluntario == false)
                            return;
                        RadAjaxManager1.ResponseScripts.Add("refreshGrid();");
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
                    rg1.DataSource = GetList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void lnkDescargar_Click(object sender, EventArgs e)
        {

        }
        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string link = "wfrmDetalleCliente.aspx?" +
                        "ID=" + item.GetDataKeyValue("Id_Cte").ToString() +
                        "&Seg=" + item.GetDataKeyValue("Id_Seg").ToString() +
                        "&Ter=" + item.GetDataKeyValue("Id_Terr").ToString();
                    (item.FindControl("lnkNombre") as LinkButton).PostBackUrl = link;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_SortCommand(object source, GridSortCommandEventArgs e)
        {
            try
            {
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        protected void cmbUEN_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarSegmentos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbSegmento_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarTerritorio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ibtnFiltro_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ErrorManager();
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                CargarUEN();
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUEN()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd, session.Id_U, session.Emp_Cnx, "spCatRikUen_Combo", ref cmbUEN);                
                cmbUEN.Items.Remove(0);

                RadComboBoxItem rcb = new RadComboBoxItem();
                rcb.Value = "-1";
                rcb.Text = "-- Todos --";
                cmbUEN.Items.Insert(0, rcb);
                cmbUEN.SelectedIndex = 0;
                CargarSegmentos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarSesion()
        {
            try
            {
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
        private void ValidarPermisos()
        {
            try
            {
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla
                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSegmentos()
        {
            try
            {
                if (cmbUEN.SelectedValue == "")
                    cmbUEN.SelectedIndex = 0;

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(cmbUEN.SelectedValue), Convert.ToInt32(session.Id_U), session.Emp_Cnx, "spCatSegmentosUen_Combo", ref cmbSegmento);
                cmbSegmento.Items.Remove(0);

                RadComboBoxItem rcb = new RadComboBoxItem();
                rcb.Value = "-1";
                rcb.Text = "-- Todos --";
                cmbSegmento.Items.Insert(0, rcb);
                cmbSegmento.SelectedIndex = 0;
                CargarTerritorio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Clientes> GetList()
        {
            try
            {
                List<Clientes> List = new List<Clientes>();
                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes cte = new Clientes();
                cte.Id_Emp = session.Id_Emp;
                cte.Id_Cd = session.Id_Cd_Ver;
                cte.Id_Uen = cmbUEN.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(cmbUEN.SelectedValue);
                cte.Id_Seg = cmbSegmento.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(cmbSegmento.SelectedValue);
                cte.Id_Terr = cmbTerritorios.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(cmbTerritorios.SelectedValue);
                cte.Id_Rik = session.Id_U;
                cte.Id_Cte = (int?)txtNoCliente.Value;
                cte.Cte_NomComercial = txtNombre.Text;
                cn_catcliente.Lista(cte, session.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorio()
        {
            try
            {
                if (cmbSegmento.SelectedValue == "")
                    cmbSegmento.SelectedIndex = 0;

                if (cmbSegmento.SelectedIndex != -1)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd, Convert.ToInt32(cmbSegmento.SelectedValue), session.Id_Rik == -1 ? (int?)null : session.Id_Rik, session.Emp_Cnx, "spCatTerritorioSegmento_Combo", ref cmbTerritorios);
                    cmbTerritorios.Items.Remove(0);
                    RadComboBoxItem rcb = new RadComboBoxItem();
                    rcb.Value = "-1";
                    rcb.Text = "-- Todos --";
                    cmbTerritorios.Items.Insert(0, rcb);
                    cmbTerritorios.SelectedIndex = 0;
                }
                else
                    cmbTerritorios.DataSource = null;
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
                //RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
                //this.lblMensaje.Text = "";
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
                //this.lblMensaje.Text = Message;
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
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();
            }
            catch (Exception)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                Inicializar();
            }
            catch (Exception)
            {

            }
        }
    }
}

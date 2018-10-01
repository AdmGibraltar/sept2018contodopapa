using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Runtime.Remoting;

namespace SIANWEB
{
    public partial class CrmMetas : System.Web.UI.Page
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
                        Inicializar();
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
                    dgMetas.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void dgMetas_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {//paginador
            try
            {
                ErrorManager();
                dgMetas.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlCDS_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                dgMetas.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ibtnAplicar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AplicaraTodos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void lnkGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
                GuardarMetas();
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
                llenarCombo();
                dgMetas.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void llenarCombo()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref ddlCDS);
                this.ddlCDS.SelectedValue = Sesion.Id_Cd_Ver.ToString();               
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
                    Cache["href"] = pag[pag.Length - 1];
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
        private List<Meta> GetList()
        {
            try
            {
                List<Meta> List = new List<Meta>();
                CN_CatMeta clsCatMeta = new CN_CatMeta();
                int valor = !string.IsNullOrEmpty(ddlCDS.SelectedValue) ? Convert.ToInt32(ddlCDS.SelectedValue) : session.Id_Cd_Ver;
                Meta meta = new Meta();
                meta.Id_Emp = session.Id_Emp;
                meta.Id_Cd = valor;//session.Id_Cd_Ver;
                meta.Id_Rik = session.Id_U;
                clsCatMeta.Lista(meta, session.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GuardarMetas()
        {
            try
            {
                int validador = 0;
                for (int i = 0; i < this.dgMetas.Items.Count; i++)
                {
                    Meta metas = new Meta();
                    RadNumericTextBox txtCantidad = new RadNumericTextBox();
                    RadNumericTextBox txtMonto = new RadNumericTextBox();
                    RadNumericTextBox txtAvances = new RadNumericTextBox();
                    RadNumericTextBox txtCantidadCerrados = new RadNumericTextBox();
                    RadNumericTextBox txtMontoCerrados = new RadNumericTextBox();

                    txtCantidad = (RadNumericTextBox)this.dgMetas.Items[i].Cells[5].FindControl("txtCantidad");
                    txtMonto = (RadNumericTextBox)this.dgMetas.Items[i].Cells[6].FindControl("txtMonto");
                    txtAvances = (RadNumericTextBox)this.dgMetas.Items[i].Cells[7].FindControl("txtAvances");
                    txtCantidadCerrados = (RadNumericTextBox)this.dgMetas.Items[i].Cells[8].FindControl("txtCantidadCerrados");
                    txtMontoCerrados = (RadNumericTextBox)this.dgMetas.Items[i].Cells[9].FindControl("txtMontoCerrados");

                    metas.Id_Emp = Convert.ToInt32(this.session.Id_Emp);
                    metas.Id_Cd = !string.IsNullOrEmpty(ddlCDS.SelectedValue) ? Convert.ToInt32(ddlCDS.SelectedValue) : session.Id_Cd_Ver;
                    metas.Id_Rik = Convert.ToInt32(dgMetas.Items[i]["Id_Rik"].Text);
                    metas.Id_Met = Convert.ToInt32(dgMetas.Items[i]["Id_Met"].Text);
                    metas.Met_Proyectos = txtCantidad.Value.HasValue ? Convert.ToInt32(txtCantidad.Value.Value) : 0;
                    metas.Monto = txtMonto.Value.HasValue ? Convert.ToDouble(txtMonto.Value.Value) : 0;
                    metas.Met_Avances = txtAvances.Value.HasValue ? Convert.ToDouble(txtAvances.Value.Value) : 0;
                    metas.Met_CantCerrado = txtCantidadCerrados.Value.HasValue ? Convert.ToInt32(txtCantidadCerrados.Value.Value) : 0;
                    metas.Met_MontCerrado = txtMontoCerrados.Value.HasValue ? Convert.ToDouble(txtMontoCerrados.Value.Value) : 0;

                    CN_CatMeta catMeta = new CN_CatMeta();
                    catMeta.Modificar(metas, session, ref validador);
                }
                if (dgMetas.Items.Count > 0 && validador >= 1)
                    Alerta("Registros guardados correctamente");
                else
                    Alerta("No existen registros para actualizar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AplicaraTodos()
        {
            RadNumericTextBox txtCantidad = new RadNumericTextBox();
            RadNumericTextBox txtMonto = new RadNumericTextBox();
            RadNumericTextBox txtAvances = new RadNumericTextBox();
            RadNumericTextBox txtCantidadCerrados = new RadNumericTextBox();
            RadNumericTextBox txtMontoCerrados = new RadNumericTextBox();

            if (string.IsNullOrEmpty(txtACantidad.Text) && string.IsNullOrEmpty(this.txtAMonto.Text) && string.IsNullOrEmpty(this.txtAAvances.Text) && string.IsNullOrEmpty(this.txtACantidadCerrados.Text) && string.IsNullOrEmpty(this.txtAMontoCerrados.Text))
            {
                Alerta("Ingrese algún valor en los campos del recuadro");
                return;
            }
            if (dgMetas.Items.Count == 0)
                Alerta("No existen registros para actualizar");
            else
            {
                for (int i = 0; i < this.dgMetas.Items.Count; i++)
                {
                    txtCantidad = (RadNumericTextBox)this.dgMetas.Items[i].Cells[5].FindControl("txtCantidad");
                    txtMonto = (RadNumericTextBox)this.dgMetas.Items[i].Cells[6].FindControl("txtMonto");
                    txtAvances = (RadNumericTextBox)this.dgMetas.Items[i].Cells[7].FindControl("txtAvances");
                    txtCantidadCerrados = (RadNumericTextBox)this.dgMetas.Items[i].Cells[8].FindControl("txtCantidadCerrados");
                    txtMontoCerrados = (RadNumericTextBox)this.dgMetas.Items[i].Cells[9].FindControl("txtMontoCerrados");
                    if (!string.IsNullOrEmpty(txtACantidad.Text))
                        txtCantidad.Text = txtACantidad.Text;

                    if (!string.IsNullOrEmpty(this.txtAMonto.Text))
                        txtMonto.Text = this.txtAMonto.Text;

                    if (!string.IsNullOrEmpty(this.txtAAvances.Text))
                        txtAvances.Text = this.txtAAvances.Text;

                    if (!string.IsNullOrEmpty(this.txtACantidadCerrados.Text))
                        txtCantidadCerrados.Text = this.txtACantidadCerrados.Text;

                    if (!string.IsNullOrEmpty(this.txtAMontoCerrados.Text))
                        txtMontoCerrados.Text = this.txtAMontoCerrados.Text;
                }
                ////Borrando valores
                this.txtACantidad.Text = string.Empty;
                this.txtAMonto.Text = string.Empty;
                this.txtAAvances.Text = string.Empty;
                this.txtACantidadCerrados.Text = string.Empty;
                this.txtAMontoCerrados.Text = string.Empty;
            }
        }
        private void ValidarPermisos()
        {
            try
            {
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;


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
                {
                    Response.Redirect("Inicio.aspx");
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
                this.lblMensaje.Text = "";
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
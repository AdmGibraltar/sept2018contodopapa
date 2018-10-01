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

namespace SIANWEB
{
    public partial class ProRastreoDocumentos : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (session.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = session;
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                session = sesion2;
                Limpiar();
                cmbTipo.SelectedIndex = 0;
                txtDocumento.Value = null;
                cmbSerie.SelectedIndex = 0;
                rgDocumentos.Rebind();
                rgLogDocumento.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarConsecutivos();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CmbTipoBusqueda_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CmbTipoBusqueda.SelectedValue == "1")
            {
                txtDocumento.Visible = true;
                txtDocumentoFolioFiscal.Visible = false;
                txtDocumentoFolioFiscal.Text = "";
            }
            else
            {
                txtDocumento.Text = "-1";
                txtDocumento.Visible = false;
                txtDocumentoFolioFiscal.Visible = true;
            }
        }

        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (cmbTipo.SelectedValue == "1")
                {
                    CN_CapFactura cn_capfactura = new CN_CapFactura();
                    CN_Rastreo cn_rastreo = new CN_Rastreo();
                    Factura fac = new Factura();
                    fac.Id_Emp = session.Id_Emp;
                    fac.Id_Cd = session.Id_Cd_Ver;
                    fac.Id_FacSerie = cmbSerie.Text + txtDocumento.Text;
                    fac.Fac_FolioFiscal = txtDocumentoFolioFiscal.Text;
                    cn_capfactura.Rastreo(ref fac, session.Emp_Cnx, Int32.Parse(CmbTipoBusqueda.SelectedValue));
                    if (fac.Id_Cte != 0)
                    {
                        txtClienteId.Value = fac.Id_Cte;
                        txtCliente.Text = fac.Cte_NomComercial;
                        txtIva.Value = fac.Fac_ImporteIva;
                        txtImporte.Value = fac.Fac_SubTotal;
                        txtTotal.Value = fac.Fac_ImporteIva + fac.Fac_SubTotal;
                        txtSaldo.Value = fac.Fac_ImporteIva + fac.Fac_SubTotal - fac.Fac_Pagado;
                        txtEstatus.Text = fac.Fac_EstatusStr;
                        txtFolioFiscal.Text = fac.Fac_FolioFiscal;
                        txtDocumentoNumero.Value = fac.Id_Fac;
                        dpFecha.SelectedDate = fac.Fac_Fecha == default(DateTime) ? null : (DateTime?)fac.Fac_Fecha;
                        rgDocumentos.Rebind();
                        rgLogDocumento.Rebind();
                     

                    }
                    else
                    {
                        Limpiar();
                        Alerta("No se encontró el documento");
                    }
                }
                else
                {
                    CN_CapNotaCargo cn_capnota = new CN_CapNotaCargo();
                    NotaCargo nca = new NotaCargo();
                    nca.Id_Emp = Sesion.Id_Emp;
                    nca.Id_Cd = Sesion.Id_Cd_Ver;
                    nca.Id_NcaSerie = cmbSerie.Text + txtDocumento.Text;
                    nca.Nca_FolioFiscal = txtDocumentoFolioFiscal.Text;
                    cn_capnota.Rastreo(ref nca, Sesion.Emp_Cnx, Int32.Parse(CmbTipoBusqueda.SelectedValue));
                    if (nca.Id_Cte != 0)
                    {
                        txtClienteId.Value = nca.Id_Cte;
                        txtCliente.Text = nca.Cte_NomComercial;
                        txtIva.Value = nca.Nca_Iva;
                        txtImporte.Value = nca.Nca_Subtotal;
                        txtTotal.Value = nca.Nca_Iva + nca.Nca_Subtotal;
                        txtSaldo.Value = nca.Nca_Iva + nca.Nca_Subtotal - nca.Nca_Pagado;
                        txtEstatus.Text = nca.Nca_EstatusStr;
                        dpFecha.SelectedDate = nca.Nca_Fecha;
                        txtFolioFiscal.Text = nca.Nca_FolioFiscal;
                        txtDocumentoNumero.Value = nca.Id_Nca;
                        rgDocumentos.Rebind();
                    }
                    else
                    {
                        Limpiar();
                        Alerta("No se encontró el documento");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDocumentos_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgDocumentos.DataSource = GetList();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }


        protected void rgLogDocumento_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgLogDocumento.DataSource = GetLogList();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void rgDocumentos_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rgDocumentos.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgLogDocumento_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rgLogDocumento.Rebind();
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
                CargarTipo();
                CargarTipoBusqueda();
                CargarCentros();
                CargarConsecutivos();
                rgDocumentos.Rebind();
                rgLogDocumento.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Limpiar()
        {
            txtClienteId.Value = null;
            txtCliente.Text = "";
            txtIva.Value = null;
            txtImporte.Value = null;
            txtTotal.Value = null;
            txtSaldo.Value = null;
            txtEstatus.Text = "";
            dpFecha.SelectedDate = null;
            txtFolioFiscal.Text = "";
            txtDocumentoNumero.Value = null;
        }
        private void CargarTipo()
        {
            try
            {
                cmbTipo.Items.Clear();
                cmbTipo.Items.Add(new RadComboBoxItem("Facturas", "1"));
                cmbTipo.Items.Add(new RadComboBoxItem("Nota de cargo", "2"));
                cmbTipo.Sort = RadComboBoxSort.Ascending;
                cmbTipo.SortItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipoBusqueda()
        {
            try
            {
                CmbTipoBusqueda.Items.Clear();
                CmbTipoBusqueda.Items.Add(new RadComboBoxItem("Documento", "1"));
                CmbTipoBusqueda.Items.Add(new RadComboBoxItem("Folio Fiscal", "2"));
                CmbTipoBusqueda.Sort = RadComboBoxSort.Ascending;
                CmbTipoBusqueda.SortItems();
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
        private void CargarConsecutivos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int tipo = !string.IsNullOrEmpty(cmbTipo.SelectedValue) ? Convert.ToInt32(cmbTipo.SelectedValue) : 1;
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, tipo, Sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbSerie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Rastreo> GetList()
        {
            try
            {
                List<Rastreo> list = new List<Rastreo>();
                CN_Rastreo cn_rastreo = new CN_Rastreo();
                Rastreo rastreo = new Rastreo();
                rastreo.Id_Emp = session.Id_Emp;
                rastreo.Id_Cd = session.Id_Cd_Ver;
                rastreo.Ras_TipoDoc = Convert.ToInt32(cmbTipo.SelectedValue);
                rastreo.Ras_SerieDoc = cmbSerie.Text;
                rastreo.Ras_Doc = txtDocumento.Text;
                rastreo.Ras_FolioFiscal = txtDocumentoFolioFiscal.Text;
                cn_rastreo.Lista(rastreo, ref list, session.Emp_Cnx, Int32.Parse(CmbTipoBusqueda.SelectedValue));
                return list.Where(Rastreo => Rastreo.Doc_Fecha >= dpFecha.SelectedDate.Value.AddDays(-30)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Rastreo> GetLogList()
        {
            try
            {
                List<Rastreo> list = new List<Rastreo>();
                CN_Rastreo cn_rastreo = new CN_Rastreo();
                Rastreo rastreo = new Rastreo();
                rastreo.Id_Emp = session.Id_Emp;
                rastreo.Id_Cd = session.Id_Cd_Ver;
                rastreo.Ras_TipoDoc = Convert.ToInt32(cmbTipo.SelectedValue);
                rastreo.Ras_SerieDoc = cmbSerie.Text;
                rastreo.Ras_Doc = txtDocumento.Text;
                rastreo.Ras_FolioFiscal = txtDocumentoFolioFiscal.Text;
                cn_rastreo.LogDocumento(rastreo, ref list, session.Emp_Cnx, Int32.Parse(CmbTipoBusqueda.SelectedValue));
                return list;
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
                }
                else
                    Response.Redirect("Inicio.aspx");
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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
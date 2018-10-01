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
    public partial class ProFacturaRuta_Entrega : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool ProcEntAlm;
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];         
                    Response.Redirect("login.aspx" , false);
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
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {//cmbCentro
            try
            {
                Sesion sesion = new Sesion();  
                sesion = (Sesion)Session["Sesion" + Session.SessionID];    
                if (sesion == null)             
                {                   
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);      
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];  
                    Response.Redirect("login.aspx" , false);           
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
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {//carga inicial del grid
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgFactura.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rgFactura_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Boolean Proceso;
                Proceso = Sesion.ProcEntAlm;
                
                    if (e.CommandName.ToString() == "Autorizar")
                    {
                        if (Proceso == true)
                        {
                            Int32 item = default(Int32);
                            item = e.Item.ItemIndex;
                            if (item >= 0)
                            {
                                CN__Comun.RemoverValidadores(Validators);
                                FacturaEntregaRuta facturas = new FacturaEntregaRuta();
                                facturas.Id_Emb = Convert.ToInt32(rgFactura.Items[item]["Id_Emb"].Text);
                                Autorizar(facturas);
                            }
                        }
                        else
                        {
                            Alerta("El proceso no esta activado para el CDI");
                        }
                    }
                
               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFactura_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {//cambio de pagina
            try
            {
                ErrorManager();
                this.rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }       
        protected void rgFactura_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = string.Empty;

                Button = (WebControl)item["Autorizar"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Emb").ToString());
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {//boton de filtro        
            if (dpFechaini.SelectedDate > dpFechafin.SelectedDate)
            {
                Alerta("La fecha inicial no debe ser mayor a la fecha final");
                return;
            }
            try
            {
                this.rgFactura.Rebind();
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
                ComboEstatus();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ProcEntAlm = sesion.ProcEntAlm;
                if (sesion.CalendarioIni >= dpFechaini.MinDate && sesion.CalendarioIni <= dpFechaini.MaxDate)
                {
                    dpFechaini.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= dpFechafin.MinDate && sesion.CalendarioFin <= dpFechafin.MaxDate)
                {
                    dpFechafin.DbSelectedDate = sesion.CalendarioFin;
                }
                rgFactura.Rebind();
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
        private void Nuevo()
        {
            try
            {
                ComboEstatus();
                cmbEstatus.SelectedIndex = 0;
                cmbEstatus.SelectedValue = "0";
                txtEmbarque.Text = string.Empty;
                dpFechaini.DateInput.Text = string.Empty;
                dpFechafin.DateInput.Text = string.Empty;
                dpFechaini.Clear();
                dpFechafin.Clear();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Autorizar(FacturaEntregaRuta facturas)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_FacturasRutaEntrega clsFactura = new CN_FacturasRutaEntrega();
                int verificador = 0;
                clsFactura.ModificarFacturaRutaEntrega(session, facturas, ref verificador);
                if (verificador == 1)
                    Alerta("El embarque # " + facturas.Id_Emb + " fue entregado correctamente");
                else
                    Alerta("No se pudo actualizar el embarque");
                rgFactura.Rebind();                
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
        private List<FacturaEntregaRuta> GetList()
        {
            try
            {
                List<FacturaEntregaRuta> List = new List<FacturaEntregaRuta>();

                CN_FacturasRutaEntrega clsFacturasEntrega = new CN_FacturasRutaEntrega();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaEntregaRuta facturafiltro = new FacturaEntregaRuta();
                facturafiltro.Filtro_Embarque = txtEmbarque.Text;
                facturafiltro.Filtro_Estatus = !string.IsNullOrEmpty(cmbEstatus.SelectedValue) ? cmbEstatus.SelectedValue : "";
                facturafiltro.Filtro_FecIni= dpFechaini.SelectedDate.HasValue ? dpFechaini.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                facturafiltro.Filtro_FecFin = dpFechafin.SelectedDate.HasValue ? dpFechafin.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                clsFacturasEntrega.ConsultaFacturasEntrega(session, facturafiltro, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ComboEstatus()
        {
            if (!IsPostBack)
            {
                cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));            
                cmbEstatus.Items.Add(new RadComboBoxItem("Impreso", "I"));               
                cmbEstatus.Items.Add(new RadComboBoxItem("Entrega parcial", "P"));               
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
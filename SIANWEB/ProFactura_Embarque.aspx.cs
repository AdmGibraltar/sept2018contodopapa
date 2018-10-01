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
    public partial class ProFactura_Embarque : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }  
        #endregion Variables

        #region Eventos
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

        protected void rgFactura_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = "";

                    Button = (WebControl)item["Confirmar"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Fac").ToString());
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
                        //this.rgFactura.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                this.ValidarPermisos();
                if (_PermisoModificar)
                {
                    Int32 item = default(Int32);

                    //if (item != 0)
                    //{
                        item = e.Item.ItemIndex;
                    //}

                    int verificador = -1;

                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    DateTime fechaPeriodoInicio = session2.CalendarioIni;
                    DateTime fechaPeriodoFinal = session2.CalendarioFin;
                    switch (e.CommandName)
                    {
                        case "Confirmar":
                            Factura factura = new Factura();

                            factura.Id_Emp = session2.Id_Emp;
                            factura.Id_Cd = session2.Id_Cd_Ver;
                            factura.Id_Fac = Convert.ToInt32(rgFactura.Items[item]["Id_Fac"].Text);
                            try
                            {
                                factura.Fac_PedNum = Convert.ToInt32(rgFactura.Items[item]["Fac_PedNum"].Text);
                            }
                            catch
                            {
                                factura.Fac_PedNum = 0;
                            }
                            factura.Id_U = session2.Id_U;
                            factura.Fac_Fecha = DateTime.Now;

                            CN_ProFactura_Embarque CNProFactura_Embarque = new CN_ProFactura_Embarque();

                            CNProFactura_Embarque.CambiaEstatusFacturaEmbarque(factura, dt, session2.Emp_Cnx, ref verificador);

                            if (verificador > -1)
                            {
                                this.Alerta("La factura ha sido cambiada de estatus a embarcado");
                                this.rgFactura.Rebind();
                            }
                            else
                            {
                                this.Alerta("Ocurrió un problema al cambiar el estatus de la factura");
                            }
                            break;
                    }
                }
                else
                {
                    this.Alerta("No tiene autorización para cambiar el estatus a la factura");
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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
                this.rgFactura.Rebind();
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];      
                    Response.Redirect("login.aspx" , false);            
                }              
                CN__Comun comun = new CN__Comun();   
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                this.Inicializar();
                rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        #endregion Eventos

        #region Funciones
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
                    _PermisoModificar = Permiso.PModificar;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())               
                    if (!Page.IsPostBack)
                    {
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
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
                            CargarCentros();
                            this.Inicializar();
                        }
                    }               
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
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
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
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion.CalendarioIni >= dpFecha1.MinDate && sesion.CalendarioIni <= dpFecha1.MaxDate)
            {
                dpFecha1.DbSelectedDate = sesion.CalendarioIni;
            }
            if (sesion.CalendarioFin >= dpFecha2.MinDate && sesion.CalendarioFin <= dpFecha2.MaxDate)
            {
                dpFecha2.DbSelectedDate = sesion.CalendarioFin;
            }
            this.rgFactura.Rebind();
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgFactura.DataSource = this.GetList();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgReclamaciones_NeedDataSource"));
            }
        }

        private List<Factura> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<Factura> listaFacturas = new List<Factura>();
                Factura factura = new Factura();

                new CN_ProFactura_Embarque().BuscaFacturaEmbarque(factura, sesion.Emp_Cnx, ref listaFacturas, sesion.Id_Emp, sesion.Id_Cd_Ver,
                    this.txtNombre.Text,
                    this.txtCliente1.Text == string.Empty ? -1 : Convert.ToInt32(this.txtCliente1.Text),
                    this.dpFecha1.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.dpFecha1.SelectedDate),
                    this.dpFecha2.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.dpFecha2.SelectedDate));
                
                return listaFacturas;
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

        #endregion ErrorManager

    }
}
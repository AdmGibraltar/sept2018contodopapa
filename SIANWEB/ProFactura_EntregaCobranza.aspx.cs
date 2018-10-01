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
using System.Configuration;
using System.Data.SqlClient;

namespace SIANWEB
{
    public partial class ProFactura_EntregaCobranza : System.Web.UI.Page
    {
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
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
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
                if (e.CommandName.ToString() == "Autorizar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);
                        FacturaEntrega facturas = new FacturaEntrega();
                        facturas.Id_Fac = Convert.ToInt32(rgFactura.Items[item]["Id_Fac"].Text);
                        facturas.Num_Cliente = Convert.ToInt32(rgFactura.Items[item]["Num_Cliente"].Text);
                        facturas.Pedido = Convert.ToInt32(rgFactura.Items[item]["Pedido"].Text);
                        facturas.Numero = rgFactura.Items[item]["Numero"].Text;
                        Autorizar(facturas);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFactura_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
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
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (txtClienteini.Value > txtClientefin.Value)
            {
                Alerta("El número de cliente inicial no debe ser mayor al número de cliente final");
                return;
            }
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
        protected void rgFactura_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = string.Empty;

                Button = (WebControl)item["Autorizar"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Numero").ToString());
            }
        }
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion.CalendarioIni >= dpFechaini.MinDate && Sesion.CalendarioIni <= dpFechaini.MaxDate)
                {
                    dpFechaini.DbSelectedDate = Sesion.CalendarioIni;
                }
                if (Sesion.CalendarioFin >= dpFechafin.MinDate && Sesion.CalendarioFin <= dpFechafin.MaxDate)
                {
                    dpFechafin.DbSelectedDate = Sesion.CalendarioFin;
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
                txtClienteini.Text = string.Empty;
                txtClientefin.Text = string.Empty;
                txtNombre.Text = string.Empty;
                dpFechaini.DateInput.Text = string.Empty;
                dpFechafin.DateInput.Text = string.Empty;
                dpFechaini.Clear();
                dpFechafin.Clear();
                HF_ID.Value = string.Empty;
                HF_PED.Value = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Autorizar(FacturaEntrega facturas)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_FacturasEntrega clsFactura = new CN_FacturasEntrega();
                int verificador = -1;
                facturas.DbName = (new SqlConnectionStringBuilder(session.Emp_Cnx)).InitialCatalog;
                //
                clsFactura.ModificarProFacturaEntrega(session.Id_Emp, session.Id_Cd_Ver, session.Id_U, facturas, Emp_CnxCob, ref verificador, facturas.DbName);
                if (verificador == 1)
                    Alerta("La factura <b>" + facturas.Numero + "</b> fue recibida correctamente");
                else
                    Alerta("No se pudo autorizar la factura");

                rgFactura.Rebind();
                //Nuevo();
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
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<FacturaEntrega> GetList()
        {
            try
            {
                List<FacturaEntrega> List = new List<FacturaEntrega>();
                CN_FacturasEntrega clsFacturasEntrega = new CN_FacturasEntrega();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaEntrega facturafiltro = new FacturaEntrega();

                facturafiltro.Filtro_Nombre = txtNombre.Text;
                facturafiltro.Filtro_Id_Cte = txtClienteini.Text;
                facturafiltro.Filtro_Id_Cte2 = txtClientefin.Text;
                facturafiltro.Filtro_FecIni = dpFechaini.SelectedDate;
                facturafiltro.Filtro_FecFin = dpFechafin.SelectedDate;

                clsFacturasEntrega.ConsultaFacturasEntrega(session2.Id_Emp, session2.Id_Cd_Ver, Emp_CnxCob, facturafiltro, ref List);
                return List;
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
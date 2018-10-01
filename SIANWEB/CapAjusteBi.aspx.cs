using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using CapaDatos;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class CapAjusteBase : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion session
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
        private DataTable dt
        {
            get
            {
                return (DataTable)Session["dt" + Session.SessionID];
            }
            set
            {
                Session["dt" + Session.SessionID] = value;
            }
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
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
                        cmbTerritorioDestino.SelectedIndex = 0;
                        cmbTerritorioOrigen.SelectedIndex = 0;
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
                ErrorManager();
                session.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
                Limpiar();
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgAjuste.DataSource = dt;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                rgAjuste.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        Inicializar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridItem gi = e.Item;
                switch (e.CommandName)
                {
                    case "Delete":
                        string Abi_Estatus = string.Empty;
                        if (!Convert.IsDBNull(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Abi_Estatus"]))
                        {
                            Abi_Estatus = (gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Abi_Estatus"]).ToString();
                        }
                        if (string.IsNullOrEmpty(Abi_Estatus) || Abi_Estatus == "C")
                        {
                            Eliminar(gi);
                        }
                        else
                        {
                            Alerta("No se puede eliminar el registro, porque ya ha sido autorizado");
                        }
                        break;
                }
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
                ErrorManager();
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "save":
                        if (Page.IsValid)
                            Guardar();
                        break;
                    case "add":
                        break;
                    case "new":
                        Limpiar();
                        cmbSolicitud.SelectedIndex = 0;
                        cmbSolicitud.Text = cmbSolicitud.Items[0].Text;
                        dt.Rows.Clear();
                        rgAjuste.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgPago_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = "";


                    //string Abi_Estatus = string.Empty;
                    //if (!Convert.IsDBNull(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Abi_Estatus"]))
                    //{
                    //    Abi_Estatus = (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Abi_Estatus"]).ToString();
                    //}
                    ////string Abi_Estatus1 = (item.FindControl("Abi_Estatus") as RadTextBox).Text;
                    //if (string.IsNullOrEmpty(Abi_Estatus) || Abi_Estatus == "C")
                    //{
                        Button = (WebControl)item["Baja"].Controls[0];
                        clickHandler = Button.Attributes["onclick"];
                        Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Pag").ToString());
                    //}
                    Button = (WebControl)item["Imprimir"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Pag").ToString());
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
                ErrorManager();
                Limpiar();
                dt.Rows.Clear();
                if (cmbSolicitud.SelectedValue != "-1")
                {
                    CN_CapAjusteBaseInstalada cn_ajuste = new CN_CapAjusteBaseInstalada();
                    AjusteBaseInstalada cabezera = new AjusteBaseInstalada();
                    cabezera.Id_Emp = session.Id_Emp;
                    cabezera.Id_Cd = session.Id_Cd_Ver;
                    cabezera.Abi_Unique = cmbSolicitud.SelectedValue;
                    bool encontrado = false;
                    cn_ajuste.ConsultarAjusteBaseInstalada_PorUnique(ref cabezera, session.Emp_Cnx, ref encontrado);
                    if (encontrado)
                    {
                        foreach (AjusteBaseInstaladaDet adet in cabezera.ListaAjusteBaseInstalada)
                        {
                            dt.Rows.Add(new object[] { 
                                adet.Abi_Tipo,
                                cmbTipo.FindItemByValue(adet.Abi_Tipo.ToString()).Text.Replace("-","").Trim(),
                                adet.Id_Ter_Origen,
                                adet.Id_Cte_Origen,
                                adet.Id_Prd_Origen,
                                adet.Abi_CantActual_Origen,
                                adet.Abi_CantQuitar_Origen,
                                adet.Id_Ter_Destino,
                                adet.Id_Cte_Destino,
                                adet.Id_Prd_Destino,
                                adet.Abi_CantActual_Destino,
                                adet.Abi_CantQuitar_Destino,
                                adet.Abi_ExplicacionCaso,
                                adet.Abi_Estatus,
                                Estatus(adet.Abi_Estatus),
                            });
                        }

                        txtAutorizacion.Text = cabezera.Abi_FechaAutoriza.HasValue ? cabezera.Abi_FechaAutoriza.Value.ToString("dd/MM/yyyy HH:mm") : "";
                    }
                    else
                        Alerta("No se encontró la solicitud");
                }
                rgAjuste.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ErrorManager();
                if (txtTerritorioOrigen.Text != txtTerritorioDestino.Text || txtClienteOrigen.Text != txtClienteDestino.Text || txtProductoOrigen.Text != txtProductoDestino.Text)
                {
                    if (!string.IsNullOrEmpty(txtCantQuitar.Text))
                    {
                        int cantidad = Convert.ToInt32(txtCantQuitar.Text);
                        if (cantidad > 0)
                        {
                            Agregar();
                        }
                        else
                        {
                            AlertaFocus("La cantidad a quitar no debe ser menor o igual a cero", txtCantQuitar.ClientID);
                        }
                    }
                    else
                    {
                        AlertaFocus("La cantidad a quitar no debe ser nula", txtCantQuitar.ClientID);
                    }
                }
                else
                {
                    Alerta("El origen y el destino no pueden ser el mismo");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtClienteOrigen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (!txtClienteOrigen.Value.HasValue)
                {
                    txtClienteNombreOrigen.Text = "";
                    return;
                }
                Clientes cte = new Clientes();
                cte.Id_Emp = session.Id_Emp;
                cte.Id_Cd = session.Id_Cd_Ver;
                //cte.Id_Cte = Convert.ToInt32(cmbCliente.SelectedValue);
                cte.Id_Cte = Convert.ToInt32(txtClienteOrigen.Value.HasValue ? txtClienteOrigen.Value.Value : -1);
                cte.Id_Terr = txtTerritorioOrigen.Value.HasValue ? Convert.ToInt32(txtTerritorioOrigen.Text) : -1;
                CN_CatCliente catcliente = new CN_CatCliente();
                try
                {
                    catcliente.ConsultaClienteTransf(ref cte, session.Emp_Cnx);
                    txtClienteNombreOrigen.Text = cte.Cte_NomComercial;
                    txtClienteDestino.Focus();
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtClienteOrigen.ClientID);
                    txtClienteOrigen.Text = "";
                    txtClienteNombreOrigen.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void txtClienteDestino_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (!txtClienteDestino.Value.HasValue)
                {
                    txtClienteNombreDestino.Text = "";
                    return;
                }
                Clientes cte = new Clientes();
                cte.Id_Emp = session.Id_Emp;
                cte.Id_Cd = session.Id_Cd_Ver;
                //cte.Id_Cte = Convert.ToInt32(cmbCliente.SelectedValue);
                cte.Id_Cte = Convert.ToInt32(txtClienteDestino.Value.HasValue ? txtClienteDestino.Value.Value : -1);
                cte.Id_Terr = txtTerritorioDestino.Value.HasValue ? Convert.ToInt32(txtTerritorioDestino.Text) : -1;
                CN_CatCliente catcliente = new CN_CatCliente();
                try
                {
                    catcliente.ConsultaClienteTransf(ref cte, session.Emp_Cnx);
                    txtClienteNombreDestino.Text = cte.Cte_NomComercial;
                    txtProductoOrigen.Focus();
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtClienteDestino.ClientID);
                    txtClienteDestino.Text = "";
                    txtClienteNombreDestino.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProductoOrigen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (!txtProductoOrigen.Value.HasValue)
                {
                    txtProductoNombreOrigen.Text = "";
                    return;
                }
                Producto prd = new Producto();
                prd.Id_Emp = session.Id_Emp;
                prd.Id_Cd = session.Id_Cd_Ver;
                prd.Id_Prd = Convert.ToInt32(txtProductoOrigen.Value.HasValue ? txtProductoOrigen.Value.Value : -1);
                CN_CatProducto catproducto = new CN_CatProducto();
                try
                {
                    catproducto.ConsultaProducto(ref prd, session.Emp_Cnx, prd.Id_Emp, prd.Id_Cd, prd.Id_Prd);
                    txtProductoNombreOrigen.Text = prd.Prd_Descripcion;
                    if (prd.Id_Ptp != 1)
                    {
                        AlertaFocus("El producto no es accesorio", txtProductoOrigen.ClientID);
                        txtProductoOrigen.Text = "";
                        txtProductoNombreOrigen.Text = "";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtProductoOrigen.ClientID);
                    txtProductoOrigen.Text = "";
                    txtProductoNombreOrigen.Text = "";
                    return;
                }

                if (txtProductoDestino.Text != "" && txtProductoOrigen.Text != "")
                {
                    txtCantQuitar.Enabled = true;
                    txtCantQuitar.Focus();
                }
                else
                {
                    txtCantQuitar.Enabled = false;
                    txtProductoDestino.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProductoDestino_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (!txtProductoDestino.Value.HasValue)
                {
                    txtProductoNombreDestino.Text = "";
                    return;
                }

                Producto prd = new Producto();
                prd.Id_Emp = session.Id_Emp;
                prd.Id_Cd = session.Id_Cd_Ver;
                //cte.Id_Cte = Convert.ToInt32(cmbCliente.SelectedValue);
                prd.Id_Prd = Convert.ToInt32(txtProductoDestino.Value.HasValue ? txtProductoDestino.Value.Value : -1);
                CN_CatProducto catproducto = new CN_CatProducto();
                try
                {
                    catproducto.ConsultaProducto(ref prd, session.Emp_Cnx, prd.Id_Emp, prd.Id_Cd, prd.Id_Prd);
                    if (prd.Id_Ptp != 1)
                    {
                        AlertaFocus("El producto no es accesorio", txtProductoDestino.ClientID);
                        txtProductoDestino.Text = "";
                        txtProductoNombreDestino.Text = "";
                        return;
                    }
                    txtProductoNombreDestino.Text = prd.Prd_Descripcion;

                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtProductoDestino.ClientID);
                    txtProductoDestino.Text = "";
                    txtProductoNombreDestino.Text = "";
                    return;
                }

                if (txtProductoDestino.Text != "" && txtProductoOrigen.Text != "")
                {
                    txtCantQuitar.Enabled = true;
                    txtCantQuitar.Focus();
                }
                else
                {
                    txtCantQuitar.Enabled = false;
                    txtExplicacion.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion

        #region Funciones
        private void CargarCentros()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                if (session.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, session.Id_Emp, session.Id_U, session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(session.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, session.Id_Emp, session.Id_U, session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = session.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            try
            {
                CargarTipo();
                CargarSolicitudes();
                CargarTerritorios();
                CargarClientes();
                CargarProductos();
                GetList();
                rgAjuste.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipo()
        {
            try
            {
                cmbTipo.Items.Clear();
                cmbTipo.Items.Add(new RadComboBoxItem("-- Todos --", "3"));
                /*cmbTipo.Items.Add(new RadComboBoxItem("Facturado", "1"));
                cmbTipo.Items.Add(new RadComboBoxItem("Comodato", "2"));*/

                cmbTipo.Sort = RadComboBoxSort.Ascending;
                cmbTipo.SortItems();
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
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, session.Emp_Cnx, "spCapAjusteBi_Combo", ref cmbSolicitud, true);
                cmbSolicitud.Items.Insert(0, new RadComboBoxItem("-- Nueva --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorios()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, session.Emp_Cnx, "spCatTerritorio_ComboTodos_Activos_Inactivos", ref cmbTerritorioOrigen);
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, session.Emp_Cnx, "spCatTerritorio_Combo", ref cmbTerritorioDestino);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarClientes()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                //CN_Comun.LlenaCombo(1, session.Id_Emp, session.Id_Cd_Ver, session.Emp_Cnx, "spCatCliente_Combo", ref cmbClienteOrigen);
                //CN_Comun.LlenaCombo(1, session.Id_Emp, session.Id_Cd_Ver, session.Emp_Cnx, "spCatCliente_Combo", ref cmbClienteDestino);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProductos()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                //CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, session.Emp_Cnx, "spCatProductoBi_Combo", ref cmbProductoOrigen);
                //CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, session.Emp_Cnx, "spCatProductoBi_Combo", ref cmbProductoDestino);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Eliminar(GridItem gi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Id_Ter_Origen " + (gi.Cells[6].Text.Replace("&nbsp;", "") != "" ? "='" + gi.Cells[6].Text + "'" : "is null") + " and ");
                sb.Append("Id_Cte_Origen " + (gi.Cells[7].Text.Replace("&nbsp;", "") != "" ? "='" + gi.Cells[7].Text + "'" : "is null") + " and ");
                sb.Append("Id_Prd_Origen " + (gi.Cells[8].Text.Replace("&nbsp;", "") != "" ? "='" + gi.Cells[8].Text + "'" : "is null") + " and ");
                sb.Append("Id_Ter_Destino " + (gi.Cells[11].Text.Replace("&nbsp;", "") != "" ? "='" + gi.Cells[11].Text + "'" : "is null") + " and ");
                sb.Append("Id_Cte_Destino " + (gi.Cells[12].Text.Replace("&nbsp;", "") != "" ? "='" + gi.Cells[12].Text + "'" : "is null") + " and ");
                sb.Append("Id_Prd_Destino " + (gi.Cells[13].Text.Replace("&nbsp;", "") != "" ? "='" + gi.Cells[13].Text + "'" : "is null"));
                DataRow[] dr = dt.Select(sb.ToString());

                if (dr.Length > 0)
                {
                    dr[0].Delete();
                    dt.AcceptChanges();
                }
                else
                    Alerta("No se encontró el registro");
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        private void GetList()
        {
            try
            {
                dt = new DataTable();
                dt.Columns.Add("Abi_Tipo");
                dt.Columns.Add("Abi_TipoStr");
                dt.Columns.Add("Id_Ter_Origen");
                dt.Columns.Add("Id_Cte_Origen");
                dt.Columns.Add("Id_Prd_Origen");
                dt.Columns.Add("Abi_CantActual_Origen");
                dt.Columns.Add("Abi_CantQuitar_Origen");
                dt.Columns.Add("Id_Ter_Destino");
                dt.Columns.Add("Id_Cte_Destino");
                dt.Columns.Add("Id_Prd_Destino");
                dt.Columns.Add("Abi_CantActual_Destino");
                dt.Columns.Add("Abi_CantQuitar_Destino");
                dt.Columns.Add("Abi_ExplicacionCaso");
                dt.Columns.Add("Abi_Estatus");
                dt.Columns.Add("Abi_EstatusStr");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Limpiar()
        {
            try
            {
                cmbTipo.SelectedIndex = 0;
                cmbTipo.Text = cmbTipo.Items[0].Text;
                txtTerritorioOrigen.Value = null;
                cmbTerritorioOrigen.SelectedIndex = 0;
                cmbTerritorioOrigen.Text = cmbTerritorioOrigen.Items[0].Text;
                txtTerritorioDestino.Value = null;
                cmbTerritorioDestino.SelectedIndex = 0;
                cmbTerritorioDestino.Text = cmbTerritorioDestino.Items[0].Text;
                txtClienteOrigen.Value = null;
                txtClienteNombreOrigen.Text = "";
                txtClienteDestino.Value = null;
                txtClienteNombreDestino.Text = "";
                txtProductoOrigen.Value = null;
                txtProductoNombreOrigen.Text = "";
                txtProductoDestino.Value = null;
                txtProductoNombreDestino.Text = "";
                txtCantActualOrigen.Value = null;
                txtCantActualDestino.Value = null;
                txtCantQuitar.Value = null;
                txtCantModificada.Value = null;
                txtExplicacion.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Agregar()
        {
            try
            {
                bool TerritoriosDiferentes = txtTerritorioOrigen.Value != txtTerritorioDestino.Value;
                bool ClientesDiferentes = txtClienteOrigen.Value != txtClienteDestino.Value;
                bool ProductosDiferentes = txtProductoOrigen.Value != txtProductoDestino.Value;

                bool TerritoriosVacios = !txtTerritorioOrigen.Value.HasValue && !txtTerritorioDestino.Value.HasValue;
                bool ClienteVacios = !txtClienteOrigen.Value.HasValue && !txtClienteDestino.Value.HasValue;
                bool ProductosVacios = !txtProductoOrigen.Value.HasValue && !txtProductoDestino.Value.HasValue;

                if (txtTerritorioOrigen.Value.HasValue)
                {
                    if (!txtTerritorioDestino.Value.HasValue)
                    {
                        AlertaFocus("Favor de capturar el territorio destino", txtTerritorioDestino.ClientID);
                        return;
                    }
                }
                if (txtTerritorioDestino.Value.HasValue)
                {
                    if (!txtTerritorioOrigen.Value.HasValue)
                    {
                        AlertaFocus("Favor de capturar el territorio origen", txtTerritorioOrigen.ClientID);
                        return;
                    }
                }

                if (txtClienteOrigen.Value.HasValue)
                {
                    if (!txtClienteDestino.Value.HasValue)
                    {
                        AlertaFocus("Favor de capturar el cliente destino", txtClienteDestino.ClientID);
                        return;
                    }
                }
                if (txtClienteDestino.Value.HasValue)
                {
                    if (!txtClienteOrigen.Value.HasValue)
                    {
                        AlertaFocus("Favor de capturar el cliente origen", txtClienteOrigen.ClientID);
                        return;
                    }
                }

                if (txtProductoOrigen.Value.HasValue)
                {
                    if (!txtProductoDestino.Value.HasValue)
                    {
                        AlertaFocus("Favor de capturar el producto destino", txtProductoDestino.ClientID);
                        return;
                    }
                }
                if (txtProductoDestino.Value.HasValue)
                {
                    if (!txtProductoOrigen.Value.HasValue)
                    {
                        AlertaFocus("Favor de capturar el producto origen", txtProductoOrigen.ClientID);
                        return;
                    }
                }
                if (txtTerritorioDestino.Value.HasValue)
                {
                    CN_CatTerritorios catterritorio = new CN_CatTerritorios();
                    Territorios terr = new Territorios();
                    terr.Id_Emp = session.Id_Emp;
                    terr.Id_Cd = session.Id_Cd_Ver;
                    terr.Id_Ter = (int)txtTerritorioDestino.Value.Value;
                    catterritorio.ConsultaTerritorios(ref terr, session.Emp_Cnx);
                    if (terr.Rik_Nombre == null)
                    {
                        Alerta("El territorio destino no está asignado a algún representante");
                        return;
                    }
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("Id_Ter_Origen " + (txtTerritorioOrigen.Value.HasValue ? "='" + txtTerritorioOrigen.Value + "'" : "is null") + " and ");
                sb.Append("Id_Cte_Origen " + (txtClienteOrigen.Value.HasValue ? "='" + txtClienteOrigen.Value + "'" : "is null") + " and ");
                sb.Append("Id_Prd_Origen " + (txtProductoOrigen.Value.HasValue ? "='" + txtProductoOrigen.Value + "'" : "is null") + " and ");
                sb.Append("Id_Ter_Destino " + (txtTerritorioDestino.Value.HasValue ? "='" + txtTerritorioDestino.Value + "'" : "is null") + " and ");
                sb.Append("Id_Cte_Destino " + (txtClienteDestino.Value.HasValue ? "='" + txtClienteDestino.Value + "'" : "is null") + " and ");
                sb.Append("Id_Prd_Destino " + (txtProductoDestino.Value.HasValue ? "='" + txtProductoDestino.Value + "'" : "is null"));
                DataRow[] dr = dt.Select(sb.ToString());

                if (dr.Length > 0)
                {
                    Alerta("Movimiento ya existente en la solicitud");
                    return;
                }

                dt.Rows.Add(new object[] {
                        this.cmbTipo.SelectedValue, 
                        this.cmbTipo.SelectedItem.Text.Replace(" ","").Replace("-",""),
                        this.txtTerritorioOrigen.Value, 
                        this.txtClienteOrigen.Value,
                        this.txtProductoOrigen.Value,
                        this.txtCantActualOrigen.Value,
                        this.txtCantQuitar.Value,
                        this.txtTerritorioDestino.Value,
                        this.txtClienteDestino.Value,
                        this.txtProductoDestino.Value,
                        this.txtCantActualDestino.Value,
                        this.txtCantModificada.Value,
                        this.txtExplicacion.Text,
                        "C",
                        Estatus("C")
                    });
                rgAjuste.Rebind();
                Limpiar();               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private object Estatus(string p)
        {
            try
            {
                switch (p.ToUpper())
                {
                    case "C": return "Capturado";
                    case "A": return "Autorizado";
                    default: return "";
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
                if (dt.Rows.Count == 0)
                {
                    Alerta("La solicitud no tiene partidas agregadas");
                    return;
                }
                string verificador = "";
                Funciones funcion = new Funciones();
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permiso para guardar");
                    return;
                }
                AjusteBaseInstalada cabezera = new AjusteBaseInstalada();
                cabezera.Id_Emp = session.Id_Emp;
                cabezera.Id_Cd = session.Id_Cd_Ver;
                cabezera.Id_U = session.Id_U;
                cabezera.Abi_Fecha = funcion.GetLocalDateTime(session.Minutos);
                cabezera.Abi_Unique = cmbSolicitud.SelectedValue;
                CN_CapAjusteBaseInstalada cn_ajuste = new CN_CapAjusteBaseInstalada();
                cn_ajuste.Insertar(cabezera, dt, session.Emp_Cnx, ref verificador);
                if (verificador != "")
                {
                    Alerta("Solicitud de autorización de ajuste de base instalada creada exitosamente");
                    EnviaEmail(verificador);
                    GetList();
                    rgAjuste.Rebind();
                    CargarSolicitudes();
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

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.rtb1.Items[5].Visible = false;
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
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviaEmail(string solicitud)
        {
            try
            {
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                if (configuracion.Mail_BaseInstalada.Length == 0)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }

                CN_CapAjusteBaseInstalada cn_ajuste = new CN_CapAjusteBaseInstalada();
                AjusteBaseInstalada cabezera = new AjusteBaseInstalada();
                cabezera.Id_Emp = session.Id_Emp;
                cabezera.Id_Cd = session.Id_Cd_Ver;
                cabezera.Abi_Unique = solicitud;
                bool encontrado = false;
                cn_ajuste.ConsultarAjusteBaseInstalada_PorUnique(ref cabezera, session.Emp_Cnx, ref encontrado);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table style='font-family: verdana; font-size:9pt'><tr><td>");
                cuerpo_correo.Append("<img src=\"cid:companylogo\"></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de ajuste de base instalada con el número de folio <b>" + cabezera.Id_Abi.ToString() + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("Centro de distribución: <b>" + session.Id_Cd_Ver + " - " + session.Cd_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("Solicitó: <b>" + session.Id_U + " - " + session.U_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'><br>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br><a href='" + Request.Url.ToString().Replace((new FileInfo(Request.Url.AbsolutePath)).Name, "") + "CapAjusteBi_Autorizacion.aspx?id1=" + session.Id_Emp + "&Id2=" + session.Id_Cd_Ver + "&Id3=" + solicitud + "'>Solicitud de autorización de ajuste de base instalada</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                string[] To = configuracion.Mail_BaseInstalada.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                for (int x = 0; x < To.Length; x++)
                    m.To.Add(new MailAddress(To[x]));

                m.Subject = "Solicitud de autorización de ajuste de base instalada";
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
            catch (Exception ex)
            {
                throw ex;
                //Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
            }
        }
        #endregion

        #region ErrorManager
        //private void RadConfirm(string mensaje)
        //{
        //    try
        //    {

        //        RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, "Alerta");
        //    }

        //}
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
    }
}
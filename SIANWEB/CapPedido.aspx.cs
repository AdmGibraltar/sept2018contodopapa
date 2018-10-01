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
using System.Collections;
using CapaDatos; 

namespace SIANWEB
{
    public partial class CapPedido : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public double Iva_cd
        {
            get
            {
                object _iva_cd = Session["Iva_cd" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
                return _iva_cd == null ? 0 : (double)_iva_cd;
            }
            set
            {
                Session["Iva_cd" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool terr = false;
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
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
                Sesion.URL = HttpContext.Current.Request.Url.Host;
                if (Sesion == null)
                    CerrarVentana();
                else
                {
                    if (!Page.IsPostBack)
                    {
                        Sesion.HoraInicio = DateTime.Now;
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();                        

                        if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(divGenerales.Controls);
                            deshabilitarcontroles(formularioTotales.Controls);
                            txtComentarios.Enabled = false;
                            GridCommandItem cmdItem = (GridCommandItem)rgDetalles.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 
                            ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  

                            rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 1].Display = false;
                            rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 2].Display = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgDetalles.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
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
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                    {
                        Guardar();
                        HiddenRebind.Value = "1";
                    }
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    CerrarVentana();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
        //protected void cmbCliente_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cmbCliente.SelectedIndex != 0)
        //            CargarCliente();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        protected void cmbTerritorio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                CargarRik();
                //txtRepresentanteID.Focus();
                rgDetalles.Rebind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void cmbTerritorio_DataBinding(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (terr)
                    return;
                terr = true;
                RadComboBox comboBox = ((RadComboBox)sender);
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtNumCliente.Value.HasValue ? txtNumCliente.Value.Value : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, Sesion.Emp_Cnx, "spCatTerCte_Combo", ref comboBox);
                //CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtNumCliente.Value.HasValue ? txtNumCliente.Value.Value : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, Sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmbTerritorio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbTerritorio_DataBound(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadComboBox comboBox = ((RadComboBox)sender);
                string id = ((Label)comboBox.Parent.Parent.FindControl("lblTer2")).Text;
                if (!string.IsNullOrEmpty(id))
                    comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void cmbTerritorioDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //((sender as RadComboBox).Parent.Parent.FindControl("cmbDescr") as RadComboBox).DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }       
        protected void rdFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if (dpFecha2.SelectedDate < rdFecha.SelectedDate)
                    dpFecha2.SelectedDate = rdFecha.SelectedDate;

                DateTime fecha = rdFecha.SelectedDate.HasValue ? rdFecha.SelectedDate.Value : DateTime.Now;
                fecha = fecha.AddDays(1);
                dpFecha2.SelectedDate = fecha;
                txtNumCliente.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        //COMBO PRODUCTO
        //protected void cmbProducto_DataBinding(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        RadComboBox comboBox = ((RadComboBox)sender);
        //        if (prod)
        //            return;
        //        prod = true;
        //        Sesion Sesion = new Sesion();
        //        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref comboBox);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //protected void cmbProducto_DataBound(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        RadComboBox comboBox = ((RadComboBox)sender);
        //        string id = ((RadNumericTextBox)comboBox.Parent.Parent.FindControl("txtProd")).Text;
        //        if (id != "")
        //            comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox rdBox = (sender as RadNumericTextBox);
                if (rdBox.Value.HasValue)
                {
                    string Id_Ter = ((RadComboBox)rdBox.Parent.Parent.FindControl("cmbTer")).SelectedValue;
                    string Id_PrdEdit = ((Label)rdBox.Parent.Parent.FindControl("lblProdEdit")).Text;

                    DataRow[] Ar_dr = dt.Select("Id_Ter='" + Id_Ter + "' and Id_prd='" + rdBox.Value + "'");
                    if (Ar_dr.Length > 0 && Id_PrdEdit != rdBox.Value.ToString())
                    {
                        Alerta("Producto ya capturado");
                        rdBox.Text = "";
                        ((RadTextBox)rdBox.Parent.Parent.FindControl("txtProductoNombre")).Text = "";
                        ((Label)rdBox.Parent.Parent.FindControl("lblPres2")).Text = "";
                        ((Label)rdBox.Parent.Parent.FindControl("lblUnidad2")).Text = "";
                        ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtPrecio")).Text = "";
                        ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtCantidad")).Text = "";
                        ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtImporte")).Text = "";
                    }
                    else
                    {
                        Producto prd = new Producto();
                        prd.Id_Emp = Sesion.Id_Emp;
                        prd.Id_Cd = Sesion.Id_Cd_Ver;
                        prd.Id_Prd = Convert.ToInt32(rdBox.Value);
                        prd.Fecha = rdFecha.SelectedDate.Value;
                        prd.Id_Ter = Convert.ToInt32(Id_Ter);
                        prd.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                        CN_CatProducto catproducto = new CN_CatProducto();
                        try
                        {
                            prd.EmpBen = strEmp == "" ? (int?)null : Convert.ToInt32(strEmp);
                            catproducto.ConsultaProducto(ref prd, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, prd.Id_Prd);
                        }
                        catch (Exception ex)
                        {
                            AlertaFocus(ex.Message, rdBox.ClientID);
                            rdBox.Text = "";
                            ((RadTextBox)rdBox.Parent.Parent.FindControl("txtProductoNombre")).Text = "";
                            ((Label)rdBox.Parent.Parent.FindControl("lblPres2")).Text = "";
                            ((Label)rdBox.Parent.Parent.FindControl("lblUnidad2")).Text = "";
                            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtPrecio")).Text = "";
                            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtCantidad")).Text = "";
                            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtImporte")).Text = "";
                            return;
                        }
                        ((Label)rdBox.Parent.Parent.FindControl("lblPres2")).Text = prd.Prd_Presentacion;
                        ((Label)rdBox.Parent.Parent.FindControl("lblUnidad2")).Text = prd.Prd_UniNs;
                        ((RadTextBox)rdBox.Parent.Parent.FindControl("txtProductoNombre")).Text = prd.Prd_Descripcion;

                        Producto prd2 = new Producto();
                        prd2.Id_Emp = Sesion.Id_Emp;
                        prd2.Id_Cd = Sesion.Id_Cd;
                        prd2.Id_Prd = prd.Id_Prd;
                        prd2.Fecha = rdFecha.SelectedDate.Value;
                        int cliente = !string.IsNullOrEmpty(txtNumCliente.Text) ? Convert.ToInt32(txtNumCliente.Text) : 0;
                        catproducto.ConsultaProductoCte(ref prd2, Sesion.Emp_Cnx, cliente);
                       
                        if (prd2.Prd_FechaFinEsp != Convert.ToDateTime("01/01/2000"))
                        {
                            if (prd2.Prd_FechaFinEsp.Subtract(rdFecha.SelectedDate.Value).Days >= 0)
                            {
                                Alerta("Faltan solo <b>" + prd2.Prd_FechaFinEsp.Subtract(rdFecha.SelectedDate.Value).Days + "</b> día(s) para que venza el precio especial de la solicitud " + prd2.Sol_PEsp + " de precios especiales.</br>Productos sin actualizar el <b>Precio AAA Especial</b>, impacta directamente en los cálculos de utilidad del CDI y por ende, en los sistemas de compensación de todo el personal");
                            }
                            else
                            {
                                Alerta("La solicitud de precios especiales <b>" + prd2.Sol_PEsp + "</b> tiene producto(s) con <b>" + rdFecha.SelectedDate.Value.Subtract(prd2.Prd_FechaFinEsp).Days + "</b> días de vencido(s).</br>Productos sin actualizar el <b>Precio AAA Especial</b>, impacta directamente en los cálculos de utilidad del CDI y por ende, en los sistemas de compensación de todo el personal");
                            }
                        }

                        if (prd2.Prd_Precio != "0" && !string.IsNullOrEmpty(prd2.Prd_Precio))
                        {
                            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtPrecio")).Text = prd2.Prd_Precio.ToString();
                            (rdBox.Parent.Parent.FindControl("txtCantidad") as RadNumericTextBox).Focus();
                        }
                        else
                        {                          
                            int VGEmpresa = 0;
                            Int32.TryParse(strEmp, out VGEmpresa);

                            if (VGEmpresa == Sesion.Id_Emp)
                            {//Si la empresa es Bennetts
                                Alerta("Por favor ingrese un precio público a este producto, antes de agregarlo al pedido");
                                rdBox.Text = "";
                                ((RadTextBox)rdBox.Parent.Parent.FindControl("txtProductoNombre")).Text = "";
                                ((Label)rdBox.Parent.Parent.FindControl("lblPres2")).Text = "";
                                ((Label)rdBox.Parent.Parent.FindControl("lblUnidad2")).Text = "";
                                ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtPrecio")).Text = "";
                                ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtCantidad")).Text = "";
                                ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtImporte")).Text = "";
                            }
                            else
                            {
                                Alerta("No se ha establecido un precio público a este producto");
                            }
                        }                        
                    }
                }
                else
                    LimpiarRegistro(rdBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cantidad = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Text;
                string precio = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Text;
                int Prd_Cantidad = 0;
                double Prd_Precio = 0;

                if (!string.IsNullOrEmpty(cantidad))
                    Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Text);
                if (!string.IsNullOrEmpty(precio))
                    Prd_Precio = Convert.ToDouble(((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Text);

                string Id_prd = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtProd")).Text;

                ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).DbValue = Prd_Cantidad * Prd_Precio;

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<int> Actuales = new List<int>();
                CN_CatProducto catproducto = new CN_CatProducto();
                catproducto.ConsultaProducto_Disponible(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Id_prd, ref Actuales, Sesion.Emp_Cnx);

                if (Actuales.Count > 0)
                {
                    if (Prd_Cantidad > Actuales[2])
                    {
                        AlertaFocus("Inventario disponible insuficiente, <br>Inventario final: " + Actuales[0] + " <br>Asignado: " + Actuales[1] + " <br>Disponible: " + Actuales[2], ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).ClientID);
                    }
                }
                ((ImageButton)(sender as RadNumericTextBox).Parent.FindControl("PerformInsertButton")).Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        //GRID
        protected void rgDetalles_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    rgDetalles.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void rgDetalles_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void rgDetalles_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        //if (cmbCliente.SelectedIndex == 0)
                        if (!txtNumCliente.Value.HasValue)
                        {
                            Alerta("No se ha seleccionado un cliente");
                            e.Canceled = true;
                        }
                        if (rgDetalles.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        break;
                    case "Update":
                        Update(e);
                        break;
                    case "Delete":
                        Delete(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtNumCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarCliente();
                GetListDet();
                rgDetalles.Rebind();
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("popup();");
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
                switch (e.Argument.ToString())
                {
                    case "cliente":
                        txtNumCliente.DbValue = Convert.ToInt32(Session["Id_Buscar" + Session.SessionID]);
                        txtNumCliente_TextChanged(null, null);
                        break;
                    case "precio":
                        (producto as RadNumericTextBox).DbValue = Session["Id_Buscar" + Session.SessionID];
                        cmbProductoDet_TextChanged(producto, null);
                        if ((producto as RadNumericTextBox).Value.HasValue)
                        {
                            ((producto as RadNumericTextBox).Parent.FindControl("txtPrecio") as RadNumericTextBox).Focus();
                        }
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 190);
                        Unit altura2 = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 250);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura2;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        if ((bool)Session["bool" + Session.SessionID])
                        {
                            txtNumCliente.Focus();
                            Session["bool" + Session.SessionID] = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
        }
        protected void rgDetalles_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {              
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_Ter"].FindControl("txtTerritorioPartida");
                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form["Prd_Cantidad"].FindControl("txtCantidad");
                    }
                    dataField.Focus();

                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    int VGEmpresa = 0;
                    Int32.TryParse(strEmp, out VGEmpresa);

                    if (VGEmpresa == Sesion.Id_Emp)
                    {//si es Bennetts
                        ((RadNumericTextBox)form.FindControl("txtPrecio")).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        #endregion
        #region Funciones
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = false;
                        break;
                }
                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }
        }
        private void CargarCliente()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            bool cancelar = false;
            Clientes cte = new Clientes();
            cte.Id_Emp = Sesion.Id_Emp;
            cte.Id_Cd = Sesion.Id_Cd_Ver;
            cte.Id_Cte = Convert.ToInt32(txtNumCliente.Value.HasValue ? txtNumCliente.Value.Value : -1);
            cte.Id_Rik = Sesion.Id_Rik;
            CN_CatCliente catcliente = new CN_CatCliente();
            try
            {
                catcliente.ConsultaClientes(ref cte, Sesion.Emp_Cnx);
                if (!cte.Cte_Facturacion)
                {
                    AlertaFocus("CUIDADO: Este cliente se encuentra bloqueado por parte de cobranza; favor de aclarar su situación de créditos", txtNumCliente.ClientID);
                    cancelar = true;
                }
            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, txtNumCliente.ClientID);
                cancelar = true;
            }

            if (cancelar)
            {
                txtCliente.Text = "";
                txtNumCliente.Text = "";
                txtTerritorio.Text = "";
                txtRepresentanteID.Text = "";
                cmbTerritorio.Items.Clear();
                cmbTerritorio.Text = "";
                cmbRik.Items.Clear();
                cmbRik.Text = "";
            }
            cmbTerritorio.Items.Clear();
            cmbRik.Items.Clear();
            CargarTerritorios();
            txtCliente.Text = cte.Cte_NomComercial;
            txtSolicito.Text = cte.Cte_Contacto;
            txtCondiciones.Text = cte.Cte_CondPago.ToString();
        }        
        private void CargarTerritorios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtNumCliente.Value.HasValue ? txtNumCliente.Value.Value : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, Sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmbTerritorio);
                if (cmbTerritorio.Items.Count > 1)
                {
                    cmbTerritorio.SelectedIndex = 1;
                    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    txtTerritorio.Text = cmbTerritorio.Items[1].Value;
                    CargarRik();
                }
                else
                {
                    if (Request.QueryString["id"] != "-1")
                    {
                        CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtNumCliente.Value.HasValue ? txtNumCliente.Value.Value : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, 0, Sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmbTerritorio);
                        if (cmbTerritorio.Items.Count > 1)
                        {
                            cmbTerritorio.SelectedIndex = 1;
                            cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                            txtTerritorio.Text = cmbTerritorio.Items[1].Value;
                            CargarRik();
                        }
                    }
                    else
                    {
                        txtTerritorio.Text = "";
                        txtRepresentanteID.Text = "";
                        cmbTerritorio.Items.Clear();
                        cmbTerritorio.Text = "";
                        cmbRik.Items.Clear();
                        cmbRik.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRik() //Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int territorio = !string.IsNullOrEmpty(cmbTerritorio.SelectedValue) ? Convert.ToInt32(cmbTerritorio.SelectedValue) : 0;
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, territorio, 1, Sesion.Emp_Cnx, "spCatRikTerr_Combo", ref cmbRik);
                if (cmbRik.Items.Count > 1)
                {
                    cmbRik.SelectedIndex = 1;
                    cmbRik.Text = cmbRik.Items[1].Text;
                    txtRepresentanteID.Text = cmbRik.Items[1].Value;
                    if (_PermisoGuardar)
                        this.rtb1.Items[6].Visible = true;
                    if (_PermisoGuardar & _PermisoModificar)
                        this.rtb1.Items[5].Visible = true;
                }
                else
                {
                    cmbRik.SelectedIndex = -1;
                    cmbRik.Text = "";
                    txtRepresentanteID.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete(GridCommandEventArgs e)
        {
            try
            {
                int Id_PedDet = 0;
                DataRow[] Ar_dr;
                GridItem gi = e.Item;
                Id_PedDet = Convert.ToInt32(((Label)gi.FindControl("lblDet")).Text);
                Ar_dr = dt.Select("Id_PedDet='" + Id_PedDet + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    dt.AcceptChanges();
                }
                CalcularTotales();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void GetListDet()
        {
            try
            {
                dt = new DataTable();
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Id_PedDet", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Ter", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Ter_Nombre", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Unidad", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Importe", System.Type.GetType("System.Double"));
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
                    Alerta("Por favor capture el detalle");
                    return;
                }
                CalcularTotales();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;

                if (string.IsNullOrEmpty(txtNumCliente.Text))
                {
                    Alerta("Por favor capture un cliente");
                    return;
                }
                if (string.IsNullOrEmpty(txtTerritorio.Text))
                {
                    Alerta("Por favor capture un territorio");
                    return;
                }
                if (string.IsNullOrEmpty(txtRepresentanteID.Text))
                {
                    Alerta("Por favor capture un representante");
                    return;
                }

                Funciones funciones = new Funciones();

                pedido.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                //pedido.Ped_Fecha = rdFecha.SelectedDate.Value;
                pedido.Ped_Fecha = funciones.GetLocalDateTime(session.Minutos);
                pedido.Id_Rik = Convert.ToInt32(txtRepresentanteID.Text);
                pedido.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                pedido.Pedido_del = txtPedidodel.Text;
                pedido.Requisicion = txtRequisicion.Text;
                pedido.Ped_Solicito = txtSolicito.Text;
                pedido.Ped_Flete = txtFlete.Text;
                pedido.Ped_OrdenEntrega = txtOrden.Text;
                pedido.Ped_CondEntrega = Convert.ToInt32(txtCondiciones.Value.HasValue ? txtCondiciones.Value.Value : 0);
                pedido.Ped_FechaEntrega = dpFecha2.SelectedDate.HasValue ? dpFecha2.SelectedDate.Value : rdFecha.SelectedDate.Value.AddDays(1);
                pedido.Ped_Observaciones = txtObservaciones.Text;
                pedido.Ped_DescPorcen1 = Convert.ToDouble(txtDescuento.Text);
                pedido.Ped_DescPorcen2 = Convert.ToDouble(txtDescuento2.Text);
                pedido.Ped_Desc1 = txtConcepto.Text;
                pedido.Ped_Desc2 = txtConcepto2.Text;
                pedido.Ped_Importe = Convert.ToDouble(txtImporte.Text);
                pedido.Ped_Subtotal = Convert.ToDouble(txtSub.Text);
                pedido.Ped_Iva = Convert.ToDouble(txtIVA.Text);
                pedido.Ped_Total = Convert.ToDouble(txtTotal.Text);
                pedido.Ped_Comentarios = txtComentarios.Text;
                pedido.Id_U = session.Id_U;
                CN_CapPedido clsCapPedido = new CN_CapPedido();
                int verificador = -1;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    pedido.Estatus = "C";
                    pedido.Ped_Tipo = 1;
                    clsCapPedido.InsertarPedido(pedido, dt, session.Emp_Cnx, ref verificador);
                    if (verificador >= 1)
                    {
                        new CN_Rendimientos().InsertarRendimientosPedidos(session, session.Emp_Cnx, Session.SessionID, ref pedido, ref verificador);
                        RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");
                    }
                    else
                        Alerta("Ocurrió un error al intentar guardar el pedido");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    pedido.Id_Ped = Convert.ToInt32(txtClave.Text);
                    clsCapPedido.ModificarPedido(pedido, dt, session.Emp_Cnx, ref verificador);
                    if (verificador >= 1)
                    {
                        new CN_Rendimientos().InsertarRendimientosPedidos(session, session.Emp_Cnx, Session.SessionID, ref pedido, ref verificador);
                        RAM1.ResponseScripts.Add("CloseAlert('Los datos se modificaron correctamente');");
                    }
                    else
                        Alerta("Ocurrió un error al intentar modificar los cambios");
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                rdFecha.SelectedDate = DateTime.Now;

                if (Sesion.Id_Rik <= 0)
                {
                    rdFecha.Enabled = false;
                    txtNumCliente.Focus();
                }
                else
                {
                    rdFecha.Enabled = true;
                    rdFecha.Focus();
                }

                dpFecha2.SelectedDate = DateTime.Now.AddDays(1);                
                _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
                _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
                _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
                _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);

                CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
                CentroDistribucion centroDistribucion = new CentroDistribucion();
                double icd = Iva_cd;
                cd.ConsultarIva(Sesion.Id_Emp, Sesion.Id_Cd_Ver, ref icd, Sesion.Emp_Cnx);
                Iva_cd = icd;

                if (Request.QueryString["id"] != "-1")
                {
                    txtClave.Text = Request.QueryString["id"];
                    cargarPedido();
                }
                else
                {
                    txtClave.Text = MaximoId();
                    GetListDet();
                    rgDetalles.Rebind();
                }
                ValidarPermisos();
                
                Session["bool" + Session.SessionID] = true;

                //if (Sesion.Id_TU == 2)
                //{
                    rdFecha.Enabled = false;
                    txtNumCliente.Focus();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarPedido()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CapPedido cappedido = new CN_CapPedido();
                Pedido pedido = new Pedido();
                pedido.Id_Emp = Sesion.Id_Emp;
                pedido.Id_Cd = Sesion.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtClave.Text);

                cappedido.ConsultaPedido(ref pedido, Sesion.Emp_Cnx);

                txtComentarios.Text = pedido.Ped_Comentarios;
                txtConcepto.Text = pedido.Ped_Desc1;
                txtConcepto2.Text = pedido.Ped_Desc2;
                txtCondiciones.Text = pedido.Ped_CondEntrega.ToString();
                txtDescuento.Text = pedido.Ped_DescPorcen1.ToString("#,##0.00");
                txtDescuento2.Text = pedido.Ped_DescPorcen2.ToString("#,##0.00");
                txtFlete.Text = pedido.Ped_Flete;
                txtImporte.Text = pedido.Ped_Importe.ToString("#,##0.00");
                txtIVA.Text = pedido.Ped_Iva.ToString("#,##0.00");
                txtNumCliente.Text = pedido.Id_Cte.ToString();
                txtObservaciones.Text = pedido.Ped_Observaciones;
                txtOrden.Text = pedido.Ped_OrdenEntrega;
                txtPedidodel.Text = pedido.Pedido_del;
                txtTerritorio.Text = pedido.Id_Ter.ToString();
                txtRepresentanteID.Text = pedido.Id_Rik.ToString();
                txtSolicito.Text = pedido.Ped_Solicito;
                //CargarCliente();

                //-----------------------
                bool cancelar = false;
                Clientes cte = new Clientes();
                cte.Id_Emp = Sesion.Id_Emp;
                cte.Id_Cd = Sesion.Id_Cd_Ver;
                cte.Id_Cte = Convert.ToInt32(txtNumCliente.Value.HasValue ? txtNumCliente.Value.Value : -1);
                cte.Id_Rik = Sesion.Id_Rik;
                CN_CatCliente catcliente = new CN_CatCliente();
                try
                {
                    catcliente.ConsultaClientes(ref cte, Sesion.Emp_Cnx);
                    if (!cte.Cte_Facturacion)
                    {
                        AlertaFocus("CUIDADO: Este cliente se encuentra bloqueado por parte de cobranza; favor de aclarar su situación de créditos", txtNumCliente.ClientID);
                        cancelar = true;
                        this.rtb1.Items[6].Visible = false;
                        this.rtb1.Items[5].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtNumCliente.ClientID);
                    cancelar = true;
                }

                if (cancelar)
                {
                    if (Request.QueryString["id"] != "-1")
                    {
                        cte.Ignora_Inactivo = true;
                        catcliente.ConsultaClientes(ref cte, Sesion.Emp_Cnx);
                        this.rtb1.Items[6].Visible = false;
                        this.rtb1.Items[5].Visible = false;
                    }
                }
                cmbTerritorio.Items.Clear();
                cmbRik.Items.Clear();
                CargarTerritorios();
                txtCliente.Text = cte.Cte_NomComercial;

                txtTerritorio.Text = pedido.Id_Ter.ToString();
                int campo = cmbTerritorio.FindItemIndexByValue(pedido.Id_Ter.ToString());
                if (campo > 0)
                {
                    cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(pedido.Id_Ter.ToString());
                    cmbTerritorio.Text = cmbTerritorio.Items[campo].Text;
                    if (!cancelar)
                    {
                        if (_PermisoGuardar)
                            this.rtb1.Items[6].Visible = true;
                        if (_PermisoGuardar & _PermisoModificar)
                            this.rtb1.Items[5].Visible = true;
                    }
                }
                else
                {
                    Territorios ter = new Territorios();
                    ter.Id_Emp = Sesion.Id_Emp;
                    ter.Id_Cd = Sesion.Id_Cd_Ver;
                    ter.Id_Ter = pedido.Id_Ter;
                    CN_CatTerritorios terr = new CN_CatTerritorios();
                    terr.ConsultaTerritorio(ref ter, Sesion.Emp_Cnx);
                    string mensaje = string.Empty;
                    if (!string.IsNullOrEmpty(ter.Descripcion))
                    {
                        cmbTerritorio.ClearSelection();
                        cmbTerritorio.Text = ter.Descripcion;
                        txtTerritorio.Text = ter.Id_Ter.ToString();
                        mensaje = "El territorio ha sido dado de baja";
                    }
                    else
                        mensaje = "El territorio no existe";

                    this.rtb1.Items[6].Visible = false;
                    this.rtb1.Items[5].Visible = false;
                    Alerta(mensaje);
                }
                //CargarRik();

                int campo2 = cmbRik.FindItemIndexByValue(pedido.Id_Rik.ToString());
                if (campo2 > 0)
                {
                    cmbRik.SelectedIndex = cmbRik.FindItemIndexByValue(pedido.Id_Rik.ToString());
                    if (_PermisoGuardar == false)
                        this.rtb1.Items[6].Visible = false;
                    if (_PermisoGuardar == false & _PermisoModificar == false)
                        this.rtb1.Items[5].Visible = false;
                }
                else
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtTerritorio.Text), 0, Sesion.Emp_Cnx, "spCatRikTerr_Combo", ref cmbRik);
                    if (cmbRik.Items.Count > 0)
                    {
                        cmbRik.SelectedIndex = 0;
                        cmbRik.Text = cmbRik.Items[0].Text;
                        txtRepresentanteID.Text = cmbRik.Items[0].Value;
                    }
                    else
                    {
                        if (Request.QueryString["id"] != "-1")
                        {
                            this.rtb1.Items[6].Visible = false;
                            this.rtb1.Items[5].Visible = false;
                            Alerta("El representante ha sido dado de baja o no existe");
                        }
                    }
                }
                //----------------

                txtRequisicion.Text = pedido.Requisicion;
                txtSolicito.Text = pedido.Ped_Solicito;
                txtSub.Text = pedido.Ped_Subtotal.ToString("#,##0.00");

                txtTotal.Text = pedido.Ped_Total.ToString("#,##0.00");
                HF_ID.Value = txtClave.Text;

                rdFecha.DbSelectedDate = pedido.Ped_Fecha;
                try
                {
                    dpFecha2.DbSelectedDate = pedido.Ped_FechaEntrega;
                }
                catch (Exception)
                {

                }
                GetListDet();
                DataTable dt2 = dt;
                cappedido.ConsultaPedidoDet(pedido, ref dt2, Sesion.Emp_Cnx);
                for (int x = 0; x < dt2.Rows.Count; x++)
                {
                    int Id_Prd = int.Parse(dt2.Rows[x]["Id_Prd"].ToString());

                    if (dt2.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    {
                        double cant = Convert.ToDouble(dt2.Rows[x]["Prd_Cantidad"].ToString());
                        double precio = Convert.ToDouble(dt2.Rows[x]["Prd_Precio"].ToString());

                        dt2.Rows[x].BeginEdit();                        
                        dt2.Rows[x]["Prd_Importe"] = cant * precio;
                        dt2.Rows[x].AcceptChanges();
                    }
                }
                
                dt = dt2;
                rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarFecha(DateTime ini, DateTime fin)
        {
            return !(rdFecha.SelectedDate < ini || rdFecha.SelectedDate > fin);
        }
        private void LimpiarRegistro(RadNumericTextBox rdBox)
        {
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtProd")).Text = string.Empty;
            ((RadTextBox)rdBox.Parent.Parent.FindControl("txtProductoNombre")).Text = string.Empty;
            ((Label)rdBox.Parent.Parent.FindControl("lblPres2")).Text = string.Empty;
            ((Label)rdBox.Parent.Parent.FindControl("lblUnidad2")).Text = string.Empty;
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtPrecio")).Text = "0";
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtCantidad")).Text = "0";
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtImporte")).Text = "0";
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapPedido", "Id_Ped", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Funciones funciones = new Funciones();
                rdFecha.SelectedDate = funciones.GetLocalDateTime(Sesion.Minutos);
                dpFecha2.SelectedDate = funciones.GetLocalDateTime(Sesion.Minutos).AddDays(1);
                rdFecha.Focus();
                txtClave.Text = MaximoId();
                txtComentarios.Text = string.Empty;
                txtConcepto.Text = string.Empty;
                txtConcepto2.Text = string.Empty;
                txtCondiciones.Text = string.Empty;
                txtDescuento.Text = string.Empty;
                txtDescuento2.Text = string.Empty;
                txtFlete.Text = string.Empty;
                txtImporte.Text = string.Empty;
                txtIVA.Text = string.Empty;
                txtNumCliente.Text = string.Empty;
                txtCliente.Text = string.Empty;
                txtObservaciones.Text = string.Empty;
                txtOrden.Text = string.Empty;
                txtPedidodel.Text = string.Empty;
                txtRepresentanteID.Text = string.Empty;
                txtRequisicion.Text = string.Empty;
                txtSolicito.Text = string.Empty;
                txtSub.Text = string.Empty;
                txtTerritorio.Text = string.Empty;
                txtTotal.Text = string.Empty;
                //cmbCliente.SelectedIndex = 0;
                //cmbCliente.Text = cmbCliente.Items[0].Text;
                cmbRik.SelectedIndex = 0;
                cmbRik.Text = cmbRik.Items.Count > 0 ? cmbRik.Items[0].Text : "";
                cmbTerritorio.SelectedIndex = 0;
                cmbTerritorio.Text = cmbTerritorio.Items.Count > 0 ? cmbTerritorio.Items[0].Text : "";
                HF_ID.Value = "";
                dt.Rows.Clear();
                rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PerformInsert(GridCommandEventArgs e)
        {
            try
            {
                int Id_PedDet = 0;
                int Id_Ter = 0;
                string Ter_Nombre = "";
                int Id_prd = 0;
                string Prd_Descripcion = "";
                string Prd_Presentacion = "";
                string Prd_Unidad = "";
                double Prd_Precio = 0;
                int Prd_Cantidad = 0;
                double Prd_Importe = 0;
                DataRow[] Ar_dr;
                GridItem gi = e.Item;
                if (((RadComboBox)gi.FindControl("cmbTer")).SelectedIndex == 0 ||
                    !((RadNumericTextBox)gi.FindControl("txtProd")).Value.HasValue ||
                    ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "")
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }
                Id_Ter = Convert.ToInt32(((RadComboBox)gi.FindControl("cmbTer")).SelectedValue);
                Ter_Nombre = ((RadComboBox)gi.FindControl("cmbTer")).Text;
                Id_prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtProd")).Value);
                Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                Prd_Precio = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text);
                Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                Prd_Presentacion = ((Label)gi.FindControl("lblPres2")).Text;
                Prd_Unidad = ((Label)gi.FindControl("lblUnidad2")).Text;
                Prd_Importe = Prd_Precio * Prd_Cantidad;
                Id_PedDet = dt.Rows.Count + 1;
                Ar_dr = dt.Select("Id_Ter='" + Id_Ter + "' and Id_prd='" + Id_prd + "'");
                if (Ar_dr.Length > 0)
                {
                    Alerta("Producto ya capturado");
                    e.Canceled = true;
                }
                else
                {                 
                    dt.Rows.Add(new object[] {  
                                Id_PedDet, 
                                Id_Ter, 
                                Ter_Nombre,
                                Id_prd,
                                Prd_Descripcion,
                                Prd_Presentacion,
                                Prd_Unidad,
                                Prd_Precio,
                                Prd_Cantidad,
                                Prd_Importe
                                 });
                }
                CalcularTotales();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                    funcion = "CloseWindow()";
                else
                    funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void CalcularTotales()
        {
            try
            {
                double importe = 0;
                double sub = 0;
                double iva = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    importe += (Convert.ToDouble(dt.Rows[x]["Prd_Importe"]));
                    sub += (Convert.ToDouble(dt.Rows[x]["Prd_Importe"]) * (1 - Convert.ToDouble(txtDescuento.Text == "" ? "0" : txtDescuento.Text) / 100)) * (1 - Convert.ToDouble(txtDescuento2.Text == "" ? "0" : txtDescuento2.Text) / 100);
                }
                txtImporte.Text = importe.ToString("#,##0.00");
                iva = sub * (Iva_cd / 100);
                txtSub.Text = sub.ToString("#,##0.00");
                txtIVA.Text = iva.ToString("#,##0.00");
                txtTotal.Text = (sub + iva).ToString("#,##0.00");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Update(GridCommandEventArgs e)
        {
            try
            {
                int Id_PedDet = 0;
                int Id_Ter = 0;
                string Ter_Nombre = "";
                int Id_prd = 0;
                string Prd_Descripcion = "";
                string Prd_Presentacion = "";
                string Prd_Unidad = "";
                double Prd_Precio = 0;
                int Prd_Cantidad = 0;
                double Prd_Importe = 0;
                GridItem gi = e.Item;
                DataRow[] Ar_dr;
                if (((RadComboBox)gi.FindControl("cmbTer")).SelectedIndex == 0 ||
                                !((RadNumericTextBox)gi.FindControl("txtProd")).Value.HasValue ||
                                ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" ||
                                ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == ""
                               )
                {
                    e.Canceled = true;
                    return;
                }
                Id_Ter = Convert.ToInt32(((RadComboBox)gi.FindControl("cmbTer")).SelectedValue);
                Ter_Nombre = ((RadComboBox)gi.FindControl("cmbTer")).Text;
                Id_prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtProd")).Value);
                Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                Prd_Precio = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text);
                Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                Prd_Presentacion = ((Label)gi.FindControl("lblPres2")).Text;
                Prd_Unidad = ((Label)gi.FindControl("lblUnidad2")).Text;
                Prd_Importe = Prd_Precio * Prd_Cantidad; // Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtImporte")).Text);
                Id_PedDet = Convert.ToInt32(((Label)gi.FindControl("lblDet2")).Text);

                Ar_dr = dt.Select("Id_PedDet='" + Id_PedDet + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Ter"] = Id_Ter;
                    Ar_dr[0]["Ter_Nombre"] = Ter_Nombre;
                    Ar_dr[0]["id_prd"] = Id_prd;
                    Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;
                    Ar_dr[0]["Prd_Precio"] = Prd_Precio;
                    Ar_dr[0]["Prd_Cantidad"] = Prd_Cantidad;
                    Ar_dr[0]["Prd_Presentacion"] = Prd_Presentacion;
                    Ar_dr[0]["Prd_Unidad"] = Prd_Unidad;
                    Ar_dr[0]["Prd_Importe"] = Prd_Importe;
                    Ar_dr[0].AcceptChanges();
                }
                CalcularTotales();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void imprimir()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                ALValorParametrosInternos.Add(Sesion.Id_Emp);
                ALValorParametrosInternos.Add(Sesion.Emp_Cnx);
                Type instance = null;
                instance = typeof(LibreriaReportes.ReportePrueba);
                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReportePadre()");
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
                if (_PermisoGuardar == false)
                    this.rtb1.Items[6].Visible = false;
                if (_PermisoGuardar == false & _PermisoModificar == false)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
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
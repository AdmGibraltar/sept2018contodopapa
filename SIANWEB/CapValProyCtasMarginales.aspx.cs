using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Net;
using CapaDatos;

namespace SIANWEB
{
    public partial class CapValProyCtasMarginales : System.Web.UI.Page
    {
        #region Variables
        public string FechaEnable
        {
            get
            {
                return _Editable;
            }
        }
        private int Id_Vap = 0;        
        private int Id_Cd = 0;
        private int Id_Emp = 0;
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        private string _Editable;
        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }

        //Variable de lista de productos para el combo del grid Editable
        private List<Producto> _listaProductos;
        public List<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; }
        }

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        private List<ValuacionProyectoDetalle> ListaProductosValProyecto
        {
            get { return (List<ValuacionProyectoDetalle>)Session["ListaProductosValProyecto"]; }
            set { Session["ListaProductosValProyecto"] = value; }
        }

        //Propiedad de lista de tipo de Moneda
        private List<TipoMoneda> ListaTipoMoneda
        {
            get { return (List<TipoMoneda>)Session["ListaTipoMoneda"]; }
            set { Session["ListaTipoMoneda"] = value; }
        }
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
                    RAM1.ResponseScripts.Add("RefreshParentPage()");
                }
                else
                    if (!Page.IsPostBack)
                    { //obtener valores desde la URL
                        parametros();
                        this.Inicializar(Id_Emp, Id_Cd, Id_Vap);
                        double ancho = 0;
                        foreach (GridColumn gc in rgDetalle.Columns)
                        {
                            if (gc.Display)
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                        rgDetalle.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDetalle.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        txtFecha.Focus();
                    }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string mensajeError = string.Empty;
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgDetalle.Rebind();
                        break;
                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 70);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        RadSplitter3.Height = altura;
                        RadPane3.Height = altura;
                        RadPane3.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                        mensajeError = hiddenId.Value == string.Empty ? "CapValProyecto_insert_error" : "CapValProyecto_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgDetalle.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgDetalle.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);
                    //Llenar Grid
                    rgDetalle.DataSource = this.ListaProductosValProyecto;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    string cmbTipo = ((RadComboBox)editItem.FindControl("cmbTipo")).ClientID.ToString();
                    string lblVal_cmbTipo = ((Label)editItem.FindControl("lblVal_cmbTipo")).ClientID.ToString();
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    string txtVap_Cantidad = ((RadNumericTextBox)editItem.FindControl("txtVap_Cantidad")).ClientID.ToString();
                    string lblVal_txtVap_Cantidad = ((Label)editItem.FindControl("lblVal_txtVap_Cantidad")).ClientID.ToString();
                    string txtVap_Precio = ((RadNumericTextBox)editItem.FindControl("txtVap_Precio")).ClientID.ToString();
                    string lblVal_txtVap_Precio = ((Label)editItem.FindControl("lblVal_txtVap_Precio")).ClientID.ToString();
                    string lblVap_CostoEditClientID = ((Label)editItem.FindControl("lblVap_CostoEdit")).ClientID.ToString();

                    ////Llenar combo de productos
                    //RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    ////comboProductoItem.DataSource = this.ListaProductos;
                    ////comboProductoItem.DataBind();
                    ////comboProductoItem.SelectedIndex = 0;
                    //CargarProductos(comboProductoItem);

                    RadComboBox cmbTipoCombo = (RadComboBox)editItem.FindControl("cmbTipo");

                    string jsControles = string.Concat(
                        "cmbTipoClientID='", cmbTipo, "';"
                        , "lblVal_cmbTipoClientID='", lblVal_cmbTipo, "';"
                        , "txtId_PrdClientID='", txtId_Prd, "';"
                        , "lbl_cmbProductoClientID='", lbl_cmbProducto, "';"
                        , "txtVap_CantidadClientID='", txtVap_Cantidad, "';"
                        , "lblVal_txtVap_CantidadClientID='", lblVal_txtVap_Cantidad, "';"
                        , "txtVap_PrecioClientID='", txtVap_Precio, "';"
                        , "lblVal_txtVap_PrecioClientID='", lblVal_txtVap_Precio, "';"
                        , "lblVap_CostoEditClientID='", lblVap_CostoEditClientID, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = true;
                        ((RadTextBox)editItem.FindControl("txtProductoNombre")).Enabled = true;
                        //comboProductoItem.Enabled = true;

                        insertbtn.Attributes.Add("onclick", jsControles);

                        cmbTipoCombo.SelectedIndex = 1;
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {

                        int Id_Prd = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"]);
                        foreach (ValuacionProyectoDetalle vpd in this.ListaProductosValProyecto)
                        {
                            if (vpd.Id_Prd == Id_Prd)
                            {
                                cmbTipoCombo.SelectedIndex = cmbTipoCombo.FindItemIndexByValue(vpd.Vap_Tipo.ToString());
                                cmbTipoCombo.Text = cmbTipoCombo.FindItemByValue(vpd.Vap_Tipo.ToString()).Text;

                                if (vpd.Vap_Tipo == 2)
                                {
                                    ((RadNumericTextBox)editItem.FindControl("txtVap_Precio")).Enabled = false;
                                }
                                ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = false;
                                ((RadTextBox)editItem.FindControl("txtProductoNombre")).Enabled = false;
                                ((RadComboBox)editItem.FindControl("cmbTipo")).Enabled = false;                              
                            }
                        }
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");
                        updatebtn.Attributes.Add("onclick", jsControles);
                    }
                    cmbTipoCombo.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ValuacionProyectoDetalle valuacionProyectoDetalle = new ValuacionProyectoDetalle();

                valuacionProyectoDetalle.Id_Emp = sesion.Id_Emp;
                valuacionProyectoDetalle.Id_Cd = sesion.Id_Cd_Ver;
                valuacionProyectoDetalle.Id_Vap = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la valuacion de proyecto, cuando actualiza queda igual
                valuacionProyectoDetalle.Id_VapDet = 0;

                valuacionProyectoDetalle.Vap_Tipo = Convert.ToInt32((insertedItem.FindControl("cmbTipo") as RadComboBox).SelectedValue);
                valuacionProyectoDetalle.Vap_TipoStr = (insertedItem.FindControl("cmbTipo") as RadComboBox).SelectedItem.Text;
                RadNumericTextBox comboProducto = (insertedItem.FindControl("txtId_Prd") as RadNumericTextBox);
                valuacionProyectoDetalle.Id_Prd = Convert.ToInt32(comboProducto.Value);
                valuacionProyectoDetalle.Producto = new Producto();
                valuacionProyectoDetalle.Producto.Id_Prd = Convert.ToInt32(comboProducto.Value);
                valuacionProyectoDetalle.Producto.Prd_Descripcion = (insertedItem.FindControl("txtProductoNombre") as RadTextBox).Text;
                valuacionProyectoDetalle.Producto.Prd_Presentacion = (insertedItem.FindControl("lblPrd_PresentacionEdit") as Label).Text;
                valuacionProyectoDetalle.Producto.Prd_UniNs = (insertedItem.FindControl("lblPrd_UniNsEdit") as Label).Text;

                valuacionProyectoDetalle.Vap_Cantidad = Convert.ToInt32((insertedItem.FindControl("txtVap_Cantidad") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_Costo = Convert.ToDouble((insertedItem.FindControl("lblVap_CostoEdit") as Label).Text);
                valuacionProyectoDetalle.Vap_Precio = Convert.ToDouble((insertedItem.FindControl("txtVap_Precio") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_PrecioEspecial = Convert.ToDouble((insertedItem.FindControl("lblVap_ListaEdit") as Label).Text);//lblVap_PrecioEspecialEdit") as Label).Text);

                if (valuacionProyectoDetalle.Vap_Costo > 0 && (insertedItem.FindControl("txtVap_Precio") as RadNumericTextBox).Enabled)
                    if (valuacionProyectoDetalle.Vap_Costo > valuacionProyectoDetalle.Vap_Precio)
                    {
                        Alerta("El precio de venta no puede ser menor que el precio AAA");
                        return;
                    }
                //agregar producto de nota de cargo a la lista
                List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;

                //buscar producto de factura en la lista para ver si ya existe
                for (int i = 0; i < lista.Count; i++)
                {
                    ValuacionProyectoDetalle valProyectoDet = lista[i];
                    if (valProyectoDet.Id_Prd == valuacionProyectoDetalle.Id_Prd)//si el producto es el mismo
                    {
                        e.Canceled = true;
                        AlertaFocus("Producto ya capturado", comboProducto.ClientID);
                        return;
                    }
                }
                lista.Add(valuacionProyectoDetalle);
                this.ListaProductosValProyecto = lista;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ValuacionProyectoDetalle valuacionProyectoDetalle = new ValuacionProyectoDetalle();

                valuacionProyectoDetalle.Id_Emp = sesion.Id_Emp;
                valuacionProyectoDetalle.Id_Cd = sesion.Id_Cd_Ver;
                valuacionProyectoDetalle.Id_Vap = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la valuacion de proyecto, cuando actualiza queda igual
                valuacionProyectoDetalle.Id_VapDet = 0;

                valuacionProyectoDetalle.Vap_Tipo = Convert.ToInt32((insertedItem.FindControl("cmbTipo") as RadComboBox).SelectedValue);
                valuacionProyectoDetalle.Vap_TipoStr = (insertedItem.FindControl("cmbTipo") as RadComboBox).SelectedItem.Text;
                RadNumericTextBox comboProducto = (insertedItem.FindControl("txtId_Prd") as RadNumericTextBox);
                valuacionProyectoDetalle.Id_Prd = Convert.ToInt32(comboProducto.Value);
                valuacionProyectoDetalle.Producto = new Producto();
                valuacionProyectoDetalle.Producto.Id_Prd = Convert.ToInt32(comboProducto.Value);
                valuacionProyectoDetalle.Producto.Prd_Descripcion = (insertedItem.FindControl("txtProductoNombre") as RadTextBox).Text;
                valuacionProyectoDetalle.Producto.Prd_Presentacion = (insertedItem.FindControl("lblPrd_PresentacionEdit") as Label).Text;
                valuacionProyectoDetalle.Producto.Prd_UniNs = (insertedItem.FindControl("lblPrd_UniNsEdit") as Label).Text;

                valuacionProyectoDetalle.Vap_Cantidad = Convert.ToInt32((insertedItem.FindControl("txtVap_Cantidad") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_Costo = Convert.ToDouble((insertedItem.FindControl("lblVap_CostoEdit") as Label).Text);
                valuacionProyectoDetalle.Vap_Precio = Convert.ToDouble((insertedItem.FindControl("txtVap_Precio") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_PrecioEspecial = Convert.ToDouble((insertedItem.FindControl("lblVap_ListaEdit") as Label).Text);//lblVap_PrecioEspecialEdit") as Label).Text);
                
                if (valuacionProyectoDetalle.Vap_Costo > 0)
                    if (valuacionProyectoDetalle.Vap_Costo > valuacionProyectoDetalle.Vap_Precio)
                    {
                        return;
                    }
                List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;
                //buscar producto de factura en la lista
                for (int i = 0; i < lista.Count; i++)
                {
                    ValuacionProyectoDetalle valProyectoDet = lista[i];
                    if (valProyectoDet.Id_Prd == valuacionProyectoDetalle.Id_Prd)
                    {
                        lista[i] = valuacionProyectoDetalle;
                        break;
                    }
                }
                this.ListaProductosValProyecto = lista;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                //eliminar producto de nota de cargo a la lista
                this.ListaProductosValProyecto_EliminarProducto(id_Prd);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                RadComboBox rcb = (sender as RadComboBox);
                RadNumericTextBox rtb = rcb.Parent.Parent.FindControl("txtVap_Precio") as RadNumericTextBox;
                (rcb.Parent.Parent.FindControl("lblPrd_PresentacionEdit") as Label).Text = "";
                (rcb.Parent.Parent.FindControl("lblPrd_UniNsEdit") as Label).Text = "";
                (rcb.Parent.Parent.FindControl("lblVap_CostoEdit") as Label).Text = "";
                (rcb.Parent.Parent.FindControl("lblVap_ListaEdit") as Label).Text = "";// as Label).Text = "";
                (rcb.Parent.Parent.FindControl("txtVap_Cantidad") as RadNumericTextBox).Text = "";
                (rcb.Parent.Parent.FindControl("hdSisProp") as HiddenField).Value = "";
                (rcb.Parent.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = "";
                (rcb.Parent.Parent.FindControl("txtVap_Precio") as RadNumericTextBox).Text = "";
                (rcb.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Text = "";
                if (rcb.SelectedValue == "2")
                {
                    rtb.Value = 0;
                    rtb.Enabled = false;
                }
                else
                {
                    rtb.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProducto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadComboBox combo = (RadComboBox)sender;
                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                Label lblPrd_PresentacionEdit = (Label)tabla.FindControl("lblPrd_PresentacionEdit");
                Label lblPrd_UniNsEdit = (Label)tabla.FindControl("lblPrd_UniNsEdit");
                Label lblVap_CostoEdit = (Label)tabla.FindControl("lblVap_CostoEdit");
                RadNumericTextBox txt_Cantidad = (RadNumericTextBox)tabla.FindControl("txtVap_Cantidad");
                Label txtVap_PrecioEspecialEdit = (Label)tabla.FindControl("lblVap_ListaEdit");
                RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);
                int id_Cd_Prod = sesion.Id_Cd_Ver;
                Producto producto = null;
                if (e.Value != string.Empty && e.Value != "-1")
                {
                    CN_CatProducto clsProducto = new CN_CatProducto();
                    clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, Convert.ToInt32(e.Value), 1);
                }

                lblPrd_PresentacionEdit.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                lblPrd_UniNsEdit.Text = producto == null ? string.Empty : producto.Prd_UniNs;

                //obtener precio de producto
                double precioAAA = 0;
                double precioLista = 0;
                new CN_ProductoPrecios().ConsultarPrecios(ref precioAAA, ref precioLista, sesion, Convert.ToInt32(txtCliente.Text), Convert.ToInt32(e.Value));
                lblVap_CostoEdit.Text = precioAAA.ToString("N");
                txtVap_PrecioEspecialEdit.Text = precioLista.ToString("N");

                //Limpiar controles de compras locales
                //combo.Items[0].FindControl("liComprasLocales").Controls.Clear();
                txt_Cantidad.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                
                RadNumericTextBox txtProducto = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)txtProducto.Parent;
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                session.Id_Cd = session.Id_Cd_Ver;
                Label lblPrd_PresentacionEdit = (Label)tabla.FindControl("lblPrd_PresentacionEdit");
                Label lblPrd_UniNsEdit = (Label)tabla.FindControl("lblPrd_UniNsEdit");
                Label lblVap_CostoEdit = (Label)tabla.FindControl("lblVap_CostoEdit");
                Label txtVap_PrecioEspecialEdit = (Label)tabla.FindControl("lblVap_ListaEdit");
                RadNumericTextBox txt_Cantidad = (RadNumericTextBox)tabla.FindControl("txtVap_Cantidad");

                List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;
                //buscar producto de factura en la lista para ver si ya existe
                for (int i = 0; i < lista.Count; i++)
                {
                    ValuacionProyectoDetalle valProyectoDet = lista[i];
                    if (valProyectoDet.Id_Prd.ToString() == txtProducto.Text)//si el producto es el mismo
                    {
                        AlertaFocus("Producto ya capturado", txtProducto.ClientID);
                        txtProducto.Text = "";
                        return;
                    }
                }
                Producto prd = new Producto();
                prd.Prd_AparatoSisProp = (txtProducto.Parent.Parent.FindControl("cmbTipo") as RadComboBox).SelectedValue == "2" ? true : false;
                try
                {
                    new CN_CatProducto().ConsultaProducto(ref prd, session.Emp_Cnx, session.Id_Emp, session.Id_Cd, Convert.ToInt32(txtProducto.Value.HasValue ? (sender as RadNumericTextBox).Value : -1));
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtProducto.ClientID);
                    txtProducto.Text = "";
                    (tabla.FindControl("txtProductoNombre") as RadTextBox).Text = "";
                    lblPrd_PresentacionEdit.Text = "";
                    lblPrd_UniNsEdit.Text = "";
                    lblVap_CostoEdit.Text = "";
                    txtVap_PrecioEspecialEdit.Text = "";
                    return;
                }
                (tabla.FindControl("hdSisProp") as HiddenField).Value = prd.Prd_AparatoSisProp.ToString();
                (tabla.FindControl("txtProductoNombre") as RadTextBox).Text = prd.Prd_Descripcion;

                int id_Cd_Prod = session.Id_Cd_Ver; 
                lblPrd_PresentacionEdit.Text = prd == null ? string.Empty : prd.Prd_Presentacion;
                lblPrd_UniNsEdit.Text = prd == null ? string.Empty : prd.Prd_UniNs;

                //obtener precio de producto
                //Id_Cd = 210;
                //Id_Emp = 1;
                double precioAAA = 0;               
                double precioLista = 0;
                
                new CN_ProductoPrecios().ConsultarPrecios(ref precioAAA, ref precioLista, session, Convert.ToInt32(txtCliente.Text), Convert.ToInt32(txtProducto.Value));                
                lblVap_CostoEdit.Text = precioAAA.ToString("N");
                txtVap_PrecioEspecialEdit.Text = precioLista.ToString("N");
               
               
                //Limpiar controles de compras locales
                //combo.Items[0].FindControl("liComprasLocales").Controls.Clear();
                txt_Cantidad.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ListaProductosValProyecto = new List<ValuacionProyectoDetalle>();
                rgDetalle.Rebind();

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cte = new Clientes();
                cte.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1);
                cte.Id_Emp = sesion.Id_Emp;
                cte.Id_Cd = sesion.Id_Cd_Ver;
                cte.Id_Rik = sesion.Id_Rik;
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    txtClienteNombre.Text = "";
                    txtCliente.Text = "";
                    return;
                }
                txtClienteNombre.Text = cte.Cte_NomComercial;
                txtNota.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {             
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
        #endregion

        #region Funciones
        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Vap)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            TipoMoneda tipoMoneda = new TipoMoneda();
            List<TipoMoneda> lista = new List<TipoMoneda>();
            new CN_CatTipoMoneda().ConsultaTipoMoneda(tipoMoneda, Sesion.Id_Emp
                , Sesion.Emp_Cnx, ref lista);
            this.ListaTipoMoneda = lista;

            //nueva variable para controlar tabla dinamica de productos de nota de cargo
            Session["ListaProductosValProyecto"] = new List<ValuacionProyectoDetalle>();

            //establece valores de controles de inicio
            if (Id_Emp > 0 && Id_Cd > 0 && Id_Vap > 0)
            {
                this.LLenarFormValProyecto(Id_Emp, Id_Cd, Id_Vap); //EDICION 
                this.hiddenId.Value = Id_Vap.ToString();
                LlenarParametros(Id_Vap);
            }
            else //Nueva
            {
                LlenarParametros(null);
                Id_Vap = !string.IsNullOrEmpty(Page.Request.QueryString["Id"]) ? Convert.ToInt32(Page.Request.QueryString["Id"]) : 0;
                _Editable = "1";
                if (Id_Vap == 0)
                {
                    this.hiddenId.Value = string.Empty;
                    this.txtFecha.SelectedDate = DateTime.Now;
                    this.txtFolio.Text = this.Valor;
                }
                else
                {
                    this.LLenarFormValProyecto(Sesion.Id_Emp, Sesion.Id_Cd, Id_Vap);
                    this.hiddenId.Value = Id_Vap.ToString();
                    LlenarParametros(Id_Vap);
                }
            }
            this.rgDetalle.Rebind();
            this.txtFecha.Focus();
        }
        protected string ObtenerDescripcionProducto(object oc)
        {
            if (oc is ValuacionProyectoDetalle) { return ((ValuacionProyectoDetalle)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }
        protected string ObtenerPresentacionProducto(object oc)
        {
            if (oc is ValuacionProyectoDetalle) { return ((ValuacionProyectoDetalle)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }
        protected string ObtenerUnidadesProducto(object oc)
        {
            if (oc is ValuacionProyectoDetalle) { return ((ValuacionProyectoDetalle)oc).Producto.Prd_UniNs; } else { return string.Empty; }
        }
        protected int ObtenerInvFinal(object oc)
        {
            if (oc is ValuacionProyectoDetalle) { return ((ValuacionProyectoDetalle)oc).Producto.Prd_InvFinal; } else { return 0; }
        }
        protected void ListaProductosValProyecto_EliminarProducto(int id_Prd)
        {
            List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;

            //buscar producto de factura en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                ValuacionProyectoDetalle valProyectoDet = lista[i];
                if (valProyectoDet.Id_Prd == id_Prd)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaProductosValProyecto = lista;
        }
        private double PartidasCalcularPrecioLista(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Prd, string Conexion)
        {
            double precioProductoAceptado = 0;
            //obtener precio especial del producto 
            //para el cliente actual de la factura
            //desde la CAPTURA de SOLICITUDES DE PRECIOS ESPECIALES
            VentanaPrecioEspecialPro precioEspecialPro = null;
            new CN_PrecioEspecial().PrecioEspecialProductoCliente_Consulta(ref precioEspecialPro, Conexion
                , Id_Emp, Id_Cd, Id_Cte, Id_Prd /* , Convert.ToInt32(cmbMoneda.SelectedValue) */);
            if (precioEspecialPro != null && precioEspecialPro.Ape_PreEsp > 0)
            {
                /*
                    * NOTA: si el precio está en dólares u otro tipo de moneda, 
                    * se hace la conversión al tipo de moneda de la Valuacion de proyectos
                    */
                if (precioEspecialPro.Id_Mon != 1) // MONEDA = PESO (1) siempre en captura de valuacion proyectos
                { //Consultar tipo de cambio
                    double tipoCambioFactura = 1; // MONEDA = PESO (1) siempre en captura de valuacion proyectos
                    double tipoCambioPrecioEspecial = 0;
                    foreach (TipoMoneda tm in ListaTipoMoneda)
                    {
                        if (tm.Id_Mon == precioEspecialPro.Id_Mon)
                            tipoCambioPrecioEspecial = tm.Mon_TipCambio;
                    }
                    precioProductoAceptado = (precioEspecialPro.Ape_PreEsp * tipoCambioPrecioEspecial) / tipoCambioFactura;
                }
                else
                    precioProductoAceptado = precioEspecialPro.Ape_PreEsp;
            }
            else
            {
                //Si no hay un precio especial en SOLICITUD DE PRECIOS ESPECIALES
                //va por el precio del catalogo CLIENTE-PRODUCTO, si no hay toma el precio AAA normal del producto
                //obtener precio AAA
                float precioAAA = 0;
                new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precioAAA, Id_Emp, Id_Cd, Id_Prd, Conexion);

                //obtener precio especial de producto
                //desde el catálogo CAT_CLIENTEPRODUCTO
                float precioPublicoCAT_CLIENTEPRODUCTO = 0;
                ClienteProd clienteProd = new ClienteProd();
                clienteProd.Id_Emp = Id_Emp;
                clienteProd.Id_Cd = Id_Cd;
                clienteProd.Id_Cte = Id_Cte;
                clienteProd.Id_Prd = Id_Prd;
                clienteProd.Id_Vap = Id_Vap;
                new CN_CatClienteProd().ClienteProductoPrecioPublico_Consultar(ref clienteProd, Conexion, ref precioPublicoCAT_CLIENTEPRODUCTO);

                precioProductoAceptado = precioPublicoCAT_CLIENTEPRODUCTO > 0 ? precioPublicoCAT_CLIENTEPRODUCTO : precioAAA;
            }
            return precioProductoAceptado;
        }
        private void LlenarParametros(int? Id_Vap)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Id_Vap == null)
                {
                    CN_CapValuacionProyectoCtasMarg cn_valuacion = new CN_CapValuacionProyectoCtasMarg();
                    ValuacionParametrosCtasMarg vp = new ValuacionParametrosCtasMarg();
                    vp.Id_Emp = sesion.Id_Emp;
                    vp.Id_Cd = sesion.Id_Cd_Ver;
                    
                    vp.Id_Cd = sesion.Id_Cd;

                    int verificador = 0;
                    cn_valuacion.consultarCondicionesCentro(ref vp, sesion.Emp_Cnx, ref  verificador);

                    
                    txtManoObra.DbValue = vp.Vap_Mano_Obra;
                    txtCuentasporCobrar.DbValue = vp.Vap_Dias_Cuentas_por_Cobrar;                   
                    txtFleteLocales.DbValue = vp.Vap_Gasto_Flete_Locales;
                    txtGAdmitivos.DbValue = vp.Vap_Gastos_Admin;
                    txtCostosFijosNoPapel.DbValue = vp.Vap_Contribucion_Costos_Fijos;                   
                    txtIsr.DbValue = vp.Vap_ISR;
                    txtInversionActivosFijos.DbValue = vp.Vap_Inversion_Activos;
                    txtIva.DbValue = vp.Vap_IVA;
                    txtCetes.DbValue = vp.Vap_Cetes;
                    txtAdicionalCetes.DbValue = vp.Vap_Adicional_Cetes;                                
                    txtInventarioKey.DbValue = vp.Vap_Inventario_Key;
                    txtCreditoProveedor.DbValue = vp.Vap_Credito_Key;
                    txtOtrosGastos.DbValue = vp.Vap_Otros_Gastos_Variable;
                    //txtCreditoProveedorPapel.DbValue = vp.Vap_Credito_Papel;
                    //txtComision.DbValue = vp.Vap_Participacion;
                    //txtVigencia.DbValue = vp.Vap_Vigencia;
                    //txtAmortizacion.DbValue = vp.Vap_Amortizacion;
                    //txtNumEntregas.DbValue = vp.Vap_Numero_Entregas;
                    //txtCostoEntregas.DbValue = vp.Vap_Costo_Entregas;
                    //txtComisionFactoraje.DbValue = vp.Vap_Comision_Factoraje;
                    //txtInventarioKeyConsignacion.DbValue = vp.Vap_Inventario_Consignacion;
                    //txtCostosFijosPapel.DbValue = vp.Vap_Costos_Fijos_Papel;
                    //txtUcs.DbValue = vp.Vap_Ucs;
                    //txtComisionCruce.DbValue = vp.Vap_Comision_Anden;
                }
                else
                {
                    CN_CapValuacionProyectoCtasMarg cnValuacion = new CN_CapValuacionProyectoCtasMarg();
                    ValuacionParametrosCtasMarg vp = new ValuacionParametrosCtasMarg();
                    vp.Id_Emp = sesion.Id_Emp;
                    vp.Id_Cd = sesion.Id_Cd_Ver;
                    vp.Id_Vap = (int)Id_Vap;
                    int verificador = 0;
                    cnValuacion.consultarCondiciones(ref vp, sesion.Emp_Cnx, ref  verificador);

                    if (verificador == 0) return;
                 
                    txtManoObra.DbValue = vp.Vap_Mano_Obra;
                    txtCuentasporCobrar.DbValue = vp.Vap_Dias_Cuentas_por_Cobrar;
                    txtFleteLocales.DbValue = vp.Vap_Gasto_Flete_Locales;
                    txtIva.DbValue = vp.Vap_IVA;                    
                    txtInventarioKey.DbValue = vp.Vap_Inventario_Key;                    
                    txtIsr.DbValue = vp.Vap_ISR;                    
                    txtCetes.DbValue = vp.Vap_Cetes;
                    txtAdicionalCetes.DbValue = vp.Vap_Adicional_Cetes;                    
                    txtGAdmitivos.DbValue = vp.Vap_Gastos_Admin;
                    txtInversionActivosFijos.DbValue = vp.Vap_Inversion_Activos;
                    txtCreditoProveedor.DbValue = vp.Vap_Credito_Key;
                    txtOtrosGastos.DbValue = vp.Vap_Otros_Gastos_Variable;
                    txtCostosFijosNoPapel.DbValue = vp.Vap_Contribucion_Costos_Fijos;                   
                    //txtInventarioKeyConsignacion.DbValue = vp.Vap_Inventario_Consignacion;                    
                    //txtCostosFijosPapel.DbValue = vp.Vap_Costos_Fijos_Papel;
                    //txtCreditoProveedorPapel.DbValue = vp.Vap_Credito_Papel;
                    //txtUcs.DbValue = vp.Vap_Ucs;
                    //txtAmortizacion.DbValue = vp.Vap_Amortizacion;
                    //txtNumEntregas.DbValue = vp.Vap_Numero_Entregas;
                    //txtCostoEntregas.DbValue = vp.Vap_Costo_Entregas;
                    //txtComisionFactoraje.DbValue = vp.Vap_Comision_Factoraje;
                    //txtComisionCruce.DbValue = vp.Vap_Comision_Anden;
                    //  txtVigencia.DbValue = vp.Vap_Vigencia;
                    //txtComision.DbValue = vp.Vap_Participacion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EstablecerDataSourceProductosLista(string filtro)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = new Producto();

                List<Producto> listaProducto = new List<Producto>();
                new CN_CatProducto().ConsultaListaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, filtro, ref listaProducto, 1);

                producto = new Producto();
                producto.Id_Prd = -1;
                producto.Prd_Descripcion = "-- Seleccionar --";
                listaProducto.Insert(0, producto);
                this.ListaProductos = listaProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LLenarFormValProyecto(int Id_Emp, int Id_Cd, int Id_Vap)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            ValuacionProyecto valuacionProyecto = new ValuacionProyecto();
            valuacionProyecto.Id_Emp = Id_Emp;
            valuacionProyecto.Id_Cd = Id_Cd;
            valuacionProyecto.Id_Vap = Id_Vap;

            new CN_CapValuacionProyectoCtasMarg().ConsultarValuacionProyecto(ref valuacionProyecto, sesion.Emp_Cnx);
            txtFolio.Text = valuacionProyecto.Id_Vap.ToString();
            txtFecha.SelectedDate = valuacionProyecto.Vap_Fecha;
            txtCliente.Text = valuacionProyecto.Id_Cte.ToString();
            txtClienteNombre.Text = valuacionProyecto.Cte_NomComercial;
            txtNota.Text = valuacionProyecto.Vap_Nota;
            //calcular precio de lista de cada producto de la partida
            foreach (ValuacionProyectoDetalle vd in valuacionProyecto.ListaProductosValuacionProyecto)
            {
                vd.Vap_PrecioEspecial = this.PartidasCalcularPrecioLista(vd.Id_Emp, vd.Id_Cd, valuacionProyecto.Id_Cte, vd.Id_Prd, sesion.Emp_Cnx);                     
            }
            rgDetalle.MasterTableView.Columns[11].Display = true;
            this.ListaProductosValProyecto = valuacionProyecto.ListaProductosValuacionProyecto;

            double ancho = 0;
            foreach (GridColumn gc in rgDetalle.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgDetalle.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgDetalle.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

            rgDetalle.Rebind();
        }
        private ValuacionProyecto LlenarObjetoValProyecto()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ValuacionProyecto valuacionProyecto = new ValuacionProyecto();
            valuacionProyecto.Id_Emp = sesion.Id_Emp;
            valuacionProyecto.Id_Cd = sesion.Id_Cd_Ver;
            valuacionProyecto.Id_Vap = Convert.ToInt32(txtFolio.Text);

            valuacionProyecto.Id_U = sesion.Id_U;
            valuacionProyecto.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1);
            Funciones funcion = new Funciones();
            valuacionProyecto.Vap_Fecha = Convert.ToDateTime(txtFecha.SelectedDate.Value.ToShortDateString() + funcion.GetLocalDateTime(sesion.Minutos).ToString(" HH:mm:ss"));
            valuacionProyecto.Vap_Nota = txtNota.Text;
            valuacionProyecto.Vap_Estatus = "C";
            valuacionProyecto.ListaProductosValuacionProyecto = this.ListaProductosValProyecto;
            return valuacionProyecto;
        }
        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ValuacionProyecto valuacionProyecto = this.LlenarObjetoValProyecto();
                string mensaje = string.Empty;
                ValuacionParametrosCtasMarg vp = new ValuacionParametrosCtasMarg();
                vp.Id_Cd = sesion.Id_Cd_Ver;
                vp.Id_Emp = sesion.Id_Emp;
                
                vp.Vap_Mano_Obra = txtManoObra.Value.HasValue ? txtManoObra.Value.Value : 0;
                vp.Vap_Gasto_Flete_Locales = txtFleteLocales.Value.HasValue ? txtFleteLocales.Value.Value : 0;
                vp.Vap_IVA = txtIva.Value.HasValue ? txtIva.Value.Value : 0;
                vp.Vap_Dias_Cuentas_por_Cobrar = txtCuentasporCobrar.Value.HasValue ? txtCuentasporCobrar.Value.Value : 0;
                vp.Vap_Inventario_Key = txtInventarioKey.Value.HasValue ? (int)txtInventarioKey.Value.Value : 0;
                vp.Vap_Credito_Key = txtCreditoProveedor.Value.HasValue ? (int)txtCreditoProveedor.Value.Value : 0;
                vp.Vap_ISR = txtIsr.Value.HasValue ? txtIsr.Value.Value : 0;
                vp.Vap_Cetes = txtCetes.Value.HasValue ? txtCetes.Value.Value : 0;
                vp.Vap_Adicional_Cetes = txtAdicionalCetes.Value.HasValue ? txtAdicionalCetes.Value.Value : 0;
                vp.Vap_Contribucion_Costos_Fijos = txtCostosFijosNoPapel.Value.HasValue ? txtCostosFijosNoPapel.Value.Value : 0;
                vp.Vap_Gastos_Admin = txtGAdmitivos.Value.HasValue ? txtGAdmitivos.Value.Value : 0;
                vp.Vap_Inversion_Activos = txtInversionActivosFijos.Value.HasValue ? (int)txtInversionActivosFijos.Value.Value : 0;
                vp.Vap_Otros_Gastos_Variable = txtOtrosGastos.Value.HasValue ? txtOtrosGastos.Value.Value : 0;
                
                //vp.Vap_Vigencia = txtVigencia.Value.HasValue ? (int)txtVigencia.Value.Value : 0;
                //vp.Vap_Participacion = txtComision.Value.HasValue ? txtComision.Value.Value : 0;
                //vp.Vap_Amortizacion = txtAmortizacion.Value.HasValue ? txtAmortizacion.Value.Value : 0;
                //vp.Vap_Numero_Entregas = txtNumEntregas.Value.HasValue ? (int)txtNumEntregas.Value.Value : 0;
                //vp.Vap_Costo_Entregas = txtCostoEntregas.Value.HasValue ? txtCostoEntregas.Value.Value : 0;
                //vp.Vap_Comision_Factoraje = txtComisionFactoraje.Value.HasValue ? txtComisionFactoraje.Value.Value : 0;
                //vp.Vap_Comision_Anden = txtComisionCruce.Value.HasValue ? txtComisionCruce.Value.Value : 0;                
                //vp.Vap_Inventario_Consignacion = txtInventarioKeyConsignacion.Value.HasValue ? (int)txtInventarioKeyConsignacion.Value.Value : 0;                
                //vp.Vap_Credito_Papel = txtCreditoProveedorPapel.Value.HasValue ? (int)txtCreditoProveedorPapel.Value.Value : 0;                
              // vp.Vap_Ucs = txtUcs.Value.HasValue ? txtUcs.Value.Value : 0;                
                //vp.Vap_Costos_Fijos_Papel = txtCostosFijosPapel.Value.HasValue ? txtCostosFijosPapel.Value.Value : 0;
                
                

                int verificador = 0;
                if (this.hiddenId.Value == string.Empty) //nueva valuación de proyecto
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    if (valuacionProyecto.ListaProductosValuacionProyecto.Count > 0)
                    {
                        new CN_CapValuacionProyectoCtasMarg().InsertarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador);
                        EnviaEmail(Convert.ToInt32(txtFolio.Text));
                        mensaje = "Los datos se guardaron correctamente";
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura
                    }
                    else
                        Alerta("Ingrese por lo menos un producto en el detalle");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    if (valuacionProyecto.ListaProductosValuacionProyecto.Count > 0)
                    {
                        new CN_CapValuacionProyectoCtasMarg().ModificarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador);
                        EnviaEmail(Convert.ToInt32(txtFolio.Text));
                        mensaje = "Los datos se modificaron correctamente";
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura
                    }
                    else
                        Alerta("Ingrese por lo menos un producto en el detalle");
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

                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.RadToolBar1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviaEmail(int solicitud)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                if (configuracion.Mail_Valuacion.Length == 0)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table style='font-family: verdana; font-size:9pt'><tr><td>");
                cuerpo_correo.Append("<img src=\"cid:companylogo\"></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de valuación de proyectos con el número de folio <b>" + solicitud.ToString() + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("Centro de distribución: <b>" + session.Id_Cd_Ver + " - " + session.Cd_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("Solicitó: <b>" + session.Id_U + " - " + session.U_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'><br>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "CapValProyectos_Autorizacion.aspx?Id1=" + solicitud.ToString() + "&id2=" + session.Id_Emp + "&id3=" + session.Id_Cd_Ver + "'>Solicitud de autorización de valuación de proyectos</a></font></center>");

                // cuerpo_correo.Append("<center><br><a href='http://" + url + "/CapValProyectos_Autorizacion.aspx?id1=" + solicitud.ToString() + "&id2=" + session.Id_Emp + "&id3=" + session.Id_Cd_Ver + "'>Solicitud de autorización de valuación de proyectos</a></font></center>");
                // cuerpo_correo.Append("<center><br><a href='" + Request.Url.ToString().Replace((new FileInfo(Request.Url.AbsolutePath)).Name, "") + "CapValProyectos.aspx?id1=" + solicitud.ToString() + "&id2=" + session.Id_Emp + "&id3=" + session.Id_Cd_Ver + "'>Solicitud de autorización de valuación de proyectos</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                string To = configuracion.Mail_Valuacion;//cambiar al hacerlo jalar por Mail_ValProyectos
                for (int x = 0; x < To.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Length; x++)
                    m.To.Add(new MailAddress(To.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[x]));

                m.Subject = "Solicitud de autorización de valuación de proyectos";
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
        private void parametros()
        {
            try
            {
                Id_Vap = Convert.ToInt32(Page.Request.QueryString["Id_Vap"]);
                Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;
                _Editable = Page.Request.QueryString["modificable"];
                if (_Editable == "0")
                    RadToolBar1.FindItemByValue("save").Visible = true; // devolver a false
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapValProyectoCtasMarg", "Id_Vap", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref sender);
                sender.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void CargarComboCliente()
        //{
        //    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);
        //}
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgValProyectoDet_insert_repetida"))
                    Alerta("Este producto ya ha sido capturado");
                else
                    if (mensaje.Contains("PermisoGuardarNo"))
                        Alerta("No tiene permisos para grabar");
                    else
                        if (mensaje.Contains("PermisoModificarNo"))
                            Alerta("No tiene permisos para actualizar");
                        else
                            if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                Alerta("Error al consultar los datos del producto");
                            else
                                if (mensaje.Contains("CapNotaCredito_insert_ok"))
                                    Alerta("Los datos se guardaron correctamente");
                                else
                                    if (mensaje.Contains("CapValProyecto_insert_error"))
                                        Alerta("No se pudo guardar la valuación de proyecto");
                                    else
                                        if (mensaje.Contains("CapNotaCredito_update_ok"))
                                            Alerta("Los datos se modificaron correctamente");
                                        else
                                            if (mensaje.Contains("CapValProyecto_update_error"))
                                                Alerta("No se pudo actualizar la valuación de proyecto");
                                            else
                                                if (mensaje.Contains("rgDetalleDet_NeedDataSource"))
                                                    Alerta("Error al cargar el grid de detalle de la valuación de proyecto");
                                                else
                                                    if (mensaje.Contains("rgDetalle_ItemDataBound"))
                                                        Alerta("Error al momento de preparar un registro para edición");
                                                    else
                                                        if (mensaje.Contains("rgValProyectoDet_insert_error"))
                                                            Alerta("Error al momento de agregar la partida a la lista de partidas de la valuación de proyecto");
                                                        else
                                                            if (mensaje.Contains("rgNotaCreditoDet_delete_error"))
                                                                Alerta("Error al momento de eliminar el producto a la lista de productos de la nota de crédito");
                                                            else
                                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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
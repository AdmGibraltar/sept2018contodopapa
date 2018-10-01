using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using Telerik.Web.UI.GridExcelBuilder;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace SIANWEB
{
    public partial class CapOrdCompra : System.Web.UI.Page
    {
        #region Variables
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
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        private DataTable listaPartidas
        {
            get { return (DataTable)Session["listaPartidas" + Session.SessionID]; }
            set { Session["listaPartidas" + Session.SessionID] = value; }
        }
        #endregion

        #region Propiedades
        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }
        private List<Producto> _listaProductos;
        public List<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; }
        }
        private List<OrdenCompraDet> ListaProductosOrdenCompra
        {
            get { return (List<OrdenCompraDet>)Session["ListaProductosOrdenCompra"]; }
            set { Session["ListaProductosOrdenCompra"] = value; }
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Page.IsPostBack)
                {
                    Session["_IdProducto"] = 0;
                    this.LlenarComboProveedores();
                    this.CargarComboTipoModeda();

                    txtMoneda.Text = "2";

                    //obtener valores desde la URL
                    int Id_Ord = Convert.ToInt32(Page.Request.QueryString["Id_Ord"]);
                    _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                    _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                    _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                    _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;

                    //establece valores de controles de inicio
                    if (Id_Ord > 0)
                        this.LLenarFormOrdCompra(Id_Ord);
                    else
                    {
                        this.hiddenId.Value = string.Empty;
                        this.txtFecha.SelectedDate = DateTime.Now;
                        //this.rgOrdCompra.Enabled = false;
                        this.txtFecha.Focus();
                        //nueva variable para controlar tabla dinamica de productos de orden de compra
                        Session["ListaProductosOrdenCompra"] = new List<OrdenCompraDet>();
                        this.Nuevo();
                        this.txtFolio.Text = this.Valor;
                        this.HabilitaBotonesToolBar(false, true, false, false, false, false);
                    }

                    if (!((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible)
                    {
                        deshabilitarcontroles(divPrincipal.Controls);
                        //deshabilitarcontroles(formularioTotales.Controls);
                        GridCommandItem cmdItem = (GridCommandItem)rgOrdCompra.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                        ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 
                        ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  

                        rgOrdCompra.MasterTableView.Columns[rgOrdCompra.MasterTableView.Columns.Count - 1].Display = false;
                        rgOrdCompra.MasterTableView.Columns[rgOrdCompra.MasterTableView.Columns.Count - 2].Display = false;

                        rgOrdCompra.Columns.FindByUniqueName("Prd_Descripcion").HeaderStyle.Width = (Unit)(Convert.ToInt32(rgOrdCompra.Columns.FindByUniqueName("Prd_Descripcion").HeaderStyle.Width.Value) + 111);
                    }

                    double ancho = 0;
                    foreach (GridColumn gc in rgOrdCompra.Columns)
                    {
                        if (gc.Display && gc.Visible)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    rgOrdCompra.Width = Unit.Pixel(Convert.ToInt32(ancho) + 55);
                    rgOrdCompra.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho) + 38);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
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
        #region "Métodos para obtrener desde objetos los valores para los controles durante la inserción/actualización de un Grid editable"
        protected string ObtenerDescripcion(object oc)
        {
            if (oc is OrdenCompraDet) { return ((OrdenCompraDet)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }
        protected string ObtenerPresentacion(object oc)
        {
            if (oc is OrdenCompraDet) { return ((OrdenCompraDet)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }
        protected string ObtenerUnidades(object oc)
        {
            if (oc is OrdenCompraDet) { return ((OrdenCompraDet)oc).Producto.Prd_UniNe; } else { return string.Empty; }
        }
        protected int ObtenerInvFinal(object oc)
        {
            if (oc is OrdenCompraDet) { return ((OrdenCompraDet)oc).Producto.Prd_InvFinal; } else { return 0; }
        }

        protected float ObtenerPrecio(object oc)
        {
            if (oc is OrdenCompraDet) { return ((OrdenCompraDet)oc).ProductoPrecio.Prd_Pesos; } else { return 0; }
        }
        #endregion
        #region "Métodos para manejar la lista dinámica de Productos de una orden de compra"
        protected void ListaProductosOrdenCompra_AgregarProducto(OrdenCompraDet ordCompra_prod)
        {
            List<OrdenCompraDet> lista = this.ListaProductosOrdenCompra;

            //buscar producto de orden de compra en la lista para ver si ya existe
            for (int i = 0; i < lista.Count; i++)
            {
                OrdenCompraDet orden = lista[i];
                if (orden.Id_Prd == ordCompra_prod.Id_Prd)
                {
                    throw new Exception("rgOrdCompra_insert_repetida");
                }
            }

            lista.Add(ordCompra_prod);
            this.ListaProductosOrdenCompra = lista;
            CalcularTotal(lista);
        }
        private void CalcularTotal(List<OrdenCompraDet> lista)
        {
            try
            {
                double? total = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                for (int i = 0; i < lista.Count; i++)
                {
                    OrdenCompraDet orden = lista[i];

                    // CN_CatProducto cn_producto = new CN_CatProducto();
                    //Producto prd = new Producto();
                    // cn_producto.ConsultaProducto(ref prd, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, orden.Id_Prd);
                    total += orden.Ord_Cantidad * orden.ProductoPrecio.Prd_Pesos;
                }

                txtTotal.DbValue = total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaProductosOrdenCompra_ModificarProducto(OrdenCompraDet ordCompra_prod)
        {
            List<OrdenCompraDet> lista = this.ListaProductosOrdenCompra;

            //buscar producto de orden de compra en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                OrdenCompraDet orden = lista[i];
                if (orden.Id_Prd == ordCompra_prod.Id_Prd)
                {
                    lista[i] = ordCompra_prod;
                    break;
                }
            }
            this.ListaProductosOrdenCompra = lista;
            CalcularTotal(lista);
        }
        protected void ListaProductosOrdenCompra_EliminarProducto(int id_Prd)
        {
            List<OrdenCompraDet> lista = this.ListaProductosOrdenCompra;

            //buscar producto de orden de compra en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                OrdenCompraDet orden = lista[i];
                if (orden.Id_Prd == id_Prd)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaProductosOrdenCompra = lista;
            CalcularTotal(lista);
        }
        protected void ListaProductosOrdenCompra_EliminarTodos()
        {
            this.ListaProductosOrdenCompra = new List<OrdenCompraDet>();
            txtTotal.DbValue = 0;
        }
        #endregion
        protected void rgOrdCompra_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);

                    //Llenar Grid
                    rgOrdCompra.DataSource = this.ListaProductosOrdenCompra;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgOrdCompra_NeedDataSource"));
            }
        }
        protected void rgOrdCompra_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();//txtId_Prd
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();//lbl_cmbProducto                   
                    string cmbProducto = ((RadTextBox)editItem.FindControl("txtPrd_Descripcion")).ClientID.ToString();
                    string lblOrd_Cantidad = ((Label)editItem.FindControl("lblVal_txtOrd_Cantidad")).ClientID.ToString();
                    RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editItem.FindControl("txtOrd_Cantidad");
                    string txtOrd_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                    string HD_Prd_UniEmp = ((HiddenField)editItem.FindControl("HD_Prd_UniEmp")).ClientID.ToString();

                    string jsControles = string.Concat(
                        "lbl_cmbProductoClientId='", lbl_cmbProducto, "';"
                        , "cmbProductoClientId='", cmbProducto, "';"
                        , "lblVal_txtOrd_CantidadClientId='", lblOrd_Cantidad, "';"
                        , "txtOrd_CantidadClientId='", txtOrd_Cantidad, "';"
                        , "HD_Prd_UniEmpClientId='", HD_Prd_UniEmp, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //es registro nuevo y se inhabilita el campo de cantidad (se habilita hasta ke elija un producto del combo)
                        Ctrl_txtOrd_Cantidad.Enabled = false;
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //establecer unidades de empaque
                        int claveOrden = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Ord"].ToString());
                        int claveProducto = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString());
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        Producto producto = null;
                        new CN_CatProducto().ConsultaProducto_OrdenCompra(ref producto, sesion.Emp_Cnx, claveOrden, claveProducto, sesion.Id_Emp, sesion.Id_Cd_Ver);
                        ((HiddenField)editItem.FindControl("HD_Prd_UniEmp")).Value = producto.Prd_UniEmp.ToString();
                        //-------------------------------

                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto
                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = false;
                        //es registro de edición, se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                        Ctrl_txtOrd_Cantidad.Enabled = true;
                    }
                }
                /*
                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_PrdN"].FindControl("txtId_Prd");
                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form["Ord_Cantidad"].FindControl("txtOrd_Cantidad");
                    }
                    dataField.Focus();
                }*/
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgOrdCompra_ItemDataBound"));
            }
        }
        protected void rgOrdCompra_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                OrdenCompraDet ordenCompraDet = new OrdenCompraDet();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)insertedItem.FindControl("txtOrd_Cantidad");
                string txtOrd_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                string txtId_Prd = ((RadNumericTextBox)insertedItem.FindControl("txtId_Prd")).ClientID.ToString();//txtId_Prd

                if (Ctrl_txtOrd_Cantidad.Enabled)
                    if (Ctrl_txtOrd_Cantidad.Value.HasValue)
                    {
                        if (Ctrl_txtOrd_Cantidad.Value.Value == 0)
                        {
                            AlertaFocus("La cantidad ordenada es requerida", (insertedItem["Ord_Cantidad"].FindControl("txtOrd_Cantidad") as RadNumericTextBox).ClientID);
                            e.Canceled = true;
                            return;
                        }
                    }
                    else
                    {
                        AlertaFocus("La cantidad ordenada es requerida", (insertedItem["Ord_Cantidad"].FindControl("txtOrd_Cantidad") as RadNumericTextBox).ClientID);
                        e.Canceled = true;
                        return;
                    }
                ordenCompraDet.Id_Emp = sesion.Id_Emp;
                ordenCompraDet.Id_Cd = sesion.Id_Cd_Ver;
                ordenCompraDet.Id_Ord = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                ordenCompraDet.Id_OrdDet = 0; //identity
                ordenCompraDet.Id_Prd = Convert.ToInt32((insertedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                ordenCompraDet.Ord_Cantidad = !string.IsNullOrEmpty((insertedItem["Ord_Cantidad"].FindControl("txtOrd_Cantidad") as RadNumericTextBox).Text) ? Convert.ToInt32((insertedItem["Ord_Cantidad"].FindControl("txtOrd_Cantidad") as RadNumericTextBox).Text) : 0;
                ordenCompraDet.Ord_CantidadGen = Convert.ToInt32((insertedItem["Ord_CantidadGen"].FindControl("txtPrd_CantGen") as RadNumericTextBox).Text);

                if (ordenCompraDet.Id_Prd == 0)
                {
                    AlertaFocus("El producto es requerido", (insertedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }
                double unidadEmpaque = Convert.ToDouble(((HiddenField)insertedItem.FindControl("HD_Prd_UniEmp")).Value);
                if (unidadEmpaque != 0 && Ctrl_txtOrd_Cantidad.Value.HasValue)
                {
                    //    if ((Ctrl_txtOrd_Cantidad.Value.Value % unidadEmpaque) != 0)
                    //    {
                    //        AlertaFocus("Favor de capturar m&uacute;ltiplos seg&uacute;n las unidades de empaque configuradas en el cat&aacute;logo de productos. Las unidades de empaque para este producto son " + unidadEmpaque.ToString(), (insertedItem.FindControl("txtOrd_Cantidad") as RadNumericTextBox).ClientID);
                    //        e.Canceled = true;
                    //        return;
                    //    }

                    if (unidadEmpaque == 0)
                        unidadEmpaque = 1;

                    if (Ctrl_txtOrd_Cantidad.Value.Value % unidadEmpaque != 0)
                    {
                        while (Ctrl_txtOrd_Cantidad.Value.Value % unidadEmpaque != 0)
                        {
                            Ctrl_txtOrd_Cantidad.Value += 1;
                        }
                    }

                }


                float Prd_Precio = Convert.ToSingle(((HiddenField)insertedItem.FindControl("HD_Prd_PrecioAAA")).Value);
                //datos del producto de la orden de compra
                ordenCompraDet.Producto = new Producto();
                ordenCompraDet.Producto.Id_Prd = Convert.ToInt32((insertedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                ordenCompraDet.Producto.Id_Emp = sesion.Id_Emp;
                ordenCompraDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                ordenCompraDet.Producto.Prd_Descripcion = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                ordenCompraDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                ordenCompraDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;
                ordenCompraDet.ProductoPrecio = new ProductoPrecios();

                ordenCompraDet.ProductoPrecio.Prd_Pesos = Prd_Precio;

                //agregar producto de orden de compra a la lista
                this.ListaProductosOrdenCompra_AgregarProducto(ordenCompraDet);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgOrdCompra_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgOrdCompra_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                OrdenCompraDet ordenCompraDet = new OrdenCompraDet();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editedItem.FindControl("txtOrd_Cantidad");
                string txtOrd_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                string txtId_Prd = ((RadNumericTextBox)editedItem.FindControl("txtId_Prd")).ClientID.ToString();//txtId_Prd

                if (Ctrl_txtOrd_Cantidad.Enabled)
                    if (Ctrl_txtOrd_Cantidad.Value.HasValue)
                    {
                        if (Ctrl_txtOrd_Cantidad.Value.Value == 0)
                        {
                            AlertaFocus("La cantidad ordenada es requerida", (editedItem["Ord_Cantidad"].FindControl("txtOrd_Cantidad") as RadNumericTextBox).ClientID);
                            e.Canceled = true;
                            return;
                        }
                    }
                    else
                    {
                        AlertaFocus("La cantidad ordenada es requerida", (editedItem["Ord_Cantidad"].FindControl("txtOrd_Cantidad") as RadNumericTextBox).ClientID);
                        e.Canceled = true;
                        return;
                    }
                ordenCompraDet.Id_Emp = sesion.Id_Emp;
                ordenCompraDet.Id_Cd = sesion.Id_Cd_Ver;
                ordenCompraDet.Id_Ord = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                ordenCompraDet.Id_OrdDet = 0; //identity
                ordenCompraDet.Id_Prd = Convert.ToInt32((editedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                ordenCompraDet.Ord_Cantidad = !string.IsNullOrEmpty((editedItem["Ord_Cantidad"].FindControl("txtOrd_Cantidad") as RadNumericTextBox).Text) ? Convert.ToInt32((editedItem["Ord_Cantidad"].FindControl("txtOrd_Cantidad") as RadNumericTextBox).Text) : 0;
                ordenCompraDet.Ord_CantidadGen = Convert.ToInt32((editedItem["Ord_CantidadGen"].FindControl("txtPrd_CantGen") as RadNumericTextBox).Text);

                if (ordenCompraDet.Id_Prd == 0)
                {
                    AlertaFocus("El producto es requerido", (editedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }
                double unidadEmpaque = Convert.ToDouble(((HiddenField)editedItem.FindControl("HD_Prd_UniEmp")).Value);


                if (unidadEmpaque == 0)
                    unidadEmpaque = 1;

                if (unidadEmpaque != 0 && Ctrl_txtOrd_Cantidad.Value.HasValue)
                {
                    if ((Ctrl_txtOrd_Cantidad.Value.Value % unidadEmpaque) != 0)
                    {
                        AlertaFocus("Favor de capturar m&uacute;ltiplos seg&uacute;n las unidades de empaque configuradas en el cat&aacute;logo de productos. Las unidades de empaque para este producto son " + unidadEmpaque.ToString(), (editedItem.FindControl("txtOrd_Cantidad") as RadNumericTextBox).ClientID);
                        e.Canceled = true;
                        return;
                    }
                }
                //datos del producto de la orden de compra
                ordenCompraDet.Producto = new Producto();
                ordenCompraDet.Producto.Id_Prd = Convert.ToInt32((editedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                ordenCompraDet.Producto.Id_Emp = sesion.Id_Emp;
                ordenCompraDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                ordenCompraDet.Producto.Prd_Descripcion = (editedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                ordenCompraDet.Producto.Prd_Presentacion = (editedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                ordenCompraDet.Producto.Prd_UniNe = (editedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;

                //actualizar producto de orden de compra a la lista
                this.ListaProductosOrdenCompra_ModificarProducto(ordenCompraDet);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgOrdCompra_update_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgOrdCompra_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                //actualizar producto de orden de compra a la lista
                this.ListaProductosOrdenCompra_EliminarProducto(id_Prd);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgOrdCompra_delete_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgOrdCompra_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgOrdCompra.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void txtProducto_TextChanged(object sender, EventArgs e)//protected void cmbProducto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                RadTextBox txtPrd_Descripcion = (RadTextBox)tabla.FindControl("txtPrd_Descripcion");
                RadTextBox txtPrd_Presentacion = (RadTextBox)tabla.FindControl("txtPrd_Presentacion");
                RadTextBox txtPrd_UniNe = (RadTextBox)tabla.FindControl("txtPrd_UniNe");
                RadNumericTextBox txtPrd_CantGen = (RadNumericTextBox)tabla.FindControl("txtPrd_CantGen");
                RadNumericTextBox txtOrd_Cantidad = (RadNumericTextBox)tabla.FindControl("txtOrd_Cantidad");
                RadNumericTextBox txtId_Prd = (RadNumericTextBox)tabla.FindControl("txtId_Prd");//txtId_Prd
                txtOrd_Cantidad.Enabled = true;
                Producto producto = null;
                if (txtId_Prd.Value.ToString() != string.Empty && txtId_Prd.Value.ToString() != "-1")
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_CatProducto clsProducto = new CN_CatProducto();
                    clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, Convert.ToInt32(txtId_Prd.Value.ToString()),0);
                }

                if (producto.Id_Pvd != Convert.ToInt32(cmbProveedor.SelectedValue))
                {
                    Alerta("La Orden de Compra tiene el Proveedor # " + Convert.ToString(cmbProveedor.SelectedValue) + " y el Producto tiene el Proveedor # " + Convert.ToString(producto.Id_Pvd) + "; Imposible Capturar un Producto de un Proveedor diferente al de la Ordene de Compra.");
                    txtId_Prd.Focus();
                }
                else
                {


                    txtPrd_Descripcion.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
                    txtPrd_Presentacion.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                    txtPrd_UniNe.Text = producto == null ? string.Empty : producto.Prd_UniNe;
                    txtPrd_CantGen.Text = "0";
                    ((HiddenField)tabla.FindControl("HD_Prd_PrecioAAA")).Value = producto == null ? string.Empty : producto.Prd_Precio.ToString();

                    //--------controles auxiliares--------
                    //establecer unidades de empaque
                    ((HiddenField)tabla.FindControl("HD_Prd_UniEmp")).Value = producto == null ? string.Empty : producto.Prd_UniEmp.ToString();

                    //este evento es porque se elige producto, por lo que 
                    //se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque

                    txtOrd_Cantidad.Text = string.Empty;
                    txtOrd_Cantidad.Focus();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbProducto_IndexChanging_error"));
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)//protected void cmbProducto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                string lbl_cmbProducto = ((Label)tabla.FindControl("lbl_cmbProducto")).ClientID.ToString();
                RadTextBox txtPrd_Descripcion = (RadTextBox)tabla.FindControl("txtPrd_Descripcion");
                RadTextBox txtPrd_Presentacion = (RadTextBox)tabla.FindControl("txtPrd_Presentacion");
                RadTextBox txtPrd_UniNe = (RadTextBox)tabla.FindControl("txtPrd_UniNe");
                RadNumericTextBox txtPrd_CantGen = (RadNumericTextBox)tabla.FindControl("txtPrd_CantGen");
                RadNumericTextBox txtId_Prd = (RadNumericTextBox)tabla.FindControl("txtId_Prd");//txtId_Prd
                string HD_Prd_UniEmp = ((HiddenField)tabla.FindControl("HD_Prd_UniEmp")).ClientID.ToString();
                double unidadEmpaque = Convert.ToDouble(((HiddenField)tabla.FindControl("HD_Prd_UniEmp")).Value);

                RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)tabla.FindControl("txtOrd_Cantidad");
                string txtOrd_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();//txtOrd_Cantidad
                string lblOrd_Cantidad = ((Label)tabla.FindControl("lblVal_txtOrd_Cantidad")).ClientID.ToString();

                if (!Ctrl_txtOrd_Cantidad.Value.HasValue)
                    AlertaFocus("La cantidad ordenada es requerida", (tabla.FindControl("txtOrd_Cantidad") as RadNumericTextBox).ClientID);
                if (Ctrl_txtOrd_Cantidad.Value.HasValue)
                    if (Ctrl_txtOrd_Cantidad.Value <= 0)
                        AlertaFocus("La cantidad ordenada debe ser mayor a 0", (tabla.FindControl("txtOrd_Cantidad") as RadNumericTextBox).ClientID);

                //if (unidadEmpaque != 0 && Ctrl_txtOrd_Cantidad.Value.HasValue)
                //{
                //    if ((Ctrl_txtOrd_Cantidad.Value.Value % unidadEmpaque) != 0)
                //    {
                //        AlertaFocus("Favor de capturar m&uacute;ltiplos seg&uacute;n las unidades de empaque configuradas en el cat&aacute;logo de productos. Las unidades de empaque para este producto son " + unidadEmpaque.ToString(), Ctrl_txtOrd_Cantidad.ClientID);
                //    }
                //}
                if (unidadEmpaque == 0)
                    unidadEmpaque = 1;

                if (Ctrl_txtOrd_Cantidad.Value.Value % unidadEmpaque != 0)
                {
                    while (Ctrl_txtOrd_Cantidad.Value.Value % unidadEmpaque != 0)
                    {
                        Ctrl_txtOrd_Cantidad.Value += 1;
                    }
                }
                #region comentarizado
                //string jsControles = string.Concat(
                //        "lbl_cmbProductoClientId='", lbl_cmbProducto, "';"
                //        , "cmbProductoClientId='", txtPrd_Descripcion, "';"
                //        , "lblVal_txtOrd_CantidadClientId='", lblOrd_Cantidad, "';"
                //        , "txtOrd_CantidadClientId='", txtOrd_Cantidad, "';"
                //        , "HD_Prd_UniEmpClientId='", HD_Prd_UniEmp, "';"
                //        );

                //Llenar combo de productos
                //RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                //comboProductoItem.DataSource = this.ListaProductos;
                //comboProductoItem.DataBind();
                //comboProductoItem.SelectedIndex = 0;

                //ImageButton insertbtn = (ImageButton)tabla.FindControl("PerformInsertButton");
                //if (insertbtn != null)
                //{
                //cuando la edición se usa para inserción, se habilita el combo de producto
                //((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = true;
                //es registro nuevo y se inhabilita el campo de cantidad (se habilita hasta ke elija un producto del combo)
                // Ctrl_txtOrd_Cantidad.Enabled = false;

                //jsControles = string.Concat(
                //    jsControles
                //    , "return ValidaFormEdit(\"insertar\");");

                //insertbtn.Attributes.Add("onclick", jsControles);
                //}
                #endregion
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "txtCantidad_IndexChanging_error"));
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
                    case "new":
                        this.Nuevo();
                        break;

                    case "save":
                        mensajeError = hiddenId.Value == string.Empty ? "CapOrdCompra_insert_error" : "CapOrdCompra_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void cmbOrdCompra_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.LLenarFormOrdCompra(Convert.ToInt32(e.Value));
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat("CapOrdCompra_LlenarForm_error", ex.Message));
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                if (e.Item.Value == "-1")
                {
                    e.Item.FindControl("liComprasLocales").Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "cmbProductosLista_ItemsDataBound"));
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtProveedor.Text != "")
                {
                    RAM1.ResponseScripts.Add("popup('" + txtProveedor.Text + "');");
                }
                else
                {
                    Alerta("No se ha seleccionado un proveedor");
                }
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
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "cliente":
                        ((RadNumericTextBox)producto).Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtProducto_TextChanged(producto, null);
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 120);
                        RPVDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RPVGenerales.Width;
                        RadSplitter1.Height = altura;
                        RPVGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RPVGenerales.Width;
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
        private void CargarComboTipoModeda()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatTipoMoneda_Combo", ref cmbMoneda);
                this.cmbMoneda.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Funciones
        private void HabilitaBotonesToolBar(bool nuevo, bool guardar, bool regresar, bool eliminar, bool imprimir, bool correo)
        {
            try
            {
                this.RadToolBar1.Items[6].Visible = nuevo;

                this.RadToolBar1.Items[5].Visible = guardar;
                if (guardar)
                    if (_PermisoGuardar == false & _PermisoModificar == false)
                    {
                        this.RadToolBar1.Items[5].Visible = false;
                    }
                //Regresar
                this.RadToolBar1.Items[4].Visible = regresar;
                //Eliminar
                this.RadToolBar1.Items[3].Visible = false; //eliminar;
                //Imprimir
                this.RadToolBar1.Items[2].Visible = false; //imprimir;
                //Correo
                this.RadToolBar1.Items[1].Visible = false; //correo;
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
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapOrdCompra", "Id_Ord", sesion.Emp_Cnx, "spCatLocal_Maximo");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            this.HabilitaControles(true);

            cmbTipo.SelectedIndex = 1;
            rgOrdCompra.DataSource = this.ListaProductosOrdenCompra = new List<OrdenCompraDet>();
            rgOrdCompra.DataBind();

            txtFecha.Focus();
        }
        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                List<OrdenCompraDet> ListaOrdenautorizar = new List<OrdenCompraDet>();
                CN_CapOrdenCompra clsCapOrdCompra = new CN_CapOrdenCompra();
                int verificador = -1;
                int VtaPromedio = 0;
                OrdenCompra ordCompra = this.LlenarObjetoOrdCompra();
                ordCompra.ListOrdenCompra = this.ListaProductosOrdenCompra;
                List<OrdenCompraDet> listaPartida = new List<OrdenCompraDet>();
                List<OrdenCompraDet> listaPartidanoaceptado = new List<OrdenCompraDet>();
                GetList();
                // 
                // CalcularTotal(ListaProductosOrdenCompra);

                if (ordCompra.ListOrdenCompra == null || ordCompra.ListOrdenCompra.Count == 0)
                {
                    DisplayMensajeAlerta("OrdenCompraSinProductos");
                    return;
                }

                if (hiddenId.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        DisplayMensajeAlerta("PermisoGuardarNo");
                        return;
                    }
                    foreach (OrdenCompraDet Partida in ordCompra.ListOrdenCompra)
                    {
                        DataRow[] Prod = listaPartidas.Select("Id_Prd =" + Partida.Id_Prd);
                        VtaPromedio = 0;
                        if (Prod.Count() > 0)
                        {
                            if (Partida.Id_Prd > 999000000)
                            {
                                int prod = Partida.Id_Prd - 999000000;
                                DataRow[] Prod2 = listaPartidas.Select("Id_Prd =" + Partida.Id_Prd);
                                if (Prod.Count() > 0)
                                {
                                    foreach (DataRow Row in listaPartidas.Select("Id_Prd In(" + Partida.Id_Prd + "," + prod + ")"))
                                    {
                                        VtaPromedio = VtaPromedio + int.Parse(Row["ventaPromedio"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                int prod2 = Partida.Id_Prd + 999000000;
                                DataRow[] Prod3 = listaPartidas.Select("Id_Prd =" + prod2);
                                if (Prod.Count() > 0)
                                {
                                    foreach (DataRow Row in listaPartidas.Select("Id_Prd In(" + Partida.Id_Prd + "," + prod2 + ")"))
                                    {
                                        VtaPromedio = VtaPromedio + int.Parse(Row["ventaPromedio"].ToString());
                                    }
                                    //OrdenCompraDet Partida2 = new OrdenCompraDet();
                                    //Partida2 = Partida;
                                    ////se valida que sea un sistema propietario y que el solicitado supera el promedio de venta 
                                    //if (int.Parse(Row["Id_Ptp"].ToString()) == 1 && int.Parse(Row["ventaPromedio"].ToString()) < Partida.Ord_Cantidad)
                                    //    listaPartidanoaceptado.Add(Partida2);
                                    //else
                                    //    listaPartida.Add(Partida2);
                                }
                            }
                            foreach (DataRow Row in listaPartidas.Select("Id_Prd =" + Partida.Id_Prd))
                            {
                                OrdenCompraDet Partida2 = new OrdenCompraDet();
                                Partida2 = Partida;
                                //se valida que sea un sistema propietario y que el solicitado supera el promedio de venta 
                                if (int.Parse(Row["Id_Ptp"].ToString()) == 1 && VtaPromedio < Partida.Ord_Cantidad)
                                    listaPartidanoaceptado.Add(Partida2);
                                else
                                    listaPartida.Add(Partida2);
                            }
                        }
                        else
                        {
                            OrdenCompraDet Partida3 = new OrdenCompraDet();
                            Partida3 = Partida;
                            if (Partida3.Id_Ptp == 1)
                                listaPartidanoaceptado.Add(Partida3);
                            else
                                listaPartida.Add(Partida3);
                        }
                    }
                    if (listaPartida.Count > 0)
                    {
                        ordCompra.ListOrdenCompra = listaPartida;
                        clsCapOrdCompra.InsertarOrdenCompra(ref ordCompra, session.Emp_Cnx, ref verificador);
                    }
                    if (listaPartidanoaceptado.Count > 0)
                    {
                        int Partidasnoaceptadas = 1;
                        ordCompra.ListOrdenCompra = listaPartidanoaceptado;
                        clsCapOrdCompra.InsertarOrdenCompra(ref ordCompra, session.Emp_Cnx, ref verificador, Partidasnoaceptadas);
                        hiddenId.Value = this.txtFolio.Text = ordCompra.Id_Ord.ToString();
                        this.HabilitaBotonesToolBar(false, true, false, true, true, true);
                        InsertarOrdenPorAutorizar(ordCompra.Id_Ord, listaPartidanoaceptado);
                        EnviaEmail(ordCompra.Id_Ord);
                        RAM1.ResponseScripts.Add("CloseAlert('Se genero la orden de compra. Esta orden de compra quedará pendiente de autorización.');");
                    }
                    else
                    {
                        hiddenId.Value = this.txtFolio.Text = ordCompra.Id_Ord.ToString();
                        this.HabilitaBotonesToolBar(false, true, false, true, true, true);
                        RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");
                    }
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        DisplayMensajeAlerta("PermisoModificarNo");
                        return;
                    }

                    clsCapOrdCompra.ModificarOrdenCompra(ordCompra, session.Emp_Cnx, ref verificador);
                    this.HabilitaBotonesToolBar(false, true, false, true, true, true);

                    string mensaje = "Los datos se modificaron correctamente";
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de detalle de orden de compra
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
              
        }

        private DataTable GetList()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int productoInicial = -1;
            int productoFinal = -1;

            DataTable dtPartidasOrdenAutomatica = null;
            new CN_CapOrdenCompra().GeneraOrdenCompraAutomatica(sesion.Emp_Cnx, ref dtPartidasOrdenAutomatica, "tabla", sesion.Id_Emp, sesion.Id_Cd_Ver, 100, productoInicial, productoFinal, true, null);

            float ventaMes1 = 0, ventaMes2 = 0, ventaMes3 = 0, ventaMes0 = 0;
            int Prd_MaxExistencia = 0, existencia = 0, Id_Ptp = 0;
            foreach (DataRow row in dtPartidasOrdenAutomatica.Rows)
            {
                //this.VentaMes0Desc = row["ventaMes0Desc"].ToString();
                //this.VentaMes1Desc = row["ventaMes1Desc"].ToString();
                //this.VentaMes2Desc = row["ventaMes2Desc"].ToString();
                //this.VentaMes3Desc = row["ventaMes3Desc"].ToString();

                Prd_MaxExistencia = Convert.ToInt32(row["Prd_MaxExistencia"]);
                existencia = Convert.ToInt32(row["existencia"]);
                Id_Ptp = Convert.ToInt32(row["Id_Ptp"]);
                //calcular promedio de venta
                ventaMes0 = Convert.ToSingle(row["ventaMes0"]);
                ventaMes1 = Convert.ToSingle(row["ventaMes1"]);
                ventaMes2 = Convert.ToSingle(row["ventaMes2"]);
                ventaMes3 = Convert.ToSingle(row["ventaMes3"]);
                row["ventaPromedio"] = (ventaMes1 + ventaMes2 + ventaMes3 + ventaMes0) / 4;
                if (Convert.ToInt32(row["ordenado"]) != 0)
                {
                    int Prd_UniEmp = 0;
                    int ordenado = 0;
                    if (Convert.ToInt32(row["Prd_UniEmp"]) == 0)
                    {
                        Prd_UniEmp = 1;
                    }
                    else
                    {
                        Prd_UniEmp = Convert.ToInt32(row["Prd_UniEmp"]);
                    }
                    ordenado = Convert.ToInt32(row["Ordenado"]);

                    if (ordenado % Prd_UniEmp != 0)
                    {
                        while (ordenado % Prd_UniEmp != 0)
                        {
                            ordenado += 1;
                        }
                    }
                    row["ordenado"] = ordenado;
                }
            }
            Session["listaPartidas" + Session.SessionID] = dtPartidasOrdenAutomatica;
            return listaPartidas;
        }

        private void InsertarOrdenPorAutorizar(int Id_OrdCompra, List<OrdenCompraDet> listaPartidanoaceptado)
        {
            try
            {
                int verificador = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<OrdenCompraDet> listaPartidaTemp = new List<OrdenCompraDet>();
                List<AutorizaOrdenCom> listaporAutorizar = new List<AutorizaOrdenCom>();
                AutorizaOrdenCom ordenporautorizar;
                foreach (OrdenCompraDet Detalle in listaPartidanoaceptado)
                {
                    DataRow[] Prod = listaPartidas.Select("Id_Prd =" + Detalle.Id_Prd);
                    if (Prod.Count() > 0)
                    {
                        foreach (DataRow row in this.listaPartidas.Select("Id_Prd =" + Detalle.Id_Prd))
                        {
                            ordenporautorizar = new AutorizaOrdenCom();
                            ordenporautorizar.Id_OrdCompra = Id_OrdCompra;
                            ordenporautorizar.Id_Prd = Convert.ToInt32(row["Id_Prd"]);
                            ordenporautorizar.Prd_Nom = row["Prd_Descripcion"].ToString();
                            ordenporautorizar.Prd_Presentacion = row["Prd_Presentacion"].ToString();
                            ordenporautorizar.Vta3 = Convert.ToInt32(row["VentaMes3"]);
                            ordenporautorizar.Vta2 = Convert.ToInt32(row["VentaMes2"]);
                            ordenporautorizar.Vta1 = Convert.ToInt32(row["VentaMes1"]);
                            ordenporautorizar.Vta0 = Convert.ToInt32(row["VentaMes0"]);
                            ordenporautorizar.Promedio = Convert.ToInt32(row["ventaPromedio"]);
                            ordenporautorizar.Maximo = Convert.ToInt32(row["Prd_MaxExistencia"]);
                            ordenporautorizar.Ordenado = Convert.ToInt32(Detalle.Ord_Cantidad);
                            ordenporautorizar.Id_U = sesion.Id_U;
                            ordenporautorizar.Pendiente = Convert.ToInt32(Detalle.Ord_Cantidad);

                            listaporAutorizar.Add(ordenporautorizar);
                        }
                    }
                    else
                    {
                        ordenporautorizar = new AutorizaOrdenCom();
                        ordenporautorizar.Id_OrdCompra = Id_OrdCompra;
                        ordenporautorizar.Id_Prd = Convert.ToInt32(Detalle.Id_Prd);
                        ordenporautorizar.Prd_Nom = Detalle.Producto.Prd_Descripcion;
                        ordenporautorizar.Prd_Presentacion = Detalle.Producto.Prd_Presentacion;
                        ordenporautorizar.Vta3 = 0;
                        ordenporautorizar.Vta2 = 0;
                        ordenporautorizar.Vta1 = 0;
                        ordenporautorizar.Vta0 = 0;
                        ordenporautorizar.Promedio = 0;
                        ordenporautorizar.Maximo = 0;
                        ordenporautorizar.Ordenado = Convert.ToInt32(Detalle.Ord_Cantidad);
                        ordenporautorizar.Id_U = sesion.Id_U;
                        ordenporautorizar.Pendiente = Convert.ToInt32(Detalle.Ord_Cantidad);

                        listaporAutorizar.Add(ordenporautorizar);
                    }


                }
                new CN_CapOrdenCompra().InsertarOrdCompraAutoriza(listaporAutorizar, sesion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnviaEmail(int ordCompra)
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

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de una orden de compra con el folio  " + ordCompra);
                cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Centro de distribución:  " + session.Id_Cd_Ver + " - " + session.Cd_Nombre);
                cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Solicitó : " + session.Id_U + " - " + session.U_Nombre);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProOrdenCompra_Autorizacion.aspx?Id=" + ordCompra + "&Accion=2&email=1&PermisoGuardar=true&PermisoModificar=true&PermisoEliminar=true&PermisoImprimir=true'" + ">");
                cuerpo_correo.Append("Solicitud de autorización de ordenes de compra</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(configuracion.Mail_OrdenCompra_sisprop));
                m.Subject = "Solicitud de autorización de Orden de Compra #" + ordCompra + " del centro " + session.Id_Cd_Ver;
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
                try
                {
                    sm.Send(m);
                }
                catch (Exception)
                {
                    //CambiarEstatus(ordCompra, "C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                rgOrdCompra.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private OrdenCompra LlenarObjetoOrdCompra()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            OrdenCompra ordCompra = new OrdenCompra();

            ordCompra.Id_Emp = sesion.Id_Emp;
            ordCompra.Id_Cd = sesion.Id_Cd_Ver;
            ordCompra.Id_Ord = Convert.ToInt32(txtFolio.Text);
            ordCompra.Id_Pvd = Convert.ToInt32(cmbProveedor.SelectedValue);
            ordCompra.Id_U = sesion.Id_U;
            //cuando se da de alta por el usuario el estatus siempre es M = Manual
            ordCompra.Ord_Estatus = "C"; //cmbEstatus.SelectedValue; //M = Manual, A = Automático

            CapaDatos.Funciones funciones = new CapaDatos.Funciones();
            DateTime fechaServidor = funciones.GetLocalDateTime(sesion.Minutos);
            DateTime fechaDatePicker = Convert.ToDateTime(txtFecha.SelectedDate);
            DateTime fechaDatePickerEntrega = Convert.ToDateTime(txtFechaEntrega.SelectedDate);

            ordCompra.Ord_Fecha = new DateTime(fechaDatePicker.Year, fechaDatePicker.Month, fechaDatePicker.Day, fechaServidor.Hour, fechaServidor.Minute, fechaServidor.Second);
            ordCompra.Ord_Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
            ordCompra.Ord_Notas = txtNotas.Text;
            ordCompra.Id_Mon = Convert.ToInt32(txtMoneda.Text);
            ordCompra.Ord_Fecha_Entrega = new DateTime(fechaDatePickerEntrega.Year, fechaDatePickerEntrega.Month, fechaDatePickerEntrega.Day, fechaServidor.Hour, fechaServidor.Minute, fechaServidor.Second);

            return ordCompra;
        }
        private void LLenarFormOrdCompra(int Id_Ord)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            OrdenCompra ordCompra = new OrdenCompra();
            ordCompra.Id_Emp = sesion.Id_Emp;
            ordCompra.Id_Cd = sesion.Id_Cd_Ver;
            ordCompra.Id_Ord = Id_Ord;

            new CN_CapOrdenCompra().ConsultaOrdenCompra(ref ordCompra, sesion.Emp_Cnx);

            txtFolio.Text = ordCompra.Id_Ord.ToString();
            this.hiddenId.Value = Id_Ord.ToString();
            txtProveedor.Text = ordCompra.Id_Pvd == -1 ? string.Empty : ordCompra.Id_Pvd.ToString();
            cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue(ordCompra.Id_Pvd.ToString());
            txtFecha.SelectedDate = ordCompra.Ord_Fecha;
            cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(ordCompra.Ord_Tipo.ToString()); //1 = Manual, 2 = Automático
            txtNotas.Text = ordCompra.Ord_Notas;
            HD_ordenCompraEstatus.Value = ordCompra.Ord_Estatus;

            //llenar grid de detalle de orden de compra
            List<OrdenCompraDet> listaOrdCompraDet = new List<OrdenCompraDet>();
            OrdenCompraDet ordenCompraDet = new OrdenCompraDet();

            ordenCompraDet.Id_Emp = sesion.Id_Emp;
            ordenCompraDet.Id_Cd = sesion.Id_Cd_Ver;
            ordenCompraDet.Id_Ord = Id_Ord;
            new CN_CapOrdenCompraDet().ConsultaOrdenCompraDetalle_Lista(ordenCompraDet, sesion.Emp_Cnx, ref listaOrdCompraDet);
            rgOrdCompra.Enabled = true;
            rgOrdCompra.DataSource = listaOrdCompraDet;
            rgOrdCompra.DataBind();

            //asignar lista de productos de orden de comptra
            this.ListaProductosOrdenCompra = listaOrdCompraDet;

            //habilita/deshabilita controles de la orden consultada
            this.ValidarModificacionOrdenCompra(ordCompra.Ord_Estatus);

            CalcularTotal(listaOrdCompraDet);
        }
        /// <summary>
        /// Habilita o deshabilita controles dependiendo si la orden se puede modificar o no
        /// </summary>
        /// <param name="estatus"> Estatus de la orden consultada
        /// C = Capturada
        /// B = Baja
        /// I = Impresa
        /// </param>
        private void ValidarModificacionOrdenCompra(string estatus)
        {
            if (estatus == "B") //si el estatus es baja o impreso los controles se deshabilitan
            {
                this.HabilitaControles(false);
                this.HabilitaBotonesToolBar(false, false, false, false, false, false);
            }
            else
                if (estatus == "I") //si el estatus es baja o impreso los controles se deshabilitan
                {
                    this.HabilitaControles(false);
                    this.HabilitaBotonesToolBar(false, false, false, false, true, false);
                }
                else
                {
                    this.HabilitaControles(true);
                    this.HabilitaBotonesToolBar(false, true, false, true, true, true);
                }
        }
        /// <summary>
        /// Habilita o deshabilita controles
        /// </summary>
        private void HabilitaControles(bool habilitar)
        {
            txtProveedor.Enabled = habilitar;
            cmbProveedor.Enabled = habilitar;
            if (habilitar)
            {
                rgOrdCompra.Enabled = habilitar;
            }
            txtNotas.Enabled = habilitar;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = habilitar;
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
        private void LlenarComboProveedores()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spProveedores_Combo", ref cmbProveedor);
            //this.cmbProveedor.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("OrdenCompraSinProductos"))
                    Alerta("No se han agregado productos a la orden de compra");
                else
                    if (mensaje.Contains("rgOrdCompra_insert_repetida"))
                        Alerta("Este producto ya est&aacute; agregado en la orden de compra");
                    else
                        if (mensaje.Contains("rgOrdCompra_insert_error"))
                            Alerta("Error al momento de agregar el producto a la lista de productos de la orden de compra");
                        else
                            if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                Alerta("Error al consultar los datos de producto");
                            else
                                if (mensaje.Contains("txtCantidad_IndexChanging_error"))
                                    Alerta("Error al insertar la cantidad del producto");
                                else
                                    if (mensaje.Contains("rgOrdCompra_ItemDataBound"))
                                        Alerta("Error al momento de preparar un registro para edición");
                                    else
                                        if (mensaje.Contains("CapOrdCompraDetalle_consulta_error"))
                                            Alerta("Error al consultar el detalle de la orden de compra");
                                        else
                                            if (mensaje.Contains("CapOrdCompraDetalle_insert_error"))
                                                Alerta("Error al guardar el detalle de la orden de compra");
                                            else
                                                if (mensaje.Contains("rgOrdCompra_NeedDataSource"))
                                                    Alerta("Error al cargar el grid de detalle de orden de compra");
                                                else
                                                    if (mensaje.Contains("rgOrdCompra_ItemCommand"))
                                                        Alerta("Error al ejecutar un evento (rgOrdCompra_ItemCommand) al cargar el grid de detalle de orden de compra");
                                                    else
                                                        if (mensaje.Contains("rgOrdCompra_Actualizar_ok"))
                                                            Alerta("El producto de la orden de compra fue actualizado correctamente");
                                                        else
                                                            if (mensaje.Contains("rgOrdCompra_Actualizar_error"))
                                                                Alerta("Error al actualizar el producto de la orden de compra");
                                                            else
                                                                if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                                                    Alerta("Error al cambiar de centro de distribución");
                                                                else
                                                                    if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                        Alerta("Error al cambiar de página");
                                                                    else
                                                                        if (mensaje.Contains("CapOrdCompra_LlenarForm_error"))
                                                                            Alerta("Error al momento de consultar los datos de la orden de compra");
                                                                        else
                                                                            if (mensaje.Contains("PermisoGuardarNo"))
                                                                                Alerta("No tiene permisos para grabar");
                                                                            else
                                                                                if (mensaje.Contains("PermisoModificarNo"))
                                                                                    Alerta("No tiene permisos para actualizar");
                                                                                else
                                                                                    if (mensaje.Contains("CapOrdCompraDetalle_delete_error"))
                                                                                        Alerta("Error al momento de eliminar el detalle de la orden de compra");
                                                                                    else
                                                                                        if (mensaje.Contains("CapOrdCompra_insert_ok"))
                                                                                            Alerta("Los datos se guardaron correctamente");
                                                                                        else
                                                                                            if (mensaje.Contains("CapOrdCompra_insert_error"))
                                                                                                Alerta("No se pudo guardar los datos de la orden de compra");
                                                                                            else
                                                                                                if (mensaje.Contains("CapOrdCompra_update_ok"))
                                                                                                    Alerta("Los datos se modificaron correctamente");
                                                                                                else
                                                                                                    if (mensaje.Contains("CapOrdCompra_update_error"))
                                                                                                        Alerta("No se pudo actualizar los datos de la orden de compra");
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
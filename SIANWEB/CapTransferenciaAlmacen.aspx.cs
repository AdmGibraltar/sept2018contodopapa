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

namespace SIANWEB
{
    public partial class CapTransferenciaAlmacen : System.Web.UI.Page
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
        private List<TransferenciaAlmacenDet> ListaProductosTransferenciaAlmacen
        {
            get { return (List<TransferenciaAlmacenDet>)Session["ListaProductosTransferencia"]; }
            set { Session["ListaProductosTransferencia"] = value; }
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
                  

                    //obtener valores desde la URL
                    int Id_Trans = Convert.ToInt32(Page.Request.QueryString["Id_Trans"]);
                    _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                    _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                    _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                    _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;

                    //establece valores de controles de inicio
                    if (Id_Trans > 0)
                        this.LLenarFormTransferenciaAlmacen(Id_Trans);
                    else
                    {
                        this.hiddenId.Value = string.Empty;
                        this.txtFecha.SelectedDate = DateTime.Now;
                        //this.rgTransferenciaAlmacen.Enabled = false;
                        this.txtFecha.Focus();
                        //nueva variable para controlar tabla dinamica de productos de orden de compra
                        Session["ListaProductosTransferenciaAlmacen"] = new List<TransferenciaAlmacenDet>();
                        this.Nuevo();
                        this.txtFolio.Text = this.Valor;
                        this.HabilitaBotonesToolBar(false, true, false, false, false, false);
                    }

                    
                        deshabilitarcontroles(divPrincipal.Controls);
                        //deshabilitarcontroles(formularioTotales.Controls);
                        GridCommandItem cmdItem = (GridCommandItem)rgTransferenciaAlmacen.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                        ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 
                        ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  

                        
                        

                        rgTransferenciaAlmacen.Columns.FindByUniqueName("Prd_Descripcion").HeaderStyle.Width = (Unit)(Convert.ToInt32(rgTransferenciaAlmacen.Columns.FindByUniqueName("Prd_Descripcion").HeaderStyle.Width.Value) + 111);
                    

                    double ancho = 0;
                    foreach (GridColumn gc in rgTransferenciaAlmacen.Columns)
                    {
                        if (gc.Display && gc.Visible)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    rgTransferenciaAlmacen.Width = Unit.Pixel(Convert.ToInt32(ancho) + 55);
                    rgTransferenciaAlmacen.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho) + 38);
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
            if (oc is TransferenciaAlmacenDet) { return ((TransferenciaAlmacenDet)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }
        protected string ObtenerPresentacion(object oc)
        {
            if (oc is TransferenciaAlmacenDet) { return ((TransferenciaAlmacenDet)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }
        protected string ObtenerUnidades(object oc)
        {
            if (oc is TransferenciaAlmacenDet) { return ((TransferenciaAlmacenDet)oc).Producto.Prd_UniNe; } else { return string.Empty; }
        }
        protected int ObtenerInvFinal(object oc)
        {
            if (oc is TransferenciaAlmacenDet) { return ((TransferenciaAlmacenDet)oc).Producto.Prd_InvFinal; } else { return 0; }
        }

        protected float ObtenerPrecio(object oc)
        {
            if (oc is TransferenciaAlmacenDet) { return ((TransferenciaAlmacenDet)oc).ProductoPrecio.Prd_Pesos; } else { return 0; }
        }
        #endregion
        #region "Métodos para manejar la lista dinámica de Productos de una orden de compra"
        protected void ListaProductosTransferenciaAlmacen_AgregarProducto(TransferenciaAlmacenDet TransferenciaAlmacen_prod)
        {
            List<TransferenciaAlmacenDet> lista = this.ListaProductosTransferenciaAlmacen;

            //buscar producto de orden de compra en la lista para ver si ya existe
            for (int i = 0; i < lista.Count; i++)
            {
                TransferenciaAlmacenDet orden = lista[i];
                if (orden.Id_Prd == TransferenciaAlmacen_prod.Id_Prd)
                {
                    throw new Exception("rgTransferenciaAlmacen_insert_repetida");
                }
            }

            lista.Add(TransferenciaAlmacen_prod);
            this.ListaProductosTransferenciaAlmacen = lista;
            CalcularTotal(lista);
        }
        private void CalcularTotal(List<TransferenciaAlmacenDet> lista)
        {
            try
            {
                double? total = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                for (int i = 0; i < lista.Count; i++)
                {
                    TransferenciaAlmacenDet transferencia = lista[i];

                   // CN_CatProducto cn_producto = new CN_CatProducto();
                    //Producto prd = new Producto();
                   // cn_producto.ConsultaProducto(ref prd, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, orden.Id_Prd);
                    total += transferencia.Trans_Cant * transferencia.ProductoPrecio.Prd_Pesos;
                }

                txtTotal.DbValue = total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaProductosTransferenciaAlmacen_ModificarProducto(TransferenciaAlmacenDet TransferenciaAlmacen_prod)
        {
            List<TransferenciaAlmacenDet> lista = this.ListaProductosTransferenciaAlmacen;

            //buscar producto de orden de compra en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                TransferenciaAlmacenDet orden = lista[i];
                if (orden.Id_Prd == TransferenciaAlmacen_prod.Id_Prd)
                {
                    lista[i] = TransferenciaAlmacen_prod;
                    break;
                }
            }
            this.ListaProductosTransferenciaAlmacen = lista;
            CalcularTotal(lista);
        }
        protected void ListaProductosTransferenciaAlmacen_EliminarProducto(int id_Prd)
        {
            List<TransferenciaAlmacenDet> lista = this.ListaProductosTransferenciaAlmacen;

            //buscar producto de orden de compra en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                TransferenciaAlmacenDet orden = lista[i];
                if (orden.Id_Prd == id_Prd)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaProductosTransferenciaAlmacen = lista;
            CalcularTotal(lista);
        }
        protected void ListaProductosTransferenciaAlmacen_EliminarTodos()
        {
            this.ListaProductosTransferenciaAlmacen = new List<TransferenciaAlmacenDet>();
            txtTotal.DbValue = 0;
        }
        #endregion
        protected void rgTransferenciaAlmacen_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);

                    //Llenar Grid
                    rgTransferenciaAlmacen.DataSource = this.ListaProductosTransferenciaAlmacen;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgTransferenciaAlmacen_NeedDataSource"));
            }
        }
        protected void rgTransferenciaAlmacen_ItemDataBound(object sender, GridItemEventArgs e)
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
                    string lblTrans_Cant = ((Label)editItem.FindControl("lblVal_txtTrans_Cant")).ClientID.ToString();
                    RadNumericTextBox Ctrl_txtTrans_Cant = (RadNumericTextBox)editItem.FindControl("txtTrans_Cant");
                    string txtTrans_Cant = Ctrl_txtTrans_Cant.ClientID.ToString();
                    string HD_Prd_UniEmp = ((HiddenField)editItem.FindControl("HD_Prd_UniEmp")).ClientID.ToString();

                    string jsControles = string.Concat(
                        "lbl_cmbProductoClientId='", lbl_cmbProducto, "';"
                        , "cmbProductoClientId='", cmbProducto, "';"
                        , "lblVal_txtTrans_CantClientId='", lblTrans_Cant, "';"
                        , "txtTrans_CantClientId='", txtTrans_Cant, "';"
                        , "HD_Prd_UniEmpClientId='", HD_Prd_UniEmp, "';"
                        );
                    
                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //es registro nuevo y se inhabilita el campo de cantidad (se habilita hasta ke elija un producto del combo)
                        Ctrl_txtTrans_Cant.Enabled = false;
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                  /*  if (updatebtn != null)
                    {
                        //establecer unidades de empaque
                        int claveOrden = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Trans"].ToString());
                        int claveProducto = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString());
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        Producto producto = null;
                        new CN_CatProducto().ConsultaProducto_TransferenciaAlmacen(ref producto, sesion.Emp_Cnx, claveOrden, claveProducto, sesion.Id_Emp, sesion.Id_Cd_Ver);
                        ((HiddenField)editItem.FindControl("HD_Prd_UniEmp")).Value = producto.Prd_UniEmp.ToString();
                        //-------------------------------

                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto
                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = false;
                        //es registro de edición, se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                        Ctrl_txtTrans_Cant.Enabled = true;
                    }*/
                }
                /*
                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_PrdN"].FindControl("txtId_Prd");
                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form["Trans_Cant"].FindControl("txtTrans_Cant");
                    }
                    dataField.Focus();
                }*/
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgTransferenciaAlmacen_ItemDataBound"));
            }
        }
        protected void rgTransferenciaAlmacen_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                TransferenciaAlmacenDet TransferenciaAlmacenDet = new TransferenciaAlmacenDet();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                RadNumericTextBox Ctrl_txtTrans_Cant = (RadNumericTextBox)insertedItem.FindControl("txtTrans_Cant");
                string txtTrans_Cant = Ctrl_txtTrans_Cant.ClientID.ToString();
                string txtId_Prd = ((RadNumericTextBox)insertedItem.FindControl("txtId_Prd")).ClientID.ToString();//txtId_Prd

                if (Ctrl_txtTrans_Cant.Enabled)
                    if (Ctrl_txtTrans_Cant.Value.HasValue)
                    {
                        if (Ctrl_txtTrans_Cant.Value.Value == 0)
                        {
                            AlertaFocus("La cantidad ordenada es requerida", (insertedItem["Trans_Cant"].FindControl("txtTrans_Cant") as RadNumericTextBox).ClientID);
                            e.Canceled = true;
                            return;
                        }
                    }
                    else
                    {
                        AlertaFocus("La cantidad ordenada es requerida", (insertedItem["Trans_Cant"].FindControl("txtTrans_Cant") as RadNumericTextBox).ClientID);
                        e.Canceled = true;
                        return;
                    }
                TransferenciaAlmacenDet.Id_Emp = sesion.Id_Emp;
                TransferenciaAlmacenDet.Id_Cd = sesion.Id_Cd_Ver;
                TransferenciaAlmacenDet.Id_Trans = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                TransferenciaAlmacenDet.Id_TransDet = 0; //identity
                TransferenciaAlmacenDet.Id_Prd = Convert.ToInt32((insertedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                TransferenciaAlmacenDet.Trans_Cant = !string.IsNullOrEmpty((insertedItem["Trans_Cant"].FindControl("txtTrans_Cant") as RadNumericTextBox).Text) ? Convert.ToInt32((insertedItem["Trans_Cant"].FindControl("txtTrans_Cant") as RadNumericTextBox).Text) : 0;
               

                if (TransferenciaAlmacenDet.Id_Prd == 0)
                {
                    AlertaFocus("El producto es requerido", (insertedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }
                double unidadEmpaque = Convert.ToDouble(((HiddenField)insertedItem.FindControl("HD_Prd_UniEmp")).Value);
                if (unidadEmpaque != 0 && Ctrl_txtTrans_Cant.Value.HasValue)
                {
                    //    if ((Ctrl_txtTrans_Cant.Value.Value % unidadEmpaque) != 0)
                    //    {
                    //        AlertaFocus("Favor de capturar m&uacute;ltiplos seg&uacute;n las unidades de empaque configuradas en el cat&aacute;logo de productos. Las unidades de empaque para este producto son " + unidadEmpaque.ToString(), (insertedItem.FindControl("txtTrans_Cant") as RadNumericTextBox).ClientID);
                    //        e.Canceled = true;
                    //        return;
                    //    }

                    if (unidadEmpaque == 0)
                        unidadEmpaque = 1;

                    if (Ctrl_txtTrans_Cant.Value.Value % unidadEmpaque != 0)
                    {
                        while (Ctrl_txtTrans_Cant.Value.Value % unidadEmpaque != 0)
                        {
                            Ctrl_txtTrans_Cant.Value += 1;
                        }
                    }

                }


                float Prd_Precio = Convert.ToSingle(((HiddenField)insertedItem.FindControl("HD_Prd_PrecioAAA")).Value);
                //datos del producto de la orden de compra
                TransferenciaAlmacenDet.Producto = new Producto();
                TransferenciaAlmacenDet.Producto.Id_Prd = Convert.ToInt32((insertedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                TransferenciaAlmacenDet.Producto.Id_Emp = sesion.Id_Emp;
                TransferenciaAlmacenDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                TransferenciaAlmacenDet.Producto.Prd_Descripcion = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                TransferenciaAlmacenDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                TransferenciaAlmacenDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;
                TransferenciaAlmacenDet.ProductoPrecio = new ProductoPrecios();
                 
                TransferenciaAlmacenDet.ProductoPrecio.Prd_Pesos = Prd_Precio;

                //agregar producto de orden de compra a la lista
                this.ListaProductosTransferenciaAlmacen_AgregarProducto(TransferenciaAlmacenDet);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgTransferenciaAlmacen_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgTransferenciaAlmacen_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                TransferenciaAlmacenDet TransferenciaAlmacenDet = new TransferenciaAlmacenDet();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                RadNumericTextBox Ctrl_txtTrans_Cant = (RadNumericTextBox)editedItem.FindControl("txtTrans_Cant");
                string txtTrans_Cant = Ctrl_txtTrans_Cant.ClientID.ToString();
                string txtId_Prd = ((RadNumericTextBox)editedItem.FindControl("txtId_Prd")).ClientID.ToString();//txtId_Prd

                if (Ctrl_txtTrans_Cant.Enabled)
                    if (Ctrl_txtTrans_Cant.Value.HasValue)
                    {
                        if (Ctrl_txtTrans_Cant.Value.Value == 0)
                        {
                            AlertaFocus("La cantidad ordenada es requerida", (editedItem["Trans_Cant"].FindControl("txtTrans_Cant") as RadNumericTextBox).ClientID);
                            e.Canceled = true;
                            return;
                        }
                    }
                    else
                    {
                        AlertaFocus("La cantidad ordenada es requerida", (editedItem["Trans_Cant"].FindControl("txtTrans_Cant") as RadNumericTextBox).ClientID);
                        e.Canceled = true;
                        return;
                    }
                TransferenciaAlmacenDet.Id_Emp = sesion.Id_Emp;
                TransferenciaAlmacenDet.Id_Cd = sesion.Id_Cd_Ver;
                TransferenciaAlmacenDet.Id_Trans = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                TransferenciaAlmacenDet.Id_TransDet = 0; //identity
                TransferenciaAlmacenDet.Id_Prd = Convert.ToInt32((editedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                TransferenciaAlmacenDet.Trans_Cant = !string.IsNullOrEmpty((editedItem["Trans_Cant"].FindControl("txtTrans_Cant") as RadNumericTextBox).Text) ? Convert.ToInt32((editedItem["Trans_Cant"].FindControl("txtTrans_Cant") as RadNumericTextBox).Text) : 0;
               

                if (TransferenciaAlmacenDet.Id_Prd == 0)
                {
                    AlertaFocus("El producto es requerido", (editedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }
                double unidadEmpaque = Convert.ToDouble(((HiddenField)editedItem.FindControl("HD_Prd_UniEmp")).Value);


                if (unidadEmpaque == 0)
                    unidadEmpaque = 1;

                if (unidadEmpaque != 0 && Ctrl_txtTrans_Cant.Value.HasValue)
                {
                    if ((Ctrl_txtTrans_Cant.Value.Value % unidadEmpaque) != 0)
                    {
                        AlertaFocus("Favor de capturar m&uacute;ltiplos seg&uacute;n las unidades de empaque configuradas en el cat&aacute;logo de productos. Las unidades de empaque para este producto son " + unidadEmpaque.ToString(), (editedItem.FindControl("txtTrans_Cant") as RadNumericTextBox).ClientID);
                        e.Canceled = true;
                        return;
                    }
                }
                //datos del producto de la orden de compra
                TransferenciaAlmacenDet.Producto = new Producto();
                TransferenciaAlmacenDet.Producto.Id_Prd = Convert.ToInt32((editedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                TransferenciaAlmacenDet.Producto.Id_Emp = sesion.Id_Emp;
                TransferenciaAlmacenDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                TransferenciaAlmacenDet.Producto.Prd_Descripcion = (editedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                TransferenciaAlmacenDet.Producto.Prd_Presentacion = (editedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                TransferenciaAlmacenDet.Producto.Prd_UniNe = (editedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;

                //actualizar producto de orden de compra a la lista
                this.ListaProductosTransferenciaAlmacen_ModificarProducto(TransferenciaAlmacenDet);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgTransferenciaAlmacen_update_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgTransferenciaAlmacen_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                //actualizar producto de orden de compra a la lista
                this.ListaProductosTransferenciaAlmacen_EliminarProducto(id_Prd);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgTransferenciaAlmacen_delete_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgTransferenciaAlmacen_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgTransferenciaAlmacen.Rebind();
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
                RadNumericTextBox txtTrans_Cant = (RadNumericTextBox)tabla.FindControl("txtTrans_Cant");
                RadNumericTextBox txtId_Prd = (RadNumericTextBox)tabla.FindControl("txtId_Prd");//txtId_Prd
                txtTrans_Cant.Enabled = true;
                Producto producto = null;
                if (txtId_Prd.Value.ToString() != string.Empty && txtId_Prd.Value.ToString() != "-1")
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_CatProducto clsProducto = new CN_CatProducto();
                    clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, Convert.ToInt32(txtId_Prd.Value.ToString()),1);
                }

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

                txtTrans_Cant.Text = string.Empty;
                txtTrans_Cant.Focus();
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

                RadNumericTextBox Ctrl_txtTrans_Cant = (RadNumericTextBox)tabla.FindControl("txtTrans_Cant");
                string txtTrans_Cant = Ctrl_txtTrans_Cant.ClientID.ToString();//txtTrans_Cant
                string lblTrans_Cant = ((Label)tabla.FindControl("lblVal_txtTrans_Cant")).ClientID.ToString();

                if (!Ctrl_txtTrans_Cant.Value.HasValue)
                    AlertaFocus("La cantidad ordenada es requerida", (tabla.FindControl("txtTrans_Cant") as RadNumericTextBox).ClientID);
                if (Ctrl_txtTrans_Cant.Value.HasValue)
                    if (Ctrl_txtTrans_Cant.Value <= 0)
                        AlertaFocus("La cantidad ordenada debe ser mayor a 0", (tabla.FindControl("txtTrans_Cant") as RadNumericTextBox).ClientID);

                //if (unidadEmpaque != 0 && Ctrl_txtTrans_Cant.Value.HasValue)
                //{
                //    if ((Ctrl_txtTrans_Cant.Value.Value % unidadEmpaque) != 0)
                //    {
                //        AlertaFocus("Favor de capturar m&uacute;ltiplos seg&uacute;n las unidades de empaque configuradas en el cat&aacute;logo de productos. Las unidades de empaque para este producto son " + unidadEmpaque.ToString(), Ctrl_txtTrans_Cant.ClientID);
                //    }
                //}
                if (unidadEmpaque == 0)
                    unidadEmpaque = 1;

                if (Ctrl_txtTrans_Cant.Value.Value % unidadEmpaque != 0)
                {
                    while (Ctrl_txtTrans_Cant.Value.Value % unidadEmpaque != 0)
                    {
                        Ctrl_txtTrans_Cant.Value += 1;
                    }
                }
                #region comentarizado
                //string jsControles = string.Concat(
                //        "lbl_cmbProductoClientId='", lbl_cmbProducto, "';"
                //        , "cmbProductoClientId='", txtPrd_Descripcion, "';"
                //        , "lblVal_txtTrans_CantClientId='", lblTrans_Cant, "';"
                //        , "txtTrans_CantClientId='", txtTrans_Cant, "';"
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
                // Ctrl_txtTrans_Cant.Enabled = false;

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
                        mensajeError = hiddenId.Value == string.Empty ? "CapTransferenciaAlmacen_insert_error" : "CapTransferenciaAlmacen_update_error";
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
        protected void cmbTransferenciaAlmacen_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.LLenarFormTransferenciaAlmacen(Convert.ToInt32(e.Value));
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat("CapTransferenciaAlmacen_LlenarForm_error", ex.Message));
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
               /* if (txtProveedor.Text != "")
                {
                    RAM1.ResponseScripts.Add("popup('" + txtProveedor.Text + "');");
                }
                else
                {
                    Alerta("No se ha seleccionado un proveedor");
                }*/
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
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapTransferencia", "Id_Trans", sesion.Emp_Cnx, "spCatLocal_Maximo");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            this.HabilitaControles(true);

           
            rgTransferenciaAlmacen.DataSource = this.ListaProductosTransferenciaAlmacen = new List<TransferenciaAlmacenDet>();
            rgTransferenciaAlmacen.DataBind();

            txtFecha.Focus();
        }
        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];


                CN_CapTransferenciaAlmacen clsCapTransferenciaAlmacen = new CN_CapTransferenciaAlmacen();
                int verificador = -1;
                TransferenciaAlmacen TransferenciaAlmacen = this.LlenarObjetoTransferenciaAlmacen();
                TransferenciaAlmacen.ListTransferenciaAlmacen = this.ListaProductosTransferenciaAlmacen;
               // CalcularTotal(ListaProductosTransferenciaAlmacen);

                if (TransferenciaAlmacen.ListTransferenciaAlmacen == null || TransferenciaAlmacen.ListTransferenciaAlmacen.Count == 0)
                {
                    DisplayMensajeAlerta("TransferenciaAlmacenSinProductos");
                    return;
                }

                if (hiddenId.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        DisplayMensajeAlerta("PermisoGuardarNo");
                        return;
                    }

                   // clsCapTransferenciaAlmacen.InsertarTransferenciaAlmacen(ref TransferenciaAlmacen, session.Emp_Cnx, ref verificador);

                    hiddenId.Value = this.txtFolio.Text = TransferenciaAlmacen.Id_Trans.ToString();
                    //this.Nuevo();
                    this.HabilitaBotonesToolBar(false, true, false, true, true, true);

                    //this.DisplayMensajeAlerta("CapTransferenciaAlmacen_insert_ok");
                    //string mensaje = "Los datos se guardaron correctamente";
                    RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        DisplayMensajeAlerta("PermisoModificarNo");
                        return;
                    }

                  //  clsCapTransferenciaAlmacen.ModificarTransferenciaAlmacen(TransferenciaAlmacen, session.Emp_Cnx, ref verificador);
                    this.HabilitaBotonesToolBar(false, true, false, true, true, true);

                    //this.DisplayMensajeAlerta("CapTransferenciaAlmacen_update_ok");
                    string mensaje = "Los datos se modificaron correctamente";
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de detalle de orden de compra
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private TransferenciaAlmacen LlenarObjetoTransferenciaAlmacen()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            TransferenciaAlmacen TransferenciaAlmacen = new TransferenciaAlmacen();

            TransferenciaAlmacen.Id_Emp = sesion.Id_Emp;
            TransferenciaAlmacen.Id_Cd = sesion.Id_Cd_Ver;
            TransferenciaAlmacen.Id_Trans = Convert.ToInt32(txtFolio.Text);
            
            TransferenciaAlmacen.Id_U = sesion.Id_U;
            //cuando se da de alta por el usuario el estatus siempre es M = Manual
            TransferenciaAlmacen.Trans_Estatus = "C"; //cmbEstatus.SelectedValue; //M = Manual, A = Automático

            CapaDatos.Funciones funciones = new CapaDatos.Funciones();
            DateTime fechaServidor = funciones.GetLocalDateTime(sesion.Minutos);
            DateTime fechaDatePicker = Convert.ToDateTime(txtFecha.SelectedDate);

            TransferenciaAlmacen.Trans_Fecha = new DateTime(fechaDatePicker.Year, fechaDatePicker.Month, fechaDatePicker.Day, fechaServidor.Hour, fechaServidor.Minute, fechaServidor.Second);
           
            TransferenciaAlmacen.TransNota = txtNotas.Text;

            return TransferenciaAlmacen;
        }
        private void LLenarFormTransferenciaAlmacen(int Id_Trans)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            TransferenciaAlmacen TransferenciaAlmacen = new TransferenciaAlmacen();
            TransferenciaAlmacen.Id_Emp = sesion.Id_Emp;
            TransferenciaAlmacen.Id_Cd = sesion.Id_Cd_Ver;
            TransferenciaAlmacen.Id_Trans = Id_Trans;

            new CN_CapTransferenciaAlmacen().ConsultaTransferenciaAlmacen(ref TransferenciaAlmacen, sesion.Emp_Cnx);

            txtFolio.Text = TransferenciaAlmacen.Id_Trans.ToString();
            this.hiddenId.Value = Id_Trans.ToString();            
            txtFecha.SelectedDate = TransferenciaAlmacen.Trans_Fecha;
            txtCdIOrigen.Value = TransferenciaAlmacen.Id_CdOrigen;
            txtCdIOrigenStr.Text = TransferenciaAlmacen.Id_CdOrigenStr;
            txtId_RemOrigen.Value = TransferenciaAlmacen.Id_RemOrigen;
            Id_UOrigen.Value = TransferenciaAlmacen.Id_UOrigen;
            U_NombreOrigen.Text = TransferenciaAlmacen.U_NombreOrigen;
            txtNotas.Text = TransferenciaAlmacen.TransNota;

            HD_TransferenciaAlmacenEstatus.Value = TransferenciaAlmacen.Trans_Estatus;

            //llenar grid de detalle de orden de compra
            List<TransferenciaAlmacenDet> listaTransferenciaAlmacenDet = new List<TransferenciaAlmacenDet>();
          
            new CN_CapTransferenciaAlmacenDet().ConsultaTransferenciaAlmacenDetalle_Lista(TransferenciaAlmacen, sesion.Emp_Cnx, ref listaTransferenciaAlmacenDet);
            rgTransferenciaAlmacen.Enabled = true;
            rgTransferenciaAlmacen.DataSource = listaTransferenciaAlmacenDet;
            rgTransferenciaAlmacen.DataBind();

            //asignar lista de productos de orden de comptra
            this.ListaProductosTransferenciaAlmacen = listaTransferenciaAlmacenDet;

            //habilita/deshabilita controles de la orden consultada
            this.ValidarModificacionTransferenciaAlmacen(TransferenciaAlmacen.Trans_Estatus);

            CalcularTotal(listaTransferenciaAlmacenDet);
        }
        /// <summary>
        /// Habilita o deshabilita controles dependiendo si la orden se puede modificar o no
        /// </summary>
        /// <param name="estatus"> Estatus de la orden consultada
        /// C = Capturada
        /// B = Baja
        /// I = Impresa
        /// </param>
        private void ValidarModificacionTransferenciaAlmacen(string estatus)
        {
           /* if (estatus == "B") //si el estatus es baja o impreso los controles se deshabilitan
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
                }*/
        }
        /// <summary>
        /// Habilita o deshabilita controles
        /// </summary>
        private void HabilitaControles(bool habilitar)
        {
            /*txti .Enabled = habilitar;
            cmbProveedor.Enabled = habilitar;*/
            if (habilitar)
            {
                rgTransferenciaAlmacen.Enabled = habilitar;
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
       
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("TransferenciaAlmacenSinProductos"))
                    Alerta("No se han agregado productos a la orden de compra");
                else
                    if (mensaje.Contains("rgTransferenciaAlmacen_insert_repetida"))
                        Alerta("Este producto ya est&aacute; agregado en la orden de compra");
                    else
                        if (mensaje.Contains("rgTransferenciaAlmacen_insert_error"))
                            Alerta("Error al momento de agregar el producto a la lista de productos de la orden de compra");
                        else
                            if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                Alerta("Error al consultar los datos de producto");
                            else
                                if (mensaje.Contains("txtCantidad_IndexChanging_error"))
                                    Alerta("Error al insertar la cantidad del producto");
                                else
                                    if (mensaje.Contains("rgTransferenciaAlmacen_ItemDataBound"))
                                        Alerta("Error al momento de preparar un registro para edición");
                                    else
                                        if (mensaje.Contains("CapTransferenciaAlmacenDetalle_consulta_error"))
                                            Alerta("Error al consultar el detalle de la orden de compra");
                                        else
                                            if (mensaje.Contains("CapTransferenciaAlmacenDetalle_insert_error"))
                                                Alerta("Error al guardar el detalle de la orden de compra");
                                            else
                                                if (mensaje.Contains("rgTransferenciaAlmacen_NeedDataSource"))
                                                    Alerta("Error al cargar el grid de detalle de orden de compra");
                                                else
                                                    if (mensaje.Contains("rgTransferenciaAlmacen_ItemCommand"))
                                                        Alerta("Error al ejecutar un evento (rgTransferenciaAlmacen_ItemCommand) al cargar el grid de detalle de orden de compra");
                                                    else
                                                        if (mensaje.Contains("rgTransferenciaAlmacen_Actualizar_ok"))
                                                            Alerta("El producto de la orden de compra fue actualizado correctamente");
                                                        else
                                                            if (mensaje.Contains("rgTransferenciaAlmacen_Actualizar_error"))
                                                                Alerta("Error al actualizar el producto de la orden de compra");
                                                            else
                                                                if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                                                    Alerta("Error al cambiar de centro de distribución");
                                                                else
                                                                    if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                        Alerta("Error al cambiar de página");
                                                                    else
                                                                        if (mensaje.Contains("CapTransferenciaAlmacen_LlenarForm_error"))
                                                                            Alerta("Error al momento de consultar los datos de la orden de compra");
                                                                        else
                                                                            if (mensaje.Contains("PermisoGuardarNo"))
                                                                                Alerta("No tiene permisos para grabar");
                                                                            else
                                                                                if (mensaje.Contains("PermisoModificarNo"))
                                                                                    Alerta("No tiene permisos para actualizar");
                                                                                else
                                                                                    if (mensaje.Contains("CapTransferenciaAlmacenDetalle_delete_error"))
                                                                                        Alerta("Error al momento de eliminar el detalle de la orden de compra");
                                                                                    else
                                                                                        if (mensaje.Contains("CapTransferenciaAlmacen_insert_ok"))
                                                                                            Alerta("Los datos se guardaron correctamente");
                                                                                        else
                                                                                            if (mensaje.Contains("CapTransferenciaAlmacen_insert_error"))
                                                                                                Alerta("No se pudo guardar los datos de la orden de compra");
                                                                                            else
                                                                                                if (mensaje.Contains("CapTransferenciaAlmacen_update_ok"))
                                                                                                    Alerta("Los datos se modificaron correctamente");
                                                                                                else
                                                                                                    if (mensaje.Contains("CapTransferenciaAlmacen_update_error"))
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
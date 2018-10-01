using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Net;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Data;

namespace SIANWEB
{
    public partial class CapNotaCredito_Especial : System.Web.UI.Page
    {
        #region Propiedades
        public DataTable dt_detalles
        {
            get
            {
                return (Session["ListaProductosNotaCredito" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosNotaCredito" + Session.SessionID] = value;
            }
        }
        //Variable de lista de productos para el combo del grid Editable
        private List<Producto> _listaProductos;
        public List<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; }
        }

        private List<NotaCreditoDet> ListaProductosNotaCreditoEspecial
        {
            get { return (List<NotaCreditoDet>)Session["ListaProductosNotaCreditoEspecial" + Session.SessionID]; }
            set { Session["ListaProductosNotaCreditoEspecial" + Session.SessionID] = value; }
        }

        private FacturaEspecial NotaCreditoEspecial
        {
            get { return (FacturaEspecial)Session["NotaCreditoEspecial" + Session.SessionID]; }
            set { Session["NotaCreditoEspecial" + Session.SessionID] = value; }
        }

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Page.IsPostBack)
                    this.Inicializar();
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
                        mensajeError = "CapFacturaEspecial_insert_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoEspecialDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                { //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);

                    //Llenar Grid
                    rgNotaCreditoEspecialDet.DataSource = this.ListaProductosNotaCreditoEspecial;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoEspecialDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editItem.FindControl("txtNcr_Cantidad");
                    string lblNcr_Cantidad = ((Label)editItem.FindControl("lblVal_txtNcr_Cantidad")).ClientID.ToString();
                    string txtNcr_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    string lblVal_txtPrd_Descripcion = ((Label)editItem.FindControl("lblVal_txtPrd_Descripcion")).ClientID.ToString();
                    string txtPrd_Descripcion = ((RadTextBox)editItem.FindControl("txtPrd_Descripcion")).ClientID.ToString();
                    string lblVal_txtPrd_Presentacion = ((Label)editItem.FindControl("lblVal_txtPrd_Presentacion")).ClientID.ToString();
                    string txtPrd_Presentacion = ((RadTextBox)editItem.FindControl("txtPrd_Presentacion")).ClientID.ToString();
                    string lblVal_txtPrd_UniNe = ((Label)editItem.FindControl("lblVal_txtPrd_UniNe")).ClientID.ToString();
                    string txtPrd_UniNe = ((RadTextBox)editItem.FindControl("txtPrd_UniNe")).ClientID.ToString();
                    string lblVal_txtNcr_Precio = ((Label)editItem.FindControl("lblVal_txtNcr_Precio")).ClientID.ToString();
                    string txtNcr_Precio = ((RadNumericTextBox)editItem.FindControl("txtNcr_Precio")).ClientID.ToString();

                    //Llenar combo de productos
                    RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    CargarProductos(comboProductoItem);

                    string jsControles = string.Concat(
                        "lblNcr_CantidadClientId='", lblNcr_Cantidad, "';"
                        , "txtNcr_CantidadClientId='", txtNcr_Cantidad, "';"
                        , "lbl_cmbProductoClientId='", lbl_cmbProducto, "';"
                        , "txtId_PrdClientId='", txtId_Prd, "';"
                        , "lblVal_txtPrd_DescripcionClientId='", lblVal_txtPrd_Descripcion, "';"
                        , "txtPrd_DescripcionClientId='", txtPrd_Descripcion, "';"
                        , "lblVal_txtPrd_PresentacionClientId='", lblVal_txtPrd_Presentacion, "';"
                        , "txtPrd_PresentacionClientId='", txtPrd_Presentacion, "';"
                        , "lblVal_txtPrd_UniNeClientId='", lblVal_txtPrd_UniNe, "';"
                        , "txtPrd_UniNeClientId='", txtPrd_UniNe, "';"
                        , "lblVal_txtNcr_PrecioClientId='", lblVal_txtNcr_Precio, "';"
                        , "txtNcr_PrecioClientId='", txtNcr_Precio, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //cuando la edición se usa para inserción, se habilita el combo de producto
                        ((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = true;
                        //es registro nuevo y se inhabilita el campo de cantidad (se habilita hasta ke elija un producto del combo)
                        Ctrl_txtOrd_Cantidad.Enabled = false;

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Focus();
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto
                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = false;
                        ((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = false;
                        //es registro de edición, se habilita el campo de cantidad porque ya eligió producto
                        Ctrl_txtOrd_Cantidad.Enabled = true;

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");
                        updatebtn.Attributes.Add("onclick", jsControles);

                        //cuando es actualización se selecciona el producto del combo
                        comboProductoItem.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();
                        Ctrl_txtOrd_Cantidad.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoEspecialDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            NotaCreditoDet facturaDet = new NotaCreditoDet();
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                facturaDet.Id_Emp = sesion.Id_Emp;
                facturaDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Id_Ncr = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                facturaDet.Id_NcrDet = 0;
                facturaDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//cliente de datos generales de la factura
                facturaDet.Id_Prd = Convert.ToInt32((insertedItem.FindControl("cmbProducto") as RadComboBox).SelectedValue);
                facturaDet.Ncr_Cant = 0;
                facturaDet.Clp_Release = (insertedItem.FindControl("txtClp_ReleaseEdit") as RadTextBox).Text;
                //float precioPartida = Convert.ToSingle((insertedItem["Ncr_Precio"].FindControl("txtNcr_Precio") as RadNumericTextBox).Text.Replace("$", string.Empty));
                //(insertedItem["Ncr_Importe"].FindControl("lblNcr_ImporteEdit") as Label).Text = precioPartida.ToString("C");
                facturaDet.Ncr_Importe = (insertedItem.FindControl("txtNcr_Importe") as RadNumericTextBox).Value != null ? (insertedItem["Ncr_Importe"].FindControl("txtNcr_Importe") as RadNumericTextBox).Value.Value : 0;
                facturaDet.Ncr_Precio = 0;

                //datos del producto de la orden de compra
                facturaDet.Producto = new Producto();
                facturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem.FindControl("cmbProducto") as RadComboBox).SelectedValue);
                facturaDet.Producto.Prd_Descripcion = (insertedItem.FindControl("cmbProducto") as RadComboBox).Text;
                facturaDet.Producto.Id_Emp = sesion.Id_Emp;
                facturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Producto.Prd_DescripcionEspecial = (insertedItem.FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                facturaDet.Producto.Prd_Presentacion = (insertedItem.FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                facturaDet.Producto.Prd_UniNe = (insertedItem.FindControl("txtPrd_UniNe") as RadTextBox).Text;
                facturaDet.Id_Emp = sesion.Id_Emp;
                facturaDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Producto.Id_PrdEsp = (insertedItem.FindControl("txtId_PrdEsp") as RadTextBox).Text; ///<------
                //agregar producto de orden de compra a la lista
                this.ListaProductosFacturaEspecial_AgregarProducto(facturaDet);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgFacturaEspecial_insert_error");
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoEspecialDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            NotaCreditoDet facturaDet = new NotaCreditoDet();
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                facturaDet.Id_Emp = sesion.Id_Emp;
                facturaDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Id_Ncr = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                facturaDet.Id_NcrDet = 0;
                facturaDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//cliente de datos generales de la factura
                facturaDet.Id_Prd = Convert.ToInt32((insertedItem.FindControl("cmbProducto") as RadComboBox).SelectedValue);
                facturaDet.Ncr_Cant = 0;
                facturaDet.Clp_Release = (insertedItem.FindControl("txtClp_ReleaseEdit") as RadTextBox).Text;
                float precioPartida = Convert.ToSingle((insertedItem.FindControl("txtNcr_Precio") as RadNumericTextBox).Text.Replace("$", string.Empty));
                facturaDet.Ncr_Importe = (insertedItem.FindControl("txtNcr_Importe") as RadNumericTextBox).Value != null ? (insertedItem["Ncr_Importe"].FindControl("txtNcr_Importe") as RadNumericTextBox).Value.Value : 0;
                facturaDet.Ncr_Precio = 0;

                //datos del producto de la orden de compra
                facturaDet.Producto = new Producto();
                facturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem.FindControl("cmbProducto") as RadComboBox).SelectedValue);
                facturaDet.Producto.Prd_Descripcion = (insertedItem.FindControl("cmbProducto") as RadComboBox).Text;
                facturaDet.Producto.Id_Emp = sesion.Id_Emp;
                facturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Producto.Prd_DescripcionEspecial = (insertedItem.FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                facturaDet.Producto.Prd_Presentacion = (insertedItem.FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                facturaDet.Producto.Prd_UniNe = (insertedItem.FindControl("txtPrd_UniNe") as RadTextBox).Text;
                facturaDet.Producto.Id_PrdEsp = (insertedItem.FindControl("txtId_PrdEsp") as RadTextBox).Text; ///<------
                //agregar producto de orden de compra a la lista
                this.ListaProductosFacturaEspecial_ModificarProducto(facturaDet, e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgFacturaEspecial_insert_error");
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoEspecialDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                string descripcion = ((Label)item.FindControl("lblPrd_Descripcion")).Text;
                //actualizar producto de orden de compra a la lista
                //this.ListaProductosFacturaEspecial_EliminarProducto(id_Prd, descripcion);
                ListaProductosFacturaEspecial_EliminarProducto(e.Item.ItemIndex);
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoEspecialDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgNotaCreditoEspecialDet.EditItems.Count > 0)
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
        protected void rgNotaCreditoEspecialDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgNotaCreditoEspecialDet.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                if (e.Item.Value == "-1")
                    e.Item.FindControl("liComprasLocales").Controls.Clear();
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
                //verificar que el producto este el segmento
                if (false)
                {

                }
                else
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    RadComboBox combo = (RadComboBox)sender;
                    //obtiene la tabla contenedora de los controles de edición de registro del Grid
                    Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;
                    RadTextBox txtPrd_Descripcion = (RadTextBox)tabla.FindControl("txtPrd_Descripcion");
                    RadTextBox txtPrd_Presentacion = (RadTextBox)tabla.FindControl("txtPrd_Presentacion");
                    RadTextBox txtPrd_UniNe = (RadTextBox)tabla.FindControl("txtPrd_UniNe");
                    RadNumericTextBox txtNcr_Cantidad = (RadNumericTextBox)tabla.FindControl("txtNcr_Cantidad");
                    RadNumericTextBox txtNcr_Precio = (RadNumericTextBox)tabla.FindControl("txtNcr_Precio");
                    RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);
                    int id_Cd_Prod = sesion.Id_Cd_Ver;// Convert.ToInt32(((Label)item.FindControl("lblLiCd")).Text);

                    Producto producto = null;
                    if (e.Value != string.Empty && e.Value != "-1")
                    {

                        //obtener datos de producto
                        CN_CatProducto clsProducto = new CN_CatProducto();
                        clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, Convert.ToInt32(e.Value),0);


                        //Finalmente introduce el precio de producto aceptado para la partida
                        txtNcr_Precio.Text = "";
                    }
                    txtPrd_Descripcion.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
                    txtPrd_Presentacion.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                    txtPrd_UniNe.Text = producto == null ? string.Empty : producto.Prd_UniNe;

                    //este evento es porque se elige producto, por lo que 
                    //se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                    txtNcr_Cantidad.Enabled = true;
                    txtNcr_Cantidad.Text = string.Empty;

                    txtPrd_Descripcion.Focus();
                    //Limpiar controles de compras locales
                    //combo.Items[0].FindControl("liComprasLocales").Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 150);
                        RadPane1.Height = altura;
                        RadSplitter1.Height = altura;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "RAM1_AjaxRequest"));
            }
        }
        #endregion
        #region Funciones
        protected string ObtenerIdEspecial(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Producto.Id_PrdEsp; } else { return string.Empty; }
        }
        private void Inicializar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Convert.ToBoolean(Request.QueryString["Modificar"]))
                {
                    RadToolBar1.Items[1].Visible = false;
                }

                this.HD_Cliente.Value = Request.QueryString["Id_Cte"].ToString();
                this.HD_ImporteTotal.Value = Request.QueryString["Ncr_ImporteTotal"].ToString();
                this.HD_IVAfacturacion.Value = Request.QueryString["IVA_Ncr"].ToString();
                string folio = Request.QueryString["Folio"].ToString();
                string Id_NcrSerie = Request.QueryString["Id_NcrSerie"].ToString();
                this.HDId_NcrSerie.Value = Id_NcrSerie;

                if (Session["NcreditoEspecialGuardada" + Session.SessionID].ToString() != "1")
                    ListaProductosNotaCreditoEspecial = new List<NotaCreditoDet>();

                if (ListaProductosNotaCreditoEspecial != null && ListaProductosNotaCreditoEspecial.Count > 0)
                {
                    if (ListaProductosNotaCreditoEspecial[0].Producto.Prd_DescripcionEspecial == null)
                        ListaProductosNotaCreditoEspecial[0].Producto.Prd_DescripcionEspecial = "";

                    ListaProductosNotaCreditoEspecial[0].Producto.Prd_DescripcionEspecial = (ListaProductosNotaCreditoEspecial[0].Producto.Prd_DescripcionEspecial.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)).Length > 0 ? (ListaProductosNotaCreditoEspecial[0].Producto.Prd_DescripcionEspecial.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))[0] : "";
                }
                else
                {
                    ListaProductosNotaCreditoEspecial = new List<NotaCreditoDet>();
                    NotaCredito nota = new NotaCredito();
                    nota.Id_Emp = sesion.Id_Emp;
                    nota.Id_Cd = sesion.Id_Cd_Ver;
                    nota.Id_Ncr = !string.IsNullOrEmpty(folio) ? Convert.ToInt32(folio) : 0;
                    List<NotaCreditoDet> listaProdFacturaEspecialFinal = new List<NotaCreditoDet>();
                    if (!string.IsNullOrEmpty(folio))
                    {
                        new CN_CapNotaCredito().ConsultaNotaCreditoEspecialDetalle(ref listaProdFacturaEspecialFinal
                            , sesion.Emp_Cnx
                            , sesion.Id_Emp
                            , sesion.Id_Cd_Ver
                            , Convert.ToInt32(folio)
                            , Id_NcrSerie
                            , Convert.ToInt32(this.HD_Cliente.Value));
                    }

                    if (listaProdFacturaEspecialFinal.Count != 0)
                        ListaProductosNotaCreditoEspecial = listaProdFacturaEspecialFinal;
                    else
                    {
                        //// -------------------------------------------------------------------------------------------
                        //// obtener claves de productos de Remision original
                        //// -------------------------------------------------------------------------------------------
                        string clavesProducto = string.Empty;
                        foreach (DataRow dr in dt_detalles.Rows)
                        {
                            clavesProducto = clavesProducto + dr["Id_Prd"] + "|";
                        }
                        // -------------------------------------------------------------------------------------------
                        // consulta productos de factura especial en el catalogo de Cliente-Producto en base a los productos de la factura original
                        // -------------------------------------------------------------------------------------------
                        List<NotaCreditoDet> listaProdFacturaEspecial = new List<NotaCreditoDet>();
                        if (!string.IsNullOrEmpty(this.HD_Cliente.Value))
                            new CN_CatClienteProd().ConsultaClienteProd_NCreditoEspecial(ref listaProdFacturaEspecial
                                , sesion.Emp_Cnx
                                , sesion.Id_Emp
                                , sesion.Id_Cd_Ver
                                , Convert.ToInt32(this.HD_Cliente.Value)
                                , clavesProducto);
                        // -------------------------------------------------------------------------------------------
                        // Crear partidas adicionales de remision si es que la descripción viene con separadores "|"
                        // -------------------------------------------------------------------------------------------
                        for (int i = 0; i < listaProdFacturaEspecial.Count; i++)
                        {
                            NotaCreditoDet remisionDet = listaProdFacturaEspecial[i];
                            remisionDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//actualiza el cliente de la partida que es el cliente de la fact. original
                            string[] descripcion = remisionDet.Producto.Prd_DescripcionEspecial.Split(new char[] { '|' });

                            for (int j = 0; j < descripcion.Length; j++)
                            {
                                NotaCreditoDet remisionCopia = new NotaCreditoDet();
                                remisionCopia.Producto = new Producto();
                                remisionCopia.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);
                                remisionCopia.Id_Prd = remisionDet.Id_Prd;
                                remisionCopia.Producto.Id_PrdEsp = remisionDet.Producto.Id_PrdEsp;
                                remisionCopia.Producto.Prd_Descripcion = remisionDet.Producto.Prd_Descripcion;
                                remisionCopia.Producto.Prd_Presentacion = remisionDet.Producto.Prd_Presentacion;
                                remisionCopia.Producto.Prd_UniNe = remisionDet.Producto.Prd_UniNe;
                                remisionCopia.Producto.Prd_InvFinal = remisionDet.Producto.Prd_InvFinal;
                                remisionCopia.Producto.Prd_DescripcionEspecial = remisionDet.Producto.Prd_DescripcionEspecial;// descripcion[j];
                                remisionCopia.Id_Emp = sesion.Id_Emp;
                                remisionCopia.Id_Cd = sesion.Id_Cd_Ver;

                                if (j == 0)
                                {
                                    try
                                    {
                                        remisionCopia.Ncr_Importe = Convert.ToInt32(dt_detalles.Select("Id_Prd=" + remisionDet.Id_Prd)[0]["Ncr_Importe"]);
                                    }
                                    catch
                                    {
                                        remisionCopia.Ncr_Importe = 0;
                                    }
                                }
                                else
                                {
                                    remisionCopia.Ncr_Importe = 0;
                                }
                                ListaProductosNotaCreditoEspecial.Add(remisionCopia);
                            }
                        }
                    }
                }
                double ancho = 0;
                foreach (GridColumn gc in rgNotaCreditoEspecialDet.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgNotaCreditoEspecialDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgNotaCreditoEspecialDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                this.CalcularTotales();
                this.rgNotaCreditoEspecialDet.Rebind();
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
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<NotaCreditoDet> lista = this.ListaProductosNotaCreditoEspecial;
                double importeTotal = 0;
                float porcDescuento1 = HD_Descuento1.Value != string.Empty ? Convert.ToInt32(HD_Descuento1.Value) : 0;
                float porcDescuento2 = HD_Descuento2.Value != string.Empty ? Convert.ToInt32(HD_Descuento2.Value) : 0;

                for (int i = 0; i < lista.Count; i++)
                {
                    NotaCreditoDet factura = lista[i];
                    importeTotal += factura.Ncr_Importe;
                }
                CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
                double iva = 0;
                cn_cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva, sesion.Emp_Cnx);

                txtImporte.Text = importeTotal.ToString();
                importeTotal = porcDescuento1 > 0 ? (importeTotal - (importeTotal * (porcDescuento1 / 100))) : importeTotal;
                importeTotal = porcDescuento2 > 0 ? (importeTotal - (importeTotal * (porcDescuento2 / 100))) : importeTotal;
                txtSubTotal.Text = importeTotal.ToString();
                txtIVA.Text = HD_IVAfacturacion.Value.Trim() != string.Empty ? (importeTotal * iva / 100).ToString() : "0";//(importeTotal * (Convert.ToSingle(HD_IVAfacturacion.Value.Trim()) / 100)).ToString() : "0";
                txtTotal.Value = txtSubTotal.Value.Value + txtIVA.Value.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProductos(RadComboBox cmb)
        {
            try
            {
                List<Producto> listaProducto = new List<Producto>();
                Producto prd = new Producto();
                prd.Id_Prd = -1;
                prd.Prd_Descripcion = "-- Seleccionar --";
                listaProducto.Add(prd);
                foreach (DataRow item in dt_detalles.Rows)
                {
                    prd = new Producto();
                    prd.Id_Prd = Convert.ToInt32(item["Id_Prd"]);
                    prd.Prd_Descripcion = item.ItemArray[9].ToString();
                    listaProducto.Add(prd);
                }
                cmb.DataSource = listaProducto;
                cmb.DataBind();
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
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = 0;
                //Guardar los datos de los productos de factura especial
                //en catálogo de Cliente-Producto
                List<NotaCreditoDet> ListaPrdNcrEspecial = new List<NotaCreditoDet>();

                //Datos del centro de distribución
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);

                if (Session["fTotalNC" + Session.SessionID] != null)
                {
                    if (txtTotal.Text.Length > 0)
                    {
                        decimal fTotalNCOriginal = decimal.Round(decimal.Parse(Session["fTotalNC" + Session.SessionID].ToString()), 2);

                        if (((fTotalNCOriginal + (decimal)cd.Cd_MargenDiferenciaDocs) < decimal.Parse(txtTotal.Text)) || ((fTotalNCOriginal - (decimal)cd.Cd_MargenDiferenciaDocs) > decimal.Parse(txtTotal.Text)))
                        {
                            Alerta("El monto Total de la Nota de Crédito especial tiene una diferencia considerable con respecto a la Nota de Crédito original.");
                            return;
                        }
                    }
                }

                for (int i = 0; i < this.ListaProductosNotaCreditoEspecial.Count; i++)
                {
                    ListaPrdNcrEspecial.Add((NotaCreditoDet)this.ListaProductosNotaCreditoEspecial[i]);//this.ClonarFacturaDetalleEspecial(this.ListaProductosNotaCargo[i]));
                }



                new CN_CatClienteProd().ModificarClienteProdNCreditoEspecial(ListaPrdNcrEspecial, session.Emp_Cnx, ref verificador);
                //SET variable de encabezado de factura especial
                FacturaEspecial facturaEsp = new FacturaEspecial();
                facturaEsp.Id_Emp = session.Id_Emp;
                facturaEsp.Id_Cd = session.Id_Cd_Ver;
                facturaEsp.FacEsp_Importe = Convert.ToDouble(txtImporte.Text);
                facturaEsp.FacEsp_SubTotal = Convert.ToDouble(txtSubTotal.Text);
                facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(txtIVA.Text);
                facturaEsp.FacEsp_Total = Convert.ToDouble(txtTotal.Text);
                this.NotaCreditoEspecial = facturaEsp;
                Session["NcreditoEspecialGuardada" + Session.SessionID] = "1";

                string mensaje = "Los datos se guardaron correctamente";
                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_FacturaEspecial('", mensaje, "')")); //cerrar ventana radWindow de factura especial
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object ClonarFacturaDetalleEspecial(object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }
        protected string ObtenerDescripcionEspecial(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Producto.Prd_DescripcionEspecial; } else { return string.Empty; }
        }
        protected string ObtenerDescripcion(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }
        protected string ObtenerPresentacion(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }
        protected string ObtenerUnidades(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Producto.Prd_UniNe; } else { return string.Empty; }
        }
        protected int ObtenerInvFinal(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Producto.Prd_InvFinal; } else { return 0; }
        }
        protected void ListaProductosFacturaEspecial_AgregarProducto(NotaCreditoDet factura_prod)
        {
            List<NotaCreditoDet> lista = this.ListaProductosNotaCreditoEspecial;
            lista.Add(factura_prod);
            this.ListaProductosNotaCreditoEspecial = lista;
            this.CalcularTotales();
        }
        protected void ListaProductosFacturaEspecial_ModificarProducto(NotaCreditoDet factura_prod, int index)
        {
            List<NotaCreditoDet> lista = this.ListaProductosNotaCreditoEspecial;

            //buscar producto de factura en la lista       
            NotaCreditoDet factura = lista[index];
            if (factura.Id_Prd == factura_prod.Id_Prd)
                lista[index] = factura_prod;

            this.ListaProductosNotaCreditoEspecial = lista;
            this.CalcularTotales();
        }
        protected void ListaProductosFacturaEspecial_EliminarProducto(int index)
        {
            try
            {
                List<NotaCreditoDet> lista = this.ListaProductosNotaCreditoEspecial;
                lista.RemoveAt(index);
                this.ListaProductosNotaCreditoEspecial = lista;
                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                    Alerta("Error al consultar los datos de producto");
                else
                    if (mensaje.Contains("rgFacturaEspecial_insert_repetida"))
                        Alerta("Este producto ya ha sido capturado");
                    else
                        if (mensaje.Contains("rgOrdCompra_delete_item_error"))
                            Alerta("Error al momento de eliminar el producto a la lista de productos de la nota de crédito");
                        else
                            if (mensaje.Contains("CapFacturaEspecial_insert_error"))
                                Alerta("Error al momento de guardar los datos de nota de crédito especial");
                            else
                                if (mensaje.Contains("rgFacturaEspecial_insert_error"))
                                    Alerta("Error al momento de agregar el producto a la lista");
                                else
                                    if (mensaje.Contains("rgNotaCreditoEspecialDet_ItemDataBound"))
                                        Alerta("Error al momento de preparar un registro para edici&oacute;n");
                                    else
                                        if (mensaje.Contains("rgNotaCreditoEspecialDet_NeedDataSource"))
                                            Alerta("Error al cargar el grid de detalle de factura");
                                        else
                                            if (mensaje.Contains("rgOrdCompra_ItemCommand"))
                                                Alerta("Error al ejecutar un evento (rgOrdCompra_ItemCommand) al cargar el grid de detalle de orden de compra");
                                            else
                                                if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                    Alerta("Error al cambiar de página");
                                                else
                                                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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
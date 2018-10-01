using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;

namespace SIANWEB
{
    public partial class CapFactura_Especial : System.Web.UI.Page
    {
        public DataTable dt_detalles
        {
            get
            {
                return Session["ListaProductosFactura" + Session.SessionID] as DataTable;
            }
            set
            {
                Session["ListaProductosFactura" + Session.SessionID] = value;
            }
        }
        public DataTable ListaProductosFactura
        {
            get
            {
                return (Session["ListaProductosFactura" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosFactura" + Session.SessionID] = value;
            }
        }

        #region Propiedades

        //Variable de lista de productos para el combo del grid Editable
        //private List<Producto> _listaProductos;
        //public List<Producto> ListaProductos
        //{
        //    get { return _listaProductos; }
        //    set { _listaProductos = value; }
        //}

        private List<FacturaDet> ListaProductosFacturaEspecial
        {
            get { return (List<FacturaDet>)Session["ListaProductosFacturaEspecial" + Session.SessionID]; }
            set { Session["ListaProductosFacturaEspecial" + Session.SessionID] = value; }
        }

        private FacturaEspecial FacturaEspecial
        {
            get { return (FacturaEspecial)Session["FacturaEspecial" + Session.SessionID]; }
            set { Session["FacturaEspecial" + Session.SessionID] = value; }
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { // Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Page.IsPostBack)
                    this.Inicializar();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
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
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgFacturaEspecialDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgFacturaEspecialDet.DataSource = this.ListaProductosFacturaEspecial;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaEspecialDet_NeedDataSource"));
            }
        }
        protected void rgFacturaEspecialDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editItem.FindControl("txtRem_Cantidad");
                    string lblRem_Cantidad = ((Label)editItem.FindControl("lblVal_txtRem_Cantidad")).ClientID.ToString();
                    string txtRem_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    string lblVal_txtPrd_Descripcion = ((Label)editItem.FindControl("lblVal_txtPrd_Descripcion")).ClientID.ToString();
                    string txtPrd_Descripcion = ((RadTextBox)editItem.FindControl("txtPrd_Descripcion")).ClientID.ToString();
                    string lblVal_txtPrd_Presentacion = ((Label)editItem.FindControl("lblVal_txtPrd_Presentacion")).ClientID.ToString();
                    string txtPrd_Presentacion = ((RadTextBox)editItem.FindControl("txtPrd_Presentacion")).ClientID.ToString();
                    string lblVal_txtPrd_UniNe = ((Label)editItem.FindControl("lblVal_txtPrd_UniNe")).ClientID.ToString();
                    string txtPrd_UniNe = ((RadTextBox)editItem.FindControl("txtPrd_UniNe")).ClientID.ToString();
                    string lblVal_txtFac_Precio = ((Label)editItem.FindControl("lblVal_txtRem_Precio")).ClientID.ToString();
                    string txtRem_Precio = ((RadNumericTextBox)editItem.FindControl("txtRem_Precio")).ClientID.ToString();

                    //Llenar combo de productos
                    RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    CargarProductos(comboProductoItem);

                    string jsControles = string.Concat(
                        "lblRem_CantidadClientId='", lblRem_Cantidad, "';"
                        , "txtRem_CantidadClientId='", txtRem_Cantidad, "';"
                        , "lbl_cmbProductoClientId='", lbl_cmbProducto, "';"
                        , "txtId_PrdClientId='", txtId_Prd, "';"
                        , "lblVal_txtPrd_DescripcionClientId='", lblVal_txtPrd_Descripcion, "';"
                        , "txtPrd_DescripcionClientId='", txtPrd_Descripcion, "';"
                        , "lblVal_txtPrd_PresentacionClientId='", lblVal_txtPrd_Presentacion, "';"
                        , "txtPrd_PresentacionClientId='", txtPrd_Presentacion, "';"
                        , "lblVal_txtPrd_UniNeClientId='", lblVal_txtPrd_UniNe, "';"
                        , "txtPrd_UniNeClientId='", txtPrd_UniNe, "';"
                        , "lblVal_txtRem_PrecioClientId='", lblVal_txtFac_Precio, "';"
                        , "txtRem_PrecioClientId='", txtRem_Precio, "';"
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
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaEspecialDet_ItemDataBound"));
            }
        }
        protected void rgFacturaEspecialDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            FacturaDet FacturaDet = new FacturaDet();
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                FacturaDet.Id_Emp = sesion.Id_Emp;
                FacturaDet.Id_Cd = sesion.Id_Cd_Ver;
                FacturaDet.Id_Fac = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                FacturaDet.Id_FacDet = 0;
                FacturaDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//cliente de datos generales de la factura
                FacturaDet.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                FacturaDet.Fac_CantE = (insertedItem["Fac_CantE"].FindControl("txtRem_Cantidad") as RadNumericTextBox).Value.HasValue ? float.Parse((insertedItem["Fac_CantE"].FindControl("txtRem_Cantidad") as RadNumericTextBox).Value.Value.ToString()) : 0;
                FacturaDet.Clp_Release = (insertedItem["Clp_Release"].FindControl("txtClp_ReleaseEdit") as RadTextBox).Text;
                double precio = (insertedItem["Fac_Precio"].FindControl("txtRem_Precio") as RadNumericTextBox).Value.HasValue ? (double)(insertedItem["Fac_Precio"].FindControl("txtRem_Precio") as RadNumericTextBox).Value.Value : 0;
                (insertedItem["Fac_Importe"].FindControl("lblRem_ImporteEdit") as Label).Text = FacturaDet.Fac_ImporteE.ToString();
                FacturaDet.Fac_Precio = precio;

                //datos del producto de la orden de compra
                FacturaDet.Producto = new Producto();
                FacturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                FacturaDet.Producto.Id_Emp = sesion.Id_Emp;
                FacturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                FacturaDet.Producto.Id_PrdEsp = (insertedItem.FindControl("txtId_PrdEsp") as RadTextBox).Text; ///<------
                FacturaDet.Producto.Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).Text;
                FacturaDet.Producto.Prd_DescripcionEspecial = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                FacturaDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                FacturaDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;

                //agregar producto de orden de compra a la lista
                this.ListaProductosFacturaEspecial_AgregarProducto(FacturaDet);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgFacturaEspecial_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgFacturaEspecialDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            FacturaDet FacturaDet = new FacturaDet();
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                FacturaDet.Id_Emp = sesion.Id_Emp;
                FacturaDet.Id_Cd = sesion.Id_Cd_Ver;
                FacturaDet.Id_Fac = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                FacturaDet.Id_FacDet = 0;
                FacturaDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//cliente de datos generales de la factura
                FacturaDet.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);


                FacturaDet.Fac_CantE = (insertedItem["Fac_CantE"].FindControl("txtRem_Cantidad") as RadNumericTextBox).Value.HasValue ? float.Parse((insertedItem["Fac_CantE"].FindControl("txtRem_Cantidad") as RadNumericTextBox).Value.Value.ToString()) : 0;
                FacturaDet.Clp_Release = (insertedItem["Clp_Release"].FindControl("txtClp_ReleaseEdit") as RadTextBox).Text;
                double precioPartida = (insertedItem["Fac_Precio"].FindControl("txtRem_Precio") as RadNumericTextBox).Value.HasValue ? Convert.ToDouble((insertedItem["Fac_Precio"].FindControl("txtRem_Precio") as RadNumericTextBox).Value.Value) : 0;
              
                
                (insertedItem["Fac_Importe"].FindControl("lblRem_ImporteEdit") as Label).Text = FacturaDet.Fac_ImporteE.ToString();
                FacturaDet.Fac_Precio = precioPartida;
                

                //datos del producto de la orden de compra
                FacturaDet.Producto = new Producto();
                FacturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                FacturaDet.Producto.Id_Emp = sesion.Id_Emp;
                FacturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                FacturaDet.Producto.Id_PrdEsp = (insertedItem.FindControl("txtId_PrdEsp") as RadTextBox).Text;
                FacturaDet.Producto.Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).Text;
                FacturaDet.Producto.Prd_DescripcionEspecial = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                FacturaDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                FacturaDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;

                //agregar producto de orden de compra a la lista
                this.ListaProductosFacturaEspecial_ModificarProducto(FacturaDet, e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgFacturaEspecial_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgFacturaEspecialDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                string descripcion = ((Label)item["Prd_Descripcion"].FindControl("lblPrd_Descripcion")).Text;
                //actualizar producto de orden de compra a la lista
                ListaProductosFacturaEspecial_EliminarProducto(e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgOrdCompra_delete_item_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgFacturaEspecialDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgFacturaEspecialDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void rgFacturaEspecialDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgFacturaEspecialDet.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
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
                    RadComboBox combo = (RadComboBox)sender;
                    //obtiene la tabla contenedora de los controles de edición de registro del Grid
                    Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                    RadTextBox txtPrd_Descripcion = (RadTextBox)tabla.FindControl("txtPrd_Descripcion");
                    RadTextBox txtPrd_Presentacion = (RadTextBox)tabla.FindControl("txtPrd_Presentacion");
                    RadTextBox txtPrd_UniNe = (RadTextBox)tabla.FindControl("txtPrd_UniNe");
                    RadNumericTextBox txtRem_Cantidad = (RadNumericTextBox)tabla.FindControl("txtRem_Cantidad");
                    RadNumericTextBox txtRem_Precio = (RadNumericTextBox)tabla.FindControl("txtRem_Precio");

                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);
                    int id_Cd_Prod = sesion.Id_Cd_Ver;

                    Producto producto = new Producto();
                    if (e.Value != string.Empty && e.Value != "-1")
                    {
                        //obtener datos de producto
                        CN_CatProducto clsProducto = new CN_CatProducto();
                        clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, Convert.ToInt32(e.Value),1);
                    }
                    txtRem_Precio.Text = "";
                    txtPrd_Descripcion.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
                    txtPrd_Presentacion.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                    txtPrd_UniNe.Text = producto == null ? string.Empty : producto.Prd_UniNe;

                    //este evento es porque se elige producto, por lo que 
                    //se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                    txtRem_Cantidad.Enabled = true;
                    txtRem_Cantidad.Text = string.Empty;

                    txtPrd_Descripcion.Focus();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbProducto_IndexChanging_error"));
            }
        }
        #endregion

        #region Funciones
        private void Inicializar()
        {
            try
            {
                if (!Convert.ToBoolean(Request.QueryString["Modificar"]))
                    RadToolBar1.Items[1].Visible = false;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                this.HD_Cliente.Value = Request.QueryString["Id_Cte"].ToString();
                this.HD_Moneda.Value = Request.QueryString["Id_Mon"].ToString();
                this.HD_ImporteTotal.Value = Convert.ToDouble(Request.QueryString["Fac_ImporteTotal"]).ToString();
                this.HD_IVARemision.Value = Request.QueryString["IVAfacturacion"].ToString();
                //Desc1 = Convert.ToDouble((Request.QueryString["Descuento1"]).ToString());
                //Desc2 = Convert.ToDouble((Request.QueryString["Descuento2"]).ToString());


                if (Session["FacEspecialGuardada" + Session.SessionID] != null)
                    if (Session["FacEspecialGuardada" + Session.SessionID].ToString() != "1")
                        ListaProductosFacturaEspecial = new List<FacturaDet>();

                if (ListaProductosFacturaEspecial != null && ListaProductosFacturaEspecial.Count > 0)
                {
                    if (ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial == null)
                        ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial = "";

                    ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial = (ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)).Length > 0 ? (ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))[0] : "";
                }
                else
                {
                    ListaProductosFacturaEspecial = new List<FacturaDet>();

                    string folio = Request.QueryString["Folio"].ToString();
                    Factura remision = new Factura();
                    remision.Id_Emp = sesion.Id_Emp;
                    remision.Id_Cd = sesion.Id_Cd_Ver;
                    remision.Id_Fac = !string.IsNullOrEmpty(folio) ? Convert.ToInt32(folio) : 0;

                    List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
                    if (!string.IsNullOrEmpty(folio))
                    {
                        new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                            , sesion.Emp_Cnx
                            , sesion.Id_Emp
                            , sesion.Id_Cd_Ver
                            , Convert.ToInt32(folio)
                            , Convert.ToInt32(this.HD_Cliente.Value));
                    }

                    if (listaProdFacturaEspecialFinal.Count != 0)
                        ListaProductosFacturaEspecial = listaProdFacturaEspecialFinal;
                    else
                    {
                        //// -------------------------------------------------------------------------------------------
                        //// obtener claves de productos de Factura original
                        //// -------------------------------------------------------------------------------------------
                        string clavesProducto = string.Empty;
                        List<FacturaDet> listProdFact = new List<FacturaDet>();

                        foreach (DataRow dr in dt_detalles.Rows)
                        {
                            if (!clavesProducto.Contains(string.Format("{0}|", dr["Id_Prd"])))
                                clavesProducto = clavesProducto + dr["Id_Prd"] + "|";

                            FacturaDet fDet = new FacturaDet();
                            fDet.Id_Prd = Convert.ToInt32(dr["Id_Prd"]);
                            fDet.Id_Ter = Convert.ToInt32(dr["Id_Ter"]);
                            fDet.Fac_CantE = Convert.ToInt32(dr["Fac_Cant"]);
                            fDet.Fac_Precio = Convert.ToDouble(dr["Fac_Precio"]);
                            listProdFact.Add(fDet);
                        }
                        // -------------------------------------------------------------------------------------------
                        // consulta productos de facturacion especial en el catalogo de Cliente-Producto en base a los productos de la Remision original
                        // -------------------------------------------------------------------------------------------
                        List<FacturaDet> listaProdFacturaEspecial = new List<FacturaDet>();

                        if (!string.IsNullOrEmpty(this.HD_Cliente.Value))
                            new CN_CatClienteProd().ConsultaClienteProd_FacturaEspecial(ref listaProdFacturaEspecial
                                , sesion.Emp_Cnx
                                , sesion.Id_Emp
                                , sesion.Id_Cd_Ver
                                , Convert.ToInt32(this.HD_Cliente.Value)
                                , clavesProducto);
                                                                       
                        IEnumerable<FacturaDet> grupedDT = from dt in listProdFact
                                       group dt by dt.Id_Prd into grpDT
                                       select new FacturaDet
                                       {
                                           Id_Prd = grpDT.Key,
                                           Fac_CantE = grpDT.Sum(a => a.Fac_CantE),
                                           Fac_Precio = grpDT.Max(a => a.Fac_Precio)
                                       };

                        var joinedList = from dt in grupedDT
                                         join lt in listaProdFacturaEspecial
                                            on dt.Id_Prd equals lt.Id_Prd into jProd
                                         from subQry in jProd.DefaultIfEmpty()
                                         select new {
                                            lProds = dt,
                                            lProdsE = subQry
                                         };

                        // -------------------------------------------------------------------------------------------
                        // Crear partidas adicionales de remision si es que la descripción viene con separadores "|"
                        // -------------------------------------------------------------------------------------------
                        foreach (var jItem in joinedList)
                        {
                            FacturaDet cFacturaDet = jItem.lProdsE;
                            cFacturaDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//actualiza el cliente de la partida que es el cliente de la fact. original
                            string[] descripcion = cFacturaDet.Producto.Prd_DescripcionEspecial.Split(new char[] { '|' });

                            for (int j = 0; j < descripcion.Length; j++)
                            {
                                FacturaDet remisionCopia = new FacturaDet();
                                remisionCopia.Producto = new Producto();
                                remisionCopia.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);
                                remisionCopia.Id_Prd = cFacturaDet.Id_Prd;
                                remisionCopia.Producto.Id_PrdEsp = cFacturaDet.Producto.Id_PrdEsp.ToString();
                                remisionCopia.Producto.Id_Prd = cFacturaDet.Producto.Id_Prd;
                                remisionCopia.Producto.Prd_Descripcion = cFacturaDet.Producto.Prd_Descripcion;
                                remisionCopia.Producto.Prd_Presentacion = cFacturaDet.Producto.Prd_Presentacion;
                                remisionCopia.Producto.Prd_UniNe = cFacturaDet.Producto.Prd_UniNe;
                                remisionCopia.Producto.Prd_InvFinal = cFacturaDet.Producto.Prd_InvFinal;
                                remisionCopia.Producto.Prd_DescripcionEspecial = descripcion[j];
                                try
                                {                                    
                                    remisionCopia.Fac_Precio = jItem.lProds.Fac_Precio;
                                }
                                catch (Exception)
                                {
                                }
                                remisionCopia.Id_Emp = sesion.Id_Emp;
                                remisionCopia.Id_Cd = sesion.Id_Cd_Ver;
                                if (j == 0)
                                {
                                    try
                                    {
                                        //remisionCopia.Fac_CantE = Convert.ToInt32(dt_detalles.Select("Id_Prd=" + FacturaDet.Id_Prd)[0]["Fac_Cant"]);
                                        remisionCopia.Fac_CantE = jItem.lProds.Fac_CantE;
                                    }
                                    catch
                                    {
                                        remisionCopia.Fac_CantE = 0;
                                    }
                                }
                                else
                                {
                                    remisionCopia.Fac_CantE = 0;
                                }
                                ListaProductosFacturaEspecial.Add(remisionCopia);
                            }
                        }
                    }
                }
                double ancho = 0;
                foreach (GridColumn gc in rgFacturaEspecialDet.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgFacturaEspecialDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgFacturaEspecialDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                this.CalcularTotales();
                rgFacturaEspecialDet.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularTotales()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<FacturaDet> lista = this.ListaProductosFacturaEspecial;
            double importeTotal = 0;
            //if ( Convert.ToDouble((Request.QueryString["Descuento1"]).ToString())  )

          

            double porcDescuento1 = (Request.QueryString["Descuento1"]) == null ? 0 :  Convert.ToDouble((Request.QueryString["Descuento1"]).ToString());
            double porcDescuento2 = (Request.QueryString["Descuento2"]) == null ? 0 : Convert.ToDouble((Request.QueryString["Descuento2"]).ToString());

            for (int i = 0; i < lista.Count; i++)
            {
                FacturaDet rem = lista[i];
                importeTotal += rem.Fac_ImporteE;
            }
            CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
            double iva = 0;
            cn_cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva, sesion.Emp_Cnx);

            txtImporte.Text = importeTotal.ToString();
            importeTotal = porcDescuento1 > 0 ? (importeTotal - (importeTotal * (porcDescuento1 / 100))) : importeTotal;
            importeTotal = porcDescuento2 > 0 ? (importeTotal - (importeTotal * (porcDescuento2 / 100))) : importeTotal;
            txtSubTotal.Text = importeTotal.ToString();
            txtIVA.Text = HD_IVARemision.Value.Trim() != string.Empty ? (importeTotal * iva / 100).ToString() : "0";
            txtTotal.Text = (Convert.ToSingle(txtSubTotal.Text) + Convert.ToSingle(txtIVA.Text)).ToString();
            Session["FacEspecialGuardada" + Session.SessionID] = 2;
        }
        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = 0;
                //Guardar los datos de los productos de factura especial
                //en catálogo de Cliente-Producto
                List<FacturaDet> ListaPrdRemEspecial = new List<FacturaDet>();
                for (int i = 0; i < this.ListaProductosFacturaEspecial.Count; i++)
                    ListaPrdRemEspecial.Add((FacturaDet)this.ListaProductosFacturaEspecial[i]);


                //Datos del centro de distribución
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);


                if (Session["fTotalFactura" + Session.SessionID] != null)
                {
                    if (txtTotal.Text.Length > 0)
                    {
                        decimal fTotalFacturaOriginal = decimal.Round(decimal.Parse(Session["fTotalFactura" + Session.SessionID].ToString()),2);

                        if (((fTotalFacturaOriginal + (decimal)cd.Cd_MargenDiferenciaDocs) < decimal.Parse(txtTotal.Text)) || ((fTotalFacturaOriginal - (decimal)cd.Cd_MargenDiferenciaDocs) > decimal.Parse(txtTotal.Text)))
                        {
                            Alerta("El monto Total de la Factura especial tiene una diferencia considerable con respecto a la Factura original.");
                            return;
                        }
                    }
                }

                new CN_CatClienteProd().ModificarClienteProdFacturaEspecial(ListaPrdRemEspecial, session.Emp_Cnx, ref verificador);

                //SET variable de encabezado de factura especial
                FacturaEspecial facturaEsp = new FacturaEspecial();
                facturaEsp.Id_Emp = session.Id_Emp;
                facturaEsp.Id_Cd = session.Id_Cd_Ver;
                facturaEsp.FacEsp_Importe = Convert.ToDouble(txtImporte.Text);
                facturaEsp.FacEsp_SubTotal = Convert.ToDouble(txtSubTotal.Text);
                facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(txtIVA.Text);
                facturaEsp.FacEsp_Total = Convert.ToDouble(txtTotal.Text);
                this.FacturaEspecial = facturaEsp;

                Session["FacEspecialGuardada" + Session.SessionID] = "1";

                string mensaje = "Los datos se guardaron correctamente";
                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_FacturaEspecial('", mensaje, "')")); //cerrar ventana radWindow de factura especial
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected string ObtenerIdEspecial(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Id_PrdEsp; } else { return string.Empty; }
        }
        protected string ObtenerDescripcionEspecial(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_DescripcionEspecial; } else { return string.Empty; }
        }
        protected string ObtenerDescripcion(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }
        protected string ObtenerPresentacion(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }
        protected string ObtenerUnidades(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_UniNe; } else { return string.Empty; }
        }
        protected int ObtenerInvFinal(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_InvFinal; } else { return 0; }
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
                    prd.Prd_Descripcion = item["Prd_Descripcion"].ToString();
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
        protected void ListaProductosFacturaEspecial_AgregarProducto(FacturaDet remision_prod)
        {
            try
            {
                List<FacturaDet> lista = this.ListaProductosFacturaEspecial;

                lista.Add(remision_prod);
                this.ListaProductosFacturaEspecial = lista;
                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaProductosFacturaEspecial_ModificarProducto(FacturaDet remision_prod, int index)
        {
            try
            {
                List<FacturaDet> lista = this.ListaProductosFacturaEspecial;
                //buscar producto de factura en la lista           
                FacturaDet remision = lista[index];
                if (remision.Id_Prd == remision_prod.Id_Prd)
                    lista[index] = remision_prod;

                this.ListaProductosFacturaEspecial = lista;
                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaProductosFacturaEspecial_EliminarProducto(int index)
        {
            try
            {
                List<FacturaDet> lista = this.ListaProductosFacturaEspecial;
                lista.RemoveAt(index);

                this.ListaProductosFacturaEspecial = lista;
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
                            Alerta("Error al momento de eliminar el producto a la lista de productos de la remisión");
                        else
                            if (mensaje.Contains("CapFacturaEspecial_insert_error"))
                                Alerta("Error al momento de guardar los datos de remisión especial");
                            else
                                if (mensaje.Contains("rgFacturaEspecial_insert_error"))
                                    Alerta("Error al momento de agregar el producto a la lista");
                                else
                                    if (mensaje.Contains("rgFacturaEspecialDet_ItemDataBound"))
                                        Alerta("Error al momento de preparar un registro para edici&oacute;n");
                                    else
                                        if (mensaje.Contains("rgFacturaEspecialDet_NeedDataSource"))
                                            Alerta("Error al cargar el grid de detalle de remisión");
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

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 120);
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
    }
}
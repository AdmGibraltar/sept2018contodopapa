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
using System.Xml;

namespace SIANWEB
{
    public partial class CapRemisiones_Especial : System.Web.UI.Page
    {
        public DataTable dt_detalles
        {
            get
            {
                return Session["dt_DetallesRem" + Session.SessionID] as DataTable;
            }
            set
            {
                Session["dt_DetallesRem" + Session.SessionID] = value;
            }
        }
        #region Propiedades
        private List<RemisionDet> ListaProductosRemisionEspecial
        {
            get { return (List<RemisionDet>)Session["ListaProductosRemisionEspecial" + Session.SessionID]; }
            set { Session["ListaProductosRemisionEspecial" + Session.SessionID] = value; }
        }
        private FacturaEspecial RemisionEspecial
        {
            get { return (FacturaEspecial)Session["RemisionEspecial" + Session.SessionID]; }
            set { Session["RemisionEspecial" + Session.SessionID] = value; }
        }

         private int Id_CuentaNacional
        {
            get { return (int)Session["Id_CuentaNacional" + Session.SessionID]; }
            set { Session["Id_CuentaNacional" + Session.SessionID ] = value; }
            
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
                        mensajeError = "CapRemisionEspecial_insert_error";
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
        protected void rgRemisionEspecialDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgRemisionEspecialDet.DataSource = this.ListaProductosRemisionEspecial;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgRemisionEspecialDet_NeedDataSource"));
            }
        }
        protected void rgRemisionEspecialDet_ItemDataBound(object sender, GridItemEventArgs e)
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
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgRemisionEspecialDet_ItemDataBound"));
            }
        }
        protected void rgRemisionEspecialDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            RemisionDet remisionDet = new RemisionDet();
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                remisionDet.Id_Emp = sesion.Id_Emp;
                remisionDet.Id_Cd = sesion.Id_Cd_Ver;
                remisionDet.Id_Rem = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                remisionDet.Id_RemDet = 0;
                remisionDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//cliente de datos generales de la factura
                remisionDet.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                int cantidad = (insertedItem["Rem_Cant"].FindControl("txtRem_Cantidad") as RadNumericTextBox).Value.HasValue ? (int)(insertedItem["Rem_Cant"].FindControl("txtRem_Cantidad") as RadNumericTextBox).Value.Value : 0;
                remisionDet.Rem_Cant = Convert.ToInt32(cantidad);
                remisionDet.Clp_Release = (insertedItem["Clp_Release"].FindControl("txtClp_ReleaseEdit") as RadTextBox).Text;
                double precio = (insertedItem["Rem_Precio"].FindControl("txtRem_Precio") as RadNumericTextBox).Value.HasValue ? (double)(insertedItem["Rem_Precio"].FindControl("txtRem_Precio") as RadNumericTextBox).Value.Value : 0;
                (insertedItem["Rem_Importe"].FindControl("lblRem_ImporteEdit") as Label).Text = precio.ToString();
                remisionDet.Rem_Importe = (precio * remisionDet.Rem_Cant);
                remisionDet.Rem_Precio = precio;

                //datos del producto de la orden de compra
                remisionDet.Producto = new Producto();
                remisionDet.Producto.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                remisionDet.Producto.Id_Emp = sesion.Id_Emp;
                remisionDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                remisionDet.Producto.Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).Text;
                remisionDet.Producto.Prd_DescripcionEspecial = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                remisionDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                remisionDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;
                remisionDet.Producto.Id_PrdEsp = (insertedItem.FindControl("txtId_PrdEsp") as RadTextBox).Text;

                if (Request.QueryString["Id_Tmov"].ToString() == "54")
                {
                    CN_CatCliente cn_catcliente = new CN_CatCliente();
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Cte = Convert.ToInt32(Request.QueryString["Id_Cte"]);
                    cn_catcliente.ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    if (remisionDet.Producto.Id_Prd.ToString() == remisionDet.Producto.Id_PrdEsp && cliente.Cte_RemisionElectronica == 1)
                    {
                        Alerta("El codigo Key y el codigo del cliente no pueden ser iguales");
                        e.Canceled = true;
                        return;
                    }

                    if (remisionDet.Producto.Id_Prd.ToString() == "" && cliente.Cte_RemisionElectronica == 1)
                    {
                        Alerta("El codigo Key es requerido");
                        e.Canceled = true;
                        return;
                    }

                    if (remisionDet.Producto.Id_PrdEsp == "" && cliente.Cte_RemisionElectronica == 1)
                    {
                        Alerta("El codigo del cliente es requerido");
                        e.Canceled = true;
                        return;
                    }

                    if (remisionDet.Clp_Release == "" && cliente.Cte_RemisionElectronica == 1)
                    {
                        Alerta("El release es requerido");
                        e.Canceled = true;
                        return;
                    }
                }







                //agregar producto de orden de compra a la lista
                this.ListaProductosRemisionEspecial_AgregarProducto(remisionDet);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRemisionEspecial_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgRemisionEspecialDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            RemisionDet remisionDet = new RemisionDet();
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                remisionDet.Id_Emp = sesion.Id_Emp;
                remisionDet.Id_Cd = sesion.Id_Cd_Ver;
                remisionDet.Id_Rem = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                remisionDet.Id_RemDet = 0;
                remisionDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//cliente de datos generales de la factura
                remisionDet.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                remisionDet.Rem_Cant = Convert.ToInt32((insertedItem["Rem_Cant"].FindControl("txtRem_Cantidad") as RadNumericTextBox).Text);
                remisionDet.Clp_Release = (insertedItem["Clp_Release"].FindControl("txtClp_ReleaseEdit") as RadTextBox).Text;
                double precioPartida = Convert.ToDouble((insertedItem["Rem_Precio"].FindControl("txtRem_Precio") as RadNumericTextBox).Value.Value);
                (insertedItem["Rem_Importe"].FindControl("lblRem_ImporteEdit") as Label).Text = precioPartida.ToString();
                remisionDet.Rem_Importe = (precioPartida * remisionDet.Rem_Cant);
                remisionDet.Rem_Precio = precioPartida;

                //datos del producto de la orden de compra
                remisionDet.Producto = new Producto();
                remisionDet.Producto.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                remisionDet.Producto.Id_Emp = sesion.Id_Emp;
                remisionDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                remisionDet.Producto.Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).Text;
                remisionDet.Producto.Prd_DescripcionEspecial = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                remisionDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                remisionDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;
                remisionDet.Producto.Id_PrdEsp = (insertedItem.FindControl("txtId_PrdEsp") as RadTextBox).Text; ///<------
                //agregar producto de orden de compra a la lista



                if (Request.QueryString["Id_Tmov"].ToString() == "54")
                {
                    CN_CatCliente cn_catcliente = new CN_CatCliente();
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Cte = Convert.ToInt32(Request.QueryString["Id_Cte"]);
                    cn_catcliente.ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    if (remisionDet.Producto.Id_Prd.ToString() == remisionDet.Producto.Id_PrdEsp && cliente.Cte_RemisionElectronica == 1)
                    {
                        Alerta("El codigo Key y el codigo del cliente no pueden ser iguales");
                        e.Canceled = true;
                        return;
                    }

                    if (remisionDet.Producto.Id_Prd.ToString() == "" && cliente.Cte_RemisionElectronica == 1)
                    {
                        Alerta("El codigo Key es requerido");
                        e.Canceled = true;
                        return;
                    }

                    if (remisionDet.Producto.Id_PrdEsp == "" && cliente.Cte_RemisionElectronica == 1)
                    {
                        Alerta("El codigo del cliente es requerido");
                        e.Canceled = true;
                        return;
                    }

                    if (remisionDet.Clp_Release == "" && cliente.Cte_RemisionElectronica == 1)
                    {
                        Alerta("El release es requerido");
                        e.Canceled = true;
                        return;
                    }
                }

                this.ListaProductosRemisionEspecial_ModificarProducto(remisionDet, e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRemisionEspecial_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgRemisionEspecialDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                string descripcion = ((Label)item["Prd_Descripcion"].FindControl("lblPrd_Descripcion")).Text;
                //actualizar producto de orden de compra a la lista
                ListaProductosRemisionEspecial_EliminarProducto(e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgOrdCompra_delete_item_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgRemisionEspecialDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgRemisionEspecialDet.EditItems.Count > 0)
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
        protected void rgRemisionEspecialDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgRemisionEspecialDet.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //try
            //{
            //    if (e.Item.Value == "-1")
            //    {
            //        e.Item.FindControl("liComprasLocales").Controls.Clear();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DisplayMensajeAlerta(string.Concat(ex.Message, "cmbProductosLista_ItemsDataBound"));
            //}
        }
        protected void cmbProducto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {  //verificar que el producto este el segmento
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
                    RadTextBox txtId_PrdEsp = (RadTextBox)tabla.FindControl("txtId_PrdEsp");
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
                    txtId_PrdEsp.Text = producto == null ? string.Empty : producto.Id_Prd.ToString();
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
                {
                    RadToolBar1.Items[1].Visible = false;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                this.HD_Cliente.Value = Request.QueryString["Id_Cte"].ToString();
                this.HD_Moneda.Value = "";
                this.HD_ImporteTotal.Value = Request.QueryString["Rem_ImporteTotal"].ToString();
                this.HD_IVARemision.Value = Request.QueryString["IVARem"].ToString();

                if (Session["RemEspecialGuardada" + Session.SessionID] != null)
                {
                    if (Session["RemEspecialGuardada" + Session.SessionID].ToString() != "1")
                    {
                        ListaProductosRemisionEspecial = new List<RemisionDet>();
                    }
                }

                if (ListaProductosRemisionEspecial != null && ListaProductosRemisionEspecial.Count > 0)
                {
                    if (ListaProductosRemisionEspecial[0].Producto.Prd_DescripcionEspecial == null)
                    {
                        ListaProductosRemisionEspecial[0].Producto.Prd_DescripcionEspecial = "";
                    }
                    ListaProductosRemisionEspecial[0].Producto.Prd_DescripcionEspecial = (ListaProductosRemisionEspecial[0].Producto.Prd_DescripcionEspecial.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)).Length > 0 ? (ListaProductosRemisionEspecial[0].Producto.Prd_DescripcionEspecial.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))[0] : "";
                }
                else
                {
                    ListaProductosRemisionEspecial = new List<RemisionDet>();

                    string folio = Request.QueryString["Folio"].ToString();
                    Remision remision = new Remision();
                    remision.Id_Emp = sesion.Id_Emp;
                    remision.Id_Cd = sesion.Id_Cd_Ver;
                    remision.Id_Rem = !string.IsNullOrEmpty(folio) ? Convert.ToInt32(folio) : 0;

                    List<RemisionDet> listaProdFacturaEspecialFinal = new List<RemisionDet>();
                    if (!string.IsNullOrEmpty(folio))
                    {
                        new CN_CapRemision().ConsultaRemisionEspecialDetalle(ref listaProdFacturaEspecialFinal
                            , sesion.Emp_Cnx
                            , sesion.Id_Emp
                            , sesion.Id_Cd_Ver
                            , Convert.ToInt32(folio)
                            , Convert.ToInt32(this.HD_Cliente.Value));
                    }

                    if (listaProdFacturaEspecialFinal.Count != 0)
                        ListaProductosRemisionEspecial = listaProdFacturaEspecialFinal;
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
                        // consulta productos de Remision especial en el catalogo de Cliente-Producto en base a los productos de la Remision original
                        // -------------------------------------------------------------------------------------------
                        List<RemisionDet> listaProdRemisionEspecial = new List<RemisionDet>();

                        if (!string.IsNullOrEmpty(this.HD_Cliente.Value))
                            new CN_CatClienteProd().ConsultaClienteProd_RemisionEspecial(ref listaProdRemisionEspecial
                                , sesion.Emp_Cnx
                                , sesion.Id_Emp
                                , sesion.Id_Cd_Ver
                                , Convert.ToInt32(this.HD_Cliente.Value)
                                , clavesProducto);

                        /*
                        int Id_Prd = 0;
                        List<Producto> lProducto = new List<Producto>();
                        foreach (DataRow dr in dt_detalles.Rows)
                        {
                            Id_Prd = (int) dr["Id_Prd"];                      

                            
                            if (Id_CuentaNacional != null || Id_CuentaNacional > 0)
                            {
                                    Producto oneProducto = new Producto();
                                    WS_Producto.Service1 ws = new WS_Producto.Service1();
                                    ws.Url = ConfigurationManager.AppSettings["WS_Producto"].ToString();

                                    string envio = "" + Id_CuentaNacional + "|" + Id_Prd + "";
                                    object respuesta = ws.TraeProductoCN(envio);
                                    XmlDocument Xml = new XmlDocument();
                                    Xml.LoadXml(respuesta.ToString());

                                    XmlNode NodeError = Xml.SelectSingleNode("//MsgError/@error");
                                    XmlNode NodeValida = Xml.SelectSingleNode("//Valida/@ValidaCodEsp");
                                    XmlNode NodeProductoID = Xml.SelectSingleNode("//Producto/@ProNum");
                                    XmlNode NodeProductoDesc = Xml.SelectSingleNode("//Producto/@ProDesc");
                                    XmlNode NodeProUM = Xml.SelectSingleNode("//Producto/@ProUM");
                                    XmlNode NodeProPrecio = Xml.SelectSingleNode("//Producto/@ProPrecio");
                                 //   oneProducto.Id_Prd = (int)NodeProductoID.InnerText;
                                    oneProducto.Prd_DescripcionEspecial = NodeProductoDesc.InnerText;
                                    oneProducto.Prd_UniNe = NodeProUM.InnerText;
                                    oneProducto.Prd_Precio = NodeProPrecio.InnerText;
                                    lProducto.Add(oneProducto);

                                }
                        }*/

                        string DescripcionEspecial = null;
                        float Precio = 0;
                        string uMedida= null;
                        // -------------------------------------------------------------------------------------------
                        // Crear partidas adicionales de remision si es que la descripción viene con separadores "|"
                        // -------------------------------------------------------------------------------------------
                        for (int i = 0; i < listaProdRemisionEspecial.Count; i++)
                        {
                            RemisionDet remisionDet = listaProdRemisionEspecial[i];
                            remisionDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//actualiza el cliente de la partida que es el cliente de la fact. original
                            string[] descripcion = remisionDet.Producto.Prd_DescripcionEspecial.Split(new char[] { '|' });

                            for (int j = 0; j < descripcion.Length; j++)
                            {
                                
                                RemisionDet remisionCopia = new RemisionDet();
                                /*
                                if (lProducto.Count > 0)
                                {
                                  DescripcionEspecial   = lProducto.Find(x => x.Id_Prd == remisionCopia.Id_Prd).Prd_DescripcionEspecial;
                                 // Precio = (float)lProducto.Find(x => x.Id_Prd == remisionCopia.Id_Prd).Prd_Precio;
                                  uMedida = lProducto.Find(x => x.Id_Prd == remisionCopia.Id_Prd).Prd_UniNe;
                                }*/
                                //remisionCopia = (RemisionDet)remisionDet;
                                remisionCopia.Producto = new Producto();
                                remisionCopia.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);
                                remisionCopia.Id_Prd = remisionDet.Id_Prd;
                                remisionCopia.Producto.Id_PrdEsp = remisionDet.Producto.Id_PrdEsp;
                                remisionCopia.Producto.Id_Prd = remisionDet.Producto.Id_Prd;
                                remisionCopia.Producto.Prd_Descripcion = remisionDet.Producto.Prd_Descripcion;
                                remisionCopia.Producto.Prd_Presentacion = remisionDet.Producto.Prd_Presentacion;
                                remisionCopia.Producto.Prd_UniNe =  remisionDet.Producto.Prd_UniNe;
                                remisionCopia.Producto.Prd_InvFinal = remisionDet.Producto.Prd_InvFinal;
                                remisionCopia.Producto.Prd_DescripcionEspecial =  descripcion[j];
                                try
                                {
                                    remisionCopia.Rem_Precio = (Precio == 0) ? Precio : Convert.ToDouble(dt_detalles.Select("Id_Prd=" + remisionDet.Id_Prd)[0]["Precio"]);
                                }
                                catch
                                {
                                    remisionCopia.Rem_Precio = 0;

                                }

                                remisionCopia.Id_Emp = sesion.Id_Emp;
                                remisionCopia.Id_Cd = sesion.Id_Cd_Ver;


                                try
                                {
                                    remisionCopia.Rem_Importe = Convert.ToDouble(dt_detalles.Select("Id_Prd=" + remisionDet.Id_Prd)[0]["Importe"]);
                                }
                                catch
                                {
                                    remisionCopia.Rem_Importe = 0;
                                }

                                if (j == 0)
                                {
                                    try
                                    {
                                        remisionCopia.Rem_Cant = Convert.ToInt32(dt_detalles.Select("Id_Prd=" + remisionDet.Id_Prd)[0]["Cantidad"]);
                                    }
                                    catch
                                    {
                                        remisionCopia.Rem_Cant = 0;
                                    }

                                }
                                else
                                {
                                    remisionCopia.Rem_Cant = 0;
                                }
                                ListaProductosRemisionEspecial.Add(remisionCopia);
                            }
                        }
                    }
                }
                double ancho = 0;
                foreach (GridColumn gc in rgRemisionEspecialDet.Columns)
                {
                    if (gc.Display)
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                }
                rgRemisionEspecialDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgRemisionEspecialDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                this.CalcularTotales();
                this.rgRemisionEspecialDet.Rebind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CalcularTotales()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<RemisionDet> lista = this.ListaProductosRemisionEspecial;
            double importeTotal = 0;
            float porcDescuento1 = HD_Descuento1.Value != string.Empty ? Convert.ToInt32(HD_Descuento1.Value) : 0;
            float porcDescuento2 = HD_Descuento2.Value != string.Empty ? Convert.ToInt32(HD_Descuento2.Value) : 0;

            for (int i = 0; i < lista.Count; i++)
            {
                RemisionDet rem = lista[i];
                importeTotal += rem.Rem_Importe;
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
            Session["RemEspecialGuardada" + Session.SessionID] = 2;
        }
        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = 0;
                //Guardar los datos de los productos de factura especial
                //en catálogo de Cliente-Producto
                List<RemisionDet> ListaPrdRemEspecial = new List<RemisionDet>();

                //Datos del centro de distribución
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);

                for (int i = 0; i < this.ListaProductosRemisionEspecial.Count; i++)
                    ListaPrdRemEspecial.Add((RemisionDet)this.ListaProductosRemisionEspecial[i]);


                foreach (RemisionDet d in ListaProductosRemisionEspecial)
                {
                    if (Request.QueryString["Id_Tmov"].ToString() == "54")
                    {
                        CN_CatCliente cn_catcliente = new CN_CatCliente();
                        Clientes cliente = new Clientes();
                        cliente.Id_Emp = session.Id_Emp;
                        cliente.Id_Cd = session.Id_Cd_Ver;
                        cliente.Id_Cte = Convert.ToInt32(Request.QueryString["Id_Cte"]);
                        cn_catcliente.ConsultaClientes(ref cliente, session.Emp_Cnx);

                        if (d.Producto.Id_Prd.ToString() == d.Producto.Id_PrdEsp && cliente.Cte_RemisionElectronica == 1)
                        {
                            Alerta("El codigo Key y el codigo del cliente no pueden ser iguales");
                            return;
                        }

                        if (d.Producto.Id_Prd.ToString() == "" && cliente.Cte_RemisionElectronica == 1)
                        {
                            Alerta("El codigo Key es requerido");
                            return;
                        }

                        if (d.Producto.Id_PrdEsp == "" && cliente.Cte_RemisionElectronica == 1)
                        {
                            Alerta("El codigo del cliente es requerido");
                            return;
                        }

                        if (d.Clp_Release == "" && cliente.Cte_RemisionElectronica == 1)
                        {
                            Alerta("El release es requerido");
                            return;
                        }
                    }
                }

                if (Session["fTotal" + Session.SessionID] != null)
                {
                    if (txtTotal.Text.Length > 0)
                    {
                        decimal fTotalRemisionOriginal = decimal.Round(decimal.Parse(Session["fTotal" + Session.SessionID].ToString()), 2);

                        if (((fTotalRemisionOriginal + (decimal)cd.Cd_MargenDiferenciaDocs) < decimal.Parse(txtTotal.Text)) || ((fTotalRemisionOriginal - (decimal)cd.Cd_MargenDiferenciaDocs) > decimal.Parse(txtTotal.Text)))
                        {
                            Alerta("El monto Total de la remisión especial tiene una diferencia considerable con respecto a la remisión original.");
                            return;
                        }
                    }
                }
                new CN_CatClienteProd().ModificarClienteProdRemisionEspecial(ListaPrdRemEspecial, session.Emp_Cnx, ref verificador);

                //SET variable de encabezado de factura especial
                FacturaEspecial facturaEsp = new FacturaEspecial();
                facturaEsp.Id_Emp = session.Id_Emp;
                facturaEsp.Id_Cd = session.Id_Cd_Ver;
                facturaEsp.FacEsp_Importe = Convert.ToDouble(txtImporte.Text);
                facturaEsp.FacEsp_SubTotal = Convert.ToDouble(txtSubTotal.Text);
                facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(txtIVA.Text);
                facturaEsp.FacEsp_Total = Convert.ToDouble(txtTotal.Text);
                this.RemisionEspecial = facturaEsp;

                Session["RemEspecialGuardada" + Session.SessionID] = "1";

                string mensaje = "Los datos especiales se guardaron correctamente";
                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_RemisionEspecial('", mensaje, "')")); //cerrar ventana radWindow de factura especial
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected string ObtenerIdEspecial(object oc)
        {
            if (oc is RemisionDet) { return ((RemisionDet)oc).Producto.Id_PrdEsp; } else { return string.Empty; }
        }
        #region "Métodos para obtrener desde objetos los valores para los controles durante la inserción/actualización de un Grid editable"

        protected string ObtenerDescripcionEspecial(object oc)
        {
            if (oc is RemisionDet) { return ((RemisionDet)oc).Producto.Prd_DescripcionEspecial; } else { return string.Empty; }
        }
        protected string ObtenerDescripcion(object oc)
        {
            if (oc is RemisionDet) { return ((RemisionDet)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }
        protected string ObtenerPresentacion(object oc)
        {
            if (oc is RemisionDet) { return ((RemisionDet)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }
        protected string ObtenerUnidades(object oc)
        {
            if (oc is RemisionDet) { return ((RemisionDet)oc).Producto.Prd_UniNe; } else { return string.Empty; }
        }
        protected int ObtenerInvFinal(object oc)
        {
            if (oc is RemisionDet) { return ((RemisionDet)oc).Producto.Prd_InvFinal; } else { return 0; }
        }
        #endregion
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
                    prd.Prd_Descripcion = item.ItemArray[3].ToString();
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
        #region "Métodos para manejar la lista dinámica de Productos de la remision especial"

        protected void ListaProductosRemisionEspecial_AgregarProducto(RemisionDet remision_prod)
        {
            List<RemisionDet> lista = this.ListaProductosRemisionEspecial;
            //buscar producto de factura en la lista para ver si ya existe
            //for (int i = 0; i < lista.Count; i++)
            //{
            //    RemisionDet factura = lista[i];
            //    if (factura.Id_Prd == remision_prod.Id_Prd)//si el producto es el mismo
            //    {
            //        throw new Exception("rgRemisionEspecial_insert_repetida");
            //    }
            //}
            lista.Add(remision_prod);
            this.ListaProductosRemisionEspecial = lista;
            this.CalcularTotales();
        }

        protected void ListaProductosRemisionEspecial_ModificarProducto(RemisionDet remision_prod, int index)
        {
            List<RemisionDet> lista = this.ListaProductosRemisionEspecial;

            //buscar producto de factura en la lista           
            RemisionDet remision = lista[index];
            if (remision.Id_Prd == remision_prod.Id_Prd)
                lista[index] = remision_prod;

            this.ListaProductosRemisionEspecial = lista;
            this.CalcularTotales();
        }

        //protected void ListaProductosRemisionEspecial_EliminarProducto(int id_Prd, string descripcion)
        //{
        //    List<RemisionDet> lista = this.ListaProductosRemisionEspecial;

        //    //buscar producto de factura en la lista
        //    for (int i = 0; i < lista.Count; i++)
        //    {
        //        RemisionDet remision2 = lista[i];
        //        if (remision2.Id_Prd == id_Prd && remision2.Producto.Prd_Descripcion == descripcion)
        //        {
        //            lista.RemoveAt(i);
        //            break;
        //        }
        //    }
        //    this.ListaProductosRemisionEspecial = lista;
        //    this.CalcularTotales();
        //}
        protected void ListaProductosRemisionEspecial_EliminarProducto(int index)
        {
            try
            {
                List<RemisionDet> lista = this.ListaProductosRemisionEspecial;


                lista.RemoveAt(index);

                this.ListaProductosRemisionEspecial = lista;
                this.CalcularTotales();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
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
                    if (mensaje.Contains("rgRemisionEspecial_insert_repetida"))
                        Alerta("Este producto ya ha sido capturado");
                    else
                        if (mensaje.Contains("rgOrdCompra_delete_item_error"))
                            Alerta("Error al momento de eliminar el producto a la lista de productos de la remisión");
                        else
                            if (mensaje.Contains("CapRemisionEspecial_insert_error"))
                                Alerta("Error al momento de guardar los datos de remisión especial");
                            else
                                if (mensaje.Contains("rgRemisionEspecial_insert_error"))
                                    Alerta("Error al momento de agregar el producto a la lista");
                                else
                                    if (mensaje.Contains("rgRemisionEspecialDet_ItemDataBound"))
                                        Alerta("Error al momento de preparar un registro para edici&oacute;n");
                                    else
                                        if (mensaje.Contains("rgRemisionEspecialDet_NeedDataSource"))
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
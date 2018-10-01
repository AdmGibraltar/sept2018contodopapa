using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using Telerik.Web.UI;
using System.Collections;

namespace SIANWEB
{
    public partial class CapEntradaSalida : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value] = value; } }
        private DataTable dt
        {
            get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value]; }
            set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value] = value; }
        }
        private DataTable dt_original
        {
            get { return (DataTable)Session["dt_original" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value]; }
            set { Session["dt_original" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] + HF_ClvPag.Value] = value; }
        }
        public int ReqReferencia
        {
            get
            {
                return Convert.ToInt32(requiereReferencia);
            }
        }
        //static List<Producto> productos = new List<Producto>();
        private List<Producto> productos
        {
            set { Session["productosES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (List<Producto>)Session["productosES" + Session.SessionID + HF_ClvPag.Value]; }
        }
        private double subtotal
        {
            set { Session["subtotalES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { double? st = (double?)Session["subtotalES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? 0 : (double)st; }
        }
        private double total
        {
            set { Session["totalES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { double? st = (double?)Session["totalES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? 0 : (double)st; }
        }
        private double IVA
        {
            set { Session["IVAES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { double? st = (double?)Session["IVAES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? 0 : (double)st; }
        }
        private int id_detalle
        {
            set { Session["id_detalleES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { int? st = (int?)Session["id_detalleES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? 0 : (int)st; }
        }
        private bool requiereReferencia
        {
            set { Session["requiereReferenciaES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (bool)Session["requiereReferenciaES" + Session.SessionID + HF_ClvPag.Value]; }
        }
        private int Tm_ReqTDoc
        {
            set { Session["Tm_ReqTDocES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { int? st = (int?)Session["Tm_ReqTDocES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? 0 : (int)st; }
        }
        private bool referencia_valida
        {
            set { Session["referencia_validaES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (bool)Session["referencia_validaES" + Session.SessionID + HF_ClvPag.Value]; }
        }
        /// <summary>
        /// Grupo 1 : Movimientos 6, 15, 16 ||
        /// Grupo 2 : Movimientos 14 ||
        /// Grupo 3 : Movimientos 7, 11, 12, 13 ||
        /// Grupo 4 : Movimientos 2, 4 ||
        /// Grupo 0 : Cualquier otro movimiento 
        /// </summary>

        private int grupoMovimientosActivo
        {
            set { Session["grupoMovimientosActivoES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { int? st = (int?)Session["grupoMovimientosActivoES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? 0 : (int)st; }
        }
        /// <summary>
        /// tabla de productos por agrupador
        /// </summary>
        private DataTable dttemp1
        {
            set { Session["dttemp1ES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (DataTable)Session["dttemp1ES" + Session.SessionID + HF_ClvPag.Value]; }
        }
        private DataTable dttemp1_original
        {
            set { Session["dttemp1_originalES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (DataTable)Session["dttemp1_originalES" + Session.SessionID + HF_ClvPag.Value]; }
        }
        /// <summary>
        /// esta tabla no es exclusiva de remision, se usa (del mismo modo) tambien para facturas
        /// </summary>
        //static DataTable tablaRemision = new DataTable(); //lsita de productos (en la remision) por agrupador, con su total
        private DataTable tablaRemision
        {
            set { Session["tablaRemisionES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (DataTable)Session["tablaRemisionES" + Session.SessionID + HF_ClvPag.Value]; }
        }
        private bool actualizacionDocumento
        {
            set { Session["actualizacionDocumentoES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (bool)Session["actualizacionDocumentoES" + Session.SessionID + HF_ClvPag.Value]; }
        }


        //static int Id_EsDet_A = -1; //id de la partida que se va actualizar
        private int Id_EsDet_A
        {
            set { Session["Id_EsDet_AES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { int? st = (int?)Session["Id_EsDet_AES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? -1 : (int)st; }
        }
        //static int cantidad_A = -1; //cantidad de la partida que se va actualizar
        private int cantidad_A
        {
            set { Session["cantidad_AES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { int? st = (int?)Session["cantidad_AES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? -1 : (int)st; }
        }
        //static double costo_A = -1; //costo de la partida que se va actualizar
        private double costo_A
        {
            set { Session["costo_AES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { double? st = (double?)Session["costo_AES" + Session.SessionID + HF_ClvPag.Value]; return st == null ? -1 : (double)st; }
        }
        /// <summary>
        /// Tabla con los productos de compra local y sus precios a actualizar
        /// </summary>
        //static DataTable ProdPreLoc_Actualiza;
        private DataTable ProdPreLoc_Actualiza
        {
            set { Session["ProdPreLoc_ActualizaES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (DataTable)Session["ProdPreLoc_ActualizaES" + Session.SessionID + HF_ClvPag.Value]; }
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
                    CerrarVentana();
                    RadAjaxManager1.ResponseScripts.Add("RefreshParentPage()");
                }
                else
                {  //RadNumericTextBox2.Focus();
                    if (!Page.IsPostBack)
                    {

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();


                        productos = LlenarComboProductosLista();
                        CargarProveedor();
                        if (cmbNaturaleza.SelectedIndex == -1)
                        {
                            reiniciarVariables();
                            txtFolio.Text = "";
                        }
                        llenarNaturaleza();
                        dpFecha.SelectedDate = DateTime.Now;
                        crearDT();
                        Inicializar();
                        // Grupo 1 : Movimientos 6, 15, 16 ||
                        // Grupo 2 : Movimientos 14 ||
                        // Grupo 3 : Movimientos 7, 11, 12, 13 ||
                        // Grupo 4 : Movimientos 2, 4 ||
                        // Grupo 0 : Cualquier otro movimiento 

                        if (cmbTipoMovimento.SelectedIndex > 0)
                        {
                            switch (Convert.ToInt32(cmbTipoMovimento.SelectedValue))
                            {
                                case 6:
                                case 15:
                                case 16:
                                    grupoMovimientosActivo = 1;
                                    break;
                                case 14:
                                    grupoMovimientosActivo = 2;
                                    break;
                                case 7:
                                case 11:
                                case 12:
                                case 13:
                                    grupoMovimientosActivo = 3;
                                    break;
                                case 2:
                                case 4:
                                    grupoMovimientosActivo = 4;
                                    break;
                                default:
                                    grupoMovimientosActivo = 0;
                                    break;
                            }
                        }


                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");

                        if (!((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible)
                        {
                            //rgEntradaSalida.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                            GridCommandItem cmdItem = (GridCommandItem)rgEntradaSalida.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 

                            rgEntradaSalida.MasterTableView.Columns[rgEntradaSalida.MasterTableView.Columns.Count - 2].Display = false;
                            rgEntradaSalida.MasterTableView.Columns[rgEntradaSalida.MasterTableView.Columns.Count - 3].Display = false;
                            ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  
                            txtClienteNombre.Enabled = false;
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        //GRID
        protected void rgEntradaSalida_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgEntradaSalida.DataSource = dt;

            double ancho = 0;
            foreach (GridColumn gc in rgEntradaSalida.Columns)
            {
                if ((gc.Display && gc.Visible) || gc.UniqueName == "EditCommandColumn" || gc.UniqueName == "DeleteColumn")
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgEntradaSalida.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgEntradaSalida.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
        }
        protected void rgEntradaSalida_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox comboboxProductos = (editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox);
                if (
                        Convert.ToInt32(comboboxProductos.Value.HasValue ? comboboxProductos.Value.Value : -1) == -1 //producto                        
                        || (editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text == "" //cantidad
                        || (editedItem["Costo"].FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text == "" //costo
                        || (rgEntradaSalida.MasterTableView.Columns.FindByUniqueName("territorio").Visible == true && (editedItem["territorio"].FindControl("RadComboBox1") as RadComboBox).SelectedValue == "-1") //territorio
                    )
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                float precio = 0;
                int Id_Pre = 0;
                new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precio, session, Convert.ToInt32(comboboxProductos.Value.HasValue ? comboboxProductos.Value.Value : -1), ref Id_Pre);

                int Id_Prd = Convert.ToInt32(comboboxProductos.Value.HasValue ? comboboxProductos.Value.Value : -1);
                string descripcion = (editedItem.FindControl("DescripcionTextBox") as RadTextBox).Text;
                string presentacion = (editedItem.FindControl("PresenTextBox") as RadTextBox).Text;
                int cantidad = Convert.ToInt32((editedItem.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text);
                float costo = float.Parse((editedItem.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text);
                float importe = cantidad * costo;

                Producto producto = new Producto();
                new CN_CatProducto().ConsultaProducto(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Id_Prd);

                int Prd_AgrupadoSpo = producto.Prd_AgrupadoSpo; //int.Parse((comboboxProductos.SelectedItem.FindControl("LiPrd_AgrupadoSpo").Controls[1] as Label).Text);

                if (cantidad == 0)
                {
                    Alerta("No puede ingresar una partida con cantidad 0");
                    e.Canceled = true;
                }
                bool afecta = bool.Parse((editedItem["afecta"].Controls[0] as CheckBox).Checked.ToString()); //En el caso de los 2 y 4

                switch (grupoMovimientosActivo)
                {
                    case 1:
                    case 2:
                    case 3:
                        if (grupoMovimientosActivo == 1 || grupoMovimientosActivo == 2)
                        {

                            if (grupoMovimientosActivo == 1) //esto es solo para el grupo 1
                            {  //validar que el producto sea por sistema de propietarios --grupo 1
                                if (!((bool)producto.Prd_AparatoSisProp))
                                {
                                    Alerta("El producto no es un sistema de propietarios");
                                    e.Canceled = true;
                                    return;
                                }
                            }
                        }

                        int territorio = int.Parse((editedItem["territorio"].FindControl("RadComboBox1") as RadComboBox).SelectedValue);
                        bool buenEstado = (editedItem["buenEstado"].Controls[0] as CheckBox).Checked;

                        if (e.Canceled != true)
                        {
                            double iva_cd = 0;
                            new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                            dt.Rows.Add(new object[] { ++id_detalle, Id_Prd, descripcion, presentacion, cantidad, costo, importe, null, territorio, buenEstado, Prd_AgrupadoSpo });
                            subtotal += cantidad * costo;
                            IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                            total = subtotal + IVA;
                            RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                            RadNumericTextBoxIVA.Text = IVA.ToString();
                            RadNumericTextBoxTotal.Text = total.ToString();

                            /*agregando productos a la lista (dttemp1)*/
                            DataRow[] editable_dr;
                            if (dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                            {
                                editable_dr = dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'");
                                editable_dr[0].BeginEdit();
                                editable_dr[0]["NumeroElementos"] = int.Parse(editable_dr[0]["NumeroElementos"].ToString()) + cantidad;
                                editable_dr[0].AcceptChanges();
                            }
                            else
                                dttemp1.Rows.Add(new object[] { Prd_AgrupadoSpo, territorio, cantidad });
                        }
                        break;
                    case 4:

                        //si es compra local el precio no puede ser 0
                        if ( /*producto.Id_Cd == 0 && */ producto.Prd_Colo == true && costo == 0)
                        {
                            Alerta("Es importante tener actualizado los costos en productos de " +
                                "compras locales; favor de entrar a Inventarios - Catálogo - Productos " +
                                ", para capturar el precio vigente AAA");
                            //no dejar continuar
                            e.Canceled = true;
                            return;
                        }
                        if (e.Canceled != true)
                        {
                            double iva_cd = 0;
                            new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                            DataRow[] dRow = dt.Select("Id_Prd=" + Id_Prd.ToString());
                            if (dRow.Length == 0)
                            {
                                dt.Rows.Add(new object[] { ++id_detalle, Id_Prd, descripcion, presentacion, cantidad, costo, importe, afecta, null, null, Prd_AgrupadoSpo });
                                subtotal += cantidad * costo;
                                IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                                total = subtotal + IVA;
                                RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                                RadNumericTextBoxIVA.Text = IVA.ToString();
                                RadNumericTextBoxTotal.Text = total.ToString();
                            }
                            else
                                Alerta("No es permitido ingresar el mismo producto");
                        }
                        if (e.Canceled != true && producto.Prd_Colo)
                        {
                            ProdPreLoc_Actualiza.Rows.Add(new object[] { id_detalle, session.Id_Emp, session.Id_Cd_Ver
                                , Id_Prd, Id_Pre, true, DateTime.Now, DateTime.Now, "AAA", costo});
                        }

                        break;
                    case 0:
                        if (e.Canceled != true)
                        {
                            double iva_cd = 0;
                            new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                            DataRow[] dRow = dt.Select("Id_Prd=" + Id_Prd.ToString());
                            if (dRow.Length == 0)
                            {
                                dt.Rows.Add(new object[] { ++id_detalle, Id_Prd, descripcion, presentacion, cantidad, costo, importe, afecta, null, null, Prd_AgrupadoSpo });
                                subtotal += cantidad * costo;
                                IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                                total = subtotal + IVA;
                                RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                                RadNumericTextBoxIVA.Text = IVA.ToString();
                                RadNumericTextBoxTotal.Text = total.ToString();
                            }
                            else
                                Alerta("No es permitido ingresar el mismo producto");
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void rgEntradaSalida_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox comboboxProductos = (editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox);
                float precio = obtenerPrecioAAA(Convert.ToInt32(comboboxProductos.Value.HasValue ? comboboxProductos.Value.Value : -1));

                string Id_EsDet = (rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Id_EsDet"].Controls[0] as TextBox).Text;
                int Id_Prd = Convert.ToInt32(comboboxProductos.Value.HasValue ? comboboxProductos.Value.Value : -1);
                string descripcion = (editedItem.FindControl("DescripcionTextBox") as RadTextBox).Text;
                string presentacion = (editedItem.FindControl("PresenTextBox") as RadTextBox).Text;
                int cantidad = Convert.ToInt32((editedItem.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text);
                float costo = float.Parse((editedItem.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text);
                bool buenestado = (e.Item.Cells[10].Controls[0] as CheckBox).Checked;

                Producto producto = new Producto();
                new CN_CatProducto().ConsultaProducto(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, session.Id_Cd_Ver, Id_Prd);
                float importe = cantidad * costo;
                int Prd_AgrupadoSpo = producto.Prd_AgrupadoSpo;
                double iva_cd = 0;
                new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                DataRow[] editable_dr;
                if (cantidad == 0)
                {
                    Alerta("No puede ingresar una partida con cantidad 0");
                    e.Canceled = true;
                }
                bool afecta = bool.Parse((editedItem["afecta"].Controls[0] as CheckBox).Checked.ToString()); //En el caso de los 2 y 4

                switch (grupoMovimientosActivo)
                {
                    case 1:
                    case 2:
                    case 3:
                        if (grupoMovimientosActivo == 1 || grupoMovimientosActivo == 2)
                        {
                            if (grupoMovimientosActivo == 1) //esto es solo para el grupo 1
                            {//validar que el producto sea por sistema de propietarios --grupo 1
                                if (!((bool)producto.Prd_AparatoSisProp))
                                {
                                    Alerta("El producto no es un sistema de propietarios");
                                    e.Canceled = true;
                                }
                            }
                        }

                        int territorio = int.Parse((editedItem["territorio"].FindControl("RadComboBox1") as RadComboBox).SelectedValue);
                        bool buenEstado = (editedItem["buenEstado"].Controls[0] as CheckBox).Checked;

                        //int cantidadenRemision = 0;
                        //if (tablaRemision.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                        //{
                        //    cantidadenRemision = int.Parse(tablaRemision.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'")[0]["Cantidad"].ToString());
                        //}
                        //int cantidadEnDt = 0;
                        //if (dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                        //{
                        //    cantidadEnDt = int.Parse(dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'")[0]["NumeroElementos"].ToString());
                        //}

                        //int cantidadEnDttemp_original = 0;
                        ////si es actualizacion del documento, contar lo que ya se tenìa
                        //if (actualizacionDocumento)
                        //{
                        //    if (dttemp1_original.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                        //    {
                        //        cantidadEnDttemp_original = int.Parse(dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'")[0]["NumeroElementos"].ToString());
                        //    }
                        //}

                        ///*validar qe el articulo y territorio seleccionado correspondan igual ke en la remision*/
                        ////--grupo 1 ,2 y 3
                        //if (tablaRemision.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length == 0)
                        //{
                        //    Alerta("El territorio para este artículo no corresponde con el del documento");
                        //    e.Canceled = true;
                        //}

                        ////c) Verifica la cantidad que no sea mayor a la remisión por agrupacion --grupo 1 ,2 

                        ////if ((actualizacionDocumento) && ((cantidadEnDt + cantidad) - cantidadEnDttemp_original > cantidadenRemision))
                        ////{
                        ////    Alerta("Los artículos sobrepasan lo que se tiene en el documento");
                        ////    e.Canceled = true;
                        ////}

                        //int cantanterior = 0;
                        DataRow[] dr1 = dt.Select("Id_EsDet='" + Id_EsDet + "'");
                        //if (dr1.Length > 0)
                        //{
                        //    cantanterior = Convert.ToInt32(dr1[0]["Cantidad"]);
                        //}

                        //if (cantidadEnDt + cantidad - cantanterior > cantidadenRemision)
                        //// if ((actualizacionDocumento == false) && ((cantidadEnDt + cantidad) > cantidadenRemision))
                        //{
                        //    Alerta("Los artículos sobrepasan lo que se tiene en el documento");
                        //    e.Canceled = true;
                        //}
                        //else
                        //{
                        if (e.Canceled != true)
                        {
                            if (dr1.Length > 0)
                            {

                                dr1[0].BeginEdit();

                                dr1[0]["Cantidad"] = cantidad;
                                dr1[0]["Costo"] = costo;
                                dr1[0]["importe"] = importe;
                                dr1[0]["buenEstado"] = buenEstado;
                                dr1[0].AcceptChanges();
                            }
                            subtotal += cantidad * costo;
                            IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                            total = subtotal + IVA;
                            RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                            RadNumericTextBoxIVA.Text = IVA.ToString();
                            RadNumericTextBoxTotal.Text = total.ToString();
                            /*agregando productos a la lista (dttemp1)*/
                            editable_dr = null;
                            if (dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                            {
                                editable_dr = dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'");
                                editable_dr[0].BeginEdit();
                                editable_dr[0]["NumeroElementos"] = cantidad; //int.Parse(editable_dr[0]["NumeroElementos"].ToString()) + cantidad  ;
                                editable_dr[0].AcceptChanges();
                            }
                            else
                            {
                                dttemp1.Rows.Add(new object[] { Prd_AgrupadoSpo, territorio, cantidad });
                            }
                        }
                        break;
                    case 4:
                        //si es compra local el precio no puede ser 0
                        if ( /*producto.Id_Cd == 0 &&*/ producto.Prd_Colo == true)
                        {
                            if (precio == 0)
                            {
                                Alerta("Es importante tener actualizado los costos en productos de " +
                                    "compras locales; favor de entrar a Inventarios - Catálogo - Productos " +
                                    ", para capturar el precio vigente AAA");
                                e.Canceled = true;
                            }
                        }
                        if (e.Canceled != true && producto.Prd_Colo)
                        {

                            DataRow[] prodPre;
                            prodPre = ProdPreLoc_Actualiza.Select("Id_EsDet=" + Id_EsDet + " and Id_Prd=" + Id_Prd);
                            prodPre[0].BeginEdit();
                            prodPre[0]["Prd_Pesos"] = costo;
                            prodPre[0].AcceptChanges();
                        }

                        if (e.Canceled != true)
                        {
                            subtotal -= costo_A * cantidad_A; //se borran las cantidades anteriores del subtotal
                            editable_dr = null;
                            editable_dr = dt.Select("Id_EsDet=" + Id_EsDet_A);
                            editable_dr[0].BeginEdit();
                            editable_dr[0]["afecta"] = afecta;
                            editable_dr[0]["cantidad"] = cantidad;
                            editable_dr[0]["costo"] = costo;
                            editable_dr[0]["importe"] = cantidad * costo;
                            editable_dr[0].AcceptChanges();

                            subtotal += cantidad * costo;
                            IVA = double.Parse((subtotal * (iva_cd / 100)).ToString());
                            total = subtotal + IVA;
                            RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                            RadNumericTextBoxIVA.Text = IVA.ToString();
                            RadNumericTextBoxTotal.Text = total.ToString();
                        }
                        break;
                    case 0:

                        subtotal -= costo_A * cantidad_A; //se borran las cantidades anteriores del subtotal
                        editable_dr = null;
                        editable_dr = dt.Select("Id_EsDet=" + Id_EsDet_A);
                        editable_dr[0].BeginEdit();
                        editable_dr[0]["costo"] = costo;
                        editable_dr[0]["cantidad"] = cantidad;
                        editable_dr[0]["importe"] = cantidad * costo;
                        editable_dr[0].AcceptChanges();
                        subtotal += cantidad * costo;
                        IVA = double.Parse((subtotal * (iva_cd / 100)).ToString());
                        total = subtotal + IVA;
                        RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                        RadNumericTextBoxIVA.Text = IVA.ToString();
                        RadNumericTextBoxTotal.Text = total.ToString();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void rgEntradaSalida_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                double iva_cd = 0;
                new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                bool eventocancelado = false;
                int Id_Es = Convert.ToInt32(txtFolio.Text);
                int Es_Naturaleza = !string.IsNullOrEmpty(this.cmbNaturaleza.SelectedValue) ? Convert.ToInt32(this.cmbNaturaleza.SelectedValue) : 0;
                string Id_EsDet = rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Id_EsDet"].Text;
                int cantidad = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["cantidad"].FindControl("CantidadLabel") as Label).Text);
                int Id_Prd = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                double costo = double.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["costo"].FindControl("CostoLabel") as Label).Text);
                int verificador = 0;
                DataRow[] roww;
                switch (grupoMovimientosActivo)
                {
                    case 1:
                    case 2:
                    case 3:
                        if (eventocancelado != true)
                        {
                            roww = dt.Select("Id_EsDet=" + Id_EsDet);
                            if (roww.Length != 1)
                            {
                                throw new Exception(" ");
                            }
                            dt.Rows.Remove(roww[0]);
                            subtotal -= cantidad * costo;
                            IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                            total = subtotal + IVA;
                            RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                            RadNumericTextBoxIVA.Text = IVA.ToString();
                            RadNumericTextBoxTotal.Text = total.ToString();

                            ///*QUITAR productos a la lista (dttemp1)*/
                            DataRow[] editable_dr;
                            string Prd_AgrupadoSpo = rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Prd_AgrupadoSpo"].Text;
                            string territorio = (rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["territorio"].FindControl("Label1") as Label).Text;

                            if (dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                            {
                                editable_dr = dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'");
                                editable_dr[0].BeginEdit();
                                editable_dr[0]["NumeroElementos"] = int.Parse(editable_dr[0]["NumeroElementos"].ToString()) - cantidad;
                                editable_dr[0].AcceptChanges();
                            }
                            else
                            {
                                throw new Exception(" ");
                            }
                        }
                        break;
                    case 4:
                        DataRow[] rcom = ProdPreLoc_Actualiza.Select("Id_EsDet=" + Id_EsDet + " and Id_Prd=" + Id_Prd);
                        if (rcom.Length > 0)
                            ProdPreLoc_Actualiza.Rows.Remove(rcom[0]);
                        roww = dt.Select("Id_EsDet=" + Id_EsDet);
                        if (roww.Length != 1)
                        {
                            throw new Exception(" ");
                        }
                        if (actualizacionDocumento && Es_Naturaleza == 0)
                        {
                            CN_CatProducto cn_pro = new CN_CatProducto();
                            try
                            {
                                cn_pro.ConsultaProductoInventario(ref verificador, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Id_Es, Es_Naturaleza, Convert.ToInt32(Id_EsDet));
                            }
                            catch (Exception ex)
                            {
                                Alerta(ex.Message);
                                return;
                            }
                        }
                        dt.Rows.Remove(roww[0]);
                        subtotal -= cantidad * costo;
                        IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                        total = subtotal + IVA;
                        RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                        RadNumericTextBoxIVA.Text = IVA.ToString();
                        RadNumericTextBoxTotal.Text = total.ToString();
                        break;
                    case 0:
                        roww = dt.Select("Id_EsDet=" + Id_EsDet);
                        if (roww.Length != 1)
                        {
                            throw new Exception(" ");
                        }
                        if (actualizacionDocumento && Es_Naturaleza == 0)
                        {
                            CN_CatProducto cn_pro = new CN_CatProducto();
                            try
                            {
                                cn_pro.ConsultaProductoInventario(ref verificador, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Id_Es, Es_Naturaleza, Convert.ToInt32(Id_EsDet));
                            }
                            catch (Exception ex)
                            {
                                Alerta(ex.Message);
                                return;
                            }
                        }
                        dt.Rows.Remove(roww[0]);
                        subtotal -= cantidad * costo;
                        IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                        total = subtotal + IVA;
                        RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                        RadNumericTextBoxIVA.Text = IVA.ToString();
                        RadNumericTextBoxTotal.Text = total.ToString();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void rgEntradaSalida_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value] = 0;
            if (e.CommandName == "InitInsert")
            {
                Session["estatus" + Session.SessionID + HF_ClvPag.Value] = "2";
                switch (grupoMovimientosActivo)
                {
                    case 3:
                        /*traer lista(remision) de spCapRemisionDet_ConsultarTotalProducto (agrupacion) para verificar entradas*/
                        if (id_detalle == 0)
                        {
                            Sesion Sesion = new Sesion();
                            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                            DataTable tablaRemision2 = new DataTable();
                            new CN_CatRemision().ConsultarTotalProductoDocumento(int.Parse(txtReferencia2.Text), 1, Sesion, ref tablaRemision2);
                            tablaRemision = tablaRemision2;
                            crear_dttemp1();
                        }
                        break;
                    case 1:

                    case 2:
                        ///*traer lista(facturas) de spCapRemisionDet_ConsultarTotalProducto (agrupacion) para verificar entradas*/
                        //if (id_detalle == 0)
                        //{
                        //    Sesion Sesion = new Sesion();
                        //    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        //    tablaRemision = new DataTable();
                        //    new CN_CatRemision().ConsultarTotalProductoDocumento(int.Parse(txtReferencia2.Text), 2, Sesion, ref tablaRemision);
                        //    crear_dttemp1();
                        //}
                        break;
                    default:
                        break;
                }
            }
            else if (e.CommandName == "Edit")
            {
                Id_EsDet_A = int.Parse(rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Id_EsDet"].Text);
                cantidad_A = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["cantidad"].FindControl("CantidadLabel") as Label).Text);
                costo_A = double.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["costo"].FindControl("CostoLabel") as Label).Text);
                Session["estatus" + Session.SessionID + HF_ClvPag.Value] = "1"; //1=Edit
                Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value] = cantidad_A;
            }
            else
            {
                Session["estatus" + Session.SessionID + HF_ClvPag.Value] = "3";
            }
        }
        protected void rgEntradaSalida_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;
                    RadNumericTextBox cmbProductosLista = editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox;
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox1")).ClientID.ToString();
                    string jsControles = string.Concat(
                            "txtProductoClientID='", txtId_Prd, "';"
                            );

                    RadComboBox cmb = (editItem.FindControl("RadComboBox1") as RadComboBox);
                    if (requiereReferencia && txtReferencia2.Text != "")
                        cargarTerritorioDetalles(ref cmb);

                    Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        cmbProductosLista.Enabled = true;
                        (editItem["importe"].Controls[0] as TextBox).Visible = false;
                    }

                    Control updatebtn = (Control)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        if (grupoMovimientosActivo == 1 || grupoMovimientosActivo == 3 || grupoMovimientosActivo == 2)
                            cmbProductosLista.Enabled = false;
                        (editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();
                        cmbProductosLista.Enabled = false;
                        (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = false;//txtbox id del producto
                        (editItem.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Cantidad"].ToString();
                        (editItem.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Costo"].ToString();
                        editItem["importe"].Controls[1].Visible = false;
                    }
                    if (grupoMovimientosActivo == 1 || grupoMovimientosActivo == 2)
                    {
                        Sesion Sesion = new Sesion();
                        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                        CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1), Sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmb);
                        if (cmb.Items.Count > 1)
                        {
                            cmb.SelectedIndex = 1;
                            cmb.Text = cmb.Items[1].Text;
                            cmb.Text = cmb.Items[1].Value;
                        }
                    }
                }
                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_Prd"].FindControl("RadNumericTextBox1");
                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form["Cantidad"].FindControl("RadNumericTextBoxCantidad");
                    }
                    dataField.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private bool ValidarDisponible(int producto, int cantidad)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapEntradaSalida cn_capEntradaSalida = new CN_CapEntradaSalida();

                EntradaSalida entsal = new EntradaSalida();
                entsal.Id_Emp = session.Id_Emp;
                entsal.Id_Cd = session.Id_Cd_Ver;

                string verificador = "";
                cn_capEntradaSalida.ConsultarDisponible(entsal, session.Emp_Cnx, producto, cantidad, ref verificador);

                if (verificador == "")
                    return false;
                else
                {
                    Alerta(verificador);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void txtClienteId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dt = null;
                crearDT();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cte = new Clientes();
                CN_CatCliente catcliente = new CN_CatCliente();
                cte.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                cte.Id_Emp = Sesion.Id_Emp;
                cte.Id_Cd = Sesion.Id_Cd_Ver;
                try
                {
                    catcliente.ConsultaClientes(ref cte, Sesion.Emp_Cnx);
                    txtClienteNombre.Text = cte.Cte_NomComercial;
                    txtReferencia.Focus();
                }
                catch (Exception ex)
                {
                    txtClienteNombre.Text = "";
                    txtClienteId.Text = "";
                    AlertaFocus(ex.Message, txtClienteId.ClientID);
                    return;
                }

                if (requiereReferencia)
                    validarCliente_Territorio();
                if (txtReferencia.Visible)
                {
                    txtReferencia.Focus();
                }
                else
                {
                    txtReferencia2.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Costo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadNumericTextBox rdBox = (sender as RadNumericTextBox);
                GridDataItem j = rdBox.Parent.Parent as GridDataItem;

                int id_Prd = (int)(j.FindControl("RadNumericTextBox1") as RadNumericTextBox).Value.Value;
                RadNumericTextBox txt_Precio = (j.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox);
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = new Producto();
                CN_CatProducto cn_pro = new CN_CatProducto();
                try
                {
                    cn_pro.ConsultaProducto(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, session.Id_Cd_Ver, id_Prd);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, rdBox.ClientID);
                    return;
                }

                double precio = txt_Precio.Value.HasValue ? txt_Precio.Value.Value : 0;

                if (producto.Prd_Colo && precio == 0)
                {
                    AlertaFocus("Es importante tener actualizados los costos en los productos de compras locales; favor de entrar a Inventarios>Catalogo>Productos para capturar correctamente el precio vigente AAA", txt_Precio.ClientID);
                    txt_Precio.Text = "";
                    return;
                }
                else if (precio == 0)
                {
                    AlertaFocus("El costo no puede ser cero", txt_Precio.ClientID);
                    txt_Precio.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox rdBox = (sender as RadNumericTextBox);
                GridDataItem j = rdBox.Parent.Parent as GridDataItem;
                //(j.FindControl("DescripcionTextBox") as TextBox).Text = (sender as RadComboBox).Text;



                if (Convert.ToInt32(rdBox.Value.HasValue ? rdBox.Value.Value : -1) != -1)
                {
                    Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                    Producto producto = new Producto();
                    //RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);

                    CN_CatProducto cn_pro = new CN_CatProducto();
                    try
                    {
                        cn_pro.ConsultaProducto(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, session.Id_Cd_Ver, Convert.ToInt32(rdBox.Value.HasValue ? rdBox.Value.Value : -1));
                    }
                    catch (Exception ex)
                    {
                        AlertaFocus(ex.Message, rdBox.ClientID);
                        rdBox.Text = "";
                        return;
                    }


                    if (cmbTipoMovimento.SelectedValue == "15" || cmbTipoMovimento.SelectedValue == "16" || cmbTipoMovimento.SelectedValue == "6")
                    {
                        if (producto.Prd_Nuevo)
                        {
                            rdBox.Text = "";
                            AlertaFocus("El codigo de producto debe ser codigo usado", rdBox.ClientID);

                            return;
                        }
                    }

                    //if (txtProveedorId.Enabled && cmbNaturaleza.SelectedValue != "1")
                    //{
                    //    if (producto.Id_Pvd != txtProveedorId.Value.Value)
                    //    {
                    //        AlertaFocus("El producto no pertenece al proveedor", rdBox.ClientID);
                    //        return;
                    //    }
                    //}





                    float precio = obtenerPrecioAAA(Convert.ToInt32(rdBox.Value.HasValue ? rdBox.Value.Value : -1));




                    if (grupoMovimientosActivo == 4 /*&& producto.Id_Cd == 0*/ && producto.Prd_Colo == true)
                    {
                        if (precio == 0)
                        {
                            Alerta("Es importante tener actualizado los costos en productos de " +
                                "compras locales; favor de entrar a Inventarios - Catálogo - Productos " +
                                ", para capturar el precio vigente AAA");
                            //no dejar continuar
                            (j.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = "";
                            (j.FindControl("DescripcionTextBox") as RadTextBox).Text = "";
                            (j.FindControl("PresenTextBox") as RadTextBox).Text = "";
                            (j.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text = "";
                            (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";
                        }
                    }
                    else
                    {
                        if (precio == 0)
                            Alerta("No se ha definido el precio para este producto");
                    }



                    if (grupoMovimientosActivo == 1)
                    {
                        //string Id_Ter = (j.FindControl("RadComboBox1") as RadComboBox).SelectedValue;
                        //// a)validar que el producto sea usado
                        ////if (producto.Id_Ptp == 1 && producto.Prd_Nuevo)
                        ////    Alerta("El producto seleccionado tiene código inválido (no es un producto usado)");

                        ////validar que el producto exista en tablaremision (CapRemisiones)                        
                        //int agrupadoABuscar = producto.Prd_AgrupadoSpo;//int.Parse(((sender as RadComboBox).SelectedItem.FindControl("LiPrd_AgrupadoSpo").Controls[1] as Label).Text);


                        //if (tablaRemision.Select("Prd_AgrupadoSpo='" + agrupadoABuscar + "' and Id_Ter='" + Id_Ter + "'").Length == 0)
                        //{ //la canidad en tablaremision < 1
                        //    AlertaFocus("El agrupador del producto no pertenece al documento de referencia", (j.FindControl("RadNumericTextBox1") as RadNumericTextBox).ClientID); (j.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = "";
                        //    (j.FindControl("DescripcionTextBox") as RadTextBox).Text = "";
                        //    (j.FindControl("PresenTextBox") as RadTextBox).Text = "";
                        //    (j.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text = "";
                        //    (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";
                        //    return;
                        //}

                        if (grupoMovimientosActivo == 1)
                        {
                            ////validar que el producto sea por sistema de propietarios
                            if (!((bool)producto.Prd_AparatoSisProp))
                            {
                                rdBox.Text = "";
                                AlertaFocus("El producto no es un sistema de propietarios", rdBox.ClientID);
                                (j.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = "";
                                (j.FindControl("DescripcionTextBox") as RadTextBox).Text = "";
                                (j.FindControl("PresenTextBox") as RadTextBox).Text = "";
                                (j.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text = "";
                                (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";
                                return;
                            }
                        }
                    }
                    if (grupoMovimientosActivo == 2)
                    {

                    }
                    if (grupoMovimientosActivo == 3)
                    {
                        ////validar que el producto exista en tablaremision (CapRemisiones)                        
                        ////int agrupadoABuscar = producto.Prd_AgrupadoSpo;//int.Parse(((sender as RadComboBox).SelectedItem.FindControl("LiPrd_AgrupadoSpo").Controls[1] as Label).Text);

                        //string Id_Ter = (j.FindControl("RadComboBox1") as RadComboBox).SelectedValue;
                        //if (tablaRemision.Select("Prd_AgrupadoSpo='" + agrupadoABuscar + "' and Id_Ter='" + Id_Ter + "'").Length == 0)
                        //{ //la canidad en tablaremision < 1
                        //    AlertaFocus("El agrupador del producto no pertenece al documento de referencia", (j.FindControl("RadNumericTextBox1") as RadNumericTextBox).ClientID);

                        //    return;
                        //}
                        string Id_Ter = (j.FindControl("RadComboBox1") as RadComboBox).SelectedValue;
                        CN_CapRemision cnremision = new CN_CapRemision();
                        Remision remision = new Remision();
                        remision.Id_Emp = session.Id_Emp;
                        remision.Id_Cd = session.Id_Cd_Ver;
                        remision.Id_Rem = txtReferencia.Visible ? Convert.ToInt32(txtReferencia.Text) : Convert.ToInt32(txtReferencia2.Text);
                        List<RemisionDet> list = new List<RemisionDet>();
                        cnremision.ConsultarRemisionesDetalle(session, remision, ref list);

                        int encontrados = 0;
                        foreach (RemisionDet rd in list)
                        {
                            if (producto.Id_Prd == rd.Id_Prd && Id_Ter == rd.Id_Ter.ToString())
                            {
                                encontrados += 1;
                            }
                        }
                        if (encontrados == 0)
                        {
                            AlertaFocus("El producto no pertenece al documento de referencia", (j.FindControl("RadNumericTextBox1") as RadNumericTextBox).ClientID);
                            (j.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = "";
                            (j.FindControl("DescripcionTextBox") as RadTextBox).Text = "";
                            (j.FindControl("PresenTextBox") as RadTextBox).Text = "";
                            (j.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text = "";
                            (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";
                            return;
                        }


                    }

                    //if (grupoMovimientosActivo == 4)
                    //{  // a)validar que el producto sea NUEVO
                    //    if (!producto.Prd_Nuevo)
                    //        Alerta("El producto seleccionado tiene código inválido (no es un producto nuevo)");
                    //    //si el producto es COMPRA LOCAL y e el costo es 0, despliega mensaje y no deja 
                    //    //continuar : "Es importante...          <--- agregado lineas arriba
                    //}
                    //busca precio

                    (j.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text = precio.ToString();
                    (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";


                    (j.FindControl("PresenTextBox") as RadTextBox).Text = producto.Prd_Presentacion;
                    (j.FindControl("DescripcionTextBox") as RadTextBox).Text = producto.Prd_Descripcion;
                    //si el producto es COMPRA LOCAL y e el costo es 0, despliega mensaje y no deja 
                    //continuar : "Es importante...

                    ((sender as RadNumericTextBox).Parent.Parent.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Focus();

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
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "new":
                        nuevo();
                        break;
                    case "save":
                        Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void dpFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {

                dt = null;
                crearDT();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                if ((dpFecha.SelectedDate >= session.CalendarioIni) && (dpFecha.SelectedDate <= session.CalendarioFin))
                {
                }
                else
                {
                    Alerta("Fecha se encuentra fuera del periodo");
                    dpFecha.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbNaturaleza_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbNaturaleza_indiceCambiado();
            dpFecha.Focus();
        }
        protected void cmbTipoMovimento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbTipoMovimento_indiceCambiado();
            if (!requiereReferencia)
            {
                RequiredFieldValidator7.ValidationGroup = "nn";
                RequiredFieldValidator8.ValidationGroup = "nn";
            }
            else
            {
                RequiredFieldValidator7.ValidationGroup = "pestaniaDetalles";
                RequiredFieldValidator8.ValidationGroup = "pestaniaDetalles";
            }


            if (txtClienteId.Enabled)
            {
                txtClienteId.Focus();
            }
            else
            {
                txtProveedorId.Focus();
            }
        }
        protected void txtReferencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dt = null;
                crearDT();
                txtNotas.Focus();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtReferencia2_TextChanged(object sender, EventArgs e)
        {
            try
            {

                RadComboBoxTerritorio.Items.Clear();
                RadComboBoxTerritorio.Text = "";
                dt = null;
                crearDT();
                if (txtReferencia2.Text != "")
                { //comprobar referencia
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_CatRemision cn_rem = new CN_CatRemision();
                    int verificador = 0;
                    switch (grupoMovimientosActivo)
                    {
                        case 1:
                        case 3:

                            HF_TipoDoc.Value = "1";//el 1 es remision
                            break;
                        case 2:

                            HF_TipoDoc.Value = "2";//el 2 es factura 
                            //throw new Exception("Error");
                            break;
                        case 4:
                            HF_TipoDoc.Value = "2";//el 2 es factura
                            break;
                        case 0:
                            throw new Exception("Error");
                        //break;
                        default:
                            throw new Exception("Error");
                        //break;
                    }

                    cn_rem.ConsultarReferencia(Sesion, int.Parse(txtReferencia2.Text), ref verificador, Convert.ToInt32(HF_TipoDoc.Value));

                    if (verificador == -1)
                    {
                        Alerta("El número de referencia no existe");
                        referencia_valida = false;
                    }
                    else if (verificador == 0)
                    {
                        Alerta("El número de referencia está en estatus no valido");
                    }
                    else
                    {
                        cargarTerritorio();
                        //llenar combo con la referencia
                        referencia_valida = true;
                        if (grupoMovimientosActivo == 1 || grupoMovimientosActivo == 3)
                        {
                            int partidasConSaldo = 0;
                            new CN_CapRemision().ConsultarPartidasConSaldo(Sesion, int.Parse(txtReferencia2.Text), ref partidasConSaldo);
                            if (partidasConSaldo == 0)
                                Alerta("Remisión con saldo insuficiente");
                        }
                    }
                }
                if (requiereReferencia)
                    validarCliente_Territorio();
                txtNotas.Focus();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Cantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox rdBox = (sender as RadNumericTextBox);
                GridDataItem j = rdBox.Parent.Parent as GridDataItem;
                int id_prd = (int)(j.FindControl("RadNumericTextBox1") as RadNumericTextBox).Value.Value;


                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = new Producto();
                //RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);

                CN_CatProducto cn_pro = new CN_CatProducto();
                try
                {
                    cn_pro.ConsultaProducto(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, session.Id_Cd_Ver, id_prd);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, rdBox.ClientID);
                    return;
                }
                if (cmbNaturaleza.SelectedValue == "1")
                {
                    int cantidadB = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Id_Prd"].ToString() == id_prd.ToString())
                        {
                            cantidadB = cantidadB + Convert.ToInt32(dr["Cantidad"]);
                        }
                    }
                    if (Session["estatus" + Session.SessionID + HF_ClvPag.Value].ToString() == "1")
                    {
                        cantidadB = cantidadB - Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
                    }

                    CN_CapRemision rem = new CN_CapRemision();
                    int cantidadES2 = 0;
                    if (actualizacionDocumento)
                    {
                        rem.ConsultarRemisionesCantidadRemCantidad(session.Id_Emp, session.Id_Cd_Ver, txtFolio.Text, id_prd, cmbNaturaleza.SelectedValue, ref cantidadES2, session.Emp_Cnx);
                    }


                    if (producto.Prd_InvFinal - producto.Prd_Asignado + cantidadES2 < rdBox.Value + cantidadB)
                    {
                        AlertaFocus("No hay producto suficiente", rdBox.ClientID);
                        rdBox.Text = "";
                    }


                }
                else if (grupoMovimientosActivo == 0)
                {
                    int edicion = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
                    if (producto.Prd_InvFinal - producto.Prd_Asignado - (edicion - rdBox.Value) < 0)
                    {
                        AlertaFocus("Producto " + producto.Id_Prd.ToString() + " inventario disponible insuficiente, inventario final: " + producto.Prd_InvFinal.ToString() + ", asignado: " + producto.Prd_Asignado.ToString() + ", disponible:" + (producto.Prd_InvFinal - producto.Prd_Asignado).ToString(), rdBox.ClientID);
                        rdBox.Text = "";
                        return;
                    }
                }

                if (grupoMovimientosActivo == 2 || grupoMovimientosActivo == 1)
                {
                    int territorio = int.Parse((j.FindControl("RadComboBox1") as RadComboBox).SelectedValue);
                    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                    int verificador = 0;
                    CNentrada.ConsultarSaldo(session.Id_Emp, session.Id_Cd_Ver, id_prd.ToString(), territorio.ToString(), txtClienteId.Text, session.Emp_Cnx, ref verificador, cmbTipoMovimento.SelectedValue);

                    int Prd_AgrupadoSpo = producto.Prd_AgrupadoSpo;

                    int cantidadEnDt = 0;
                    foreach (DataRow dr in dt.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and territorio='" + territorio + "'"))
                    {
                        cantidadEnDt += Convert.ToInt32(dr["Cantidad"]);
                    }

                    int cantidadenRemision = 0;

                    if (actualizacionDocumento)
                        if (dttemp1_original.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                            cantidadenRemision = int.Parse(dttemp1_original.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'")[0]["NumeroElementos"].ToString());

                    int cantidadEnDttemp_original = 0;

                    if (Session["estatus" + Session.SessionID + HF_ClvPag.Value].ToString() != "1")
                    {
                        cantidadEnDttemp_original = 0;
                    }
                    else
                    {
                        cantidadEnDttemp_original = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
                    }

                    if ((actualizacionDocumento) && cantidadEnDt - cantidadEnDttemp_original + rdBox.Value.Value > verificador + cantidadenRemision)
                    //if ((actualizacionDocumento) && ((cantidadEnDt + rdBox.Value.Value + cantidadES) - cantidadEnDttemp_original   > cantidadenRemision))
                    {
                        AlertaFocus("Los artículos sobrepasan lo disponible", rdBox.ClientID);
                        rdBox.Text = "";
                        return;
                    }

                    if ((actualizacionDocumento == false) && ((cantidadEnDt + rdBox.Value.Value - cantidadEnDttemp_original) > verificador + cantidadenRemision))
                    {
                        AlertaFocus("Los artículos sobrepasan lo disponible", rdBox.ClientID);
                        rdBox.Text = "";
                        return;
                    }
                }
                else if (grupoMovimientosActivo == 3)
                {
                    CN_CapRemision rem = new CN_CapRemision();

                    string refe = txtReferencia.Visible ? txtReferencia.Text : txtReferencia2.Text;
                    int cantidadES = 0;

                    int cantidadEnDttemp_original = 0;
                    if (Session["estatus" + Session.SessionID + HF_ClvPag.Value].ToString() != "1")
                    {
                        cantidadEnDttemp_original = 0;
                    }
                    else
                    {
                        cantidadEnDttemp_original = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
                    }

                    int cantidadB = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Id_Prd"].ToString() == id_prd.ToString())
                        {
                            cantidadB += Convert.ToInt32(dr["Cantidad"]);

                        }
                    }


                    //rem.ConsultarRemisionesCantidad(session.Id_Emp, session.Id_Cd_Ver, refe, id_prd, ref cantidadES, session.Emp_Cnx);
                    rem.ConsultarRemisionesCantidadRem(session.Id_Emp, session.Id_Cd_Ver, refe, id_prd, ref cantidadES, session.Emp_Cnx);
                    int cantidadES2 = 0;
                    if (actualizacionDocumento)
                    {
                        rem.ConsultarRemisionesCantidadRemCantidad(session.Id_Emp, session.Id_Cd_Ver, txtFolio.Text, id_prd, cmbNaturaleza.SelectedValue, ref cantidadES2, session.Emp_Cnx);
                        cantidadES += cantidadES2;
                    }



                    if (cantidadES < cantidadB - cantidadEnDttemp_original + rdBox.Value.Value)
                    //if (cantidadES < rdBox.Value.Value)
                    {
                        AlertaFocus("Los artículos sobrepasan el disponible", rdBox.ClientID);
                        rdBox.Text = "";
                        return;
                    }

                    if (producto.Prd_InvFinal - producto.Prd_Asignado - (cantidadEnDttemp_original - rdBox.Value.Value) < 0)
                    {
                        AlertaFocus("Producto " + producto.Id_Prd.ToString() + " inventario disponible insuficiente, inventario final: " + producto.Prd_InvFinal.ToString() + ", asignado: " + producto.Prd_Asignado.ToString() + ", disponible:" + (producto.Prd_InvFinal - producto.Prd_Asignado).ToString(), rdBox.ClientID);
                        rdBox.Text = "";
                        return;
                    }
                }
                else if (grupoMovimientosActivo == 4)
                {
                    if (actualizacionDocumento)
                    {
                        CN_CapRemision rem = new CN_CapRemision();
                        int cantidadES2 = 0;
                        rem.ConsultarRemisionesCantidadRemCantidad(session.Id_Emp, session.Id_Cd_Ver, txtFolio.Text, id_prd, cmbNaturaleza.SelectedValue, ref cantidadES2, session.Emp_Cnx);

                        Producto cp = new Producto();
                        CN_CatProducto cn_catproducto = new CN_CatProducto();
                        cn_catproducto.ConsultaProducto(ref cp, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, id_prd);

                        int cantidadB = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["Id_Prd"].ToString() == id_prd.ToString())
                            {
                                cantidadB += Convert.ToInt32(dr["Cantidad"]);

                            }
                        }

                        cantidadB = cantidadB - Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]) + (int)rdBox.Value.Value;
                        if (cantidadB < cantidadES2 && (cantidadES2 - cantidadB) > (cp.Prd_InvFinal - cp.Prd_Asignado))
                        {
                            AlertaFocus("Producto " + id_prd.ToString() + " inventario disponible insuficiente, inventario final: " + cp.Prd_InvFinal.ToString() + ", asignado: " + cp.Prd_Asignado.ToString() + " , disponible: " + (cp.Prd_InvFinal - cp.Prd_Asignado).ToString() + "", rdBox.ClientID);
                            rdBox.Text = "";
                            return;
                        }

                    }
                }

                RadNumericTextBox txt_Precio = (j.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox);
                txt_Precio.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProveedor_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            dt = null;
            crearDT();
            if (txtReferencia.Visible)
            {
                txtReferencia.Focus();
            }
            else
            {
                txtReferencia2.Focus();
            }
        }
        protected void RAM1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 180);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void llenarNaturaleza()
        {
            cmbNaturaleza.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbNaturaleza.Items.Insert(1, new RadComboBoxItem("Entrada", "0"));
            cmbNaturaleza.Items.Insert(2, new RadComboBoxItem("Salida", "1"));

            cmbTipoMovimento.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }
        private int consultarConsecutivo(int Naturaleza_movimiento)
        {
            CN_CapEntradaSalida cn_entradasal = new CN_CapEntradaSalida();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int naturalela = int.Parse(cmbNaturaleza.SelectedValue);
            int consecutivo = 0;
            cn_entradasal.ConsultarConsecutivo(Sesion, naturalela, ref consecutivo);
            return consecutivo;
        }
        private void CargarTipoMovimiento(int tipo_movimiento) //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, tipo_movimiento, Sesion.Emp_Cnx, "spCatMovimientoFiltro_Combo", ref cmbTipoMovimento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RemoverItem(int[] NoVisibles)
        {
            foreach (int tm in NoVisibles)
            {
                RadComboBoxItem bi = cmbTipoMovimento.FindItemByValue(tm.ToString());
                if (bi != null)
                    cmbTipoMovimento.Items.Remove(bi);
            }
        }
        private List<Producto> LlenarComboProductosLista()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = new Producto();

                List<Producto> listaProducto = new List<Producto>();
                new CN_CatProducto().ConsultaListaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, string.Empty, ref listaProducto, 1);

                producto = new Producto();
                producto.Id_Prd = -1;
                producto.Prd_Descripcion = "-- Seleccionar --";
                listaProducto.Insert(0, producto);
                return listaProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProveedor()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spProveedores_Combo", ref cmbProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Carga el combo RadComboBoxTerritorio, con el territorio de la cabecera, de la remision correspondiente
        /// </summary>
        private void cargarTerritorio()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                new CapaNegocios.CN__Comun().LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, int.Parse(txtReferencia2.Text), Convert.ToInt32(HF_TipoDoc.Value), Sesion.Emp_Cnx, "spCapRemision_ComboXReferencia", ref RadComboBoxTerritorio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarTerritorioDetalles(ref RadComboBox combo_a_llenar)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, int.Parse(txtReferencia2.Text), Convert.ToInt32(HF_TipoDoc.Value), Sesion.Emp_Cnx, "spCapRemision_ComboDetalleXReferencia", ref combo_a_llenar);
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
                if (Request.QueryString["id"] != "-1") // EDICION
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                }
                else //NUEVO
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                }

                bool valido = true;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                if ((dpFecha.SelectedDate >= session.CalendarioIni) && (dpFecha.SelectedDate <= session.CalendarioFin))
                {
                    if (cmbNaturaleza.SelectedValue != "-1")
                    {
                        #region "comentarios"
                        //switch (grupoMovimientosActivo)
                        //{//tablas para comparar documentos
                        //    case 1:
                        //    case 3:
                        //        /*traer tabla con elementos de la base de datos*/
                        //        // REMISION
                        //        tablaRemision = new DataTable();
                        //        new CN_CatRemision().ConsultarTotalProductoDocumento(int.Parse(txtReferencia2.Text), 1, session, ref tablaRemision);
                        //        break;
                        //    case 2:
                        //        /*traer tabla con elementos de la base de datos*/
                        //        //FACTURA
                        //        tablaRemision = new DataTable();
                        //        new CN_CatRemision().ConsultarTotalProductoDocumento(int.Parse(txtReferencia2.Text), 2, session, ref tablaRemision);
                        //        break;
                        //    default:
                        //        break;
                        //}
                        #endregion
                        if (requiereReferencia)
                        {
                            #region comentarizado
                            //tablaRemision = new DataTable();
                            //new CN_CatRemision().ConsultarTotalProductoDocumento(int.Parse(txtReferencia2.Text), Tm_ReqTDoc, session, ref tablaRemision);
                            ///*Comparar elementos en dttemp1 contra los de la base de datos */
                            //foreach (DataRow dttemp1row in dttemp1.Rows)
                            //{
                            //    if (int.Parse(dttemp1row["NumeroElementos"].ToString()) > int.Parse(tablaRemision.Select("Prd_AgrupadoSpo='" + dttemp1row["Prd_AgrupadoSpo"].ToString() + "'")[0]["Cantidad"].ToString()))
                            //    {
                            //        valido = false;
                            //        switch (Tm_ReqTDoc)
                            //        {
                            //            case 1:
                            //                Alerta("El número de productos sobrepasa la cantidad en la remision");
                            //                break;
                            //            case 2:
                            //                Alerta("El número de productos sobrepasa la cantidad en la factura");
                            //                break;
                            //            default:
                            //                throw new Exception("H");
                            //        }
                            //    }
                            //}
                            #endregion
                        }
                        //{/*verifica campos vacios pestaña detalles*/
                        if (valido)
                        {
                            if (valido == true && dt == null)
                            {
                                Alerta("Aún no se han capturado partidas");
                                valido = false;
                                return;
                            }
                            if (valido == true && dt.Rows.Count == 0)
                            {
                                Alerta("Aún no se han capturado partidas");
                                valido = false;
                                return;
                            }
                        }
                        else
                            return;
                        //}

                        switch (grupoMovimientosActivo)
                        {
                            case 1:

                            case 2:
                                grabar(0);
                                break;
                            case 3:
                                grabar(1);
                                #region "comentarios"
                                ///*Comparar elementos en dttemp1 contra los de la base de datos */
                                //foreach (DataRow dttemp1row in dttemp1.Rows)
                                //{
                                //    if (int.Parse(dttemp1row["NumeroElementos"].ToString()) > int.Parse(tablaRemision.Select("Prd_AgrupadoSpo='" + dttemp1row["Prd_AgrupadoSpo"].ToString() + "'")[0]["Cantidad"].ToString()))
                                //    {
                                //        valido = false;
                                //        switch (grupoMovimientosActivo)
                                //        {
                                //            case 1:
                                //            case 3:
                                //                throw new Exception("El numero de productos sobrepasa la cantidad en la remision");
                                //                break;
                                //            case 2:
                                //                throw new Exception("El numero de productos sobrepasa la cantidad en la factura");
                                //                break;
                                //            default:
                                //                throw new Exception("H");
                                //                break;
                                //        }
                                //    }
                                //}
                                //if (true)
                                //{/*verifica campos vacios pestaña detalles*/
                                //    if (valido)
                                //    {
                                //        valido = validarCamposDetalle();
                                //    }
                                //    if (valido)
                                //    {
                                //        if (valido == true && dt == null)
                                //        {
                                //            valido = false;
                                //        }
                                //        if (valido == true && dt.Rows.Count == 0)
                                //        {
                                //            valido = false;
                                //        }
                                //    }
                                //}
                                #endregion
                                /* validar que los elementos no pasen de lo que se tiene en base
                                 * instalada para los casos 6,15,16 (grupo 1) ---> ESBITERR
                                 * (SE VALIDA EN "GuardarEntradaSalida")*/
                                break;
                            case 4:
                                #region "comentarios"
                                //{/*verifica campos vacios pestaña detalles*/
                                //    if (valido)
                                //    {
                                //        valido = validarCamposDetalle();
                                //    }
                                //    if (valido)
                                //    {
                                //        if (valido == true && dt == null)
                                //        {
                                //            valido = false;
                                //        }
                                //        if (valido == true && dt.Rows.Count == 0)
                                //        {
                                //            valido = false;
                                //        }
                                //    }
                                //}
                                #endregion
                                if (valido)
                                    grabar(2);
                                break;
                            case 0:
                                #region "comentarios"
                                //{/*verifica campos vacios pestaña detalles*/
                                //    if (valido)
                                //    {
                                //        valido = validarCamposDetalle();
                                //    }
                                //    if (valido)
                                //    {
                                //        if (valido == true && dt == null)
                                //        {
                                //            valido = false;
                                //            Alerta("Aun no se han capturado partidas");
                                //        }
                                //        if (valido == true && dt.Rows.Count == 0)
                                //        {
                                //            valido = false;
                                //            Alerta("Aun no se han capturado partidas");
                                //        }
                                //    }
                                //}
                                #endregion
                                if (valido)
                                    grabar(0);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                        Alerta("Seleccione naturaleza del movimiento");
                }
                else
                    Alerta("Fecha se encuentra fuera del periodo");
            }
            catch (Exception ex)
            {
                atraparMsgBaseDatos(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        /// <summary>
        /// afecta 1 remision, 2 orden de compra, 0 nada
        /// </summary>
        /// <param name="afecta">1 remision, 2 orden de compra, 0 nada</param>
        private void grabar(int afecta)
        {
            try
            {
                int VGEmpresa = 0;
                Int32.TryParse(strEmp, out VGEmpresa);
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapEntradaSalida cn_capEntradaSalida = new CN_CapEntradaSalida();

                EntradaSalida entsal = new EntradaSalida();
                entsal.Id_Emp = session.Id_Emp;
                entsal.Id_Cd = session.Id_Cd_Ver;
                entsal.Id_Es = int.Parse(txtFolio.Text);
                entsal.Es_Naturaleza = int.Parse(cmbNaturaleza.SelectedValue);
                //verificar fecha dentro del periodo
                entsal.Es_Fecha = Convert.ToDateTime(dpFecha.SelectedDate);
                entsal.Id_Tm = int.Parse(cmbTipoMovimento.SelectedValue);
                entsal.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                entsal.Id_Pvd = int.Parse(cmbProveedor.SelectedValue);
                if (requiereReferencia)
                    entsal.Es_Referencia = txtReferencia2.Text;
                else
                    entsal.Es_Referencia = txtReferencia.Text;

                entsal.Es_Notas = txtNotas.Text;
                entsal.Es_SubTotal = subtotal;
                entsal.Es_Iva = IVA;
                entsal.Es_Total = total;
                entsal.Es_Estatus = "C";

                List<EntradaSalidaDetalle> listaDetalleS = new List<EntradaSalidaDetalle>();
                EntradaSalidaDetalle detalle = new EntradaSalidaDetalle();
                foreach (DataRow row in dt.Rows)
                {
                    detalle = new EntradaSalidaDetalle();
                    detalle.Id_Emp = session.Id_Emp;
                    detalle.Id_Cd = session.Id_Cd_Ver;
                    detalle.Id_EsDet = int.Parse(row["Id_EsDet"].ToString());
                    detalle.Id_Ter = Convert.IsDBNull(row["territorio"]) ? -1 : Convert.ToInt32(row["territorio"]);
                    detalle.Es_BuenEstado = Convert.IsDBNull(row["buenEstado"]) ? false : Convert.ToBoolean(row["buenEstado"]);
                    detalle.Id_Prd = int.Parse(row["Id_Prd"].ToString());
                    detalle.Es_Cantidad = int.Parse(row["Cantidad"].ToString());
                    detalle.Es_Costo = float.Parse(row["Costo"].ToString());
                    detalle.Afct_OrdCompra = Convert.IsDBNull(row["afecta"]) ? false : Convert.ToBoolean(row["afecta"]);
                    detalle.Prd_AgrupadoSpo = int.Parse(row["Prd_AgrupadoSpo"].ToString());
                    listaDetalleS.Add(detalle);
                }
                int verificador = 0;
                string verificadorStr = "";
                if (!actualizacionDocumento)
                {
                 /*  cn_capEntradaSalida.GuardarEntradaSalida(ref entsal, ref listaDetalleS, session, ref verificador, int.Parse(cmbTipoMovimento.SelectedValue),
                        grupoMovimientosActivo, afecta, (int.Parse(cmbNaturaleza.SelectedValue) == 0) ? true : false,
                        grupoMovimientosActivo == 4 ? ProdPreLoc_Actualiza : new DataTable(), ref verificadorStr, VGEmpresa);*/
                }
                else
                {
                    cn_capEntradaSalida.EdicionEntradaSalida(ref entsal, ref listaDetalleS, session, ref verificador, int.Parse(cmbTipoMovimento.SelectedValue),
                       grupoMovimientosActivo, afecta, (int.Parse(cmbNaturaleza.SelectedValue) == 0) ? true : false,
                       grupoMovimientosActivo == 4 ? ProdPreLoc_Actualiza : new DataTable(), ref verificadorStr, VGEmpresa);
                }
                if (verificadorStr != "0")
                {
                    RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindowA('", verificadorStr + "<br>Los datos se guardaron correctamente", "')"));
                }
                else
                {
                    RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindowA('", "<br>Los datos se guardaron correctamente", "')"));
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        private void crearDT()
        {
            if (dt == null)
            {
                subtotal = 0;
                total = 0;
                IVA = 0;
                id_detalle = 0;
                RadNumericTextBoxSubTotal.Value = 0;
                RadNumericTextBoxIVA.Value = 0;
                RadNumericTextBoxTotal.Value = 0;

                dt = new DataTable();
                crear_dttemp1();
                dt.Columns.Add("Id_EsDet"); //el id o numeracion de cada detalle de la entrada o salida
                dt.Columns.Add("Id_Prd"); //Id_Prd
                dt.Columns.Add("Descripcion"); //i
                dt.Columns.Add("Presen"); //i
                dt.Columns.Add("Cantidad"); //@Es_Cantidad 
                dt.Columns.Add("Costo", typeof(double)); //@Es_Costo 
                dt.Columns.Add("importe", typeof(double)); //i
                dt.Columns.Add("afecta"); //@Afct_OrdCompra
                dt.Columns.Add("territorio"); //@Id_Ter 
                dt.Columns.Add("buenEstado"); //@Es_BuenEstado
                dt.Columns.Add("Prd_AgrupadoSpo"); //@Prd_AgrupadoSpo
                rgEntradaSalida.Rebind();
            }
        }
        private float obtenerPrecioAAA(int Id_Prd)
        {
            float precio = 0;
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ProductoPrecios cn_proprec = new CN_ProductoPrecios();
            int Id_Pre = 0;
            cn_proprec.ConsultaListaProductoPrecioAAA(ref precio, Sesion, Id_Prd, ref Id_Pre);
            return precio;
        }
        private string msgerror(Exception exception)
        {
            switch (exception.Message)
            {
                case "msg01":
                    { return "Ya existe periodo con este Año - Mes"; }

                case "msg02":
                    { return "Fecha final debe ser mayor a la fecha inicial"; }

                case "msg03":
                    { return "El periodo seleccionado se empalma con uno existente"; }
                default:
                    { return exception.Message; }
            }
        }
        private void crear_dttemp1()
        {
            dttemp1 = new DataTable();
            dttemp1.Columns.Add("Prd_AgrupadoSpo");
            dttemp1.Columns.Add("Id_Ter");
            dttemp1.Columns.Add("NumeroElementos");
        }
        private void contar()
        {
            /*METODO OBSOLETO*/
            DataRow[] editable_dr;
            foreach (DataRow rowdt in dt.Rows)
            {
                if (dttemp1.Select("Prd_AgrupadoSpo='" + rowdt["Prd_AgrupadoSpo"].ToString() + "'").Length > 0)
                {
                    editable_dr = dttemp1.Select("Prd_AgrupadoSpo='" + rowdt["Prd_AgrupadoSpo"].ToString() + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["NumeroElementos"] = int.Parse(editable_dr[0]["NumeroElementos"].ToString()) + int.Parse(rowdt["Cantidad"].ToString());
                    editable_dr[0].AcceptChanges();
                }
                else
                    dttemp1.Rows.Add(new object[] { rowdt["Prd_AgrupadoSpo"].ToString(), rowdt["Cantidad"].ToString() });
            }
        }
        /// <summary>
        /// Muestra u oculta las columnas "territorio" y "buen estado" del grid "rgEntradaSalida"...
        /// "No hay mujer fea, sólo belleza rara"
        /// </summary>
        /// <param name="mostrar"></param>
        private void mostrarColumnas(bool mostrarColumnas)
        {
            if (mostrarColumnas)
            {
                rgEntradaSalida.Columns.FindByUniqueName("territorio").Visible = true;
                ((GridTemplateColumn)rgEntradaSalida.Columns.FindByUniqueName("territorio")).ReadOnly = false;
                rgEntradaSalida.Columns.FindByUniqueName("buenEstado").Visible = true;
                ((GridCheckBoxColumn)rgEntradaSalida.Columns.FindByUniqueName("buenEstado")).ReadOnly = false;
                rgEntradaSalida.Rebind();
            }
            else
            {
                rgEntradaSalida.Columns.FindByUniqueName("territorio").Visible = false;
                ((GridTemplateColumn)rgEntradaSalida.Columns.FindByUniqueName("territorio")).ReadOnly = true;
                rgEntradaSalida.Columns.FindByUniqueName("buenEstado").Visible = false;
                ((GridCheckBoxColumn)rgEntradaSalida.Columns.FindByUniqueName("buenEstado")).ReadOnly = true;
                rgEntradaSalida.Rebind();
            }
        }
        /// <summary>
        /// Muestra u oculta la columna "afecta orden de compra"
        /// </summary>
        /// <param name="mostrar"></param>
        private void mostrarColumnas2(bool mostrarColumnas)
        {
            if (mostrarColumnas)
            {
                rgEntradaSalida.Columns.FindByUniqueName("afecta").Visible = true;
                ((GridCheckBoxColumn)rgEntradaSalida.Columns.FindByUniqueName("afecta")).ReadOnly = false;
                rgEntradaSalida.Rebind();
            }
            else
            {
                rgEntradaSalida.Columns.FindByUniqueName("afecta").Visible = false;
                ((GridCheckBoxColumn)rgEntradaSalida.Columns.FindByUniqueName("afecta")).ReadOnly = true;
                rgEntradaSalida.Rebind();
            }
        }
        private void reiniciarVariables()
        {
            dt = null;
            crearDT();
            requiereReferencia = false;
            referencia_valida = false;
            grupoMovimientosActivo = 0;
            tablaRemision = null;
            rgEntradaSalida.Rebind();
            crearProdPreLoc_Actualiza();
        }
        private void crearProdPreLoc_Actualiza()
        {
            ProdPreLoc_Actualiza = new DataTable();
            ProdPreLoc_Actualiza.Columns.Add("Id_EsDet");
            ProdPreLoc_Actualiza.Columns.Add("Id_Emp");
            ProdPreLoc_Actualiza.Columns.Add("Id_Cd");
            ProdPreLoc_Actualiza.Columns.Add("Id_Prd");
            ProdPreLoc_Actualiza.Columns.Add("Id_Pre");
            ProdPreLoc_Actualiza.Columns.Add("Prd_Actual");
            ProdPreLoc_Actualiza.Columns.Add("Prd_FechaInicio");
            ProdPreLoc_Actualiza.Columns.Add("Prd_FechaFin");
            ProdPreLoc_Actualiza.Columns.Add("Prd_PreDescripcion");
            ProdPreLoc_Actualiza.Columns.Add("Prd_Pesos", typeof(double));
        }
        private bool validarCliente_Territorio()
        {
            bool valido = true;
            if (RadComboBoxTerritorio.SelectedValue != null && RadComboBoxTerritorio.SelectedValue != "" && txtClienteId.Value.HasValue)
            {
                if (int.Parse(RadComboBoxTerritorio.SelectedValue) > 0 && Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1) > 0)
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    ClienteDet cliente = new ClienteDet();
                    CN_CatCliente catcliente = new CN_CatCliente();

                    List<Territorios> territorios = new List<Territorios>();
                    catcliente.ConsultaTerritoriosDelCliente(Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1), Sesion, ref territorios);
                    if (territorios.Count > 0)
                    {
                        Territorios t1 = territorios.Find(delegate(Territorios p) { return p.Id_Ter == int.Parse(RadComboBoxTerritorio.SelectedValue); });
                        if (t1 == null)
                        {
                            Alerta("Este cliente no corresponde con el territorio del documento al que hace referencia");
                            valido = false;
                        }
                    }
                    else
                    {
                        Alerta("El cliente no tiene territorios asignados");
                        valido = false;
                    }
                }
            }
            return valido;
        }
        private bool validarCamposDetalle()
        {
            bool valido = true;
            //comprobar campos de referencia
            if (requiereReferencia)// si requiere referencia
            {
                if (txtReferencia2.Text == "")//verifica que no esté vacia
                {
                    RadTabStrip1.SelectedIndex = 0;
                    RadMultiPage1.SelectedIndex = 0;
                    RadAjaxManager1.ResponseScripts.Add(@"CloseWindow3()"); //llama una funcion de java script que avisa sobre ref vacia y hace focus en el campo
                    return false;
                }
                else
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_CatRemision cn_rem = new CN_CatRemision();
                    int verificador = 0;
                    int Tipodoc = 0;
                    switch (grupoMovimientosActivo)
                    {
                        case 1:
                        case 3:
                            Tipodoc = 1;//el 1 es remision
                            break;
                        case 2:
                            Tipodoc = 2;//el 2 es factura 
                            break;
                        case 4:
                            Tipodoc = 2;//el 2 es factura
                            break;
                        case 0:
                            throw new Exception("Error");
                        //break;
                        default:
                            throw new Exception("Error");
                        //break;
                    }

                    cn_rem.ConsultarReferencia(Sesion, int.Parse(txtReferencia2.Text), ref verificador, Tipodoc);
                    if (verificador != 0)
                        referencia_valida = true;
                    if (!referencia_valida)
                    {
                        RadTabStrip1.SelectedIndex = 0;
                        RadMultiPage1.SelectedIndex = 0;
                        RadAjaxManager1.ResponseScripts.Add(@"CloseWindow2('El número de referencia no existe')");
                        return false;
                    }
                    if (grupoMovimientosActivo == 1 || grupoMovimientosActivo == 3)
                    {
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        int partidasConSaldo = 0;
                        new CN_CapRemision().ConsultarPartidasConSaldo(sesion, int.Parse(txtReferencia2.Text), ref partidasConSaldo);
                        if (partidasConSaldo == 0)
                        {
                            Alerta("Remisión con saldo insuficiente");
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (txtReferencia.Text == "")
                {
                    RadTabStrip1.SelectedIndex = 0;
                    RadMultiPage1.SelectedIndex = 0;
                    RadAjaxManager1.ResponseScripts.Add(@"CloseWindow()");
                    valido = false;
                }
            }
            if (valido == true)
            {//validar campos
                //naturaleza
                if (int.Parse(cmbNaturaleza.SelectedValue) != -1)
                {//cmbtipomovimiento
                    if (int.Parse(cmbTipoMovimento.SelectedValue) != -1)
                    {
                        //validar cliente
                        if ((Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1) < 1) &&
                            ((cmbProveedor.SelectedValue == null || int.Parse(cmbProveedor.SelectedValue) < 1) &&
                            (cmbProveedor.SelectedValue == null || int.Parse(cmbProveedor.SelectedValue) < 1)))
                        {
                            Alerta("Favor de capturar todos los campos en la pestaña de datos generales");
                            RadTabStrip1.SelectedIndex = 0;
                            RadMultiPage1.SelectedIndex = 0;
                            valido = false;
                        }
                    }
                    else
                    {
                        Alerta("Favor de capturar todos los campos en la pestaña de datos generales");
                        RadTabStrip1.SelectedIndex = 0;
                        RadMultiPage1.SelectedIndex = 0;
                        valido = false;
                    }
                }
                else
                {
                    Alerta("Favor de capturar todos los campos en la pestaña de datos generales");
                    RadTabStrip1.SelectedIndex = 0;
                    RadMultiPage1.SelectedIndex = 0;
                    valido = false;
                }
            }
            //verificar notas
            if (valido == true && txtNotas.Text == "")
            {
                Alerta("Favor de capturar todos los campos en la pestaña de datos generales");
                RadTabStrip1.SelectedIndex = 0;
                RadMultiPage1.SelectedIndex = 0;
                valido = false;
            }
            if (valido == true)
                valido = validarCliente_Territorio();
            /*verificar fecha dentro del periodo*/
            return valido;
        }
        /// <summary>
        /// Prepara la pantalla para una nueva captura ("reinicia la pantalla")
        /// </summary>
        private void nuevo()
        {
            cmbNaturaleza.SelectedIndex = 0;
            LimpiarClienteProveedor();
            reiniciarVariables();
            cmbTipoMovimento.Enabled = false;
            cmbTipoMovimento.ClearSelection();
            txtTipoId.Enabled = false;
            txtTipoId.Text = "";
            txtFolio.Text = "";
            txtNotas.Text = "";
            RadNumericTextBoxSubTotal.Value = 0;
            RadNumericTextBoxIVA.Value = 0;
            RadNumericTextBoxTotal.Value = 0;
            RadTabStrip1.SelectedIndex = 0;
            RadMultiPage1.SelectedIndex = 0;
        }
        private void Inicializar()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);

            if (Request.QueryString["id"] != "-1")//Edicion
            {
                actualizacionDocumento = true;
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Es;
                int.TryParse(Request.QueryString["id"], out Id_Es);
                int Es_Naturaleza;
                int.TryParse(Request.QueryString["Es_Naturaleza"], out Es_Naturaleza);
                cargarMovimientoEntSal(sesion, sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Es, Es_Naturaleza);
                if (Request.QueryString["soloVer"].ToString() != "undefined")
                    HF_SoloVer.Value = "1";

                //Sacar copia de los originales dt y dttemp1
                dt_original = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    dt_original.ImportRow(dr);
                }

                dttemp1_original = dttemp1.Clone();
                foreach (DataRow dr in dttemp1.Rows)
                {
                    dttemp1_original.ImportRow(dr);
                }

                if (!RadComboBoxTerritorio.Visible)
                {
                    RequiredFieldValidator7.ValidationGroup = "nn";
                    RequiredFieldValidator8.ValidationGroup = "nn";
                }
            }
            else //movimiento nuevo
            {
                actualizacionDocumento = false;
                nuevo();
            }
            habilitarDeshabilitar(_PermisoModificar);
            botones_radtoolbar();//esconde o muestra los botones grabar , nuevo , imprimir , etc segun los permisos
        }
        /// <summary>
        /// Metodo que carga el movimeitno de EntradaSalida que se va a modificar
        /// </summary>
        private void cargarMovimientoEntSal(Sesion sesion, int Id_Emp, int Id_Cd_Ver, int Id_Es, int Es_Naturaleza)
        {
            ////aqui se va traer la info del documento a editar             
            EntradaSalida entradaSalida = new EntradaSalida();
            try
            {
                new CN_CapEntradaSalida().ConsultarEntradaSalida(sesion, sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Es, Es_Naturaleza, ref entradaSalida);
                cmbNaturaleza.SelectedValue = entradaSalida.Es_Naturaleza.ToString();
                cmbNaturaleza_indiceCambiado();//al cambiar el combo naturaleza se modifica el folio y el tipo de movimiento
                txtFolio.Text = entradaSalida.Id_Es.ToString();//se asigna el folio correcto(el que se va a modificar)
                if (entradaSalida.Es_Fecha != DateTime.MinValue)
                    dpFecha.SelectedDate = entradaSalida.Es_Fecha;
                txtTipoId.Text = entradaSalida.Id_Tm.ToString();
                // cmbTipoMovimento.SelectedValue = entradaSalida.Id_Tm.ToString();
                // string value = cmbTipoMovimento.Items.FindItemByValue(entradaSalida.Id_Tm.ToString()).Value;
                if (cmbTipoMovimento.FindItemIndexByValue(entradaSalida.Id_Tm.ToString()) > 0)
                {
                    //if (!string.IsNullOrEmpty(cmbTipoMovimento.Items.FindItemByValue(entradaSalida.Id_Tm.ToString()).Value))
                    //    if (cmbTipoMovimento.Items.Contains(cmbTipoMovimento.Items[entradaSalida.Id_Tm]))
                    cmbTipoMovimento.SelectedIndex = cmbTipoMovimento.FindItemIndexByValue(entradaSalida.Id_Tm.ToString());
                    cmbTipoMovimento.Text = cmbTipoMovimento.FindItemByValue(entradaSalida.Id_Tm.ToString()).Text;
                    //cmbTipoMovimento.Text = cmbTipoMovimento.Items[entradaSalida.Id_Tm].Text;
                }
                cmbTipoMovimento_indiceCambiado();
                txtClienteNombre.Text = entradaSalida.Cte_NomComercial;
                txtClienteId.Text = entradaSalida.Id_Cte == -1 ? "" : entradaSalida.Id_Cte.ToString();//asignar cuando corresponda
                //cmbProveedor.SelectedValue = entradaSalida.Id_Pvd.ToString();//asignar cuando corresponda
                //if (entradaSalida.Id_Pvd != -1)
                //    if (cmbProveedor.Items.Contains(cmbProveedor.Items[entradaSalida.Id_Pvd]))
                //        cmbProveedor.Text = cmbProveedor.Items[entradaSalida.Id_Pvd].Text; //cmbProveedor.Items[cmbProveedor.FindItemIndexByValue(entradaSalida.Id_Pvd.ToString())].Text;

                if (cmbProveedor.FindItemIndexByValue(entradaSalida.Id_Pvd.ToString()) > 0)
                {
                    //if (!string.IsNullOrEmpty(cmbTipoMovimento.Items.FindItemByValue(entradaSalida.Id_Tm.ToString()).Value))
                    //    if (cmbTipoMovimento.Items.Contains(cmbTipoMovimento.Items[entradaSalida.Id_Tm]))
                    cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue(entradaSalida.Id_Pvd.ToString());
                    cmbProveedor.Text = cmbProveedor.FindItemByValue(entradaSalida.Id_Pvd.ToString()).Text;
                    //cmbTipoMovimento.Text = cmbTipoMovimento.Items[entradaSalida.Id_Tm].Text;
                }

                txtProveedorId.Text = entradaSalida.Id_Pvd == -1 ? "" : entradaSalida.Id_Pvd.ToString();//asignar cuando corresponda                
                if (requiereReferencia)
                {
                    double a = 0;
                    if (!double.TryParse(entradaSalida.Es_Referencia, out a))
                        a = Convert.ToDouble(entradaSalida.Es_Referencia.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[1]);

                    txtReferencia.Visible = false;
                    txtReferencia2.Visible = true;
                    txtReferencia2.Text = a.ToString();
                    txtReferencia.Text = "";
                }
                else
                {
                    txtReferencia.Visible = true;
                    txtReferencia.Text = entradaSalida.Es_Referencia;
                    txtReferencia2.Text = "";
                    txtReferencia2.Visible = false;
                }
                txtNotas.Text = entradaSalida.Es_Notas;

                List<EntradaSalidaDetalle> detalles = new List<EntradaSalidaDetalle>();
                //DataTable dt = new DataTable();
                new CN_CapEntradaSalida().ConsultarEntradaSalidaDetalles(sesion, entradaSalida, ref detalles);//, ref dt);
                //cargar tabla remision
                switch (grupoMovimientosActivo)
                {

                    case 3:
                        /*traer lista(remision) de spCapRemisionDet_ConsultarTotalProducto (agrupacion) para verificar entradas*/
                        if (id_detalle == 0)
                        {
                            Sesion Sesion = new Sesion();
                            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                            DataTable tablaRemision2 = new DataTable();
                            new CN_CatRemision().ConsultarTotalProductoDocumento(int.Parse(txtReferencia2.Text), 1, Sesion, ref tablaRemision2);
                            tablaRemision = tablaRemision2;
                            HF_TipoDoc.Value = "1";
                            cargarTerritorio();
                            crear_dttemp1();
                        }
                        break;
                    case 1:
                    case 2:
                        /*traer lista(facturas) de spCapRemisionDet_ConsultarTotalProducto (agrupacion) para verificar entradas*/
                        if (id_detalle == 0)
                        {
                            Sesion Sesion = new Sesion();
                            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                            tablaRemision = new DataTable();
                            //new CN_CatRemision().ConsultarTotalProductoDocumento(int.Parse(txtReferencia2.Text), 2, Sesion, ref tablaRemision);
                            HF_TipoDoc.Value = "2";
                            //cargarTerritorio();
                            crear_dttemp1();
                        }
                        break;
                    default:
                        break;
                }

                //Aqui se va agregar los detalles al dt, que es con el que trabajamos las vlidaciones y es el que llena la base de datos
                foreach (EntradaSalidaDetalle detalle in detalles)
                {
                    //Tambien hay que agregarlo al dttemp1 que es el que maneja las validaciones por agrupador
                    cargarDetalle_a_Dttemp1(detalle.Id_EsDet, detalle.Id_Ter, detalle.Es_BuenEstado, detalle.Prd_AgrupadoSpo, detalle.Id_Prd,
                                            detalle.Prd_Descripcion, detalle.Prd_Presentacion, detalle.Es_Cantidad, detalle.Es_Costo,
                                            (detalle.Es_Cantidad * detalle.Es_Costo), detalle.Afct_OrdCompra);
                }

                rgEntradaSalida.DataSource = dt;
                rgEntradaSalida.Rebind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void cargarDetalle_a_Dttemp1(int id_actual, int territorio, bool buenEstado, int Prd_AgrupadoSpo, int Id_Prd, string descripcion, string presentacion, int cantidad, double costo, double importe, bool afecta)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            double iva_cd = 0;
            new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
            bool eventocancelado = false;
            DataRow[] editable_dr;
            switch (grupoMovimientosActivo)
            {
                case 1:
                case 2:
                    dt.Rows.Add(new object[] { id_actual, Id_Prd, descripcion, presentacion, cantidad, costo, importe, null, territorio, buenEstado, Prd_AgrupadoSpo });
                    id_detalle = id_actual;
                    subtotal += cantidad * costo;
                    IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                    total = subtotal + IVA;
                    RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                    RadNumericTextBoxIVA.Text = IVA.ToString();
                    RadNumericTextBoxTotal.Text = total.ToString();

                    /*agregando productos a la lista (dttemp1)*/

                    if (dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                    {
                        editable_dr = dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'");
                        editable_dr[0].BeginEdit();
                        editable_dr[0]["NumeroElementos"] = int.Parse(editable_dr[0]["NumeroElementos"].ToString()) + cantidad;
                        editable_dr[0].AcceptChanges();
                    }
                    else
                        dttemp1.Rows.Add(new object[] { Prd_AgrupadoSpo, territorio, cantidad });



                    break;


                //case 2:
                case 3:
                    #region "comentarios"

                    //if (grupoMovimientosActivo == 1 || grupoMovimientosActivo == 2)
                    //{
                    //    // a)validar que el producto sea usado  -- grupos 1, 2
                    //    if (producto.Prd_Nuevo)
                    //    {
                    //        Alerta("El producto seleccionado tiene codigo invalido (no es un producto usado)");
                    //        e.Canceled = true;
                    //    }
                    //    if (grupoMovimientosActivo == 1) //esto es solo para el grupo 1
                    //    {
                    //        //validar que el producto sea por sistema de propietarios --grupo 1
                    //        if (!producto.Prd_AparatoSisProp)
                    //        {
                    //            Alerta("El producto no es un sistema de propietarios");
                    //            e.Canceled = true;
                    //        }
                    //    }
                    //}

                    //int territorio = int.Parse((editedItem["territorio"].FindControl("RadComboBox1") as RadComboBox).SelectedValue);
                    //bool buenEstado = (editedItem["buenEstado"].Controls[0] as CheckBox).Checked;
                    #endregion
                    int cantidadenRemision = 0;
                    if (tablaRemision.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                        cantidadenRemision = int.Parse(tablaRemision.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'")[0]["Cantidad"].ToString());

                    int cantidadEnDt = 0;
                    if (dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                        cantidadEnDt = int.Parse(dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'")[0]["NumeroElementos"].ToString());

                    /*validar qe el articulo y territorio seleccionado correspondan igual ke en la remision*/
                    //--grupo 1 ,2 y 3

                    //if (tablaRemision.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length == 0 && _PermisoGuardar)
                    //{
                    //    Alerta("El territorio para este artículo no corresponde con el del documento");
                    //    eventocancelado = true;
                    //}
                    #region "comentarios"
                    //c) Verifica la cantidad que no sea mayor a la remisión por agrupacion --grupo 1 ,2 
                    //if ((cantidadEnDt + cantidad) > cantidadenRemision)
                    //{
                    //    Alerta("Los articulos sobrepasa lo que se tiene en la remision");
                    //    e.Canceled = true;
                    //}
                    #endregion
                    //else
                    //{
                    if (eventocancelado != true)
                    {
                        dt.Rows.Add(new object[] { id_actual, Id_Prd, descripcion, presentacion, cantidad, costo, importe, null, territorio, buenEstado, Prd_AgrupadoSpo });
                        id_detalle = id_actual;
                        subtotal += cantidad * costo;
                        IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                        total = subtotal + IVA;
                        RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                        RadNumericTextBoxIVA.Text = IVA.ToString();
                        RadNumericTextBoxTotal.Text = total.ToString();

                        /*agregando productos a la lista (dttemp1)*/

                        if (dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'").Length > 0)
                        {
                            editable_dr = dttemp1.Select("Prd_AgrupadoSpo='" + Prd_AgrupadoSpo + "' and Id_Ter='" + territorio + "'");
                            editable_dr[0].BeginEdit();
                            editable_dr[0]["NumeroElementos"] = int.Parse(editable_dr[0]["NumeroElementos"].ToString()) + cantidad;
                            editable_dr[0].AcceptChanges();
                        }
                        else
                            dttemp1.Rows.Add(new object[] { Prd_AgrupadoSpo, territorio, cantidad });
                    }
                    //}

                    break;
                case 4:
                    #region "comentarios"
                    //// a)validar que el producto sea nuevo
                    //if (!producto.Prd_Nuevo)
                    //{
                    //    Alerta("El producto seleccionado tiene codigo invalido (no es un producto nuevo)");
                    //    e.Canceled = true;
                    //}
                    //si es compra local el precio no puede ser 0
                    //if (grupoMovimientosActivo == 4 && producto.Id_Cd == 0 && producto.Prd_Colo == true)
                    //{
                    //    if (precio == 0)
                    //    {
                    //        Alerta("Es importante tener actualizado los costos en productos de " +
                    //            "compras locales; favor de entrar a Inventarios - Catálogo - Productos " +
                    //            ", para capturar el precio vigente AAA");
                    //        //no dejar continuar
                    //        e.Canceled = true;
                    //    }
                    //}
                    #endregion
                    if (eventocancelado != true)
                    {
                        dt.Rows.Add(new object[] { id_actual, Id_Prd, descripcion, presentacion, cantidad, costo, importe, afecta, null, null, Prd_AgrupadoSpo });
                        id_detalle = id_actual;
                        subtotal += cantidad * costo;
                        IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                        total = subtotal + IVA;
                        RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                        RadNumericTextBoxIVA.Text = IVA.ToString();
                        RadNumericTextBoxTotal.Text = total.ToString();
                    }

                    break;
                case 0:
                    if (eventocancelado != true)
                    {
                        dt.Rows.Add(new object[] { id_actual, Id_Prd, descripcion, presentacion, cantidad, costo, importe, afecta, null, null, Prd_AgrupadoSpo });
                        id_detalle = id_actual;
                        subtotal += cantidad * costo;
                        IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                        total = subtotal + IVA;
                        RadNumericTextBoxSubTotal.Text = subtotal.ToString();
                        RadNumericTextBoxIVA.Text = IVA.ToString();
                        RadNumericTextBoxTotal.Text = total.ToString();
                    }
                    break;
                default:
                    break;
            }
        }
        private void cmbNaturaleza_indiceCambiado()
        {
            switch (cmbNaturaleza.SelectedValue)
            {
                case "0":
                    //entrada
                    cmbTipoMovimento.Enabled = actualizacionDocumento ? _PermisoModificar : true;
                    txtTipoId.Enabled = actualizacionDocumento ? _PermisoModificar : true;
                    txtTipoId.Text = actualizacionDocumento ? cmbTipoMovimento.SelectedValue : "";
                    cmbTipoMovimento.SelectedIndex = 0;
                    cmbTipoMovimento.Text = cmbTipoMovimento.Items[0].Text;
                    reiniciarVariables();
                    txtFolio.Text = actualizacionDocumento ? txtFolio.Text : consultarConsecutivo(int.Parse(cmbNaturaleza.SelectedValue)).ToString();
                    CargarTipoMovimiento(0);
                    RemoverItem(new int[] { 18, 51 });
                    break;
                case "1":
                    //salida
                    cmbTipoMovimento.Enabled = actualizacionDocumento ? _PermisoModificar : true;
                    txtTipoId.Enabled = actualizacionDocumento ? _PermisoModificar : true;
                    txtTipoId.Text = actualizacionDocumento ? cmbTipoMovimento.SelectedValue : "";
                    cmbTipoMovimento.SelectedIndex = 0;
                    cmbTipoMovimento.Text = cmbTipoMovimento.Items[0].Text;
                    reiniciarVariables();
                    txtFolio.Text = actualizacionDocumento ? txtFolio.Text : consultarConsecutivo(int.Parse(cmbNaturaleza.SelectedValue)).ToString();
                    CargarTipoMovimiento(1);
                    RemoverItem(new int[] { 17, 51, 53, 54, 60, 62, 63, 64, 65, 70, 72, 73, 74, 75 });
                    break;
                case "-1":
                    //-- Seleccionar --
                    cmbTipoMovimento.Enabled = false;
                    cmbTipoMovimento.ClearSelection();
                    cmbTipoMovimento.SelectedIndex = 0;
                    cmbTipoMovimento.Text = cmbTipoMovimento.Items[0].Text;
                    txtTipoId.Enabled = false;
                    txtTipoId.Text = "";
                    txtFolio.Text = "";
                    reiniciarVariables();
                    break;
                default:
                    break;
            }
            LimpiarClienteProveedor();
            dt = null;
            crearDT();
        }
        private void LimpiarClienteProveedor()
        {
            txtClienteNombre.Text = "";
            txtClienteId.Enabled = false;
            txtClienteId.Text = "";

            cmbProveedor.Enabled = false;
            cmbProveedor.Text = cmbProveedor.Items[0].Text;
            txtProveedorId.Enabled = false;
            txtProveedorId.Text = "";

            RadComboBoxTerritorio.Items.Clear();
            RadComboBoxTerritorio.Text = "";
            txtReferencia.Text = "";
            txtReferencia2.Text = "";
            txtReferencia.Visible = true;
            txtReferencia2.Visible = false;
            RadComboBoxTerritorio.Visible = false;
            LabelTerritorio.Visible = false;
            txtNotas.Text = "";

            rgEntradaSalida.MasterTableView.ClearEditItems();
            rgEntradaSalida.EditIndexes.Clear();
        }
        private void habilitarDeshabilitar(bool habilitar)
        {
            switch (actualizacionDocumento)
            {
                case true:
                    cmbNaturaleza.Enabled = false;
                    //rgEntradaSalida.Enabled = _PermisoModificar;
                    dpFecha.Enabled = false;
                    cmbTipoMovimento.Enabled = false;
                    txtTipoId.Enabled = false;
                    //cmbCliente.Enabled = false;
                    txtClienteId.Enabled = false;
                    cmbProveedor.Enabled = false;
                    txtProveedorId.Enabled = false;
                    txtReferencia.Enabled = false;
                    txtReferencia2.Enabled = false;
                    txtNotas.Enabled = _PermisoModificar;
                    break;
                case false:
                    cmbNaturaleza.Enabled = true;
                    rgEntradaSalida.Enabled = true;
                    dpFecha.Enabled = true;
                    txtReferencia.Enabled = true;
                    txtReferencia2.Enabled = true;
                    txtNotas.Enabled = true;
                    break;
                default:
                    break;
            }
        }
        private void botones_radtoolbar()
        {
            try
            {
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = actualizacionDocumento ? _PermisoModificar : true;
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar || _PermisoModificar ? true : false;
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = false; //!actualizacionDocumento ;
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = false;
                //Regresar
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("undo")).Visible = false;
                //Imprimir
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = false;
                //Correo
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("mail")).Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cmbTipoMovimento_indiceCambiado()
        {
            LimpiarClienteProveedor();
            dt = null;
            crearDT();
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatMovimientos cn_catMovimientos = new CN_CatMovimientos();
            int afecta = 2;
            cn_catMovimientos.ConsultaTmovimientoAfecta(session, int.Parse(cmbTipoMovimento.SelectedValue), ref afecta);
            bool requiereReferencia2 = false;
            int Tm_ReqTDoc2 = 0;
            cn_catMovimientos.ConsultarTmovimientoReqReferencia(session, int.Parse(cmbTipoMovimento.SelectedValue), 1, ref requiereReferencia2, ref Tm_ReqTDoc2);
            requiereReferencia = requiereReferencia2;
            Tm_ReqTDoc = Tm_ReqTDoc2;
            if (requiereReferencia)
            {
                if (Tm_ReqTDoc == -1)
                    Alerta("La base de datos no se ha llenado correctamente"); //este msg no aparece con una base de datos alimentada correctamente                
                //requiereReferencia = true;                
                txtReferencia2.Visible = true;
                txtReferencia.Visible = false;
                LabelTerritorio.Visible = true;
                RadComboBoxTerritorio.Visible = true;
            }
            else
            {
                txtReferencia2.Visible = false;
                txtReferencia.Visible = true;
                LabelTerritorio.Visible = false;
                RadComboBoxTerritorio.Visible = false;
            }
            switch (afecta)
            {
                case 0:
                    RequiredFieldValidator4.Enabled = true;
                    txtClienteNombre.Text = "";
                    txtClienteId.Text = "";
                    txtClienteId.Enabled = true;
                    cmbProveedor.Enabled = false;
                    RequiredFieldValidator6.Enabled = false;
                    cmbProveedor.SelectedIndex = -1;
                    txtProveedorId.Enabled = false;
                    txtProveedorId.Text = string.Empty;
                    if (cmbTipoMovimento.SelectedValue == "-1")
                        LimpiarClienteProveedor();
                    break;
                case 1:
                    RequiredFieldValidator4.Enabled = false;
                    txtClienteNombre.Text = "";
                    txtClienteId.Enabled = false;
                    txtClienteId.Text = string.Empty;
                    cmbProveedor.Enabled = true;
                    RequiredFieldValidator6.Enabled = true;
                    txtProveedorId.Enabled = true;
                    break;
                default:
                    Alerta("Ocurrio un problema al validar el movimiento");
                    break;
            }
            switch (cmbTipoMovimento.SelectedValue)
            {
                case "6":
                case "15":
                case "16": //6,15,16 son el grupo 1
                    //mostrar columnas
                    grupoMovimientosActivo = 1;
                    mostrarColumnas(true);
                    break;
                case "14":
                    grupoMovimientosActivo = 2;
                    mostrarColumnas(true);
                    break;
                case "7":
                case "11":
                case "12":
                case "13":
                case "18":
                    grupoMovimientosActivo = 3;
                    mostrarColumnas(true);
                    break;
                case "2":
                case "4":
                    /*muestra columna afecta orden de compra*/
                    grupoMovimientosActivo = 4;
                    mostrarColumnas(false);
                    break;
                default:
                    grupoMovimientosActivo = 0;
                    mostrarColumnas(false);
                    break;
            }
            /*verificar tabla invtipmov(CATtMOVIMIENTO) campo tmiordcomp (AFCT_ORDCOMPRA)*/
            bool afectaOrdCom = new bool();
            cn_catMovimientos.ConsultaTmovimientoAfectaOrdCom(session, int.Parse(cmbTipoMovimento.SelectedValue), ref afectaOrdCom);
            mostrarColumnas2(afectaOrdCom);
        }
        private void atraparMsgBaseDatos(Exception exception, string metodoAtrapaExcepcion)
        {
            string[] Msgs =
                {  //spCapRemisionDet_Insertar
                    "No puede asigna una cantidad 0"
                    ,"La remision no cuenta con el saldo suficiente"
                    ,"La cantidad exede lo que se pide en las ordenes de compra"
                    ,"inventario disponible insuficiente, inventario final"
                    ,"La remision no cuenta con el saldo suficiente"
                    ,"Cantidad excede el tránsito"
                    ,"La cantidad excede lo que se pide en las órdenes de compra"
                    ,"El saldo de"
                };

            bool msgConosido = false;
            foreach (string men in Msgs)
            {
                if (exception.Message.Contains(men))
                    msgConosido = true;
            }

            if (msgConosido)
                Alerta(exception.Message);
            else
                ErrorManager(exception, metodoAtrapaExcepcion);
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
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
                RadAjaxManager1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
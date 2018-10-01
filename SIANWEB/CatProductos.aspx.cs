using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Configuration;
using System.IO;

using Telerik.Web.UI;

using CapaEntidad;
using CapaNegocios;
using System.Runtime.Serialization.Formatters.Binary;



using System.Data;
using System.Data.SqlClient;
using System.Xml;


namespace SIANWEB
{
    public partial class CatProductos : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private List<ProductoPrecios> listSource
        {
            get { return (List<ProductoPrecios>)Session["listSource"]; }
            set { Session["listSource"] = value; }
        }

        public int IdProducto
        {
            get { return Convert.ToInt32(Session["_IdProducto"]); }
            set { Session["_IdProducto"] = value; }
        }

        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }


        #endregion
        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                CargarProductos();
                //this.LlenarComboProductosLista(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["_IdProducto"] = 0;
                        Session["listSource"] = null;
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                            return;
                        }
                        TextId_Prd.Text = this.Valor;
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (!((CheckBox)sender).Checked && hiddenId.Value != "")
                {
                    if (!Deshabilitar())
                    {
                        this.DisplayMensajeAlerta("El registro está siendo utilizado por otro componente");
                        ((CheckBox)sender).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                CargarProductos();
                //preparar controles
                this.NuevoProducto();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbFam_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.LlenarComboProductoSubFamilia(Convert.ToInt32(e.Value));
                txtSubFam.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void grdPrecios_PreRender(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                foreach (GridDataItem item in rgPrecios.MasterTableView.Items)
                {
                    if (Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_Actual"]))
                    {   //si es precio actual, se colorea de azul el Row                    
                        foreach (TableCell cell in item.Cells)
                        {
                            cell.CssClass = "styleCellRowAGridPrecios";
                        }
                    }
                    else //Se quita la capacidad de edición del precio                   
                        item["EditCommandColumn"].Controls[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdPrecios_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                ProductoPrecios productoPrecio = new ProductoPrecios();
                productoPrecio.Id_Emp = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Emp"]);
                productoPrecio.Id_Cd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Cd"]);
                productoPrecio.Id_Prd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Prd"]);
                productoPrecio.Id_Pre = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Pre"]);

                productoPrecio.Prd_Actual = Convert.ToBoolean(((Literal)editedItem["Prd_Actual"].Controls[1]).Text);
                productoPrecio.Prd_FechaInicio = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicio") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_FechaFin = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFin") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_PreDescripcion = (editedItem["Prd_PreDescripcion"].FindControl("txtPrd_PreDescripcion") as RadTextBox).Text.Trim();
                productoPrecio.Pre_Descripcion = (editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEdit") as Label).Text.Trim();
                productoPrecio.Prd_Pesos = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_Pesos") as RadNumericTextBox).Text);

                //Checar que es un rango de fechas correcto para SQL Server
                if (Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) < 0 || Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) < 0)
                    throw new Exception("rgPrecios_FechasRango_incorrecto");

                List<ProductoPrecios> listaProdPre = new List<ProductoPrecios>();
                for (int i = 0; i < this.listSource.Count; i++)
                    listaProdPre.Add((ProductoPrecios)this.ClonarPrecioProducto(this.listSource[i]));

                for (int i = 0; i < this.listSource.Count / 2; i++)
                {
                    listaProdPre[i].Prd_FechaInicio = null;
                    listaProdPre[i].Prd_FechaFin = null;
                }
                //this.ValidarPeriodosPrecioProducto(ref productoPrecio, listaProdPre);

                //Agregar precio a la lista actual
                foreach (ProductoPrecios p in this.listSource)
                {
                    if (p.Id_Pre == productoPrecio.Id_Pre && p.Prd_Actual == productoPrecio.Prd_Actual && p.Prd_Actual == true)
                    {
                        List<ProductoPrecios> listaPP = new List<ProductoPrecios>(this.listSource);
                        int posicionFila = rgPrecios.CurrentPageIndex * rgPrecios.PageSize + e.Item.ItemIndex;
                        listaPP[posicionFila - listaPP.Count / 2] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                        listaPP[posicionFila - listaPP.Count / 2].Prd_Actual = false;
                        listaPP[posicionFila] = (ProductoPrecios)this.ClonarPrecioProducto(productoPrecio);
                        //if (productoPrecio.Prd_Actual)
                        //    this.ValidarPeriodosPrecioProducto(ref productoPrecio, listSource);
                        this.listSource = listaPP;
                        break;
                    }
                }
                rgPrecios.Rebind();
            }
            catch (Exception ex)
            {  //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdPrecios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgPrecios.DataSource = this.listSource;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdPrecios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editItem = (GridEditFormItem)e.Item;

                    string datePickerFechaInicio = ((RadDatePicker)editItem.FindControl("datePickerFechaInicio")).ClientID.ToString();
                    string datePickerFechaFin = ((RadDatePicker)editItem.FindControl("datePickerFechaFin")).ClientID.ToString();
                    string txtPrd_Pesos = ((RadNumericTextBox)editItem.FindControl("txtPrd_Pesos")).ClientID.ToString();

                    string jsControles = string.Concat(
                        "datePickerFechaInicioClientId='", datePickerFechaInicio, "';"
                        , "datePickerFechaFinClientId='", datePickerFechaFin, "';"
                        , "txtPrd_PesosClientId='", txtPrd_Pesos, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormGridPrecioProductos(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormGridPrecioProductos(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                    }
                }
            }
            catch (Exception ex)
            {   //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdPrecios_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgPrecios.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        this.cmbProductosListaArreglaItem0();
                        this.LimpiarCampos();
                        TextId_Prd.Text = this.Valor;
                        this.NuevoProducto();
                        break;
                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductosLista_DataBound(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductosLista_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                ErrorManager();
                //if (e.Item.Value == "-1")
                //{
                //    Label lblAuxiliar = new Label();
                //    lblAuxiliar.Width = new Unit(80, UnitType.Pixel);
                //    e.Item.FindControl("liClave").Controls.Clear();
                //    e.Item.FindControl("liComprasLocales").Controls.Clear();
                //    e.Item.FindControl("liActivo").Controls.Clear();
                //    e.Item.FindControl("liDescripcion").Controls.AddAt(0, lblAuxiliar);
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductosLista_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                ErrorManager();
                CargarProductos();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductosLista_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                if (e.Value != string.Empty && e.Value != "-1")
                {
                    RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);
                    int id_Cd_Prod = session.Id_Cd_Ver;

                    this.LlenarFormularioProducto(Convert.ToInt32(e.Value), id_Cd_Prod);
                    this.hiddenId.Value = e.Value;
                    TextId_Prd.Enabled = false;
                }
                if (e.Value == string.Empty || e.Value == "-1")
                {
                    this.LimpiarCampos();
                    cmbProductosLista.Text = "-- Seleccionar --";
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void cmbProductosListaArreglaItem0()
        {
            Label lblAuxiliar2 = new Label();
            lblAuxiliar2.Width = new Unit(85, UnitType.Pixel);
            Label lblAuxiliar = new Label();
            lblAuxiliar.Text = "-- Seleccionar --";

            if (cmbProductosLista.Items.Count > 0)
            {
                cmbProductosLista.Items[0].FindControl("liActivo").Controls.Clear();
                cmbProductosLista.Items[0].FindControl("liComprasLocales").Controls.Clear();
                cmbProductosLista.Items[0].FindControl("liDescripcion").Controls.Clear();
                cmbProductosLista.Items[0].FindControl("liDescripcion").Controls.AddAt(0, lblAuxiliar);
                cmbProductosLista.Items[0].FindControl("liDescripcion").Controls.AddAt(0, lblAuxiliar2);
            }
        }
        #endregion
        #region Funciones
        public object ClonarPrecioProducto(object obj)
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
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarUnidades();
            this.CargarProductosServicios();


            this.CargarCentros();

            this.CargarProductos();

            this.LlenarComboProductoTipo();
            this.LlenarComboRentabilidad();
            this.LlenarComboSisPropietario();
            this.LlenarComboProductoCategoria();
            this.LlenarComboProductoFamilia();
            this.LlenarComboProveedores();
            this.LlenarComboUnidades(this.cmbUentrada);
            this.LlenarComboUnidades(this.cmbUsalida);

            this.hiddenId.Value = string.Empty;

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            List<SubFamProducto> listaSF = new List<SubFamProducto>();
            SubFamProducto subFamilia = new SubFamProducto();
            new CN_CatSubFamProducto().ConsultaSubFamProducto(subFamilia, sesion.Emp_Cnx, sesion.Id_Emp, ref listaSF);
            str.Append(string.Concat("arregloSubFamilias = new Array(3);"));
            str.Append(string.Concat("arregloSubFamilias[0] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamilias[1] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamilias[2] = new Array(", listaSF.Count, ");"));
            for (int i = 0; i < listaSF.Count; i++)
            {
                subFamilia = listaSF[i];
                str.Append(string.Concat("arregloSubFamilias[0][", i.ToString(), "] = '", subFamilia.Id_Fam, "';"));
                str.Append(string.Concat("arregloSubFamilias[1][", i.ToString(), "] = '", subFamilia.Id_Sub, "';"));
                str.Append(string.Concat("arregloSubFamilias[2][", i.ToString(), "] = '", subFamilia.Sub_Descripcion, "';"));
            }
            this.listSource = new List<ProductoPrecios>();

            this.NuevoProducto();
            if (Session["IdLocal" + Session.SessionID] != null)
            {
                this.TextId_Prd.Text = Session["IdLocal" + Session.SessionID].ToString();
                TextId_Prd.Enabled = false;
                chkComprasLocales.Checked = true;
                Session["IdLocal" + Session.SessionID] = null;

                if (Session["IdCategoria" + Session.SessionID] != null)
                {
                    int? index = cmbCategoria.FindItemIndexByValue(Session["IdCategoria" + Session.SessionID].ToString());
                    cmbCategoria.SelectedIndex = index != null ? (int)index : 0;
                    cmbCategoria.Text = cmbCategoria.Items[cmbCategoria.SelectedIndex].Text;
                    if (cmbCategoria.SelectedIndex > 0)
                    {
                        txtCategoria.Text = Session["IdCategoria" + Session.SessionID].ToString();
                    }
                }

                // Como es alta de producto de compra local
                // se habilitan controles de la pestaña de compras locales
                this.HabilitaCamposComprasLocales(true);
            }
            else
            { // Como es alta de producto normal
                // se deshabilitan controles de la pestaña de compras locales (solo se habilitan cuando se generó previamente un código para producto local)
                this.HabilitaCamposComprasLocales(false);
            }
            lblTituloProducto.Text = string.Concat(TextId_Prd.Text, " - ", TextPrd_Descrpcion.Text);

            RAM1.ResponseScripts.Add(string.Concat(@"", str.ToString()));
        }
        private void ValidarPeriodosPrecioProducto(ref ProductoPrecios productoPrecio, List<ProductoPrecios> listaPordPre)
        {
            try
            {
                //Checar que el rango de fechas del periodo actual no se empalma con el anterior o viceversa
                foreach (ProductoPrecios p in listaPordPre)
                {
                    if (p.Id_Pre == productoPrecio.Id_Pre && !p.Prd_Actual)
                    {
                        //Checar en este momento que los rangos no se empalmen

                        //if (Convert.ToDateTime(p.Prd_FechaInicio).CompareTo(productoPrecio.Prd_FechaInicio) >= 0 && Convert.ToDateTime(p.Prd_FechaInicio).CompareTo(productoPrecio.Prd_FechaFin) <= 0)
                        //    throw new Exception("rgPrecios_FechasRango_empalmado");

                        //if (Convert.ToDateTime(p.Prd_FechaFin).CompareTo(productoPrecio.Prd_FechaInicio) >= 0 && Convert.ToDateTime(p.Prd_FechaFin).CompareTo(productoPrecio.Prd_FechaFin) <= 0)
                        //    throw new Exception("rgPrecios_FechasRango_empalmado");

                        //if (Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(p.Prd_FechaInicio) >= 0 && Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(p.Prd_FechaFin) <= 0)
                        //    throw new Exception("rgPrecios_FechasRango_empalmado");

                        //if (Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(p.Prd_FechaInicio) >= 0 && Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(p.Prd_FechaFin) <= 0)
                        //    throw new Exception("rgPrecios_FechasRango_empalmado");

                        //Checar que no haya Dias entre periodos (el dia inicial del periodo nuevo debe ser el siguiente dia despues del dia final del periodo anterior)
                        //NOTA: el precio ke se esta agregando siempre es el actual, los precios anteriores no tienen la opción de Edición
                        if (p.Prd_FechaInicio != null && p.Prd_FechaFin != null)
                        {

                            //    // Difference in days, hours, and minutes.
                            //    TimeSpan ts = Convert.ToDateTime(productoPrecio.Prd_FechaInicio) - Convert.ToDateTime(p.Prd_FechaFin);

                            //    if (ts.Days > 1)
                            //        throw new Exception("rgPrecios_FechasRango_DiasEntrePeriodo");

                            //    if (ts.Days < 0)
                            //        throw new Exception("rgPrecios_FechasRango_PeridoNuevoAnterior");
                        }
                        break;
                    }
                }
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
                return CN_Comun.Maximo(sesion.Id_Emp, "CatProducto", "Id_Prd", sesion.Emp_Cnx, "spCatCentral_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (hiddenId.Value != "") //Hidden Field BANDERA
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = sesion.Id_Emp; //Si no ocupa empresa su catalogo se pone -1
                    ct.Id_Cd = sesion.Id_Cd_Ver; //Si no ocupa Centro de distribución se pone -1
                    ct.Id = Convert.ToInt32(hiddenId.Value); //Si no ocupa Id se pone -1
                    ct.Tabla = "CatProducto"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Prd"; //Nombre de la columna del ID del catalogo
                    CN_Comun.Deshabilitar(ct, sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HabilitaCamposComprasLocales(bool habilitar)
        {
            txtFnombre.Enabled = habilitar;
            txtFcodigo.Enabled = habilitar;
            txtFdescripcion.Enabled = habilitar;
            txtFpresentacion.Enabled = habilitar;
            txtPnombre.Enabled = habilitar;
            txtPcodigo.Enabled = habilitar;
            txtPdescripcion.Enabled = habilitar;
            txtPpresentacion.Enabled = habilitar;
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                        this.RadToolBar1.Items[6].Visible = false;

                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.RadToolBar1.Items[5].Visible = false;

                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void NuevoProducto()
        {   //rgPrecios.
            this.listSource = this.ConsultarPorductoPrecios(0);
            rgPrecios.DataSource = this.listSource;
            rgPrecios.DataBind();

            txtSubFam.Text = string.Empty;
            cmbSubFam.Text = "";
            cmbSubFam.ClearSelection();
            cmbSubFam.Items.Clear();
            this.hiddenId.Value = string.Empty;

            //Nuevo producto:
            //deshabilta controles de pestaña de compras locales
            this.HabilitaCamposComprasLocales(false);
            chkComprasLocales.Checked = false;

            TextId_Prd.Enabled = true;
            TextId_Prd.Focus();
        }

        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto clsCatProducto = new CN_CatProducto();
                int verificador = -1;
                Producto producto = this.LlenatObjetoProducto();

                foreach (GridDataItem dataItem in rgPrecios.MasterTableView.Items)
                {
                    if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 1 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                    {
                        if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                        {
                            Alerta("El precio AAA actual no puede ser 0");
                            return;
                        }
                    }
                }

                lblTituloProducto.Text = string.Concat(producto.Id_Prd.ToString(), " - ", producto.Prd_Descripcion);

                if (hiddenId.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        DisplayMensajeAlerta("PermisoGuardarNo");
                        return;
                    }

                    clsCatProducto.InsertarProducto(producto, session.Emp_Cnx, ref verificador);
                    this.LimpiarCampos();
                    this.IdProducto = producto.Id_Prd;
                    TextId_Prd.Text = this.Valor;
                    this.DisplayMensajeAlerta("CatProducto_insert_ok");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    clsCatProducto.ModificarProducto(producto, session.Emp_Cnx, ref verificador);
                    TextId_Prd.Enabled = false;
                    this.LimpiarCampos();
                    cmbProductosLista.Text = "";
                    this.DisplayMensajeAlerta("CatProducto_update_ok");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            lblTituloProducto.Text = string.Empty;
            cmbProductosLista.ClearSelection();

            TextId_Prd.Text = string.Empty;
            txtPorUtilidades.Text = string.Empty;
            chkActivo.Checked = true;
            chkProductoNuevo.Checked = false;
            txtCodProd.Text = string.Empty;
            TextPrd_Descrpcion.Text = string.Empty;
            txtPresentacion.Text = string.Empty;
            txtTipoProducto.Text = string.Empty;
            cmbTipoProducto.SelectedIndex = cmbTipoProducto.FindItemIndexByValue("-1");
            TextId_Spo.Text = string.Empty;
            cmbSisProp.SelectedIndex = cmbSisProp.FindItemIndexByValue("-1");
            txtAgrupadoSpo.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            cmbCategoria.SelectedIndex = cmbCategoria.FindItemIndexByValue("-1");

            txtSubFam.Text = string.Empty;
            cmbSubFam.SelectedIndex = cmbSubFam.FindItemIndexByValue("-1");
            txtProveedor.Text = string.Empty;
            cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue("-1");
            cmbUentrada.SelectedIndex = cmbUentrada.FindItemIndexByValue("-1");
            txtFactorConversion.Text = string.Empty;
            cmbUsalida.SelectedIndex = cmbUsalida.FindItemIndexByValue("-1");
            txtUempaque.Text = string.Empty;

            txtFam.Text = string.Empty;
            cmbFam.SelectedIndex = cmbFam.FindItemIndexByValue("-1");

            txtInvSeguridad.Text = string.Empty;
            chkSistProp.Checked = false;
            txtTentrega.Text = string.Empty;
            txtTtransporte.Text = string.Empty;
            txtRentabilidad.Text = string.Empty;
            cmbRentabilidad.SelectedIndex = 0;
            chkComprasLocales.Checked = false;
            txtAmortizacion.Text = string.Empty;
            txtPesos.Text = string.Empty;
            txtExistencia.Text = string.Empty;
            txtUbicacion.Text = string.Empty;
            txtContribucion.Text = string.Empty;

            txtAsignado.Text = string.Empty;
            txtInicial.Text = string.Empty;
            txtOrdenado.Text = string.Empty;
            txtFinal.Text = string.Empty;
            txtTransito.Text = string.Empty;
            txtFisico.Text = string.Empty;
            txtPlanAbasto.Text = string.Empty;
            txtMinCompra.Text = string.Empty;


            this.listSource = this.ConsultarPorductoPrecios(0);
            rgPrecios.Rebind();

            txtFnombre.Text = string.Empty;
            txtFcodigo.Text = string.Empty;
            txtFdescripcion.Text = string.Empty;
            txtFpresentacion.Text = string.Empty;
            txtPnombre.Text = string.Empty;
            txtPcodigo.Text = string.Empty;
            txtPdescripcion.Text = string.Empty;
            txtPpresentacion.Text = string.Empty;


        }

        private void LlenarFormularioProducto(int id_Producto, int id_Cd_Prod)
        {
            try
            {
                Producto producto = ConsultarPorducto(id_Producto, id_Cd_Prod);
                TextId_Prd.Text = producto.Id_Prd.ToString();
                txtCodProd.Text = producto.Prd_Unico == 0 ? string.Empty : producto.Prd_Unico.ToString();
                chkActivo.Checked = producto.Prd_Activo;
                chkProductoNuevo.Checked = producto.Prd_Nuevo;
                TextPrd_Descrpcion.Text = producto.Prd_Descripcion;
                lblTituloProducto.Text = string.Concat(TextId_Prd.Text, " - ", TextPrd_Descrpcion.Text);
                txtPresentacion.Text = producto.Prd_Presentacion;

                TextId_Spo.Text = string.Empty;
                cmbSisProp.Text = "";
                cmbSisProp.ClearSelection();
                if (producto.Id_Spo != 0)
                {
                    TextId_Spo.Text = producto.Id_Spo.ToString();
                    cmbSisProp.SelectedValue = producto.Id_Spo.ToString();
                }

                txtTipoProducto.Text = producto.Id_Ptp.ToString();
                cmbTipoProducto.SelectedValue = producto.Id_Ptp.ToString();
                txtCategoria.Text = string.Empty;
                cmbCategoria.Text = "";
                cmbCategoria.ClearSelection();
                if (producto.Id_Cpr != 0)
                {
                    txtCategoria.Text = producto.Id_Cpr.ToString();
                    cmbCategoria.SelectedValue = producto.Id_Cpr.ToString();
                }
                txtFam.Text = string.Empty;
                cmbFam.Text = "";
                cmbFam.ClearSelection();
                if (producto.Id_Fam != 0)
                {
                    txtFam.Text = producto.Id_Fam.ToString();
                    cmbFam.SelectedValue = producto.Id_Fam.ToString();
                }
                txtSubFam.Text = string.Empty;
                cmbSubFam.Text = "";
                cmbSubFam.ClearSelection();
                cmbSubFam.Items.Clear();
                if (producto.Id_Sub != 0)
                {
                    this.LlenarComboProductoSubFamilia(producto.Id_Fam);
                    txtSubFam.Text = producto.Id_Sub.ToString();
                    cmbSubFam.SelectedValue = producto.Id_Sub.ToString();
                }
                txtProveedor.Text = producto.Id_Pvd.ToString();
                cmbProveedor.SelectedValue = producto.Id_Pvd.ToString();
                cmbUentrada.SelectedValue = producto.Prd_UniNe;
                txtFactorConversion.Text = producto.Prd_FactorConv.ToString();
                cmbUsalida.SelectedValue = producto.Prd_UniNs;
                txtUempaque.Text = producto.Prd_UniEmp.ToString();

                txtAgrupadoSpo.Text = producto.Prd_AgrupadoSpo == 0 ? string.Empty : producto.Prd_AgrupadoSpo.ToString();
                txtContribucion.Text = producto.Prd_Contribucion.ToString();
                txtPorUtilidades.Text = producto.Prd_PorUtilidades.ToString();

                txtInvSeguridad.Text = producto.Prd_InvSeg.ToString();
                chkSistProp.Checked = (bool)producto.Prd_AparatoSisProp;
                txtTentrega.Text = producto.Prd_TEntre.ToString();
                txtTtransporte.Text = producto.Prd_TTrans.ToString();
                cmbRentabilidad.SelectedIndex = cmbRentabilidad.FindItemIndexByText(producto.Prd_Ren.ToString(), true);
                txtRentabilidad.Text = cmbRentabilidad.SelectedValue;
                chkComprasLocales.Checked = producto.Prd_Colo;
                txtAmortizacion.Text = producto.Prd_Mes.ToString();
                txtPesos.Text = producto.Prd_PesConTecnico.ToString();
                txtExistencia.Text = producto.Prd_MaxExistencia.ToString();
                txtUbicacion.Text = producto.Prd_Ubicacion;

                txtAsignado.Text = producto.Prd_Asignado.ToString();
                txtInicial.Text = producto.Prd_InvInicial.ToString();
                txtOrdenado.Text = producto.Prd_Ordenado.ToString();
                txtFinal.Text = producto.Prd_InvFinal.ToString();
                txtTransito.Text = producto.Prd_Transito.ToString();
                txtFisico.Text = producto.Prd_Fisico.ToString();
                txtMinCompra.Text = producto.Prd_Minimo.ToString();
                txtPlanAbasto.Text = producto.Prd_PlanAbasto;

                this.listSource = this.ConsultarPorductoPrecios(id_Producto);

                txtFnombre.Text = producto.Prd_CLNomFab;
                txtFcodigo.Text = producto.Prd_CLIdFab;
                txtFdescripcion.Text = producto.Prd_CLDesFab;
                txtFpresentacion.Text = producto.Prd_CLPreFab;
                txtPnombre.Text = producto.Prd_CLNomPro;
                txtPcodigo.Text = producto.Prd_CLIdPro;
                txtPdescripcion.Text = producto.Prd_CLDesPro;
                txtPpresentacion.Text = producto.Prd_CLPrePro;




                //**********************************//
                //* Consultar precios de productos *//
                //**********************************//
                this.IdProducto = id_Producto;
                rgPrecios.Enabled = true;
                rgPrecios.Rebind();
                // Si es consulta de producto de compra local
                // se habilitan controles de la pestaña de compras locales
                if (producto.Prd_Colo)
                    this.HabilitaCamposComprasLocales(true);
                else
                    this.HabilitaCamposComprasLocales(false);

                if (producto.Prd_SGCUP == "S")
                {
                    this.RadToolBar1.Items[5].Visible = false;

                }
                else
                {
                    this.RadToolBar1.Items[5].Visible = true;
                }


                try
                {


                    cmbClaveProductoServicio.SelectedIndex = cmbClaveProductoServicio.FindItemIndexByValue(producto.Prd_ClaveProdServ.ToString());
                    cmbClaveProductoServicio.Text = cmbClaveProductoServicio.FindItemByValue(producto.Prd_ClaveProdServ.ToString()).Text;


                    cmbClaveUnidad.SelectedIndex = cmbClaveUnidad.FindItemIndexByValue(producto.Prd_ClaveUnidad.ToString());
                    cmbClaveUnidad.Text = cmbClaveUnidad.FindItemByValue(producto.Prd_ClaveUnidad.ToString()).Text;

                }
                catch
                {
                    cmbClaveProductoServicio.SelectedIndex = cmbClaveProductoServicio.FindItemIndexByValue("01010101");
                    cmbClaveProductoServicio.Text = cmbClaveProductoServicio.FindItemByValue("01010101").Text;


                    cmbClaveUnidad.SelectedIndex = cmbClaveUnidad.FindItemIndexByValue("H87");
                    cmbClaveUnidad.Text = cmbClaveUnidad.FindItemByValue("H87").Text;

                    throw;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Producto LlenatObjetoProducto()
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            Producto producto = new Producto();

            producto.Id_Emp = session.Id_Emp;
            producto.Id_Cd = session.Id_Cd_Ver;
            producto.Id_Prd = Convert.ToInt32(TextId_Prd.Text);
            producto.Id_Spo = TextId_Spo.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Spo.Text);
            producto.Id_Ptp = txtTipoProducto.Text == string.Empty ? 0 : Convert.ToInt32(txtTipoProducto.Text);
            producto.Id_Cpr = txtCategoria.Text == string.Empty ? 0 : Convert.ToInt32(txtCategoria.Text);
            producto.Id_Fam = txtFam.Value.HasValue ? Convert.ToInt32(txtFam.Text) : 0;
            producto.Id_Sub = txtSubFam.Value.HasValue ? Convert.ToInt32(txtSubFam.Text) : 0;
            producto.Id_Pvd = txtProveedor.Text == string.Empty ? 0 : Convert.ToInt32(txtProveedor.Text);
            producto.Prd_Descripcion = TextPrd_Descrpcion.Text;
            producto.Prd_Presentacion = txtPresentacion.Text;
            producto.Prd_InvInicial = txtInicial.Text == string.Empty ? 0 : Convert.ToInt32(txtInicial.Text);
            producto.Prd_InvFinal = txtFinal.Text == string.Empty ? 0 : Convert.ToInt32(txtFinal.Text);
            producto.Prd_AgrupadoSpo = txtAgrupadoSpo.Text == string.Empty ? 0 : Convert.ToInt32(txtAgrupadoSpo.Text);
            producto.Prd_FactorConv = txtFactorConversion.Text == string.Empty ? 0 : Convert.ToSingle(txtFactorConversion.Text);
            producto.Prd_AparatoSisProp = chkSistProp.Checked;
            producto.Prd_Fisico = txtFisico.Text == string.Empty ? 0 : Convert.ToInt32(txtFisico.Text);
            producto.Prd_Ordenado = txtOrdenado.Text == string.Empty ? 0 : Convert.ToInt32(txtOrdenado.Text);
            producto.Prd_Asignado = txtAsignado.Text == string.Empty ? 0 : Convert.ToInt32(txtAsignado.Text);
            producto.Prd_InvSeg = txtInvSeguridad.Text == string.Empty ? 0 : Convert.ToInt32(txtInvSeguridad.Text);
            producto.Prd_TTrans = txtTtransporte.Text == string.Empty ? 0 : Convert.ToInt32(txtTtransporte.Text);
            producto.Prd_TEntre = txtTentrega.Text == string.Empty ? 0 : Convert.ToInt32(txtTentrega.Text);
            producto.Prd_Transito = txtTransito.Text == string.Empty ? 0 : Convert.ToInt32(txtTransito.Text);
            producto.Prd_UniNe = cmbUentrada.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUentrada.SelectedValue;
            producto.Prd_UniNs = cmbUsalida.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUsalida.SelectedValue;
            producto.Prd_Unico = txtCodProd.Text == string.Empty ? 0 : Convert.ToInt32(txtCodProd.Text);
            producto.Prd_UniEmp = txtUempaque.Text == string.Empty ? 0 : Convert.ToSingle(txtUempaque.Text);
            producto.Prd_Colo = chkComprasLocales.Checked;
            producto.Prd_Ren = txtRentabilidad.Text.Length > 0 ? txtRentabilidad.Text[0] : ' ';
            producto.Prd_Mes = txtAmortizacion.Text == string.Empty ? 0 : Convert.ToInt32(txtAmortizacion.Text);
            producto.Prd_CLNomFab = txtFnombre.Text;
            producto.Prd_CLIdFab = txtFcodigo.Text;
            producto.Prd_CLDesFab = txtFdescripcion.Text;
            producto.Prd_CLPreFab = txtFpresentacion.Text;
            producto.Prd_CLNomPro = txtPnombre.Text;
            producto.Prd_CLIdPro = txtPcodigo.Text;
            producto.Prd_CLDesPro = txtPdescripcion.Text;
            producto.Prd_CLPrePro = txtPpresentacion.Text;
            producto.Prd_MaxExistencia = txtExistencia.Text == string.Empty ? 0 : Convert.ToInt32(txtExistencia.Text);
            producto.Prd_Ubicacion = txtUbicacion.Text;
            producto.Prd_Contribucion = txtContribucion.Text == string.Empty ? 0 : Convert.ToSingle(txtContribucion.Text);
            producto.Prd_PorUtilidades = txtPorUtilidades.Text == string.Empty ? 0 : Convert.ToSingle(txtPorUtilidades.Text);
            producto.Prd_Nuevo = chkProductoNuevo.Checked;
            producto.Prd_PesConTecnico = txtPesos.Text == string.Empty ? 0 : Convert.ToDouble(txtPesos.Text);
            producto.Prd_CptSv = string.Empty;
            producto.Prd_Activo = chkActivo.Checked;
            producto.Prd_FecAlta = DateTime.Now;
            producto.Prd_Minimo = txtMinCompra.Text == string.Empty ? 0 : Convert.ToInt32(txtMinCompra.Text);
            producto.Prd_PlanAbasto = txtPlanAbasto.Text;
            producto.Prd_ClaveProdServ = cmbClaveProductoServicio.SelectedValue.ToString().Trim();
            producto.Prd_ClaveUnidad = cmbClaveUnidad.SelectedValue.ToString().Trim();


            producto.ListaProductoPrecios = this.listSource;

            return producto;
        }

        private Producto ConsultarPorducto(int id_Producto, int id_Cd_Prod)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto clsCatProducto = new CN_CatProducto();
                Producto producto = new Producto();
                clsCatProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, id_Producto, true);
                return producto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ProductoPrecios> ConsultarPorductoPrecios(int id_Producto)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<ProductoPrecios> list = new List<ProductoPrecios>();
                ProductoPrecios producto = new ProductoPrecios();
                producto.Id_Emp = sesion.Id_Emp;
                producto.Id_Cd = sesion.Id_Cd_Ver;
                producto.Id_Prd = id_Producto;

                new CN_ProductoPrecios().ConsultaListaProductoPrecios(producto, sesion.Emp_Cnx, ref list);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarProductosServicios()
        {
            cmbClaveProductoServicio.Items.Clear();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");

            Dt.Rows.Add(new object[] { "01010101 - No existe en el catálogo", "01010101" });
            Dt.Rows.Add(new object[] { "10191500 - Pesticidas o repelentes de plagas", "10191500" });
            Dt.Rows.Add(new object[] { "10191700 - Dispositivos para control de plagas", "10191700" });
            Dt.Rows.Add(new object[] { "12161803 - Aerosoles", "12161803" });
            Dt.Rows.Add(new object[] { "14111701 - Pañuelos faciales", "14111701" });
            Dt.Rows.Add(new object[] { "14111702 - Cubiertos de asientos de sanitario", "14111702" });
            Dt.Rows.Add(new object[] { "14111703 - Toallas de papel", "14111703" });
            Dt.Rows.Add(new object[] { "14111704 - Papel higiénico", "14111704" });
            Dt.Rows.Add(new object[] { "14111705 - Servilletas de papel", "14111705" });
            Dt.Rows.Add(new object[] { "15121500 - Preparados lubricantes", "15121500" });
            Dt.Rows.Add(new object[] { "15121514 - Lubricantes espray", "15121514" });
            Dt.Rows.Add(new object[] { "15121800 - Anticorrosivos", "15121800" });
            Dt.Rows.Add(new object[] { "15121807 - Anticongelante", "15121807" });
            Dt.Rows.Add(new object[] { "23181506 - Maquinas de lavado", "23181506" });
            Dt.Rows.Add(new object[] { "24122000 - Botellas", "24122000" });
            Dt.Rows.Add(new object[] { "26111702 - Pilas alcalinas", "26111702" });
            Dt.Rows.Add(new object[] { "27112913 - Lubricador de aceite", "27112913" });
            Dt.Rows.Add(new object[] { "31132100 - Forjas de acero", "31132100" });
            Dt.Rows.Add(new object[] { "31191500 - Abrasivos y medios de abrasivo", "31191500" });
            Dt.Rows.Add(new object[] { "40101601 - Secadores", "40101601" });
            Dt.Rows.Add(new object[] { "40141742 - Atomizadores", "40141742" });
            Dt.Rows.Add(new object[] { "40151505 - Bombas dosificadoras", "40151505" });
            Dt.Rows.Add(new object[] { "40151724 - Partes de repuesto para bombas dosificadoras", "40151724" });
            Dt.Rows.Add(new object[] { "41104210 - Disolventes", "41104210" });
            Dt.Rows.Add(new object[] { "41112200 - Instrumentos de medida de temperatura y calor", "41112200" });
            Dt.Rows.Add(new object[] { "41113035 - Tiras o papeles para pruebas químicas", "41113035" });
            Dt.Rows.Add(new object[] { "42281700 - Soluciones y equipo de limpieza pre- esterilización", "42281700" });
            Dt.Rows.Add(new object[] { "42281711 - Desincrustadores de esterilización", "42281711" });
            Dt.Rows.Add(new object[] { "44121626 - Removedor de adhesivo", "44121626" });
            Dt.Rows.Add(new object[] { "46181500 - Ropa de seguridad", "46181500" });
            Dt.Rows.Add(new object[] { "46181504 - Guantes de protección", "46181504" });
            Dt.Rows.Add(new object[] { "46182400 - Equipo de limpieza de seguridad y materiales de descontaminación", "46182400" });
            Dt.Rows.Add(new object[] { "47101530 - Equipo de control de olores", "47101530" });
            Dt.Rows.Add(new object[] { "47101600 - Consumibles para el tratamiento de agua", "47101600" });
            Dt.Rows.Add(new object[] { "47101606 - Químicos de control de corrosión", "47101606" });
            Dt.Rows.Add(new object[] { "47101607 - Químicos de control de olor", "47101607" });
            Dt.Rows.Add(new object[] { "47121500 - Carritos y accesorios para limpieza", "47121500" });
            Dt.Rows.Add(new object[] { "47121600 - Máquinas y accesorios para pisos", "47121600" });
            Dt.Rows.Add(new object[] { "47121608 - Almohadillas de máquinas de piso", "47121608" });
            Dt.Rows.Add(new object[] { "47121612 - Barredoras para pisos", "47121612" });
            Dt.Rows.Add(new object[] { "47121613 - Accesorios para brilladoras de pisos", "47121613" });
            Dt.Rows.Add(new object[] { "47121700 - Envases y accesorios para residuos", "47121700" });
            Dt.Rows.Add(new object[] { "47121701 - Bolsas de basura", "47121701" });
            Dt.Rows.Add(new object[] { "47121800 - Equipo de limpieza", "47121800" });
            Dt.Rows.Add(new object[] { "47121804 - Baldes para limpieza", "47121804" });
            Dt.Rows.Add(new object[] { "47131500 - Trapos y paños de limpieza", "47131500" });
            Dt.Rows.Add(new object[] { "47131502 - Pañitos o toallas para limpiar", "47131502" });
            Dt.Rows.Add(new object[] { "47131600 - Escobas, traperos, cepillos y accesorios", "47131600" });
            Dt.Rows.Add(new object[] { "47131600 - ESCOBAS,TRAPEROS,CEPILLOS Y ACCESORIOS", "47131600" });
            Dt.Rows.Add(new object[] { "47131604 - Escobas", "47131604" });
            Dt.Rows.Add(new object[] { "47131605 - Cepillos de limpieza", "47131605" });
            Dt.Rows.Add(new object[] { "47131611 - Recogedor de basura", "47131611" });
            Dt.Rows.Add(new object[] { "47131617 - Traperos para polvo", "47131617" });
            Dt.Rows.Add(new object[] { "47131701 - Dispensadores de toallas de papel", "47131701" });
            Dt.Rows.Add(new object[] { "47131702 - Dispensadores de productos sanitarios", "47131702" });
            Dt.Rows.Add(new object[] { "47131704 - Dispensadores institucionales de jabón o loción", "47131704" });
            Dt.Rows.Add(new object[] { "47131705 - Accesorios para urinales o inodoros", "47131705" });
            Dt.Rows.Add(new object[] { "47131707 - Secadores de manos institucionales", "47131707" });
            Dt.Rows.Add(new object[] { "47131710 - Dispensadores de papel higiénico", "47131710" });
            Dt.Rows.Add(new object[] { "47131800 - Soluciones de limpieza y desinfección", "47131800" });
            Dt.Rows.Add(new object[] { "47131801 - Limpiadores de pisos", "47131801" });
            Dt.Rows.Add(new object[] { "47131802 - Terminados o ceras para pisos", "47131802" });
            Dt.Rows.Add(new object[] { "47131805 - Limpiadores de propósito general", "47131805" });
            Dt.Rows.Add(new object[] { "47131807 - Blanqueadores", "47131807" });
            Dt.Rows.Add(new object[] { "47131810 - Productos para el lavaplatos", "47131810" });
            Dt.Rows.Add(new object[] { "47131811 - Productos de lavandería", "47131811" });
            Dt.Rows.Add(new object[] { "47131812 - Refrescador de aire", "47131812" });
            Dt.Rows.Add(new object[] { "47131821 - Compuestos desengrasantes", "47131821" });
            Dt.Rows.Add(new object[] { "47131824 - Limpiadores de vidrio o ventanas", "47131824" });
            Dt.Rows.Add(new object[] { "47131826 - Limpiadores de alfombras o tapizados", "47131826" });
            Dt.Rows.Add(new object[] { "47131828 - Limpiadores de automotores", "47131828" });
            Dt.Rows.Add(new object[] { "47131829 - Limpiadores de baños", "47131829" });
            Dt.Rows.Add(new object[] { "47131830 - Limpiadores de muebles", "47131830" });
            Dt.Rows.Add(new object[] { "47131833 - Antisépticos para uso en alimentos", "47131833" });
            Dt.Rows.Add(new object[] { "47131900 - Absorbentes", "47131900" });
            Dt.Rows.Add(new object[] { "47131902 - Absorbentes granulares", "47131902" });
            Dt.Rows.Add(new object[] { "47132100 - Kits de limpieza", "47132100" });
            Dt.Rows.Add(new object[] { "48101600 - Equipos para preparado de alimentos", "48101600" });
            Dt.Rows.Add(new object[] { "48101615 - Lavadoras de platos para uso comercial", "48101615" });
            Dt.Rows.Add(new object[] { "48101618 - Partes de máquinas para lavar platos para uso comercial", "48101618" });
            Dt.Rows.Add(new object[] { "48101711 - Dispensadores de agua embotellada o accesorios", "48101711" });
            Dt.Rows.Add(new object[] { "48101916 - Dispensadores de servilletas para servicio de comidas", "48101916" });
            Dt.Rows.Add(new object[] { "52101505 - Alfombras sintéticas", "52101505" });
            Dt.Rows.Add(new object[] { "52101507 - Tapetes de baño", "52101507" });
            Dt.Rows.Add(new object[] { "52101510 - Tapetes anti fatiga", "52101510" });
            Dt.Rows.Add(new object[] { "52101511 - Tapetes de caucho o vinilo", "52101511" });
            Dt.Rows.Add(new object[] { "52151500 - Utensilios de cocina desechables domésticos", "52151500" });
            Dt.Rows.Add(new object[] { "52152000 - Platos, utensilios para servir y recipientes para almacenar", "52152000" });
            Dt.Rows.Add(new object[] { "53131608 - Jabones", "53131608" });
            Dt.Rows.Add(new object[] { "53131625 - Redecillas para el cabello o la barba", "53131625" });
            Dt.Rows.Add(new object[] { "53131626 - Desinfectante de manos", "53131626" });
            Dt.Rows.Add(new object[] { "53131627 - Limpiador de manos", "53131627" });
            Dt.Rows.Add(new object[] { "55101516 - Manuales operativos o de instrucciones", "55101516" });
            Dt.Rows.Add(new object[] { "55101520 - Hojas o folletos de instrucciones", "55101520" });
            Dt.Rows.Add(new object[] { "55121600 - Etiquetas", "55121600" });
            Dt.Rows.Add(new object[] { "55121704 - Señales de seguridad", "55121704" });
            Dt.Rows.Add(new object[] { "76121501 - Recolección o destrucción o transformación o eliminación de basuras", "76121501" });
            Dt.Rows.Add(new object[] { "80141605 - Mercancía promocional", "80141605" });
            Dt.Rows.Add(new object[] { "82121500 - Impresión", "82121500" });

            cmbClaveProductoServicio.DataSource = Dt;
            cmbClaveProductoServicio.DataValueField = "Id";
            cmbClaveProductoServicio.DataTextField = "Descripcion";
            cmbClaveProductoServicio.DataBind();
        }




        private void CargarUnidades()
        {
            cmbClaveUnidad.Items.Clear();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");



            Dt.Rows.Add(new object[] { "A76 - Galon", "A76" });
            Dt.Rows.Add(new object[] { "E48 - Unidad de servicio", "E48" });
            Dt.Rows.Add(new object[] { "EA - Elemento", "EA" });
            Dt.Rows.Add(new object[] { "H87 - Pieza", "H87" });
            Dt.Rows.Add(new object[] { "KGM - Kilogramo", "KGM" });
            Dt.Rows.Add(new object[] { "LTR - Litro", "LTR" });
            Dt.Rows.Add(new object[] { "MLT - Mililitro", "MLT" });
            Dt.Rows.Add(new object[] { "PR - Par", "PR" });
            Dt.Rows.Add(new object[] { "XBX - Caja", "XBX" });
            Dt.Rows.Add(new object[] { "XKI - Kit (Conjunto de piezas)", "XKI" });
            Dt.Rows.Add(new object[] { "XPK - Paquete", "XPK" });
            Dt.Rows.Add(new object[] { "XRO - Rollo", "XRO" });
            Dt.Rows.Add(new object[] { "XUN - Unidad", "XUN" });

            cmbClaveUnidad.DataSource = Dt;
            cmbClaveUnidad.DataValueField = "Id";
            cmbClaveUnidad.DataTextField = "Descripcion";
            cmbClaveUnidad.DataBind();
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
        private void CargarProductos()//Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(0, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref cmbProductosLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void LlenarComboProductosLista(string filtro)
        //{
        //    try
        //    {
        //        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        Producto producto = new Producto();

        //        List<Producto> listaProducto = new List<Producto>();
        //        new CN_CatProducto().ConsultaListaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, filtro, ref listaProducto, null);

        //        producto = new Producto();
        //        producto.Id_Prd = -1;
        //        producto.Prd_Descripcion = "-- Seleccionar --";
        //        listaProducto.Insert(0, producto);
        //        cmbProductosLista.DataSource = listaProducto;
        //        cmbProductosLista.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void LlenarComboProductoTipo()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatTipoProducto_Combo", ref cmbTipoProducto);
        }

        private void LlenarComboSisPropietario()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatSisPropietarios_Combo", ref cmbSisProp);
        }

        private void LlenarComboRentabilidad()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatRentabilidad_Combo", ref cmbRentabilidad);
            this.cmbRentabilidad.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProductoCategoria()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatProductoCategoria_Combo", ref cmbCategoria);
            this.cmbCategoria.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProductoFamilia()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatFamProducto_Combo", ref cmbFam);
            this.cmbFam.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProductoSubFamilia(int familia)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, familia, sesion.Emp_Cnx, "spCatSubFamProducto_Combo", ref cmbSubFam);
            this.cmbSubFam.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProveedores()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spProveedores_Combo", ref cmbProveedor);
        }

        private void LlenarComboUnidades(RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatUnidad_Combo", ref combo, true);
            combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgPrecios_FechasRango_incorrecto"))
                    Alerta("Favor de capturar un rango de fechas correcto");
                else
                    if (mensaje.Contains("rgPrecios_FechasRango_PeridoNuevoAnterior"))
                        Alerta("El periodo nuevo no debe ser menor al periodo actual");
                    else
                        if (mensaje.Contains("rgPrecios_FechasRango_DiasEntrePeriodo"))
                            Alerta("Rango de fechas no válido. Hay días entre el periodo anterior y el periodo nuevo. La fecha de inicio del periodo actual debe ser el siguiente día después de la fecha final del periodo anterior");
                        else
                            if (mensaje.Contains("rgPrecios_FechasRango_empalmado"))
                                Alerta("Rango de fechas empalmado.");
                            else
                                if (mensaje.Contains("cmbProductosLista_ItemsDataBound"))
                                    Alerta("Error al llenar la lista de productos, combo cmbProductos");
                                else
                                    if (mensaje.Contains("cmbProductosLista_UpdateCount"))
                                        Alerta("No se pudo actualizar el n&uacute;mero de registros de la lista de productos");
                                    else
                                        if (mensaje.Contains("cmbProductosLista_ItemsRequested"))
                                            Alerta("No se pudo actualizar la lista de productos");
                                        else
                                            if (mensaje.Contains("CatProducto_fechaEmpalmada_error"))
                                                Alerta("Los datos del precio de producto no se guardaron.<br/> Rango de fechas empalmado");
                                            else
                                                if (mensaje.Contains("rgPrecios_ItemDataBound"))
                                                    Alerta("Error al colorear los precions actuales en el grid de precios de producto");
                                                else
                                                    if (mensaje.Contains("rgPrecios_NeedDataSource"))
                                                        Alerta("Error al cargar el Grid de tipos de costos");
                                                    else
                                                        if (mensaje.Contains("rgPrecios_ItemCommand"))
                                                            Alerta("Error al ejecutar un evento (rgPrecios_ItemCommand) al cargar el Grid de tipo de costos");
                                                        else
                                                            if (mensaje.Contains("rgPrecios_Actualizar_ok"))
                                                                Alerta("El precio del producto fue actualizado correctamente");
                                                            else
                                                                if (mensaje.Contains("rgPrecios_Actualizar_error"))
                                                                    Alerta("Error al actualizar el precio del producto");
                                                                else
                                                                    if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                                                        Alerta("Error al cambiar de centro de distribución");
                                                                    else
                                                                        if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                            Alerta("Error al cambiar de página");
                                                                        else
                                                                            if (mensaje.Contains("PermisoGuardarNo"))
                                                                                Alerta("No tiene permisos para grabar");
                                                                            else
                                                                                if (mensaje.Contains("CatProductoIdRepetida_error"))
                                                                                    Alerta("La clave ya existe");
                                                                                else
                                                                                    if (mensaje.Contains("CatProductoDescripcionRepetida_error"))
                                                                                        Alerta("La descripción ya existe");
                                                                                    else
                                                                                        if (mensaje.Contains("PermisoModificarNo"))
                                                                                            Alerta("No tiene permisos para actualizar");
                                                                                        else
                                                                                            if (mensaje.Contains("ProductoBuscarNoExiste"))
                                                                                                Alerta(string.Concat("El producto con clave ", TextId_Prd.Text, " no se ha encontrado"));
                                                                                            else
                                                                                                if (mensaje.Contains("CatProducto_insert_ok"))
                                                                                                    Alerta("Los datos se guardaron correctamente");
                                                                                                else
                                                                                                    if (mensaje.Contains("CatProducto_insert_error"))
                                                                                                        Alerta("No se pudo guardar los datos del tipo de precio");
                                                                                                    else
                                                                                                        if (mensaje.Contains("CatProducto_update_ok"))
                                                                                                            Alerta("Los datos se modificaron correctamente");
                                                                                                        else
                                                                                                            if (mensaje.Contains("CatProducto_update_error"))
                                                                                                                Alerta("No se pudo actualizar los datos del tipo de precio");
                                                                                                            else
                                                                                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Telerik.Web.UI;

using CapaEntidad;
using CapaNegocios;
using System.Collections;

using Telerik.Web.UI.GridExcelBuilder;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.OleDb;
using System.Text;
using System.Net;
using System.Net.Mail;
namespace SIANWEB
{
    public partial class ProOrdenCompra_Autorizacion : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private string tempPath = @"~/App_Data/RadUploadTemp";

        private Sesion sesion
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
        public string VentaMes0Desc
        {
            get { return Session["ventaMes0Desc" + Session.SessionID].ToString(); }
            set { Session["ventaMes0Desc" + Session.SessionID] = value; }
        }
        //private string ventaMes1Desc; //COMENTARIZADA POR NO SER UTILIZADA
        public string VentaMes1Desc
        {
            get { return Session["ventaMes1Desc" + Session.SessionID].ToString(); }
            set { Session["ventaMes1Desc" + Session.SessionID] = value; }
        }
        //private string ventaMes2Desc; //COMENTARIZADA POR NO SER UTILIZADA
        public string VentaMes2Desc
        {
            get { return Session["ventaMes2Desc" + Session.SessionID].ToString(); }
            set { Session["ventaMes2Desc" + Session.SessionID] = value; }
        }
        //private string ventaMes3Desc; //COMENTARIZADA POR NO SER UTILIZADA
        public string VentaMes3Desc
        {
            get { return Session["ventaMes3Desc" + Session.SessionID].ToString(); }
            set { Session["ventaMes3Desc" + Session.SessionID] = value; }
        }
        private DataTable listaPartidas
        {
            get { return (DataTable)Session["listaPartidas" + Session.SessionID]; }
            set { Session["listaPartidas" + Session.SessionID] = value; }
        }
        FileUpload f;
        //variables de excel
        bool isConfigured = false; //Configuración de página al exportar a Excel.
        StyleElement priceStyle;
        StyleElement percentStyle;
        StyleElement percentStyleNegative;

        private int _Accion
        {
            get
            {
                return (int)Session["SesionAccion" + Session.SessionID];
            }
            set
            {
                Session["SesionAccion" + Session.SessionID] = value;
            }

        }
        
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
                {
                    CerrarVentana();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);

                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
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

        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        protected void btnBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int flag = 0;
                if (txtId_Ord.Text == "")
                {
                    Alerta("Debes proporcionar una orden de compra valida.");
                    GetList();
                    wrapper.Visible = false;
                    rgOrdenCompra.Rebind();
                    return;
                }
                this.GetList(int.Parse(txtId_Ord.Text), ref flag);
                if (flag == 1)
                    wrapper.Visible = true;    
                rgOrdenCompra.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "btnBuscar_Click"));
            }
        }
        protected void ToolBar_ClientClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "new":
                        break;

                    case "save":
                        accionError = "CapOrdenCompraAuto_insert_error";
                        this.Guardar();
                        break;
                    case "Upordinstalacion":
                        RAM1.ResponseScripts.Add("Upordinstalacion('"+ txtId_Ord.Text +"')");
                        break;
                    case "Downloadordinstalacion":
                        this.Descargar(int.Parse(txtId_Ord.Text));
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, accionError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        private void Descargar(int Id_OrdCompra)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapOrdenCompra Orden = new CN_CapOrdenCompra();
                DataTable dtPartidas = new DataTable();
                Orden.consultaArchivosDesc(sesion, Id_OrdCompra, ref dtPartidas);
                int Count =  dtPartidas.Rows.Count;
                string ruta;
                string ruta2;
                string DocNombre;
                    foreach (DataRow Row in dtPartidas.Rows)
                    {
                        DocNombre = Row["Doc_Nombre"].ToString();
                        ruta = Server.MapPath("App_Data/RadUploadTemp\\") + DocNombre;
                        if (File.Exists(ruta))
                        {
                            ruta2 = Server.MapPath("Download\\") + DocNombre;
                            if (File.Exists(ruta2))
                            {
                                File.Delete(ruta2);
                            }
                            File.Move(ruta, Server.MapPath("Download\\") + DocNombre);
                            Response.Redirect(ruta2, false);
                            return;
                        }
                        else
                            Alerta("Ocurrio un problema al intentar descargar el archivo.");
                    }
                    if (dtPartidas.Rows.Count == 0) 
                    {
                        Alerta("No existen archivos relacionados a esta orden de compra.");
                    }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
           // Response.Close();
        }
        protected void rgOrdenCompra_DeleteCommand(object sender, GridCommandEventArgs e) 
        {
            try {
                GridDataItem itemAde = (GridDataItem)e.Item;
                int Id_PrdFac = Convert.ToInt32(itemAde.OwnerTableView.DataKeyValues[itemAde.ItemIndex]["Id_Prd"]);
                string eliminar = "SI";
                if (rgOrdenCompra.Items.Count > 1)
                {
                    foreach (GridDataItem item2 in rgOrdenCompra.Items)
                    {
                        //int IdProducto = Convert.ToInt32( item["Id_Prod"].Text);
                        int IdProducto = Convert.ToInt32(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Id_Prd"]);
                    }
                }

                if (eliminar == "NO")
                {
                    Alerta("No se Puede Eliminar este Producto, Existen Adendas Capturadas");
                }
                else
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    //int Id_FacDet = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_FacDet"]);
                    int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                    int Id_Ord = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Ord"]);
                    //actualizar producto de orden de compra a la lista
                    this.listaPartidas_EliminarProducto(Id_Prd, Id_Ord);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void listaPartidas_EliminarProducto(int Id_Prd, int Id_Ord)
        {
            try
            {
                DataRow[] Ar_dr;
                Id_Ord = int.Parse(txtId_Ord.Text);
                Ar_dr = listaPartidas.Select("Id_Prd='" + Id_Prd + "'"); //+ "' and Id_Ord='" + Id_Ord + "'");
                if (Ar_dr.Length > 0)
                {
                    if (this.hiddenId.Value != string.Empty)
                    {
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        Ar_dr[0].Delete();
                        listaPartidas.AcceptChanges();
                    }
                    else
                    {
                        Ar_dr[0].Delete();
                        listaPartidas.AcceptChanges();
                    }
                }
                //this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void rgOrdenCompra_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgOrdenCompra.DataSource = listaPartidas;// this.GetList();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgOrdenCompra_NeedDataSource"));
            }
        }
        protected void rgOrdenCompra_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                int Id_Prd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Prd"]);
                double ordenado = Convert.ToDouble((editedItem["ordenado"].FindControl("txtordenado") as RadNumericTextBox).Text);
                double existencia = Convert.ToDouble(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["existencia"]);
                double Prd_MaxExistencia = Convert.ToDouble(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Prd_MaxExistencia"]);
                string unidades_X_empaque = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Prd_UniEmp"].ToString();
                double Prd_UniEmp = unidades_X_empaque == "" ? 1 : Convert.ToDouble(unidades_X_empaque);

                if (Prd_UniEmp == 0)
                {
                    Prd_UniEmp = 1;
                }

                if (ordenado % Prd_UniEmp != 0)
                {
                    while (ordenado % Prd_UniEmp != 0)
                    {
                        ordenado += 1;
                        Alerta("Se ajustó el ordenado a las unidades por empaque");
                    }
                }


                int error = 0;
                //if ((ordenado + existencia) > Prd_MaxExistencia)
                if (ordenado < 0)
                {
                    DisplayMensajeAlerta("rgOrdenCompra_OrdenadoIncorrecto");
                    error = 1;
                }
                //actualizar ordenado de producto
                if (error == 0)
                {
                    if (listaPartidas != null)
                    {
                        DataRow[] ar = listaPartidas.Select("Id_Prd='" + Id_Prd + "'");
                        if (ar.Length > 0)
                        {
                            ar[0].BeginEdit();
                            ar[0]["ordenado"] = ordenado;
                            ar[0].AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgOrdenCompra_Actualizar_error"));
            }
        }
        protected void rgOrdenCompra_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //if (e.Item is GridHeaderItem)
                //{
                //    GridHeaderItem header = (GridHeaderItem)e.Item;
                //    header["ventaMes0"].Text = string.Concat("Venta ", this.VentaMes0Desc);
                //    header["ventaMes1"].Text = string.Concat("Venta ", this.VentaMes1Desc);
                //    header["ventaMes2"].Text = string.Concat("Venta ", this.VentaMes2Desc);
                //    header["ventaMes3"].Text = string.Concat("Venta ", this.VentaMes3Desc);

                //    GridItem cmdItem = rgOrdenCompra.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                //    cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgOrdenCompra_ItemDataBound"));
            }
        }
        
        protected void rgOrdenCompra_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rgOrdenCompra.Rebind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void rgOrdenCompra_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {//para los botones de exportar
                if (e.CommandName.ToString().ToUpper().Contains("EXPORTTO"))
                {
                    rgOrdenCompra.MasterTableView.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
                    if (e.CommandName.ToString().ToUpper().Contains("Excel"))
                        rgOrdenCompra.MasterTableView.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width = Unit.Pixel(200);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        rgOrdenCompra.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rgOrdenCompra_ExcelMLExportStylesCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
        {
            priceStyle = new StyleElement("priceItemStyle");
            priceStyle.NumberFormat.FormatType = NumberFormatType.Currency;
            e.Styles.Add(priceStyle);

            percentStyle = new StyleElement("percentItemStyle");
            percentStyle.NumberFormat.FormatType = NumberFormatType.Percent;
            percentStyle.FontStyle.Italic = true;
            e.Styles.Add(percentStyle);

            percentStyleNegative = new StyleElement("percentItemStyleNegative");
            percentStyleNegative.NumberFormat.FormatType = NumberFormatType.Percent;
            percentStyleNegative.FontStyle.Italic = true;
            percentStyleNegative.FontStyle.Color = System.Drawing.Color.Red;
            e.Styles.Add(percentStyleNegative);

            foreach (StyleElement style in e.Styles)
            {
                if (style.Id == "headerStyle")
                {
                    style.InteriorStyle.Pattern = InteriorPatternType.Solid;
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                }
            }
        }
        protected void rgOrdenCompra_ExcelMLExportRowCreated(object sender, GridExportExcelMLRowCreatedArgs e)
        {
            if (e.RowType == GridExportExcelMLRowType.DataRow)
            {
                if (!isConfigured)
                {
                    foreach (ColumnElement column in e.Worksheet.Table.Columns)
                    {
                        if (e.Worksheet.Table.Columns.IndexOf(column) == 7)
                            column.Attributes["ss:Width"] = "180"; //set width 180 a columna Nombre
                        else
                            column.Attributes["ss:Width"] = "80"; //set width 80 al resto de columnas
                    }

                    //Set Page options
                    PageSetupElement pageSetup = e.Worksheet.WorksheetOptions.PageSetup;
                    pageSetup.PageLayoutElement.IsCenteredVertical = true;
                    pageSetup.PageLayoutElement.IsCenteredHorizontal = true;
                    pageSetup.PageMarginsElement.Left = 0.5;
                    pageSetup.PageMarginsElement.Top = 0.5;
                    pageSetup.PageMarginsElement.Right = 0.5;
                    pageSetup.PageMarginsElement.Bottom = 0.5;
                    pageSetup.PageLayoutElement.PageOrientation = PageOrientationType.Landscape;

                    //Freeze panes
                    e.Worksheet.WorksheetOptions.AllowFreezePanes = true;
                    e.Worksheet.WorksheetOptions.LeftColumnRightPaneNumber = 1;
                    e.Worksheet.WorksheetOptions.TopRowBottomPaneNumber = 1;
                    e.Worksheet.WorksheetOptions.SplitHorizontalOffset = 1;
                    e.Worksheet.WorksheetOptions.SplitVerticalOffest = 1;

                    isConfigured = true;
                }
            }
        }
        #endregion

        #region Funciones
        private bool ValidarSesion()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void Inicializar()
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
            configuracion.Id_Cd = sesion.Id_Cd_Ver;
            configuracion.Id_Emp = sesion.Id_Emp;
            CN_Configuracion cn_configuracion = new CN_Configuracion();
            cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);


            this.CargarCentros();

            Session["ventaMes1Desc"] = string.Empty;
            Session["ventaMes2Desc"] = string.Empty;
            Session["ventaMes3Desc"] = string.Empty;

            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]); ;
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]); ;
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]); ;
            _Accion = Convert.ToInt32(Request.QueryString["Accion"]);


            ValidarPermisos();
            if (_Accion == 0)
            {
                if (sesion.U_Correo != configuracion.Mail_OrdenCompra_sisprop)
                    this.RadToolBar1.Items[6].Visible = false;
                else
                    this.RadToolBar1.Items[6].Visible = true;

                GetList();
                rgOrdenCompra.Rebind();

            }
            else
            {

                int flag = 0;
                if (Request.QueryString["Id"].ToString() != "-1")
                {
                    if (_Accion == 0 || _Accion == 2 || _Accion == 4)
                    {
                        txtId_Ord.Text = Request.QueryString["Id"].ToString();
                        this.GetList(int.Parse(txtId_Ord.Text), ref flag);
                        if (flag == 1)
                            wrapper.Visible = true;
                        rgOrdenCompra.Rebind();
                        return;
                    }
                }
            }
        }

        /*private void Inicializar()
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
            configuracion.Id_Cd = sesion.Id_Cd_Ver;
            configuracion.Id_Emp = sesion.Id_Emp;
            CN_Configuracion cn_configuracion = new CN_Configuracion();
            cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);            
            this.CargarCentros();

            Session["ventaMes1Desc"] = string.Empty;
            Session["ventaMes2Desc"] = string.Empty;
            Session["ventaMes3Desc"] = string.Empty;

            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]); ;
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]); ;
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]); ;
            _Accion = Convert.ToInt32(Request.QueryString["Accion"]);
            
            
            ValidarPermisos();
            if (_Accion == 0)
            {
                this.RadToolBar1.Items[6].Visible = false;
                GetList();
                rgOrdenCompra.Rebind();
                return;
            }else{

                int flag = 0;
                if (Request.QueryString["Id"].ToString() != "-1")
                {
                    if (_Accion == 0 || _Accion == 2 || _Accion == 4)
                    {
                        txtId_Ord.Text = Request.QueryString["Id"].ToString();
                        this.GetList(int.Parse(txtId_Ord.Text), ref flag);
                        if (flag == 1)
                            wrapper.Visible = true;
                        rgOrdenCompra.Rebind();
                    }
                }
            }
         }
        */

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = sesion.Id_Emp;
                ordCompra.Id_Cd = sesion.Id_Cd_Ver;
                ordCompra.Id_Pvd = 100;
                ordCompra.Id_Ord = int.Parse(txtId_Ord.Text);
                ordCompra.Id_U = sesion.Id_U;
                ordCompra.Ord_CorreoU = sesion.U_Correo;
                //cuando se da de alta por el usuario el estatus siempre es M = Manual

                ordCompra.Ord_Estatus = "C";

                CapaDatos.Funciones funciones = new CapaDatos.Funciones();
                DateTime fechaServidor = funciones.GetLocalDateTime(sesion.Minutos);
                ordCompra.Ord_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, fechaServidor.Hour, fechaServidor.Minute, fechaServidor.Second);

                ordCompra.Ord_Tipo = 2; //1 = Manual, 2 = Automática
                ordCompra.Ord_Notas = "Orden generada automáticamente";

                List<OrdenCompraDet> listaPartidaTemp = new List<OrdenCompraDet>();
                OrdenCompraDet ordenCompraDet;
                foreach (DataRow row in this.listaPartidas.Select("ordenado > 0"))
                {
                    ordenCompraDet = new OrdenCompraDet();
                    ordenCompraDet.Id_Emp = sesion.Id_Emp;
                    ordenCompraDet.Id_Cd = sesion.Id_Cd_Ver;
                    ordenCompraDet.Id_Ord = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                    ordenCompraDet.Id_OrdDet = 0; //identity
                    ordenCompraDet.Id_Prd = Convert.ToInt32(row["Id_Prd"]);
                    ordenCompraDet.Ord_Cantidad = Convert.ToInt32(row["ordenado"]);
                    ordenCompraDet.ventaPromedio = Convert.ToInt32(row["ventaPromedio"]);
                    ordenCompraDet.Ord_CantidadGen = 0;
                    listaPartidaTemp.Add(ordenCompraDet);
                }
                // Create the query.
                IEnumerable<OrdenCompraDet> sortedStudents =
                    from Partida in listaPartidaTemp
                    orderby Partida.Id_Prd ascending
                    select Partida;

                List<OrdenCompraDet> listaAutorizado = new List<OrdenCompraDet>();
                foreach (OrdenCompraDet Partida in sortedStudents)
                {
                    //****************************************************************************************************************//
                    // Se valida que lo solicitado no sebre pase el promedio de venta y valida que el producto sea sistema propietario
                    //RMB 24/01/2017
                    //***************************************************************************************************************//
                    listaAutorizado.Add(Partida);
                }
                //Genera la orden de compra solo si la lista trae 1 o mas partidas
                if (listaAutorizado.Count > 0)
                {
                    ordCompra.ListOrdenCompra = listaAutorizado;
                    int verificador = 0;
                    DataTable DtOrdenCompra = null;
                    new CN_CapOrdenCompra().AutorizaOrdenCompra(ref ordCompra, sesion.Emp_Cnx, ref verificador);
                    EnviarOrden(ordCompra.Id_Ord);


                    EnviarCorreoAutorizacion(ordCompra, "alejandra.benavente@key.com.mx", sesion.U_Nombre);
                    Alerta("Se genero la orden de compra #" + ordCompra.Id_Ord.ToString());
                    new CN_CapOrdenCompra().ActualizaNivel2(ordCompra, sesion, ref verificador, ref DtOrdenCompra);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void EnviarOrden(int id_ord)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            OrdenCompra ordCompra = new OrdenCompra();
            try
            {
                string habilitaenviodirecto = ConfigurationManager.AppSettings["OrdenCompraEnvioDirecto"].ToString();
                if (habilitaenviodirecto == "0")
                {

                    List<string> arregloURL = this.OrdenCompra_CrearURL_EnvioInternet(sesion.Id_Emp, sesion.Id_Cd, id_ord);
                    this.OrdenCompra_EnvioPorInternet(sesion.Id_Emp, sesion.Id_Cd, id_ord, arregloURL, "");
                }
                else
                {
                    this.OrdenCompra_CrearEnviaXML(sesion.Id_Emp, sesion.Id_Cd, id_ord, DateTime.Now  , "");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<string> OrdenCompra_CrearURL_EnvioInternet(int Id_Emp, int Id_Cd, int Id_Ord)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Consulta encabezado de la orden de compra
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = Id_Emp;
                ordCompra.Id_Cd = Id_Cd;
                ordCompra.Id_Ord = Id_Ord;
                new CN_CapOrdenCompra().ConsultaOrdenCompra(ref ordCompra, sesion.Emp_Cnx);
                //Consulta detalle de la orden de compra
                List<OrdenCompraDet> listaOrdCompraDet = new List<OrdenCompraDet>();
                OrdenCompraDet ordenCompraDet = new OrdenCompraDet();

                ordenCompraDet.Id_Emp = Id_Emp;
                ordenCompraDet.Id_Cd = Id_Cd;
                ordenCompraDet.Id_Ord = Id_Ord;
                new CN_CapOrdenCompraDet().ConsultaOrdenCompraDetalle_Lista(ordenCompraDet, sesion.Emp_Cnx, ref listaOrdCompraDet);

                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, ordCompra.Id_Cd, ordCompra.Id_Emp, sesion.Emp_Cnx);

                //construir URL
                List<string> arregloURL = new List<string>();
                System.Text.StringBuilder URL = new System.Text.StringBuilder();
                foreach (OrdenCompraDet ordComDet in listaOrdCompraDet)
                {
                    URL.Clear();
                    URL.Append(string.Concat("http://148.244.244.141/oc/SubeDetalleOC.asp?oczonnumsian=", Cd.Id_Cd.ToString()));
                    URL.Append(string.Concat("&ocnumsian=", ordCompra.Id_Ord.ToString()));
                    URL.Append(string.Concat("&ocpronumsian=", ordComDet.Producto.Id_Prd));
                    URL.Append(string.Concat("&ocprodescsian=", ordComDet.Producto.Prd_Descripcion));
                    URL.Append(string.Concat("&ocpropresensian=", ordComDet.Producto.Prd_Presentacion));
                    URL.Append(string.Concat("&ocprounidadsian=", ordComDet.Producto.Prd_UniNe));
                    URL.Append(string.Concat("&ocprocostosian=", ordComDet.ProductoPrecio.Prd_Pesos.ToString()));
                    URL.Append(string.Concat("&ocantidadsian=", ordComDet.Ord_Cantidad.ToString()));
                    URL.Append(string.Concat("&ocimportesian=", (ordComDet.Ord_Cantidad * ordComDet.ProductoPrecio.Prd_Pesos).ToString()));
                    arregloURL.Add(URL.ToString());
                }
                //Crear URL de datos de encabezado
                URL.Clear();
                URL.Append(string.Concat("http://148.244.244.141/oc/SubeFinalSuc.asp?oczonnumsian=", Cd.Id_Cd.ToString()));
                URL.Append(string.Concat("&oczondescsian=", Cd.Cd_Descripcion));
                URL.Append(string.Concat("&ocnumsian=", ordCompra.Id_Ord.ToString()));
                URL.Append(string.Concat("&ocusunumsian=", Cd.Cd_NumMacola != null ? Cd.Cd_NumMacola.ToString() : "0")); //"27868"
                URL.Append(string.Concat("&ocusunomsian=", Cd.Cd_Descripcion));
                URL.Append(string.Concat("&oczoncallesian=", Cd.Cd_Calle));
                URL.Append(string.Concat("&oczoncolsian=", Cd.Cd_Colonia));
                URL.Append(string.Concat("&oczoncd=", Cd.Cd_Ciudad));
                URL.Append(string.Concat("&oczonedo=", Cd.Cd_Estado));
                URL.Append(string.Concat("&oczonlnum=", Cd.Cd_Numero));
                URL.Append(string.Concat("&oczoncpsian=", Cd.Cd_CP));
                URL.Append(string.Concat("&octotalsian=0"));
                URL.Append(string.Concat("&ocinvsian=0"));
                URL.Append(string.Concat("&octransian=0"));
                arregloURL.Add(URL.ToString());

                return arregloURL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OrdenCompra_CrearEnviaXML(int Id_Emp, int Id_Cd, int Id_Ord, DateTime fechaOrden, string Ord_EstatusStr)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Consulta encabezado de la orden de compra
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = Id_Emp;
                ordCompra.Id_Cd = Id_Cd;
                ordCompra.Id_Ord = Id_Ord;
                new CN_CapOrdenCompra().ConsultaOrdenCompra(ref ordCompra, sesion.Emp_Cnx);
                //Consulta detalle de la orden de compra
                List<OrdenCompraDet> listaOrdCompraDet = new List<OrdenCompraDet>();
                OrdenCompraDet ordenCompraDet = new OrdenCompraDet();

                ordenCompraDet.Id_Emp = Id_Emp;
                ordenCompraDet.Id_Cd = Id_Cd;
                ordenCompraDet.Id_Ord = Id_Ord;
                new CN_CapOrdenCompraDet().ConsultaOrdenCompraDetalle_Lista(ordenCompraDet, sesion.Emp_Cnx, ref listaOrdCompraDet);

                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, ordCompra.Id_Cd, ordCompra.Id_Emp, sesion.Emp_Cnx);

                StringBuilder XML_Enviar = new StringBuilder();
                XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>");
                XML_Enviar.Append("<OrdenCompra");
                XML_Enviar.Append(" fecha=\"\"");
                XML_Enviar.Append(" ocnumsian=\"\"");
                XML_Enviar.Append(" oczonnumsian=\"\" >");

                XML_Enviar.Append(" <Encabezado");
                XML_Enviar.Append(" ocnumsian=\"\"");
                XML_Enviar.Append(" oczonnumsian=\"\"");
                XML_Enviar.Append(" octransian=\"\"");
                XML_Enviar.Append(" ocinvsian=\"\"");
                XML_Enviar.Append(" octotalsian=\"\"");
                XML_Enviar.Append(" oczoncpsian=\"\"");
                XML_Enviar.Append(" oczonlnum=\"\"");
                XML_Enviar.Append(" oczonedo=\"\"");
                XML_Enviar.Append(" oczoncd=\"\"");
                XML_Enviar.Append(" oczoncolsian=\"\"");
                XML_Enviar.Append(" oczoncallesian=\"\"");
                XML_Enviar.Append(" ocusunommacola=\"\"");
                XML_Enviar.Append(" ocusunummacola=\"\"");
                XML_Enviar.Append(" ocfechamacola=\"\"");

                XML_Enviar.Append(" ocfechasian=\"\"");
                XML_Enviar.Append(" ocusunomsian=\"\"");
                XML_Enviar.Append(" ocusunumsian=\"\"");
                XML_Enviar.Append(" oczondescsian=\"\"/>");
                XML_Enviar.Append("<Detalle>");

                var importe = 0.0;
                //PARTIDA DETALLE
                if (listaOrdCompraDet.Count() > 0)
                {
                    foreach (OrdenCompraDet ocd in listaOrdCompraDet)
                    {
                        importe = Math.Round(ocd.Ord_Cantidad * ocd.ProductoPrecio.Prd_Pesos, 2);
                        XML_Enviar.Append(" <Partida");
                        XML_Enviar.Append(" ocnumsian=\"" + ocd.Id_Ord + "\"");
                        XML_Enviar.Append(" oczonnumsian=\"" + ocd.Id_Cd + "\"");
                        XML_Enviar.Append(" ocimportesian=\"" + importe + "\"");
                        XML_Enviar.Append(" occantidadsian=\"" + ocd.Ord_Cantidad + "\"");
                        XML_Enviar.Append(" ocprocostosian=\"" + ocd.ProductoPrecio.Prd_Pesos + "\"");
                        XML_Enviar.Append(" ocprounidadsian=\"" + ocd.Producto.Prd_UniNe + "\"");
                        XML_Enviar.Append(" ocpropresensian=\"" + ocd.Producto.Prd_Presentacion + "\"");
                        XML_Enviar.Append(" ocprodescsian=\"" + ocd.Producto.Prd_Descripcion.ToString().Replace("\"", "").Replace("'", "").Replace("&", "")
                            + "\"");
                        XML_Enviar.Append(" ocpronumsian=\"" + ocd.Id_Prd + "\"");
                        XML_Enviar.Append(" />");
                    }
                }



                XML_Enviar.Append(" </Detalle>");
                XML_Enviar.Append(" </OrdenCompra>");

                XmlDocument xml = new XmlDocument();

                xml.LoadXml(XML_Enviar.ToString());

                XmlNode OrdenCompra = xml.SelectSingleNode("OrdenCompra");
                OrdenCompra.Attributes["fecha"].Value = string.Format("{0:s}", ordCompra.Ord_Fecha);
                OrdenCompra.Attributes["ocnumsian"].Value = ordCompra.Id_Ord.ToString();
                OrdenCompra.Attributes["oczonnumsian"].Value = ordCompra.Id_Cd.ToString();


                XmlNode Cabecera = OrdenCompra.SelectSingleNode("Encabezado");

                Cabecera.Attributes["ocnumsian"].Value = ordCompra.Id_Ord.ToString();
                Cabecera.Attributes["oczonnumsian"].Value = Cd.Id_Cd.ToString();
                Cabecera.Attributes["octransian"].Value = Cd.Cd_NumMacola != null ? Cd.Cd_NumMacola.ToString() : Cd.Id_Cd.ToString();
                Cabecera.Attributes["ocinvsian"].Value = ordCompra.Id_Ord.ToString();
                Cabecera.Attributes["octotalsian"].Value = "0";
                Cabecera.Attributes["oczoncpsian"].Value = Cd.Cd_CP.ToString();
                Cabecera.Attributes["oczonlnum"].Value = Cd.Cd_Numero.ToString();
                Cabecera.Attributes["oczonedo"].Value = Cd.Cd_Descripcion;
                Cabecera.Attributes["oczoncd"].Value = Cd.Cd_Descripcion.ToString();
                Cabecera.Attributes["oczoncolsian"].Value = Cd.Cd_Colonia.ToString();
                Cabecera.Attributes["oczoncallesian"].Value = Cd.Cd_Calle.ToString();
                Cabecera.Attributes["ocusunommacola"].Value = "100";
                Cabecera.Attributes["ocusunummacola"].Value = "0";
                Cabecera.Attributes["ocfechamacola"].Value = string.Format("{0:s}", ordCompra.Ord_Fecha); ;
                Cabecera.Attributes["ocfechasian"].Value = string.Format("{0:s}", ordCompra.Ord_Fecha); ;
                Cabecera.Attributes["ocusunomsian"].Value = Cd.Cd_Descripcion.ToString();
                Cabecera.Attributes["ocusunumsian"].Value = ordCompra.Id_Pvd.ToString();
                Cabecera.Attributes["oczondescsian"].Value = Cd.Cd_Descripcion.ToString();



                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xml.WriteTo(tx);
                string xmlString = sw.ToString();

                //throw new Exception(sw.ToString());



                XmlDocument xmlOrdenCompra = new XmlDocument();


                OrdendeCompra.Service1 sianEnvioOrdendeCompra = new OrdendeCompra.Service1();


                object sianEnvioOrdendeCompraResult = sianEnvioOrdendeCompra.OrdenCompra(xmlString);

                // xmlOrdenCompra.LoadXml(sianEnvioOrdendeCompraResult.ToString());


                int verificador = 0;
                ordCompra.Id_Emp = Id_Emp;
                ordCompra.Id_Cd = Id_Cd;
                ordCompra.Id_Ord = Id_Ord;
                ordCompra.Ord_Estatus = "I";
                new CN_CapOrdenCompra().ModificarOrdenCompra_Estatus(ordCompra, sesion.Emp_Cnx, ref verificador);

                try
                {
                    ordCompra.Ord_EstatusEmision = Convert.ToInt32(sianEnvioOrdendeCompraResult);
                }
                catch (FormatException e)
                {
                    ordCompra.Ord_EstatusEmision = 5;
                }


                ordCompra.Ord_EstatusEmisionStr = getEstatusEmision(ordCompra.Ord_EstatusEmision);

                new CN_CapOrdenCompra().ModificarOrdenCompra_EstatusEmision(ordCompra, sesion.Emp_Cnx, ref verificador);

                Alerta(getEstatusEmision(ordCompra.Ord_EstatusEmision));

                if (ordCompra.Ord_EstatusEmision == 1)
                {
                    this.OrdenCompra_Impresion(Id_Emp, Id_Cd, Id_Ord, fechaOrden, Ord_EstatusStr);
                }
                // RAM1.ResponseScripts.Add("Alert('" + sianEnvioOrdendeCompraResult.ToString() + "')");

            }
            catch (Exception ex)
            {
                //this.EnviaEmail(ex.ToString());
                throw ex;
            }
        }

        private void OrdenCompra_Impresion(int Id_Emp, int Id_Cd, int Id_Ord, DateTime fechaOrden, string Ord_EstatusStr)
        {
            try
            {
                if (Ord_EstatusStr.Contains("Baja"))
                    throw new Exception("OrdCompra_estatus_incorrecto");
                else
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    ArrayList ALValorParametrosInternos = new ArrayList();
                    //Consulta centro de distribución
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                    ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                    ALValorParametrosInternos.Add(sesion.Id_Emp);
                    ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                    ALValorParametrosInternos.Add(Id_Ord);
                    ALValorParametrosInternos.Add(Cd.Cd_Descripcion);
                    ALValorParametrosInternos.Add(Cd.Cd_Calle);
                    ALValorParametrosInternos.Add(Cd.Cd_Numero.ToString());
                    ALValorParametrosInternos.Add(Cd.Cd_CP);
                    ALValorParametrosInternos.Add(Cd.Cd_Ciudad);
                    ALValorParametrosInternos.Add(Cd.Cd_Estado);
                    ALValorParametrosInternos.Add(fechaOrden.Day.ToString());
                    ALValorParametrosInternos.Add(fechaOrden.Month.ToString());
                    ALValorParametrosInternos.Add(fechaOrden.Year.ToString());
                    Type instance = null;
                    instance = typeof(LibreriaReportes.OrdenCompraImpresion);
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                    //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getEstatusEmision(int? estatus)
        {
            try
            {

                estatus = (estatus == null) ? -1 : estatus;
                string respuesta = "";

                switch (estatus)
                {
                    case -1:
                        respuesta = "";
                        break;
                    case 0:
                        respuesta = "La orden no se ha enviado";
                        break;
                    case 1:
                        respuesta = "Orden enviada";
                        break;
                    case 2:
                        respuesta = "Orden ya ha sido enviada";
                        break;
                    case 3:
                        respuesta = "Error al recibir Información, Vuelva a intentar enviar ";
                        break;
                    case 4:
                        respuesta = "Pendiente de Autorización ";
                        break;
                    default:
                        respuesta = "No hay Conexión con la planta";
                        break;
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OrdenCompra_EnvioPorInternet(int Id_Emp, int Id_Cd, int Id_Ord, List<string> arregloURL, string Ord_EstatusStr)
        {
            try
            {
                if (Ord_EstatusStr.Contains("Baja"))
                    throw new Exception("OrdCompra_estatus_incorrecto");
                else
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    string jsArregloURL = string.Empty;
                    int verificador = 0;
                    string urlFinal = "";
                    foreach (string url in arregloURL)
                    {
                        jsArregloURL = string.Concat(jsArregloURL, "'", url, "',");

                        if (url.Contains("SubeFinalSuc.asp"))
                        {
                            urlFinal = url;
                        }
                        else
                        {
                            //this.HTTPrequest_ResponseText(url);
                        }
                    }
                    jsArregloURL = jsArregloURL.Substring(0, jsArregloURL.Length - 1);
                    jsArregloURL = string.Concat("arregloURL = new Array(", jsArregloURL, ");");

                    //actualiza estatus de orden de compra a Impreso (I)
                    OrdenCompra ordCompra = new OrdenCompra();
                    ordCompra.Id_Emp = Id_Emp;
                    ordCompra.Id_Cd = Id_Cd;
                    ordCompra.Id_Ord = Id_Ord;
                    ordCompra.Ord_Estatus = "I";
                    new CN_CapOrdenCompra().ModificarOrdenCompra_Estatus(ordCompra, sesion.Emp_Cnx, ref verificador);
                    //this.DisplayMensajeAlerta("CapOrdCompra_envioInternet_ok");
                    RAM1.ResponseScripts.Add("AbrirResultado('" + urlFinal + "')");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnviarCorreoAutorizacion(OrdenCompra ordCompra, string correo, string Usuario)
        {
            using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
      
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
               
                message.From = new MailAddress(configuracion.Mail_Remitente);
                // Dirección de destino
                message.To.Add(correo);
                // Asunto 
                message.Subject = "Autorización de Orden de compra #" + ordCompra.Id_Ord;
                // Mensaje 
                message.Body = "Se han validado los documentos y autorizado la orden de compra #" + ordCompra.Id_Ord;

               // Se envía el mensaje y se informa al usuario
                string mensaje = string.Empty;
                try
                {
                    sm.Send(message);
                    GetList();
                    rgOrdenCompra.Rebind();
                    txtId_Ord.Text = "";
                    wrapper.Visible = false;
                    Alerta("Se ha enviado correo al usuario con la confirmación de la orden de compra.");
                }
                catch (Exception ex)
                {
                    Alerta("Ocurrió un error: " + ex.Message);
                }
                //resultado.Text = mensaje;
            }

            // Se borran los ficheros de la carpeta temporal
            while (lstFiles.Items.Count > 0)
            {
                borraEntrada(lstFiles.Items[0].Value);
            }
        }

        private DataTable GetList()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DataTable dtPartidasOrdenAutomatica = null;
            new CN_CapOrdenCompra().CargaOrdenaAutorizar(sesion.Emp_Cnx, -1, ref dtPartidasOrdenAutomatica);


            float ventaMes1 = 0, ventaMes2 = 0, ventaMes3 = 0, ventaMes0 = 0;
            int Prd_MaxExistencia = 0, existencia = 0;
            foreach (DataRow row in dtPartidasOrdenAutomatica.Rows)
            {
                this.VentaMes0Desc = row["ventaMes0Desc"].ToString();
                this.VentaMes1Desc = row["ventaMes1Desc"].ToString();
                this.VentaMes2Desc = row["ventaMes2Desc"].ToString();
                this.VentaMes3Desc = row["ventaMes3Desc"].ToString();

                Prd_MaxExistencia = Convert.ToInt32(row["Prd_MaxExistencia"]);
                existencia = Convert.ToInt32(row["existencia"]);
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
            return dtPartidasOrdenAutomatica;
        }
        private DataTable GetList(int Id_OrdCompra, ref int flag)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DataTable dtPartidasOrdenAutomatica = null;
            new CN_CapOrdenCompra().CargaOrdenaAutorizar(sesion.Emp_Cnx, Id_OrdCompra, ref dtPartidasOrdenAutomatica);
            float ventaMes1 = 0, ventaMes2 = 0, ventaMes3 = 0, ventaMes0 = 0;
            int Prd_MaxExistencia = 0, existencia = 0;
            foreach (DataRow row in dtPartidasOrdenAutomatica.Rows)
            {
                this.VentaMes0Desc = row["ventaMes0Desc"].ToString();
                this.VentaMes1Desc = row["ventaMes1Desc"].ToString();
                this.VentaMes2Desc = row["ventaMes2Desc"].ToString();
                this.VentaMes3Desc = row["ventaMes3Desc"].ToString();

                Prd_MaxExistencia = Convert.ToInt32(row["Prd_MaxExistencia"]);
                existencia = Convert.ToInt32(row["existencia"]);
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
            if (dtPartidasOrdenAutomatica.Rows.Count > 0)
                {flag = 1;}
            return dtPartidasOrdenAutomatica;
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

        private void ValidarPermisos()
        {
            try
            {
                if (_PermisoGuardar == false)
                    this.RadToolBar1.Items[5].Visible = false;
                if (_PermisoGuardar == false & _PermisoModificar == false)
                    this.RadToolBar1.Items[5].Visible = false;
                //Regresar
                this.RadToolBar1.Items[4].Visible = false;
                //Eliminar
                this.RadToolBar1.Items[3].Visible = false;
                //Imprimir
                this.RadToolBar1.Items[2].Visible = false;
                //Correo
                this.RadToolBar1.Items[1].Visible = false;
                //Return
                this.RadToolBar1.Items[5].Visible = false;
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
                if (mensaje.Contains("rgOrdenCompra_ItemDataBound"))
                    Alerta("Error al momento de preparar un registro para edici&oacute;n");
                else
                    if (mensaje.Contains("ProOrdenCompraAuto_insert_NoPartidas"))
                        Alerta("Los datos no se guardaron, no hay partidas que guardar o la cantidad de cada una de ellas es 0");
                    else
                        if (mensaje.Contains("ProOrdenCompraAuto_insert_ok"))
                            Alerta("Los datos se guardaron correctamente");
                        else
                            if (mensaje.Contains("btnBuscar_Click"))
                                Alerta("Error al momento de cargar las partidas");
                            else
                                if (mensaje.Contains("rgOrdenCompra_NeedDataSource"))
                                    Alerta("Error al cargar el Grid de partidas");
                                else
                                    if (mensaje.Contains("CapOrdenCompraAuto_insert_error"))
                                        Alerta("Error al momento de insertar la orden de compra");
                                    else
                                        if (mensaje.Contains("rgOrdenCompra_OrdenadoIncorrecto"))
                                            Alerta("La cantidad ordenada debe ser mayor a cero");
                                        //Alerta("La cantidad de existencia más ordenado, no debe ser mayor al máximo");
                                        else
                                            if (mensaje.Contains("rgOrdenCompra_Actualizar_error"))
                                                Alerta("Error al momento de actualizar la partida");
                                            else
                                                if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                                    Alerta("Error al cambiar de centro de distribución");
                                                else
                                                    if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                        Alerta("Error al cambiar de página");
                                                    else
                                                        Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }

        #endregion

        #region Subir archivos
        protected void cmdAddFile_Click(object sender, EventArgs e)
        {
            f = fUpload;

            // No se hace nada si no hay fichero
            if (!f.HasFile)
            {
                //Alerta("Primero debe seleccionar un archivo con extensión pdf");
                return;
            }

            // Se crea un Item para el ListBox
            //  - Value: Nombre del fichero
            //  - Text : Texto para mostrar
            ListItem item = new ListItem();
            item.Value = f.FileName;
            item.Text = f.FileName +
                        " (" + f.FileContent.Length.ToString("N0") +
                        " bytes).";

            // Se sube el fichero a la carpeta temporal
            f.SaveAs(Server.MapPath(Path.Combine(tempPath, item.Value)));

            // Se deja el nombre del fichero en el ListBox
            lstFiles.Items.Add(item);
        }

        protected void cmdDelFile_Click(object sender, EventArgs e)
        {
            ListBox lb = lstFiles;
            // Se comprueba que exista algún item seleccionado
            if (lb.SelectedItem == null)
            {
                Alerta("Primero se debe seleccionar un archivo para eliminar");
                return;
            }

            // Se elimina el fichero seleccionado
            borraEntrada(lb.SelectedItem.Value);
        }

        protected void cmdSendMail_Click(object sender, EventArgs e)
        {
            enviaCorreo(int.Parse(txtId_Ord.Text));
        }

        /// <summary>
        /// Elimina el fichero de la carpeta temporal y del ListBox.
        /// </summary>
        /// <param name="fileName"></param>
        private void borraEntrada(string fileName)
        {
            string fichero = Server.MapPath(Path.Combine(tempPath, fileName));
            File.Delete(fichero);

            ListItem l = lstFiles.Items.FindByValue(fileName);
            if (l != null)
                lstFiles.Items.Remove(l);
        }

        /// <summary>
        /// Envía el correo electrónico.
        /// Los datos de configuración del servidor de correo SMTP se configuran 
        /// en el fichero web.config.
        /// </summary>
        private void enviaCorreo(int Ord_Compra)
        {
            using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
      
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
               
                message.From = new MailAddress(configuracion.Mail_Remitente);
                // Dirección de destino
                message.To.Add(configuracion.Mail_OrdenesCompra);
                // Asunto 
                message.Subject = "Envio de documentos";
                // Mensaje 
                message.Body = "Documentos para actualización de ordenes de compra #" + Ord_Compra;

                // Se recuperan los ficheros
                if (lstFiles.Items.Count == 0)
                {
                    Alerta("Debes agregar archivos a enviar.");
                    return;
                }
                else
                {
                    foreach (ListItem l in lstFiles.Items)
                    {
                        // Lectura del nombre del fichero
                        string fichero = Server.MapPath(Path.Combine(tempPath, l.Value));
                        
                        // Adjuntado del fichero a la colección Attachments
                        message.Attachments.Add(new System.Net.Mail.Attachment(fichero));
                    }
                }
                // Se envía el mensaje y se informa al usuario
                string mensaje = string.Empty;
                try
                {
                    sm.Send(message);
                    GetList();
                    rgOrdenCompra.Rebind();
                    txtId_Ord.Text = "";
                    wrapper.Visible = false;
                    Alerta("Correo enviado con éxito");
                }
                catch (Exception ex)
                {
                    Alerta("Ocurrió un error: " + ex.Message);
                }
                //resultado.Text = mensaje;
            }

            // Se borran los ficheros de la carpeta temporal
            while (lstFiles.Items.Count > 0)
            {
                borraEntrada(lstFiles.Items[0].Value);
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
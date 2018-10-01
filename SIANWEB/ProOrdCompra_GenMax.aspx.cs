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
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class ProOrdCompra_GenMax : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

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
        //variables de excel
        bool isConfigured = false; //Configuración de página al exportar a Excel.
        StyleElement priceStyle;
        StyleElement percentStyle;
        StyleElement percentStyleNegative;
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        Session["_IdProducto"] = 0;
                        Session["listaPartidas" + Session.SessionID] = null;
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
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
                this.GetList();
                rgOrdenCompra.Rebind();
                RAM1.ResponseScripts.Add("ObtenerNombreInicio();");
                RAM1.ResponseScripts.Add("ObtenerNombreFinal();");
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "btnBuscar_Click"));
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
                        break;

                    case "save":
                        accionError = "CapOrdenCompraAuto_insert_error";
                        this.Guardar();
                        break;
                    case "arcExcel":
                        this.archivoExcel();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, accionError);
                this.DisplayMensajeAlerta(mensaje);
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
                if (e.Item is GridHeaderItem)
                {
                    GridHeaderItem header = (GridHeaderItem)e.Item;
                    header["ventaMes0"].Text = string.Concat("Venta ", this.VentaMes0Desc);
                    header["ventaMes1"].Text = string.Concat("Venta ", this.VentaMes1Desc);
                    header["ventaMes2"].Text = string.Concat("Venta ", this.VentaMes2Desc);
                    header["ventaMes3"].Text = string.Concat("Venta ", this.VentaMes3Desc);

                    GridItem cmdItem = rgOrdenCompra.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                    cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgOrdenCompra_ItemDataBound"));
            }
        }
        protected void txtId_PrdInicial_TextChanged(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Producto producto = new Producto();
            CN_CatProducto clsProducto = new CN_CatProducto();
            try
            {
                int productoInicial = txtId_PrdInicial.Value.HasValue ? (int)txtId_PrdInicial.Value.Value : -1;
                if (productoInicial > 0)
                    clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, productoInicial, 0);
                else
                {
                    txtId_PrdInicial.Text = "";
                    txtProductoIni.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, txtId_PrdInicial.ClientID);
                txtId_PrdInicial.Text = "";
                txtProductoIni.Text = "";
                return;
            }
            txtProductoIni.Text = producto.Prd_Descripcion;
            txtId_PrdFinal.Focus();
        }
        protected void txtId_PrdFinal_TextChanged(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Producto producto = new Producto();
            CN_CatProducto clsProducto = new CN_CatProducto();
            try
            {
                int productoFinal = txtId_PrdFinal.Value.HasValue ? (int)txtId_PrdFinal.Value.Value : -1;
                if (productoFinal > 0)
                    clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, productoFinal, 0);
                else
                {
                    txtId_PrdFinal.Text = "";
                    txtProductoFin.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, txtProductoFin.ClientID);
                txtId_PrdFinal.Text = "";
                txtProductoFin.Text = "";
                return;
            }
            txtProductoFin.Text = producto.Prd_Descripcion;
            btnBusqueda.Focus();
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
                    //Set Worksheet name
                    // e.Worksheet.Name = "Hoja";

                    //Set Column widths
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
            this.CargarCentros();
            this.LlenarComboProveedores();

            Session["ventaMes1Desc"] = string.Empty;
            Session["ventaMes2Desc"] = string.Empty;
            Session["ventaMes3Desc"] = string.Empty;

            //por default carga las partidas para el proveedor 100 (Almacén central)
            chkTransito.Checked = true;
            txtProveedor.Text = "100";
            cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue("100");

            this.GetList();
            rgOrdenCompra.Rebind();
            txtProveedor.Focus();
        }

        private void Guardar()
        {
            int verificador2 = 0;
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = sesion.Id_Emp;
                ordCompra.Id_Cd = sesion.Id_Cd_Ver;
                ordCompra.Id_Ord = 0;
                ordCompra.Id_Pvd = txtProveedor.Text == "" ? 0 : Convert.ToInt32(txtProveedor.Text);
                ordCompra.Id_U = sesion.Id_U;
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
                    ordenCompraDet.Id_Ptp = Convert.ToInt32(row["Id_Ptp"]);
                    listaPartidaTemp.Add(ordenCompraDet);
                }
                // Create the query.
                IEnumerable<OrdenCompraDet> sortedStudents =
                    from Partida in listaPartidaTemp
                    orderby Partida.Id_Prd ascending
                    select Partida;

                List<OrdenCompraDet> listaPartida = new List<OrdenCompraDet>();
                List<OrdenCompraDet> listaPartidanoaceptado = new List<OrdenCompraDet>();
                foreach (OrdenCompraDet Partida2 in sortedStudents)
                {
                    //****************************************************************************************************************//
                    // Se valida que lo solicitado no sebre pase el promedio de venta y valida que el producto sea sistema propietario
                    //RMB 24/01/2017
                    //***************************************************************************************************************//
                    if (Partida2.ventaPromedio < Partida2.Ord_Cantidad && Partida2.Id_Ptp == 1)
                        listaPartidanoaceptado.Add(Partida2);
                    else
                        listaPartida.Add(Partida2);
                }
                //Genera la orden de compra solo si la lista trae 1 o mas partidas
                if (listaPartida.Count > 0)
                {
                    ordCompra.ListOrdenCompra = listaPartida;
                    int verificador = 0;
                    new CN_CapOrdenCompra().InsertarOrdenCompra(ref ordCompra, sesion.Emp_Cnx, ref verificador);
                    Alerta("Se genero la orden de compra #" + verificador.ToString());
                    //actualiza grid por si hubo redondeo de partidas por multiplos
                }
                if (listaPartidanoaceptado.Count > 0)
                {
                    ordCompra.ListOrdenCompra = listaPartidanoaceptado;
                    int Partidasnoaceptadas = 1;

                    new CN_CapOrdenCompra().InsertarOrdenCompra(ref ordCompra, sesion.Emp_Cnx, ref verificador2, Partidasnoaceptadas);
                    Alerta("Se genero la orden de compra #" + verificador2.ToString() + ". Esta orden de compra quedará pendiente de autorización.");
                    //actualiza grid por si hubo redondeo de partidas por multiplos

                    //Se Ingresa registro de Orden de Compra pendienmte de Autorización
                    //RBM 25-01-2017
                    //Begin
                    InsertarOrdenPorAutorizar(verificador2);
                    EnviaEmail(verificador2);
                }
                else
                    this.DisplayMensajeAlerta("ProOrdenCompraAuto_insert_NoPartidas");

                txtId_PrdInicial_TextChanged(null, null);
                txtId_PrdFinal_TextChanged(null, null);

                this.GetList();
                rgOrdenCompra.Rebind();
                if (listaPartidanoaceptado.Count > 0)
                    RAM1.ResponseScripts.Add("AbrirAutorizacion('" + verificador2 + "')");
            }
            catch (Exception)
            {
                throw;
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
                m.To.Add(new MailAddress("raul.borquez@gibraltar.com.mx", "alejandra.benavente@key.com.mx"));
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
                    CambiarEstatus(ordCompra, "C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                rgOrdenCompra.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CambiarEstatus(int ordCompra, string estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapOrdenCompra cn_ordcompra = new CN_CapOrdenCompra();
                OrdenCompra orden = new OrdenCompra();

                orden.Id_Emp = session.Id_Emp;
                orden.Id_Cd = session.Id_Cd_Ver;
                orden.Id_Ord = ordCompra;
                orden.Ord_Estatus = estatus;
                int verificador = -1;
                //cn_ordcompra.actualizarEstatus(ordCompra, session.Emp_Cnx, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void InsertarOrdenPorAutorizar(int Id_OrdCompra)
        {
            try
            {
                int verificador = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<OrdenCompraDet> listaPartidaTemp = new List<OrdenCompraDet>();
                List<AutorizaOrdenCom> listaporAutorizar = new List<AutorizaOrdenCom>();
                AutorizaOrdenCom ordenporautorizar;

                foreach (DataRow row in this.listaPartidas.Select("ordenado > 0"))
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
                    ordenporautorizar.Ordenado = Convert.ToInt32(row["Ordenado"]);
                    ordenporautorizar.Id_U = Convert.ToInt32(row["Id_U"]);
                    ordenporautorizar.Id_U = Convert.ToInt32(row["Pendiente"]);
                    if (ordenporautorizar.Ordenado > ordenporautorizar.Promedio)
                    {
                        //ordenporautorizar.Autorizacion = 1;
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

        private void EnviarCorreoAutorizacion()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = sesion.Id_Emp;
                ordCompra.Id_Cd = sesion.Id_Cd_Ver;
                ordCompra.Id_Ord = 0;
                ordCompra.Id_Pvd = txtProveedor.Text == "" ? 0 : Convert.ToInt32(txtProveedor.Text);
                ordCompra.Id_U = sesion.Id_U;

                //ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                //configuracion.Id_Cd = session.Id_Cd_Ver;
                //configuracion.Id_Emp = session.Id_Emp;
                //CN_Configuracion cn_configuracion = new CN_Configuracion();
                //cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);





            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable GetList()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int proveedor = !string.IsNullOrEmpty(txtProveedor.Text) ? Convert.ToInt32(txtProveedor.Text) : 0;
            int productoInicial = txtId_PrdInicial.Text == "" ? 0 : Convert.ToInt32(txtId_PrdInicial.Text);
            int productoFinal = txtId_PrdFinal.Text == "" ? 0 : Convert.ToInt32(txtId_PrdFinal.Text);

            if (!Page.IsPostBack)
            {
                productoInicial = -1;
                productoFinal = -1;
            }

            bool aplicaTransito = chkTransito.Checked;
            DataTable dtPartidasOrdenAutomatica = null;
            new CN_CapOrdenCompra().GeneraOrdenCompraAutomatica(sesion.Emp_Cnx, ref dtPartidasOrdenAutomatica, "tabla", sesion.Id_Emp, sesion.Id_Cd_Ver, proveedor, productoInicial, productoFinal, aplicaTransito, null);

            float ventaMes1 = 0, ventaMes2 = 0, ventaMes3 = 0, ventaMes0 = 0;
            int Prd_MaxExistencia = 0, existencia = 0, Id_Ptp = 0;
            foreach (DataRow row in dtPartidasOrdenAutomatica.Rows)
            {
                this.VentaMes0Desc = row["ventaMes0Desc"].ToString();
                this.VentaMes1Desc = row["ventaMes1Desc"].ToString();
                this.VentaMes2Desc = row["ventaMes2Desc"].ToString();
                this.VentaMes3Desc = row["ventaMes3Desc"].ToString();

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
                    //nuevo
                    this.RadToolBar1.Items[1].Visible = false;

                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        //guardar
                        this.RadToolBar1.Items[6].Visible = false;
                        //subir archivo
                        this.RadToolBar1.Items[7].Visible = false;
                    }
                    //Regresar
                    this.RadToolBar1.Items[5].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[3].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[2].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
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
        private void ajustarTabla(ref DataTable dt)
        {
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

        private void archivoExcel()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int proveedor = !string.IsNullOrEmpty(txtProveedor.Text) ? Convert.ToInt32(txtProveedor.Text) : 0;
                int productoInicial = txtId_PrdInicial.Text == "" ? 0 : Convert.ToInt32(txtId_PrdInicial.Text);
                int productoFinal = txtId_PrdFinal.Text == "" ? 0 : Convert.ToInt32(txtId_PrdFinal.Text);
                bool aplicaTransito = chkTransito.Checked;
                if (!Page.IsPostBack)
                {
                    productoInicial = -1;
                    productoFinal = -1;
                }
                RAM1.ResponseScripts.Add("AbrirVentana_Excel('" + sesion.Id_Emp + "','" + sesion.Id_Cd_Ver + "','" + proveedor + "','" + productoInicial + "','" + productoFinal + "','" + aplicaTransito + "')");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
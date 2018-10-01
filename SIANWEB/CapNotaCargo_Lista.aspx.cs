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
using System.Text;
using System.Net;
using System.Collections;
using Telerik.Web.UI.GridExcelBuilder;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Xml;


namespace SIANWEB
{
    public partial class CapNotaCargo_Lista : System.Web.UI.Page
    {
        #region Variables

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public List<NotaCargo> listNotaCargo;

        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }

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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        rgNotaCargo.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }
                double ancho = 0;
                foreach (GridColumn gc in rgNotaCargo.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgNotaCargo.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgNotaCargo.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                Session["Sesion" + Session.SessionID] = sesion;
                rgNotaCargo.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        protected void rgNotaCargo_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgNotaCargo.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgNotaCargo_NeedDataSource"));
            }
        }
        protected void rgNotaCargo_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgNotaCargo.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void rgNotaCargo_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                Int32 item = default(Int32);

                if (e.Item == null) return;

                item = e.Item.ItemIndex;
                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgNotaCargo.Items[item]["Id_Emp"].Text);
                    int Id_Cd = Convert.ToInt32(rgNotaCargo.Items[item]["Id_Cd"].Text);
                    int Id_Nca = Convert.ToInt32(rgNotaCargo.Items[item]["Id_Nca"].Text);
                    string Id_NcaSerie = rgNotaCargo.Items[item]["Id_NcaSerie"].Text;
                    bool tienePDF = Convert.ToBoolean(rgNotaCargo.Items[item]["PDF"].Text);
                    bool tieneXML = Convert.ToBoolean(rgNotaCargo.Items[item]["NcaXML"].Text);
                    string estatus = rgNotaCargo.Items[item]["Nca_EstatusStr"].Text;
                    string[] datePart = rgNotaCargo.Items[item]["Nca_Fecha"].Text.Split(new char[] { '/' });
                    DateTime fechaNotaCargo = new DateTime(Convert.ToInt32(datePart[2]), Convert.ToInt32(datePart[1]), Convert.ToInt32(datePart[0]));

                    switch (e.CommandName.ToString())
                    {
                        case "Eliminar":
                            mensajeError = "CapNotaCargo_delete_error";
                            if (estatus.Contains("Baja"))
                                this.DisplayMensajeAlerta("CapNotaCargo_EsBaja");
                            else
                            {
                                if ((estatus.Contains("Impreso") || estatus.Contains("Capturado")) && (fechaNotaCargo >= sesion.CalendarioIni && fechaNotaCargo <= sesion.CalendarioFin))
                                {
                                    if (_PermisoEliminar)
                                    {
                                        this.CancelarNotaCargo(Id_Emp, Id_Cd, Id_Nca, Id_NcaSerie);
                                       
                                    }
                                    else
                                        this.DisplayMensajeAlerta("PermisoEliminarDenegado");
                                }
                                else
                                    this.DisplayMensajeAlerta("CapNotaCargo_EstatusIncorrecto");
                            }
                            break;
                        case "Modificar":
                            string notaModificable = "1";
                            if (estatus.Contains("Capturado") && (fechaNotaCargo >= sesion.CalendarioIni && fechaNotaCargo <= sesion.CalendarioFin))
                                notaModificable = "1";
                            else
                                notaModificable = "0";

                            if (_PermisoModificar)
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_NotaCargo_Edicion('", Id_Emp, "','", Id_Cd, "','",Id_NcaSerie , "','", Id_Nca, "','", notaModificable, "')"));
                            else
                                this.DisplayMensajeAlerta("PermisoModificarDenegado");

                            break;
                        case "Imprimir":
                            mensajeError = "CapNotaCargo_print_error";
                            if ((estatus.Contains("Impreso") || estatus.Contains("Capturado")))// && (fechaNotaCargo >= sesion.CalendarioIni && fechaNotaCargo <= sesion.CalendarioFin))
                            {
                                if (_PermisoImprimir)
                                    this.ImprimirNotaCargo(Id_Emp, Id_Cd, Id_Nca,Id_NcaSerie, tienePDF);
                                else
                                    this.DisplayMensajeAlerta("PermisoImprimirDenegado");
                            }
                            else
                                this.DisplayMensajeAlerta("CapNotaCargo_EstatusIncorrecto");
                            break;
                        case "PDF":
                            if (tienePDF)
                                descargarPDF(Id_Nca, Id_NcaSerie);
                            else
                                Alerta("Esta nota de cargo aún no cuenta con un archivo PDF");
                            break;
                        case "XML":
                            if (tieneXML)
                                descargarXML(Id_Nca, Id_NcaSerie);
                            else
                                Alerta("Esta nota de cargo aún no cuenta con un archivo XML");
                            break;
                    }
                }
                //para los botones de exportar
                if (e.CommandName.ToString().ToUpper().Contains("EXPORTTO"))
                {
                    rgNotaCargo.MasterTableView.Columns.FindByUniqueName("Editar").Visible = false;
                    rgNotaCargo.MasterTableView.Columns.FindByUniqueName("Eliminar").Visible = false;
                    rgNotaCargo.MasterTableView.Columns.FindByUniqueName("Imprimir").Visible = false;
                    rgNotaCargo.MasterTableView.Columns.FindByUniqueName("PDF").Visible = false;
                    rgNotaCargo.MasterTableView.Columns.FindByUniqueName("NcaXML").Visible = false;

                    if (e.CommandName.ToString().ToUpper().Contains("PDF"))
                        rgNotaCargo.MasterTableView.Columns.FindByUniqueName("Cte_NomComercial").HeaderStyle.Width = Unit.Pixel(200);
                }
                if (e.CommandName.ToString().ToUpper().Contains("SORT"))
                {
                    ErrorManager();
                    this.rgNotaCargo.Rebind();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, mensajeError));
            }
        }
        private void descargarXML(int Id_Nca, string Id_NcaSerie)
        {
            NotaCargo notaCargo = new NotaCargo();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            notaCargo.Id_Emp = Sesion.Id_Emp;
            notaCargo.Id_Cd = Sesion.Id_Cd_Ver;
            notaCargo.Id_Nca = Id_Nca;
            notaCargo.Id_NcaSerie = Id_NcaSerie;
            CN_CapNotaCargo notaCargo2 = new CN_CapNotaCargo();
            notaCargo2.ArchivoPdf_Xml(ref notaCargo, Sesion.Emp_Cnx);
            string ruta = null;
            System.IO.StreamWriter sw = null;
            ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Nca" + Id_Nca.ToString() + ".txt";
            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Nca" + Id_Nca.ToString() + ".xml"))
                File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Nca" + Id_Nca.ToString() + ".xml");
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            sw.WriteLine(notaCargo.Nca_Xml.ToString());
            sw.Close();
            File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Nca" + Id_Nca.ToString() + ".xml");
            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "Nca", Id_Nca.ToString(), ".xml')"));
        }
        private void descargarPDF(int Id_Nca, string Id_NcaSerie)
        {
            try
            {
                // ------------------------------
                // Abrir PDF de Nota de Cargo
                // ------------------------------
                object resultado = null;
                NotaCargo notaCargo = new NotaCargo();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                notaCargo.Id_Emp = Sesion.Id_Emp;
                notaCargo.Id_Cd = Sesion.Id_Cd_Ver;
                notaCargo.Id_Nca = Id_Nca;
                notaCargo.Id_NcaSerie = Id_NcaSerie;
                CN_CapNotaCargo nota = new CN_CapNotaCargo();
                nota.ConsultarNotaCargoSAT(ref notaCargo, Sesion.Emp_Cnx, ref resultado);
                byte[] archivoPdf = (byte[])resultado;
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("NOTACARGO_"
                            , Sesion.Id_Emp.ToString()
                            , "_", Sesion.Id_Cd.ToString()
                            , "_", Id_Nca.ToString()
                            , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                    this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add("AbrirNotaCargoPDF('" + WebURLtempPDF + "')");
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void rgNotaCargo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                GridItem cmdItem = rgNotaCargo.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void rgNotaCargo_ExcelMLExportStylesCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
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
        protected void rgNotaCargo_ExcelMLExportRowCreated(object sender, GridExportExcelMLRowCreatedArgs e)
        {
            if (e.RowType == GridExportExcelMLRowType.DataRow)
            {
                if (!isConfigured)
                {
                    //Set Worksheet name
                    e.Worksheet.Name = "Notas de cargos";

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
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                rgNotaCargo.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "btnBuscar_error"));
            }
        }
        #endregion
        #region Funciones
        private List<NotaCargo> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                listNotaCargo = new List<NotaCargo>();
                NotaCargo notaCargo = new NotaCargo();
                notaCargo.Id_Emp = sesion.Id_Emp;
                notaCargo.Id_Cd = sesion.Id_Cd_Ver;

                int? objectInt = null;
                DateTime? objectDateTime = null;

                new CN_CapNotaCargo().ConsultaNotaCargo_Buscar(notaCargo, ref listNotaCargo, sesion.Emp_Cnx
                    , this.txtCliente1.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtCliente1.Text)
                    , this.txtCliente2.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtCliente2.Text)
                    , this.txtFecha1.SelectedDate == null ? objectDateTime : Convert.ToDateTime(this.txtFecha1.SelectedDate)
                    , this.txtFecha2.SelectedDate == null ? objectDateTime : Convert.ToDateTime(this.txtFecha2.SelectedDate)
                    , this.cmbEstatus.SelectedValue == "-1" ? string.Empty : this.cmbEstatus.SelectedValue
                    , this.txtNotaCargo1.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtNotaCargo1.Text)
                    , this.txtNotaCargo2.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtNotaCargo2.Text)
                    , sesion.Propia ? sesion.Id_U : objectInt);

                return listNotaCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CancelarNotaCargo(int Id_Emp, int Id_Cd, int Id_Nca,string Id_NcaSerie)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            NotaCargo notaCargo = new NotaCargo();            

            CN_CapNotaCargo cn_notaCargo = new CN_CapNotaCargo();           
            notaCargo.ListaNotaCargo = new List<NotaCargoDet>();
            notaCargo.Id_Emp = Id_Emp;
            notaCargo.Id_Cd = Id_Cd;
            notaCargo.Id_Nca = Id_Nca;
            notaCargo.Id_NcaSerie = Id_NcaSerie;
            cn_notaCargo.ConsultaNotaCargo(ref notaCargo, sesion.Emp_Cnx);
            /*
            
            int TSATCANCELACION = 1;
            string RFC = string.Empty;
            string UUID = string.Empty;

            XmlDocument xmlBD = new XmlDocument();
            xmlBD.LoadXml(notaCargo.Nca_Xml.ToString());

            foreach (XmlNode nodo in xmlBD.ChildNodes)
            {
                if (nodo.Name == "Comprobante")
                {
                    TSATCANCELACION = 1;
                }
                else if (nodo.Name == "cfdi:Comprobante")
                {
                    TSATCANCELACION = 2;
                    foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                    {

                        if (Nodo_nivel2.Name == "cfdi:Complemento")
                        {
                            XmlNode Nodo_nivel3;
                            Nodo_nivel3 = Nodo_nivel2.FirstChild;
                            UUID = Nodo_nivel3.Attributes["UUID"].Value;
                        }

                        if (Nodo_nivel2.Name == "cfdi:Emisor")
                        {
                            RFC = Nodo_nivel2.Attributes["rfc"].Value;
                        }

                    }
                }
            }



            if (TSATCANCELACION == 2)
            {
                string valorResultadoCancelacion = "0";
                WS_CFDICancelacion.Service1 ws = new WS_CFDICancelacion.Service1();
                ws.Url = ConfigurationManager.AppSettings["WS_CFDICancelacion"].ToString();
                String respuestaCancelacion = ws.CancelacionWS("" + RFC + "," + UUID + "");
                XmlDocument XmlCancelacion = new XmlDocument();
                XmlCancelacion.LoadXml(respuestaCancelacion);


                foreach (XmlNode nodo in XmlCancelacion.ChildNodes)
                {
                    if (nodo.Name == "Acuse")
                    {
                        foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                        {
                            if (Nodo_nivel2.Name == "Folios")
                            {
                                foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                {
                                    if (Nodo_nivel3.Name == "EstatusUUID")
                                    {
                                        valorResultadoCancelacion = Nodo_nivel3.InnerText;
                                    }

                                }

                            }
                        }

                    }
                }
                string valorResultadoCancelacionTexto = string.Empty;
                switch (valorResultadoCancelacion)
                {
                    case "202":
                        valorResultadoCancelacionTexto = "Documento Previamente Cancelado";
                        break;
                    case "203":
                        valorResultadoCancelacionTexto = "Documento No corresponda al emisor";
                        break;
                    case "204":
                        valorResultadoCancelacionTexto = "Documento No Aplicable para cancelación";
                        break;
                    case "205":
                        valorResultadoCancelacionTexto = "Documento No Existe emisión";
                        break;
                    default:
                        valorResultadoCancelacionTexto = "No se hizo conexión con el servicio de cancelación";
                        break;
                }

                if (valorResultadoCancelacion != "201")
                {
                    this.Alerta(valorResultadoCancelacionTexto);
                    return;
                }
            }
            */

            int verificador = 0;

            notaCargo.Id_Emp = Id_Emp;
            notaCargo.Id_Cd = Id_Cd;
            notaCargo.Id_Nca = Id_Nca;
            notaCargo.Id_NcaSerie = Id_NcaSerie;
            new CN_CapNotaCargo().EliminarNotaCargo(notaCargo, sesion.Emp_Cnx, ref verificador);

            this.rgNotaCargo.Rebind();
            this.DisplayMensajeAlerta("CapNotaCargo_delete_ok");


        }
        private void ImprimirNotaCargo(int Id_Emp, int Id_Cd, int Id_Nca, string Id_NcaSerie, bool tienePDF = false)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = 0;
                // --------------------------------------------------------------------
                // Consulta detalle de factura para generar lista de productos
                // --------------------------------------------------------------------
                CN_CapNotaCargo cn_notaCargo = new CN_CapNotaCargo();
                NotaCargo notaCargo = new NotaCargo();
                notaCargo.ListaNotaCargo = new List<NotaCargoDet>();
                notaCargo.Id_Emp = Id_Emp;
                notaCargo.Id_Cd = Id_Cd;
                notaCargo.Id_Nca = Id_Nca;
                notaCargo.Id_NcaSerie = Id_NcaSerie;
                cn_notaCargo.ConsultaNotaCargo(ref notaCargo, sesion.Emp_Cnx);


                 // Validar si la Remisión es Válida o no en base a la suma de los montos en las partidas de la remisión y la remisión especial.
                bool bDocumentoValido = false;
                new CN_CapNotaCargo().ValidaMontosImpresion(notaCargo, sesion.Id_Cd_Ver, sesion.Id_Emp, 4, sesion.Emp_Cnx, ref bDocumentoValido);

                if (bDocumentoValido)
                {
                    List<AdendaDet> listCabT = new List<AdendaDet>();
                    List<AdendaDet> listDetT = new List<AdendaDet>();
                    new CN_CapNotaCargo().ConsultarAdenda(Id_Emp, Id_Cd, Id_Nca, Id_NcaSerie, "3", "4", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                    // -------------------------------------------------------------------------------------------
                    // Consulta productos de nota especial de la tabla 'CapFacturaEspecialDet' si esque la nota especial existe
                    // esto es si es una actualización de nota --> si el parametro Folio trae un Id de nota
                    // -------------------------------------------------------------------------------------------
                    List<NotaCargoDet> listaProdNotaEspecialFinal = new List<NotaCargoDet>();
                    new CN_CapNotaCargo().ConsultaNotaCargoEspecialDetalle(ref listaProdNotaEspecialFinal, sesion.Emp_Cnx, Id_Emp
                        , Id_Cd
                        , Id_Nca
                        , Id_NcaSerie
                        , (int)notaCargo.Id_Cte);
                    // -------------------------------------------------------------------------------------------                
                    #region variable XML a enviar
                    StringBuilder XML_Enviar = new StringBuilder();
                    XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    XML_Enviar.Append("<Comprobante");
                    XML_Enviar.Append(" serie=\"\"");
                    XML_Enviar.Append(" folio=\"\"");
                    XML_Enviar.Append(" fecha=\"\"");
                    XML_Enviar.Append(" formaDePago=\"\"");
                    XML_Enviar.Append(" subTotal=\"\"");
                    XML_Enviar.Append(" total=\"\"");
                    XML_Enviar.Append(" tipoDeComprobante=\"\"");
                    XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CARGO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION NOTA DE CARGO
                    XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                    XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                    XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                    XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CARGO
                    XML_Enviar.Append(" ConceptoDescuento1=\"\"");
                    XML_Enviar.Append(" TasaDescuento1=\"\"");
                    XML_Enviar.Append(" ConceptoDescuento2=\"\"");
                    XML_Enviar.Append(" TasaDescuento2=\"\"");
                    XML_Enviar.Append(" Notas=\"\"");
                    XML_Enviar.Append(" CliNum=\"\"");
                    XML_Enviar.Append(" MetodoPago=\"\"");
                    XML_Enviar.Append(" CuentaBancaria=\"\"");
                    XML_Enviar.Append(" Motivo=\"\"");
                    XML_Enviar.Append(" ComprobanteVersion=\"\"");
                    XML_Enviar.Append(">");
                    XML_Enviar.Append(" <Emisor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" numero=\"\" />");
                    XML_Enviar.Append(" <Receptor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" nombre=\"\"");
                    XML_Enviar.Append(" UsoCFDI=\"\">");
                    XML_Enviar.Append(" <Domicilio");
                    XML_Enviar.Append(" calle=\"\"");
                    XML_Enviar.Append(" noExterior=\"\"");
                    XML_Enviar.Append(" colonia=\"\"");
                    XML_Enviar.Append(" municipio=\"\"");
                    XML_Enviar.Append(" estado=\"\"");
                    XML_Enviar.Append(" pais=\"\"");
                    XML_Enviar.Append(" codigoPostal=\"\" />");
                    XML_Enviar.Append(" </Receptor>");
                    XML_Enviar.Append(" <Conceptos>");
                    XML_Enviar.Append(" <Concepto");
                    XML_Enviar.Append(" ClaveProdServ=\"\"");
                    XML_Enviar.Append(" ClaveUnidad=\"\"");
                    XML_Enviar.Append(" cantidad=\"0\"");
                    XML_Enviar.Append(" noIdentificacion=\"\"");
                    XML_Enviar.Append(" descripcion=\"\"");
                    XML_Enviar.Append(" valorUnitario=\"0\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Conceptos>");

                    XML_Enviar.Append(" <Impuestos");
                    XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                    XML_Enviar.Append(" <Traslados>");
                    XML_Enviar.Append(" <Traslado");
                    XML_Enviar.Append(" impuesto=\"\"");
                    XML_Enviar.Append(" tasa=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Traslados>");
                    XML_Enviar.Append(" </Impuestos>");

                    XML_Enviar.Append(" <Addenda>");
                    //ADENDA CABECERA

                    XML_Enviar.Append(" <cabecera");
                    XML_Enviar.Append(" Pedido=\"\"");
                    XML_Enviar.Append(" Requisicion=\"\"");
                    XML_Enviar.Append(" consignarRenglon1=\"\"");
                    XML_Enviar.Append(" consignarRenglon2=\"\"");
                    XML_Enviar.Append(" consignarRenglon3=\"\"");
                    XML_Enviar.Append(" consignarRenglon4=\"\"");
                    XML_Enviar.Append(" consignarRenglon5=\"\"");
                    XML_Enviar.Append(" Conducto=\"\"");
                    XML_Enviar.Append(" CondicionesPago=\"\"");
                    XML_Enviar.Append(" NumeroGuia=\"\"");
                    XML_Enviar.Append(" ControlPedido=\"\"");
                    XML_Enviar.Append(" OrdenEmbarque=\"\"");
                    XML_Enviar.Append(" Zona=\"\"");
                    XML_Enviar.Append(" Territorio=\"\"");
                    XML_Enviar.Append(" Agente=\"\"");
                    XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                    XML_Enviar.Append(" Formulo=\"\"");
                    XML_Enviar.Append(" Autorizo=\"\"");
                    XML_Enviar.Append(" NombreAddenda=\"\"");
                    foreach (AdendaDet det in listCabT)
                    {
                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    }

                    XML_Enviar.Append("/>");

                    //ADENDA DETALLE
                    if (listaProdNotaEspecialFinal.Count > 0)
                    {
                        foreach (NotaCargoDet notaCargoDet in listaProdNotaEspecialFinal)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + notaCargoDet.Producto.Id_PrdEsp + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + notaCargoDet.Producto.Prd_Presentacion.Trim() + " " + notaCargoDet.Producto.Prd_UniNs + "\"");
                            XML_Enviar.Append(" ClaveProdServ=\"01010101\"");
                            XML_Enviar.Append(" ClaveUnidad=\"H87\""); 
                            foreach (AdendaDet det in listDetT)
                            {
                                if (notaCargoDet.Id_Prd == det.Id_Prd)
                                {
                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                }
                            }
                            XML_Enviar.Append("/>");
                        }
                    }
                    else
                    {
                        foreach (NotaCargoDet notaCargoDet in notaCargo.ListaNotaCargo)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + notaCargoDet.Id_Prd + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + notaCargoDet.Prd_Presentacion.Trim() + " " + notaCargoDet.Prd_Unis + "\"");
                            XML_Enviar.Append(" ClaveProdServ=\"01010101\"");
                            XML_Enviar.Append(" ClaveUnidad=\"H87\""); 
                            foreach (AdendaDet det in listDetT)
                            {
                                if (notaCargoDet.Id_Prd == det.Id_Prd)
                                {
                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                }
                            }
                            XML_Enviar.Append("/>");
                        }
                    }
                    XML_Enviar.Append(" </Addenda>");
                    XML_Enviar.Append(" </Comprobante>");

                    #endregion

                    #region Codigo pruebas

                    //PruebaServicio.Service1 servicio = new PruebaServicio.Service1();
                    //float suma = servicio.Suma(Convert.ToSingle(txtNumero1.Text), Convert.ToSingle(txtNumero2.Text));
                    //this.Alerta(suma.ToString());

                    //Uri objURI = new Uri("");
                    //WebRequest objWebRequest = WebRequest.Create(objURI);
                    //WebResponse objWebResponse = objWebRequest.GetResponse();
                    //Stream objStream = objWebResponse.GetResponseStream();
                    //StreamReader objStreamReader = new StreamReader(objStream);
                    //string responseText = objStreamReader.ReadToEnd();

                    #endregion

                    // --------------------------------------
                    // Consulta centro de distribución
                    // --------------------------------------
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    // --------------------------------------
                    // cargar xml de factura que se envia a SAT
                    // --------------------------------------
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XML_Enviar.ToString());
                    // --------------------------------------//
                    // --------------------------------------//
                    //         LLENAR DATOS DEL XML        --//
                    // --------------------------------------//
                    // --------------------------------------//
                    #region Llenar datos Nota de cargo a Enviar
                    CN_CatTipoMoneda cn_moneda = new CN_CatTipoMoneda();
                    TipoMoneda tm = new TipoMoneda();
                    tm.Id_Emp = sesion.Id_Emp;
                    tm.Id_Mon = 2;
                    cn_moneda.ConsultaTipoMonedaIndividual(ref tm, sesion.Emp_Cnx);

                    //encabezado
                    XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                    Comprobante.Attributes["serie"].Value = notaCargo.Id_NcaSerie.Replace(notaCargo.Id_Nca.ToString(), "");
                    Comprobante.Attributes["folio"].Value = notaCargo.Id_Nca.ToString();
                    Comprobante.Attributes["fecha"].Value = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", notaCargo.Nca_FechaHr);
                    Comprobante.Attributes["subTotal"].Value = notaCargo.Nca_Subtotal.ToString();
                    Comprobante.Attributes["total"].Value = notaCargo.Nca_Total.ToString();
                    Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                    Comprobante.Attributes["tipoMovimiento"].Value = "NOTA DE CARGO";
                    Comprobante.Attributes["tipoMoneda"].Value = tm.Mon_Abrev;
                    Comprobante.Attributes["tipoCambio"].Value = tm.Mon_TipCambio.ToString();
                    Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                    Comprobante.Attributes["movimientoacancelar"].Value = ""; //
                    Comprobante.Attributes["CliNum"].Value = notaCargo.Id_Cte.ToString();
                    Comprobante.Attributes["Notas"].Value = notaCargo.Nca_Notas;

                    Comprobante.Attributes["MetodoPago"].Value =  "00".Substring(1, 2 - notaCargo.Nca_FPago.Trim().Length) + notaCargo.Nca_FPago.Trim(); //FormaPagoNombre(notaCargo.Nca_FPago);

                    Comprobante.Attributes["CuentaBancaria"].Value = notaCargo.Nca_UDigitos.ToString();
                    Comprobante.Attributes["Motivo"].Value = notaCargo.Tm_Nombre;
                    //consultar datos del cliente de la nota de credito
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Cte = Convert.ToInt32(notaCargo.Id_Cte);
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    //receptor
                    XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                    Receptor.Attributes["rfc"].Value = cliente.Cte_FacRfc;
                    Receptor.Attributes["nombre"].Value = cliente.Cte_NomComercial;
                    Receptor.Attributes["UsoCFDI"].Value = cliente.Cte_UsoCFDI;
                    Comprobante.Attributes["formaDePago"].Value = cliente.Cte_MetodoPago;
                    Comprobante.Attributes["ComprobanteVersion"].Value = "3.3";

                    //
                    XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                    Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                    Emisor.Attributes["numero"].Value = Cd.Cd_Numero;
                    //Domicilio
                    XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                    Domicilio.Attributes["calle"].Value = cliente.Cte_FacCalle;
                    Domicilio.Attributes["noExterior"].Value = cliente.Cte_FacNumero;
                    Domicilio.Attributes["colonia"].Value = cliente.Cte_FacColonia;
                    Domicilio.Attributes["municipio"].Value = cliente.Cte_FacMunicipio;
                    Domicilio.Attributes["estado"].Value = cliente.Cte_FacEstado;
                    Domicilio.Attributes["pais"].Value = "México";
                    Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;

                    // ---------------------
                    // Conceptos --> partidas = producto
                    // Detalle --> productoDetalle
                    // ---------------------              
                    XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                    XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                    XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");

                    //Si existe una nota especial, en los nodos de conceptos del producto se pone
                    //los productos de la nota especial
                    //si no, se pone los datos de productos de la nota original
                    if (listaProdNotaEspecialFinal.Count > 0)
                    {
                        foreach (NotaCargoDet notaDet in listaProdNotaEspecialFinal)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = notaDet.Producto.Id_PrdEsp;
                            prd.Attributes["descripcion"].Value = notaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                            prd.Attributes["importe"].Value = notaDet.Nca_Importe.ToString();
                            prd.Attributes["ClaveProdServ"].Value = "01010101";
                            prd.Attributes["ClaveUnidad"].Value = "H87";
                            producto.ParentNode.AppendChild(prd);
                        }
                    }
                    else
                    {
                        foreach (NotaCargoDet notaCargoDet in notaCargo.ListaNotaCargo)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = notaCargoDet.Id_Prd.ToString();
                            prd.Attributes["descripcion"].Value = notaCargoDet.Prd_Nombre.Replace("\"", "");
                            prd.Attributes["importe"].Value = notaCargoDet.Nca_Importe.ToString();
                            prd.Attributes["ClaveProdServ"].Value = "01010101";
                            prd.Attributes["ClaveUnidad"].Value = "H87";
                            producto.ParentNode.AppendChild(prd);
                        }
                    }
                    producto.ParentNode.RemoveChild(producto);
                    //Impuestos
                    XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                    Impuestos.Attributes["totalImpuestosTrasladados"].Value = notaCargo.Nca_Iva.ToString();

                    //Traslado (impuestos desgloce)
                    XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                    XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                    Traslado.Attributes["impuesto"].Value = "IVA";
                    Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();
                    Traslado.Attributes["importe"].Value = notaCargo.Nca_Iva.ToString();

                    //datos de cabecera
                    XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                    cabecera.Attributes["Pedido"].Value = "";
                    cabecera.Attributes["Zona"].Value = notaCargo.Id_Cd.ToString();//Cd.Cd_Descripcion;
                    cabecera.Attributes["Territorio"].Value = notaCargo.Id_Ter.ToString();//notaCargo.Ter_Nombre == null ? string.Empty : notaCargo.Ter_Nombre;
                    cabecera.Attributes["Agente"].Value = notaCargo.Id_Rik.ToString();//== null ? string.Empty : notaCargo.Id_Rik.ToString();
                    cabecera.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                    cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                    cabecera.Attributes["Requisicion"].Value = "";
                    cabecera.Attributes["consignarRenglon1"].Value = cliente.Cte_NomComercial;
                    cabecera.Attributes["consignarRenglon2"].Value = string.Concat(cliente.Cte_Calle, " ", cliente.Cte_Numero);
                    cabecera.Attributes["consignarRenglon3"].Value = string.Concat(cliente.Cte_Colonia, " ", cliente.Cte_Municipio);
                    cabecera.Attributes["consignarRenglon4"].Value = cliente.Cte_Estado;
                    cabecera.Attributes["consignarRenglon5"].Value = "México";
                    cabecera.Attributes["Conducto"].Value = "";
                    cabecera.Attributes["CondicionesPago"].Value = "";
                    cabecera.Attributes["NumeroGuia"].Value = "";
                    cabecera.Attributes["ControlPedido"].Value = "";
                    cabecera.Attributes["OrdenEmbarque"].Value = "";
                    cabecera.Attributes["NumeroDocumentoAduanero"].Value = "";
                    cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;

                    #endregion
                    // --------------------------------------
                    // convertir XML a string 
                    // --------------------------------------
                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    xml.WriteTo(tx);
                    string xmlString = sw.ToString();
                    // ------------------------------------------------------
                    // ENVIAR XML al servicio de la aplicacion de KEY
                    // ------------------------------------------------------
                    XmlDocument xmlSAT = new XmlDocument();
                    //sian_cfd.Service1 sianFacturacionElectronica = new sian_cfd.Service1();
                    WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                    sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                    object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                    // ------------------------------------------------------
                    string stringPDF = string.Empty;
                    string selloSAT = string.Empty;
                    string folioFiscal = string.Empty;
                    string errorNum = string.Empty;
                    string errorText = string.Empty;
                    xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                    int TSAT = 1;
                    foreach (XmlNode nodo in xmlSAT.ChildNodes)
                    {
                        if (nodo.Name == "Comprobante")
                        {
                            TSAT = 1;
                            selloSAT = nodo.Attributes["Sello"].Value;
                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }
                                    nodo.RemoveChild(Nodo_nivel2);
                                }
                            }
                        }
                        else if (nodo.Name == "cfdi:Comprobante")
                        {
                            TSAT = 2;
                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodo.RemoveChild(Nodo_nivel2);
                                }

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    XmlNode Nodo_nivel3;
                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                    selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                    folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                }

                            }

                        }

                    }

                    if (TSAT == 2 && tienePDF)
                    {
                        descargarPDF(Id_Nca, Id_NcaSerie);
                        return;
                    }


                    if (errorNum != "0")
                        this.Alerta(string.Concat("El servicio de KEY ha devuelto el siguiente error:<br/>", errorText.Replace("'", "\"")));
                    else
                    {
                        notaCargo.Nca_Sello = selloSAT;
                        notaCargo.Nca_FolioFiscal = folioFiscal;
                        System.Data.SqlTypes.SqlXml sqlXml
                            = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                        notaCargo.Nca_Xml = sqlXml;
                        notaCargo.Nca_Pdf = this.Base64ToByte(stringPDF);
                        // ---------------------------------------------------------------------------------------------
                        // Se actualiza el estatus de la nota de cargo a Impreso (I)
                        // ---------------------------------------------------------------------------------------------
                        verificador = 0;
                        notaCargo.Nca_Estatus = "I";
                        new CN_CapNotaCargo().ModificarNotaCargoSAT(notaCargo, sesion.Emp_Cnx, ref verificador);
                        // ------------------------------
                        // Abrir PDF de Nota de Cargo
                        // ------------------------------
                        string tempPDFname = string.Concat("NOTACARGO_"
                                , notaCargo.Id_Emp.ToString()
                                , "_", notaCargo.Id_Cd.ToString()
                                , "_", notaCargo.Id_U.ToString()
                                , ".pdf");
                        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                        this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                        // ------------------------------------------------------------------------------------------------
                        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                        // ------------------------------------------------------------------------------------------------
                        RAM1.ResponseScripts.Add("AbrirNotaCargoPDF('" + WebURLtempPDF + "')");
                    }
                }
                else
                {
                    RAM1.ResponseScripts.Add("OpenAlert('Los montos de la Nota de Cargo y la Nota de Cargo Especial no coinciden','" + Id_Emp + "','" + Id_Cd + "','" + Id_NcaSerie + "','"  + Id_Nca + "','" + 1 + "')");
                    //RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_NotaCargo_Edicion('", Id_Emp, "','", Id_Cd, "','", Id_NcaSerie, "','", Id_Nca, "','", "1", "')"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string FormaPagoNombre(string Id_Fpa)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatFormaPago cncatformapago = new CN_CatFormaPago();
                FormaPago fpago = new FormaPago();
                fpago.Id_Emp = sesion.Id_Emp;
                fpago.Id_Fpa = Convert.ToInt32(Id_Fpa == "" ? "1" : Id_Fpa);
                cncatformapago.ConsultaFormaPago(ref fpago, sesion.Emp_Cnx);

                return fpago.Descripcion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                    filebytes = Convert.FromBase64String(data);
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
                File.Delete(tempPath);
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }
        private void ShowTempPDF(string tempPath_archivoPDF)
        {
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(tempPath_archivoPDF);
            proc.Start();
            while (!proc.HasExited)
            {
                System.Threading.Thread.Sleep(200);
            }
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            this.CargarCentros();
            this.cmbEstatus.Sort = RadComboBoxSort.Ascending;
            this.cmbEstatus.SortItems();

            //Cargar grid de ordenes de compra
            if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
            {
                txtFecha1.DbSelectedDate = sesion.CalendarioIni;
            }
            if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
            {
                txtFecha2.DbSelectedDate = sesion.CalendarioFin;
            }

            double ancho = 0;
            foreach (GridColumn gc in rgNotaCargo.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgNotaCargo.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgNotaCargo.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

            rgNotaCargo.Rebind();
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
                        this.RadToolBar1.FindItemByValue("new").Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        private void CargarCliente(ref RadComboBox combo)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref combo);
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
                if (mensaje.Contains("TempPDFNoData"))
                    Alerta("El archivo PDF no contiene datos.");
                else
                    if (mensaje.Contains("PermisoModificarDenegado"))
                        Alerta("Operación denegada, no tiene permisos para modificar notas de cargos");
                    else
                        if (mensaje.Contains("PermisoEliminarDenegado"))
                            Alerta("Operación denegada, no tiene permisos para dar de baja notas de cargos");
                        else
                            if (mensaje.Contains("PermisoImprimirDenegado"))
                                Alerta("Operación denegada, no tiene permisos para imprimir notas de cargos");
                            else
                                if (mensaje.Contains("CapNotaCargo_Modificar_Denegado"))
                                    Alerta("Imposible modificar el documento");
                                else
                                    if (mensaje.Contains("CapNotaCargo_delete_ok"))
                                        Alerta("La nota de cargo se ha dado de baja (estatus \"B\") correctamente");
                                    else
                                        if (mensaje.Contains("CapNotaCargo_EstatusIncorrecto"))
                                            Alerta("No se puede realizar la operación. El estatus es incorrecto");
                                        else
                                            if (mensaje.Contains("CapNotaCargo_EsBaja"))
                                                Alerta("La nota de cargo ya está dada de baja");
                                            else
                                                if (mensaje.Contains("CapNotaCargo_delete_error"))
                                                    Alerta("Error al momento de dar de baja la nota de cargo");
                                                else
                                                    if (mensaje.Contains("CapNotaCargo_print_error"))
                                                        Alerta(string.Concat("Error al imprimir la nota de cargo. ", mensaje.Replace("'", "\"")));
                                                    else
                                                        if (mensaje.Contains("RAM1_AjaxRequest"))
                                                            Alerta("Error al momento de actualizar el grid de notas de cargos");
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
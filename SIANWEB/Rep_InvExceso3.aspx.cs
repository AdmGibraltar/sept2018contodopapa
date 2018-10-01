using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.IO;
using CapaDatos;
using LibreriaReportes;
using Telerik.Reporting.Processing;
using System.Collections;
using System.Data;
using System.Text;

namespace SIANWEB
{
    public partial class Rep_InvExceso3 : System.Web.UI.Page
    {
        #region Variables
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
        public string indicadorTexto = "";
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Funciones funcion = new Funciones();
                        int Proveedor = Convert.ToInt32(Page.Request.QueryString["ProveedorVer"]);
                        int Centro = Convert.ToInt32(Page.Request.QueryString["Centro"]);
                        int Dias = Convert.ToInt32(Page.Request.QueryString["DiasVer"]);
                        int Tproducto = Convert.ToInt32(Page.Request.QueryString["Tproducto"]);
                        lblLeyenda.Text = lblLeyenda.Text.Replace("[Proveedor]", Proveedor == -1 ? "Todos" : Proveedor.ToString());
                        lblLeyenda.Text = lblLeyenda.Text.Replace("[Sucursal]", Centro == -1 ? "Todos" : Centro.ToString());
                        lblLeyenda.Text = lblLeyenda.Text.Replace("[Dia]", Dias == -1 ? "Todos" : Dias.ToString());
                        lblLeyenda.Text = lblLeyenda.Text.Replace("[Fecha]", funcion.GetLocalDateTime(sesion.Minutos).ToString("dd/MM/yyyy hh:mm:ss tt"));
                        RadGrid1.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    RadGrid1.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName )
            //    //||                 e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
            //    //e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
            //{
            //    ConfigureExport();
            //}

            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {

                //  RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.OpenInNewWindow = false;
            }
        }

        protected void Button1_Click(object sender, System.EventArgs e)
        {
            // ConfigureExport();
            Export = true;
            RadGrid1.MasterTableView.ExportToExcel();

            // add the style props to get the page orientation
        }

        bool Export = false;


        protected void RadGrid1_DataBound(System.Object sender, System.EventArgs e)
        {
            if (Export == true)
            {
                RadGrid1.ExportSettings.ExportOnlyData = false;
                RadGrid1.ExportSettings.FileName = "Report Name";
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.HideStructureColumns = true;
                RadGrid1.FilterMenu.Visible = false;

                RadGrid1.HeaderContextMenu.Visible = false;
                RadGrid1.EnableHeaderContextMenu = false;
                RadGrid1.EnableHeaderContextFilterMenu = false;
                RadGrid1.AllowSorting = false;
                RadGrid1.AllowPaging = false;
                RadGrid1.AllowFilteringByColumn = false;
                RadGrid1.MasterTableView.AllowFilteringByColumn = false;
                RadGrid1.MasterTableView.AllowSorting = false;
                RadGrid1.MasterTableView.AllowPaging = false;
                RadGrid1.MasterTableView.EnableHeaderContextFilterMenu = false;
                RadGrid1.MasterTableView.EnableHeaderContextMenu = false;
            }
        }

        protected void RadGrid1_GridExporting(System.Object source, Telerik.Web.UI.GridExportingArgs e)
        {
            ExportDataGridToExcel(RadGrid1, Response);
        }


        public void ExportDataGridToExcel(System.Web.UI.Control ctrl, System.Web.HttpResponse response)
        {
            response.Clear();
            response.Buffer = true;
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = "application/vnd.ms-excel";
            //response.AddHeader("content-disposition", "attachment;filename=Flash Report.xls");
            string name = "Report.xls";
            response.AddHeader("content-disposition attachment;", "filename=" + Path.GetFileNameWithoutExtension(name));
            response.Charset = "";
            this.EnableViewState = false;

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            //this.ClearControls(ctrl);
            ctrl.RenderControl(oHtmlTextWriter);

            // set content type and character set to cope with european chars like the umlaut.
            response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">" + Environment.NewLine);

            // add the style props to get the page orientation
            response.Write(AddExcelStyling());
            response.Write("<span style='font-size: 11pt; font-family: Arial; font-weight:bold;'>" + "REPORTE EXCESO DE INVENTARIO" + indicadorTexto + "</span>");
            response.Write("<br>");
            response.Write("<span style='font-size: 10pt; font-family: Arial;'>" + lblLeyenda.Text + "</span>");
            response.Write("<br>");
            response.Write("<br>");
            response.Write(oStringWriter.ToString());
            response.Write("</body>");
            response.Write("</html>");

            response.End();
        }
        private string AddExcelStyling()
        {
            // add the style props to get the page orientation
            StringBuilder sb = new StringBuilder();

            sb.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office'" + Environment.NewLine + "xmlns:x='urn:schemas-microsoft-com:office:excel'" + Environment.NewLine + "xmlns='http://www.w3.org/TR/REC-html40'>" + Environment.NewLine + "<head>" + Environment.NewLine);
            sb.Append("<style>" + Environment.NewLine);
            sb.Append("@page");

            //page margin can be changed based on requirement.....
            sb.Append("{margin:.20in .20in .20in .20in;" + Environment.NewLine);
            sb.Append("mso-header-margin:.20in;" + Environment.NewLine);
            sb.Append("mso-footer-margin:.20in;" + Environment.NewLine);
            sb.Append("mso-height-source:96.75pt;" + Environment.NewLine);
            sb.Append("mso-page-orientation:portrait;}" + Environment.NewLine);

            sb.Append("</style>" + Environment.NewLine);
            sb.Append("<!--[if gte mso 9]><xml>" + Environment.NewLine);
            sb.Append("<x:ExcelWorkbook>" + Environment.NewLine);
            sb.Append("<x:ExcelWorksheets>" + Environment.NewLine);
            sb.Append("<x:ExcelWorksheet>" + Environment.NewLine);
            sb.Append("<x:Name>Flash Report</x:Name>" + Environment.NewLine);
            sb.Append("<x:WorksheetOptions>" + Environment.NewLine);
            sb.Append("<x:Print>" + Environment.NewLine);
            sb.Append("<x:ValidPrinterInfo/>" + Environment.NewLine);
            sb.Append("<x:PaperSizeIndex>9</x:PaperSizeIndex>" + Environment.NewLine);
            sb.Append("<x:HorizontalResolution>600</x:HorizontalResolution>" + Environment.NewLine);
            sb.Append("<x:VerticalResolution>600</x:VerticalResolution>" + Environment.NewLine);
            sb.Append("<x:Scale>100</x:Scale>" + Environment.NewLine);
            sb.Append("</x:Print>" + Environment.NewLine);
            sb.Append("<x:Selected/>" + Environment.NewLine);
            sb.Append("<x:DoNotDisplayGridlines/>" + Environment.NewLine);
            sb.Append("<x:ProtectContents>False</x:ProtectContents>" + Environment.NewLine);
            sb.Append("<x:ProtectObjects>False</x:ProtectObjects>" + Environment.NewLine);
            sb.Append("<x:ProtectScenarios>False</x:ProtectScenarios>" + Environment.NewLine);
            sb.Append("</x:WorksheetOptions>" + Environment.NewLine);
            sb.Append("</x:ExcelWorksheet>" + Environment.NewLine);
            sb.Append("</x:ExcelWorksheets>" + Environment.NewLine);
            sb.Append("<x:WindowHeight>12780</x:WindowHeight>" + Environment.NewLine);
            sb.Append("<x:WindowWidth>19035</x:WindowWidth>" + Environment.NewLine);
            sb.Append("<x:WindowTopX>0</x:WindowTopX>" + Environment.NewLine);
            sb.Append("<x:WindowTopY>0</x:WindowTopY>" + Environment.NewLine);
            sb.Append("<x:ProtectStructure>False</x:ProtectStructure>" + Environment.NewLine);
            sb.Append("<x:ProtectWindows>False</x:ProtectWindows>" + Environment.NewLine);
            sb.Append("</x:ExcelWorkbook>" + Environment.NewLine);
            sb.Append("</xml><![endif]-->" + Environment.NewLine);
            sb.Append("</head>" + Environment.NewLine);
            sb.Append("<body>" + Environment.NewLine);
            return sb.ToString();
        }

       

        #endregion
        #region Funciones
        private bool ValidarSesion()
        {
            try
            {
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
        private List<RepExcesos> GetList()
        {
            try
            {
                List<RepExcesos> List = new List<RepExcesos>();



                CN_Rep_InvExceso exceso = new CN_Rep_InvExceso();
                RepExcesos rep = new RepExcesos();
                rep.Id_Emp = sesion.Id_Emp;
                rep.Id_Cd = sesion.Id_Cd_Ver;
                rep.Id_U = sesion.Id_U;
                rep.Indicador = Convert.ToInt32(Page.Request.QueryString["Indicador"]);
                rep.Proveedor = Convert.ToInt32(Page.Request.QueryString["Proveedor"]);
                rep.Centro = Convert.ToInt32(Page.Request.QueryString["Centro"]);
                rep.Dias = Convert.ToInt32(Page.Request.QueryString["Dias"]);
                rep.Tproducto = Convert.ToInt32(Page.Request.QueryString["Tproducto"]);
                rep.ProveedorVer = Convert.ToInt32(Page.Request.QueryString["ProveedorVer"]);
                rep.DiasVer = Convert.ToInt32(Page.Request.QueryString["DiasVer"]);
                rep.Salida = 3;
                exceso.Consulta3(rep, sesion.Emp_Cnx, ref List);

                indicadorTexto = "";

                if (rep.Indicador == -1)
                    indicadorTexto = " TODOS";
                else if (rep.Indicador == 1)
                    indicadorTexto = " QUE ROTA";
                else
                    indicadorTexto = " QUE NO ROTA";
                LblTitulo.Text = LblTitulo.Text.Replace("Rota", indicadorTexto == "TODOS" ? "TODOS" : indicadorTexto);
                
                return List;

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
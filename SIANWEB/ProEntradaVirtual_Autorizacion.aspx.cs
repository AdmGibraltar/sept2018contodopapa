using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Mime;
using Telerik.Web.UI.GridExcelBuilder;

namespace SIANWEB
{
    public partial class ProEntradaVirtual_Autorizacion : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        //int Id_Folio;

        bool isExport =  false;
      

        bool isConfigured = false; //Configuración de página al exportar a Excel.
        StyleElement priceStyle;
        StyleElement percentStyle;
        StyleElement percentStyleNegative;

        #endregion
        #region Eventos

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Params["Id4"] != "1") //check the user weather user is logged in or not
      
                this.Page.MasterPageFile = "~/MasterPage/MasterPage02.master";

            else
                this.Page.MasterPageFile = "~/MasterPage/MasterPage01.master";
            
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
                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();

                        CargarProveedor();
                        Inicializar();
                    }
                }


                if (Request.Params["Id4"] != "1") //check the user weather user is logged in or not
                {
                    this.CmbCentro.Visible = false;
                    this.Label6.Visible = false;
                    this.TblEncabezado.Visible = false;

                }


    
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void LlenarComboProveedores()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spProveedores_Combo", ref cmbProveedor);
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID]; 
                if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);  Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    Guardar();
                }

                if (btn.CommandName == "arcExcel")
               {
                   this.archivoExcel();
                   
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick1");
            }
        }

        
        private void archivoExcel()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                RAM1.ResponseScripts.Add("AbrirVentana_Excel('" + sesion.Id_Emp + "','" + sesion.Id_Cd_Ver + "')");
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
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        rg1.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rg1_ExcelMLExportStylesCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
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



        protected void rg1_ExcelMLExportRowCreated(object sender, GridExportExcelMLRowCreatedArgs e)
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


        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetListPro();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_NeedDataSource");
            }
        }

        private void CargarProveedor()
        {
       
            cmbProveedor.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbProveedor.Items.Add(new RadComboBoxItem("Almacén Central", "1"));
            cmbProveedor.Items.Add(new RadComboBoxItem("Kimberly Clark ", "2"));
            cmbProveedor.Items.Add(new RadComboBoxItem("Georgia Pacific ", "3"));
        
        }
        
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":

                        break;
                    case "PerformInsert":
                        //this.PerformInsert(e);
                        break;
                    case "Update":
                        //this.Update(e);
                        break;
                    case "Delete":
                        //this.Delete(e);
                        break;
                }

                if (e.CommandName.ToString().ToUpper().Contains("EXPORTTO"))
                {
                    rg1.MasterTableView.Columns.FindByUniqueName("Autorizar").Visible = false;
                    rg1.MasterTableView.Columns.FindByUniqueName("Rechazado").Visible = false;
                    rg1.MasterTableView.Columns.FindByUniqueName("Pendiente").Visible = false;
                    rg1.MasterTableView.Columns.FindByUniqueName("Ape_PreEsp").Visible = false;
                    rg1.MasterTableView.Columns.FindByUniqueName("Ape_PreEspHidden").Visible = true;
                    rg1.MasterTableView.Columns.FindByUniqueName("Ape_PreVta").Visible = false;
                    rg1.MasterTableView.Columns.FindByUniqueName("Ape_PreVtaHidden").Visible = true;

                    rg1.MasterTableView.Columns.FindByUniqueName("Ape_VolVta").Visible = false;
                    rg1.MasterTableView.Columns.FindByUniqueName("Ape_VolvtaHidden").Visible = true;
                    rg1.MasterTableView.Columns.FindByUniqueName("Inicia").Visible = false;
                    rg1.MasterTableView.Columns.FindByUniqueName("IniciaHidden").Visible = true;
                    rg1.MasterTableView.Columns.FindByUniqueName("Vigencia").Visible = false;
                    rg1.MasterTableView.Columns.FindByUniqueName("VigenciaHidden").Visible = true;
                    
                    
                 
                    rg1.MasterTableView.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width = Unit.Pixel(200);                    

                    rg1.MasterTableView.UseAllDataFields = true;                 

                  
                }


                

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCliente_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgCliente.DataSource = GetListCte();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkAutorizarAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                for (int x = 0; x < rg1.Items.Count; x++)
                {
                    if ((rg1.Items[x]["Autorizar"].FindControl("chkAutorizar") as CheckBox).Enabled)
                    {
                        (rg1.Items[x]["Autorizar"].FindControl("chkRechazar") as CheckBox).Checked = false;
                        (rg1.Items[x]["Rechazado"].FindControl("chkAutorizar") as CheckBox).Checked = true;
                        (rg1.Items[x]["Pendiente"].FindControl("chkPendiente") as CheckBox).Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkRechazarAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                for (int x = 0; x < rg1.Items.Count; x++)
                {
                    if ((rg1.Items[x]["Rechazado"].FindControl("chkRechazar") as CheckBox).Enabled)
                    {
                        (rg1.Items[x]["Rechazado"].FindControl("chkRechazar") as CheckBox).Checked = true;
                        (rg1.Items[x]["Autorizar"].FindControl("chkAutorizar") as CheckBox).Checked = false;
                        (rg1.Items[x]["Pendiente"].FindControl("chkPendiente") as CheckBox).Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkPendienteAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                for (int x = 0; x < rg1.Items.Count; x++)
                {
                    if ((rg1.Items[x]["Pendiente"].FindControl("chkPendiente") as CheckBox).Enabled)
                    {
                        (rg1.Items[x]["Rechazado"].FindControl("chkRechazar") as CheckBox).Checked = false;
                        (rg1.Items[x]["Autorizar"].FindControl("chkAutorizar") as CheckBox).Checked = false;
                        (rg1.Items[x]["Pendiente"].FindControl("chkPendiente") as CheckBox).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rg1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                string clientID = (e.Item.FindControl("rdp_FecIni") as RadDatePicker).ClientID;
                ScriptManager.RegisterArrayDeclaration(this, "ArrFechaInicio", String.Format("['{0}']", clientID));
            }
        } 

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DateTime? ini = dpFecha1.SelectedDate;
                DateTime? fin = dpFecha2.SelectedDate;

                if (ini == null || fin == null)
                {
                    Alerta("Seleccione una fecha de inicio o una fecha de vigencia");
                    return;
                }
                if (ini > fin)
                {
                    Alerta("La fecha de fin debe ser mayor a la fecha de inicio");
                    return;
                }

                for (int x = 0; x < rg1.Items.Count; x++)
                {
                   
                        
                            
                            (rg1.Items[x]["Inicia"].FindControl("rdp_FecIni") as RadDatePicker).SelectedDate = ini;     
                            (rg1.Items[x]["Vigencia"].FindControl("rdp_FecVigencia") as RadDatePicker).SelectedDate = fin;
                          
                        
                    
                }
                dpFecha1.SelectedDate = null;
                dpFecha2.SelectedDate = null;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }
        #endregion


        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
               
                pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Descripcion;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = sesion.Id_U;
                Permiso.Id_Cd = sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                    {
                        this.rtb1.Items[1].Visible = false;
                    }
                }
                else
                {
                    Alerta("No se cuenta con permisos suficientes para acceder a la página");
                    Response.Redirect("Inicio.aspx");
                }

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Inicializar()
        {
            try
            {
                Nuevo();

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                
                

                string IdSol = Request.Params["Id1"] != null ? Request.Params["Id1"].ToString() : "";
                string IdEmp = Request.Params["Id2"] != null ? Request.Params["Id2"].ToString() : "";
                string IdCd = Request.Params["Id3"] != null ? Request.Params["Id3"].ToString() : "";

                if (sesion.Id_Emp.ToString() == IdEmp)
                {
                    if (sesion.Id_Cd_Ver.ToString() == IdCd)
                    {
                        if (IdSol != "")
                        {
                            int verificador = -1;
                            CN_PrecioEspecial cn_ape = new CN_PrecioEspecial();
                            PrecioEspecial ape = new PrecioEspecial();
                            ape.Id_Emp = sesion.Id_Emp;
                            ape.Id_Cd = sesion.Id_Cd_Ver;
                            ape.Ape_Unique = IdSol;

                            cn_ape.ConsultaProAutPrecioEspecial(ref ape, sesion.Emp_Cnx, ref verificador);

                            if (verificador == -1)
                            {
                                Alerta("No se encontro la solicitud");

                            }
                            else
                            {
                                HF_Tipo.Value = ape.Ape_Naturaleza;
                                lblSucursalId.Text = ape.Id_Cd.ToString();
                                lblSucursal.Text = "- " + ape.Cd_Nombre;
                                lblSolicitanteId.Text = ape.Id_U.ToString();
                                lblSolicitante.Text = "- " + ape.U_Nombre;
                                lblFolio.Text = ape.Id_Ape.ToString();
                                lblFecSol.Text = ape.Ape_Fecha.Value.ToString("dd/MM/yyyy hh:mm:ss tt");
                                txtNumConvenio.Text = ape.Ape_Convenio;
                                txtNumUsuario.Text = ape.Ape_NumUsuario;
              
                                txtNotaSol.Text = ape.Ape_Nota;
                                txtNotaResp.Text = ape.Ape_NotaResp;

                                cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue(ape.Ape_NumProveedor.ToString());
                                cmbProveedor.Text = cmbProveedor.FindItemByValue(ape.Ape_NumProveedor.ToString()).Text;
                                
                            }
                        }
                        else
                        {
                            Alerta("No se encontro la solicitud");
                        }
                    }
                    else
                    {
                        Alerta("La solicitud no pertenece al centro de distribución en el que se encuentra");
                    }
                }
                else
                {
                    Alerta("La solicitud no pertenece a la empresa en la que inicio sesión");
                }

                rgCliente.Rebind();
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        protected void FecIni_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                RadDatePicker rdp_FecIni = sender as RadDatePicker;
                RadDatePicker rdp_FecVigencia = (rdp_FecIni.Parent.FindControl("rdp_FecVigencia") as RadDatePicker);
                RadNumericTextBox txtPrecioAAAEsp = (rdp_FecVigencia.Parent.FindControl("txtPrecioAAAEsp") as RadNumericTextBox);

                if (rdp_FecIni.SelectedDate.HasValue && rdp_FecVigencia.SelectedDate.HasValue)
                {
                    if (rdp_FecIni.SelectedDate > rdp_FecVigencia.SelectedDate)
                    {
                        AlertaFocus("La fecha de fin debe ser mayor a la fecha de inicio", rdp_FecVigencia.DateInput.ClientID);
                        rdp_FecVigencia.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        txtPrecioAAAEsp.Focus();
                    }
                }
                else if (rdp_FecIni.SelectedDate.HasValue)
                {
                    if (rdp_FecIni.SelectedDate < sesion.CalendarioIni)
                    {
                        AlertaFocus("La fecha de inicio debe ser mayor o igual a la fecha de inicio del periodo actual", rdp_FecIni.DateInput.ClientID);
                        rdp_FecIni.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        rdp_FecVigencia.DateInput.Focus();
                    }
                }
                else
                {
                    rdp_FecIni.DateInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void FecVigencia_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                RadDatePicker rdp_FecVigencia = sender as RadDatePicker;
                RadDatePicker rdp_FecIni = (rdp_FecVigencia.Parent.FindControl("rdp_FecIni") as RadDatePicker);

                if (rdp_FecIni.SelectedDate.HasValue && rdp_FecVigencia.SelectedDate.HasValue)
                {
                    if (rdp_FecIni.SelectedDate > rdp_FecVigencia.SelectedDate)
                    {
                        AlertaFocus("La fecha de fin debe ser mayor a la fecha de inicio", rdp_FecVigencia.DateInput.ClientID);
                        rdp_FecVigencia.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        ((Telerik.Web.UI.GridDataItem)(rdp_FecVigencia.Parent.Parent))["EditCommandColumn"].Controls[0].Focus();
                    }
                }
                else if (rdp_FecIni.SelectedDate.HasValue)
                {
                    rdp_FecVigencia.DateInput.Focus();
                }
                else
                {
                    rdp_FecIni.DateInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Nuevo()
        {
            try
            {

                //Modificar... (faltan)
                lblSucursal.Text = string.Empty;
                HF_Tipo.Value = string.Empty;
                lblSucursalId.Text = string.Empty;
                lblSucursal.Text = string.Empty;
                lblSolicitanteId.Text = string.Empty;
                lblSolicitante.Text = string.Empty;
                lblFolio.Text = string.Empty;
                lblFecSol.Text = string.Empty;
                txtNotaSol.Text = string.Empty;

                //rg1.Rebind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<VentanaPrecioEspecialCte> GetListCte()
        {
            try
            {
                List<VentanaPrecioEspecialCte> List = new List<VentanaPrecioEspecialCte>();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string GUID = Convert.ToString(Page.Request.QueryString["Id1"]);
                if (GUID == "")                
                    GUID = "-1";
                
               
                CN_PrecioEspecial cn_ape = new CN_PrecioEspecial();
                PrecioEspecial ape = new PrecioEspecial();
                ape.Id_Emp = sesion.Id_Emp;
                ape.Id_Cd = sesion.Id_Cd_Ver;
                ape.Ape_Unique = GUID;
                cn_ape.ConsultaVentanaPrecioEspecialCte(ape, sesion.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        private List<VentanaPrecioEspecialPro> GetListPro()
        {
            try
            {
                List<VentanaPrecioEspecialPro> List = new List<VentanaPrecioEspecialPro>();
                
                string GUID = Convert.ToString(Page.Request.QueryString["Id1"]);
                if (GUID == "")               
                    GUID = "-1";

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_PrecioEspecial cn_ape = new CN_PrecioEspecial();
                PrecioEspecial ape = new PrecioEspecial();
                ape.Id_Emp = sesion.Id_Emp;
                ape.Id_Cd = sesion.Id_Cd_Ver;
                ape.Ape_Unique = GUID;
                ape.Accion = "4";
                cn_ape.ConsultaVentanaPrecioEspecialPro(ape, sesion.Emp_Cnx, ref List);
                return List;
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
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaDatos.Funciones funcion = new CapaDatos.Funciones();
                CN_PrecioEspecial clsPrecioEspecial = new CN_PrecioEspecial();
                var verificador = -1;
                List<VentanaPrecioEspecialPro> List = new List<VentanaPrecioEspecialPro>();
                VentanaPrecioEspecialPro ape_Prd = default(VentanaPrecioEspecialPro);
               
                for (int x = 0; x < rg1.Items.Count; x++)
                {                 
                    ape_Prd = new VentanaPrecioEspecialPro();
                    ape_Prd.Id_Prd = !string.IsNullOrEmpty(rg1.Items[x]["Id_Prd"].Text) ?  Convert.ToInt32(rg1.Items[x]["Id_Prd"].Text) : 0;
                    ape_Prd.Ape_VolVta = (rg1.Items[x].FindControl("txtVolVta") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((rg1.Items[x].FindControl("txtVolVta") as RadNumericTextBox).Text) : 0;
                    ape_Prd.Ape_PreVta = (rg1.Items[x].FindControl("txtPrecioVta") as RadNumericTextBox).Value.HasValue ? (double)(rg1.Items[x].FindControl("txtPrecioVta") as RadNumericTextBox).Value : 0;
                    ape_Prd.Ape_PreEsp = (rg1.Items[x].FindControl("txtPrecioAAAEsp") as RadNumericTextBox).Value.HasValue ? (double)(rg1.Items[x].FindControl("txtPrecioAAAEsp") as RadNumericTextBox).Value : 0;
                    if (!string.IsNullOrEmpty((rg1.Items[x]["Inicia"].FindControl("rdp_FecIni") as RadDatePicker).DbSelectedDate.ToString()))//(rg1.Items[x]["Inicia"].Text)))
                        ape_Prd.Ape_FecInicio = !string.IsNullOrEmpty((rg1.Items[x]["Inicia"].FindControl("rdp_FecIni") as RadDatePicker).DbSelectedDate.ToString()) ? Convert.ToDateTime((rg1.Items[x]["Inicia"].FindControl("rdp_FecIni") as RadDatePicker).DbSelectedDate.ToString()) : DateTime.MinValue;
                    else
                        ape_Prd.Ape_FecInicio = DateTime.MinValue;

                    if (!string.IsNullOrEmpty((rg1.Items[x]["Vigencia"].FindControl("rdp_FecVigencia") as RadDatePicker).DbSelectedDate.ToString()))//(rg1.Items[x]["Vigencia"].Text)))
                        ape_Prd.Ape_FecFin = !string.IsNullOrEmpty((rg1.Items[x]["Vigencia"].FindControl("rdp_FecVigencia") as RadDatePicker).DbSelectedDate.ToString()) ? Convert.ToDateTime((rg1.Items[x]["Vigencia"].FindControl("rdp_FecVigencia") as RadDatePicker).DbSelectedDate.ToString()) : DateTime.MinValue;
                    else
                        ape_Prd.Ape_FecFin = DateTime.MinValue;

                    if (ape_Prd.Id_Prd == 0 || ape_Prd.Ape_VolVta == 0 || ape_Prd.Ape_PreVta == 0 || ape_Prd.Ape_FecInicio == DateTime.MinValue || ape_Prd.Ape_FecFin == DateTime.MinValue)
                    {
                        Alerta("Todos los campos son requeridos en el grid");
                        return;
                    }     

                    if ((rg1.Items[x]["Autorizar"].FindControl("chkAutorizar") as RadioButton).Checked)                   
                        ape_Prd.Ape_Estatus = "A";                  
                    else
                        if ((rg1.Items[x]["Rechazado"].FindControl("chkRechazar") as RadioButton).Checked)                       
                            ape_Prd.Ape_Estatus = "R";                       
                        else    
                        {
                            Alerta("Todas las partidas deben ser autorizadas o rechazadas");
                            return;
                        }                       

                    if (ape_Prd.Ape_FecInicio > ape_Prd.Ape_FecFin)
                    {
                        Alerta("Revise los rangos de fechas de vigencia antes de guardar la aprobación");
                        return;
                    }
                    //if (Convert.ToInt32(ape_Prd.Ape_FecInicio.ToString("MM")) < 3 && Convert.ToInt32(ape_Prd.Ape_FecFin.ToString("MM")) > 3)
                    //{
                    //    Alerta("La vigencia solo puede ser hasta el mes de Marzo, si el inicio es antes de dicho mes");
                    //    return;
                    //}
                    //ape_Prd.Ape_PreEsp = !string.IsNullOrEmpty(rg1.Items[x]["Ape_PreEsp"].Text) ? Convert.ToDouble((rg1.Items[x]["Ape_PreEsp"].FindControl("txtPrecioAAAEsp") as RadNumericTextBox).Text) : 0.00;
                    //if (ape_Prd.Ape_PreEsp <= 0.00)
                    //{
                    //    Alerta("El precio AAA Especial no debe ser ");
                    //    return;
                    //}
                    ape_Prd.Ape_FecAut = funcion.GetLocalDateTime(sesion.Minutos);
                    List.Add(ape_Prd);                 
                }
               
                PrecioEspecial ape = new PrecioEspecial();
                ape.Id_Emp = sesion.Id_Emp;
                ape.Id_Cd = sesion.Id_Cd_Ver;
                ape.Id_Ape = Convert.ToInt32(lblFolio.Text);
                ape.Ape_NotaResp = txtNotaResp.Text;
                ape.Accion = HF_Tipo.Value;
                ape.Ape_Convenio = txtNumConvenio.Text;
                ape.Ape_NumProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                ape.Ape_NumUsuario = txtNumUsuario.Text;



                clsPrecioEspecial.AutorizarPrecioEspecial(ape, sesion.Emp_Cnx, List, ref verificador);

                if (verificador == 1)
                {
                    EnviaEmail();
                    Alerta("Se atendió correctamente la solicitud <b>#" + lblFolio.Text + "</b>");
                    Inicializar();
                }
                else                
                    Alerta("Ocurrió un error al intentar atender la solicitud #" + lblFolio.Text);              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviaEmail()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
               
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Emp = sesion.Id_Emp;
                configuracion.Id_Cd = sesion.Id_Cd_Ver;

                cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face= 'Tahoma' size = '2'>");
                cuerpo_correo.Append("La solicitud #" + lblFolio.Text + " ha sido atendida.");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br><a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProEntradaVirtual_Admin.aspx'>Solicitud de autorización de precios especiales</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(ConsultarEmail(Convert.ToInt32(lblSolicitanteId.Text))));
                m.Subject = "Confirmación de autorización de precios especiales";
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
                catch (Exception )
                {
                }
                m.AlternateViews.Add(vistaHtml);
                sm.Send(m);
            }
            catch (Exception )
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                //Throw ex

            }
        }

        protected void cmbProveedor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RequiredFieldValidator3.Enabled = Convert.ToInt32(cmbProveedor.SelectedValue) == 1 ? false : true;

            ErrorManager();

        }

        private void CargarCentros()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ConsultarEmail(int id_u)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario u = new Usuario();
            u.Id_Emp = sesion.Id_Emp;
            u.Id_Cd = sesion.Id_Cd_Ver;
            u.Id_U = id_u;
            string correo = "";
            cn_catusuario.ConsultaCorreoUsuario(u, sesion.Emp_Cnx, ref correo);
            return correo;
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
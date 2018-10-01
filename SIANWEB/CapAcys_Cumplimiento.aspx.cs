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
using System.IO;
using System.Collections;
using Telerik.Reporting.Processing;
using Telerik.Charting;

namespace SIANWEB
{
    public partial class CapAcys_Cumplimiento : System.Web.UI.Page
    {
       
        #region Variables
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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

        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];



                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
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
                        this.CargarCentros();
                        LblSitio.Text = Sesion.Cd_Nombre.ToString();

                        DateTime fechatemp = DateTime.Today;
                        DateTime fecha1, fecha2;
                            
                        fecha1= new DateTime(fechatemp.Year, fechatemp.Month, 1);                        

                        if (fechatemp.Month + 1 < 13)
                        { fecha2 = new DateTime(fechatemp.Year, fechatemp.Month + 1, 1).AddDays(-1); }
                        else
                        { fecha2 = new DateTime(fechatemp.Year + 1, 1, 1).AddDays(-1); }


                        for (int i = DateTime.Now.Year + 1; i > 2000; i--)
                        {
                            cmbAnioInicio.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                            cmbAnioFin.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                        }
                        cmbAnioInicio.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                        cmbAnioFin.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                        cmbAnioInicio.SelectedValue = fechatemp.Year.ToString();
                        cmbAnioFin.SelectedValue = fechatemp.Year.ToString();

                        cmbMesInicio.SelectedValue = fechatemp.Month.ToString();
                        cmbMesFin.SelectedValue = fechatemp.Month.ToString();

                        ConsultadeVentas();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        CargarCombos();
                      
                        this.TblEncabezado.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

           
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //this.rgFacturaRuta.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                string Evento = Request.Form["__EVENTTARGET"].ToString();

                if (!_PermisoImprimir)
                {
                    this.Alerta("No tiene permisos para ver el reporte");
                    return;
                }
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "Consultar")
                    {
                   
                        ConsultadeVentas();
                   
                    }

                }
            }
            catch (Exception ex)
            {
                Alerta("Imposible generar el reporte; aún no se han generado los respaldos del mes y año seleccionados");
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            //nuevo();
        }
        
        
        #endregion Eventos
        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                    _PermisoImprimir = Permiso.PImprimir;
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCombos()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();


                cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatTerritorio_ComboTodos", ref this.cmbTer);
                //cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCliente_ComboTodos", ref this.cmbCte);
                cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_ComboTodos", ref this.cmbRep);

                //this.TxtAnio.Text = DateTime.Now.Year.ToString();
                //this.TxtMes.Text = DateTime.Now.Month.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Imprimir()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                

                Type instance = null;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
       
        private void ConsultadeVentas()
        {

            try
            {
                DataTable dt = new DataTable();
                
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN__Comun cn_comun = new CN__Comun();

                //Variables para combos
                int Id_Ter; int Id_Rep;
                            
                if (cmbTer.SelectedValue == "" || cmbTer.SelectedValue == "-1")
                { Id_Ter= 0;}
                else { Id_Ter = Convert.ToInt32(cmbTer.SelectedValue); }

                if (cmbRep.SelectedValue == "" || cmbRep.SelectedValue == "-1")
                { Id_Rep = 0; }
                else { Id_Rep = Convert.ToInt32(cmbRep.SelectedValue); }
                
                //Variables para Fecha
    

                //Fecha_Ini = RadDatePicker1.SelectedDate.Value;
                //Fecha_Fin = RadDatePicker2.SelectedDate.Value;
                //string Fecha_Inicial = Fecha_Ini.Month.ToString() + '-' + Fecha_Ini.Day.ToString() + '-' + Fecha_Ini.Year.ToString();
                //string Fecha_Final   = Fecha_Fin.Month.ToString() + '-' + Fecha_Fin.Day.ToString() + '-' + Fecha_Fin.Year.ToString();

                int Anio_Ini = 0, Mes_Ini = 0, Anio_Fin = 0, Mes_Fin = 0;

                Anio_Ini = Convert.ToInt32(cmbAnioInicio.SelectedValue);
                Anio_Fin = Convert.ToInt32(cmbAnioFin.SelectedValue);
                Mes_Ini = Convert.ToInt32(cmbMesInicio.SelectedValue);
                Mes_Fin = Convert.ToInt32(cmbMesFin.SelectedValue);

                CN_CapAcys clsCapAcys = new CN_CapAcys();

                clsCapAcys.ConsultarAcys_Rpt_Cumplimiento(sesion.Id_Cd, Id_Ter, Id_Rep, Anio_Ini, Mes_Ini, Anio_Fin, Mes_Fin, ref dt, sesion.Emp_Cnx);
                
                int Cantidad_Registros = dt.Rows.Count;
                int VentaMes_Valor = 0; int VentaInstalada_Valor = 0; string FechaCorte="";

                
                foreach (DataRow row in dt.Rows) // Loop over the rows.
                {
                    VentaMes_Valor          = VentaMes_Valor + Convert.ToInt32(row["VtaMes"]);
                    VentaInstalada_Valor    = VentaInstalada_Valor + Convert.ToInt32(row["VtaInst"]);
                    FechaCorte              = Convert.ToString(row["Cal_FechaFin"]);
                }

                if (VentaMes_Valor == 0 )
                {
                    lblVentaMes_Valor.Text = "0";
                }
                else
                {
                    //lblVentaMes_Valor.Text = VentaMes_Valor.ToString("C2");
                    lblVentaMes_Valor.Text = VentaMes_Valor.ToString();
                }

                if (VentaInstalada_Valor == 0)
                {
                    lblVentaInstalada_Valor.Text = "0";
                }
                else
                {
                    //lblVentaInstalada_Valor.Text = VentaInstalada_Valor.ToString("C2");
                    lblVentaInstalada_Valor.Text = VentaInstalada_Valor.ToString();
                }

                if (FechaCorte == "")
                {
                    FechaCorte  = Convert.ToString(System.DateTime.Today);
                }

                //lblVentaMes_Valor.Text = "499000";         
                //lblVentaInstalada_Valor.Text = "309900";    

                //DateTime newDate = Convert.ToDateTime();
                DateTime oldDate = DateTime.Now;

                // Difference in days, hours, and minutes.
                TimeSpan ts = Convert.ToDateTime(FechaCorte) - oldDate;

                lblCierreDia.Text = ts.Days.ToString();
                lblCierreHora.Text = ts.Hours.ToString();
                lblCierreMinutos.Text = ts.Minutes.ToString();
            }
                
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GeneraGraficaActividad()
        {
  
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Session != null)
                {
                    //Función que crea el XML con los datos de los proyectos cancelados, proyectos activos
                    //y proyectos inactivos3 (que no tienen registrado seguimiento en el último mes) del representante, 
                    //mismos que serán interpretados por el componente para proceder a dibujar la gráfica.

                    DataSet dsGraficaActividad = new DataSet();
                    int intDdl = 1;
                    decimal porcentaje_vta = 0, venta_mes = 0;




                    if (Convert.ToDecimal(lblVentaMes_Valor.Text) < 0 || lblVentaMes_Valor.Text == "0") //
                    {
                        //decimal.Round((Convert.ToDecimal(lblVentaInstalada_Valor.Text) * Convert.ToDecimal(100) / Convert.ToDecimal(lblVentaInstalada_Valor.Text)), 2);
                        porcentaje_vta = 100;
                        //return "";
                    }
                    else
                    {
                        //( A/B ) - 1 Sacar porcentaje y cambiar el color del numero
                        porcentaje_vta = decimal.Round((Convert.ToDecimal(lblVentaInstalada_Valor.Text) * Convert.ToDecimal(100)) / Convert.ToDecimal(lblVentaMes_Valor.Text), 2);
                    }


                    venta_mes = decimal.Round(100 - porcentaje_vta, 2);


                    StringBuilder xmlData = new StringBuilder();

                    if (intDdl == 1)
                    {
                        intDdl = intDdl + 1;

                        xmlData.Append("<chart caption='" + LblSitio.Text.ToString() + "' captionFontSize='20' subcaption='Porcentaje Cumplimiento' subcaptionFontSize='10' pieyscale='50'  smartlinethickness='2' showpercentvalues='1' showpercentintooltip='0'  showlegend='1' legendshadow='1' legendborderalpha='0' showborder='0'>");
                        if (porcentaje_vta <= 39)
                        {
                            xmlData.Append("<set label='Venta Instalada' value='" + porcentaje_vta + "'color='#fc002a'  />");
                        }
                        if (porcentaje_vta >= 39 && porcentaje_vta <= 79)
                        {
                            xmlData.Append("<set label='Venta Instalada' value='" + porcentaje_vta + "'color='#fcfc00'  />");
                        }
                        if (porcentaje_vta >= 80)
                        {
                            xmlData.Append("<set label='Venta Instalada' value='" + porcentaje_vta + "'color='#1dfc00'  />");
                        }

                        xmlData.Append("<set  label='Venta Mes' value='" + venta_mes + "' color='#0082fc'  />");

                    }
                    xmlData.Append("</chart>");
                    lblVentaMes_Valor.Text = Convert.ToDecimal(lblVentaMes_Valor.Text).ToString("C2");

                    lblVentaInstalada_Valor.Text = Convert.ToDecimal(lblVentaInstalada_Valor.Text).ToString("C2");
                    return InfoSoftGlobal.FusionCharts.RenderChartHTML("FusionCharts/Pie3D.swf", "", xmlData.ToString(), "Pie", "500", "250", false);

                }

                else
                {
                    lblVentaMes_Valor.Text = Convert.ToDecimal(lblVentaMes_Valor.Text).ToString("C2");
                    lblVentaInstalada_Valor.Text = Convert.ToDecimal(lblVentaInstalada_Valor.Text).ToString("C2");
                    return "";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                ConsultadeVentas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion Funciones
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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion

     
    }
}
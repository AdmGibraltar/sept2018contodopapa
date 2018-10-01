using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Collections;
using System.Data;
using Telerik.Reporting.Processing;
using System.IO;

namespace SIANWEB
{
    public partial class Rep_VenRentabilidad : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoModificar { get { return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoGuardar { get { return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }
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

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();



                        int PeriodoFinal = 201409;
                        int PeriodoIndice = 0;
                        int PeriodoMes1 = 0;
                        int PeriodoMes2 = 0;
                        int PeriodoMes3 = 0;
                        int PeriodoActual = 0;
                        int PeriodoInicial = 0;
                        String Cero = "";

                        CN_CatCalendario cn_calenda = new CN_CatCalendario();
                        Calendario c = new Calendario();

                        //consultar periodo
                        cn_calenda.ConsultaCalendarioActual(ref c, Sesion);

                        if (c.Cal_Mes < 10)
                        {
                            Cero = "0";
                        }
                        else
                        {
                            Cero = "";
                        }

                        PeriodoActual = Convert.ToInt32(c.Cal_Año.ToString().Trim() + Cero + c.Cal_Mes.ToString().Trim());


/*                        if (c.Cal_Mes == 1)
                        {
                            PeriodoInicial = Convert.ToInt32((c.Cal_Año - 1).ToString().Trim() + "12");
                        }
                        else
                        {

                            if ((c.Cal_Mes - 1) < 10)
                            {
                                Cero = "0";
                            }
                            else
                            {
                                Cero = "";
                            }
                            PeriodoInicial = Convert.ToInt32(c.Cal_Año.ToString().Trim() + Cero + (c.Cal_Mes - 1).ToString().Trim());
                        }*/

                        PeriodoIndice = PeriodoActual;







                        while (PeriodoIndice >= PeriodoFinal)
                        {

                            RadComboBoxItem item1 = new RadComboBoxItem();
                            item1.Text = PeriodoIndice.ToString().Trim();
                            item1.Value = PeriodoIndice.ToString().Trim();
                            cmbPeriodoCierre.Items.Add(item1);

                            if (Convert.ToInt32(PeriodoIndice.ToString().Trim().Substring(4, 2)) - 1 == 0)
                            {
                                PeriodoIndice = Convert.ToInt32((Convert.ToInt32(PeriodoIndice.ToString().Trim().Substring(0, 4)) - 1).ToString().Trim() + "12");
                            }
                            else
                            {
                                PeriodoIndice--;
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        protected void cmbPeriodoCierre_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            String PeriodoMes1 = "";
            String PeriodoMes2 = "";
            String PeriodoMes3 = "";
            String Cero = "";
            String NombreMes = "";

            String Anio = "";
            String Mes = "";


            if (cmbPeriodoCierre.SelectedValue.ToString().Trim() != "")
            {
                Anio = cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString();
                Mes = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(4, 2).ToString())).ToString().Trim();

                if (Mes == "1") { NombreMes = "Enero"; }
                if (Mes == "2") { NombreMes = "Febrero"; }
                if (Mes == "3") { NombreMes = "Marzo"; }
                if (Mes == "4") { NombreMes = "Abril"; }
                if (Mes == "5") { NombreMes = "Mayo"; }
                if (Mes == "6") { NombreMes = "Junio"; }
                if (Mes == "7") { NombreMes = "Julio"; }
                if (Mes == "8") { NombreMes = "Agosto"; }
                if (Mes == "9") { NombreMes = "Septiembre"; }
                if (Mes == "10") { NombreMes = "Octubre"; }
                if (Mes == "11") { NombreMes = "Noviembre"; }
                if (Mes == "12") { NombreMes = "Diciembre"; }

                PeriodoMes1 = NombreMes + "-" + Anio;




                if ((Convert.ToInt32(cmbPeriodoCierre.SelectedValue.ToString().Substring(4, 2).ToString()) - 1) == 0)
                {
                    Anio = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString()) - 1).ToString().Trim();
                    Mes = "12";
                }
                else
                {
                    Anio = cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString();
                    Mes = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(4, 2).ToString()) - 1).ToString().Trim();
                }

                if (Mes == "1") { NombreMes = "Enero"; }
                if (Mes == "2") { NombreMes = "Febrero"; }
                if (Mes == "3") { NombreMes = "Marzo"; }
                if (Mes == "4") { NombreMes = "Abril"; }
                if (Mes == "5") { NombreMes = "Mayo"; }
                if (Mes == "6") { NombreMes = "Junio"; }
                if (Mes == "7") { NombreMes = "Julio"; }
                if (Mes == "8") { NombreMes = "Agosto"; }
                if (Mes == "9") { NombreMes = "Septiembre"; }
                if (Mes == "10") { NombreMes = "Octubre"; }
                if (Mes == "11") { NombreMes = "Noviembre"; }
                if (Mes == "12") { NombreMes = "Diciembre"; }

                PeriodoMes2 = NombreMes + "-" + Anio;


                if ((Convert.ToInt32(cmbPeriodoCierre.SelectedValue.ToString().Substring(4, 2).ToString()) - 2) <= 0)
                {
                    Anio = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString()) - 1).ToString().Trim();
                    if ((Convert.ToInt32(cmbPeriodoCierre.SelectedValue.ToString().Substring(4, 2).ToString()) - 2) <= -1)
                    {
                        Mes = "11";
                    }
                    else
                    {
                        Mes = "12";
                    }
                }
                else
                {
                    Anio = cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString();
                    Mes = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(4, 2).ToString()) - 2).ToString().Trim();
                }

                if (Mes == "1") { NombreMes = "Enero"; }
                if (Mes == "2") { NombreMes = "Febrero"; }
                if (Mes == "3") { NombreMes = "Marzo"; }
                if (Mes == "4") { NombreMes = "Abril"; }
                if (Mes == "5") { NombreMes = "Mayo"; }
                if (Mes == "6") { NombreMes = "Junio"; }
                if (Mes == "7") { NombreMes = "Julio"; }
                if (Mes == "8") { NombreMes = "Agosto"; }
                if (Mes == "9") { NombreMes = "Septiembre"; }
                if (Mes == "10") { NombreMes = "Octubre"; }
                if (Mes == "11") { NombreMes = "Noviembre"; }
                if (Mes == "12") { NombreMes = "Diciembre"; }


                PeriodoMes3 = NombreMes + "-" + Anio;
                MesesConsiderados.Text = "Periodos considerados : " + PeriodoMes3 + ", " + PeriodoMes2 + " y " + PeriodoMes1;
            }
            else
            {
                MesesConsiderados.Text = "";
            }

        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        if (Session["ReporteRentabilidadClientes" + Session.SessionID] != null)
                        {
                            if (Session["ReporteRentabilidadClientes" + Session.SessionID].ToString() == "SI")
                            {
                                this.Imprimir(true);
                                Session["ReporteRentabilidadClientes" + Session.SessionID] = null;
                                Session["ReporteValuacionProyecto" + Session.SessionID] = null;
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
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

                if (btn.CommandName == "print")
                {
                    Imprimir(true);
                }
                else
                {
                    Imprimir(false);
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
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

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                //this.CargarCliente();
                txtCliente.Text = string.Empty;
                //cmbCliente.SelectedIndex = cmbCliente.FindItemIndexByValue("-1");

                List<Territorios> listaTerr = new List<Territorios>();
                Territorios ter = new Territorios();
                ter.Descripcion = "-- Seleccionar --";
                ter.Id_Ter = -1;
                listaTerr.Insert(0, ter);
                txtTerritorio.Text = string.Empty;

            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        protected void cmbCliente_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (txtCliente.Value.Value.ToString() != "-1")
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    List<Territorios> listaTerritorios = new List<Territorios>();

                    //Consultar territorios relacionados con el cliente
                    new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1), sesion, ref listaTerritorios);

                }
                else
                {
                    List<Territorios> listaTerr = new List<Territorios>();
                    Territorios ter = new Territorios();
                    ter.Descripcion = "-- Seleccionar --";
                    ter.Id_Ter = -1;
                    listaTerr.Insert(0, ter);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbCliente_IndexChanging_error"));
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Value);
                cliente.Id_Rik = sesion.Id_Rik;
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    txtClienteNombre.Text = cliente.Cte_NomComercial;

                    //CargarComboTerritorios();
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    txtClienteNombre.Text = "";
                    txtCliente.Text = "";

                    return;
                }

                //CargarComboTerritorios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void Imprimir(bool a_pantalla)
        {
            try
            {

                /*Obtiene Información de Periodos*/



                if (cmbPeriodoCierre.SelectedValue.ToString().Trim() == "")
                {
                    Alerta("Favor de Capturar el Periodo");
                    cmbPeriodoCierre.Focus();
                }
                else
                {

                    String PeriodoMes1 = "";
                    String PeriodoMes2 = "";
                    String PeriodoMes3 = "";
                    String Cero = "";
                    String NombreMes = "";

                    String Anio = "";
                    String Mes = "";


                    String Mes1 = "";
                    String Mes2 = "";
                    String Mes3 = "";

                    String Anio1 = "";
                    String Anio2 = "";
                    String Anio3 = "";

                    if (cmbPeriodoCierre.SelectedValue.ToString().Trim() != "")
                    {
                        Anio = cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString();
                        Mes = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(4, 2).ToString())).ToString().Trim();

                        if (Mes == "1") { NombreMes = "Enero"; }
                        if (Mes == "2") { NombreMes = "Febrero"; }
                        if (Mes == "3") { NombreMes = "Marzo"; }
                        if (Mes == "4") { NombreMes = "Abril"; }
                        if (Mes == "5") { NombreMes = "Mayo"; }
                        if (Mes == "6") { NombreMes = "Junio"; }
                        if (Mes == "7") { NombreMes = "Julio"; }
                        if (Mes == "8") { NombreMes = "Agosto"; }
                        if (Mes == "9") { NombreMes = "Septiembre"; }
                        if (Mes == "10") { NombreMes = "Octubre"; }
                        if (Mes == "11") { NombreMes = "Noviembre"; }
                        if (Mes == "12") { NombreMes = "Diciembre"; }

                        PeriodoMes1 = NombreMes + "-" + Anio;
                        Mes1 = Mes;
                        Anio1 = Anio;



                        if ((Convert.ToInt32(cmbPeriodoCierre.SelectedValue.ToString().Substring(4, 2).ToString()) - 1) == 0)
                        {
                            Anio = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString()) - 1).ToString().Trim();
                            Mes = "12";
                        }
                        else
                        {
                            Anio = cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString();
                            Mes = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(4, 2).ToString()) - 1).ToString().Trim();
                        }

                        if (Mes == "1") { NombreMes = "Enero"; }
                        if (Mes == "2") { NombreMes = "Febrero"; }
                        if (Mes == "3") { NombreMes = "Marzo"; }
                        if (Mes == "4") { NombreMes = "Abril"; }
                        if (Mes == "5") { NombreMes = "Mayo"; }
                        if (Mes == "6") { NombreMes = "Junio"; }
                        if (Mes == "7") { NombreMes = "Julio"; }
                        if (Mes == "8") { NombreMes = "Agosto"; }
                        if (Mes == "9") { NombreMes = "Septiembre"; }
                        if (Mes == "10") { NombreMes = "Octubre"; }
                        if (Mes == "11") { NombreMes = "Noviembre"; }
                        if (Mes == "12") { NombreMes = "Diciembre"; }

                        PeriodoMes2 = NombreMes + "-" + Anio;
                        Mes2 = Mes;
                        Anio2 = Anio;

                        if ((Convert.ToInt32(cmbPeriodoCierre.SelectedValue.ToString().Substring(4, 2).ToString()) - 2) <= 0)
                        {
                            Anio = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString()) - 1).ToString().Trim();
                            if ((Convert.ToInt32(cmbPeriodoCierre.SelectedValue.ToString().Substring(4, 2).ToString()) - 2) <= -1)
                            {
                                Mes = "11";
                            }
                            else
                            {
                                Mes = "12";
                            }
                        }
                        else
                        {
                            Anio = cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString();
                            Mes = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(4, 2).ToString()) - 2).ToString().Trim();
                        }

                        if (Mes == "1") { NombreMes = "Enero"; }
                        if (Mes == "2") { NombreMes = "Febrero"; }
                        if (Mes == "3") { NombreMes = "Marzo"; }
                        if (Mes == "4") { NombreMes = "Abril"; }
                        if (Mes == "5") { NombreMes = "Mayo"; }
                        if (Mes == "6") { NombreMes = "Junio"; }
                        if (Mes == "7") { NombreMes = "Julio"; }
                        if (Mes == "8") { NombreMes = "Agosto"; }
                        if (Mes == "9") { NombreMes = "Septiembre"; }
                        if (Mes == "10") { NombreMes = "Octubre"; }
                        if (Mes == "11") { NombreMes = "Noviembre"; }
                        if (Mes == "12") { NombreMes = "Diciembre"; }


                        PeriodoMes3 = NombreMes + "-" + Anio;
                        Mes3 = Mes;
                        Anio3 = Anio;
                        MesesConsiderados.Text = "Periodos considerados : " + PeriodoMes3 + ", " + PeriodoMes2 + " y " + PeriodoMes1;
                    }
                    else
                    {
                        MesesConsiderados.Text = "";
                    }

                    /*Obtiene Información de Periodos*/

                    if (txtTerritorio.Text.Trim() == "")
                    {
                        Alerta("Favor de Capturar el Territoriio");
                        txtTerritorio.Focus();

                    }
                    else
                    {


                        Sesion Sesion = new Sesion();
                        Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                        CN_CatCalendario cn_calenda = new CN_CatCalendario();
                        Calendario c = new Calendario();
                        cn_calenda.ConsultaCalendarioActual(ref c, Sesion);


                        if (c.Cal_Año.ToString().Trim() == Anio1 && c.Cal_Mes.ToString().Trim() == Mes1) // 1==1
                        {


                            /* aqui if(Mes1 = ) */

                            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                            ArrayList ALValorParametrosInternos = new ArrayList();

                            CentroDistribucion cd = new CentroDistribucion();
                            new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                            //Obtener datos de encabezado de la Valuación de proyecto
                            string Id_Cte = string.Empty, Cte_NomComercial = string.Empty, Vap_Nota = string.Empty;

                            Id_Cte = Convert.ToString(txtCliente.Value.HasValue ? txtCliente.Value : -1); //cmbCliente.SelectedValue;
                            Cte_NomComercial = txtClienteNombre.Text; //cmbCliente.SelectedItem.Text;
                            int? territorio = null;
                            if (txtTerritorio.Text != "")
                            {
                                territorio = Convert.ToInt32(txtTerritorio.Text);
                            }
                            else
                            {
                                territorio = 0;
                            }

                            DataTable dtReporteTotales = new DataTable();
                            new CN_CatCliente().ReporteRentabilidad_ConsultarTotales(
                                sesion.Id_Emp
                                , sesion.Id_Cd_Ver
                                , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                                , territorio
                                , cmbPeriodo.SelectedValue
                                , cmbVentas.SelectedValue
                                , ref dtReporteTotales
                                , sesion.Emp_Cnx);

                            ALValorParametrosInternos.Add(sesion.Id_Emp);
                            ALValorParametrosInternos.Add(sesion.Emp_Nombre);
                            ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                            ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                            ALValorParametrosInternos.Add(Id_Cte);
                            ALValorParametrosInternos.Add(Cte_NomComercial);
                            ALValorParametrosInternos.Add(txtTerritorio.Text);
                            ALValorParametrosInternos.Add(txtTerritorio.Text != "-1" ? txtTerritorio.Text : "Todos");
                            ALValorParametrosInternos.Add(cmbPeriodo.SelectedValue);
                            ALValorParametrosInternos.Add(cmbVentas.SelectedValue);
                            ALValorParametrosInternos.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                            ALValorParametrosInternos.Add(DateTime.Now.ToString("hh:MM:ss"));
                            ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                            double VentaNeta = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNeta"]);
                            double VentaNetaPapel = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNetaPapel"]);
                            double VentaNetaOtros = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNetaOtros"]);
                            double CostoMaterial = Convert.ToDouble(dtReporteTotales.Rows[0]["CostoMaterial"]);
                            double CostoMaterialNOPapel = Convert.ToDouble(dtReporteTotales.Rows[0]["CostoMaterialNOPapel"]);
                            double FactorFijos = 0;
                            double FactorUCS = 0;


                            Territorios CatTer = new Territorios();
                            CatTer.Id_Emp = sesion.Id_Emp;
                            CatTer.Id_Cd = sesion.Id_Cd;
                            CatTer.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                            new CN_CatTerritorios().ConsultaTerritorios(ref CatTer, sesion.Emp_Cnx);


                                if (VentaNeta < 5000) FactorFijos = 17.5;
                                if (VentaNeta >= 5000 && VentaNeta < 10000) FactorFijos = 16.84;
                                if (VentaNeta >= 10000 && VentaNeta < 15000) FactorFijos = 16.18;
                                if (VentaNeta >= 15000 && VentaNeta < 20000) FactorFijos = 15.53;
                                if (VentaNeta >= 20000 && VentaNeta < 25000) FactorFijos = 14.87;
                                if (VentaNeta >= 25000 && VentaNeta < 30000) FactorFijos = 14.21;
                                if (VentaNeta >= 30000 && VentaNeta < 35000) FactorFijos = 13.55;
                                if (VentaNeta >= 35000 && VentaNeta < 40000) FactorFijos = 12.89;
                                if (VentaNeta >= 40000 && VentaNeta < 45000) FactorFijos = 12.24;
                                if (VentaNeta >= 45000 && VentaNeta < 50000) FactorFijos = 11.58;
                                if (VentaNeta >= 50000 && VentaNeta < 55000) FactorFijos = 10.92;
                                if (VentaNeta >= 55000 && VentaNeta < 60000) FactorFijos = 10.26;
                                if (VentaNeta >= 60000 && VentaNeta < 65000) FactorFijos = 9.61;
                                if (VentaNeta >= 65000 && VentaNeta < 70000) FactorFijos = 8.95;
                                if (VentaNeta >= 70000 && VentaNeta < 75000) FactorFijos = 8.29;
                                if (VentaNeta >= 75000 && VentaNeta < 80000) FactorFijos = 7.63;
                                if (VentaNeta >= 80000 && VentaNeta < 85000) FactorFijos = 6.97;
                                if (VentaNeta >= 85000 && VentaNeta < 90000) FactorFijos = 6.32;
                                if (VentaNeta >= 90000 && VentaNeta < 100000) FactorFijos = 5.66;
                                if (VentaNeta >= 100000) FactorFijos = 5.0;
                            

                            if (VentaNeta < 5000) FactorUCS = 3.5;
                            if (VentaNeta >= 5000 && VentaNeta < 10000) FactorUCS = 3.0;
                            if (VentaNeta >= 10000 && VentaNeta < 25000) FactorUCS = 2.5;
                            if (VentaNeta >= 25000 && VentaNeta < 50000) FactorUCS = 2;
                            if (VentaNeta >= 50000 && VentaNeta < 100000) FactorUCS = 1.5;
                            if (VentaNeta >= 100000) FactorUCS = 1;


                            double AmortizacionTotal = Convert.ToDouble(dtReporteTotales.Rows[0]["AmortizacionTotal"]);
                            double Prd_PesConTecnico = Convert.ToDouble(dtReporteTotales.Rows[0]["Prd_PesConTecnico"]);
                            double UtilidadBruta = Convert.ToDouble(dtReporteTotales.Rows[0]["UtilidadBruta"]);

                            double Cte_CarMP = Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_CarMP"]);
                            double Cte_GasVarT = Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_GasVarT"]);
                            double Cte_FletePaga = 0; //Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_FletePaga"]);
                            double DiasRotacion = Convert.ToDouble(dtReporteTotales.Rows[0]["DiasRotacion"]);

                            string listaClavesProd = dtReporteTotales.Rows[0]["Id_PrdStr"].ToString();

                            // ------------------------------------------------
                            // calcular amortizacion de cada producto
                            // ------------------------------------------------
                            if (listaClavesProd.Length > 0) listaClavesProd = listaClavesProd.Substring(0, listaClavesProd.Length - 1);
                            string[] arrayId_Prd = listaClavesProd.Split(new char[] { ',' });
                            //objeto amortizacion
                            Amortizacion amortizacion = new Amortizacion();
                            amortizacion.Id_Emp = sesion.Id_Emp;
                            amortizacion.Id_Cd = sesion.Id_Cd_Ver;
                            amortizacion.Id_Cte = Convert.ToInt32(Id_Cte);
                            amortizacion.Id_Ter = Convert.ToInt32(txtTerritorio.Text != "" ? txtTerritorio.Text : "0");

                            //obtener productos con amortización del producto
                            int anioActual = DateTime.Now.Year;
                            int mesActual = DateTime.Now.Month;


                            //calcular financiamiento de proveedores
                            double financiamientoProveedores = (((((CostoMaterial / 30) * cd.Cd_Dias.Value) / cd.Cd_Dias.Value) * cd.Cd_DiasFinanciaProv) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)));
                            if (double.IsNaN(financiamientoProveedores) || double.IsInfinity(financiamientoProveedores))
                            {
                                financiamientoProveedores = 0;
                            }


                            double TotalInversionComodatos = 0;
                            new CN_Amortizacion().ConsultaInversionComodato(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtTerritorio.Text != "" ? txtTerritorio.Text : "0"), Convert.ToInt32(Id_Cte), sesion.Emp_Cnx, ref TotalInversionComodatos);

                            //calcular inversion total en activos
                            double inversionTotalActivos
                                = (((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)))
                                + ((CostoMaterial / 30) * cd.Cd_Dias.Value)
                                + ((CostoMaterial / 30) * cd.Cd_DiasInv.Value)
                                //+ (TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato))
                                + ((VentaNeta / 30) * cd.Cd_FactorConvActFijo.Value);
                            if (double.IsNaN(inversionTotalActivos) || double.IsInfinity(inversionTotalActivos))
                            {
                                inversionTotalActivos = 0;
                            }

                            //calcular utilidad bruta
                            UtilidadBruta =
                                VentaNeta
                                - CostoMaterial
                                - Cte_CarMP
                                - (/*CostoMaterial*/ CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)) //flete
                                - AmortizacionTotal
                                - Prd_PesConTecnico;
                            if (double.IsNaN(UtilidadBruta) || double.IsInfinity(UtilidadBruta))
                            {
                                UtilidadBruta = 0;
                            }

                            double UtilidadMarginal = 0;
                            
                            //calcular utilidad marginal
                            if (CatTer.Id_TipoRepresentante == 4)
                            {
                                UtilidadMarginal =
                                    UtilidadBruta
                                    - (UtilidadBruta * 10 / 100)
                                    - Cte_GasVarT
                                    //- (VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100))
                                    - Cte_FletePaga;
                            }
                            else
                            {
                                UtilidadMarginal =
                                    UtilidadBruta
                                    - (UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100))
                                    - Cte_GasVarT
                                    //- (VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100))
                                    - Cte_FletePaga;
                            }




                            if (double.IsNaN(UtilidadMarginal) || double.IsInfinity(UtilidadMarginal))
                            {
                                UtilidadMarginal = 0;
                            }

                            //calcular Uafir mensual
                            double UafirMensual =
                                UtilidadMarginal
                                /*                        - (VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100))
                                                          - (VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))   
                                                          - (VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)); */
                                - (VentaNeta * (Convert.ToSingle(FactorFijos) / 100))
                                - (VentaNeta * (Convert.ToSingle(FactorUCS) / 100));
                            if (double.IsNaN(UafirMensual) || double.IsInfinity(UafirMensual))
                            {
                                UafirMensual = 0;
                            }

                            //calcular Costo de capital
                            double CostoCapital = (Math.Round(inversionTotalActivos, 2) - financiamientoProveedores) * (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value) / 100;
                            if (double.IsNaN(CostoCapital) || double.IsInfinity(CostoCapital))
                            {
                                CostoCapital = 0;
                            }
                            //calcular Uafir después de impuestos
                            double UafirDespuesImpuestos = (UafirMensual * 12) - ((UafirMensual * 12) * (Convert.ToSingle(cd.Cd_ISRyPTU) / 100));
                            if (double.IsNaN(UafirDespuesImpuestos) || double.IsInfinity(UafirDespuesImpuestos))
                            {
                                UafirDespuesImpuestos = 0;
                            }


                            //calcular porcentaje de utilidad remanente
                            double UtilidadRemanentePorc = (UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100) - (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value);
                            if (double.IsNaN(UtilidadRemanentePorc) || double.IsInfinity(UtilidadRemanentePorc))
                            {
                                UtilidadRemanentePorc = 0;
                            }

                            ALValorParametrosInternos.Add(DiasRotacion); //txtCtaCobrarPorc
                            ALValorParametrosInternos.Add(cd.Cd_Dias); //"txtInvDiasCant"
                            ALValorParametrosInternos.Add(cd.Cd_DiasInv); //"txtInvConsigDiasCant"
                            ALValorParametrosInternos.Add(UtilidadRemanentePorc / 100); //"txtUtilidadRemanentePorc"

                            double ctaPorCobrar = ((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100));
                            if (double.IsNaN(ctaPorCobrar) || double.IsInfinity(ctaPorCobrar))
                            {
                                ctaPorCobrar = 0;
                            }
                            ALValorParametrosInternos.Add(ctaPorCobrar); //"txtCtaCobrar"

                            ALValorParametrosInternos.Add((CostoMaterial / 30) * cd.Cd_Dias); //"txtInvDias"
                            ALValorParametrosInternos.Add(cd.Cd_FactorInvComodato); //"txtInvComodatoOtrosProdCant"
                            ALValorParametrosInternos.Add(TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato)); //"txtInvComodatoOtrosProd"
                            ALValorParametrosInternos.Add((CostoMaterial / 30) * cd.Cd_DiasInv); //"txtInvConsigDias"

                            ALValorParametrosInternos.Add(financiamientoProveedores); //"txtFinanProv"

                            ALValorParametrosInternos.Add(inversionTotalActivos - financiamientoProveedores); //"txtInvActivosNetos" OP'N
                            ALValorParametrosInternos.Add(inversionTotalActivos); //"txtInvTotalActivos"
                            ALValorParametrosInternos.Add((VentaNeta / 30) * cd.Cd_FactorConvActFijo); //"txtInvActivosFijos"
                            ALValorParametrosInternos.Add(UtilidadRemanentePorc / 100 * (inversionTotalActivos - financiamientoProveedores)); //"txtUtilidadRemanente"

                            double txtUafirActivos = UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100;
                            if (double.IsNaN(txtUafirActivos) || double.IsInfinity(txtUafirActivos))
                            {
                                txtUafirActivos = 0;
                            }
                            ALValorParametrosInternos.Add(txtUafirActivos / 100); //"txtUafirActivos"

                            ALValorParametrosInternos.Add(Convert.ToSingle(cd.Cd_TasaCetes + cd.Cd_TasaIncCostoCapital) / 100); //"txtCostoCapital"
                            ALValorParametrosInternos.Add(VentaNeta); //"txtVentaNetaMon"
                            ALValorParametrosInternos.Add(CostoMaterial); //"txtCostoMaterialMon"
                            ALValorParametrosInternos.Add(/*CostoMaterial*/CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)); //"txtFleteMon"
                            ALValorParametrosInternos.Add(Cte_CarMP); //"txtManoObraMon"

                            ALValorParametrosInternos.Add(UtilidadBruta); //"txtUtilidadMon"
                            ALValorParametrosInternos.Add(Prd_PesConTecnico); //"txtCostoServEquipoMon"
                            ALValorParametrosInternos.Add(AmortizacionTotal); //"txtAmortizacionMon"
                            if (CatTer.Id_TipoRepresentante == 4)
                            {
                            ALValorParametrosInternos.Add((UtilidadBruta * 10 / 100)); //"txtComisionRepMon"
                                                        } else {
                            ALValorParametrosInternos.Add(UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100)); //"txtComisionRepMon"
                                                        }

                            ALValorParametrosInternos.Add(VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100)); //"txtContribucionGastosFijosOtrosMon"
                            ALValorParametrosInternos.Add(VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100)); //"txtContribucionGastosFijosPapelMon"
                            ALValorParametrosInternos.Add(UafirMensual); //"txtUafirMensualMon"
                            ALValorParametrosInternos.Add(VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)); //"txtCargoUCSMon"
                            ALValorParametrosInternos.Add(Cte_FletePaga); //"txtFletesPagadosMon"
                            ALValorParametrosInternos.Add(0); //"txtOtrosGastosVariablesMon"  VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100)
                            ALValorParametrosInternos.Add(0); //"txtGastosVariablesMon" Cte_GasVarT
                            ALValorParametrosInternos.Add(UafirMensual * 12); //"txtUafirAnualMon"

                            ALValorParametrosInternos.Add(CostoCapital); //"txtCostoCapitalMon"

                            ALValorParametrosInternos.Add(UafirDespuesImpuestos - CostoCapital); //"txtUtilidadRemanenteMon"
                            ALValorParametrosInternos.Add(UafirDespuesImpuestos); //"txtUafirDespuesImpMon"
                            ALValorParametrosInternos.Add(cd.Cd_ISRyPTU); //"txtISRyPTU"
                            double txtISRyPTUMon = (UafirMensual * 12) * (Convert.ToSingle(cd.Cd_ISRyPTU) / 100);
                            if (double.IsNaN(txtISRyPTUMon) || double.IsInfinity(txtISRyPTUMon))
                            {
                                txtISRyPTUMon = 0;
                            }
                            ALValorParametrosInternos.Add(txtISRyPTUMon); //"txtISRyPTUMon"

                            //ALValorParametrosInternos.Add(-1); //"txtISRyPTUPorc"
                            //ALValorParametrosInternos.Add(-1); //"txtUafirDespuesImpPorc"
                            //ALValorParametrosInternos.Add(-1); //"txtUtilidadRemanentePorc2"
                            //ALValorParametrosInternos.Add(-1); //"txtCostoCapitalPorc"
                            //ALValorParametrosInternos.Add(-1); //"txtUafirAnualPorc"
                            double txtGastosVariablesPorc = (Cte_GasVarT / VentaNeta) * 100;
                            if (double.IsNaN(txtGastosVariablesPorc) || double.IsInfinity(txtGastosVariablesPorc))
                            {
                                txtGastosVariablesPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtGastosVariablesPorc / 100); //"txtGastosVariablesPorc"
                            double txtOtrosGastosVariablesPorc = 0; //((VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100)) / VentaNeta) * 100;
                            if (double.IsNaN(txtOtrosGastosVariablesPorc) || double.IsInfinity(txtOtrosGastosVariablesPorc))
                            {
                                txtOtrosGastosVariablesPorc = 0;
                            }
                            ALValorParametrosInternos.Add(0); //"txtOtrosGastosVariablesPorc"  txtOtrosGastosVariablesPorc / 100)
                            double txtFletesPagadosPorc = (Cte_FletePaga / VentaNeta) * 100;
                            if (double.IsNaN(txtFletesPagadosPorc) || double.IsInfinity(txtFletesPagadosPorc))
                            {
                                txtFletesPagadosPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtFletesPagadosPorc / 100); //"txtFletesPagadosPorc"
                            double txtCargoUCSPorc = ((VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)) / VentaNeta) * 100;
                            if (double.IsNaN(txtCargoUCSPorc) || double.IsInfinity(txtCargoUCSPorc))
                            {
                                txtCargoUCSPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtCargoUCSPorc / 100); //"txtCargoUCSPorc"
                            double txtUafirMensualPorc = (UafirMensual / VentaNeta) * 100;
                            if (double.IsNaN(txtUafirMensualPorc) || double.IsInfinity(txtUafirMensualPorc))
                            {
                                txtUafirMensualPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtUafirMensualPorc / 100); //"txtUafirMensualPorc"
                            double txtContribucionGastosFijosPapelPorc = (/*(VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))*/(VentaNeta * (Convert.ToSingle(FactorFijos) / 100)) / VentaNeta) * 100;
                            if (double.IsNaN(txtContribucionGastosFijosPapelPorc) || double.IsInfinity(txtContribucionGastosFijosPapelPorc))
                            {
                                txtContribucionGastosFijosPapelPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtContribucionGastosFijosPapelPorc / 100); //"txtContribucionGastosFijosPapelPorc"
                            double txtContribucionGastosFijosOtrosPorc = ((VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100)) / VentaNeta) * 100;
                            if (double.IsNaN(txtContribucionGastosFijosOtrosPorc) || double.IsInfinity(txtContribucionGastosFijosOtrosPorc))
                            {
                                txtContribucionGastosFijosOtrosPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtContribucionGastosFijosOtrosPorc / 100); //"txtContribucionGastosFijosOtrosPorc"
                            double txtAmortizacionPorc = (AmortizacionTotal / VentaNeta) * 100;
                            if (double.IsNaN(txtAmortizacionPorc) || double.IsInfinity(txtAmortizacionPorc))
                            {
                                txtAmortizacionPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtAmortizacionPorc / 100); //"txtAmortizacionPorc"
                            double txtCostoServEquipoPorc = (Prd_PesConTecnico / VentaNeta) * 100;
                            if (double.IsNaN(txtCostoServEquipoPorc) || double.IsInfinity(txtCostoServEquipoPorc))
                            {
                                txtCostoServEquipoPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtCostoServEquipoPorc / 100); //"txtCostoServEquipoPorc"

                            double txtComisionRepPorc = 0;

                            if (CatTer.Id_TipoRepresentante == 4)
                            {
                                txtComisionRepPorc = ((UtilidadBruta * 10 / 100) / VentaNeta) * 100;
                            } else {
                                txtComisionRepPorc = ((UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100)) / VentaNeta) * 100;
                            }

                            if (double.IsNaN(txtComisionRepPorc) || double.IsInfinity(txtComisionRepPorc))
                            {
                                txtComisionRepPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtComisionRepPorc / 100); //"txtComisionRepPorc"
                            double txtUtilidadPorc = (UtilidadBruta / VentaNeta) * 100;
                            if (double.IsNaN(txtUtilidadPorc) || double.IsInfinity(txtUtilidadPorc))
                            {
                                txtUtilidadPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtUtilidadPorc / 100); //"txtUtilidadPorc"
                            double txtManoObraPorc = (Cte_CarMP / VentaNeta) * 100;
                            if (double.IsNaN(txtManoObraPorc) || double.IsInfinity(txtManoObraPorc))
                            {
                                txtManoObraPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtManoObraPorc / 100); //"txtManoObraPorc"

                            ALValorParametrosInternos.Add(cd.Cd_Flete); //"txtFletePorc"

                            double txtFletePorc2 = ((/*CostoMaterial*/CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)) / VentaNeta) * 100;
                            if (double.IsNaN(txtFletePorc2) || double.IsInfinity(txtFletePorc2))
                            {
                                txtFletePorc2 = 0;
                            }
                            ALValorParametrosInternos.Add(txtFletePorc2 / 100); //"txtFletePorc2"
                            double txtCostoMaterialPorc = (CostoMaterial / VentaNeta) * 100;
                            if (double.IsNaN(txtCostoMaterialPorc) || double.IsInfinity(txtCostoMaterialPorc))
                            {
                                txtCostoMaterialPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtCostoMaterialPorc / 100); //"txtCostoMaterialPorc"


                            ALValorParametrosInternos.Add(UtilidadMarginal); //"txtUtilidadMarginalMon"
                            double txtUtilidadMarginalPorc = (UtilidadMarginal / VentaNeta) * 100;
                            if (double.IsNaN(txtUtilidadMarginalPorc) || double.IsInfinity(txtUtilidadMarginalPorc))
                            {
                                txtUtilidadMarginalPorc = 0;
                            }
                            ALValorParametrosInternos.Add(txtUtilidadMarginalPorc / 100); //"txtUtilidadMarginalPorc"

                            //Formular HTML
                            String CadenaHtml;


                            CadenaHtml = "";

                            CadenaHtml = CadenaHtml + "        <table width=\"100%\">";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td colspan=\"4\" align=\"center\" style=\"font-family: Verdana; font-size: 12pt;background-color: #A9BCF5\"><b>Determinación de la inversión en activos netos de la operación</b></td>";
                            CadenaHtml = CadenaHtml + "                <td colspan=\"5\" align=\"center\" style=\"font-family: Verdana; font-size: 12pt;background-color: #A9BCF5\"><b>Cálculo del Uafir anual después de impuestos</b></td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Son los Días de Crédito que tiene el Cliente Autorizado en el Sistema\">Cuentas por cobrar [" + String.Format("{0:N}", DiasRotacion) + "]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", ctaPorCobrar) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula :  (Facturas Notas de Cargo-Cancelaciones de Facturas Operadas – Notas de Crédito) Promedio Calculada del Periodo Seleccionado\">Venta neta</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", VentaNeta) + "</a></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">100.00%</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default de 20 Días;Formula : (Costo de Material/30)* 25\">Inventario (Días) [" + String.Format("{0:N}", cd.Cd_Dias) + "]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", Convert.ToDouble(((CostoMaterial / 30) * cd.Cd_Dias))) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Son las Ventas Promedio del Periodo Seleccionado evaluadas al Costo AAA.\">Costo de material</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", CostoMaterial) + "</a></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((CostoMaterial / VentaNeta) * 100)) + "%</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"En Construcción\">Inventario en consignación (Días) [0.00]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$0.00</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Venta Neta - Costo Material\">Utilidad Prima</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", (VentaNeta - CostoMaterial)) + "</a></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", (((VentaNeta - CostoMaterial) / VentaNeta) * 100)) + "%</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            //CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default de 0.5;(Costo de los Equipos en Comodato)* 0.5\">Inversión en Equipo Comodato  (costo*" + String.Format("{0:N}", cd.Cd_FactorInvComodato) + ")</td>";
                            //CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", (TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato))) + "</a></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 3.1;Formula : (Venta Neta del Periodo/30)*3.1\">Inversión en activos fijos [" + String.Format("{0:N}", cd.Cd_FactorConvActFijo.Value) + "]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((VentaNeta / 30) * cd.Cd_FactorConvActFijo.Value)) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Importe identificado por Cliente y Territorio de Mano de Obra en Proyectos.\">Mano de obra en proyectos</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", Cte_CarMP) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Cuentas por Cobrar+ Inventario(Días)+ Inventario en Consignación (Días)+ Inversión en Activos Fijos\"><b>Inversión total en activos</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", inversionTotalActivos) + "</b></td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 3.0;Formula : (Costo de Material de los Productos que NO sin Papel * (3.0)/100)\">Flete al CD [" + String.Format("{0:N}", cd.Cd_Flete) + "]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (/*CostoMaterial*/CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100))) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 45;(Costo de Material/30)* 45 Más IVA del CD.\">Financiamiento de proveedores (" + String.Format("{0:N}", cd.Cd_DiasFinanciaProv) + " Días)</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", financiamientoProveedores) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Importe de Amortización del Territorio y Cliente Seleccionado a la Fecha generada el reporte\">Amortización en Equipos</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", AmortizacionTotal) + "</a></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default Cetes : 5.00 y Tasa Incremental al Costo Capital : 15.00;Formula : (Inversión en activos netos op'n - Financiamiento de Proveedores)  * ( (5.00)+ (15.00) )/100\">Costo de capital [" + String.Format("{0:N}", (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value)) + "%]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", CostoCapital) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Estadística de Base Instalada del Territorio y Cliente Seleccionado a la Fecha generada el reporte evaluada a los Pesos por Equipo.\">Costo Servicio a Equipos</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", Prd_PesConTecnico) + "</a></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";

                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            //CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Inversión Total en Activos- Financiamiento de Proveedores\"><b>Inversión en activos netos op'n</b></td>";
                            //CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Inversión Total en Activos\"><b>Inversión en activos netos op'n</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\">&nbsp;</td>";
                            //CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (inversionTotalActivos - financiamientoProveedores)) + "</b></td>";
                            //CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (inversionTotalActivos)) + "</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Venta Neta – (Costo de Material+ Mano de Obra en Proyectos+ Flete+ Amortización en Equipos+ Costo Servicio Equipos)\"><b>Utilidad bruta</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UtilidadBruta) + "<b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((UtilidadBruta / VentaNeta) * 100)) + "%</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            if (CatTer.Id_TipoRepresentante == 4)
                            {
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se definio fijo por default 20 En el Caso de Territorios Gerenciales es 10 ;Formula : (Utilidad Bruta* (20 o 10/100))\">Gastos de Servir al Cliente [" + String.Format("{0:N}", 10) + "]</td>";
                            } else {
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se definio fijo por default 20 En el Caso de Territorios Gerenciales es 10 ;Formula : (Utilidad Bruta* (20 o 10/100))\">Gastos de Servir al Cliente [" + String.Format("{0:N}", cd.Cd_ComisionRik) + "]</td>";
                            }
                            if (CatTer.Id_TipoRepresentante == 4)
                            {
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (UtilidadBruta * 10 / 100)) + "</td>";
                            } else {
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100))) + "</td>";
                            }
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            //  Eliminado por Autorización de Eugenio             CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : (Uafir Después de Impuesto/ Inversión en Activos Netos op’n)*100\">Uafir después de Impuestos/Total de Activos</td>";
                            //                                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", txtUafirActivos) + "%</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Gastos Variables Aplicados al Terr \">Gastos var. aplicados al terr</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (Cte_GasVarT /*/ VentaNeta*/)) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            //  Eliminado por Autorización de Eugenio                             CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula %: (Uafir Después de Impuestos / (Inversión en Activos Netos op’n)*100)- (Tasa Cetes+ Tasa Incremental Costo Capital)\"><b>Utilidad remanente [" + String.Format("{0:N}",UtilidadRemanentePorc)  + "%]</b></td>";
                            //                                                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}",(UafirDespuesImpuestos - CostoCapital))  + "</b></td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Fletes Pagados al Cliente\">Fletes pagados al cte</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", Cte_FletePaga) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Utilidad Bruta- Gastos de Servir al Cliente - Gastos Variables Aplicados al Terr.- Otros Gastos Variables- Fletes Pagados al Cliente\"><b>Utilidad marginal</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UtilidadMarginal) + "</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((UtilidadMarginal / VentaNeta) * 100)) + "%</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            /*                    CadenaHtml = CadenaHtml + "            <tr>";
                                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 12.00;Formula :  (Venta Neta NO Papel * (12.00 /100))\">Contrib. a gastos fijos a otros [" + String.Format("{0:N}", cd.Cd_ContribucionGastosFijosOtros) + "]</td>";
                                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100))) + "</td>";
                                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                CadenaHtml = CadenaHtml + "            </tr>"; */
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";

                            /*                  CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 7.5;Formula : (Venta Neta Papel * (7.5 /100)) \">Contribución a gastos fijos papel [" + String.Format("{0:N}", cd.Cd_ContribucionGastosFijosPapel) + "]</td>";
                                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))) + "</td>";*/

                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó evaluar la venta en una ecuación \">Contribución a gastos fijos[" + String.Format("{0:N}", FactorFijos) + "]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNeta * (Convert.ToSingle(FactorFijos) / 100))) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó evaluar la venta en una ecuación \">Cargo UCS's [" + String.Format("{0:N}", FactorUCS) + "]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNeta * (Convert.ToSingle(FactorUCS) / 100))) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Utilidad Marginal- Contrib. a Gastos Fijos Otros- Contrib. a Gastos Fijos Papel- Cargos UCS’s\"><b>Uafir mensual</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UafirMensual) + "</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((UafirMensual / VentaNeta) * 100)) + "%</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Mensual*12\"><b>Uafir anual</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (UafirMensual * 12)) + "</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Anual*(40.00/100)\">ISR y PTU (	" + String.Format("{0:N}", cd.Cd_ISRyPTU) + "%	)</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", txtISRyPTUMon) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Anual- ISR Y PTU\"><b>Uafir después de impuestos</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UafirDespuesImpuestos) + "</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default Cetes : 5.00 y Tasa Incremental al Costo Capital : 15.00;Formula : Inversión en activos netos op'n   * ( (5.00)+ (15.00) )/100\">Costo de capital [" + String.Format("{0:N}", (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value)) + "]</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", CostoCapital) + "</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";
                            CadenaHtml = CadenaHtml + "";
                            CadenaHtml = CadenaHtml + "            <tr>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : UAFIR Después de Impuestos – Costo Capital\"><b>Utilidad remanente</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (UafirDespuesImpuestos - CostoCapital)) + "</b></td>";
                            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                            CadenaHtml = CadenaHtml + "            </tr>";


                            CadenaHtml = CadenaHtml + "</table>";



                            divResumen.InnerHtml = CadenaHtml;


                        }
                        else
                        {
                            
                            Sesion sesion = new Sesion();
                            sesion = (Sesion)Session["Sesion" + Session.SessionID];


                            int? territorio = null;
                            if (txtTerritorio.Text != "")
                            {
                                territorio = Convert.ToInt32(txtTerritorio.Text);
                            }
                            else
                            {
                                territorio = 0;
                            }


                            Territorios CatTer = new Territorios();
                            CatTer.Id_Emp = sesion.Id_Emp;
                            CatTer.Id_Cd = sesion.Id_Cd;
                            CatTer.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                            new CN_CatTerritorios().ConsultaTerritorios(ref CatTer, sesion.Emp_Cnx);

                            ///aqui
                            List<EstadisticaRentabilidad> Calculo = new List<EstadisticaRentabilidad>();

                            new CN_CatCliente().ReporteRentabilidad_ConsultarEstadistica(
                                sesion.Id_Emp
                                , sesion.Id_Cd_Ver
                                , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                                , territorio
                                , Anio1
                                , Mes1
                                , ref Calculo
                                , sesion.Emp_Cnx);



                            String CadenaHtml;


                            CadenaHtml = "";



                            foreach (EstadisticaRentabilidad LeeResultadoCalculo in Calculo)
                            {



                                CadenaHtml = CadenaHtml + "        <table width=\"100%\">";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td colspan=\"4\" align=\"center\" style=\"font-family: Verdana; font-size: 12pt;background-color: #A9BCF5\"><b>Determinación de la inversión en activos netos de la operación</b></td>";
                                CadenaHtml = CadenaHtml + "                <td colspan=\"5\" align=\"center\" style=\"font-family: Verdana; font-size: 12pt;background-color: #A9BCF5\"><b>Cálculo del Uafir anual después de impuestos</b></td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Son los Días de Crédito que tiene el Cliente Autorizado en el Sistema\">Cuentas por cobrar [" + String.Format("{0:N}", LeeResultadoCalculo.CtaCobrarPorc) + "]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", LeeResultadoCalculo.CtaCobrar) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula :  (Facturas Notas de Cargo-Cancelaciones de Facturas Operadas – Notas de Crédito) Promedio Calculada del Periodo Seleccionado\">Venta neta</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", LeeResultadoCalculo.VentaNetaMon) + "</a></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">100.00%</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default de 20 Días;Formula : (Costo de Material/30)* 25\">Inventario (Días) [" + String.Format("{0:N}", LeeResultadoCalculo.InvDiasCant) + "]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", LeeResultadoCalculo.InvDias) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Son las Ventas Promedio del Periodo Seleccionado evaluadas al Costo AAA.\">Costo de material</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", LeeResultadoCalculo.CostoMaterialMon) + "</a></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((LeeResultadoCalculo.CostoMaterialMon / LeeResultadoCalculo.VentaNetaMon) * 100)) + "%</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"En Construcción\">Inventario en consignación (Días) [0.00]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$0.00</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Venta Neta - Costo Material\">Utilidad Prima</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", (LeeResultadoCalculo.VentaNetaMon - LeeResultadoCalculo.CostoMaterialMon)) + "</a></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", (((LeeResultadoCalculo.VentaNetaMon - LeeResultadoCalculo.CostoMaterialMon) / LeeResultadoCalculo.VentaNetaMon) * 100)) + "%</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                //CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default de 0.5;(Costo de los Equipos en Comodato)* 0.5\">Inversión en Equipo Comodato  (costo*" + String.Format("{0:N}", cd.Cd_FactorInvComodato) + ")</td>";
                                //CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", (TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato))) + "</a></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 3.1;Formula : (Venta Neta del Periodo/30)*3.1\">Inversión en activos fijos [" + String.Format("{0:N}", 3.10) + "]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((LeeResultadoCalculo.VentaNetaMon / 30) * 3.10)) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Importe identificado por Cliente y Territorio de Mano de Obra en Proyectos.\">Mano de obra en proyectos</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", LeeResultadoCalculo.ManoObraMon) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Cuentas por Cobrar+ Inventario(Días)+ Inventario en Consignación (Días)+ Inversión en Activos Fijos\"><b>Inversión total en activos</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", LeeResultadoCalculo.InvTotalActivos) + "</b></td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 3.0;Formula : (Costo de Material de los Productos que NO sin Papel * (3.0)/100)\">Flete al CD [" + String.Format("{0:N}", LeeResultadoCalculo.FletePorc) + "]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", LeeResultadoCalculo.FleteMon) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 30;(Costo de Material/30)* 45 Más IVA del CD.\">Financiamiento de proveedores (" + String.Format("{0:N}", 30) + " Días)</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", LeeResultadoCalculo.FinanProv) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Importe de Amortización del Territorio y Cliente Seleccionado a la Fecha generada el reporte\">Amortización en Equipos</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", LeeResultadoCalculo.AmortizacionMon) + "</a></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default Cetes : 5.00 y Tasa Incremental al Costo Capital : 15.00;Formula : (Inversión en activos netos op'n - Financiamiento de Proveedores)  * ( (5.00)+ (15.00) )/100\">Costo de capital [" + String.Format("{0:N}", LeeResultadoCalculo.CostoCapital) + "%]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", LeeResultadoCalculo.CostoCapitalMon) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Estadística de Base Instalada del Territorio y Cliente Seleccionado a la Fecha generada el reporte evaluada a los Pesos por Equipo.\">Costo Servicio a Equipos</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", LeeResultadoCalculo.CostoServEquipoMon) + "</a></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";

                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                //CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Inversión Total en Activos- Financiamiento de Proveedores\"><b>Inversión en activos netos op'n</b></td>";
                                //CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Inversión Total en Activos\"><b>Inversión en activos netos op'n</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\">&nbsp;</td>";
                                //CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (inversionTotalActivos - financiamientoProveedores)) + "</b></td>";
                                //CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (inversionTotalActivos)) + "</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Venta Neta – (Costo de Material+ Mano de Obra en Proyectos+ Flete+ Amortización en Equipos+ Costo Servicio Equipos)\"><b>Utilidad bruta</b></td>";

                                double UtilidadBruta =0;
                                UtilidadBruta = (LeeResultadoCalculo.UtilidadMon);


                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UtilidadBruta) + "<b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((UtilidadBruta / LeeResultadoCalculo.VentaNetaMon) * 100)) + "%</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";


                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";

                                //modificar


                                if (CatTer.Id_TipoRepresentante == 4)
                                {
                                    CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se definio fijo por default 20 Si es Gerencial el Territorio 10;Formula : (Utilidad Bruta* (10/100))\">Gastos de Servir al Cliente [" + String.Format("{0:N}", 10) + "]</td>";
                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (UtilidadBruta * 10 / 100)) + "</td>";
                                }
                                else
                                {
                                    CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se definio fijo por default 20 Si es Gerencial el Territorio 10;Formula : (Utilidad Bruta* (20/100))\">Gastos de Servir al Cliente [" + String.Format("{0:N}", 20) + "]</td>";
                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (UtilidadBruta * 20 / 100)) + "</td>";
                                }

                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                //  Eliminado por Autorización de Eugenio             CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : (Uafir Después de Impuesto/ Inversión en Activos Netos op’n)*100\">Uafir después de Impuestos/Total de Activos</td>";
                                //                                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", txtUafirActivos) + "%</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : (Venta Neta * (Factor de Otros Gastos Variables Aplicados al Terr /100)) \">Gastos var. aplicados al terr</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", 0) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                //  Eliminado por Autorización de Eugenio                             CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula %: (Uafir Después de Impuestos / (Inversión en Activos Netos op’n)*100)- (Tasa Cetes+ Tasa Incremental Costo Capital)\"><b>Utilidad remanente [" + String.Format("{0:N}",UtilidadRemanentePorc)  + "%]</b></td>";
                                //                                                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}",(UafirDespuesImpuestos - CostoCapital))  + "</b></td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"(Venta Neta * (Fletes Pagados al Cliente /100))\">Fletes pagados al cte</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", 0) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                double UtilidadMarginal = 0;
                                if (CatTer.Id_TipoRepresentante == 4)
                                {
                                    UtilidadMarginal = UtilidadBruta - (UtilidadBruta * 10 / 100);
                                }
                                else
                                {
                                    UtilidadMarginal = UtilidadBruta - (UtilidadBruta * 20 / 100);
                                }

                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Utilidad Bruta- Gastos de Servir al Cliente - Gastos Variables Aplicados al Terr.- Otros Gastos Variables- Fletes Pagados al Cliente\"><b>Utilidad marginal</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UtilidadMarginal) + "</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((UtilidadMarginal / LeeResultadoCalculo.VentaNetaMon) * 100)) + "%</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";


                                double FactorFijos = 0;
                                double FactorUCS = 0;

                                if (LeeResultadoCalculo.VentaNetaMon < 5000) FactorFijos = 17.5;
                                if (LeeResultadoCalculo.VentaNetaMon >= 5000 && LeeResultadoCalculo.VentaNetaMon < 10000) FactorFijos = 16.84;
                                if (LeeResultadoCalculo.VentaNetaMon >= 10000 && LeeResultadoCalculo.VentaNetaMon < 15000) FactorFijos = 16.18;
                                if (LeeResultadoCalculo.VentaNetaMon >= 15000 && LeeResultadoCalculo.VentaNetaMon < 20000) FactorFijos = 15.53;
                                if (LeeResultadoCalculo.VentaNetaMon >= 20000 && LeeResultadoCalculo.VentaNetaMon < 25000) FactorFijos = 14.87;
                                if (LeeResultadoCalculo.VentaNetaMon >= 25000 && LeeResultadoCalculo.VentaNetaMon < 30000) FactorFijos = 14.21;
                                if (LeeResultadoCalculo.VentaNetaMon >= 30000 && LeeResultadoCalculo.VentaNetaMon < 35000) FactorFijos = 13.55;
                                if (LeeResultadoCalculo.VentaNetaMon >= 35000 && LeeResultadoCalculo.VentaNetaMon < 40000) FactorFijos = 12.89;
                                if (LeeResultadoCalculo.VentaNetaMon >= 40000 && LeeResultadoCalculo.VentaNetaMon < 45000) FactorFijos = 12.24;
                                if (LeeResultadoCalculo.VentaNetaMon >= 45000 && LeeResultadoCalculo.VentaNetaMon < 50000) FactorFijos = 11.58;
                                if (LeeResultadoCalculo.VentaNetaMon >= 50000 && LeeResultadoCalculo.VentaNetaMon < 55000) FactorFijos = 10.92;
                                if (LeeResultadoCalculo.VentaNetaMon >= 55000 && LeeResultadoCalculo.VentaNetaMon < 60000) FactorFijos = 10.26;
                                if (LeeResultadoCalculo.VentaNetaMon >= 60000 && LeeResultadoCalculo.VentaNetaMon < 65000) FactorFijos = 9.61;
                                if (LeeResultadoCalculo.VentaNetaMon >= 65000 && LeeResultadoCalculo.VentaNetaMon < 70000) FactorFijos = 8.95;
                                if (LeeResultadoCalculo.VentaNetaMon >= 70000 && LeeResultadoCalculo.VentaNetaMon < 75000) FactorFijos = 8.29;
                                if (LeeResultadoCalculo.VentaNetaMon >= 75000 && LeeResultadoCalculo.VentaNetaMon < 80000) FactorFijos = 7.63;
                                if (LeeResultadoCalculo.VentaNetaMon >= 80000 && LeeResultadoCalculo.VentaNetaMon < 85000) FactorFijos = 6.97;
                                if (LeeResultadoCalculo.VentaNetaMon >= 85000 && LeeResultadoCalculo.VentaNetaMon < 90000) FactorFijos = 6.32;
                                if (LeeResultadoCalculo.VentaNetaMon >= 90000 && LeeResultadoCalculo.VentaNetaMon < 100000) FactorFijos = 5.66;
                                if (LeeResultadoCalculo.VentaNetaMon >= 100000) FactorFijos = 5.0;

                                if (LeeResultadoCalculo.VentaNetaMon < 5000) FactorUCS = 3.5;
                                if (LeeResultadoCalculo.VentaNetaMon >= 5000 && LeeResultadoCalculo.VentaNetaMon < 10000) FactorUCS = 3.0;
                                if (LeeResultadoCalculo.VentaNetaMon >= 10000 && LeeResultadoCalculo.VentaNetaMon < 25000) FactorUCS = 2.5;
                                if (LeeResultadoCalculo.VentaNetaMon >= 25000 && LeeResultadoCalculo.VentaNetaMon < 50000) FactorUCS = 2;
                                if (LeeResultadoCalculo.VentaNetaMon >= 50000 && LeeResultadoCalculo.VentaNetaMon < 100000) FactorUCS = 1.5;
                                if (LeeResultadoCalculo.VentaNetaMon >= 100000) FactorUCS = 1;




                                /*                    CadenaHtml = CadenaHtml + "            <tr>";
                                                    CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                    CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                    CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                    CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                    CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                    CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 12.00;Formula :  (Venta Neta NO Papel * (12.00 /100))\">Contrib. a gastos fijos a otros [" + String.Format("{0:N}", cd.Cd_ContribucionGastosFijosOtros) + "]</td>";
                                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100))) + "</td>";
                                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                                    CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                                    CadenaHtml = CadenaHtml + "            </tr>"; */
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";

                                /*                  CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 7.5;Formula : (Venta Neta Papel * (7.5 /100)) \">Contribución a gastos fijos papel [" + String.Format("{0:N}", cd.Cd_ContribucionGastosFijosPapel) + "]</td>";
                                                    CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))) + "</td>";*/
                                
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó evaluar la venta en una ecuación \">Contribución a gastos fijos[" + String.Format("{0:N}", FactorFijos) + "]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (LeeResultadoCalculo.VentaNetaMon * (Convert.ToSingle(FactorFijos) / 100))) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó evaluar la venta en una ecuación \">Cargo UCS's [" + String.Format("{0:N}", FactorUCS) + "]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (LeeResultadoCalculo.VentaNetaMon * (Convert.ToSingle(FactorUCS) / 100))) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Utilidad Marginal- Contrib. a Gastos Fijos Otros- Contrib. a Gastos Fijos Papel- Cargos UCS’s\"><b>Uafir mensual</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", LeeResultadoCalculo.UafirMensualMon) + "</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((LeeResultadoCalculo.UafirMensualMon / LeeResultadoCalculo.VentaNetaMon) * 100)) + "%</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Mensual*12\"><b>Uafir anual</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (LeeResultadoCalculo.UafirMensualMon * 12)) + "</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Anual*(40.00/100)\">ISR y PTU (	" + String.Format("{0:N}", LeeResultadoCalculo.ISRyPTU) + "%	)</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", LeeResultadoCalculo.ISRyPTUMon) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Anual- ISR Y PTU\"><b>Uafir después de impuestos</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", LeeResultadoCalculo.UafirDespuesImpMon) + "</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default Cetes : 5.00 y Tasa Incremental al Costo Capital : 15.00;Formula : Inversión en activos netos op'n   * ( (5.00)+ (15.00) )/100\">Costo de capital [" + String.Format("{0:N}", (LeeResultadoCalculo.CostoCapital)) + "]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", LeeResultadoCalculo.CostoCapitalMon) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";
                                CadenaHtml = CadenaHtml + "";
                                CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : UAFIR Después de Impuestos – Costo Capital\"><b>Utilidad remanente</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (LeeResultadoCalculo.UtilidadRemanenteMon)) + "</b></td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>";


                                CadenaHtml = CadenaHtml + "</table>";



                            }
                            CadenaHtml = CadenaHtml + "</table>";



                            divResumen.InnerHtml = CadenaHtml;



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ImprimirXLS(ArrayList ALValorParametrosInternos, Type instance)
        {
            try
            {
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    report1.ReportParameters[i].AllowNull = true;
                    report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", report1, null);
                string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);

                fs.Flush();
                fs.Close();

                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarCentros();
            this.CargarCombos();
            List<Territorios> listaTerr = new List<Territorios>();
            Territorios ter = new Territorios();
            ter.Descripcion = "-- Seleccionar --";
            ter.Id_Ter = -1;
            listaTerr.Insert(0, ter);
            txtTerritorio.Text = string.Empty;

            //this.CargarComboTerritorios(listaTerr);
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
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
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
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

        private void CargarCombos()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cnComun = new CN__Comun();
                cnComun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatTerritorio_ComboTodos", ref this.cmbTer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void CargarCliente()
        //{
        //    try
        //    {
        //        Sesion Sesion = new Sesion();
        //        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    if (mensaje.Contains("cmbCliente_IndexChanging_error"))
                        Alerta("Error al momento de consultar los datos del cliente");
                    else
                        Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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
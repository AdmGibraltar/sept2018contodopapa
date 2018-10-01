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
using Telerik.Reporting.Processing;
using System.IO;

namespace SIANWEB
{
    public partial class Rep_VenUb : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                        CN_CatCalendario cn_calenda = new CN_CatCalendario();
                        Calendario c = new Calendario();

                        cn_calenda.ConsultaCalendarioActual(ref c, Sesion);
                        cmbAnioInicio.SelectedValue = c.Cal_Año.ToString();
                        cmbAnioFin.SelectedValue = c.Cal_Año.ToString();
                        cmbMesInicio.SelectedValue = c.Cal_Mes.ToString();
                        cmbMesFin.SelectedValue = c.Cal_Mes.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
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

                switch (btn.CommandName)
                {
                    case "print":
                        mensajeError = "Impresion_error";
                        this.Imprimir(true);
                        break;
                    case "excel":
                        mensajeError = "Impresion_error";
                        this.Imprimir(false);
                        break;
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

        protected void rbCalculadoCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((RadioButtonList)sender).SelectedItem.Value.ToUpper())
            {
                case "1":
                    txtUtilidadMinima.Style.Add("display", "none");
                    lblUtilidadBruta.Style.Add("display", "none");
                    this.HD_CalculadoCon.Value = "1";
                    break;
                case "2":
                    txtUtilidadMinima.Style.Add("display", "block");
                    lblUtilidadBruta.Style.Add("display", "block");
                    this.HD_CalculadoCon.Value = "2";
                    break;
            }
        }

        protected void rbAgruparPor_SelectedIndexChanged(object sender, EventArgs e)
        {            
            switch (((RadioButtonList)sender).SelectedItem.Value.ToUpper())
            {
                case "1":
                    lblCliente.Enabled = true;
                    txtNumeroCliente.Enabled = true;                    
                    lblTerritorio.Enabled = true;
                    txtTerritorio.Enabled = true;                    
                    cmbTer.Enabled = true;                    
                    break;
                case "2":
                    lblCliente.Enabled = true;
                    txtNumeroCliente.Enabled = true;
                    lblTerritorio.Enabled = true;
                    txtTerritorio.Enabled = true;                    
                    cmbTer.Enabled = true;
                    break;
                case "3":
                    lblCliente.Enabled = false;
                    txtNumeroCliente.Enabled = false;
                    txtNumeroCliente.Text = string.Empty;
                    txtNombreCliente.Text = string.Empty;
                    lblTerritorio.Enabled = true;
                    txtTerritorio.Enabled = true;
                    txtTerritorio.Text = string.Empty;
                    cmbTer.SelectedIndex = 0;
                    cmbTer.Enabled = true;                    
                    break;
                case "5":
                    lblCliente.Enabled = false;
                    txtNumeroCliente.Enabled = false;
                    txtNumeroCliente.Text = string.Empty;
                    txtNombreCliente.Text = string.Empty;
                    lblTerritorio.Enabled = true;
                    txtTerritorio.Enabled = true;
                    txtTerritorio.Text = string.Empty;
                    cmbTer.SelectedIndex = 0;
                    cmbTer.Enabled = true;
                    break;
                case "4":
                    lblCliente.Enabled = false;
                    txtNumeroCliente.Enabled = false;
                    txtNumeroCliente.Text = string.Empty;
                    txtNombreCliente.Text = string.Empty;
                    lblTerritorio.Enabled = false;
                    txtTerritorio.Enabled = false;
                    txtTerritorio.Text = string.Empty;                    
                    cmbTer.SelectedIndex = 0;
                    cmbTer.Enabled = false;
                    break;
            }

            if (cmbTer.Items != null && cmbTer.Items.Any() && !cmbTer.Enabled)
            {
                cmbTer.Text = cmbTer.Items[0].Text;
                txtTerritorio.Text = string.Empty; 
            }

            if (!txtNumeroCliente.Value.HasValue)
            {
                CargarTerritorios(-1);
            }
        }

        public void txtNumCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];

                Clientes cliente = new Clientes();
                cliente.Id_Emp = gSession.Id_Emp;
                cliente.Id_Cd = gSession.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value.Value : -1);
                new CN_CatCliente().ConsultaClientes(ref cliente, gSession.Emp_Cnx);
                txtNombreCliente.Text = cliente.Cte_NomComercial;
                CargarTerritorios(cliente.Id_Cte.Value);

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                txtNumeroCliente.Text = string.Empty;
                txtNombreCliente.Text = string.Empty;
                txtTerritorio.Text = string.Empty;
                CargarTerritorios(-1);
            }
        }
        #endregion

        #region Funciones

        private void Imprimir(bool a_pantalla)
        {
            try
            {
                bool errorValidacion = false;
                if (rbCalculadoCon.SelectedItem.Value == "1")
                {
                    if (Convert.ToInt32(cmbAnioInicio.SelectedValue) > Convert.ToInt32(cmbAnioFin.SelectedValue))
                    {
                        this.Alerta("El año inicial no debe ser mayor al año final");
                        errorValidacion = true;
                    }
                    else
                    {
                        if (Convert.ToInt32(cmbAnioInicio.SelectedValue) == Convert.ToInt32(cmbAnioFin.SelectedValue))
                            if (Convert.ToInt32(cmbMesInicio.SelectedValue) > Convert.ToInt32(cmbMesFin.SelectedValue))
                            {
                                this.Alerta("El mes inicial no debe ser mayor al mes final");
                                errorValidacion = true;
                            }
                    }
                }

                if (errorValidacion == false)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    ArrayList ALValorParametrosInternos = new ArrayList();

                    //Consulta centro de distribución
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                    ALValorParametrosInternos.Add(sesion.Id_Emp);
                    ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                    ALValorParametrosInternos.Add(sesion.Emp_Nombre);
                    ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                    ALValorParametrosInternos.Add(sesion.U_Nombre);
                    ALValorParametrosInternos.Add(string.Concat(DateTime.Now.ToString("dd/MM/yyyy"), " ", DateTime.Now.ToString("t")));

                    ALValorParametrosInternos.Add(rbAgruparPor.SelectedValue);
                    ALValorParametrosInternos.Add(rbAgruparPor.SelectedItem.Text);

                    ALValorParametrosInternos.Add(txtTerritorio.Text.Trim() == string.Empty ? "Todos" : txtTerritorio.Text.Trim());
                    ALValorParametrosInternos.Add(txtTerritorio.Text);

                    ALValorParametrosInternos.Add(txtNumeroCliente.Text.Trim() == string.Empty ? "Todos" : txtNumeroCliente.Text.Trim());
                    ALValorParametrosInternos.Add(txtNumeroCliente.Text);

                    ALValorParametrosInternos.Add(txtNombreCliente.Text.Trim() == string.Empty ? "Todos" : txtNombreCliente.Text.Trim());

                    ALValorParametrosInternos.Add(cmbAnioInicio.SelectedValue);
                    ALValorParametrosInternos.Add(cmbAnioFin.SelectedValue);
                    ALValorParametrosInternos.Add(cmbMesInicio.SelectedValue);
                    ALValorParametrosInternos.Add(cmbMesFin.SelectedValue);

                    ALValorParametrosInternos.Add(rbCalculadoCon.SelectedValue);
                    ALValorParametrosInternos.Add(rbCalculadoCon.SelectedItem.Text);
                    ALValorParametrosInternos.Add(txtUtilidadMinima.Text == string.Empty ? "0" : txtUtilidadMinima.Text);
                    ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                    ALValorParametrosInternos.Add(sesion.Id_U);
                    Type instance = null;
                    switch (rbAgruparPor.SelectedValue)
                    {
                        case "1": //cliente
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenUtilidadBruta1);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenUtilidadBruta1);
                            }
                            break;
                        case "2": //producto
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenUtilidadBruta2);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenUtilidadBruta2);
                            }
                            break;
                        case "3": //territorio
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenUtilidadBruta3);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenUtilidadBruta3);
                            }
                            break;
                        case "5": //territorio
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenUtilidadBruta3Rik);
                            }
                            else
                            {
                               instance = typeof(LibreriaReportes.ExpRep_VenUtilidadBruta3Rik);
                            }
                            break;
                        case "4": //segmento
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenUtilidadBruta4);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenUtilidadBruta4);
                            }
                            break;
                    }
                    //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    if (_PermisoImprimir)
                    {
                        if (a_pantalla)
                        {
                            Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                            Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                            RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                        }
                        else
                        {
                            ImprimirXLS(ALValorParametrosInternos, instance);
                        }
                    }
                    else
                        Alerta("No tiene permiso para imprimir");
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
            //Llenar combos de periodos (Años)
            for (int i = DateTime.Now.Year + 1; i > 2000; i--)
            {
                cmbAnioInicio.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                cmbAnioFin.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
            }
            cmbAnioInicio.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbAnioFin.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

            lblUtilidadBruta.Style.Add("display", "none");
            txtUtilidadMinima.Style.Add("display", "none");

            rbAgruparPor_SelectedIndexChanged(rbAgruparPor, new EventArgs());            
        }

        private void CargarCombos()
        {
            try
            {
                CargarTerritorios(-1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarTerritorios(int pIdCliente)
        {
            try
            {
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                string vIdTer = string.Empty;
                string vTerNombre = string.Empty;

                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTodosTerritoriosDelCliente(pIdCliente, gSession, ref listaTerritorios);
                cmbTer.DataTextField = "Descripcion";
                cmbTer.DataValueField = "Id_Ter";
                cmbTer.DataSource = listaTerritorios;
                cmbTer.DataBind();

                if (cmbTer.Items != null && cmbTer.Items.Any())
                {
                    cmbTer.Text = cmbTer.Items[0].Text;
                    if (pIdCliente > 0)
                    {
                        cmbTer.SelectedIndex = 1;
                        txtTerritorio.Text = cmbTer.Items[1].Value.ToString();
                        cmbTer.Text = cmbTer.Items[1].Text;

                        vIdTer = cmbTer.SelectedValue;
                        vTerNombre = cmbTer.Text;
                    }
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
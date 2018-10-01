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
    public partial class Rep_CobSaldos : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
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
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                        if (Page.IsValid)
                        {
                            this.Imprimir(false);
                        }
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

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
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
                int VGEmpresa = 0;
                Int32.TryParse(strEmp, out VGEmpresa);

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

                ALValorParametrosInternos.Add(txtNumeroCliente.Text.Trim() == string.Empty ? "Todos" : txtNumeroCliente.Text.Trim());
                ALValorParametrosInternos.Add(txtNumeroCliente.Text);
                ALValorParametrosInternos.Add(txtTerritorio.Text.Trim() == string.Empty ? "Todos" : txtTerritorio.Text.Trim());
                ALValorParametrosInternos.Add(txtTerritorio.Text);
                ALValorParametrosInternos.Add(Convert.ToDateTime(txtFechaCorte.SelectedDate).ToString("dd/MM/yyyy"));
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                Type instance = null;
                if (chkTerritorio.Checked)
                {
                    if (chkDetalle.Checked)
                    {
                        if (a_pantalla)
                        {
                            if (VGEmpresa == sesion.Id_Emp)
                            {
                                instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos1aBts);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos1a);
                            }
                        }
                        else
                        {
                            if (VGEmpresa == sesion.Id_Emp)
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CobAntiguedadSaldos1aBts);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos1a2);
                            }
                        }
                    }
                    else
                    {
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos2a);
                        }
                        else
                            instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos2a2);
                    }
                }
                else
                {
                    if (chkDetalle.Checked)
                    {
                        if (a_pantalla)
                        {
                            if (VGEmpresa == sesion.Id_Emp)
                            {
                                instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos1bBts);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos1b);
                            }
                        }
                        else
                        {
                            if (VGEmpresa == sesion.Id_Emp)
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CobAntiguedadSaldos1bBts);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos1b2);
                            }
                        }
                    }
                    else
                    {
                        if (a_pantalla)
                            instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos2b);
                        else
                            instance = typeof(LibreriaReportes.Rep_CobAntiguedadSaldos2b2);
                    }
                }
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
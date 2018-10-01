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
    public partial class Rep_VenPronosticoCierro : System.Web.UI.Page
    {
        private bool _PermisoImprimir;
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
                    this.ValidarPermisos();
                    if (!Page.IsPostBack)
                    {
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
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region Eventos
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        RadGrid1.Rebind();
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
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

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_ProCierreMes proCierre = new CN_ProCierreMes();
                    List<PronCierre> listProCierre = new List<PronCierre>();
                    proCierre.CierreGrid(sesion, ref listProCierre);
                    RadGrid1.DataSource = listProCierre;
                    soloLectura();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Funciones

        private void soloLectura()
        {
            try
            {
                for (int i = 0; i < this.RadGrid1.Items.Count; i++)
                {
                    PronCierre pronCierre = new PronCierre();
                    RadNumericTextBox RadNumericTextBox1 = new RadNumericTextBox();
                    RadNumericTextBox1 = (RadNumericTextBox)this.RadGrid1.Items[i].Cells[6].FindControl("RadNumericTextBox1");
                    pronCierre.Id_ProCierre = Convert.ToInt32(RadGrid1.Items[i]["Id_ProCierre"].Text);
                    pronCierre.Pron_Actual = RadNumericTextBox1.Value.HasValue ? Convert.ToDouble(RadNumericTextBox1.Value.Value) : 0;
                    if (pronCierre.Pron_Actual != 0)
                        RadNumericTextBox1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Imprimir(bool a_pantalla)
        {
            try
            {
                int error = 0;
                int validador = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                if (sesion.Id_Rik > 0)
                    GuardarCampos(ref validador);
                else
                    validador = 1;


                if (validador == 1)
                {
                    //Consulta centro de distribución
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                    ALValorParametrosInternos.Add(sesion.Id_Emp);
                    ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                    ALValorParametrosInternos.Add(sesion.Id_Rik);
                    ALValorParametrosInternos.Add(sesion.Emp_Nombre);
                    ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                    ALValorParametrosInternos.Add(sesion.U_Nombre);
                    ALValorParametrosInternos.Add(string.Concat(DateTime.Now.ToString("dd/MM/yyyy"), " ", DateTime.Now.ToString("HH:mm")));
                    ALValorParametrosInternos.Add(rbPor.SelectedValue);
                    ALValorParametrosInternos.Add(rbPor.SelectedItem.Text);
                    ALValorParametrosInternos.Add(txtTerritorio.Text.Trim() == string.Empty ? "Todos" : txtTerritorio.Text.Trim());
                    ALValorParametrosInternos.Add(txtTerritorio.Text);
                    ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                    Type instance = null;
                    switch (rbPor.SelectedValue)
                    {
                        case "1": //RIK
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenPronosticoCierre1);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenPronosticoCierre1);
                            }
                            break;
                        case "2": //Sucursal
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenPronosticoCierre2);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenPronosticoCierre2);
                            }
                            break;
                        case "3": //Territorio
                            boton(txtTerritorio.Text, ref error);
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenPronosticoCierre3);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenPronosticoCierre3);
                            }
                            break;
                    }

                    //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    if (error == 0)
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
                            Alerta("No tiene permiso para imprimir el reporte");
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

            if (sesion.Id_Rik > 0)
            {
                this.RadGrid1.Visible = true;
                RadGrid1.Rebind();
            }
            else
                this.RadGrid1.Visible = false;

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
                    _PermisoImprimir = Permiso.PImprimir;
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void boton(string cadena, ref int error)
        {
            try
            {
                if (!string.IsNullOrEmpty(cadena))
                {
                    string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] split2;
                    foreach (string a in split)
                    {
                        if (a.Contains("-"))
                        {
                            split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                            if (split2.Length != 2)
                            {
                                Alerta("El rango " + a.ToString() + " no es válido");
                                error = 1;
                            }
                            if (split2.Length == 2)
                                if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                                {
                                    Alerta("El rango " + a.ToString() + " no es válido");
                                    error = 1;
                                }
                        }
                    }
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

        private void GuardarCampos(ref int validador)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {
                    if (RadGrid1.Items.Count > 0)
                    {
                        int revision = 0;
                        for (int i = 0; i < this.RadGrid1.Items.Count; i++)
                        {
                            revision = 0;
                            PronCierre pronCierre = new PronCierre();
                            RadNumericTextBox txtPron_Actual = new RadNumericTextBox();
                            txtPron_Actual = (RadNumericTextBox)this.RadGrid1.Items[i].Cells[6].FindControl("RadNumericTextBox1");
                            pronCierre.Pron_Actual = txtPron_Actual.Value.HasValue ? Convert.ToDouble(txtPron_Actual.Value.Value) : 0;
                            if (pronCierre.Pron_Actual == 0)
                            {
                                Alerta("Existen clientes sin pronóstico de cierre");
                                revision = 1;
                                return;
                            }
                            else
                            {
                                if (pronCierre.Pron_Actual < 0)
                                {
                                    Alerta("Los pronóstico no deben ser negativos");
                                    revision = 1;
                                    return;
                                }
                            }
                        }
                        if (revision == 0)
                            for (int i = 0; i < this.RadGrid1.Items.Count; i++)
                            {
                                validador = 0;
                                PronCierre pronCierre = new PronCierre();
                                RadNumericTextBox txtPron_Actual = new RadNumericTextBox();
                                txtPron_Actual = (RadNumericTextBox)this.RadGrid1.Items[i].Cells[6].FindControl("RadNumericTextBox1");
                                pronCierre.Id_Emp = Sesion.Id_Emp;
                                pronCierre.Id_Cd = Sesion.Id_Cd_Ver;
                                pronCierre.Id_Rik = Sesion.Id_Rik;
                                pronCierre.Id_ProCierre = Convert.ToInt32(RadGrid1.Items[i]["Id_ProCierre"].Text);
                                pronCierre.Id_Cte = Convert.ToInt32(RadGrid1.Items[i]["Id_Cte"].Text);
                                pronCierre.Id_Ter = Convert.ToInt32(RadGrid1.Items[i]["Id_Ter"].Text);
                                pronCierre.Pron_Anterior = Convert.ToDouble(RadGrid1.Items[i]["Pron_Anterior"].Text);
                                pronCierre.Pron_Actual = txtPron_Actual.Value.HasValue ? Convert.ToDouble(txtPron_Actual.Value.Value) : 0;
                                CN_ProCierreMes proCierre = new CN_ProCierreMes();
                                proCierre.ModificarPronosticoCierre(pronCierre, Sesion, ref validador);
                            }
                        else
                            validador = 0;
                    }
                    else
                        validador = 1;
                }
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
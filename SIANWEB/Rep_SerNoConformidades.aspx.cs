using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class Rep_SerNoConformidades : System.Web.UI.Page
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
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (_PermisoImprimir)
                    switch (btn.CommandName)
                    {
                        case "Mostrar":
                            Mostrar(true);
                            break;
                        case "excel":
                            Mostrar(false);
                            break;
                    }
                else
                    Alerta("No tiene permiso para imprimir");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
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
                comun.CambiarCdVer(this.CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "cmbCentro_SelectedIndexChanged1");
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
        private void Inicializar()
        {
            try
            {
                CargarCentros();
                CargarCombos();
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

                if (Sesion.CalendarioIni >= dpFechaini.MinDate && Sesion.CalendarioIni <= dpFechaini.MaxDate)
                {
                    dpFechaini.DbSelectedDate = Sesion.CalendarioIni;
                }
                if (Sesion.CalendarioFin >= dpFechafin.MinDate && Sesion.CalendarioFin <= dpFechafin.MaxDate)
                {
                    dpFechafin.DbSelectedDate = Sesion.CalendarioFin;
                }

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

        private void Mostrar(bool a_pantalla)
        {
            #region Captura de valores
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string nombreEmpresa = sesion.Emp_Nombre;
            string nombreSucursal = sesion.Cd_Nombre;
            DateTime Fechalocal = DateTime.Now;
            int error = 0;
            SerNoConformidades conformidades = new SerNoConformidades();
            conformidades.Id_Cd = sesion.Id_Cd_Ver;
            if (!string.IsNullOrEmpty(txtNumeroCliente.Text))
            {
                conformidades.Clientes = txtNumeroCliente.Text;
                conformidades.SClientes = txtNumeroCliente.Text;
            }
            else
                conformidades.SClientes = "Todos";
            if (!string.IsNullOrEmpty(txtTerritorio.Text))
            {
                conformidades.Territorio = txtTerritorio.Text;
                conformidades.STerritorio = txtTerritorio.Text;
            }
            else
                conformidades.STerritorio = "Todos";

            if (!string.IsNullOrEmpty(txtReclamacion.Text))
            {
                conformidades.Reclamacion = Convert.ToInt32(txtReclamacion.Text);
                conformidades.SReclamacion = txtReclamacion.Text;
            }
            else
            {
                conformidades.Reclamacion = 0;
                conformidades.SReclamacion = "Todos";
            }
            if (dpFechaini.SelectedDate.HasValue)
            {
                conformidades.Fechaini = dpFechaini.SelectedDate.HasValue ? dpFechaini.SelectedDate.Value.ToString("yyyy/MM/dd") : ""; //dpFechaini.SelectedDate.Value;
                if (dpFechafin.SelectedDate.HasValue)
                    conformidades.SFecha = "desde " + dpFechaini.SelectedDate.Value.ToString("dd/MM/yyyy");
                else
                    conformidades.SFecha = "desde " + dpFechaini.SelectedDate.Value.ToString("dd/MM/yyyy") + " en adelante";
            }
            if (dpFechafin.SelectedDate.HasValue)
            {
                conformidades.Fechafin = dpFechafin.SelectedDate.HasValue ? dpFechafin.SelectedDate.Value.ToString("yyyy/MM/dd") : "";
                if (dpFechaini.SelectedDate.HasValue)
                    conformidades.SFecha += " a " + dpFechafin.SelectedDate.Value.ToString("dd/MM/yyyy");
                else
                    conformidades.SFecha += "hasta " + dpFechafin.SelectedDate.Value.ToString("dd/MM/yyyy");
            }

            conformidades.TipoRecl = Convert.ToInt32(cmbReclamacion.SelectedValue);
            conformidades.STipoRecl = cmbReclamacion.SelectedItem.Text;
            conformidades.EstatusRecl = Convert.ToInt32(cmbEstatus.SelectedValue);
            conformidades.SEstatusRecl1 = cmbEstatus.SelectedItem.Text;

            #endregion
            #region parametros
            ArrayList ALValorParametrosInternos = new ArrayList();

            ALValorParametrosInternos.Add(conformidades.Clientes);
            ALValorParametrosInternos.Add(conformidades.SClientes);
            ALValorParametrosInternos.Add(conformidades.Territorio);
            ALValorParametrosInternos.Add(conformidades.STerritorio);
            ALValorParametrosInternos.Add(conformidades.Reclamacion);
            ALValorParametrosInternos.Add(conformidades.SReclamacion);
            ALValorParametrosInternos.Add(conformidades.Fechaini);
            ALValorParametrosInternos.Add(conformidades.Fechafin);
            ALValorParametrosInternos.Add(conformidades.SFecha);
            ALValorParametrosInternos.Add(conformidades.TipoRecl);
            ALValorParametrosInternos.Add(conformidades.STipoRecl);
            ALValorParametrosInternos.Add(conformidades.EstatusRecl);
            ALValorParametrosInternos.Add(conformidades.SEstatusRecl1);
            //parametros para el cuerpo del reporte
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(conformidades.Id_Cd);
            ALValorParametrosInternos.Add(sesion.U_Nombre);
            ALValorParametrosInternos.Add(Fechalocal);
            ALValorParametrosInternos.Add(nombreEmpresa);
            ALValorParametrosInternos.Add(nombreSucursal);
            //conexion
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            #endregion
            Type instance = null;
            if (a_pantalla)
            {
                instance = typeof(LibreriaReportes.Rep_SerNoConformidades);
            }
            else
            {
                instance = typeof(LibreriaReportes.ExpRep_SerNoConformidades);
            }

            if (error == 0) //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                if (_PermisoImprimir)
                {
                    if (a_pantalla)
                    {
                        Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
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
        private string boton(string cadena, ref int error)
        {
            StringBuilder condicion = new StringBuilder();
            //ArrayList array = new ArrayList();
            if (string.IsNullOrEmpty(cadena))
                return "";
            else
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
                            Alerta("El rango " + a.ToString() + " no es valido");
                            error = 1;
                            return "error";
                        }
                        if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                        {
                            Alerta("El rango " + a.ToString() + " no es valido");
                            error = 1;
                            return "error";
                        }
                        if ((Convert.ToInt32(split2[1]) - Convert.ToInt32(split2[0])) > 500)
                        {
                            Alerta("El rango " + a.ToString() + " no es valido, ingrese un rango menor");
                            error = 1;
                            return "error";
                        }
                        for (int i = Convert.ToInt32(split2[0]); i < Convert.ToInt32(split2[1]) + 1; i++)
                            condicion.Append(i.ToString() + ",");
                    }
                    else
                        condicion.Append(a + ",");
                }
                if (condicion.Length > 0)
                    condicion.Remove(condicion.Length - 1, 1);
            }
            return condicion.ToString();
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
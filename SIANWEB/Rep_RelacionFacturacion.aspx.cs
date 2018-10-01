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
    public partial class Rep_RelacionFacturacion : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
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
                        CargarCentros();
                        CargarCombos();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                        txtFechaini.DbSelectedDate = Sesion.CalendarioIni;
                        txtFechafin.DbSelectedDate = Sesion.CalendarioFin;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                txtFechaini.DbSelectedDate = sesion.CalendarioIni;
                txtFechafin.DbSelectedDate = sesion.CalendarioFin;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
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
                CargarTerritorios(-1, cmbTer, txtTerritorio);
                CargarTerritorios(-1, cmbTer1, txtTerritorio1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarTerritorios(int pIdCliente, RadComboBox cmb, RadNumericTextBox txt)
        {
            try
            {
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                string vIdTer = string.Empty;
                string vTerNombre = string.Empty;

                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTodosTerritoriosDelCliente(pIdCliente, gSession, ref listaTerritorios);
                cmb.DataTextField = "Descripcion";
                cmb.DataValueField = "Id_Ter";
                cmb.DataSource = listaTerritorios;
                cmb.DataBind();

                if (cmb.Items != null && cmb.Items.Any())
                {
                    cmb.Text = cmb.Items[0].Text;
                    if (pIdCliente > 0)
                    {
                        cmb.SelectedIndex = 1;
                        txt.Text = cmb.Items[1].Value.ToString();
                        cmb.Text = cmbTer.Items[1].Text;

                        vIdTer = cmb.SelectedValue;
                        vTerNombre = cmb.Text;
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
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            SerRutaServicio rutaServicio = new SerRutaServicio();
            int error = 0;
            double territorio1 = txtTerritorio.Value != null ? txtTerritorio.Value.Value : 0;
            double territorio2 = txtTerritorio1.Value != null ? txtTerritorio1.Value.Value : 0;
            string sTerritorio1 = string.Empty;

            double cliente1 = txtCliente.Value != null ? txtCliente.Value.Value : 0;
            double cliente2 = txtCliente2.Value != null ? txtCliente2.Value.Value : 0;
            string sCliente1 = string.Empty;

            string sFechaInicial = string.Empty;
            string sFechaFinal = string.Empty;
            DateTime FechaInicial = new DateTime();
            DateTime FechaFinal = new DateTime();

            double factura1 = TxtFactura1.Value != null ? TxtFactura1.Value.Value : 0;
            double factura2 = TxtFactura2.Value != null ? TxtFactura2.Value.Value : 0;
            string sFactura1 = string.Empty;

            rutaServicio.Id_Cd = sesion.Id_Cd_Ver;
            if (string.IsNullOrEmpty(txtFechaini.SelectedDate.Value.ToString()))
                error = 1;
            if (string.IsNullOrEmpty(txtFechafin.SelectedDate.Value.ToString()))
                error = 1;
            if (txtFechaini.SelectedDate > txtFechafin.SelectedDate)
            {
                Alerta("La fecha inicial no debe ser mayor a la fecha final");
                return;
            }
            if (territorio2 < territorio1)
            {
                Alerta("El territorio final no debe ser menor al territorio inicial");
                return;
            }

            if (factura2 < factura1)
            {
                Alerta("La factura final no debe ser menor al factura inicial");
                return;
            }

            if (cliente2 < cliente1)
            {
                Alerta("El cliente final no debe ser menor al cliente inicial");
                return;
            }

            if (!string.IsNullOrEmpty(txtFechaini.SelectedDate.ToString()))
            {
                FechaInicial = txtFechaini.SelectedDate.Value;
                sFechaInicial = txtFechaini.SelectedDate.Value.ToString("dd/MM/yyy");
            }
            if (!string.IsNullOrEmpty(txtFechafin.SelectedDate.ToString()))
            {
                FechaFinal = txtFechafin.SelectedDate.Value;
                sFechaFinal = txtFechafin.SelectedDate.Value.ToString("dd/MM/yyy");
            }
            else
                sFechaFinal = "Actual";

            /*Territorio*/
            if (territorio1 == 0)
                sTerritorio1 = "0";
            else
                sTerritorio1 = "De " + territorio1.ToString();

            if (territorio2 == 0)
            {
                if (territorio1 == 0)
                    sTerritorio1 = "Todos";
                else
                    sTerritorio1 += " en adelante";
            }
            else
                sTerritorio1 += " a " + territorio2.ToString();

            /*cliente*/
            if (cliente1 == 0)
                sCliente1 = "0";
            else
                sCliente1 = "De " + cliente1.ToString();

            if (cliente2 == 0)
            {
                if (cliente1 == 0)
                    sCliente1 = "Todos";
                else
                    sCliente1 += " en adelante";
            }
            else
            {
                if (cliente1 == cliente2)
                    sCliente1 = cliente2.ToString();
                else
                    sCliente1 += " a " + cliente2.ToString();
            }

            /*Factura*/
            if (factura1 == 0)
                sFactura1 = "0";
            else
                sFactura1 = "De " + factura1.ToString();

            if (factura2 == 0)
            {
                if (factura1 == 0)
                    sFactura1 = "Todos";
                else
                    sFactura1 += " en adelante";
            }
            else
                sFactura1 += " a " + factura2.ToString();
            
            ArrayList ALValorParametrosInternos = new ArrayList();

            string nombreEmpresa = sesion.Emp_Nombre;
            string nombreSucursal = sesion.Cd_Nombre;

            DateTime Fechalocal = DateTime.Now;

            //datos de filtros
            ALValorParametrosInternos.Add(FechaInicial);
            ALValorParametrosInternos.Add(FechaFinal);

            ALValorParametrosInternos.Add(territorio1);
            ALValorParametrosInternos.Add(territorio2);
            ALValorParametrosInternos.Add(sTerritorio1);
            
            ALValorParametrosInternos.Add(cliente1);
            ALValorParametrosInternos.Add(cliente2);
            ALValorParametrosInternos.Add(sCliente1);

            ALValorParametrosInternos.Add(factura1);
            ALValorParametrosInternos.Add(factura2);
            ALValorParametrosInternos.Add(sFactura1);
            
            ALValorParametrosInternos.Add(sFechaInicial);
            ALValorParametrosInternos.Add(sFechaFinal);
            
            ALValorParametrosInternos.Add(cmbEstatus.SelectedValue);
            string estatus = string.Empty;
            if (cmbEstatus.SelectedValue == "0")
                estatus = "Todos";
            else
                estatus = cmbEstatus.SelectedItem.Text;
            ALValorParametrosInternos.Add(estatus);                      

            //parametros para el cuerpo del reporte
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
            ALValorParametrosInternos.Add(sesion.U_Nombre);
            ALValorParametrosInternos.Add(Fechalocal);
            ALValorParametrosInternos.Add(nombreEmpresa);
            ALValorParametrosInternos.Add(nombreSucursal);
            //conexion
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            Type instance = null;

            int filtro = Convert.ToInt32(rbList.SelectedValue);
            if (filtro == 1)
            {
                if (a_pantalla)
                {
                    instance = typeof(LibreriaReportes.Rep_RelacionFacturacion);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_RelacionFacturacion);
                }
            }
            else
            {
                if (a_pantalla)
                {
                    instance = typeof(LibreriaReportes.Rep_RelacionFacturacionTer);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_RelacionFacturacionTer);
                }
            }
            if (error == 0)
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
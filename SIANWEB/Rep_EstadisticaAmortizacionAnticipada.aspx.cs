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
    public partial class Rep_EstadisticaAmortizacionAnticipada : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
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
                int año = -1;
                int mes = -1;
                int.TryParse(cmbAño.SelectedValue, out año);
                int.TryParse(cmbMes.SelectedValue, out mes);
                if (año > 0)
                    if (mes > 0)
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
                        Alerta("Seleccione un mes válido para el reporte");
                else
                    Alerta("Seleccione un año válido para el reporte");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbAño_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                int año = 0;
                int.TryParse(cmbAño.SelectedValue, out año);
                if (año != 0)
                    CargarMes(año);
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
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                CargarAño();
                int año = !string.IsNullOrEmpty(cmbAño.SelectedValue) ? Convert.ToInt32(cmbAño.SelectedValue) : -1;
                CargarMes(año);
            }
            catch (Exception ex)
            {
                throw ex;
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

        public void cmbTer_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            string vRepresentante = string.Empty;
            string vRepNombre = string.Empty;

            txtRepresentante.Enabled = false;

            if (!string.IsNullOrEmpty(e.Value))
            {
                Representantes rep = new Representantes();
                Territorios ter = new Territorios()
                {
                    Id_Emp = sesion.Id_Emp,
                    Id_Cd = sesion.Id_Cd_Ver,
                    Id_Ter = Convert.ToInt32(e.Value)
                };

                CN_CatRepresentantes cnRep = new CN_CatRepresentantes();
                cnRep.ConsultarRepresentantePorTerritorio(ter, sesion.Emp_Cnx, ref rep);

                txtRepresentante.Enabled = true;
                vRepresentante = rep.Id_Rik.ToString();
                vRepNombre = rep.Nombre;
            }

            txtRepresentante.Text = vRepresentante;
            txtRepOld.Value = vRepresentante;
            txtRepresentanteStr.Text = vRepNombre;
        }

        public void txtRep_TextChanged(object sender, EventArgs e)
        {
            RadTextBox txt = sender as RadTextBox;
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int vIdTer = Convert.ToInt32(txtTerritorio.Text);
            int vIdRik = Convert.ToInt32(txt.Text);
            List<Representantes> vList = new List<Representantes>();

            CN_CatRepresentantes cnRep = new CN_CatRepresentantes();
            cnRep.ComboRepresentanteXTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, vIdTer, sesion.Emp_Cnx, ref vList);

            if (!vList.Any(x => x.Id_Rik == vIdRik))
            {
                Alerta("El Representante no está relacionado al territorio seleccionado");
                txtRepresentante.Text = txtRepOld.Value;
            }
            else
            {
                txtRepresentanteStr.Text = vList.FirstOrDefault(x => x.Id_Rik == vIdRik).Nombre;
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
                CargarAño();
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

                cmbTer_SelectedIndexChanged(cmbTer, new RadComboBoxSelectedIndexChangedEventArgs(vTerNombre, string.Empty, vIdTer, string.Empty));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAño()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref cmbAño);

                try
                {
                    if (cmbAño.FindItemByValue(Sesion.CalendarioFin.Year.ToString()) != null)
                    {
                        cmbAño.SelectedIndex = cmbAño.FindItemIndexByValue(Sesion.CalendarioFin.Year.ToString());
                    }
                    else
                    {
                        cmbAño.SelectedIndex = cmbAño.Items.Count - 1;
                    }
                    CargarMes(Convert.ToInt32(cmbAño.SelectedValue));
                }
                catch
                {
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarMes(int año)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, año, Sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref cmbMes);
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
            SerAmortizacionSisP amortizacion = new SerAmortizacionSisP();
            int error = 0;
            amortizacion.Id_Cd = !string.IsNullOrEmpty(CmbCentro.SelectedValue) ? Convert.ToInt32(CmbCentro.SelectedValue) : sesion.Id_Cd_Ver;
            if (!string.IsNullOrEmpty(txtRepresentante.Text))
            {
                boton(txtRepresentante.Text, ref error);
                amortizacion.Representante = txtRepresentante.Text;
                amortizacion.SRepresentante = txtRepresentante.Text;
            }
            else
                amortizacion.SRepresentante = "Todos";

            if (!string.IsNullOrEmpty(txtTerritorio.Text))
            {
                boton(txtTerritorio.Text, ref error);
                amortizacion.Territorio = txtTerritorio.Text;
                amortizacion.STerritorio = txtTerritorio.Text;
            }
            else
                amortizacion.STerritorio = "Todos";

            if (!string.IsNullOrEmpty(txtNumeroCliente.Text))
            {
                boton(txtNumeroCliente.Text, ref error);
                amortizacion.Cliente = txtNumeroCliente.Text;
                amortizacion.SCliente = txtNumeroCliente.Text;
            }
            else
                amortizacion.SCliente = "Todos";

            if (!string.IsNullOrEmpty(txtEquipo.Text))
            {
                boton(txtEquipo.Text, ref error);
                amortizacion.Equipo = txtEquipo.Text;
                amortizacion.SEquipo = txtEquipo.Text;
            }
            else
                amortizacion.SEquipo = "Todos";

            int mes = -1;
            int año = -1;
            int.TryParse(cmbAño.SelectedValue, out año);
            if (año > 0)
            {
                amortizacion.Año = año;
                amortizacion.SAño = cmbAño.SelectedItem.Text;
            }
            int.TryParse(cmbMes.SelectedValue, out mes);
            if (mes > 0)
            {
                amortizacion.Mes = mes;
                amortizacion.SMes = cmbMes.SelectedItem.Text;
            }
            amortizacion.Detalle = 2;
            amortizacion.SDetalle = "Desactivado";
            #endregion
            #region valoresParametros
            ArrayList ALValorParametrosInternos = new ArrayList();
            CN_SerAmortizacionSisP serAmortizacion = new CN_SerAmortizacionSisP();
            string nombreEmpresa = sesion.Emp_Nombre;
            string nombreSucursal = sesion.Cd_Nombre;
            DateTime Fechalocal = DateTime.Now;

            //datos de filtros
            ALValorParametrosInternos.Add(amortizacion.Tipo);
            ALValorParametrosInternos.Add(amortizacion.STipo);
            ALValorParametrosInternos.Add(amortizacion.Representante);
            ALValorParametrosInternos.Add(amortizacion.SRepresentante);
            ALValorParametrosInternos.Add(amortizacion.Territorio);
            ALValorParametrosInternos.Add(amortizacion.STerritorio);
            ALValorParametrosInternos.Add(amortizacion.Cliente);
            ALValorParametrosInternos.Add(amortizacion.SCliente);
            ALValorParametrosInternos.Add(amortizacion.Equipo);
            ALValorParametrosInternos.Add(amortizacion.SEquipo);
            ALValorParametrosInternos.Add(amortizacion.Año);
            ALValorParametrosInternos.Add(amortizacion.SAño);
            ALValorParametrosInternos.Add(amortizacion.Mes);
            ALValorParametrosInternos.Add(amortizacion.SMes);
            ALValorParametrosInternos.Add(amortizacion.Detalle);
            ALValorParametrosInternos.Add(amortizacion.SDetalle);
            if (amortizacion.Tipo == 1)
            {//1- Estadística de amortización 
                if (amortizacion.Detalle == 1)
                {//a- Activado
                    amortizacion.Reporte = 1;
                    amortizacion.Encabezado1 = "Código del equipo";
                    amortizacion.Encabezado2 = "Nombre del equipo";
                    amortizacion.Encabezado3 = "Unidades";
                    amortizacion.Encabezado4 = "Monto de inversión";
                    amortizacion.Encabezado5 = "Estadística amortización";
                    amortizacion.Encabezado6 = "Amortización aplicada a la fecha";
                    amortizacion.Encabezado7 = "Saldo por amortizar";
                }
                else
                {//b- Desactivado 
                    amortizacion.Reporte = 2;
                    amortizacion.Encabezado1 = "Unidades";
                    amortizacion.Encabezado2 = "Monto de inversión";
                    amortizacion.Encabezado3 = "Estadística amortización";
                    amortizacion.Encabezado4 = "Amortización aplicada a la fecha";
                    amortizacion.Encabezado5 = "Saldo por amortizar";
                    amortizacion.Encabezado6 = "";
                    amortizacion.Encabezado7 = "";
                }
            }
            if (amortizacion.Tipo == 2)
            {//2- Saldo por amortizar  
                if (amortizacion.Detalle == 1)
                {//a- Activado 
                    amortizacion.Reporte = 3;
                    amortizacion.Encabezado1 = "Código del equipo";
                    amortizacion.Encabezado2 = "Nombre del equipo";
                    amortizacion.Encabezado3 = "Unidades";
                    amortizacion.Encabezado4 = "Monto de inversión";
                    amortizacion.Encabezado5 = "Amortización aplicada a la fecha";
                    amortizacion.Encabezado6 = "Saldo por amortizar";
                    amortizacion.Encabezado7 = "";
                }
                else
                {//b- Desactivado           
                    amortizacion.Reporte = 4;
                    amortizacion.Encabezado1 = "Unidades";
                    amortizacion.Encabezado2 = "Monto de inversión";
                    amortizacion.Encabezado3 = "Amortización aplicada a la fecha";
                    amortizacion.Encabezado4 = "Saldo por amortizar";
                    amortizacion.Encabezado5 = "";
                    amortizacion.Encabezado6 = "";
                    amortizacion.Encabezado7 = "";
                }
            }
            ALValorParametrosInternos.Add(amortizacion.Reporte);
            ALValorParametrosInternos.Add(amortizacion.Encabezado1);
            ALValorParametrosInternos.Add(amortizacion.Encabezado2);
            ALValorParametrosInternos.Add(amortizacion.Encabezado3);
            ALValorParametrosInternos.Add(amortizacion.Encabezado4);
            ALValorParametrosInternos.Add(amortizacion.Encabezado5);
            ALValorParametrosInternos.Add(amortizacion.Encabezado6);
            ALValorParametrosInternos.Add(amortizacion.Encabezado7);
            //parametros para el cuerpo del reporte
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(amortizacion.Id_Cd);
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
                instance = typeof(LibreriaReportes.RepEstadisticaAnticipada);
            }
            else
            {
                instance = typeof(LibreriaReportes.ExpRep_SerAmortizacion2b);
            }

            //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
            if (error == 0)
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
        private void boton(string cadena, ref int error)
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
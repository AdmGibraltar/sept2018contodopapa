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
    public partial class Rep_VenRetencionClientes : System.Web.UI.Page
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
                switch (btn.CommandName)
                {
                    case "Mostrar":
                        Mostrar(true);
                        break;
                    case "excel":
                        Mostrar(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtColfin1_TextChanged(object sender, EventArgs e)
        {
            int inicial = Convert.ToInt32(txtColini1.Text);
            int final = 0;
            int valorRegr = 0;
            if (Int32.TryParse(txtColfin1.Text, out final))
                valorRegr = valorRegreso(inicial, final);
            else
            {
                valorRegr = 21;
                txtColfin1.Text = "20";
            }
            if (valorRegr == 0)
            {
                valorRegr = 21;
                txtColfin1.Text = "20";
            }
            this.txtColini2.Text = valorRegr.ToString();
            this.txtColfin2.Focus();
        }
        protected void txtColfin2_TextChanged(object sender, EventArgs e)
        {
            int inicial = Convert.ToInt32(txtColini2.Text);
            int final = 0;
            int valorRegr = 0;
            if (Int32.TryParse(txtColfin2.Text, out final))
                valorRegr = valorRegreso(inicial, final);
            else
            {
                txtColfin2.Text = "50";
                valorRegr = 51;
            }
            if (valorRegr == 0)
            {
                valorRegr = 51;
                txtColfin2.Text = "51";
            }
            this.txtColini3.Text = valorRegr.ToString();
            this.txtColfin3.Focus();
        }
        protected void txtColfin3_TextChanged(object sender, EventArgs e)
        {
            int inicial = Convert.ToInt32(txtColini3.Text);
            int final = 0;
            int valorRegr = 0;
            if (Int32.TryParse(txtColfin3.Text, out final))
                valorRegr = valorRegreso(inicial, final);
            else
            {
                txtColfin3.Text = "99";
                valorRegr = 100;
            }
            if (valorRegr == 0)
            {
                valorRegr = 100;
                txtColfin3.Text = "99";
            }
            this.txtColini4.Text = valorRegr.ToString();
            this.txtColfin4.Focus();
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref Sesion);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
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
            catch (Exception)
            {
                throw;
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
                    Response.Redirect("Inicio.aspx");
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
            VenRetencionClientes retencion = new VenRetencionClientes();
            int error = 0;
            retencion.Id_Cd = sesion.Id_Cd_Ver;
            if (txtTerritorio.Enabled)
                if (!string.IsNullOrEmpty(txtTerritorio.Text))
                {
                    boton(txtTerritorio.Text, ref error);
                    retencion.Territorio = txtTerritorio.Text;
                    retencion.STerritorio = txtTerritorio.Text;
                }
                else
                    retencion.STerritorio = "Todos";
            else
                retencion.STerritorio = "Todos";
            //check de todos los clientes
            if (chkTodos.Enabled)
                if (chkTodos.Checked)
                    retencion.Todos = 1;
                else
                    retencion.Todos = 2;
            else
                retencion.Todos = 1;

            retencion.Colini1 = Convert.ToInt32(txtColini1.Text);
            retencion.Colini2 = Convert.ToInt32(txtColini2.Text);
            retencion.Colini3 = Convert.ToInt32(txtColini3.Text);
            retencion.Colini4 = Convert.ToInt32(txtColini4.Text);

            retencion.Colfin1 = !string.IsNullOrEmpty(txtColfin1.Text) ? Convert.ToInt32(txtColfin1.Text) : 20;
            retencion.Colfin2 = !string.IsNullOrEmpty(txtColfin2.Text) ? Convert.ToInt32(txtColfin2.Text) : 50;
            retencion.Colfin3 = !string.IsNullOrEmpty(txtColfin3.Text) ? Convert.ToInt32(txtColfin3.Text) : 99;
            retencion.Colfin4 = !string.IsNullOrEmpty(txtColfin4.Text) ? Convert.ToInt32(txtColfin4.Text) : 0;

            if (retencion.Colfin4 == 0)
                retencion.SColfin4 = "más";
            else
                retencion.SColfin4 = retencion.Colfin4.ToString() + "%";

            if (rbTerritorio.Checked)
                retencion.Agrupar = 1;
            if (rbGeneral.Checked)
                retencion.Agrupar = 2;

            retencion.SCol1 = "Columna " + retencion.Colini1.ToString() + "% a " + retencion.Colfin1.ToString() + "%";
            retencion.SCol2 = "Columna " + retencion.Colini2.ToString() + "% a " + retencion.Colfin2.ToString() + "%";
            retencion.SCol3 = "Columna " + retencion.Colini3.ToString() + "% a " + retencion.Colfin3.ToString() + "%";
            retencion.SCol4 = "Columna " + retencion.Colini4.ToString() + "% a " + retencion.SColfin4.ToString();

            retencion.SColi1 = "Del " + retencion.Colini1.ToString() + "% a " + retencion.Colfin1.ToString() + "%";
            retencion.SColi2 = "Del " + retencion.Colini2.ToString() + "% a " + retencion.Colfin2.ToString() + "%";
            retencion.SColi3 = "Del " + retencion.Colini3.ToString() + "% a " + retencion.Colfin3.ToString() + "%";
            retencion.SColi4 = "Del " + retencion.Colini4.ToString() + "% a " + retencion.SColfin4.ToString();

            #endregion
            #region valoresParametros
            ArrayList ALValorParametrosInternos = new ArrayList();
            string nombreEmpresa = sesion.Emp_Nombre;
            string nombreSucursal = sesion.Cd_Nombre;
            DateTime Fechalocal = DateTime.Now;

            //datos de filtros
            ALValorParametrosInternos.Add(retencion.Territorio);
            ALValorParametrosInternos.Add(retencion.STerritorio);
            ALValorParametrosInternos.Add(retencion.Todos);
            ALValorParametrosInternos.Add(retencion.Colini1);
            ALValorParametrosInternos.Add(retencion.Colini2);
            ALValorParametrosInternos.Add(retencion.Colini3);
            ALValorParametrosInternos.Add(retencion.Colini4);
            ALValorParametrosInternos.Add(retencion.Colfin1);
            ALValorParametrosInternos.Add(retencion.Colfin2);
            ALValorParametrosInternos.Add(retencion.Colfin3);
            ALValorParametrosInternos.Add(retencion.Colfin4);
            ALValorParametrosInternos.Add(retencion.Agrupar);

            ALValorParametrosInternos.Add(retencion.SCol1);
            ALValorParametrosInternos.Add(retencion.SCol2);
            ALValorParametrosInternos.Add(retencion.SCol3);
            ALValorParametrosInternos.Add(retencion.SCol4);
            ALValorParametrosInternos.Add(retencion.SColi1);
            ALValorParametrosInternos.Add(retencion.SColi2);
            ALValorParametrosInternos.Add(retencion.SColi3);
            ALValorParametrosInternos.Add(retencion.SColi4);

            //parametros para el cuerpo del reporte
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(retencion.Id_Cd);
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
                instance = typeof(LibreriaReportes.Rep_VenRetencionClientes);
            }
            else
            {
                instance = typeof(LibreriaReportes.ExpRep_VenRetencionClientes);
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
        public int valorRegreso(int inicial, int final)
        {
            int valorRegr = 0;
            if (final > inicial)
                valorRegr = final + 1;
            else
                Alerta("Columna final debe ser mayor a la columna inicial");
            return valorRegr;
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

        protected void rbTerritorio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTerritorio.Checked)
            {
                chkTodos.Enabled = true;
                txtTerritorio.Enabled = true;
            }
            else
            {
                chkTodos.Checked = false;
                chkTodos.Enabled = false;
                txtTerritorio.Enabled = false;
            }
        }

        protected void rbGeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGeneral.Checked)
            {
                chkTodos.Checked = false;
                chkTodos.Enabled = false;
                txtTerritorio.Enabled = false;
            }
            else
            {
                chkTodos.Enabled = true;
                txtTerritorio.Enabled = true;
            }
        }
    }
}
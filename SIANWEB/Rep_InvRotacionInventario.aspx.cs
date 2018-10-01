using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Collections;
using CapaDatos;
using Telerik.Reporting.Processing;
using System.IO;
using Telerik.Web.UI;
using Telerik.Reporting;

namespace SIANWEB
{
    public partial class Rep_InvRotacionInventario : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
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
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();
                        dpFecha.DbSelectedDate = sesion.CalendarioFin;

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
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = sesion;
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                sesion = sesion2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                string cadena = txtProducto.Text;
                StringBuilder condicion = new StringBuilder("");
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (!Page.IsValid)
                    return;
                if (_PermisoImprimir)
                {
                    if (btn.CommandName == "print")
                    {
                        this.Imprimir(cadena, true);
                    }
                    else
                    {
                        this.Imprimir(cadena, false);
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
        protected void cmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (e.Value == "4")
                {
                    chkDetalle.Visible = true;
                    RowCliente.Visible = true;
                    RowProducto.Visible = true;
                }
                else
                {
                    chkDetalle.Visible = false;
                    RowCliente.Visible = false;
                    RowProducto.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void CargarCentros()
        {
            try
            {
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
        private void Inicializar()
        {
            try
            {
                CargarTipo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipo()
        {
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("General", "1"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Producto", "2"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Sistemas propietarios", "3"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Producto en consignación", "4"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Papel", "5"));
            cmbTipo.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;
            cmbTipo.SortItems();
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
                CD_PermisosU CN_PermisosU = new CD_PermisosU();
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
        private void Imprimir(string CondicionStr, bool a_pantalla)
        {
            CN__Comun cn_comun = new CN__Comun();
            string resp = cn_comun.ValidarRango(txtProducto.Text);
            if (resp != "")
            {
                Alerta("El rango " + resp + " no es válido");
                return;
            }


            Sesion Sesion = new Sesion();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ArrayList ALValorParametrosInternos = new ArrayList();
            Funciones funcion = new Funciones();
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
            ALValorParametrosInternos.Add(CondicionStr == "" ? null : CondicionStr);
            ALValorParametrosInternos.Add(cmbTipo.SelectedItem.Text);
            ALValorParametrosInternos.Add(dpFecha.SelectedDate);
            ALValorParametrosInternos.Add(sesion.U_Nombre);
            ALValorParametrosInternos.Add(sesion.Emp_Nombre);
            ALValorParametrosInternos.Add(sesion.Cd_Nombre);
            ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos));

            
            Calendario calendario = new Calendario();
            CapaNegocios.CN_CatCalendario CN_CatCalendario = new CN_CatCalendario();

            CN_CatCalendario.ConsultaCalendarioActual(ref calendario, sesion);
            string Mes = calendario.Cal_Mes < 10 ? "0" + "" + calendario.Cal_Mes : calendario.Cal_Mes.ToString();
            string str = "" + calendario.Cal_Año + Mes + "01";
            string Dia = (calendario.Cal_Mes == 2) ? "28" : "30";
            string strFin = "" + calendario.Cal_Año + Mes + Dia;
            string[] format = { "yyyyMMdd" };
            DateTime date;

            if (!DateTime.TryParseExact(str, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
            {
                date = sesion.CalendarioIni;
            }

                   

            Type instance = null;

            switch (cmbTipo.SelectedValue)
            {
                case "1":
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_InvRotacion);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_InvRotacion);
                    }
                    break;
                case "2":
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_InvRotacion2);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_InvRotacion2);
                    }
                    break;
                case "3":
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_InvRotacion3);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_InvRotacion3);
                    }
                    break;
                case "4":
                    ALValorParametrosInternos.Add(txtCliente.Text);
                    //ALValorParametrosInternos.Add(date);
                    if (chkDetalle.Checked)
                    {

                        if (a_pantalla)
                        {
                            //instance = typeof(LibreriaReportes.Rep_InvRotacion4a);
                            instance = typeof(LibreriaReportes.ExpRep_InvRotacion4Consig);
                            
                        }
                        else
                        {
                            //instance = typeof(LibreriaReportes.ExpRep_InvRotacion4a);
                            instance = typeof(LibreriaReportes.ExpRep_InvRotacion4Consig);
                        }
                    }
                    else
                    {
                        ALValorParametrosInternos.Add(date);
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_InvRotacion4b);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_InvRotacion4b);
                        }
                    }
                    break;
                case "5":
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_InvRotacion5);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_InvRotacion5);
                    }
                    break;
            }



            //NOTA: El estatus de impresión, lo pone cuando el reporte se carga

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
            // RAM1.ResponseScripts.Add("refreshGrid();");
            //rgPago.Rebind();
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
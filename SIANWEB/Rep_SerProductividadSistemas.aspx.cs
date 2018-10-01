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
    public partial class Rep_SerProductividadSistemas : System.Web.UI.Page
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

            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        //protected void rbFecha_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtFecha.Enabled = false;
        //    cmbAnioInicio.Enabled = false;
        //    cmbMesInicio.Enabled = false;
        //    cmbAnioFin.Enabled = false;
        //    cmbMesFin.Enabled = false;

        //    switch (((RadioButtonList)sender).SelectedItem.Value.ToUpper())
        //    { 
        //        case "CALENDARIO":
        //            txtFecha.Enabled = true;;
        //            this.HD_tipoPeriodo.Value = "CALENDARIO";
        //            break;
        //        case "MENSUAL":
        //            cmbAnioInicio.Enabled = true;
        //            cmbMesInicio.Enabled = true;
        //            cmbAnioFin.Enabled = true;
        //            cmbMesFin.Enabled = true;
        //            this.HD_tipoPeriodo.Value = "MENSUAL";
        //            break;
        //    }
        //}
        #endregion
        #region Funciones
        private void Imprimir(bool a_pantalla)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                //Validar captura correcta de Rangos            
                int error = 0;
                if (!string.IsNullOrEmpty(txtGrupo.Text))
                    this.ValidaRangosSintaxis(txtGrupo.Text, ref error);

                if (!string.IsNullOrEmpty(txtRepresentante.Text))
                    this.ValidaRangosSintaxis(txtRepresentante.Text, ref error);

                if (!string.IsNullOrEmpty(txtCliente.Text))
                    this.ValidaRangosSintaxis(txtCliente.Text, ref error);

                if (error == 1)
                    return;
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

                ALValorParametrosInternos.Add(txtGrupo.Text.Trim() == string.Empty ? "Todos" : txtGrupo.Text.Trim());
                ALValorParametrosInternos.Add(txtGrupo.Text);

                ALValorParametrosInternos.Add(txtRepresentante.Text.Trim() == string.Empty ? "Todos" : txtRepresentante.Text.Trim());
                ALValorParametrosInternos.Add(txtRepresentante.Text);

                ALValorParametrosInternos.Add(txtCliente.Text.Trim() == string.Empty ? "Todos" : txtCliente.Text.Trim());
                ALValorParametrosInternos.Add(txtCliente.Text);

                ALValorParametrosInternos.Add(rbFecha.SelectedItem.Text);

                if (rbFecha.SelectedValue == "CALENDARIO")
                    ALValorParametrosInternos.Add(Convert.ToDateTime(txtFecha.SelectedDate).ToString("dd/MM/yyyy"));
                else
                {
                    ALValorParametrosInternos.Add(cmbMesInicio.SelectedValue);
                    ALValorParametrosInternos.Add(cmbAnioInicio.SelectedValue);
                    ALValorParametrosInternos.Add(cmbMesInicio.SelectedValue);
                    ALValorParametrosInternos.Add(cmbAnioInicio.SelectedValue);
                }
                ALValorParametrosInternos.Add(cmbOrden.SelectedValue);
                ALValorParametrosInternos.Add(cmbOrden.SelectedItem.Text);
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                Type instance = null;
                if (rbFecha.SelectedValue == "CALENDARIO")
                {
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_SerProductividadSisPropietarios);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_SerProductividadSisPropietarios);
                    }
                }
                else
                {
                    if (CheckBox1.Checked)
                    {
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_SerProductividadSisPropietariosPeriodos2);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_SerProductividadSisPropietariosPeriodos2);
                        }
                    }
                    else
                    {
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_SerProductividadSisPropietariosPeriodos);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_SerProductividadSisPropietariosPeriodos);
                        }
                    }
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
        private void ValidaRangosSintaxis(string cadena, ref int error)
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
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarCentros();
            //Llenar combos de periodos (Años)
            for (int i = DateTime.Now.Year + 1; i > 2000; i--)
            {
                cmbAnioInicio.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                cmbAnioFin.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
            }
            cmbAnioInicio.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbAnioFin.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            //cmbAnioInicio.Enabled = false;

            CN_CatCalendario cn_catcalendario = new CN_CatCalendario();
            Calendario cal = new Calendario();

            cn_catcalendario.ConsultaCalendarioActual(ref cal, sesion);

            cmbAnioInicio.SelectedIndex = cmbAnioInicio.FindItemIndexByValue(cal.Cal_Año.ToString());
            cmbMesInicio.SelectedIndex = cmbMesInicio.FindItemIndexByValue(cal.Cal_Mes.ToString());
            //cmbMesInicio.Enabled = false;
            cmbAnioFin.Enabled = false;
            cmbMesFin.Enabled = false;
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
                    //_PermisoGuardar = Permiso.PGrabar;
                    //_PermisoModificar = Permiso.PModificar;
                    //_PermisoEliminar = Permiso.PEliminar;
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
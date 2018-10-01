using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Globalization;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CatGastos : System.Web.UI.Page
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
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();
                        Inicializar();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                Nuevo();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        //Nuevo();
                        break;

                    case "save":
                        Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CargarGasto();
                tbPrincipal.Visible = true;
                //cmbAño.Enabled = false;
                //cmbMes.Enabled = false;

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }
        #endregion

        #region Funciones
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

                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.RadToolBar1.Items[5].Visible = false;
                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                Gasto gasto = new Gasto();
                gasto.Id_Emp = session.Id_Emp;
                gasto.Id_Cd = session.Id_Cd_Ver;
                gasto.Año = Convert.ToInt32(cmbAño.SelectedValue);
                gasto.Mes = Convert.ToInt32(cmbMes.SelectedValue);
                gasto.VarFlet = Convert.ToDouble(txtVarFlete.Text);
                gasto.VarFletPagado = Convert.ToDouble(txtVarPagados.Text);
                gasto.VarFletDevolucion = Convert.ToDouble(txtvarDevolucion.Text);
                gasto.FijGenerales = Convert.ToDouble(txtFijGenerales.Text);
                gasto.FijAdministracion = Convert.ToDouble(txtFijAdministracion.Text);
                gasto.FijOcupacion = Convert.ToDouble(txtFijOcupacion.Text);
                gasto.FijAlmacen = Convert.ToDouble(txtFijAlmacen.Text);
                gasto.FijServicio = Convert.ToDouble(txtFijServicio.Text);
                gasto.FijCobranza = Convert.ToDouble(txtFijCobranza.Text);
                gasto.UCS = Convert.ToDouble(txtUCS.Text);


                int verificador = -1;

                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }
                if (HF_ID.Value != "0")
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                }
                CN_CatGasto cngasto = new CN_CatGasto();
                cngasto.InsertarGasto(gasto, session.Emp_Cnx, ref verificador);
                Nuevo();
                if (verificador == 1)
                {
                    if (HF_ID.Value == "0")
                        Alerta("Los datos se guardaron correctamente");
                    else
                        Alerta("Los datos se modificaron correctamente");
                }
                else
                {
                    if (HF_ID.Value == "0")
                        Alerta("Ocurrió un error al intentar guardar los datos");
                    else
                        Alerta("Ocurrió un error al intentar guardar los cambios");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            cmbAño.SelectedIndex = 0;
            cmbAño.Enabled = true;
            cmbMes.SelectedIndex = 0;
            cmbMes.Enabled = true;
            tbPrincipal.Visible = false;
            txtVarFlete.Text = string.Empty;
            txtVarPagados.Text = string.Empty;
            txtvarDevolucion.Text = string.Empty;
            txtFijGenerales.Text = string.Empty;
            txtFijAdministracion.Text = string.Empty;
            txtFijOcupacion.Text = string.Empty;
            txtFijAlmacen.Text = string.Empty;
            txtFijServicio.Text = string.Empty;
            txtFijCobranza.Text = string.Empty;
            txtUCS.Text = string.Empty;
        }
        private void CargarGasto()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatGasto clsCatAdenda = new CN_CatGasto();
                Gasto gasto = new Gasto();
                gasto.Id_Emp = sesion.Id_Emp;
                gasto.Id_Cd = sesion.Id_Cd_Ver;
                gasto.Año = Convert.ToInt32(cmbAño.SelectedValue);
                gasto.Mes = Convert.ToInt32(cmbMes.SelectedValue);
                clsCatAdenda.ConsultaGasto(ref gasto, sesion.Emp_Cnx);

                txtVarFlete.Text = gasto.VarFlet.ToString("#,##0.00");
                txtVarPagados.Text = gasto.VarFletPagado.ToString("#,##0.00");
                txtvarDevolucion.Text = gasto.VarFletDevolucion.ToString("#,##0.00");
                txtFijGenerales.Text = gasto.FijGenerales.ToString("#,##0.00");
                txtFijAdministracion.Text = gasto.FijAdministracion.ToString("#,##0.00");
                txtFijOcupacion.Text = gasto.FijOcupacion.ToString("#,##0.00");
                txtFijAlmacen.Text = gasto.FijAlmacen.ToString("#,##0.00");
                txtFijServicio.Text = gasto.FijServicio.ToString("#,##0.00");
                txtFijCobranza.Text = gasto.FijCobranza.ToString("#,##0.00");
                txtUCS.Text = gasto.UCS.ToString("#,##0.00");
                if (gasto.Año == 0)
                    HF_ID.Value = "0";
                else
                    HF_ID.Value = "1";
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
        private void Inicializar()
        {
            cmbAño.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            for (int x = DateTime.Now.AddYears(-3).Year; x < DateTime.Now.AddYears(3).Year; x++)
                cmbAño.Items.Add(new RadComboBoxItem(x.ToString(), x.ToString()));

            CultureInfo cultura = CultureInfo.CurrentCulture;
            cmbMes.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            for (int x = 1; x < 13; x++)
                cmbMes.Items.Add(new RadComboBoxItem(cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.GetMonthName(x)), x.ToString()));
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
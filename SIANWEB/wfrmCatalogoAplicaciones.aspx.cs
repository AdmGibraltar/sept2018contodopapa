using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CrmCatAplicaciones : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (session.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        //CargarCentros();
                        Inicializar();
                    }
                }
            }

            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ibtnCrear_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //'Me.pnlAgregar.Visible = true
                this.ddlUENs.Enabled = false;
                this.ddlSegmentos.Enabled = false;
                this.ddlAreas.Enabled = false;
                this.ddlSol.Enabled = false;
                //''''''''''''
                trAreas.Visible = true;
                trPotencial.Visible = true;
                //this.lblAreas.Visible = true;
                //this.txtPosicion.Visible = true;
                //this.btnAgregar.Visible = true;
                //this.btnDeshacer.Visible = true;
                //this.lblPPotencial.Visible = true;
                //this.txtPotencial.Visible = true;
                //this.lblPorcentaje.Visible = true;
                this.txtPosicion.Focus();
                HF_Modificar.Value = "";
                ibtnCrear.Visible = false;
                Label1.Visible = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        protected void btnDeshacer_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //'this.pnlAgregar.Visible = false;
                this.txtPosicion.Text = "";
                txtPotencial.Text = "";
                this.ddlUENs.Enabled = true;
                this.ddlSegmentos.Enabled = true;
                this.ddlAreas.Enabled = true;
                this.ddlSol.Enabled = true;
                //''''''''''''
                trAreas.Visible = false;
                trPotencial.Visible = false;
                //this.lblAreas.Visible = false;
                //this.txtPosicion.Visible = false;
                //this.btnAgregar.Visible = false;
                //this.btnDeshacer.Visible = false;
                //this.lblPPotencial.Visible = false;
                //this.txtPotencial.Visible = false;
                //this.lblPorcentaje.Visible = false;
                HF_Modificar.Value = "";
                ibtnCrear.Visible = true;
                Label1.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void cmbUEN_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                CargarSegmentos();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbSegmento_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                CargarAreas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAplicaciones_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAplicaciones.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAplicaciones_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                rgAplicaciones.Rebind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void rgAplicaciones_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        protected void rgAplicaciones_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridItem gi = null;
                gi = e.Item;
                if (e.CommandName == "Modificar")
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }


                    txtPosicion.Text = gi.Cells[4].Text;
                    txtPotencial.Text = gi.Cells[5].Text;

                    ibtnCrear_Click(null, null);

                    HF_Modificar.Value = gi.Cells[3].Text;
                }
                else
                    if (e.CommandName == "Delete")
                    {

                        int verificador = 0;
                        if (!_PermisoEliminar)
                        {
                            Alerta("No tiene permisos para eliminar");
                            return;
                        }

                        CN_CrmCatAplicacion clsaplicacion = new CN_CrmCatAplicacion();
                        Aplicacion aplicacion = new Aplicacion();
                        aplicacion.Id_Apl = Convert.ToInt32(gi.Cells[2].Text);
                        aplicacion.Id_Emp = session.Id_Emp;
                        clsaplicacion.Eliminar(aplicacion, ref verificador, session.Emp_Cnx);

                        if (verificador == 1)
                        {
                            rgAplicaciones.Rebind();
                            ddlUENs.Enabled = true;
                            ddlSegmentos.Enabled = true;
                        }
                        else if (verificador == -2)
                        {
                            Alerta("No se puede eliminar la aplicación, ya fue asignada a un proyecto");
                        }
                    }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE constraint"))
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                }
                else
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }
        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                

                CN_CrmCatAplicacion clsaplicacion = new CN_CrmCatAplicacion();
                Aplicacion aplicacion = new Aplicacion();
                aplicacion.Id_Emp = session.Id_Emp;
                aplicacion.Id_Cd = session.Id_Cd_Ver;
                aplicacion.Id_Sol = Convert.ToInt32(ddlSol.SelectedValue);
                aplicacion.Apl_Descripcion = txtPosicion.Text;
                aplicacion.Apl_Potencial = txtPotencial.Value.Value;
                int verificador = 0;
                if (HF_Modificar.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    clsaplicacion.Insertar(aplicacion, session.Emp_Cnx, ref verificador);
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    aplicacion.Id_Apl = Convert.ToInt32(HF_Modificar.Value);
                    clsaplicacion.Modificar(aplicacion, session.Emp_Cnx, ref verificador);
                }
                if (verificador > 0)
                {
                    rgAplicaciones.Rebind();

                    this.txtPosicion.Text = "";
                    this.txtPotencial.Text = "";
                    this.ddlUENs.Enabled = true;
                    this.ddlSegmentos.Enabled = true;
                    ddlSol.Enabled = true;
                    ddlAreas.Enabled = true;
                    trAreas.Visible = false;
                    trPotencial.Visible = false;
                    //this.lblAreas.Visible = true;
                    //this.txtPosicion.Visible = true;
                    //this.btnAgregar.Visible = true;
                    //this.btnDeshacer.Visible = true;
                    //this.lblPPotencial.Visible = true;
                    //this.txtPotencial.Visible = true;
                    //this.lblPorcentaje.Visible = true;
                    this.txtPosicion.Focus();
                    HF_Modificar.Value = "";
                    ibtnCrear.Visible = true;
                    Label1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlAreas_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                CargarSolucion();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlSol_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                rgAplicaciones.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;


                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

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
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    //


                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);

                    return false;
                }
                return true;
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
                CargarUEN();
                CargarSegmentos();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            //txtClave.Text = Valor;

        }
        private void CargarUEN()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, session.Emp_Cnx, "spCatUen_Combo", ref ddlUENs);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                ddlUENs.Items.Remove(0);
                ddlUENs.SelectedIndex = 0;
                ddlUENs.Text = ddlUENs.Items[0].Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSegmentos()
        {
            try
            {
                if (ddlUENs.SelectedValue == "")
                {
                    ddlUENs.SelectedIndex = 0;
                }

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlUENs.SelectedValue), session.Emp_Cnx, "spCatSegmentos_Combo", ref ddlSegmentos);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                if (ddlSegmentos.Items.Count > 1)
                {
                    ddlSegmentos.Items.Remove(0);
                    ddlSegmentos.SelectedIndex = 0;
                    ddlSegmentos.Text = ddlSegmentos.Items[0].Text;
                    //ddl.Visible = false;
                    ddlSegmentos.Visible = true;
                    pnlAgrega.Visible = true;
                    lblMensajes.Text = "";
                    CargarAreas();
                }
                else
                {

                    ddlSegmentos.Items.Clear();
                    ddlSegmentos.Text = "";
                    ddlAreas.Items.Clear();
                    ddlAreas.Text = "";
                    ddlSol.Items.Clear();
                    ddlSol.Text = "";
                    //this.ddlSegmentos.Visible = false;
                    this.pnlAgrega.Visible = false;
                    this.lblMensajes.Text = "No se ha registrado ningún segmento en la UEN seleccionada";
                    rgAplicaciones.Rebind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAreas()
        {
            try
            {
                if (ddlSegmentos.SelectedValue == "")
                {
                    ddlSegmentos.SelectedIndex = 0;
                }

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlSegmentos.SelectedValue), session.Emp_Cnx, "spCatAreaSegmento_Combo", ref ddlAreas);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                if (ddlAreas.Items.Count > 1)
                {
                    ddlAreas.Items.Remove(0);
                    ddlAreas.SelectedIndex = 0;
                    ddlAreas.Text = ddlAreas.Items[0].Text;
                    this.CargarSolucion();
                    this.ddlAreas.Visible = true;
                    lblMensajes.Text = "";
                }
                else
                {
                    ddlAreas.Items.Clear();
                    ddlAreas.Text = "";
                    ddlSol.Items.Clear();
                    ddlSol.Text = "";
                    //this.ddlSegmentos.Visible = false;
                    this.pnlAgrega.Visible = false;
                    this.lblMensajes.Text = "No se ha registrado ningún área en el segmento y UEN seleccionados";
                    rgAplicaciones.Rebind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSolucion()
        {
            try
            {
                if (ddlAreas.SelectedValue == "")
                {
                    ddlAreas.SelectedIndex = 0;
                }

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlAreas.SelectedValue), session.Emp_Cnx, "spCatSolucionArea_Combo", ref ddlSol);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                if (ddlSol.Items.Count > 1)
                {
                    ddlSol.Items.Remove(0);
                    ddlSol.SelectedIndex = 0;
                    ddlSol.Text = ddlSol.Items[0].Text;
                    this.pnlAgrega.Visible = true;
                    lblMensajes.Text = "";
                    this.ddlSol.Visible = true;
                    this.ddlAreas.Visible = true;
                }
                else
                {
                    ddlSol.Items.Clear();
                    ddlSol.Text = "";
                    //this.ddlSegmentos.Visible = false;
                    this.pnlAgrega.Visible = false;
                    this.lblMensajes.Text = "No se ha registrado ninguna solución en el área y segmento seleccionados";
                }
                rgAplicaciones.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Aplicacion> GetList()
        {
            try
            {
                List<Aplicacion> List = new List<Aplicacion>();
                CN_CrmCatAplicacion clsaplicacion = new CN_CrmCatAplicacion();
                Aplicacion aplicacion = new Aplicacion();
                aplicacion.Id_Emp = session.Id_Emp;
                aplicacion.Id_Cd = session.Id_Cd_Ver;
                aplicacion.Id_Sol = ddlSol.SelectedValue == "" ? -1 : Convert.ToInt32(ddlSol.SelectedValue);
                clsaplicacion.Lista(aplicacion, session.Emp_Cnx, ref List);
                return List;
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
                //this.lblMensaje.Text = "";
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
                //this.lblMensaje.Text = Message;
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
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}
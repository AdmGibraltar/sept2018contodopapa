using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Data;

namespace SIANWEB
{
    public partial class CrmCatArea : System.Web.UI.Page
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
                Session["Sesion" + Session.SessionID] = value;

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
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {

                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                    {
                        Guardar();
                    }
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    //Regresar()

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                int verificador = 0;
                GridItem gi = null;
                gi = e.Item;

                if (e.CommandName == "Modificar")
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }

                    
                    txtPosicion.Text = gi.Cells[3].Text;
                    txtPotencial.Text = gi.Cells[6].Text;

                    ibtnCrear_Click(null, null);

                    HF_Modificar.Value = gi.Cells[2].Text;
                }
                else
                {

                    if (!_PermisoEliminar)
                    {
                        Alerta("No tiene permisos para eliminar");
                        return;
                    }




                    CN_CatArea clsCatArea = new CN_CatArea();
                    Area area = new Area();
                    area.Id_Area = Convert.ToInt32(gi.Cells[2].Text);
                    area.Id_Emp = session.Id_Emp;
                    clsCatArea.Borrar(area, ref verificador, session.Emp_Cnx);
                }

                if (verificador == 1)
                {
                    rg1.Rebind();
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
        protected void cmbUEN_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarSegmentos();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbSegmento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                rg1.Rebind();
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
                //this..pnlAgregar.Visible = True
                this.cmbUEN.Enabled = false;
                this.cmbSegmento.Enabled = false;
                //
                this.lblAreas.Visible = true;
                this.txtPosicion.Visible = true;
                this.btnAgregar.Visible = true;
                this.btnDeshacer.Visible = true;
                this.lblPPotencial.Visible = true;
                this.txtPotencial.Visible = true;
                //this.lblPorcentaje.Visible = true;
                this.txtPosicion.Focus();

                HF_Modificar.Value = "";
                ibtnCrear.Visible = false;
                Label12.Visible = false;
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }
        protected void btnDeshacer_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                HF_Modificar.Value = "";
                this.txtPosicion.Text = "";
                txtPotencial.Text = "";
                this.cmbUEN.Enabled = true;
                this.cmbSegmento.Enabled = true;
                //''''''''''''
                this.lblAreas.Visible = false;
                this.txtPosicion.Visible = false;
                this.btnAgregar.Visible = false;
                this.btnDeshacer.Visible = false;
                this.lblPPotencial.Visible = false;
                this.txtPotencial.Visible = false;
                //this.lblPorcentaje.Visible = false;
                ibtnCrear.Visible = true;
                Label12.Visible = true;
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
                Guardar();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                //txtClave.Text = Valor;

                CargarUEN();

                // rg1.Rebind();
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
        private void CargarUEN()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, session.Emp_Cnx, "spCatUen_Combo", ref cmbUEN);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmbUEN.Items.Remove(0);
                cmbUEN.SelectedIndex = 0;
                cmbUEN.Text = cmbUEN.Items[0].Text;
                CargarSegmentos();
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
                if (cmbUEN.SelectedValue == "")
                {
                    cmbUEN.SelectedIndex = 0;
                }

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(cmbUEN.SelectedValue), session.Emp_Cnx, "spCatSegmentos_Combo", ref cmbSegmento);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                if (cmbSegmento.Items.Count > 1)
                {
                    cmbSegmento.Items.Remove(0);
                    cmbSegmento.SelectedIndex = 0;
                    cmbSegmento.Text = cmbSegmento.Items[0].Text;
                    cmbSegmento.Visible = true;
                    pnlAgrega.Visible = true;
                    lblMensajes.Text = "";
                }
                else
                {
                    cmbSegmento.Items.Clear();
                    cmbSegmento.Text = "";
                    this.pnlAgrega.Visible = false;
                    this.lblMensajes.Text = "No se ha registrado ningún segmento en la UEN seleccionada";
                }
                rg1.Rebind();
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
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

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
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Area> GetList()
        {
            try
            {
                List<Area> List = new List<Area>();
                CN_CatArea clsCatArea = new CN_CatArea();
                Area area = new Area();
                area.Id_Emp = session.Id_Emp;
                area.Id_Cd = session.Id_Cd_Ver;
                area.Id_Seg = cmbSegmento.SelectedValue == "" ? -1 : Convert.ToInt32(cmbSegmento.SelectedValue);
                clsCatArea.Lista(area, session.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            txtPotencial.Text = "";

            cmbUEN.SelectedIndex = 0;
            cmbUEN.Text = cmbUEN.Items[0].Text;

            cmbSegmento.Items.Clear();
            cmbSegmento.Text = "";
        }
        private void Guardar()
        {
            try
            {
                Area area = new Area();
                area.Id_Area = -1;
                area.Area_Descripcion = txtPosicion.Text;
                area.Id_Emp = session.Id_Emp;
                area.Id_Uen = Convert.ToInt32(this.cmbUEN.SelectedValue);
                area.Id_Seg = Convert.ToInt32(this.cmbSegmento.SelectedValue);
                area.Area_Potencial = txtPotencial.Value.Value;
                area.Estatus = true;

                CN_CatArea clsCatArea = new CN_CatArea();
                int verificador = -1;


                //if (!_PermisoGuardar)
                //{
                //    Alerta("No tiene permisos para grabar");
                //    return;
                //}
                
                if (HF_Modificar.Value == "")
                {
                    clsCatArea.Insertar(area, session.Emp_Cnx, ref verificador);
                }
                else
                {
                    area.Id_Area = Convert.ToInt32(HF_Modificar.Value);
                    clsCatArea.Modificar(area, session.Emp_Cnx, ref verificador);
                }
                if (verificador == 1)
                {
                    // Nuevo();
                    FinGuardar();
                    HF_Modificar.Value = "";
                    //Alerta("Los datos se guardaron correctamente");
                }
                else
                {
                    Alerta("Ocurrio un error al intentar guardar");
                }
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void FinGuardar()
        {
            try
            {
                rg1.Rebind();
                //'Me.pnlAgregar.Visible = false;
                this.txtPosicion.Text = "";
                this.txtPotencial.Text = "";
                this.cmbUEN.Enabled = true;
                this.cmbSegmento.Enabled = true;
                this.lblAreas.Visible = false;
                this.txtPosicion.Visible = false;
                this.btnAgregar.Visible = false;
                this.btnDeshacer.Visible = false;
                this.lblPPotencial.Visible = false;
                this.txtPotencial.Visible = false;
                //this.lblPorcentaje.Visible = false;
                this.txtPosicion.Focus();
                ibtnCrear.Visible = true;
                Label12.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(session.Id_Emp, "CatArea", "Id_Area", session.Emp_Cnx, "spCatCentral_Maximo");
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
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
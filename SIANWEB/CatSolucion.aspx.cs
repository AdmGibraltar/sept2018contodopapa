using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaNegocios;
using CapaEntidad;

namespace SIANWEB
{
    public partial class CatSolucion : System.Web.UI.Page
    {
        #region Variables
                private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
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

                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (session.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        CargarCentros();
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbUEN_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                CargarSegmentos();

                cmbSegmento.SelectedIndex = 0;
                cmbSegmento.Text = cmbSegmento.Items.Count == 0 ? "" : cmbSegmento.Items[0].Text;
                txtSegmento.Text = "";

                cmbArea.Items.Clear();
                cmbArea.Text = "";
                txtArea.Text = "";
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Inicializar()
        {
            txtClave.Text = Valor;
            rg1.Rebind();
            CargarUEN();
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                session.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);

                Nuevo();
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
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
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
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
                if (e.CommandName.ToString() == "Modificar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);

                        txtClave.Enabled = false;
                        HF_ID.Value = rg1.Items[item]["Id_Sol"].Text;
                        txtClave.Text = rg1.Items[item]["Id_Sol"].Text;
                        txtDescripcion.Text = rg1.Items[item]["Sol_Descripcion"].Text;
                        txtSegmento.Text = rg1.Items[item]["Id_Seg"].Text;
                        txtUen.Text = rg1.Items[item]["Id_Uen"].Text;
                        cmbUEN.SelectedIndex = cmbUEN.FindItemIndexByValue(rg1.Items[item]["Id_Uen"].Text);
                        cmbUEN.Text = cmbUEN.FindItemByValue(rg1.Items[item]["Id_Uen"].Text).Text;

                        CargarSegmentos();
                        txtSegmento.Text = rg1.Items[item]["Id_Seg"].Text;
                        cmbSegmento.SelectedIndex = cmbSegmento.FindItemIndexByValue(rg1.Items[item]["Id_Seg"].Text);
                        cmbSegmento.Text = cmbSegmento.FindItemByValue(rg1.Items[item]["Id_Seg"].Text).Text;

                        CargarArea();
                        txtArea.Text = rg1.Items[item]["Id_Area"].Text;
                        cmbArea.SelectedIndex = cmbArea.FindItemIndexByValue(rg1.Items[item]["Id_Area"].Text);
                        cmbArea.Text = cmbArea.FindItemByValue(rg1.Items[item]["Id_Area"].Text).Text;



                        this.txtPotencial.Text = rg1.Items[item]["Sol_Potencial"].Text;
                        chkActivo.Checked = Convert.ToBoolean(rg1.Items[item]["Estatus"].Text);


                    }
                }
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
                CargarArea();

                cmbArea.SelectedIndex = 0;
                cmbArea.Text = cmbArea.Items.Count == 0 ? "" : cmbArea.Items[0].Text;
                txtArea.Text = "";
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!((CheckBox)sender).Checked && HF_ID.Value != "")
                {
                    if (!Deshabilitar())
                    {
                        Alerta("El registro está siendo utilizado por otro componente");
                        ((CheckBox)sender).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void CargarUEN()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, session.Emp_Cnx, "spCatUen_Combo", ref cmbUEN);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
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
                if (cmbUEN.SelectedIndex != 0)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(cmbUEN.SelectedValue), session.Emp_Cnx, "spCatSegmentosUen_Combo", ref cmbSegmento);
                }
                else
                {
                    cmbSegmento.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarArea()
        {
            try
            {
                if (cmbSegmento.SelectedIndex != 0)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(cmbSegmento.SelectedValue), session.Emp_Cnx, "spCatAreaSegmento_Combo", ref cmbArea);
                    //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                }
                else
                {
                    cmbArea.Items.Clear();
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

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (session.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, session.Id_Emp, session.Id_U, session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(session.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, session.Id_Emp, session.Id_U, session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = session.Id_Cd_Ver.ToString();
                }
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
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                    {
                        this.rtb1.Items[6].Visible = false;
                    }
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.rtb1.Items[5].Visible = false;
                    }
                    //if (Permiso.PEliminar == false)
                    //{
                    //    this.RadToolBar1.Items[3].Visible = false;
                    //}
                    //if(Permiso.PImprimir == false)
                    //{
                    //    this.RadToolBar1.Items[2].Visible = false;
                    //}

                    //Nuevo
                    //Me.RadToolBar1.Items(6).Enabled = False
                    //Guardar
                    //Me.RadToolBar1.Items(5).Enabled = False
                    //Regresar
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = false;
                    //Correo
                    this.rtb1.Items[1].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(session, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(session.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Solucion> GetList()
        {
            try
            {
                List<Solucion> List = new List<Solucion>();
                CN_CatSolucion clsCatSolucion = new CN_CatSolucion();
                Solucion solucion = new Solucion();
                solucion.Id_Emp = session.Id_Emp;
                clsCatSolucion.Lista(solucion, session.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            HF_ID.Value = "";
            txtClave.Enabled = true;
            txtClave.Text = Valor;
            txtDescripcion.Text = "";
            txtUen.Text = "";
            txtSegmento.Text = "";
            txtPotencial.Text = "";
            txtArea.Text = "";
            cmbUEN.SelectedIndex = 0;
            cmbUEN.Text = cmbUEN.Items[0].Text;

            cmbSegmento.Items.Clear();
            cmbSegmento.Text = "";

            cmbArea.Items.Clear();
            cmbArea.Text = "";

            chkActivo.Checked = true;
        }
        private void Guardar()
        {
            try
            {
                Solucion solucion = new Solucion();
                solucion.Id_Sol = Convert.ToInt32(txtClave.Text);
                solucion.Id_Area = Convert.ToInt32(cmbArea.SelectedValue);
                solucion.Sol_Descripcion = txtDescripcion.Text;
                solucion.Id_Emp = session.Id_Emp;
                solucion.Sol_Potencial = txtPotencial.Value.Value;
                solucion.Estatus = chkActivo.Checked;
                CN_CatSolucion clsCatSolucion = new CN_CatSolucion();
                int verificador = -1;

                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    clsCatSolucion.Insertar(solucion, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        Nuevo();
                        Alerta("Los datos se guardaron correctamente");
                    }
                    else
                    {
                        Alerta("La clave ya existe");
                    }
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }

                    clsCatSolucion.Modificar(solucion, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar modificar los datos");
                    }
                }
                rg1.Rebind();
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
                return CN_Comun.Maximo(session.Id_Emp, "CatSolucion", "Id_Sol", session.Emp_Cnx, "spCatCentral_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HF_ID.Value != "")
                {

                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = session.Id_Emp;
                    ct.Id_Cd = -1;
                    ct.Id = Convert.ToInt32(HF_ID.Value);
                    ct.Tabla = "CatSolucion";
                    ct.Columna = "Id_Sol";
                    CN_Comun.Deshabilitar(ct, session.Emp_Cnx, ref verificador);
                }
                return verificador;
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
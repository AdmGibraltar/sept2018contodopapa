using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using System.IO;
using CapaNegocios;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class CatSistemasPropietarios : System.Web.UI.Page
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
                    RAM.ResponseScripts.Add("IrALogin()");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM.ResponseScripts.Add("AbrirContrasenas(); return false;");
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


        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                RAM.ResponseScripts.Add("ObtenerCentroVer(" + CmbCentro.SelectedValue + ");");
                Nuevo();
                Inicializar();
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
                if (_PermisoModificar)
                {
                    if (e.CommandName.ToString() == "Modificar")
                    {
                        Int32 item = default(Int32);
                        item = e.Item.ItemIndex;
                        if (item >= 0)
                        {                          
                                CN__Comun.RemoverValidadores(Validators);
                                txtClave.Enabled = false;
                                hiddenActualiza.Value = rg1.Items[item]["Id"].Text;
                                txtClave.Text = rg1.Items[item]["Id"].Text;
                                txtDescripcion.Text = rg1.Items[item]["Descripcion"].Text;
                                txtFactor.Text = rg1.Items[item]["Factor"].Text;
                                chkClase.Checked = Convert.ToBoolean(rg1.Items[item]["Clase"].Text);
                                chkActivo.Checked = Convert.ToBoolean(rg1.Items[item]["Estatus"].Text);                           
                        }
                    }
                }
                else
                    Alerta("No tiene permiso para modificar los registros");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemCommand");
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
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
                ErrorManager(ex, "rg1_NeedDataSource");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
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
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && hiddenActualiza.Value != "")
            {
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            txtClave.Text = Valor;
            txtClave.Enabled = true;
            rg1.Rebind();
            //CargarEmpresas();
        }
        void ValidarPermisos()
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

        void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);             

                CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                if (Sesion.U_MultiOfi == false)
                {
                    CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = CmbCentro.SelectedItem.Text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        List<SistemasPropietarios> GetList()
        {
            try
            {
                List<SistemasPropietarios> List = new List<SistemasPropietarios>();
                CN_CatSisPropietarios clsCatBanco = new CN_CatSisPropietarios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                clsCatBanco.ConsultaSisPropietarios(session2.Id_Emp, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void Guardar()
        {
            try
            {
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                SistemasPropietarios sp = new SistemasPropietarios();
                sp.Id = Convert.ToInt32(txtClave.Text);
                sp.Descripcion = txtDescripcion.Text;
                sp.Factor = Convert.ToDouble(txtFactor.Text);
                sp.Clase = chkClase.Checked;
                sp.Estatus = chkActivo.Checked;
                sp.Id_Emp = session.Id_Emp;
                CN_CatSisPropietarios clsCatSisPropietarios = new CN_CatSisPropietarios();
                int verificador = -1;

                if (hiddenActualiza.Value == "")
                {
                    clsCatSisPropietarios.InsertarSisPropietarios(sp, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        Nuevo();
                        Alerta("Los datos se guardaron correctamente");
                    }
                    else                   
                        Alerta("La clave ya existe");                  
                }
                else
                {
                    sp.Id_Anterior = Convert.ToInt32(hiddenActualiza.Value);
                    clsCatSisPropietarios.ModificarSisPropietarios(sp, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)                   
                        Alerta("Los datos se modificaron correctamente");                  
                    else                 
                        Alerta("Ocurrió un error al intentar modificar los datos");                 
                }
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void Nuevo()
        {
            txtClave.Text = Valor;
            txtClave.Enabled = true;
            txtDescripcion.Text = string.Empty;
            txtFactor.Text = string.Empty;

            chkActivo.Checked = true;
            chkClase.Checked = false;
            hiddenActualiza.Value = string.Empty;
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (hiddenActualiza.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = -1;
                    ct.Id = Convert.ToInt32(hiddenActualiza.Value);
                    ct.Tabla = "CatSistemaPropietario";
                    ct.Columna = "Id_Spo";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, "CatSistemaPropietario", "Id_Spo", Sesion.Emp_Cnx, "spCatCentral_Maximo");
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
                RAM.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
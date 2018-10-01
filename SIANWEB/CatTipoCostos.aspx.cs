using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;


namespace SIANWEB
{
    public partial class CatTipoCostos : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)               
                    Response.Redirect("Login.aspx");                
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
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        private void Inicializar()
        {
            this.rgTipoCosto.Rebind();
            this.hiddenActualiza.Value = string.Empty;
            txtId.Text = this.Valor;
            txtId.Focus();
        }

        #region Eventos
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && hiddenActualiza.Value != "")
            {
                if (!Deshabilitar())
                {
                    this.DisplayMensajeAlerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];    
                    Response.Redirect("login.aspx" , false);            
                }         
                CN__Comun comun = new CN__Comun();    
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }

        protected void rgTipoCosto_ItemCommand(object source, GridCommandEventArgs e)
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
                        hiddenActualiza.Value = rgTipoCosto.Items[item]["Id_Tco"].Text;
                        txtId.Text = rgTipoCosto.Items[item]["Id_Tco"].Text;
                        txtDescripcion.Text = rgTipoCosto.Items[item]["Tco_Descripcion"].Text;
                        chkActivo.Checked = Convert.ToBoolean(rgTipoCosto.Items[item]["Tco_Activo"].Text);
                        txtId.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgTipoCosto_ItemCommand"));
            }
        }

        protected void rgTipoCosto_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)               
                    rgTipoCosto.DataSource = GetList();               
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgTipoCosto_NeedDataSource"));
            }
        }

        protected void rgTipoCosto_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgTipoCosto.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
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
                        txtId.Enabled = true;
                        txtId.Focus();
                        break;

                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, hiddenActualiza.Value == string.Empty ? "CatTipoCosto_insert_error" : "CatTipoCosto_update_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        #endregion

        #region Funciones
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, "CatTCosto", "Id_Tco", sesion.Emp_Cnx, "spCatCentral_Maximo");
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
                if (hiddenActualiza.Value != "") //Hidden Field BANDERA
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = sesion.Id_Emp; //Si no ocupa empresa su catalogo se pone -1
                    ct.Id_Cd = -1; //Si no ocupa Centro de distribución se pone -1
                    ct.Id = Convert.ToInt32(hiddenActualiza.Value); //Si no ocupa Id se pone -1
                    ct.Tabla = "CatTCosto"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Tco"; //Nombre de la columna del ID del catalogo
                    CN_Comun.Deshabilitar(ct, sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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

                TipoCosto tipoCosto = new TipoCosto();
                tipoCosto.Id_Emp = session.Id_Emp;
                tipoCosto.Id_Tco = txtId.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtId.Text);
                tipoCosto.Tco_Descripcion = txtDescripcion.Text;
                tipoCosto.Tco_Activo = chkActivo.Checked;
                CN_CatTipoCosto clsCatTipoCosto = new CN_CatTipoCosto();
                int verificador = -1;
                if (hiddenActualiza.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        DisplayMensajeAlerta("PermisoGuardarNo");
                        return;
                    }
                    clsCatTipoCosto.InsertarTipoCosto(tipoCosto, session.Emp_Cnx, ref verificador);                  
                    this.LimpiarCampos();
                    txtId.Enabled = true;
                    txtId.Text = this.Valor;
                    txtId.Focus();
                    this.DisplayMensajeAlerta("TipoCosto_insert_ok");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        DisplayMensajeAlerta("PermisoModificarNo");
                        return;
                    }
                    clsCatTipoCosto.ModificarTipoCosto(tipoCosto, session.Emp_Cnx, ref verificador);
                    txtId.Enabled = false;
                    this.DisplayMensajeAlerta("TipoCosto_update_ok");
                }
                rgTipoCosto.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            txtId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            chkActivo.Checked = true;
            txtId.Enabled = true;
            txtId.Focus();
        }

        private List<TipoCosto> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<TipoCosto> listTipoCostos = new List<TipoCosto>();
                CN_CatTipoCosto clsCatTipoPrecio = new CN_CatTipoCosto();
                TipoCosto tipoPrecio = new TipoCosto();
                clsCatTipoPrecio.ConsultaTipoCosto(tipoPrecio, sesion.Emp_Cnx, sesion.Id_Emp, ref listTipoCostos);
                return listTipoCostos;
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
                if (mensaje.Contains("rgTipoCosto_NeedDataSource"))
                    Alerta("Error al cargar el Grid de tipos de costos");
                else
                    if (mensaje.Contains("rgTipoCosto_ItemCommand"))
                        Alerta("Error al ejecutar un evento (rgTipoCosto_ItemCommand) al cargar el Grid de tipo de costos");
                    else
                        if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                            Alerta("Error al cambiar de centro de distribución");
                        else
                            if (mensaje.Contains("radGrid_PageIndexChanged"))
                                Alerta("Error al cambiar de página");
                            else
                                if (mensaje.Contains("PermisoGuardarNo"))
                                    Alerta("No tiene permisos para grabar");
                                else
                                    if (mensaje.Contains("CatTipoCostoIdRepetida_error"))
                                        Alerta("La clave ya existe");
                                    else
                                        if (mensaje.Contains("CatTipoCostoDescripcionRepetida_error"))
                                            Alerta("La descripción ya existe");
                                        else
                                            if (mensaje.Contains("PermisoModificarNo"))
                                                Alerta("No tiene permisos para modificar");
                                            else
                                                if (mensaje.Contains("TipoCosto_insert_ok"))
                                                    Alerta("Los datos se guardaron correctamente");
                                                else
                                                    if (mensaje.Contains("TipoCosto_insert_error"))
                                                        Alerta("No se pudo guardar los datos del tipo de precio");
                                                    else
                                                        if (mensaje.Contains("TipoCosto_update_ok"))
                                                            Alerta("Los datos se modificaron correctamente");
                                                        else
                                                            if (mensaje.Contains("TipoCosto_update_error"))
                                                                Alerta("No se pudo actualizar los datos del tipo de precio");
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
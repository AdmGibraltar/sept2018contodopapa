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
    public partial class CatPrecios : System.Web.UI.Page
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
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];     
                    Response.Redirect("login.aspx" , false);
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
                        this.rgPrecios.Rebind();
                        this.CargarCentros();
                        this.hiddenActualiza.Value = string.Empty;
                        this.txtIdPrecio.Text = this.Valor;
                        this.txtIdPrecio.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region Botones

        #endregion

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
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }

        protected void rgPrecios_ItemCommand(object source, GridCommandEventArgs e)
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
                        hiddenActualiza.Value = rgPrecios.Items[item]["Id_Pre"].Text;
                        txtIdPrecio.Text = rgPrecios.Items[item]["Id_Pre"].Text;
                        txtIdPrecio.Enabled = false;
                        txtDescripcionPrecio.Text = rgPrecios.Items[item]["Pre_Descripcion"].Text;                      
                        cmbPrecio.SelectedValue = rgPrecios.Items[item]["Pre_Tipo"].Text;
                        txtDescripcionPrecio.Text = rgPrecios.Items[item]["Pre_Descripcion"].Text;
                        chkActivoPrecio.Checked = Convert.ToBoolean(rgPrecios.Items[item]["Pre_Activo"].Text);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFamilia_ItemCommand"));
            }
        }

        protected void rgPrecios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)               
                    rgPrecios.DataSource = GetList();              
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgPrecios_NeedDataSource"));
            }
        }

        protected void rgPrecios_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgPrecios.Rebind();
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
                        txtIdPrecio.Enabled = true;
                        txtIdPrecio.Focus();
                        break;

                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, hiddenActualiza.Value == string.Empty ? "FamProducto_insert_error" : "FamProducto_update_error");
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
                return CN_Comun.Maximo(sesion.Id_Emp, "CatTPrecio", "Id_Pre", sesion.Emp_Cnx, "spCatCentral_Maximo");
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
                    ct.Tabla = "CatTPrecio"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Pre"; //Nombre de la columna del ID del catalogo
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

                TipoPrecio tipoPrecio = new TipoPrecio();
                tipoPrecio.Id_Emp = session.Id_Emp;
                tipoPrecio.Id_Pre = txtIdPrecio.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtIdPrecio.Text);
                tipoPrecio.Pre_Descripcion = txtDescripcionPrecio.Text;
                tipoPrecio.Pre_Tipo = Convert.ToInt16(cmbPrecio.SelectedValue);
                tipoPrecio.Pre_Activo = chkActivoPrecio.Checked;
                CN_CatTipoPrecio clsCatTipoPrecio = new CN_CatTipoPrecio();
                int verificador = -1;

                if (hiddenActualiza.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        DisplayMensajeAlerta("PermisoGuardarNo");
                        return;
                    }
                    clsCatTipoPrecio.InsertarTipoPrecio(tipoPrecio, session.Emp_Cnx, ref verificador);                    
                    this.LimpiarCampos();
                    txtIdPrecio.Enabled = true;
                    txtIdPrecio.Text = this.Valor;
                    txtIdPrecio.Focus();
                    this.DisplayMensajeAlerta("TipoPrecio_insert_ok");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        DisplayMensajeAlerta("PermisoModificarNo");
                        return;
                    }
                    clsCatTipoPrecio.ModificarTipoPrecio(tipoPrecio, session.Emp_Cnx, ref verificador);
                    txtIdPrecio.Enabled = false;
                    this.DisplayMensajeAlerta("TipoPrecio_update_ok");
                }
                rgPrecios.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            try
            {
                txtIdPrecio.Text = string.Empty;
                txtDescripcionPrecio.Text = string.Empty;
                cmbPrecio.Text = "";
                cmbPrecio.ClearSelection();
                chkActivoPrecio.Checked = true;
                txtIdPrecio.Enabled = true;
                txtIdPrecio.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        private List<TipoPrecio> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<TipoPrecio> listTipoPrecios = new List<TipoPrecio>();
                CN_CatTipoPrecio clsCatTipoPrecio = new CN_CatTipoPrecio();
                TipoPrecio tipoPrecio = new TipoPrecio();
                clsCatTipoPrecio.ConsultaTipoPrecio(tipoPrecio, sesion.Emp_Cnx, sesion.Id_Emp, ref listTipoPrecios);
                return listTipoPrecios;
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
                if (mensaje.Contains("rgPrecios_NeedDataSource"))
                    Alerta("Error al cargar el Grid de tipos de precios");
                else
                    if (mensaje.Contains("rgPrecios_ItemCommand"))
                        Alerta("Error al ejecutar un evento (rgPrecios_ItemCommand) al cargar el Grid de tipo de precios de producto");
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
                                    if (mensaje.Contains("CatPreciosIdRepetida_error"))
                                        Alerta("La clave ya existe");
                                    else
                                        if (mensaje.Contains("CatPreciosDescripcionRepetida_error"))
                                            Alerta("La descripción ya existe");
                                        else
                                            if (mensaje.Contains("PermisoModificarNo"))
                                                Alerta("No tiene permisos para modificar");
                                            else
                                                if (mensaje.Contains("TipoPrecio_insert_ok"))
                                                    Alerta("Los datos se guardaron correctamente");
                                                else
                                                    if (mensaje.Contains("TipoPrecio_insert_error"))
                                                        Alerta("No se pudo guardar los datos del tipo de precio");
                                                    else
                                                        if (mensaje.Contains("TipoPrecio_update_ok"))
                                                            Alerta("Los datos se modificaron correctamente");
                                                        else
                                                            if (mensaje.Contains("TipoPrecio_update_error"))
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
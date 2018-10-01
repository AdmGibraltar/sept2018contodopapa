using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace SIANWEB
{
    public partial class CatProveedores : System.Web.UI.Page
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
            set
            {

            }
        }

        private string Emp_CnxCen
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
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
        private void Inicializar()
        {
            try
            {
                txtClave.Text = Valor;
                rgProveedores.Rebind();
                cargarTipoProveedor(-1);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void rgProveedores_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgProveedores.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rgProveedores_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgProveedores.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgProveedores_PageIndexChanged");
            }
        }

        private void cargarTipoProveedor(int Id_Tipo) {

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Emp_CnxCen, "spCatProveedorInternoTipo_Combo", ref cmbTipo);
        }
        protected void rgProveedores_ItemCommand(object source, GridCommandEventArgs e)
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
                        HFId_Proveedor.Value = rgProveedores.Items[item]["Id"].Text;
                        txtClave.Text = rgProveedores.Items[item]["Id"].Text;
                        txtClave.Enabled = false;
                        this.txtNombre.Text =  rgProveedores.Items[item]["Descripcion"].Text.Replace("&nbsp;", "");
                        this.txtCalle.Text = rgProveedores.Items[item]["Calle"].Text.Replace("&nbsp;", "");
                        this.txtNumero.Text = rgProveedores.Items[item]["Numero"].Text.Replace("&nbsp;", "");
                        this.txtCp.Text = rgProveedores.Items[item]["CP"].Text.Replace("&nbsp;", "");
                        this.txtColonia.Text = rgProveedores.Items[item]["Colonia"].Text.Replace("&nbsp;", "");
                        this.txtMunicipio.Text = rgProveedores.Items[item]["Municipio"].Text.Replace("&nbsp;", "");
                        string Telefono = string.IsNullOrEmpty(rgProveedores.Items[item]["Telefono"].Text) ? "" : rgProveedores.Items[item]["Telefono"].Text.Replace("&nbsp;", ""); 
                        Telefono = Telefono.Replace("-", "");
                        Telefono = Telefono.Replace("/", "");
                        Telefono = Telefono.Replace("_", "");
                        Telefono = Telefono.Replace(" ", "");
                        Telefono = Telefono.Replace(".", "");
                        Telefono = Telefono.Replace("(", "");
                        Telefono = Telefono.Replace(")", "");
                       
                        this.txtTelefono.Text = Telefono;

                        string Rfc = string.IsNullOrEmpty(rgProveedores.Items[item]["RFC"].Text) ? "" : rgProveedores.Items[item]["RFC"].Text.Replace("&nbsp;", "");
                        Rfc = Rfc.Replace("-", "");
                        Rfc = Rfc.Replace("/", "");
                        Rfc = Rfc.Replace("_", "");
                        Rfc = Rfc.Replace(" ", "");
                        Rfc = Rfc.Replace(".", "");
                        Rfc = Rfc.Replace("(", "");
                        Rfc = Rfc.Replace(")", "");


                        this.txtRfc.Text = Rfc;

                        string Fax = string.IsNullOrEmpty(rgProveedores.Items[item]["Fax"].Text) ? "" : rgProveedores.Items[item]["Fax"].Text.Replace("&nbsp;", "");
                        Fax = Fax.Replace("-", "");
                        Fax = Fax.Replace("/", "");
                        Fax = Fax.Replace("_", "");
                        Fax = Fax.Replace(" ", "");
                        Fax = Fax.Replace(".", "");
                        Fax = Fax.Replace("(", "");
                        Fax = Fax.Replace(")", "");
                        

                        this.txtFax.Text = Fax;
                        this.txtEmail.Text = rgProveedores.Items[item]["Correo"].Text.Replace("&nbsp;", "");
                        this.txtEstado.Text = rgProveedores.Items[item]["Estado"].Text.Replace("&nbsp;", "");
                        this.txtContacto.Text = rgProveedores.Items[item]["Contacto"].Text.Replace("&nbsp;", "");                        
                        this.cmbTipo.SelectedValue = rgProveedores.Items[item]["Tipo"].Text.Replace("&nbsp;", "");
                        this.LlenarTMov();
                        
                        this.txtPais.Text = rgProveedores.Items[item]["Pais"].Text.Replace("&nbsp;", "");
                        this.chkActivo.Checked = Convert.ToBoolean(rgProveedores.Items[item]["Estatus"].Text.Replace("&nbsp;", ""));
                        this.HF_Habilitar.Value = rgProveedores.Items[item]["Habilitar"].Text.Replace("&nbsp;", "");

                        if (Convert.ToBoolean(this.HF_Habilitar.Value))
                        {
                            deshabilitarcontroles(RadPageViewDGenerales.Controls, true);
                        }
                        else {
                            deshabilitarcontroles(RadPageViewDGenerales.Controls, false);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_ItemCommand");
            }
        }



        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos, bool valor)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls, valor);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = valor;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = valor;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = valor;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = valor;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = valor;
                        break;
                    case "RadTimePicker":
                        (controles_contenidos[x] as RadTimePicker).Enabled = valor;
                        break;
                    case "RadListBox":
                        (controles_contenidos[x] as RadListBox).Enabled = valor;
                        break;

                }
                if (Type.Contains("System.Web.UI.WebControls.CheckBox"))
                {
                    (controles_contenidos[x] as System.Web.UI.WebControls.CheckBox).Enabled = valor;
                }


            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        //Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
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
            Inicializar();
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && HFId_Proveedor.Value != "")
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
        }
        #endregion
        #region Funciones
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                //CargarAfectacion();

                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo( 2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
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

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.rtb1.Items[5].Visible = false;
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
        private List<Proveedores> GetList()
        {
            try
            {
                List<Proveedores> List = new List<Proveedores>();
                CN_CatProveedores clsCatProveedores = new CN_CatProveedores();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Proveedores prv = new Proveedores();
                prv.Empresa = session2.Id_Emp;
                prv.Centro = session2.Id_Cd_Ver;
                string bdCentral = (new SqlConnectionStringBuilder(Emp_CnxCen)).InitialCatalog;
                clsCatProveedores.ConsultaProveedores(prv, session2.Emp_Cnx, ref List, bdCentral);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void Nuevo()
        {
            try
            {
                HFId_Proveedor.Value = "";
                this.txtNombre.Text = "";
                txtClave.Text = Valor;
                txtClave.Enabled = true;
                this.txtCalle.Text = "";
                this.txtNumero.Text = "";
                this.txtCp.Text = "";
                this.txtColonia.Text = "";
                this.txtMunicipio.Text = "";
                this.txtTelefono.Text = "";
                this.txtRfc.Text = "";
                this.txtFax.Text = "";
                this.txtEmail.Text = "";
                this.txtEstado.Text = "";
                this.txtContacto.Text = "";
                //this.chkLocal.Checked = false;
                this.txtPais.Text = "";
               // this.chkFranquicia.Checked = false;
                chkActivo.Checked = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LlenarTMov()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatProveedores cn_Proveedor = new CN_CatProveedores();
            List<Movimientos> list = new List<Movimientos>();

            Movimientos mov = new Movimientos();
            mov.Id_Emp = sesion.Id_Emp;
            mov.Id =  Convert.ToInt32(cmbTipo.SelectedValue);
            string bdCentral = (new SqlConnectionStringBuilder(Emp_CnxCen)).InitialCatalog;
            mov.NatMov = -1;
            mov.Id_Cd = sesion.Id_Cd_Ver;
            cn_Proveedor.ConsultaTMov(mov, sesion.Emp_Cnx, ref list, bdCentral);           

            listMov.Enabled = false;           
            listMov.Items.Clear();
            foreach (Movimientos c in list)
            {
                RadListBoxItem rlbi = new RadListBoxItem();
                rlbi.Value = c.Id.ToString();
                rlbi.Text = c.Nombre;
                rlbi.Checked = true;
                listMov.Items.Add(rlbi);
            }

        }


        protected void CmbTipo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                LlenarTMov();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        private void Guardar()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Proveedores prv = new Proveedores();
                prv.Empresa = sesion.Id_Emp;
                prv.Calle = txtCalle.Text;
                prv.Colonia = txtColonia.Text;
                prv.Contacto = txtContacto.Text;
                prv.Correo = txtEmail.Text;
                prv.CP = txtCp.Text;
                prv.Descripcion = txtNombre.Text;
                prv.Estado = txtEstado.Text;
                prv.Estatus = chkActivo.Checked;
                prv.Fax = txtFax.Text;               
                prv.Municipio = txtMunicipio.Text;
                prv.Numero = txtNumero.Text;
                prv.Pais = txtPais.Text;
                prv.RFC = txtRfc.Text;
                prv.Telefono = txtTelefono.Text;
                prv.Tipo = Convert.ToInt32(cmbTipo.SelectedValue);

                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];

                List<ProveedorInternoTipo> List = new List<ProveedorInternoTipo>();
                CN_CatProveedores clsCatProveedores = new CN_CatProveedores();                
                ProveedorInternoTipo prvI = new ProveedorInternoTipo();
                prvI.Id_Emp = session2.Id_Emp;              
                string bdCentral = (new SqlConnectionStringBuilder(Emp_CnxCen)).InitialCatalog;
                clsCatProveedores.ConsultarClavePorTipo(prvI, session2.Emp_Cnx, ref List, bdCentral);
               
                List<ProveedorInternoTipo> LprvIT = null;

                ProveedorInternoTipo prvIT = null;


                int verificador = 0;
                if (HFId_Proveedor.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    prv.Id = Convert.ToInt32(txtClave.Text);

                    LprvIT = List.FindAll(a => a.Id_Tpvd == prv.Tipo && a.Tpvd_Valida == true);

                    if (LprvIT.Count() > 0)
                    {
                        prvIT = LprvIT.Find(a => a.Id_Pvd ==  prv.Id);
                        if (prvIT == null)
                        {
                            Alerta("La Clave del Proveedor no es Válida para este tipo de Proveedor");
                            return;
                        }
                    }

                    clsCatProveedores.InsertarProveedores(prv, session2.Emp_Cnx, ref verificador);
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
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    prv.Id = Convert.ToInt32(HFId_Proveedor.Value);

                    LprvIT = List.FindAll(a => a.Id_Tpvd == prv.Tipo && a.Tpvd_Valida == true);

                    if (LprvIT.Count() > 0 )
                    {
                        prvIT = LprvIT.Find(a => a.Id_Pvd == prv.Id);
                        if (prvIT == null)
                        {
                            Alerta("La Clave del Proveedor no es Válida para este tipo de Proveedor");
                            return;
                        }
                    }
                    clsCatProveedores.ModificarProveedores(prv, session2.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                        Alerta("Los datos se modificaron correctamente");
                    else
                        Alerta("Ocurrió un error al intentar guardar los cambios");
                }

                rgProveedores.Rebind();
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
                return CN_Comun.Maximo(Sesion.Id_Emp, "CatProveedor", "Id_Pvd", Sesion.Emp_Cnx, "spCatCentral_Maximo");
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
                if (HFId_Proveedor.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = 0;// Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HFId_Proveedor.Value);
                    ct.Tabla = "CatProveedor";
                    ct.Columna = "Id_Pvd";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
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
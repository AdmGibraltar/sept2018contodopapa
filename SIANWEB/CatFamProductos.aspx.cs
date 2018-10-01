using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;

using Telerik.Web.UI;

using CapaEntidad;
using CapaNegocios;
using System.IO;

namespace SIANWEB
{
    public partial class CatFamProductos : System.Web.UI.Page
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

        public string ValorSubFam
        {
            get
            {
                return MaximoIdSubFam();
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
                        this.LlenarComboFamilias();
                        this.rgFamilia.Rebind();
                        this.CargarCentros();
                        this.hiddenPanelActivo.Value = "fam";
                        this.panelFamilias.Style.Add("display", "block");
                        this.panelSubFamilias.Style.Add("display", "none");
                        this.ActivaPanelFamilias();
                        this.hiddenActualiza.Value = string.Empty;
                        this.hiddenActualizaSub.Value = string.Empty;
                        txtIdFam.Focus();
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

        protected void chkActivo_CheckedChanged_SubFam(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && hiddenActualiza.Value != "")
            {
                if (!DeshabilitarSubFam())
                {
                    this.DisplayMensajeAlerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
            }
        }

        protected void optOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (optOpciones.SelectedItem.Value == "Familias")               
                    this.ActivaPanelFamilias();                
                else             
                    this.ActivaPanelSubFamilias();               
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "optOpciones_SelectedIndexChanged"));
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

        protected void rgFamilia_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)              
                    rgFamilia.DataSource = GetList();               
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFamilia_NeedDataSource"));
            }
        }

        protected void rgSubFamilia_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)               
                    rgSubFamilia.DataSource = GetListSubFamilia();               
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgSubFamilia_NeedDataSource"));
            }
        }

        protected void rgFamilia_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgFamilia.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFamilia_PageIndexChanged"));
            }
        }

        protected void rgSubFamilia_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgSubFamilia.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgSubFamilia_PageIndexChanged"));
            }
        }

        protected void rgFamilia_ItemCommand(object source, GridCommandEventArgs e)
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
                        hiddenActualiza.Value = rgFamilia.Items[item]["Id_Fam"].Text;
                        txtIdFam.Text = rgFamilia.Items[item]["Id_Fam"].Text;
                        txtIdFam.Enabled = false;
                        txtDescripcionFam.Text = rgFamilia.Items[item]["Fam_Descripcion"].Text;
                        chkActivo.Checked = Convert.ToBoolean(rgFamilia.Items[item]["Fam_Activo"].Text);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFamilia_ItemCommand"));
            }
        }

        protected void rgSubFamilia_ItemCommand(object source, GridCommandEventArgs e)
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

                        hiddenActualizaSub.Value = rgSubFamilia.Items[item]["Id_Sub"].Text;
                        txtIdSubFam.Text = rgSubFamilia.Items[item]["Id_Sub"].Text;
                        txtIdSubFam.Enabled = false;
                        txtDescripcionSubFam.Text = rgSubFamilia.Items[item]["Sub_Descripcion"].Text;
                        cmbFamilia.SelectedIndex = cmbFamilia.FindItemIndexByValue(rgSubFamilia.Items[item]["Id_Fam"].Text);
                        cmbFamilia.Text = cmbFamilia.SelectedItem.Text;
                        chkActivoSubFam.Checked = Convert.ToBoolean(rgSubFamilia.Items[item]["Sub_Activo"].Text);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgSubFamilia_ItemCommand"));
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
                        if (this.hiddenPanelActivo.Value == "fam")
                        {
                            txtIdFam.Enabled = true;
                            txtIdFam.Focus();
                        }
                        else
                        {
                            txtIdSubFam.Enabled = true;
                            txtIdSubFam.Focus();
                        }
                        break;

                    case "save":
                        if (this.hiddenPanelActivo.Value == "fam")
                            this.Guardar();
                        else
                            this.GuardarSubFamilia();
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
                return CN_Comun.Maximo(sesion.Id_Emp, "CatFamilia", "Id_Fam", sesion.Emp_Cnx, "spCatCentral_Maximo");
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
                    ct.Tabla = "CatFamilia"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Fam"; //Nombre de la columna del ID del catalogo
                    CN_Comun.Deshabilitar(ct, sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MaximoIdSubFam()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, "CatSubfamilia", "Id_Sub", sesion.Emp_Cnx, "spCatCentral_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool DeshabilitarSubFam()
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
                    ct.Tabla = "CatSubfamilia"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Sub"; //Nombre de la columna del ID del catalogo
                    CN_Comun.Deshabilitar(ct, sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void ActivaPanelFamilias()
        {
            this.hiddenPanelActivo.Value = "fam";
            panelFamilias.Style.Add("display", "block");

            panelSubFamilias.Style.Add("display", "none");          
            this.hiddenActualiza.Value = string.Empty;

            rgFamilia.Rebind();
            this.LimpiaCamposFamilia();
            txtIdFam.Text = this.Valor;
        }

        private void ActivaPanelSubFamilias()
        {
            this.hiddenPanelActivo.Value = "subfam";
            panelFamilias.Style.Add("display", "none");          
            panelSubFamilias.Style.Add("display", "block");
            this.hiddenActualizaSub.Value = string.Empty;
            this.LlenarComboFamilias();

            rgSubFamilia.Rebind();
            this.LimpiaCamposSubFamilia();
            txtIdSubFam.Text = this.ValorSubFam;
        }

        private void LimpiaCamposFamilia()
        {
            try
            {
                hiddenActualiza.Value = string.Empty;
                txtIdFam.Text = string.Empty;
                txtDescripcionFam.Text = string.Empty;
                chkActivo.Checked = true;
                txtIdFam.Enabled = true;
                txtIdFam.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiaCamposSubFamilia()
        {
            try
            {
                hiddenActualizaSub.Value = string.Empty;
                txtIdSubFam.Text = string.Empty;
                txtDescripcionSubFam.Text = string.Empty;
                cmbFamilia.Text = "";
                cmbFamilia.ClearSelection();
                chkActivoSubFam.Checked = true;
                txtIdSubFam.Enabled = true;
                txtIdSubFam.Focus();
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
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                FamProducto famProducto = new FamProducto();
                famProducto.Id_Emp = session.Id_Emp;
                famProducto.Id_Fam = txtIdFam.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtIdFam.Text);
                famProducto.Fam_Descripcion = txtDescripcionFam.Text;
                famProducto.Fam_Activo = chkActivo.Checked;

                CN_CatFamProducto clsCatFamProducto = new CN_CatFamProducto();
                int verificador = -1;

                if (hiddenActualiza.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        DisplayMensajeAlerta("PermisoGuardarNo");
                        return;
                    }

                    clsCatFamProducto.InsertarFamProducto(famProducto, session.Emp_Cnx, ref verificador);
                    this.LimpiaCamposFamilia();
                    txtIdFam.Enabled = true;
                    txtIdFam.Text = this.Valor;
                    txtIdFam.Focus();
                    this.DisplayMensajeAlerta("FamProducto_insert_ok");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        DisplayMensajeAlerta("PermisoModificarNo");
                        return;
                    }
                    clsCatFamProducto.ModificarFamProducto(famProducto, session.Emp_Cnx, ref verificador);
                    txtIdFam.Enabled = false;
                    this.DisplayMensajeAlerta("FamProducto_update_ok");
                }
                rgFamilia.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuardarSubFamilia()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                SubFamProducto subFamProducto = new SubFamProducto();
                subFamProducto.Id_Emp = session.Id_Emp;
                subFamProducto.Id_Sub = Convert.ToInt32(txtIdSubFam.Text);
                subFamProducto.Sub_Descripcion = txtDescripcionSubFam.Text;
                subFamProducto.Id_Fam = Convert.ToInt32(cmbFamilia.SelectedValue);
                subFamProducto.Sub_Activo = chkActivoSubFam.Checked;
                CN_CatSubFamProducto clsCatSubFamProducto = new CN_CatSubFamProducto();
                int verificador = -1;

                if (hiddenActualizaSub.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        DisplayMensajeAlerta("PermisoGuardarNo");
                        return;
                    }
                    clsCatSubFamProducto.InsertarSubFamProducto(subFamProducto, session.Emp_Cnx, ref verificador);
                    this.LimpiaCamposSubFamilia();
                    txtIdSubFam.Enabled = true;
                    txtIdSubFam.Text = this.ValorSubFam;
                    txtIdSubFam.Focus();
                    this.DisplayMensajeAlerta("SubFamProducto_insert_ok");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        DisplayMensajeAlerta("PermisoModificarNo");
                        return;
                    }
                    clsCatSubFamProducto.ModificarSubFamProducto(subFamProducto, session.Emp_Cnx, ref verificador);
                    txtIdSubFam.Enabled = false;
                    this.DisplayMensajeAlerta("SubFamProducto_update_ok");
                }

                rgSubFamilia.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HabilitarBotones(bool nuevo, bool guardar, bool regresar, bool eliminar, bool imprimir, bool mail)
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (nuevo)
            {
                if (Sesion.U_MultiOfi)               
                    nuevo = _PermisoGuardar;               
                else               
                    nuevo = false;               
            }

            if (guardar)
            {
                if (_PermisoGuardar || _PermisoModificar)
                    guardar = true;
                else
                    guardar = false;
            }
            if (eliminar)           
                eliminar = _PermisoEliminar;            

            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Enabled = nuevo;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Enabled = guardar;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("undo")).Enabled = regresar;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Enabled = eliminar;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Enabled = imprimir;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("mail")).Enabled = mail;
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

        private List<FamProducto> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<FamProducto> listFamProducto = new List<FamProducto>();
                CN_CatFamProducto clsCatFamProducto = new CN_CatFamProducto();
                FamProducto famProducto = new FamProducto();
                clsCatFamProducto.ConsultaFamProducto(famProducto, sesion.Emp_Cnx, sesion.Id_Emp, ref listFamProducto);
                return listFamProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<SubFamProducto> GetListSubFamilia()
        {
            try
            {
                List<SubFamProducto> listSubFamProducto = new List<SubFamProducto>();
                CN_CatSubFamProducto clsCatSubFamProducto = new CN_CatSubFamProducto();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                SubFamProducto famProducto = new SubFamProducto();

                clsCatSubFamProducto.ConsultaSubFamProducto(famProducto, sesion.Emp_Cnx, sesion.Id_Emp, ref listSubFamProducto);
                return listSubFamProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarComboFamilias()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                this.cmbFamilia.Items.Clear();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatFamProducto_Combo", ref cmbFamilia);
                this.cmbFamilia.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                Exception _ex = new Exception(string.Concat(ex.Message, "FamProducto_combo_error"));
                throw _ex;
            }
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("FamProducto_combo_error"))
                Alerta("Error al cargar la lista de familias");
            else
                if (mensaje.Contains("optOpciones_SelectedIndexChanged"))
                    Alerta("Error al cambiar de panel de familias a subfamilias o viceversa");
                else
                    if (mensaje.Contains("Page_Load_error"))
                        Alerta("Error al cargar la página");
                    else
                        if (mensaje.Contains("rgFamilia_NeedDataSource"))
                            Alerta("Error al cargar el Grid de familias de producto");
                        else
                            if (mensaje.Contains("rgSubFamilia_NeedDataSource"))
                                Alerta("Error al cargar el Grid de subfamilias de producto");
                            else
                                if (mensaje.Contains("rgFamilia_ItemCommand"))
                                    Alerta("Error al ejecutar un evento (rgFamilia_ItemCommand) al cargar el Grid de familias de producto");
                                else
                                    if (mensaje.Contains("rgSubFamilia_ItemCommand"))
                                        Alerta("Error al ejecutar un evento (rgSubFamilia_ItemCommand) al cargar el Grid de subfamilias de producto");
                                    else
                                        if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                            Alerta("Error al cargar el centro de distribución.");
                                        else
                                            if (mensaje.Contains("rgFamilia_PageIndexChanged"))
                                                Alerta("Error al cambiar de página en el grid de familias");
                                            else
                                                if (mensaje.Contains("rgSubFamilia_PageIndexChanged"))
                                                    Alerta("Error al cambiar de página en el grid de subfamilias");
                                                else
                                                    if (mensaje.Contains("PermisoGuardarNo"))
                                                        Alerta("No tiene permisos para grabar.");
                                                    else
                                                        if (mensaje.Contains("SubFamIdRepetida_error"))
                                                            Alerta("La clave ya existe");
                                                        else
                                                            if (mensaje.Contains("SubFamDescripcionRepetida_error"))
                                                                Alerta("La descripción ya existe");
                                                            else
                                                                if (mensaje.Contains("FamIdRepetida_error"))
                                                                    Alerta("La clave ya existe");
                                                                else
                                                                    if (mensaje.Contains("FamDescripcionRepetida_error"))
                                                                        Alerta("La descripción ya existe");
                                                                    else
                                                                        if (mensaje.Contains("PermisoModificarNo"))
                                                                            Alerta("No tiene permisos para modificar");
                                                                        else
                                                                            if (mensaje.Contains("FamProducto_insert_ok"))
                                                                                Alerta("Los datos se guardaron correctamente");
                                                                            else
                                                                                if (mensaje.Contains("FamProducto_insert_error"))
                                                                                    Alerta("No se pudo guardar los datos de la familia de producto");
                                                                                else
                                                                                    if (mensaje.Contains("FamProducto_update_ok"))
                                                                                        Alerta("Los datos se modificaron correctamente");
                                                                                    else
                                                                                        if (mensaje.Contains("FamProducto_update_error"))
                                                                                            Alerta("No se pudo actualizar los datos de la familia de producto");
                                                                                        else
                                                                                            if (mensaje.Contains("SubFamProducto_insert_ok"))
                                                                                                Alerta("Los datos se guardaron correctamente");
                                                                                            else
                                                                                                if (mensaje.Contains("SubFamProducto_insert_error"))
                                                                                                    Alerta("No se pudo guardar los datos de la subfamilia de producto");
                                                                                                else
                                                                                                    if (mensaje.Contains("SubFamProducto_update_ok"))
                                                                                                        Alerta("Los datos se modificaron correctamente");
                                                                                                    else
                                                                                                        if (mensaje.Contains("SubFamProducto_update_error"))
                                                                                                            Alerta("No se pudo actualizar los datos de la subfamilia de producto");
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
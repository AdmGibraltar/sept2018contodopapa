using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Data;

namespace SIANWEB
{
    public partial class CatAdenda : System.Web.UI.Page
    {
        #region  Variables
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
        public DataTable DTAddendas
        {
            get
            {
                return (Session["TblAddendas" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["TblAddendas" + Session.SessionID] = value;
            }
        }

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

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
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
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAdendas_ItemCommand(object source, GridCommandEventArgs e)
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

                        hiddenActualiza.Value = rgAdendas.Items[item]["Id_Ade"].Text;
                        txtId.Text = rgAdendas.Items[item]["Id_Ade"].Text;
                        txtDescripcion.Text = rgAdendas.Items[item]["Tco_Descripcion"].Text;
                        //chkActivo.Checked = ((CheckBox)rgAdendas.Items[item]["Tco_Activo"].Controls[0]).Checked;
                        chkActivo.Checked = Convert.ToBoolean(rgAdendas.Items[item]["Tco_Activo"].Text);
                        txtId.Enabled = false;

                        CargarAdendas(Convert.ToInt32(txtId.Text));

                        rgFacturacioncabecera.Rebind();
                        rgFacturacionDetalle.Rebind();

                        rgCargocabecera.Rebind();
                        rgCargoDetalle.Rebind();

                        rgCreditocabecera.Rebind();
                        rgCreditoDetalle.Rebind();

                        rgRefacturacioncabecera.Rebind();
                        rgRefacturacionDetalle.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAdendas_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAdendas.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAdendas_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgAdendas.Rebind();
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
                        txtId.Enabled = true;
                        txtId.Focus();
                        Inicializar();
                        break;

                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
        protected void rg_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (InsertCommand(e))
                {
                    (source as RadGrid).Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (UpdateCommand(e))
                {
                    (source as RadGrid).Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                DeleteCommand(e);
                (source as RadGrid).Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFacturacioncabecera_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgFacturacioncabecera.DataSource = Cargar("1");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFacturacionDetalle_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgFacturacionDetalle.DataSource = Cargar("2");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCargocabecera_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgCargocabecera.DataSource = Cargar("3");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCargoDetalle_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    rgCargoDetalle.DataSource = Cargar("4");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCreditocabecera_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    rgCreditocabecera.DataSource = Cargar("5");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCreditoDetalle_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    rgCreditoDetalle.DataSource = Cargar("6");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgRefacturacioncabecera_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgRefacturacioncabecera.DataSource = Cargar("7");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgRefacturacionDetalle_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgRefacturacionDetalle.DataSource = Cargar("8");
                }
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
            txtId.Text = Valor;
            txtDescripcion.Text = "";
            chkActivo.Checked = true;
            this.rgAdendas.Rebind();

            DTAddendas = InicializarTabla();

            rgFacturacioncabecera.Rebind();
            rgFacturacionDetalle.Rebind();

            rgCargocabecera.Rebind();
            rgCargoDetalle.Rebind();

            rgCreditocabecera.Rebind();
            rgCreditoDetalle.Rebind();

            rgRefacturacioncabecera.Rebind();
            rgRefacturacionDetalle.Rebind();

            this.hiddenActualiza.Value = string.Empty;
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CatAdenda", "Id_Ade", sesion.Emp_Cnx, "spCatLocal_Maximo");

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
                    ct.Tabla = "CatAdenda"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Ade"; //Nombre de la columna del ID del catalogo
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
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
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
                    {
                        this.RadToolBar1.Items[6].Visible = false;
                    }
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.RadToolBar1.Items[5].Visible = false;
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
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

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

                Adenda adenda = new Adenda();
                adenda.Id_Emp = session.Id_Emp;
                adenda.Id_Cd = session.Id_Cd_Ver;
                adenda.Id_Ade = txtId.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtId.Text);
                adenda.Tco_Descripcion = txtDescripcion.Text;
                adenda.Tco_Activo = chkActivo.Checked;

                CN_CatAdenda clsCatAdenda = new CN_CatAdenda();
                int verificador = -1;

                DataRow[] Ar_dr;

                //GRIDS DE FACTURAS
                for (int i = 0; i < rgFacturacioncabecera.Items.Count; i++)
                {

                    Ar_dr = DTAddendas.Select("Det='" + (rgFacturacioncabecera.Items[i].FindControl("lblDet") as Label).Text + "' and Tipo='" + (rgFacturacioncabecera.Items[i].FindControl("lblTipo") as Label).Text + "'");
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Requerido"] = (rgFacturacioncabecera.Items[i].FindControl("chkRequerido") as CheckBox).Checked;
                    Ar_dr[0].AcceptChanges();
                }
                for (int i = 0; i < rgFacturacionDetalle.Items.Count; i++)
                {

                    Ar_dr = DTAddendas.Select("Det='" + (rgFacturacionDetalle.Items[i].FindControl("lblDet") as Label).Text + "' and Tipo='" + (rgFacturacionDetalle.Items[i].FindControl("lblTipo") as Label).Text + "'");
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Requerido"] = (rgFacturacionDetalle.Items[i].FindControl("chkRequerido") as CheckBox).Checked;
                    Ar_dr[0].AcceptChanges();
                }

                //GRIDS DE CARGO
                for (int i = 0; i < rgCargocabecera.Items.Count; i++)
                {

                    Ar_dr = DTAddendas.Select("Det='" + (rgCargocabecera.Items[i].FindControl("lblDet") as Label).Text + "' and Tipo='" + (rgCargocabecera.Items[i].FindControl("lblTipo") as Label).Text + "'");
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Requerido"] = (rgCargocabecera.Items[i].FindControl("chkRequerido") as CheckBox).Checked;
                    Ar_dr[0].AcceptChanges();
                }
                for (int i = 0; i < rgCargoDetalle.Items.Count; i++)
                {

                    Ar_dr = DTAddendas.Select("Det='" + (rgCargoDetalle.Items[i].FindControl("lblDet") as Label).Text + "' and Tipo='" + (rgCargoDetalle.Items[i].FindControl("lblTipo") as Label).Text + "'");
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Requerido"] = (rgCargoDetalle.Items[i].FindControl("chkRequerido") as CheckBox).Checked;
                    Ar_dr[0].AcceptChanges();
                }

                //GRIDS DE CREDITO
                for (int i = 0; i < rgCreditocabecera.Items.Count; i++)
                {

                    Ar_dr = DTAddendas.Select("Det='" + (rgCreditocabecera.Items[i].FindControl("lblDet") as Label).Text + "' and Tipo='" + (rgCreditocabecera.Items[i].FindControl("lblTipo") as Label).Text + "'");
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Requerido"] = (rgCreditocabecera.Items[i].FindControl("chkRequerido") as CheckBox).Checked;
                    Ar_dr[0].AcceptChanges();
                }
                for (int i = 0; i < rgCreditoDetalle.Items.Count; i++)
                {

                    Ar_dr = DTAddendas.Select("Det='" + (rgCreditoDetalle.Items[i].FindControl("lblDet") as Label).Text + "' and Tipo='" + (rgCreditoDetalle.Items[i].FindControl("lblTipo") as Label).Text + "'");
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Requerido"] = (rgCreditoDetalle.Items[i].FindControl("chkRequerido") as CheckBox).Checked;
                    Ar_dr[0].AcceptChanges();
                }

                //GRIDS DE REFACTURAS
                for (int i = 0; i < rgRefacturacioncabecera.Items.Count; i++)
                {

                    Ar_dr = DTAddendas.Select("Det='" + (rgRefacturacioncabecera.Items[i].FindControl("lblDet") as Label).Text + "' and Tipo='" + (rgRefacturacioncabecera.Items[i].FindControl("lblTipo") as Label).Text + "'");
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Requerido"] = (rgRefacturacioncabecera.Items[i].FindControl("chkRequerido") as CheckBox).Checked;
                    Ar_dr[0].AcceptChanges();
                }
                for (int i = 0; i < rgRefacturacionDetalle.Items.Count; i++)
                {

                    Ar_dr = DTAddendas.Select("Det='" + (rgRefacturacionDetalle.Items[i].FindControl("lblDet") as Label).Text + "' and Tipo='" + (rgRefacturacionDetalle.Items[i].FindControl("lblTipo") as Label).Text + "'");
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Requerido"] = (rgRefacturacionDetalle.Items[i].FindControl("chkRequerido") as CheckBox).Checked;
                    Ar_dr[0].AcceptChanges();
                }

                //--------
                if (hiddenActualiza.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    clsCatAdenda.InsertarAdenda(adenda, session.Emp_Cnx, DTAddendas, ref verificador);
                    if (verificador > 0)
                    {
                        hiddenActualiza.Value = txtId.Text;
                        this.LimpiarCampos();

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

                    clsCatAdenda.ModificarAdenda(adenda, session.Emp_Cnx, DTAddendas, ref verificador);

                    txtId.Enabled = false;
                    if (verificador > 0)
                    {
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar modificar los datos");
                    }
                }

                rgAdendas.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiarCampos()
        {
            txtId.Text = Valor;
            txtDescripcion.Text = string.Empty;
            chkActivo.Checked = true;
            hiddenActualiza.Value = "";
            txtId.Enabled = true;
            txtId.Focus();

            DTAddendas = InicializarTabla();

            rgFacturacioncabecera.Rebind();
            rgFacturacionDetalle.Rebind();

            rgCargocabecera.Rebind();
            rgCargoDetalle.Rebind();

            rgCreditocabecera.Rebind();
            rgCreditoDetalle.Rebind();

            rgRefacturacioncabecera.Rebind();
            rgRefacturacionDetalle.Rebind();
        }
        private List<Adenda> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<Adenda> listAdenda = new List<Adenda>();
                CN_CatAdenda clsCatAdenda = new CN_CatAdenda();
                Adenda tipoPrecio = new Adenda();
                tipoPrecio.Id_Emp = sesion.Id_Emp;
                tipoPrecio.Id_Cd = sesion.Id_Cd_Ver;
                clsCatAdenda.ConsultaAdenda(tipoPrecio, sesion.Emp_Cnx, ref listAdenda);
                return listAdenda;
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
        private void CargarAdendas(int Id_Ade)
        {
            try
            {
                DataTable dt = DTAddendas;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatAdenda cnAdenda = new CN_CatAdenda();
                Adenda adenda = new Adenda();
                adenda.Id_Emp = sesion.Id_Emp;
                adenda.Id_Cd = sesion.Id_Cd_Ver;
                adenda.Id_Ade = Id_Ade;
                cnAdenda.ConsultaAdenda(adenda, ref dt, sesion.Emp_Cnx);

                DTAddendas = dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private bool InsertCommand(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;

                string Tipo = "";
                string nodo = "";
                string campo = "";
                int? longitud = 0;
                bool requerido = false;

                Tipo = ((Label)gi.FindControl("EditTipo")).Text;
                nodo = ((RadTextBox)gi.FindControl("EditNodo")).Text;
                campo = ((RadTextBox)gi.FindControl("EditCampo")).Text;
                longitud = (int?)(((RadNumericTextBox)gi.FindControl("EditLongitud")).Value);
                requerido = ((CheckBox)gi.FindControl("chkRequerido2")).Checked;

                if (nodo == "" || campo == "" || longitud == null)
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return false;
                }

                if (DTAddendas.Select("tipo='" + Tipo + "' and nodo='" + nodo + "'").Length > 0)
                {
                    Alerta("El nombre del nodo ya existe");
                    e.Canceled = true;
                    return false;
                }


                DTAddendas.Rows.Add(new object[] { DTAddendas.Rows.Count, Tipo, nodo, campo, longitud, requerido });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private bool UpdateCommand(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;

                string Det = "";
                string Tipo = "";
                string nodo = "";
                string campo = "";
                int? longitud = 0;
                bool requerido = false;

                Det = ((Label)gi.FindControl("EditDet")).Text;
                Tipo = ((Label)gi.FindControl("EditTipo")).Text;
                nodo = ((RadTextBox)gi.FindControl("EditNodo")).Text;
                campo = ((RadTextBox)gi.FindControl("EditCampo")).Text;
                longitud = (int?)(((RadNumericTextBox)gi.FindControl("EditLongitud")).Value);
                requerido = ((CheckBox)gi.FindControl("chkRequerido2")).Checked;

                if (nodo == "" || campo == "" || longitud == null)
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return false;
                }


                DataRow[] Ar_dr;
                Ar_dr = DTAddendas.Select("tipo='" + Tipo + "' and nodo='" + nodo + "' and Det<>'" + Det + "'");
                if (Ar_dr.Length > 0)
                {
                    Alerta("El nombre del nodo ya existe");
                    e.Canceled = true;
                    return false;
                }


                // DataRow[] Ar_dr;
                Ar_dr = DTAddendas.Select("Det='" + Det + "' and Tipo='" + Tipo + "'");

                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Nodo"] = nodo;
                    Ar_dr[0]["Campo"] = campo;
                    Ar_dr[0]["Longitud"] = longitud;
                    Ar_dr[0]["Requerido"] = requerido;
                    Ar_dr[0].AcceptChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DeleteCommand(GridCommandEventArgs e)
        {
            try
            {
                string Det = "";
                string Tipo = "";

                GridItem gi = null;
                DataRow[] Ar_dr;
                DataRow dr;
                gi = e.Item;

                Det = ((Label)gi.FindControl("lblDet")).Text;
                Tipo = ((Label)gi.FindControl("lblTipo")).Text;

                Ar_dr = DTAddendas.Select("Det='" + Det + "' and Tipo='" + Tipo + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    DTAddendas.AcceptChanges();
                }
                for (int x = 0; x < DTAddendas.Rows.Count; x++)
                {
                    dr = DTAddendas.Rows[x];
                    dr.BeginEdit();
                    dr["Det"] = x + 1;
                    dr.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable InicializarTabla()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Det");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Nodo");
            dt.Columns.Add("Campo");
            dt.Columns.Add("Longitud");
            dt.Columns.Add("Requerido");
            return dt;
        }

        DataTable Cargar(string tipo)
        {
            DataTable dt = InicializarTabla();
            foreach (DataRow dr in DTAddendas.Select("Tipo='" + tipo + "'"))
            {
                dt.Rows.Add(dr.ItemArray);
            }
            return dt;
        }
        #endregion
        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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
        protected void rg_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    if (((CheckBox)item.FindControl("chkRequerido")) != null)
                    {
                        ((CheckBox)item.FindControl("chkRequerido")).Checked = item.GetDataKeyValue("Requerido") == System.DBNull.Value ? false : Convert.ToBoolean(item.GetDataKeyValue("Requerido"));
                    }
                }
            }
            catch
            {
            }
        }
    }
}
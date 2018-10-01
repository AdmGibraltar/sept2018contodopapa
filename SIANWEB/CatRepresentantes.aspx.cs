using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class CatRepresentantes : System.Web.UI.Page
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
                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false");
                            return;
                        }

                        //CargarCentros_Rik();
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
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                } CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Nuevo();

                rg1.Rebind();
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
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rg1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Modificar")
                {
                    Nuevo();
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    //rg1.Items[item]["TipoRep"].Visible = true;
                    rg1.MasterTableView.GetColumn("TipoRep").Visible = true;
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);

                        HF_ID.Value = rg1.Items[item]["Id_Rik"].Text;
                        txtClave.Text = rg1.Items[item]["Id_Rik"].Text;
                        txtClave.Enabled = false;
                        txtNombre.Text = rg1.Items[item]["Nombre"].Text;
                        txtCalle.Text = rg1.Items[item]["Calle"].Text.Replace("&nbsp;", string.Empty);
                        txtNumero.Text = rg1.Items[item]["Numero"].Text.Replace("&nbsp;","0");
                        txtNumero.Text = txtNumero.Text == "0" ? string.Empty : rg1.Items[item]["Numero"].Text;
                        txtColonia.Text = rg1.Items[item]["Colonia"].Text.Replace("&nbsp;", string.Empty);
                        txtTelefono.Text = rg1.Items[item]["Telefono"].Text.Replace("&nbsp;", string.Empty);

                        txtTipoRepresentante.Text = rg1.Items[item]["TipoRep"].Text == "-1" ? string.Empty : rg1.Items[item]["TipoRep"].Text;

                        if (cmbTipoRepresentante.FindItemIndexByValue(rg1.Items[item]["TipoRep"].Text) > 0)
                        {
                            cmbTipoRepresentante.SelectedIndex = cmbTipoRepresentante.FindItemIndexByValue(rg1.Items[item]["TipoRep"].Text);
                            cmbTipoRepresentante.Text = cmbTipoRepresentante.FindItemByValue(rg1.Items[item]["TipoRep"].Text).Text;
                        }
                        else
                        {
                            cmbTipoRepresentante.SelectedIndex = 0;
                            cmbTipoRepresentante.Text = cmbTipoRepresentante.Items[0].Text;
                            cmbTipoRepresentante.Text = "";
                        }

                        rg1.Items[item]["TipoRep"].Visible =false;

                        if (rg1.Items[item]["Fecha_Alta"].Text != "&nbsp;")
                        {
                            if (Convert.ToDateTime(rg1.Items[item]["Fecha_Alta"].Text) == DateTime.MinValue)
                            {
                                dpFecha.DateInput.Text = string.Empty;
                                dpFecha.SelectedDate = null;
                            }
                            else
                            {
                                dpFecha.SelectedDate = Convert.ToDateTime(rg1.Items[item]["Fecha_Alta"].Text);
                            }
                        }
                        else
                        {
                            dpFecha.DateInput.Text = string.Empty;
                            dpFecha.SelectedDate = null;
                        }
                        txtContribucion.Text = rg1.Items[item]["Contribucion"].Text.Replace("&nbsp;", string.Empty);
                        txtCompensacion.Text = rg1.Items[item]["Compensacion"].Text.Replace("&nbsp;", string.Empty);
                        
                        //txtGte.Text = rg1.Items[item]["Gte"].Text;
                        //cmbGte.SelectedIndex = cmbGte.FindItemIndexByValue(rg1.Items[item]["Gte"].Text);
                        chkPertenece.Checked = Convert.ToBoolean(rg1.Items[item]["Pertenece"].Text.Replace("&nbsp;","false"));


                        chkActivo.Checked = Convert.ToBoolean(rg1.Items[item]["EstatusStr"].Text.Replace("Inactivo","false").Replace("Activo","true"));
                        //txtClave.Enabled = false;

                        List<RikUen> lc = new List<RikUen>();
                        CN_CatRepresentantes clsCatRepresentantes = new CN_CatRepresentantes();
                        RikUen representante = new RikUen();
                        representante.Id_Emp = Sesion.Id_Emp;
                        representante.Id_Cd = Sesion.Id_Cd_Ver;
                        representante.Id_Rik = Convert.ToInt32(rg1.Items[item]["Id_Rik"].Text);
                        clsCatRepresentantes.ConsultarRepresentantesDet(representante, Sesion.Emp_Cnx, ref lc);

                        //lbUen.Items.FindChildByValue<ControlItem>(lc[0].Id_Uen.ToString(), true).;

                        foreach (RikUen rikuen in lc)
                        {
                            lbUen.FindItemByValue(rikuen.Id_Uen.ToString()).Checked = true;
                        }
                    }
                }
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
                ErrorManager();
                rg1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }
        protected void rg1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
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
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
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
        #endregion
        #region Funciones

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
       

        private void CargarTipoRepresentante() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTipoRepresentante_Combo", ref cmbTipoRepresentante);
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
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Representantes> GetList()
        {
            try
            {
                List<Representantes> List = new List<Representantes>();
                CN_CatRepresentantes clsCatRepresentantes = new CN_CatRepresentantes();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Representantes representante = new Representantes();
                representante.Id_Emp = session2.Id_Emp;
                representante.Id_Cd = session2.Id_Cd_Ver;
                clsCatRepresentantes.ConsultarRepresentantes(representante, session2.Emp_Cnx, ref List);
                return List;
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
                if (lbUen.CheckedItems.Count == 0)
                {
                    Alerta("No es posible tener representantes sin UEN asignada");
                        return;
                }

                if (Convert.ToInt32(cmbTipoRepresentante.SelectedValue) > 0)
                {}
                else
                {
                    Alerta("Debe establecer un tipo de representante");
                    return;
                }

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                Representantes representante = new Representantes();
                representante.Nombre = txtNombre.Text;
                representante.Id_Emp = session.Id_Emp;
                representante.Id_Cd = session.Id_Cd_Ver;
                representante.Calle = txtCalle.Text;
                representante.Colonia = txtColonia.Text;
                representante.Compensacion = txtCompensacion.Text == string.Empty ? 0 : Convert.ToDouble(txtCompensacion.Text);
                representante.Contribucion = txtContribucion.Text == string.Empty ? 0 : Convert.ToDouble(txtContribucion.Text);
                representante.Fecha_Alta = dpFecha.SelectedDate == null ? DateTime.MinValue : dpFecha.SelectedDate.Value;
                //representante.Gte = Convert.ToInt32(cmbGte.SelectedValue);
                representante.Numero = txtNumero.Text == string.Empty ? 0 : Convert.ToInt32(txtNumero.Text);
                representante.Pertenece = chkPertenece.Checked;
                representante.Telefono = txtTelefono.Text;
                representante.Estatus = chkActivo.Checked;
                representante.TipoRep = Convert.ToInt32(cmbTipoRepresentante.SelectedValue);

                CN_CatRepresentantes clsCatRepresentantes = new CN_CatRepresentantes();
                int verificador = -1;
                List<Comun> lc = new List<Comun>();
                Comun c = default(Comun);
                foreach (RadListBoxItem rlbi in lbUen.CheckedItems)
                {
                    c = new Comun();
                    c.Id = Convert.ToInt32(rlbi.Value);
                    c.Descripcion = rlbi.Text;
                    lc.Add(c);
                }
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    representante.Id_Rik = Convert.ToInt32(txtClave.Text);
                    clsCatRepresentantes.InsertarRepresentantes(representante, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        clsCatRepresentantes.InsertarRepresentantesDet(representante, lc, session.Emp_Cnx, ref verificador);
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

                    representante.Id_Rik = Convert.ToInt32(HF_ID.Value);
                    clsCatRepresentantes.ModificarRepresentantes(representante, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        clsCatRepresentantes.InsertarRepresentantesDet(representante, lc, session.Emp_Cnx, ref verificador);
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar guardar los cambios");
                    }
                }
                rg1.Rebind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {

            cmbTipoRepresentante.SelectedIndex = 0;
            cmbTipoRepresentante.Text = cmbTipoRepresentante.Items[0].Text;
            txtTipoRepresentante.Text = string.Empty;

            txtClave.Text = MaximoId();
            txtClave.Enabled = true;
            txtCalle.Text = string.Empty;
            txtColonia.Text = string.Empty;
            txtCompensacion.Value = null;
            txtContribucion.Value = null;
            //txtGte.Text = string.Empty;
            txtTipoRepresentante.Text= string.Empty;

            txtNombre.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            dpFecha.SelectedDate = null;
            //-- Seleccionar --

            chkPertenece.Checked = false;
            chkActivo.Checked = true;

            HF_ID.Value = string.Empty;

            foreach (RadListBoxItem rl in lbUen.Items)
            {
                rl.Checked = false;
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HF_ID.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HF_ID.Value);
                    ct.Tabla = "CatRik";
                    ct.Columna = "Id_Rik";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            txtClave.Text = Valor;
            rg1.Rebind();
            CargarUEN();
            CargarTipoRepresentante();
        }
        private void CargarUEN() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaListBox(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUen_Combo", ref lbUen);
                lbUen.Items.Remove(lbUen.FindItemByValue("-1"));
                //lbUen.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
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
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatRik", "Id_Rik", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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
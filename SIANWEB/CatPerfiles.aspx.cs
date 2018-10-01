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
    public partial class CatPerfiles : System.Web.UI.Page
    {
        #region Variables
                private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                    if (Page.IsPostBack == false)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RadAjaxManager1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        }


                        AddExpression();
                        this.RadGrid1.Rebind();
                        CargarCentros();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    RadGrid1.DataSource = GetList();
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }

        }
        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                this.RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_PageIndexChanged");
            }
        }
        protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            try
            {
                this.RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_SortCommand");
            }
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
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

                        this.HiddenId_TU.Value = this.RadGrid1.Items[item]["IDPerfil"].Text;
                        //this.CboDepende.SelectedValue = this.RadGrid1.Items[item]["ID"].Text;
                        //this.CboDepende.Enabled = false;
                        this.txtTipoNombre.Text = this.RadGrid1.Items[item]["Perfil"].Text;
                        this.chkActivo.Checked = Convert.ToBoolean(this.RadGrid1.Items[item]["TU_Activo"].Text);
                        this.chkPropia.Checked = Convert.ToBoolean(this.RadGrid1.Items[item]["TU_Propia"].Text);
                        //this.HiddenId_Ofi.Value = this.RadGrid1.Items[item]["Id_Ofi"].Text;
                        cargarpermisos(Convert.ToInt32(this.HiddenId_TU.Value));
                        //LimpiarChecks();
                        Panel2.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_ItemCommand");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Nuevo();
                this.RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void chkAccesar_CheckedChanged(object sender, EventArgs e)
        {
            for (int x = 0; x < this.RadGrid2.Items.Count; x++)
            {
                (RadGrid2.Items[x].FindControl("ChkAccesar") as CheckBox).Checked = (sender as CheckBox).Checked;
            }
        }
        protected void chkGrabar_CheckedChanged(object sender, EventArgs e)
        {
            for (int x = 0; x < this.RadGrid2.Items.Count; x++)
            {
                (RadGrid2.Items[x].FindControl("ChkGrabar") as CheckBox).Checked = (sender as CheckBox).Checked;
            }
        }
        protected void chkModificar_CheckedChanged(object sender, EventArgs e)
        {
            for (int x = 0; x < this.RadGrid2.Items.Count; x++)
            {
                (RadGrid2.Items[x].FindControl("ChkModificar") as CheckBox).Checked = (sender as CheckBox).Checked;
            }
        }
        protected void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            for (int x = 0; x < this.RadGrid2.Items.Count; x++)
            {
                (RadGrid2.Items[x].FindControl("ChkEliminar") as CheckBox).Checked = (sender as CheckBox).Checked;
            }
        }
        protected void chkImprimir_CheckedChanged(object sender, EventArgs e)
        {
            for (int x = 0; x < this.RadGrid2.Items.Count; x++)
            {
                (RadGrid2.Items[x].FindControl("ChkImprimir") as CheckBox).Checked = (sender as CheckBox).Checked;
            }
        }
        #endregion
        #region Funciones
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
                        this.RadToolBar1.Items[6].Visible = false;
                    }
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.RadToolBar1.Items[5].Visible = false;
                    }
                    //If Permiso.PEliminar = False Then
                    //    Me.RadToolBar1.Items(3).Enabled = False
                    //End If
                    //If Permiso.PImprimir = False Then
                    //    Me.RadToolBar1.Items(2).Enabled = False
                    //End If

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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AddExpression()
        {
            GridSortExpression expression2 = new GridSortExpression();
            expression2.FieldName = "TU_Descripcion";
            expression2.SetSortOrder("Ascending");
            this.RadGrid1.MasterTableView.SortExpressions.AddSortExpression(expression2);
        }
        private void Guardar()
        {
            try
            {

                if (this.Panel2.Visible == false)
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    CapaNegocios.CN_CatTiposUsuario clsTipoU = new CapaNegocios.CN_CatTiposUsuario();
                    int item = 0;
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    TipoUsuario tipoUsuario = new TipoUsuario();
                    tipoUsuario.TU_Descripcion = this.txtTipoNombre.Text;
                    tipoUsuario.TU_Id_TU = 1;
                    tipoUsuario.TU_Activo = chkActivo.Checked;
                    tipoUsuario.Tu_Propia = chkPropia.Checked;
                    tipoUsuario.Id_Cd = session2.Id_Cd_Ver.ToString();
                    tipoUsuario.Id_Emp = session2.Id_Emp;
                    clsTipoU.InsertarTipoUsuario(tipoUsuario, session2.Emp_Cnx, ref this.RadGrid2, ref item);

                    if (item == 0)
                    {
                        Alerta("El tipo de usuario ya existe");
                    }
                    else
                    {
                        this.RadGrid1.Rebind();
                        //Deja limpio para un nuevo
                        Nuevo();
                        Alerta("Los datos se guardaron correctamente");
                    }
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }

                    //Dim valor As Int32
                    bool PAccesar = false;
                    bool PGrabar = false;
                    bool PModificar = false;
                    bool PEliminar = false;
                    bool PImprimir = false;
                    Int32 Contador = default(Int32);
                    Int32 contador2 = default(Int32);
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];


                    for (int cont = 0; cont <= this.RadGrid2.Items.Count - 1; cont++)
                    {
                        if (((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkAccesar")).Checked != Convert.ToBoolean(this.RadGrid2.Items[cont]["SpTu_PAccesar"].Text))
                        {
                            PAccesar = true;
                        }
                        if (((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkGrabar")).Checked != Convert.ToBoolean(this.RadGrid2.Items[cont]["SpTu_PGrabar"].Text))
                        {
                            PGrabar = true;
                        }
                        if (((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkModificar")).Checked != Convert.ToBoolean(this.RadGrid2.Items[cont]["SpTu_PModificar"].Text))
                        {
                            PModificar = true;
                        }
                        if (((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkEliminar")).Checked != Convert.ToBoolean(this.RadGrid2.Items[cont]["SpTu_PEliminar"].Text))
                        {
                            PEliminar = true;
                        }
                        if (((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkImprimir")).Checked != Convert.ToBoolean(this.RadGrid2.Items[cont]["SpTu_PImprimir"].Text))
                        {
                            PImprimir = true;
                        }

                        CapaNegocios.CN_PermisosTU clsPermisosTU = new CapaNegocios.CN_PermisosTU();
                        Permiso permiso = new Permiso();
                        permiso.Id_TU = Convert.ToInt32(this.HiddenId_TU.Value);
                        permiso.Sm_cve = Convert.ToInt32(this.RadGrid2.Items[cont]["MenuCve"].Text);
                        permiso.Sp_PAccesar = ((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkAccesar")).Checked;
                        permiso.Sp_PGrabar = ((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkGrabar")).Checked;
                        permiso.Sp_PModificar = ((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkModificar")).Checked;
                        permiso.Sp_PEliminar = ((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkEliminar")).Checked;
                        permiso.Sp_PImprimir = ((CheckBox)this.RadGrid2.Items[cont].FindControl("ChkImprimir")).Checked;

                        permiso.PAccesar = PAccesar;
                        permiso.PGrabar = PGrabar;
                        permiso.PModificar = PModificar;
                        permiso.PEliminar = PEliminar;
                        permiso.PImprimir = PImprimir;

                        if (PAccesar == true || PGrabar == true || PModificar == true || PEliminar == true || PImprimir == true)
                        {
                            clsPermisosTU.ModificarPermisosTipoUsuario(permiso, session2.Emp_Cnx, ref Contador);
                        }
                        PAccesar = false;
                        PGrabar = false;
                        PModificar = false;
                        PEliminar = false;
                        PImprimir = false;
                        contador2 = contador2 + Contador;
                        Contador = 0;
                    }

                    //Alerta("Afectados: " & contador2)

                    CapaNegocios.CN_CatTiposUsuario clsTipoU = new CapaNegocios.CN_CatTiposUsuario();
                    int item = 0;

                    TipoUsuario tipoUsuario = new TipoUsuario();
                    tipoUsuario.TU_Descripcion = this.txtTipoNombre.Text;
                    tipoUsuario.Id_TU = Convert.ToInt32(this.HiddenId_TU.Value);
                    tipoUsuario.TU_Activo = this.chkActivo.Checked;

                    tipoUsuario.Id_Emp = session2.Id_Emp;
                    clsTipoU.ModificarTipoUsuario(tipoUsuario, session2.Emp_Cnx, ref item);
                    if (item == 0)
                    {
                        this.RadGrid1.Rebind();
                        //No es necesario limpiar por si se quiere hacer otro cambio al mismo
                        //Nuevo()
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                    {
                        Alerta("El tipo de usuario");
                    }

                    cargarpermisos(Convert.ToInt32(HiddenId_TU.Value));
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Guardar");
            }
        }
        private void cargarpermisos(int valor)
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN_PermisosTU clsPermisosTU = new CapaNegocios.CN_PermisosTU();
                Permiso permiso = new Permiso();
                permiso.Id_TU = valor;
                permiso.Id_Emp = session2.Id_Emp;
                permiso.Id_Cd = session2.Id_Cd_Ver;
                clsPermisosTU.ConsultaPermisosTipoUsuario(permiso, session2.Emp_Cnx, ref this.RadGrid2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<TipoUsuario> GetList()
        {
            try
            {
                List<TipoUsuario> List = new List<TipoUsuario>();
                CapaNegocios.CN_CatTiposUsuario clsTipoU = new CapaNegocios.CN_CatTiposUsuario();
                TipoUsuario tipoUsuario = new TipoUsuario();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                tipoUsuario.Id_Emp = session2.Id_Emp;
                clsTipoU.ConsultaTiposDeUsuario(tipoUsuario, session2.Emp_Cnx, ref List);
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
                //this.CboDepende.SelectedValue = "0";
                //this.CboDepende.Enabled = true;

                this.HiddenId_Ofi.Value = string.Empty;
                this.HiddenId_TU.Value = string.Empty;
                this.txtTipoNombre.Text = string.Empty;
                this.chkActivo.Checked = true;
                this.chkPropia.Checked = false;
                this.Panel2.Visible = false;

                //LimpiarChecks();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Nuevo");
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
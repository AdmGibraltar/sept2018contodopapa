using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.IO;

namespace SIANWEB
{
    public partial class CatMovimientos : System.Web.UI.Page
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
                    Response.Redirect("Login.aspx");
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
                        if (Request.QueryString["ref"] != null)
                        {
                            if (Convert.ToInt32(Request.QueryString["ref"]) == 1)
                            {
                                rbCobranza.Checked = true;
                                rbInventario.Checked = false;
                            }
                            else
                            {
                                rbCobranza.Checked = false;
                                rbInventario.Checked = true;
                            }
                        }

                        Inicializar();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgMovimiento.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
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
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
        protected void rgMovimiento_ItemCommand(object source, GridCommandEventArgs e)
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

                        txtNumero.Enabled = false;
                        HFId_Mov.Value = rgMovimiento.Items[item]["Id"].Text;
                        txtNumero.Text = rgMovimiento.Items[item]["Id"].Text;
                        cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(rgMovimiento.Items[item]["Tipo"].Text);
                        //trId.Visible = true;
                        txtNombre.Text = rgMovimiento.Items[item]["Nombre"].Text;
                        cmbNaturaleza.SelectedIndex = cmbNaturaleza.FindItemIndexByValue(rgMovimiento.Items[item]["Naturaleza"].Text);
                        txtInverso.Text = cmbInverso.FindItemByValue(rgMovimiento.Items[item]["Inverso"].Text) == null || rgMovimiento.Items[item]["Inverso"].Text == "-1" ? string.Empty : rgMovimiento.Items[item]["Inverso"].Text;
                        cmbInverso.SelectedIndex = cmbInverso.FindItemByValue(rgMovimiento.Items[item]["Inverso"].Text) == null ? 0 : cmbInverso.FindItemIndexByValue(rgMovimiento.Items[item]["Inverso"].Text);
                        cmbInverso.Text = cmbInverso.FindItemByValue(rgMovimiento.Items[item]["Inverso"].Text) == null ? cmbInverso.Items[0].Text : cmbInverso.FindItemByValue(rgMovimiento.Items[item]["Inverso"].Text).Text;
                        chkIva.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["AfeIVA"].Text);
                        chkVenta.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["AfeVta"].Text);
                        chkOrden.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["AfeOrdComp"].Text);
                        chkReqRef.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["ReqReferencia"].Text);
                        chkReqSpo.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["ReqSispropietario"].Text);
                        chkActivo.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["Estatus"].Text);
                        cmbAfecta.SelectedIndex = cmbAfecta.FindItemByValue(rgMovimiento.Items[item]["Afecta"].Text) == null ? 0 : cmbAfecta.FindItemIndexByValue(rgMovimiento.Items[item]["Afecta"].Text);
                        cmbAfecta.Text = cmbAfecta.FindItemByValue(rgMovimiento.Items[item]["Afecta"].Text) == null ? cmbAfecta.Items[0].Text : cmbAfecta.FindItemByValue(rgMovimiento.Items[item]["Afecta"].Text).Text;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_ItemCommand");
            }
        }
        protected void rb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CargarTipo();
                CargarNaturaleza();
                CargarInverso();

                Nuevo();

                trCobranza.Visible = rbCobranza.Checked;
                //chkIva.Visible = rbCobranza.Checked;
                //chkVenta.Visible = rbCobranza.Checked;

                trInventario.Visible = rbInventario.Checked;
                //chkOrden.Visible = rbInventario.Checked;
                //chkReqRef.Visible = rbInventario.Checked;
                trAfecta.Visible = rbInventario.Checked;
                rgMovimiento.Rebind();

                txtNumero.Text = Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
                            Sesion sesion = new Sesion();                sesion = (Sesion)Session["Sesion" + Session.SessionID];                if (sesion == null)                {                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);                }                CN__Comun comun = new CN__Comun();                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
        }
        protected void rgMovimiento_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgMovimiento.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void CargarAfecta()
        {
            cmbAfecta.Items.Clear();
            string[] Afecta = new string[] { "-- Seleccionar --", "Clientes", "Proveedores",  "Sucursal" };
            for (int i = 0; i < Afecta.Length; i++)
            {
                cmbAfecta.Items.Add(new RadComboBoxItem(Afecta[i].ToString(), (i - 1).ToString()));
            }
        }
        private void CargarInverso()
        {
            try
            {
                cmbInverso.Items.Clear();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int NatMov = 1;
                if (rbCobranza.Checked)
                {
                    NatMov = 0;
                }
                CN_Comun.LlenaCombo(1, NatMov, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatMovimiento_Combo", ref cmbInverso);
                cmbInverso.DataValueField = "Id";
                cmbInverso.DataTextField = "Descripcion";
                cmbInverso.DataBind();
                //this.cmbInverso.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                //txtInverso.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarNaturaleza()
        {
            cmbNaturaleza.Items.Clear();
            string[] tipoInventario = new string[] { "-- Seleccionar --", "Entrada", "Salida" };
            string[] tipoCobranza = new string[] { "-- Seleccionar --", "Abono", "Cargo" };

            if (rbCobranza.Checked)
            {
                for (int i = 0; i < tipoCobranza.Length; i++)
                {
                    cmbNaturaleza.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoCobranza[i].ToString(), (i - 1).ToString()));
                }
            }
            else
            {
                for (int i = 0; i < tipoInventario.Length; i++)
                {
                    cmbNaturaleza.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoInventario[i].ToString(), (i - 1).ToString()));
                }
            }
        }
        private void CargarTipo()
        {
            cmbTipo.Items.Clear();
            string[] tipoInventario = new string[] { "-- Seleccionar --", "Entrada de almacén", "Salida de almacén", "Factura", "Remisiones", "Devoluciones parciales" };
            string[] tipoCobranza = new string[] { "-- Seleccionar --", "Factura", "Pago", "Nota de cargo", "Nota de crédito" };

            if (rbCobranza.Checked)
            {
                for (int i = 0; i < tipoCobranza.Length; i++)
                {
                    cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoCobranza[i].ToString(), (i - 1).ToString()));
                }
            }
            else
            {
                for (int i = 0; i < tipoInventario.Length; i++)
                {
                    cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoInventario[i].ToString(), (i - 1).ToString()));
                }
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
        private void Inicializar()
        {

            txtNumero.Text = Valor;
            CargarTipo();
            CargarNaturaleza();
            CargarInverso();
            CargarAfecta();
            CargarCentros();
            rgMovimiento.Rebind();

            trCobranza.Visible = rbCobranza.Checked;
            //chkIva.Visible = rbCobranza.Checked;
            //chkVenta.Visible = rbCobranza.Checked;

            trInventario.Visible = rbInventario.Checked;
            //chkOrden.Visible = rbInventario.Checked;
            //chkReqRef.Visible = rbInventario.Checked;
            trAfecta.Visible = rbInventario.Checked;
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, "CatTMovimiento", Convert.ToInt32(rbInventario.Checked), "Id_Tm", Sesion.Emp_Cnx, "spCatCentral_MaximoMov");
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
        private List<Movimientos> GetList()
        {
            try
            {
                List<Movimientos> List = new List<Movimientos>();
                CN_CatMovimientos clsCatMovimientos = new CN_CatMovimientos();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                clsCatMovimientos.ConsultaMovimientos(rbCobranza.Checked, session2.Id_Emp, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            Sesion session2 = new Sesion();
            session2 = (Sesion)Session["Sesion" + Session.SessionID];



            Movimientos mv = new Movimientos();
            mv.Id_Emp = session2.Id_Emp;
            mv.Nombre = txtNombre.Text;
            mv.Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
            mv.Naturaleza = Convert.ToInt32(cmbNaturaleza.SelectedValue);
            mv.Inverso = Convert.ToInt32(cmbInverso.SelectedValue);
            mv.Estatus = chkActivo.Checked;
            mv.Id = Convert.ToInt32(txtNumero.Text);
            mv.ReqReferencia = chkReqRef.Checked;
            mv.Afecta = Convert.ToInt32(cmbAfecta.SelectedValue);
            if (rbCobranza.Checked)
            {
                mv.AfeVta = chkVenta.Checked;
                mv.AfeIVA = chkIva.Checked;
                mv.ReqSispropietario = false;
                mv.ReqReferencia = false;
                mv.AfeOrdComp = false;
                mv.NatMov = 0;
            }
            else
            {
                mv.AfeVta = false;
                mv.AfeIVA = false;
                mv.AfeOrdComp = chkOrden.Checked;
                mv.ReqReferencia = chkReqRef.Checked;
                mv.ReqSispropietario = chkReqSpo.Checked;
                mv.NatMov = 1;
            }

            CN_CatMovimientos clsCatMovimientos = new CN_CatMovimientos();
            int verificador = 0;
            if (HFId_Mov.Value == "")
            {
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
                clsCatMovimientos.InsertarMovimientos(mv, session2.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    Inicializar();
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
                //mv.Id = Convert.ToInt32(HFId_Mov.Value);
                clsCatMovimientos.ModificarMovimientos(mv, session2.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    Alerta("Los datos se modificaron correctamente");
                    CargarInverso();
                }
                else
                {
                    Alerta("Ocurrió un error al intentar guardar los cambios");
                }
            }
            rgMovimiento.Rebind();
        }
        private void Nuevo()
        {
            txtNumero.Enabled = true;
            HFId_Mov.Value = "";
            txtNumero.Text = Valor;
            cmbTipo.SelectedIndex = 0;
             
            txtNombre.Text = "";
            cmbNaturaleza.SelectedIndex = 0;
            txtInverso.Text = "";
            cmbInverso.SelectedIndex = 0;
            cmbInverso.Text = cmbInverso.FindItemByValue("-1").Text;
            cmbAfecta.SelectedIndex = 0;
            chkIva.Checked = false;
            chkVenta.Checked = false;
            chkOrden.Checked = false;
            chkReqRef.Checked = false;
            chkReqSpo.Checked = false;
            chkActivo.Checked = true;
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
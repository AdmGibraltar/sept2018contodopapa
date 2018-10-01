using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.IO;

namespace SIANWEB
{
    public partial class Cat_ConsecutivoFacEle : System.Web.UI.Page
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];       
                    Response.Redirect("login.aspx" , false);
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
                        CargarCentros(RadComboBox1, 1);
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
            rgAcuse.Rebind();
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion.Id_Cd_Ver = Convert.ToInt32(RadComboBox1.SelectedItem.Value);
                Nuevo();
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)               
                    rgAcuse.DataSource = GetList();               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
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
                        Label15.Text = "";
                        Label16.Text = "";
                        HF_Id.Value = rgAcuse.Items[item]["Id"].Text;
                        HF_Tipo.Value = rgAcuse.Items[item]["TipoMovimiento"].Text;
                        this.txtAcuse.Text = rgAcuse.Items[item]["NombreAcuse"].Text;
                        txtLlaveSat.Text = rgAcuse.Items[item]["FolioSAT"].Text;
                        this.txtAñoAprobacion.Text = rgAcuse.Items[item]["Año"].Text;
                        this.txtRazonSocial.Text = rgAcuse.Items[item]["RazonSocial"].Text;
                        this.txtCertificadoRS.Text = rgAcuse.Items[item]["NumRazonSocial"].Text;
                        this.txtUltimoFolio.Text = rgAcuse.Items[item]["UltimoFolio"].Text.Replace("&nbsp;", "");
                        this.txtFolioIni.Text = rgAcuse.Items[item]["RangoInicial"].Text;
                        this.txtFolioFin.Text = rgAcuse.Items[item]["RangoFinal"].Text;
                        this.dpVigencia.SelectedDate = Convert.ToDateTime(rgAcuse.Items[item]["RangoFecha"].Text);
                        this.txtFolioAprobacion.Text = rgAcuse.Items[item]["FolioAprovacion"].Text;
                        this.chkActivo.Checked = Convert.ToBoolean(rgAcuse.Items[item]["Estatus"].Text);
                        //txtUltimoFolio.Enabled = true;
                        this.cmbTipoMovimiento.SelectedIndex = this.cmbTipoMovimiento.FindItemIndexByValue(rgAcuse.Items[item]["TipoMovimiento"].Text);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_ItemCommand");
            }
        }
        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgAcuse.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_PageIndexChanged");
            }
        }       
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
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
                    // Nuevo();
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
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && HF_Id.Value != "")
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
        private List<ConsecutivoFE> GetList()
        {
            try
            {
                List<ConsecutivoFE> List = new List<ConsecutivoFE>();
                CapaNegocios.CN_ConsecutivoFE clsCatUsuario = new CapaNegocios.CN_ConsecutivoFE();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                clsCatUsuario.ConsultaConsecutivo(session2.Id_Emp, session2.Id_Cd_Ver, session2.Emp_Cnx, ref List);
                return List;
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
        private void CargarCentros(RadComboBox rcb, int i)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (i == 1)
                {
                    if (Sesion.U_MultiOfi == false)
                    {
                        CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref rcb);
                        rcb.Visible = false;
                        this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + rcb.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                    }
                    else
                    {
                        CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref rcb);
                        rcb.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                    }
                }             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            if (Convert.ToInt32(txtFolioFin.Text) < Convert.ToInt32(txtFolioIni.Text))
            {
                Label15.Text = "*El folio final no puede ser menor al folio inicial";
                return;
            }
            else            
                Label15.Text = "";           

            if (txtUltimoFolio.Enabled)
            {
                if (txtUltimoFolio.Value < txtFolioIni.Value || txtUltimoFolio.Value > txtFolioFin.Value)
                {
                    Label16.Text = "*El ultimo folio debe estar entre el folio inicial y el folio final";
                    return;
                }
                else               
                    Label16.Text = "";               
            }
            else           
                Label16.Text = "";           
            if (txtAcuse.Text.Length < 3)
            {
                Alerta("La longitud del nombre del acuse debe ser de 3 caracteres");
                return;
            }

            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ConsecutivoFE ClaseCapaNegocio = new CN_ConsecutivoFE();
            ConsecutivoFE FactElect = new ConsecutivoFE();
            FactElect.NombreAcuse = txtAcuse.Text;
            FactElect.FolioSAT = txtLlaveSat.Text;
            FactElect.Año = txtAñoAprobacion.Text;
            FactElect.RazonSocial = txtRazonSocial.Text;
            FactElect.NumRazonSocial = txtCertificadoRS.Text;
            FactElect.UltimoFolio = txtUltimoFolio.Value;
            FactElect.RangoInicial = txtFolioIni.Text;
            FactElect.RangoFinal = txtFolioFin.Text;
            FactElect.RangoFecha = dpVigencia.SelectedDate;
            FactElect.TipoMovimiento = cmbTipoMovimiento.SelectedItem.Value;
            FactElect.FolioAprovacion = txtFolioAprobacion.Text;
            FactElect.Estatus = chkActivo.Checked;
            FactElect.Empresa = sesion.Id_Emp;// cmbEmpresa.SelectedItem.Value;
            FactElect.CentroDistribucion = sesion.Id_Cd_Ver;// cmbCentro.SelectedItem.Value;

            int verificador = 0;
            if (HF_Id.Value == "")
            {
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
                ClaseCapaNegocio.InsertarConsecutivo(ref FactElect, sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    rgAcuse.Rebind();
                    Nuevo();
                    Alerta("Los datos se guardaron correctamente");
                }
                else if (verificador == 3)
                    Alerta("No es posible traslapar los folios con otra serie de consecutivo");
                else
                    Alerta("La serie del consecutivo ya existe");
            }
            else
            {
                FactElect.Id = HF_Id.Value;
                FactElect.TipoMovimientoOld = HF_Tipo.Value;
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }
                ClaseCapaNegocio.ModificarConsecutivo(ref FactElect, sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    rgAcuse.Rebind();
                    Alerta("Los datos se modificaron correctamente");
                }
                else if (verificador == 2)
                    Alerta("Ocurrió un error al intentar modificar los datos");
                else if (verificador == 4)
                    Alerta("No es posible traslapar los folios con otra serie de consecutivo");
                else
                    Alerta("La serie del consecutivo ya existe");
            }
        }
        private void Nuevo()
        {
            HF_Id.Value = string.Empty;
            HF_Tipo.Value = string.Empty;
            txtAcuse.Text = string.Empty;
            txtLlaveSat.Text = string.Empty;
            txtAñoAprobacion.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtCertificadoRS.Text = string.Empty;
            txtUltimoFolio.Text = string.Empty;
            txtFolioIni.Text = string.Empty;
            txtFolioFin.Text = string.Empty;
            txtFolioAprobacion.Text = string.Empty;
            dpVigencia.SelectedDate = null;
            cmbTipoMovimiento.SelectedIndex = cmbTipoMovimiento.FindItemIndexByValue("-1");
            chkActivo.Checked = true;
            txtUltimoFolio.Enabled = false;
            Label15.Text = "";
            Label16.Text = "";
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HF_Id.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HF_Id.Value);
                    ct.Tabla = "CatConsFactEle";
                    ct.Columna = "Id_Cfe";
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.IO;

namespace SIANWEB
{
    public partial class CatPermisos : System.Web.UI.Page
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
                    Context.Items.Add("href", pag[pag.Length-1]);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    
                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    ValidarPermisos();
                    if (Page.IsPostBack == false)
                    {                       
                        cargarCboUsuarios();
                        CargarCentros();
                        this.cboUsuario.Focus();
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
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                cargarCboUsuarios();
                Nuevo();
                RadGridPermisos.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void ChkAccesarHeader_CheckedChanged(object sender, EventArgs e)
        {
            ErrorManager();
            try
            {
                bool Estatus = ((CheckBox)sender).Checked;
                foreach (GridDataItem dataItem in RadGridPermisos.MasterTableView.Items)
                {
                    ((CheckBox)dataItem.FindControl("ChkAccesar")).Checked = Estatus;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        public void ChkGrabarHeader_CheckedChanged(object sender, EventArgs e)
        {
            ErrorManager();
            try
            {
                bool Estatus = ((CheckBox)sender).Checked;
                foreach (GridDataItem dataItem in RadGridPermisos.MasterTableView.Items)
                {
                    ((CheckBox)dataItem.FindControl("ChkGrabar")).Checked = Estatus;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        public void ChkModificarHeader_CheckedChanged(object sender, EventArgs e)
        {
            ErrorManager();
            try
            {
                bool Estatus = ((CheckBox)sender).Checked;
                foreach (GridDataItem dataItem in RadGridPermisos.MasterTableView.Items)
                {
                    ((CheckBox)dataItem.FindControl("ChkModificar")).Checked = Estatus;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        public void ChkEliminarHeader_CheckedChanged(object sender, EventArgs e)
        {
            ErrorManager();
            try
            {
                bool Estatus = ((CheckBox)sender).Checked;
                foreach (GridDataItem dataItem in RadGridPermisos.MasterTableView.Items)
                {
                    ((CheckBox)dataItem.FindControl("ChkEliminar")).Checked = Estatus;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        public void ChkImprimirHeader_CheckedChanged(object sender, EventArgs e)
        {
            ErrorManager();
            try
            {
                bool Estatus = ((CheckBox)sender).Checked;
                foreach (GridDataItem dataItem in RadGridPermisos.MasterTableView.Items)
                {
                    ((CheckBox)dataItem.FindControl("ChkImprimir")).Checked = Estatus;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cboUsuario_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if (this.cboUsuario.SelectedValue != string.Empty)
                {
                    if (Convert.ToInt32(this.cboUsuario.SelectedValue) > 0)
                    {
                        cargarPermisos2();
                        //revisar();
                        this.RadGridPermisos.Visible = true;
                    }
                    else
                        this.RadGridPermisos.Visible = false;                   
                }
                else
                {
                    this.RequiredFieldValidator1.IsValid = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        private void gridChek(bool estatus, string ChekName)
        {
            try
            {
                GridHeaderItem headerItem = (GridHeaderItem)RadGridPermisos.MasterTableView.GetItems(GridItemType.Header)[0];
                CheckBox chkbx = (CheckBox)headerItem.FindControl(ChekName);
                chkbx.Checked = estatus;
            }
            catch (Exception ex)
            {
                throw ex;
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
                    //regresar()
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                    //int combo = !string.IsNullOrEmpty(cboUsuario.SelectedValue) ? Convert.ToInt32(cboUsuario.SelectedValue) : Sesion.Id_U;
                    //if (Sesion.Id_U != combo)
                    //{
                        _PermisoGuardar = Permiso.PGrabar;
                        _PermisoModificar = Permiso.PModificar;
                    //}
                    //else
                    //{
                    //    _PermisoGuardar = true;
                    //    _PermisoModificar = true;
                    //}
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (!_PermisoGuardar)//guardar                    
                        this.RadToolBar1.Items[6].Visible = false;
                    else
                        this.RadToolBar1.Items[6].Visible = true;
                    if (!_PermisoGuardar && !_PermisoModificar)//modificar                      
                        this.RadToolBar1.Items[5].Visible = false;
                    else
                        this.RadToolBar1.Items[5].Visible = true;
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarCboUsuarios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.cboUsuario);
                this.cboUsuario.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        private void cargarPermisos2()
        {
            try
            {
                CN_PermisosU clsPermisosU = new CN_PermisosU();
                Permiso permiso = new Permiso();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                permiso.Id_U = Convert.ToInt32(this.cboUsuario.SelectedValue);
                permiso.Id_Cd = session2.Id_Cd_Ver;
                permiso.Id_Emp = session2.Id_Emp;
                clsPermisosU.ConsultaPermisosUsuario(permiso, session2.Emp_Cnx, ref RadGridPermisos);
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
                this.RadGridPermisos.DataSource = null;
                this.RadGridPermisos.DataBind();
                this.RadGridPermisos.Visible = false;            
                this.cboUsuario.SelectedValue = "0";              
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (!_PermisoModificar && (Sesion.Id_U != Convert.ToInt32(cboUsuario.SelectedValue)))
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }
                CapaNegocios.CN_PermisosU clsPermisosU = new CapaNegocios.CN_PermisosU();
                Int32 Verificador = default(Int32);
                bool PAccesar = false;
                bool PGrabar = false;
                bool PModificar = false;
                bool PEliminar = false;
                bool PImprimir = false;
                for (int cont = 0; cont <= this.RadGridPermisos.Items.Count - 1; cont++)
                {
                    Permiso permiso = new Permiso();
                    permiso.Id_Cd = Sesion.Id_Cd_Ver;
                    permiso.Id_U = Convert.ToInt32(this.cboUsuario.SelectedValue);
                    permiso.Sm_cve = Convert.ToInt32(this.RadGridPermisos.Items[cont]["MenuCve"].Text);
                    permiso.Sp_PAccesar = ((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkAccesar")).Checked;
                    permiso.Sp_PGrabar = ((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkGrabar")).Checked;
                    permiso.Sp_PModificar = ((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkModificar")).Checked;
                    permiso.Sp_PEliminar = ((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkEliminar")).Checked;
                    permiso.Sp_PImprimir = ((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkImprimir")).Checked;

                    if (((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkAccesar")).Checked != Convert.ToBoolean(this.RadGridPermisos.Items[cont]["SpTu_PAccesar"].Text))                    
                        PAccesar = true;                   

                    if (((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkGrabar")).Checked != Convert.ToBoolean(this.RadGridPermisos.Items[cont]["SpTu_PGrabar"].Text))                   
                        PGrabar = true;                   

                    if (((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkModificar")).Checked != Convert.ToBoolean(this.RadGridPermisos.Items[cont]["SpTu_PModificar"].Text))                    
                        PModificar = true;                    

                    if (((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkEliminar")).Checked != Convert.ToBoolean(this.RadGridPermisos.Items[cont]["SpTu_PEliminar"].Text))                   
                        PEliminar = true;                   

                    if (((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkImprimir")).Checked != Convert.ToBoolean(this.RadGridPermisos.Items[cont]["SpTu_PImprimir"].Text))                   
                        PImprimir = true;                   

                    permiso.PAccesar = PAccesar;
                    permiso.PGrabar = PGrabar;
                    permiso.PModificar = PModificar;
                    permiso.PEliminar = PEliminar;
                    permiso.PImprimir = PImprimir;
                    clsPermisosU.ModificarPermisosUsuario(permiso, Sesion.Emp_Cnx, ref Verificador);
                }
                Alerta("Los cambios se guardaron correctamente");
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
        //private void revisar()
        //{
        //    try
        //    {
        //        bool Ac = true; 
        //        bool Gr = true;
        //        bool Mo = true;
        //        bool El = true;
        //        bool Im = true;
        //        CheckBox ChkAccesar = default(CheckBox);
        //        CheckBox ChkGrabar = default(CheckBox);
        //        CheckBox ChkModificar = default(CheckBox);
        //        CheckBox ChkEliminar = default(CheckBox);
        //        CheckBox ChkImprimir = default(CheckBox);

        //        foreach (Telerik.Web.UI.GridDataItem item in RadGridPermisos.Items)
        //        {
        //            ChkAccesar = (CheckBox)item.FindControl("ChkAccesar");
        //            ChkGrabar = (CheckBox)item.FindControl("ChkGrabar");
        //            ChkModificar = (CheckBox)item.FindControl("ChkModificar");
        //            ChkEliminar = (CheckBox)item.FindControl("ChkEliminar");
        //            ChkImprimir = (CheckBox)item.FindControl("ChkImprimir");
        //            if (ChkAccesar.Checked == false)                    
        //                Ac = false;                  
        //            if (ChkGrabar.Checked == false)                   
        //                Gr = false;                    
        //            if (ChkModificar.Checked == false)                    
        //                Mo = false;                    
        //            if (ChkEliminar.Checked == false)                   
        //                El = false;
        //            if (ChkImprimir.Checked == false)                    
        //                Im = false;                    
        //        }
        //        gridChek(Ac, "ChkAccesarHeader");
        //        gridChek(Gr, "ChkGrabarHeader");
        //        gridChek(Mo, "ChkModificarHeader");
        //        gridChek(El, "ChkEliminarHeader");
        //        gridChek(Im, "ChkImprimirHeader");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
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
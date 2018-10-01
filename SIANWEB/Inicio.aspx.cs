using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.IO;
using CapaDatos;
using LibreriaReportes;
using Telerik.Reporting.Processing;
using System.Collections;

namespace SIANWEB
{
    public partial class Inicio : System.Web.UI.Page
    {
        #region Variables
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {

                        //Session.Timeout = 1;
                        ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        string assemblyName = String.Format("Version: {0}<br>Dated: {1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(), System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("dd/MM/yyyy hh:mm tt"));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void CmbCentro_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Sesion Sesion = new Sesion();
            //Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
            try
            {


                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = sesion;
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                sesion = sesion2;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
        #region Funciones
        private bool ValidarSesion()
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    //

                    //Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);

                    return false;
                }
                return true;
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

                //CD_PermisosU CN_PermisosU = new CD_PermisosU();
                //CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                //if (Permiso.PAccesar == true)
                //{
                //    _PermisoGuardar = Permiso.PGrabar;
                //    _PermisoModificar = Permiso.PModificar;
                //    _PermisoEliminar = Permiso.PEliminar;
                //    _PermisoImprimir = Permiso.PImprimir;

                //    if (Sesion.U_MultiOfi)
                //    {
                //        this.RadToolBar1.Items[6].Enabled = _PermisoGuardar; //new
                //        if (_PermisoGuardar || _PermisoModificar)
                //        {
                //            this.RadToolBar1.Items[5].Enabled = true; //save
                //        }
                //        this.RadToolBar1.Items[4].Visible = false; //Regresar
                //        this.RadToolBar1.Items[3].Enabled = _PermisoEliminar; //Eliminar
                //        this.RadToolBar1.Items[2].Visible = false; //Imprimir
                //        this.RadToolBar1.Items[1].Visible = false; //Correo
                //    }
                //    else //usuario No multi-CentroDistribucion
                //    {
                //        this.RadToolBar1.Items[6].Visible = false; //new
                //        if (_PermisoGuardar || _PermisoModificar)
                //        {
                //            this.RadToolBar1.Items[5].Enabled = true; //save
                //        }
                //        this.RadToolBar1.Items[4].Visible = false; //Regresar
                //        this.RadToolBar1.Items[3].Enabled = _PermisoEliminar; //Eliminar
                //        this.RadToolBar1.Items[2].Visible = false; //Imprimir
                //        this.RadToolBar1.Items[1].Visible = false; //Correo
                //    }
                //}
                //else
                //{
                //    Response.Redirect("Inicio.aspx");
                //}
                //CN_Ctrl ctrl = new CN_Ctrl();
                //ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                //ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void ValidarPermisos()
        //{
        //    try
        //    {
        //        Sesion Sesion = new Sesion();
        //        Sesion = (Sesion)Session["Sesion" + Session.SessionID];

        //        Pagina pagina = new Pagina();
        //        string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
        //        if (pag.Length > 1)
        //        {
        //            pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
        //        }
        //        else
        //        {
        //            pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
        //        }
        //        CN_Pagina CapaNegocio = new CN_Pagina();
        //        CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

        //        Session["Head" + Session.SessionID] = pagina.Path;
        //        this.Title = pagina.Descripcion;
        //        Permiso Permiso = new Permiso();
        //        Permiso.Id_U = Sesion.Id_U;
        //        Permiso.Id_Cd = Sesion.Id_Cd;
        //        Permiso.Sm_cve = pagina.Clave;
        //        Esta clave depende de la pantalla

        //        CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
        //        CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

        //        if (Permiso.PAccesar == true)
        //        {
        //            _PermisoGuardar = Permiso.PGrabar;
        //            _PermisoModificar = Permiso.PModificar;
        //            _PermisoEliminar = Permiso.PEliminar;
        //            _PermisoImprimir = Permiso.PImprimir;

        //            if (Permiso.PGrabar == false)
        //            {
        //                this.rtb1.Items[6].Visible = false;
        //            }
        //            if (Permiso.PGrabar == false && Permiso.PModificar == false)
        //            {
        //                this.rtb1.Items[5].Visible = false;
        //            }
        //            if (Permiso.PEliminar == false)
        //            {
        //                this.RadToolBar1.Items[3].Visible = false;
        //            }
        //            if(Permiso.PImprimir == false)
        //            {
        //                this.RadToolBar1.Items[2].Visible = false;
        //            }

        //            Nuevo
        //            Me.RadToolBar1.Items(6).Enabled = False
        //            Guardar
        //            Me.RadToolBar1.Items(5).Enabled = False
        //            Regresar
        //            this.rtb1.Items[4].Visible = false;
        //            Eliminar
        //            this.rtb1.Items[3].Visible = false;
        //            Imprimir
        //            this.rtb1.Items[2].Visible = false;
        //            Correo
        //            this.rtb1.Items[1].Visible = false;
        //        }
        //        else
        //        {
        //            Response.Redirect("Inicio.aspx");
        //        }

        //        CN_Ctrl ctrl = new CN_Ctrl();
        //        ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
        //        ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();



                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    if (sesion.Id_Cd_Ver == 0)
                    {
                        CmbCentro_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                    }
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
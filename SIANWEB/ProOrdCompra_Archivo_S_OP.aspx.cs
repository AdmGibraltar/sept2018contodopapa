
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
using System.Collections;

using Telerik.Web.UI.GridExcelBuilder;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.OleDb;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

//using Excel = Microsoft.Office.Interop.Excel;

namespace SIANWEB
{
    public partial class ProOrdCompra_Archivo_S_OP : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ValidarPermisos();

                if (!Page.IsPostBack)
                {
                    LlenarComboProveedores();
                    cmbProveedor.SelectedValue = "100";
                    txtProveedor.Text = "100";
                }

            }
            catch (Exception ex)
            {

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
                        break;

                    case "save":

                        break;
                    case "bajarExcel":

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Núm");
                        dt.Columns.Add("Ordenado");

                        dt.Rows.Add(0, 0);

                        ExportarAExcel(dt);
                        break;

                    case "subirExcel":

                        archivoExcel();

                        break;






                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ExportarAExcel(DataTable tbl)
        {
            try
            {
                if (tbl == null || tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                grid.DataSource = tbl;
                grid.DataBind();

                //grid.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                grid.ExportSettings.ExportOnlyData = true;
                grid.ExportSettings.OpenInNewWindow = false;

                grid.MasterTableView.ExportToExcel();


            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }


        private void LlenarComboProveedores()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spProveedores_Combo", ref cmbProveedor);
        }



        private void archivoExcel()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                RAM1.ResponseScripts.Add("AbrirVentana_Excel('" + sesion.Id_Emp + "','" + sesion.Id_Cd_Ver + "','" + txtProveedor.Text + "')");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        private void EnviarCorreoAutorizacion()
        {
            try
            {





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
                    //nuevo
                    //this.RadToolBar1.Items[1].Visible = false;

                    //if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    //{
                    //    //guardar
                    //    this.RadToolBar1.Items[6].Visible = false;
                    //    //subir archivo
                    //    this.RadToolBar1.Items[7].Visible = false;
                    //}
                    ////Regresar
                    //this.RadToolBar1.Items[5].Visible = false;
                    ////Eliminar
                    //this.RadToolBar1.Items[4].Visible = false;
                    ////Imprimir
                    //this.RadToolBar1.Items[3].Visible = false;
                    ////Correo
                    //this.RadToolBar1.Items[2].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {

                }
            }
            catch (Exception ex)
            {

            }
        }




    }
}
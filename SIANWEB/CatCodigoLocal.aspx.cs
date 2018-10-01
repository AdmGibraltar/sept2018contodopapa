using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;

namespace SIANWEB
{
    public partial class CatCodigoLocal : System.Web.UI.Page
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

                    //string str = Context.Items["href"].ToString();
                    //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);Context.Items.Add("href", pag[pag.Length-1]);Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
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
        protected void CmbCentro_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                                Sesion sesion = new Sesion();                sesion = (Sesion)Session["Sesion" + Session.SessionID];                if (sesion == null)                {                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);                }                CN__Comun comun = new CN__Comun();                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                int max = 0;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatProducto CnCatProducto = new CN_CatProducto();
                CnCatProducto.ConsultarMaxLocal(Convert.ToInt32(Sesion.Id_Cd_Ver), Sesion.Id_Emp, Sesion.Emp_Cnx, ref max);
                Session["IdCategoria" + Session.SessionID] = cmbCategorias.SelectedValue;
                Session["IdLocal" + Session.SessionID] =   Sesion.Id_Cd_Ver.ToString().Substring(0,2) + cmbCategorias.SelectedValue + max.ToString("0#");
                Response.Redirect("CatProductos.aspx?id=1", false);

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
            CargarCategorias();

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
        private void CargarCategorias()
        {
            try
            {
                cmbCategorias.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Productos para el aprovisionamiento de baños (papel y control de olores)", "01"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Bolsas para manejo de basura", "02"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Cepillos de plásticos para labores de limpieza y sus mangos respectivos", "03"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Cubetas de plástico para labores de limpieza", "04"));
                //cmbCategorias.Items.Add(new RadComboBoxItem("", "05"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Detergentes en polvo para uso general", "06"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Escobas en general", "07"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Fibras de mano para limpieza en general", "08"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Franelas, jergas y trapos para labores de limpieza", "09"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Guantes", "10"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Jaladores", "11"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Recogedores de plástico y de lámina", "12"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Trapeadores húmedos", "13"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Trapeadores secos (mops secos o aviones)", "14"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Varios", "15"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Materias primas", "16"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Procesos", "17"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Códigos Key fraccionados para cuentas especiales que así lo requieran", "18"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Especialidades químicas para limpieza (ceras para piso, etc)", "19"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Equipos y suplementos para limpieza", "20"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Mano de obra por servicios prestados", "21"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Refacciones", "22"));
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

                    //if (Permiso.PGrabar == false)
                    //{
                    //    this.rtb1.Items[6].Visible = false;
                    //}
                    //if (Permiso.PGrabar == false || Permiso.PModificar == false)
                    //{
                    //    this.rtb1.Items[5].Visible = false;
                    //}
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
                    //this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    //this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    //this.rtb1.Items[2].Visible = false;
                    //Correo
                    //this.rtb1.Items[1].Visible = false;
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
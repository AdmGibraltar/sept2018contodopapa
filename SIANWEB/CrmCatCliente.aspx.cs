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
    public partial class CrmCatCliente : System.Web.UI.Page
    {
        #region Variables
        private static bool _PermisoGuardar;
        private static bool _PermisoModificar;
        private static bool _PermisoEliminar;
        private static bool _PermisoImprimir;

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (session.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        //CargarCentros();
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
            try
            {
                //txtClave.Text = Valor;

                CargarUEN();
                CargarSegmentos();
                CargarTerritorios();
                
                //CargarSegmentos();
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUEN()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(0, session.Id_Emp, session.Emp_Cnx, "spCatUen_Combo", ref cmbUEN);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

               // cmbUEN.Items.Remove(0);
                cmbUEN.SelectedIndex = 0;
                cmbUEN.Text = cmbUEN.Items[0].Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    //Cache["href"] = pag[pag.Length - 1];
                    Cache["href"] = pag[pag.Length - 1];

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
            //try
            //{


            //    Pagina pagina = new Pagina();
            //    string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
            //    if (pag.Length > 1)
            //    {
            //        pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
            //    }
            //    else
            //    {
            //        pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
            //    }

            //    CN_Pagina CapaNegocio = new CN_Pagina();
            //    CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

            //    Session["Head" + Session.SessionID] = pagina.Path;
            //    this.Title = pagina.Descripcion;
            //    Permiso Permiso = new Permiso();
            //    Permiso.Id_U = session.Id_U;
            //    Permiso.Id_Cd = session.Id_Cd;
            //    Permiso.Sm_cve = pagina.Clave;
            //    //Esta clave depende de la pantalla

            //    CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
            //    CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

            //    if (Permiso.PAccesar == true)
            //    {
            //        _PermisoGuardar = Permiso.PGrabar;
            //        _PermisoModificar = Permiso.PModificar;
            //        _PermisoEliminar = Permiso.PEliminar;
            //        _PermisoImprimir = Permiso.PImprimir;

            //        if (Permiso.PGrabar == false)
            //        {
            //            this.rtb1.Items[6].Visible = false;
            //        }
            //        if (Permiso.PGrabar == false && Permiso.PModificar == false)
            //        {
            //            this.rtb1.Items[5].Visible = false;
            //        }
            //        //if (Permiso.PEliminar == false)
            //        //{
            //        //    this.RadToolBar1.Items[3].Visible = false;
            //        //}
            //        //if(Permiso.PImprimir == false)
            //        //{
            //        //    this.RadToolBar1.Items[2].Visible = false;
            //        //}

            //        //Nuevo
            //        //Me.RadToolBar1.Items(6).Enabled = False
            //        //Guardar
            //        //Me.RadToolBar1.Items(5).Enabled = False
            //        //Regresar
            //        this.rtb1.Items[4].Visible = false;
            //        //Eliminar
            //        this.rtb1.Items[3].Visible = false;
            //        //Imprimir
            //        this.rtb1.Items[2].Visible = false;
            //        //Correo
            //        this.rtb1.Items[1].Visible = false;
            //    }
            //    else
            //    {
            //        Response.Redirect("Inicio.aspx");
            //    }

            //    CN_Ctrl ctrl = new CN_Ctrl();
            //    ctrl.ValidarCtrl(session, pagina.Clave, divPrincipal);
            //    ctrl.ListaCtrls(session.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                //RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
                //this.lblMensaje.Text = "";
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
                //this.lblMensaje.Text = Message;
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
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

        protected void cmbUEN_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarSegmentos();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CargarSegmentos()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(0, session.Id_Emp, Convert.ToInt32(cmbUEN.SelectedValue), session.Emp_Cnx, "spCatSegmentosUen_Combo", ref cmbSegmento);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                //cmbSegmento.Items.Remove(0);
                cmbSegmento.SelectedIndex = 0;
                cmbSegmento.Text = cmbSegmento.Items[0].Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void cmbSegmento_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarTerritorios();
                //Session("TerritorioID") = Me.ddlSegmentos.SelectedValue
                //Session("SegmentoID") = Me.ddlSegmentos.SelectedValue
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CargarTerritorios()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(0, session.Id_Emp, Convert.ToInt32(cmbSegmento.SelectedValue), session.Emp_Cnx, "spCatTerritorioSegmento_Combo", ref cmbTerritorios);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                //cmbTerritorios.Items.Remove(0);
                cmbTerritorios.SelectedIndex = 0;
                cmbTerritorios.Text = cmbTerritorios.Items[0].Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ibtnFiltro_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ErrorManager();
                rg1.Rebind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private List<Clientes> GetList()
        {
            try
            {
                List<Clientes> List = new List<Clientes>();
                CN_CatCliente cn_catcliente = new CN_CatCliente();

                Clientes cte = new Clientes();
                cte.Id_Emp = session.Id_Emp;
                cte.Id_Cd = session.Id_Cd_Ver;
                cte.Id_Uen = cmbUEN.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(cmbUEN.SelectedValue);
                cte.Id_Seg = cmbSegmento.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(cmbSegmento.SelectedValue);
                cte.Id_Terr = cmbTerritorios.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(cmbTerritorios.SelectedValue);
                cte.Id_Rik = session.Id_U;
                cte.Id_Cte = (int?)txtNoCliente.Value;
                cn_catcliente.Lista(cte, session.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void lnkDescargar_Click(object sender, EventArgs e)
        {

        }

        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    (item.FindControl("lnkNombre") as LinkButton).PostBackUrl = "inicio.aspx?ID=" + item.GetDataKeyValue("Id_Cte").ToString();
                    //WebControl Button = default(WebControl);
                    //string clickHandler = "";
                    //Button = (WebControl)item["Baja"].Controls[0];
                    //clickHandler = Button.Attributes["onclick"];
                    //Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", "<b>#" + item.GetDataKeyValue("Id_Ped").ToString() + "</b> de <b>" + item.GetDataKeyValue("Cte_Nom").ToString() + "</b>");

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
    }
}

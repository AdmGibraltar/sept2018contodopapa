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
    public partial class ProAsignPedxPrd_Admon : System.Web.UI.Page
    {
        #region "Variables"
        public static bool _PermisoGuardar;
        public static bool _PermisoModificar;
        public static bool _PermisoEliminar;
        public static bool _PermisoImprimir;
        #endregion
        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Context.Items.Add("href", pag[pag.Length - 1]);
                    Server.Transfer("Login.aspx");

                    //string str = Context.Items["href"].ToString();
                    //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);Context.Items.Add("href", pag[pag.Length-1]);Server.Transfer("Login.aspx");
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
                ErrorManager(ex, "Page_Load");
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgPedido.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void rg_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem item = (GridDataItem)e.Item;
            //    ImageButton imgButton = (ImageButton)item.FindControl("ImageButton1");
            //    imgButton.OnClientClick = "return AbrirVentana_OrdenCompra_Edicion('" + item.ItemIndex + "')";
            //}
        }
        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                this.rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg_PageIndexChanged");
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //DataTable dt = (DataTable)Session["ListPedidos" + Session.SessionID];
            //DataRow[] dr = dt.Select(GenerarQry());
            //List = dt.Clone();
            //foreach (DataRow d in dr)
            //{
            //    List.ImportRow(d);
            //}
            //rgPedido.Rebind();
            try
            {

                this.rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg_PageIndexChanged");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        Inicializar();
                        break;
                    case "ok":
                        string status = Session["Ped_Accion" + Session.SessionID].ToString();
                        if (status == "I")
                        {
                            //Imprimir();
                        }
                        else
                        {
                            //Baja();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }

        }
        protected void rgPedido_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                 
                switch (e.CommandName)
                {
                    case "Asignar":

                        RAM1.ResponseScripts.Add("return AbrirVentana_ProAsignPedxPrd('" + gi.Cells[2].Text + "','" + gi.Cells[5].Text + "','" + gi.Cells[6].Text + "')");
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgPedido_ItemCommand");
            }
        }
        #endregion
        #region "Funciones"
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Producto> GetList()
        {
            try
            {
                List<Producto> List = new List<Producto>();
                CN_CatProducto clsCatBanco = new CN_CatProducto();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Producto prd = new Producto();
                prd.Id_Emp = session2.Id_Emp;
                prd.Id_Cd = session2.Id_Cd_Ver;
                prd.Filtro_Nombre = txtProducto.Text;
                prd.Filtro_PrdIni = txtProducto1.Text;
                prd.Filtro_PrdFin = txtProducto2.Text;
                clsCatBanco.ConsultaProductoAsig_Admin(prd, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Inicializar()
        {
            try
            {
                //List = GetList();
                //Session["ListPedidos" + Session.SessionID] = List;
                rgPedido.Rebind();

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
                {
                    Response.Redirect("Inicio.aspx");
                }

                //CN_Ctrl ctrl = new CN_Ctrl();
                //ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                //ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);

                CN_CatCentroDistribucion catcentro = new CN_CatCentroDistribucion();
                CentroDistribucion cd = new CentroDistribucion();
                catcentro.ConsultarCentroDistribucion(ref cd, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx);

                if (!cd.Cd_ActivaCapPedRep)
                {
                    this.rtb1.Items[6].Visible = false;
                    rgPedido.Columns[12].Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region "ErrorManager"
        private void RadConfirm(string mensaje)
        {
            try
            {

                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
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

        protected void rgPedido_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgPedido.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgPedido_PageIndexChanged");
            }
        }

         
    }
}
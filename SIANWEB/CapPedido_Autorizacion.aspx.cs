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
using System.Text;
using System.Collections;
using CapaDatos;

namespace SIANWEB
{
    public partial class CapPedido_Autorizacion : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                                Sesion sesion = new Sesion();                sesion = (Sesion)Session["Sesion" + Session.SessionID];                if (sesion == null)                {                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);                }                CN__Comun comun = new CN__Comun();                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
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
        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                this.rgPedido.Rebind();
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
                if (txtFecha1.SelectedDate > txtFecha2.SelectedDate)
                {
                    Alerta("La fecha inicial no debe ser mayor a la fecha final");
                    return;
                }

                if (txtCliente1.Value > txtCliente2.Value)
                {
                    Alerta("El número de cliente inicial no debe ser mayor al número de cliente final");
                    return;
                }

                this.rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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


                    case "Autorizar":
                        Autorizar(Convert.ToInt32(gi.Cells[7].Text));
                        break;


                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgPedido_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = "";

                Button = (WebControl)item["Autorizar"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Ped").ToString());
            }
        }
        #endregion

        #region Funciones
        private void Autorizar(int pedido)
        {
            try
            {
                Funciones funcion = new Funciones();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPedido cn_cappedido = new CN_CapPedido();
                Pedido ped = new Pedido();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Ped = pedido;
                ped.Ped_FechaAut = funcion.GetLocalDateTime(sesion.Minutos);
                ped.Id_U = sesion.Id_U;
                int verificador = -1;
                cn_cappedido.Autorizar(ped, sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    Alerta("Se autorizó correctamente el pedido #" + pedido);
                }
                else
                {
                    Alerta("Ocurrió un error al intentar autorizar el pedido");
                }
                rgPedido.Rebind();

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
        private List<Pedido> GetList()
        {
            try
            {
                List<Pedido> List = new List<Pedido>();
                CN_CapPedido clsCatBanco = new CN_CapPedido();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session2.Id_Emp;
                pedido.Id_Cd = session2.Id_Cd_Ver;
                pedido.Filtro_Nombre = txtNombre.Text;
                pedido.Filtro_CteIni = txtCliente1.Value.ToString();
                pedido.Filtro_CteFin = txtCliente2.Value.ToString();
                pedido.Filtro_FecIni = txtFecha1.SelectedDate;
                pedido.Filtro_FecFin = txtFecha2.SelectedDate;
                pedido.Filtro_usuario = session2.Propia ? session2.Id_U.ToString() : "";
                clsCatBanco.ConsultaPedidoAutorizacion_Lista(pedido, session2.Emp_Cnx, ref List);
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion.CalendarioIni >= txtFecha1.MinDate && Sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                }
                if (Sesion.CalendarioFin >= txtFecha2.MinDate && Sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = Sesion.CalendarioFin;
                }
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
                    //this.rtb1.Items[5].Visible = false;
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

                //CN_Ctrl ctrl = new CN_Ctrl();
                //ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                //ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);

                //CN_CatCentroDistribucion catcentro = new CN_CatCentroDistribucion();
                //CentroDistribucion cd = new CentroDistribucion();
                //catcentro.ConsultarCentroDistribucion(ref cd, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx);

                //if (!cd.Cd_ActivaCapPedRep)
                //{
                    //this.rtb1.Items[6].Visible = false;
                //    rgPedido.Columns[12].Visible = false;
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ErrorManager
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
    }
}
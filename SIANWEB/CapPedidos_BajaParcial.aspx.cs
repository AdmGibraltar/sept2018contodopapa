using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaEntidad;
using Telerik.Web.UI;
using System.Data;

namespace SIANWEB
{
    public partial class CapPedidos_BajaParcial : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                        rgPedido.Rebind();
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
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ErrorManager();
                CN_CapPedido cn_cappedido = new CN_CapPedido();
                Pedido pedido = new Pedido();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Ped = txtId.Value.HasValue ? (int)txtId.Value.Value : -1;

                cn_cappedido.ConsultaPedido(ref pedido, sesion.Emp_Cnx);
                txtIdCte.DbValue = pedido.Id_Cte == 0 ? null : (int?)pedido.Id_Cte;
                txtNCte.Text = pedido.Cte_NomComercial;
                txtIdTer.DbValue = pedido.Id_Ter == 0 ? null : (int?)pedido.Id_Ter;
                txtNTer.Text = pedido.Ter_Nombre;
                txtIdRik.DbValue = pedido.Id_Rik == 0 ? null : (int?)pedido.Id_Rik;
                txtNRik.Text = pedido.Rik_Nombre;

                if (pedido.Id_Cte == 0)
                {
                    txtId.Text = "";
                    txtIdCte.DbValue = null;
                    txtNCte.Text = "";
                    txtIdTer.DbValue = null;
                    txtNTer.Text = "";
                    txtIdRik.DbValue = null;
                    txtNRik.Text = "";

                    if (pedido.Id_Ped != -1)
                    {
                        AlertaFocus("No se encontro el pedido", txtId.ClientID);
                    }
                }


            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ERROR|"))
                {
                    AlertaFocus(ex.Message.Replace("ERROR|", ""), txtId.ClientID);
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                rgPedido.Rebind();
            }

        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgPedido.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                Guardar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                        this.rtb1.Items[5].Visible = false;
                    }
                    else
                    {
                        this.rtb1.Items[5].Visible = true;
                    }
                    //Guardar
                    this.rtb1.Items[6].Visible = false;
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

                if (Sesion.Id_Rik != -1)
                {
                    //Captura de pedidos por parte del representante
                    CN_CatCentroDistribucion catcentro = new CN_CatCentroDistribucion();
                    CentroDistribucion cd = new CentroDistribucion();
                    catcentro.ConsultarCentroDistribucion(ref cd, Sesion.Id_Cd_Ver, Sesion.Id_Emp, Sesion.Emp_Cnx);

                    if (!cd.Cd_ActivaCapPedRep)
                    {
                        this.rtb1.Items[6].Visible = false;
                        rgPedido.Columns[12].Visible = false;
                    }
                }
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
        private List<PedidoDet> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<PedidoDet> list = new List<PedidoDet>();
                PedidoDet pedido = new PedidoDet();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Ped = txtId.Value.HasValue ? (int)txtId.Value.Value : -1;

                CN_CapPedido cn_cappedido = new CN_CapPedido();
                try
                {
                    cn_cappedido.ConsultaPedidoCancelacion(pedido, sesion.Emp_Cnx, ref list);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("ERROR|"))
                    {
                        txtId.Text = "";
                        txtIdCte.DbValue = null;
                        txtNCte.Text = "";
                        txtIdTer.DbValue = null;
                        txtNTer.Text = "";
                        txtIdRik.DbValue = null;
                        txtNRik.Text = "";
                        AlertaFocus(ex.Message.Replace("ERROR|", ""), txtId.ClientID);
                    }
                    else
                    {
                        throw ex;
                    }
                }


                return list;
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
                List<PedidoDet> list = new List<PedidoDet>();
                PedidoDet det;
                double? cancelado;
                foreach (GridDataItem gdi in rgPedido.Items)
                {
                    det = new PedidoDet();
                    det.Id_Prd = Convert.ToInt32(gdi["Id_Prd"].Text);
                    det.Id_Ter = Convert.ToInt32(gdi["Id_Ter"].Text);
                    cancelado = (gdi["Cant_cancelado"].FindControl("RadNumericTextBox1") as RadNumericTextBox).Value;
                    det.Cancelado = cancelado != null ? Convert.ToInt32(cancelado) : 0;
                    list.Add(det);
                }
                CN_CapPedido cn_cappedido = new CN_CapPedido();
                Pedido ped = new Pedido();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Ped = (int)txtId.Value;

                int verificador = 0;

                cn_cappedido.BajaParcial(ped, list, sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    Alerta("Las cantidades fueron actualizadas correctamente.");
                    txtId.Text = "";
                    txtId.Text = "";
                    txtIdCte.DbValue = null;
                    txtNCte.Text = "";
                    txtIdTer.DbValue = null;
                    txtNTer.Text = "";
                    txtIdRik.DbValue = null;
                    txtNRik.Text = "";
                    rgPedido.Rebind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                imgAceptar_Click(null, null);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
    }
}
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
    public partial class wfrmPrincipalCampanias : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

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
                DisplayMensajeAlerta(ex.Message);
            }
        }

        #region Eventos

        protected void rgCampanas_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgCampanas.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void rgCampanas_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgCampanas.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }

        protected void rgCampanas_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                Int32 item = default(Int32);

                if (e.Item == null) return;

                item = e.Item.ItemIndex;
                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgCampanas.Items[item]["Id_Emp"].Text);
                    int Id_Cam = Convert.ToInt32(rgCampanas.Items[item]["Id_Cam"].Text);

                    switch (e.CommandName.ToString())
                    {
                        case "Editar":
                            Response.Redirect(string.Concat("wfrmDetalleCampanias.aspx?Id_Emp=", Id_Emp.ToString(), "&Id_Cam=", Id_Cam.ToString()));
                            break;

                        case "Eliminar":
                            mensajeError = "wfrmCampana_delete_error";
                            int verificador = 0;
                            new CN_wfrmCampanas().EliminarCampana(Id_Emp, Id_Cam, this.session.Emp_Cnx, ref verificador);
                            rgCampanas.Rebind();
                            DisplayMensajeAlerta("wfrmCampana_delete_ok");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, mensajeError));
            }
        }


        //protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    try
        //    {
        //        ErrorManager();
        //        if (e.Item is GridDataItem)
        //        {
        //            GridDataItem item = (GridDataItem)e.Item;
        //            string link = "wfrmDetalleCliente.aspx?" +
        //                "ID=" + item.GetDataKeyValue("Id_Cte").ToString() +
        //                //"&Uen=" + item.GetDataKeyValue("Id_Uen").ToString() +
        //                "&Seg=" + item.GetDataKeyValue("Id_Seg").ToString() +
        //                "&Ter=" + item.GetDataKeyValue("Id_Terr").ToString();
        //            (item.FindControl("lnkNombre") as LinkButton).PostBackUrl = link;

        //            //WebControl Button = default(WebControl);
        //            //string clickHandler = "";
        //            //Button = (WebControl)item["Baja"].Controls[0];
        //            //clickHandler = Button.Attributes["onclick"];
        //            //Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", "<b>#" + item.GetDataKeyValue("Id_Ped").ToString() + "</b> de <b>" + item.GetDataKeyValue("Cte_Nom").ToString() + "</b>");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}

        protected void ibtnNuevaOportunidad_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrmDetalleCampanias.aspx");
        }

        #endregion

        #region Funciones
        private void Inicializar()
        {
            try
            {

                //CargarSegmentos();
                rgCampanas.Rebind();
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

        private List<Campanas> GetList()
        {
            try
            {
                List<Campanas> List = new List<Campanas>();
                new CN_wfrmCampanas().ConsultaCampanas(this.session.Emp_Cnx, this.session.Id_Emp, this.session.Id_Cd_Ver, ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("wfrmCampana_delete_error"))
                    Alerta("Error al momento de eliminar la campaña");
                else
                    if (mensaje.Contains("wfrmCampana_delete_ok"))
                        Alerta("La campaña se eliminó correctamente");
                    else
                        Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }

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
            catch (Exception)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}
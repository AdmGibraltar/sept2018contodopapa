using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace SIANWEB
{
    public partial class CapGestionPrecios_Solicitud : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public Convenio convenio
        {
            get { return (Convenio)Session["Convenio" + Session.SessionID]; }
            set { Session["Convenio" + Session.SessionID] = value; }

        }
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
                    Response.Redirect("login.aspx", false);                  
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                       this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();

                        //CargarPermisosEspeciales();

                        double ancho = 0;
                        foreach (GridColumn gc in rg1.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rg1.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rg1.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rg1.Rebind();

                        //if (Page.Request.QueryString["Unq"].ToString() != "0") 
                        //{
                        //    string Sol_Unique = Page.Request.QueryString["Unq"].ToString();
                        //    Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                        //    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                        //    if (conv.Pue_Admin1 != sesion.Id_U && conv.Pue_Admin2 != sesion.Id_U)
                        //    {
                        //        Alerta("No tiene permisos para atender la solicitud");
                        //        return;
                        //    }

                        //    int TipoOp = 0;
                        //    CN_Convenio cn_conv = new CN_Convenio();
                        //    SolConvenio sol = new SolConvenio();
                        //    string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                        //    cn_conv.ConvenioSolicitud_ConsultaAt(Sol_Unique, ref sol, Conexion);

                        //    if (sol.Sol_Estatus == "C")
                        //    {
                        //        Alerta("Imposible atender la solicitud ya que se encuentra en estatus cancelado");
                        //        return;
                        //    }
                        //    else if (sol.Sol_Estatus == "A")
                        //    {
                        //        TipoOp = 1;
                        //    }

                     
                        //    RAM1.ResponseScripts.Add("return OpenWindowSolicitudAt('" + Sol_Unique + "', '" + TipoOp + "')");
 
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                //this.Nuevo();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rg1.DataSource = GetList();
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
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                int Id_Sol = 0;
                string Sol_Unique;

                switch (e.CommandName)
                {
                         
                    case "Editar":

                        if (this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Estatus"].Text == "Atendido")
                        {
                            Sol_Unique = this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Unique"].Text;
                            RAM1.ResponseScripts.Add("return OpenWindowSolicitudAt('" + Sol_Unique + "', '1')");
                        }
                        else
                        {
                            if (this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Estatus"].Text == "Cancelado")
                            {
                                Alerta("Imposible modificar la solicitud ya que se encuentra en estatus cancelado");
                                return;
                            }
                            Id_Sol = int.Parse(this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Id_Sol"].Text);
                            RAM1.ResponseScripts.Add("return OpenWindowSolicitud('" + Id_Sol + "')");
                        }
                     break;
                    case "Eliminar":

                     if (this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Estatus"].Text == "Cancelado")
                     {
                         Alerta("Imposible cancelar la solicitud ya que se encuentra en estatus cancelado");
                         return;

                     }
                     if (this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Estatus"].Text == "Atendido")
                     {
                         Alerta("Imposible cancelar la solicitud ya que se encuentra en estatus atendido");
                         return;
                     }

                      Cancelar(int.Parse(this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Id_Sol"].Text));
           
                     break;
                    case "Atender":

                     int TipoOp = 0;
                     if (this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Estatus"].Text == "Cancelado")
                     {
                         Alerta("Imposible atender la solicitud ya que se encuentra en estatus cancelado");
                         return;
                     }
                     if (this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Estatus"].Text == "Atendido")
                     {
                         TipoOp = 1; 
                     }

                      Sol_Unique = this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Unique"].Text;

                      RAM1.ResponseScripts.Add("return OpenWindowSolicitudAt('" + Sol_Unique + "', '" + TipoOp + "')");

                     break;
                    case "Enviar":

                     if (this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Estatus"].Text == "Cancelado")
                     {
                         Alerta("Imposible enviar la solicitud ya que se encuentra en estatus cancelado");
                         return;

                     }
                     if (this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Sol_Estatus"].Text == "Atendido")
                     {
                         Alerta("Imposible enviar la solicitud ya que se encuentra en estatus atendido");
                         return;

                     }

                     Id_Sol = int.Parse(this.rg1.MasterTableView.Items[e.Item.ItemIndex]["Id_Sol"].Text);
                     EnviarCorreo(Id_Sol);

                     break;
                }


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
                        rg1.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rg1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            if (e.Item is GridDataItem)
            {
                //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                //GridDataItem item = (GridDataItem)e.Item;
                //if (conv.Pue_Admin1 != sesion.Id_U && conv.Pue_Admin2 != sesion.Id_U)
                //{
                //    item["Atender"].Visible = false;
                //}
                //else
                //{
                //    item["Editar"].Visible = false;
                //    item["Enviar"].Visible = false;
                //    item["Eliminar"].Visible = false;
                //}

            }

            if (e.Item is GridHeaderItem)
            {
                //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                //GridHeaderItem item = (GridHeaderItem)e.Item;
                //if (conv.Pue_Admin1 != sesion.Id_U && conv.Pue_Admin2 != sesion.Id_U)
                //{
                //    item["Atender"].Visible = false;

                //}
                //else
                //{
                //    item["Editar"].Visible = false;
                //    item["Enviar"].Visible = false;
                //    item["Eliminar"].Visible = false;
                //}
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
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                //if (pag.Length > 1)
                //    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                //else
                //    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                pagina.Url = "CapGestionPrecios_Solicitud.aspx?Unq=0";

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
                        this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");


                //if (ConsultarAutorizacionPrecio() == "True")
                //{

                //    this.rg1.Columns[11].Visible = true ;
                //}

                //else
                //{
                //    this.rg1.Columns[11].Visible = false;
                
                //}
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

                this.lblCentro.Visible = false;
                this.Centro.Visible = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<SolConvenio> GetList()
        {
            try
            {
                List<SolConvenio> List = new List<SolConvenio>();
                CN_SolConvenio cn_conv = new CN_SolConvenio();
                SolConvenio conv = new SolConvenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio c = (Convenio)Session["Convenio" + Session.SessionID];

                conv.FiltroId_Sol = TxtId_Sol.Text == "" ? (int?)null : int.Parse(TxtId_Sol.Text);
                conv.FiltroPc_NoConvenio = TxtNo_Convenio.Text;
                conv.FiltroSol_Estatus = this.cmbEstatus.SelectedValue;

                //if (c.Pue_Admin1 == sesion.Id_U || c.Pue_Admin2 == sesion.Id_U)
                //{
                //    conv.FiltroId_CD = (int?)null;

                //}
                //else
                //{
                    conv.FiltroId_CD = sesion.Id_Cd_Ver;
                //}
                cn_conv.ProPrecioConv_SolicitudLista(conv, ref List, Conexion);

                return List;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Cancelar(int Id_Sol)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();
                int Verificador = 0;

                cn_conv.ConvenioSolicitud_Cancelar(Id_Sol, ref Verificador, Conexion);

                if (Verificador == -1)
                {

                    Alerta("Se canceló la solicitud de manera exitosa");
                    rg1.Rebind();
                }
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
        private void EnviarCorreo(int Id_Sol)
        {
            try
            {
               
                CN_Convenio cn_conv = new CN_Convenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
                int Verificador = 0;

                cn_conv.ConvenioSolicitud_EnviarCorreoCreoSol(Id_Sol, ref Verificador, Conexion);

                if (Verificador == -1)
                {
                    Alerta("Se ha enviado la solicitud por correo electrónico");
                }
                else
                {
                    Alerta("Ocurrio un error al tratar de envíar el correo, favor de intentarlo nuevamente");
                }
     

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void CargarPermisosEspeciales()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = new Convenio();
                CN_Convenio cn_conv = new CN_Convenio();

                cn_conv.ConsultaUsuariosEspeciales(ref conv, sesion.Emp_Cnx);

                convenio = conv;

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
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

namespace SIANWEB
{
    public partial class CapGestionPrecios_SolicitudAt : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private List<SolConvenioDet> ListDet
        {
            get { return (List<SolConvenioDet>)Session["ListDet" + Session.SessionID ]; }
            set { Session["ListDet" + Session.SessionID] = value; }
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
         
                        this.CargarCentros();
                        ListDet = null;
                        ListDet = new List<SolConvenioDet>();
                        HFSol_Unique.Value =  Page.Request.QueryString["Unq"].ToString();
                        HFTipoOp.Value = Page.Request.QueryString["TipoOp"].ToString();
                        ConsultaEncabezado();
                        ConsultaDetalle();

                        if (HFTipoOp.Value == "1")
                        {
                            rtb1.Items[1].Visible = false;
                        }

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
       
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
            
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                    if (btn.CommandName == "save")
                    {
                            Guardar();
                    }

                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgSolicitudDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                   
                    rgSolicitudDet.DataSource = ListDet;
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
                        rgSolicitudDet.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void chkAutorizarAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                for (int x = 0; x < rgSolicitudDet.Items.Count; x++)
                {
                    if (( this.rgSolicitudDet.Items[x]["Autorizar"].FindControl("chkAutorizar") as CheckBox).Enabled)
                    {
                        (rgSolicitudDet.Items[x]["Autorizar"].FindControl("chkRechazar") as CheckBox).Checked = false;
                        (rgSolicitudDet.Items[x]["Rechazado"].FindControl("chkAutorizar") as CheckBox).Checked = true;
                      
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkRechazarAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                for (int x = 0; x < this.rgSolicitudDet.Items.Count; x++)
                {
                    if ((rgSolicitudDet.Items[x]["Rechazado"].FindControl("chkRechazar") as CheckBox).Enabled)
                    {
                        (rgSolicitudDet.Items[x]["Rechazado"].FindControl("chkRechazar") as CheckBox).Checked = true;
                        (rgSolicitudDet.Items[x]["Autorizar"].FindControl("chkAutorizar") as CheckBox).Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgSolicitudDet_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (HFTipoOp.Value == "0")
                {
                    item["Cancelado"].Visible = false;
                }
                else
                {
                    RadioButton rbA;
                    RadioButton rbR;
                    RadioButton rbC;

                    rbA = ((RadioButton)item.FindControl("chkAutorizar"));
                    rbR = ((RadioButton)item.FindControl("chkRechazar"));
                    rbC = ((RadioButton)item.FindControl("chkCancelado"));
                    rbA.Enabled = false;
                    rbR.Enabled = false;
                    rbC.Enabled = false;

                }
            }

            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem item = (GridHeaderItem)e.Item;
                if (HFTipoOp.Value == "0")
                {
                    item["Cancelado"].Visible = false;
                }
                else
                {
                    RadioButton rbA;
                    RadioButton rbR;
                    RadioButton rbC;

                    rbA = ((RadioButton)item.FindControl("chkAutorizarAll"));
                    rbR = ((RadioButton)item.FindControl("chkRechazarAll"));
                    rbC = ((RadioButton)item.FindControl("chkCanceladoAll"));
                    rbA.Enabled = false;
                    rbR.Enabled = false;
                    rbC.Enabled = false;

                }
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ConsultaEncabezado()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();
                SolConvenio sol = new SolConvenio();

                cn_conv.ConvenioSolicitud_ConsultaAt(HFSol_Unique.Value, ref sol, Conexion);

                LblId_Sol.Text = sol.Id_Sol.ToString();
                LblCd_Nombre.Text = sol.CD_Nombre;
                LblU_Nombre.Text = sol.Sol_UNombre;
                LblU_Correo.Text = sol.Sol_UCorreo;
                LblSol_Fecha.Text = sol.Sol_Fecha.ToShortDateString();
                LblPC_NoConvenio.Text = sol.PC_NoConvenio;
                LblPC_Nombre.Text = sol.PC_Nombre;
                LblId_CatStr.Text = sol.Cat_DescCorta;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void ConsultaDetalle()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();
                List<SolConvenioDet> List = new List<SolConvenioDet>();

                cn_conv.ConvenioSolicitud_ConsultaDetAt(int.Parse(this.LblId_Sol.Text), ref List, Conexion);

                ListDet = List;
                rgSolicitudDet.Rebind();

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
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                string Conexion2 = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
                Sesion sesion = (Sesion) Session["Sesion" + Session.SessionID ];
                List<SolConvenioDet> List = new List<SolConvenioDet>();
                SolConvenioDet s;
                foreach (GridDataItem item in this.rgSolicitudDet.MasterTableView.Items)
                {
                    s = new SolConvenioDet();
                    s.Id_Sol = int.Parse(LblId_Sol.Text);
                    s.Id_Cte = int.Parse(item["Id_Cte"].Text);
                    s.Sol_CteNombre = item["Sol_CteNombre"].Text;
                    if ((item["Rechazado"].FindControl("chkRechazar") as CheckBox).Checked == true)
                    {
                        s.SolD_Estatus = "R";
                    }
                    else if ((item["Autorizar"].FindControl("chkAutorizar") as CheckBox).Checked == true)
                    {
                        s.SolD_Estatus = "A";
                    }
                    else
                    {
                        Alerta("Debe seleccionar una opcion para el cliente <b>" + s.Id_Cte.ToString() + "-" + s.Sol_CteNombre + "</b>");
                        return;
                    }


                    List.Add(s);
                }
                int Id_Sol = int.Parse(LblId_Sol.Text);
                int Verificador = 0;
                CN_Convenio cn_conv = new CN_Convenio();

                cn_conv.ConvenioSolicitud_Atender(Id_Sol, List, ref Verificador, Conexion);

                if (Verificador == -1)
                {

                    cn_conv.ConvenioSolicitud_EnviarCorreoAtendio(Id_Sol, ref Verificador, Conexion2);
                    //string script = "<script>RefreshParentPage()</" + "script>";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "RefreshParentPage", script, false); 
                    AlertaCerrar("Se ha atendido la solicitud de manera exitosa");
                }
                else
                {
                    Alerta("Alerta al tratar de atender la solicitud");
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
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
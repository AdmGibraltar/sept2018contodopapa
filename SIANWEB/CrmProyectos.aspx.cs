using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Data;

namespace SIANWEB
{
    public partial class CrmProyectos : System.Web.UI.Page
    {
        #region Variables
        //private static bool _PermisoGuardar;
        //private static bool _PermisoModificar;
        //private static bool _PermisoEliminar;
        //private static bool _PermisoImprimir;
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
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (session.Cu_Modif_Pass_Voluntario == false)
                            return;
                        Inicializar();
                    }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlUEN_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int segmento = 0;
            int area = 0;
            int solucion = 0;
            int aplicacion = 0;
            int cds = session.Id_Cd;
            int uen = !string.IsNullOrEmpty(ddlUEN.SelectedValue) ? Convert.ToInt32(ddlUEN.SelectedValue) : 0;
            CargarSegmentos(uen, ref segmento);
            ddlSegmento.SelectedValue = segmento.ToString();
            //filtro2
            CargarAreas(segmento, ref area);
            ddlArea.SelectedValue = area.ToString();
            CargarSolucion(area, ref solucion);
            ddlSolucion.SelectedValue = solucion.ToString();
            CargarAplicacion(solucion, ref aplicacion);
            ddlAplicacion.SelectedValue = aplicacion.ToString();
        }
        protected void ddlSegmento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
            int area = 0;
            int solucion = 0;
            int aplicacion = 0;
            //filtro2
            CargarAreas(segmento, ref area);
            ddlArea.SelectedValue = area.ToString();
            CargarSolucion(area, ref solucion);
            ddlSolucion.SelectedValue = solucion.ToString();
            CargarAplicacion(solucion, ref aplicacion);
            ddlAplicacion.SelectedValue = aplicacion.ToString();
        }
        protected void ddlTerritorio_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
        }
        protected void ddlArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
            int solucion = 0;
            int aplicacion = 0;
            //filtro2        
            CargarSolucion(area, ref solucion);
            ddlSolucion.SelectedValue = solucion.ToString();
            CargarAplicacion(solucion, ref aplicacion);
            ddlAplicacion.SelectedValue = aplicacion.ToString();
        }
        protected void ddlSolucion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int solucion = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : 0;
            int aplicacion = 0;
            //filtro2  
            CargarAplicacion(solucion, ref aplicacion);
            ddlAplicacion.SelectedValue = aplicacion.ToString();
        }
        protected void ddlAplicacion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
        }
        protected void ibtnGuardar_Click(object sender, EventArgs e)
        {
            int error = 0;
            CRMRegistroProyectos registros = new CRMRegistroProyectos();
            registros.Uen = Convert.ToInt32(ddlUEN.SelectedValue);
            registros.Segmento = Convert.ToInt32(ddlSegmento.SelectedValue);
            registros.Territorio = Convert.ToInt32(ddlTerritorio.SelectedValue);
            registros.Area = Convert.ToInt32(ddlArea.SelectedValue);
            registros.Solucion = Convert.ToInt32(ddlSolucion.SelectedValue);
            registros.Aplicacion = Convert.ToInt32(ddlAplicacion.SelectedValue); 
            if (registros.Uen <= 0)
                error = 1;
            if (registros.Segmento <= 0)
                error = 1;
            if (registros.Territorio <= 0)
                error = 1;
            if (registros.Area <= 0)
                error = 1;
            if (registros.Solucion <= 0)
                error = 1;
            if (registros.Aplicacion <= 0)
                error = 1;
            registros.Cliente = !string.IsNullOrEmpty(txtNoCliente.Text) ? Convert.ToInt32(txtNoCliente.Text) : 0;
            registros.VentaNoRepetitiva = chkNoRepetitiva.Checked;
            registros.ValorPotencialT = !string.IsNullOrEmpty(txtVPTeorico.Text) ? Convert.ToInt32(txtVPTeorico.Text) : 0;
            registros.ValorPotencialO = !string.IsNullOrEmpty(txtVPObservado.Text) ? Convert.ToInt32(txtVPObservado.Text) : 0;
            registros.Comentarios = txtComentarios.Text;
            registros.Productos = txtProductos.Text;
            if (chkAnalisis.Checked)
                registros.Analisis = Convert.ToDateTime(lblAnalisis.Text);
            if (chkPresentacion.Checked)
                registros.Presentacion = Convert.ToDateTime(lblPresentacion.Text);
            if (chkNegociacion.Checked)
                registros.Negociacion = Convert.ToDateTime(lblNegociacion.Text);
            if (chkCierre.Checked)
                registros.Cancelacion = Convert.ToDateTime(lblCierre.Text);
            registros.FechaCotizacion = txtCotizacion.SelectedDate.Value;
            registros.VentaPromedio = Convert.ToInt32(txtVentaMensual.Text);


            Response.Redirect("wfrmPrincipalOportunidades.aspx");
        }
        protected void ibtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrmPrincipalOportunidades.aspx");
        }
        #endregion
        #region funciones
        private void ValidarPermisos()
        {
            //try
            //{
            //    Pagina pagina = new Pagina();
            //    string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
            //    if (pag.Length > 1)               
            //        pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];               
            //    else                
            //        pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;               

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
            //    }
            //    else                
            //        Response.Redirect("Inicio.aspx");               
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                   
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
        private void Inicializar()
        {
            try
            {
                int cds = session.Id_Cd;    
                int uen = 0;
                int segmento = 0;
                int area = 0;
                int solucion = 0;
                int aplicacion = 0;
                //hidden y checkbox
                HiddenField1.Value = cds.ToString();
                lblAnalisis.Text = DateTime.Now.ToShortDateString();
                chkPresentacion.Enabled = true;
                chkNegociacion.Enabled = false;
                chkCierre.Enabled = false;
                //filtro1              
                CargarUEN(ref uen);
                ddlUEN.SelectedValue = uen.ToString();
                CargarSegmentos(uen, ref segmento);
                ddlSegmento.SelectedValue = segmento.ToString();
                CargarTerritorio(cds);
                //filtro2
                CargarAreas(segmento, ref area);
                ddlArea.SelectedValue = area.ToString();
                CargarSolucion(area, ref solucion);
                ddlSolucion.SelectedValue = solucion.ToString();
                CargarAplicacion(solucion, ref aplicacion);
                ddlAplicacion.SelectedValue = aplicacion.ToString();               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUEN(ref int valorUEN)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmPromociones> list = new List<CrmPromociones>();
                CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                clscrmCat.ComboUen(sesion, ref list);
                ddlUEN.Items.Clear();
                if (list.Count > 0)
                {
                    valorUEN = list[0].Id;
                    for (int i = 0; i < list.Count; i++)
                        ddlUEN.Items.Add(new RadComboBoxItem(list[i].Descripcion, list[i].Id.ToString()));
                    ddlUEN.SelectedValue = valorUEN.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSegmentos(int UEN, ref int valorSegmento)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmPromociones> list = new List<CrmPromociones>();
                CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                clscrmCat.ComboSegmento(sesion, UEN, ref list);
                ddlSegmento.Items.Clear();
                if (list.Count > 0)
                {
                    valorSegmento = list[0].Id;
                    for (int i = 0; i < list.Count; i++)
                        ddlSegmento.Items.Add(new RadComboBoxItem(list[i].Descripcion, list[i].Id.ToString()));
                    ddlSegmento.SelectedValue = valorSegmento.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorio(int CDS)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, CDS, Sesion.Emp_Cnx, "spCatTerritorio_Combo", ref ddlTerritorio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAreas(int segmento, ref int area)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmPromociones> list = new List<CrmPromociones>();
                CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                clscrmCat.ComboArea(sesion, segmento, ref list);
                ddlArea.Items.Clear();
                if (list.Count > 0)
                {
                    area = list[0].Id;
                    for (int i = 0; i < list.Count; i++)
                        ddlArea.Items.Add(new RadComboBoxItem(list[i].Descripcion, list[i].Id.ToString()));
                    ddlArea.SelectedValue = area.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSolucion(int area, ref int solucion)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmPromociones> list = new List<CrmPromociones>();
                CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                clscrmCat.CargarSolucion(sesion, area, ref list);
                ddlSolucion.Items.Clear();
                if (list.Count > 0)
                {
                    solucion = list[0].Id;
                    for (int i = 0; i < list.Count; i++)
                        ddlSolucion.Items.Add(new RadComboBoxItem(list[i].Descripcion, list[i].Id.ToString()));
                    ddlSolucion.SelectedValue = solucion.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAplicacion(int solucion, ref int aplicacion)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmPromociones> list = new List<CrmPromociones>();
                CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                clscrmCat.ConsultaAplicacion(sesion, solucion, ref list);
                ddlAplicacion.Items.Clear();
                if (list.Count > 0)
                {
                    aplicacion = list[0].Id;
                    for (int i = 0; i < list.Count; i++)
                        ddlAplicacion.Items.Add(new RadComboBoxItem(list[i].Descripcion, list[i].Id.ToString()));
                    ddlAplicacion.SelectedValue = aplicacion.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DeshabilitarDatosOportunidad()
        {//Procedimiento que deshabilita la información general de la oportunidad, para
         //que al darle seguimiento no se vaya a modificar algun valor establecido en su captura.
            ddlUEN.Enabled = false;
            ddlSegmento.Enabled = false;
            ddlTerritorio.Enabled = false;
            ddlArea.Enabled = false;
            ddlSolucion.Enabled = false;
            ddlAplicacion.Enabled = false;
            ibtnBuscarCliente.Enabled = false;
            txtCotizacion.Enabled = false;
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapDevParcial", "Id_Dev", sesion.Emp_Cnx, "spCatLocal_Maximo");
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

        protected void chkPresentacion_CheckedChanged(object sender, EventArgs e)
        {
            DeshabilitarDatosOportunidad();
            lblPresentacion.Text = DateTime.Now.ToShortDateString();
            chkNegociacion.Enabled = true;
            chkPresentacion.Enabled = false;
        }

        protected void chkNegociacion_CheckedChanged(object sender, EventArgs e)
        {
            DeshabilitarDatosOportunidad();
            lblNegociacion.Text = DateTime.Now.ToShortDateString();
            txtVentaMensual.ReadOnly = false;
            chkNegociacion.Enabled = false;
            chkCierre.Enabled = true;
            lblMensaje.Text = "";
        }

        protected void chkCierre_CheckedChanged(object sender, EventArgs e)
        {
            DeshabilitarDatosOportunidad();
            lblCierre.Text = DateTime.Now.ToShortDateString();
            txtVentaMensual.ReadOnly = false;
            chkCierre.Enabled = false;
            lblMensaje.Text = "";
            lblVenta.Visible = true;
            txtVentaMensual.Visible = true;
            txtVentaMensual.Text = txtVPObservado.Text;
        }
    }
}
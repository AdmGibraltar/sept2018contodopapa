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
    public partial class wfrmSeguimientoOportunidad : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public int totAplicaciones
        {
            get { return DataGrid1.Items.Count; }
        }
        
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
        public int idOportunidad = 0;
        public int idCD = 0;
        #endregion

        #region Eventos 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
               
                if (ValidarSesion())
                    if (!Page.IsPostBack)
                    {
                        if (session.Cu_Modif_Pass_Voluntario == false)
                            return;
                        ValidarPermisos();
                        Inicializar();
                        
                    }
               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item is DataGridItem)
                {
                    DataGridItem item = (DataGridItem)e.Item;
                    CheckBox cb = default(CheckBox);
                    RadNumericTextBox tx = default(RadNumericTextBox);

                    if (item.Cells[6].FindControl("chk1") != null && item.Cells[6].FindControl("txt1") != null)
                    {
                        cb = (CheckBox)item.Cells[6].FindControl("chk1");
                        tx = (RadNumericTextBox)item.Cells[6].FindControl("txt1");
                        cb.Attributes.Add("onClick", "checkedchanged('" + tx.ClientID + "', this);");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlUEN_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
        protected void ddlSegmento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                int segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                int area = 0;
                int solucion = 0;
                int aplicacion = 0;
                //filtro2
                CargarAreas(segmento, area);
                //ddlArea.SelectedValue = area.ToString();
                CargarSolucion(area, solucion);
                //ddlSolucion.SelectedValue = solucion.ToString();
                CargarAplicacion(solucion, aplicacion);
                //ddlAplicacion.SelectedValue = aplicacion.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlTerritorio_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
        }
        protected void ddlArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                int area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
                int solucion = 0;
                int aplicacion = 0;
                //filtro2        
                CargarSolucion(area,   solucion);
                //ddlSolucion.SelectedValue = solucion.ToString();
                CargarAplicacion(solucion,   aplicacion);
                //ddlAplicacion.SelectedValue = aplicacion.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlSolucion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                int solucion = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : 0;
                int aplicacion = 0;
                //filtro2  
                CargarAplicacion(solucion,   aplicacion);
                //ddlAplicacion.SelectedValue = aplicacion.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlAplicacion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                int cliente = !string.IsNullOrEmpty(HiddenField6.Value) ? Convert.ToInt32(HiddenField6.Value) : 0;
                int segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                idOportunidad = parametros();
                CargarGrid(cliente, segmento, idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ibtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool visualizar = true;
                ibtnGuardar.Visible = false;
                ibtnGuardar.Visible = false;
                if (_PermisoModificar)
                {

                    if (procesoGuardado() == 0)
                    {
                        visualizar = false;
                        Response.Redirect("wfrmPrincipalOportunidades.aspx", false);
                    }
                }
                else               
                    Alerta("No tiene permisos para modificar");

                if (visualizar)
                {
                    ibtnGuardar.Visible = true;
                    ibtnGuardar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ibtnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Response.Redirect("wfrmPrincipalOportunidades.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void chkPresentacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPresentacion.Checked)
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    ibtnGuardar.Visible = false;
                    ibtnRegresar.Visible = false;
                    string funcion = "AbrirVentanaAutorizar(1)";
                    string script = "<script>" + funcion + "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                }
                else
                    chkPresentacion.Checked = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void chkNegociacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkNegociacion.Checked)
                {
                    if ( !_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    ibtnGuardar.Visible = false;
                    ibtnRegresar.Visible = false;
                    string funcion = "AbrirVentanaAutorizar(2)";
                    string script = "<script>" + funcion + "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                    txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;                     
                }
                else
                    chkNegociacion.Checked = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void chkCierre_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCierre.Checked)
                {
                    if ( !_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    ibtnGuardar.Visible = false;
                    ibtnRegresar.Visible = false;
                    string funcion = "AbrirVentanaAutorizar(3)";
                    string script = "<script>" + funcion + "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                    txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;
                     
                }
                else
                    chkCierre.Checked = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void chkCancelacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCancelacion.Checked)
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    ibtnGuardar.Visible = false;
                    ibtnRegresar.Visible = false;
                    string funcion = "AbrirVentana_Cancelar(4)";
                    string script = "<script>" + funcion + "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                    txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlCausa2_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                pnlCancela.Visible = true;
                CargarTipos();
                lblCancelacion.Text = DateTime.Now.ToShortDateString();
                if (Session["NumCausa"] != null)
                    if (!string.IsNullOrEmpty(Session["NumCausa"].ToString()))
                        ddlCausa2.SelectedValue = Session["NumCausa"].ToString();
                if (Session["NumCancela"] != null)
                    if (!string.IsNullOrEmpty(Session["NumCancela"].ToString()))
                        txtCancela.Text = Session["NumCancela"].ToString();
                if (Session["NumCompetencia"] != null)
                    if (!string.IsNullOrEmpty(Session["NumCompetencia"].ToString()))
                        txtCompetencia.Text = Session["NumCompetencia"].ToString();
                txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                ibtnGuardar.Visible = false;
                ibtnRegresar.Visible = false;
                //string funcion;
                //string script;
                switch (cmd)
                {
                    //case "P":
                    //    funcion = "AbrirVentanaAutorizar(1)";
                    //    script = "<script>" + funcion + "</script>";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                    //    break;
                    //case "N":
                    //    funcion = "AbrirVentanaAutorizar(2)";
                    //    script = "<script>" + funcion + "</script>";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                    //    txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;
                    //    break;
                    //case "C":
                    //    funcion = "AbrirVentanaAutorizar(3)";
                    //    script = "<script>" + funcion + "</script>";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                    //    txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;
                    //    break;
                    //case "Ca":
                    //    funcion = "AbrirVentana_Cancelar(4)";
                    //    script = "<script>" + funcion + "</script>";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                    //    txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;
                    //    break;
                    case "opcionA":
                        //Activacion de Check de Cancelacion
                        ibtnGuardar.Visible = false;
                        ibtnRegresar.Visible = false;
                        pnlCancela.Visible = true;
                        CargarTipos();
                        chkCancelacion.Checked = true;
                        chkCancelacion.Enabled = false;
                        chkCierre.Enabled = false;
                        chkNegociacion.Enabled = false;
                        chkPresentacion.Enabled = false;
                        txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;
                        HiddenField5.Value = "5";
                        lblCancelacion.Text = DateTime.Now.ToShortDateString();
                        if (Session["NumCausa"] != null)
                            if (!string.IsNullOrEmpty(Session["NumCausa"].ToString()))
                                ddlCausa2.SelectedValue = Session["NumCausa"].ToString();
                        if (Session["NumCancela"] != null)
                            if (!string.IsNullOrEmpty(Session["NumCancela"].ToString()))
                                txtCancela.Text = Session["NumCancela"].ToString();
                        if (Session["NumCompetencia"] != null)
                            if (!string.IsNullOrEmpty(Session["NumCompetencia"].ToString()))
                                txtCompetencia.Text = Session["NumCompetencia"].ToString();
                        txtComentarios.ReadOnly = true;
                        txtProductos.ReadOnly = true;

                        if (_PermisoModificar)
                        {
                            if (procesoGuardado() == 0)
                            {
                                Alerta("Redireccionando, espere por favor");
                                Response.Redirect("wfrmPrincipalOportunidades.aspx", false);
                            }
                            else
                            {
                                ibtnGuardar.Visible = true;
                                ibtnRegresar.Visible = true;
                            }
                        }
                        else
                            Alerta("No tiene permiso para modificar el proyecto");
                        break;
                    case "opcionB":
                        //Validaciones para Presentacion, Negociacion y Cierre
                        int tipo = 0;
                        int valido = -1;
                        if (Session["NumValido"] != null)
                            if (!string.IsNullOrEmpty(Session["NumValido"].ToString()))
                                int.TryParse(Session["NumValido"].ToString(), out valido);
                        if (valido > -1)
                            if (Session["NumTipo"] != null)
                                if (!string.IsNullOrEmpty(Session["NumTipo"].ToString()))
                                {
                                    int.TryParse(Session["NumTipo"].ToString(), out tipo);
                                    avances(tipo, valido);
                                }
                        txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;
                        if (valido > 0)
                        {
                            if (_PermisoModificar)
                            {
                                if (procesoGuardado() == 0)
                                {
                                    Alerta("Redireccionando, espere por favor");
                                    Response.Redirect("wfrmPrincipalOportunidades.aspx", false);
                                }
                                else
                                {
                                    ibtnGuardar.Visible = true;
                                    ibtnRegresar.Visible = true;
                                }
                            }
                            else
                                Alerta("No tiene permiso para modificar el proyecto");
                        }
                        else
                        {
                            ibtnGuardar.Visible = true;
                            ibtnRegresar.Visible = true;
                        }
                        break;
                    case "opcionC":
                        //    //Cancelacion de Check de Cancelacion
                        chkCancelacion.Checked = false;
                        ibtnGuardar.Visible = true;
                        ibtnGuardar.Enabled = true;
                        ibtnRegresar.Visible = true;
                        ibtnRegresar.Enabled = true;
                        //    chkCancelacion.Enabled = true;
                        //    chkCierre.Enabled = false;
                        //    chkNegociacion.Enabled = false;
                        //    chkPresentacion.Enabled = false;
                        //    lblCancelacion.Text = "";
                        //    txtVPObservado.Value = !string.IsNullOrEmpty(txtVPObservado.Value.ToString()) ? txtVPObservado.Value.Value : 0.00;
                        //    pnlCancela.Visible = false;
                        //    txtComentarios.ReadOnly = false;
                        //    txtProductos.ReadOnly = false;
                        break;
                }
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
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
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
                parametros3();
                int cds = idCD;
                if (cds == 0)
                    cds = session.Id_Cd_Ver;
                int segmento = 0;
               // int area = 0;
               // int solucion = 0;
               // int aplicacion = 0;
                idOportunidad = parametros();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<CrmOportunidades> list = new List<CrmOportunidades>();
                CN_CrmOportunidad clscrmCat = new CN_CrmOportunidad();
                CN_CatCampanas clscrmCam = new CN_CatCampanas();


                idOportunidad = (idOportunidad == null) ? 0 : idOportunidad;

                if (idOportunidad <= 0)
                {
                    Response.Redirect("wfrmPrincipalOportunidades.aspx");
                            return;
                }
               
                if (idOportunidad > 0)
                  clscrmCat.ConsultaOportunidad(sesion, cds, idOportunidad, ref list);
                //filtro1        
                CargarUEN(list[0].Id_Uen);
                CargarSegmentos(ref segmento);
                ddlSegmento.SelectedValue = list[0].Id_Seg.ToString();
                HiddenField2.Value = list[0].Id_Emp.ToString();
                HiddenField3.Value = list[0].Id_Cd.ToString();
                HiddenField6.Value = list[0].Id_Cte.ToString();
                CargarTerritorio(list[0].Id_Cd);
                ddlTerritorio.SelectedValue = list[0].Id_Ter.ToString();
                //filtro2
                CargarAreas(list[0].Id_Seg, list[0].ID_Area);
                //ddlArea.SelectedValue = list[0].ID_Area.ToString();
                CargarSolucion(list[0].ID_Area,  list[0].Id_Sol);
                //ddlSolucion.SelectedValue = list[0].Id_Sol.ToString();
                CargarAplicacion(list[0].Id_Sol, list[0].Id_Apl);
                //ddlAplicacion.SelectedValue = list[0].Id_Apl.ToString();

                HiddenField1.Value = cds.ToString();
                txtNoCliente.Text = list[0].Descripcion.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[1];
                chkNoRepetitiva.Checked = list[0].VentaNoRepetitiva;
                //txtVPTeorico.Text = list[0].MontoProyecto.ToString();
                txtVPTeorico.Value = list[0].ValorPotencialT;
                txtVPObservado.Value = list[0].MontoProyecto;
                //txtVPObservado.Text = list[0].MontoProyecto.ToString();
                txtComentarios.Text = list[0].Comentarios;
                txtProductos.Text = list[0].Productos;
                txtVentaMensual.Value = list[0].VentaMensual;
                HiddenField4.Value = list[0].Estatus.ToString();
                HiddenField5.Value = list[0].Estatus.ToString();
                if (!string.IsNullOrEmpty(list[0].FechaCotizacion))
                    txtCotizacion.SelectedDate = Convert.ToDateTime(list[0].FechaCotizacion.ToString());
                if (!string.IsNullOrEmpty(list[0].Analisis))
                {//analisis
                    chkAnalisis.Checked = true;
                    lblAnalisis.Text = list[0].Analisis;
                    chkAnalisis.Enabled = false;
                }
                if (!string.IsNullOrEmpty(list[0].Presentacion))
                {//presentacion
                    chkPresentacion.Checked = true;
                    lblPresentacion.Text = list[0].Presentacion;
                    chkPresentacion.Enabled = false;
                }
                if (!string.IsNullOrEmpty(list[0].Negociacion))
                {//negociacion
                    chkNegociacion.Checked = true;
                    lblNegociacion.Text = list[0].Negociacion;
                    chkNegociacion.Enabled = false;
                }
                if (!string.IsNullOrEmpty(list[0].Cierre))
                {//cierre
                    chkCierre.Checked = true;
                    lblCierre.Text = list[0].Cierre;
                    chkCierre.Enabled = false;
                    ibtnGuardar.Visible = false;
                }
                if (!string.IsNullOrEmpty(list[0].FechaCancelacion))
                {//cancelacion
                    chkAnalisis.Enabled = true;
                    chkPresentacion.Enabled = true;
                    chkNegociacion.Enabled = true;
                    chkCierre.Enabled = true;
                    chkCancelacion.Checked = true;
                    lblCancelacion.Text = list[0].FechaCancelacion;
                    chkCancelacion.Enabled = false;
                    pnlCancela.Visible = true;
                    CargarTipos();
                    ddlCausa2.SelectedValue = list[0].Id_Causa.ToString();
                    txtCompetencia.Text = list[0].Competidor;
                    txtCancela.Text = list[0].Cancelacion;
                    ibtnGuardar.Visible = false;
                }

                txtCampania.Text = list[0].Campania;
                HiddenCampaniaId.Value = list[0].Id_Cam.ToString();
                CheckPerteneceCampania.Checked = list[0].Id_Cam == 0 || string.IsNullOrEmpty(list[0].Id_Cam.ToString()) ? false : true;
                
                Campanas Campana = new Campanas();
                Campana.Id_Emp = sesion.Id_Emp;
                Campana.Id_Cd = session.Id_Cd;
                Campana.Id_Uen = list[0].Id_Uen;
                Campana.Id_Seg =  list[0].Id_Seg;
                Campana.Id_Area =  list[0].ID_Area;
                Campana.Id_Sol = list[0].Id_Sol;
                Campana.Id_Aplicacion = list[0].Id_Apl;
                Campana.Cam_Aplicacion = ddlAplicacion.Text;

                if (list[0].Id_Cam == 0 || string.IsNullOrEmpty(list[0].Id_Cam.ToString()))
                {

                    clscrmCam.ConsultaCampanaOportunidad(ref Campana, sesion.Emp_Cnx);
                    HiddenCampania.Value = Campana.Cam_Nombre;
                    HiddenCampaniaId.Value = Campana.Id_Cam.ToString();


                    if (Campana.Id_Cam > 0 )
                    {
                        RadConfirm("Existe una Campaña \"" + Campana.Id_Cam + " - " + Campana.Cam_Nombre + "\" con Vigencia del " + Campana.Cam_FechaInicio.ToShortDateString() + " al " + Campana.Cam_FechaFin.ToShortDateString() + " Para esta Aplicación, ¿Desea relacionar este proyecto a la campaña? ");
                    }
                }

                int hiden = Convert.ToInt32(HiddenField4.Value);
                switch (hiden)
                {
                    case 1:
                        chkAnalisis.Enabled = false;
                        chkPresentacion.Enabled = true;
                        //chkNegociacion.Enabled = false;
                        //chkCierre.Enabled = false;
                        chkCancelacion.Enabled = true;
                        pnlCancela.Visible = false;
                        break;
                    case 2:
                        chkAnalisis.Enabled = false;
                        chkPresentacion.Enabled = false;
                        chkNegociacion.Enabled = true;
                        //chkCierre.Enabled = false;
                        chkCancelacion.Enabled = true;
                        pnlCancela.Visible = false;
                        break;
                    case 3:
                        chkAnalisis.Enabled = false;
                        chkPresentacion.Enabled = false;
                        chkNegociacion.Enabled = false;
                        chkCierre.Enabled = true;
                        chkCancelacion.Enabled = true;
                        pnlCancela.Visible = false;
                        break;
                    case 4:
                        chkAnalisis.Enabled = false;
                        chkPresentacion.Enabled = false;
                        chkNegociacion.Enabled = false;
                        txtCotizacion.Enabled = false;
                        chkCierre.Enabled = false;
                        chkCancelacion.Enabled = true;
                        chkCancelacion.Checked = false;
                        pnlCancela.Visible = false;
                        break;
                    case 5:
                        txtVPTeorico.Enabled = false;
                        txtVPObservado.Enabled = false;
                        txtComentarios.Enabled = false;
                        txtProductos.Enabled = false;
                        txtCotizacion.Enabled = false;
                        txtVentaMensual.Enabled = false;
                        chkAnalisis.Enabled = false;
                        chkPresentacion.Enabled = false;
                        chkNegociacion.Enabled = false;
                        chkCierre.Enabled = false;
                        chkCancelacion.Enabled = false;
                        ibtnGuardar.Visible = false;
                        break;
                }
                ddlUEN.Enabled = false;
                //ddlSegmento.Enabled = false;
                ddlTerritorio.Enabled = false;
                //ddlArea.Enabled = false;
                //ddlSolucion.Enabled = false;
                //if (ddlAplicacion.SelectedValue != "-1")
                //    ddlAplicacion.Enabled = false;
                txtNoCliente.Enabled = false;
                
                //CargarGrid(Convert.ToInt32(list[0].Descripcion.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0]), list[0].Id_Seg, list[0].Id_Op);

                if (sesion.Id_TU != 2)
                {
                    chkNoRepetitiva.Enabled = false;
                    txtVPTeorico.Enabled = false;
                    txtVPObservado.Enabled = false;
                    txtComentarios.Enabled = false;
                    txtProductos.Enabled = false;
                    txtCotizacion.Enabled = false;
                    txtVentaMensual.Enabled = false;
                    ibtnGuardar.Visible = false;
                    chkAnalisis.Enabled = false;
                    chkCancelacion.Enabled = false;
                    chkCierre.Enabled = false;
                    chkNegociacion.Enabled = false;
                    chkPresentacion.Enabled = false;                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int procesoGuardado()
        {
            try
            {
                if (tableGridDatos.Visible)
                {
                    double teorico = 0;
                    double observado = 0;

                    for (int i = 0; i < DataGrid1.Items.Count; i++)
                        if ((DataGrid1.Items[i].Cells[6].FindControl("chk1") as CheckBox).Checked)
                        {
                            observado += (DataGrid1.Items[i].Cells[6].FindControl("txt1") as RadNumericTextBox).Value.HasValue ? (DataGrid1.Items[i].Cells[6].FindControl("txt1") as RadNumericTextBox).Value.Value : 0;
                            teorico += Convert.ToDouble((DataGrid1.Items[i].Cells[5].FindControl("txt2") as RadNumericTextBox).Value);
                        }
                    txtVPTeorico.Text = teorico.ToString();
                    txtVPObservado.Value = observado;
                }

                int validador = 0;
                int error = 0;
                lblMensaje.Text = "";
                CrmOportunidades registros = new CrmOportunidades();
                registros.Id_Op = parametros();
                registros.Id_Emp = Convert.ToInt32(HiddenField2.Value);
                registros.Id_Cd = Convert.ToInt32(HiddenField3.Value);
                registros.Id_Seg = Convert.ToInt32(ddlSegmento.SelectedValue);
                registros.Id_Ter = Convert.ToInt32(ddlTerritorio.SelectedValue);
                registros.ID_Area = Convert.ToInt32(ddlArea.SelectedValue);
                registros.Id_Sol = Convert.ToInt32(ddlSolucion.SelectedValue);
                registros.Id_Apl = Convert.ToInt32(ddlAplicacion.SelectedValue);
                registros.Id_Cam = Convert.ToInt32(!string.IsNullOrEmpty(HiddenCampaniaId.Value) ? Convert.ToInt32(HiddenCampaniaId.Value) : 0);
                registros.VentaNoRepetitiva = chkNoRepetitiva.Checked;
                registros.MontoProyecto = txtVPObservado.Value == null ? 0 : txtVPObservado.Value.Value;
                if (registros.MontoProyecto == 0)
                {
                    error = 1;
                    Alerta("Ingrese un valor potencial observado");
                }
                registros.Comentarios = txtComentarios.Text;
                registros.Productos = txtProductos.Text;
                int valorHidden = !string.IsNullOrEmpty(HiddenField4.Value) ? Convert.ToInt32(HiddenField4.Value) : 1;
                int valorHidden2 = !string.IsNullOrEmpty(HiddenField5.Value) ? Convert.ToInt32(HiddenField5.Value) : 1;
                switch (valorHidden2)
                {
                    case 1://analisis
                        registros.Analisis = lblAnalisis.Text;
                        registros.Estatus = valorHidden2;
                        break;
                    case 2://presentacion
                        registros.Analisis = lblAnalisis.Text;
                        registros.Presentacion = lblPresentacion.Text;
                        if (string.IsNullOrEmpty(registros.Presentacion))
                            registros.Presentacion = DateTime.Now.ToShortDateString();
                        registros.Estatus = valorHidden2;
                        break;
                    case 3://negociacion
                        registros.Analisis = lblAnalisis.Text;
                        registros.Presentacion = lblPresentacion.Text;
                        if (string.IsNullOrEmpty(registros.Presentacion))
                            registros.Presentacion = DateTime.Now.ToShortDateString();
                        registros.Negociacion = lblNegociacion.Text;
                        if (string.IsNullOrEmpty(registros.Negociacion))
                            registros.Negociacion = DateTime.Now.ToShortDateString();
                        registros.Estatus = valorHidden2;
                        break;
                    case 4://cierre
                        registros.Analisis = lblAnalisis.Text;
                        registros.Presentacion = lblPresentacion.Text;
                        if (string.IsNullOrEmpty(registros.Presentacion))
                            registros.Presentacion = DateTime.Now.ToShortDateString();
                        registros.Negociacion = lblNegociacion.Text;
                        if (string.IsNullOrEmpty(registros.Negociacion))
                            registros.Negociacion = DateTime.Now.ToShortDateString();
                        registros.Cierre = lblCierre.Text;
                        if (string.IsNullOrEmpty(registros.Cierre))
                            registros.Cierre = DateTime.Now.ToShortDateString();
                        registros.Estatus = valorHidden2;
                        break;
                    case 5://cancelacion
                        registros.Analisis = lblAnalisis.Text;
                        registros.Presentacion = lblPresentacion.Text;
                        registros.Negociacion = lblNegociacion.Text;
                        registros.Cierre = lblCierre.Text;
                        registros.Cancelacion = DateTime.Now.ToString();
                        registros.Estatus = valorHidden2;
                        registros.Id_Causa = Convert.ToInt32(ddlCausa2.SelectedValue);
                        registros.Competidor = txtCompetencia.Text;
                        registros.ComentariosCancel = txtCancela.Text;
                        break;
                }
                if (!string.IsNullOrEmpty(txtCotizacion.SelectedDate.Value.ToString()))
                    registros.FechaCotizacion = txtCotizacion.SelectedDate.Value.ToShortDateString();
                else
                {
                    error = 1;
                    Alerta("Ingrese una fecha de cotización");
                }
                registros.VentaMensual = !string.IsNullOrEmpty(txtVentaMensual.Value.ToString()) ? txtVentaMensual.Value.Value : 0.00;
                if (registros.VentaMensual == 0.00)
                {
                    error = 1;
                    Alerta("Ingrese un valor de venta promedio mensual esperada");
                }


                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CrmOportunidad clscrmCat = new CN_CrmOportunidad();
                if (error == 0)
                {
                    ///
                    bool dividir = false;
                    if (ddlAplicacion.SelectedValue == "-1")                    
                        for (int i = 0; i < this.DataGrid1.Items.Count; i++)
                        {
                            CheckBox cb = (CheckBox)this.DataGrid1.Items[i].FindControl("chk1");
                            if (!cb.Checked)                           
                                dividir = true;                           
                        }                    

                    if (!dividir)                    
                        clscrmCat.UpdateOportunidad(sesion, registros, ref validador);                   
                    else
                    {
                        string Aplicaciones = "";
                        for (int i = 0; i < this.DataGrid1.Items.Count; i++)
                        {
                            CheckBox cb = (CheckBox)this.DataGrid1.Items[i].FindControl("chk1");
                            if (cb.Checked)
                            {
                                CN_CrmPromocion clscrmCat2 = new CN_CrmPromocion();
                                CRMRegistroProyectos registros2 = new CRMRegistroProyectos();
                                //Aplicaciones = (DataGrid1.Items[i].FindControl("lblIdAplicacion") as HiddenField).Value;
                                //registros.Id_Apl = Convert.ToInt32(Aplicaciones);
                                //registros.ValorPotencialO = (DataGrid1.Items[i].FindControl("txt1") as RadNumericTextBox).Value.Value;

                                registros2.Uen = Convert.ToInt32(ddlUEN.SelectedValue);
                                registros2.Segmento = registros.Id_Seg;
                                registros2.Territorio = registros.Id_Ter;
                                registros2.Area = registros.ID_Area;
                                registros2.Solucion = registros.Id_Sol;
                                registros2.Aplicacion = registros.Id_Apl;

                                registros2.Cliente = Convert.ToInt32(HiddenField6.Value);
                                registros2.Productos = registros.Productos;
                                registros2.VentaNoRepetitiva = registros.VentaNoRepetitiva;
                                registros2.Comentarios = registros.Comentarios;
                                registros2.Analisis = Convert.ToDateTime(registros.Analisis);
                                if (registros.Presentacion !=null && registros.Presentacion != "")                                
                                registros2.Presentacion = Convert.ToDateTime(registros.Presentacion);
                                
                                if (registros.Negociacion != null && registros.Negociacion != "")                                
                                    registros2.Negociacion = Convert.ToDateTime(registros.Negociacion);
                                
                                if (registros.Cierre != null && registros.Cierre != "")                                
                                    registros2.Cierre = Convert.ToDateTime(registros.Cierre);
                                
                                if (registros.Cancelacion != null && registros.Cancelacion != "")
                                {
                                    registros2.Cancelacion = Convert.ToDateTime(registros.Cancelacion);
                                    registros2.Id_Causa = registros.Id_Causa;
                                    registros2.Competidor = registros.Competidor;
                                    registros2.ComentariosCancel = registros.ComentariosCancel;
                                }
                                registros2.FechaCotizacion = Convert.ToDateTime(registros.FechaCotizacion);
                                registros2.VentaPromedio = registros.VentaMensual;
                                registros2.ValorPotencialO = (DataGrid1.Items[i].FindControl("txt1") as RadNumericTextBox).Value.Value;
                                registros2.Estatus = registros.Estatus;
                                registros2.Aplicacion = Convert.ToInt32((DataGrid1.Items[i].FindControl("lblIdAplicacion") as RadNumericTextBox).Value);
                                registros2.Id_Op = parametros();
                                clscrmCat2.InsertarOportunidad(sesion, registros2, ref validador, Aplicaciones.ToString());
                            }
                        }
                        clscrmCat.DeleteOportunidad(sesion.Id_Emp, sesion.Id_Cd_Ver, registros.Id_Op, sesion.Emp_Cnx);
                    }
                    ////
                    if (validador == 1)
                        Guardar(registros.Id_Op);
                    else
                        Alerta("No se pudo actualizar el registro");
                }
                return error;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar(int oportunidad)
        {
            try
            {
                CrmOportunidades registros = new CrmOportunidades();
                CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                registros.Id_Emp = session.Id_Emp;
                registros.Id_Cd = session.Id_Cd_Ver;
                registros.Id_Ter = !string.IsNullOrEmpty(ddlTerritorio.SelectedValue) ? Convert.ToInt32(ddlTerritorio.SelectedValue) : 0;
                registros.Id_Cte = !string.IsNullOrEmpty(HiddenField1.Value) ? Convert.ToInt32(HiddenField1.Value) : 0;
                registros.ID_Area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
                registros.Id_Seg = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                registros.Id_Uen = !string.IsNullOrEmpty(ddlUEN.SelectedValue) ? Convert.ToInt32(ddlUEN.SelectedValue) : 0;
                registros.Id_Sol = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : 0;
                registros.Id_Op = oportunidad;
                registros.Comentarios = txtComentarios.Text;
                registros.Productos = txtProductos.Text;
                for (int i = 0; i < this.DataGrid1.Items.Count; i++)
                {
                    int verificador = 0;
                    RadNumericTextBox RadNumericTextBox1 = new RadNumericTextBox();
                    CheckBox cb = new CheckBox();
                    RadNumericTextBox1 = (RadNumericTextBox)this.DataGrid1.Items[i].Cells[6].FindControl("txt1");
                    cb = (CheckBox)this.DataGrid1.Items[i].Cells[6].FindControl("chk1");
                    registros.Id_Apl = (int)(DataGrid1.Items[i].Cells[6].FindControl("lblIdAplicacion") as RadNumericTextBox).Value.Value;  //!string.IsNullOrEmpty(DataGrid1.Items[i].Cells[9].Text) ? Convert.ToInt32(DataGrid1.Items[i].Cells[9].Text) : 0;
                    registros.Id_Estruc = (int)(DataGrid1.Items[i].Cells[6].FindControl("lblIdEstructura") as RadNumericTextBox).Value.Value; //!string.IsNullOrEmpty(DataGrid1.Items[i].Cells[0].Text) ? Convert.ToInt32(DataGrid1.Items[i].Cells[0].Text) : 0;
                    double porcentaje = Convert.ToDouble(RadNumericTextBox1.Value);
                    registros.Activo = cb.Checked;
                    if (!cb.Checked)
                        registros.Porcentaje = 0;
                    else
                        registros.Porcentaje = porcentaje;
                    clscrmCat.ActualizaDimension(registros, session.Emp_Cnx, ref verificador);
                    if (verificador == 0)
                    {
                        Alerta("hubo un error al actualizar la tabla de aplicación");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarGrid(int Id_Cte, int Id_Seg, int Id_Op)
        {
            try
            {
                int vTotalGralAreas = 0;
                int totSoluciones = 0;
                int vSolucionAnterior = 0;
                int totAnterior = 0;
                int totAreas = 0;
                int vAreaAnterior = 0;
                int totAnteriorA = 0;
                DataSet dsEstructuraSegmento = new DataSet();
                CrmOportunidades registros = new CrmOportunidades();
                CN_CatCliente cn_catcliente = new CN_CatCliente();

                int aplicacion = !string.IsNullOrEmpty(ddlAplicacion.SelectedValue) ? Convert.ToInt32(ddlAplicacion.SelectedValue) : -1;

                if (aplicacion == -1)
                {
                    CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                    registros.Id_Emp = session.Id_Emp;
                    registros.Id_Cd = session.Id_Cd_Ver;
                    registros.Id_Cte = Id_Cte;
                    registros.Id_Seg = Id_Seg;
                    registros.Id_Op = Id_Op;
                    registros.Id_Sol = Convert.ToInt32(ddlSolucion.SelectedValue);
                    clscrmCat.EstructuraSegmento(ref dsEstructuraSegmento, registros, session.Emp_Cnx);
                    DataTable dtTotalAreas = new DataTable();
                    DataTable dtTotalSoluciones = new DataTable();

                    DataGrid1.DataSource = dsEstructuraSegmento.Tables[0];
                    DataGrid1.DataBind();

                    dtTotalAreas = dsEstructuraSegmento.Tables[1];
                    dtTotalSoluciones = dsEstructuraSegmento.Tables[2];
                    if (this.DataGrid1.Items.Count == 0)
                    {
                        this.lblMensaje.Text = "El segmento no tiene areas configuradas";
                        ibtnGuardar.Visible = false;
                        return; 
                    }
                    this.DataGrid1.Items[0].Cells[0].RowSpan = this.DataGrid1.Items.Count;
                    this.DataGrid1.Items[0].Cells[1].RowSpan = this.DataGrid1.Items.Count;

                    for (int i = 1; i <= this.DataGrid1.Items.Count - 1; i++)
                    {
                        this.DataGrid1.Items[i].Cells.RemoveAt(0);
                        this.DataGrid1.Items[i].Cells.RemoveAt(0);
                    }

                    for (int i = 0; i <= dtTotalAreas.Rows.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            if (vAreaAnterior != (int)dtTotalAreas.Rows[i]["AreaID"])
                            {
                                vAreaAnterior = (int)dtTotalAreas.Rows[i]["AreaID"];
                                totAreas = (int)dtTotalAreas.Rows[i]["TotalArea"];
                                this.DataGrid1.Items[totAnteriorA].Cells[2].RowSpan = totAreas;
                                for (int j = totAnteriorA + 1; j <= (totAnteriorA + totAreas) - 1; j++)
                                    this.DataGrid1.Items[j].Cells.RemoveAt(0);
                                totAnteriorA = totAreas;
                            }
                            else if (i >= 1)
                            {
                                vAreaAnterior = (int)dtTotalAreas.Rows[i]["AreaID"];
                                totAreas = (int)dtTotalAreas.Rows[i]["TotalArea"];
                                this.DataGrid1.Items[totAnteriorA].Cells[0].RowSpan = totAreas;
                                for (int j = totAnteriorA + 1; j <= (totAnteriorA + totAreas) - 1; j++)
                                    this.DataGrid1.Items[j].Cells.RemoveAt(0);
                                totAnteriorA = totAnteriorA + totAreas;
                            }
                        }
                    }

                    for (int i = 0; i <= dtTotalSoluciones.Rows.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            if (vSolucionAnterior != (int)dtTotalSoluciones.Rows[i]["SolucionID"])
                            {
                                vSolucionAnterior = (int)dtTotalSoluciones.Rows[i]["SolucionID"];
                                totSoluciones = (int)dtTotalSoluciones.Rows[i]["TotalSolucion"];
                                if (totSoluciones != 1)
                                {
                                    this.DataGrid1.Items[totAnterior].Cells[3].RowSpan = totSoluciones;
                                    for (int j = totAnterior + 1; j <= (totAnterior + totSoluciones) - 1; j++)
                                        this.DataGrid1.Items[j].Cells.RemoveAt(0);
                                    totAnterior = totSoluciones;
                                }
                            }
                        }
                        else if (i >= 1)
                        {
                            vSolucionAnterior = (int)dtTotalSoluciones.Rows[i]["SolucionID"];
                            totSoluciones = (int)dtTotalSoluciones.Rows[i]["TotalSolucion"];
                            if (totSoluciones != 1)
                            {
                                if (vTotalGralAreas == 2)
                                {
                                    if (i == 2)
                                        this.DataGrid1.Items[totAnterior].Cells[0].RowSpan = totSoluciones;
                                    else
                                        this.DataGrid1.Items[totAnterior].Cells[1].RowSpan = totSoluciones;
                                }
                                else if (vTotalGralAreas == 3)
                                {
                                    if (i == 3)
                                        this.DataGrid1.Items[totAnterior].Cells[0].RowSpan = totSoluciones;
                                    else
                                        this.DataGrid1.Items[totAnterior].Cells[1].RowSpan = totSoluciones;
                                }
                                else
                                    this.DataGrid1.Items[totAnterior].Cells[0].RowSpan = totSoluciones;
                                for (int j = totAnterior + 1; j <= (totAnterior + totSoluciones) - 1; j++)
                                    this.DataGrid1.Items[j].Cells.RemoveAt(0);
                            }
                            totAnterior = totAnterior + totSoluciones;
                        }
                    }

                    //for (int x = 7; x <= 11; x++)
                    //{
                    //    DataGrid1.Columns[x].Visible = false;
                    //}

                    if (dsEstructuraSegmento.Tables[0].Rows.Count > 0)
                    {
                        //tableDatos.Visible = false;
                        vpo.Visible = false;
                        vpt.Visible = false;
                        tableGridDatos.Visible = true;
                    }
                    else
                    {
                        //tableDatos.Visible = true;
                        vpo.Visible = true;
                        vpt.Visible = true;
                        tableGridDatos.Visible = false;
                    }
                }
                else
                {
                    //tableDatos.Visible = true;
                    vpo.Visible = true;
                    vpt.Visible = true;
                    tableGridDatos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int parametros()
        {
            try
            {
                int idOportunidad = 0;
                if (Request.QueryString["id"] != null)
                {
                    string strId = Request.QueryString["id"].ToString();
                    Int32.TryParse(strId, out idOportunidad);
                }
                return idOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void parametros3()
        {
            try
            {
                if (Request.QueryString["cd"] != null)
                {
                    string strCd = Request.QueryString["cd"].ToString();
                    Int32.TryParse(strCd, out idCD);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void avances(int tipo, int valido)
        {
            try
            {
                if (valido == 1)
                    switch (tipo)
                    {//Avance
                        case 1://promocion                          
                            lblPresentacion.Text = DateTime.Now.ToShortDateString();
                            //chkNegociacion.Enabled = true;
                            //chkPresentacion.Enabled = false;
                            //chkCancelacion.Enabled = true;
                            HiddenField5.Value = "2";
                            break;
                        case 2://negociacion
                            lblNegociacion.Text = DateTime.Now.ToShortDateString();
                            //chkNegociacion.Enabled = false;
                            //chkPresentacion.Enabled = false;
                            //chkCierre.Enabled = true;
                            //chkCancelacion.Enabled = true;
                            HiddenField5.Value = "3";
                            break;
                        case 3://cierre                            
                            lblCierre.Text = DateTime.Now.ToShortDateString();
                            //chkNegociacion.Enabled = false;
                            //chkPresentacion.Enabled = false;
                            //chkCierre.Enabled = false;
                            //chkCancelacion.Enabled = true;
                            HiddenField5.Value = "4";
                            break;
                    }
                else
                    switch (tipo)
                    {//Cancelacion de avance
                        case 1://presentacion
                            lblPresentacion.Text = "";
                            chkPresentacion.Checked = false;
                            //chkNegociacion.Enabled = false;
                            //chkCierre.Enabled = false;
                            //chkCancelacion.Enabled = true;
                            HiddenField5.Value = "1";
                            break;
                        case 2://negociacion                           
                            lblNegociacion.Text = "";
                            chkNegociacion.Checked = false;
                            //chkPresentacion.Enabled = false;
                            //chkNegociacion.Enabled = true;
                            //chkCierre.Enabled = false;
                            //chkCancelacion.Enabled = true;
                            HiddenField5.Value = "2";
                            break;
                        case 3://cierre                           
                            lblCierre.Text = "";
                            chkCierre.Checked = false;
                            //chkPresentacion.Enabled = false;
                            //chkNegociacion.Enabled = false;
                            //chkCierre.Enabled = true;
                            //chkCancelacion.Enabled = true;
                            HiddenField5.Value = "3";
                            break;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUEN(int valorUEN)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUen_Combo", ref ddlUEN);
                ddlUEN.SelectedValue = valorUEN.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSegmentos(ref int valorSegmento)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmOportunidades> list = new List<CrmOportunidades>();
                CN_CrmOportunidad clscrmCat = new CN_CrmOportunidad();
                clscrmCat.ComboSegmento(sesion, ref list);
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
        private void CargarAreas(int segmento, int area)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmOportunidades> list = new List<CrmOportunidades>();
                CN_CrmOportunidad clscrmCat = new CN_CrmOportunidad();
                clscrmCat.ComboArea(sesion, segmento, ref list);
                ddlArea.Items.Clear();
                if (list.Count > 0)
                {
                    //area = list[0].Id;
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
        private void CargarSolucion(int area,   int solucion)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmOportunidades> list = new List<CrmOportunidades>();
                CN_CrmOportunidad clscrmCat = new CN_CrmOportunidad();
                clscrmCat.CargarSolucion(sesion, area, ref list);
                ddlSolucion.Items.Clear();
                if (list.Count > 0)
                {
                    //solucion = list[0].Id;
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
        private void CargarAplicacion(int solucion,   int aplicacion)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmOportunidades> list = new List<CrmOportunidades>();
                CN_CrmOportunidad clscrmCat = new CN_CrmOportunidad();
                clscrmCat.ConsultaAplicacion(sesion, solucion, ref list);
                ddlAplicacion.Items.Clear();
                if (list.Count > 0)
                {
                    //aplicacion = list[0].Id;
                    for (int i = 1; i < list.Count; i++)
                        ddlAplicacion.Items.Add(new RadComboBoxItem(list[i].Descripcion, list[i].Id.ToString()));

                    RadComboBoxItem rcbi = new RadComboBoxItem();
                    rcbi.Value = "-1";
                    rcbi.Text = "";
                    ddlAplicacion.Items.Insert(0, rcbi);
                    //if (aplicacion != -1)
                    //{
                        ddlAplicacion.SelectedValue = aplicacion.ToString();
                    //}
                    //else
                    //{
                    //    ddlAplicacion.SelectedIndex = 0;
                    //}
                    //ddlAplicacion.Enabled = true;
                }
                int cliente = !string.IsNullOrEmpty(HiddenField6.Value) ? Convert.ToInt32(HiddenField6.Value) : 0;
                int segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : -1;
                int oportunidad = parametros();
                CargarGrid(cliente, segmento, oportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipos()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Emp_Cnx, "spCrmCausasCancelacion_Combo", ref ddlCausa2);
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
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);

                pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");
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
        private void RadConfirm(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("oldConfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }

        private void RadConfirmC(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("oldConfirm('" + mensaje + "<br /><br />', confirmCallBackFn);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void RadConfirmN(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFnN);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
       
        private void RadConfirmCa(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFnCa);");
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
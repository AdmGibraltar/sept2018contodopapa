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
using System.Web.UI.HtmlControls;
using System.Text;
using CapaDatos;

namespace SIANWEB
{
    public partial class wfrmRegistrarOportunidad : System.Web.UI.Page
    {
        #region Variables

        public int totAplicaciones
        {
            get { return DataGrid1.Items.Count; }
        }
        public string fechaActual
        {
            get
            {
                Funciones funcion = new Funciones();
                return funcion.GetLocalDateTime(session.Minutos).ToString("dd/MM/yyyy");
            }
            set { }
        }
        private int cliente
        {
            set
            {
                Session["cliente" + Session.SessionID] = value;
            }
            get
            {
                return Convert.ToInt32(Session["cliente" + Session.SessionID]);
            }
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
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        private bool _PermisoGuardar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion

        #region Eventos
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    chkPresentacion.Attributes.Add("onClick", "Presentacion(this);");
        //    chkNegociacion.Attributes.Add("onClick", "Negociacion(this);");
        //    chkCierre.Attributes.Add("onClick", "Cierre(this);");
        //}
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
            catch (Exception)
            {
                Response.Redirect("wfrmPrincipalOportunidades.aspx");
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlUEN_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarSegmentos();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlSegmento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                txtCliente.Text = "";
                HiddenField1.Value = "";
                CargarTerritorio();
                CargarAreas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlTerritorio_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                txtCliente.Text = "";
                HiddenField1.Value = "";
                CargarAreas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarSolucion();
                CalculaValor();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlSolucion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarAplicacion();
                CalculaValor();
                int segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                CargarGrid(segmento);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlAplicacion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
               this.ValidateCampana();
                CalculaValor();
                int segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                CargarGrid(segmento);
                
                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            switch (e.Argument)
            {
                case "ok":
                    if (_PermisoGuardar)
                    {
                        Guardando();
                        Response.Redirect("wfrmPrincipalOportunidades.aspx", false);
                    }
                    else
                        Alerta("No tiene permisos para grabar");
                    break;
                case "opcionA":
                    int cliente = 0;
                    if (Session["NumCliente" + Session.SessionID] != null)
                        if (!string.IsNullOrEmpty(Session["NumCliente" + Session.SessionID].ToString()))
                        {
                            int.TryParse(Session["NumCliente" + Session.SessionID].ToString(), out cliente);
                            lblMensaje.Text = "";
                            txtCliente.Text = Session["NombreCliente" + Session.SessionID].ToString();
                            chkNoRepetitiva.Focus();
                            HiddenField1.Value = cliente.ToString();
                            CalculaValor();
                        }
                    break;
                case "opcionB":
                    int tipo = 0;
                    int valido = -1;
                    if (Session["NumValido"] != null)
                        if (!string.IsNullOrEmpty(Session["NumValido"].ToString()))
                            int.TryParse(Session["NumValido"].ToString(), out valido);
                    if (valido > -1)
                        if (Session["NumTipo"] != null)
                            if (!string.IsNullOrEmpty(Session["NumTipo"].ToString()))
                                int.TryParse(Session["NumTipo"].ToString(), out tipo);
                    if (_PermisoGuardar)
                    {
                        Guardando();
                        Response.Redirect("wfrmPrincipalOportunidades.aspx", false);
                    }
                    else
                        Alerta("No tiene permiso para guardar el proyecto");
                    break;
                case "opcionC":
                    int validador = -1;
                    if (Session["NumValido"] != null)
                        if (!string.IsNullOrEmpty(Session["NumValido"].ToString()))
                            int.TryParse(Session["NumValido"].ToString(), out validador);
                    if (validador == 1)
                        if (_PermisoGuardar)
                        {
                            Guardando();
                            Response.Redirect("wfrmPrincipalOportunidades.aspx", false);
                        }
                        else
                            Alerta("No tiene permiso para guardar el proyecto");
                    break;
                case "opcionD":                        
                         
                    if (Session["Id_Cam" + Session.SessionID] != null) {
                    //    RadAjaxManager1.ResponseScripts.Add("setCampania(" + Session["Id_Cam" + Session.SessionID].ToString() + ",'" + Session["campania" + Session.SessionID].ToString() + "')");
                                       
                    }
                           
                       
                    break;
                default:
                    break;
            }
        }
        protected void ibtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool visualizar = true;
                ibtnGuardar.Visible = false;
                ibtnGuardar.Visible = false;
                if (_PermisoGuardar)
                {
                    if (ddlAplicacion.Text == "")
                    {
                        Alerta("Seleccione por favor Una Aplicación");
                    }
                    else if (ddlAplicacion.Text == "" && DataGrid1.Items.Count == 0)
                    {
                        Alerta("Ingrese un valor potencial observado");
                    }
                    else
                    {
                        visualizar = Guardando();
                    }
                }
                else
                    Alerta("No tiene permiso para guardar el proyecto");

                if (visualizar)
                {
                    ibtnGuardar.Visible = true;
                    ibtnGuardar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
            catch (Exception)
            {
                throw;
            }
        }
        //protected void chkPresentacion_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkPresentacion.Checked)
        //    {
        //        int error = 0;
        //        CRMRegistroProyectos registros = new CRMRegistroProyectos();
        //        registros.Uen = Convert.ToInt32(ddlUEN.SelectedValue);
        //        registros.Segmento = Convert.ToInt32(ddlSegmento.SelectedValue);
        //        registros.Territorio = Convert.ToInt32(ddlTerritorio.SelectedValue);
        //        registros.Area = Convert.ToInt32(ddlArea.SelectedValue);
        //        registros.Solucion = Convert.ToInt32(ddlSolucion.SelectedValue);
        //        registros.Aplicacion = Convert.ToInt32(ddlAplicacion.SelectedValue);
        //        registros.Cliente = !string.IsNullOrEmpty(HiddenField1.Value) ? Convert.ToInt32(HiddenField1.Value) : 0;
        //        if (registros.Uen <= 0)
        //            error = 1;
        //        if (error == 0)
        //            if (registros.Segmento <= 0)
        //                error = 1;
        //        if (error == 0)
        //            if (registros.Territorio <= 0)
        //                error = 1;
        //        if (error == 0)
        //            if (registros.Area <= 0)
        //                error = 1;
        //        if (error == 0)
        //            if (registros.Solucion <= 0)
        //                error = 1;
        //        if (error == 0)
        //            if (registros.Cliente <= 0)
        //                error = 2;
        //        if (error == 0)
        //        {
        //            //string funcion = "AbrirVentanaAutorizar(1)";
        //            //string script = "<script>" + funcion + "</script>";
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
        //        }
        //        else
        //        {
        //            HiddenField3.Value = "1";
        //            chkPresentacion.Checked = false;
        //            if (error == 1)
        //                Alerta("Complete el llenado de los campos de selección");
        //            else
        //                Alerta("Ingrese un cliente");
        //            chkPresentacion.Attributes.Add("OnCheckedChanged", "chkPresentacion_CheckedChanged");
        //        }
        //    }
        //    else
        //    {
        //        HiddenField3.Value = "1";
        //        chkPresentacion.Checked = false;
        //        chkPresentacion.Attributes.Add("OnCheckedChanged", "chkPresentacion_CheckedChanged");
        //    }
        //}
        //protected void chkNegociacion_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkNegociacion.Checked)
        //        {
        //            int error = 0;
        //            CRMRegistroProyectos registros = new CRMRegistroProyectos();
        //            registros.VentaPromedio = !string.IsNullOrEmpty(txtVentaMensual.Value.ToString()) ? txtVentaMensual.Value.Value : 0.00;
        //            if (registros.VentaPromedio <= 0)
        //                error = 1;
        //            if (error == 0)
        //            {
        //                //string funcion = "AbrirVentanaAutorizar(2)";
        //                //string script = "<script>" + funcion + "</script>";
        //                //ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
        //            }
        //            else
        //            {
        //                HiddenField3.Value = "2";
        //                chkPresentacion.Enabled = false;
        //                chkNegociacion.Enabled = true;
        //                chkNegociacion.Checked = false;
        //                Alerta("Ingrese una cantidad en venta promedio mensual");
        //                chkNegociacion.Attributes.Add("OnCheckedChanged", "chkNegociacion_CheckedChanged");
        //            }
        //        }
        //        else
        //        {
        //            HiddenField3.Value = "2";
        //            chkPresentacion.Enabled = false;
        //            chkNegociacion.Enabled = true;
        //            chkNegociacion.Checked = false;
        //            chkNegociacion.Attributes.Add("OnCheckedChanged", "chkNegociacion_CheckedChanged");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //protected void chkCierre_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkCierre.Checked)
        //        {
        //            //string funcion = "AbrirVentanaAutorizar(3)";
        //            //string script = "<script>" + funcion + "</script>";
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
        //        }
        //        else
        //        {
        //            HiddenField3.Value = "3";
        //            chkPresentacion.Enabled = false;
        //            chkNegociacion.Enabled = false;
        //            chkCierre.Enabled = true;
        //            chkCierre.Checked = false;
        //            chkCierre.Attributes.Add("OnCheckedChanged", "chkCierre_CheckedChanged");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        protected void txtNoCliente_TextChanged(object sender, EventArgs e)
        {
            //clientes();
            //CargarGrid();
        }
        protected void ibtnBuscarCliente_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int cte = !string.IsNullOrEmpty(HiddenField1.Value) ? Convert.ToInt32(HiddenField1.Value) : 0;
                int ter = !string.IsNullOrEmpty(ddlTerritorio.SelectedValue) ? Convert.ToInt32(ddlTerritorio.SelectedValue) : 0;
                int uen = !string.IsNullOrEmpty(ddlUEN.SelectedValue) ? Convert.ToInt32(ddlUEN.SelectedValue) : 0;
                int seg = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                RadAjaxManager1.ResponseScripts.Add("return AbrirVentana(" + cte + "," + ter + "," + uen + "," + seg + ");");
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void id_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int cantidad = this.DataGrid1.Items[0].Cells.Count;
                string valor = e.ToString();
                string valor2 = sender.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
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
        protected void chkPresentacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as CheckBox).Checked)
                {
                    lblPresentacion.Text = fechaActual;
                    HiddenField3.Value = "2";
                }
                else
                {
                    lblPresentacion.Text = "";
                    HiddenField3.Value = "1";
                    chkNegociacion.Checked = false;
                    lblNegociacion.Text = "";
                    chkCierre.Checked = false;
                    lblCierre.Text = "";
                }
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
                if ((sender as CheckBox).Checked)
                {
                    lblNegociacion.Text = fechaActual;
                    //chkPresentacion.Enabled = false;
                    //chkCierre.Enabled = true;
                    if (!chkPresentacion.Checked)
                    {
                        chkPresentacion.Checked = true;
                        lblPresentacion.Text = fechaActual;
                    }
                    HiddenField3.Value = "3";
                }
                else
                {
                    lblNegociacion.Text = "";
                    HiddenField3.Value = "2";
                    //chkPresentacion.Enabled = true;
                    //chkCierre.Enabled = false;
                    chkCierre.Checked = false;
                    lblCierre.Text = "";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void chkCierre_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as CheckBox).Checked)
                {
                    lblCierre.Text = fechaActual;
                    //chkNegociacion.Enabled = false;
                    if (!chkNegociacion.Checked)
                    {
                        chkNegociacion.Checked = true;
                        lblNegociacion.Text = fechaActual;
                    }
                    if (!chkPresentacion.Checked)
                    {
                        chkPresentacion.Checked = true;
                        lblPresentacion.Text = fechaActual;
                    }
                    HiddenField3.Value = "4";
                }
                else
                {
                    lblCierre.Text = "";
                    HiddenField3.Value = "3";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region funciones
        private void CargarGrid(int Id_Seg)
        {
            try
            {
                //if (HiddenField1.Value != "")
                //{
                //    if (Convert.ToInt32(HiddenField1.Value) <= 0 || Id_Seg <= 0)
                //    {
                //        return;
                //    }
                //}
                //else
                //{
                //    return;
                //}
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

                if (ddlAplicacion.Text == "")
                {
                    CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                    registros.Id_Emp = session.Id_Emp;
                    registros.Id_Cd = session.Id_Cd_Ver;
                    //registros.Id_Cte = Id_Cte == 0 ? Convert.ToInt32(HiddenField1.Value) : Id_Cte;
                    registros.Id_Seg = Id_Seg;
                    registros.Id_Sol = Convert.ToInt32(ddlSolucion.SelectedValue == "" ? "-1" : ddlSolucion.SelectedValue);
                    registros.Id_Op = -1;

                    clscrmCat.EstructuraSegmento(ref dsEstructuraSegmento, registros, session.Emp_Cnx);
                    DataTable dtTotalAreas = new DataTable();
                    DataTable dtTotalSoluciones = new DataTable();

                    DataGrid1.DataSource = dsEstructuraSegmento.Tables[0];
                    DataGrid1.DataBind();

                    dtTotalAreas = dsEstructuraSegmento.Tables[1];
                    dtTotalSoluciones = dsEstructuraSegmento.Tables[2];
                    if (DataGrid1.Items.Count > 0)
                    {
                        this.DataGrid1.Items[0].Cells[0].RowSpan = this.DataGrid1.Items.Count;
                        this.DataGrid1.Items[0].Cells[1].RowSpan = this.DataGrid1.Items.Count;
                    }
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

                    //  if (dsEstructuraSegmento.Tables[0].Rows.Count > 0)
                    // {
                    //tableDatos.Visible = false;
                    vpo.Visible = false;
                    vpt.Visible = false;
                    tableGridDatos.Visible = true;
                    //}
                    //else
                    //{
                    //tableDatos.Visible = true;
                    //  vpo.Visible = true;
                    //vpt.Visible = true;
                    //tableGridDatos.Visible = false;
                    //}
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
        private void CopiaValoresPotenciales()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RevisandoEstructuraSegmento(ref int validador)
        {
            try
            {
                if (DataGrid1.Items != null)
                    if (DataGrid1.Items.Count > 0)
                        for (int i = 0; i < this.DataGrid1.Items.Count; i++)
                        {
                            RadNumericTextBox RadNumericTextBox1 = new RadNumericTextBox();
                            CheckBox cb = new CheckBox();
                            RadNumericTextBox1 = (RadNumericTextBox)this.DataGrid1.Items[i].Cells[11].FindControl("txt");
                            cb = (CheckBox)this.DataGrid1.Items[i].Cells[11].FindControl("id");
                            int Id_Apl = !string.IsNullOrEmpty(DataGrid1.Items[i].Cells[8].Text) ? Convert.ToInt32(DataGrid1.Items[i].Cells[8].Text) : 0;
                            int Id_Estruc = !string.IsNullOrEmpty(DataGrid1.Items[i].Cells[12].Text) ? Convert.ToInt32(DataGrid1.Items[i].Cells[12].Text) : 0;
                            double porcentaje = Convert.ToDouble(RadNumericTextBox1.Value);
                            bool Activo = cb.Checked;
                            if (!cb.Checked)
                                validador = 1;
                        }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool Guardando()
        {
            try
            {

                double teorico = 0;
                double observado = 0;
                if (DataGrid1.Visible)
                    if (DataGrid1.Items.Count > 0)
                    {
                        for (int i = 0; i < DataGrid1.Items.Count; i++)
                        {
                            if ((DataGrid1.Items[i].Cells[6].FindControl("chk1") as CheckBox).Checked)
                            {
                                observado += (DataGrid1.Items[i].FindControl("txt1") as RadNumericTextBox).Value.HasValue ? (DataGrid1.Items[i].Cells[6].FindControl("txt1") as RadNumericTextBox).Value.Value : 0;
                                if (i == 0)
                                    teorico += Convert.ToDouble(DataGrid1.Items[i].Cells[5].Text);
                                else
                                    teorico += Convert.ToDouble(DataGrid1.Items[i].Cells[1].Text);
                            }
                        }
                        txtVPTeorico.Text = teorico.ToString();
                        if (observado == 0)
                            observado = 0.0000001;
                        txtVPObservado.Value = observado;
                    }
                int error = 0;
                int validador = 0;
                lblMensaje.Text = "";
                CRMRegistroProyectos registros = new CRMRegistroProyectos();
                registros.Uen = !string.IsNullOrEmpty(ddlUEN.SelectedValue) ? Convert.ToInt32(ddlUEN.SelectedValue) : 0;
                registros.Segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                registros.Territorio = !string.IsNullOrEmpty(ddlTerritorio.SelectedValue) ? Convert.ToInt32(ddlTerritorio.SelectedValue) : 0;
                registros.Area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
                registros.Solucion = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : 0;
                registros.Aplicacion = !string.IsNullOrEmpty(ddlAplicacion.SelectedValue) ? Convert.ToInt32(ddlAplicacion.SelectedValue) : 0;
                registros.Cliente = !string.IsNullOrEmpty(HiddenField1.Value) ? Convert.ToInt32(HiddenField1.Value) : 0;
                if (string.IsNullOrEmpty(registros.Cliente.ToString()))
                    registros.Cliente = !string.IsNullOrEmpty(HiddenField2.Value) ? Convert.ToInt32(HiddenField2.Value) : 0;
                registros.VentaNoRepetitiva = chkNoRepetitiva.Checked;
                registros.ValorPotencialT = !string.IsNullOrEmpty(txtVPTeorico.Text) ? Convert.ToDouble(txtVPTeorico.Text) : 0;
                registros.ValorPotencialO = !string.IsNullOrEmpty(txtVPObservado.Text) ? Convert.ToDouble(txtVPObservado.Text) : 0;
                registros.Comentarios = !string.IsNullOrEmpty(txtComentarios.Text)? txtComentarios.Text : "";
                registros.Productos = txtProductos.Text;
                registros.Id_Cam = !string.IsNullOrEmpty(HiddenCampaniaId.Value)?  Convert.ToInt32(HiddenCampaniaId.Value) : 0;
                registros.Id_Cam = CheckPerteneceCampania.Checked ? registros.Id_Cam : 0;
                
                int valorHidden = !string.IsNullOrEmpty(HiddenField3.Value) ? Convert.ToInt32(HiddenField3.Value) : 1;
                switch (valorHidden)
                {
                    case 1:
                        registros.Analisis = DateTime.Now;
                        registros.Estatus = valorHidden;
                        break;
                    case 2:
                        registros.Analisis = DateTime.Now;
                        registros.Presentacion = DateTime.Now;
                        registros.Estatus = valorHidden;
                        break;
                    case 3:
                        registros.Analisis = DateTime.Now;
                        registros.Presentacion = DateTime.Now;
                        registros.Negociacion = DateTime.Now;
                        registros.Estatus = valorHidden;
                        break;
                    case 4:
                        registros.Analisis = DateTime.Now;
                        registros.Presentacion = DateTime.Now;
                        registros.Negociacion = DateTime.Now;
                        registros.Cierre = DateTime.Now;
                        registros.Estatus = valorHidden;
                        break;
                }
                if (string.IsNullOrEmpty(txtCotizacion.SelectedDate.Value.ToString()))
                {
                    error = 1;
                    Alerta("Seleccione una fecha de cotización");
                }
                else
                    registros.FechaCotizacion = txtCotizacion.SelectedDate.Value;

                if (registros.ValorPotencialO <= 0.0000001)
                {
                    error = 1;
                    if (ddlAplicacion.SelectedIndex == 0)
                        CargarGrid(ddlSegmento.SelectedValue == "" ? -1 : Convert.ToInt32(ddlSegmento.SelectedValue));
                    Alerta("Ingrese un valor potencial observado");
                }

                registros.VentaPromedio = !string.IsNullOrEmpty(txtVentaMensual.Text) ? Convert.ToDouble(txtVentaMensual.Text) : 0;
                string valor = MaximoId();
                registros.IdMax = !string.IsNullOrEmpty(valor) ? Convert.ToInt32(valor) : 0;
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                if (error == 0)
                {
                    bool dividir = false;
                    Session["NumCliente" + Session.SessionID] = "";
                    string Aplicaciones = "";
                    if (ddlAplicacion.SelectedValue == "-1")
                    {
                        for (int i = 0; i < this.DataGrid1.Items.Count; i++)
                        {
                            CheckBox cb = (CheckBox)this.DataGrid1.Items[i].FindControl("chk1");
                            if (cb.Checked)                            
                                Aplicaciones = Aplicaciones + (DataGrid1.Items[i].FindControl("lblIdAplicacion") as HiddenField).Value + ",";                           
                            else                           
                                dividir = true;                           
                        }
                        Aplicaciones = Aplicaciones.ToString().Remove(Aplicaciones.Length - 1);
                    }
                    else                    
                        Aplicaciones = ddlAplicacion.SelectedValue;                    

                    if (!dividir)                   
                        clscrmCat.InsertarOportunidad(sesion, registros, ref validador, Aplicaciones.ToString());                   
                    else
                    {
                        for (int i = 0; i < this.DataGrid1.Items.Count; i++)
                        {
                            CheckBox cb = (CheckBox)this.DataGrid1.Items[i].FindControl("chk1");
                            if (cb.Checked)
                            {
                                Aplicaciones = (DataGrid1.Items[i].FindControl("lblIdAplicacion") as HiddenField).Value;
                                registros.Aplicacion = Convert.ToInt32(Aplicaciones);
                                registros.ValorPotencialO = (DataGrid1.Items[i].FindControl("txt1") as RadNumericTextBox).Value.Value;
                                clscrmCat.InsertarOportunidad(sesion, registros, ref validador, Aplicaciones.ToString());
                            }
                        }
                    }
                    if (validador > 0)
                    {
                        Alerta("Registro se agregó correctamente");
                        ////////revisar
                        if (ddlAplicacion.SelectedValue == "-1" && !dividir)                       
                            Guardar(validador);

                        Alerta("Redireccionando, espere por favor");
                        Response.Redirect("wfrmPrincipalOportunidades.aspx", false);
                        return false;
                    }
                    else
                    {
                        if (validador == -1)
                        {
                            Alerta("Imposible crear oportunidad, ya existe una oportunidad para la solucion y/o aplicación seleccionada");
                            CargarGrid(Convert.ToInt32(ddlSegmento.SelectedValue));
                        }
                        else
                        {
                            Alerta("Ocurrió un error al intentar dar de alta la oportunidad");
                            CargarGrid(Convert.ToInt32(ddlSegmento.SelectedValue));
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                return true;
            }
        }
        private void ValidarPermisos()
        {
            try
            {
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
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
        private void Guardar(int validador)
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
                registros.Id_Op = validador;

                for (int i = 0; i < this.DataGrid1.Items.Count; i++)
                {
                    int verificador = 0;
                    RadNumericTextBox RadNumericTextBox1 = new RadNumericTextBox();
                    CheckBox cb = new CheckBox();
                    RadNumericTextBox1 = (RadNumericTextBox)this.DataGrid1.Items[i].Cells[6].FindControl("txt1");
                    cb = (CheckBox)this.DataGrid1.Items[i].FindControl("chk1");
                    registros.Id_Apl = Convert.ToInt32((DataGrid1.Items[i].FindControl("lblIdAplicacion") as HiddenField).Value);  //!string.IsNullOrEmpty(DataGrid1.Items[i].Cells[9].Text) ? Convert.ToInt32(DataGrid1.Items[i].Cells[9].Text) : 0;
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
        private void ActualizarValorPotencialCliente()
        {
            try
            {
                //for (int i = 0; i <= dg2.Items.Count - 1; i++)
                //{
                //    // Session["AplicacionID"] = DataGrid1.DataKeys[i];
                //    RadNumericTextBox txt1 = new RadNumericTextBox();
                //    txt1 = (RadNumericTextBox)DataGrid1.Items[i].FindControl("txt");
                //    RadNumericTextBox txt2 = new RadNumericTextBox();
                //    txt2 = (RadNumericTextBox)dg2.Items[i].FindControl("txt");

                //    double VPONuevo = 0;
                //    double VPDiff = 0;

                //    if (CastDouble(txt1.Text) != CastDouble(txt2.Text))
                //    {
                //        if (CastDouble(txt1.Text) < CastDouble(txt2.Text))
                //        {
                //            VPDiff = CastDouble(txt2.Text) - CastDouble(txt1.Text);
                //            VPONuevo = CastDouble(txtValorPO.Text) - VPDiff;
                //        }
                //        else
                //        {
                //            VPDiff = CastDouble(txt1.Text) - CastDouble(txt2.Text);
                //            VPONuevo = CastDouble(txtValorPO.Text) + VPDiff;
                //        }

                //        CN_CatCliente cn_catacliente = new CN_CatCliente();
                //        Clientes cte = new Clientes();
                //        cte.Id_Emp = session.Id_Emp;
                //        cte.Id_Cd = session.Id_Cd_Ver;
                //        cte.Id_Seg = Convert.ToInt32(lblSeg.Text);
                //        cte.Id_Terr = Convert.ToInt32(lblTer.Text);
                //        cte.Id_Cte = Convert.ToInt32(lblCte.Text);
                //        cte.Id_Apl = Convert.ToInt32(DataGrid1.DataKeys[i]);

                //        int verificador = 0;
                //        cn_catacliente.ActualizaPotencial(cte, VPONuevo, CastDouble(txt1.Text).ToString(), ref verificador, session.Emp_Cnx);

                //        txtValorPO.Text = VPONuevo.ToString("$ #,##0.00");
                //    }
                //}
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            return true;
        }
        private void Inicializar()
        {
            try
            {
                int cds = session.Id_Cd_Ver;
                DateTime fecha = DateTime.Now;
                //lblVPObservado.Text = "0";
                //lblVPTeorico.Text = "0";
                lblAnalisis.Text = fecha.ToShortDateString();
                txtCotizacion.SelectedDate = fecha;
                txtCotizacion.Enabled = false;
                //filtro1  
                //if (!IsPostBack)
                //{
                //    chkNegociacion.Enabled = false;
                //    chkCierre.Enabled = false;
                //}
                CargarUEN();
                CalculaValor();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarUEN()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd, session.Id_U, session.Emp_Cnx, "spCatRikUen_Combo", ref ddlUEN);
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
                if (ddlUEN.SelectedValue == "")
                    ddlUEN.SelectedIndex = 0;

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaComboUEN(1, session.Id_Emp, Convert.ToInt32(ddlUEN.SelectedValue), Convert.ToInt32(session.Id_U), session.Emp_Cnx, "spCatSegmentosUen_ComboCRM", ref ddlSegmento,session.Id_Cd_Ver);
                ddlSegmento.Items.Remove(0);

                
                ddlSegmento.SelectedIndex = 0;
                CargarTerritorio();
                CargarAreas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void CargarUEN()
        //{
        //    try
        //    {
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd, session.Id_U, session.Emp_Cnx, "spCatRikUen_Combo", ref ddlUEN);
        //        ddlUEN.Items.Remove(0);
        //        ddlUEN.SelectedIndex = 0;
        //        CargarSegmentos();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        //private void CargarSegmentos()
        //{
        //    try
        //    {
        //        if (ddlUEN.SelectedValue == "")
        //            ddlUEN.SelectedIndex = 0;

        //        if (ddlUEN.SelectedIndex != -1)
        //        {
        //            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //            CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlUEN.SelectedValue), session.Emp_Cnx, "spCatSegmentosUen_Combo", ref ddlSegmento);
        //            ddlSegmento.Items.Remove(0);
        //            ddlSegmento.SelectedIndex = 0;
        //        }
        //        else
        //            ddlSegmento.DataSource = null;
        //        CargarTerritorio();
        //        CargarAreas();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        private void CargarTerritorio()
        {
            try
            {
                if (ddlSegmento.SelectedValue == "")
                    ddlSegmento.SelectedIndex = 0;

                if (ddlSegmento.SelectedIndex != -1)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaComboCRM(session.Id_Emp, session.Id_Cd, Convert.ToInt32(ddlSegmento.SelectedValue), session.Id_Rik, session.Emp_Cnx, "spCatTerritorioSegmento_ComboCRM", ref ddlTerritorio, session.Id_Cd_Ver);
                    ddlTerritorio.Items.Remove(0);
                    ddlTerritorio.SelectedIndex = 0;
                }
                else
                    ddlSegmento.DataSource = null;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarAreas()
        {
            try
            {
                if (ddlSegmento.SelectedValue == "")
                    ddlSegmento.SelectedIndex = 0;

                if (ddlSegmento.SelectedIndex != -1)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlSegmento.SelectedValue), session.Emp_Cnx, "spCatAreaSegmento_Combo", ref ddlArea);
                    ddlArea.Items.Remove(0);
                    ddlArea.SelectedIndex = 0;
                }
                else
                    ddlSegmento.Items.Clear();
                this.CargarSolucion();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarSolucion()
        {
            try
            {
                if (ddlArea.SelectedValue == "")
                    ddlArea.SelectedIndex = 0;

                if (ddlArea.SelectedIndex != -1)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlArea.SelectedValue), session.Emp_Cnx, "spCatSolucionArea_Combo", ref ddlSolucion);
                    ddlSolucion.Items.Remove(0);
                    ddlSolucion.SelectedIndex = 0;
                }
                else
                    ddlSolucion.Items.Clear();
                CargarAplicacion();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarAplicacion()
        {
            try
            {
                if (ddlSolucion.SelectedValue == "")
                    ddlSolucion.SelectedIndex = 0;

                if (ddlSolucion.SelectedIndex != -1)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlSolucion.SelectedValue), session.Emp_Cnx, "spCatAplicacionSolucion_Combo", ref ddlAplicacion);
                    ddlAplicacion.Items.Remove(0);

                    if (ddlAplicacion.Items.Count == 1)                   
                        ddlAplicacion.SelectedIndex = 0;                    
                    else
                    {
                        RadComboBoxItem rcbi = new RadComboBoxItem();
                        rcbi.Value = "-1";
                        rcbi.Text = "";
                        ddlAplicacion.Items.Insert(0, rcbi);
                        ddlAplicacion.SelectedIndex = 0;
                        CargarGrid(Convert.ToInt32(ddlSegmento.SelectedValue));
                    }
                }
                else
                {
                    ddlAplicacion.Items.Clear();
                    if (ddlSegmento.SelectedValue != "")                  
                        CargarGrid(Convert.ToInt32(ddlSegmento.SelectedValue));                   
                }

                this.ValidateCampana();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void ValidateCampana() {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_CatCampanas clscrmCam = new CN_CatCampanas();
            int uen = !string.IsNullOrEmpty(ddlUEN.SelectedValue) ? Convert.ToInt32(ddlUEN.SelectedValue) : -1;
            int segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : -1;
            int area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : -1;
            int solucion = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : -1;
            int aplicacion = !string.IsNullOrEmpty(ddlAplicacion.SelectedValue) ? Convert.ToInt32(ddlAplicacion.SelectedValue) : -1;



            HiddenCampaniaId.Value = "0";
            HiddenCampania.Value = "";
            txtCampania.Text = "";
            CheckPerteneceCampania.Checked = false;
            Campanas Campana = new Campanas();
            Campana.Id_Emp = sesion.Id_Emp;
            Campana.Id_Cd = session.Id_Cd;
            Campana.Id_Uen = uen;
            Campana.Id_Seg = segmento;
            Campana.Id_Area = area;
            Campana.Id_Sol = solucion;
            Campana.Id_Aplicacion = aplicacion;
            Campana.Cam_Aplicacion = ddlAplicacion.Text;
            clscrmCam.ConsultaCampanaOportunidad(ref Campana, sesion.Emp_Cnx);
            if (Campana.Id_Cam > 0)
            {
                CheckPerteneceCampania.Checked = false;
                HiddenCampaniaId.Value = Campana.Id_Cam.ToString();
                HiddenCampania.Value = Campana.Cam_Nombre;

                RadConfirm("Existe una Campaña \"" + Campana.Id_Cam + " - " + Campana.Cam_Nombre + "\" con Vigencia del " + Campana.Cam_FechaInicio.ToShortDateString() + " al " + Campana.Cam_FechaFin.ToShortDateString() + " Para esta Aplicación, ¿Desea relacionar este proyecto a la campaña? ");
            }
        
        }



        private void CalculaValor()
        {
            try
            {
                if (ddlAplicacion.SelectedValue != "-1")
                {
                    CN_CrmOportunidad crm = new CN_CrmOportunidad();
                    CrmOportunidades registros = new CrmOportunidades();
                    registros.ID_Area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
                    registros.Id_Sol = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : 0;
                    registros.Id_Seg = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                    registros.Id_Apl = !string.IsNullOrEmpty(ddlAplicacion.SelectedValue) ? Convert.ToInt32(ddlAplicacion.SelectedValue) : 0;
                    registros.Id_Cte = !string.IsNullOrEmpty(HiddenField1.Value) ? Convert.ToInt32(HiddenField1.Value) : 0;
                    registros.Id_Ter = !string.IsNullOrEmpty(ddlTerritorio.SelectedValue) ? Convert.ToInt32(ddlTerritorio.SelectedValue) : 0;
                    double valorTeorico = 0;
                    double valorObservado = 0;
                    double? Teorico = 0;
                    double? Observado = 0;
                    crm.ConsultaVPotencialCliente(session, registros, ref valorTeorico, ref valorObservado, ref  Teorico, ref  Observado);
                    if (Observado != null)                    
                        txtVPObservado.Text = (0).ToString("#,##0.00");
                        //lblVPObservado.Text = txtVPObservado.Text;
                    
                    if (Teorico != null)                    
                        txtVPTeorico.Text = ((double)valorTeorico).ToString("#,##0.00");
                        //lblVPTeorico.Text = txtVPTeorico.Text;
                    
                    txtVPObservado.ReadOnly = false;
                    //if (!string.IsNullOrEmpty(lblVPTeorico.Text))
                    //{
                    //    double valorteorico = 0;
                    //    if (Double.TryParse(lblVPTeorico.Text, out valorteorico))
                    //        txtVPTeorico.Text = (valorteorico * (valorObservado / 100)).ToString("#,##0.00");
                    //}
                    //if (!string.IsNullOrEmpty(lblVPObservado.Text))
                    //    txtVPObservado.Text = txtVPTeorico.Text;
                    //if (ddlAplicacion.SelectedIndex != -1)
                    //    lblVPObservado.Text = txtVPObservado.Text;
                    //txtVPObservado.Text = valorTeorico.ToString("#,##0.00");
                    //lblVPObservado.Text = txtVPObservado.Text;
                }
                else
                {
                    txtVPObservado.Value = null;
                    txtVPTeorico.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CrmOportunidades", "Id_Op", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void avances(int tipo, int valido)
        //{
        //    try
        //    {
        //        if (valido == 1)
        //            switch (tipo)
        //            {//Avance
        //                case 1://promocion
        //                    lblPresentacion.Text = DateTime.Now.ToShortDateString();
        //                    chkNegociacion.Enabled = true;
        //                    chkPresentacion.Enabled = false;
        //                    HiddenField3.Value = "2";
        //                    deshabilitar();
        //                    break;
        //                case 2://negociacion
        //                    lblPresentacion.Text = DateTime.Now.ToShortDateString();
        //                    lblNegociacion.Text = DateTime.Now.ToShortDateString();
        //                    chkNegociacion.Enabled = false;
        //                    chkPresentacion.Enabled = false;
        //                    chkCierre.Enabled = true;
        //                    txtVentaMensual.ReadOnly = true;
        //                    HiddenField3.Value = "3";
        //                    deshabilitar();
        //                    break;
        //                case 3://cierre
        //                    lblPresentacion.Text = DateTime.Now.ToShortDateString();
        //                    lblNegociacion.Text = DateTime.Now.ToShortDateString();
        //                    lblCierre.Text = DateTime.Now.ToShortDateString();
        //                    chkNegociacion.Enabled = false;
        //                    chkPresentacion.Enabled = false;
        //                    chkCierre.Enabled = false;
        //                    txtVentaMensual.ReadOnly = true;
        //                    HiddenField3.Value = "4";
        //                    deshabilitar();
        //                    break;
        //            }
        //        else
        //            switch (tipo)
        //            {//Cancelacion de avance
        //                case 1://presentacion
        //                    lblPresentacion.Text = "";
        //                    chkPresentacion.Checked = false;
        //                    chkNegociacion.Enabled = false;
        //                    chkCierre.Enabled = false;
        //                    HiddenField3.Value = "1";
        //                    break;
        //                case 2://negociacion
        //                    lblPresentacion.Text = DateTime.Now.ToShortDateString();
        //                    lblNegociacion.Text = "";
        //                    chkNegociacion.Checked = false;
        //                    chkPresentacion.Enabled = false;
        //                    chkNegociacion.Enabled = true;
        //                    chkCierre.Enabled = false;
        //                    HiddenField3.Value = "2";
        //                    break;
        //                case 3://cierre
        //                    lblPresentacion.Text = DateTime.Now.ToShortDateString();
        //                    lblNegociacion.Text = DateTime.Now.ToShortDateString();
        //                    lblCierre.Text = "";
        //                    chkCierre.Checked = false;
        //                    chkPresentacion.Enabled = false;
        //                    chkNegociacion.Enabled = false;
        //                    chkCierre.Enabled = true;
        //                    HiddenField3.Value = "3";
        //                    break;
        //            }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        private void deshabilitar()
        {
            lblMensaje.Text = "";
            ddlUEN.Enabled = false;
            ddlSegmento.Enabled = false;
            ddlTerritorio.Enabled = false;
            ddlArea.Enabled = false;
            ddlSolucion.Enabled = false;
            ddlAplicacion.Enabled = false;
            ibtnBuscarCliente.Enabled = false;
            txtCliente.ReadOnly = true;
        }
        private void btn_Click(object sender, EventArgs e)
        {
            //clientes();
        }
        #endregion

        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("oldConfirm('" + mensaje + "', confirmCallBackFn, 550, 200);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }

        private void RadConfirmx(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("oldConfirm('" + mensaje + "', callBackFn, 550, 200);");
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
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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
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
using System.Collections;
using CapaDatos;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;

namespace SIANWEB
{
    public partial class CapPagos_Lista : System.Web.UI.Page
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
        #endregion}
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                    if (!Page.IsPostBack)
                    {
                        Session["PosBackPagos" + Session.SessionID] = null;
                        ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();



                        CN_CapPago cn_cappago = new CN_CapPago();
                        Pago pag = new Pago();
                        pag.Id_Emp = sesion.Id_Emp;
                        pag.Id_Cd = sesion.Id_Cd_Ver;
                        int verificador = 0;
                        cn_cappago.PermitirExtemporaneo(pag, sesion.Emp_Cnx, ref verificador);

                        HF_EXT.Value = verificador.ToString();
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
                Sesion sesion2 = sesion;
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                if (sesion2.CalendarioIni >= txtFecha1.MinDate && sesion2.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion2.CalendarioIni;
                }
                if (sesion2.CalendarioFin >= txtFecha2.MinDate && sesion2.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion2.CalendarioFin;
                }
                sesion = sesion2;
                rgPago.Rebind();
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
                    rgPago.DataSource = GetList();
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
                this.rgPago.Rebind();
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
                if (txtPedido1.Value > txtPedido2.Value)
                {
                    Alerta("La clave de pago inicial no debe ser mayor a la clave de pago final");
                    return;
                }
                if (txtFecha1.SelectedDate > txtFecha2.SelectedDate)
                {
                    Alerta("La fecha inicial no debe ser mayor a la fecha final");
                    return;
                }
                this.rgPago.Rebind();
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
                        //Inicializar();
                        rgPago.Rebind();
                        CargarEstatus();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            string Id_Emp = sesion.Id_Emp.ToString();
            string Id_Cd = sesion.Id_Cd_Ver.ToString();

            try
            {
                Session["PosBackPagos" + Session.SessionID] = null;
                DateTime fechaPeriodoInicio = sesion.CalendarioIni;
                DateTime fechaPeriodoFinal = sesion.CalendarioFin;
                GridItem gi = e.Item;
                List<string> statusPosibles;
                switch (e.CommandName)
                {
                    case "Timbre":

                        statusPosibles = new List<string>() { "C" };
                        if (statusPosibles.Contains(gi.Cells[8].Text.ToUpper()))
                        {
                            RAM1.ResponseScripts.Add("OpenAlert('El pago se encuentra en estatus no válido para realizar la modificación','" + Id_Emp + "','" + Id_Cd + "','" + gi.Cells[2].Text + "','False','False')");
                            //Alerta("El pago se encuentra en estatus no válido para realizar la modificación");
                            e.Canceled = true;
                            return;
                        }
                        else
                        {
                            //if (!Convert.ToBoolean(gi.Cells[10].Text))
                            //{
                                if (!_PermisoModificar)
                                {
                                    RAM1.ResponseScripts.Add("OpenAlert('No tiene permiso para modificar el registro','" + Id_Emp + "','" + Id_Cd + "','" + gi.Cells[2].Text + "','False','False')");
                                    //Alerta("No tiene permiso para modificar el registro");
                                    e.Canceled = true;
                                    return;
                                }
                                Session["PostBackPagos" + Session.SessionID] = false;
                                RAM1.ResponseScripts.Add("return AbrirVentana_Pagos_Timbre('" + gi.Cells[2].Text + "','" + _PermisoGuardar + "','" + _PermisoModificar + "')");

                            //}
                        }

                        break;
                    case "Baja":
                        statusPosibles = new List<string>() { "C", "I" };
                        if (!statusPosibles.Contains(gi.Cells[8].Text.ToUpper()))
                        {
                            Alerta("El pago se encuentra en estatus no válido para realizar la baja");
                            e.Canceled = true;
                            return;
                        }
                        if (gi.Cells[3].Text.Replace("&nbsp;", "") != "")
                        {
                            Alerta("No es posible cancelar un pago externo");
                            e.Canceled = true;
                            return;
                        }
                        if (!Convert.ToBoolean(gi.Cells[10].Text))
                        {
                            if (Convert.ToDateTime(gi.Cells[12].Text) < fechaPeriodoInicio || Convert.ToDateTime(gi.Cells[12].Text) > fechaPeriodoFinal)
                            {
                                Alerta("El pago no se encuentra en el periodo actual");
                                e.Canceled = true;
                                return;
                            }
                        }
                        else
                        {
                            CN_CapPago cn_cappago = new CN_CapPago();
                            Pago pag = new Pago();
                            pag.Id_Emp = sesion.Id_Emp;
                            pag.Id_Cd = sesion.Id_Cd_Ver;
                            pag.Pag_Fecha = Convert.ToDateTime(gi.Cells[12].Text);
                            int verificador = 0;
                            cn_cappago.PermitirExtemporaneo(pag, sesion.Emp_Cnx, ref verificador);
                            if (!Convert.ToBoolean(verificador))
                            {
                                Alerta("Ya se ha efectuado el cierre extemporáneo de pagos, no es posible dar de baja el pago");
                                e.Canceled = true;
                                return;
                            }
                        }
                        if (!_PermisoEliminar)
                        {
                            Alerta("No tiene permiso para eliminar el registro");
                            e.Canceled = true;
                            return;
                        }
                        Baja(gi.Cells[2].Text.ToUpper());
                        break;

                    case "Imprimir":
                        statusPosibles = new List<string>() { "C", "I", "B" };
                        if (!statusPosibles.Contains(gi.Cells[8].Text.ToUpper()))
                        {
                            Alerta("El pago se encuentra en estatus no válido para realizar la impresión");
                            e.Canceled = true;
                            return;
                        }
                        if (!_PermisoImprimir)
                        {
                            Alerta("No tiene permiso para imprimir el registro");
                            e.Canceled = true;
                            return;
                        }
                        Imprimir(gi.Cells[2].Text.ToUpper(), gi.Cells[4].Text.ToUpper().Replace("&NBSP;", ""), gi.Cells[6].Text.ToUpper(), gi.Cells[14].Text.ToUpper());
                        break;

                    case "Editar":

                        statusPosibles = new List<string>() { "C" };
                        if (!statusPosibles.Contains(gi.Cells[8].Text.ToUpper()))
                        {
                            RAM1.ResponseScripts.Add("OpenAlert('El pago se encuentra en estatus no válido para realizar la modificación','" + Id_Emp + "','" + Id_Cd + "','" + gi.Cells[2].Text + "','False','False')");
                            //Alerta("El pago se encuentra en estatus no válido para realizar la modificación");
                            e.Canceled = true;
                            return;
                        }
                        if (!Convert.ToBoolean(gi.Cells[10].Text))
                        {
                            if (Convert.ToDateTime(gi.Cells[12].Text) < fechaPeriodoInicio || Convert.ToDateTime(gi.Cells[12].Text) > fechaPeriodoFinal)
                            {
                                RAM1.ResponseScripts.Add("OpenAlert('El pago no se encuentra en el periodo actual','" + Id_Emp + "','" + Id_Cd + "','" + gi.Cells[2].Text + "','False','False')");
                                //Alerta("El pago no se encuentra en el periodo actual");
                                e.Canceled = true;
                                return;
                            }
                            if (!_PermisoModificar)
                            {
                                RAM1.ResponseScripts.Add("OpenAlert('No tiene permiso para modificar el registro','" + Id_Emp + "','" + Id_Cd + "','" + gi.Cells[2].Text + "','False','False')");
                                //Alerta("No tiene permiso para modificar el registro");
                                e.Canceled = true;
                                return;
                            }
                            Session["PostBackPagos" + Session.SessionID] = false;
                            RAM1.ResponseScripts.Add("return AbrirVentana_Pagos_Edicion('" + gi.Cells[2].Text + "','" + _PermisoGuardar + "','" + _PermisoModificar + "')");
                        }
                        else
                        {
                            CN_CapPago cn_cappago = new CN_CapPago();
                            Pago pag = new Pago();
                            pag.Id_Emp = sesion.Id_Emp;
                            pag.Id_Cd = sesion.Id_Cd_Ver;
                            pag.Pag_Fecha = Convert.ToDateTime(gi.Cells[12].Text);
                            int verificador = 0;
                            cn_cappago.PermitirExtemporaneo(pag, sesion.Emp_Cnx, ref verificador);
                            if (!Convert.ToBoolean(verificador))
                            {
                                RAM1.ResponseScripts.Add("OpenAlertExp('Ya se ha efectuado el cierre extemporáneo de pagos, no es posible modificar el pago','" + Id_Emp + "','" + Id_Cd + "','" + gi.Cells[2].Text + "','False','False')");
                                //Alerta("El pago no se encuentra en el periodo actual");
                                e.Canceled = true;
                                return;
                            }

                            if (!_PermisoModificar)
                            {
                                RAM1.ResponseScripts.Add("OpenAlertExp('No tiene permiso para modificar el registro','" + Id_Emp + "','" + Id_Cd + "','" + gi.Cells[2].Text + "','False','False')");
                                //Alerta("No tiene permiso para modificar el registro");
                                e.Canceled = true;
                                return;
                            }
                            Session["PostBackPagos" + Session.SessionID] = false;
                            RAM1.ResponseScripts.Add("return AbrirVentana_PagosExp_Edicion('" + gi.Cells[2].Text + "','" + _PermisoGuardar + "','" + _PermisoModificar + "')");
                        }

                        break;
                }
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
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName.ToLower())
                {
                    case "print":
                        Listado();
                        break;
                    case "new":
                        RAM1.ResponseScripts.Add("AbrirVentana_Pagos(-1, '" + _PermisoGuardar + "','" + _PermisoModificar + "');");
                        //Session["PostBackPagos" + Session.SessionID] = false;
                        break;
                    case "newext":
                        RAM1.ResponseScripts.Add("AbrirVentana_PagosExt(-1, '" + _PermisoGuardar + "', '" + _PermisoModificar + "');");
                        //Session["PostBackPagos" + Session.SessionID] = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rgPago_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = "";
                Button = (WebControl)item["Baja"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Pag").ToString());
                Button = (WebControl)item["Imprimir"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Pag").ToString());
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
        private void CargarCentros()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarEstatus()
        {
            cmbEstatus.Items.Clear();
            cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));
            cmbEstatus.Items.Add(new RadComboBoxItem("Capturado", "C"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Impreso", "I"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Baja", "B"));
            cmbEstatus.Sort = RadComboBoxSort.Ascending;
            cmbEstatus.SortItems();
        }
        private List<Pago> GetList()
        {
            try
            {
                List<Pago> List = new List<Pago>();
                CN_CapPago clsCapPago = new CN_CapPago();
                Pago pago = new Pago();
                pago.Id_Emp = sesion.Id_Emp;
                pago.Id_Cd = sesion.Id_Cd_Ver;
                pago.Filtro_FecIni = txtFecha1.SelectedDate;
                pago.Filtro_FecFin = txtFecha2.SelectedDate;
                pago.Filtro_Estatus = cmbEstatus.SelectedValue == "-1" ? "" : cmbEstatus.SelectedValue;
                pago.Filtro_PagIni = txtPedido1.Text;
                pago.Filtro_PagFin = txtPedido2.Text;
                pago.Filtro_usuario = sesion.Propia ? sesion.Id_U.ToString() : "";
                pago.Filtro_Extemporaneo = Convert.ToInt32(cmbExtemporaneo.SelectedValue);
                clsCapPago.ConsultaPago(pago, sesion.Emp_Cnx, ref List);
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
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }
                rgPago.Rebind();
                CargarEstatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Baja(string Id_Pag)
        {
            try
            {
                CN_CapPago cn_cappago = new CN_CapPago();
                Pago pag = new Pago();
                pag.Id_Emp = sesion.Id_Emp;
                pag.Id_Cd = sesion.Id_Cd_Ver;
                pag.Id_CdOrigen = sesion.Id_Cd_Ver;
                pag.Id_Pag = Convert.ToInt32(Id_Pag);
                int verificador = 0;
                cn_cappago.Baja(pag, sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {

                    int Tipo_CDC = 0;
                    new CN_CatCliente().ConsultaTipoCDC(sesion.Id_Cd_Ver, ref Tipo_CDC, sesion.Emp_Cnx);

                    WS_PagosExternos.Service1 ws = new WS_PagosExternos.Service1();
                    ws.Url = ConfigurationManager.AppSettings["WS_PagosExternos"].ToString();
                    ws.CancelarPagoExterno(Serialize(pag), Emp_CnxCob, sesion.Emp_Cnx, null, Tipo_CDC);

                    Alerta("Se dio de baja el pago #" + Id_Pag);
                    rgPago.Rebind();
                }
                else
                    Alerta("Ocurrió un error al intentar dar de baja");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string Serialize(object o)
        {
            var xs = new XmlSerializer(o.GetType());
            var xml = new StringWriter();
            xs.Serialize(xml, o);

            return xml.ToString();
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
                CapaNegocio.PaginaConsultar(ref pagina, sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = sesion.Id_U;
                Permiso.Id_Cd = sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla
                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[6].Visible = false;
                    //Guardar
                    this.rtb1.Items[5].Visible = false;
                    //Regresar
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = true;
                    //Correo
                    this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Listado()
        {
            try
            {
                ArrayList ALValorParametrosInternos = new ArrayList();
                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                string PagoStr = "";
                string FechaStr = "";
                ALValorParametrosInternos.Add(sesion.Emp_Cnx); //@Conexion
                ALValorParametrosInternos.Add(sesion.Id_Emp); //@Id_Emp
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver); //@Id_Cd
                ALValorParametrosInternos.Add(txtPedido1.Text == "" ? (object)null : txtPedido1.Text);
                ALValorParametrosInternos.Add(txtPedido2.Text == "" ? (object)null : txtPedido2.Text);
                if (txtPedido1.Text == "" && txtPedido2.Text == "") //@Id_Pag                   
                    ALValorParametrosInternos.Add("Todos");
                else
                {
                    PagoStr = txtPedido1.Text == "" ? "Del primer pago " : "Del " + txtPedido1.Text + " ";
                    PagoStr += txtPedido2.Text == "" ? "al ultimo pago" : " al " + txtPedido2.Text;
                    ALValorParametrosInternos.Add(PagoStr);
                }
                ALValorParametrosInternos.Add(txtFecha1.SelectedDate.HasValue ? txtFecha1.SelectedDate.Value : (object)null);
                ALValorParametrosInternos.Add(txtFecha2.SelectedDate.HasValue ? txtFecha2.SelectedDate.Value : (object)null);
                if (!txtFecha1.SelectedDate.HasValue && !txtFecha2.SelectedDate.HasValue)
                    ALValorParametrosInternos.Add("Todos");
                else
                {
                    FechaStr = txtFecha1.SelectedDate.HasValue ? "Del " + txtFecha1.SelectedDate.Value.ToString("dd/mm/yyyy") + " " : "";
                    FechaStr += txtFecha2.SelectedDate.HasValue ? "al " + txtFecha2.SelectedDate.Value.ToString("dd/mm/yyyy") : "";
                    ALValorParametrosInternos.Add(FechaStr); //@FechaIni
                }
                ALValorParametrosInternos.Add(cmbEstatus.SelectedValue == "" ? (object)null : cmbEstatus.SelectedValue); //@Estatus
                ALValorParametrosInternos.Add(cmbEstatus.SelectedValue == "" ? "Todos" : cmbEstatus.SelectedItem.Text); //@EstatusStr
                ALValorParametrosInternos.Add(sesion.Propia ? sesion.Id_U.ToString() : (object)null); //@Usuario

                Type instance = null;
                instance = typeof(LibreriaReportes.PagoListado);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Imprimir(string relacion, string foranea, string elaboro, string ejecutor)
        {
            try
            {
                ArrayList ALValorParametrosInternos = new ArrayList();

                string Orden = "Cliente y territorio";
                Orden = "Referencia";

                /*   PARAMETROS QUE LLENAN EL ENCABEZADO   */
                ALValorParametrosInternos.Add(relacion); // OPCION DE SELECCION POR RELACIONES
                ALValorParametrosInternos.Add(Orden); // PARAMETRO DE CALCULADO CON

                /*   PARAMETROS PARA LLENAR EL TITULO DEL ENCABEZADO   */
                ALValorParametrosInternos.Add(this.sesion.Emp_Nombre); // NOMBRE DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.Cd_Nombre); // UBICACION DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.U_Nombre); // NOMBRE DEL USUARIO

                /*   PARAMETROS OBLIGATORIOS PARA EL REPORTE   */
                ALValorParametrosInternos.Add(sesion.Emp_Cnx); // CADENA DE CONEXION A LA BASE DE DATOS
                ALValorParametrosInternos.Add(this.sesion.Id_Emp); // ID DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.Id_Cd_Ver); //ID DEL CENTRO DE DISTRIBUCION

                ///*   PARAMETROS QUE PUEDEN SER NULOS   */
                ALValorParametrosInternos.Add(relacion); //CADENA DE ID'S DE RELACIONES

                ALValorParametrosInternos.Add(foranea != "" ? elaboro + "\n\rZONA " + foranea : "Elaboro"); //Elaboro
                ALValorParametrosInternos.Add(foranea != "" ? "COBRANZA FORANEA \"ZONA " + foranea + "\"" : ejecutor != "0" ? "COBRANZA FORANEA \"EJECUTOR\"" : ""); //Foranea

                /*  MANDA LLAMAR EL REPORTE Y LO MUESTRA EN PANTALLA  */
                Type instance = null;
                instance = typeof(LibreriaReportes.Rep_CobRelacionCobranzaFto1);

                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");

                CN_CapPago cn_cappedido = new CN_CapPago();
                Pago pag = new Pago();
                pag.Id_Emp = this.sesion.Id_Emp;
                pag.Id_Cd = this.sesion.Id_Cd_Ver;
                pag.Id_Pag = Convert.ToInt32(relacion);
                int verificador = 0;
                cn_cappedido.Imprimir(pag, this.sesion.Emp_Cnx, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        //private void RadConfirm(string mensaje)
        //{
        //    try
        //    {

        //        RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, "Alerta");
        //    }

        //}
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
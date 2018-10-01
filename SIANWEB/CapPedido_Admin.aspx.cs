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

namespace SIANWEB
{
    public partial class CapPedido_Admin : System.Web.UI.Page
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
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = sesion;
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
                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
            if (txtCliente1.Value > txtCliente2.Value)
            {
                Alerta("El número de cliente inicial no puede ser mayor al número de cliente final");
                return;
            }
            if (txtFecha1.SelectedDate > txtFecha2.SelectedDate)
            {
                Alerta("La fecha inicial no debe ser mayor a la fecha final");
                return;
            }
            if (txtPedido1.Value > txtPedido2.Value)
            {
                Alerta("El número de pedido inicial no puede ser mayor al número de pedido final");
                return;
            }
            try
            {
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
                        rgPedido.Rebind();
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
                List<string> statusPosibles;
                switch (e.CommandName)
                {
                    case "BajaAdmin":
                        statusPosibles = new List<string>() { "R", "O", "A", "U", "I", "C" };
                        if (!statusPosibles.Contains(gi.Cells[5].Text.ToUpper()))
                        {
                            Alerta("El pedido se encuentra en estatus no válido para realizar la baja");
                            e.Canceled = true;
                            return;
                        }

                        if (gi.Cells[3].Text == "3")
                        {
                            Alerta("Pedido captado desde módulos para venta instalada, no es posible dar de baja");
                            e.Canceled = true;
                            return;
                        }
                        Baja("D", gi);
                        break;

                    case "BajaCliente":
                        statusPosibles = new List<string>() { "R", "O", "A", "U", "I", "C" };
                        if (!statusPosibles.Contains(gi.Cells[5].Text.ToUpper()))
                        {
                            Alerta("El pedido se encuentra en estatus no válido para realizar la baja");
                            e.Canceled = true;
                            return;
                        }
                        if (gi.Cells[3].Text == "3")
                        {
                            Alerta("Pedido captado desde módulos para venta instalada, no es posible dar de baja");
                            e.Canceled = true;
                            return;
                        }
                        Baja("B", gi);
                        break;

                    case "Imprimir":
                        if (!_PermisoImprimir)
                        {
                            Alerta("No tiene permiso para imprimir");
                            return;
                        }
                        statusPosibles = new List<string>() { "B", "D" };
                        if (statusPosibles.Contains(gi.Cells[5].Text.ToUpper()))
                        {
                            Alerta("El pedido se encuentra en estatus no válido para realizar la impresión");
                            e.Canceled = true;
                            return;
                        }
                        if (gi.Cells[14].Text.ToLower() == "false")
                        {
                            Alerta("CUIDADO: Este cliente se encuentra bloqueado por parte de cobranza; favor de aclarar su situación de créditos");
                        }
                        Imprimir(gi);
                        break;

                    case "Editar":
                        statusPosibles = new List<string>() { "C" };
                        bool modificar = _PermisoModificar;
                        bool guardar = _PermisoGuardar;
                        if (!statusPosibles.Contains(gi.Cells[5].Text.ToUpper()))
                        {
                            modificar = false;
                            guardar = false;
                        }
                        if (gi.Cells[3].Text == "3")
                        {
                            RAM1.ResponseScripts.Add("return AbrirVentana_ProPedidoVI('" + gi.Cells[7].Text + "', '" + guardar + "', '" + modificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')");
                            break;
                        }
                        else
                        {
                            RAM1.ResponseScripts.Add("return AbrirVentana_Pedidos('" + gi.Cells[7].Text + "', '" + guardar + "', '" + modificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')");
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
                RAM1.ResponseScripts.Add("return AbrirVentana_Pedidos('-1', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = "";

                    Button = (WebControl)item["BajaAdmin"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Ped").ToString());
                    if (item.GetDataKeyValue("Ped_Tipo").ToString() == "3")
                    {
                        Button.Visible = false;
                    }

                    Button = (WebControl)item["BajaCliente"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Ped").ToString());
                    if (item.GetDataKeyValue("Ped_Tipo").ToString() == "3")
                    {
                        Button.Visible = false;
                    }

                    Button = (WebControl)item["Imprimir"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Ped").ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUsuarios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.cmbUsuario);
                this.cmbUsuario.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipos()
        {
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add(new RadComboBoxItem("-- Todos --", ""));
            cmbTipo.Items.Add(new RadComboBoxItem("Venta instalada", "3"));
            cmbTipo.Items.Add(new RadComboBoxItem("Venta nueva", "4"));
            cmbTipo.Items.Add(new RadComboBoxItem("Sin distribución", "1"));
            cmbTipo.Items.Add(new RadComboBoxItem("Con distribución", "2"));
            cmbTipo.Sort = RadComboBoxSort.Ascending;
            cmbTipo.SortItems();
        }
        private void CargarEstatus()
        {
            cmbEstatus.Items.Clear();
            cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));
            cmbEstatus.Items.Add(new RadComboBoxItem("Confirmado", "O"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Capturado", "C"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Impreso", "I"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Autorizado", "U"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Asignado", "A"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Facturado", "F"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Remisionado", "R"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Facturado/Remisionado", "X"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Embarque", "E"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Entregado", "N"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Baja por administración", "D"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Baja por cliente", "B"));

            cmbEstatus.Sort = RadComboBoxSort.Ascending;
            cmbEstatus.SortItems();
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
                pedido.Filtro_Tipo = cmbTipo.SelectedValue == "-1" || cmbTipo.SelectedValue == "" ? (string)null : cmbTipo.SelectedValue;
                pedido.Filtro_FecIni = txtFecha1.SelectedDate;
                pedido.Filtro_FecFin = txtFecha2.SelectedDate;
                pedido.Filtro_Estatus = cmbEstatus.SelectedValue == "-1" || cmbEstatus.SelectedValue == "" ? (string)null : cmbEstatus.SelectedValue;
                pedido.Filtro_PedIni = txtPedido1.Value;
                pedido.Filtro_PedFin = txtPedido2.Value;
                pedido.Filtro_usuario = session2.Propia ? session2.Id_U.ToString() : cmbUsuario.SelectedValue;
                clsCatBanco.ConsultaPedido(pedido, session2.Emp_Cnx, ref List);
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
                drUsuario.Visible = !Sesion.Propia;
                btnBuscar1.Visible = Sesion.Propia;
                if (Sesion.CalendarioIni >= txtFecha1.MinDate && Sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                }
                if (Sesion.CalendarioFin >= txtFecha2.MinDate && Sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = Sesion.CalendarioFin;
                }
                double ancho = 0;
                foreach (GridColumn gc in rgPedido.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgPedido.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgPedido.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgPedido.Rebind();
                CargarUsuarios();
                CargarTipos();
                CargarEstatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Imprimir(GridItem gi)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Convert.ToInt32(gi.Cells[7].Text));

                Type instance = null;
                instance = typeof(LibreriaReportes.PedidoImpresion);
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
        private void Baja(string estatus, GridItem gi)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPedido cn_cappedido = new CN_CapPedido();
                Pedido ped = new Pedido();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Ped = Convert.ToInt32(gi.Cells[7].Text);
                ped.Estatus = estatus;
                int verificador = 0;
                cn_cappedido.Baja(ped, sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    Alerta("Se dio de baja el pedido #" + ped.Id_Ped.ToString());
                    RAM1.ResponseScripts.Add("refreshGrid();");
                }
                else if (verificador == -2)
                {
                    Alerta("El pedido se encuentra Facturado/Remisionado, no es posible darlo de baja");
                }
                else if (verificador == -3)
                {
                    Alerta("El pedido se encuentra Facturado, no es posible darlo de baja");
                }
                else if (verificador == -4)
                {
                    Alerta("El pedido se encuentra Remisionado, no es posible darlo de baja");
                }
                else
                {
                    Alerta("Ocurrió un error al intentar dar de baja");
                }
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
                        this.rtb1.Items[6].Visible = false;
                    else
                        this.rtb1.Items[6].Visible = true;
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
                    Response.Redirect("Inicio.aspx");

                if (Sesion.Id_Rik != -1 || Sesion.Id_TU == 2)
                { //Captura de pedidos por parte del representante
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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
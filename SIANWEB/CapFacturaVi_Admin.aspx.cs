using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using CapaDatos;
using System.Configuration;
using System.Xml;
using System.IO;


using System.Net;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;



namespace SIANWEB
{
    public partial class CapFacturaVi_Admin : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
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
        protected void rgPedido_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
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
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                GridItem gi = (Session["Gi" + Session.SessionID] as GridItem);
                string cmd = e.Argument.ToString();
                int verificador = 0;
                Funciones funcion = new Funciones();
                CN_ProDesasignaPedido_Aut cn_prodesasigna = new CN_ProDesasignaPedido_Aut();
                switch (cmd)
                {
                    case "RebindGrid":
                        if (Session["PreguntarImpresionVI" + Session.SessionID] != null)
                        {
                            RAM1.ResponseScripts.Add("return Confirma();");
                        }
                        if (Session["PreguntarImpresionVIRem" + Session.SessionID] != null)
                        {
                            RAM1.ResponseScripts.Add("return ConfirmaRem();");
                        }
                        rgPedido.Rebind();
                        break;
                    case "ok":
                        int vi = -1;
                        if (Session["PreguntarImpresionVI" + Session.SessionID] != null)
                        {
                            vi = Convert.ToInt32(Session["PreguntarImpresionVI" + Session.SessionID]);
                            Session["PreguntarImpresionVI" + Session.SessionID] = null;
                            ImprimirFactura(session.Id_Emp, session.Id_Cd_Ver, vi, "FACTURA", "");
                        }
                        break;
                    case "okRem":
                        int viRem = -1;
                        if (Session["PreguntarImpresionVIRem" + Session.SessionID] != null)
                        {
                            viRem = Convert.ToInt32(Session["PreguntarImpresionVIRem" + Session.SessionID]);
                            Session["PreguntarImpresionVIRem" + Session.SessionID] = null;
                            ImprimirRemision(viRem);
                            ImprimirContrato(viRem);
                        }
                        break;
                    case "no":
                        Session["PreguntarImpresionVI" + Session.SessionID] = null;
                        break;
                    case "AsManual":
                        RAM1.ResponseScripts.Add("return AbrirVentana_AsigPrdxPed('" + gi.Cells[3].Text + "','" + gi.Cells[8].Text + "','" + gi.Cells[9].Text + "')");
                        break;
                    case "DesManual":
                        RAM1.ResponseScripts.Add("return AbrirVentana_AsigPrdxPed('" + gi.Cells[3].Text + "','" + gi.Cells[8].Text + "','" + gi.Cells[9].Text + "')");
                        break;
                    case "AsAuto":
                        cn_prodesasigna.AsignacionPedido_Aut(session.Id_Emp, session.Id_Cd_Ver, funcion.GetLocalDateTime(session.Minutos), session.Id_U, gi.Cells[3].Text, ref verificador, session.Emp_Cnx);
                        if (verificador > 0)
                        {
                            Alerta("Asignación automática realizada exitosamente");
                        }
                        else
                        {
                            Alerta("No se cuenta con el inventario suficiente para realizar la asignación automática");
                        }
                        rgPedido.Rebind();
                        break;
                    case "DesAuto":
                        cn_prodesasigna.DesasignacionPedido_Aut(session.Id_Emp, session.Id_Cd_Ver, gi.Cells[3].Text, ref verificador, session.Emp_Cnx);
                        if (verificador == 1)
                        {
                            Alerta("Desasignación automática realizada exitosamente");
                        }
                        else
                        {
                            Alerta("Ocurrió un error al intentar realizar la desasignación automática");
                        }
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
                ErrorManager();
                GridItem gi = e.Item;
                string PermisoGuardar;
                string PermisoModificar;
                string PermisoEliminar;
                string PermisoImprimir;

                switch (e.CommandName)
                {
                    case "Asignar":
                        RAM1.ResponseScripts.Add("radconfirm('¿Desea asignar en forma automática?</br></br>',  confirmCallBackFn, 350, 150);");
                        Session["Gi" + Session.SessionID] = gi;
                        break;
                    case "Desasignar":
                        RAM1.ResponseScripts.Add("radconfirm('¿Desea desasignar en forma automática?</br></br>',  confirmCallBackFnDes, 350, 150);");
                        Session["Gi" + Session.SessionID] = gi;
                        break;
                    case "Facturar":
                        Session["PedidoVI" + Session.SessionID] = true;
                        Session["PedidoFacturacion" + Session.SessionID] = gi.Cells[3].Text;
                        PermisoGuardar = _PermisoGuardar ? "1" : "0";
                        PermisoModificar = _PermisoModificar ? "1" : "0";
                        PermisoEliminar = _PermisoEliminar ? "1" : "0";
                        PermisoImprimir = _PermisoImprimir ? "1" : "0";
                        RAM1.ResponseScripts.Add("return AbrirVentana_Factura('0','0','0','" + PermisoGuardar + "', '" + PermisoModificar + "', '" + PermisoEliminar + "','" + PermisoImprimir + "')");
                        break;
                    case "Remisionar":
                        Session["PedidoVI" + Session.SessionID] = true;
                        Session["PedidoRemision" + Session.SessionID] = gi.Cells[3].Text;
                        RAM1.ResponseScripts.Add("return AbrirVentana_Remision(" + gi.Cells[3].Text + "," + _PermisoGuardar.ToString().ToLower() + ")");
                        break;
                    case "Baja":
                        CN_CapPedido cn_cappedidovtainst = new CN_CapPedido();
                        Pedido ped = new Pedido();
                        ped.Id_Emp = session.Id_Emp;
                        ped.Id_Cd = session.Id_Cd_Ver;
                        ped.Id_Ped = Convert.ToInt32(gi.Cells[3].Text);
                        int verificador = -1;
                        cn_cappedidovtainst.Baja(ped, session.Emp_Cnx, ref verificador);
                        if (verificador == 1)
                        {
                            Alerta("El pedido ha sido dado de baja");
                        }
                        else if (verificador == -2)
                        {
                            Alerta("El pedido ya ha sido facturado/remisionado parcialmente, no es posible darlo de baja");
                        }
                        else if (verificador == -3)
                        {
                            Alerta("El pedido ya ha sido facturado parcialmente, no es posible darlo de baja");
                        }
                        else if (verificador == -4)
                        {
                            Alerta("El pedido ya ha sido remisionado parcialmente, no es posible darlo de baja");
                        }
                        else
                            Alerta("Ocurrió un error al intentar dar de baja el pedido");
                        rgPedido.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = session;
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                if (sesion2.CalendarioIni >= txtFecha1.MinDate && sesion2.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion2.CalendarioIni;
                }
                if (sesion2.CalendarioFin >= txtFecha2.MinDate && sesion2.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion2.CalendarioFin;
                }

                if (sesion2.CalendarioIni >= txtFecha1.MinDate && sesion2.CalendarioIni <= txtFechaFIni.MaxDate)
                {
                    txtFechaFIni.DbSelectedDate = sesion2.CalendarioIni;
                }
                if (sesion2.CalendarioFin >= txtFecha2.MinDate && sesion2.CalendarioFin <= txtFechaFFin.MaxDate)
                {
                    txtFechaFFin.DbSelectedDate = sesion2.CalendarioFin;
                }


                session = sesion2;

                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ErrorManager();
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
                rgPedido.Rebind();



            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgPedido_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = "";
                    Button = (WebControl)item["Baja"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", "<b>#" + item.GetDataKeyValue("Id_Ped").ToString() + "</b> de <b>" + item.GetDataKeyValue("Cte_Nom").ToString() + "</b>");

                }
                rgPedido.Columns.FindByUniqueName("BtnFacturar").Display = rdFactura.Checked; //.Columns[13].Visible = rdFactura.Checked; //Facturar
                rgPedido.Columns.FindByUniqueName("BtnRemisionar").Display = !rdFactura.Checked;//[14].Visible = !rdFactura.Checked;//Remisionar
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void SelTodo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                for (int x = 0; x < rgPedido.Items.Count; x++)
                    (rgPedido.Items[x].FindControl("chkSeleccionar") as CheckBox).Checked = (sender as CheckBox).Checked && (rgPedido.Items[x].FindControl("chkSeleccionar") as CheckBox).Visible;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Sel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                CheckBox chk = sender as CheckBox;
                if ((chk.Parent.Parent as GridDataItem).Cells[11].Text == "N")
                {
                    Alerta("El pedido no tiene ninguna partida asignada");
                    chk.Checked = false;
                    return;
                }
                if ((chk.Parent.Parent as GridDataItem).Cells[18].Text == "true")
                {
                    Alerta("Este cliente no está autorizado para facturarle; Para poder facturarle favor de entrar al catálogo de clientes en la pestaña de parámetros, y activar la opción \"Permitir facturar\"");
                    chk.Checked = false;
                    return;
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
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "generarF":
                        GenerarFacturas();
                        break;
                    case "generarR":
                        NuevaRemision();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
                
        protected void rgPedido_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            rgPedido.Rebind();
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                CargarAsignar();
                Session["PedidoFacturacion" + Session.SessionID] = null;

                if (session.CalendarioIni >= txtFecha1.MinDate && session.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = session.CalendarioIni;
                }
                if (session.CalendarioFin >= txtFecha2.MinDate && session.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = session.CalendarioFin;
                }


                if (session.CalendarioIni >= txtFecha1.MinDate && session.CalendarioIni <= txtFechaFIni.MaxDate)
                {
                    txtFechaFIni.DbSelectedDate = session.CalendarioIni;
                }
                if (session.CalendarioFin >= txtFecha2.MinDate && session.CalendarioFin <= txtFechaFFin.MaxDate)
                {
                    txtFechaFFin.DbSelectedDate = session.CalendarioFin;
                }

                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAsignar()
        {
            try
            {
                cmbEstatus.Items.Clear();
                cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));
                cmbEstatus.Items.Add(new RadComboBoxItem("No", "N"));
                cmbEstatus.Items.Add(new RadComboBoxItem("Si", "S"));
                cmbEstatus.Items.Add(new RadComboBoxItem("Parcial", "P"));
                cmbEstatus.Sort = RadComboBoxSort.Ascending;
                cmbEstatus.SortItems();
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
        private List<PedidoVtaInst> GetList()
        {
            try
            {
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                CN_CapPedidoVtaInst clsCatBanco = new CN_CapPedidoVtaInst();

                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Filtro_Nombre = txtNombre.Text;
                pedido.Filtro_CteIni = txtCliente1.Value.ToString();
                pedido.Filtro_CteFin = txtCliente2.Value.ToString();
                pedido.Filtro_Tipo = rdFactura.Checked ? "F" : "R";
                pedido.Filtro_FecIni = txtFecha1.SelectedDate;
                pedido.Filtro_FecFin = txtFecha2.SelectedDate;
                pedido.Filtro_FecFIni = txtFechaFIni.SelectedDate;
                pedido.Filtro_FecFFin = txtFechaFFin.SelectedDate;
                pedido.Filtro_Estatus = cmbEstatus.SelectedValue == "-1" || cmbEstatus.SelectedValue == "" ? (string)null : cmbEstatus.SelectedValue;
                pedido.Filtro_usuario = session.Propia ? session.Id_U.ToString() : (string)null;
                pedido.Filtro_Documento = rdFactura.Checked ? "F" : "R";
                clsCatBanco.ListaFacturacion(pedido, session.Emp_Cnx, ref List);

                return List;
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

                ////Captura de pedidos por parte del representante
                //CN_CatCentroDistribucion catcentro = new CN_CatCentroDistribucion();
                //CentroDistribucion cd = new CentroDistribucion();
                //catcentro.ConsultarCentroDistribucion(ref cd, Sesion.Id_Cd_Ver, Sesion.Id_Emp, Sesion.Emp_Cnx);

                //if (!cd.Cd_ActivaCapPedRep)
                //{
                //    this.rtb1.Items[6].Visible = false;
                //    //rgPedido.Columns[12].Visible = false;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //REMISION
        private void GenerarRemisiones()
        {
            try
            {
                for (int x = 0; x < rgPedido.Items.Count; x++)
                {
                    if ((rgPedido.Items[x].FindControl("chkSeleccionar") as CheckBox).Checked)
                        GuardarRemision(Convert.ToInt32(rgPedido.Items[x]["Id_Ped"].Text));
                }
                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void NuevaRemision()
        {
            Session["PedidoVI" + Session.SessionID] = true;
            RAM1.ResponseScripts.Add("return AbrirVentana_Pagos(1, -1, true)");
        }

        private void GuardarRemision(int? pedidoPrevio)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                EntradaSalida entSal = new EntradaSalida();
                List<EntradaSalidaDetalle> listaEntSal = new List<EntradaSalidaDetalle>();
                List<EntradaSalida> listaEntSalRemisiones = new List<EntradaSalida>();
                string productosFactura = string.Empty; //para las notas de la entrada-salida

                //llenar datos de factura
                Remision remision = new Remision();
                this.LlenarRemision(ref remision, pedidoPrevio);
                List<RemisionDet> listaRemisionDet = ObtenerDetalleRemision(pedidoPrevio);

                int verificador = -1;
                CN_CapRemision cn_capRemision = new CN_CapRemision();
                int Id_Rem = 0;
                bool tipoMovimento = false;
                string mensaje = "";
                cn_capRemision.GuardarRemision(remision, listaRemisionDet, session, ref verificador, false, true, ref Id_Rem, ref tipoMovimento, ref mensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<RemisionDet> ObtenerDetalleRemision(int? pedidoPrevio)
        {
            try
            {
                List<RemisionDet> listaRemisionDet = new List<RemisionDet>();
                DataTable dtPedido = new DataTable();
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = (int)pedidoPrevio;
                pedido.Filtro_Doc = "R";

                DataTable tabla = new DataTable();
                tabla.Columns.Add("Id_PedDet");
                tabla.Columns.Add("Id_Ter");
                tabla.Columns.Add("Ter_Nombre");
                tabla.Columns.Add("Id_Prd");
                tabla.Columns.Add("Prd_Descripcion");
                tabla.Columns.Add("Prd_Presentacion");
                tabla.Columns.Add("Prd_Unidad");
                tabla.Columns.Add("Prd_Precio");
                tabla.Columns.Add("Prd_Cantidad");//en lugar de la cantidad, cargar "disponible de remision"
                tabla.Columns.Add("Prd_Importe");
                tabla.Columns.Add("Id_Rem");
                tabla.DefaultView.Sort = "Id_PedDet";
                new CN_CapPedido().ConsultaPedidoDetDisp(pedido, ref tabla, null, session.Emp_Cnx);

                RemisionDet remdetalle;
                foreach (DataRow row in tabla.Rows)
                {
                    remdetalle = new RemisionDet();
                    remdetalle.Id_Emp = session.Id_Emp;
                    remdetalle.Id_Cd = session.Id_Cd_Ver;
                    //remdetalle.Id_Rem=se asigna del valor que devuelve el inser de la remision
                    remdetalle.Id_RemDet = int.Parse(row["Id_PedDet"].ToString());
                    remdetalle.Id_Ter = int.Parse(row["Id_Ter"].ToString());
                    remdetalle.Id_Prd = int.Parse(row["Id_Prd"].ToString());
                    remdetalle.Rem_Cant = int.Parse(row["Prd_Cantidad"].ToString());
                    remdetalle.Rem_Precio = double.Parse(row["Prd_Precio"].ToString());
                    //si es edicion de remision de pedido
                    remdetalle.Ped_Pertenece = true;
                    listaRemisionDet.Add(remdetalle);
                }
                return listaRemisionDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LlenarRemision(ref Remision remision, int? pedidoFacturacion)
        {
            try
            {
                Funciones funcion = new Funciones();

                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = (int)pedidoFacturacion;
                new CN_CapPedido().ConsultaPedido(ref pedido, session.Emp_Cnx);

                Clientes cliente = new Clientes();
                cliente.Id_Emp = session.Id_Emp;
                cliente.Id_Cd = session.Id_Cd_Ver;
                cliente.Id_Cte = pedido.Id_Cte;

                new CN_CatCliente().ConsultaClientes(ref cliente, session.Emp_Cnx);

                remision.Id_Emp = session.Id_Emp;
                remision.Id_Cd = session.Id_Cd_Ver;
                remision.Id_Rem = -1; //cambia cuando se inserta la remision si es nueva, permanece si se modifica
                remision.Rem_ManAut = 1; // manual
                remision.Rem_Tipo = "3"; // 3=remision
                remision.Rem_Fecha = funcion.GetLocalDateTime(session.Minutos);// ????????????????
                remision.Rem_FechaEntrega = funcion.GetLocalDateTime(session.Minutos).AddDays(1);// ????????????????
                remision.Id_Ped = (int)pedidoFacturacion;
                remision.Id_Cte = pedido.Id_Cte;
                remision.Id_Ter = pedido.Id_Ter;
                remision.Id_Rik = pedido.Id_Rik;
                remision.Id_U = session.Id_U;
                remision.Rem_Calle = pedido.Ped_ConsignadoCalle;
                remision.Rem_Numero = pedido.Ped_ConsignadoNo;
                remision.Rem_Cp = pedido.Ped_ConsignadoCp;
                remision.Rem_Colonia = pedido.Ped_ConsignadoColonia;
                remision.Rem_Municipio = pedido.Ped_ConsignadoMunicipio;
                remision.Rem_Estado = pedido.Ped_ConsignadoEstado;
                remision.Rem_Rfc = cliente.Cte_DRfc == string.Empty ? cliente.Cte_FacRfc : cliente.Cte_DRfc;
                remision.Rem_Telefono = cliente.Cte_Telefono;
                remision.Rem_Guia = "";
                remision.Rem_Nota = pedido.Ped_Observaciones;
                remision.Rem_Contacto = pedido.Ped_Solicito;
                remision.Rem_Conducto = pedido.Ped_Solicito;
                remision.Rem_Total = Convert.ToSingle(pedido.Ped_Importe);
                remision.Rem_Subtotal = Convert.ToSingle(pedido.Ped_Subtotal);
                remision.Rem_Iva = Convert.ToSingle(pedido.Ped_Iva);
                remision.Id_Tm = 17; //CAMBIAR
                remision.Rem_Estatus = "C";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //FACTURACION
        private void GenerarFacturas()
        {
            try
            {
                for (int x = 0; x < rgPedido.Items.Count; x++)
                {
                    if ((rgPedido.Items[x].FindControl("chkSeleccionar") as CheckBox).Checked)
                        GuardarFactura(Convert.ToInt32(rgPedido.Items[x]["Id_Ped"].Text));
                }
                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GuardarFactura(int? pedidoPrevio)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                EntradaSalida entSal = new EntradaSalida();
                List<EntradaSalidaDetalle> listaEntSal = new List<EntradaSalidaDetalle>();
                List<EntradaSalida> listaEntSalRemisiones = new List<EntradaSalida>();
                string productosFactura = string.Empty; //para las notas de la entrada-salida

                //llenar datos de factura
                Factura factura = new Factura();
                if (this.LlenarFactura(ref factura, pedidoPrevio))
                {
                    return;
                }

                ////***--------------------------------------***
                ////***          GUARDAR FACTURA             ***
                ////***--------------------------------------***
                int verificador = -1;
                //// ---------------
                //// NUEVA FACTURA
                //// ---------------
                DataTable listaFacturaDet = ObtenerDetalleFacturacion(pedidoPrevio);
                DataTable listaFacturaDetAdenda = new DataTable();
                EntradaSalida es = new EntradaSalida();
                List<EntradaSalidaDetalle> listes = new List<EntradaSalidaDetalle>();
                List<AdendaDet> listAdenda = new List<AdendaDet>();
                List<FacturaDet> listaProductosFacturaEspecial = new List<FacturaDet>();
                FacturaEspecial facturaEsp = null;


                new CN_CapFactura().InsertarFactura(session, ref factura
                    , ref listaFacturaDet
                    ,ref listaFacturaDetAdenda
                    , 0
                    , session.Emp_Cnx
                    , ref verificador
                    , ref pedidoPrevio
                    , ref listaEntSalRemisiones, listAdenda, "", "", ref es, ref  listes, ref facturaEsp, ref listaProductosFacturaEspecial, false );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable ObtenerDetalleFacturacion(int? pedidoPrevio)
        {
            try
            {
                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                DataTable dtPedido = new DataTable();
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = (int)pedidoPrevio;
                pedido.Filtro_Doc = "F";

                dtPedido.Columns.Add("Id_PedDet", typeof(int));
                dtPedido.Columns.Add("Id_Ter", typeof(int));
                dtPedido.Columns.Add("Ter_Nombre", typeof(string));
                dtPedido.Columns.Add("Id_Prd", typeof(int));
                dtPedido.Columns.Add("Prd_Descripcion", typeof(string));
                dtPedido.Columns.Add("Prd_Presentacion", typeof(string));
                dtPedido.Columns.Add("Prd_Unidad", typeof(string));
                dtPedido.Columns.Add("Prd_Precio", typeof(double));
                dtPedido.Columns.Add("Prd_Cantidad", typeof(int));
                dtPedido.Columns.Add("Prd_Importe", typeof(double));
                new CN_CapPedido().ConsultaPedidoDet(pedido, ref dtPedido, session.Emp_Cnx);
                return dtPedido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool LlenarFactura(ref Factura factura, int? pedidoFacturacion)
        {
            try
            {
                Funciones funcion = new Funciones();

                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = (int)pedidoFacturacion;
                new CN_CapPedido().ConsultaPedido(ref pedido, session.Emp_Cnx);

                Clientes cliente = new Clientes();
                cliente.Id_Emp = session.Id_Emp;
                cliente.Id_Cd = session.Id_Cd_Ver;
                cliente.Id_Cte = pedido.Id_Cte;
                new CN_CatCliente().ConsultaClientes(ref cliente, session.Emp_Cnx);
                if (cliente.Id_Cfe == -1)
                {
                    Alerta("El cliente no tiene consecutivo de facturación electrónica asignado");
                    return true;
                }

                factura.Id_Emp = session.Id_Emp;
                factura.Id_Cd = session.Id_Cd_Ver;
                factura.Id_Fac = -1; //cambia cuando se inserta la factura si es nueva, permanece si se modifica
                factura.Id_Cfe = cliente.Id_Cfe;
                factura.Id_FacSerie = cliente.FacSerie;
                factura.Id_U = session.Id_U;
                factura.Id_Tm = 51;
                factura.Fac_PedNum = pedidoFacturacion;
                factura.Fac_PedDesc = ""; // ????????????????
                factura.Fac_Req = ""; // ????????????????
                factura.Fac_Fecha = funcion.GetLocalDateTime(session.Minutos);// ????????????????
                factura.Id_Cte = pedido.Id_Cte;
                factura.Id_Ter = pedido.Id_Ter;
                factura.Id_Rik = pedido.Id_Rik;

                factura.Id_Mon = cliente.Id_Mon;
                factura.Fac_DesgIva = cliente.Cte_DesgIva;
                factura.Fac_RetIva = cliente.Cte_RetIva;
                factura.Fac_CteCalle = pedido.Ped_ConsignadoCalle;
                factura.Fac_CteNumero = pedido.Ped_ConsignadoNo;
                factura.Fac_CteCp = pedido.Ped_ConsignadoCp;
                factura.Fac_CteColonia = pedido.Ped_ConsignadoColonia;
                factura.Fac_CteMunicipio = pedido.Ped_ConsignadoMunicipio;
                factura.Fac_CteEstado = pedido.Ped_ConsignadoEstado;
                factura.Fac_CteRfc = cliente.Cte_DRfc == string.Empty ? cliente.Cte_FacRfc : cliente.Cte_DRfc;
                factura.Fac_CteTel = cliente.Cte_Telefono;
                factura.Fac_OrdEntrega = "0"; // ????????????????
                factura.Fac_NumeroGuia = "";
                factura.Fac_CondEntrega = pedido.Ped_CondEntrega.ToString();
                factura.Fac_NumEntrega = 0; // ????????????????
                factura.Fac_Notas = pedido.Ped_Observaciones;
                factura.Fac_DescPorcen1 = Convert.ToSingle(pedido.Ped_DescPorcen1);
                factura.Fac_DescPorcen2 = Convert.ToSingle(pedido.Ped_DescPorcen2);
                factura.Fac_Desc1 = pedido.Ped_Desc1;
                factura.Fac_Desc2 = pedido.Ped_Desc2;

                factura.Fac_Tipo = "VI";
                factura.Fac_Conducto = pedido.Ped_Solicito;
                factura.Fac_CanNum = null; //????

                factura.Fac_FecCan = null;
                factura.Fac_Pagado = 0;
                factura.Fac_FecPag = funcion.GetLocalDateTime(session.Minutos);

                factura.Fac_Importe = Convert.ToSingle(pedido.Ped_Importe);
                factura.Fac_SubTotal = Convert.ToSingle(pedido.Ped_Subtotal);
                factura.Fac_ImporteIva = Convert.ToSingle(pedido.Ped_Iva);

                factura.Fac_Estatus = "C";
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImprimirRemision(int Id_Rem)
        {
            try
            {
                ///Valida que el estatus esté en capturado, impreso, embarcado o entregado.
                ///Que sea diferente a baja
                ///Lo manda a imprimir, y se cambia el estatus a impreso.
                //List<string> statusPosibles = new List<string>() { "C", "I", "E", "N" };
                //if (!statusPosibles.Contains(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.Trim().ToUpper()))
                //{
                //    Alerta("El documento se encuentra en estatus no válido");
                //    e.Canceled = true;
                //    return;
                //}

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Emp = sesion.Id_Emp;
                int Id_Cd_Ver = sesion.Id_Cd_Ver;                
                                
                Remision remision = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 0);
                ArrayList ALValorParametrosInternos = new ArrayList();

                // Validar si la Remisión es Válida o no en base a la suma de los montos en las partidas de la remisión y la remisión especial.
                bool bRemisionValida = false;
                new CN_CapRemision().ValidaMontosImpresion(remision, sesion.Id_Cd_Ver, sesion.Id_Emp, 1, sesion.Emp_Cnx, ref bRemisionValida);

                if (bRemisionValida)
                {
                    if (remision.Id_Tm == 54 || remision.Id_Tm == 80)
                    {
                        CN_CatCliente cn_catcliente = new CN_CatCliente();
                        Clientes cliente = new Clientes();
                        cliente.Id_Emp = sesion.Id_Emp;
                        cliente.Id_Cd = sesion.Id_Cd_Ver;
                        cliente.Id_Cte = remision.Id_Cte;
                        cn_catcliente.ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                        if (remision.Rem_CteCuentaNacional != 0 || cliente.Cte_RemisionElectronica > 0)
                        {
                            if (cliente.Cte_RemisionElectronica > 0 && remision.Rem_CteCuentaNacional == null || remision.Rem_CteCuentaNacional == 1)
                            {
                                ImprimirRemisionElectronicaPIPES(remision);
                            }
                            else
                            {
                                ImprimirRemisionElectronica(remision);
                            }

                            return;
                        }
                    }

                    ////Consulta centro de distribución               
                    ALValorParametrosInternos.Add(remision.Id_Emp);
                    ALValorParametrosInternos.Add(remision.Rem_Calle);
                    if (!string.IsNullOrEmpty(remision.Rem_Numero))
                        remision.Rem_Numero = "# " + remision.Rem_Numero;
                    ALValorParametrosInternos.Add(remision.Rem_Numero);
                    ALValorParametrosInternos.Add(remision.Rem_Colonia);
                    ALValorParametrosInternos.Add(remision.Rem_Municipio);
                    ALValorParametrosInternos.Add(remision.Rem_Estado);
                    if (!string.IsNullOrEmpty(remision.Rem_Cp))
                        if (!remision.Rem_Cp.Contains("C.P."))
                            remision.Rem_Cp = "C.P. " + remision.Rem_Cp;
                    ALValorParametrosInternos.Add(remision.Rem_Cp);
                    ALValorParametrosInternos.Add(remision.Id_Rem);
                    ALValorParametrosInternos.Add(remision.Rem_Fecha.ToShortDateString());
                    ALValorParametrosInternos.Add((remision.Rem_FechaHr == null) ? "00:00" : remision.Rem_FechaHr.Value.ToShortTimeString());
                    ALValorParametrosInternos.Add(remision.Cte_NomComercial);
                    ALValorParametrosInternos.Add((remision.Id_Ped == 0 || remision.Id_Ped == -1) ? string.Empty : remision.Id_Ped.ToString());
                    ALValorParametrosInternos.Add(remision.Rem_Conducto);
                    ALValorParametrosInternos.Add(remision.Cte_CondPago);

                    ALValorParametrosInternos.Add(remision.Cte_Calle);
                    ALValorParametrosInternos.Add(remision.Cte_Numero);
                    ALValorParametrosInternos.Add(remision.Cte_Colonia);

                    ALValorParametrosInternos.Add(remision.Id_Cte);
                    ALValorParametrosInternos.Add(remision.Id_Tm);
                    ALValorParametrosInternos.Add(remision.Tm_Nombre);
                    ALValorParametrosInternos.Add(remision.Id_Cd);
                    ALValorParametrosInternos.Add((remision.Id_Ter == -1) ? string.Empty : remision.Id_Ter.ToString());
                    ALValorParametrosInternos.Add(remision.Id_Rik == -1 ? string.Empty : remision.Id_Rik.ToString());

                    ALValorParametrosInternos.Add(remision.Rik_Calle);
                    ALValorParametrosInternos.Add(remision.Rik_Numero);
                    ALValorParametrosInternos.Add(remision.Rik_Colonia);

                    ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}", remision.Rem_Subtotal));
                    ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}", remision.Rem_Iva));
                    ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}", remision.Rem_Total));
                    ALValorParametrosInternos.Add(remision.Rem_OrdenCompra);
                    ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                    StringBuilder NotaProductosOriginales = new StringBuilder();

                    if (remision.Rem_Especial > 0)
                    {
                        List<RemisionDet> detalles = new List<RemisionDet>();
                        new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);

                        foreach (RemisionDet detalle in detalles)
                        {
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(detalle.Id_Prd.ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(Math.Round(detalle.Rem_Precio, 2).ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(detalle.Rem_Cant.ToString());
                        }
                    }
                    ALValorParametrosInternos.Add(remision.Rem_Nota + NotaProductosOriginales.ToString());



                    ALValorParametrosInternos.Add("pp");
                    ALValorParametrosInternos.Add("jj");

                    ALValorParametrosInternos.Add("jj");
                    ALValorParametrosInternos.Add("jj");
                    ALValorParametrosInternos.Add("ks");
                    ALValorParametrosInternos.Add("sd");
                    ALValorParametrosInternos.Add("sd");
                    ALValorParametrosInternos.Add("sd");
                    ALValorParametrosInternos.Add("sds");
                    ALValorParametrosInternos.Add("sd");


                    Type instance = null;
                    instance = typeof(LibreriaReportes.RemisionImpresion);
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                    //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
                else
                {
                    RAM1.ResponseScripts.Add("return OpenWindow('" + 2 + "','" + Id_Rem + "','" + true + "','" + "Los montos de la Remisión y la Remisión Especial no coinciden" + "')");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImprimirContrato(int Id_Rem)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Emp = sesion.Id_Emp;// int.Parse(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Emp"].Text);
                int Id_Cd_Ver = sesion.Id_Cd_Ver;// int.Parse(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Cd"].Text);

                Remision remision = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 1);

                List<string> statusPosibles = new List<string>() { "60" };
                if (!statusPosibles.Contains(remision.Id_Tm.ToString()))
                {                    
                    return;
                }
                ///Valida que el estatus esté en capturado, impreso, embarcado o entregado.
                ///Que sea diferente a baja
                statusPosibles = new List<string>() { "C", "I", "E", "N" };
                if (!statusPosibles.Contains(remision.Rem_Estatus))
                {                    
                    return;
                }
                
                ArrayList ALValorParametrosInternos = new ArrayList();

                ////Consulta centro de distribución
                ALValorParametrosInternos.Add(remision.Id_Emp);
                ALValorParametrosInternos.Add(remision.Emp_Nombre);
                ALValorParametrosInternos.Add(remision.Id_Cd);
                ALValorParametrosInternos.Add(remision.Id_Rem);
                ALValorParametrosInternos.Add(remision.Id_Cte);
                ALValorParametrosInternos.Add(remision.Rik_Nombre);

                ALValorParametrosInternos.Add(remision.Cte_NomComercial);
                //
                ALValorParametrosInternos.Add(remision.Rem_Fecha.ToShortDateString());
                ALValorParametrosInternos.Add(remision.Rem_Calle + " " + remision.Rem_Numero + " " + remision.Rem_Colonia + " " + remision.Rem_Municipio + " " + remision.Rem_Cp);

                CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
                CentroDistribucion oficina = new CentroDistribucion();
                cn_cd.ConsultarCentroDistribucion(ref oficina, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(oficina.Cd_Calle);
                ALValorParametrosInternos.Add(oficina.Cd_Numero);
                ALValorParametrosInternos.Add(oficina.Cd_Colonia);
                ALValorParametrosInternos.Add(oficina.Cd_Municipio);
                string Mes = string.Empty;
                MesNombre(remision.Rem_Fecha.Month, ref Mes);
                ALValorParametrosInternos.Add(Mes);

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(remision.NumContratoImpresion == string.Empty ? (object)null : remision.NumContratoImpresion);
                ALValorParametrosInternos.Add("ss");
                Type instance = null;
                instance = typeof(LibreriaReportes.RemisionContratoComodato);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value + "nf"] = null;
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value + "nf"] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value + "nf"] = instance.AssemblyQualifiedName;

                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "nf" + "');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImprimirFactura(int Id_Emp, int Id_Cd, int Id_Fac, string movimiento, string agregado_nota_cancelacion, bool tienePDF = false)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                int verificador = 0;

                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                CN_CapFactura cn_factura = new CN_CapFactura();
                Factura factura = new Factura();
                Factura facturaNacional = new Factura();
                factura.Id_Emp = sesion.Id_Emp;
                factura.Id_Cd = sesion.Id_Cd_Ver;
                factura.Id_Fac = Id_Fac;

                facturaNacional.Id_Emp = sesion.Id_Emp;
                facturaNacional.Id_Cd = sesion.Id_Cd_Ver;
                facturaNacional.Id_Fac = Id_Fac;

                cn_factura.ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                cn_factura.ConsultaFacturaNacional(ref facturaNacional, sesion.Emp_Cnx);

                // Validar si la Remisión es Válida o no en base a la suma de los montos en las partidas de la remisión y la remisión especial.
                bool bDocumentoValido = false;
                new CN_CapFactura().ValidaMontosImpresion(factura, sesion.Id_Cd_Ver, sesion.Id_Emp, 2, sesion.Emp_Cnx, ref bDocumentoValido);

                if (bDocumentoValido)
                {
                    List<AdendaDet> listCabT = new List<AdendaDet>();
                    List<AdendaDet> listDetT = new List<AdendaDet>();
                    List<AdendaDet> listCabR = new List<AdendaDet>();
                    List<AdendaDet> listDetR = new List<AdendaDet>();
                    List<AdendaDet> listCabNacionalT = new List<AdendaDet>();
                    List<AdendaDet> listDetNacionalT = new List<AdendaDet>();
                    new CN_CapFactura().ConsultarAdenda(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                    new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);
                    new CN_CapFactura().ConsultarAdendaNacional(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabNacionalT, ref listDetNacionalT, sesion.Emp_Cnx);

                    // -------------------------------------------------------------------------------------------
                    // Consulta productos de factura especial de la tabla 'CapFacturaEspecialDet' si esque la factura especial existe
                    // esto es si es una actualización de factura --> si el parametro Folio trae un Id de factura
                    // -------------------------------------------------------------------------------------------
                    List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();

                    new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                        , sesion.Emp_Cnx
                        , Id_Emp
                        , Id_Cd
                        , Id_Fac
                        , factura.Id_Cte);
                    // -------------------------------------------------------------------------------------------

                    #region variable XML a enviar
                    StringBuilder XML_Enviar = new StringBuilder();
                    XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    XML_Enviar.Append("<Comprobante");
                    XML_Enviar.Append(" serie=\"\"");
                    XML_Enviar.Append(" folio=\"\"");
                    XML_Enviar.Append(" fecha=\"\"");
                    XML_Enviar.Append(" formaDePago=\"\"");
                    XML_Enviar.Append(" subTotal=\"\"");
                    XML_Enviar.Append(" total=\"\"");

                    XML_Enviar.Append(" tipoDeComprobante=\"\"");
                    XML_Enviar.Append(" Sustituye=\"\"");
                    XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CARGO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION NOTA DE CARGO
                    XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                    XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                    XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                    XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CARGO
                    XML_Enviar.Append(" ConceptoDescuento1=\"\"");
                    XML_Enviar.Append(" TasaDescuento1=\"\"");
                    XML_Enviar.Append(" ConceptoDescuento2=\"\"");
                    XML_Enviar.Append(" TasaDescuento2=\"\"");
                    XML_Enviar.Append(" Notas=\"\"");
                    XML_Enviar.Append(" Correo=\"\"");
                    XML_Enviar.Append(" CliNum=\"\"");

                    XML_Enviar.Append(" MetodoPago=\"\"");
                    XML_Enviar.Append(" CuentaBancaria=\"\"");
                    XML_Enviar.Append(" Referencia=\"\"");
                    XML_Enviar.Append(" ComprobanteVersion=\"\"");
                    XML_Enviar.Append(">");
                    XML_Enviar.Append(" <Emisor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" numero=\"\" />");
                    XML_Enviar.Append(" <Receptor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" nombre=\"\"");
                    XML_Enviar.Append(" UsoCFDI=\"\">");
                    XML_Enviar.Append(" <Domicilio");
                    XML_Enviar.Append(" calle=\"\"");
                    XML_Enviar.Append(" noExterior=\"\"");
                    XML_Enviar.Append(" noInterior=\"\"");
                    XML_Enviar.Append(" colonia=\"\"");
                    XML_Enviar.Append(" municipio=\"\"");
                    XML_Enviar.Append(" estado=\"\"");
                    XML_Enviar.Append(" pais=\"\"");
                    XML_Enviar.Append(" codigoPostal=\"\" />");
                    XML_Enviar.Append(" </Receptor>");
                    XML_Enviar.Append(" <Conceptos>");
                    XML_Enviar.Append(" <Concepto");
                    XML_Enviar.Append(" ClaveProdServ=\"\"");
                    XML_Enviar.Append(" ClaveUnidad=\"\"");
                    XML_Enviar.Append(" cantidad=\"\"");
                    XML_Enviar.Append(" noIdentificacion=\"\"");
                    XML_Enviar.Append(" descripcion=\"\"");
                    XML_Enviar.Append(" valorUnitario=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Conceptos>");
                    XML_Enviar.Append(" <Impuestos");
                    XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                    XML_Enviar.Append(" <Traslados>");
                    XML_Enviar.Append(" <Traslado");
                    XML_Enviar.Append(" impuesto=\"\"");
                    XML_Enviar.Append(" tasa=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Traslados>");

                    if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                    {
                        XML_Enviar.Append(" <Retenidos>");
                        XML_Enviar.Append(" <Retenido");
                        XML_Enviar.Append(" importe=\"\"");
                        XML_Enviar.Append(" impuesto=\"\" />");
                        XML_Enviar.Append(" </Retenidos>");
                    }
                    XML_Enviar.Append(" </Impuestos>");

                    XML_Enviar.Append(" <Addenda>");

                    //ADENDA CABECERA
                    XML_Enviar.Append(" <cabecera");
                    XML_Enviar.Append(" Pedido=\"\"");
                    XML_Enviar.Append(" Requisicion=\"\"");
                    XML_Enviar.Append(" consignarRenglon1=\"\"");
                    XML_Enviar.Append(" consignarRenglon2=\"\"");
                    XML_Enviar.Append(" consignarRenglon3=\"\"");
                    XML_Enviar.Append(" consignarRenglon4=\"\"");
                    XML_Enviar.Append(" consignarRenglon5=\"\"");
                    XML_Enviar.Append(" Conducto=\"\"");
                    XML_Enviar.Append(" CondicionesPago=\"\"");
                    XML_Enviar.Append(" NumeroGuia=\"\"");
                    XML_Enviar.Append(" ControlPedido=\"\"");
                    XML_Enviar.Append(" OrdenEmbarque=\"\"");
                    XML_Enviar.Append(" Zona=\"\"");
                    XML_Enviar.Append(" Territorio=\"\"");
                    XML_Enviar.Append(" Agente=\"\"");
                    XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                    XML_Enviar.Append(" Formulo=\"\"");
                    XML_Enviar.Append(" Autorizo=\"\"");

                    XML_Enviar.Append(" NombreAddenda=\"\"");
                    foreach (AdendaDet det in listCabT)
                    {
                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    }
                    foreach (AdendaDet det in listCabR)
                    {
                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    }
                    XML_Enviar.Append("/>");




                    //ADENDA DETALLE
                    if (listaProdFacturaEspecialFinal.Count > 0)
                    {
                        foreach (FacturaDet fd in listaProdFacturaEspecialFinal)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                            string primerNodo = "";
                            int primerfila = 0;
                            foreach (AdendaDet det in listDetT)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            primerNodo = "";
                            primerfila = 0;
                            foreach (AdendaDet det in listDetR)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            XML_Enviar.Append("/>");
                        }
                    }
                    else
                    {
                        //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                        //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                            string primerNodo = "";
                            int primerfila = 0;
                            foreach (AdendaDet det in listDetT)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            primerNodo = "";
                            primerfila = 0;
                            foreach (AdendaDet det in listDetR)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            XML_Enviar.Append("/>");
                        }

                    }
                    XML_Enviar.Append(" </Addenda>");
                    if (facturaNacional != null)
                    {
                        if (movimiento != "CANCELACION")
                        {
                            //COMPROBANTE NACIONAL
                            XML_Enviar.Append(" <ComprobanteCN");
                            XML_Enviar.Append(" CliNum=\"\"");
                            XML_Enviar.Append(">");
                            XML_Enviar.Append(" <Conceptos>");
                            XML_Enviar.Append(" <Concepto");
                            XML_Enviar.Append(" cantidad=\"\"");
                            XML_Enviar.Append(" noIdentificacion=\"\"");
                            XML_Enviar.Append(" descripcion=\"\"");
                            XML_Enviar.Append(" valorUnitario=\"\"");
                            XML_Enviar.Append(" importe=\"\" />");
                            XML_Enviar.Append(" </Conceptos>");

                            //ADENDA NACIONAL
                            XML_Enviar.Append(" <AddendaCN>");

                            //ADENDA NACIONAL CABECERA
                            XML_Enviar.Append(" <CabeceraCN");
                            XML_Enviar.Append(" Pedido=\"\"");
                            XML_Enviar.Append(" Requisicion=\"\"");
                            XML_Enviar.Append(" consignarRenglon1=\"\"");
                            XML_Enviar.Append(" consignarRenglon2=\"\"");
                            XML_Enviar.Append(" consignarRenglon3=\"\"");
                            XML_Enviar.Append(" consignarRenglon4=\"\"");
                            XML_Enviar.Append(" consignarRenglon5=\"\"");
                            XML_Enviar.Append(" Conducto=\"\"");
                            XML_Enviar.Append(" CondicionesPago=\"\"");
                            XML_Enviar.Append(" NumeroGuia=\"\"");
                            XML_Enviar.Append(" ControlPedido=\"\"");
                            XML_Enviar.Append(" OrdenEmbarque=\"\"");
                            XML_Enviar.Append(" Zona=\"\"");
                            XML_Enviar.Append(" Territorio=\"\"");
                            XML_Enviar.Append(" Agente=\"\"");
                            XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                            XML_Enviar.Append(" Formulo=\"\"");
                            XML_Enviar.Append(" Autorizo=\"\"");

                            XML_Enviar.Append(" NombreAddenda=\"\"");
                            foreach (AdendaDet det in listCabNacionalT)
                            {
                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                            }
                            XML_Enviar.Append("/>");


                            //ADENDA NACIONAL DETALLE

                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            foreach (FacturaDet fd in listaFacturaDet)
                            {
                                XML_Enviar.Append(" <Detalle");
                                XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                                string primerNodo = "";
                                int primerfila = 0;
                                foreach (AdendaDet det in listDetNacionalT)
                                {

                                    if (fd.Id_Prd == det.Id_Prd)
                                    {
                                        if (primerfila == 0)
                                        { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                            primerNodo = det.Nodo;
                                        }
                                        if (primerfila > 0 && det.Nodo == primerNodo)
                                        {
                                            XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                            // ABRIMOS UNA NUEVA ADENDA
                                            XML_Enviar.Append(" <Detalle");
                                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                        }

                                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        primerfila++;
                                    }
                                }

                                XML_Enviar.Append("/>");
                            }

                            XML_Enviar.Append(" </AddendaCN>");

                            XML_Enviar.Append(" </ComprobanteCN>");
                        }
                        else
                        {
                            XML_Enviar.Append("<ComprobanteCN UUID=\"" + factura.Fac_FolioFiscalCN + "\" Folio=\"" + factura.Fac_FolioCN.ToString() + "\" Serie=\"" + factura.Serie + "\" />");
                            facturaNacional = null;
                        }
                    }
                    XML_Enviar.Append(" </Comprobante>");

                    //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                    //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA

                    //foreach (FacturaDet fd in listaFacturaDet)
                    //{
                    //    XML_Enviar.Append(" <Detalle");
                    //    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                    //    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                    //    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\""); 
                    //    foreach (AdendaDet det in listDetT)
                    //    {
                    //        if (fd.Id_Prd == det.Id_Prd)
                    //        {
                    //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    //        }
                    //    }
                    //    foreach (AdendaDet det in listDetR)
                    //    {
                    //        if (fd.Id_Prd == det.Id_Prd)
                    //        {
                    //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    //        }
                    //    }
                    //    XML_Enviar.Append("/>");
                    //}






                    #endregion

                    #region Codigo pruebas

                    //PruebaServicio.Service1 servicio = new PruebaServicio.Service1();
                    //float suma = servicio.Suma(Convert.ToSingle(txtNumero1.Text), Convert.ToSingle(txtNumero2.Text));
                    //this.Alerta(suma.ToString());

                    //Uri objURI = new Uri("");
                    //WebRequest objWebRequest = WebRequest.Create(objURI);
                    //WebResponse objWebResponse = objWebRequest.GetResponse();
                    //Stream objStream = objWebResponse.GetResponseStream();
                    //StreamReader objStreamReader = new StreamReader(objStream);
                    //string responseText = objStreamReader.ReadToEnd();

                    #endregion

                    // --------------------------------------
                    // Consulta centro de distribución
                    // --------------------------------------
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    // --------------------------------------------------------------------
                    // Consulta detalle de factura para generar lista de productos
                    // --------------------------------------------------------------------
                    //if (factura.Fac_Sello != "" && factura.Fac_Sello != null && movimiento == "FACTURA")
                    //{
                    //    //Abre el XML y carga el PDF de la factura
                    //    object resultado = null;
                    //    cn_factura.ConsultarFacturaSAT(ref factura, sesion.Emp_Cnx, ref resultado);
                    //    byte[] archivoPdf = (byte[])resultado;
                    //    if (archivoPdf.Length > 0)
                    //    {
                    //        string tempPDFname = string.Concat("FACTURA_"
                    //                 , factura.Id_Emp.ToString()
                    //                 , "_", factura.Id_Cd.ToString()
                    //                 , "_", factura.Id_U.ToString()
                    //                 , ".pdf");
                    //        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    //        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                    //        this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    //        // ------------------------------------------------------------------------------------------------
                    //        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    //        // ------------------------------------------------------------------------------------------------

                    //        RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                    //    }
                    //    else
                    //        this.DisplayMensajeAlerta("TempPDFNoData");
                    //}
                    //else
                    //{
                    // --------------------------------------
                    // cargar xml de factura que se envia a SAT
                    // --------------------------------------
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XML_Enviar.ToString());

                    // --------------------------------------//
                    // --------------------------------------//
                    //         LLENAR DATOS DEL XML          //
                    // --------------------------------------//
                    // --------------------------------------//
                    #region Llenar datos factura a Enviar
                    //encabezado
                    XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = factura.Id_Emp;
                    cliente.Id_Cd = factura.Id_Cd;
                    cliente.Id_Cte = factura.Id_Cte;
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    Comprobante.Attributes["serie"].Value = factura.Serie;
                    Comprobante.Attributes["folio"].Value = factura.Folio_cancelacion > 0 ? factura.Folio_cancelacion.ToString() : factura.Id_Fac.ToString();
                    //Comprobante.Attributes["fecha"].Value = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", factura.Fac_FechaHr);
                    Comprobante.Attributes["fecha"].Value = string.Format("{0:s}", factura.Fac_Fecha);

                    Comprobante.Attributes["formaDePago"].Value = cliente.Cte_MetodoPago;/*"PAGO EN UNA SOLA EXHIBICION"*/;
                    Comprobante.Attributes["subTotal"].Value = factura.Fac_SubTotal == null ? "0" : Math.Round((double)factura.Fac_SubTotal, 2).ToString();
                    Comprobante.Attributes["total"].Value = (Math.Round((double)factura.Fac_SubTotal, 2) + Math.Round((double)factura.Fac_ImporteIva, 2)).ToString();
                    Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                    Comprobante.Attributes["Sustituye"].Value = factura.Fac_Refactura;
                    Comprobante.Attributes["tipoMovimiento"].Value = movimiento;
                    Comprobante.Attributes["tipoMoneda"].Value = factura.Mon_Unidad;
                    Comprobante.Attributes["tipoCambio"].Value = factura.Mon_TipCambio.ToString();
                    Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                    Comprobante.Attributes["movimientoacancelar"].Value = ""; //

                    Comprobante.Attributes["ConceptoDescuento1"].Value = factura.Fac_Desc1;
                    Comprobante.Attributes["TasaDescuento1"].Value = factura.Fac_DescPorcen1 == null ? string.Empty : factura.Fac_DescPorcen1.ToString();
                    Comprobante.Attributes["ConceptoDescuento2"].Value = factura.Fac_Desc2;
                    Comprobante.Attributes["TasaDescuento2"].Value = factura.Fac_DescPorcen2 == null ? string.Empty : factura.Fac_DescPorcen2.ToString();
                    Comprobante.Attributes["Correo"].Value = factura.Cte_Email;
                    Comprobante.Attributes["CliNum"].Value = factura.Id_Cte.ToString();
                    Comprobante.Attributes["MetodoPago"].Value = "00".Substring(1, 2 - factura.Fac_FPago.Trim().Length) + factura.Fac_FPago.Trim();


                    //Comprobante.Attributes["MetodoPago"].Value = FormaPagoNombre(factura.Fac_FPago);
                    Comprobante.Attributes["CuentaBancaria"].Value = factura.Fac_UDigitos.ToString();
                    Comprobante.Attributes["Referencia"].Value = cliente.Cte_Referencia;
                    Comprobante.Attributes["ComprobanteVersion"].Value = "3.3";


                    XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                    Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                    Emisor.Attributes["numero"].Value = Cd.Id_Cd.ToString();

                    //receptor
                    XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                    Receptor.Attributes["rfc"].Value = factura.Fac_CteRfc;
                    Receptor.Attributes["nombre"].Value = factura.Cte_NomComercial;
                    Receptor.Attributes["UsoCFDI"].Value = cliente.Cte_UsoCFDI;

                    //Domicilio
                    XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                    Domicilio.Attributes["calle"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacCalle); // factura.Fac_CteCalle.Replace("\"", "");
                    Domicilio.Attributes["noExterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumero);// factura.Fac_CteNumero;
                    Domicilio.Attributes["noInterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumeroInterior);// factura.Fac_CteNumero;
                    Domicilio.Attributes["colonia"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacColonia);// factura.Fac_CteColonia;
                    Domicilio.Attributes["municipio"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacMunicipio);// factura.Fac_CteMunicipio;
                    Domicilio.Attributes["estado"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacEstado);// factura.Fac_CteEstado;
                    Domicilio.Attributes["pais"].Value = "México";
                    Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;// factura.Fac_CteCp;
                    // ---------------------
                    // Conceptos --> partidas = producto
                    // Detalle --> productoDetalle
                    // ---------------------         
                    XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                    XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                    XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");

                    XmlNode ComprobanteCN = Comprobante.SelectNodes("ComprobanteCN").Count > 0 ? Comprobante.SelectSingleNode("ComprobanteCN") : null;
                    XmlNode AddendaCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("AddendaCN") : null;
                    XmlNode ConceptosCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("Conceptos") : null;
                    XmlNode productoCN = ConceptosCN != null ? ConceptosCN.SelectSingleNode("Concepto") : null;

                    if (facturaNacional != null)
                    {
                        ComprobanteCN.Attributes["CliNum"].Value = facturaNacional != null ? facturaNacional.Id_Cte.ToString() : "0";
                    }


                    //Si existe una factura especial, en los nodos de conceptos del producto se pone
                    //los productos de la factura especial
                    //si no, se pone los datos de productos de la factura original
                    StringBuilder NotaProductosOriginales = new StringBuilder();
                    if (listaProdFacturaEspecialFinal.Count > 0)
                    {
                        foreach (FacturaDet facturaDet in listaProdFacturaEspecialFinal)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = facturaDet.Producto.Id_PrdEsp;
                            prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                            prd.Attributes["cantidad"].Value = facturaDet.Fac_CantE.ToString();
                            prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_ImporteE, 2).ToString();
                            prd.Attributes["ClaveProdServ"].Value = "01010101";
                            prd.Attributes["ClaveUnidad"].Value = "H87";
                            producto.ParentNode.AppendChild(prd);
                        }

                        foreach (FacturaDet facturaDet in listaFacturaDet)
                        {
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(facturaDet.Id_Prd.ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(Math.Round(facturaDet.Fac_Precio, 2).ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(facturaDet.Fac_Cant.ToString());
                        }
                    }
                    else
                    {
                        foreach (FacturaDet facturaDet in listaFacturaDet)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                            prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                            prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                            prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                            prd.Attributes["ClaveProdServ"].Value = facturaDet.Fac_ClaveProdServ.ToString();
                            prd.Attributes["ClaveUnidad"].Value = facturaDet.Fac_ClaveUnidad.ToString();
                            producto.ParentNode.AppendChild(prd);


                            if (facturaNacional != null)
                            {
                                XmlNode prdCN = productoCN.Clone();
                                prdCN.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                                prdCN.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                                prdCN.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                                prdCN.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                                prdCN.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                                prd.Attributes["ClaveProdServ"].Value = "01010101";
                                prd.Attributes["ClaveUnidad"].Value = "H87";
                                productoCN.ParentNode.AppendChild(prdCN);
                            }
                        }
                    }
                    producto.ParentNode.RemoveChild(xml.SelectNodes("//Concepto").Item(0));

                    if (facturaNacional != null)
                    {
                        productoCN.ParentNode.RemoveChild(xml.SelectNodes("//ComprobanteCN//Conceptos//Concepto").Item(0));
                    }


                    //Impuestos
                    XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                    Impuestos.Attributes["totalImpuestosTrasladados"].Value = factura.Fac_ImporteIva == null ? "0" : factura.Fac_ImporteIva.ToString();

                    //Traslado (impuestos desgloce)
                    XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                    XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                    Traslado.Attributes["impuesto"].Value = "IVA";
                    if (cliente.BPorcientoIVA == true)
                        Traslado.Attributes["tasa"].Value = cliente.PorcientoIVA.ToString();
                    else
                        Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();
                    Traslado.Attributes["importe"].Value = factura.Fac_ImporteIva == null ? "0" : Math.Round((double)factura.Fac_ImporteIva, 2).ToString();

                    if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                    {
                        XmlNode Retenidos = Impuestos.SelectSingleNode("Retenidos");
                        XmlNode Retenido = Retenidos.SelectSingleNode("Retenido");
                        Retenido.Attributes["importe"].Value = factura.Fac_ImporteRetencion == null ? "0" : Math.Round((double)factura.Fac_ImporteRetencion, 2).ToString();
                        Retenido.Attributes["impuesto"].Value = "IVA";
                    }

                    //Addenda
                    XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                    cabecera.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabecera.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    //consulta datos cliente                 
                    cabecera.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                    cabecera.Attributes["consignarRenglon2"].Value = string.Concat(factura.Fac_CteCalle.Replace("\"", ""), " ", factura.Fac_CteNumero);
                    cabecera.Attributes["consignarRenglon3"].Value = factura.Fac_CteColonia;
                    cabecera.Attributes["consignarRenglon4"].Value = string.Concat(factura.Fac_CteMunicipio, " ", factura.Fac_CteEstado, " ", factura.Fac_CteCp);
                    cabecera.Attributes["consignarRenglon5"].Value = "México";
                    cabecera.Attributes["Conducto"].Value = factura.Fac_Conducto;
                    cabecera.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                    cabecera.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                    cabecera.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabecera.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                    cabecera.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                    cabecera.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                    cabecera.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                    cabecera.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    cabecera.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                    cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                    cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;


                    //Addenda Nacional
                    if (facturaNacional != null)
                    {
                        XmlNode cabeceraCN = AddendaCN.SelectSingleNode("CabeceraCN");
                        cabeceraCN.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                        cabeceraCN.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                        //consulta datos cliente                 
                        cabeceraCN.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                        cabeceraCN.Attributes["consignarRenglon2"].Value = string.Concat(facturaNacional.Fac_CteCalle.Replace("\"", ""), " ", facturaNacional.Fac_CteNumero);
                        cabeceraCN.Attributes["consignarRenglon3"].Value = facturaNacional.Fac_CteColonia;
                        cabeceraCN.Attributes["consignarRenglon4"].Value = string.Concat(facturaNacional.Fac_CteMunicipio, " ", facturaNacional.Fac_CteEstado, " ", facturaNacional.Fac_CteCp).Replace('É', 'E');
                        cabeceraCN.Attributes["consignarRenglon5"].Value = "Mexico";
                        cabeceraCN.Attributes["Conducto"].Value = factura.Fac_Conducto;
                        cabeceraCN.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                        cabeceraCN.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                        cabeceraCN.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                        cabeceraCN.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                        cabeceraCN.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                        cabeceraCN.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                        cabeceraCN.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                        cabeceraCN.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                        cabeceraCN.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                        cabeceraCN.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                        cabeceraCN.Attributes["NombreAddenda"].Value = facturaNacional.Fac_CteAdeNombre;//cliente.Ade_Nombre;
                    }


                    Factura factura_remision = new Factura();
                    factura_remision.Id_Emp = factura.Id_Emp;
                    factura_remision.Id_Cd = factura.Id_Cd;
                    factura_remision.Id_Fac = factura.Id_Fac;
                    string agregado_nota = "";
                    cn_factura.FacturaRemision_Nota(factura_remision, sesion.Emp_Cnx, ref agregado_nota);
                    StringBuilder NotaCompleta = new StringBuilder();

                    NotaCompleta.Append(agregado_nota + "//");
                    NotaCompleta.Append(NotaProductosOriginales.ToString() + "//");
                    NotaCompleta.Append(factura.Fac_Notas + "//");
                    NotaCompleta.Append(agregado_nota_cancelacion);
                    Comprobante.Attributes["Notas"].Value = NotaCompleta.ToString();

                    /*
                    if (!ValidaImpresionFactura(xml)) 
                    {
                        Alerta("No se puede Imprimir Documento: Detalle de factura no coincide con total, Revise factura");
                        return;
                    
                    }*/

                    #endregion
                    // --------------------------------------
                    // convertir XML a string
                    // --------------------------------------
                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    xml.WriteTo(tx);
                    string xmlString = sw.ToString();
                    // ------------------------------------------------------   
                    // ENVIAR XML al servicio de la aplicacion de KEY
                    // -------- ----------------------------------------------
                    XmlDocument xmlSAT = new XmlDocument();

                    int TSAT = 1;

                    XmlDocument xmlBD = new XmlDocument();

                    if (factura.Fac_Xml != null && factura.Fac_Xml != "")
                    {
                        xmlBD.LoadXml(factura.Fac_Xml.ToString());

                        foreach (XmlNode nodo in xmlBD.ChildNodes)
                        {
                            if (nodo.Name == "Comprobante")
                            {
                                TSAT = 1;
                            }
                            else if (nodo.Name == "cfdi:Comprobante")
                            {
                                TSAT = 2;

                            }
                        }
                    }


                    //sian_cfd.Service1 sianFacturacionElectronica = new sian_cfd.Service1();

                    if (TSAT == 2 && tienePDF)
                    {
                        descargarPDF(Id_Fac);
                        return;
                    }

                    WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                    sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                    object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                    if (movimiento == "CANCELACION")
                    {
                        string XmLRegex = string.Empty;
                        XmLRegex = Regex.Replace(sianFacturacionElectronicaResult.ToString(), @"(?s)(?<=<cfdi:Addenda>).+?(?=</cfdi:Addenda>)", "");
                        XmLRegex = XmLRegex.Replace("<cfdi:Addenda>", "");
                        XmLRegex = XmLRegex.Replace("</cfdi:Addenda>", "");
                        xmlSAT.LoadXml(XmLRegex);
                    }
                    else
                    {
                        xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                    }





                    //*********************************************//
                    //* Procesar XML recibido de servicio de SAT  *//
                    //*********************************************//
                    string stringPDF = string.Empty;
                    string stringPDFCN = string.Empty;
                    string selloSAT = string.Empty;
                    string selloSATCN = string.Empty;
                    string folioFiscal = string.Empty;
                    string folioFiscalCN = string.Empty;
                    string errorNum = string.Empty;
                    string errorText = string.Empty;
                    string errorNumCN = string.Empty;
                    string errorTextCN = string.Empty;

                    TSAT = 1;

                    foreach (XmlNode nodoSistemaFacturacion in xmlSAT.ChildNodes)
                    {
                        if (nodoSistemaFacturacion.Name == "Comprobante")
                        {
                            TSAT = 1;
                            selloSAT = nodoSistemaFacturacion.Attributes["sello"].Value;

                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                                }


                            }
                        }
                        else if (nodoSistemaFacturacion.Name == "cfdi:Comprobante")
                        {
                            TSAT = 2;
                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                                }

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "tfd:TimbreFiscalDigital")
                                        {
                                            selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                            folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                        }
                                    }

                                }

                            }

                        }
                        if (nodoSistemaFacturacion.Name == "SistemaFacturacion")
                        {
                            foreach (XmlNode nodoXmlSAT in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (nodoXmlSAT.Name == "ComprobanteCDIK")
                                {
                                    foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                    {
                                        if (nodo.Name == "Comprobante")
                                        {
                                            TSAT = 1;
                                            selloSAT = nodo.Attributes["sello"].Value;

                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }
                                            }
                                        }
                                        else if (nodo.Name == "cfdi:Comprobante")
                                        {
                                            TSAT = 2;
                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }

                                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                {
                                                    XmlNode Nodo_nivel3;
                                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                                    selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                                    folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                                }

                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    if (nodoXmlSAT.Name == "ComprobanteKSL")
                                    {
                                        foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                        {
                                            if (nodo.Name == "Comprobante")
                                            {
                                                TSAT = 1;
                                                selloSATCN = nodo.Attributes["sello"].Value;

                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            if (Nodo_nivel3.Name == "ERROR")
                                                            {
                                                                errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                                errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                            }
                                                        }

                                                        nodo.RemoveChild(Nodo_nivel2);
                                                    }


                                                }
                                            }
                                            else if (nodo.Name == "cfdi:Comprobante")
                                            {
                                                TSAT = 2;
                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            if (Nodo_nivel3.Name == "ERROR")
                                                            {
                                                                errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                                errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                            }
                                                        }

                                                        nodo.RemoveChild(Nodo_nivel2);
                                                    }

                                                    if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                    {
                                                        XmlNode Nodo_nivel3;
                                                        Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                                        selloSATCN = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                                        folioFiscalCN = Nodo_nivel3.Attributes["UUID"].Value;
                                                    }

                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }

                    }



                    if (errorNum != "0")
                    {
                        this.Alerta(string.Concat(errorText.Replace("'", "\"")));

                        /* factura.Fac_Sello = selloSAT;
                         System.Data.SqlTypes.SqlXml sqlXml
                             = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                         factura.Fac_Xml = sqlXml;
                         factura.Fac_Pdf = this.Base64ToByte(stringPDF);

                         verificador = 0;

                         new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);*/
                    }
                    else
                    {
                        //ComprobanteSAT.RemoveChild(AddendaSAT);

                        if ((facturaNacional != null) && (errorNumCN != "0"))
                        {
                            this.Alerta(string.Concat(errorTextCN.Replace("'", "\"")));
                        }
                        else
                        {
                            factura.Fac_Sello = selloSAT;
                            factura.Fac_SelloCN = selloSATCN;

                            System.Data.SqlTypes.SqlXml sqlXml;
                            System.Data.SqlTypes.SqlXml sqlXmlCN;

                            if (xmlSAT.SelectNodes("SistemaFacturacion").Count > 0)
                            {
                                //sqlXml = sqlXml.Value.Replace("ComprobanteCDIK", "").;
                                sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteCDIK").OuterXml.Replace("<ComprobanteCDIK>", "").Replace("</ComprobanteCDIK>", ""), XmlNodeType.Document, null));
                                sqlXmlCN = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").OuterXml.Replace("<ComprobanteKSL>", "").Replace("</ComprobanteKSL>", ""), XmlNodeType.Document, null));
                                factura.Fac_FolioCN = int.Parse(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["folio"].Value == string.Empty ? "0" : xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["folio"].Value);
                            }
                            else
                            {
                                sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                                sqlXmlCN = null;
                                factura.Fac_FolioCN = null;
                            }


                            if (movimiento != "CANCELACION")
                            {

                                factura.Fac_Xml = sqlXml;
                                factura.Fac_XmlCN = sqlXmlCN;
                                factura.Fac_FolioFiscal = folioFiscal;
                                factura.Fac_FolioFiscalCN = folioFiscalCN;
                            }

                            factura.Fac_Pdf = this.Base64ToByte(stringPDF);
                            factura.Fac_PdfCN = this.Base64ToByte(stringPDFCN);

                            #region reporte factura


                            #endregion

                            // ---------------------------------------------------------------------------------------------
                            // Se actualiza el estatus de la factura a Impreso (I)
                            // ---------------------------------------------------------------------------------------------
                            if (movimiento != "CANCELACION")
                            {
                                factura.Fac_Estatus = "I";
                                new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);
                            }
                            else
                            {
                                factura.Fac_Estatus = "B";
                            }
                            verificador = 0;


                            // -----------------------
                            // Abrir PDF de factura
                            // -----------------------
                            string tempPDFname = string.Concat("FACTURA_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_Fac.ToString(), ".pdf");
                            string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                            string tempPDFCNname = string.Concat("FACTURACN_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_Fac.ToString(), ".pdf");
                            string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                            string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);

                            this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                            if (facturaNacional != null)
                            {
                                this.ByteToTempPDF(URLtempPDFCN, this.Base64ToByte(stringPDFCN));
                                // ------------------------------------------------------------------------------------------------
                                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                                // ------------------------------------------------------------------------------------------------
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','", WebURLtempPDFCN, "')"));
                            }
                            else
                            {
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','')"));
                            }
                        }
                        //}
                    }
                }
                else
                {
                    RAM1.ResponseScripts.Add("OpenAlert('Los montos de la Factura y la Factura Especial no coinciden','" + Id_Emp + "','" + Id_Cd + "','" + Id_Fac + "','" + 1 + "')");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void descargarPDF(int Id_Fac)
        {
            object resultado = null;
            object resultadoCN = null;
            Factura fac = new Factura();
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            fac.Id_Emp = Sesion.Id_Emp;
            fac.Id_Cd = Sesion.Id_Cd_Ver;
            fac.Id_Fac = Id_Fac;
            CN_CapFactura factura = new CN_CapFactura();
            factura.ConsultarFacturaSAT(ref fac, Sesion.Emp_Cnx, ref resultado, ref resultadoCN);
            byte[] archivoPdf = (byte[])resultado;
            byte[] archivoPdfCN = resultadoCN != System.DBNull.Value ? (byte[])resultadoCN : new byte[0];
            if (archivoPdf.Length > 0)
            {
                string tempPDFname = string.Concat("FACTURA_"
                         , Sesion.Id_Emp.ToString()
                         , "_", Sesion.Id_Cd.ToString()
                         , "_", Id_Fac.ToString()
                         , ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, archivoPdf);

                if (archivoPdfCN.Length > 0)
                {
                    string tempPDFCNname = string.Concat("FACTURACN_", Sesion.Id_Emp.ToString(), "_", Sesion.Id_Cd.ToString(), "_", Id_Fac.ToString(), ".pdf");
                    string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                    string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);

                    this.ByteToTempPDF(URLtempPDFCN, archivoPdfCN);

                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','" + WebURLtempPDFCN + "')"));
                }
                else
                {
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','')"));
                }
            }
        }

        /*
        private void ImprimirFactura(int Id_Emp, int Id_Cd, int Id_Fac, string movimiento, string agregado_nota_cancelacion)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                int verificador = 0;

                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                CN_CapFactura cn_factura = new CN_CapFactura();
                Factura factura = new Factura();
                factura.Id_Emp = sesion.Id_Emp;
                factura.Id_Cd = sesion.Id_Cd_Ver;
                factura.Id_Fac = Id_Fac;
                cn_factura.ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);

                List<AdendaDet> listCabT = new List<AdendaDet>();
                List<AdendaDet> listDetT = new List<AdendaDet>();
                List<AdendaDet> listCabR = new List<AdendaDet>();
                List<AdendaDet> listDetR = new List<AdendaDet>();
                new CN_CapFactura().ConsultarAdenda(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);

                // -------------------------------------------------------------------------------------------
                // Consulta productos de factura especial de la tabla 'CapFacturaEspecialDet' si esque la factura especial existe
                // esto es si es una actualización de factura --> si el parametro Folio trae un Id de factura
                // -------------------------------------------------------------------------------------------
                List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
                new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                    , sesion.Emp_Cnx
                    , Id_Emp
                    , Id_Cd
                    , Id_Fac
                    , factura.Id_Cte);
                // -------------------------------------------------------------------------------------------

                #region variable XML a enviar
                StringBuilder XML_Enviar = new StringBuilder();
                XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                XML_Enviar.Append("<Comprobante");
                XML_Enviar.Append(" serie=\"\"");
                XML_Enviar.Append(" folio=\"\"");
                XML_Enviar.Append(" fecha=\"\"");
                XML_Enviar.Append(" formaDePago=\"\"");
                XML_Enviar.Append(" subTotal=\"\"");
                XML_Enviar.Append(" total=\"\"");

                XML_Enviar.Append(" tipoDeComprobante=\"\"");
                XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CARGO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION NOTA DE CARGO
                XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CARGO
                XML_Enviar.Append(" ConceptoDescuento1=\"\"");
                XML_Enviar.Append(" TasaDescuento1=\"\"");
                XML_Enviar.Append(" ConceptoDescuento2=\"\"");
                XML_Enviar.Append(" TasaDescuento2=\"\"");
                XML_Enviar.Append(" Notas=\"\"");
                XML_Enviar.Append(" Correo=\"\"");
                XML_Enviar.Append(" CliNum=\"\"");

                XML_Enviar.Append(" MetodoPago=\"\"");
                XML_Enviar.Append(" CuentaBancaria=\"\"");
                XML_Enviar.Append(">");
                XML_Enviar.Append(" <Emisor");
                XML_Enviar.Append(" rfc=\"\"");
                XML_Enviar.Append(" numero=\"\" />");
                
                XML_Enviar.Append(" <Receptor");
                XML_Enviar.Append(" rfc=\"\"");
                XML_Enviar.Append(" nombre=\"\">");
                XML_Enviar.Append(" <Domicilio");
                XML_Enviar.Append(" calle=\"\"");
                XML_Enviar.Append(" noExterior=\"\"");
                XML_Enviar.Append(" noInterior=\"\"");
                XML_Enviar.Append(" colonia=\"\"");
                XML_Enviar.Append(" municipio=\"\"");
                XML_Enviar.Append(" estado=\"\"");
                XML_Enviar.Append(" pais=\"\"");
                XML_Enviar.Append(" codigoPostal=\"\" />");
                XML_Enviar.Append(" </Receptor>");
                XML_Enviar.Append(" <Conceptos>");
                XML_Enviar.Append(" <Concepto");
                XML_Enviar.Append(" cantidad=\"\"");
                XML_Enviar.Append(" noIdentificacion=\"\"");
                XML_Enviar.Append(" descripcion=\"\"");
                XML_Enviar.Append(" valorUnitario=\"\"");
                XML_Enviar.Append(" importe=\"\" />");
                XML_Enviar.Append(" </Conceptos>");
                XML_Enviar.Append(" <Impuestos");
                XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                XML_Enviar.Append(" <Traslados>");
                XML_Enviar.Append(" <Traslado");
                XML_Enviar.Append(" impuesto=\"\"");
                XML_Enviar.Append(" tasa=\"\"");
                XML_Enviar.Append(" importe=\"\" />");
                XML_Enviar.Append(" </Traslados>");
                XML_Enviar.Append(" </Impuestos>");

                XML_Enviar.Append(" <Addenda>");

                //ADENDA CABECERA
                XML_Enviar.Append(" <cabecera");
                XML_Enviar.Append(" Pedido=\"\"");
                XML_Enviar.Append(" Requisicion=\"\"");
                XML_Enviar.Append(" consignarRenglon1=\"\"");
                XML_Enviar.Append(" consignarRenglon2=\"\"");
                XML_Enviar.Append(" consignarRenglon3=\"\"");
                XML_Enviar.Append(" consignarRenglon4=\"\"");
                XML_Enviar.Append(" consignarRenglon5=\"\"");
                XML_Enviar.Append(" Conducto=\"\"");
                XML_Enviar.Append(" CondicionesPago=\"\"");
                XML_Enviar.Append(" NumeroGuia=\"\"");
                XML_Enviar.Append(" ControlPedido=\"\"");
                XML_Enviar.Append(" OrdenEmbarque=\"\"");
                XML_Enviar.Append(" Zona=\"\"");
                XML_Enviar.Append(" Territorio=\"\"");
                XML_Enviar.Append(" Agente=\"\"");
                XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                XML_Enviar.Append(" Formulo=\"\"");
                XML_Enviar.Append(" Autorizo=\"\"");

                XML_Enviar.Append(" NombreAddenda=\"\"");
                foreach (AdendaDet det in listCabT)
                {
                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                }
                foreach (AdendaDet det in listCabR)
                {
                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                }
                XML_Enviar.Append("/>");

                //ADENDA DETALLE
                if (listaProdFacturaEspecialFinal.Count > 0)
                {
                    foreach (FacturaDet fd in listaProdFacturaEspecialFinal)
                    {
                        XML_Enviar.Append(" <Detalle");
                        XML_Enviar.Append(" noProducto=\"" + fd.Producto.Id_PrdEsp + "\"");
                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion + " " + fd.Producto.Prd_UniNs + "\"");
                        foreach (AdendaDet det in listDetT)
                        {
                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                            }
                        }
                        foreach (AdendaDet det in listDetR)
                        {
                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                            }
                        }

                        XML_Enviar.Append("/>");
                    }
                }
                else
                {
                    foreach (FacturaDet fd in listaFacturaDet)
                    {
                        XML_Enviar.Append(" <Detalle");
                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion + " " + fd.Prd_Unis + "\"");
                        foreach (AdendaDet det in listDetT)
                        {
                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                            }
                        }
                        foreach (AdendaDet det in listDetR)
                        {
                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                            }
                        }

                        XML_Enviar.Append("/>");
                    }
                }
                XML_Enviar.Append(" </Addenda>");
                XML_Enviar.Append(" </Comprobante>");

                #endregion

                #region Codigo pruebas

                //PruebaServicio.Service1 servicio = new PruebaServicio.Service1();
                //float suma = servicio.Suma(Convert.ToSingle(txtNumero1.Text), Convert.ToSingle(txtNumero2.Text));
                //this.Alerta(suma.ToString());

                //Uri objURI = new Uri("");
                //WebRequest objWebRequest = WebRequest.Create(objURI);
                //WebResponse objWebResponse = objWebRequest.GetResponse();
                //Stream objStream = objWebResponse.GetResponseStream();
                //StreamReader objStreamReader = new StreamReader(objStream);
                //string responseText = objStreamReader.ReadToEnd();

                #endregion

                // --------------------------------------
                // Consulta centro de distribución
                // --------------------------------------
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                // --------------------------------------------------------------------
                // Consulta detalle de factura para generar lista de productos
                // --------------------------------------------------------------------
                if (factura.Fac_Sello != "" && factura.Fac_Sello != null)
                { //Abre el XML y carga el PDF de la factura
                    object resultado = null;
                    cn_factura.ConsultarFacturaSAT(ref factura, sesion.Emp_Cnx, ref resultado);
                    byte[] archivoPdf = (byte[])resultado;
                    if (archivoPdf.Length > 0)
                    {
                        string tempPDFname = string.Concat("FACTURA_"
                                 , factura.Id_Emp.ToString()
                                 , "_", factura.Id_Cd.ToString()
                                 , "_", factura.Id_U.ToString()
                                 , ".pdf");
                        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                        this.ByteToTempPDF(URLtempPDF, archivoPdf);
                        // ------------------------------------------------------------------------------------------------
                        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                        // ------------------------------------------------------------------------------------------------
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                    }
                    else
                        this.DisplayMensajeAlerta("TempPDFNoData");
                }
                else
                {
                    // --------------------------------------
                    // cargar xml de factura que se envia a SAT
                    // --------------------------------------
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XML_Enviar.ToString());

                    // --------------------------------------//
                    // --------------------------------------//
                    //         LLENAR DATOS DEL XML          //
                    // --------------------------------------//
                    // --------------------------------------//
                    #region Llenar datos factura a Enviar
                    //encabezado
                    XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = factura.Id_Emp;
                    cliente.Id_Cd = factura.Id_Cd;
                    cliente.Id_Cte = factura.Id_Cte;
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    Comprobante.Attributes["serie"].Value = factura.Serie;
                    Comprobante.Attributes["folio"].Value = factura.Folio_cancelacion > 0 ? factura.Folio_cancelacion.ToString() : factura.Id_Fac.ToString();
                    Comprobante.Attributes["fecha"].Value = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", factura.Fac_FechaHr);
                    Comprobante.Attributes["formaDePago"].Value = "PAGO EN UNA SOLA EXHIBICION";
                    Comprobante.Attributes["subTotal"].Value = factura.Fac_SubTotal == null ? "0" : Math.Round((double)factura.Fac_SubTotal, 2).ToString();
                    Comprobante.Attributes["total"].Value = (Math.Round((double)factura.Fac_SubTotal, 2) + Math.Round((double)factura.Fac_ImporteIva, 2)).ToString();
                    Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";

                    Comprobante.Attributes["tipoMovimiento"].Value = movimiento;
                    Comprobante.Attributes["tipoMoneda"].Value = factura.Mon_Unidad;
                    Comprobante.Attributes["tipoCambio"].Value = factura.Mon_TipCambio.ToString();
                    Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                    Comprobante.Attributes["movimientoacancelar"].Value = ""; //

                    Comprobante.Attributes["ConceptoDescuento1"].Value = factura.Fac_Desc1;
                    Comprobante.Attributes["TasaDescuento1"].Value = factura.Fac_DescPorcen1 == null ? string.Empty : factura.Fac_DescPorcen1.ToString();
                    Comprobante.Attributes["ConceptoDescuento2"].Value = factura.Fac_Desc2;
                    Comprobante.Attributes["TasaDescuento2"].Value = factura.Fac_DescPorcen2 == null ? string.Empty : factura.Fac_DescPorcen2.ToString();
                    Comprobante.Attributes["Correo"].Value = factura.Cte_Email;
                    Comprobante.Attributes["CliNum"].Value = factura.Id_Cte.ToString();

                    Comprobante.Attributes["MetodoPago"].Value = FormaPagoNombre(factura.Fac_FPago);
                    Comprobante.Attributes["CuentaBancaria"].Value = factura.Fac_UDigitos.ToString();

                    XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                    Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                    Emisor.Attributes["numero"].Value = Cd.Id_Cd.ToString();

                    //receptor
                    XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                    Receptor.Attributes["rfc"].Value = factura.Fac_CteRfc;
                    Receptor.Attributes["nombre"].Value = factura.Cte_NomComercial;

                    //Domicilio
                    XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                    Domicilio.Attributes["calle"].Value = cliente.Cte_FacCalle.Replace("\"", "");// factura.Fac_CteCalle.Replace("\"", "");
                    Domicilio.Attributes["noExterior"].Value = cliente.Cte_FacNumero;// factura.Fac_CteNumero;
                    Domicilio.Attributes["noInterior"].Value = cliente.Cte_FacNumeroInterior;// factura.Fac_CteNumero;
                    Domicilio.Attributes["colonia"].Value = cliente.Cte_FacColonia;// factura.Fac_CteColonia;
                    Domicilio.Attributes["municipio"].Value = cliente.Cte_FacMunicipio;// factura.Fac_CteMunicipio;
                    Domicilio.Attributes["estado"].Value = cliente.Cte_FacEstado;// factura.Fac_CteEstado;
                    Domicilio.Attributes["pais"].Value = "México";
                    Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;// factura.Fac_CteCp;

                    // ---------------------
                    // Conceptos --> partidas = producto
                    // Detalle --> productoDetalle
                    // ---------------------          
                    XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                    XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                    XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");


                    //Si existe una factura especial, en los nodos de conceptos del producto se pone
                    //los productos de la factura especial
                    //si no, se pone los datos de productos de la factura original
                    StringBuilder NotaProductosOriginales = new StringBuilder();
                    if (listaProdFacturaEspecialFinal.Count > 0)
                    {
                        foreach (FacturaDet facturaDet in listaProdFacturaEspecialFinal)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = facturaDet.Producto.Id_PrdEsp;
                            prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                            prd.Attributes["cantidad"].Value = facturaDet.Fac_CantE.ToString();
                            prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_ImporteE, 2).ToString();
                            producto.ParentNode.AppendChild(prd);
                        }

                        foreach (FacturaDet facturaDet in listaFacturaDet)
                        {
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(facturaDet.Id_Prd.ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(Math.Round(facturaDet.Fac_Precio, 2).ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(facturaDet.Fac_Cant.ToString());
                        }
                    }
                    else
                    {
                        foreach (FacturaDet facturaDet in listaFacturaDet)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                            prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                            prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();//facturaDet.Fac_Cant.ToString();
                            prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                            producto.ParentNode.AppendChild(prd);
                        }
                    }
                    producto.ParentNode.RemoveChild(xml.SelectNodes("//Concepto").Item(0));

                    //Impuestos
                    XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                    Impuestos.Attributes["totalImpuestosTrasladados"].Value = factura.Fac_ImporteIva == null ? "0" : factura.Fac_ImporteIva.ToString();

                    //Traslado (impuestos desgloce)
                    XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                    XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                    Traslado.Attributes["impuesto"].Value = "IVA";
                    Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();
                    Traslado.Attributes["importe"].Value = factura.Fac_ImporteIva == null ? "0" : Math.Round((double)factura.Fac_ImporteIva, 2).ToString();

                    //Addenda
                    XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                    cabecera.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabecera.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    //consulta datos cliente
                    //Clientes cliente = new Clientes();
                    //cliente.Id_Emp = factura.Id_Emp;
                    //cliente.Id_Cd = factura.Id_Cd;
                    //cliente.Id_Cte = factura.Id_Cte;
                    //new CN_CatCliente().ConsultaClientes(ref cliente,  sesion.Emp_Cnx);
                    cabecera.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                    cabecera.Attributes["consignarRenglon2"].Value = string.Concat(factura.Fac_CteCalle.Replace("\"", ""), " ", factura.Fac_CteNumero);
                    cabecera.Attributes["consignarRenglon3"].Value = factura.Fac_CteColonia;
                    cabecera.Attributes["consignarRenglon4"].Value = string.Concat(factura.Fac_CteMunicipio, " ", factura.Fac_CteEstado, " ", factura.Fac_CteCp);
                    cabecera.Attributes["consignarRenglon5"].Value = "México";
                    cabecera.Attributes["Conducto"].Value = factura.Fac_Conducto;
                    cabecera.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                    cabecera.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                    cabecera.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabecera.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                    cabecera.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                    cabecera.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                    cabecera.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                    cabecera.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    cabecera.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                    cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                    cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;

                    Factura factura_remision = new Factura();
                    factura_remision.Id_Emp = factura.Id_Emp;
                    factura_remision.Id_Cd = factura.Id_Cd;
                    factura_remision.Id_Fac = factura.Id_Fac;
                    string agregado_nota = "";
                    cn_factura.FacturaRemision_Nota(factura_remision, sesion.Emp_Cnx, ref agregado_nota);
                    StringBuilder NotaCompleta = new StringBuilder();

                    NotaCompleta.Append(agregado_nota + "//");
                    NotaCompleta.Append(NotaProductosOriginales.ToString() + "//");
                    NotaCompleta.Append(factura.Fac_Notas + "//");
                    NotaCompleta.Append(agregado_nota_cancelacion);
                    Comprobante.Attributes["Notas"].Value = NotaCompleta.ToString();
                    //cabecera.Attributes["Correo"].Value = factura.Cte_Email;

                    #endregion
                    // --------------------------------------
                    // convertir XML a string !!!!!!!!!!!!!!!!!!!!!!!
                    // --------------------------------------
                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    xml.WriteTo(tx);
                    string xmlString = sw.ToString();
                    // ------------------------------------------------------   
                    // ENVIAR XML al servicio de la aplicacion de KEY
                    // ------------------------------------------------------
                    XmlDocument xmlSAT = new XmlDocument();
                    //sian_cfd.Service1 sianFacturacionElectronica = new sian_cfd.Service1();
                    WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                    sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                    object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                    // ------------------------------------------------------
                    //string nombreFactura = string.Concat("Bitacora_FACTURA_", factura.Id_Fac.ToString(), ".xml");
                    //new CN_CapFactura().LogError_Insertar("9", "Inicia carga de XML temporal, lee del Disco duro", sesion.Emp_Cnx);
                    ////string PathXMLtemporal = Server.MapPath("~/xml/Bitacora_FACTURA_HYD_63.XML");
                    //new CN_CapFactura().LogError_Insertar("10", string.Concat("Path XML temporal: ", PathXMLtemporal), sesion.Emp_Cnx);
                    ////xmlSAT.Load(PathXMLtemporal); //temporal mientras levantan en servicio de pruebas de Key
                    xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());

                    string stringPDF = string.Empty;
                    string selloSAT = string.Empty;
                    string errorNum = string.Empty;
                    string folioFiscal = string.Empty;
                    string errorText = string.Empty;
                    int TSAT = 1;
                    //new CN_CapFactura().LogError_Insertar("11", string.Concat("XML temporal cargado correctamente desde el disco duro", PathXMLtemporal), sesion.Emp_Cnx);


                    foreach (XmlNode nodo in xmlSAT.ChildNodes)
                    {
                        if (nodo.Name == "Comprobante")
                        {
                            TSAT = 1;
                            selloSAT = nodo.Attributes["sello"].Value;

                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodo.RemoveChild(Nodo_nivel2);
                                }


                            }
                        }
                        else if (nodo.Name == "cfdi:Comprobante")
                        {
                            TSAT = 2;
                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodo.RemoveChild(Nodo_nivel2);
                                }

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    XmlNode Nodo_nivel3;
                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                    selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                    folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                }

                            }

                        }

                    }
                    //XmlNode ComprobanteSAT = xmlSAT.SelectSingleNode("Comprobante");
                    //XmlNode AddendaSAT = ComprobanteSAT.SelectSingleNode("Addenda");
                    //XmlNode errorSAT = AddendaSAT.SelectSingleNode("ERROR");
                    if (errorNum != "0")
                    {
                        this.Alerta(string.Concat(errorText.Replace("'", "\"")));
                    }
                    else
                    {
                        factura.Fac_Sello = selloSAT;
                        factura.Fac_FolioFiscal = folioFiscal;
                        //factura.Fac_AcuseDirectorio = Server.MapPath(string.Concat("xmlSAT/", nombreFactura));
                        //new CN_CapFactura().LogError_Insertar("13", "asigan xml factura", sesion.Emp_Cnx);
                        System.Data.SqlTypes.SqlXml sqlXml
                            = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                        factura.Fac_Xml = sqlXml;
                        //new CN_CapFactura().LogError_Insertar("13", "convierte PDF to bytes", sesion.Emp_Cnx);
                        factura.Fac_Pdf = this.Base64ToByte(stringPDF);

                        #region reporte factura

                        //// --------------------------------------
                        //// Agregar parámetros del reporte
                        //// --------------------------------------
                        //ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                        //ALValorParametrosInternos.Add(sesion.Id_Emp);
                        //ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                        //ALValorParametrosInternos.Add(Id_Fac);
                        //ALValorParametrosInternos.Add(xmlString);
                        //ALValorParametrosInternos.Add(arrayProductoUnidades);
                        //ALValorParametrosInternos.Add(Cd.Cd_Calle);
                        //ALValorParametrosInternos.Add(Cd.Cd_CalleNo);
                        //ALValorParametrosInternos.Add(Cd.Cd_CP);
                        //ALValorParametrosInternos.Add(Cd.Cd_Ciudad);
                        //ALValorParametrosInternos.Add(Cd.Cd_Estado);

                        //Type instance = null;
                        //instance = typeof(LibreriaReportes.FacturaImpresion);
                        //Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                        //Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;

                        ////NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                        //RAM1.ResponseScripts.Add("AbrirReporte()");

                        #endregion

                        // ---------------------------------------------------------------------------------------------
                        // Guarda la factrura en el directorio 
                        // ---------------------------------------------------------------------------------------------
                        //xmlSAT.Save(factura.Fac_AcuseDirectorio);
                        // ---------------------------------------------------------------------------------------------
                        // Ae actualiza el estatus de la factura a Impreso (I)
                        // ---------------------------------------------------------------------------------------------
                        //new CN_CapFactura().LogError_Insertar("14", "Modifica estatus factura", sesion.Emp_Cnx);
                        verificador = 0;
                        factura.Fac_Estatus = "I";
                        new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);
                        //new CN_CapFactura().LogError_Insertar("15", "Estatus modificado correctamente", sesion.Emp_Cnx);
                        // -----------------------
                        // Abrir PDF de factura
                        // -----------------------
                        string tempPDFname = string.Concat("FACTURA_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_U.ToString(), ".pdf");
                        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                        //new CN_CapFactura().LogError_Insertar("15", "path factura", sesion.Emp_Cnx);
                        //new CN_CapFactura().LogError_Insertar("16", URLtempPDF, sesion.Emp_Cnx);

                        this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                        //new CN_CapFactura().LogError_Insertar("17", "invoco metodo 'ByteToTempPDF'", sesion.Emp_Cnx);
                        //this.ShowTempPDF(URLtempPDF);
                        //new CN_CapFactura().LogError_Insertar("18", "termina metodo 'ShowTempPDF'", sesion.Emp_Cnx);
                        //rgFactura.Rebind();
                        //new CN_CapFactura().LogError_Insertar("19", "termina RebindGrid", sesion.Emp_Cnx);

                        // ------------------------------------------------------------------------------------------------
                        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                        // ------------------------------------------------------------------------------------------------
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                    }
                }
            }
            catch (Exception ex)
            {
                //Alerta(ex.ToString());
                throw ex;
            }
        }
*/
        private void ImprimirRemisionElectronica(Remision remision)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<RemisionDet> listaProdFacturaEspecialFinal = new List<RemisionDet>();

                if (remision.Rem_Estatus != "B")
                {
                    remision.Rem_Estatus = "I";
                }
                int verificador = 0;
                new CN_CapRemision().ModificarRemision_Estatus(remision, sesion.Emp_Cnx, ref verificador);

                new CN_CapRemision().ConsultaRemisionEspecialDetalle(ref listaProdFacturaEspecialFinal, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, remision.Id_Rem, remision.Id_Cte);

                List<RemisionDet> detalles = new List<RemisionDet>();
                new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);


                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes clientes = new Clientes();
                clientes.Id_Emp = sesion.Id_Emp;
                clientes.Id_Cd = sesion.Id_Cd_Ver;
                clientes.Id_Cte = remision.Id_Cte;
                cn_catcliente.ConsultaClientes(ref clientes, sesion.Emp_Cnx);

                int Id_Rem = remision.Id_Rem;
                Remision remision2 = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision2, 0);

                CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
                CentroDistribucion cd = new CentroDistribucion();
                double iva = 0;
                cn_cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva, sesion.Emp_Cnx);

                StringBuilder XML_Enviar = new StringBuilder();
                XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>");
                XML_Enviar.Append("<RemisionCuentaNacional");
                XML_Enviar.Append(" TipoDocumento=\"\">");

                XML_Enviar.Append(" <Sucursal");
                XML_Enviar.Append(" CDINum=\"\"");
                XML_Enviar.Append(" CDINom=\"\"");
                XML_Enviar.Append(" CDIIVA=\"\"/>");

                XML_Enviar.Append(" <Documento");
                XML_Enviar.Append(" Folio=\"\"");
                XML_Enviar.Append(" Status=\"\"");
                XML_Enviar.Append(" Fecha=\"\"");
                XML_Enviar.Append(" Conducto=\"\"");
                XML_Enviar.Append(" Total=\"\"/>");

                XML_Enviar.Append(" <Cliente");
                XML_Enviar.Append(" NoCliente=\"\"");
                XML_Enviar.Append(" Nombre=\"\"");
                XML_Enviar.Append(" CuentaNacional=\"\">");

                XML_Enviar.Append(" <DatosFiscales");
                XML_Enviar.Append(" Calle=\"\"");
                XML_Enviar.Append(" Numero=\"\"");
                XML_Enviar.Append(" Colonia=\"\"");
                XML_Enviar.Append(" Municipio=\"\"");
                XML_Enviar.Append(" Estado=\"\"");
                XML_Enviar.Append(" C.P.=\"\"/>");

                XML_Enviar.Append(" <DatosConsignacion");
                XML_Enviar.Append(" Calle=\"\"");
                XML_Enviar.Append(" Numero=\"\"");
                XML_Enviar.Append(" Colonia=\"\"");
                XML_Enviar.Append(" Municipio=\"\"");
                XML_Enviar.Append(" Estado=\"\"");
                XML_Enviar.Append(" C.P.=\"\"/>");
                XML_Enviar.Append(" </Cliente>");

                XML_Enviar.Append(" <DetalleKey>");
                if (detalles.Count() > 0)
                {
                    foreach (RemisionDet d in detalles)
                    {
                        XML_Enviar.Append(" <Producto");
                        XML_Enviar.Append(" Codigo=\"" + d.Producto.Id_Prd + "\"");
                        XML_Enviar.Append(" Descipcion=\"" + d.Producto.Prd_Descripcion.ToString().Replace("\"", "").Replace("'", "").Replace("&", "") +
                                          "\"");
                        XML_Enviar.Append(" Cantidad=\"" + d.Rem_Cant + "\"");
                        XML_Enviar.Append(" Unidad=\"" + d.Prd_UniNe + "\"");
                        XML_Enviar.Append(" Presentacion=\"" + d.Prd_Presentacion + "\"");
                        XML_Enviar.Append(" Precio=\"" + d.Rem_Precio + "\"");
                        XML_Enviar.Append(" Territorio=\"" + d.Producto.Id_Ter + "\"");
                        XML_Enviar.Append(" Nombre=\"" + ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx) + "\"");

                        XML_Enviar.Append(" />");
                    }
                }

                XML_Enviar.Append(" </DetalleKey>");

                if (remision.Rem_CteCuentaNacional == 6 || remision.Rem_CteCuentaNacional == 7)
                {
                    XML_Enviar.Append(" <DetalleEspecial>");
                    if (listaProdFacturaEspecialFinal.Count() > 0)
                    {
                        foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                        {
                            XML_Enviar.Append(" <ProductoEspecial");
                            XML_Enviar.Append(" Codigo=\"" + d.Producto.Id_PrdEsp + "\"");
                            XML_Enviar.Append(" Descipcion=\"" + d.Producto.Prd_DescripcionEspecial.ToString().Replace("\"", "").Replace("'", "").Replace("&", "") +
                                              "\"");
                            XML_Enviar.Append(" Cantidad=\"" + d.Rem_Cant + "\"");
                            XML_Enviar.Append(" Unidad=\"" + d.Producto.Prd_UniNe + "\"");
                            XML_Enviar.Append(" Presentacion=\"" + d.Producto.Prd_Presentacion + "\"");
                            XML_Enviar.Append(" Precio=\"" + d.Rem_Precio + "\"");
                            XML_Enviar.Append(" Territorio=\"" + d.Producto.Id_Ter + "\"");
                            XML_Enviar.Append(" Nombre=\"" + ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx) + "\"");
                            XML_Enviar.Append(" CodigoOriginal=\"" + d.Producto.Id_Prd + "\"");
                            XML_Enviar.Append(" Release=\"" + d.Clp_Release + "\"");

                            XML_Enviar.Append(" />");
                        }
                    }
                    XML_Enviar.Append(" </DetalleEspecial>");
                }
                XML_Enviar.Append(" </RemisionCuentaNacional>");

                XmlDocument xml = new XmlDocument();

                xml.LoadXml(XML_Enviar.ToString());

                XmlNode RemisionCuentaNacional = xml.SelectSingleNode("RemisionCuentaNacional");
                RemisionCuentaNacional.Attributes["TipoDocumento"].Value = "Remision";

                XmlNode Sucursal = RemisionCuentaNacional.SelectSingleNode("Sucursal");
                Sucursal.Attributes["CDINum"].Value = remision.Id_Cd.ToString();
                Sucursal.Attributes["CDINom"].Value = "Remision";
                Sucursal.Attributes["CDIIVA"].Value = iva.ToString();

                XmlNode Documento = RemisionCuentaNacional.SelectSingleNode("Documento");
                Documento.Attributes["Folio"].Value = remision.Id_Rem.ToString();
                Documento.Attributes["Status"].Value = remision.Rem_Estatus.ToString();
                Documento.Attributes["Fecha"].Value = remision.Rem_Fecha.ToShortDateString();
                Documento.Attributes["Conducto"].Value = remision.Rem_Conducto.ToString();
                Documento.Attributes["Total"].Value = remision.Rem_Total.ToString();

                XmlNode Cliente = RemisionCuentaNacional.SelectSingleNode("Cliente");
                Cliente.Attributes["NoCliente"].Value = remision.Id_Cte.ToString();
                Cliente.Attributes["Nombre"].Value = remision.Cte_NomComercial.ToString();
                Cliente.Attributes["CuentaNacional"].Value = remision.Rem_CteCuentaNacional.ToString();

                XmlNode DatosFiscales = Cliente.SelectSingleNode("DatosFiscales");
                DatosFiscales.Attributes["Calle"].Value = clientes.Cte_FacCalle.ToString();
                DatosFiscales.Attributes["Numero"].Value = clientes.Cte_FacNumero.ToString();
                DatosFiscales.Attributes["Colonia"].Value = clientes.Cte_FacColonia.ToString();
                DatosFiscales.Attributes["Municipio"].Value = clientes.Cte_FacMunicipio.ToString();
                DatosFiscales.Attributes["Estado"].Value = clientes.Cte_FacEstado.ToString();
                DatosFiscales.Attributes["C.P."].Value = clientes.Cte_FacCp.ToString();

                XmlNode DatosConsignacion = Cliente.SelectSingleNode("DatosConsignacion");
                DatosConsignacion.Attributes["Calle"].Value = remision2.Rem_Calle.ToString();
                DatosConsignacion.Attributes["Numero"].Value = remision2.Rem_Numero.ToString();
                DatosConsignacion.Attributes["Colonia"].Value = remision2.Rem_Colonia.ToString();
                DatosConsignacion.Attributes["Municipio"].Value = remision.Rem_Municipio.ToString();
                DatosConsignacion.Attributes["Estado"].Value = remision.Rem_Estado.ToString();
                DatosConsignacion.Attributes["C.P."].Value = remision.Rem_Cp.ToString();

                /*
                StringBuilder cadena = new StringBuilder();
                int contador = 0;
                foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                {
                if (contador > 0)
                cadena.Append("|");
                cadena.Append(remision.Id_Cd);
                cadena.Append("|");
                cadena.Append(d.Clp_Release.Replace("|",""));
                cadena.Append("|");
                cadena.Append(remision.Id_Rem);
                cadena.Append("|");
                cadena.Append(remision.Rem_EstatusStr.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Id_Ter);
                cadena.Append("|");
                cadena.Append(ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx));
                cadena.Append("|");
                cadena.Append(remision.Id_Cte);
                cadena.Append("|");
                cadena.Append(remision.Cte_NomComercial.Replace("|", ""));
                cadena.Append("|");
                //DATOS FISCALES
                cadena.Append(clientes.Cte_FacCalle.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacNumero.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacColonia.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacMunicipio.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacEstado.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacCp.Replace("|", ""));
                cadena.Append("|");
                //DATOS DE CONSIGNACION
                cadena.Append(remision2.Rem_Calle.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Numero.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Colonia.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Municipio.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Estado.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Cp.Replace("|", ""));
                cadena.Append("|");
                //PRODUCTOS
                cadena.Append(d.Producto.Id_PrdEsp);
                cadena.Append("|");
                cadena.Append(d.Producto.Id_Prd);
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_DescripcionEspecial.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_Presentacion.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_UniNs.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Rem_Cant);
                cadena.Append("|");
                cadena.Append(d.Rem_Precio);
                cadena.Append("|");
                cadena.Append(iva);
                cadena.Append("|");
                cadena.Append(remision.Rem_Total);
                if (remision.Rem_FechaHr == null)
                {
                cadena.Append("|");
                cadena.Append(remision.Rem_Fecha);
                }
                else
                {
                cadena.Append("|");
                cadena.Append(remision.Rem_FechaHr);
                }
                contador = 1;
                }
                string cadena_Final = cadena.ToString();*/

                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xml.WriteTo(tx);
                string xmlString = sw.ToString();

                WS_RemElectronicaCtaNacional.Service1 remelectronica = new WS_RemElectronicaCtaNacional.Service1();

                string sianRemisionElectronicaResult = remelectronica.Imprime(xmlString).ToString();

                byte[] PDFRemision = this.Base64ToByte(sianRemisionElectronicaResult);
                remision.PDF = PDFRemision;
                verificador = 0;
                new CN_CapRemision().ModificarRemision_PDF(remision, sesion.Emp_Cnx, ref verificador);

                string tempPDFname = string.Concat("REMISION_", remision.Id_Emp.ToString(), "_", remision.Id_Cd.ToString(), "_", remision.Id_U.ToString(), ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(System.Configuration.ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(System.Configuration.ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, PDFRemision);
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtenerNombreTerritorio(int Id_Emp, int Id_Cd, int? Id_ter, string Conexion)
        {
            CN_CatTerritorios cn_catterritorio = new CN_CatTerritorios();
            Territorios terr = new Territorios();
            terr.Id_Emp = Id_Emp;
            terr.Id_Cd = Id_Cd;
            terr.Id_Ter = (int)Id_ter;
            cn_catterritorio.ConsultaTerritorio(ref terr, Conexion);
            return terr.Descripcion;
        }

        private void ImprimirRemisionElectronicaPIPES(Remision remision)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<RemisionDet> listaProdFacturaEspecialFinal = new List<RemisionDet>();

                remision.Rem_Estatus = "I";
                int verificador = 0;
                new CN_CapRemision().ModificarRemision_Estatus(remision, sesion.Emp_Cnx, ref verificador);

                new CN_CapRemision().ConsultaRemisionEspecialDetalle(ref listaProdFacturaEspecialFinal, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, remision.Id_Rem, remision.Id_Cte);

                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes clientes = new Clientes();
                clientes.Id_Emp = sesion.Id_Emp;
                clientes.Id_Cd = sesion.Id_Cd_Ver;
                clientes.Id_Cte = remision.Id_Cte;
                cn_catcliente.ConsultaClientes(ref clientes, sesion.Emp_Cnx);

                int Id_Rem = remision.Id_Rem;
                Remision remision2 = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision2, 0);

                CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
                CentroDistribucion cd = new CentroDistribucion();
                double iva = 0;
                cn_cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva, sesion.Emp_Cnx);

                StringBuilder cadena = new StringBuilder();
                int contador = 0;
                foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                {
                    if (contador > 0)
                        cadena.Append("|");
                    cadena.Append(remision.Id_Cd);
                    cadena.Append("|");
                    cadena.Append(d.Clp_Release.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision.Id_Rem);
                    cadena.Append("|");
                    cadena.Append(remision.Rem_EstatusStr.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(d.Producto.Id_Ter);
                    cadena.Append("|");
                    cadena.Append(ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx));
                    cadena.Append("|");
                    cadena.Append(remision.Id_Cte);
                    cadena.Append("|");
                    cadena.Append(remision.Cte_NomComercial.Replace("|", ""));
                    cadena.Append("|");
                    //DATOS FISCALES
                    cadena.Append(clientes.Cte_FacCalle.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacNumero.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacColonia.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacMunicipio.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacEstado.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(clientes.Cte_FacCp.Replace("|", ""));
                    cadena.Append("|");
                    //DATOS DE CONSIGNACION
                    cadena.Append(remision2.Rem_Calle.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Numero.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Colonia.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Municipio.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Estado.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(remision2.Rem_Cp.Replace("|", ""));
                    cadena.Append("|");
                    //PRODUCTOS
                    cadena.Append(d.Producto.Id_PrdEsp);
                    cadena.Append("|");
                    cadena.Append(d.Producto.Id_Prd);
                    cadena.Append("|");
                    cadena.Append(d.Producto.Prd_DescripcionEspecial.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(d.Producto.Prd_Presentacion.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(d.Producto.Prd_UniNs.Replace("|", ""));
                    cadena.Append("|");
                    cadena.Append(d.Rem_Cant);
                    cadena.Append("|");
                    cadena.Append(d.Rem_Precio);
                    cadena.Append("|");
                    cadena.Append(iva);
                    cadena.Append("|");
                    cadena.Append(remision.Rem_Total);
                    if (remision.Rem_FechaHr == null)
                    {
                        cadena.Append("|");
                        cadena.Append(remision.Rem_Fecha);
                    }
                    else
                    {
                        cadena.Append("|");
                        cadena.Append(remision.Rem_FechaHr);
                    }
                    contador = 1;
                }
                string cadena_Final = cadena.ToString();

                RemElectronica.Service1 remelectronica = new RemElectronica.Service1();

                string sianRemisionElectronicaResult = remelectronica.Imprime(cadena_Final).ToString();

                byte[] PDFRemision = this.Base64ToByte(sianRemisionElectronicaResult);
                remision.PDF = PDFRemision;
                verificador = 0;
                new CN_CapRemision().ModificarRemision_PDF(remision, sesion.Emp_Cnx, ref verificador);

                string tempPDFname = string.Concat("REMISION_", remision.Id_Emp.ToString(), "_", remision.Id_Cd.ToString(), "_", remision.Id_U.ToString(), ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(System.Configuration.ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(System.Configuration.ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, PDFRemision);
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string FormaPagoNombre(string Id_Fpa)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatFormaPago cncatformapago = new CN_CatFormaPago();
                FormaPago fpago = new FormaPago();
                fpago.Id_Emp = sesion.Id_Emp;
                if (Id_Fpa != "")
                {
                    fpago.Id_Fpa = Convert.ToInt32(Id_Fpa);
                    cncatformapago.ConsultaFormaPago(ref fpago, sesion.Emp_Cnx);
                }
                else
                {
                    fpago.Descripcion = "";
                }
                return fpago.Descripcion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }
        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    filebytes = Convert.FromBase64String(data);
                }
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MesNombre(int fechaMes, ref string mes)
        {
            if (fechaMes > 0)
            {
                switch (fechaMes)
                {
                    case 1:
                        mes = "Enero";
                        break;
                    case 2:
                        mes = "Febrero";
                        break;
                    case 3:
                        mes = "Marzo";
                        break;
                    case 4:
                        mes = "Abril";
                        break;
                    case 5:
                        mes = "Mayo";
                        break;
                    case 6:
                        mes = "Junio";
                        break;
                    case 7:
                        mes = "Julio";
                        break;
                    case 8:
                        mes = "Agosto";
                        break;
                    case 9:
                        mes = "Septiembre";
                        break;
                    case 10:
                        mes = "Octubre";
                        break;
                    case 11:
                        mes = "Noviembre";
                        break;
                    case 12:
                        mes = "Diciembre";
                        break;
                }
            }
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("TempPDFNoData"))
                    Alerta("El archivo PDF no contiene datos.");
                else
                    if (mensaje.Contains("TempPDFNoEncontrado"))
                        Alerta("No se encontró el archivo PDF de la factura");
                    else
                        if (mensaje.Contains("CapFactura_EsBaja"))
                            Alerta("La factura ya está dada de baja");
                        else
                            if (mensaje.Contains("CapFactura_EstatusIncorrecto"))
                                Alerta("No se puede dar de baja la factura. Estatus incorrecto");
                            else
                                if (mensaje.Contains("PermisoModificarDenegado"))
                                    Alerta("Operación denegada, no tiene permisos para modificar facturas");
                                else
                                    if (mensaje.Contains("PermisoEliminarDenegado"))
                                        Alerta("Operación denegada, no tiene permisos para dar de baja facturas");
                                    else
                                        if (mensaje.Contains("PermisoImprimirDenegado"))
                                            Alerta("Operación denegada, no tiene permisos para imprimir facturas");
                                        else
                                            if (mensaje.Contains("CapFactura_Imprimir_Denegado"))
                                                Alerta("Imposible imprimir el documento");
                                            else
                                                if (mensaje.Contains("CapFactura_Modificar_Denegado"))
                                                    Alerta("Imposible modificar el documento");
                                                else
                                                    if (mensaje.Contains("CapFactura_print_error"))
                                                        Alerta(string.Concat("Error al imprimir la factura. ", mensaje.Replace("'", "\"")));
                                                    else
                                                        if (mensaje.Contains("CapFactura_delete_ok"))
                                                            Alerta("La factura se ha dado de baja (estatus \"B\") correctamente");
                                                        else
                                                            if (mensaje.Contains("CapFactura_TienePagos"))
                                                                Alerta("La factura no puede ser cancelada; tiene pagos aplicados");
                                                            else
                                                                if (mensaje.Contains("btnBuscar_error"))
                                                                    Alerta("Error al momento de filtrar la información");
                                                                else
                                                                    if (mensaje.Contains("RAM1_AjaxRequest"))
                                                                        Alerta("Error al momento de actualizar el grid de factura");
                                                                    else
                                                                        if (mensaje.Contains("rgFactura_NeedDataSource"))
                                                                            Alerta("Error al cargar el grid de detalle de factura");
                                                                        else
                                                                            if (mensaje.Contains("rgFactura_ItemCommand"))
                                                                                Alerta("Error al ejecutar un evento (ItemCommand) al cargar el grid de factura");
                                                                            else
                                                                                if (mensaje.Contains("CapFactura_delete_error"))
                                                                                    Alerta("Error al momento de dar de baja la factura");
                                                                                else
                                                                                    if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                                        Alerta("Error al cambiar de página");
                                                                                    else
                                                                                        Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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
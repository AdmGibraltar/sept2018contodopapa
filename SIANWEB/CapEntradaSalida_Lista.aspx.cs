using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System.Globalization;

namespace SIANWEB
{
    public partial class CapEntradaSalida_Lista : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion

        #region Eventos


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Dictionary<string, object> vPars = new Dictionary<string, object>();

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
                        dpFecha1.DbSelectedDate = Sesion.CalendarioIni;
                        dpFecha2.DbSelectedDate = Sesion.CalendarioFin;
                        ValidarPermisos();
                        LlenarCombos();
                        CargarCentros();

                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");

                        double ancho = 0;
                        foreach (GridColumn gc in rgEntSal.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgEntSal.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgEntSal.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                        RadAjaxManager1.ResponseScripts.Add("IniciarPaginasAuxiliares();");

                        vPars = (Dictionary<string, object>)Session["Parametros" + Session.SessionID];

                        if (vPars != null && (vPars.ContainsKey("NumIni") && vPars.ContainsKey("NumFin")))
                        {
                            //Asigna al campo del filtro numero incial
                            txtNumero1.Text = vPars.FirstOrDefault(x => x.Key == "NumIni").Value.ToString();
                            //Asigna al campo del filtro numero final
                            txtCliente3.Text = vPars.FirstOrDefault(x => x.Key == "NumFin").Value.ToString();

                            rgEntSal.DataSource = null;
                            rgEntSal.Rebind();

                            Session["Parametros" + Session.SessionID] = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                rgEntSal.DataSource = GetList();
            }
            catch (Exception)
            {
                Alerta("Error al cargar los datos");
            }
        }
        protected void rgEntSal_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem) && (e.Item.IsDataBound))
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item["Id_Pvd"].Text == "-1")
                    item["Id_Pvd"].Text = "";

                if (item["Id_Cte"].Text == "-1")
                    item["Id_Cte"].Text = "";
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= dpFecha1.MinDate && sesion.CalendarioIni <= dpFecha1.MaxDate)
                {
                    dpFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= dpFecha2.MinDate && sesion.CalendarioFin <= dpFecha2.MaxDate)
                {
                    dpFecha2.DbSelectedDate = sesion.CalendarioFin;
                }

                Session["Sesion" + Session.SessionID] = sesion;

                rgEntSal.Rebind();
                limpiarCamposBusqueda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                rgEntSal.Rebind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        //nuevo();
                        RadAjaxManager1.ResponseScripts.Add("return AbrirVentana_Pagos('-1','-1','" + _PermisoGuardar.ToString().ToLower() + "','" + _PermisoModificar.ToString().ToLower() + "','"
                                + _PermisoEliminar.ToString().ToLower() + "','" + _PermisoImprimir.ToString().ToLower() + "')");
                        break;
                    case "save":
                        //Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEntSal_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                List<string> statusPosibles = new List<string>();
                switch (e.CommandName)
                {
                    case "Baja":
                            baja(ref e, ref statusPosibles, ref item);                     
                        break;

                    case "Imprimir":
                        if (_PermisoImprimir)
                            Imprimir(ref e, ref statusPosibles);
                        else
                            Alerta("No tiene permiso para imprimir");
                        //el estatus impreso lo asigna el reporte                        
                        break;

                    case "Editar":
                        ///El movimiento solo se podrá modificar siempre y cuando se encuentre en estatus C capturado y 
                        ///la entrada sea tipo de movimiento que no sea automático.                     
                            statusPosibles = new List<string>() { "C" };
                            string ides = rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Es"].Text;
                            string es_naturaleza = rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Naturaleza"].Text;

                            if (!statusPosibles.Contains(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Estatus"].Text.ToUpper()))
                            {
                                RadAjaxManager1.ResponseScripts.Add("OpenAlert('El documento se encuentra en estatus no válido para realizar la modificación','" + ides + "','" + es_naturaleza + "');");
                                break;
                            }
                            if (rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ManAutStr"].Text != "Manual")
                            {
                                RadAjaxManager1.ResponseScripts.Add("OpenAlert('El documento fue creado de manera automática y no puede ser modificado','" + ides + "','" + es_naturaleza + "');");
                                break;
                            }

                            Sesion Sesion = new Sesion();
                            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                            DateTime fecha = DateTime.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Fecha"].Text);

                            if (!(fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))//validar fecha dentro del periodo
                            {
                                //solo se muestra la info del movimientoEntradaSalida pero no se permite modificar
                                RadAjaxManager1.ResponseScripts.Add("OpenAlert('La fecha del documento esta fuera del periodo y no puede ser modificado','" + ides + "','" + es_naturaleza + "');");
                                break;
                            }
                            else
                            {
                                RadAjaxManager1.ResponseScripts.Add("return AbrirVentana_Pagos('" + ides + "','" + es_naturaleza
                                    + "','" + _PermisoGuardar.ToString().ToLower() + "','" + _PermisoModificar.ToString().ToLower() + "','"
                                    + _PermisoEliminar.ToString().ToLower() + "','" + _PermisoImprimir.ToString().ToLower() + "')");
                                break;
                            }                     
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgPedido_ItemCommand");
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
                        Inicializar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        #endregion

        #region Funciones
        private List<EntradaSalida> GetList()
        {
            try
            {
                List<EntradaSalida> entradasSalidas = new List<EntradaSalida>();
                EntradaSalida entsal = new EntradaSalida();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapEntradaSalida cnentSal = new CN_CapEntradaSalida();
                int ManAut;
                switch (cmbManAuto.SelectedValue)
                {
                    case "1":
                        ManAut = 1; //manual
                        break;
                    case "0":
                        ManAut = 0; //automatico
                        break;
                    default:
                        ManAut = -1; //todos los demas
                        break;
                }
                cnentSal.ConsultarEntradasSalidas(ref entradasSalidas, ref entsal, session,
                    txtNombre2.Text,
                    txtCliente1.Text == "" ? -1 : int.Parse(txtCliente1.Text), txtCliente2.Text == "" ? -1 : int.Parse(txtCliente2.Text),
                    ManAut,
                    txtProveedor1.Text == "" ? -1 : int.Parse(txtProveedor1.Text), txtProveedor2.Text == "" ? -1 : int.Parse(txtProveedor2.Text),
                    txtReferencia2.Text,
                    dpFecha1.SelectedDate, dpFecha2.SelectedDate,//<==
                    cmbEstatus.SelectedValue,
                    txtNumero1.Text == "" ? -1 : int.Parse(txtNumero1.Text), txtCliente3.Text == "" ? -1 : int.Parse(txtCliente3.Text));
                return entradasSalidas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LlenarCombos()
        {
            cmbManAuto.Items.Insert(0, new RadComboBoxItem("Manual", "1"));
            cmbManAuto.Items.Insert(0, new RadComboBoxItem("Automático", "0"));
            cmbManAuto.Items.Insert(0, new RadComboBoxItem("-- Todos --", ""));
            cmbManAuto.Sort = RadComboBoxSort.Ascending;
            cmbManAuto.SortItems();
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Baja", "b"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Impreso", "i"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Capturado", "c"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("-- Todos --", ""));
            cmbEstatus.Sort = RadComboBoxSort.Ascending;
            cmbEstatus.SortItems();
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

                    if (Permiso.PGrabar)
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = _PermisoGuardar;
                }
                else
                    Response.Redirect("Inicio.aspx");
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
        private void Imprimir(ref GridCommandEventArgs e, ref List<string> statusPosibles)
        {
            try
            {   ///El movimiento solo se podrá imprimir siempre y cuando se encuentre en estatus C capturado, e I de impreso.
                ///Lo manda a imprimir, y se cambia el estatus a impreso.
                statusPosibles = new List<string>() { "C", "I" };
                if (!statusPosibles.Contains(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Estatus"].Text.ToUpper()))
                {
                    Alerta("El documento se encuentra en estatus no válido");
                    e.Canceled = true;
                    return;
                }
                int Id_Emp = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Emp"].Text);
                int Id_Cd_Ver = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Cd"].Text);
                int Id_Es = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Es"].Text);
                int Es_Naturaleza = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Naturaleza"].Text);

                GridItem gi = Session["Pedido" + Session.SessionID] as GridItem;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

               

                EntradaSalida entSal = new EntradaSalida();
                new CN_CapEntradaSalida().ConsultarEncabezadoImprimir(sesion, Id_Emp, Id_Cd_Ver, Id_Es, Es_Naturaleza, ref entSal);
                entSal.Id_Es = Id_Es;
                
                if (entSal.Id_Tm == 25)
                {
                    CN_CatCliente cn_catcliente = new CN_CatCliente();
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Cte = entSal.Id_Cte;
                    cn_catcliente.ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    if (cliente.Cte_RemisionElectronica != -1)
                    {
                        ImprimirRemisionElectronica(entSal);
                        return;
                    }
                }

                ArrayList ALValorParametrosInternos = new ArrayList();                
                ALValorParametrosInternos.Add(entSal.Es_NaturalezaStr);
                ALValorParametrosInternos.Add(entSal.Es_Fecha);
                ALValorParametrosInternos.Add(entSal.Nombre_Cliente);
                ALValorParametrosInternos.Add(entSal.Calle);
                ALValorParametrosInternos.Add(entSal.Numero);//<
                ALValorParametrosInternos.Add(entSal.Colonia);
                ALValorParametrosInternos.Add(entSal.Municipio);
                ALValorParametrosInternos.Add(entSal.Estado);
                ALValorParametrosInternos.Add(entSal.Id_Cte);
                ALValorParametrosInternos.Add(entSal.Id_Tm);
                ALValorParametrosInternos.Add(entSal.Tm_Nombre);
                ALValorParametrosInternos.Add(entSal.Es_Referencia);
                ALValorParametrosInternos.Add(entSal.Id_Cd);
                ALValorParametrosInternos.Add((entSal.Id_Ter == -1) ? string.Empty : entSal.Id_Ter.ToString());
                ALValorParametrosInternos.Add(entSal.Id_Rik == -1 ? string.Empty : entSal.Id_Rik.ToString());
                ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}", entSal.Es_SubTotal));
                ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}", entSal.Es_Iva));
                ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}",entSal.Es_Total));
                //parametros para el cuerpo del reporte
                ALValorParametrosInternos.Add(Id_Emp);
                ALValorParametrosInternos.Add(Id_Es);
                ALValorParametrosInternos.Add(entSal.Es_Naturaleza);
                ALValorParametrosInternos.Add(entSal.Es_Notas);
                //conexion
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                //ALValorParametrosInternos.Add(entSal.Es_Notas);
                Type instance = null;
                instance = typeof(LibreriaReportes.EntSalImprimir);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                if (_PermisoImprimir)
                    RadAjaxManager1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                else
                    Alerta("No tiene permiso para imprimir");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void baja(ref GridCommandEventArgs e, ref List<string> statusPosibles, ref GridItem item)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DateTime fecha = DateTime.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Fecha"].Text);

            ///Si el documento es automatico no se puede cancelar.
            ///Si el documento no se puede dar de baja, el sistema mostrará un mensaje: 
            ///“Imposible dar de baja este documento.”
            if (rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ManAutStr"].Text != "Manual")
            {
                Alerta("Imposible dar de baja este documento");
                e.Canceled = true;
                return;
            }
            if (!(fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))////validar fecha dentro del periodo
            {
                Alerta("La fecha se encuentra fuera del periodo");
                e.Canceled = true;
                return;
            }
            //validar que no sea tipo impreso
            statusPosibles = new List<string>() { "B" };
            if (statusPosibles.Contains(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Estatus"].Text.ToUpper()))
            {
                Alerta("El documento se encuentra en estatus no válido para realizar la baja");
                e.Canceled = true;
                return;
            }
            //Si es un movimiento de entrada va a checar si se tiene disponible 
            //suficiente (inventario final menos asignado). 
            EntradaSalida entSal = new EntradaSalida();
            entSal.Id_Emp = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Emp"].Text);
            entSal.Id_Cd = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Cd"].Text);
            entSal.Id_Es = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Es"].Text);
            entSal.Es_Naturaleza = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Naturaleza"].Text);
            entSal.Id_Tm = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Tm"].Text);
            entSal.Id_Cte = -1;
            entSal.Es_CteCuentaNacional = -1;  
            new CN_CapEntradaSalida().ConsultarEntradaSalida(Sesion, Sesion.Id_Emp, Sesion.Id_Cd_Ver, entSal.Id_Es, entSal.Es_Naturaleza, ref entSal);//, ref dt);
            entSal.Id_U = Sesion.Id_U;
            entSal.Es_Estatus = "B";
            
            List<EntradaSalidaDetalle> detalles = new List<EntradaSalidaDetalle>();
            //DataTable dt = new DataTable();
            new CN_CapEntradaSalida().ConsultarEntradaSalidaDetalles(Sesion, entSal, ref detalles);//, ref dt);
            //Cuando es cancelacion de una entrada
            if (rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Es_Naturaleza"].Text == "0") // 0 es entrada, 1 es salida
            {   //***************************************
                //Agrupar lista de EntsalDetalles y VALIDAR el DISPONIBLE de los productos
                DataTable dt_detalles = new DataTable();
                dt_detalles.Columns.Add("Id_Prd");
                dt_detalles.Columns.Add("Cantidad");
                foreach (EntradaSalidaDetalle EnSalDet in detalles)
                {
                    dt_detalles.Rows.Add(new object[] { EnSalDet.Id_Prd, EnSalDet.Es_Cantidad });
                }
                DataTable dt_detalles2 = new DataTable();
                dt_detalles2.Columns.Add("Id_Prd");
                dt_detalles2.Columns.Add("Cantidad");
                DataRow[] editable_dr;
                foreach (DataRow rowdt in dt_detalles.Rows)
                {
                    if (dt_detalles2.Select("Id_Prd='" + rowdt["Id_Prd"].ToString() + "'").Length > 0)
                    {
                        editable_dr = dt_detalles2.Select("Id_Prd='" + rowdt["Id_Prd"].ToString() + "'");
                        editable_dr[0].BeginEdit();
                        editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) + int.Parse(rowdt["Cantidad"].ToString());
                        editable_dr[0].AcceptChanges();
                    }
                    else
                        dt_detalles2.Rows.Add(new object[] { rowdt["Id_Prd"].ToString(), rowdt["Cantidad"].ToString() });
                }
               
                foreach (DataRow row in dt_detalles2.Rows)
                {
                    int disponible = 0;
                    int invFinal = 0;
                    int asignado = 0;
                    new CN_CapEntradaSalida().ConsultarDisponible(Sesion, int.Parse(row["Id_Prd"].ToString()), ref disponible, ref invFinal, ref asignado);
                    if (int.Parse(row["Cantidad"].ToString()) > disponible)
                    {// MSG asignado por antiguo sian
                        Alerta("Producto " + row["Id_Prd"].ToString() +
                            " inventario disponible insuficiente, inventario final: " + invFinal.ToString() +
                            ", asignado: " + asignado.ToString() + ", disponible: " + disponible.ToString());
                        e.Canceled = true;
                        return;
                    }
                }
            }
            ///Cuando es la cancelación de una entrada se le va a decrementar y cuando es 
            //la cancelación de una salida se va a incrementar el inventario 
            //CANCELAR EL MOVIMIENTO            
            int afecta = -1;
            switch (entSal.Id_Tm)
            {
                case 6:
                case 15:
                case 16:
                    afecta = 0;
                    break;
                case 7:
                case 11:
                case 12:
                case 13:
                    //Afectan remision
                    afecta = 1;
                    break;
                case 2:
                case 4:
                    //Afectan orden de compra
                    afecta = 2;
                    entSal.Es_Referencia = "sin ref";
                    break;
                case 14:
                    afecta = 3;
                    break;
                default:
                    //No afectan nada
                    break;
            }
            int verificador = -1;           
            try
            {

                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes cliente = new Clientes();
                cliente.Id_Emp = Sesion.Id_Emp;
                cliente.Id_Cd = Sesion.Id_Cd_Ver;
                cliente.Id_Cte = entSal.Id_Cte;

                new CN_CapEntradaSalida().BajaEntradaSalida(ref entSal, ref detalles, Sesion, ref verificador, afecta, entSal.Es_Naturaleza == 1 ? false : true, false);
                rgEntSal.Rebind();
                Alerta("El documento se dio de baja correctamente"); //<==CAMBIAR MSG

                if (entSal.Id_Cte != -1)
                {
                    cn_catcliente.ConsultaClientes(ref cliente, Sesion.Emp_Cnx);

                    cliente.Cte_RemisionElectronica = cliente.Cte_RemisionElectronica == null ? -1 : cliente.Cte_RemisionElectronica;

                    if (cliente.Cte_RemisionElectronica > 0)
                    {
                        ImprimirRemisionElectronica(entSal);
                    }

                }
              
            }
            catch(Exception ex)
            {
                Alerta(ex.Message);
            }        
        }
        private void Inicializar()
        {//funcion usada para el refresh del grid , despues de cambiar la pantalla "anidada"
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion.CalendarioIni >= dpFecha1.MinDate && Sesion.CalendarioIni <= dpFecha1.MaxDate)
                {
                    dpFecha1.DbSelectedDate = Sesion.CalendarioIni;
                }
                if (Sesion.CalendarioFin >= dpFecha2.MinDate && Sesion.CalendarioFin <= dpFecha2.MaxDate)
                {
                    dpFecha2.DbSelectedDate = Sesion.CalendarioFin;
                }

               
                rgEntSal.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImprimirRemisionElectronica(EntradaSalida entSal)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<EntradaSalidaDetalle> listaProdFacturaEspecialFinal = new List<EntradaSalidaDetalle>();

                entSal.Es_Estatus = "I";
                int verificador = 0;
                entSal.Id_Emp = sesion.Id_Emp;
                new CN_CapEntradaSalida().ModificarEntradaSalida_Estatus(entSal, sesion.Emp_Cnx, ref verificador);

              
                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes clientes = new Clientes();
                clientes.Id_Emp = sesion.Id_Emp;
                clientes.Id_Cd = sesion.Id_Cd_Ver;
                clientes.Id_Cte = entSal.Id_Cte;
                cn_catcliente.ConsultaClientes(ref clientes, sesion.Emp_Cnx);
                entSal.Id_Emp = sesion.Id_Emp;
                CN_CapEntradaSalida cn_catensal = new CN_CapEntradaSalida();
                cn_catensal.ConsultarEntradaSalidaDetalles(sesion, entSal, ref listaProdFacturaEspecialFinal);

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
                XML_Enviar.Append(" CuentaNacional=\"\"");
                XML_Enviar.Append(" Status=\"\"");
                XML_Enviar.Append(" Fecha=\"\"");
                XML_Enviar.Append(" Remision=\"\"");
                XML_Enviar.Append(" Total=\"\"/>");

                

                XML_Enviar.Append(" <DetalleKey>");
                if (listaProdFacturaEspecialFinal.Count() > 0)
                {
                    foreach (EntradaSalidaDetalle d in listaProdFacturaEspecialFinal)
                    {

                        XML_Enviar.Append(" <Producto");
                        XML_Enviar.Append(" Codigo=\"" + d.Id_Prd.ToString() + "\"");
                        XML_Enviar.Append(" Descipcion=\"" + d.Prd_Descripcion.ToString().Replace("\"", "").Replace("'", "").Replace("&", "")
                            + "\"");
                        XML_Enviar.Append(" Cantidad=\"" + d.Es_Cantidad + "\"");
                        XML_Enviar.Append(" Unidad=\"" + d.Prd_Unidad + "\"");
                        XML_Enviar.Append(" Presentacion=\"" + d.Prd_Presentacion + "\"");
                        XML_Enviar.Append(" Precio=\"" + d.Es_Costo + "\"");                      
                        XML_Enviar.Append(" />");
                    }
                }
                XML_Enviar.Append(" </DetalleKey>");
                
                XML_Enviar.Append(" </RemisionCuentaNacional>");



                XmlDocument xml = new XmlDocument();

                xml.LoadXml(XML_Enviar.ToString());


                XmlNode RemisionCuentaNacional = xml.SelectSingleNode("RemisionCuentaNacional");
                RemisionCuentaNacional.Attributes["TipoDocumento"].Value = "Entrada";

                XmlNode Sucursal = RemisionCuentaNacional.SelectSingleNode("Sucursal");
                Sucursal.Attributes["CDINum"].Value = entSal.Id_Cd.ToString();
                Sucursal.Attributes["CDINom"].Value = "Prueba";
                Sucursal.Attributes["CDIIVA"].Value = iva.ToString();


                XmlNode Documento = RemisionCuentaNacional.SelectSingleNode("Documento");
                Documento.Attributes["Folio"].Value = entSal.Id_Es.ToString();
                Documento.Attributes["Status"].Value = entSal.Es_Estatus.ToString();
                Documento.Attributes["CuentaNacional"].Value = entSal.Es_CteCuentaNacional.ToString();
                Documento.Attributes["Remision"].Value = entSal.Es_Referencia.ToString();
                Documento.Attributes["Fecha"].Value = entSal.Es_Fecha.ToShortDateString();               
                Documento.Attributes["Total"].Value = entSal.Es_Total.ToString();


               

                
              

                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xml.WriteTo(tx);
                string xmlString = sw.ToString();

                WS_RemElectronicaCtaNacional.Service1 remelectronica = new WS_RemElectronicaCtaNacional.Service1();

                string sianRemisionElectronicaResult = remelectronica.Imprime_Entrada(xmlString).ToString();

                byte[] PDFRemision = this.Base64ToByte(sianRemisionElectronicaResult);


                string tempPDFname = string.Concat("BAJAREMISION_", entSal.Id_Emp.ToString(), "_", entSal.Id_Cd.ToString(), "_", entSal.Id_U.ToString(), ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(System.Configuration.ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(System.Configuration.ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, PDFRemision);
                RadAjaxManager1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
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

        private void limpiarCamposBusqueda()
        {
            txtNombre2.Text = "";
            txtCliente1.Text = "";
            txtCliente2.Text = "";
            cmbManAuto.SelectedIndex = 0;
            txtProveedor1.Text = "";
            txtProveedor2.Text = "";
            txtReferencia2.Text = "";
            dpFecha1.SelectedDate = null;
            dpFecha2.SelectedDate = null;
            cmbEstatus.SelectedIndex = 0;
            txtNumero1.Text = "";
            txtCliente3.Text = "";
        }
        #endregion

        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
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
    }
}
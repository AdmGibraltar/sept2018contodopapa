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
using System.Text;
using System.IO;
using CapaDatos;
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using Telerik.Reporting.Processing;


namespace SIANWEB
{
    public partial class CapRemisiones_Lista : System.Web.UI.Page
    {

        #region Variables

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

        private bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private bool _PermisoEliminar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private bool _PermisoImprimir
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        LlenarCombos();
                        CargarCentros();
                        CargarTipoMovimiento();
                        txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                        txtFecha2.DbSelectedDate = Sesion.CalendarioFin;

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
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                        }
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }

        protected void RAM1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                GridItem gi = (Session["Gi" + Session.SessionID] as GridItem);
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        if (Session["PreguntarImpresion" + Session.SessionID] != null)
                        {
                            RAM1.ResponseScripts.Add("return Confirma();");
                        }
                        rgPedido.Rebind();
                        break;
                    case "ok":
                        int vi = -1;
                        if (Session["PreguntarImpresion" + Session.SessionID] != null)
                        {
                            vi = Convert.ToInt32(Session["PreguntarImpresion" + Session.SessionID]);
                            Session["PreguntarImpresion" + Session.SessionID] = null;
                            imprimirContratoAlGuardar(vi);
                        }
                        break;
                    case "no":
                        Session["PreguntarImpresion" + Session.SessionID] = null;
                        break;
                    default:
                        rgPedido.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void rg_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
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

        protected void rg_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            rgPedido.Rebind();
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                rgPedido.Rebind();
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
                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPedido_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                List<string> statusPosibles = new List<string>();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                switch (e.CommandName)
                {
                    case "Baja":
                        CD_CapDevolucionRemision cdDevRem = new CD_CapDevolucionRemision();
                        List<DevolucionRemisionDet> listDevRemDet = new List<DevolucionRemisionDet>();

                        int vId_Rem = Convert.ToInt32((e.Item as GridDataItem)["Id_Rem"].Text);

                        cdDevRem.ConsultaDetallePorRemision(ref listDevRemDet, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, vId_Rem);

                        if (listDevRemDet.Any())
                        {
                            Alerta("La remisión tiene devoluciones aplicadas. No se puede dar de baja.");
                            return;
                        }
                        else
                        {
                            baja(ref e);
                        }
                        break;
                    case "Imprimir":
                        Imprimir(ref e);
                        //el estatus impreso lo asigna el reporte                        
                        break;
                    case "ImprimirContrato":
                        ImprimirContrato(ref e);
                        //el estatus impreso lo asigna el reporte                        
                        break;
                    case "Editar":
                        DateTime fecha = DateTime.Parse(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_Fecha"].Text);
                        bool permitirModificar = _PermisoModificar;
                        string mensaje = "";
                        if (!(fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))//validar fecha dentro del periodo
                        {
                            mensaje = "La fecha se encuentra fuera del periodo";
                            permitirModificar = false;
                        }

                        statusPosibles = new List<string>() { "C" };
                        if (!statusPosibles.Contains(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.ToUpper()))
                        {
                            mensaje = "El documento se encuentra en estatus no válido para realizar la modificación";
                            permitirModificar = false;
                        }

                        if (rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_ManAutStr"].Text.ToLower() != "manual")
                        {
                            mensaje = "El documento fue creado de manera automática y no puede ser modificado";
                            permitirModificar = false;
                        }

                        string Id_Rem = rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text;
                        CN_CapRemision cn_capremision = new CN_CapRemision();
                        int verificador = 0;
                        cn_capremision.ConsultarPermitirModificar(Convert.ToInt32(Id_Rem), Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, ref verificador);
                        if (verificador > 0)
                        {
                            mensaje = "La remisión ya tiene movimientos aplicados, no es posible modificarla";
                            permitirModificar = false;
                        }
                        RAM1.ResponseScripts.Add("return OpenWindow('" + 2 + "','" + Id_Rem + "','" + permitirModificar + "','" + mensaje + "')");
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgPedido_ItemCommand");
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
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Rem").ToString());

                Button = (WebControl)item["Imprimir"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Rem").ToString());

                Button = (WebControl)item["ImprimirContrato"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Rem").ToString());
            }
        }
        protected void ImgExportar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //rgPedido.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
                //rgPedido.ExportSettings.IgnorePaging = true;
                //rgPedido.ExportSettings.ExportOnlyData = true;
                //rgPedido.ExportSettings.OpenInNewWindow = true;
                //rgPedido.ExportSettings.FileName = "Listado remisiones";
                //rgPedido.MasterTableView.ExportToExcel();

                //rgPedido.PageSize = RadGrid1.MasterTableView.VirtualItemCount;
                //rgPedido.ExportSettings.IgnorePaging = true;
                //rgPedido.ExportSettings.OpenInNewWindow = true;
                //rgPedido.ExportSettings.FileName = "Listado remisiones";
                //rgPedido.MasterTableView.ExportToExcel();
                GenerarExcel();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, "ImgExportar_Click");
            }

        }
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "ImprimirTodo")
                {
                    //  ImprimirT();
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion

        #region Funciones

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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

                    //boton toolbar "nuevo"     
                    rtb1.Items.FindItemByValue("new").Visible = _PermisoGuardar;
                    rtb1.Items.FindItemByValue("remisionPedido").Visible = _PermisoGuardar;

                    if (Permiso.PModificar)
                    {
                        //columna editar
                    }
                    //rgPedido.Columns.FindByUniqueName("Editar").Visible = Permiso.PModificar;

                    if (Permiso.PEliminar)
                    {
                        //columna borrar
                        //((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Enabled = _PermisoEliminar;
                    }
                    rgPedido.Columns.FindByUniqueName("Baja").Visible = Permiso.PEliminar;

                    rgPedido.Columns.FindByUniqueName("Imprimir").Visible = Permiso.PImprimir;

                    rgPedido.Columns.FindByUniqueName("ImprimirContrato").Visible = Permiso.PImprimir;
                    //((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible = false;
                    //((RadToolBarItem)rtb1.Items.FindItemByValue("delete")).Visible = false;
                    ////Regresar
                    //((RadToolBarItem)rtb1.Items.FindItemByValue("undo")).Visible = false;
                    ////Imprimir
                    //((RadToolBarItem)rtb1.Items.FindItemByValue("print")).Visible = false;
                    ////Correo
                    //((RadToolBarItem)rtb1.Items.FindItemByValue("mail")).Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Remision> GetList()//C:\Documents and Settings\Usuario\mis documentos\visual studio 2010\Projects\SIANWEB\SIANWEB\CapRutaServicio.aspx
        {
            try
            {
                List<Remision> remisiones = new List<Remision>();
                Remision remision = new Remision();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapRemision cn_remision = new CN_CapRemision();
                int ManAut;
                switch (cmbTipo.SelectedValue)
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
                cn_remision.ConsultarRemisiones(ref remisiones, ref remision, session,
                    txtNombre.Text,
                    txtCliente1.Text == "" ? -1 : int.Parse(txtCliente1.Text), txtCliente2.Text == "" ? -1 : int.Parse(txtCliente2.Text),
                    ManAut,
                    txtFecha1.SelectedDate, txtFecha2.SelectedDate, cmbEstatus.SelectedValue, //<==
                    txtPedido1.Text == "" ? -1 : int.Parse(txtPedido1.Text), txtPedido2.Text == "" ? -1 : int.Parse(txtPedido2.Text),
                    txtPedido3.Text == "" ? -1 : int.Parse(txtPedido3.Text),
                    TxtRem_OrdCompra.Text,
                    txtTipoId.Text == "" ? -1 : int.Parse(txtTipoId.Text));
                //, txtCliente2.Text == "" ? -1 : int.Parse(txtTipoId.Text));
                return remisiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarCombos()
        {
            cmbTipo.Items.Insert(0, new RadComboBoxItem("Manual", "1"));
            cmbTipo.Items.Insert(0, new RadComboBoxItem("Automático", "0"));
            cmbTipo.Items.Insert(0, new RadComboBoxItem("-- Todos --", ""));
            //cmbTipo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", ""));
            this.cmbTipo.Sort = RadComboBoxSort.Ascending;
            this.cmbTipo.SortItems();

            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Entregado", "n"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Embarque", "e"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Baja", "b"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Impreso", "i"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Capturado", "c"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Solicitado", "s"));
            //cmbEstatus.Items.Insert(0, new RadComboBoxItem("Autorizado", "a"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("rechazado", "r"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("-- Todos --", ""));
            //cmbEstatus.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", ""));
            this.cmbEstatus.Sort = RadComboBoxSort.Ascending;
            this.cmbEstatus.SortItems();
        }

        private void Imprimir(ref GridCommandEventArgs e)
        {
            try
            {
                ///Valida que el estatus esté en capturado, impreso, embarcado o entregado.
                ///Que sea diferente a baja
                ///Lo manda a imprimir, y se cambia el estatus a impreso.
                List<string> statusPosibles = new List<string>() { "C", "I", "E", "N", "A" };
                if (!statusPosibles.Contains(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.Trim().ToUpper()))
                {
                    Alerta("El documento se encuentra en estatus no válido");
                    e.Canceled = true;
                    return;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Emp = sesion.Id_Emp;
                int Id_Cd_Ver = sesion.Id_Cd_Ver;
                int Id_Rem = int.Parse(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text);

                GridItem gi = Session["Pedido" + Session.SessionID] as GridItem;

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

        private void ImprimirContrato(ref GridCommandEventArgs e)
        {
            try
            {
                List<string> statusPosibles = new List<string>() { "60" };
                if (!statusPosibles.Contains(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Tm"].Text.ToUpper()))
                {
                    Alerta("El documento no es de tipo de movimiento 60");
                    e.Canceled = true;
                    return;
                }
                ///Valida que el estatus esté en capturado, impreso, embarcado o entregado.
                ///Que sea diferente a baja
                statusPosibles = new List<string>() { "C", "I", "E", "N" };
                if (!statusPosibles.Contains(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.ToUpper()))
                {
                    Alerta("El documento se encuentra en estatus no válido");
                    e.Canceled = true;
                    return;
                }
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Emp = sesion.Id_Emp;
                int Id_Cd_Ver = sesion.Id_Cd_Ver;
                int Id_Rem = int.Parse(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text);

                GridItem gi = Session["Pedido" + Session.SessionID] as GridItem;
                Remision remision = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 1);
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

        private void imprimirContratoAlGuardar(int Id_Rem)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Emp = sesion.Id_Emp;// int.Parse(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Emp"].Text);
                int Id_Cd_Ver = sesion.Id_Cd_Ver;// int.Parse(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Cd"].Text);

                GridItem gi = Session["Pedido" + Session.SessionID] as GridItem;
                Remision remision = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 1);
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

                Type instance = null;
                instance = typeof(LibreriaReportes.RemisionContratoComodato);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;

                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
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

        private void baja(ref GridCommandEventArgs e)
        {
            List<string> statusPosibles;
            List<RemisionesVencidas> vRemList = new List<RemisionesVencidas>();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DateTime fecha = DateTime.Parse(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_Fecha"].Text);

            if (!(fecha >= sesion.CalendarioIni && fecha <= sesion.CalendarioFin))////validar fecha dentro del periodo
            {
                Alerta("La fecha se encuentra fuera del periodo");
                e.Canceled = true;
                return;
            }

            //validar que no sea tipo impreso
            statusPosibles = new List<string>() { "B" };
            if ((statusPosibles.Contains(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_Estatus"].Text.ToUpper())))
            {
                Alerta("El documento se encuentra en estatus no válido para realizar la baja");
                e.Canceled = true;
                return;
            }

            // REMTIPO debe ser diferente a A para poderse eliminar. ?????
            // posiblemente validar que sea manual
            if (rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Rem_ManAutStr"].Text.ToLower() != "manual")
            {
                Alerta("El documento fue creado de manera automática y no puede ser modificado");
                e.Canceled = true;
                return;
            }

            // afectar inventario final <== se afecta en el sp

            Remision remision = new Remision();
            remision.Id_Emp = Convert.ToInt32(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Emp"].Text);
            remision.Id_Cd = Convert.ToInt32(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Cd"].Text);
            remision.Id_Rem = Convert.ToInt32(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text);
            remision.Id_Ter = Convert.ToInt32(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Ter"].Text);
            remision.Rem_Fecha = fecha;
            remision.Rem_Estatus = "B";
            remision.Id_Tm = Convert.ToInt32(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Tm"].Text);
            remision.Id_Cte = Convert.ToInt32(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Cte"].Text);
            if (Convert.ToInt32(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Ped"].Text) > 0)
            {
                remision.Id_Ped = Convert.ToInt32(rgPedido.MasterTableView.Items[e.Item.ItemIndex]["Id_Ped"].Text);
            }
            else
            {
                remision.Id_Ped = -1;
            }

            new CN_CapRemision().ConsultaRemisionesVencidas(sesion, remision, ref vRemList);

            if (vRemList.Where(r => r.Id_Rem == remision.Id_Rem && r.Rem_Dev > 0).Any())
            {
                Alerta("El documento tiene devoluciones y no puede ser dado de baja.");
                e.Canceled = true;
                return;
            }

            new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, remision.Id_Rem, ref remision, 0);

            List<RemisionDet> detalles = new List<RemisionDet>();
            new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);

            int verificador12 = -1;
            try
            {
                new CN_CapRemision().BajaRemision(ref remision, ref detalles, sesion, ref verificador12);
                if (remision.Rem_CteCuentaNacional > 0)
                {
                    ImprimirRemisionElectronica(remision);
                }
                //                else
                //                {
                //                    new CN_CapRemision().BajaRemision(ref remision, ref detalles, sesion, ref verificador12);
                //                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                return;
            }

            rgPedido.Rebind();

            Alerta("El movimiento se dio de baja correctamente");
        }

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
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

        private void CargarTipoMovimiento()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatMovimiento_ComboParaRemisiones", ref cmbTipoMovimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Tm_ReqSpo
        {
            set
            {
                Session["Tm_ReqSpoREM" + Session.SessionID] = value;
            }
            get
            {
                return (bool)Session["Tm_ReqSpoREM" + Session.SessionID];
            }
        }

        protected void cmbTipoMovimiento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if (cmbTipoMovimiento.SelectedValue != "" && cmbTipoMovimiento.SelectedValue != "-1")
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    bool Tm_ReqSpo2 = false;
                    new CN_CatMovimientos().ConsultarTmovimientoReqSpo(sesion, int.Parse(cmbTipoMovimiento.SelectedValue), ref Tm_ReqSpo2);
                    Tm_ReqSpo = Tm_ReqSpo2;

                    hf_spo.Value = Tm_ReqSpo.ToString();
                }

                int tipo = Convert.ToInt32(cmbTipoMovimiento.SelectedValue);
                //txtClienteId.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void GenerarExcel()
        {
            try
            {

                StringBuilder tabla = new StringBuilder();
                Funcion fn = new Funcion();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                fn.ExportarExcel("Listado_Remisiones", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalle(ref StringBuilder Tabla)
        {
            try
            {
                String width;


                List<Remision> List = new List<Remision>();
                List = GetList();
                DataTable dt = new DataTable();

                dt = Funcion.Convertidor<Remision>.ListaToDatatable(List);

                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "UsuNom")
                    {
                        width = (i == 0) ? "180px" : "210px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Usuario");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rem_ManAutStr")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Man/Aut");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rem_TipoStr")
                    {
                        width = (i == 0) ? "180px" : "210px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Tipo");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rem_EstatusStr")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Left' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Estatus");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Rem")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Número");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rem_Fecha")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Cte")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Núm. Cte.");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cte_NomComercial")
                    {
                        width = (i == 0) ? "370px" : "300px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Cliente");
                        Tabla.Append("</th>");
                    }

                    else if (dt.Columns[i].ColumnName == "Rem_Subtotal")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Subtotal");
                        Tabla.Append("</th>");
                    }

                    else if (dt.Columns[i].ColumnName == "Rem_Iva")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("I.V.A.");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rem_Total")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Total");
                        Tabla.Append("</th>");
                    }



                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "UsuNom")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_ManAutStr")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_TipoStr")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_EstatusStr")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Rem")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_Fecha")
                        {

                            DateTime datetime = Convert.ToDateTime(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(datetime.ToShortDateString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Cte")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cte_NomComercial")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_Subtotal")
                        {
                            double valor = double.Parse(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_Iva")
                        {
                            double valor = double.Parse(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rem_Total")
                        {
                            double valor = double.Parse(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }

                    }
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
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

        protected void txtPedido1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
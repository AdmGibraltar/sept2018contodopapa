using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Mime;
using System.Web.Services;

namespace SIANWEB
{
    public partial class CapEntSalAutorizacion : System.Web.UI.Page
    {
        #region Variables

        private string Emp_CnxCen
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }
        public int strEmp
        {
            get
            {
                int VGEmpresa = 0;
                Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"], out VGEmpresa);
                return VGEmpresa;
            }
        }
        bool fecha_focus = false;
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
        }
        private List<EntSalSolicitudDet> list_Es
        {
            get { return (List<EntSalSolicitudDet>)Session["ListEs" + Session.SessionID + HF_ClvPag.Value]; }
            set { Session["ListEs" + Session.SessionID + HF_ClvPag.Value] = value; }
        }

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool actualizacionDocumento
        {
            set { Session["actualizacionDocumentoES" + Session.SessionID + HF_ClvPag.Value] = value; }
            get { return (bool)Session["actualizacionDocumentoES" + Session.SessionID + HF_ClvPag.Value]; }
        }

        /// <summary>
        /// Grupo 1: Movimientos 2 y 4 |
        /// Grupo 2: Movimientos 6, 15 y 16 |
        /// Grupo 3: Movimientos 7, 11, 12 y 13 |
        /// Grupo 4: Movimientos 14
        /// </summary>
        private int GrupoActual
        {
            get
            {
                string[] grupo1 = new string[] { "2", "4" };
                string[] grupo2 = new string[] { "6", "15", "16" };
                string[] grupo3 = new string[] { "7", "11", "12", "13" };
                string[] grupo4 = new string[] { "14" };

                if (grupo1.Contains(cmbTipoMovimento.SelectedValue))
                {
                    return 1;
                }
                else if (grupo2.Contains(cmbTipoMovimento.SelectedValue))
                {
                    return 2;
                }
                else if (grupo3.Contains(cmbTipoMovimento.SelectedValue))
                {
                    return 3;
                }
                else if (grupo4.Contains(cmbTipoMovimento.SelectedValue))
                {
                    return 4;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
                {
                    CerrarVentana();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);

                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        Inicializar();

                        if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
                        {
                            GridCommandItem cmdItem = (GridCommandItem)rgEntradaSalida.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 

                            rgEntradaSalida.MasterTableView.Columns[rgEntradaSalida.MasterTableView.Columns.Count - 2].Display = false;
                            rgEntradaSalida.MasterTableView.Columns[rgEntradaSalida.MasterTableView.Columns.Count - 3].Display = false;
                            ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button 
                                             }
                        else
                        {
                            BtnAutorizar.Visible = false;
                            BtnRechazar.Visible = false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void RAM1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 180);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "new":
                        Nuevo();
                        break;
                    case "save":
                        Guardar();
                        break;
                    case "rechazar":
                        BtnRechazar_Click(null, EventArgs.Empty);
                        break;
                    case "autorizar":
                        Autorizar();
                        break;


                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void BtnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Autorizar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }



        protected void BtnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("return AbrirVentana_RechazarSolicitud('"+  sesion.Id_Emp.ToString() + "','" + sesion.Id_Cd.ToString() + "','" + this.txtFolio.Text +  "')");

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "BtnRechazar_Click");
            }
        }


        protected void dpFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e) { }
        protected void rgEntradaSalida_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgEntradaSalida.DataSource = list_Es;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtOC_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtOCId.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtOCId.Text) > 0)
                    {
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                        String Resultado = "";
                        new CN_CapOrdenCompra().SP_Consulta_Saldo_OC(Convert.ToInt32(sesion.Id_Emp), Convert.ToInt32(sesion.Id_Cd_Ver), Convert.ToInt32(txtOCId.Text), sesion.Emp_Cnx, ref Resultado);

                        if (Resultado.Trim() != "")
                        {
                            Alerta(Resultado);

                        }
                        else
                        {
                            List<OrdenCompra> listOrdenCompra = new List<OrdenCompra>();
                            List<OrdenCompra> listOrdenCompraFinal = new List<OrdenCompra>();
                            OrdenCompra ordCompra = new OrdenCompra();
                            ordCompra.Id_Emp = sesion.Id_Emp;
                            ordCompra.Id_Cd = sesion.Id_Cd_Ver;
                            ordCompra.Id_U = sesion.Propia ? sesion.Id_U : -1;

                            new CN_CapOrdenCompra().ConsultaOrdenCompra_Lista(ordCompra, sesion.Emp_Cnx, ref listOrdenCompra
                            , Convert.ToInt32(txtOCId.Text)
                            , Convert.ToInt32(txtOCId.Text)
                            , DateTime.MinValue
                            , DateTime.MinValue
                            , ""
                            );


                            if (listOrdenCompra.Count != 0)
                            {

                                String CerosMes = "";
                                String CerosDia = "";
                                foreach (OrdenCompra oCompra in listOrdenCompra)
                                {
                                    if (oCompra.Ord_Fecha.Day < 10)
                                    {
                                        CerosDia = "0";
                                    }
                                    if (oCompra.Ord_Fecha.Month < 10)
                                    {
                                        CerosMes = "0";
                                    }



                                    txtFechaOC.Text = CerosDia + Convert.ToString(oCompra.Ord_Fecha.Day) + "/" + CerosMes + Convert.ToString(oCompra.Ord_Fecha.Month + "/" + oCompra.Ord_Fecha.Year);
                                }


                                List<OrdenCompraDet> listaOrdCompraDet = new List<OrdenCompraDet>();
                                OrdenCompraDet ordenCompraDet = new OrdenCompraDet();

                                ordenCompraDet.Id_Emp = sesion.Id_Emp;
                                ordenCompraDet.Id_Cd = sesion.Id_Cd_Ver;
                                ordenCompraDet.Id_Ord = Convert.ToInt32(txtOCId.Text);
                                new CN_CapOrdenCompraDet().spCapOrdCompraDetalle_Consulta_Entradas(ordenCompraDet, sesion.Emp_Cnx, ref listaOrdCompraDet);


                                double total = 0;


                                List<EntSalSolicitudDet> detalles = new List<EntSalSolicitudDet>();
                                EntSalSolicitudDet EntSalDetalle = new EntSalSolicitudDet();

                                for (int i = 0; i < listaOrdCompraDet.Count; i++)
                                {
                                    OrdenCompraDet orden = listaOrdCompraDet[i];
                                    total += orden.Ord_Cantidad * orden.ProductoPrecio.Prd_Pesos;



                                    EntSalDetalle = new EntSalSolicitudDet();
                                    EntSalDetalle.Id_Emp = orden.Id_Emp;
                                    EntSalDetalle.Id_Cd = orden.Id_Cd;
                                    EntSalDetalle.Id_EsDetStr = Convert.ToString(i + 1);
                                    EntSalDetalle.Id_Ter = 0;
                                    EntSalDetalle.Ter_Nombre = "";
                                    EntSalDetalle.Id_Prd = orden.Id_Prd;
                                    EntSalDetalle.Prd_AgrupadoSpo = 0;
                                    EntSalDetalle.Prd_Descripcion = orden.Producto.Prd_Descripcion;
                                    EntSalDetalle.Prd_Presentacion = orden.Producto.Prd_Presentacion;
                                    EntSalDetalle.Prd_Unidad = orden.Producto.Prd_UniNe;
                                    EntSalDetalle.Presentacion = orden.Producto.Prd_Presentacion;

                                    EntSalDetalle.Afct_OrdCompra = true;
                                    EntSalDetalle.Afecta = true;
                                    EntSalDetalle.Importe = Convert.ToInt32(orden.Ord_Cantidad) * orden.ProductoPrecio.Prd_Pesos;

                                    // dt.Rows.Add(EntSalDetalle);
                                    detalles.Add(EntSalDetalle);

                                }

                                list_Es = detalles;
                                rgEntradaSalida.DataSource = list_Es;
                                rgEntradaSalida.Rebind();

                                CalcularTotales();


                                txtTotalOC.Text = Convert.ToString(total);

                            }
                            else
                            {
                                Alerta("Folio de OC NO existe;Favor de Capturar un Folio de OC Valido");
                                txtOCId.Focus();
                                return;

                            }


                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rgEntradaSalida_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.IsInEditMode))
            {
                GridDataItem item = e.Item as GridDataItem;
                RadComboBox rcb = item.FindControl("cmbTerritorio") as RadComboBox;
                RadNumericTextBox dataField = item.FindControl("txtId_Prd") as RadNumericTextBox;

                RadNumericTextBox rtb = item.FindControl("txtTerritorio") as RadNumericTextBox;
                if (txtTerritorioNombre.Visible)
                {
                    cargarTerritorioDetalles(ref rcb);
                }
                else
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1), sesion.Emp_Cnx, "spCatTerCte_Combo", ref rcb);
                }

                if (rcb.Items.Count > 0)
                {
                    rtb.DbValue = rcb.SelectedValue;
                }

                Control updatebtn = (Control)item.FindControl("UpdateButton");


                dataField.Enabled = true;
                if (updatebtn != null)
                {
                    string id_ter = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Ter"].ToString();
                    rcb.SelectedIndex = rcb.FindItemIndexByValue(id_ter);

                    rcb.Text = rcb.Items[rcb.SelectedIndex].Text;
                    rtb.DbValue = id_ter;
                    dataField.Enabled = false;
                }

                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form.FindControl("RadNumericTextBoxCantidad");
                    }
                    dataField.Focus();
                }
            }
        }
        protected void rgEntradaSalida_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                string estatus_registro = "0";
                int cantidad_registro = 0;
                if (e.CommandName == "InitInsert")
                {
                    estatus_registro = "2";
                }
                else if (e.CommandName == "Edit")
                {
                    int cantidad_A = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["cantidad"].FindControl("CantidadLabel") as Label).Text);
                    estatus_registro = "1"; //1=Edit
                    cantidad_registro = cantidad_A;
                }
                else
                {
                    Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value] = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["cantidad"].FindControl("CantidadLabel") as Label).Text);
                    int prd_ = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                    int ter_ = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Id_Ter"].FindControl("TerLabel") as Label).Text);
                    int can_ = 0;
                    string nat_ = cmbNaturaleza.SelectedValue;
                    string ref_ = txtReferencia.Text;
                    string cte_ = txtClienteId.Text;
                    string es_ = txtFolio.Text;
                    string mov_ = txtTipoId.Text;
                    int _gpo;
                    switch (Convert.ToInt32(txtTipoId.Text))
                    {
                        case 2:
                        case 4:
                            _gpo = 1;
                            break;
                        case 6:
                        case 15:
                        case 16:
                            _gpo = 2;
                            break;
                        case 7:
                        case 11:
                        case 12:
                        case 13:
                            _gpo = 3;
                            break;
                        case 14:
                            _gpo = 4;
                            break;
                        default:
                            _gpo = 0;
                            break;
                    }
                    string valor_retorno = "";
                    Producto producto = new Producto();

                    valor_retorno = Producto_Cantidad(sesion, valor_retorno.ToString(), nat_ == "" ? "-1" : nat_, _gpo.ToString() == "" ? "-1" : _gpo.ToString(), Convert.ToInt32(prd_), ref_ == "" ? "-1" : ref_, Convert.ToInt32(es_), Convert.ToInt32(ter_), can_, mov_ == "" ? "-1" : mov_, cte_ == "" ? "-1" : cte_, producto);
                    string[] valores = valor_retorno.Split(new string[] { "@@" }, StringSplitOptions.None);
                    if (valores.Length > 1)
                    {
                        Alerta(valores[1]);
                        e.Canceled = true;
                        return;
                    }


                    estatus_registro = "3";
                }
                Session["estatus" + Session.SessionID + HF_ClvPag.Value] = estatus_registro;
                Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value] = cantidad_registro;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEntradaSalida_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                EntSalSolicitudDet Es_Det = new EntSalSolicitudDet();
                Es_Det.Id_EsDetStr = Guid.NewGuid().ToString();

                if (this.txtTipoId.Text == "2")
                {
                    if (double.Parse((editedItem.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Text) == 0)
                    {
                        Alerta("El costo no puede ser 0");
                        e.Canceled = true; return;
                    }
                }

                if ((editedItem.FindControl("cmbTerritorio") as RadComboBox).SelectedIndex < 1 && rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("territorio").OrderIndex - 2].Display)
                {
                    Alerta("No se ha seleccionado un territorio");
                    e.Canceled = true; return;
                }

                GenerarDetalle(editedItem, ref Es_Det);

                if (list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_Prd == Es_Det.Id_Prd).ToList().Count > 0)
                {
                    Alerta("El producto ya fue capturado");
                    e.Canceled = true; return;
                }

                list_Es.Add(Es_Det);
                rgEntradaSalida.Rebind();
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEntradaSalida_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                EntSalSolicitudDet Es_Det = new EntSalSolicitudDet();
                Es_Det.Id_EsDetStr = (editedItem["Id_EsDetStr"].FindControl("lblDet_Edit") as Label).Text;
                if ((editedItem.FindControl("cmbTerritorio") as RadComboBox).SelectedIndex < 1 && rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("territorio").OrderIndex - 2].Display)
                {
                    Alerta("No se ha seleccionado un territorio");
                    e.Canceled = true; return;
                }

                GenerarDetalle(editedItem, ref Es_Det);

                if (list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_Prd == Es_Det.Id_Prd).ToList().Count > 1)
                {
                    Alerta("El producto ya fue capturado");
                    e.Canceled = true; return;
                }

                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Id_Ter = Es_Det.Id_Ter;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Ter_Nombre = Es_Det.Ter_Nombre;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Id_Prd = Es_Det.Id_Prd;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Prd_Descripcion = Es_Det.Prd_Descripcion;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Presentacion = Es_Det.Presentacion;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].ESol_Cantidad = Es_Det.ESol_Cantidad;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].ESol_EsCosto = Es_Det.ESol_EsCosto;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Importe = Es_Det.Importe;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Afecta = Es_Det.Afecta;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].ESol_BuenEstado = Es_Det.ESol_BuenEstado;

                rgEntradaSalida.Rebind();
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEntradaSalida_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                EntradaSalidaDetalle Es_Det = new EntradaSalidaDetalle();
                Es_Det.Id_EsDetStr = (editedItem["Id_EsDetStr"].FindControl("lblDet_Item") as Label).Text;
                list_Es.Remove(list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0]);

                rgEntradaSalida.Rebind();
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbNaturaleza_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //txtFolio.Text = consultarConsecutivo(Convert.ToInt32(cmbNaturaleza.SelectedValue)).ToString();
                CargarTipoMovimiento(Convert.ToInt32(cmbNaturaleza.SelectedValue));

                if (cmbNaturaleza.SelectedValue != "-1")
                {
                    dpFecha.Focus();
                }
                fecha_focus = true;
                cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);
                fecha_focus = false;
                cmbTipoMovimento.Enabled = false;
                cmbAfecta.SelectedIndex = 0;
                trCliente.Visible = false;
                trProveedor.Visible = false;

                //que cargue los movimientos sin seleccionar el afecta 28 mayo 
                cmbTipoMovimento.Enabled = true;
                txtTipoId.Enabled = true;
                //////cmbAfecta.SelectedIndex = 2;
                //////int AfectaId = 2;
                //////CargarTipoMovimiento(Convert.ToInt32(cmbNaturaleza.SelectedValue), AfectaId);
                //////cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);
                //////MostrarCampos(AfectaId);
                //////cmbAfecta.Visible = false;
                //////Label3.Visible = false;

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void cmbProveedor_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                int Id_pvd = Convert.ToInt32(txtProveedorId.Text);
                RadComboBox cmb = sender as RadComboBox;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Movimientos Movimientos = new Movimientos();
                Movimientos.Id_Emp = Sesion.Id_Emp;
                string bdCentral = (new SqlConnectionStringBuilder(Emp_CnxCen)).InitialCatalog;
                CN_CapEntradaSalida cn_entsal = new CN_CapEntradaSalida();
                Movimientos.Id = Id_pvd;
                Movimientos.Id_Cd = Sesion.Id_Cd_Ver;


                Movimientos.NatMov = Convert.ToInt32(cmbNaturaleza.SelectedValue);
                cn_entsal.ConsultaTProveedor(ref Movimientos, Sesion.Emp_Cnx, bdCentral);
                cn_entsal.ConsultaTMov(ref Movimientos, Sesion.Emp_Cnx, bdCentral);
                List<Movimientos> list_mov = new List<Movimientos>();
                list_mov.Add(Movimientos);


                if (Movimientos.Id == 0 && Movimientos.NatMov == 0)
                {
                    Movimientos.Id = 2;
                }

                if (Movimientos.Id == -1)
                {
                    txtTipoId.DbValue = null;
                    cmbTipoMovimento.Text = cmbTipoMovimento.Items[0].Text;
                    cmbTipoMovimento.SelectedIndex = 0;

                }
                else
                {

                    txtTipoId.DbValue = Movimientos.Id;
                    cmbTipoMovimento.Enabled = true;
                    txtTipoId.Enabled = true;
                    cmbTipoMovimento.Text = cmbTipoMovimento.Items[cmbTipoMovimento.FindItemIndexByValue(Movimientos.Id.ToString())].Text;
                    cmbTipoMovimento.SelectedIndex = cmbTipoMovimento.FindItemIndexByValue(Movimientos.Id.ToString());


                }
                LimpiarClienteProducto(true);
                showDetalles();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void CmbProveedorF_ClientSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                CargarTipoMovimiento(Convert.ToInt32(cmbNaturaleza.SelectedValue));
                cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Movimientos Movimientos = new Movimientos();
                string bdCentral = (new SqlConnectionStringBuilder(Emp_CnxCen)).InitialCatalog;
                CN_CapEntradaSalida cn_entsal = new CN_CapEntradaSalida();
                cn_entsal.ConsultaTMov(ref Movimientos, Sesion.Emp_Cnx, bdCentral);
                cmbTipoMovimento.SelectedValue = Movimientos.Id.ToString();


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void cmbAfecta_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                cmbTipoMovimento.Enabled = false;
                txtTipoId.Enabled = false;
                int AfectaId = Convert.ToInt32(cmbAfecta.SelectedValue);
                CargarTipoMovimiento(Convert.ToInt32(cmbNaturaleza.SelectedValue), AfectaId);
                cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);
                MostrarCampos(AfectaId);


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        private void MostrarCampos(int AfectaId)
        {
            bool mostrar_clie = false;
            bool mostrar_prov = false;



            LabelOC.Visible = false;
            txtOCId.Visible = false;
            txtOCId.Enabled = false;
            txtOCId.ReadOnly = true;

            LabelFechaOC.Visible = false;

            txtFechaOC.Visible = false;
            txtFechaOC.Enabled = false;
            txtFechaOC.ReadOnly = true;

            LabelTotalOC.Visible = false;

            txtTotalOC.Visible = false;
            txtTotalOC.Enabled = false;
            txtTotalOC.ReadOnly = true;

            trOCID.Visible = false;
            trFechaOC.Visible = false;
            trTotalOC.Visible = false;
            switch (AfectaId)
            {
                case 0:
                    trProveedor.Visible = false;
                    trCliente.Visible = true;
                    mostrar_clie = true;
                    trFechaReferencia.Visible = false;
                    cmbTipoMovimento.Enabled = true;
                    txtTipoId.Enabled = true;
                    break;
                case 1:
                    trCliente.Visible = false;
                    trProveedor.Visible = true;
                    mostrar_prov = false;
                    trFechaReferencia.Visible = true;
                    cmbTipoMovimento.Enabled = false;
                    txtTipoId.Enabled = false;
                    trOCID.Visible = true;
                    trFechaOC.Visible = true;
                    trTotalOC.Visible = true;

                    LabelOC.Visible = true;
                    txtOCId.Visible = true;
                    txtOCId.Enabled = true;
                    txtOCId.ReadOnly = false;

                    LabelFechaOC.Visible = true;

                    txtFechaOC.Visible = true;
                    txtFechaOC.Enabled = false;
                    txtFechaOC.ReadOnly = true;

                    LabelTotalOC.Visible = true;

                    txtTotalOC.Visible = true;
                    txtTotalOC.Enabled = false;
                    txtTotalOC.ReadOnly = true;
                    break;
                case 2:
                    trCliente.Visible = false;
                    trProveedor.Visible = false;
                    trFechaReferencia.Visible = false;
                    cmbTipoMovimento.Enabled = true;
                    txtTipoId.Enabled = true;
                    break;
                default:
                    mostrar_clie = false;
                    trCliente.Visible = false;
                    trProveedor.Visible = false;
                    mostrar_prov = false;
                    trFechaReferencia.Visible = false;
                    cmbTipoMovimento.Enabled = false;
                    break;

            }


            if (mostrar_clie)
            {
                LabelTerritorio.Visible = true;
                txtterritorio.Visible = true;
                txtTerritorioNombre.Visible = true;
                if (txtClienteId.Enabled)
                {
                    txtClienteId.Focus();
                }
                else
                {
                    txtReferencia.Focus();
                }
            }
            else if (mostrar_prov)
            {

                LabelTerritorio.Visible = false;
                txtterritorio.Visible = false;
                txtTerritorioNombre.Visible = false;
                if (Convert.ToInt32(this.txtTipoId.Text) == 26)
                {
                    this.txtProveedorFId.Focus();

                }
                else
                {
                    txtProveedorId.Focus();
                }
            }
            else
            {
                txtTipoId.Focus();
            }

        }
        protected void cmbTipoMovimento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                LimpiarClienteProducto();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();


                cn_comun.LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(cmbTipoMovimento.SelectedValue), sesion.Emp_Cnx, "spCatUsuario_Combo_AutorizadorSolicitudes", ref CmbId_UEnviar);

                 //@Id1 As int,    -- id_Empres  
                 //@Id2 As int,    -- Id_Cd
                 //@Id3 As int,    -- Id_TipoMovimiento
                 //@Id4 As int = -1  --  nada

                showDetalles();

                if (fecha_focus)
                {
                    dpFecha.Focus();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void showDetalles()
        {
          
            bool mostrar_prov = false;
            List<Movimientos> list_mov = ObtenerMovimiento(cmbTipoMovimento.SelectedValue);


            if (list_mov.Count > 0)
            {
                int AfectaId = Convert.ToInt32(cmbAfecta.SelectedValue);
                Movimientos mov = list_mov[0];
                LabelTerritorio.Visible = AfectaId == 0 && mov.ReqReferencia;
                txtTerritorioNombre.Visible = AfectaId == 0 && mov.ReqReferencia;
                txtClienteId.Enabled = !mov.ReqReferencia;

                txtRequerido.DbValue = (AfectaId == 0 && mov.ReqReferencia) ? true : false;
                mostrar_prov = (AfectaId == 1) ? true : false;


                RequiredFieldValidator4.ValidationGroup = !Convert.ToBoolean(mov.Afecta) ? "pestaniaDetalles" : "nn";
                RequiredFieldValidator6.ValidationGroup = Convert.ToBoolean(mov.Afecta) ? "pestaniaDetalles" : "nn";

                RequiredFieldValidator7.ValidationGroup = mov.ReqReferencia ? "pestaniaDetalles" : "nn";

                rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("territorio").OrderIndex - 2].Display = true;
                rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("buenEstado").OrderIndex - 2].Display = true;
                rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("afecta").OrderIndex - 2].Display = true;

                if (GrupoActual == 1)
                {
                    rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("territorio").OrderIndex - 2].Display = false;
                    rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("buenEstado").OrderIndex - 2].Display = false;
                }
                else if (GrupoActual == 0 || cmbNaturaleza.SelectedValue == "1")
                {
                    rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("territorio").OrderIndex - 2].Display = false;
                    rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("buenEstado").OrderIndex - 2].Display = false;

                }

                rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("afecta").OrderIndex - 2].Display = mov.AfeOrdComp;
            }

            double ancho = 0;
            foreach (GridColumn gc in rgEntradaSalida.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgEntradaSalida.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgEntradaSalida.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

            //JFCV 
            trFechaReferencia.Visible = mostrar_prov;
            trFechaReferencia.Visible = false;
            if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
            {
                mostrar_prov = false;
                BtnAutorizar.Visible = true;
                BtnRechazar.Visible = true;
            }
            else
            {
                BtnAutorizar.Visible = false;
                BtnRechazar.Visible = false;
            }

            //JFCV 
            RequiredFieldValidatorFechaReferencia.Enabled = mostrar_prov;
            RequiredFieldValidatorFechaReferencia.Enabled = false;

        }
        protected void rgEntradaSalida_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    if (txtTipoId.Text == "2" || txtTipoId.Text == "4")
                    {
                        (item.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                Random randObj = new Random(DateTime.Now.Millisecond);
                HF_ClvPag.Value = randObj.Next().ToString();
                dpFecha.SelectedDate = DateTime.Now;

                _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
                _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
                _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
                _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
                HFTipoOp.Value = Request.QueryString["TipoOp"].ToString();
                ValidarPermisos();
                CargarConsecutivo();
                CargarNaturaleza();
                CargarProveedor();
                CargarAfectacion();
                CargarCombosUsuarios();
                //JMM:Cargamos el combo de proveedor franquicia
                CargarProveedorFranquicia();
                list_Es = new List<EntSalSolicitudDet>();

                actualizacionDocumento = (Request.QueryString["id"] != "-1" && Request.QueryString["id"] != null);


                if (Request.QueryString["id"] != "-1" && Request.QueryString["id"] != null)//Edicion
                {
                    cargarMovimientoEntSal();
                }

                //RadAjaxManager1.ResponseScripts.Add("IniciarPaginasAuxiliares();");


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void CargarCombosUsuarios()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();

                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UEnviar);
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UCC);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CargarConsecutivo()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                int Id_ESol = 0;
                cn_es.CapEntSalSolicitud_ConsultaConsecutivo(sesion, ref Id_ESol);
                txtFolio.Text = Id_ESol.ToString();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void cargarMovimientoEntSal()
        {
            ////aqui se va traer la info del documento a editar             
            //EntradaSalida entradaSalida = new EntradaSalida();
            try
            {
                string ESol_Unique = Request.QueryString["id"].ToString();
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                EntSalSolicitud es = new EntSalSolicitud();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                cn_es.CapEntSalSolicitud_Consulta(ESol_Unique, ref es, sesion.Emp_Cnx);

                //cn_entsal.ConsultarEntradaSalida(sesion, sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Es, Es_Naturaleza, ref entradaSalida);
                cmbNaturaleza.SelectedValue = es.ESol_Naturaleza.ToString();

                CN_CatMovimientos CN_Mov = new CN_CatMovimientos();
                int afectaId = -1;
                CN_Mov.ConsultaTmovimientoAfecta(sesion, es.Id_Tm, ref afectaId);
                CargarTipoMovimiento(es.ESol_Naturaleza, afectaId);
                MostrarCampos(afectaId);

                cmbAfecta.SelectedIndex = cmbAfecta.FindItemIndexByValue(afectaId.ToString());
                cmbAfecta.Text = cmbAfecta.FindItemByValue(afectaId.ToString()).Text;

                CmbId_UEnviar.SelectedValue = es.Id_UEnviar.ToString();
                CmbId_UCC.SelectedValue = es.Id_UCC.ToString();
                txtFolio.Text = es.Id_ESol.ToString();
                dpFecha.SelectedDate = es.ESol_Fecha;
                txtTipoId.Text = es.Id_Tm.ToString();
                cmbTipoMovimento.SelectedIndex = cmbTipoMovimento.FindItemIndexByValue(es.Id_Tm.ToString());
                cmbTipoMovimento.Text = cmbTipoMovimento.FindItemByValue(es.Id_Tm.ToString()).Text;
                cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);
                txtClienteId.DbValue = es.Id_Cte == -1 ? (int?)null : es.Id_Cte;
                txtClienteNombre.Text = es.Cte_NomComercial;
                HiddenCteCuentaNacional.Value = es.ESol_CteCuentaNacional.ToString();
                HiddenNumCuentaContNacional.Value = es.ESol_CteCuentaContNacional.ToString();
                txtHora.Text = es.ESol_Fecha.ToString("H:mm:ss");


                if (es.Id_Tm == 26)
                {
                    this.CmbProveedorF.SelectedIndex = this.CmbProveedorF.FindItemIndexByValue(es.Id_Pvd.ToString());
                    this.CmbProveedorF.Text = this.CmbProveedorF.FindItemByValue(es.Id_Pvd.ToString()).Text;
                    this.txtProveedorFId.DbValue = es.Id_Pvd == -1 ? (int?)null : es.Id_Pvd;
                }
                else
                {
                    cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue(es.Id_Pvd.ToString());
                    cmbProveedor.Text = cmbProveedor.FindItemByValue(es.Id_Pvd.ToString()).Text;
                    txtProveedorId.DbValue = es.Id_Pvd == -1 ? (int?)null : es.Id_Pvd;
                }
                if (es.ESol_FechaReferencia != null)
                {
                    dpFechaReferencia.SelectedDate = es.ESol_FechaReferencia;
                }
                if (es.ESol_Estatus != "P")
                {
                    this.rtb1.Items[1].Visible = false;
                    this.rtb1.Items[2].Visible = false;
                    this.BtnAutorizar.Enabled = false;
                    this.BtnRechazar.Enabled = false;
                }

                txtReferencia.Text = es.ESol_Referencia;
                txtNotas.Text = es.ESol_Notas;
                txtterritorio.DbValue = es.Id_Ter;
                txtTerritorioNombre.Text = es.Ter_Nombre;

                List<EntSalSolicitudDet> List = new List<EntSalSolicitudDet>();
                cn_es.CapEntSalSolicitud_ConsultaDet(ESol_Unique, ref List, sesion.Emp_Cnx);

                list_Es = List;
                rgEntradaSalida.DataSource = list_Es;
                rgEntradaSalida.Rebind();

                CalcularTotales();

                habilitarDeshabilitar();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void habilitarDeshabilitar()
        {
            switch (actualizacionDocumento)
            {
                case true:
                    cmbNaturaleza.Enabled = false;
                    cmbAfecta.Enabled = false;
                    //rgEntradaSalida.Enabled = _PermisoModificar;
                    dpFecha.Enabled = false;
                    cmbTipoMovimento.Enabled = false;
                    txtTipoId.Enabled = false;
                    //cmbCliente.Enabled = false;
                    txtClienteId.Enabled = false;
                    cmbProveedor.Enabled = false;
                    CmbId_UCC.Enabled = false;
                    CmbId_UEnviar.Enabled = false;
                    txtProveedorId.Enabled = false;
                    this.CmbProveedorF.Enabled = false;
                    this.txtProveedorFId.Enabled = false;
                    txtReferencia.Enabled = false;
                    txtNotas.Enabled = false;
                    // dpFechaReferencia.Enabled = false;
                    break;
                case false:
                    cmbNaturaleza.Enabled = true;
                    rgEntradaSalida.Enabled = true;
                    dpFecha.Enabled = true;
                    txtReferencia.Enabled = true;
                    txtNotas.Enabled = true;
                    dpFechaReferencia.Enabled = true;
                    break;
                default:
                    break;
            }
        }
        private void Guardar()
        {
            try
            {


                if (Request.QueryString["id"] != "-1" && !_PermisoModificar) // EDICION
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }

                if (Request.QueryString["id"] == "-1" && !_PermisoGuardar) //NUEVO
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                if (list_Es.Count == 0)
                {
                    RadTabStrip1.Tabs[1].Selected = true;
                    RadPageViewDetalles.Selected = true;
                    Alerta("Aún no se han capturado partidas");
                    this.rtb1.Items[1].Visible = true;
                    BtnAutorizar.Visible = true;
                    BtnRechazar.Visible = true;
                    return;
                }
                else
                {
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                }

                if (txtNotas.Text=="")
                {
                    RadTabStrip1.Tabs[1].Selected = true;
                    RadPageViewDetalles.Selected = true;
                    Alerta("Aún no se han capturado las notas.");
                    this.rtb1.Items[1].Visible = true;
                    return;
                }
                


                RadTabStrip1.Enabled = false;
                RadMultiPage1.Enabled = false;
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                EntSalSolicitud entsal = new EntSalSolicitud();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                int Verificador = 0;

                entsal.Id_Emp = sesion.Id_Emp;
                entsal.Id_Cd = sesion.Id_Cd_Ver;
                entsal.ESol_Unique = Guid.NewGuid().ToString();
                entsal.ESol_Naturaleza = int.Parse(cmbNaturaleza.SelectedValue);
                entsal.Id_Tm = int.Parse(cmbTipoMovimento.SelectedValue);
                entsal.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                entsal.Id_Ord = Convert.ToInt32(txtOCId.Value.HasValue ? txtOCId.Value.Value : 0);

                // De acuerdo al tipo de mov se toma de un combo u otro el valor
                if (Convert.ToInt32(this.txtTipoId.Text) == 26)
                {
                    entsal.Id_Pvd = int.Parse(this.CmbProveedorF.SelectedValue);
                }
                else
                {
                    entsal.Id_Pvd = int.Parse(cmbProveedor.SelectedValue);
                }
                entsal.Id_Ter = txtterritorio.Value.HasValue ? (int)txtterritorio.Value.Value : -1;
                entsal.ESol_Referencia = txtReferencia.Text;
                entsal.Id_UEnviar = int.Parse(CmbId_UEnviar.SelectedValue);
                //entsal.Id_UCC = int.Parse(CmbId_UCC.SelectedValue);
                entsal.Id_UCC = string.IsNullOrEmpty(CmbId_UCC.SelectedValue) ? -1 : int.Parse(CmbId_UCC.SelectedValue);

                entsal.ESol_Notas = txtNotas.Text;
                entsal.ESol_CteCuentaNacional = string.IsNullOrEmpty(HiddenCteCuentaNacional.Value) ? -1 : Convert.ToInt32(HiddenCteCuentaNacional.Value);
                entsal.ESol_CteCuentaContNacional = string.IsNullOrEmpty(HiddenNumCuentaContNacional.Value) ? 0 : Convert.ToInt32(HiddenNumCuentaContNacional.Value);
                if (dpFechaReferencia.SelectedDate.HasValue)
                {
                    entsal.ESol_FechaReferencia = Convert.ToDateTime(dpFechaReferencia.SelectedDate);
                }
                entsal.ESol_Subtotal = RadNumericTextBoxSubTotal.Value.Value;
                entsal.ESol_Impuesto = RadNumericTextBoxIVA.Value.Value;
                entsal.ESol_Total = RadNumericTextBoxTotal.Value.Value;
                entsal.Id_UCreo = sesion.Id_U;
                entsal.ESol_Fecha = Convert.ToDateTime(dpFecha.SelectedDate);

                List<EntSalSolicitudDet> List = list_Es;
                int Id_ESol;
                //string verificadorStr = "";
                if (Request.QueryString["id"] == "-1" || Request.QueryString["id"] == null)
                {
                    cn_es.CapEntSalSolicitud_Insertar(entsal, ref Verificador, sesion.Emp_Cnx);
                    Id_ESol = Verificador;
                    if (Verificador != 0)
                    {
                        cn_es.CapEntSalSolicitudDet_Insertar(Id_ESol, entsal, List, ref Verificador, sesion.Emp_Cnx);


                      

                        if (Verificador == -1)
                        {
                            ///Validar si debe enviarse a autorizar o no 
                            ///jfcv validar spCapEntSal_SolicitudValidar
                            ///
                            
                            EntSalSolicitud es = new EntSalSolicitud();
                           
                            int Verificador2 = 0;
                          
                            cn_es.CapEntSalSolicitud_ValidarMonto(entsal, ref Verificador2, sesion.Emp_Cnx);

                            if (Verificador2 == 1)
                            {
                                Autorizar();
                                AlertaCerrar("Se ha creado correctamente la solicitud con Folio: <b> #" + Id_ESol.ToString() + "</b> y se ha autorizado de forma automatica.");
                            }
                            else
                            {
                                EnviarCorreoCreo(Id_ESol);

                                // cn_es.CapEntSalSolicitud_CorreoCreo(entsal.Id_Cd, Id_ESol, URL,ref Verificador, sesion.Emp_Cnx);
                                AlertaCerrar("Se ha creado correctamente la solicitud con Folio: <b> #" + Id_ESol.ToString() + "</b> y fue enviado para su autorización por sobrepasar los importes autorizados.");
                            }
                        }
                        else
                        {
                            Alerta("Error al tratar de guardar el detalle de la solicitud");
                        }

                    }
                    else
                    {
                        Alerta("Error al tratar de guardar la solicitud");
                    }

                }
                else if (HFTipoOp.Value == "2")
                {
                    Id_ESol = int.Parse(this.txtFolio.Text);

                    cn_es.CapEntSalSolicitud_EliminarDet(sesion.Id_Cd_Ver, Id_ESol, ref Verificador, sesion.Emp_Cnx);
                    if (Verificador == -1)
                    {
                        cn_es.CapEntSalSolicitudDet_Insertar(Id_ESol, entsal, List, ref Verificador, sesion.Emp_Cnx);
                        if (Verificador == -1)
                        {
                            AlertaCerrar("Se ha modificado correctamente la solicitud con Folio: <b> #" + Id_ESol.ToString() + "</b>");
                        }
                        else
                        {
                            Alerta("Error al tratar de guardar el detalle de la solicitud");
                        }

                    }
                    else
                    {
                        Alerta("Error al tratar de modificar la solicitud");

                    }

                }


            }
            catch (Exception ex)
            {
                this.rtb1.Items[5].Enabled = true;
                RadTabStrip1.Enabled = true;
                RadMultiPage1.Enabled = true;
                Alerta(ex.Message);
            }
        }
        private void Nuevo()
        {
            try
            {
                RadTabStrip1.Tabs[0].Selected = true;
                RadPageViewDGenerales.Selected = true;
                txtTipoId.Text = "";
                LimpiarCombo(cmbNaturaleza);
                LimpiarCombo(cmbTipoMovimento);
                txtFolio.Text = "";
                cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);
                LimpiarClienteProducto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        private void Autorizar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                EntradaSalida es = new EntradaSalida();
                CN_EntSalSolicitud cn_esol = new CN_EntSalSolicitud();

                es.Id_Emp = sesion.Id_Emp;
                es.Id_Cd = sesion.Id_Cd_Ver;
                es.Id_U = sesion.Id_U;
                es.Id_Es = int.Parse(txtFolio.Text);
                es.Es_Naturaleza = int.Parse(cmbNaturaleza.SelectedValue);
                es.Es_Fecha = Convert.ToDateTime(dpFecha.SelectedDate);
                es.Id_Tm = int.Parse(cmbTipoMovimento.SelectedValue);
                es.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                if (dpFechaReferencia.SelectedDate.HasValue)
                {
                    es.Es_FechaReferencia = Convert.ToDateTime(dpFechaReferencia.SelectedDate);
                }

                // De acuerdo al tipo de mov se toma de un combo u otro el valor
                if (Convert.ToInt32(this.txtTipoId.Text) == 26)
                {
                    es.Id_Pvd = int.Parse(this.CmbProveedorF.SelectedValue);
                }
                else
                {
                    es.Id_Pvd = int.Parse(cmbProveedor.SelectedValue);
                }

                es.Es_Referencia = txtReferencia.Text;
                es.Es_Notas = txtNotas.Text;
                es.Es_SubTotal = RadNumericTextBoxSubTotal.Value.Value;
                es.Es_Iva = RadNumericTextBoxIVA.Value.Value;
                es.Es_Total = RadNumericTextBoxTotal.Value.Value;
                es.Es_Estatus = "I";
                es.Id_Ter = txtterritorio.Value.HasValue ? (int)txtterritorio.Value.Value : -1;
                es.Es_CteCuentaNacional = string.IsNullOrEmpty(HiddenCteCuentaNacional.Value) ? -1 : Convert.ToInt32(HiddenCteCuentaNacional.Value);
                es.Es_CteCuentaContNacional = string.IsNullOrEmpty(HiddenNumCuentaContNacional.Value) ? 0 : Convert.ToInt32(HiddenNumCuentaContNacional.Value);
                List<EntradaSalidaDetalle> listaDetalle = new List<EntradaSalidaDetalle>(); ;
                EntradaSalidaDetalle s = new EntradaSalidaDetalle();
                foreach (EntSalSolicitudDet e in list_Es)
                {
                    s = new EntradaSalidaDetalle();
                    s.Id_Emp = e.Id_Emp;
                    s.Id_Cd = e.Id_Cd;
                    s.Id_Ter = e.Id_Ter;
                    s.Es_BuenEstado = e.ESol_BuenEstado;
                    s.Id_Prd = e.Id_Prd;
                    s.Es_Cantidad = e.ESol_Cantidad;
                    s.Es_Costo = e.ESol_EsCosto;
                    s.Afecta = e.Afecta;
                    s.Prd_AgrupadoSpo = e.Prd_AgrupadoSpo;
                    listaDetalle.Add(s);
                }

                string VerificadorStr = "";

                cn_esol.GuardarEntradaSalida(ref es, listaDetalle, ref VerificadorStr, strEmp, sesion.Emp_Cnx);

                if (es.Id_Es != 0)
                {
                    int Verificador = 0;
                    cn_esol.CapEntSalSolicitud_Autorizo(sesion.Id_Cd_Ver, int.Parse(txtFolio.Text), es.Id_Es, ref Verificador, sesion.Emp_Cnx);

                    if (Verificador == -1)
                    {
                        string[] url = Request.Url.ToString().Split(new char[] { '/' });
                        string URL = Request.Url.ToString().Replace(url[url.Length - 1], "");
                        EnviarCorreoAtendio(1 /*Aceptado*/, int.Parse(this.txtFolio.Text));
                        //cn_esol.CapEntSalSolicitud_CorreoAtendio(sesion.Id_Cd_Ver, int.Parse(this.txtFolio.Text), URL, ref Verificador, sesion.Emp_Cnx);
                        AlertaCerrar("Se autorizó la solicitud con Folio: <b> #" + this.txtFolio.Text + "</b> <br> se generó el movimiento <b>#" + es.Id_Es.ToString() + "</b> ");
                    }
                    else
                    {
                        Alerta("Error al modificar el estatus de la solicitud");
                    }
                }
                else
                {

                    Alerta("Error al realizar la operación");
                }




            }
            catch (Exception ex)
            {
                AlertaFocus("Autorizar()" + ex.Message, BtnAutorizar.ClientID);
            }
        }
        private void LimpiarCombo(RadComboBox rcb)
        {
            try
            {
                rcb.SelectedIndex = 0;
                rcb.Text = rcb.Items[0].Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiarClienteProducto(bool provedor = false)
        {
            try
            {
                txtClienteId.Text = "";
                txtClienteNombre.Text = "";
                if (!provedor)
                {
                    cmbProveedor.SelectedIndex = 0;
                    cmbProveedor.Text = cmbProveedor.Items[0].Text;
                    CmbProveedorF.SelectedIndex = 0;
                    CmbProveedorF.Text = cmbProveedor.Items[0].Text;
                    txtProveedorId.Text = "";
                    this.txtProveedorFId.Text = "";
                }
                txtReferencia.Text = "";
                txtNotas.Text = "";


                LabelTerritorio.Visible = false;
                txtTerritorioNombre.Visible = false;
                txtterritorio.Text = "";
                txtTerritorioNombre.Text = "";

                list_Es = new List<EntSalSolicitudDet>();
                rgEntradaSalida.Rebind();

                CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipoMovimiento(int tipo_movimiento, int Tm_Afecta = -1) //Central
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Id_Emp, tipo_movimiento, Tm_Afecta, tipo_movimiento, 1, sesion.Emp_Cnx, "spCatMovimientoFiltro_Combo", ref cmbTipoMovimento);
                if (cmbNaturaleza.SelectedValue == "0")
                {
                    RemoverItem(new int[] { 18, 51, 78, 81, 82 });
                }
                else
                {
                    RemoverItem(new int[] { 17, 51, 53, 54, 60, 62, 63, 64, 65, 70, 72, 73, 74, 75 });
                }
                /*
                if (Request.QueryString["id"] == "-1" && Request.QueryString["id"] != null || Request.QueryString["id"] == null)
                {
                    RemoverItem(new int[] { 7, 11, 12, 13 });
                }*/


                cmbTipoMovimento.Enabled = !(tipo_movimiento == -1);
                txtTipoId.Enabled = !(tipo_movimiento == -1);
                cmbTipoMovimento.Text = cmbTipoMovimento.Items[0].Text;
                cmbTipoMovimento.SelectedIndex = 0;
                txtTipoId.DbValue = cmbTipoMovimento.Items[0].Value == "-1" ? null : cmbTipoMovimento.Items[0].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RemoverItem(int[] NoVisibles)
        {
            foreach (int tm in NoVisibles)
            {
                RadComboBoxItem bi = cmbTipoMovimento.FindItemByValue(tm.ToString());
                if (bi != null)
                    cmbTipoMovimento.Items.Remove(bi);
            }
        }
        private void RemoverItemProveedor(int[] NoVisibles)
        {
            foreach (int tm in NoVisibles)
            {
                RadComboBoxItem bi = cmbProveedor.FindItemByValue(tm.ToString());
                if (bi != null)
                    cmbProveedor.Items.Remove(bi);
            }
        }
        private int consultarConsecutivo(int Naturaleza_movimiento)
        {
            try
            {
                CN_CapEntradaSalida cn_entradasal = new CN_CapEntradaSalida();

                int naturalela = Convert.ToInt32(cmbNaturaleza.SelectedValue);
                int consecutivo = 0;
                cn_entradasal.ConsultarConsecutivo(sesion, naturalela, ref consecutivo);
                return consecutivo;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private List<Movimientos> ObtenerMovimiento(string TipoMovimento)
        {
            try
            {
                List<Movimientos> List = new List<Movimientos>();
                CN_CatMovimientos clsCatMovimientos = new CN_CatMovimientos();

                clsCatMovimientos.ConsultaMovimientos(false, sesion.Id_Emp, sesion.Emp_Cnx, ref List);
                return List.Where(Movimientos => Movimientos.Id.ToString() == TipoMovimento).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarNaturaleza()
        {
            cmbNaturaleza.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbNaturaleza.Items.Insert(1, new RadComboBoxItem("Entrada", "0"));
            cmbNaturaleza.Items.Insert(2, new RadComboBoxItem("Salida", "1"));

            cmbTipoMovimento.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }
        private void CargarAfectacion()
        {
            cmbAfecta.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbAfecta.Items.Insert(1, new RadComboBoxItem("Cliente", "0"));
            cmbAfecta.Items.Insert(2, new RadComboBoxItem("Proveedor", "1"));
            cmbAfecta.Items.Insert(3, new RadComboBoxItem("Sucursal", "2"));

        }
        private void CargarProveedor()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spProveedores_Combo", ref cmbProveedor);
                if (Request.QueryString["id"] == "-1" && Request.QueryString["id"] != null || Request.QueryString["id"] == null)
                {
                    //  RemoverItemProveedor(new int[] { 100 });
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProveedorFranquicia()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spProveedores_ComboFranquicia", ref this.CmbProveedorF);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarTerritorioDetalles(ref RadComboBox combo_a_llenar)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, int.Parse(txtReferencia.Text), 1, Sesion.Emp_Cnx, "spCapRemision_ComboDetalleXReferencia", ref combo_a_llenar);
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
                //if (_PermisoGuardar == false)
                //    this.rtb1.Items[6].Visible = false;
                if (_PermisoGuardar == false & _PermisoModificar == false)
                {
                    this.rtb1.Items[1].Visible = false;
                    this.rtb1.Items[2].Visible = false;
                    this.rtb1.Items[3].Visible = false;
                }


                if (HFTipoOp.Value == "2" || HFTipoOp.Value == "1")
                {
                    this.rtb1.Items[1].Visible = false;
                    this.rtb1.Items[2].Visible = false;
                    this.BtnAutorizar.Enabled = false;
                    this.BtnRechazar.Enabled = false;
                }
                else if (HFTipoOp.Value == "3")
                {
                    this.rtb1.Items[3].Visible = false;
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private float obtenerPrecioAAA(int Id_Prd)
        {
            try
            {
                float precio = 0;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ProductoPrecios cn_proprec = new CN_ProductoPrecios();
                int Id_Pre = 0;
                cn_proprec.ConsultaListaProductoPrecioAAA(ref precio, Sesion, Id_Prd, ref Id_Pre);
                return precio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularTotales()
        {
            try
            {
                double iva_cd = 0;
                new CN_CatCentroDistribucion().ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva_cd, sesion.Emp_Cnx);

                double subtotal = Math.Round(list_Es.Sum(EntradaSalidaDetalle => EntradaSalidaDetalle.Importe), 2);
                double iva = Math.Round(subtotal * iva_cd / 100, 2);
                double total = subtotal + iva;

                RadNumericTextBoxSubTotal.DbValue = subtotal;
                RadNumericTextBoxIVA.DbValue = iva;
                RadNumericTextBoxTotal.DbValue = total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private EntSalSolicitudDet GenerarDetalle(GridEditableItem editedItem, ref EntSalSolicitudDet Es_Det)
        {
            try
            {


                Es_Det.Id_Emp = sesion.Id_Emp;
                Es_Det.Id_Cd = sesion.Id_Cd_Ver;
                Es_Det.Id_Ter = (editedItem.FindControl("txtTerritorio") as RadNumericTextBox).Value.HasValue ? (int)(editedItem.FindControl("txtTerritorio") as RadNumericTextBox).Value : 0;
                Es_Det.Ter_Nombre = (editedItem.FindControl("cmbTerritorio") as RadComboBox).Text;
                Es_Det.Id_Prd = (int)(editedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value;
                Es_Det.Prd_Descripcion = (editedItem.FindControl("DescripcionTextBox") as RadTextBox).Text;
                Es_Det.Presentacion = (editedItem.FindControl("PresenTextBox") as RadTextBox).Text;
                Es_Det.ESol_Cantidad = (int)(editedItem.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Value;
                Es_Det.ESol_EsCosto = (double)((editedItem.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Value);
                Es_Det.Importe = Es_Det.ESol_Cantidad * Es_Det.ESol_EsCosto;
                Es_Det.Afecta = (editedItem["afecta"].Controls[0] as CheckBox).Checked;
                Es_Det.ESol_BuenEstado = (editedItem["buenEstado"].Controls[0] as CheckBox).Checked;
                Es_Det.Prd_AgrupadoSpo = (int)(editedItem.FindControl("AgrupadorTextBox") as RadNumericTextBox).Value;
                return Es_Det;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void ConsultaTMov(Movimientos mov, string Conexion, ref List<Movimientos> List, string bdCentral)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Empresa", "@Id_TProv", "bdCentral" };
                object[] Valores = { mov.Id_Emp, mov.Id, bdCentral };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spMovimientosxProveedor_Consulta", ref dr, Parametros, Valores);

                Movimientos Mov = default(Movimientos);


                while (dr.Read())
                {

                    Mov = new Movimientos();
                    Mov.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));
                    Mov.Nombre = dr.GetValue(dr.GetOrdinal("Tm_Nombre")).ToString();

                    List.Add(Mov);

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string Producto_Cantidad(Sesion sesion, string valor_retorno, string nat_, string gpo_, int id_prd, string ref_, int es_, int ter_, int can_, string mov_, string cte_, Producto producto)
        {
            try
            {
                if (nat_ == "1")
                {
                    int cantidadB = 0;
                    foreach (EntSalSolicitudDet dr in list_Es)
                    {
                        if (dr.Id_Prd.ToString() == id_prd.ToString())
                        {
                            cantidadB = cantidadB + Convert.ToInt32(dr.ESol_Cantidad);
                        }
                    }
                    if (Session["estatus" + Session.SessionID + HF_ClvPag.Value].ToString() == "1")
                    {
                        cantidadB = cantidadB - Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
                    }

                    CN_CapRemision rem = new CN_CapRemision();
                    int cantidadES2 = 0;
                    if (actualizacionDocumento)
                    {
                        rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);
                    }


                    if (producto.Prd_InvFinal - producto.Prd_Asignado + cantidadES2 < can_ + cantidadB)
                    {
                        return "-1@@" + "No hay producto suficiente";
                    }


                }
                else if (gpo_ == "0")
                {
                    int edicion = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
                    if (producto.Prd_InvFinal - producto.Prd_Asignado - (edicion - can_) < 0)
                    {
                        return "-1@@" + "Producto " + producto.Id_Prd.ToString() + " inventario disponible insuficiente, inventario final: " + producto.Prd_InvFinal.ToString() + ", asignado: " + producto.Prd_Asignado.ToString() + ", disponible:" + (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();
                    }
                }

                if (gpo_ == "4" || gpo_ == "2")
                {

                    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                    int verificador = 0;
                    CNentrada.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd.ToString(), ter_.ToString(), cte_, sesion.Emp_Cnx, ref verificador, mov_);
                    int Prd_AgrupadoSpo = producto.Prd_AgrupadoSpo;

                    int cantidadEnDt = 0;
                    foreach (EntSalSolicitudDet dr in list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Prd_AgrupadoSpo == Prd_AgrupadoSpo && EntradaSalidaDetalle.Id_Ter == ter_ && EntradaSalidaDetalle.Id_Prd != id_prd).ToList())
                    {
                        cantidadEnDt += dr.ESol_Cantidad;
                    }

                    CN_CapRemision rem = new CN_CapRemision();
                    int cantidadES2 = 0;
                    if (actualizacionDocumento)
                    {
                        rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, Prd_AgrupadoSpo, nat_, ref cantidadES2, sesion.Emp_Cnx);
                        verificador += cantidadES2;
                    }


                    if (cantidadEnDt + can_ > verificador)
                    {
                        return "-1@@" + "Los artículos sobrepasan lo disponible";
                    }

                }
                else if (gpo_ == "3")
                {
                    CN_CapRemision rem = new CN_CapRemision();


                    int cantidadES = 0;

                    int cantidadEnDttemp_original = 0;
                    if (Session["estatus" + Session.SessionID + HF_ClvPag.Value].ToString() != "1")
                    {
                        cantidadEnDttemp_original = 0;
                    }
                    else
                    {
                        cantidadEnDttemp_original = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
                    }

                    int cantidadB = 0;
                    foreach (EntSalSolicitudDet dr in list_Es)
                    {
                        if (dr.Id_Prd.ToString() == id_prd.ToString())
                        {
                            cantidadB += dr.ESol_Cantidad;

                        }
                    }


                    //rem.ConsultarRemisionesCantidad(session.Id_Emp, session.Id_Cd_Ver, refe, id_prd, ref cantidadES, session.Emp_Cnx);
                    rem.ConsultarRemisionesCantidadRem(sesion.Id_Emp, sesion.Id_Cd_Ver, ref_, id_prd, ref cantidadES, sesion.Emp_Cnx);
                    int cantidadES2 = 0;
                    if (actualizacionDocumento)
                    {
                        rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);
                        cantidadES += cantidadES2;
                    }




                    if (cantidadES < cantidadB - cantidadEnDttemp_original + can_)
                    //if (cantidadES < can_)
                    {
                        return "-1@@" + "Los artículos sobrepasan el disponible";

                    }

                    if (producto.Prd_InvFinal - producto.Prd_Asignado - (cantidadEnDttemp_original - can_) < 0)
                    {
                        return "-1@@" + "Producto " + producto.Id_Prd.ToString() + " inventario disponible insuficiente, inventario final: " + producto.Prd_InvFinal.ToString() + ", asignado: " + producto.Prd_Asignado.ToString() + ", disponible:" + (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();

                    }
                }
                else if (gpo_ == "1")
                {
                    if (actualizacionDocumento)
                    {
                        CN_CapRemision rem = new CN_CapRemision();
                        int cantidadES2 = 0;
                        rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);

                        Producto cp = new Producto();
                        CN_CatProducto cn_catproducto = new CN_CatProducto();
                        cn_catproducto.ConsultaProducto(ref cp, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd);

                        int cantidadB = 0;
                        foreach (EntSalSolicitudDet dr in list_Es)
                        {
                            if (dr.Id_Prd.ToString() == id_prd.ToString())
                            {
                                cantidadB += dr.ESol_Cantidad;
                            }
                        }

                        cantidadB = cantidadB - Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]) + (int)can_;
                        if (cantidadB < cantidadES2 && (cantidadES2 - cantidadB) > (cp.Prd_InvFinal - cp.Prd_Asignado))
                        {
                            return "-1@@" + "Producto " + id_prd.ToString() + " inventario disponible insuficiente, inventario final: " + cp.Prd_InvFinal.ToString() + ", asignado: " + cp.Prd_Asignado.ToString() + " , disponible: " + (cp.Prd_InvFinal - cp.Prd_Asignado).ToString() + "";
                        }
                    }
                }
                return "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviarCorreoCreo(int Id_ESol)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                EntSalSolicitud es = new EntSalSolicitud();

                cn_es.CapEntSolicitud_ConsultaDatosEnvio(sesion.Id_Cd_Ver, Id_ESol, ref es, sesion.Emp_Cnx);

                string[] url = Request.Url.ToString().Split(new char[] { '/' });
                string URL = Request.Url.ToString().Replace(url[url.Length - 1], "");


                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = sesion.Id_Cd_Ver;
                configuracion.Id_Emp = sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<Table> <tr><td><b></b><td></tr> <tr><td><b>");
                cuerpo_correo.Append("Se ha colocado la solicitud de movimiento de almacén con el folio # " + Id_ESol + "");
                cuerpo_correo.Append("</b><td></tr><tr><td>&nbsp;<td></tr><tr><td>");
                //cuerpo_correo.Append("<a href=" + URL + "CapEntSalAutorizacion_Lista.aspx?u=" + es.ESol_Unique + ">Ver solicitud de autorización</a>");
                cuerpo_correo.Append("<a href=" + URL + "CapEntSalAutorizacion.aspx?Id="      + es.ESol_Unique + "&Es_Naturaleza=0&PermisoGuardar=true&PermisoModificar=true&PermisoEliminar=true&PermisoImprimir=true&TipoOp=3 >Ver solicitud de autorización</a>");
                cuerpo_correo.Append("</td></tr></Table>");

                


                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(es.ESol_CorreoDest));
                //m.To.Add(new MailAddress("jmartinez@axsistec.com"));
                if (es.ESol_CorreoCC.Length > 1)
                {
                    m.CC.Add(new MailAddress(es.ESol_CorreoCC));
                }
                //m.Subject = "Solicitud de autorización de precios especiales";
                m.Subject = "Solicitud de movimiento de almacén #" + Id_ESol + " " + sesion.U_Nombre;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception ex)
                {
                    throw ex;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EnviarCorreoAtendio(int Tipo, int Id_ESol)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                EntSalSolicitud es = new EntSalSolicitud();

                cn_es.CapEntSolicitud_ConsultaDatosEnvio(sesion.Id_Cd_Ver, Id_ESol, ref es, sesion.Emp_Cnx);

                string[] url = Request.Url.ToString().Split(new char[] { '/' });
                string URL = Request.Url.ToString().Replace(url[url.Length - 1], "");


                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = sesion.Id_Cd_Ver;
                configuracion.Id_Emp = sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<Table> <tr><td><b></b><td></tr> <tr><td><b>");
                if (Tipo == 0)
                {
                    cuerpo_correo.Append("La solicitud #" + Id_ESol + " ha sido RECHAZADA");
                }
                else
                {
                    cuerpo_correo.Append("La solicitud #" + Id_ESol + " ha sido APROBADA");
                }
                cuerpo_correo.Append("</b><td></tr><tr><td>&nbsp;<td></tr><tr><td>");
                cuerpo_correo.Append("<a href=" + URL + "CapEntSalAutorizacion_Lista.aspx?u=" + es.ESol_Unique + ">Ver solicitud de autorización</a>");
                cuerpo_correo.Append("</td></tr></Table>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(es.ESol_CorreoDest));
                //m.To.Add(new MailAddress("jmartinez@axsistec.com"));
                if (es.ESol_CorreoCC.Length > 1)
                {
                    m.CC.Add(new MailAddress(es.ESol_CorreoCC));
                }
                //m.Subject = "Solicitud de autorización de precios especiales";
                if (Tipo == 0)
                {
                    m.Subject = "Solicitud de movimiento de almacén #" + Id_ESol + " ha sido RECHAZADA";
                }
                else
                {
                    m.Subject = "Solicitud de movimiento de almacén #" + Id_ESol + " ha sido APROBADA";
                }

                
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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
                RadAjaxManager1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
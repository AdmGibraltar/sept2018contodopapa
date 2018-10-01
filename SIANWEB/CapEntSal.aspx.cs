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

namespace SIANWEB
{
    public partial class CapEntSal : System.Web.UI.Page
    {
        #region Variables

        public string Resultado;
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
        private List<EntradaSalidaDetalle> list_Es
        {
            get { return (List<EntradaSalidaDetalle>)Session["ListEs" + Session.SessionID + HF_ClvPag.Value]; }
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
            bool nuevo = false;
            try
            {
                if (sesion == null)
                {
                    CerrarVentana();

                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        nuevo = !(Request.QueryString["id"] != "-1" && Request.QueryString["id"] != null);
                        Inicializar(nuevo);

                        if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
                        {
                            GridCommandItem cmdItem = (GridCommandItem)rgEntradaSalida.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 

                            rgEntradaSalida.MasterTableView.Columns[rgEntradaSalida.MasterTableView.Columns.Count - 2].Display = false;
                            rgEntradaSalida.MasterTableView.Columns[rgEntradaSalida.MasterTableView.Columns.Count - 3].Display = false;
                            ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  

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
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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

                        Resultado = "";
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


                                List<EntradaSalidaDetalle> detalles = new List<EntradaSalidaDetalle>();
                                EntradaSalidaDetalle EntSalDetalle = new EntradaSalidaDetalle();

                                for (int i = 0; i < listaOrdCompraDet.Count; i++)
                                {
                                    OrdenCompraDet orden = listaOrdCompraDet[i];
                                    total += orden.Ord_Cantidad * orden.ProductoPrecio.Prd_Pesos;



                                    EntSalDetalle = new EntradaSalidaDetalle();
                                    EntSalDetalle.Id_Emp = orden.Id_Emp;
                                    EntSalDetalle.Id_Cd = orden.Id_Cd;
                                    EntSalDetalle.Id_Es = Convert.ToInt32(txtFolio.Text);
                                    EntSalDetalle.Id_EsDet = i + 1;
                                    EntSalDetalle.Id_EsDetStr = Convert.ToString(i + 1);
                                    EntSalDetalle.EsDet_Naturaleza = Convert.ToInt32(cmbNaturaleza.SelectedValue);
                                    EntSalDetalle.Id_Ter = 0;
                                    EntSalDetalle.Ter_Nombre = "";
                                    EntSalDetalle.Id_Prd = orden.Id_Prd;
                                    EntSalDetalle.Prd_AgrupadoSpo = 0;
                                    EntSalDetalle.Prd_Descripcion = orden.Producto.Prd_Descripcion;
                                    EntSalDetalle.Prd_Presentacion = orden.Producto.Prd_Presentacion;
                                    EntSalDetalle.Prd_Unidad = orden.Producto.Prd_UniNe;
                                    EntSalDetalle.Presentacion = orden.Producto.Prd_Presentacion;
                                    EntSalDetalle.Es_Cantidad = Convert.ToInt32(orden.Ord_Cantidad);
                                    EntSalDetalle.Es_Costo = orden.ProductoPrecio.Prd_Pesos;
                                    EntSalDetalle.Es_BuenEstado = true;
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

                //Label TipoSalidaIdDesc = (e.Item.FindControl("TipoSalidaIdDesc") as Label);

                //Label ConceptoTipoSalida = (e.Item.FindControl("ConceptoTipoSalida") as Label);


                RadComboBox RadComboBoxTipoSalida = e.Item.FindControl("RadComboBoxTipoSalida") as RadComboBox;
                new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1), sesion.Emp_Cnx, "sp_CatESTipoSalida", ref RadComboBoxTipoSalida);



                RadComboBox RadComboBoxConceptoTipoSalida = e.Item.FindControl("RadComboBoxConceptoTipoSalida") as RadComboBox;
                new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, -1, sesion.Emp_Cnx, "sp_CatESConceptoTipoSalida", ref RadComboBoxConceptoTipoSalida);



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
                    CN_CatProducto clsCatProducto = new CN_CatProducto();
                    clsCatProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, prd_, true);
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
                EntradaSalidaDetalle Es_Det = new EntradaSalidaDetalle();
                Es_Det.Id_EsDetStr = Guid.NewGuid().ToString();
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
                EntradaSalidaDetalle Es_Det = new EntradaSalidaDetalle();
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
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Es_Cantidad = Es_Det.Es_Cantidad;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Es_Costo = Es_Det.Es_Costo;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Importe = Es_Det.Importe;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Afecta = Es_Det.Afecta;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].Es_BuenEstado = Es_Det.Es_BuenEstado;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].TipoSalida = Es_Det.TipoSalida;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].ConceptoTipoSalida = Es_Det.ConceptoTipoSalida;

                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].DescTipoSalida = Es_Det.DescTipoSalida;
                list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0].DescConceptoTipoSalida = Es_Det.DescConceptoTipoSalida;


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
        protected void cmbTipoSalidaIdDesc_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox combo = sender as RadComboBox;
            RadComboBox RadComboBoxConceptoTipoSalida = combo.Parent.FindControl("RadComboBoxConceptoTipoSalida") as RadComboBox;
            new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(e.Value), sesion.Emp_Cnx, "sp_CatESConceptoTipoSalida", ref RadComboBoxConceptoTipoSalida);
            RadComboBoxConceptoTipoSalida.SelectedValue = "1";
            RadComboBoxConceptoTipoSalida.Text = "";
        }



        protected void cmbNaturaleza_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                bool nuevo = !(Request.QueryString["id"] != "-1" && Request.QueryString["id"] != null);
                txtFolio.Text = consultarConsecutivo(Convert.ToInt32(cmbNaturaleza.SelectedValue)).ToString();
                CargarTipoMovimiento(Convert.ToInt32(cmbNaturaleza.SelectedValue), nuevo: nuevo);

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
                bool nuevo = !(Request.QueryString["id"] != "-1" && Request.QueryString["id"] != null);
                CargarTipoMovimiento(Convert.ToInt32(cmbNaturaleza.SelectedValue), nuevo: nuevo);
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
                bool nuevo = !(Request.QueryString["id"] != "-1" && Request.QueryString["id"] != null);
                cmbTipoMovimento.Enabled = false;
                txtTipoId.Enabled = false;
                int AfectaId = Convert.ToInt32(cmbAfecta.SelectedValue);
                CargarTipoMovimiento(Convert.ToInt32(cmbNaturaleza.SelectedValue), AfectaId, nuevo);
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
            bool mostrar_clie = false;
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


            trFechaReferencia.Visible = mostrar_prov;
            if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
            {
                mostrar_prov = false;
            }


            RequiredFieldValidatorFechaReferencia.Enabled = mostrar_prov;

            if (cmbTipoMovimento.SelectedValue == "66")
            {
                rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("TipoSalida").OrderIndex - 2].Display = true;
                rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("ConceptoTipoSalida").OrderIndex - 2].Display = true;
            }
            else
            {
                rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("TipoSalida").OrderIndex - 2].Display = false;
                rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("ConceptoTipoSalida").OrderIndex - 2].Display = false;
            }

        }




        #endregion
        #region Funciones
        private void Inicializar(bool nuevo)
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
                ValidarPermisos();

                CargarNaturaleza();
                CargarProveedor();
                CargarAfectacion();

                //JMM:Cargamos el combo de proveedor franquicia
                CargarProveedorFranquicia();
                list_Es = new List<EntradaSalidaDetalle>();

                actualizacionDocumento = (Request.QueryString["id"] != "-1" && Request.QueryString["id"] != null);

                if (Request.QueryString["id"] != "-1" && Request.QueryString["id"] != null)//Edicion
                {
                    cargarMovimientoEntSal(nuevo);
                }

                //RadAjaxManager1.ResponseScripts.Add("IniciarPaginasAuxiliares();");


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void cargarMovimientoEntSal(bool nuevo)
        {
            ////aqui se va traer la info del documento a editar             
            EntradaSalida entradaSalida = new EntradaSalida();
            try
            {
                int Id_Es = Convert.ToInt32(Request.QueryString["id"]);
                int Es_Naturaleza = Convert.ToInt32(Request.QueryString["Es_Naturaleza"]);

                CN_CapEntradaSalida cn_entsal = new CN_CapEntradaSalida();
                cn_entsal.ConsultarEntradaSalida(sesion, sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Es, Es_Naturaleza, ref entradaSalida);
                cmbNaturaleza.SelectedValue = entradaSalida.Es_Naturaleza.ToString();

                CN_CatMovimientos CN_Mov = new CN_CatMovimientos();
                int afectaId = -1;
                CN_Mov.ConsultaTmovimientoAfecta(sesion, entradaSalida.Id_Tm, ref afectaId);
                CargarTipoMovimiento(entradaSalida.Es_Naturaleza, afectaId, nuevo);
                MostrarCampos(afectaId);

                cmbAfecta.SelectedIndex = cmbAfecta.FindItemIndexByValue(afectaId.ToString());
                cmbAfecta.Text = cmbAfecta.FindItemByValue(afectaId.ToString()).Text;

                txtFolio.Text = entradaSalida.Id_Es.ToString();
                dpFecha.SelectedDate = entradaSalida.Es_Fecha;
                txtTipoId.Text = entradaSalida.Id_Tm.ToString();
                cmbTipoMovimento.SelectedIndex = cmbTipoMovimento.FindItemIndexByValue(entradaSalida.Id_Tm.ToString());
                cmbTipoMovimento.Text = cmbTipoMovimento.FindItemByValue(entradaSalida.Id_Tm.ToString()).Text;
                cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);
                txtClienteId.DbValue = entradaSalida.Id_Cte == -1 ? (int?)null : entradaSalida.Id_Cte;
                txtClienteNombre.Text = entradaSalida.Cte_NomComercial;
                HiddenCteCuentaNacional.Value = entradaSalida.Es_CteCuentaNacional.ToString();
                HiddenNumCuentaContNacional.Value = entradaSalida.Es_CteCuentaContNacional.ToString();
                txtHora.Text = entradaSalida.Es_FechaHr.ToString("H:mm:ss");


                if (entradaSalida.Id_Tm == 26)
                {
                    this.CmbProveedorF.SelectedIndex = this.CmbProveedorF.FindItemIndexByValue(entradaSalida.Id_Pvd.ToString());
                    this.CmbProveedorF.Text = this.CmbProveedorF.FindItemByValue(entradaSalida.Id_Pvd.ToString()).Text;
                    this.txtProveedorFId.DbValue = entradaSalida.Id_Pvd == -1 ? (int?)null : entradaSalida.Id_Pvd;
                }
                else
                {
                    if (entradaSalida.Id_Pvd.ToString() != "0")
                    {
                        cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue(entradaSalida.Id_Pvd.ToString());
                        cmbProveedor.Text = cmbProveedor.FindItemByValue(entradaSalida.Id_Pvd.ToString()).Text;
                        txtProveedorId.DbValue = entradaSalida.Id_Pvd == -1 ? (int?)null : entradaSalida.Id_Pvd;
                    }
                }
                if (entradaSalida.Es_FechaReferencia != null)
                {
                    dpFechaReferencia.SelectedDate = entradaSalida.Es_FechaReferencia;
                }

                txtReferencia.Text = entradaSalida.Es_Referencia;
                txtNotas.Text = entradaSalida.Es_Notas;
                txtterritorio.DbValue = entradaSalida.Id_Ter;
                txtTerritorioNombre.Text = entradaSalida.Ter_Nombre;
                List<EntradaSalidaDetalle> detalles = new List<EntradaSalidaDetalle>();
                ////DataTable dt = new DataTable();
                new CN_CapEntradaSalida().ConsultarEntradaSalidaDetalles(sesion, entradaSalida, ref detalles);//, ref dt);
                list_Es = detalles;
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
                    txtProveedorId.Enabled = false;
                    this.CmbProveedorF.Enabled = false;
                    this.txtProveedorFId.Enabled = false;
                    txtReferencia.Enabled = false;
                    txtNotas.Enabled = _PermisoModificar;
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
                    return;
                }
                else
                {
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                }

                RadTabStrip1.Enabled = false;
                RadMultiPage1.Enabled = false;
                CN_CapEntradaSalida cn_capEntradaSalida = new CN_CapEntradaSalida();

                EntradaSalida entsal = new EntradaSalida();
                entsal.Id_Emp = sesion.Id_Emp;
                entsal.Id_Cd = sesion.Id_Cd_Ver;
                entsal.Id_U = sesion.Id_U;
                entsal.Id_Es = int.Parse(txtFolio.Text);
                entsal.Es_Naturaleza = int.Parse(cmbNaturaleza.SelectedValue);
                entsal.Es_Fecha = Convert.ToDateTime(dpFecha.SelectedDate);
                entsal.Id_Tm = int.Parse(cmbTipoMovimento.SelectedValue);
                entsal.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                entsal.Id_Ord = Convert.ToInt32(txtOCId.Value.HasValue ? txtOCId.Value.Value : 0);

                if (dpFechaReferencia.SelectedDate.HasValue)
                {
                    entsal.Es_FechaReferencia = Convert.ToDateTime(dpFechaReferencia.SelectedDate);
                }

                // De acuerdo al tipo de mov se toma de un combo u otro el valor
                if (Convert.ToInt32(this.txtTipoId.Text) == 26)
                {
                    entsal.Id_Pvd = int.Parse(this.CmbProveedorF.SelectedValue == "" ? "0" : this.CmbProveedorF.SelectedValue);
                }
                else
                {
                    entsal.Id_Pvd = int.Parse(this.cmbProveedor.SelectedValue == "" ? "0" : this.cmbProveedor.SelectedValue);
                }

                entsal.Es_Referencia = txtReferencia.Text;
                entsal.Es_Notas = txtNotas.Text;
                entsal.Es_SubTotal = RadNumericTextBoxSubTotal.Value.Value;
                entsal.Es_Iva = RadNumericTextBoxIVA.Value.Value;
                entsal.Es_Total = RadNumericTextBoxTotal.Value.Value;
                entsal.Es_Estatus = "C";
                entsal.Id_Ter = txtterritorio.Value.HasValue ? (int)txtterritorio.Value.Value : -1;
                entsal.Es_CteCuentaNacional = string.IsNullOrEmpty(HiddenCteCuentaNacional.Value) ? -1 : Convert.ToInt32(HiddenCteCuentaNacional.Value);
                entsal.Es_CteCuentaContNacional = string.IsNullOrEmpty(HiddenNumCuentaContNacional.Value) ? 0 : Convert.ToInt32(HiddenNumCuentaContNacional.Value);
                List<EntradaSalidaDetalle> listaDetalle = list_Es;

                string verificadorStr = "";
                if (Request.QueryString["id"] == "-1" || Request.QueryString["id"] == null)
                {
                    cn_capEntradaSalida.GuardarEntradaSalida(entsal, listaDetalle, ref verificadorStr, strEmp, sesion.Emp_Cnx);
                }
                else
                {
                    cn_capEntradaSalida.EdicionEntradaSalida(entsal, listaDetalle, ref verificadorStr, strEmp, sesion.Emp_Cnx);
                }

                this.rtb1.Items[5].Enabled = false;
                if (verificadorStr.Trim() != "")
                {
                    verificadorStr = verificadorStr + "<br";
                }

                AlertaCerrar(verificadorStr + "Los datos se guardaron correctamente");

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
                    // cmbProveedor.SelectedIndex = 0;
                    // cmbProveedor.Text = cmbProveedor.Items[0].Text;
                    // CmbProveedorF.SelectedIndex = 0;
                    // CmbProveedorF.Text = cmbProveedor.Items[0].Text;
                    // txtProveedorId.Text = "";
                    // this.txtProveedorFId.Text = "";
                }
                txtReferencia.Text = "";
                txtNotas.Text = "";


                LabelTerritorio.Visible = false;
                txtTerritorioNombre.Visible = false;
                txtterritorio.Text = "";
                txtTerritorioNombre.Text = "";

                list_Es = new List<EntradaSalidaDetalle>();
                rgEntradaSalida.Rebind();

                CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipoMovimiento(int tipo_movimiento, int Tm_Afecta = -1, bool nuevo = false) //Central
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Id_Emp, tipo_movimiento, Tm_Afecta, tipo_movimiento, sesion.Emp_Cnx, "spCatMovimientoFiltro_Combo", ref cmbTipoMovimento);
                if (cmbNaturaleza.SelectedValue == "0")
                {
                    RemoverItem(new int[] { 18, 51, 78, 81, 82 });
                }
                else
                {
                    RemoverItem(new int[] { 17, 51, 53, 54, 60, 62, 63, 64, 65, 70, 72, 73, 74, 75 });
                }

                if (nuevo)
                {
                    RemoverItem(new int[] { 7, 11, 12, 13 });
                }

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
                if (_PermisoGuardar == false)
                    this.rtb1.Items[6].Visible = false;
                if (_PermisoGuardar == false & _PermisoModificar == false)
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

        private EntradaSalidaDetalle GenerarDetalle(GridEditableItem editedItem, ref EntradaSalidaDetalle Es_Det)
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
                Es_Det.Es_Cantidad = (int)(editedItem.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Value;
                Es_Det.Es_Costo = (double)((editedItem.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Value);
                Es_Det.Importe = Es_Det.Es_Cantidad * Es_Det.Es_Costo;
                Es_Det.Afecta = (editedItem["afecta"].Controls[0] as CheckBox).Checked;
                Es_Det.Es_BuenEstado = (editedItem["buenEstado"].Controls[0] as CheckBox).Checked;
                //Es_Det.Prd_AgrupadoSpo = (int)(editedItem.FindControl("AgrupadorTextBox") as RadNumericTextBox).Value;
                Es_Det.Es_Usado = Convert.ToInt32((editedItem.FindControl("RadNumericTextNumUsado") as RadTextBox).Text);
                Es_Det.TipoSalida = Convert.ToInt32((editedItem.FindControl("RadComboBoxTipoSalida") as RadComboBox).SelectedValue);
                Es_Det.ConceptoTipoSalida = Convert.ToInt32((editedItem.FindControl("RadComboBoxConceptoTipoSalida") as RadComboBox).SelectedValue);
                Es_Det.DescTipoSalida = Convert.ToString((editedItem.FindControl("RadComboBoxTipoSalida") as RadComboBox).Text);
                Es_Det.DescConceptoTipoSalida = Convert.ToString((editedItem.FindControl("RadComboBoxConceptoTipoSalida") as RadComboBox).Text);
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
                    foreach (EntradaSalidaDetalle dr in list_Es)
                    {
                        if (dr.Id_Prd.ToString() == id_prd.ToString())
                        {
                            cantidadB = cantidadB + Convert.ToInt32(dr.Es_Cantidad);
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
                    foreach (EntradaSalidaDetalle dr in list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Prd_AgrupadoSpo == Prd_AgrupadoSpo && EntradaSalidaDetalle.Id_Ter == ter_ && EntradaSalidaDetalle.Id_Prd != id_prd).ToList())
                    {
                        cantidadEnDt += dr.Es_Cantidad;
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
                    foreach (EntradaSalidaDetalle dr in list_Es)
                    {
                        if (dr.Id_Prd.ToString() == id_prd.ToString())
                        {
                            cantidadB += dr.Es_Cantidad;

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
                        foreach (EntradaSalidaDetalle dr in list_Es)
                        {
                            if (dr.Id_Prd.ToString() == id_prd.ToString())
                            {
                                cantidadB += dr.Es_Cantidad;
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
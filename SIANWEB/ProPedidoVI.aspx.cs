using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using System.Collections;
using CapaNegocios;
using CapaDatos;

namespace SIANWEB
{
    public partial class ProVentInst_PedidoCap : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public DataTable dt
        {
            get
            {
                return (DataTable)ViewState["dtPedidoVI"];
            }
            set
            {
                ViewState["dtPedidoVI"] = value;

            }
        }

        public DataTable dt_Resto
        {
            get
            {
                return (DataTable)ViewState["dtPedidoVI_Resto"];
            }
            set
            {
                ViewState["dtPedidoVI_Resto"] = value;
            }
        }

        public ArrayList al
        {
            get
            {
                return (ArrayList)Session["Borrados" + Session.SessionID];
            }
            set { Session["Borrados" + Session.SessionID] = value; }
        }
        double iva_cd
        {
            get
            {
                double? _iva_cd = (double?)Session["iva_cd" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
                return _iva_cd == null ? 0 : (double)_iva_cd;
            }
            set
            {
                Session["iva_cd" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        //private bool terr = false;
        private bool prod = false;
        public Sesion session
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
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public int productoNuevo = 0;
        public int Id_TG
        {
            get
            {
                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {
                        return _idTG;
                    }
                }
                return _idTG;
            }
        }
        #endregion
        #region Eventos
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (rg1.DataSourceIsAssigned)
            {
                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {
                        var gi = rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                        LinkButton lnkAgregar = gi.FindControl("LinkButton2") as LinkButton;
                        if (lnkAgregar != null)
                        {
                            lnkAgregar.Visible = false;
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                txtPedCaptadorPor.Text = Sesion.U_Nombre;
                if (Sesion == null)
                {
                    CerrarVentana();
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        _nuevoPedidoSinProgramar = false;
                        Session["Id_Ped" + Session.SessionID] = null;
                        Session["dtPedidoVI" + Session.SessionID] = null;
                        Session["Borrados" + Session.SessionID] = null; ;

                        //Edsg28052015
                        Session["ProductosNoAcys"] = null;
                        Session.Add("ProductosNoAcys", new List<int>());



                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        if (Session["Borrados" + Session.SessionID] == null)
                        {
                            Session["Borrados" + Session.SessionID] = new ArrayList();
                        }
                        Inicializar();
                        al = new ArrayList();
                        if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
                        {
                            //deshabilitarcontroles(RadPane2.Controls);
                            deshabilitarcontroles(divTotales.Controls);
                            GridCommandItem cmdItem = (GridCommandItem)rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];

                            if (cmdItem.FindControl("AddNewRecordButton") != null)
                                ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 

                            if (cmdItem.FindControl("InitInsertButton") != null)
                                ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  

                            //rg1.Columns.FindByUniqueName("EditCommandColumn").Display = false;
                            rg1.Columns.FindByUniqueName("DeleteColumn").Display = false;
                        }
                    }
                }
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
                    Imprimir(Convert.ToInt32(Session["Id_Ped" + Session.SessionID]));
                    break;
                case "rebind":
                    rg1.Rebind();

                    string _idTGStr = Request.QueryString["Id_TG"];
                    int _idTG = 0;
                    if (_idTGStr != null)
                    {
                        if (int.TryParse(_idTGStr, out _idTG))
                        {
                            var gi = rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            LinkButton lnkAgregar = gi.FindControl("LinkButton2") as LinkButton;
                            if (lnkAgregar != null)
                            {
                                lnkAgregar.Visible = false;
                            }
                        }
                    }

                    rg1_Resto.Rebind();
                    CalcularTotales();
                    break;
                case "continuar":
                    rg1.Rebind();

                    _idTGStr = Request.QueryString["Id_TG"];
                    _idTG = 0;
                    if (_idTGStr != null)
                    {
                        if (int.TryParse(_idTGStr, out _idTG))
                        {
                            var gi = rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            LinkButton lnkAgregar = gi.FindControl("LinkButton2") as LinkButton;
                            if (lnkAgregar != null)
                            {
                                lnkAgregar.Visible = false;
                            }
                        }
                    }

                    rg1_Resto.Rebind();
                    CalcularTotales();
                    Guardar();
                    break;
                case "cliente":
                    txtIdCte.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                    //txtClienteNombre.Text = Session["ClienteNombre_Bucsar" + Session.SessionID].ToString();
                    txtIdCte_TextChanged(null, null);
                    break;
                case "direccion":
                    //txtIdCte.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                    //txtClienteNombre.Text = Session["ClienteNombre_Bucsar" + Session.SessionID].ToString();
                    //txtIdCte_TextChanged(null, null);
                    CN_CatCliente clsCliente = new CN_CatCliente();
                    ClienteDirEntrega cliente = new ClienteDirEntrega();
                    Sesion session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    cliente.Id_Emp = session2.Id_Emp;
                    cliente.Id_Cd = session2.Id_Cd_Ver;
                    cliente.Id_CteDirEntrega = Int32.Parse(Session["Id_Buscar" + Session.SessionID].ToString()) - 1;
                    cliente.Id_Cte = Int32.Parse(Session["Descripcion_Buscar" + Session.SessionID].ToString());
                    clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx);
                    txtCalle.Text = cliente.Cte_Calle;
                    txtNo.Text = cliente.Cte_Numero;
                    txtCp.Text = cliente.Cte_Cp.Trim();
                    txtColonia.Text = cliente.Cte_Colonia;
                    txtMunicipio.Text = cliente.Cte_Municipio;
                    txtEstado.Text = cliente.Cte_Estado;
                    txtRHoraam1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cliente.Cte_HoraAm1);
                    txtRHoraam2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cliente.Cte_HoraAm2);
                    txtRHorapm1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cliente.Cte_HoraPm1);
                    txtRHorapm2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cliente.Cte_HoraPm2);
                    break;
                case "precio":
                    (producto as RadNumericTextBox).DbValue = Session["Id_Buscar" + Session.SessionID];
                    cmbProductoDet_TextChanged(producto, null);
                    ((producto as RadNumericTextBox).Parent.FindControl("txtPrecio") as RadNumericTextBox).Focus();
                    break;
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
                default:
                    break;

            }
        }
        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
        }
        protected void rdFechaF_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                //if (HF_InicioSemana.Value == "" || HF_FinSemana.Value == "")
                //{
                //    Alerta("No está configurada la semana actual");
                //    return;
                //}
                //if (rdFechaF.SelectedDate < Convert.ToDateTime(HF_InicioSemana.Value) || rdFechaF.SelectedDate > Convert.ToDateTime(HF_FinSemana.Value))
                //{
                //    Alerta("La fecha no pertenece a la semana actual");
                //    rdFechaF.SelectedDate = Convert.ToDateTime(HF_FechaActual.Value);
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkMod_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkbox = (sender as CheckBox);

                (chkbox.Parent.FindControl("cmbDia") as RadComboBox).Enabled = chkbox.Checked;
                (chkbox.Parent.FindControl("txtFrecuencia") as RadNumericTextBox).Enabled = chkbox.Checked;

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("popup();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirBuscarDireccionEntrega();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode) && HF_ID.Value != "")
            {

                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPedido cn_pedido = new CN_CapPedido();
                Pedido ped = new Pedido();
                ped.Id_Emp = session.Id_Emp;
                ped.Id_Cd = session.Id_Cd_Ver;
                ped.Id_Ped = Convert.ToInt32(HF_ID.Value);
                cn_pedido.ConsultaPedido(ref ped, session.Emp_Cnx);

                string[] estatus = { "O", "I", "U", "A", "F", "R", "X", "E", "N", "D", "B" };

                if (estatus.Contains(ped.Estatus))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;
                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        ((RadNumericTextBox)editItem.FindControl("txtCantidad")).Enabled = false;
                        //((RadNumericTextBox)editItem.FindControl("txtPrecio")).Enabled = false;
                        //((RadNumericTextBox)editItem.FindControl("txtImporte")).Enabled = false;
                        ((CheckBox)editItem.FindControl("chkModTemp")).Enabled = false;
                    }
                }

                CN_CatTipoVenta cnTv = new CN_CatTipoVenta(session);

                DataRowView drv = (DataRowView)e.Item.DataItem;
                if (drv["Id_TG"] != null)
                {
                    if (!(drv["Id_TG"] is DBNull))
                    {
                        int idTG = (int)drv["Id_TG"];
                        if (idTG != 0)
                        {
                            RadComboBox rcb = e.Item.FindControl("cmbDocumento") as RadComboBox;
                            rcb.SelectedIndex = 1;
                            rcb.Enabled = false;
                        }
                    }
                }
            }
            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["Id_Prod"].FindControl("txtProd");
                if (!dataField.Enabled)
                {
                    dataField = (RadNumericTextBox)form["Acys_Cantidad"].FindControl("txtCantidad");
                }
                dataField.Focus();

                //Edsg 07042015 Si es pedido de internet se desactiva el combo de documento de entrega:
                //if (Request.QueryString["IdPeInt"] != null)
                //   if (e.Item.FindControl("cmbDocumento") != null)
                //        ((RadComboBox)e.Item.FindControl("cmbDocumento")).Enabled = false;


                CN_CatTipoVenta cnTv = new CN_CatTipoVenta(session);

                if (!(e.Item is GridDataInsertItem))
                {
                    DataRowView drv = (DataRowView)e.Item.DataItem;
                    if (drv["Id_TG"] != null)
                    {
                        if (!(drv["Id_TG"] is DBNull))
                        {
                            int idTG = (int)drv["Id_TG"];
                            if (idTG != 0)
                            {
                                RadComboBox rcb = e.Item.FindControl("cmbDocumento") as RadComboBox;
                                ((RadNumericTextBox)e.Item.FindControl("txtPrecio")).ReadOnly = true;
                                rcb.SelectedIndex = 1;
                                rcb.Enabled = false;
                            }
                        }
                    }
                }
            }

            if (e.Item is GridDataItem && !e.Item.IsInEditMode)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                if (!_nuevoPedidoSinProgramar)
                {
                    if (drv["Id_TG"] != null)
                    {
                        if (!(drv["Id_TG"] is DBNull))
                        {
                            int idTG = (int)drv["Id_TG"];
                            if (idTG != 0)
                            {
                                GridDataItem gdi = (e.Item as GridDataItem);
                                if (gdi != null)
                                {
                                    CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                                    string idTGStr = Request.QueryString["Id_TG"];
                                    int? idTGNullable = null;
                                    idTG = 0;
                                    if (idTGStr != null)
                                    {
                                        if (int.TryParse(idTGStr, out idTG))
                                        {
                                            idTGNullable = idTG;
                                        }
                                    }
                                    List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                                    DataTable dtTemp = dt.Clone();
                                    PedidoVtaInst pedido = new PedidoVtaInst();
                                    pedido.Id_Emp = session.Id_Emp;
                                    pedido.Id_Cd = session.Id_Cd_Ver;
                                    pedido.Id_Acs = (int)drv["Id_Acs"];
                                    pedido.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                                    pedido.Acs_Anio = Convert.ToInt32(txtFecha.Value);

                                    cn_capPedidoVI.ConsultarDet(pedido, ref List, ref dtTemp, session.Emp_Cnx, idTGNullable);
                                    var productosExistentes = (from dr in dtTemp.AsEnumerable()
                                                               where (int)dr["Id_Prd"] == (int)drv["Id_Prd"]
                                                               select 1).ToList();
                                    if (productosExistentes.Count > 0)
                                        gdi["DeleteColumn"].Visible = false;
                                    //gdi["EditCommandColumn"].Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void rg1_Resto_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        //PRODUCTOS
        protected void cmbProducto_DataBound(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            //comboBox.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            string id = ((RadNumericTextBox)comboBox.Parent.Parent.FindControl("txtProd")).Text;
            if (id != "")
            {
                comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            ErrorManager();
            RadNumericTextBox rdBox = (sender as RadNumericTextBox);
            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            try
            {
                DataRow[] Ar_dr;
                Ar_dr = dt.Select("Id_prd='" + rdBox.Text + "'");

                int idProd = 0;
                if (!int.TryParse(rdBox.Text, out idProd))
                {
                    AlertaFocus("El formato del identificador del producto debe ser numérico. Por favor, capture un valor aceptable.", rdBox.ClientID);
                    return;
                }
                CN_CapAcys cnCa = new CN_CapAcys();
                if (!txtIdTer.Value.HasValue)
                {
                    AlertaFocus("Por favor, capture un territorio en la vista \"Datos Generales\"", rdBox.ClientID);
                    return;
                }
                if (!txtIdCte.Value.HasValue)
                {
                    AlertaFocus("Por favor, capture un territorio en la vista \"Datos Generales\"", rdBox.ClientID);
                    return;
                }
                if (!txtIdRik.Value.HasValue)
                {
                    AlertaFocus("Por favor, capture un representante de ventas en la vista \"Datos Generales\"", rdBox.ClientID);
                    return;
                }
                if (_nuevoPedidoSinProgramar && cnCa.ExisteProductoEnGarantia(session.Id_Emp, session.Id_Cd, idProd, Convert.ToInt32(txtIdTer.Value.Value), Convert.ToInt32(txtIdCte.Value.Value), Convert.ToInt32(txtIdRik.Value.Value), session))
                {
                    AlertaFocus("Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.", rdBox.ClientID);
                    return;
                }
                if (Ar_dr.Length > 0)
                {
                    rdBox.Text = "";
                    AlertaFocus("Producto ya capturado", rdBox.ClientID);

                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtClave.Text))
                        productoNuevo = 1;
                    pr.Id_Cte = !string.IsNullOrEmpty(txtIdCte.Text) ? Convert.ToInt32(txtIdCte.Text) : 0;
                    cn_catproducto.ConsultaProductos(ref pr, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt32(rdBox.Text), ref productoNuevo);
                    (rdBox.Parent.FindControl("LabelPresent2") as Label).Text = pr.Prd_Presentacion;
                    (rdBox.Parent.FindControl("LabelUnidad2") as Label).Text = pr.Prd_UniNs;
                    (rdBox.Parent.FindControl("txtCantidad") as RadNumericTextBox).Value = 0;
                    (rdBox.Parent.FindControl("txtPrecio") as RadNumericTextBox).Text = pr.Prd_Precio;
                    (rdBox.Parent.FindControl("txtPrecioAcys") as RadNumericTextBox).Text = pr.Prd_Precio;
                    (rdBox.Parent.FindControl("txtImporte") as RadNumericTextBox).Text = pr.Prd_Precio;
                    (rdBox.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = pr.Prd_Descripcion;

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);
                    (rdBox.Parent.FindControl("Labelmes12") as Label).Text = pr.ventaMes[0].ToString();
                    (rdBox.Parent.FindControl("Labelmes22") as Label).Text = pr.ventaMes[1].ToString();
                    (rdBox.Parent.FindControl("Labelmes32") as Label).Text = pr.ventaMes[2].ToString();
                    (rdBox.Parent.FindControl("txtCantidad") as RadNumericTextBox).Focus();
                }
            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, rdBox.ClientID);
                (rdBox.Parent.FindControl("LabelPresent2") as Label).Text = "";
                (rdBox.Parent.FindControl("LabelUnidad2") as Label).Text = "";
                (rdBox.Parent.FindControl("txtCantidad") as RadNumericTextBox).Value = 0;
                (rdBox.Parent.FindControl("txtPrecio") as RadNumericTextBox).Text = "";
                (rdBox.Parent.FindControl("txtPrecioAcys") as RadNumericTextBox).Text = "";
                (rdBox.Parent.FindControl("txtImporte") as RadNumericTextBox).Text = "";
                (rdBox.Parent.FindControl("Labelmes12") as Label).Text = "";
                (rdBox.Parent.FindControl("Labelmes22") as Label).Text = "";
                (rdBox.Parent.FindControl("Labelmes32") as Label).Text = "";
            }
        }
        protected void cmbProducto_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox comboBox = ((RadComboBox)sender);// new RadComboBox() ;
                if (prod)
                {
                    return;
                }
                prod = true;

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 0, Sesion.Emp_Cnx, "spCatProducto_Combo", ref comboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //CLIENTE
        protected void txtIdCte_TextChanged(object sender, EventArgs e)
        {
            CargarCliente();

            //Edsg 29052015
            var pedido = new PedidoVtaInst();
            CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();

            pedido.Id_Emp = session.Id_Emp;
            pedido.Id_Cd = session.Id_Cd_Ver;
            pedido.Id_Acs = 0;
            pedido.Acs_Semana = 0;
            pedido.Acs_Anio = 0;

            if (txtIdCte.Text != "")
                pedido.Id_Cte = Int32.Parse(txtIdCte.Text);


            DataTable dtTemp_Resto = this.dt_Resto;
            List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
            cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, Id_TG);

            this.dt_Resto = dtTemp_Resto;
            rg1_Resto.Rebind();


        }

        //TERRITORIO
        protected void txtTerritorioNom_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (txtTerritorioNom.SelectedValue != "")
            {
                CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                Territorios terr = new Territorios();
                terr.Id_Emp = session.Id_Emp;
                terr.Id_Cd = session.Id_Cd_Ver;
                terr.Id_Ter = Convert.ToInt32(txtTerritorioNom.SelectedValue);
                cn_terr.ConsultaTerritorios(ref terr, session.Emp_Cnx);
                txtRikNom.Text = terr.Rik_Nombre;
                txtIdRik.Text = terr.Id_Rik.ToString();
                txtIdTer.Text = txtTerritorioNom.SelectedValue;
            }
        }

        //GRID
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                        PreGuardar();
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    CerrarVentana();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    double ancho = 0;
                    foreach (GridColumn gc in rg1.Columns)
                    {
                        if (gc.Display && gc.Visible)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    if (rg1.Items.Count > 4)
                    {
                        //  ancho += 500;
                    }

                    rg1.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rg1.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rg1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rg1_Resto_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    double ancho = 0;
                    foreach (GridColumn gc in rg1.Columns)
                    {
                        if (gc.Display && gc.Visible)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    if (rg1.Items.Count > 4)
                    {
                        //ancho += 500;
                    }

                    rg1_Resto.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rg1_Resto.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rg1_Resto.DataSource = dt_Resto;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rg1.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        break;
                    case "Update":
                        Update(e);
                        break;
                    case "Delete":
                        if (rg1.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        else
                        {
                            Delete(e);
                        }

                        rg1.Rebind();

                        string _idTGStr = Request.QueryString["Id_TG"];
                        int _idTG = 0;
                        if (_idTGStr != null)
                        {
                            if (int.TryParse(_idTGStr, out _idTG))
                            {
                                var gi = rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                                LinkButton lnkAgregar = gi.FindControl("LinkButton2") as LinkButton;
                                if (lnkAgregar != null)
                                {
                                    lnkAgregar.Visible = false;
                                }
                            }
                        }

                        rg1_Resto.Rebind();
                        break;
                    case "Edit":
                        rg1.MasterTableView.IsItemInserted = false;
                        break;

                    // Edsg26052015
                    case "DeleteSelected":

                        foreach (GridItem item in rg1.SelectedItems)
                            Delete(item, e);

                        rg1.Rebind();

                        _idTGStr = Request.QueryString["Id_TG"];
                        _idTG = 0;
                        if (_idTGStr != null)
                        {
                            if (int.TryParse(_idTGStr, out _idTG))
                            {
                                var gi = rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                                LinkButton lnkAgregar = gi.FindControl("LinkButton2") as LinkButton;
                                if (lnkAgregar != null)
                                {
                                    lnkAgregar.Visible = false;
                                }
                            }
                        }

                        rg1_Resto.Rebind();

                        break;


                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rg1_PreRender(object sender, EventArgs e)
        {
            //Edsg 07042015 Desactiva boton agregar pedido
            if (Request.QueryString["IdPeInt"] != null)
            {
                foreach (GridDataItem dataItem in rg1.MasterTableView.Items)
                {
                    ((ImageButton)dataItem["EditCommandColumn"].Controls[0]).Enabled = false;
                    ((ImageButton)dataItem["DeleteColumn"].Controls[0]).Enabled = false;
                    GridCommandItem cmdItem = (GridCommandItem)rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                    ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;
                    ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;
                }
            }

        }

        protected void rg1_Resto_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                DataRow[] renglon;

                switch (e.CommandName)
                {
                    case "Agregar":

                        foreach (GridItem item in rg1_Resto.SelectedItems)
                        {

                            int Id_Prd = Convert.ToInt32(((Label)item.FindControl("lblProd_resto")).Text);

                            renglon = dt_Resto.Select("Id_Prd='" + Id_Prd + "'");

                            dt.ImportRow(renglon[0]);
                            dt.AcceptChanges();

                            renglon[0].Delete();
                            dt_Resto.AcceptChanges();

                        }
                        rg1.Rebind();

                        string _idTGStr = Request.QueryString["Id_TG"];
                        int _idTG = 0;
                        if (_idTGStr != null)
                        {
                            if (int.TryParse(_idTGStr, out _idTG))
                            {
                                var gi = rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                                LinkButton lnkAgregar = gi.FindControl("LinkButton2") as LinkButton;
                                if (lnkAgregar != null)
                                {
                                    lnkAgregar.Visible = false;
                                }
                            }
                        }

                        rg1_Resto.Rebind();

                        CalcularTotales();

                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }




        protected void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string Id_prd = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtProd")).Text;
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                pedido_vta.ConsultarAAAEspecial(Sesion.Id_Emp, Sesion.Id_Cd_Ver, txtIdCte.Value.Value, Id_prd, ref verificador, Sesion.Emp_Cnx);

                if (verificador > 0)
                {
                    AlertaFocus("El producto cuenta con precio AAA especial", ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).ClientID);
                }
                double cantidad = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Value.HasValue ? ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Value.Value : 0;
                double precio = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Value.HasValue ? ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Value.Value : 0;
                double importe = cantidad * precio;
                ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).Text = importe.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox rdtn = (RadNumericTextBox)sender;

                string cantidad = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Text;
                string precio = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Text;
                int Prd_Cantidad = 0;
                double Prd_Precio = 0;

                if (cantidad != "")
                {
                    if (int.Parse(cantidad) == 0)
                    {
                        AlertaFocus("La cantidad debe ser mayor a 0", ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).ClientID);
                        return;
                    }
                }
                else
                {
                    AlertaFocus("La cantidad debe ser mayor a 0", ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).ClientID);
                    rdtn.Value = 0;
                    return;
                }

                if (!string.IsNullOrEmpty(cantidad))
                    Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Text);
                if (!string.IsNullOrEmpty(precio))
                    Prd_Precio = Convert.ToDouble(((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Text);

                string Id_prd = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtProd")).Text;
                ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).DbValue = Prd_Cantidad * Prd_Precio;

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<int> Actuales = new List<int>();
                CN_CatProducto catproducto = new CN_CatProducto();
                catproducto.ConsultaProducto_Disponible(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Id_prd, ref Actuales, Sesion.Emp_Cnx);

                if (Actuales.Count > 0)
                {
                    if (Prd_Cantidad > Actuales[2])
                    {
                        AlertaFocus("Inventario disponible insuficiente, <br>Inventario final: " + Actuales[0] + " <br>Asignado: " + Actuales[1] + " <br>Disponible: " + Actuales[2], ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).ClientID);
                        return;
                    }
                    else
                    {
                        ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Focus();
                    }
                }
                else
                {
                    ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Focus();
                }
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                if (!string.IsNullOrEmpty(txtIdCte.Text))
                    pedido_vta.ConsultarAAAEspecial(Sesion.Id_Emp, Sesion.Id_Cd_Ver, txtIdCte.Value.Value, Id_prd, ref verificador, Sesion.Emp_Cnx);
                if (verificador > 0)
                {
                    AlertaFocus("El producto cuenta con precio AAA especial", ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).ClientID);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void PreGuardar()
        {
            try
            {
                string mensaje;
                if (this.rdFechaF.SelectedDate == null)
                {
                    Alerta("Debe capturar la fecha facturación");
                    return;
                }



                if (this.rdFechaE.SelectedDate == null)
                {
                    Alerta("Debe capturar la fecha compromiso de entrega");
                    return;
                }


                //if (CheckBox1.Checked && txtReqOrd.Text == "")
                //{
                //    Alerta("El acuerdo está configurado para que solicite el número de orden de compra, capture este dato por favor");
                //    return;
                //}

                foreach (DataRow dataR in dt.Rows)
                {
                    if (dt.Select("Id_Prd = " + dataR["Id_Prd"].ToString()).Length > 1)
                    {
                        Alerta("El producto " + dataR["Id_Prd"].ToString() + " no puede ser captado mas de una vez en el mismo pedido");
                        return;
                    }
                }

                //Edsg05062015 
                Session["dtPedidoVI" + Session.SessionID] = dt;

                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["Ped_Asignar"].ToString() == "")
                        dr["Ped_Asignar"] = 0;

                    if (Convert.ToInt32(dr["Ped_Asignar"]) > 0)
                    {
                        Alerta("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido");
                        return;
                    }
                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr = new Producto();
                    List<int> actuales = new List<int>();
                    cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString(), ref actuales, session.Emp_Cnx);

                    if ((Convert.ToInt32(dr["Prd_Cantidad"]) - (Convert.ToInt32(dr["Ped_Asignar"] == DBNull.Value ? 0 : dr["Ped_Asignar"]))) > actuales[2])
                    {
                        RAM1.ResponseScripts.Add("return AbrirVentana_InvIns('" + rdFechaF.SelectedDate.ToString().Replace("/", "") + "','" + this.TxtPed_ReqAcys.Text + "','" + txtClave.Text + "')");
                        return;
                    }
                }

                mensaje = "Revisa los siguientes datos </br>Fecha de la factura</br>Orden de compra</br></br>¿Desea hacer alguna corrección?";
                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "</br></br>',  confirmCallBackFnGuardar, 330, 200);");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {
                rtb1.Enabled = false;
                if (dt.Rows.Count == 0)
                {
                    Alerta("No ha agregado ningún producto al detalle");
                    rtb1.Enabled = true;
                    return;
                }

                DataRow[] dr = dt.Select("Acs_Doc = ''");
                if (dr.Length > 0)
                {
                    Alerta("No se seleccionó documento de entrega para el producto <b>" + dr[0][1] + " - " + dr[0][2] + "</b>");
                    rtb1.Enabled = true;
                    return;
                }

                int verificador = -1;
                Funciones funcion = new Funciones();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Cte = Convert.ToInt32(txtIdCte.Text);
                pedido.Ped_Fecha = funcion.GetLocalDateTime(session.Minutos);
                pedido.Id_Rik = Convert.ToInt32(txtIdRik.Text);

                if (txtIdTer.Text == "")
                {
                    Alerta("El Territorio es Requerido");
                    rtb1.Enabled = true;
                    return;

                }
                pedido.Id_Ter = Convert.ToInt32(txtIdTer.Text);

                pedido.Pedido_del = TxtPed_PedAcys.Text.Trim();
                pedido.Requisicion = TxtPed_ReqAcys.Text.Trim();
                pedido.Ped_Solicito = txtContactoNom.Text;
                pedido.Ped_Flete = string.Empty;
                pedido.Ped_OrdenEntrega = TxtPed_OCAcys.Text.Trim();
                pedido.Ped_CondEntrega = 0;
                pedido.Ped_FechaEntrega = rdFechaE.SelectedDate.Value;
                pedido.Ped_Comentarios = txtNotas.Text;

                pedido.Ped_Observaciones = string.Empty;
                pedido.Ped_DescPorcen1 = 0;
                pedido.Ped_DescPorcen2 = 0;
                pedido.Ped_Desc1 = string.Empty;
                pedido.Ped_Desc2 = string.Empty;
                pedido.Ped_Importe = txtSubtotal.Value.HasValue ? txtSubtotal.Value.Value : 0;
                pedido.Ped_Subtotal = txtSubtotal.Value.HasValue ? txtSubtotal.Value.Value : 0;
                pedido.Ped_Iva = txtIva.Value.HasValue ? txtIva.Value.Value : 0;
                pedido.Ped_Total = txtTotal.Value.HasValue ? txtTotal.Value.Value : 0;

                pedido.Id_U = session.Id_U;
                pedido.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                pedido.Id_Acs = Convert.ToInt32(txtClave.Value);
                pedido.Acs_Anio = Convert.ToInt32(txtFecha.Value);
                pedido.Ped_SolicitoTel = txtContactoTel.Text;
                pedido.Ped_SolicitoEmail = txtContactoMail.Text;
                pedido.Ped_SolicitoPuesto = txtContactoPuesto.Text;
                pedido.Ped_ConsignadoCalle = txtCalle.Text;
                pedido.Ped_ConsignadoNo = txtNo.Text;
                pedido.Ped_ConsignadoCp = txtCp.Text;
                pedido.Ped_ConsignadoMunicipio = txtMunicipio.Text;
                pedido.Ped_ConsignadoEstado = txtEstado.Text;
                pedido.Ped_ConsignadoColonia = txtColonia.Text;
                pedido.Ped_ReqOrden = ChkOrdCompra.Checked;
                pedido.Ped_OrdenCompra = TxtPed_OCAcys.Text;
                pedido.Ped_AcysSemana = Convert.ToInt32(txtSemana.Value);
                pedido.Ped_AcysAnio = Convert.ToInt32(txtFecha.Value);
                pedido.Id_Acs = Convert.ToInt32(txtClave.Value);
                pedido.Estatus = "U";
                pedido.Ped_Tipo = txtClave.Text == "" || txtClave.Text == "0" ? 4 : 3;

                // Edsg Proyecto Internet
                if (rdModInternet.Checked) pedido.Ped_Tipo = 5;
                pedido.FechaFacAcys = rdFechaF.SelectedDate.Value;
                pedido.PedAcys = TxtPed_PedAcys.Text.Trim();
                pedido.ReqAcys = TxtPed_ReqAcys.Text.Trim();
                pedido.OcAcys = TxtPed_OCAcys.Text.Trim();



                CN_CapPedidoVtaInst clsCapPedido = new CN_CapPedidoVtaInst();

                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        rtb1.Enabled = true;
                        return;
                    }

                    int _prd = 0;
                    for (int x = 0; x < rg1.Items.Count; x++)
                    {
                        _prd = Convert.ToInt32((rg1.Items[x]["Id_Prod"].FindControl("lblProd") as Label).Text);
                        PedidoVtaInst pvi = new PedidoVtaInst();
                        pvi.Id_Emp = session.Id_Emp;
                        pvi.Id_Cd = session.Id_Cd_Ver;
                        pvi.Id_Cte = Convert.ToInt32(txtIdCte.Text);
                        pvi.Id_Ter = Convert.ToInt32(txtIdTer.Text);
                        pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                        pvi.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                        clsCapPedido.ConsultarPedidoExistente(pvi, _prd, session.Emp_Cnx, ref verificador);

                        if (verificador == 1)
                        {
                            Alerta("El producto " + _prd.ToString() + " ya ha sido captado por otro usuario");
                            rtb1.Enabled = true;
                            return;
                        }
                    }

                    /**
                     * Si el pedido en captura tiene una garantía asociada, esta se pasa en el registro; en caso contrario se establece a nulo.
                     **/
                    
                    if (Id_TG != 0)
                    {
                        if (Request.QueryString["Id"] != "")
                        {
                            CapaEntidad.Acys acys = new Acys();
                            acys.Id_Emp = session.Id_Emp;
                            acys.Id_Cd = session.Id_Cd;
                            acys.Id_Acs = Convert.ToInt32(Request.QueryString["Id"]);
                            CN_CapAcys cnCapAcys = new CN_CapAcys();
                            cnCapAcys.ConsultaUltimaVersion(ref acys, session.Emp_Cnx);

                            clsCapPedido.Insertar(pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, acys.Id_AcsVersion);
                        }
                        else
                        {
                            clsCapPedido.Insertar(pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, null);
                        }
                    }
                    else
                    {
                        clsCapPedido.Insertar(pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, null);
                    }

                    if (verificador >= 1)
                    {
                        Session["Id_Ped" + Session.SessionID] = verificador;
                        RAM1.ResponseScripts.Add("radconfirm('¿Desea imprimir el pedido?</br></br>',  confirmCallBackFn, 330, 150);");
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar guardar el pedido");
                        rtb1.Enabled = true;
                    }
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        rtb1.Enabled = true;
                        return;
                    }
                    pedido.Id_Ped = Convert.ToInt32(HF_ID.Value);
                    int captado = 0;
                    if (Request.QueryString["IdVI"] != null)
                        captado = Convert.ToInt32(txtFolio.Text);

                    clsCapPedido.Modificar(pedido, dt, session.Emp_Cnx, captado, ref verificador, al);

                    if (verificador >= 1)
                    {
                        Session["Id_Ped" + Session.SessionID] = verificador;
                        RAM1.ResponseScripts.Add("radconfirm('¿Desea imprimir el pedido?</br></br>',  confirmCallBackFn, 330, 150);");
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar guardar el pedido");
                        rtb1.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        private void Inicializar()
        {
            CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
            CentroDistribucion centroDistribucion = new CentroDistribucion();
            double iva = iva_cd;
            cd.ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva, session.Emp_Cnx);
            iva_cd = iva;
            UltimosPeriodos();
            if (Request.QueryString["idP"] != null)
            {
                txtFolio.Text = Request.QueryString["idP"].ToString();
                CargarPedido();
            }
            else
            {//IdVI
                if (Request.QueryString["IdVI"] != null)
                {
                    if (Session["PedidoCaptado" + Session.SessionID] != null)
                        txtFolio.Text = Session["PedidoCaptado" + Session.SessionID].ToString();
                    CargarPedido();
                    Session["PedidoCaptado" + Session.SessionID] = null;
                }
                else
                    // Pedidos de internet
                    if (Request.QueryString["IdPeInt"] != null)
                    {
                        //txtFolio.Text = Request.QueryString["IdPeInt"].ToString();

                        this.CargarPedidoInternet(Convert.ToInt32(Request.QueryString["IdPeInt"]));




                    }
                    else
                    {
                        txtFolio.Text = MaximoId();

                        if (Request.QueryString["id"] != "-1")
                        {
                            txtClave.Text = Request.QueryString["id"];
                            CargarAcuerdo();
                            if (string.IsNullOrEmpty(txtClave.Text))
                                productoNuevo = 1;
                        }
                    }
            }
            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            ValidarPermisos();

        }
        private void UltimosPeriodos()
        {
            try
            {
                Funciones funciones = new Funciones();

                rg1.Columns[5].HeaderText = "Venta " + Nombre(funciones.GetLocalDateTime(session.Minutos).AddMonths(-3).Month);
                rg1.Columns[6].HeaderText = "Venta " + Nombre(funciones.GetLocalDateTime(session.Minutos).AddMonths(-2).Month);
                rg1.Columns[7].HeaderText = "Venta " + Nombre(funciones.GetLocalDateTime(session.Minutos).AddMonths(-1).Month);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string Nombre(int p)
        {
            switch (p)
            {
                case 1: return "Ene.";
                case 2: return "Feb.";
                case 3: return "Mar.";
                case 4: return "Abr.";
                case 5: return "May.";
                case 6: return "Jun.";
                case 7: return "Jul.";
                case 8: return "Ago.";
                case 9: return "Sep.";
                case 10: return "Oct.";
                case 11: return "Nov.";
                case 12: return "Dic.";
                default: return "";

            }
        }
        private void CargarAcuerdo()
        {
            try
            {
                int verificador = 0;
                double imp = 0;
                DateTime fechaf = default(DateTime);
                Funciones funcion = new Funciones();
                txtFecha.Text = Request.QueryString["Anio"].ToString();
                txtSemana.Text = Request.QueryString["Semana"].ToString();

                if (Request.QueryString["Id"] != "")
                {
                    PedidoVtaInst pedido = new PedidoVtaInst();
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = Convert.ToInt32(Request.QueryString["Id"]);
                    Clientes cc = new Clientes();
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                    cn_capPedidoVI.Consultar(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                    if (verificador == 1)
                    {

                        txtClienteNom.Text = pedido.Cte_Nom;
                        txtIdCte.DbValue = pedido.Id_Cte;
                        txtRikNom.Text = pedido.Rik_Nombre;
                        txtIdRik.DbValue = pedido.Id_Rik;

                        txtTerritorioNom.Text = pedido.Ter_Nombre;
                        txtIdTer.DbValue = pedido.Id_Ter;

                        //txtContactoNom.Text = pedido.Acs_Contacto;
                        //txtContactoTel.Text = pedido.Acs_Telefono;
                        //txtContactoMail.Text = pedido.Acs_email;
                        //txtContactoPuesto.Text = pedido.Acs_Puesto;

                        txtContactoNom.Enabled = false;
                        txtContactoTel.Enabled = false;
                        txtContactoMail.Enabled = false;
                        txtContactoPuesto.Enabled = false;

                        txtCalle.Text = cc.Cte_Calle;
                        txtNo.Text = cc.Cte_Numero;
                        txtCp.DbValue = cc.Cte_Cp;
                        txtMunicipio.Text = cc.Cte_Municipio;
                        txtEstado.Text = cc.Cte_Estado;
                        txtColonia.Text = cc.Cte_Colonia;

                        TxtVersion.Text = pedido.Id_AcsVersion.ToString();
                        txtContactoNom.Text = pedido.Acs_PedidoEncargadoEnviar;
                        txtContactoTel.Text = pedido.Acs_PedidoTelefono;
                        txtContactoMail.Text = pedido.Acs_PedidoEmail;
                        txtContactoPuesto.Text = pedido.Acs_PedidoPuesto;
                        this.ChkOrdCompra.Checked = pedido.Acs_ReqOrden;
                        this.ChckOrdReposicion.Checked = pedido.Acs_ReqDocReposicion;
                        this.ChckFolio.Checked = pedido.Acs_ReqDocFolio;
                        this.LblEOtro.Text = pedido.Acs_ReqDocOtro;

                        if (pedido.Acs_Modalidad == "A")
                        {
                            rdModFrencuenciaEstablecida.Checked = true;
                        }
                        else if (pedido.Acs_Modalidad == "B")
                        {
                            rdModOrdenAbierta.Checked = true;
                        }
                        else if (pedido.Acs_Modalidad == "C")
                        {
                            rdModConsignacion.Checked = true;
                        }
                        this.txtContactoClientealmacen.Text = pedido.Acs_Contacto3;
                        if (pedido.Acs_Telefono3 == "0")
                        {
                            this.txtContactoClientealmacenTel.Text = "";
                        }
                        else
                        {
                            this.txtContactoClientealmacenTel.Text = pedido.Acs_Telefono3;
                        }
                        this.txtContactoClientealmacenEmail.Text = pedido.Acs_Email3;


                        //CheckBox1.Checked = pedido.Acs_ReqOrden;
                    }
                    else
                    {
                        Alerta("No se encontro");
                    }

                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = Convert.ToInt32(Request.QueryString["Id"]);
                    pedido.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                    pedido.Acs_Anio = Convert.ToInt32(txtFecha.Value);

                    dt = null;

                    GetListDet();
                    DataTable dtTemp = dt;
                    List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                    string idTGStr = Request.QueryString["Id_TG"];
                    int? idTGNullable = 0;
                    int idTG = 0;
                    if (idTGStr != null)
                    {
                        if (int.TryParse(idTGStr, out idTG))
                        {
                            idTGNullable = idTG;
                        }
                    }
                    cn_capPedidoVI.ConsultarDet(pedido, ref List, ref dtTemp, session.Emp_Cnx, idTGNullable);

                    rgAcuerdos.DataSource = List;
                    rgAcuerdos.DataBind();

                    DataTable dtTemp_Resto = this.dt_Resto;
                    List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                    cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, Id_TG);



                    dt = dtTemp;
                    dt_Resto = dtTemp_Resto;

                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr;
                    foreach (DataRow dr in dt.Rows)
                    {
                        pr = new Producto();
                        pr.Id_Emp = session.Id_Emp;
                        pr.Id_Cd = session.Id_Cd_Ver;
                        pr.Id_Prd = Convert.ToInt32(dr["Id_prd"]);

                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                        dr["mes1"] = pr.ventaMes[0].ToString();
                        dr["mes2"] = pr.ventaMes[1].ToString();
                        dr["mes3"] = pr.ventaMes[2].ToString();
                    }

                    foreach (DataRow dr in dt_Resto.Rows)
                    {
                        pr = new Producto();
                        pr.Id_Emp = session.Id_Emp;
                        pr.Id_Cd = session.Id_Cd_Ver;
                        pr.Id_Prd = Convert.ToInt32(dr["Id_prd"]);

                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                        dr["mes1"] = pr.ventaMes[0].ToString();
                        dr["mes2"] = pr.ventaMes[1].ToString();
                        dr["mes3"] = pr.ventaMes[2].ToString();
                    }
                }
                else
                {
                    //imgBuscar.Visible = true;
                    txtIdCte.AutoPostBack = true;
                    txtTerritorioNom.AutoPostBack = true;
                    txtIdCte.Enabled = true;
                    txtIdCte.ReadOnly = false;
                    txtIdTer.ReadOnly = false;
                    txtIdTer.Enabled = true;
                    imgBuscar.Visible = true;
                    GetListDet();
                    fechaf = Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString()) > session.CalendarioFin ? session.CalendarioFin : Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString());
                    fechaf = fechaf.AddDays(1);
                    rg1.Columns.FindByUniqueName("Acs_Frecuencia").Display = false;
                    rg1.Columns.FindByUniqueName("Acs_Dia").Display = false;
                    rg1.Columns.FindByUniqueName("Mod").Display = false;

                    _nuevoPedidoSinProgramar = true;

                }

                rg1.Rebind();

                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {
                        var gi = rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                        LinkButton lnkAgregar = gi.FindControl("LinkButton2") as LinkButton;
                        if (lnkAgregar != null)
                        {
                            lnkAgregar.Visible = false;
                        }
                    }
                }

                rg1_Resto.Rebind();

                foreach (DataRow i in dt.Rows)
                {
                    imp += Convert.ToDouble(i["Prd_Importe"]);
                    //    if (i["Acs_FechaF"] == DBNull.Value||fechaf > Convert.ToDateTime(i["Acs_FechaF"]) || fechaf == default(DateTime))
                    //    {
                    //        fechaf = Convert.ToDateTime(i["Acs_FechaF"]);
                    //    }
                }
                txtSubtotal.DbValue = imp;
                //rdFechaF.DbSelectedDate = fechaf.Year == 1 ? (DateTime?)null : fechaf;
                //Funciones funcion = new Funciones();
                //DateTime fecha_actual = funcion.GetLocalDateTime(session.Minutos).AddDays(1);
                //rdFechaF.DbSelectedDate = fecha_actual > session.CalendarioFin ? session.CalendarioFin : fecha_actual;


                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString());
                verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    HF_FechaActual.Value = rdFechaF.SelectedDate.ToString();
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString();
                }

                double iva_cd = 0;
                CN_CatCentroDistribucion cn = new CN_CatCentroDistribucion();
                cn.ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);

                txtIva.DbValue = imp * iva_cd / 100;

                txtTotal.DbValue = txtSubtotal.Value + txtIva.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool _nuevoPedidoSinProgramar
        {
            get
            {
                return (bool)ViewState["_nuevoPedidoSinProgramar"];
            }
            set
            {
                ViewState["_nuevoPedidoSinProgramar"] = value;
            }
        }


        private void CargarPedidoInternet(int num_pedido)
        {
            try
            {
                //  HF_ID.Value = txtFolio.Text;
                Sesion sesion = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido_Internet pedido = new Pedido_Internet();
                ClienteDirEntrega dirEntrega = new ClienteDirEntrega();

                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Num_Pedido = num_pedido;

                CN_CapPedido_Internet cn_capPedidoInternet = new CN_CapPedido_Internet();
                cn_capPedidoInternet.ConsultarPedido_Datos(session.Emp_Cnx, ref pedido, ref dirEntrega, num_pedido);

                txtClienteNom.Text = pedido.Nom_Cliente;
                txtIdCte.DbValue = pedido.Id_Cte;
                TxtPed_ReqAcys.Text = pedido.Num_Pedido.ToString();

                CargarTerritorios();

                //txtRikNom.Text = pedido.Rik_Nombre;
                //txtIdRik.DbValue = pedido.Id_Rik;
                //txtTerritorioNom.Text = pedido.Ter_Nombre;
                //txtIdTer.DbValue = pedido.Id_Ter;

                txtContactoNom.Text = pedido.Nombre_Usuario;
                txtContactoTel.Text = pedido.Telefono_Usuario;
                txtContactoMail.Text = pedido.Cuenta_Usuario;
                txtContactoPuesto.Text = "Usuario Internet";

                txtCalle.Text = dirEntrega.Cte_Calle;
                txtNo.Text = dirEntrega.Cte_Numero;

                int res = 0;
                if (Int32.TryParse(dirEntrega.Cte_Cp, out res)) txtCp.DbValue = dirEntrega.Cte_Cp;

                txtMunicipio.Text = dirEntrega.Cte_Municipio;
                txtEstado.Text = dirEntrega.Cte_Estado;
                txtColonia.Text = dirEntrega.Cte_Colonia;
                txtNotas.Text = pedido.Observaciones;

                txtContactoClientealmacen.Text = pedido.Nombre_Usuario;
                if (Int32.TryParse(pedido.Telefono, out res)) txtContactoClientealmacenTel.Text = pedido.Telefono;
                txtContactoClientealmacenEmail.Text = pedido.Correo;
                txtContactoClientealmacen.Text = pedido.Nombre;


                txtRHoraam1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + dirEntrega.Cte_HoraAm1);
                txtRHoraam2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + dirEntrega.Cte_HoraAm2);
                txtRHorapm1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + dirEntrega.Cte_HoraPm1);
                txtRHorapm2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + dirEntrega.Cte_HoraPm2);

                rdModInternet.Checked = true;



                txtSubtotal.DbValue = pedido.Subtotal;
                txtIva.DbValue = pedido.IVA;
                txtTotal.DbValue = pedido.Total;

                //Edsg Desactiva los campos
                TxtPed_ReqAcys.Enabled = false;
                txtNotas.Enabled = false;
                ImgBuscarDireccionEntrega.Enabled = false;



                GetListDet();

                DataTable dtTemp = dt;
                cn_capPedidoInternet.ConsultarPedido_Detalle(session.Emp_Cnx, num_pedido, ref dtTemp);
                dt = dtTemp;

                CN_CatProducto cn_catproducto = new CN_CatProducto();
                Producto pr;
                foreach (DataRow dr in dt.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt32(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString();
                    dr["mes2"] = pr.ventaMes[1].ToString();
                    dr["mes3"] = pr.ventaMes[2].ToString();
                }
                rg1.Rebind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarPedido()
        {
            try
            {
                HF_ID.Value = txtFolio.Text;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtFolio.Text);

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                cn_capPedido.ConsultaPedido(ref pedido, session.Emp_Cnx);

                txtClienteNom.Text = pedido.Cte_NomComercial;
                txtIdCte.DbValue = pedido.Id_Cte;

                CargarTerritorios();

                txtRikNom.Text = pedido.Rik_Nombre;
                txtIdRik.DbValue = pedido.Id_Rik;
                txtTerritorioNom.Text = pedido.Ter_Nombre;
                txtIdTer.DbValue = pedido.Id_Ter;

                txtContactoNom.Text = pedido.Ped_Solicito;
                txtContactoTel.Text = pedido.Ped_SolicitoTel;
                txtContactoMail.Text = pedido.Ped_SolicitoEmail;
                txtContactoPuesto.Text = pedido.Ped_SolicitoPuesto;

                txtCalle.Text = pedido.Ped_ConsignadoCalle;
                txtNo.Text = pedido.Ped_ConsignadoNo;
                txtCp.DbValue = pedido.Ped_ConsignadoCp;
                txtMunicipio.Text = pedido.Ped_ConsignadoMunicipio;
                txtEstado.Text = pedido.Ped_ConsignadoEstado;
                txtColonia.Text = pedido.Ped_ConsignadoColonia;
                txtNotas.Text = pedido.Ped_Comentarios;

                //CheckBox1.Checked = pedido.Ped_ReqOrden;
                //txtReqOrd.Text = pedido.Ped_OrdenCompra;


                txtFecha.DbValue = pedido.Ped_AcysAnio;
                txtSemana.DbValue = pedido.Ped_AcysSemana;
                txtClave.Value = pedido.Id_Acs;



                txtSubtotal.DbValue = pedido.Ped_Subtotal;
                txtIva.DbValue = pedido.Ped_Iva;
                txtTotal.DbValue = pedido.Ped_Total;

                rdFechaE.SelectedDate = pedido.Ped_FechaEntrega;

                if (pedido.FechaFacAcys.Year != 1)
                {
                    rdFechaF.SelectedDate = pedido.FechaFacAcys;
                }
                TxtPed_PedAcys.Text = pedido.Pedido_del;
                TxtPed_ReqAcys.Text = pedido.Requisicion;
                TxtPed_OCAcys.Text = pedido.Ped_OrdenCompra;


                pedido.Ped_Tipo = 3;
                pedido.Ped_Captacion = true;

                CargarInfoAcys(pedido.Id_Acs, pedido.Ped_AcysSemana, pedido.Ped_AcysAnio);

                GetListDet();
                DataTable dtTemp = dt;
                cn_capPedido.ConsultaPedidoDet(pedido, ref dtTemp, session.Emp_Cnx);
                dt = dtTemp;

                CN_CatProducto cn_catproducto = new CN_CatProducto();
                Producto pr;
                foreach (DataRow dr in dt.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt32(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString();
                    dr["mes2"] = pr.ventaMes[1].ToString();
                    dr["mes3"] = pr.ventaMes[2].ToString();
                }
                rg1.Rebind();

                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {
                        var gi = rg1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                        LinkButton lnkAgregar = gi.FindControl("LinkButton2") as LinkButton;
                        if (lnkAgregar != null)
                        {
                            lnkAgregar.Visible = false;
                        }
                    }
                }

                Funciones funcion = new Funciones();
                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
                int verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    HF_FechaActual.Value = rdFechaF.SelectedDate.ToString();
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarInfoAcys(int Id_Acys, int AcysMes, int AcysAnio)
        {
            try
            {
                int verificador = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = Id_Acys;
                Clientes cc = new Clientes();
                CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                cn_capPedidoVI.Consultar(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                txtCalle.Text = cc.Cte_Calle;
                txtNo.Text = cc.Cte_Numero;
                txtCp.DbValue = cc.Cte_Cp;
                txtMunicipio.Text = cc.Cte_Municipio;
                txtEstado.Text = cc.Cte_Estado;
                txtColonia.Text = cc.Cte_Colonia;

                TxtVersion.Text = pedido.Id_AcsVersion.ToString();
                txtContactoNom.Text = pedido.Acs_PedidoEncargadoEnviar;
                txtContactoTel.Text = pedido.Acs_PedidoTelefono;
                txtContactoMail.Text = pedido.Acs_PedidoEmail;
                txtContactoPuesto.Text = pedido.Acs_PedidoPuesto;
                this.ChkOrdCompra.Checked = pedido.Acs_ReqOrden;
                this.ChckOrdReposicion.Checked = pedido.Acs_ReqDocReposicion;
                this.ChckFolio.Checked = pedido.Acs_ReqDocFolio;
                this.LblEOtro.Text = pedido.Acs_ReqDocOtro;

                if (pedido.Acs_Modalidad == "A")
                {
                    rdModFrencuenciaEstablecida.Checked = true;
                }
                else if (pedido.Acs_Modalidad == "B")
                {
                    rdModOrdenAbierta.Checked = true;
                }
                else if (pedido.Acs_Modalidad == "C")
                {
                    rdModConsignacion.Checked = true;
                }
                this.txtContactoClientealmacen.Text = pedido.Acs_Contacto3;
                if (pedido.Acs_Telefono3 == "0")
                {
                    this.txtContactoClientealmacenTel.Text = "";
                }
                else
                {
                    this.txtContactoClientealmacenTel.Text = pedido.Acs_Telefono3;
                }
                this.txtContactoClientealmacenEmail.Text = pedido.Acs_Email3;



                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = Id_Acys;
                pedido.Acs_Semana = AcysMes;
                pedido.Acs_Anio = AcysAnio;
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                cn_capPedidoVI.ConsultarDetAcys(pedido, ref List, session.Emp_Cnx);

                rgAcuerdos.DataSource = List;
                rgAcuerdos.DataBind();



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CargarTerritorios()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtIdCte.Value.HasValue ? txtIdCte.Value.Value : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, Sesion.Emp_Cnx, "spCatTerCte_Combo", ref txtTerritorioNom);
                if (txtTerritorioNom.Items.Count > 1)
                {
                    txtTerritorioNom.SelectedIndex = 1;
                    txtTerritorioNom.Text = txtTerritorioNom.Items[1].Text;
                    txtIdTer.Text = txtTerritorioNom.Items[1].Value;
                }
                else
                {
                    if (Request.QueryString["id"] != "-1")
                    {
                        CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtIdCte.Value.HasValue ? txtIdCte.Value.Value : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, 0, Sesion.Emp_Cnx, "spCatTerCte_Combo", ref txtTerritorioNom);
                        if (txtTerritorioNom.Items.Count > 1)
                        {
                            txtTerritorioNom.SelectedIndex = 1;
                            txtTerritorioNom.Text = txtTerritorioNom.Items[1].Text;
                            txtIdTer.Text = txtTerritorioNom.Items[1].Value;
                        }
                    }
                    else
                    {
                        txtIdTer.Text = "";
                        txtTerritorioNom.Items.Clear();
                        txtTerritorioNom.Text = "";
                    }
                }
                if (txtTerritorioNom.SelectedValue != "")
                {
                    CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                    Territorios terr = new Territorios();
                    terr.Id_Emp = session.Id_Emp;
                    terr.Id_Cd = session.Id_Cd_Ver;
                    terr.Id_Ter = Convert.ToInt32(txtTerritorioNom.SelectedValue);
                    cn_terr.ConsultaTerritorios(ref terr, session.Emp_Cnx);

                    txtRikNom.Text = terr.Rik_Nombre;
                    txtIdRik.Text = terr.Id_Rik.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiarRegistro(RadComboBox rdBox)
        {
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtProd")).Text = string.Empty;
            ((Label)rdBox.Parent.Parent.FindControl("lblPres2")).Text = string.Empty;
            ((Label)rdBox.Parent.Parent.FindControl("lblUnidad2")).Text = string.Empty;
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtPrecio")).Text = "0";
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtCantidad")).Text = "0";
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtImporte")).Text = "0";
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapPedido", "Id_Ped", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            try
            {
                //Sesion Sesion =  (Sesion)Session["Sesion" + Session.SessionID];  
                //Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Funciones funciones = new Funciones();
                //rdFecha.SelectedDate = funciones.GetLocalDateTime(Sesion.Minutos);
                //dpFecha2.SelectedDate = funciones.GetLocalDateTime(Sesion.Minutos).AddDays(1);
                //rdFecha.Focus();
                //txtClave.Text = MaximoId();
                //txtComentarios.Text = string.Empty;
                //txtConcepto.Text = string.Empty;
                //txtConcepto2.Text = string.Empty;
                //txtCondiciones.Text = string.Empty;
                //txtDescuento.Text = string.Empty;
                //txtDescuento2.Text = string.Empty;
                //txtFlete.Text = string.Empty;
                //txtImporte.Text = string.Empty;
                //txtIVA.Text = string.Empty;
                //txtNumCliente.Text = string.Empty;
                //txtObservaciones.Text = string.Empty;
                //txtOrden.Text = string.Empty;
                //txtPedidodel.Text = string.Empty;
                //txtRepresentanteID.Text = string.Empty;
                //txtRequisicion.Text = string.Empty;
                //txtSolicito.Text = string.Empty;
                //txtSub.Text = string.Empty;
                //txtTerritorio.Text = string.Empty;
                //txtTotal.Text = string.Empty;

                //cmbCliente.SelectedIndex = 0;
                //cmbCliente.Text = cmbCliente.Items[0].Text;

                //cmbRik.SelectedIndex = 0;
                //cmbRik.Text = cmbCliente.Items[0].Text;

                //cmbTerritorio.SelectedIndex = 0;
                //cmbTerritorio.Text = cmbCliente.Items[0].Text;

                //HF_ID.Value = "";

                //dt.Rows.Clear();
                //rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                RadComboBox productoDet = (sender.Parent.Parent.FindControl("cmbDescr") as RadComboBox);
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt32(sender.SelectedValue), session.Emp_Cnx, "spCatProdSeg_Combo", ref productoDet);
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
                //if (this.HiddenRebind.Value == "0")
                //{
                //    funcion = "CloseWindow()";
                //}
                //else
                //{
                funcion = "CloseAndRebind()";
                //}
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Imprimir(int Id_Ped)
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
                ALValorParametrosInternos.Add(Id_Ped);

                Type instance = null;
                instance = typeof(LibreriaReportes.PedidoImpresion);
                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                RAM1.ResponseScripts.Add("AbrirReportePadre();");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void imprimir()
        {

            try
            {
                CapaDatos.Funciones funciones = new CapaDatos.Funciones();

                CapaDatos.Funciones fecha = new CapaDatos.Funciones();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                CapaDatos.Funciones CD = new CapaDatos.Funciones();

                ALValorParametrosInternos.Add(Sesion.Id_Emp);

                ALValorParametrosInternos.Add(Sesion.Emp_Cnx);

                Type instance = null;
                instance = typeof(LibreriaReportes.ReportePrueba);
                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReportePadre()");
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
                this.rtb1.Items[6].Visible = false;
                if (_PermisoGuardar == false & _PermisoModificar == false)
                {
                    this.rtb1.Items[5].Visible = false;
                }

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
        private void CargarCliente()
        {

            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            bool cancelar = false;
            Clientes cte = new Clientes();
            cte.Id_Emp = Sesion.Id_Emp;
            cte.Id_Cd = Sesion.Id_Cd_Ver;
            cte.Id_Cte = Convert.ToInt32(txtIdCte.Value.HasValue ? txtIdCte.Value.Value : -1);
            cte.Id_Rik = Sesion.Id_Rik;
            CN_CatCliente catcliente = new CN_CatCliente();
            try
            {
                catcliente.ConsultaClientes(ref cte, Sesion.Emp_Cnx);
                if (!cte.Cte_Facturacion)
                {
                    AlertaFocus("CUIDADO: Este cliente se encuentra bloqueado por parte de cobranza; favor de aclarar su situación de créditos", txtIdCte.ClientID);
                    cancelar = true;
                }


                if (cte.Cte_CreditoSuspendido)
                {

                    AlertaFocus("Este cliente tiene el crédito suspendido", txtIdCte.ClientID);
                    cancelar = true;
                }

            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, txtIdCte.ClientID);
                cancelar = true;
            }


            CargarTerritorios();
            txtClienteNom.Text = cte.Cte_NomComercial;
            txtContactoNom.Text = cte.Cte_Contacto;
            txtContactoMail.Text = cte.Cte_Email;
            txtContactoTel.Text = cte.Cte_Telefono;
            txtCalle.Text = cte.Cte_Calle;
            txtColonia.Text = cte.Cte_Colonia;
            txtEstado.Text = cte.Cte_Estado;
            txtNo.Text = cte.Cte_Numero;


            if (cte.Cte_Cp != null)
            {
                if (cte.Cte_Cp.Trim() != "")
                {
                    txtCp.Text = cte.Cte_Cp;
                }
            }
            txtMunicipio.Text = cte.Cte_Municipio;




            if (cancelar)
            {

                txtClienteNom.Text = "";
                txtIdCte.Text = "";
                txtIdTer.Text = "";
                txtIdRik.Text = "";
                txtTerritorioNom.Items.Clear();
                txtTerritorioNom.Text = "";
                txtIdRik.Text = "";
                txtRikNom.Text = "";

            }

        }
        //Grid
        private void GetListDet()
        {
            try
            {
                dt = new DataTable();
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Id_PrdOld", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Unidad", System.Type.GetType("System.String"));

                dt.Columns.Add("Mes1", System.Type.GetType("System.Double"));
                dt.Columns.Add("Mes2", System.Type.GetType("System.Double"));
                dt.Columns.Add("Mes3", System.Type.GetType("System.Double"));

                dt.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_PrecioAcys", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Importe", System.Type.GetType("System.Double"));
                dt.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_FechaF", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Mod", System.Type.GetType("System.Boolean"));
                dt.Columns.Add("Acs_Dia", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_DiaStr", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_Frecuencia", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_RemFact", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Ped_Asignar", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_TG", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Acs", System.Type.GetType("System.Int32"));

                dt_Resto = dt.Clone();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete(GridCommandEventArgs e)
        {
            DataRow[] Ar_dr;
            GridItem gi = e.Item;

            int Id_Prd = Convert.ToInt32(((Label)gi.FindControl("lblProd")).Text);
            int Cantasignado = gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"] == DBNull.Value ? 0 : Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"]);
            if (Request.QueryString["IdVI"] != null && Cantasignado > 0)
            {
                Alerta("No es posible eliminar el producto captado");
                e.Canceled = true;
                return;
            }
            else
            {

                al.Add(Id_Prd);
                Ar_dr = dt.Select("Id_Prd='" + Id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    var ProductosNoAcys = (List<Int32>)Session["ProductosNoAcys"];

                    if (!ProductosNoAcys.Contains(Id_Prd))
                        dt_Resto.ImportRow(Ar_dr[0]);

                    dt_Resto.AcceptChanges();

                    Ar_dr[0].Delete();
                    dt.AcceptChanges();
                }

                CalcularTotales();
            }
        }

        private void Delete(GridItem item, GridCommandEventArgs e)
        {
            DataRow[] Ar_dr;
            GridItem gi = item;

            int Id_Prd = Convert.ToInt32(((Label)gi.FindControl("lblProd")).Text);
            int Cantasignado = gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"] == DBNull.Value ? 0 : Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"]);
            if (Request.QueryString["IdVI"] != null && Cantasignado > 0)
            {
                Alerta("No es posible eliminar el producto captado");
                e.Canceled = true;
                return;
            }
            else
            {

                al.Add(Id_Prd);
                Ar_dr = dt.Select("Id_Prd='" + Id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    var ProductosNoAcys = (List<Int32>)Session["ProductosNoAcys"];
                    if (!ProductosNoAcys.Contains(Id_Prd))
                        dt_Resto.ImportRow(Ar_dr[0]);

                    dt_Resto.AcceptChanges();

                    Ar_dr[0].Delete();
                    dt.AcceptChanges();
                }

                CalcularTotales();
            }
        }

        private void PerformInsert(GridCommandEventArgs e)
        {
            try
            {
                DataRow[] Ar_dr;
                GridItem gi = e.Item;

                if (((RadNumericTextBox)gi.FindControl("txtProd")).Value == 0 ||
                    ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "")
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                if (int.Parse(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text) == 0)
                {
                    Alerta("La cantidad debe ser mayor a 0");
                    e.Canceled = true;
                    return;

                }

                bool modificar = ((CheckBox)gi.FindControl("chkModTemp")).Checked;
                if (modificar)
                {
                    if (((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text == "")
                    {
                        Alerta("Todos los campos son requeridos");
                        e.Canceled = true;
                        return;
                    }
                }

                int Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtProd")).Value);
                string Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                double? Prd_Precio = ((RadNumericTextBox)gi.FindControl("txtPrecio")).Value;
                double? Prd_PrecioAcys = ((RadNumericTextBox)gi.FindControl("txtPrecioAcys")).Value;
                int Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                double Mes1 = Convert.ToDouble(((Label)gi.FindControl("Labelmes12")).Text.Replace("&nbsp;", "0"));
                double Mes2 = Convert.ToDouble(((Label)gi.FindControl("Labelmes22")).Text.Replace("&nbsp;", "0"));
                double Mes3 = Convert.ToDouble(((Label)gi.FindControl("Labelmes32")).Text.Replace("&nbsp;", "0"));
                string Prd_Presentacion = ((Label)gi.FindControl("LabelPresent2")).Text;
                string Prd_Unidad = ((Label)gi.FindControl("LabelUnidad2")).Text;
                string Acs_Doc = ((RadComboBox)gi.FindControl("cmbDocumento")).Text;
                int Ped_Asignar = 0;// Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad2")).Value.Value);

                double? Prd_Importe = Prd_Precio * Prd_Cantidad;

                string dia = ((RadComboBox)gi.FindControl("cmbDia")).SelectedValue;
                string diaStr = ((RadComboBox)gi.FindControl("cmbDia")).SelectedItem.Text;
                int frecuencia = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Value);

                Ar_dr = dt.Select("Id_prd='" + Id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    Alerta("Producto ya capturado");
                    e.Canceled = true;
                }
                else
                {
                    int verificador = -1;
                    CN_CapPedidoVtaInst cn_vi = new CN_CapPedidoVtaInst();
                    PedidoVtaInst pvi = new PedidoVtaInst();
                    pvi.Id_Emp = session.Id_Emp;
                    pvi.Id_Cd = session.Id_Cd_Ver;
                    pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                    pvi.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                    cn_vi.ConsultarPedidoExistente(pvi, Id_Prd, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        Alerta("El producto " + Id_Prd + " ya ha sido captado por otro usuario ");
                        e.Canceled = true;
                        return;
                    }

                    dt.Rows.Add(new object[] { 
                                -1,
                                Id_Prd, 
                                Prd_Descripcion, 
                                Prd_Presentacion,
                                Prd_Unidad,
                                Mes1,
                                Mes2,
                                Mes3,
                                Prd_Cantidad,
                                Prd_Precio,
                                Prd_PrecioAcys,
                                Prd_Importe,
                                Acs_Doc,
                                rdFechaF.SelectedDate,
                                modificar,
                                dia,
                                diaStr,
                                frecuencia,
                                0,
                                Ped_Asignar
                                 });

                    CalcularTotales();

                    al.Remove(Id_Prd);

                    //Edsg 
                    var ProductosNoAcys = (List<Int32>)Session["ProductosNoAcys"];
                    ProductosNoAcys.Add(Id_Prd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Update(GridCommandEventArgs e)
        {
            try
            {
                DataRow[] Ar_dr;
                GridItem gi = e.Item;
                int? idTG = null;
                int idTGi = 0;
                idTG = (int?)(e.Item as GridDataItem).GetDataKeyValue("Id_TG");

                if (idTG != null)
                {
                    idTGi = idTG.Value;
                }

                if (int.Parse(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text) == 0 && idTGi == 0)
                {
                    Alerta("La cantidad debe ser mayor a 0");
                    e.Canceled = true;
                    return;

                }

                if ((((RadNumericTextBox)gi.FindControl("txtProd")).Value == 0 ||
                    ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "") && idTGi == 0)
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                bool modificar = ((CheckBox)gi.FindControl("chkModTemp")).Checked;
                if (modificar)
                {
                    if (((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text == "")
                    {
                        Alerta("Todos los campos son requeridos");
                        e.Canceled = true;
                        return;
                    }
                }

                int Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtProd")).Value);
                string Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                double Prd_Precio = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text);
                int Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                double Mes1 = Convert.ToDouble(((Label)gi.FindControl("Labelmes12")).Text.Replace("&nbsp;", "0"));
                double Mes2 = Convert.ToDouble(((Label)gi.FindControl("Labelmes22")).Text.Replace("&nbsp;", "0"));
                double Mes3 = Convert.ToDouble(((Label)gi.FindControl("Labelmes32")).Text.Replace("&nbsp;", "0"));
                string Prd_Presentacion = ((Label)gi.FindControl("LabelPresent2")).Text;
                string Prd_Unidad = ((Label)gi.FindControl("LabelUnidad2")).Text;
                string Acs_Doc = ((RadComboBox)gi.FindControl("cmbDocumento")).Text;

                double Prd_Importe = Prd_Precio * Prd_Cantidad;

                string dia = ((RadComboBox)gi.FindControl("cmbDia")).SelectedValue;
                string diaStr = ((RadComboBox)gi.FindControl("cmbDia")).SelectedItem.Text;
                int frecuencia = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Value);
                int Ped_Asignar = ((RadNumericTextBox)gi.FindControl("txtCantidad2")).Value.HasValue ? Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad2")).Value.Value) : 0;

                int verificador = -1;
                CN_CapPedidoVtaInst cn_vi = new CN_CapPedidoVtaInst();
                PedidoVtaInst pvi = new PedidoVtaInst();
                pvi.Id_Emp = session.Id_Emp;
                pvi.Id_Cd = session.Id_Cd_Ver;
                pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                pvi.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                cn_vi.ConsultarPedidoExistente(pvi, Id_Prd, session.Emp_Cnx, ref verificador);

                if (verificador == 1 && HF_ID.Value == "")
                {
                    Alerta("El producto " + Id_Prd + " ya ha sido captado por otro usuario ");
                    e.Canceled = true;
                    return;
                }

                Ar_dr = dt.Select("Id_prd='" + Id_Prd + "'");

                if (Ar_dr.Length > 0)
                {
                    if (Request.QueryString["IdVI"] != null)
                    {
                        int asignado = Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"] == DBNull.Value ? 0 : gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"]);
                        if (asignado > Prd_Cantidad)
                        {
                            Alerta("La cantidad del producto no puede ser menor a la cantidad asignada.</br>Cantidad asignada: <b>" + asignado + "</b>");
                            e.Canceled = true;
                            return;
                        }
                    }

                    if (Convert.ToInt32(Ar_dr[0]["Prd_RemFact"]) > Prd_Cantidad)
                    {
                        Alerta("La cantidad del producto no puede ser menor a la cantidad remisionada y/o facturada.</br>Cantidad remisionada y/o facturada: <b>" + Ar_dr[0]["Prd_RemFact"] + "</b>");
                        e.Canceled = true;
                        return;
                    }

                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Prd"] = Id_Prd;
                    Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;
                    Ar_dr[0]["Prd_Presentacion"] = Prd_Presentacion;
                    Ar_dr[0]["Prd_Unidad"] = Prd_Unidad;
                    Ar_dr[0]["Mes1"] = Mes1;
                    Ar_dr[0]["Mes2"] = Mes2;
                    Ar_dr[0]["Mes3"] = Mes3;
                    Ar_dr[0]["Prd_Cantidad"] = Prd_Cantidad;
                    Ar_dr[0]["Prd_Precio"] = Prd_Precio;
                    Ar_dr[0]["Prd_Importe"] = Prd_Importe;
                    Ar_dr[0]["Acs_Doc"] = Acs_Doc;
                    //Ar_dr[0]["Acs_FechaF"] = rdFechaF.SelectedDate;
                    Ar_dr[0]["Mod"] = modificar;
                    Ar_dr[0]["Acs_Dia"] = dia;
                    Ar_dr[0]["Acs_DiaStr"] = diaStr;
                    Ar_dr[0]["Acs_Frecuencia"] = frecuencia;
                    Ar_dr[0]["Ped_Asignar"] = Ped_Asignar;

                    Ar_dr[0].AcceptChanges();

                    CalcularTotales();
                }
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
                double subtotal = 0;

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    subtotal += Convert.ToDouble(dt.Rows[x]["Prd_Importe"]);


                }

                double iva = subtotal * iva_cd / 100;
                double total = subtotal + iva;

                txtSubtotal.Value = subtotal;
                txtIva.Value = iva;
                txtTotal.Value = total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = false;
                        break;
                }
                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }
        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
            bool checkHeader = true;
            foreach (GridDataItem dataItem in this.rg1.MasterTableView.Items)
            {
                if (!(dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    checkHeader = false;
                    break;
                }
            }
            GridHeaderItem headerItem = rg1.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
        }


        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in rg1.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }





        #endregion
        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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
        private void RadConfirm(string mensaje)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        #endregion
    }
}
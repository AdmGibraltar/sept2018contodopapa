using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Configuration;
using System.IO;
using System.Net;

using Telerik.Web.UI;

using CapaEntidad;
using CapaNegocios;
using System.Collections;

namespace SIANWEB
{
    public partial class CapNotaCargo : System.Web.UI.Page
    {
        #region Variables
        public bool HabilitarGuardar
        {
            get
            {
                //DEVUELVE SI SE PUEDE O NO GUARDAR
                return RadToolBar1.Items[1].Visible;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public string FechaEnable
        {
            get
            {
                return Convert.ToInt32(HabilitarGuardar).ToString();// txtFecha.Enabled;
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

        //ADENDA
        public List<AdendaDet> ListCab
        {
            get
            {
                return Session["CabeceraCargo" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["CabeceraCargo" + Session.SessionID] = value;
            }
        }
        public List<AdendaDet> ListDet
        {
            get
            {
                return Session["DetalleCargo" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["DetalleCargo" + Session.SessionID] = value;
            }
        }

        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }

        //Variable de lista de productos para el combo del grid Editable
        private List<Producto> _listaProductos;
        public List<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; }
        }

        //Variable de lista de territorios para el combo del grid Editable
        private List<Territorios> _listaTerritorios;
        public List<Territorios> ListaTerritorios
        {
            get { return _listaTerritorios; }
            set { _listaTerritorios = value; }
        }

        //Variable de lista de representantes para el combo del grid Editable
        private List<Representantes> _listaRepresentantes;
        public List<Representantes> ListaRepresentantes
        {
            get { return _listaRepresentantes; }
            set { _listaRepresentantes = value; }
        }

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        //ADENDA
        public DataTable ListaProductosNotaCargo
        {
            get
            {
                return (Session["ListaProductosNotaCargo" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosNotaCargo" + Session.SessionID] = value;
            }
        }

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    CerrarVentana();
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        int Id_Nca = Convert.ToInt32(Page.Request.QueryString["Id_Nca"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        string Id_NcaSerie = Page.Request.QueryString["Id_NcaSerie"].ToString();
                        string notaModificable = Page.Request.QueryString["notaModificable"].ToString();
                        _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                        _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                        _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                        _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;

                        this.Inicializar(Id_Emp, Id_Cd, Id_Nca, Id_NcaSerie,notaModificable);

                        if (!((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(formulario.Controls);
                            deshabilitarcontroles(formularioTotales.Controls);
                            txtIva2.Enabled = false;

                            HabilitarColumnas(true);
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgNotaCargoDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgNotaCargoDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgNotaCargoDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                Producto prd = new Producto();
                RadNumericTextBox txtProducto = (RadNumericTextBox)sender;
                //ADENDAS
                if (ListaProductosNotaCargo.Select("Id_Prd='" + txtProducto.Value.ToString() + "'").Length > 0)
                {
                    AlertaFocus("El producto ya ha sido agregado a la nota de cargo", txtProducto.ClientID);
                    txtProducto.Text = "";
                    ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = "";
                    ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtNca_Importe") as RadNumericTextBox).Text = "";

                    return;
                }
                RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)txtProducto.Parent.FindControl("txtTerritorioPartida");
                try
                {
                    new CN_CatProducto().ConsultaProducto(ref prd, session.Emp_Cnx, session.Id_Emp, session.Id_Cd, Convert.ToInt32(txtProducto.Value.HasValue ? (sender as RadNumericTextBox).Value : -1));
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtProducto.ClientID);
                }
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = prd.Prd_Descripcion;
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtNca_Importe") as RadNumericTextBox).Focus();
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
                        rgNotaCargoDet.Rebind();
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
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = hiddenId.Value == string.Empty ? "CapNotaCargo_insert_error" : "CapNotaCargo_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rgNotaCargoDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgNotaCargoDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCargoDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgNotaCargoDet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCargoDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);

                    //Llenar Grid
                    rgNotaCargoDet.DataSource = this.ListaProductosNotaCargo;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCargoDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    string txtTerritorioPartida = ((RadNumericTextBox)editItem.FindControl("txtTerritorioPartida")).ClientID.ToString();
                    string lbltxtTerritorioPartida = ((Label)editItem.FindControl("lbltxtTerritorioPartida")).ClientID.ToString();
                    string txtRepresentantePartida = ((RadNumericTextBox)editItem.FindControl("txtRepresentantePartida")).ClientID.ToString();
                    string lblTxtRepresentantePartida = ((Label)editItem.FindControl("lblTxtRepresentantePartida")).ClientID.ToString();
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    string txtNca_Importe = ((RadNumericTextBox)editItem.FindControl("txtNca_Importe")).ClientID.ToString();
                    string lbl_txtNca_Importe = ((Label)editItem.FindControl("lbl_txtNca_Importe")).ClientID.ToString();

                    //Llenar combo de territorios
                    RadComboBox comboTerritorioPartidaItem = (RadComboBox)editItem.FindControl("cmbTerritorioPartida");
                    List<Territorios> listaTerritorios = new List<Territorios>();
                    new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1), sesion, ref listaTerritorios);

                    comboTerritorioPartidaItem.DataTextField = "Descripcion";
                    comboTerritorioPartidaItem.DataValueField = "Id_Ter";
                    comboTerritorioPartidaItem.DataSource = listaTerritorios;
                    comboTerritorioPartidaItem.DataBind();

                    string jsControles = string.Concat(
                        "txtTerritorioPartidaClientID='", txtTerritorioPartida, "';"
                        , "lbltxtTerritorioPartidaClientID='", lbltxtTerritorioPartida, "';"
                        , "txtRepresentantePartidaClientID='", txtRepresentantePartida, "';"
                        , "lblTxtRepresentantePartidaClientID='", lblTxtRepresentantePartida, "';"
                        , "txtId_PrdClientID='", txtId_Prd, "';"
                        , "lbl_cmbProductoClientID='", lbl_cmbProducto, "';"
                        , "txtNca_ImporteClientID='", txtNca_Importe, "';"
                        , "lbl_txtNca_ImporteClientID='", lbl_txtNca_Importe, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        int Id_Prd = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"]);
                        foreach (DataRow ncd in this.ListaProductosNotaCargo.Rows)
                        {
                            if (Convert.ToInt32(ncd["Id_Prd"]) == Id_Prd)
                            {
                                comboTerritorioPartidaItem.SelectedIndex = comboTerritorioPartidaItem.FindItemIndexByValue(ncd["Id_Ter"].ToString());
                                ((RadTextBox)editItem.FindControl("txtRepresentantePartidaStr")).Text = ncd["Rik_Nombre"].ToString();
                            }
                        }
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");
                        updatebtn.Attributes.Add("onclick", jsControles);
                        ((RadNumericTextBox)editItem.FindControl("txtNca_Importe")).Focus();
                    }
                }
                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_TerN"].FindControl("txtTerritorioPartida");
                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form["Id_PrdN"].FindControl("txtId_Prd");
                    }
                    dataField.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCargoDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                NotaCargoDet notaCargoDet = new NotaCargoDet();

                notaCargoDet.Id_Emp = sesion.Id_Emp;
                notaCargoDet.Id_Cd = sesion.Id_Cd_Ver;
                notaCargoDet.Id_Nca = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                notaCargoDet.Id_NcaDet = 0;
                RadComboBox comboTerritorio = (insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox);
                notaCargoDet.Id_Ter = Convert.ToInt32(comboTerritorio.SelectedValue);
                notaCargoDet.Ter_Nombre = (comboTerritorio.SelectedItem.FindControl("lblTerritorioPartidaEdit") as Label).Text;
                notaCargoDet.Id_Rik = Convert.ToInt32((insertedItem["Id_Ter"].FindControl("txtRepresentantePartida") as RadNumericTextBox).Text);
                notaCargoDet.Rik_Nombre = (insertedItem["Id_Rik"].FindControl("txtRepresentantePartidaStr") as RadTextBox).Text;
                RadNumericTextBox comboProducto = (insertedItem.FindControl("txtId_Prd") as RadNumericTextBox);
                RadTextBox ProductoNombre = (insertedItem.FindControl("txtProductoNombre") as RadTextBox);
                notaCargoDet.Id_Prd = Convert.ToInt32(comboProducto.Value);
                notaCargoDet.Prd_Nombre = ProductoNombre.Text;
                notaCargoDet.Nca_Importe = Convert.ToDouble((insertedItem["Nca_Importe"].FindControl("txtNca_Importe") as RadNumericTextBox).Text);

                //agregar producto de nota de cargo a la lista
                //this.ListaProductosNotaCargo_AgregarProducto(notaCargoDet);

                //ADENDAS
                if (ListaProductosNotaCargo.Select("Id_Prd='" + notaCargoDet.Id_Prd.ToString() + "'").Length > 0)
                {
                    Alerta("El producto ya ha sido agregado a la nota de cargo");
                    e.Canceled = true;
                    return;
                }
                ArrayList al = new ArrayList();

                al.Add(notaCargoDet.Id_Emp);
                al.Add(notaCargoDet.Id_Cd);
                al.Add(notaCargoDet.Id_Nca);
                al.Add(notaCargoDet.Id_NcaDet);
                al.Add(notaCargoDet.Id_Ter);
                al.Add(notaCargoDet.Ter_Nombre);
                al.Add(notaCargoDet.Id_Rik);
                al.Add(notaCargoDet.Rik_Nombre);
                al.Add(notaCargoDet.Id_Prd);
                al.Add(notaCargoDet.Prd_Nombre);
                al.Add(notaCargoDet.Nca_Importe);
                TextBox Txtadenda = new TextBox();
                bool falta_adenda = false;
                string valor_adenda = "";
                string adenda_faltante = "";
                foreach (AdendaDet det in ListDet)
                {
                    Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                    valor_adenda = Txtadenda.Text.Replace("'", "");
                    if (valor_adenda == "" && det.Requerido)
                    {
                        adenda_faltante = det.Campo;
                        falta_adenda = true;
                        break;
                    }
                    else
                    {
                        al.Add(valor_adenda);
                    }
                }
                if (falta_adenda)
                {
                    AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                    e.Canceled = true;
                }
                else
                {
                    ListaProductosNotaCargo.Rows.Add(al.ToArray());
                    this.CalcularTotales();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCargoDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                NotaCargoDet notaCargoDet = new NotaCargoDet();

                notaCargoDet.Id_Emp = sesion.Id_Emp;
                notaCargoDet.Id_Cd = sesion.Id_Cd_Ver;
                notaCargoDet.Id_Nca = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                notaCargoDet.Id_NcaDet = 0;
                RadComboBox comboTerritorio = (insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox);
                notaCargoDet.Id_Ter = Convert.ToInt32(comboTerritorio.SelectedValue);
                notaCargoDet.Ter_Nombre = (comboTerritorio.SelectedItem.FindControl("lblTerritorioPartidaEdit") as Label).Text;
                notaCargoDet.Id_Rik = Convert.ToInt32((insertedItem["Id_Ter"].FindControl("txtRepresentantePartida") as RadNumericTextBox).Text);
                notaCargoDet.Rik_Nombre = (insertedItem["Id_Rik"].FindControl("txtRepresentantePartidaStr") as RadTextBox).Text;
                RadNumericTextBox comboProducto = (insertedItem.FindControl("txtId_Prd") as RadNumericTextBox);
                RadTextBox ProductoNombre = (insertedItem.FindControl("txtProductoNombre") as RadTextBox);
                notaCargoDet.Id_Prd = Convert.ToInt32(comboProducto.Value);
                notaCargoDet.Prd_Nombre = ProductoNombre.Text;
                notaCargoDet.Nca_Importe = Convert.ToDouble((insertedItem["Nca_Importe"].FindControl("txtNca_Importe") as RadNumericTextBox).Text);
                string id_producto = (insertedItem["Id_Prd"].FindControl("lbl_productoold") as Label).Text;

                //actualizar producto de nota de cargo a la lista
                //this.ListaProductosNotaCargo_ModificarProducto(notaCargoDet);

                //ADENDA
                DataRow[] Ar_dr;

                Ar_dr = ListaProductosNotaCargo.Select("Id_Prd='" + id_producto + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Emp"] = notaCargoDet.Id_Emp;
                    Ar_dr[0]["Id_Cd"] = notaCargoDet.Id_Cd;
                    Ar_dr[0]["Id_Nca"] = notaCargoDet.Id_Nca;
                    Ar_dr[0]["Id_NcaDet"] = notaCargoDet.Id_NcaDet;
                    Ar_dr[0]["Id_Ter"] = notaCargoDet.Id_Ter;
                    Ar_dr[0]["Ter_Nombre"] = notaCargoDet.Ter_Nombre;
                    Ar_dr[0]["Id_Rik"] = notaCargoDet.Id_Rik;
                    Ar_dr[0]["Rik_Nombre"] = notaCargoDet.Rik_Nombre;
                    Ar_dr[0]["Id_Prd"] = notaCargoDet.Id_Prd;
                    Ar_dr[0]["Prd_Nombre"] = notaCargoDet.Prd_Nombre;
                    Ar_dr[0]["Nca_Importe"] = notaCargoDet.Nca_Importe;

                    TextBox Txtadenda = new TextBox();
                    bool falta_adenda = false;
                    string valor_adenda = "";
                    string adenda_faltante = "";
                    foreach (AdendaDet det in ListDet)
                    {
                        Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                        valor_adenda = Txtadenda.Text;
                        if (valor_adenda == "" && det.Requerido)
                        {
                            adenda_faltante = det.Campo;
                            falta_adenda = true;
                            break;
                        }
                        else
                            Ar_dr[0][det.Id_AdeDet.ToString()] = valor_adenda;
                    }
                    if (falta_adenda)
                    {
                        AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                        e.Canceled = true;
                    }
                    else
                        Ar_dr[0].AcceptChanges();
                }
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCargoDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                //eliminar producto de nota de cargo a la lista
                this.ListaProductosNotaCargo_EliminarProducto(id_Prd);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgNotaCargoDet_delete_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void cmbMov_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Limpiar controles totales
            //txtSubtotal2.Text = "0";
            //txtIva2.Text = "0";
            //txtTotal2.Text = "0";
            this.ValidarHabilitacionGrid();
            /*
             * Si el tipo de movimiento afecta a ventas TMCAFEVTA=S no se puede modificar el IVA ni el subtotal. 
             */
            //if (e.Value != string.Empty && e.Value != "-1")
            //{
            //    RadComboBox combo = (RadComboBox)sender;
            //    this.ValidarHabilitacionTotales(ref combo);
            //}

            /*
             * Cuando el movimiento es igual 58 el sistema despliega el campo Banco (Requerido).
             * Cuando el movimiento es igual 56 se despliega el campo de cuenta contable (Requerido).
             */
            if (e.Value == "58")
            {
                this.HD_PanelVisible.Value = "b"; //banco
                txtBanco.Focus();
                this.panelBanco.Style.Add("display", "block");
                this.panelCuentaContable.Style.Add("display", "none");
            }
            else
            {
                if (e.Value == "56")
                {
                    this.HD_PanelVisible.Value = "c"; //cuenta contable
                    txtCuentaContable.Focus();
                    this.panelBanco.Style.Add("display", "none");
                    this.panelCuentaContable.Style.Add("display", "block");
                }
                else
                {
                    this.HD_PanelVisible.Value = ""; //cuenta contable
                    this.panelBanco.Style.Add("display", "none");
                    this.panelCuentaContable.Style.Add("display", "none");
                }
            }

            CalcularTotales();
        }
        protected void cmbConsFacEle_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                int cfe = Convert.ToInt32(cmbConsFacEle.SelectedValue);
                this.ObtenerConsecutivoFacElectronica(cfe);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            this.ValidarHabilitacionGrid();
            this.ConsultarDatosCliente(txtCliente.Value.HasValue ? txtCliente.Value.Value.ToString() : "-1", false);
            
            CargarFormaPago();
        }
        protected void cmbTerritorio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (e.Value != string.Empty && e.Value != "-1")
                {
                    RadComboBoxItem item = ((RadComboBox)sender).SelectedItem;
                    Label lbl_Id_Rik = (Label)item.FindControl("lbl_Id_Rik");
                    Label lbl_Rik_Nombre = (Label)item.FindControl("lbl_Rik_Nombre");
                    if (lbl_Id_Rik.Text != "-1" && lbl_Id_Rik.Text != string.Empty)
                    {
                        txtRepresentante.Text = lbl_Id_Rik.Text;
                        txtRepresentanteStr.Text = lbl_Rik_Nombre.Text;
                    }
                    txtReferencia.Focus();
                }
                else
                {
                    txtRepresentante.Text = string.Empty;
                    txtRepresentanteStr.Text = string.Empty;
                }
                this.ValidarHabilitacionGrid();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbCliente_IndexChanging_error"));
            }
        }
        protected void cmbTerritorioPartida_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            { //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)((RadComboBox)sender).Parent;

                if (e.Value != string.Empty && e.Value != "-1")
                {
                    RadComboBoxItem item = ((RadComboBox)sender).SelectedItem;
                    Label lbl_Id_Rik = (Label)item.FindControl("lbl_Id_Rik_Partida");
                    Label lbl_Rik_Nombre = (Label)item.FindControl("lbl_Rik_Nombre_Partida");
                    if (lbl_Id_Rik.Text != "-1" && lbl_Id_Rik.Text != string.Empty)
                    {
                        ((RadNumericTextBox)tabla.FindControl("txtRepresentantePartida")).Text = ((Label)item.FindControl("lbl_Id_Rik_Partida")).Text;
                        ((RadTextBox)tabla.FindControl("txtRepresentantePartidaStr")).Text = ((Label)item.FindControl("lbl_Rik_Nombre_Partida")).Text;
                        ((RadNumericTextBox)tabla.FindControl("txtId_Prd")).Focus();
                    }
                }
                else
                {
                    ((RadNumericTextBox)tabla.FindControl("txtRepresentantePartida")).Text = string.Empty;
                    ((RadTextBox)tabla.FindControl("txtRepresentantePartidaStr")).Text = string.Empty;
                }
                //this.ValidarHabilitacionGrid();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbCliente_IndexChanging_error"));
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                //if (e.Item.Value == "-1")
                //{
                //    e.Item.FindControl("liComprasLocales").Controls.Clear();
                //}
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "cmbProductosLista_ItemsDataBound"));
            }
        }
        protected void rgAdendaNotaCargo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAdendaNotaCargo.DataSource = ListCab;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCargoDet_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem editItem = (GridDataItem)e.Item;
                    TextBox txt;
                    if (ListDet != null)
                        foreach (AdendaDet det in ListDet)
                        {
                            if (editItem[det.Id_AdeDet.ToString()].Controls.Count > 0)
                            {
                                txt = editItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                txt.Attributes.Add("onkeypress", "return SoloAlfanumerico(this)");
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkDesgloceIva_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                IVA.Visible = chkDesgloceIva.Checked;

                if (chkDesgloceIva.Checked)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    HD_IVAfacturacion.Value = cd.Cd_IvaPedidosFacturacion.ToString();
                }
                else
                {
                    HD_IVAfacturacion.Value = "0";
                    txtIva2.Text = "0";
                }
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCargoDet_DataBound(object sender, EventArgs e)
        {
            ValidarHabilitacionTotales(ref cmbMov);
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
        protected void RAM1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 155);
                        
                        //DETALLE
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        
                        //GENERAL
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;

                        //ADDENDA
                        rpvAdendaNCargo.Height = altura;
                        RadSplitter3.Height = altura;
                        RadPane3.Height = altura;
                        RadPane3.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadPageViewDetalles_Load(object sender, EventArgs e)
        {
            object a = RadPageViewDetalles.Width;
        }
        #endregion
        #region Funciones
        private void CargarFormaPago()
        {
            try
            {
                cmbFormaPago.Items.Clear();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, txtCliente.Value.HasValue ? (int)txtCliente.Value.Value : -1, sesion.Emp_Cnx, "spCatClienteFormaPago_Combo", ref cmbFormaPago);
                this.cmbFormaPago.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                if (cmbFormaPago.Items.Count > 1)
                {
                    cmbFormaPago.SelectedIndex = 1;
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
        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref sender);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InicializarTablaProductos()
        {
            ListaProductosNotaCargo = new DataTable();
            ListaProductosNotaCargo.Columns.Add("Id_Emp");
            ListaProductosNotaCargo.Columns.Add("Id_Cd");
            ListaProductosNotaCargo.Columns.Add("Id_Nca");
            ListaProductosNotaCargo.Columns.Add("Id_NcaDet");
            ListaProductosNotaCargo.Columns.Add("Id_Ter");
            ListaProductosNotaCargo.Columns.Add("Ter_Nombre");
            ListaProductosNotaCargo.Columns.Add("Id_Rik");
            ListaProductosNotaCargo.Columns.Add("Rik_Nombre");
            ListaProductosNotaCargo.Columns.Add("Id_Prd");
            ListaProductosNotaCargo.Columns.Add("Prd_Nombre");
            ListaProductosNotaCargo.Columns.Add("Nca_Importe", typeof(System.Double));
        }
        private void ValidarHabilitacionGrid()
        {
            InicializarTablaProductos();
            rgNotaCargoDet.Rebind();
            CalcularTotales();
        }
        private void ValidarHabilitacionTotales(ref RadComboBox combo)
        {
            /*
             * Si el tipo de movimiento afecta a ventas TMCAFEVTA=S no se puede modificar el IVA ni el subtotal. 
             */
            RadComboBoxItem item = ((RadComboBox)combo).SelectedItem;
            if (item.Value != string.Empty && item.Value != "-1")
            {
                Label lbl_AfeVta = (Label)item.FindControl("lbl_AfeVta");
                if (lbl_AfeVta.Text.ToUpper() == "TRUE")
                {
                    this.HabilitaTotales(false);
                }
                else
                {
                    this.HabilitaTotales(true);
                }

            }
        }
        private void HabilitaTotales(bool habilitar)
        {
            this.txtSubtotal2.Enabled = habilitar;
            this.txtIva2.Enabled = habilitar;
            HabilitarColumnas(habilitar);
        }
        private void HabilitarColumnas(bool habilitar)
        {
            GridCommandItem cmdItem = (GridCommandItem)rgNotaCargoDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            cmdItem.FindControl("AddNewRecordButton").Parent.Visible = !habilitar;
            try
            {
                rgNotaCargoDet.Columns.FindByUniqueName("EditCommandColumn").Display = !habilitar;
            }
            catch
            { }
            try
            {
                rgNotaCargoDet.Columns.FindByUniqueName("DeleteColumn").Display = !habilitar;
            }
            catch
            {
            }
        }
        private void ObtenerConsecutivoFacElectronica(int id_Cfe)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int consecutivo = 0;

                txtFolio.Text = string.Empty;
                if (cmbConsFacEle.SelectedValue != "-1")
                {
                    new CN__Comun().ConsultaFactura_ConsecutivoFacElectronica(
                        sesion.Id_Emp
                        , sesion.Id_Cd_Ver
                        , id_Cfe
                        , 2 // 1 = factura, 2 = nota de cargo, 3 = nota de credito
                        , ref consecutivo
                        , sesion.Emp_Cnx);
                    txtFolio.Text = consecutivo.ToString();
                }
            }
            catch (Exception ex)
            {
                txtFolio.Text = string.Empty;
                cmbConsFacEle.SelectedIndex = 0;
                cmbConsFacEle.SelectedValue = "-1";
                cmbConsFacEle.Text = "-- Seleccionar --";

                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbConsFacEle_ObtenerConsFacElectFallo"));
            }
        }
        private void ConsultarDatosCliente(string idCliente, bool modificar)
        {
            txtClienteNombre.Text = "";
            txtTerritorio.Text = "";
            cmbTerritorio.Items.Clear();
            cmbTerritorio.Text = "";
            txtRepresentante.Text = "";
            txtRepresentanteStr.Text = "";
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (idCliente != string.Empty && idCliente != "-1")
            { //Consultar clientes
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32(idCliente);
                cliente.Id_Rik = sesion.Id_Rik;
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    txtCliente.Text = "";
                    return;
                }
                txtUDigitos.Text = cliente.Cte_UDigitos;
                txtClienteNombre.Text = cliente.Cte_NomComercial;
                this.chkDesgloceIva.Checked = cliente.Cte_DesgIva;
                if (cliente.Cte_SerieNCa > 0)
                {
                    this.cmbConsFacEle.SelectedIndex = this.cmbConsFacEle.FindItemIndexByValue(cliente.Cte_SerieNCa.ToString());
                    if (!modificar) //Trae el cosecutivo si no es una modificación de documento                   
                        this.ObtenerConsecutivoFacElectronica(cliente.Cte_SerieNCa);
                }
                //Consultar territorios relacionados con el cliente
                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(idCliente), sesion, ref listaTerritorios);

                this.CargarComboTerritorios(listaTerritorios);

                //ADENDAS
                if (!modificar)
                {
                    List<AdendaDet> listCabT = new List<AdendaDet>();
                    List<AdendaDet> listDetT = new List<AdendaDet>();
                    List<AdendaDet> listCabR = new List<AdendaDet>();
                    List<AdendaDet> listCab = new List<AdendaDet>();
                    List<AdendaDet> listDet = new List<AdendaDet>();
                    for (int i = rgNotaCargoDet.Columns.Count; i > 9; i--)
                        rgNotaCargoDet.Columns.RemoveAt(rgNotaCargoDet.Columns.Count - 1);

                    new CN_CatCliente().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(idCliente), "3,4", ref listCabT, ref listDetT, ref listCabR, sesion.Emp_Cnx);

                    if (listCabT.Count > 0)
                        RadTabStrip1.Tabs[1].Visible = true;
                    else
                        RadTabStrip1.Tabs[1].Visible = false;

                    ListCab = listCabT;
                    rgAdendaNotaCargo.Rebind();
                    InicializarTablaProductos();
                    GridBoundColumn boundColumn1;
                    foreach (AdendaDet ad in listDetT)
                    {
                        boundColumn1 = new GridBoundColumn();
                        rgNotaCargoDet.MasterTableView.Columns.Add(boundColumn1);
                        boundColumn1.DataField = ad.Id_AdeDet.ToString();
                        boundColumn1.UniqueName = ad.Id_AdeDet.ToString();
                        boundColumn1.HeaderText = ad.Campo;
                        boundColumn1.HeaderStyle.Width = Unit.Pixel(150);
                        boundColumn1.MaxLength = ad.Longitud;
                        ListaProductosNotaCargo.Columns.Add(ad.Id_AdeDet.ToString());
                    }

                    //CREA BOTON DE EDITAR
                    GridEditCommandColumn commandColumn = new GridEditCommandColumn();
                    rgNotaCargoDet.MasterTableView.Columns.Add(commandColumn);
                    commandColumn.ButtonType = GridButtonColumnType.ImageButton;
                    commandColumn.UniqueName = "EditCommandColumn";
                    commandColumn.EditText = "Editar";
                    commandColumn.CancelText = "Cancelar";
                    commandColumn.InsertText = "Aceptar";
                    commandColumn.UpdateText = "Actualizar";
                    commandColumn.HeaderStyle.Width = Unit.Pixel(90);
                    commandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    commandColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    //CREA BOTON ELIMINAR
                    GridButtonColumn deleteColumn = new GridButtonColumn();
                    rgNotaCargoDet.MasterTableView.Columns.Add(deleteColumn);
                    deleteColumn.ConfirmText = "¿Desea quitar este producto de la lista?";
                    deleteColumn.ConfirmDialogHeight = Unit.Pixel(150);
                    deleteColumn.ConfirmDialogWidth = Unit.Pixel(350);
                    deleteColumn.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                    deleteColumn.ButtonType = GridButtonColumnType.ImageButton;
                    deleteColumn.CommandName = "Delete";
                    deleteColumn.Text = "Eliminar";
                    deleteColumn.UniqueName = "DeleteColumn";
                    deleteColumn.HeaderStyle.Width = Unit.Pixel(50);
                    deleteColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    deleteColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    double ancho = 0;
                    foreach (GridColumn gc in rgNotaCargoDet.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }

                    rgNotaCargoDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgNotaCargoDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    ListDet = listDetT;
                    rgNotaCargoDet.Rebind();
                    txtTerritorio.Focus();
                }
            }
        }
        private void EstablecerDataSourceProductosLista(string filtro)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = new Producto();
                List<Producto> listaProducto = new List<Producto>();
                new CN_CatProducto().ConsultaListaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, string.Empty, ref listaProducto, 1);

                producto = new Producto();
                producto.Id_Prd = -1;
                producto.Prd_Descripcion = "-- Seleccionar --";
                listaProducto.Insert(0, producto);
                this.ListaProductos = listaProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LLenarFormNotaCargo(int Id_Emp, int Id_Cd, int Id_Nca, string Id_NcaSerie)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            NotaCargo notaCargo = new NotaCargo();
            notaCargo.Id_Emp = Id_Emp;
            notaCargo.Id_Cd = Id_Cd;
            notaCargo.Id_Nca = Id_Nca;
            notaCargo.Id_NcaSerie = Id_NcaSerie;
            new CN_CapNotaCargo().ConsultaNotaCargo(ref notaCargo, sesion.Emp_Cnx);

            //Agregar Adendas
            List<AdendaDet> listCabT = new List<AdendaDet>();
            List<AdendaDet> listDetT = new List<AdendaDet>();
            for (int i = rgNotaCargoDet.Columns.Count; i > 9; i--)
                rgNotaCargoDet.Columns.RemoveAt(rgNotaCargoDet.Columns.Count - 1);

            new CN_CapNotaCargo().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Nca,Id_NcaSerie, "3", "4", ref listCabT, ref listDetT, sesion.Emp_Cnx);

            if (listCabT.Count > 0)
            {
                RadTabStrip1.Tabs[1].Visible = true;
                ListCab = listCabT;
                rgAdendaNotaCargo.Rebind();
            }

            GridBoundColumn boundColumn1;
            foreach (AdendaDet ad in listDetT)
            {
                if (!ListaProductosNotaCargo.Columns.Contains(ad.Id_AdeDet.ToString()))
                {
                    boundColumn1 = new GridBoundColumn();
                    rgNotaCargoDet.MasterTableView.Columns.Add(boundColumn1);
                    boundColumn1.DataField = ad.Id_AdeDet.ToString();
                    boundColumn1.UniqueName = ad.Id_AdeDet.ToString();
                    boundColumn1.HeaderText = ad.Campo;
                    boundColumn1.HeaderStyle.Width = Unit.Pixel(150);
                    boundColumn1.MaxLength = ad.Longitud;
                    ListaProductosNotaCargo.Columns.Add(ad.Id_AdeDet.ToString());
                }
            }
            ListDet = listDetT;

            //CREA BOTON DE EDITAR
            GridEditCommandColumn commandColumn = new GridEditCommandColumn();
            rgNotaCargoDet.MasterTableView.Columns.Add(commandColumn);
            commandColumn.ButtonType = GridButtonColumnType.ImageButton;
            commandColumn.UniqueName = "EditCommandColumn";
            commandColumn.EditText = "Editar";
            commandColumn.CancelText = "Cancelar";
            commandColumn.InsertText = "Aceptar";
            commandColumn.UpdateText = "Actualizar";
            commandColumn.HeaderStyle.Width = Unit.Pixel(90);
            commandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            commandColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            //CREA BOTON ELIMINAR
            GridButtonColumn deleteColumn = new GridButtonColumn();
            rgNotaCargoDet.MasterTableView.Columns.Add(deleteColumn);
            deleteColumn.ConfirmText = "¿Desea quitar este producto de la lista?";
            deleteColumn.ConfirmDialogHeight = Unit.Pixel(150);
            deleteColumn.ConfirmDialogWidth = Unit.Pixel(350);
            deleteColumn.ConfirmDialogType = GridConfirmDialogType.RadWindow;
            deleteColumn.ButtonType = GridButtonColumnType.ImageButton;
            deleteColumn.CommandName = "Delete";
            deleteColumn.Text = "Eliminar";
            deleteColumn.UniqueName = "DeleteColumn";
            deleteColumn.HeaderStyle.Width = Unit.Pixel(50);
            deleteColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            deleteColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            double ancho = 0;
            foreach (GridColumn gc in rgNotaCargoDet.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgNotaCargoDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgNotaCargoDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgNotaCargoDet.Rebind();

            cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(notaCargo.Nca_Tipo.ToString());
            txtFolio.Text = notaCargo.Id_Nca.ToString();
            txtFecha.SelectedDate = notaCargo.Nca_Fecha;
            txtFecha.Enabled = false;
            txtMov.Text = notaCargo.Id_Tm.ToString();
            cmbMov.SelectedIndex = cmbMov.FindItemIndexByValue(notaCargo.Id_Tm.ToString());
            if (notaCargo.Id_Ban != null)
            {
                this.panelBanco.Style.Add("display", "block");
                txtBanco.Text = notaCargo.Id_Ban.ToString();
                cmbBanco.SelectedIndex = cmbBanco.FindItemIndexByValue(notaCargo.Id_Ban.ToString());
                this.HD_PanelVisible.Value = "b";
            }
            else
            {
                if (notaCargo.Nca_CtaContable != string.Empty)
                {
                    this.panelCuentaContable.Style.Add("display", "block");
                    txtCuentaContable.Text = notaCargo.Nca_CtaContable;
                    this.HD_PanelVisible.Value = "c";
                }
                else
                    this.HD_PanelVisible.Value = string.Empty;
            }
            txtCliente.Text = notaCargo.Id_Cte.ToString();
            txtClienteNombre.Text = notaCargo.Cte_NomComercial;
            txtCliente.Enabled = false;
            txtClienteNombre.Enabled = false;

            CargarFormaPago();

            //consultar datos del cliente
            this.ConsultarDatosCliente(notaCargo.Id_Cte.ToString(), true);

            txtUDigitos.Text = notaCargo.Nca_UDigitos;
            cmbFormaPago.SelectedIndex = cmbFormaPago.FindItemIndexByValue(notaCargo.Nca_FPago);

            if (notaCargo.Id_Cfe == null) cmbConsFacEle.SelectedIndex = 0; else cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue(notaCargo.Id_Cfe.ToString());

            txtTerritorio.Text = notaCargo.Id_Ter.ToString();
            cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(notaCargo.Id_Ter.ToString());
            txtRepresentante.Text = notaCargo.Id_Rik.ToString();
            txtRepresentanteStr.Text = ((Label)cmbTerritorio.SelectedItem.FindControl("lbl_Rik_Nombre")).Text;
            chkDesgloceIva.Checked = notaCargo.Nca_Desgloce;
            txtReferencia.Text = notaCargo.Nca_Referencia.ToString();
            txtNotas.Text = notaCargo.Nca_Notas;


            CargarEspecial(Id_Nca, Id_NcaSerie, sesion, notaCargo);

            //ADENDA
            ConvertiraDt(notaCargo.ListaNotaCargo);
            // this.ValidarHabilitacionGrid();
            //this.ValidarHabilitacionTotales(ref cmbMov);
            rgNotaCargoDet.Rebind();

            chkDesgloceIva_CheckedChanged(null, null);

            txtSubtotal2.Text = notaCargo.Nca_Subtotal.ToString();

            txtIva2.Text = notaCargo.Nca_Iva.ToString();
            txtTotal2.Text = notaCargo.Nca_Total.ToString();
        }
        private void CargarEspecial(int Id_Nca, string Id_NcaSerie,Sesion sesion, NotaCargo notaCargo)
        {
            //- Especial



            List<NotaCargoDet> listaProdFacturaEspecialFinal = new List<NotaCargoDet>();
            new CN_CapNotaCargo().ConsultaNotaCargoEspecialDetalle(ref listaProdFacturaEspecialFinal
                , sesion.Emp_Cnx
                , sesion.Id_Emp
                , sesion.Id_Cd_Ver
                , Convert.ToInt32(Id_Nca)
                , Id_NcaSerie
                , notaCargo.Id_Cte);

            if (listaProdFacturaEspecialFinal.Count > 0)
            {
                Session["ListaProductosNotaCargoEspecial" + Session.SessionID] = listaProdFacturaEspecialFinal;
                Session["NcargoEspecialGuardada" + Session.SessionID] = 1;
            }
            //-
        }
        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Nca,string Id_NcaSerie, string notaModificable)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Session["NCargoEspecialGuardada" + Session.SessionID] = "0";

            //ADENDA
            Session["ListaProductosNotaCargoEspecial" + Session.SessionID] = null;
            InicializarTablaProductos();
            this.CargarConsFactElectronica();
            this.CargarComboTipoMovimientos();
            this.CargarComboBancos();

            CentroDistribucion cd = new CentroDistribucion();
            new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
            HD_IVAfacturacion.Value = cd.Cd_IvaPedidosFacturacion.ToString();

            //nueva variable para controlar tabla dinamica de productos de nota de cargo
            Session["ListaProductosNotaCargo"] = new List<NotaCargoDet>();

            //establece valores de controles de inicio
            if (Id_Emp > 0 && Id_Cd > 0 && Id_Nca > 0)
            {
                this.panelBanco.Style.Add("display", "none");
                this.panelCuentaContable.Style.Add("display", "none");
                this.LLenarFormNotaCargo(Id_Emp, Id_Cd, Id_Nca, Id_NcaSerie ); //EDICION de factura
                this.hiddenId.Value = Id_Nca.ToString();
                if (notaModificable == "0")
                    RadToolBar1.FindItemByValue("save").Visible = false;
            }
            else //NOTA Nueva
            {
                this.hiddenId.Value = string.Empty;
                this.chkDesgloceIva.Checked = true;
                this.txtFecha.SelectedDate = DateTime.Now;
                this.panelBanco.Style.Add("display", "none");
                this.panelCuentaContable.Style.Add("display", "none");
                if (cmbConsFacEle.Items.Count > 1)
                {
                    cmbConsFacEle.SelectedIndex = 1;
                    cmbConsFacEle_SelectedIndexChanged(null, null);
                }
            }
            this.rgNotaCargoDet.Rebind();
            this.txtFecha.Focus();
            this.HdId_NcaSerie.Value = this.cmbConsFacEle.SelectedItem.Text;
        }
        private void ConvertiraDt(List<NotaCargoDet> listaFacturaDet)
        {
            try
            {
                ArrayList al;
                foreach (NotaCargoDet notaCargoDet in listaFacturaDet)
                {
                    al = new ArrayList();
                    al.Add(notaCargoDet.Id_Emp);
                    al.Add(notaCargoDet.Id_Cd);
                    al.Add(notaCargoDet.Id_Nca);
                    al.Add(notaCargoDet.Id_NcaDet);
                    al.Add(notaCargoDet.Id_Ter);
                    al.Add(notaCargoDet.Ter_Nombre);
                    al.Add(notaCargoDet.Id_Rik);
                    al.Add(notaCargoDet.Rik_Nombre);
                    al.Add(notaCargoDet.Id_Prd);
                    al.Add(notaCargoDet.Prd_Nombre);
                    al.Add(notaCargoDet.Nca_Importe);
                    foreach (AdendaDet ad in ListDet)
                    {
                        if (ad.Id_Prd == notaCargoDet.Id_Prd && ad.Id_Ter == notaCargoDet.Id_Ter)
                            al.Add(ad.Valor);
                    }
                    ListaProductosNotaCargo.Rows.Add(al.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private NotaCargo LlenarObjetoNotaCargo()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            NotaCargo notaCargo = new NotaCargo();
            notaCargo.Id_Emp = sesion.Id_Emp;
            notaCargo.Id_Cd = sesion.Id_Cd_Ver;
            notaCargo.Id_Nca = Convert.ToInt32(txtFolio.Text);

            if (cmbConsFacEle.SelectedValue == "-1") notaCargo.Id_Cfe = null; else notaCargo.Id_Cfe = Convert.ToInt32(cmbConsFacEle.SelectedValue);

            if (cmbConsFacEle.SelectedValue == "-1") notaCargo.Id_NcaSerie = null; else notaCargo.Id_NcaSerie = string.Concat(cmbConsFacEle.Text);

            notaCargo.Id_Reg = 0;
            notaCargo.Id_Tm = Convert.ToInt32(cmbMov.SelectedValue);
            if (this.HD_PanelVisible.Value == "b")
            {
                notaCargo.Id_Ban = Convert.ToInt32(cmbBanco.SelectedValue);
                notaCargo.Nca_CtaContable = string.Empty;
            }
            else
                if (this.HD_PanelVisible.Value == "c")
                {
                    notaCargo.Id_Ban = null;
                    notaCargo.Nca_CtaContable = txtCuentaContable.Text;
                }
                else
                {
                    notaCargo.Id_Ban = null;
                    notaCargo.Nca_CtaContable = string.Empty;
                }
            notaCargo.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
            notaCargo.Id_Rik = Convert.ToInt32(txtRepresentante.Text);
            notaCargo.Id_U = sesion.Id_U;
            notaCargo.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1);
            notaCargo.Nca_Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
            notaCargo.Nca_Fecha = Convert.ToDateTime(txtFecha.SelectedDate);
            notaCargo.Nca_Desgloce = chkDesgloceIva.Checked;
            notaCargo.Nca_Referencia = txtReferencia.Text == "" ? -1 : Convert.ToInt32(txtReferencia.Text);
            notaCargo.Nca_Notas = txtNotas.Text;
            notaCargo.Nca_Subtotal = Convert.ToDouble(txtSubtotal2.Text);
            notaCargo.Nca_Iva = Convert.ToDouble(txtIva2.Text);
            notaCargo.Nca_Total = Convert.ToDouble(txtTotal2.Text);
            notaCargo.Nca_RSubtotal = 0;
            notaCargo.Nca_RIva = 0;
            notaCargo.Nca_RTotal = 0;
            notaCargo.Nca_Estatus = "C";
            notaCargo.Nca_FPago = cmbFormaPago.SelectedValue;
            notaCargo.Nca_UDigitos = txtUDigitos.Text;
            return notaCargo;
        }
        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadComboBoxItem item = ((RadComboBox)cmbMov).SelectedItem;
                Label lbl_AfeVta = (Label)item.FindControl("lbl_AfeVta");
                NotaCargo notaCargo = this.LlenarObjetoNotaCargo();
                string mensaje = string.Empty;

                if (notaCargo.Nca_Subtotal == 0)
                {
                    Alerta("La nota no puede grabarse con totales en ceros");
                }
                else
                {
                    if (lbl_AfeVta.Text.ToUpper() == "TRUE" && this.ListaProductosNotaCargo.Rows.Count == 0)
                        this.DisplayMensajeAlerta("CapturarProductosRequerido");
                    else
                    { //ADENDA
                        List<AdendaDet> listAdendaCabecera = new List<AdendaDet>();
                        AdendaDet ad;
                        RadTextBox txtAdenda = new RadTextBox();
                        for (int i = 0; i < rgAdendaNotaCargo.MasterTableView.Items.Count; i++)
                        {
                            ad = new AdendaDet();
                            txtAdenda = rgAdendaNotaCargo.MasterTableView.Items[i]["valor"].FindControl("RadTextBox1") as RadTextBox;
                            ad.Tipo = Convert.ToInt32(rgAdendaNotaCargo.MasterTableView.Items[i]["Tipo"].Text);
                            ad.Id_AdeDet = Convert.ToInt32(rgAdendaNotaCargo.MasterTableView.Items[i]["Id_AdeDet"].Text);
                            ad.Campo = rgAdendaNotaCargo.MasterTableView.Items[i]["campo"].Text;
                            ad.Nodo = rgAdendaNotaCargo.MasterTableView.Items[i]["nodo"].Text;
                            ad.Valor = (rgAdendaNotaCargo.MasterTableView.Items[i]["valor"].FindControl("RadTextBox1") as RadTextBox).Text.Replace("'", "").Trim();
                            
                            bool addenda_Requerida = ListCab.Where(AdendaDet => AdendaDet.Id_AdeDet == ad.Id_AdeDet).ToList()[0].Requerido;
                            if (ad.Valor == "" && addenda_Requerida)
                            {
                                AlertaFocus("El campo <b>" + ad.Campo + "</b> de la addenda es requerido", txtAdenda.ClientID);
                                RadTabStrip1.Tabs[1].Selected = true;
                                rpvAdendaNCargo.Selected = true;
                                return;
                            }
                            else
                                listAdendaCabecera.Add(ad);
                        }
                        // Evita que se guarde el documento si los totales no coinciden
                        if (Session["ListaProductosNotaCargoEspecial" + Session.SessionID] != null)
                        {
                            //if (Session["NcargoEspecialGuardada" + Session.SessionID].ToString() == "1")
                            //{
                                double totalEspecial = 0;
                                foreach (NotaCargoDet ncd in (List<NotaCargoDet>)Session["ListaProductosNotaCargoEspecial" + Session.SessionID])
                                {
                                    totalEspecial += ncd.Nca_Importe;
                                }

                                bool bEncontrados = true;

                                //Buscar que todos los Id´s de productos de la nota de cargo especial estén también en la nota de cargo detalle.
                                foreach (NotaCargoDet f in (List<NotaCargoDet>)Session["ListaProductosNotaCargoEspecial" + Session.SessionID])
                                {
                                    bEncontrados = false;
                                    for (int m = 0; m < ListaProductosNotaCargo.Rows.Count; m++)
                                    {
                                        if (ListaProductosNotaCargo.Rows[m]["Id_Prd"].ToString() == f.Id_Prd.ToString())
                                        {
                                            bEncontrados = true;
                                            break;
                                        }
                                    }
                                    if (bEncontrados == false)
                                        break;
                                }
                                if (bEncontrados == false)
                                {
                                    Alerta("La nota de cargo especial contiene partidas con productos distintos a la nota de cargo original.");
                                    return;
                                }

                                //Datos del centro de distribución (Para obtener la tolerancia o diferencia permitida entre totales de fac y fact especial)
                                CentroDistribucion cd = new CentroDistribucion();
                                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                                // Se indico que solo podía haber diferecia de 90 centavos
                                double TE1 = (Math.Round(txtSubtotal2.Value.Value, 2) + Math.Round((double)cd.Cd_MargenDiferenciaDocs, 2)); // Math.Round(totalEspecial, 2) + .90; // se suman 70 centavos al total especial -- modificar si se desea disminuir o aumentar el rango
                                double TE2 = (Math.Round(txtSubtotal2.Value.Value, 2) - Math.Round((double)cd.Cd_MargenDiferenciaDocs, 2)); // se restan 70 centavos al total especia

                                if (TE1 < Math.Round(totalEspecial, 2) || TE2 > Math.Round(totalEspecial, 2))
                                {
                                    Alerta("Los montos de la Nota de Cargo y la Nota de Cargo Especial tienen una diferencia considerable, favor de revisarlos.");
                                    return;
                                }
                            //}
                        }

                        int verificador = 0;
                        if (this.hiddenId.Value == string.Empty) //nueva nota de cargo
                        {
                            new CN_CapNotaCargo().InsertarNotaCargo(ref notaCargo, sesion.Emp_Cnx, ref verificador, listAdendaCabecera, ListaProductosNotaCargo);
                            mensaje = "Los datos se guardaron correctamente";
                        }
                        else
                        {
                            new CN_CapNotaCargo().ModificarNotaCargo(ref notaCargo, sesion.Emp_Cnx, ref verificador, listAdendaCabecera, ListaProductosNotaCargo);
                            mensaje = "Los datos se modificaron correctamente";
                        }

                        //SI GUARDÓ BIEN LA NOTA DE CARGO:
                        //Guardar los datos de los productos de NCargo especial
                        //en catálogo de Cliente-Producto
                        verificador = 0;
                        if (Session["ListaProductosNotaCargoEspecial" + Session.SessionID] != null)
                        {
                            if (Session["NcargoEspecialGuardada" + Session.SessionID].ToString() == "1") //guarda solo si hizo clic en guardar en pantalla de facturaEspecial.
                            {
                                FacturaEspecial facturaEsp = new FacturaEspecial();
                                facturaEsp.Id_Emp = notaCargo.Id_Emp;
                                facturaEsp.Id_Cd = notaCargo.Id_Cd;
                                facturaEsp.Id_Fac = notaCargo.Id_Nca;
                                facturaEsp.Id_Ter = Convert.ToInt32(notaCargo.Id_Ter);
                                facturaEsp.FacEsp_Fecha = notaCargo.Nca_Fecha;
                                facturaEsp.FacEsp_Importe = Convert.ToDouble(notaCargo.Nca_Total);
                                facturaEsp.FacEsp_SubTotal = Convert.ToDouble(notaCargo.Nca_Subtotal);
                                facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(notaCargo.Nca_Iva);
                                facturaEsp.FacEsp_Total = Convert.ToDouble(notaCargo.Nca_Total);
                                List<NotaCargoDet> listaProductosFacturaEspecial = (List<NotaCargoDet>)Session["ListaProductosNotaCargoEspecial" + Session.SessionID];
                                new CN_CatClienteProd().ModificarNCargoEspecial(ref facturaEsp, ref listaProductosFacturaEspecial, sesion.Emp_Cnx, ref verificador, string.IsNullOrEmpty(this.hiddenId.Value) ? 0 : 1);
                            }
                        }
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GuardarAdenda()
        {
            try
            {
                int verificador = 0;
                NotaCargo notaCargo = new NotaCargo();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                notaCargo.Id_Emp = sesion.Id_Emp;
                notaCargo.Id_Cd = sesion.Id_Cd_Ver;
                notaCargo.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1);

                for (int i = 0; i < this.rgAdendaNotaCargo.Items.Count; i++)
                {
                    RadTextBox txtAde_Campo = new RadTextBox();
                    RadTextBox txtAde_Longitud = new RadTextBox();
                    // txtAde_Campo = (RadTextBox)this.rgAdendaNotaCargo.Items[i].Cells[0].FindControl("txtAde_Campo");
                    txtAde_Longitud = (RadTextBox)this.rgAdendaNotaCargo.Items[i].Cells[1].FindControl("txtAde_Longitud");
                    //notaCargo.Ade_Campo = txtAde_Campo.Text;
                    notaCargo.Ade_Longitud = txtAde_Longitud.Text;

                    if (!string.IsNullOrEmpty(notaCargo.Ade_Campo) && !string.IsNullOrEmpty(notaCargo.Ade_Longitud))
                        new CN_CapNotaCargo().AgregarAdenda(notaCargo, sesion, ref verificador);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapNotaCargo", "Id_Nca", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarConsFactElectronica()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 2, sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbConsFacEle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoMovimientos()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Movimientos mov = new Movimientos();
            mov.Id_Emp = sesion.Id_Emp;
            List<Movimientos> listaMovimientos = null;
            new CN_CapNotaCargo().ConsultarMovsNotaCargo(mov, ref listaMovimientos, sesion.Emp_Cnx);
            cmbMov.DataTextField = "Nombre";
            cmbMov.DataValueField = "Id";
            cmbMov.DataSource = listaMovimientos;
            cmbMov.DataBind();
        }
        private void CargarComboTerritorios(List<Territorios> lista)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            //int cliente = !string.IsNullOrEmpty(txtCliente.Text) ? Convert.ToInt32(txtCliente.Text) : 0;                      
            //CN_Comun.LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, cliente, sesion.Emp_Cnx, "spCatTerritorioCte_Combo", ref cmbTerritorio);
            cmbTerritorio.DataTextField = "Descripcion";
            cmbTerritorio.DataValueField = "Id_Ter";
            cmbTerritorio.DataSource = lista;
            cmbTerritorio.DataBind();
            if (cmbTerritorio.Items.Count > 1)
            {
                cmbTerritorio.SelectedIndex = 1;
                cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                txtTerritorio.Text = cmbTerritorio.Items[1].Value;

                CN_CatTerritorios territorio = new CN_CatTerritorios();
                Territorios ter = new Territorios();
                ter.Id_Emp = sesion.Id_Emp;
                ter.Id_Cd = sesion.Id_Cd_Ver;
                ter.Id_Ter = Convert.ToInt32(cmbTerritorio.Items[1].Value);
                territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                txtRepresentante.Text = ter.Id_Rik.ToString();
                txtRepresentanteStr.Text = ter.Rik_Nombre;
            }
        }
        private void CargarComboBancos()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatBanco_Combo", ref cmbBanco);
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgNotaCargoDet_insert_repetida"))
                    Alerta("Este producto ya ha sido capturado");
                else
                    if (mensaje.Contains("cmbConsFacEle_ObtenerConsFacElectFallo"))
                        Alerta("Error al momento de obtener el consecutivo de facturación electrónica");
                    else
                        if (mensaje.Contains("CapturarProductosRequerido"))
                            Alerta("El tipo de movimiento de la nota de cargo afecta venta, debe capturar al menos un producto en el grid de la pestaña \"detalles\"");
                        else
                            if (mensaje.Contains("MovFacturacionPedidoNoValido"))
                                Alerta("El tipo movimiento no es válido");
                            else
                                if (mensaje.Contains("PermisoGuardarNo"))
                                    Alerta("No tiene permisos para grabar");
                                else
                                    if (mensaje.Contains("PermisoModificarNo"))
                                        Alerta("No tiene permisos para actualizar");
                                    else
                                        if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                            Alerta("Error al consultar los datos del producto");
                                        else
                                            if (mensaje.Contains("CapNotaCargo_insert_ok"))
                                                Alerta("Los datos se guardaron correctamente");
                                            else
                                                if (mensaje.Contains("CapNotaCargo_insert_error"))
                                                    Alerta(string.Concat("No se pudo guardar la nota de cargo. ", mensaje.Replace("'", "\"")));
                                                else
                                                    if (mensaje.Contains("CapNotaCargo_update_ok"))
                                                        Alerta("Los datos se modificaron correctamente");
                                                    else
                                                        if (mensaje.Contains("CapNotaCargo_update_error"))
                                                            Alerta(string.Concat("No se pudo actualizar la nota de cargo. ", mensaje.Replace("'", "\"")));
                                                        else
                                                            if (mensaje.Contains("rgNotaCargoDet_NeedDataSource"))
                                                                Alerta("Error al cargar el grid de detalle de la nota de cargo");
                                                            else
                                                                if (mensaje.Contains("rgNotaCargoDet_ItemDataBound"))
                                                                    Alerta("Error al momento de preparar un registro para edici&oacute;n");
                                                                else
                                                                    if (mensaje.Contains("rgNotaCargoDet_insert_error"))
                                                                        Alerta("Error al momento de agregar el producto a la lista de productos de la nota de cargo");
                                                                    else
                                                                        if (mensaje.Contains("rgNotaCargoDet_delete_error"))
                                                                            Alerta("Error al momento de eliminar el producto a la lista de productos de la nota de cargo");
                                                                        else
                                                                            Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        protected void ListaProductosNotaCargo_EliminarProducto(int id_Prd)
        {
            try
            {
                DataRow[] Ar_dr;
                Ar_dr = ListaProductosNotaCargo.Select("id_Prd='" + id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    ListaProductosNotaCargo.AcceptChanges();
                }
                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularTotales()
        {
            //if (!txtIva2.Visible)
            //{
            //    txtTotal2.Text = txtSubtotal2.Text;
            //    return;
            //}
            double importeTotal = 0;
            for (int i = 0; i < ListaProductosNotaCargo.Rows.Count; i++)
            {
                importeTotal += Convert.ToDouble(ListaProductosNotaCargo.Rows[i]["Nca_Importe"]);
            }
            //if (!txtSubtotal2.Enabled)
            //{
            txtSubtotal2.Text = importeTotal.ToString();
            //}
            //if (!txtIva2.Enabled)
            //{
            txtIva2.Text = HD_IVAfacturacion.Value.Trim() != string.Empty ? (importeTotal * (Convert.ToSingle(HD_IVAfacturacion.Value.Trim()) / 100)).ToString() : "0";
            //}
            txtTotal2.Text = (Convert.ToDouble(txtSubtotal2.Text) + Convert.ToDouble(txtIva2.Text)).ToString();
            Session["fTotalNCargo" + Session.SessionID] = txtTotal2.Text;
        }
        protected string ObtenerDescripcionProducto(object oc)
        {
            if (oc is NotaCargoDet) { return ((NotaCargoDet)oc).Prd_Nombre; } else { return string.Empty; }
        }
        protected string ObtenerDescripcionTerritorio(object oc)
        {
            if (oc is NotaCargoDet) { return ((NotaCargoDet)oc).Ter_Nombre; } else { return string.Empty; }
        }
        protected string ObtenerDescripcionRepresentante(object oc)
        {
            if (oc is NotaCargoDet) { return ((NotaCargoDet)oc).Rik_Nombre; } else { return string.Empty; }
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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
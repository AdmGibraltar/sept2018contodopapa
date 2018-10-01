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
    public partial class CapNotaCredito : System.Web.UI.Page
    {
        #region Variables

        //Variable global para conocer el territorio.

        int Id_territorio;

        public bool HabilitarGuardar
        {
            get
            {
                //DEVUELVE SI SE PUEDE O NO GUARDAR
                return RadToolBar1.Items[1].Visible;
            }
        }
        //ADENDA
        public List<AdendaDet> ListCab
        {
            get
            {
                return Session["CabeceraCredito" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["CabeceraCredito" + Session.SessionID] = value;
            }
        }
        public List<AdendaDet> ListDet
        {
            get
            {
                return Session["DetalleCredito" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["DetalleCredito" + Session.SessionID] = value;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private string _Editable;

        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        public DataTable ListaProductosNotaCredito
        {
            get
            {
                return (Session["ListaProductosNotaCredito" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosNotaCredito" + Session.SessionID] = value;
            }
        }
        public string FechaEnable
        {
            get
            {
                return _Editable;// txtFecha.Enabled;
            }
        }

        #region "Propiedades"

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
        //private List<NotaCreditoDet> ListaProductosNotaCredito
        //{
        //    get { return (List<NotaCreditoDet>)Session["ListaProductosNotaCredito"]; }
        //    set { Session["ListaProductosNotaCredito"] = value; }
        //}

        #endregion
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    CerrarVentana();
                else
                {
                    if (!Page.IsPostBack)
                    { //obtener valores desde la URL
                        int Id_Ncr = Convert.ToInt32(Page.Request.QueryString["Id_Ncr"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        string Id_NcrSerie = Page.Request.QueryString["Id_NcrSerie"].ToString();
                        this.HDId_NcrSerie.Value = Id_NcrSerie;
                      
                        string notaModificable = Page.Request.QueryString["notaModificable"].ToString();
                        _Editable = notaModificable;
                        _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                        _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                        _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                        _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;

                        this.Inicializar(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie,notaModificable);

                        if (notaModificable == "0")//!((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(formulario.Controls);
                            HabilitarColumnas(true);
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgNotaCreditoDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgNotaCreditoDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgNotaCreditoDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        protected void rgNotaCreditoDet_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem editItem = (GridDataItem)e.Item;
                    TextBox txt;
                    if (ListDet != null)
                    {
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
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgNotaCreditoDet.Rebind();
                        break;

                    case "GuardarMovimiento":
                        mensajeError = hiddenId.Value == string.Empty ? "CapNotaCredito_insert_error" : "CapNotaCredito_update_error";
                        this.Guardar();
                        break;

                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 155);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
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
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = hiddenId.Value == string.Empty ? "CapNotaCredito_insert_error" : "CapNotaCredito_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                { //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);
                    //Llenar Grid
                    rgNotaCreditoDet.DataSource = this.ListaProductosNotaCredito;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgNotaCreditoDet.EditItems.Count > 0)
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
        protected void rgNotaCreditoDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgNotaCreditoDet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
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
                    string txtNcr_Importe = ((RadNumericTextBox)editItem.FindControl("txtNcr_Importe")).ClientID.ToString();
                    string lbl_txtNcr_Importe = ((Label)editItem.FindControl("lbl_txtNcr_Importe")).ClientID.ToString();

                    //Llenar combo de territorios
                    int cliente = !string.IsNullOrEmpty(txtCliente.Text) ? Convert.ToInt32(txtCliente.Text) : 0;

                    RadComboBox comboTerritorioPartidaItem = (RadComboBox)editItem.FindControl("cmbTerritorioPartida");
                    List<Territorios> listaTerritorios = new List<Territorios>();
                    new CN_CatCliente().ConsultaTodosTerritoriosDelCliente(cliente, sesion, ref listaTerritorios);
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
                        , "txtNcr_ImporteClientID='", txtNcr_Importe, "';"
                        , "lbl_txtNcr_ImporteClientID='", lbl_txtNcr_Importe, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                        comboTerritorioPartidaItem.Focus();
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        int Id_Prd = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"]);
                        foreach (DataRow ncd in this.ListaProductosNotaCredito.Rows)
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
                        ((RadNumericTextBox)editItem.FindControl("txtNcr_Importe")).Focus();
                    }
                }
                ValidarHabilitacionTotales(ref cmbMov);


                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_TerN"].FindControl("txtTerritorioPartida");
                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form["Ncr_Importe"].FindControl("txtNcr_Importe");
                    }

                    dataField.Focus();
                }
                //-----------------------------------------
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                NotaCreditoDet notaCreditoDet = new NotaCreditoDet();

                notaCreditoDet.Id_Emp = sesion.Id_Emp;
                notaCreditoDet.Id_Cd = sesion.Id_Cd_Ver;
                notaCreditoDet.Id_Ncr = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                notaCreditoDet.Id_NcrDet = 0;
                RadComboBox comboTerritorio = (insertedItem.FindControl("cmbTerritorioPartida") as RadComboBox);
                notaCreditoDet.Id_Ter = Convert.ToInt32(comboTerritorio.SelectedValue);
                notaCreditoDet.Ter_Nombre = comboTerritorio.SelectedItem.Text;
                notaCreditoDet.Id_Rik = Convert.ToInt32((insertedItem.FindControl("txtRepresentantePartida") as RadNumericTextBox).Text);
                notaCreditoDet.Rik_Nombre = (insertedItem.FindControl("txtRepresentantePartidaStr") as RadTextBox).Text;
                notaCreditoDet.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value.Value);
                notaCreditoDet.Prd_Nombre = (insertedItem.FindControl("txtProductoNombre") as RadTextBox).Text;
                notaCreditoDet.Ncr_Importe = Convert.ToDouble((insertedItem.FindControl("txtNcr_Importe") as RadNumericTextBox).Text);

                //ADENDAS
                if (ListaProductosNotaCredito.Select("Id_Prd='" + notaCreditoDet.Id_Prd.ToString() + "'").Length > 0)
                {
                    Alerta("El producto ya ha sido agregado a la nota de crédito");
                    e.Canceled = true;
                    return;
                }
                ArrayList al = new ArrayList();

                al.Add(notaCreditoDet.Id_Emp);
                al.Add(notaCreditoDet.Id_Cd);
                al.Add(notaCreditoDet.Id_Ncr);
                al.Add(notaCreditoDet.Id_NcrDet);
                al.Add(notaCreditoDet.Id_Ter);
                al.Add(notaCreditoDet.Ter_Nombre);
                al.Add(notaCreditoDet.Id_Rik);
                al.Add(notaCreditoDet.Rik_Nombre);
                al.Add(notaCreditoDet.Id_Prd);
                al.Add(notaCreditoDet.Prd_Nombre);
                al.Add(notaCreditoDet.Ncr_Importe);

                bool falta_adenda = false;
                TextBox Txtadenda = new TextBox();
                string adenda_faltante = "";
                string valor_adenda = "";
                foreach (AdendaDet det in ListDet)
                {
                    Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                    valor_adenda = Txtadenda.Text;
                    if (valor_adenda == "" && det.Requerido)
                    {
                        falta_adenda = true;
                        adenda_faltante = det.Campo;
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
                    ListaProductosNotaCredito.Rows.Add(al.ToArray());
                    this.CalcularTotales();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                NotaCreditoDet notaCreditoDet = new NotaCreditoDet();

                notaCreditoDet.Id_Emp = sesion.Id_Emp;
                notaCreditoDet.Id_Cd = sesion.Id_Cd_Ver;
                notaCreditoDet.Id_Ncr = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                notaCreditoDet.Id_NcrDet = 0;
                RadComboBox comboTerritorio = (insertedItem.FindControl("cmbTerritorioPartida") as RadComboBox);
                notaCreditoDet.Id_Ter = Convert.ToInt32(comboTerritorio.SelectedValue);
                notaCreditoDet.Ter_Nombre = comboTerritorio.Text;
                notaCreditoDet.Id_Rik = Convert.ToInt32((insertedItem.FindControl("txtRepresentantePartida") as RadNumericTextBox).Text);
                notaCreditoDet.Rik_Nombre = (insertedItem.FindControl("txtRepresentantePartidaStr") as RadTextBox).Text;
                RadComboBox comboProducto = (insertedItem.FindControl("cmbProducto") as RadComboBox);
                notaCreditoDet.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value.Value);
                notaCreditoDet.Prd_Nombre = (insertedItem.FindControl("txtProductoNombre") as RadTextBox).Text;
                notaCreditoDet.Ncr_Importe = Convert.ToDouble((insertedItem.FindControl("txtNcr_Importe") as RadNumericTextBox).Text);
                string id_Prd = insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Prd"].ToString();

                //ADENDA
                DataRow[] Ar_dr;

                Ar_dr = ListaProductosNotaCredito.Select("Id_Prd='" + id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Emp"] = notaCreditoDet.Id_Emp;
                    Ar_dr[0]["Id_Cd"] = notaCreditoDet.Id_Cd;
                    Ar_dr[0]["Id_Ncr"] = notaCreditoDet.Id_Ncr;
                    Ar_dr[0]["Id_NcrDet"] = notaCreditoDet.Id_NcrDet;
                    Ar_dr[0]["Id_Ter"] = notaCreditoDet.Id_Ter;
                    Ar_dr[0]["Ter_Nombre"] = notaCreditoDet.Ter_Nombre;
                    Ar_dr[0]["Id_Rik"] = notaCreditoDet.Id_Rik;
                    Ar_dr[0]["Rik_Nombre"] = notaCreditoDet.Rik_Nombre;
                    Ar_dr[0]["Id_Prd"] = notaCreditoDet.Id_Prd;
                    Ar_dr[0]["Prd_Nombre"] = notaCreditoDet.Prd_Nombre;
                    Ar_dr[0]["Ncr_Importe"] = notaCreditoDet.Ncr_Importe;

                    bool falta_adenda = false;
                    string valor_adenda = "";
                    TextBox Txtadenda = new TextBox();
                    string adenda_faltante = "";
                    foreach (AdendaDet det in ListDet)
                    {
                        Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                        valor_adenda = Txtadenda.Text;
                        if (valor_adenda == "" && det.Requerido)
                        {
                            falta_adenda = true;
                            adenda_faltante = det.Campo;
                            break;
                        }
                        else
                        {
                            Ar_dr[0][det.Id_AdeDet.ToString()] = valor_adenda;
                        }
                    }

                    if (falta_adenda)
                    {
                        AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                        e.Canceled = true;
                    }
                    else
                    {
                        Ar_dr[0].AcceptChanges();
                    }
                }
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNotaCreditoDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridDataItem item = (GridDataItem)e.Item;
                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                DataRow[] Ar_dr;
                Ar_dr = ListaProductosNotaCredito.Select("id_Prd='" + id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    ListaProductosNotaCredito.AcceptChanges();
                }
                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbMov_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                //Limpiar controles Totales
                txtSubtotal.Text = "0";
                txtIva.Text = "0";
                txtTotal.Text = "0";

                /*
                 * Si el tipo de movimiento afecta a ventas TMCAFEVTA=S no se puede modificar el IVA ni el subtotal. 
                 */
                RadComboBox combo = (RadComboBox)sender;
                /*
                 * Cuando el tipo de movimiento es 5 no permite capturar información eso debe capturar desde la devolución parcial. Mostrará el mensaje:
                 * "Movimiento no se puede aplicar directamente es necesario que se aplique en Inventarios-Capturas-Devoluciones Parciales"
                 */
                if (e.Value == "6")
                {
                    this.HD_PanelVisible.Value = "e"; //empleado
                    this.panelEmpleado.Style.Add("display", "block");
                    this.panelCuentaContable.Style.Add("display", "none");
                }
                else
                {
                    if (e.Value == "3")
                    {
                        this.HD_PanelVisible.Value = "c"; //cuenta contable
                        this.panelEmpleado.Style.Add("display", "none");
                        this.panelCuentaContable.Style.Add("display", "block");
                    }
                    else
                    {
                        this.HD_PanelVisible.Value = "";
                        this.panelEmpleado.Style.Add("display", "none");
                        this.panelCuentaContable.Style.Add("display", "none");
                        if (e.Value == "5")
                        {
                            this.txtMov.Text = string.Empty;
                            this.cmbMov.SelectedIndex = this.cmbMov.FindItemIndexByValue("-1");
                            this.DisplayMensajeAlerta("Movimiento_AplicaNotCredDenegado");

                            this.HabilitaTotales(true);
                        }
                    }
                }
                cmbMov.Focus(); 
                InicializarTablaProductos();
                this.ValidarHabilitacionGrid();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void cmbCliente_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.ConsultarDatosCliente(txtCliente.Text, false);
                this.txtTerritorio.Text = string.Empty;
                this.txtRepresentante.Text = string.Empty;
                this.txtRepresentanteStr.Text = string.Empty;
                InicializarTablaProductos();
                this.ValidarHabilitacionGrid();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbTerritorio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
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
                    if (txtEmpleado.Visible)
                    {
                        txtEmpleado.Focus();
                    }
                    else
                    {
                        txtCuentaContable.Focus();
                    }
                }
                else
                {
                    txtRepresentante.Text = string.Empty;
                    txtRepresentanteStr.Text = string.Empty;
                }
                InicializarTablaProductos();
                this.ValidarHabilitacionGrid();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbTerritorioPartida_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                //obtiene la tabla contenedora de los controles de edición de registro del Grid
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
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbMovimientoTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                this.LimpiarControlesMovimiento_FacturaNotaCargo();
                if (e.Value != "-1")
                {
                    if (txtReferencia.Text != string.Empty)
                    {
                        this.ConsultarDatosMovimiento_FacturaNotaCargo();
                    }
                }
                InicializarTablaProductos();
                CargarSerie(int.Parse(this.cmbMovimientoTipo.SelectedValue));
                this.ValidarHabilitacionGrid();
                txtReferencia.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                ErrorManager();
                //if (e.Item.Value == "-1")
                //{
                //    e.Item.FindControl("liComprasLocales").Controls.Clear();
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAdendaNotaCredito_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAdendaNotaCredito.DataSource = ListCab;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox txtProducto = (RadNumericTextBox)sender;
                //ADENDAS
                if (ListaProductosNotaCredito.Select("Id_Prd='" + txtProducto.Value.ToString() + "'").Length > 0)
                {
                    AlertaFocus("El producto ya ha sido agregado a la nota de crédito", txtProducto.ClientID);
                    txtProducto.Text = "";
                    ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = "";
                    ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtNcr_Importe") as RadNumericTextBox).Text = "";

                    return;
                }
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto prd = new Producto();
                CN_CatProducto cnProducto = new CN_CatProducto();

                RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)txtProducto.Parent.FindControl("txtTerritorioPartida");
                try
                {
                    cnProducto.ConsultaProducto(ref prd, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value : -1));
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtProducto.ClientID);
                    txtProducto.Text = "";
                    ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = "";
                    return;
                }
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = prd.Prd_Descripcion;
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtNcr_Importe") as RadNumericTextBox).Focus();
            }
            catch (Exception)
            {
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            InicializarTablaProductos();
            this.ValidarHabilitacionGrid();
            this.ConsultarDatosCliente(txtCliente.Value.HasValue ? txtCliente.Value.Value.ToString() : "-1", false);
            

        }
        protected void cmbConsFacEle_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.ObtenerConsecutivoFacElectronica(Convert.ToInt32(cmbConsFacEle.SelectedValue));
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtReferencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                if (this.cmbSerie.SelectedValue == "-1")
                {
                    Alerta("Debe seleccionar la serie del documento");
                    return;
                }

                this.LimpiarControlesMovimiento_FacturaNotaCargo();

                if (txtReferencia.Text != string.Empty)
                {
                    this.ConsultarDatosMovimiento_FacturaNotaCargo();
                }

                this.ValidarHabilitacionGrid();
                
                txtCliente.Focus();

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
                    txtIva.Text = "0";
                }
                CalcularTotales();
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
        #endregion
        #region Funciones
        private void CargarSerie(int Tipo)
        {
            try
            {
                CN__Comun cn_comun = new CN__Comun();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver , Tipo, sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref this.cmbSerie);
 
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
        //ADENDA
        private void InicializarTablaProductos()
        {
            ListaProductosNotaCredito = new DataTable();
            ListaProductosNotaCredito.Columns.Add("Id_Emp");
            ListaProductosNotaCredito.Columns.Add("Id_Cd");
            ListaProductosNotaCredito.Columns.Add("Id_Ncr");
            ListaProductosNotaCredito.Columns.Add("Id_NcrDet");
            ListaProductosNotaCredito.Columns.Add("Id_Ter");
            ListaProductosNotaCredito.Columns.Add("Ter_Nombre");
            ListaProductosNotaCredito.Columns.Add("Id_Rik");
            ListaProductosNotaCredito.Columns.Add("Rik_Nombre");
            ListaProductosNotaCredito.Columns.Add("Id_Prd");
            ListaProductosNotaCredito.Columns.Add("Prd_Nombre");
            ListaProductosNotaCredito.Columns.Add("Ncr_Importe", typeof(System.Double));
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
        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Ncr,string Id_NcrSerie, string notaModificable)
        {
            //ADENDA
            Session["ListaProductosNotaCreditoEspecial" + Session.SessionID] = null;
            Session["NCreditoEspecialGuardada" + Session.SessionID] = "0";
            InicializarTablaProductos();

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarConsFactElectronica();
            this.CargarComboTipoMovimientos();

            CentroDistribucion cd = new CentroDistribucion();
            new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
            HD_IVAfacturacion.Value = cd.Cd_IvaPedidosFacturacion.ToString();

            //nueva variable para controlar tabla dinamica de productos de nota de cargo
            Session["ListaProductosNotaCredito"] = new List<NotaCreditoDet>();

            this.panelEmpleado.Style.Add("display", "none");
            this.panelCuentaContable.Style.Add("display", "none");
            //establece valores de controles de inicio
            if (Id_Emp > 0 && Id_Cd > 0 && Id_Ncr > 0)
            {
                this.LLenarFormNotaCredito(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie); //EDICION DE NOTA
                this.hiddenId.Value = Id_Ncr.ToString();
                this.rgNotaCreditoDet.Enabled = true;
                if (notaModificable == "0")
                    RadToolBar1.FindItemByValue("save").Visible = false;
            }
            else //NOTA NUEVA
            {
                this.hiddenId.Value = string.Empty;
                this.chkDesgloceIva.Checked = true;
                this.txtFecha.SelectedDate = DateTime.Now;
                this.rgNotaCreditoDet.Enabled = false;

                if (cmbConsFacEle.Items.Count > 1)
                {
                    cmbConsFacEle.SelectedIndex = 1;
                    cmbConsFacEle_SelectedIndexChanged(null, null);
                }
            }
            this.rgNotaCreditoDet.Rebind();
            this.txtFecha.Focus();
        }
        private void LimpiarControlesMovimiento_FacturaNotaCargo()
        {
            //limpiar controles de cliente
            txtSaldo.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtTerritorio.Text = string.Empty;
            cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue("-1");
            txtRepresentante.Text = string.Empty;
            txtRepresentanteStr.Text = string.Empty;
        }
        private void ConsultarDatosMovimiento_FacturaNotaCargo()
        {
            RadTabStrip1.Tabs[1].Visible = false;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Consecutivo = 0;
            bool encontrado = false;
            txtClienteNombre.Text = "";
            cmbTerritorio.Items.Clear();
            cmbTerritorio.Text = "";
            int id_territorio;
            Consecutivo = Convert.ToInt32(txtReferencia.Text);
            switch (cmbMovimientoTipo.SelectedValue)
            {
                case "-1":
                    Alerta("Favor de seleccionar el tipo de documento");
                    cmbMovimientoTipo.Focus();
                    txtReferencia.Text = "";
                    break;

                case "1": //factura
                    Factura factura = new Factura();
                    factura.Id_Emp = sesion.Id_Emp;
                    factura.Id_Cd = sesion.Id_Cd_Ver;
                    factura.Id_Fac = Consecutivo;
                    factura.Id_FacSerie = this.cmbSerie.Text + Consecutivo.ToString();

                    new CN_CapFactura().ConsultaFacturaEncabezado(ref factura, sesion.Emp_Cnx, ref encontrado);
                    id_territorio = Convert.ToInt32 ( factura.Id_Ter);

                    if (!encontrado)
                    {
                        AlertaFocus("El movimiento no fue encontrado", txtReferencia.ClientID);
                        txtReferencia.Text = "";
                    }
                    else
                    {
                        if (factura.Fac_Estatus.ToUpper() == "B")
                        {
                            AlertaFocus("Movimiento se encuentra cancelado; imposible aplicarle una nota de crédito", txtReferencia.ClientID);
                            txtReferencia.Text = "";
                        }
                        else
                        {
                            if (factura.Fac_Estatus.ToUpper() == "C")
                            {
                                AlertaFocus("Movimiento se encuentra en estatus capturado; imposible aplicarle una nota de crédito", txtReferencia.ClientID);
                                txtReferencia.Text = "";
                            }
                            else
                            {
                                if (factura.Fac_Saldo <= 0)
                                {
                                    AlertaFocus("El movimiento no tiene saldo, imposible aplicarle una nota de crédito", txtReferencia.ClientID);
                                    txtReferencia.Text = "";
                                }
                                else
                                {
                                    txtSaldo.Text = factura.Fac_Saldo.ToString();
                                    txtCliente.Text = factura.Id_Cte.ToString();
                                    //agregar sp consulta.. de cliente
                                    txtClienteNombre.Text = factura.Cte_NomComercial;
                                    //consulta datos del ciente y sus territorios
                                    int? id_terr;
                                    
                                    id_terr = factura.Id_Ter;

                                
                                    this.ConsultarDatosCliente2(factura.Id_Cte.ToString(), false, id_territorio);
                                    chkDesgloceIva.Focus();
                                }
                            }
                        }
                    }
                    break;

                case "2": //nota de cargo
                    NotaCargo notaCargo = new NotaCargo();
                    notaCargo.Id_Emp = sesion.Id_Emp;
                    notaCargo.Id_Cd = sesion.Id_Cd_Ver;
                    notaCargo.Id_Nca = Consecutivo;
                    notaCargo.Id_NcaSerie = this.cmbSerie.Text + Consecutivo.ToString();

                    new CN_CapNotaCargo().ConsultaNotaCargo_Encabezado(ref notaCargo, sesion.Emp_Cnx, ref encontrado);
                    id_territorio = notaCargo.Id_Ter;

                    if (!encontrado)
                    {
                        AlertaFocus("El movimiento no fue encontrado", txtReferencia.ClientID);
                        txtReferencia.Text = "";
                    }
                    else
                    {
                        if (notaCargo.Nca_Estatus.ToUpper() == "B")
                        {
                            AlertaFocus("Movimiento se encuentra cancelado; imposible aplicarle una nota de crédito", txtReferencia.ClientID);
                            txtReferencia.Text = "";
                        }
                        else
                        {
                            if (notaCargo.Nca_Estatus.ToUpper() == "C")
                            {
                                AlertaFocus("Movimiento se encuentra en estatus capturado; imposible aplicarle una nota de crédito", txtReferencia.ClientID);
                                txtReferencia.Text = "";
                            }

                            else
                            {
                                if (notaCargo.Nca_Saldo <= 0)
                                {
                                    AlertaFocus("El movimiento no tiene saldo, imposible aplicarle una nota de crédito", txtReferencia.ClientID);
                                    txtReferencia.Text = "";
                                }
                                else
                                {
                                    txtSaldo.Text = notaCargo.Nca_Saldo.ToString();
                                    txtCliente.Text = notaCargo.Id_Cte.ToString();

                                    //agregar sp consulta.. de cliente
                                    txtClienteNombre.Text = notaCargo.Cte_NomComercial;
                                    //consulta datos del ciente y sus territorios
                                    this.ConsultarDatosCliente2(notaCargo.Id_Cte.ToString(), false, id_territorio);
                                    chkDesgloceIva.Focus();
                                }
                            }
                        }
                    }
                    break;
            }
        }
        private void ValidarHabilitacionTotales(ref RadComboBox combo)
        {  /*
            * Si el tipo de movimiento afecta a ventas TMCAFEVTA=S no se puede modificar el IVA ni el subtotal. 
            */
            RadComboBoxItem item = ((RadComboBox)combo).SelectedItem;
            if (item.Value != string.Empty && item.Value != "-1")
            {
                Label lbl_AfeVta = (Label)item.FindControl("lbl_AfeVta");
                if (lbl_AfeVta.Text.ToUpper() == "TRUE")
                    this.HabilitaTotales(false);
                else
                    this.HabilitaTotales(true);
            }
        }
        private void ValidarHabilitacionGrid()
        {
            //InicializarTablaProductos();
            rgNotaCreditoDet.Rebind();
            CalcularTotales();
        }
        private void HabilitaTotales(bool habilitar)
        {
            this.txtSubtotal.Enabled = habilitar;
            this.txtIva.Enabled = habilitar;

            this.rgNotaCreditoDet.Enabled = true;

            HabilitarColumnas(habilitar);
        }
        private void HabilitarColumnas(bool habilitar)
        {
            GridCommandItem cmdItem = (GridCommandItem)rgNotaCreditoDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            cmdItem.FindControl("AddNewRecordButton").Parent.Visible = !habilitar;
            try
            {
                rgNotaCreditoDet.Columns.FindByUniqueName("EditCommandColumn").Display = !habilitar;
            }
            catch
            { }
            try
            {
                rgNotaCreditoDet.Columns.FindByUniqueName("DeleteColumn").Display = !habilitar;
            }
            catch
            {
            }
        }
        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                ErrorManager();
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
                        , 3 // 1 = factura, 2 = nota de cargo, 3 = nota de credito
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

                throw ex;
            }
        }
        private void ConsultarDatosCliente(string idCliente, bool modificar, int? id_terr = 1)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<Territorios> listaTerritorios = new List<Territorios>();
            txtClienteNombre.Text = "";
            if (idCliente != string.Empty && idCliente != "-1")
            {//Consultar clientes
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
                    txtClienteNombre.Text = "";
                    return;
                }

                txtClienteNombre.Text = cliente.Cte_NomComercial;
                this.chkDesgloceIva.Checked = cliente.Cte_DesgIva;
                chkDesgloceIva_CheckedChanged(null, null);
                if (cliente.Cte_SerieNCre > 0)
                {
                    this.cmbConsFacEle.SelectedIndex = this.cmbConsFacEle.FindItemIndexByValue(cliente.Cte_SerieNCre.ToString());
                    if (!modificar) //Trae el cosecutivo si no es una modificación de documento                   
                        this.ObtenerConsecutivoFacElectronica(cliente.Cte_SerieNCre);
                }
                //Consultar territorios relacionados con el cliente
                if (Convert.ToInt32(idCliente) > 0)
                {

                    new CN_CatCliente().ConsultaTodosTerritoriosDelCliente(Convert.ToInt32(idCliente), sesion, ref listaTerritorios);
                    this.CargarComboTerritorios(listaTerritorios, id_terr );
                }
                //ADENDAS
                if (!modificar)
                {
                    List<AdendaDet> listCabT = new List<AdendaDet>();
                    List<AdendaDet> listDetT = new List<AdendaDet>();
                    List<AdendaDet> listCabR = new List<AdendaDet>();
                    ListCab = new List<AdendaDet>();
                    ListDet = new List<AdendaDet>();
                    for (int i = rgNotaCreditoDet.Columns.Count; i > 9; i--)
                        rgNotaCreditoDet.Columns.RemoveAt(rgNotaCreditoDet.Columns.Count - 1);
                    new CN_CatCliente().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(idCliente), "5,6", ref listCabT, ref listDetT, ref listCabR, sesion.Emp_Cnx);

                    if (listCabT.Count > 0)
                    {
                        RadTabStrip1.Tabs[1].Visible = true;
                        ListCab = listCabT;
                        rgAdendaNotaCredito.Rebind();
                    }

                    GridBoundColumn boundColumn1;
                    InicializarTablaProductos();
                    foreach (AdendaDet ad in listDetT)
                    {
                        boundColumn1 = new GridBoundColumn();
                        rgNotaCreditoDet.MasterTableView.Columns.Add(boundColumn1);
                        boundColumn1.DataField = ad.Id_AdeDet.ToString();
                        boundColumn1.UniqueName = ad.Id_AdeDet.ToString();
                        boundColumn1.HeaderText = ad.Campo;
                        boundColumn1.HeaderStyle.Width = Unit.Pixel(150);
                        boundColumn1.MaxLength = ad.Longitud;
                        ListaProductosNotaCredito.Columns.Add(ad.Id_AdeDet.ToString());
                    }

                    //CREA BOTON DE EDITAR
                    GridEditCommandColumn commandColumn = new GridEditCommandColumn();
                    rgNotaCreditoDet.MasterTableView.Columns.Add(commandColumn);
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
                    rgNotaCreditoDet.MasterTableView.Columns.Add(deleteColumn);
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
                    foreach (GridColumn gc in rgNotaCreditoDet.Columns)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                    rgNotaCreditoDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgNotaCreditoDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    ListDet = listDetT;
                    rgNotaCreditoDet.Rebind();
                }
                else
                {
                    /*
                    if (id_terr ==1 )
                    {
                    Territorios ter = new Territorios();
                    ter.Descripcion = "-- Seleccionar --";
                    ter.Id_Ter = -1;
                    listaTerritorios.Insert(0, ter);
                    this.CargarComboTerritorios(listaTerritorios);
                    }*/
                }
                txtTerritorio.Focus();
            }
        }
        private void ConsultarDatosCliente2(string idCliente, bool modificar, int id_territorio, int? id_terr = 1)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<Territorios> listaTerritorios = new List<Territorios>();
            txtClienteNombre.Text = "";
            if (idCliente != string.Empty && idCliente != "-1")
            {//Consultar clientes
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
                    txtClienteNombre.Text = "";
                    return;
                }

                txtClienteNombre.Text = cliente.Cte_NomComercial;
                this.chkDesgloceIva.Checked = cliente.Cte_DesgIva;
                chkDesgloceIva_CheckedChanged(null, null);
                if (cliente.Cte_SerieNCre > 0)
                {
                    this.cmbConsFacEle.SelectedIndex = this.cmbConsFacEle.FindItemIndexByValue(cliente.Cte_SerieNCre.ToString());
                    if (!modificar) //Trae el cosecutivo si no es una modificación de documento                   
                        this.ObtenerConsecutivoFacElectronica(cliente.Cte_SerieNCre);
                }
                //Consultar territorios relacionados con el cliente
                if (Convert.ToInt32(idCliente) > 0)
                {

                    new CN_CatCliente().ConsultaTodosTerritoriosDelCliente(Convert.ToInt32(idCliente), sesion, ref listaTerritorios);
                    this.CargarComboTerritorios2(listaTerritorios, id_territorio);
                    this.cmbTerritorio.SelectedValue = Convert.ToString ( id_territorio);

                }
                //ADENDAS
                if (!modificar)
                {
                    List<AdendaDet> listCabT = new List<AdendaDet>();
                    List<AdendaDet> listDetT = new List<AdendaDet>();
                    List<AdendaDet> listCabR = new List<AdendaDet>();
                    ListCab = new List<AdendaDet>();
                    ListDet = new List<AdendaDet>();
                    for (int i = rgNotaCreditoDet.Columns.Count; i > 9; i--)
                        rgNotaCreditoDet.Columns.RemoveAt(rgNotaCreditoDet.Columns.Count - 1);
                    new CN_CatCliente().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(idCliente), "5,6", ref listCabT, ref listDetT, ref listCabR, sesion.Emp_Cnx);

                    if (listCabT.Count > 0)
                    {
                        RadTabStrip1.Tabs[1].Visible = true;
                        ListCab = listCabT;
                        rgAdendaNotaCredito.Rebind();
                    }

                    GridBoundColumn boundColumn1;
                    InicializarTablaProductos();
                    foreach (AdendaDet ad in listDetT)
                    {
                        boundColumn1 = new GridBoundColumn();
                        rgNotaCreditoDet.MasterTableView.Columns.Add(boundColumn1);
                        boundColumn1.DataField = ad.Id_AdeDet.ToString();
                        boundColumn1.UniqueName = ad.Id_AdeDet.ToString();
                        boundColumn1.HeaderText = ad.Campo;
                        boundColumn1.HeaderStyle.Width = Unit.Pixel(150);
                        boundColumn1.MaxLength = ad.Longitud;
                        ListaProductosNotaCredito.Columns.Add(ad.Id_AdeDet.ToString());
                    }

                    //CREA BOTON DE EDITAR
                    GridEditCommandColumn commandColumn = new GridEditCommandColumn();
                    rgNotaCreditoDet.MasterTableView.Columns.Add(commandColumn);
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
                    rgNotaCreditoDet.MasterTableView.Columns.Add(deleteColumn);
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
                    foreach (GridColumn gc in rgNotaCreditoDet.Columns)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                    rgNotaCreditoDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgNotaCreditoDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    ListDet = listDetT;
                    rgNotaCreditoDet.Rebind();
                }
                else
                {
                    Territorios ter = new Territorios();
                    ter.Descripcion = "-- Seleccionar --";
                    ter.Id_Ter = -1;
                    listaTerritorios.Insert(0, ter);
                    this.CargarComboTerritorios2(listaTerritorios, id_territorio);
                }
                txtTerritorio.Focus();
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
        private void LLenarFormNotaCredito(int Id_Emp, int Id_Cd, int Id_Ncr, string Id_NcrSerie)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            NotaCredito notaCredito = new NotaCredito();
            notaCredito.Id_Emp = Id_Emp;
            notaCredito.Id_Cd = Id_Cd;
            notaCredito.Id_Ncr = Id_Ncr;
            notaCredito.Id_NcrSerie = Id_NcrSerie;
            new CN_CapNotaCredito().ConsultarNotaCredito(ref notaCredito, sesion.Emp_Cnx);
            //Agregar Adendas
            List<AdendaDet> listCabT = new List<AdendaDet>();
            List<AdendaDet> listDetT = new List<AdendaDet>();

            for (int i = rgNotaCreditoDet.Columns.Count; i > 9; i--)
            {
                rgNotaCreditoDet.Columns.RemoveAt(rgNotaCreditoDet.Columns.Count - 1);
            }

            new CN_CapNotaCredito().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Ncr,Id_NcrSerie, "5", "6", ref listCabT, ref listDetT, sesion.Emp_Cnx);

            if (listCabT.Count > 0)
            {
                RadTabStrip1.Tabs[1].Visible = true;
                ListCab = listCabT;
                rgAdendaNotaCredito.Rebind();
            }

            GridBoundColumn boundColumn1;
            foreach (AdendaDet ad in listDetT)
            {
                if (!ListaProductosNotaCredito.Columns.Contains(ad.Id_AdeDet.ToString()))
                {
                    boundColumn1 = new GridBoundColumn();
                    rgNotaCreditoDet.MasterTableView.Columns.Add(boundColumn1);
                    boundColumn1.DataField = ad.Id_AdeDet.ToString();
                    boundColumn1.UniqueName = ad.Id_AdeDet.ToString();
                    boundColumn1.HeaderText = ad.Campo;
                    boundColumn1.HeaderStyle.Width = Unit.Pixel(150);
                    boundColumn1.MaxLength = ad.Longitud;
                    boundColumn1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    ListaProductosNotaCredito.Columns.Add(ad.Id_AdeDet.ToString());
                }
            }

            //CREA BOTON DE EDITAR
            GridEditCommandColumn commandColumn = new GridEditCommandColumn();
            rgNotaCreditoDet.MasterTableView.Columns.Add(commandColumn);
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
            rgNotaCreditoDet.MasterTableView.Columns.Add(deleteColumn);
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

            ListDet = listDetT;
            double ancho = 0;
            foreach (GridColumn gc in rgNotaCreditoDet.Columns)
            {
                ancho = ancho + gc.HeaderStyle.Width.Value;
            }
            rgNotaCreditoDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgNotaCreditoDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgNotaCreditoDet.Rebind();


            cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(notaCredito.Ncr_Tipo.ToString());
            txtFolio.Text = notaCredito.Id_Ncr.ToString();
            txtFecha.SelectedDate = notaCredito.Ncr_Fecha;
            txtFecha.Enabled = false;
            txtMov.Text = notaCredito.Id_Tm.ToString();
            cmbMov.SelectedIndex = cmbMov.FindItemIndexByValue(notaCredito.Id_Tm.ToString());

            if (notaCredito.Ncr_EmpleadoNumNomina != null)
            {
                this.panelEmpleado.Style.Add("display", "block");
                txtEmpleado.Text = notaCredito.Ncr_EmpleadoNumNomina.ToString();
                txtNombreEmpleado.Text = notaCredito.Ncr_EmpleadoNombre.ToString();
                this.HD_PanelVisible.Value = "e";
            }
            else
            {
                if (notaCredito.Ncr_CtaContable != string.Empty)
                {
                    this.panelCuentaContable.Style.Add("display", "block");
                    txtCuentaContable.Text = notaCredito.Ncr_CtaContable;
                    this.HD_PanelVisible.Value = "c";
                }
                else
                    this.HD_PanelVisible.Value = string.Empty;
            }

            cmbMovimientoTipo.SelectedIndex = notaCredito.Ncr_Movimiento == null ? -1 : cmbMovimientoTipo.FindItemIndexByValue(notaCredito.Ncr_Movimiento.ToString());
            cmbMovimientoTipo.Enabled = false;
            txtReferencia.Text = notaCredito.Ncr_Referencia == null ? string.Empty : notaCredito.Ncr_Referencia.ToString();
            txtReferencia.Enabled = false;
            txtSaldo.Text = notaCredito.Ncr_Saldo == null ? "0" : (notaCredito.Ncr_Saldo + notaCredito.Ncr_Total).ToString();
            txtCliente.Text = notaCredito.Id_Cte.ToString();
            //agregar sp consulta.. de cliente
            txtClienteNombre.Text = notaCredito.Cte_NomComercial;
            txtCliente.Enabled = false;

            //consultar datos del cliente
            this.ConsultarDatosCliente(notaCredito.Id_Cte.ToString(), true, notaCredito.Id_Ter);

            if (notaCredito.Id_Cfe == null) cmbConsFacEle.SelectedIndex = 0; else cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue(notaCredito.Id_Cfe.ToString());

            txtTerritorio.Text = notaCredito.Id_Ter.ToString();
            cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(notaCredito.Id_Ter.ToString());
            txtRepresentante.Text = notaCredito.Id_Rik.ToString();
            txtRepresentanteStr.Text = ((Label)cmbTerritorio.SelectedItem.FindControl("lbl_Rik_Nombre")).Text;
            chkDesgloceIva.Checked = Convert.ToBoolean(notaCredito.Ncr_Desgloce);
            IVA.Visible = chkDesgloceIva.Checked;
            chkDesglocePartidasFormato.Checked = Convert.ToBoolean(notaCredito.Ncr_DesglocePartidas);
            txtNotas.Text = notaCredito.Ncr_Notas;
            txtSubtotal.Text = notaCredito.Ncr_Subtotal.ToString();
            txtIva.Text = notaCredito.Ncr_Iva.ToString();
            txtTotal.Text = notaCredito.Ncr_Total.ToString();

            CargarEspecial(Id_Ncr, sesion, notaCredito);

            //ADENDA            
            ConvertiraDt(notaCredito.ListaNotaCredito);
            rgNotaCreditoDet.Rebind();
        }
        private void CargarEspecial(int Id_Ncr, Sesion sesion, NotaCredito notaCredito)
        {
            //- Especial
            List<NotaCreditoDet> listaProdFacturaEspecialFinal = new List<NotaCreditoDet>();
            new CN_CapNotaCredito().ConsultaNotaCreditoEspecialDetalle(ref listaProdFacturaEspecialFinal
                , sesion.Emp_Cnx
                , sesion.Id_Emp
                , sesion.Id_Cd_Ver
                , Convert.ToInt32(Id_Ncr)
                , HDId_NcrSerie.Value
                , (int)notaCredito.Id_Cte);

            if (listaProdFacturaEspecialFinal.Count > 0)
            {
                Session["ListaProductosNotaCreditoEspecial" + Session.SessionID] = listaProdFacturaEspecialFinal;
                Session["NcreditoEspecialGuardada" + Session.SessionID] = 1;
            }
        }
        private void ConvertiraDt(List<NotaCreditoDet> listaNotaDet)
        {
            try
            {
                ArrayList al;
                foreach (NotaCreditoDet notaCargoDet in listaNotaDet)
                {
                    al = new ArrayList();
                    al.Add(notaCargoDet.Id_Emp);
                    al.Add(notaCargoDet.Id_Cd);
                    al.Add(notaCargoDet.Id_Ncr);
                    al.Add(notaCargoDet.Id_NcrDet);
                    al.Add(notaCargoDet.Id_Ter);
                    al.Add(notaCargoDet.Ter_Nombre);
                    al.Add(notaCargoDet.Id_Rik);
                    al.Add(notaCargoDet.Rik_Nombre);
                    al.Add(notaCargoDet.Id_Prd);
                    al.Add(notaCargoDet.Prd_Nombre);
                    al.Add(notaCargoDet.Ncr_Importe);
                    foreach (AdendaDet ad in ListDet)
                    {
                        if (ad.Id_Prd == notaCargoDet.Id_Prd && ad.Id_Ter == notaCargoDet.Id_Ter)
                            al.Add(ad.Valor);
                    }
                    ListaProductosNotaCredito.Rows.Add(al.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private NotaCredito LlenarObjetoNotaCredito()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            NotaCredito notaCredito = new NotaCredito();
            notaCredito.Id_Emp = sesion.Id_Emp;
            notaCredito.Id_Cd = sesion.Id_Cd_Ver;
            notaCredito.Id_Ncr = Convert.ToInt32(txtFolio.Text);

            if (cmbConsFacEle.SelectedValue == "-1") notaCredito.Id_Cfe = null; else notaCredito.Id_Cfe = Convert.ToInt32(cmbConsFacEle.SelectedValue);
            if (cmbConsFacEle.SelectedValue == "-1") notaCredito.Id_NcrSerie = null; else notaCredito.Id_NcrSerie = string.Concat(cmbConsFacEle.Text);

            notaCredito.Id_Reg = null;
            notaCredito.Id_Tm = Convert.ToInt32(cmbMov.SelectedValue);
            if (this.HD_PanelVisible.Value == "e")
            {
                notaCredito.Ncr_EmpleadoNumNomina = Convert.ToInt32(txtEmpleado.Text);
                notaCredito.Ncr_EmpleadoNombre = txtNombreEmpleado.Text;
                notaCredito.Ncr_CtaContable = string.Empty;
            }
            else
            {
                if (this.HD_PanelVisible.Value == "c")
                {
                    notaCredito.Ncr_EmpleadoNumNomina = null;
                    notaCredito.Ncr_EmpleadoNombre = string.Empty;
                    notaCredito.Ncr_CtaContable = txtCuentaContable.Text;
                }
                else
                {
                    notaCredito.Ncr_EmpleadoNumNomina = null;
                    notaCredito.Ncr_EmpleadoNombre = string.Empty;
                    notaCredito.Ncr_CtaContable = string.Empty;
                }
            }
            notaCredito.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
            notaCredito.Id_Rik = Convert.ToInt32(txtRepresentante.Text);
            notaCredito.Id_U = sesion.Id_U;
            int cliente = !string.IsNullOrEmpty(txtCliente.Text) ? Convert.ToInt32(txtCliente.Text) : 0;
            notaCredito.Id_Cte = cliente;
            notaCredito.Ncr_Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
            notaCredito.Ncr_Fecha = Convert.ToDateTime(txtFecha.SelectedDate);
            notaCredito.Ncr_Movimiento = Convert.ToInt32(cmbMovimientoTipo.SelectedValue);
            notaCredito.Ncr_Referencia = Convert.ToInt32(txtReferencia.Text);
            notaCredito.Ncr_Saldo = 0; //Convert.ToInt32(txtSaldo.Text);
            notaCredito.Ncr_Desgloce = chkDesgloceIva.Checked;
            notaCredito.Ncr_DesglocePartidas = chkDesglocePartidasFormato.Checked;
            notaCredito.Ncr_CteDIVA = false;
            notaCredito.Ncr_Notas = txtNotas.Text;
            notaCredito.Ncr_Subtotal = Convert.ToDouble(txtSubtotal.Text);
            notaCredito.Ncr_Iva = Convert.ToDouble(txtIva.Text);
            notaCredito.Ncr_Total = Convert.ToDouble(txtTotal.Text);
            notaCredito.Ncr_Pagado = Convert.ToDouble(txtTotal.Text);
            notaCredito.Ncr_FecPagado = DateTime.Now;
            notaCredito.Ncr_Estatus = "C";
            notaCredito.Ncr_ReferenciaSerie = cmbConsFacEle.Text;
            return notaCredito;
        }
        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadComboBoxItem item = ((RadComboBox)cmbMov).SelectedItem;
                Label lbl_AfeVta = (Label)item.FindControl("lbl_AfeVta");
                NotaCredito notaCredito = this.LlenarObjetoNotaCredito();
                string mensaje = string.Empty;
                RadTextBox txtAdenda = new RadTextBox();
                if (lbl_AfeVta.Text.ToUpper() == "TRUE" && this.ListaProductosNotaCredito.Rows.Count == 0)
                    this.DisplayMensajeAlerta("CapturarProductosRequerido");
                else
                { //ADENDA
                    List<AdendaDet> listAdendaCabecera = new List<AdendaDet>();
                    AdendaDet ad;
                    for (int i = 0; i < rgAdendaNotaCredito.MasterTableView.Items.Count; i++)
                    {
                        ad = new AdendaDet();
                        ad.Tipo = Convert.ToInt32(rgAdendaNotaCredito.MasterTableView.Items[i]["Tipo"].Text);
                        ad.Id_AdeDet = Convert.ToInt32(rgAdendaNotaCredito.MasterTableView.Items[i]["Id_AdeDet"].Text);
                        ad.Campo = rgAdendaNotaCredito.MasterTableView.Items[i]["campo"].Text;
                        ad.Nodo = rgAdendaNotaCredito.MasterTableView.Items[i]["nodo"].Text;
                        txtAdenda = rgAdendaNotaCredito.MasterTableView.Items[i]["valor"].FindControl("RadTextBox1") as RadTextBox;
                        ad.Valor = txtAdenda.Text.Replace("'", "").Trim();
                        bool addenda_Requerida = ListCab.Where(AdendaDet => AdendaDet.Id_AdeDet == ad.Id_AdeDet).ToList()[0].Requerido;
                        if (ad.Valor == "" && addenda_Requerida)
                        {
                            AlertaFocus("El campo <b>" + ad.Campo + "</b> de la addenda es requerido", txtAdenda.ClientID);
                            RadTabStrip1.Tabs[1].Selected = true;
                            rpvAdendaNCredito.Selected = true;
                            return;
                        }
                        else
                        {
                            listAdendaCabecera.Add(ad);
                        }
                    }
                    // Evita que se guarde el documento si los totales no coinciden
                    if (Session["ListaProductosNotaCreditoEspecial" + Session.SessionID] != null)
                    {
                        //if (Session["NcreditoEspecialGuardada" + Session.SessionID].ToString() == "1")
                        //{
                            double totalEspecial = 0;
                            foreach (NotaCreditoDet ncd in (List<NotaCreditoDet>)Session["ListaProductosNotaCreditoEspecial" + Session.SessionID])
                            {
                                totalEspecial += ncd.Ncr_Importe;
                            }

                            bool bEncontrados = true;

                            //Buscar que todos los Id´s de productos de la nota de credito especial estén también en la nota de credito detalle.
                            foreach (NotaCreditoDet f in (List<NotaCreditoDet>)Session["ListaProductosNotaCreditoEspecial" + Session.SessionID])
                            {
                                bEncontrados = false;
                                for (int m = 0; m < ListaProductosNotaCredito.Rows.Count; m++)
                                {
                                    if (ListaProductosNotaCredito.Rows[m]["Id_Prd"].ToString() == f.Id_Prd.ToString())
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
                                Alerta("La nota de crédito especial contiene partidas con productos distintos a la nota de crédito original.");
                                return;
                            }

                            //Datos del centro de distribución (Para obtener la tolerancia o diferencia permitida entre totales de fac y fact especial)
                            CentroDistribucion cd = new CentroDistribucion();
                            new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                            // Se indico que solo podía haber diferecia de 90 centavos
                            double TE1 = (Math.Round(txtSubtotal.Value.Value, 2) + Math.Round((double)cd.Cd_MargenDiferenciaDocs, 2)); // Math.Round(totalEspecial, 2) + .90; // se suman 70 centavos al total especial -- modificar si se desea disminuir o aumentar el rango
                            double TE2 = (Math.Round(txtSubtotal.Value.Value, 2) - Math.Round((double)cd.Cd_MargenDiferenciaDocs, 2)); // se restan 70 centavos al total especia

                            if (TE1 < Math.Round(totalEspecial, 2) || TE2 > Math.Round(totalEspecial, 2))
                            {
                                Alerta("Los montos de la Nota de Crédito y la Nota de Crédito Especial tienen una diferencia considerable, favor de revisarlos.");
                                return;
                            }
                        //}
                    }

                    if (notaCredito.Ncr_Subtotal == 0)
                    {
                        Alerta("El total de la nota de crédito no puede ser cero");
                        return;
                    }

                    int verificador = 0;
                    if (this.hiddenId.Value == string.Empty) //nueva nota de cargo
                    {
                        new CN_CapNotaCredito().InsertarNotaCredito(ref notaCredito, sesion.Emp_Cnx, ref verificador, listAdendaCabecera, ListaProductosNotaCredito);
                        mensaje = "Los datos se guardaron correctamente";
                    }
                    else
                    {
                        new CN_CapNotaCredito().ModificarNotaCredito(ref notaCredito, sesion.Emp_Cnx, ref verificador, listAdendaCabecera, ListaProductosNotaCredito);
                        mensaje = "Los datos se modificaron correctamente";
                    }
                    //SI GUARDÓ BIEN LA NOTA DE CREDITO:
                    //Guardar los datos de los productos de NCredito especial
                    verificador = 0;
                    if (Session["ListaProductosNotaCreditoEspecial" + Session.SessionID] != null)
                    {
                        if (Session["NcreditoEspecialGuardada" + Session.SessionID].ToString() == "1") //guarda solo si hizo clic en guardar en pantalla de facturaEspecial.
                        {
                            FacturaEspecial facturaEsp = new FacturaEspecial();
                            facturaEsp.Id_Emp = notaCredito.Id_Emp;
                            facturaEsp.Id_Cd = notaCredito.Id_Cd;
                            facturaEsp.Id_Fac = notaCredito.Id_Ncr;
                            facturaEsp.Id_FacSerie = notaCredito.Id_NcrSerie + "" + notaCredito.Id_Ncr;
                            facturaEsp.Id_Ter = Convert.ToInt32(notaCredito.Id_Ter);
                            facturaEsp.FacEsp_Fecha = notaCredito.Ncr_Fecha;
                            facturaEsp.FacEsp_Importe = Convert.ToDouble(notaCredito.Ncr_Total);
                            facturaEsp.FacEsp_SubTotal = Convert.ToDouble(notaCredito.Ncr_Subtotal);
                            facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(notaCredito.Ncr_Iva);
                            facturaEsp.FacEsp_Total = Convert.ToDouble(notaCredito.Ncr_Total);
                            List<NotaCreditoDet> listaProductosFacturaEspecial = (List<NotaCreditoDet>)Session["ListaProductosNotaCreditoEspecial" + Session.SessionID];
                            new CN_CatClienteProd().ModificarNCreditoEspecial(ref facturaEsp, ref listaProductosFacturaEspecial, sesion.Emp_Cnx, ref verificador, string.IsNullOrEmpty(this.hiddenId.Value) ? 0 : 1);
                        }
                    }
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapNotaCredito", "Id_Ncr", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string funcion;
                    funcion = "CloseAndRebind()";
                    string script = "<script>" + funcion + "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                    return "";
                }
                else
                    throw ex;
            }
        }
        private void CargarConsFactElectronica()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 3, sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbConsFacEle);
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
            new CN_CapNotaCredito().ConsultarMovsNotaCredito(mov, ref listaMovimientos, sesion.Emp_Cnx);
            cmbMov.DataTextField = "Nombre";
            cmbMov.DataValueField = "Id";
            cmbMov.DataSource = listaMovimientos;
            cmbMov.DataBind();
        }
        private void CargarComboTerritorios(List<Territorios> lista, int? id_terr = 1)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            cmbTerritorio.DataTextField = "Descripcion";
            cmbTerritorio.DataValueField = "Id_Ter";
            cmbTerritorio.DataSource = lista;
            cmbTerritorio.DataBind();
          
            if (cmbTerritorio.Items.Count > 1)
            {
                int idTer;
                if (id_terr.HasValue) {
                    idTer = (int)id_terr;
                } else {
                    idTer = 1;
                }

                //cmbTerritorio.Text = cmbTerritorio.(idTer.ToString());
                cmbTerritorio.Text = cmbTerritorio.Items.FindItemByValue(idTer.ToString()).Text;
                txtTerritorio.Text = Convert.ToString(idTer);

                CN_CatTerritorios territorio = new CN_CatTerritorios();
                Territorios ter = new Territorios();
                ter.Id_Emp = sesion.Id_Emp;
                ter.Id_Cd = sesion.Id_Cd_Ver;
                ter.Id_Ter = idTer;
                territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                txtRepresentante.Text = ter.Id_Rik.ToString();
                txtRepresentanteStr.Text = ter.Rik_Nombre;
            }

        }
        private void CargarComboTerritorios2(List<Territorios> lista, int id_territorio, int? id_terr = 1)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            cmbTerritorio.DataTextField = "Descripcion";
            cmbTerritorio.DataValueField = "Id_Ter";
            cmbTerritorio.DataSource = lista;
            cmbTerritorio.DataBind();

            if (cmbTerritorio.Items.Count > 1)
            {
                int idTer;
                if (id_terr.HasValue)
                {
                    idTer = (int)id_terr;
                }
                else
                {
                    idTer = 1;
                }
                cmbTerritorio.SelectedIndex = id_territorio;
                //cmbTerritorio.Text = cmbTerritorio.FindItemIndexByValue (idTer.ToString());
                //cmbTerritorio.Text = cmbTerritorio.Items.FindItemByValue(idTer.ToString()).Text;
                txtTerritorio.Text = Convert.ToString(id_territorio);

                CN_CatTerritorios territorio = new CN_CatTerritorios();
                Territorios ter = new Territorios();
                ter.Id_Emp = sesion.Id_Emp;
                ter.Id_Cd = sesion.Id_Cd_Ver;
                ter.Id_Ter = idTer;
                territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                txtRepresentante.Text = ter.Id_Rik.ToString();
                txtRepresentanteStr.Text = ter.Rik_Nombre;
            }

        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("txtReferencia_TextChanged"))
                    Alerta("Error al consultar los datos del movimiento de referencia");
                else
                    if (mensaje.Contains("cmbConsFacEle_ObtenerConsFacElectFallo"))
                        Alerta("Error al momento de obtener el consecutivo de facturación electrónica");
                    else
                        if (mensaje.Contains("MovFacturaNotaCargo_NoExiste"))
                            Alerta("El movimiento no fue encontrado");
                        else
                            if (mensaje.Contains("MovFacturaNotaCargo_NoSaldo"))
                                Alerta("El movimiento no tiene saldo, imposible aplicarle una nota de crédito");
                            else
                                if (mensaje.Contains("MovFacturaNotaCargo_Baja"))
                                    Alerta("La factura se encuentra cancelada; imposible aplicarle una nota de crédito");
                                else
                                    if (mensaje.Contains("MovFacturaNotaCargo_Captura"))
                                        Alerta("La factura se encuentra en estatus capturado; imposible aplicarle una nota de crédito");
                                    else
                                        if (mensaje.Contains("Movimiento_AplicaNotCredDenegado"))
                                            Alerta("Movimiento no se puede aplicar directamente, es necesario que se aplique en Inventarios-Capturas-Devoluciones Parciales");
                                        else
                                            if (mensaje.Contains("CapNotaCredito_MovTipoRequerido"))
                                                Alerta("Favor de capturar el tipo de movimiento para consultar los datos del cliente");
                                            else
                                                if (mensaje.Contains("rgNotaCreditoDet_insert_repetida"))
                                                    Alerta("Este producto ya ha sido capturado");
                                                else
                                                    if (mensaje.Contains("CapturarProductosRequerido"))
                                                        Alerta("El tipo de movimiento de la nota de crédito afecta venta, debe capturar al menos un producto en el grid de la pestaña \"detalles\"");
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
                                                                    if (mensaje.Contains("CapNotaCredito_insert_ok"))
                                                                        Alerta("Los datos se guardaron correctamente");
                                                                    else
                                                                        if (mensaje.Contains("CapNotaCredito_insert_error"))
                                                                            Alerta(string.Concat("No se pudo guardar la nota de crédito. ", mensaje.Replace("'", "\"")));
                                                                        else
                                                                            if (mensaje.Contains("CapNotaCredito_update_ok"))
                                                                                Alerta("Los datos se modificaron correctamente");
                                                                            else
                                                                                if (mensaje.Contains("CapNotaCredito_update_error"))
                                                                                    Alerta(string.Concat("No se pudo actualizar la nota de crédito. ", mensaje.Replace("'", "\"")));
                                                                                else
                                                                                    if (mensaje.Contains("rgNotaCreditoDet_NeedDataSource"))
                                                                                        Alerta("Error al cargar el grid de detalle de la nota de crédito");
                                                                                    else
                                                                                        if (mensaje.Contains("rgNotaCreditoDet_ItemDataBound"))
                                                                                            Alerta("Error al momento de preparar un registro para edición");
                                                                                        else
                                                                                            if (mensaje.Contains("rgNotaCreditoDet_insert_error"))
                                                                                                Alerta("Error al momento de agregar el producto a la lista de productos de la nota de crédito");
                                                                                            else
                                                                                                if (mensaje.Contains("rgNotaCreditoDet_delete_error"))
                                                                                                    Alerta("Error al momento de eliminar el producto a la lista de productos de la nota de crédito");
                                                                                                else
                                                                                                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #region "Métodos para obtrener desde objetos los valores para los controles durante la inserción/actualización de un Grid editable"
        protected string ObtenerDescripcionProducto(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Prd_Nombre; } else { return string.Empty; }
        }

        protected string ObtenerDescripcionTerritorio(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Ter_Nombre; } else { return string.Empty; }
        }

        protected string ObtenerDescripcionRepresentante(object oc)
        {
            if (oc is NotaCreditoDet) { return ((NotaCreditoDet)oc).Rik_Nombre; } else { return string.Empty; }
        }

        #endregion
        #region "Métodos para manejar la lista dinámica de Productos de la Nota de crédito"

        //protected void ListaProductosNotaCredito_AgregarProducto(NotaCreditoDet notaCredito_nueva)
        //{
        //    List<NotaCreditoDet> lista = this.ListaProductosNotaCredito;

        //    //buscar producto de factura en la lista para ver si ya existe
        //    for (int i = 0; i < lista.Count; i++)
        //    {
        //        NotaCreditoDet notaCargoDet = lista[i];
        //        if (notaCargoDet.Id_Prd == notaCredito_nueva.Id_Prd)//si el producto es el mismo
        //        {
        //            throw new Exception("rgNotaCreditoDet_insert_repetida");
        //        }
        //    }
        //    lista.Add(notaCredito_nueva);
        //    this.ListaProductosNotaCredito = lista;
        //    this.CalcularTotales();
        //}

        //protected void ListaProductosNotaCredito_ModificarProducto(NotaCreditoDet notaCredito_nueva)
        //{
        //    List<NotaCreditoDet> lista = this.ListaProductosNotaCredito;

        //    //buscar producto de factura en la lista
        //    for (int i = 0; i < lista.Count; i++)
        //    {
        //        NotaCreditoDet notaCargoDet = lista[i];
        //        if (notaCargoDet.Id_Prd == notaCredito_nueva.Id_Prd)
        //        {
        //            lista[i] = notaCredito_nueva;
        //            break;
        //        }
        //    }
        //    this.ListaProductosNotaCredito = lista;
        //    this.CalcularTotales();
        //}

        //protected void ListaProductosNotaCredito_EliminarProducto(int id_Prd)
        //{
        //    List<NotaCreditoDet> lista = this.ListaProductosNotaCredito;

        //    //buscar producto de factura en la lista
        //    for (int i = 0; i < lista.Count; i++)
        //    {
        //        NotaCreditoDet notaCargoDet = lista[i];
        //        if (notaCargoDet.Id_Prd == id_Prd)
        //        {
        //            lista.RemoveAt(i);
        //            break;
        //        }
        //    }
        //    this.ListaProductosNotaCredito = lista;
        //    this.CalcularTotales();
        //}

        //protected void ListaProductosFactura_EliminarTodos()
        //{
        //    this.ListaProductosFactura = new List<FacturaDet>();
        //}

        private void CalcularTotales()
        {
            double importeTotal = 0;
            for (int i = 0; i < ListaProductosNotaCredito.Rows.Count; i++)
            {
                importeTotal += Convert.ToDouble(ListaProductosNotaCredito.Rows[i]["Ncr_Importe"]);
            }
            txtSubtotal.Text = importeTotal.ToString();
            txtIva.Text = HD_IVAfacturacion.Value.Trim() != string.Empty ? (importeTotal * (Convert.ToSingle(HD_IVAfacturacion.Value.Trim()) / 100)).ToString() : "0";
            txtTotal.Text = (Convert.ToDouble(txtSubtotal.Text) + Convert.ToDouble(txtIva.Text)).ToString();
            Session["fTotalNC" + Session.SessionID] = txtTotal.Text;
        }
        #endregion
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
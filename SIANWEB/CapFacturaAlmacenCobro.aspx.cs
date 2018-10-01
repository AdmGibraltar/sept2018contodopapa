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
using System.Data.SqlClient;
using CapaDatos;

namespace SIANWEB
{
    public partial class CapFacturaAlmacenCobro : System.Web.UI.Page 
    {
        #region Variables

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string CtrlCabezera
        {
            get
            {
                object cabezera = Session["Sesion" + Session.SessionID + "CtrlCabezera"];
                return cabezera == null ? "" : cabezera.ToString();
            }
            set
            {
                Session["Sesion" + Session.SessionID + "CtrlCabezera"] = value;
            }
        }
        private int Id_Fac;
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        private List<FacturaAlmacenCobroDet> ListaFacturaAlmacenCobro
        {
            get { return (List<FacturaAlmacenCobroDet>)Session[Session.SessionID + "ListaFacturaAlmacenCobroDet"]; }
            set { Session[Session.SessionID + "ListaFacturaAlmacenCobroDet"] = value; }
        }

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion != null)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["_IdProducto"] = 0;

                        //obtener valores desde la URL
                        Id_Fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                        _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                        _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                        _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;

                        this.Inicializar(Id_Emp, Id_Cd, Id_Fac);
                        rgFacturaAlmacenCobroDet.Rebind();

                        if (Request.QueryString["Estatus"].ToString() != "C" && Request.QueryString["Estatus"].ToString() != "undefined")
                        {
                            txtEntrego.Enabled = false;
                            txtRecibio.Enabled = false;
                            dpFecha.Enabled = false;
                            btnBuscar.Enabled = false;

                            rgFacturaAlmacenCobroDet.Columns[rgFacturaAlmacenCobroDet.Columns.FindByUniqueName("EditCommandColumn").OrderIndex - 2].Display = false;
                            rgFacturaAlmacenCobroDet.Columns[rgFacturaAlmacenCobroDet.Columns.FindByUniqueName("DeleteColumn").OrderIndex - 2].Display = false;

                            //GridCommandItem cmdItem = (GridCommandItem)rgFacturaAlmacenCobroDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            //cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                            rgFacturaAlmacenCobroDet.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;



                            RadToolBar1.Items[1].Visible = false;
                            BtnConfirmarTodos.Visible = true;

                          //  rgFacturaAlmacenCobroDet.Columns[rgFacturaAlmacenCobroDet.Columns.FindByUniqueName("Incluir").OrderIndex - 2].Display = false;
                            //  rgFacturaAlmacenCobroDet.Columns[rgFacturaAlmacenCobroDet.Columns.FindByUniqueName("Confirmar").OrderIndex - 2].Display = true;
                        }
                        else
                        {
                            rgFacturaAlmacenCobroDet.Columns[rgFacturaAlmacenCobroDet.Columns.FindByUniqueName("Confirmar").OrderIndex - 2].Display = false;
                            BtnConfirmarTodos.Visible = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgFacturaAlmacenCobroDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaAlmacenCobroDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaAlmacenCobroDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        rgFacturaAlmacenCobroDet.Rebind();
                    }
                }
                else
                {
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseAndRebind()"));

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Fac)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Session["ListaFacturaAlmacenCobroDet"] = new List<FacturaAlmacenCobroDet>();


            //establece valores de controles de inicio
            if (Id_Emp > 0 && Id_Cd > 0 && Id_Fac > 0)
            {
                this.LLenarFormFacturaAlmacenCobro(Id_Emp, Id_Cd, Id_Fac);
                this.hiddenId.Value = Id_Fac.ToString();
            }
            else //Nueva
            {
                Funciones funcion = new Funciones();
                dpFecha.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                dpFechaFin.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                this.LLenarFormFacturaAlmacenCobro_sugerido(Id_Emp, Id_Cd, Id_Fac);
                this.hiddenId.Value = string.Empty;
            }
        }

        #region "Métodos para manejar la lista dinámica de Productos de la Nota de crédito"

        protected void ListaFacturaAlmacenCobro_Agregar(FacturaAlmacenCobroDet FacturaAlmacenCobro_nueva)
        {

            int repetido = ListaFacturaAlmacenCobro.Where(FacturaAlmacenCobro => FacturaAlmacenCobro.Fac_Doc == FacturaAlmacenCobro_nueva.Fac_Doc && FacturaAlmacenCobro.Fac_Tipo == FacturaAlmacenCobro_nueva.Fac_Tipo).ToList().Count;

            if (repetido > 0)
            {
                Alerta("Este documento ya ha sido capturado");
            }
            else
            {
                ListaFacturaAlmacenCobro.Add(FacturaAlmacenCobro_nueva);
            }
            //List<FacturaAlmacenCobroDet> lista = this.ListaFacturaAlmacenCobro;

            //buscar en la lista para ver si ya existe
            //for (int i = 0; i < lista.Count; i++)
            //{
            //    FacturaAlmacenCobroDet FacturaAlmacenCobro = lista[i];
            //    if (FacturaAlmacenCobro.Fac_Doc == FacturaAlmacenCobro_nueva.Fac_Doc && FacturaAlmacenCobro.Fac_Tipo == FacturaAlmacenCobro_nueva.Fac_Tipo)//si el documento es el mismo
            //    {
            //        throw new Exception("rgFacturaAlmacenCobroDet_insert_repetida");
            //    }
            //}

            //this.ListaFacturaAlmacenCobro = lista;
        }

        protected void ListaFacturaAlmacenCobro_Modificar(FacturaAlmacenCobroDet FacturaAlmacenCobro_nueva, string doc_old)
        {
            List<FacturaAlmacenCobroDet> lista = this.ListaFacturaAlmacenCobro;

            //buscar producto de factura en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                FacturaAlmacenCobroDet notaCargoDet = lista[i];
                if (notaCargoDet.Fac_Doc.ToString() == doc_old && notaCargoDet.Fac_Tipo == FacturaAlmacenCobro_nueva.Fac_Tipo)
                {
                    lista[i] = FacturaAlmacenCobro_nueva;
                    break;
                }
            }
            this.ListaFacturaAlmacenCobro = lista;
        }

        protected void ListaFacturaAlmacenCobro_Eliminar(int Fac_Doc, string Fac_Tipo)
        {
            List<FacturaAlmacenCobroDet> lista = this.ListaFacturaAlmacenCobro;

            //buscar producto de factura en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                FacturaAlmacenCobroDet FacturaAlmacenCobroDetDet = lista[i];
                if (FacturaAlmacenCobroDetDet.Fac_Doc == Fac_Doc && FacturaAlmacenCobroDetDet.Fac_Tipo == Fac_Tipo)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaFacturaAlmacenCobro = lista;
        }

        #endregion

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string mensajeError = string.Empty;
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgFacturaAlmacenCobroDet.Rebind();
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 100);
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDetalles.Width;
                        RadSplitter1.Height = altura;
                        rgFacturaAlmacenCobroDet.Rebind();
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
                        mensajeError = hiddenId.Value == string.Empty ? "CapFacturaAlmacenCobro_insert_error" : "CapFacturaAlmacenCobro_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaAlmacenCobroDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgFacturaAlmacenCobroDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "Confirmar":
                        int item = e.Item.ItemIndex;
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_FacturasEntrega clsFactura = new CN_FacturasEntrega();
                        FacturaEntrega facturas = new FacturaEntrega();
                        facturas.Id_Fac = Convert.ToInt32((rgFacturaAlmacenCobroDet.Items[item].FindControl("lblFac_Doc") as Label).Text);
                        facturas.Pedido = -1;
                        facturas.DbName = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;
                        int verificador = -1;
                      //  clsFactura.ModificarFacturasEntregaCob(sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_U, facturas, Emp_CnxCob, ref verificador);                  
                        CN_CapFacturaAlmacenCobro cn_capfacturaalmcobro = new CN_CapFacturaAlmacenCobro();
                        FacturaAlmacenCobro almcob = new FacturaAlmacenCobro();
                        almcob.Id_Emp = sesion.Id_Emp;
                        almcob.Id_Cd = sesion.Id_Cd_Ver;
                        almcob.Id_AlmCob = Convert.ToInt32(hiddenId.Value);
                        almcob.Id_Fac = Convert.ToInt32((rgFacturaAlmacenCobroDet.Items[item].FindControl("lblFac_Doc") as Label).Text);
                        cn_capfacturaalmcobro.Confirmar(almcob, sesion.Emp_Cnx);


                        int Id_Fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        LLenarFormFacturaAlmacenCobro(Id_Emp, Id_Cd, Id_Fac);
                        rgFacturaAlmacenCobroDet.Rebind();

                        if (verificador == 1)
                            Alerta("La factura <b># " + almcob.Id_Fac + "</b> fue entregada correctamente");
                        else
                            Alerta("No se pudo autorizar la factura");

                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaAlmacenCobroDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgFacturaAlmacenCobroDet.DataSource = this.ListaFacturaAlmacenCobro;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaAlmacenCobroDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = string.Empty;
                    CheckBox check = default(CheckBox);

                    try
                    {
                        Button = (WebControl)item["Confirmar"].Controls[0];
                            if (Convert.ToBoolean(item.GetDataKeyValue("Fac_Confirmado")))
                            {
                                Button.Visible = false;
                                Image img = new Image();
                                img.CssClass = "aceptar";
                                img.ImageUrl = "~/Imagenes/blank.png";
                                img.ToolTip = "Confirmado";
                                img.AlternateText = "Confirmado";
                                (Button.Parent as GridTableCell).Controls.Add(img);
                                //(Button.Parent as GridTableCell).Text = "CONFIRMADO";
                            }
                            else
                            {
                                Button.Visible = false;
                                Image img = new Image();
                                img.CssClass = "amarillo";
                                img.ImageUrl = "~/Imagenes/blank.png";
                                img.ToolTip = "No Confirmado";
                                img.AlternateText = "No Confirmado";
                                (Button.Parent as GridTableCell).Controls.Add(img);

                                /*
                                clickHandler = Button.Attributes["onclick"];
                                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Fac_Doc").ToString());*/

                            }
                    }
                    catch
                    {
                    }

                    try
                    {
                        check = (CheckBox)item["Incluir"].Controls[1];
                        check.Checked = Convert.ToBoolean(item.GetDataKeyValue("Fac_Seleccionado"));
                        check.Enabled = Convert.ToBoolean(item.GetDataKeyValue("Fac_Confirmado")) ? false : true; 

                        clickHandler = check.Attributes["onclick"];
                        check.Attributes["onclick"] = "return actualizar_tabla(" + item.GetDataKeyValue("Fac_Doc").ToString() + ", this.checked);";// clickHandler.Replace("[[ID]]", );

                    }
                    catch
                    {
                    }

                }
                else if (e.Item is GridHeaderItem)
                {
                    GridHeaderItem item = (GridHeaderItem)e.Item;
                    string clickHandler = string.Empty;
                    CheckBox check = default(CheckBox);
                    try
                    {
                        check = (CheckBox)item["Incluir"].Controls[1];
                        clickHandler = check.Attributes["onclick"];
                        check.Attributes["onclick"] = "return CheckAllIncluir(this);";

                        int seleccionados = ListaFacturaAlmacenCobro.Where(FacturaAlmacenCobroDet => FacturaAlmacenCobroDet.Fac_Seleccionado == true).ToList().Count;
                        int total = ListaFacturaAlmacenCobro.Count;

                        check.Checked = seleccionados == total;

                        CtrlCabezera = check.ClientID;
                    }
                    catch
                    {
                    }
                }

                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    string cmbTipo = ((RadComboBox)editItem.FindControl("cmbTipo")).ClientID.ToString();
                    string lblVal_cmbTipo = ((Label)editItem.FindControl("lblVal_cmbTipo")).ClientID.ToString();
                    string txtFac_Doc = ((RadNumericTextBox)editItem.FindControl("txtFac_Doc")).ClientID.ToString();
                    string lblVal_txtFac_Doc = ((Label)editItem.FindControl("lblVal_txtFac_Doc")).ClientID.ToString();
                    string cmbFac_EnviarA = ((RadComboBox)editItem.FindControl("cmbFac_EnviarA")).ClientID.ToString();
                    string lblVal_cmbFac_EnviarA = ((Label)editItem.FindControl("lblVal_cmbFac_EnviarA")).ClientID.ToString();


                    string jsControles = string.Concat(
                        "cmbTipoClientID='", cmbTipo, "';"
                        , "lblVal_cmbTipoClientID='", lblVal_cmbTipo, "';"
                        , "txtFac_DocClientID='", txtFac_Doc, "';"
                        , "lblVal_txtFac_DocClientID='", lblVal_txtFac_Doc, "';"
                        , "cmbFac_EnviarAClientID='", cmbFac_EnviarA, "';"
                        , "lblVal_cmbFac_EnviarAClientID='", lblVal_cmbFac_EnviarA, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                        ((RadComboBox)editItem.FindControl("cmbTipo")).Focus();
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {

                        int Fac_Doc = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Fac_Doc"]);
                        foreach (FacturaAlmacenCobroDet FacDet in this.ListaFacturaAlmacenCobro)
                        {
                            if (FacDet.Fac_Doc == Fac_Doc)
                            {
                                ((RadComboBox)editItem.FindControl("cmbTipo")).SelectedIndex =
                                    ((RadComboBox)editItem.FindControl("cmbTipo")).FindItemIndexByValue(FacDet.Fac_Tipo);
                                ((RadComboBox)editItem.FindControl("cmbFac_EnviarA")).SelectedIndex =
                                    ((RadComboBox)editItem.FindControl("cmbFac_EnviarA")).FindItemIndexByValue(FacDet.Fac_EnviarA.ToString());
                            }
                        }

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                        ((RadComboBox)editItem.FindControl("cmbFac_EnviarA")).Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaAlmacenCobroDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaAlmacenCobroDet FacturaAlmacenCobroDet = new FacturaAlmacenCobroDet();

                FacturaAlmacenCobroDet.Id_Emp = sesion.Id_Emp;
                FacturaAlmacenCobroDet.Id_Cd = sesion.Id_Cd_Ver;
                FacturaAlmacenCobroDet.Id_Fac = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                FacturaAlmacenCobroDet.Id_FacDet = 0;
                FacturaAlmacenCobroDet.Id_Reg = null;
                FacturaAlmacenCobroDet.Fac_Doc = Convert.ToInt32((insertedItem.FindControl("txtFac_Doc") as RadNumericTextBox).Text);
                FacturaAlmacenCobroDet.Fac_Fecha = Convert.ToDateTime((insertedItem.FindControl("txtFac_Fecha") as RadDatePicker).SelectedDate);
                FacturaAlmacenCobroDet.Id_Cte = Convert.ToInt32((insertedItem.FindControl("lblId_CteEdit") as Label).Text);
                FacturaAlmacenCobroDet.Cte_NomComercial = (insertedItem.FindControl("lblId_CteStrEdit") as Label).Text;
                FacturaAlmacenCobroDet.Fac_Importe = Convert.ToDouble((insertedItem.FindControl("txtFac_ImporteEdit") as RadNumericTextBox).Text);
               // FacturaAlmacenCobroDet.Fac_EnviarA = Convert.ToInt32((insertedItem.FindControl("cmbFac_EnviarA") as RadComboBox).SelectedValue);
               // FacturaAlmacenCobroDet.Fac_EnviarAStr = (insertedItem.FindControl("cmbFac_EnviarA") as RadComboBox).SelectedItem.Text;
                FacturaAlmacenCobroDet.Fac_Seleccionado = (insertedItem.FindControl("chkIncluirEditar") as CheckBox).Checked;
                //agregar producto de nota de cargo a la lista
                this.ListaFacturaAlmacenCobro_Agregar(FacturaAlmacenCobroDet);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaAlmacenCobroDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaAlmacenCobroDet FacturaAlmacenCobroDet = new FacturaAlmacenCobroDet();

                FacturaAlmacenCobroDet.Id_Emp = sesion.Id_Emp;
                FacturaAlmacenCobroDet.Id_Cd = sesion.Id_Cd_Ver;
                FacturaAlmacenCobroDet.Id_Fac = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                FacturaAlmacenCobroDet.Id_FacDet = 0;
                FacturaAlmacenCobroDet.Id_Reg = null;


                FacturaAlmacenCobroDet.Fac_Doc =
                    Convert.ToInt32((insertedItem["Fac_Doc"].FindControl("txtFac_Doc") as RadNumericTextBox).Text);
                FacturaAlmacenCobroDet.Fac_Fecha =
                    Convert.ToDateTime((insertedItem["Fac_Fecha"].FindControl("txtFac_Fecha") as RadDatePicker).SelectedDate);
                FacturaAlmacenCobroDet.Id_Cte =
                    Convert.ToInt32((insertedItem["Id_Cte"].FindControl("lblId_CteEdit") as Label).Text);
                FacturaAlmacenCobroDet.Cte_NomComercial = (insertedItem["Id_CteStr"].FindControl("lblId_CteStrEdit") as Label).Text;
                FacturaAlmacenCobroDet.Fac_Importe =
                    Convert.ToDouble((insertedItem["Fac_Importe"].FindControl("txtFac_ImporteEdit") as RadNumericTextBox).Text);
                FacturaAlmacenCobroDet.Fac_EnviarA =
                    Convert.ToInt32((insertedItem["Fac_EnviarA"].FindControl("cmbFac_EnviarA") as RadComboBox).SelectedValue);
                FacturaAlmacenCobroDet.Fac_EnviarAStr = (insertedItem["Fac_EnviarA"].FindControl("cmbFac_EnviarA") as RadComboBox).SelectedItem.Text;
                string doc_old = (insertedItem["Fac_Doc"].FindControl("lblVal_txtFac_Doc") as Label).Text;
                //actualizar producto de nota de cargo a la lista
                this.ListaFacturaAlmacenCobro_Modificar(FacturaAlmacenCobroDet, doc_old);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaAlmacenCobroDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int Fac_Doc = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Fac_Doc"]);
                string Fac_Tipo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Fac_Tipo"].ToString();
                //eliminar producto de nota de cargo a la lista
                this.ListaFacturaAlmacenCobro_Eliminar(Fac_Doc, Fac_Tipo);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaAlmacenCobroDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgFacturaAlmacenCobroDet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void txtFac_Doc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConsultaDatosDocumento(sender);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.ConsultaDatosDocumento(sender);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion

        #region Funciones

        private void ConsultaDatosDocumento(object sender)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Telerik.Web.UI.GridTableCell tabla = new Telerik.Web.UI.GridTableCell();

                if (sender is RadNumericTextBox)
                {
                    tabla = (Telerik.Web.UI.GridTableCell)((RadNumericTextBox)sender).Parent;
                }
                if (sender is RadComboBox)
                {
                    tabla = (Telerik.Web.UI.GridTableCell)((RadComboBox)sender).Parent;
                }

                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                RadDatePicker txtFac_Fecha = ((RadDatePicker)tabla.FindControl("txtFac_Fecha"));
                Label lblId_CteEdit = ((Label)tabla.FindControl("lblId_CteEdit"));
                Label lblId_CteStrEdit = ((Label)tabla.FindControl("lblId_CteStrEdit"));
                RadNumericTextBox txtFac_ImporteEdit = ((RadNumericTextBox)tabla.FindControl("txtFac_ImporteEdit"));

                txtFac_Fecha.SelectedDate = null;
                lblId_CteEdit.Text = string.Empty;
                lblId_CteStrEdit.Text = string.Empty;
                txtFac_ImporteEdit.Text = string.Empty;
                int Verificador = 0;
             



                RadNumericTextBox txtFac_Doc = ((RadNumericTextBox)tabla.FindControl("txtFac_Doc"));

                try
                {
                    CN_CapFacturaAlmacenCobro cn_facalm = new CN_CapFacturaAlmacenCobro();
                    Factura factura = new Factura();
                    factura.Id_Emp = sesion.Id_Emp;
                    factura.Id_Cd = sesion.Id_Cd_Ver;
                    factura.Id_Fac = Convert.ToInt32(txtFac_Doc.Text);
                    cn_facalm.ValidaProcesoFacturaAlmacenCobro(factura, ref Verificador, sesion.Emp_Cnx);


                }
                catch (Exception ex)
                {
                    Alerta(ex.Message);
                    txtFac_Doc.Text = string.Empty;
                    txtFac_Doc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtFac_Doc.Text))
                {
                    //this.DisplayMensajeAlerta("rgFacturaAlmacenCobroDet_IntroducirNumDocumento");
                    txtFac_Doc.Focus();
                }
                else
                {
                    bool encontrado = false;

                    Factura factura = new Factura();
                    factura.Id_Emp = sesion.Id_Emp;
                    factura.Id_Cd = sesion.Id_Cd_Ver;
                    factura.Id_Fac = Convert.ToInt32(txtFac_Doc.Text);
                    new CN_CapFactura().ConsultaFacturaEncabezado(ref factura, sesion.Emp_Cnx, ref encontrado);

                    if (encontrado)
                    {
                        if (factura.Fac_Saldo <= 0)
                        {
                            txtFac_Doc.Text = string.Empty;
                            txtFac_Doc.Focus();
                            this.DisplayMensajeAlerta("MovFacRevCobro_NoSaldo");
                        }
                        else
                        {
                            if (factura.Fac_Estatus.ToUpper() != "B" && factura.Fac_Estatus.ToUpper() != "C")
                            {
                                txtFac_Fecha.SelectedDate = factura.Fac_Fecha;
                                lblId_CteEdit.Text = factura.Id_Cte.ToString();
                                lblId_CteStrEdit.Text = factura.Cte_NomComercial;
                                txtFac_ImporteEdit.Text = factura.Fac_Saldo.ToString();
                            }
                            else
                            {
                                txtFac_Doc.Text = string.Empty;
                                txtFac_Doc.Focus();
                                this.DisplayMensajeAlerta("MovFacRevCobro_EstatusInvalido");
                            }
                        }
                    }
                    else
                    {
                        this.DisplayMensajeAlerta("rgFacturaAlmacenCobroDet_DocNoEncontrado");
                        txtFac_Doc.Text = string.Empty;
                        txtFac_Doc.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarFormFacturaAlmacenCobro(int Id_Emp, int Id_Cd, int Id_Fac)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            FacturaAlmacenCobro FacturaAlmacenCobro = new FacturaAlmacenCobro();
            FacturaAlmacenCobro.Id_Emp = Id_Emp;
            FacturaAlmacenCobro.Id_Cd = Id_Cd;
            FacturaAlmacenCobro.Id_Fac = Id_Fac;
            FacturaAlmacenCobro.DbName = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog;
            new CN_CapFacturaAlmacenCobro().ConsultarFacturaAlmacenCobro(ref FacturaAlmacenCobro, sesion.Emp_Cnx);
            txtEntrego.Text = FacturaAlmacenCobro.Fac_Entrego;
            txtRecibio.Text = FacturaAlmacenCobro.Fac_Recibio;
            this.hiddenId.Value = Id_Fac.ToString();


            int total_facturas = FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet.Count();

            int i = 0;

            foreach (FacturaAlmacenCobroDet fac in FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet)
            {

                if (fac.Fac_Confirmado)
                {
                    i++;
                }
            }
            if (total_facturas == i)
            {
                BtnConfirmarTodos.Enabled = false;
            }


            this.ListaFacturaAlmacenCobro = FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet;
        }
        private void LLenarFormFacturaAlmacenCobro_sugerido(int Id_Emp, int Id_Cd, int Id_Fac)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            FacturaAlmacenCobro facturaAlmacenCobro = new FacturaAlmacenCobro();
            facturaAlmacenCobro.ListaFacturaAlmacenCobroDet = new List<FacturaAlmacenCobroDet>();

            facturaAlmacenCobro.Id_Emp = sesion.Id_Emp;
            facturaAlmacenCobro.Id_Cd = sesion.Id_Cd_Ver;
            facturaAlmacenCobro.Fac_Fecha = dpFecha.SelectedDate.Value;
            facturaAlmacenCobro.Fac_FechaFin = dpFechaFin.SelectedDate.Value;
            facturaAlmacenCobro.DbName = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;
            new CN_CapFacturaAlmacenCobro().ConsultarFacturaAlmacenCobro_Sugerido(ref facturaAlmacenCobro, Emp_CnxCob);

            this.ListaFacturaAlmacenCobro = facturaAlmacenCobro.ListaFacturaAlmacenCobroDet;
        }
        private FacturaAlmacenCobro LlenarObjetoFacturaAlmacenCobro()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            FacturaAlmacenCobro FacturaAlmacenCobro = new FacturaAlmacenCobro();
            FacturaAlmacenCobro.Id_Emp = sesion.Id_Emp;
            FacturaAlmacenCobro.Id_Cd = sesion.Id_Cd_Ver;
            if (this.hiddenId.Value != string.Empty)
            {
                FacturaAlmacenCobro.Id_Fac = Convert.ToInt32(this.hiddenId.Value);
            }
            else
            {
                FacturaAlmacenCobro.Id_Fac = 0;
            }
            FacturaAlmacenCobro.Id_Reg = null;
            FacturaAlmacenCobro.Id_U = sesion.Id_U;

            FacturaAlmacenCobro.Fac_Entrego = txtEntrego.Text;
            FacturaAlmacenCobro.Fac_Recibio = txtRecibio.Text;
            FacturaAlmacenCobro.Fac_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            FacturaAlmacenCobro.Fac_FecEnvio = null;
            FacturaAlmacenCobro.Fac_FecRecibio = null;
            FacturaAlmacenCobro.Fac_Estatus = "C";

            FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet = ListaFacturaAlmacenCobro.Where(FacturaAlmacenCobroDet => FacturaAlmacenCobroDet.Fac_Seleccionado == true).ToList();
            //foreach (FacturaAlmacenCobroDet fac in FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet)
            //{
            //    FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet.Remove(fac);
            //}
            return FacturaAlmacenCobro;
        }


        protected void BtnConfirmarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion(); 
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];


             
                FacturaAlmacenCobro FacturaAlmacenCobro = this.LlenarObjetoFacturaAlmacenCobro();
                string mensaje = string.Empty;

                int verificador = 0;
                
                if (FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet.Count == 0)
                {
                    this.DisplayMensajeAlerta("rgFacturaAlmacenCobroDet_NoPartidas");
                    return;
                }
                              
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

               
                CN_FacturasEntrega clsFactura = new CN_FacturasEntrega();
                string conexiondb = (new SqlConnectionStringBuilder(Sesion.Emp_Cnx)).InitialCatalog;

                clsFactura.ModificarFacturasEntregaCob(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_U, FacturaAlmacenCobro, Emp_CnxCob, ref verificador, conexiondb);

                CN_CapFacturaAlmacenCobro cn_capfacturaalmcobro = new CN_CapFacturaAlmacenCobro();
                cn_capfacturaalmcobro.Confirmar(FacturaAlmacenCobro, Sesion.Emp_Cnx);


                int Id_Fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                LLenarFormFacturaAlmacenCobro(Id_Emp, Id_Cd, Id_Fac);
                rgFacturaAlmacenCobroDet.Rebind();

                if (verificador == 1)
                {
                    Alerta("Las Facturas seleccionadas fueron confirmadas exitosamente");
                }



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaAlmacenCobro FacturaAlmacenCobro = this.LlenarObjetoFacturaAlmacenCobro();
                string mensaje = string.Empty;

                int verificador = 0;




                if (FacturaAlmacenCobro.ListaFacturaAlmacenCobroDet.Count == 0)
                {
                    this.DisplayMensajeAlerta("rgFacturaAlmacenCobroDet_NoPartidas");
                    return;
                }

                if (this.hiddenId.Value == string.Empty) //nueva nota de cargo
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    new CN_CapFacturaAlmacenCobro().InsertarFacturaAlmacenCobro(ref FacturaAlmacenCobro, sesion.Emp_Cnx, ref verificador);
                    mensaje = "Los datos se guardaron correctamente";
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }

                    new CN_CapFacturaAlmacenCobro().ModificarFacturaAlmacenCobro(ref FacturaAlmacenCobro, sesion.Emp_Cnx, ref verificador);
                    mensaje = "Los datos se modificaron correctamente";
                }
                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboCliente(RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCliente_Combo", ref combo);
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgFacturaAlmacenCobroDet_DocNoEncontrado"))
                    Alerta("El documento no fue encontrado");
                else
                    if (mensaje.Contains("rgFacturaAlmacenCobroDet_NoPartidas"))
                        Alerta("Favor de capturar al menos un documento");
                    else
                        if (mensaje.Contains("rgFacturaAlmacenCobroDet_IntroducirNumDocumento"))
                            Alerta("Favor de introducir el número de documento");
                        else
                            if (mensaje.Contains("rgFacturaAlmacenCobroDet_SeleccionarTipo"))
                                Alerta("Favor de capturar el tipo de documento");
                            else
                                if (mensaje.Contains("MovFacRevCobro_EstatusInvalido"))
                                    Alerta("El estatus del documento es inválido");
                                else
                                    if (mensaje.Contains("MovFacRevCobro_NoSaldo"))
                                        Alerta("El documento no tiene saldo");
                                    else
                                        if (mensaje.Contains("rgFacturaAlmacenCobroDet_insert_repetida"))
                                            Alerta("Este documento ya ha sido capturado");
                                        else
                                            if (mensaje.Contains("PermisoGuardarNo"))
                                                Alerta("No tiene permisos para grabar");
                                            else
                                                if (mensaje.Contains("PermisoModificarNo"))
                                                    Alerta("No tiene permisos para actualizar");
                                                else
                                                    if (mensaje.Contains("CapFacturaAlmacenCobro_insert_error"))
                                                        Alerta("No se pudo guardar la captura de relación factura a revisión o cobro");
                                                    else
                                                        if (mensaje.Contains("CapFacturaAlmacenCobro_update_error"))
                                                            Alerta("No se pudo actualizar la captura de relación de factura a revisión o cobro");
                                                        else
                                                            if (mensaje.Contains("rgFacturaAlmacenCobroDet_NeedDataSource"))
                                                                Alerta("Error al cargar el grid de detalle");
                                                            else
                                                                if (mensaje.Contains("rgFacturaAlmacenCobroDet_ItemDataBound"))
                                                                    Alerta("Error al momento de preparar un registro para edición");
                                                                else
                                                                    if (mensaje.Contains("rgFacturaAlmacenCobroDet_insert_error"))
                                                                        Alerta("Error al momento de agregar el documento");
                                                                    else
                                                                        if (mensaje.Contains("rgFacturaAlmacenCobroDet_delete_error"))
                                                                            Alerta("Error al momento de eliminar el documento");
                                                                        else
                                                                            Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }

        #endregion

        #region ErrorManager

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

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            this.LLenarFormFacturaAlmacenCobro_sugerido(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac);
            rgFacturaAlmacenCobroDet.Rebind();
        }

    }
}
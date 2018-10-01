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
    public partial class CapFacturaSvtas : System.Web.UI.Page
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
        private int Id_Fva;
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        private List<FacturaSvtaAlmacenDet> ListaFacturaSvtaAlmacen
        {
            get { return (List<FacturaSvtaAlmacenDet>)Session[Session.SessionID + "ListaFacturaSvtaAlmacenDet"]; }
            set { Session[Session.SessionID + "ListaFacturaSvtaAlmacenDet"] = value; }
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
                        int Id_Fva = Convert.ToInt32(Page.Request.QueryString["Id_Fva"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                        _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                        _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                        _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;

                        this.Inicializar(Id_Emp, Id_Cd, Id_Fva);

                        if (Request.QueryString["Estatus"].ToString() != "C" && Request.QueryString["Estatus"].ToString() != "undefined")
                        {
                            txtEntrego.Enabled = false;
                            txtRecibio.Enabled = false;
                            dpFecha.Enabled = false;
                            dpFechaFin.Enabled = false;
                            btnBuscar.Enabled = false;

                            //rgFacturaSvtaAlmacenDet.Columns[rgFacturaSvtaAlmacenDet.Columns.FindByUniqueName("Incluir").OrderIndex - 2].Display = false;
                            rgFacturaSvtaAlmacenDet.Columns[rgFacturaSvtaAlmacenDet.Columns.FindByUniqueName("EditCommandColumn").OrderIndex - 2].Display = false;
                            rgFacturaSvtaAlmacenDet.Columns[rgFacturaSvtaAlmacenDet.Columns.FindByUniqueName("DeleteColumn").OrderIndex - 2].Display = false;

                            //GridCommandItem cmdItem = (GridCommandItem)FacturaSvtaAlmacenDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            //cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                            rgFacturaSvtaAlmacenDet.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                            rgFacturaSvtaAlmacenDet.Rebind();

                            RadToolBar1.Items[1].Visible = false;
                            BtnConfirmarTodos.Visible = true;

                            
                        }
                        else
                        {
                            rgFacturaSvtaAlmacenDet.Columns[rgFacturaSvtaAlmacenDet.Columns.FindByUniqueName("Confirmar").OrderIndex - 2].Display = false;
                            BtnConfirmarTodos.Visible = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgFacturaSvtaAlmacenDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaSvtaAlmacenDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaSvtaAlmacenDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
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

        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Fva)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Session["ListaFacturaSvtaAlmacenDet"] = new List<FacturaSvtaAlmacenDet>();


            //establece valores de controles de inicio
            if (Id_Emp > 0 && Id_Cd > 0 && Id_Fva > 0)
            {
                this.LLenarFormFacturaSvtaAlmacen(Id_Emp, Id_Cd, Id_Fva);
                this.hiddenId.Value = Id_Fva.ToString();
            }
            else //Nueva
            {
                Funciones funcion = new Funciones();
                dpFecha.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                dpFechaFin.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                this.LLenarFormFacturaSvtaAlmacen_sugerido(Id_Emp, Id_Cd, Id_Fva);
                this.hiddenId.Value = string.Empty;
            }
            this.rgFacturaSvtaAlmacenDet.Rebind();
        }

        #region "Métodos para manejar la lista dinámica de Productos de la Nota de crédito"

        protected void ListaFacturaSvtaAlmacen_Agregar(FacturaSvtaAlmacenDet FacturaSvtaAlmacen_nueva)
        {

            int repetido = ListaFacturaSvtaAlmacen.Where(FacturaSvtaAlmacen => FacturaSvtaAlmacen.Fva_Doc == FacturaSvtaAlmacen_nueva.Fva_Doc).ToList().Count;

            if (repetido > 0)
            {
                Alerta("Este documento ya ha sido capturado");
            }
            else
            {
                ListaFacturaSvtaAlmacen.Add(FacturaSvtaAlmacen_nueva);
            }
            //List<FacturaSvtaAlmacenDet> lista = this.ListaFacturaSvtaAlmacen;

            //buscar en la lista para ver si ya existe
            //for (int i = 0; i < lista.Count; i++)
            //{
            //    FacturaSvtaAlmacenDet FacturaSvtaAlmacen = lista[i];
            //    if (FacturaSvtaAlmacen.Fva_Doc == FacturaSvtaAlmacen_nueva.Fva_Doc && FacturaSvtaAlmacen.Fva_Tipo == FacturaSvtaAlmacen_nueva.Fva_Tipo)//si el documento es el mismo
            //    {
            //        throw new Exception("rgFacturaSvtaAlmacenDet_insert_repetida");
            //    }
            //}

            //this.ListaFacturaSvtaAlmacen = lista;
        }

        protected void ListaFacturaSvtaAlmacen_Modificar(FacturaSvtaAlmacenDet FacturaSvtaAlmacen_nueva, string doc_old)
        {
            List<FacturaSvtaAlmacenDet> lista = this.ListaFacturaSvtaAlmacen;

            //buscar producto de factura en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                FacturaSvtaAlmacenDet notaCargoDet = lista[i];
                if (notaCargoDet.Fva_Doc.ToString() == doc_old && notaCargoDet.Fva_Tipo == FacturaSvtaAlmacen_nueva.Fva_Tipo)
                {
                    lista[i] = FacturaSvtaAlmacen_nueva;
                    break;
                }
            }
            this.ListaFacturaSvtaAlmacen = lista;
        }

        protected void ListaFacturaSvtaAlmacen_Eliminar(int Fva_Doc, string Fva_Tipo)
        {
            List<FacturaSvtaAlmacenDet> lista = this.ListaFacturaSvtaAlmacen;

            //buscar producto de factura en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                FacturaSvtaAlmacenDet FacturaSvtaAlmacenDetDet = lista[i];
                if (FacturaSvtaAlmacenDetDet.Fva_Doc == Fva_Doc && FacturaSvtaAlmacenDetDet.Fva_Tipo == Fva_Tipo)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaFacturaSvtaAlmacen = lista;
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
                        rgFacturaSvtaAlmacenDet.Rebind();
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 100);
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDetalles.Width;
                        RadSplitter1.Height = altura;
                        rgFacturaSvtaAlmacenDet.Rebind();
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
                        mensajeError = hiddenId.Value == string.Empty ? "CapFacturaSvtasCobro_insert_error" : "CapFacturaSvtasCobro_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaSvtaAlmacenDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgFacturaSvtaAlmacenDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "Confirmar":
                        int item = e.Item.ItemIndex;
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_CapFacturaSvtaAlmacen cn_svta = new CN_CapFacturaSvtaAlmacen();
                        FacturaSvtaAlmacenDet det = new FacturaSvtaAlmacenDet();
                        det.Id_Emp = sesion.Id_Emp;
                        det.Id_Cd = sesion.Id_Cd_Ver;
                        det.Id_Fva = Convert.ToInt32(Page.Request.QueryString["Id_Fva"]);
                        det.Fva_Doc = Convert.ToInt32((rgFacturaSvtaAlmacenDet.Items[item].FindControl("lblFva_Doc") as Label).Text);
                        int verificador = 0;
                       // cn_svta.Confirmar(det, ref verificador, sesion.Emp_Cnx);

                        int Id_Fva = Convert.ToInt32(Page.Request.QueryString["Id_Fva"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        LLenarFormFacturaSvtaAlmacen(Id_Emp, Id_Cd, Id_Fva);
                        rgFacturaSvtaAlmacenDet.Rebind();

                        if (verificador == 1)
                        {
                            //Alerta("La factura <b># " + cob.Id_Fac + "</b> fue entregada correctamente");
                        }
                        else
                        {
                            Alerta("No se pudo autorizar la factura");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaSvtaAlmacenDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgFacturaSvtaAlmacenDet.DataSource = this.ListaFacturaSvtaAlmacen;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaSvtaAlmacenDet_ItemDataBound(object sender, GridItemEventArgs e)
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
                        //if (item.GetDataKeyValue("Fva_Confirmar").ToString() == "0")
                        //{
                        if (Convert.ToBoolean(item.GetDataKeyValue("Fva_Confirmado")))
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
                        
                        //}
                        //else
                        //{
                        //    Button.Visible = true;
                        //}
                    }
                    catch
                    {
                    }

                    try
                    {
                        check = (CheckBox)item["Incluir"].Controls[1];
                        check.Checked = Convert.ToBoolean(item.GetDataKeyValue("Fva_Seleccionado"));                      
                        check.Enabled = Convert.ToBoolean(item.GetDataKeyValue("Fva_Confirmado")) ? false : true; 
                        clickHandler = check.Attributes["onclick"];
                        check.Attributes["onclick"] = "return actualizar_tabla(" + item.GetDataKeyValue("Fva_Doc").ToString() + ", this.checked);";// clickHandler.Replace("[[ID]]", );

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

                        int seleccionados = ListaFacturaSvtaAlmacen.Where(FacturaSvtaAlmacenDet => FacturaSvtaAlmacenDet.Fva_Seleccionado == true).ToList().Count;
                        int total = ListaFacturaSvtaAlmacen.Count;

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
                    string txtFva_Doc = ((RadNumericTextBox)editItem.FindControl("txtFva_Doc")).ClientID.ToString();
                    string lblVal_txtFva_Doc = ((Label)editItem.FindControl("lblVal_txtFva_Doc")).ClientID.ToString();
                    string cmbFva_EnviarA = ((RadComboBox)editItem.FindControl("cmbFva_EnviarA")).ClientID.ToString();
                    string lblVal_cmbFva_EnviarA = ((Label)editItem.FindControl("lblVal_cmbFva_EnviarA")).ClientID.ToString();


                    string jsControles = string.Concat(
                        "cmbTipoClientID='", cmbTipo, "';"
                        , "lblVal_cmbTipoClientID='", lblVal_cmbTipo, "';"
                        , "txtFva_DocClientID='", txtFva_Doc, "';"
                        , "lblVal_txtFva_DocClientID='", lblVal_txtFva_Doc, "';"
                        , "cmbFva_EnviarAClientID='", cmbFva_EnviarA, "';"
                        , "lblVal_cmbFva_EnviarAClientID='", lblVal_cmbFva_EnviarA, "';"
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

                        int Fva_Doc = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Fva_Doc"]);
                        foreach (FacturaSvtaAlmacenDet FvaDet in this.ListaFacturaSvtaAlmacen)
                        {
                            if (FvaDet.Fva_Doc == Fva_Doc)
                            {
                                ((RadComboBox)editItem.FindControl("cmbTipo")).SelectedIndex =
                                    ((RadComboBox)editItem.FindControl("cmbTipo")).FindItemIndexByValue(FvaDet.Fva_Tipo);
                                ((RadComboBox)editItem.FindControl("cmbFva_EnviarA")).SelectedIndex =
                                    ((RadComboBox)editItem.FindControl("cmbFva_EnviarA")).FindItemIndexByValue(FvaDet.Fva_EnviarA.ToString());
                            }
                        }

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                        ((RadComboBox)editItem.FindControl("cmbFva_EnviarA")).Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaSvtaAlmacenDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaSvtaAlmacenDet FacturaSvtaAlmacenDet = new FacturaSvtaAlmacenDet();

                FacturaSvtaAlmacenDet.Id_Emp = sesion.Id_Emp;
                FacturaSvtaAlmacenDet.Id_Cd = sesion.Id_Cd_Ver;
                FacturaSvtaAlmacenDet.Id_Fva = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                FacturaSvtaAlmacenDet.Id_FvaDet = 0;
                FacturaSvtaAlmacenDet.Id_Reg = null;
                FacturaSvtaAlmacenDet.Fva_Doc = Convert.ToInt32((insertedItem.FindControl("txtFva_Doc") as RadNumericTextBox).Text);
                FacturaSvtaAlmacenDet.Fva_Fecha = Convert.ToDateTime((insertedItem.FindControl("txtFva_Fecha") as RadDatePicker).SelectedDate);
                FacturaSvtaAlmacenDet.Id_Cte = Convert.ToInt32((insertedItem.FindControl("lblId_CteEdit") as Label).Text);
                FacturaSvtaAlmacenDet.Cte_NomComercial = (insertedItem.FindControl("lblId_CteStrEdit") as Label).Text;
                FacturaSvtaAlmacenDet.Fva_Importe = Convert.ToDouble((insertedItem.FindControl("txtFva_ImporteEdit") as RadNumericTextBox).Text);
                //FacturaSvtaAlmacenDet.Fva_EnviarA = Convert.ToInt32((insertedItem.FindControl("cmbFva_EnviarA") as RadComboBox).SelectedValue);
                FacturaSvtaAlmacenDet.Fva_DiaRev = (insertedItem.FindControl("lblVal_Fva_DiaRev") as Label).Text;
                FacturaSvtaAlmacenDet.Fva_Seleccionado = (insertedItem.FindControl("chkIncluirEditar") as CheckBox).Checked;
                //agregar producto de nota de cargo a la lista
                this.ListaFacturaSvtaAlmacen_Agregar(FacturaSvtaAlmacenDet);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaSvtaAlmacenDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaSvtaAlmacenDet FacturaSvtaAlmacenDet = new FacturaSvtaAlmacenDet();

                FacturaSvtaAlmacenDet.Id_Emp = sesion.Id_Emp;
                FacturaSvtaAlmacenDet.Id_Cd = sesion.Id_Cd_Ver;
                FacturaSvtaAlmacenDet.Id_Fva = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                FacturaSvtaAlmacenDet.Id_FvaDet = 0;
                FacturaSvtaAlmacenDet.Id_Reg = null;

                RadComboBox cmbTipo = (insertedItem["Fva_Tipo"].FindControl("cmbTipo") as RadComboBox);
                FacturaSvtaAlmacenDet.Fva_Tipo = cmbTipo.SelectedValue;
                FacturaSvtaAlmacenDet.Fva_TipoStr = cmbTipo.SelectedItem.Text;
                FacturaSvtaAlmacenDet.Fva_Doc =
                    Convert.ToInt32((insertedItem["Fva_Doc"].FindControl("txtFva_Doc") as RadNumericTextBox).Text);
                FacturaSvtaAlmacenDet.Fva_Fecha =
                    Convert.ToDateTime((insertedItem["Fva_Fecha"].FindControl("txtFva_Fecha") as RadDatePicker).SelectedDate);
                FacturaSvtaAlmacenDet.Id_Cte =
                    Convert.ToInt32((insertedItem["Id_Cte"].FindControl("lblId_CteEdit") as Label).Text);
                FacturaSvtaAlmacenDet.Cte_NomComercial = (insertedItem["Id_CteStr"].FindControl("lblId_CteStrEdit") as Label).Text;
                FacturaSvtaAlmacenDet.Fva_Importe =
                    Convert.ToDouble((insertedItem["Fva_Importe"].FindControl("txtFva_ImporteEdit") as RadNumericTextBox).Text);

                FacturaSvtaAlmacenDet.Fva_DiaRev = (insertedItem["Fva_DiaRev"].FindControl("cmbFva_DiaRev") as RadComboBox).SelectedItem.Text;
                string doc_old = (insertedItem["Fva_Doc"].FindControl("lblVal_txtFva_Doc") as Label).Text;
                //actualizar producto de nota de cargo a la lista
                this.ListaFacturaSvtaAlmacen_Modificar(FacturaSvtaAlmacenDet, doc_old);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaSvtaAlmacenDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int Fva_Doc = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Fva_Doc"]);
                string Fva_Tipo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Fva_Tipo"].ToString();
                //eliminar producto de nota de cargo a la lista
                this.ListaFacturaSvtaAlmacen_Eliminar(Fva_Doc, Fva_Tipo);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaSvtaAlmacenDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgFacturaSvtaAlmacenDet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void txtFva_Doc_TextChanged(object sender, EventArgs e)
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
                RadDatePicker txtFva_Fecha = ((RadDatePicker)tabla.FindControl("txtFva_Fecha"));
                Label lblId_CteEdit = ((Label)tabla.FindControl("lblId_CteEdit"));
                Label lblId_CteStrEdit = ((Label)tabla.FindControl("lblId_CteStrEdit"));
                RadNumericTextBox txtFva_ImporteEdit = ((RadNumericTextBox)tabla.FindControl("txtFva_ImporteEdit"));
                Label lblVal_Fva_DiaRev = ((Label)tabla.FindControl("lblVal_Fva_DiaRev"));

                txtFva_Fecha.SelectedDate = null;
                lblId_CteEdit.Text = string.Empty;
                lblId_CteStrEdit.Text = string.Empty;
                txtFva_ImporteEdit.Text = string.Empty;
                lblVal_Fva_DiaRev.Text = string.Empty;

                RadNumericTextBox txtFva_Doc = ((RadNumericTextBox)tabla.FindControl("txtFva_Doc"));
                if (string.IsNullOrEmpty(txtFva_Doc.Text))
                {
                    //this.DisplayMensajeAlerta("FacturaSvtaAlmacenDet_IntroducirNumDocumento");
                    txtFva_Doc.Focus();
                }
                else
                {
                    bool encontrado = false;
                    Factura factura = new Factura();
                    factura.Id_Emp = sesion.Id_Emp;
                    factura.Id_Cd = sesion.Id_Cd_Ver;
                    factura.Id_Fac = Convert.ToInt32(txtFva_Doc.Text);
                    new CN_CapFacturaSvtaAlmacen().ConsultaFacturaEncabezado(ref factura, sesion.Emp_Cnx, ref encontrado);

                    if (encontrado)
                    {
                        if (factura.Fac_Saldo <= 0)
                        {
                            txtFva_Doc.Text = string.Empty;
                            txtFva_Doc.Focus();
                            this.DisplayMensajeAlerta("MovFacRevCobro_NoSaldo");
                        }
                        else
                        {
                            if (factura.Fac_Estatus.ToUpper() != "B" && factura.Fac_Estatus.ToUpper() != "C")
                            {
                                txtFva_Fecha.SelectedDate = factura.Fac_Fecha;
                                lblId_CteEdit.Text = factura.Id_Cte.ToString();
                                lblId_CteStrEdit.Text = factura.Cte_NomComercial;
                                txtFva_ImporteEdit.Text = factura.Fac_Saldo.ToString();
                                lblVal_Fva_DiaRev.Text = factura.Fac_Notas.ToString();
                            }
                            else
                            {
                                txtFva_Doc.Text = string.Empty;
                                txtFva_Doc.Focus();
                                this.DisplayMensajeAlerta("MovFacRevCobro_EstatusInvalido");
                            }
                        }
                    }
                    else
                    {
                        this.DisplayMensajeAlerta("FacturaSvtaAlmacenDet_DocNoEncontrado");
                        txtFva_Doc.Text = string.Empty;
                        txtFva_Doc.Focus();
                    }




                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarFormFacturaSvtaAlmacen(int Id_Emp, int Id_Cd, int Id_Fva)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            FacturaSvtaAlmacen FacturaSvtaAlmacen = new FacturaSvtaAlmacen();
            FacturaSvtaAlmacen.Id_Emp = Id_Emp;
            FacturaSvtaAlmacen.Id_Cd = Id_Cd;
            FacturaSvtaAlmacen.Id_Fva = Id_Fva;
            FacturaSvtaAlmacen.DbName = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog;
            new CN_CapFacturaSvtaAlmacen().ConsultarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen, sesion.Emp_Cnx);
            txtEntrego.Text = FacturaSvtaAlmacen.Fva_Entrego;
            txtRecibio.Text = FacturaSvtaAlmacen.Fva_Recibio;
            this.hiddenId.Value = Id_Fva.ToString();

            int total_facturas = FacturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet.Count();

            int i = 0;

            foreach (FacturaSvtaAlmacenDet fac in FacturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet)
            {

                if (fac.Fva_Confirmado)
                {
                    i++;
                }
            }
            if (total_facturas == i)
            {
                BtnConfirmarTodos.Enabled = false;
            }

            this.ListaFacturaSvtaAlmacen = FacturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet;
        }
        private void LLenarFormFacturaSvtaAlmacen_sugerido(int Id_Emp, int Id_Cd, int Id_Fva)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            FacturaSvtaAlmacen FacturaSvtaAlmacen = new FacturaSvtaAlmacen();
            FacturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet = new List<FacturaSvtaAlmacenDet>();

            FacturaSvtaAlmacen.Id_Emp = sesion.Id_Emp;
            FacturaSvtaAlmacen.Id_Cd = sesion.Id_Cd_Ver;
            FacturaSvtaAlmacen.Fva_Fecha = dpFecha.SelectedDate.Value;//ESTA LINEA CAMBIARIA PARA QUE TOME LA FECHA DE UN CONTROL
            FacturaSvtaAlmacen.Fva_FechaFin = dpFechaFin.SelectedDate.Value;
            new CN_CapFacturaSvtaAlmacen().ConsultarFacturaSvtaAlmacen_Sugerido(ref FacturaSvtaAlmacen, sesion.Emp_Cnx);

            this.ListaFacturaSvtaAlmacen = FacturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet;
        }
        private FacturaSvtaAlmacen LlenarObjetoFacturaSvtaAlmacen()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            FacturaSvtaAlmacen FacturaSvtaAlmacen = new FacturaSvtaAlmacen();
            FacturaSvtaAlmacen.Id_Emp = sesion.Id_Emp;
            FacturaSvtaAlmacen.Id_Cd = sesion.Id_Cd_Ver;
            if (this.hiddenId.Value != string.Empty)
            {
                FacturaSvtaAlmacen.Id_Fva = Convert.ToInt32(this.hiddenId.Value);
            }
            else
            {
                FacturaSvtaAlmacen.Id_Fva = 0;
            }
            FacturaSvtaAlmacen.Id_Reg = null;
            FacturaSvtaAlmacen.Id_U = sesion.Id_U;

            FacturaSvtaAlmacen.Fva_Entrego = txtEntrego.Text;
            FacturaSvtaAlmacen.Fva_Recibio = txtRecibio.Text;
            FacturaSvtaAlmacen.Fva_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            FacturaSvtaAlmacen.Fva_FecEnvio = null;
            FacturaSvtaAlmacen.Fva_FecRecibio = null;
            FacturaSvtaAlmacen.Fva_Estatus = "C";

            FacturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet = ListaFacturaSvtaAlmacen.Where(FacturaSvtaAlmacenDet => FacturaSvtaAlmacenDet.Fva_Seleccionado == true).ToList();

          


            return FacturaSvtaAlmacen;
        }

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaSvtaAlmacen FacturaSvtaAlmacen = this.LlenarObjetoFacturaSvtaAlmacen();
                string mensaje = string.Empty;

                int verificador = 0;




                if (FacturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet.Count == 0)
                {
                    this.DisplayMensajeAlerta("FacturaSvtaAlmacenDet_NoPartidas");
                    return;
                }

                if (this.hiddenId.Value == string.Empty) //nueva nota de cargo
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    new CN_CapFacturaSvtaAlmacen().InsertarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen, sesion.Emp_Cnx, ref verificador);
                    mensaje = "Los datos se guardaron correctamente";
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }

                    new CN_CapFacturaSvtaAlmacen().ModificarFacturaSvtaAlmacen(ref FacturaSvtaAlmacen, sesion.Emp_Cnx, ref verificador);
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



        protected void BtnConfirmarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];                
           
                FacturaSvtaAlmacen FacturaSvtaAlmacen = this.LlenarObjetoFacturaSvtaAlmacen();
                string mensaje = string.Empty;

                int verificador = 0;

                if (FacturaSvtaAlmacen.ListaFacturaSvtaAlmacenDet.Count == 0)
                {
                    this.DisplayMensajeAlerta("FacturaSvtaAlmacenDet_NoPartidas");
                    return;
                }

                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }


                CN_CapFacturaSvtaAlmacen cn_svta = new CN_CapFacturaSvtaAlmacen();
                cn_svta.Confirmar(FacturaSvtaAlmacen, ref verificador, Sesion.Emp_Cnx);

                int Id_Fva = Convert.ToInt32(Page.Request.QueryString["Id_Fva"]);
                int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                LLenarFormFacturaSvtaAlmacen(Id_Emp, Id_Cd, Id_Fva);
                rgFacturaSvtaAlmacenDet.Rebind();

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


        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("FacturaSvtaAlmacenDet_DocNoEncontrado"))
                    Alerta("El documento no fue encontrado");
                else
                    if (mensaje.Contains("FacturaSvtaAlmacenDet_NoPartidas"))
                        Alerta("Favor de capturar al menos un documento");
                    else
                        if (mensaje.Contains("FacturaSvtaAlmacenDet_IntroducirNumDocumento"))
                            Alerta("Favor de introducir el número de documento");
                        else
                            if (mensaje.Contains("FacturaSvtaAlmacenDet_SeleccionarTipo"))
                                Alerta("Favor de capturar el tipo de documento");
                            else
                                if (mensaje.Contains("MovFacRevCobro_EstatusInvalido"))
                                    Alerta("El estatus del documento es inválido");
                                else
                                    if (mensaje.Contains("MovFacRevCobro_NoSaldo"))
                                        Alerta("El documento no tiene saldo");
                                    else
                                        if (mensaje.Contains("rgFacturaSvtaAlmacenDet_insert_repetida"))
                                            Alerta("Este documento ya ha sido capturado");
                                        else
                                            if (mensaje.Contains("PermisoGuardarNo"))
                                                Alerta("No tiene permisos para grabar");
                                            else
                                                if (mensaje.Contains("PermisoModificarNo"))
                                                    Alerta("No tiene permisos para actualizar");
                                                else
                                                    if (mensaje.Contains("CapFacturaSvtasCobro_insert_error"))
                                                        Alerta("No se pudo guardar la captura de relación factura a revisión o cobro");
                                                    else
                                                        if (mensaje.Contains("CapFacturaSvtasCobro_update_error"))
                                                            Alerta("No se pudo actualizar la captura de relación de factura a revisión o cobro");
                                                        else
                                                            if (mensaje.Contains("rgFacturaSvtaAlmacenDet_NeedDataSource"))
                                                                Alerta("Error al cargar el grid de detalle");
                                                            else
                                                                if (mensaje.Contains("rgFacturaSvtaAlmacenDet_ItemDataBound"))
                                                                    Alerta("Error al momento de preparar un registro para edición");
                                                                else
                                                                    if (mensaje.Contains("rgFacturaSvtaAlmacenDet_insert_error"))
                                                                        Alerta("Error al momento de agregar el documento");
                                                                    else
                                                                        if (mensaje.Contains("rgFacturaSvtaAlmacenDet_delete_error"))
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
            this.LLenarFormFacturaSvtaAlmacen_sugerido(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fva);
            rgFacturaSvtaAlmacenDet.Rebind();
        }
    }
}
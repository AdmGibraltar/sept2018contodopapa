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
    public partial class CapFacturaRevision : System.Web.UI.Page
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
        private int Id_Frc;
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }



        private DataTable dtFacturaRevision
        {
            get
            {
                return (DataTable)Session["dtFacturaRevision" + Session.SessionID];
            }
            set
            {
                Session["dtFacturaRevision" + Session.SessionID] = value;
            }
        }

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        private List<FacturaRevisionCobroDet> ListaFacturaRevisionCobro
        {
            get { return (List<FacturaRevisionCobroDet>)Session[Session.SessionID + "ListaFacturaRevisionCobroDet"]; }
            set { Session[Session.SessionID + "ListaFacturaRevisionCobroDet"] = value; }
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
                        Id_Frc = Convert.ToInt32(Page.Request.QueryString["Id_Frc"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                        _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                        _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                        _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;

                        this.Inicializar(Id_Emp, Id_Cd, Id_Frc);

                        if (Request.QueryString["Estatus"].ToString() != "C" && Request.QueryString["Estatus"].ToString() != "undefined")
                        {
                            txtEntrego.Enabled = false;
                            txtRecibio.Enabled = false;
                            dpFecha.Enabled = false;
                            btnBuscar.Enabled = false;

                            rgFacturaRevDet.Columns[rgFacturaRevDet.Columns.FindByUniqueName("EditCommandColumn").OrderIndex - 2].Display = false;
                            rgFacturaRevDet.Columns[rgFacturaRevDet.Columns.FindByUniqueName("DeleteColumn").OrderIndex - 2].Display = false;

                            //GridCommandItem cmdItem = (GridCommandItem)rgFacturaRevDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            //cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                            rgFacturaRevDet.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                            
                            RadToolBar1.Items[1].Visible = false;

                            //rgFacturaRevDet.Columns[rgFacturaRevDet.Columns.FindByUniqueName("Incluir").OrderIndex - 2].Display = false;
                            BtnConfirmarTodos.Visible = true;
                        }
                        else
                        {
                            rgFacturaRevDet.Columns[rgFacturaRevDet.Columns.FindByUniqueName("Confirmar").OrderIndex - 2].Display = false;
                            rgFacturaRevDet.Columns[rgFacturaRevDet.Columns.FindByUniqueName("Efectivo").OrderIndex - 2].Display = false;
                            rgFacturaRevDet.Columns[rgFacturaRevDet.Columns.FindByUniqueName("Cheque").OrderIndex - 2].Display = false;
                            BtnConfirmarTodos.Visible = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgFacturaRevDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaRevDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaRevDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
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

        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Frc)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Session["ListaFacturaRevisionCobroDet"] = new List<FacturaRevisionCobroDet>();

           
            //establece valores de controles de inicio
            if (Id_Emp > 0 && Id_Cd > 0 && Id_Frc > 0)
            {
                this.LLenarFormFacturaRevisionCobro(Id_Emp, Id_Cd, Id_Frc);
                this.hiddenId.Value = Id_Frc.ToString();
            }
            else //Nueva
            {
                Funciones funcion = new Funciones();
                dpFecha.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                this.LLenarFormFacturaRevisionCobro_sugerido(Id_Emp, Id_Cd, Id_Frc);
                this.hiddenId.Value = string.Empty;
            }
            this.rgFacturaRevDet.Rebind();
        }

        #region "Métodos para manejar la lista dinámica de Productos de la Nota de crédito"

        protected void ListaFacturaRevisionCobro_Agregar(FacturaRevisionCobroDet facturaRevisionCobro_nueva)
        {

            int repetido = ListaFacturaRevisionCobro.Where(FacturaRevisionCobro => FacturaRevisionCobro.Frc_Doc == facturaRevisionCobro_nueva.Frc_Doc && FacturaRevisionCobro.Frc_Tipo == facturaRevisionCobro_nueva.Frc_Tipo).ToList().Count;

            if (repetido > 0)
            {
                Alerta("Este documento ya ha sido capturado");
            }
            else
            {
                ListaFacturaRevisionCobro.Add(facturaRevisionCobro_nueva);
            }
            //List<FacturaRevisionCobroDet> lista = this.ListaFacturaRevisionCobro;

            //buscar en la lista para ver si ya existe
            //for (int i = 0; i < lista.Count; i++)
            //{
            //    FacturaRevisionCobroDet facturaRevisionCobro = lista[i];
            //    if (facturaRevisionCobro.Frc_Doc == facturaRevisionCobro_nueva.Frc_Doc && facturaRevisionCobro.Frc_Tipo == facturaRevisionCobro_nueva.Frc_Tipo)//si el documento es el mismo
            //    {
            //        throw new Exception("rgFacturaRevisionCobroDet_insert_repetida");
            //    }
            //}

            //this.ListaFacturaRevisionCobro = lista;
        }

        protected void ListaFacturaRevisionCobro_Modificar(FacturaRevisionCobroDet facturaRevisionCobro_nueva, string doc_old)
        {
            List<FacturaRevisionCobroDet> lista = this.ListaFacturaRevisionCobro;

            //buscar producto de factura en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                FacturaRevisionCobroDet notaCargoDet = lista[i];
                if (notaCargoDet.Frc_Doc.ToString() == doc_old && notaCargoDet.Frc_Tipo == facturaRevisionCobro_nueva.Frc_Tipo)
                {
                    lista[i] = facturaRevisionCobro_nueva;
                    break;
                }
            }
            this.ListaFacturaRevisionCobro = lista;
        }

        protected void ListaFacturaRevisionCobro_Eliminar(int Frc_Doc, string Frc_Tipo)
        {
            List<FacturaRevisionCobroDet> lista = this.ListaFacturaRevisionCobro;

            //buscar producto de factura en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                FacturaRevisionCobroDet facturaRevisionCobroDetDet = lista[i];
                if (facturaRevisionCobroDetDet.Frc_Doc == Frc_Doc && facturaRevisionCobroDetDet.Frc_Tipo == Frc_Tipo)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaFacturaRevisionCobro = lista;
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
                        rgFacturaRevDet.Rebind();
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 100);
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDetalles.Width;
                        RadSplitter1.Height = altura;
                        rgFacturaRevDet.Rebind();
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
                        mensajeError = hiddenId.Value == string.Empty ? "CapFacturaRevisionCobro_insert_error" : "CapFacturaRevisionCobro_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaRevDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgFacturaRevDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "Confirmar":
                        int item = e.Item.ItemIndex;
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_GestorCobranza cn_gestor = new CN_GestorCobranza();
                        Cobranza cob = new Cobranza();
                        cob.Id_Emp = sesion.Id_Emp;
                        cob.Id_Cd = sesion.Id_Cd_Ver;
                        cob.Id_Fac = Convert.ToInt32((rgFacturaRevDet.Items[item].FindControl("lblFrc_Doc") as Label).Text);
                        cob.DbName = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;
                        int verificador = 0;

                       // cn_gestor.ConfirmarRevision(cob, ref verificador, Emp_CnxCob);


                        int Id_Frc = Convert.ToInt32(Page.Request.QueryString["Id_Frc"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        LLenarFormFacturaRevisionCobro(Id_Emp, Id_Cd, Id_Frc);
                        rgFacturaRevDet.Rebind();

                        if (verificador == 1)
                            Alerta("La factura <b># " + cob.Id_Fac + "</b> fue entregada correctamente");
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

        protected void rgFacturaRevDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgFacturaRevDet.DataSource = this.ListaFacturaRevisionCobro;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaRevDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    ImageButton button = default(ImageButton);
                    string clickHandler = string.Empty;
                    CheckBox check = default(CheckBox);
                    RadioButton ConfirmarCobrocheque = default(RadioButton);
                    RadioButton ConfirmarCobroefectivo = default(RadioButton);

                    try
                    {
                        button = (ImageButton)item["Confirmar"].Controls[1];
                        
                            if (Convert.ToBoolean(item.GetDataKeyValue("Frc_Confirmado")))
                            {
                               // button.Visible = false;
                                button.CssClass = "aceptar";                                
                                button.ToolTip = "Confirmado";
                                button.AlternateText = "Confirmado";
                                
                                
                                //(Button.Parent as GridTableCell).Text = "CONFIRMADO";
                            }
                            else
                            {
                                //button.Visible = false;
                                button.CssClass = "amarillo";                                
                                button.ToolTip = "No Confirmado";
                                button.AlternateText = "No Confirmado";
                              
                               
                            }
                        
                       
                    }
                    catch
                    {
                    }

                    try
                    {
                        check = (CheckBox)item["Incluir"].Controls[1];                        
                        check.Checked = Convert.ToBoolean(item.GetDataKeyValue("Frc_Seleccionado"));
                        check.Enabled = Convert.ToBoolean(item.GetDataKeyValue("Frc_Confirmado")) ? false : true;
                        int cobro =  Convert.ToInt32(item.GetDataKeyValue("Frc_EnviarA"));
                        ConfirmarCobrocheque = (RadioButton)item["Cheque"].Controls[1];
                        ConfirmarCobroefectivo = (RadioButton)item["efectivo"].Controls[1];

                        if (Request.QueryString["Estatus"].ToString() != "C" && Request.QueryString["Estatus"].ToString() != "undefined")
                        {
                            button.Visible =  true;
                            ConfirmarCobrocheque.Visible = cobro == 1 ? false : true;
                            ConfirmarCobroefectivo.Visible = cobro == 1 ? false : true;
                            
                        }
                        clickHandler = check.Attributes["onclick"];
                        check.Attributes["onclick"] = "return actualizar_tabla(" + item.GetDataKeyValue("Frc_Doc").ToString() + ", this.checked);";// clickHandler.Replace("[[ID]]", );

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

                        int seleccionados = ListaFacturaRevisionCobro.Where(FacturaRevisionCobroDet => FacturaRevisionCobroDet.Frc_Seleccionado == true).ToList().Count;
                        int total = ListaFacturaRevisionCobro.Count;

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
                    string txtFrc_Doc = ((RadNumericTextBox)editItem.FindControl("txtFrc_Doc")).ClientID.ToString();
                    string lblVal_txtFrc_Doc = ((Label)editItem.FindControl("lblVal_txtFrc_Doc")).ClientID.ToString();
                    string cmbFrc_EnviarA = ((RadComboBox)editItem.FindControl("cmbFrc_EnviarA")).ClientID.ToString();
                    string lblVal_cmbFrc_EnviarA = ((Label)editItem.FindControl("lblVal_cmbFrc_EnviarA")).ClientID.ToString();


                    string jsControles = string.Concat(
                        "cmbTipoClientID='", cmbTipo, "';"
                        , "lblVal_cmbTipoClientID='", lblVal_cmbTipo, "';"
                        , "txtFrc_DocClientID='", txtFrc_Doc, "';"
                        , "lblVal_txtFrc_DocClientID='", lblVal_txtFrc_Doc, "';"
                        , "cmbFrc_EnviarAClientID='", cmbFrc_EnviarA, "';"
                        , "lblVal_cmbFrc_EnviarAClientID='", lblVal_cmbFrc_EnviarA, "';"
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

                        int Frc_Doc = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Frc_Doc"]);
                        foreach (FacturaRevisionCobroDet FrcDet in this.ListaFacturaRevisionCobro)
                        {
                            if (FrcDet.Frc_Doc == Frc_Doc)
                            {
                                ((RadComboBox)editItem.FindControl("cmbTipo")).SelectedIndex =
                                    ((RadComboBox)editItem.FindControl("cmbTipo")).FindItemIndexByValue(FrcDet.Frc_Tipo);
                                ((RadComboBox)editItem.FindControl("cmbFrc_EnviarA")).SelectedIndex =
                                    ((RadComboBox)editItem.FindControl("cmbFrc_EnviarA")).FindItemIndexByValue(FrcDet.Frc_EnviarA.ToString());
                            }
                        }

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                        ((RadComboBox)editItem.FindControl("cmbFrc_EnviarA")).Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaRevDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaRevisionCobroDet facturaRevisionCobroDet = new FacturaRevisionCobroDet();

                facturaRevisionCobroDet.Id_Emp = sesion.Id_Emp;
                facturaRevisionCobroDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaRevisionCobroDet.Id_Frc = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                facturaRevisionCobroDet.Id_FrcDet = 0;
                facturaRevisionCobroDet.Id_Reg = null;

                RadComboBox cmbTipo = (insertedItem.FindControl("cmbTipo") as RadComboBox);
                facturaRevisionCobroDet.Frc_Tipo = cmbTipo.SelectedValue;
                facturaRevisionCobroDet.Frc_TipoStr = cmbTipo.SelectedItem.Text;
                facturaRevisionCobroDet.Frc_Doc = Convert.ToInt32((insertedItem.FindControl("txtFrc_Doc") as RadNumericTextBox).Text);
                facturaRevisionCobroDet.Frc_Fecha = Convert.ToDateTime((insertedItem.FindControl("txtFrc_Fecha") as RadDatePicker).SelectedDate);
                facturaRevisionCobroDet.Id_Cte = Convert.ToInt32((insertedItem.FindControl("lblId_CteEdit") as Label).Text);
                facturaRevisionCobroDet.Cte_NomComercial = (insertedItem.FindControl("lblId_CteStrEdit") as Label).Text;
                facturaRevisionCobroDet.Frc_Importe = Convert.ToDouble((insertedItem.FindControl("txtFrc_ImporteEdit") as RadNumericTextBox).Text);
                facturaRevisionCobroDet.Frc_EnviarA = Convert.ToInt32((insertedItem.FindControl("cmbFrc_EnviarA") as RadComboBox).SelectedValue);
                facturaRevisionCobroDet.Frc_EnviarAStr = (insertedItem.FindControl("cmbFrc_EnviarA") as RadComboBox).SelectedItem.Text;
                facturaRevisionCobroDet.Frc_Seleccionado = (insertedItem.FindControl("chkIncluirEditar") as CheckBox).Checked;
                //agregar producto de nota de cargo a la lista
                this.ListaFacturaRevisionCobro_Agregar(facturaRevisionCobroDet);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }




        protected void rgFacturaRevDet_PreRender(object sender, EventArgs e)
        {
            RadioButton ConfirmarCobrocheque = default(RadioButton);
            RadioButton ConfirmarCobroefectivo = default(RadioButton);
            foreach (GridDataItem item in rgFacturaRevDet.Items)
            {
                ConfirmarCobrocheque = (RadioButton)item["confirmar"].Controls[3];
                ConfirmarCobroefectivo = (RadioButton)item["confirmar"].Controls[5];
                //RadioButton myRadioBtnListTipo = (RadioButton)item.FindControl("RadioBtnListTipo");

            }
        } 
 


        protected void rgFacturaRevDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaRevisionCobroDet facturaRevisionCobroDet = new FacturaRevisionCobroDet();

                facturaRevisionCobroDet.Id_Emp = sesion.Id_Emp;
                facturaRevisionCobroDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaRevisionCobroDet.Id_Frc = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                facturaRevisionCobroDet.Id_FrcDet = 0;
                facturaRevisionCobroDet.Id_Reg = null;

                RadComboBox cmbTipo = (insertedItem["Frc_Tipo"].FindControl("cmbTipo") as RadComboBox);
                facturaRevisionCobroDet.Frc_Tipo = cmbTipo.SelectedValue;
                facturaRevisionCobroDet.Frc_TipoStr = cmbTipo.SelectedItem.Text;
                facturaRevisionCobroDet.Frc_Doc =
                    Convert.ToInt32((insertedItem["Frc_Doc"].FindControl("txtFrc_Doc") as RadNumericTextBox).Text);
                facturaRevisionCobroDet.Frc_Fecha =
                    Convert.ToDateTime((insertedItem["Frc_Fecha"].FindControl("txtFrc_Fecha") as RadDatePicker).SelectedDate);
                facturaRevisionCobroDet.Id_Cte =
                    Convert.ToInt32((insertedItem["Id_Cte"].FindControl("lblId_CteEdit") as Label).Text);
                facturaRevisionCobroDet.Cte_NomComercial = (insertedItem["Id_CteStr"].FindControl("lblId_CteStrEdit") as Label).Text;
                facturaRevisionCobroDet.Frc_Importe =
                    Convert.ToDouble((insertedItem["Frc_Importe"].FindControl("txtFrc_ImporteEdit") as RadNumericTextBox).Text);
                facturaRevisionCobroDet.Frc_EnviarA =
                    Convert.ToInt32((insertedItem["Frc_EnviarA"].FindControl("cmbFrc_EnviarA") as RadComboBox).SelectedValue);
                facturaRevisionCobroDet.Frc_EnviarAStr = (insertedItem["Frc_EnviarA"].FindControl("cmbFrc_EnviarA") as RadComboBox).SelectedItem.Text;
                string doc_old = (insertedItem["Frc_Doc"].FindControl("lblVal_txtFrc_Doc") as Label).Text;
                //actualizar producto de nota de cargo a la lista
                this.ListaFacturaRevisionCobro_Modificar(facturaRevisionCobroDet, doc_old);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaRevDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int Frc_Doc = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Frc_Doc"]);
                string Frc_Tipo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Frc_Tipo"].ToString();
                //eliminar producto de nota de cargo a la lista
                this.ListaFacturaRevisionCobro_Eliminar(Frc_Doc, Frc_Tipo);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaRevDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgFacturaRevDet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void txtFrc_Doc_TextChanged(object sender, EventArgs e)
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
                RadDatePicker txtFrc_Fecha = ((RadDatePicker)tabla.FindControl("txtFrc_Fecha"));
                Label lblId_CteEdit = ((Label)tabla.FindControl("lblId_CteEdit"));
                Label lblId_CteStrEdit = ((Label)tabla.FindControl("lblId_CteStrEdit"));
                RadNumericTextBox txtFrc_ImporteEdit = ((RadNumericTextBox)tabla.FindControl("txtFrc_ImporteEdit"));

                txtFrc_Fecha.SelectedDate = null;
                lblId_CteEdit.Text = string.Empty;
                lblId_CteStrEdit.Text = string.Empty;
                txtFrc_ImporteEdit.Text = string.Empty;

                RadComboBox cmbTipo = ((RadComboBox)tabla.FindControl("cmbTipo"));
                if (cmbTipo.SelectedValue == "-1")
                {
                    this.DisplayMensajeAlerta("rgFacturaRevDet_SeleccionarTipo");
                    cmbTipo.Focus();
                }
                else
                {
                    RadNumericTextBox txtFrc_Doc = ((RadNumericTextBox)tabla.FindControl("txtFrc_Doc"));
                    if (string.IsNullOrEmpty(txtFrc_Doc.Text))
                    {
                        //this.DisplayMensajeAlerta("rgFacturaRevDet_IntroducirNumDocumento");
                        txtFrc_Doc.Focus();
                    }
                    else
                    {
                        bool encontrado = false;
                        switch (cmbTipo.SelectedValue)
                        {
                            case "1": //Factura
                                Factura factura = new Factura();
                                factura.Id_Emp = sesion.Id_Emp;
                                factura.Id_Cd = sesion.Id_Cd_Ver;
                                factura.Id_Fac = Convert.ToInt32(txtFrc_Doc.Text);
                                new CN_CapFactura().ConsultaFacturaEncabezado(ref factura, sesion.Emp_Cnx, ref encontrado);

                                if (encontrado)
                                {
                                    if (factura.Fac_Saldo <= 0)
                                    {
                                        txtFrc_Doc.Text = string.Empty;
                                        txtFrc_Doc.Focus();
                                        this.DisplayMensajeAlerta("MovFacRevCobro_NoSaldo");
                                    }
                                    else
                                    {
                                        if (factura.Fac_Estatus.ToUpper() != "B" && factura.Fac_Estatus.ToUpper() != "C")
                                        {
                                            txtFrc_Fecha.SelectedDate = factura.Fac_Fecha;
                                            lblId_CteEdit.Text = factura.Id_Cte.ToString();
                                            lblId_CteStrEdit.Text = factura.Cte_NomComercial;
                                            txtFrc_ImporteEdit.Text = factura.Fac_Saldo.ToString();
                                        }
                                        else
                                        {
                                            txtFrc_Doc.Text = string.Empty;
                                            txtFrc_Doc.Focus();
                                            this.DisplayMensajeAlerta("MovFacRevCobro_EstatusInvalido");
                                        }
                                    }
                                }
                                else
                                {
                                    this.DisplayMensajeAlerta("rgFacturaRevDet_DocNoEncontrado");
                                    txtFrc_Doc.Text = string.Empty;
                                    txtFrc_Doc.Focus();
                                }
                                break;

                            case "2": //Nota de cargo
                                NotaCargo notaCargo = new NotaCargo();
                                notaCargo.Id_Emp = sesion.Id_Emp;
                                notaCargo.Id_Cd = sesion.Id_Cd_Ver;
                                notaCargo.Id_Nca = Convert.ToInt32(txtFrc_Doc.Text);
                                
                                new CN_CapNotaCargo().ConsultaNotaCargo_Encabezado(ref notaCargo, sesion.Emp_Cnx, ref encontrado);

                                if (encontrado)
                                {
                                    if (notaCargo.Nca_Saldo <= 0)
                                    {
                                        txtFrc_Doc.Text = string.Empty;
                                        txtFrc_Doc.Focus();
                                        this.DisplayMensajeAlerta("MovFacRevCobro_NoSaldo");
                                    }
                                    else
                                    {
                                        if (notaCargo.Nca_Estatus.ToUpper() != "B" && notaCargo.Nca_Estatus.ToUpper() != "C")
                                        {
                                            txtFrc_Fecha.SelectedDate = notaCargo.Nca_Fecha;
                                            lblId_CteEdit.Text = notaCargo.Id_Cte.ToString();
                                            lblId_CteStrEdit.Text = notaCargo.Cte_NomComercial;
                                            txtFrc_ImporteEdit.Text = notaCargo.Nca_Saldo.ToString();
                                        }
                                        else
                                        {
                                            txtFrc_Doc.Text = string.Empty;
                                            txtFrc_Doc.Focus();
                                            this.DisplayMensajeAlerta("MovFacRevCobro_EstatusInvalido");
                                        }
                                    }
                                }
                                else
                                {
                                    txtFrc_Doc.Text = string.Empty;
                                    txtFrc_Doc.Focus();
                                    this.DisplayMensajeAlerta("rgFacturaRevDet_DocNoEncontrado");
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarFormFacturaRevisionCobro(int Id_Emp, int Id_Cd, int Id_Frc)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            FacturaRevisionCobro facturaRevisionCobro = new FacturaRevisionCobro();
            facturaRevisionCobro.Id_Emp = Id_Emp;
            facturaRevisionCobro.Id_Cd = Id_Cd;
            facturaRevisionCobro.Id_Frc = Id_Frc;
            facturaRevisionCobro.DbName = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog;
            new CN_CapFacturaRevisionCobro().ConsultarFacturaRevisionCobro(ref facturaRevisionCobro, sesion.Emp_Cnx);
            txtEntrego.Text = facturaRevisionCobro.Frc_Entrego;
            txtRecibio.Text = facturaRevisionCobro.Frc_Recibio;
            this.hiddenId.Value = Id_Frc.ToString();


            int total_facturas = facturaRevisionCobro.ListaFacturaRevisionCobroDet.Count();

            int i = 0;

            foreach (FacturaRevisionCobroDet fac in facturaRevisionCobro.ListaFacturaRevisionCobroDet)
            {

                if (fac.Frc_Confirmado)
                {
                    i++;
                }
            }
            if (total_facturas == i)
            {
                BtnConfirmarTodos.Enabled = false;
            }


            this.ListaFacturaRevisionCobro = facturaRevisionCobro.ListaFacturaRevisionCobroDet;
            rgFacturaRevDet.Rebind();
        }
        private void LLenarFormFacturaRevisionCobro_sugerido(int Id_Emp, int Id_Cd, int Id_Frc)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            FacturaRevisionCobro facturaRevisionCobro = new FacturaRevisionCobro();
            facturaRevisionCobro.ListaFacturaRevisionCobroDet = new List<FacturaRevisionCobroDet>();

            facturaRevisionCobro.Id_Emp = sesion.Id_Emp;
            facturaRevisionCobro.Id_Cd = sesion.Id_Cd_Ver;
            facturaRevisionCobro.Frc_Fecha = dpFecha.SelectedDate.Value;//ESTA LINEA CAMBIARIA PARA QUE TOME LA FECHA DE UN CONTROL
            facturaRevisionCobro.DbName = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;
            new CN_CapFacturaRevisionCobro().ConsultarFacturaRevisionCobro_Sugerido(ref facturaRevisionCobro, Emp_CnxCob);

            this.ListaFacturaRevisionCobro = facturaRevisionCobro.ListaFacturaRevisionCobroDet;
            rgFacturaRevDet.Rebind();
        }
        private FacturaRevisionCobro LlenarObjetoFacturaRevisionCobro()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            FacturaRevisionCobro facturaRevisionCobro = new FacturaRevisionCobro();
            facturaRevisionCobro.Id_Emp = sesion.Id_Emp;
            facturaRevisionCobro.Id_Cd = sesion.Id_Cd_Ver;
            if (this.hiddenId.Value != string.Empty)
            {
                facturaRevisionCobro.Id_Frc = Convert.ToInt32(this.hiddenId.Value);
            }
            else
            {
                facturaRevisionCobro.Id_Frc = 0;
            }
            facturaRevisionCobro.Id_Reg = null;
            facturaRevisionCobro.Id_U = sesion.Id_U;

            facturaRevisionCobro.Frc_Entrego = txtEntrego.Text;
            facturaRevisionCobro.Frc_Recibio = txtRecibio.Text;
            facturaRevisionCobro.Frc_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            facturaRevisionCobro.Frc_FecEnvio = null;
            facturaRevisionCobro.Frc_FecRecibio = null;
            facturaRevisionCobro.Frc_Estatus = "C";

            facturaRevisionCobro.ListaFacturaRevisionCobroDet = ListaFacturaRevisionCobro.Where(FacturaRevisionCobroDet => FacturaRevisionCobroDet.Frc_Seleccionado == true).ToList();
            

            List<FacturaRevisionCobroDet> ListFinal = new List<FacturaRevisionCobroDet>();

                          
                 foreach (FacturaRevisionCobroDet fac in facturaRevisionCobro.ListaFacturaRevisionCobroDet)
                 {
                    for (int x = 0; x < rgFacturaRevDet.Items.Count; x++)
                    { 
                        if(Convert.ToInt32((rgFacturaRevDet.Items[x].FindControl("lblFrc_Doc") as Label).Text) == fac.Frc_Doc) 
                        {
                            fac.Frc_Cheque = Convert.ToInt32((rgFacturaRevDet.Items[x].FindControl("chkCheque") as RadioButton).Checked);
                            fac.Frc_Efectivo = Convert.ToInt32((rgFacturaRevDet.Items[x].FindControl("chkEfectivo") as RadioButton).Checked);                        
                        }
                   
                    }
                    ListFinal.Add(fac);
                 }
            
           facturaRevisionCobro.ListaFacturaRevisionCobroDet = ListFinal;
            return facturaRevisionCobro;
        }

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaRevisionCobro facturaRevisionCobro = this.LlenarObjetoFacturaRevisionCobro();
                string mensaje = string.Empty;

                int verificador = 0;




                if (facturaRevisionCobro.ListaFacturaRevisionCobroDet.Count == 0)
                {
                    this.DisplayMensajeAlerta("rgFacturaRevDet_NoPartidas");
                    return;
                }

                if (this.hiddenId.Value == string.Empty) //nueva nota de cargo
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    new CN_CapFacturaRevisionCobro().InsertarFacturaRevisionCobro(ref facturaRevisionCobro, sesion.Emp_Cnx, ref verificador);
                    mensaje = "Los datos se guardaron correctamente";
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }

                    new CN_CapFacturaRevisionCobro().ModificarFacturaRevisionCobro(ref facturaRevisionCobro, sesion.Emp_Cnx, ref verificador);
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
                if (mensaje.Contains("rgFacturaRevDet_DocNoEncontrado"))
                    Alerta("El documento no fue encontrado");
                else
                    if (mensaje.Contains("rgFacturaRevDet_NoPartidas"))
                        Alerta("Favor de capturar al menos un documento");
                    else
                        if (mensaje.Contains("rgFacturaRevDet_IntroducirNumDocumento"))
                            Alerta("Favor de introducir el número de documento");
                        else
                            if (mensaje.Contains("rgFacturaRevDet_SeleccionarTipo"))
                                Alerta("Favor de capturar el tipo de documento");
                            else
                                if (mensaje.Contains("MovFacRevCobro_EstatusInvalido"))
                                    Alerta("El estatus del documento es inválido");
                                else
                                    if (mensaje.Contains("MovFacRevCobro_NoSaldo"))
                                        Alerta("El documento no tiene saldo");
                                    else
                                        if (mensaje.Contains("rgFacturaRevisionCobroDet_insert_repetida"))
                                            Alerta("Este documento ya ha sido capturado");
                                        else
                                            if (mensaje.Contains("PermisoGuardarNo"))
                                                Alerta("No tiene permisos para grabar");
                                            else
                                                if (mensaje.Contains("PermisoModificarNo"))
                                                    Alerta("No tiene permisos para actualizar");
                                                else
                                                    if (mensaje.Contains("CapFacturaRevisionCobro_insert_error"))
                                                        Alerta("No se pudo guardar la captura de relación factura a revisión o cobro");
                                                    else
                                                        if (mensaje.Contains("CapFacturaRevisionCobro_update_error"))
                                                            Alerta("No se pudo actualizar la captura de relación de factura a revisión o cobro");
                                                        else
                                                            if (mensaje.Contains("rgFacturaRevisionCobroDet_NeedDataSource"))
                                                                Alerta("Error al cargar el grid de detalle");
                                                            else
                                                                if (mensaje.Contains("rgFacturaRevisionCobroDet_ItemDataBound"))
                                                                    Alerta("Error al momento de preparar un registro para edición");
                                                                else
                                                                    if (mensaje.Contains("rgFacturaRevisionCobroDet_insert_error"))
                                                                        Alerta("Error al momento de agregar el documento");
                                                                    else
                                                                        if (mensaje.Contains("rgFacturaRevisionCobroDet_delete_error"))
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
            this.LLenarFormFacturaRevisionCobro_sugerido(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Frc);
        }


        protected void BtnConfirmarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                FacturaRevisionCobro FacturaRevisionCobro = this.LlenarObjetoFacturaRevisionCobro();
                string mensaje = string.Empty;

                int verificador = 0;

                if (FacturaRevisionCobro.ListaFacturaRevisionCobroDet.Count == 0)
                {
                    this.DisplayMensajeAlerta("rgFacturaRevDet_NoPartidas");
                    return;
                }

                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                
                 CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                 string dbname = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;

                 cn_gestor.ConfirmarRevision(FacturaRevisionCobro, ref verificador, Emp_CnxCob, dbname);

                   
                int Id_Frc = Convert.ToInt32(Page.Request.QueryString["Id_Frc"]);
                int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                LLenarFormFacturaRevisionCobro(Id_Emp, Id_Cd, Id_Frc);
                rgFacturaRevDet.Rebind();

               
                if (verificador == 1)
                    Alerta("La facturas seleccionadas fueron entregadas correctamente");
                else
                    Alerta("No se pudo autorizar la factura");



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }
    }
}
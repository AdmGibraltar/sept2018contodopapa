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
    public partial class CapRemisionSvtas : System.Web.UI.Page
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
        private int Id_Rva;
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        private List<RemisionSvtaAlmacenDet> ListaRemisionSvtaAlmacen
        {
            get { return (List<RemisionSvtaAlmacenDet>)Session[Session.SessionID + "ListaRemisionSvtaAlmacenDet"]; }
            set { Session[Session.SessionID + "ListaRemisionSvtaAlmacenDet"] = value; }
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
                        int Id_Rva = Convert.ToInt32(Page.Request.QueryString["Id_Rva"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                        _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                        _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                        _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;

                        this.Inicializar(Id_Emp, Id_Cd, Id_Rva);

                        if (Request.QueryString["Estatus"].ToString() != "C" && Request.QueryString["Estatus"].ToString() != "undefined")
                        {
                            txtEntrego.Enabled = false;
                            txtRecibio.Enabled = false;
                            dpFecha.Enabled = false;
                            dpFechaFin.Enabled = false;
                            btnBuscar.Enabled = false;

                            rgRemisionSvtaAlmacenDet.Columns[rgRemisionSvtaAlmacenDet.Columns.FindByUniqueName("Incluir").OrderIndex - 2].Display = false;
                            rgRemisionSvtaAlmacenDet.Columns[rgRemisionSvtaAlmacenDet.Columns.FindByUniqueName("EditCommandColumn").OrderIndex - 2].Display = false;
                            rgRemisionSvtaAlmacenDet.Columns[rgRemisionSvtaAlmacenDet.Columns.FindByUniqueName("DeleteColumn").OrderIndex - 2].Display = false;

                            //GridCommandItem cmdItem = (GridCommandItem)RemisionSvtaAlmacenDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            //cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                            rgRemisionSvtaAlmacenDet.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                            rgRemisionSvtaAlmacenDet.Rebind();

                            RadToolBar1.Items[1].Visible = false;

                            
                        }
                        else
                        {
                            rgRemisionSvtaAlmacenDet.Columns[rgRemisionSvtaAlmacenDet.Columns.FindByUniqueName("Confirmar").OrderIndex - 2].Display = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgRemisionSvtaAlmacenDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgRemisionSvtaAlmacenDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgRemisionSvtaAlmacenDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
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

        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Rva)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Session["ListaRemisionSvtaAlmacenDet"] = new List<RemisionSvtaAlmacenDet>();


            //establece valores de controles de inicio
            if (Id_Emp > 0 && Id_Cd > 0 && Id_Rva > 0)
            {
                this.LLenarFormRemisionSvtaAlmacen(Id_Emp, Id_Cd, Id_Rva);
                this.hiddenId.Value = Id_Rva.ToString();
            }
            else //Nueva
            {
                Funciones funcion = new Funciones();
                dpFecha.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                dpFechaFin.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                this.LLenarFormRemisionSvtaAlmacen_sugerido(Id_Emp, Id_Cd, Id_Rva);
                this.hiddenId.Value = string.Empty;
            }
            this.rgRemisionSvtaAlmacenDet.Rebind();
        }

        #region "Métodos para manejar la lista dinámica de Productos de la Nota de crédito"

        protected void ListaRemisionSvtaAlmacen_Agregar(RemisionSvtaAlmacenDet RemisionSvtaAlmacen_nueva)
        {

            int repetido = ListaRemisionSvtaAlmacen.Where(RemisionSvtaAlmacen => RemisionSvtaAlmacen.Rva_Doc == RemisionSvtaAlmacen_nueva.Rva_Doc).ToList().Count;

            if (repetido > 0)
            {
                Alerta("Este documento ya ha sido capturado");
            }
            else
            {
                ListaRemisionSvtaAlmacen.Add(RemisionSvtaAlmacen_nueva);
            }
            //List<RemisionSvtaAlmacenDet> lista = this.ListaRemisionSvtaAlmacen;

            //buscar en la lista para ver si ya existe
            //for (int i = 0; i < lista.Count; i++)
            //{
            //    RemisionSvtaAlmacenDet RemisionSvtaAlmacen = lista[i];
            //    if (RemisionSvtaAlmacen.Rva_Doc == RemisionSvtaAlmacen_nueva.Rva_Doc && RemisionSvtaAlmacen.Rva_Tipo == RemisionSvtaAlmacen_nueva.Rva_Tipo)//si el documento es el mismo
            //    {
            //        throw new Exception("rgRemisionSvtaAlmacenDet_insert_repetida");
            //    }
            //}

            //this.ListaRemisionSvtaAlmacen = lista;
        }

        protected void ListaRemisionSvtaAlmacen_Modificar(RemisionSvtaAlmacenDet RemisionSvtaAlmacen_nueva, string doc_old)
        {
            List<RemisionSvtaAlmacenDet> lista = this.ListaRemisionSvtaAlmacen;

            //buscar producto de Remision en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                RemisionSvtaAlmacenDet notaCargoDet = lista[i];
                if (notaCargoDet.Rva_Doc.ToString() == doc_old && notaCargoDet.Rva_Tipo == RemisionSvtaAlmacen_nueva.Rva_Tipo)
                {
                    lista[i] = RemisionSvtaAlmacen_nueva;
                    break;
                }
            }
            this.ListaRemisionSvtaAlmacen = lista;
        }

        protected void ListaRemisionSvtaAlmacen_Eliminar(int Rva_Doc, string Rva_Tipo)
        {
            List<RemisionSvtaAlmacenDet> lista = this.ListaRemisionSvtaAlmacen;

            //buscar producto de Remision en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                RemisionSvtaAlmacenDet RemisionSvtaAlmacenDetDet = lista[i];
                if (RemisionSvtaAlmacenDetDet.Rva_Doc == Rva_Doc && RemisionSvtaAlmacenDetDet.Rva_Tipo == Rva_Tipo)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaRemisionSvtaAlmacen = lista;
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
                        rgRemisionSvtaAlmacenDet.Rebind();
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 100);
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDetalles.Width;
                        RadSplitter1.Height = altura;
                        rgRemisionSvtaAlmacenDet.Rebind();
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
                        mensajeError = hiddenId.Value == string.Empty ? "CapRemisionSvtasCobro_insert_error" : "CapRemisionSvtasCobro_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRemisionSvtaAlmacenDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgRemisionSvtaAlmacenDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "Confirmar":
                        int item = e.Item.ItemIndex;
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_CapRemisionSvtaAlmacen cn_svta = new CN_CapRemisionSvtaAlmacen();
                        RemisionSvtaAlmacenDet det = new RemisionSvtaAlmacenDet();
                        det.Id_Emp = sesion.Id_Emp;
                        det.Id_Cd = sesion.Id_Cd_Ver;
                        det.Id_Rva = Convert.ToInt32(Page.Request.QueryString["Id_Rva"]);
                        det.Rva_Doc = Convert.ToInt32((rgRemisionSvtaAlmacenDet.Items[item].FindControl("lblRva_Doc") as Label).Text);
                        int verificador = 0;
                        cn_svta.Confirmar(det, ref verificador, sesion.Emp_Cnx);

                        int Id_Rva = Convert.ToInt32(Page.Request.QueryString["Id_Rva"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        LLenarFormRemisionSvtaAlmacen(Id_Emp, Id_Cd, Id_Rva);
                        rgRemisionSvtaAlmacenDet.Rebind();

                        if (verificador == 1)
                        {
                            //Alerta("La Remision <b># " + cob.Id_Rem + "</b> fue entregada correctamente");
                        }
                        else
                        {
                            Alerta("No se pudo autorizar la Remision");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRemisionSvtaAlmacenDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgRemisionSvtaAlmacenDet.DataSource = this.ListaRemisionSvtaAlmacen;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRemisionSvtaAlmacenDet_ItemDataBound(object sender, GridItemEventArgs e)
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
                        //if (item.GetDataKeyValue("Rva_Confirmar").ToString() == "0")
                        //{
                        if (Convert.ToBoolean(item.GetDataKeyValue("Rva_Confirmado")))
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
                            clickHandler = Button.Attributes["onclick"];
                            Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Rva_Doc").ToString());
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
                        check.Checked = Convert.ToBoolean(item.GetDataKeyValue("Rva_Seleccionado"));
                        clickHandler = check.Attributes["onclick"];
                        check.Attributes["onclick"] = "return actualizar_tabla(" + item.GetDataKeyValue("Rva_Doc").ToString() + ", this.checked);";// clickHandler.Replace("[[ID]]", );

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

                        int seleccionados = ListaRemisionSvtaAlmacen.Where(RemisionSvtaAlmacenDet => RemisionSvtaAlmacenDet.Rva_Seleccionado == true).ToList().Count;
                        int total = ListaRemisionSvtaAlmacen.Count;

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
                    string txtRva_Doc = ((RadNumericTextBox)editItem.FindControl("txtRva_Doc")).ClientID.ToString();
                    string lblVal_txtRva_Doc = ((Label)editItem.FindControl("lblVal_txtRva_Doc")).ClientID.ToString();
                    string cmbRva_EnviarA = ((RadComboBox)editItem.FindControl("cmbRva_EnviarA")).ClientID.ToString();
                    string lblVal_cmbRva_EnviarA = ((Label)editItem.FindControl("lblVal_cmbRva_EnviarA")).ClientID.ToString();


                    string jsControles = string.Concat(
                        "cmbTipoClientID='", cmbTipo, "';"
                        , "lblVal_cmbTipoClientID='", lblVal_cmbTipo, "';"
                        , "txtRva_DocClientID='", txtRva_Doc, "';"
                        , "lblVal_txtRva_DocClientID='", lblVal_txtRva_Doc, "';"
                        , "cmbRva_EnviarAClientID='", cmbRva_EnviarA, "';"
                        , "lblVal_cmbRva_EnviarAClientID='", lblVal_cmbRva_EnviarA, "';"
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

                        int Rva_Doc = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Rva_Doc"]);
                        foreach (RemisionSvtaAlmacenDet RvaDet in this.ListaRemisionSvtaAlmacen)
                        {
                            if (RvaDet.Rva_Doc == Rva_Doc)
                            {
                                ((RadComboBox)editItem.FindControl("cmbTipo")).SelectedIndex =
                                    ((RadComboBox)editItem.FindControl("cmbTipo")).FindItemIndexByValue(RvaDet.Rva_Tipo);
                                ((RadComboBox)editItem.FindControl("cmbRva_EnviarA")).SelectedIndex =
                                    ((RadComboBox)editItem.FindControl("cmbRva_EnviarA")).FindItemIndexByValue(RvaDet.Rva_EnviarA.ToString());
                            }
                        }

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                        ((RadComboBox)editItem.FindControl("cmbRva_EnviarA")).Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRemisionSvtaAlmacenDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RemisionSvtaAlmacenDet RemisionSvtaAlmacenDet = new RemisionSvtaAlmacenDet();

                RemisionSvtaAlmacenDet.Id_Emp = sesion.Id_Emp;
                RemisionSvtaAlmacenDet.Id_Cd = sesion.Id_Cd_Ver;
                RemisionSvtaAlmacenDet.Id_Rva = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                RemisionSvtaAlmacenDet.Id_RvaDet = 0;
                RemisionSvtaAlmacenDet.Id_Reg = null;
                RemisionSvtaAlmacenDet.Rva_Doc = Convert.ToInt32((insertedItem.FindControl("txtRva_Doc") as RadNumericTextBox).Text);
                RemisionSvtaAlmacenDet.Rva_Fecha = Convert.ToDateTime((insertedItem.FindControl("txtRva_Fecha") as RadDatePicker).SelectedDate);
                RemisionSvtaAlmacenDet.Id_Cte = Convert.ToInt32((insertedItem.FindControl("lblId_CteEdit") as Label).Text);
                RemisionSvtaAlmacenDet.Cte_NomComercial = (insertedItem.FindControl("lblId_CteStrEdit") as Label).Text;
                RemisionSvtaAlmacenDet.Rva_Importe = Convert.ToDouble((insertedItem.FindControl("txtRva_ImporteEdit") as RadNumericTextBox).Text);
                //RemisionSvtaAlmacenDet.Rva_EnviarA = Convert.ToInt32((insertedItem.FindControl("cmbRva_EnviarA") as RadComboBox).SelectedValue);
                RemisionSvtaAlmacenDet.Rva_DiaRev = (insertedItem.FindControl("lblVal_Rva_DiaRev") as Label).Text;
                RemisionSvtaAlmacenDet.Rva_Seleccionado = (insertedItem.FindControl("chkIncluirEditar") as CheckBox).Checked;
                //agregar producto de nota de cargo a la lista
                this.ListaRemisionSvtaAlmacen_Agregar(RemisionSvtaAlmacenDet);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRemisionSvtaAlmacenDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RemisionSvtaAlmacenDet RemisionSvtaAlmacenDet = new RemisionSvtaAlmacenDet();

                RemisionSvtaAlmacenDet.Id_Emp = sesion.Id_Emp;
                RemisionSvtaAlmacenDet.Id_Cd = sesion.Id_Cd_Ver;
                RemisionSvtaAlmacenDet.Id_Rva = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                RemisionSvtaAlmacenDet.Id_RvaDet = 0;
                RemisionSvtaAlmacenDet.Id_Reg = null;

                RadComboBox cmbTipo = (insertedItem["Rva_Tipo"].FindControl("cmbTipo") as RadComboBox);
                RemisionSvtaAlmacenDet.Rva_Tipo = cmbTipo.SelectedValue;
                RemisionSvtaAlmacenDet.Rva_TipoStr = cmbTipo.SelectedItem.Text;
                RemisionSvtaAlmacenDet.Rva_Doc =
                    Convert.ToInt32((insertedItem["Rva_Doc"].FindControl("txtRva_Doc") as RadNumericTextBox).Text);
                RemisionSvtaAlmacenDet.Rva_Fecha =
                    Convert.ToDateTime((insertedItem["Rva_Fecha"].FindControl("txtRva_Fecha") as RadDatePicker).SelectedDate);
                RemisionSvtaAlmacenDet.Id_Cte =
                    Convert.ToInt32((insertedItem["Id_Cte"].FindControl("lblId_CteEdit") as Label).Text);
                RemisionSvtaAlmacenDet.Cte_NomComercial = (insertedItem["Id_CteStr"].FindControl("lblId_CteStrEdit") as Label).Text;
                RemisionSvtaAlmacenDet.Rva_Importe =
                    Convert.ToDouble((insertedItem["Rva_Importe"].FindControl("txtRva_ImporteEdit") as RadNumericTextBox).Text);

                RemisionSvtaAlmacenDet.Rva_DiaRev = (insertedItem["Rva_DiaRev"].FindControl("cmbRva_DiaRev") as RadComboBox).SelectedItem.Text;
                string doc_old = (insertedItem["Rva_Doc"].FindControl("lblVal_txtRva_Doc") as Label).Text;
                //actualizar producto de nota de cargo a la lista
                this.ListaRemisionSvtaAlmacen_Modificar(RemisionSvtaAlmacenDet, doc_old);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRemisionSvtaAlmacenDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int Rva_Doc = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Rva_Doc"]);
                string Rva_Tipo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Rva_Tipo"].ToString();
                //eliminar producto de nota de cargo a la lista
                this.ListaRemisionSvtaAlmacen_Eliminar(Rva_Doc, Rva_Tipo);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRemisionSvtaAlmacenDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgRemisionSvtaAlmacenDet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void txtRva_Doc_TextChanged(object sender, EventArgs e)
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
                RadDatePicker txtRva_Fecha = ((RadDatePicker)tabla.FindControl("txtRva_Fecha"));
                Label lblId_CteEdit = ((Label)tabla.FindControl("lblId_CteEdit"));
                Label lblId_CteStrEdit = ((Label)tabla.FindControl("lblId_CteStrEdit"));
                RadNumericTextBox txtRva_ImporteEdit = ((RadNumericTextBox)tabla.FindControl("txtRva_ImporteEdit"));
                Label lblVal_Rva_DiaRev = ((Label)tabla.FindControl("lblVal_Rva_DiaRev"));

                txtRva_Fecha.SelectedDate = null;
                lblId_CteEdit.Text = string.Empty;
                lblId_CteStrEdit.Text = string.Empty;
                txtRva_ImporteEdit.Text = string.Empty;
                lblVal_Rva_DiaRev.Text = string.Empty;

                RadNumericTextBox txtRva_Doc = ((RadNumericTextBox)tabla.FindControl("txtRva_Doc"));
                if (string.IsNullOrEmpty(txtRva_Doc.Text))
                {
                    //this.DisplayMensajeAlerta("RemisionSvtaAlmacenDet_IntroducirNumDocumento");
                    txtRva_Doc.Focus();
                }
                else
                {
                    bool encontrado = false;
                    Remision Remision = new Remision();
                    Remision.Id_Emp = sesion.Id_Emp;
                    Remision.Id_Cd = sesion.Id_Cd_Ver;
                    Remision.Id_Rem = Convert.ToInt32(txtRva_Doc.Text);
                    new CN_CapRemisionSvtaAlmacen().ConsultaRemisionEncabezado(ref Remision, sesion.Emp_Cnx, ref encontrado);

                    if (encontrado)
                    {
                     
                            if (Remision.Rem_Estatus.ToUpper() != "B" && Remision.Rem_Estatus.ToUpper() != "C")
                            {
                                txtRva_Fecha.SelectedDate = Remision.Rem_Fecha;
                                lblId_CteEdit.Text = Remision.Id_Cte.ToString();
                                lblId_CteStrEdit.Text = Remision.Cte_NomComercial;
                     //           txtRva_ImporteEdit.Text = Remision.Rem_Saldo.ToString();
                       //         lblVal_Rva_DiaRev.Text = Remision.Rem_Notas.ToString();
                            }
                            else
                            {
                                txtRva_Doc.Text = string.Empty;
                                txtRva_Doc.Focus();
                                this.DisplayMensajeAlerta("MovRemRevCobro_EstatusInvalido");
                            }
                        
                    }
                    else
                    {
                        this.DisplayMensajeAlerta("RemisionSvtaAlmacenDet_DocNoEncontrado");
                        txtRva_Doc.Text = string.Empty;
                        txtRva_Doc.Focus();
                    }




                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarFormRemisionSvtaAlmacen(int Id_Emp, int Id_Cd, int Id_Rva)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            RemisionSvtaAlmacen RemisionSvtaAlmacen = new RemisionSvtaAlmacen();
            RemisionSvtaAlmacen.Id_Emp = Id_Emp;
            RemisionSvtaAlmacen.Id_Cd = Id_Cd;
            RemisionSvtaAlmacen.Id_Rva = Id_Rva;
            RemisionSvtaAlmacen.DbName = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog;
            new CN_CapRemisionSvtaAlmacen().ConsultarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen, sesion.Emp_Cnx);
            txtEntrego.Text = RemisionSvtaAlmacen.Rva_Entrego;
            txtRecibio.Text = RemisionSvtaAlmacen.Rva_Recibio;
            this.hiddenId.Value = Id_Rva.ToString();

            this.ListaRemisionSvtaAlmacen = RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet;
        }
        private void LLenarFormRemisionSvtaAlmacen_sugerido(int Id_Emp, int Id_Cd, int Id_Rva)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RemisionSvtaAlmacen RemisionSvtaAlmacen = new RemisionSvtaAlmacen();
            RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet = new List<RemisionSvtaAlmacenDet>();

            RemisionSvtaAlmacen.Id_Emp = sesion.Id_Emp;
            RemisionSvtaAlmacen.Id_Cd = sesion.Id_Cd_Ver;
            RemisionSvtaAlmacen.Rva_Fecha = dpFecha.SelectedDate.Value;//ESTA LINEA CAMBIARIA PARA QUE TOME LA FECHA DE UN CONTROL
            RemisionSvtaAlmacen.Rva_FechaFin = dpFechaFin.SelectedDate.Value;
            new CN_CapRemisionSvtaAlmacen().ConsultarRemisionSvtaAlmacen_Sugerido(ref RemisionSvtaAlmacen, sesion.Emp_Cnx);

            this.ListaRemisionSvtaAlmacen = RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet;
        }
        private RemisionSvtaAlmacen LlenarObjetoRemisionSvtaAlmacen()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RemisionSvtaAlmacen RemisionSvtaAlmacen = new RemisionSvtaAlmacen();
            RemisionSvtaAlmacen.Id_Emp = sesion.Id_Emp;
            RemisionSvtaAlmacen.Id_Cd = sesion.Id_Cd_Ver;
            if (this.hiddenId.Value != string.Empty)
            {
                RemisionSvtaAlmacen.Id_Rva = Convert.ToInt32(this.hiddenId.Value);
            }
            else
            {
                RemisionSvtaAlmacen.Id_Rva = 0;
            }
            RemisionSvtaAlmacen.Id_Reg = null;
            RemisionSvtaAlmacen.Id_U = sesion.Id_U;

            RemisionSvtaAlmacen.Rva_Entrego = txtEntrego.Text;
            RemisionSvtaAlmacen.Rva_Recibio = txtRecibio.Text;
            RemisionSvtaAlmacen.Rva_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            RemisionSvtaAlmacen.Rva_FecEnvio = null;
            RemisionSvtaAlmacen.Rva_FecRecibio = null;
            RemisionSvtaAlmacen.Rva_Estatus = "C";

            RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet = ListaRemisionSvtaAlmacen.Where(RemisionSvtaAlmacenDet => RemisionSvtaAlmacenDet.Rva_Seleccionado == true).ToList();
            //foreach (RemisionSvtaAlmacenDet Rem in RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet)
            //{
            //    RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet.Remove(Rem);
            //}
            return RemisionSvtaAlmacen;
        }

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RemisionSvtaAlmacen RemisionSvtaAlmacen = this.LlenarObjetoRemisionSvtaAlmacen();
                string mensaje = string.Empty;

                int verificador = 0;




                if (RemisionSvtaAlmacen.ListaRemisionSvtaAlmacenDet.Count == 0)
                {
                    this.DisplayMensajeAlerta("RemisionSvtaAlmacenDet_NoPartidas");
                    return;
                }

                if (this.hiddenId.Value == string.Empty) //nueva nota de cargo 
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    new CN_CapRemisionSvtaAlmacen().InsertarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen, sesion.Emp_Cnx, ref verificador);
                    mensaje = "Los datos se guardaron correctamente";
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }

                    new CN_CapRemisionSvtaAlmacen().ModificarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen, sesion.Emp_Cnx, ref verificador);
                    mensaje = "Los datos se modificaron correctamente";
                }
                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de Remision


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
                if (mensaje.Contains("RemisionSvtaAlmacenDet_DocNoEncontrado"))
                    Alerta("El documento no fue encontrado");
                else
                    if (mensaje.Contains("RemisionSvtaAlmacenDet_NoPartidas"))
                        Alerta("Favor de capturar al menos un documento");
                    else
                        if (mensaje.Contains("RemisionSvtaAlmacenDet_IntroducirNumDocumento"))
                            Alerta("Favor de introducir el número de documento");
                        else
                            if (mensaje.Contains("RemisionSvtaAlmacenDet_SeleccionarTipo"))
                                Alerta("Favor de capturar el tipo de documento");
                            else
                                if (mensaje.Contains("MovRemRevCobro_EstatusInvalido"))
                                    Alerta("El estatus del documento es inválido");
                                else
                                    if (mensaje.Contains("MovRemRevCobro_NoSaldo"))
                                        Alerta("El documento no tiene saldo");
                                    else
                                        if (mensaje.Contains("rgRemisionSvtaAlmacenDet_insert_repetida"))
                                            Alerta("Este documento ya ha sido capturado");
                                        else
                                            if (mensaje.Contains("PermisoGuardarNo"))
                                                Alerta("No tiene permisos para grabar");
                                            else
                                                if (mensaje.Contains("PermisoModificarNo"))
                                                    Alerta("No tiene permisos para actualizar");
                                                else
                                                    if (mensaje.Contains("CapRemisionSvtasCobro_insert_error"))
                                                        Alerta("No se pudo guardar la captura de relación Remision a revisión o cobro");
                                                    else
                                                        if (mensaje.Contains("CapRemisionSvtasCobro_update_error"))
                                                            Alerta("No se pudo actualizar la captura de relación de Remision a revisión o cobro");
                                                        else
                                                            if (mensaje.Contains("rgRemisionSvtaAlmacenDet_NeedDataSource"))
                                                                Alerta("Error al cargar el grid de detalle");
                                                            else
                                                                if (mensaje.Contains("rgRemisionSvtaAlmacenDet_ItemDataBound"))
                                                                    Alerta("Error al momento de preparar un registro para edición");
                                                                else
                                                                    if (mensaje.Contains("rgRemisionSvtaAlmacenDet_insert_error"))
                                                                        Alerta("Error al momento de agregar el documento");
                                                                    else
                                                                        if (mensaje.Contains("rgRemisionSvtaAlmacenDet_delete_error"))
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
            this.LLenarFormRemisionSvtaAlmacen_sugerido(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Rva);
            rgRemisionSvtaAlmacenDet.Rebind();
        }
    }
}
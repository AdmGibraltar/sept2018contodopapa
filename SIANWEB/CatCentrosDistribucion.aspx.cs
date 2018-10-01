using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;

using CapaEntidad;
using CapaNegocios;
using CapaDatos;

using Telerik.Web.UI;
using System.IO;
using System.Data;
using System.Text;


namespace SIANWEB
{
    public partial class CatCentrosDistribucion : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        //private List<CentroDistribucion> _ListaCobranzaCentroDistribucion;
        public List<CentroDistribucion> ListaCobranzaCentroDistribucion
        {
            get { return Session["_ListaCobranzaCentroDistribucion" + Session.LCID] as List<CentroDistribucion>; }
            set { Session["_ListaCobranzaCentroDistribucion" + Session.LCID] = value; }
        }
        //private List<CentroDistribucion> _ListaRentabilidadCentroDistribucion;
        public List<CentroDistribucion> ListaRentabilidadCentroDistribucion
        {
            get { return Session["_ListaRentabilidadCentroDistribucion" + Session.LCID] as List<CentroDistribucion>; }
            set { Session["_ListaRentabilidadCentroDistribucion" + Session.LCID] = value; }
        }
        //public List<CategoriaParticipacion> ListaCategoriasCentroDistribucion
        //{
        //    get { return Session["_ListaCategoriasCentroDistribucion" + Session.LCID] as List<CategoriaParticipacion>; }
        //    set { Session["_ListaCategoriasCentroDistribucion" + Session.LCID] = value; }
        //}
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
                    Context.Items.Add("href", pag[pag.Length - 1]);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        CargarTCentro();

                        ValidarPermisos();
                        //llenar Datos de valuación de proyectos del C. de Dist.
                        CentroDistribucion cdValProy = new CentroDistribucion();
                        new CN_CatCentroDistribucion().ConsultaCentroDistribucion_DatosValProyecto(ref cdValProy, Sesion.Emp_Cnx);
                        this.LlenarControlesDatosCentroDistValuacionProyectos(ref cdValProy);

                        this.LlenarComboEmpresas();
                        //this.LlenarComboRegiones();

                        this.HabilitarControlesAdminitrador();
                        this.CargaDatosCentroDistribucion();
                        this.LlenarFormularioCentroDistribucion(Sesion.Id_Cd_Ver);
                        this.hiddenCdi.Value = Sesion.Id_Cd_Ver.ToString();
                        txtCentroDistribucion.Enabled = false;

                        ObtenerCobranza();
                        ObtenerRentabilidad();
                        //ObtenerCategorias();
                        LlenarZonas();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CargarTCentro()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spCatTcentro_Combo", ref cmbTipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && hiddenCdi.Value != "")
            {
                if (!Deshabilitar())
                {
                    this.DisplayMensajeAlerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":  //******** traer consecutivo **********
                        Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        ListaCobranzaCentroDistribucion = new List<CentroDistribucion>();
                        rgCobranza.Rebind();
                        ListaRentabilidadCentroDistribucion = new List<CentroDistribucion>();
                        rgRentabilidad.Rebind();
                        cmbEmpresaID.SelectedValue = Sesion.Id_Emp.ToString();
                        txtEmpresaID.Text = Sesion.Id_Emp.ToString();
                        //rgRentabilidad.Rebind();
                        //rgCobranza.Rebind();
                        break;

                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun.RemoverValidadores(Validators);
                //Actualiza sesion con el centro seleccionado
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref Sesion);

                this.LlenarFormularioCentroDistribucion(Convert.ToInt32(e.Value));
                this.txtCentroDistribucion.Enabled = false;
                this.hiddenCdi.Value = e.Value;
                //habilita botones según la pestaña activa
                if (Sesion.U_MultiOfi)
                    this.HabilitarBotones(true, true, false, false, false, false);
                ObtenerCobranza();
                rgCobranza.Rebind();
                ObtenerRentabilidad();
                LlenarZonas();
                rgRentabilidad.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        protected void cmbEmpresaID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                LlenarComboRegiones();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //COBRANZA
        protected void rgCobranza_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgCobranza.DataSource = ListaCobranzaCentroDistribucion;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCobranza_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditableItem insertedItem = (GridEditableItem)e.Item;

                if ((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Value > (insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Value)
                {
                    AlertaFocus("El día límite no puede ser menor al día de inicio", (insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }
                if ((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Text == "" || (insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Text == "" || (insertedItem["Cob_Multiplicador"].FindControl("txtCobMultiplicador") as RadNumericTextBox).Text == "")
                {
                    AlertaFocus("Todos los campos son requeridos", (insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }
                CentroDistribucion cd = new CentroDistribucion();
                cd.ID_Emp = sesion.Id_Emp;
                cd.ID_Cd = sesion.Id_Cd_Ver;
                cd.Cob_DiaInicio = Convert.ToInt32((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Text);
                cd.Cob_DiaLimite = Convert.ToInt32((insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Text);
                cd.Cob_Multiplicador = Convert.ToDouble((insertedItem["Cob_Multiplicador"].FindControl("txtCobMultiplicador") as RadNumericTextBox).Text);

                if (ListaCobranza_Agregar(cd))
                {
                    e.Canceled = true;
                    return;
                }
                rgCobranza.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCobranza_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditableItem insertedItem = (GridEditableItem)e.Item;

                if ((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Value > (insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Value)
                {
                    AlertaFocus("El día límite no puede ser menor al día de inicio", (insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }
                if ((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Text == "" || (insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Text == "" || (insertedItem["Cob_Multiplicador"].FindControl("txtCobMultiplicador") as RadNumericTextBox).Text == "")
                {
                    AlertaFocus("Todos los campos son requeridos", (insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }
                CentroDistribucion cd = new CentroDistribucion();
                cd.ID_Emp = sesion.Id_Emp;
                cd.ID_Cd = sesion.Id_Cd_Ver;
                cd.Id_Cob = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Cob"]);
                cd.Cob_DiaInicio = Convert.ToInt32((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Text);
                cd.Cob_DiaLimite = Convert.ToInt32((insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Text);
                cd.Cob_Multiplicador = Convert.ToDouble((insertedItem["Cob_Multiplicador"].FindControl("txtCobMultiplicador") as RadNumericTextBox).Text);

                ListaCobranzaCentroDistribucion.RemoveAt(e.Item.ItemIndex);
                if (ListaCobranza_Agregar(cd))
                {
                    e.Canceled = true;
                    return;
                }
                //if (ListaCobranza_Modificar(cd, e.Item.ItemIndex))
                //{
                //    e.Canceled = true;
                //    return;
                //}
                rgCobranza.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCobranza_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                //GridDataItem item = (GridDataItem)e.Item;
                //CentroDistribucion cd = new CentroDistribucion();
                //cd.Id_Cob = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Cob"]);
                ////actualizar producto de orden de compra a la lista
                //this.ListaCobranza_Eliminar(cd);
                ListaCobranzaCentroDistribucion.RemoveAt(e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCobranza_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgCobranza.EditItems.Count > 0)
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
        protected void rgCobranza_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgCobranza.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCobranza_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
        //RENTABILIDAD
        protected void rgRentabilidad_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgRentabilidad.DataSource = ListaRentabilidadCentroDistribucion;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgRentabilidad_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditableItem insertedItem = (GridEditableItem)e.Item;


                if ((insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).Value > (insertedItem["Rent_LSuperior"].FindControl("txtLSuperior") as RadNumericTextBox).Value)
                {
                    AlertaFocus("El límite superior no puede ser menor al límite inferior", (insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }


                if ((insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).Text == "" ||
                    (insertedItem["Rent_LSuperior"].FindControl("txtLSuperior") as RadNumericTextBox).Text == "" ||
                    (insertedItem["Rent_Multiplicador"].FindControl("txtRentMultiplicador") as RadNumericTextBox).Text == "")
                {
                    AlertaFocus("Todos los campos son requeridos", (insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }

                CentroDistribucion cd = new CentroDistribucion();
                cd.ID_Emp = sesion.Id_Emp;
                cd.ID_Cd = sesion.Id_Cd_Ver;
                cd.Rent_LInferior = Convert.ToDouble((insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).Text);
                cd.Rent_LSuperior = Convert.ToDouble((insertedItem["Rent_LSuperior"].FindControl("txtLSuperior") as RadNumericTextBox).Text);
                cd.Rent_Multiplicador = Convert.ToDouble((insertedItem["Rent_Multiplicador"].FindControl("txtRentMultiplicador") as RadNumericTextBox).Text);

                if (ListaRentabilidad_Agregar(cd))
                {
                    e.Canceled = true;
                    return;
                }
                rgRentabilidad.Rebind();
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRentabilidad_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgRentabilidad_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditableItem insertedItem = (GridEditableItem)e.Item;

                if ((insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).Value > (insertedItem["Rent_LSuperior"].FindControl("txtLSuperior") as RadNumericTextBox).Value)
                {
                    AlertaFocus("El límite superior no puede ser menor al límite inferior", (insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }


                if ((insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).Text == "" ||
                    (insertedItem["Rent_LSuperior"].FindControl("txtLSuperior") as RadNumericTextBox).Text == "" ||
                    (insertedItem["Rent_Multiplicador"].FindControl("txtRentMultiplicador") as RadNumericTextBox).Text == "")
                {
                    AlertaFocus("Todos los campos son requeridos", (insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).ClientID);
                    e.Canceled = true;
                    return;
                }

                CentroDistribucion cd = new CentroDistribucion();
                cd.ID_Emp = sesion.Id_Emp;
                cd.ID_Cd = sesion.Id_Cd_Ver;
                cd.Id_Rent = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rent"]);
                cd.Rent_LInferior = Convert.ToDouble((insertedItem["Rent_LInferior"].FindControl("txtLInferior") as RadNumericTextBox).Text);
                cd.Rent_LSuperior = Convert.ToDouble((insertedItem["Rent_LSuperior"].FindControl("txtLSuperior") as RadNumericTextBox).Text);
                cd.Rent_Multiplicador = Convert.ToDouble((insertedItem["Rent_Multiplicador"].FindControl("txtRentMultiplicador") as RadNumericTextBox).Text);

                ListaRentabilidadCentroDistribucion.RemoveAt(e.Item.ItemIndex);
                if (ListaRentabilidad_Agregar(cd))
                {
                    e.Canceled = true;
                    return;
                }

                rgRentabilidad.Rebind();
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRentabilidad_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgRentabilidad_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                //GridDataItem item = (GridDataItem)e.Item;
                //CentroDistribucion cd = new CentroDistribucion();

                //cd.Id_Rent = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Rent"]);
                ////actualizar producto de orden de compra a la lista
                //this.ListaRentabilidad_Eliminar(cd);

                ListaRentabilidadCentroDistribucion.RemoveAt(e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRentabilidad_delete_item_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgRentabilidad_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgRentabilidad.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void rgRentabilidad_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgRentabilidad.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void rgRentabilidad_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        //Comisiones-Categorias
        //protected void rgCategorias_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    try
        //    {
        //        if (e.RebindReason == GridRebindReason.ExplicitRebind)
        //            rgCategorias.DataSource = ListaCategoriasCentroDistribucion;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        //protected void rgCategorias_InsertCommand(object source, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        //GridEditableItem insertedItem = (GridEditableItem)e.Item;

        //        //if ((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Value > (insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Value)
        //        //{
        //        //    AlertaFocus("El día límite no puede ser menor al día de inicio", (insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).ClientID);
        //        //    e.Canceled = true;
        //        //    return;
        //        //}
        //        //if ((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Text == "" || (insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Text == "" || (insertedItem["Cob_Multiplicador"].FindControl("txtCobMultiplicador") as RadNumericTextBox).Text == "")
        //        //{
        //        //    AlertaFocus("Todos los campos son requeridos", (insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).ClientID);
        //        //    e.Canceled = true;
        //        //    return;
        //        //}
        //        //CentroDistribucion cd = new CentroDistribucion();
        //        //cd.ID_Emp = sesion.Id_Emp;
        //        //cd.ID_Cd = sesion.Id_Cd_Ver;
        //        //cd.Cob_DiaInicio = Convert.ToInt32((insertedItem["Cob_DiaInicio"].FindControl("txtDiaInicio") as RadNumericTextBox).Text);
        //        //cd.Cob_DiaLimite = Convert.ToInt32((insertedItem["Cob_DiaLimite"].FindControl("txtDiaLimite") as RadNumericTextBox).Text);
        //        //cd.Cob_Multiplicador = Convert.ToDouble((insertedItem["Cob_Multiplicador"].FindControl("txtCobMultiplicador") as RadNumericTextBox).Text);

        //        //if (ListaCobranza_Agregar(cd))
        //        //{
        //        //    e.Canceled = true;
        //        //    return;
        //        //}
        //        //rgCobranza.Rebind();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        //protected void rgCategorias_UpdateCommand(object source, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        GridEditableItem insertedItem = (GridEditableItem)e.Item;

        //        if ((insertedItem["Cat_Descripcion"].FindControl("txtCat_Descripcion") as RadTextBox).Text == "" || (insertedItem["Cat_Participacion"].FindControl("txtCat_Participacion") as RadNumericTextBox).Text == "" )
        //        {
        //            AlertaFocus("Todos los campos son requeridos", (insertedItem["Cat_Descripcion"].FindControl("txtCat_Descripcion") as RadTextBox).ClientID);
        //            e.Canceled = true;
        //            return;
        //        }

        //        CategoriaParticipacion c= new CategoriaParticipacion();
        //        c.Id_Cat = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Cat"]);
        //        c.Cat_Descripcion = (insertedItem["Cat_Descripcion"].FindControl("txtCat_Descripcion") as RadTextBox).Text;
        //        c.Cat_Participacion = Convert.ToDouble((insertedItem["Cat_Participacion"].FindControl("txtCat_Participacion") as RadNumericTextBox).Text);

        //        ListaCategoriasCentroDistribucion.RemoveAt(e.Item.ItemIndex);
        //        ListaCategoriasCentroDistribucion.Add(c);
        //        //rgCategorias.Rebind();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        ////protected void rgCategorias_DeleteCommand(object source, GridCommandEventArgs e)
        ////{
        ////    try
        ////    {
        ////        ListaCategoriasCentroDistribucion.RemoveAt(e.Item.ItemIndex);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        ////    }
        ////}
        ////protected void rgCategorias_ItemCommand(object source, GridCommandEventArgs e)
        ////{
        ////    try
        ////    {
        ////        switch (e.CommandName)
        ////        {
        ////            case "InitInsert":
        ////                if (rgCategorias.EditItems.Count > 0)
        ////                {
        ////                    Alerta("Ya está editando un registro");
        ////                    e.Canceled = true;
        ////                }
        ////                break;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        ////    }
        ////}
        ////protected void rgCategorias_PageIndexChanged(object source, GridPageChangedEventArgs e)
        ////{
        ////    try
        ////    {
        ////        this.rgCategorias.Rebind();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        ////    }
        ////}
        ////protected void rgCategorias_ItemDataBound(object sender, GridItemEventArgs e)
        ////{

        ////}

        #endregion
        #region Funciones
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, "CatCentroDistribucion", "Id_Cd", sesion.Emp_Cnx, "spCatCentral_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (hiddenCdi.Value != "") //Hidden Field BANDERA
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = sesion.Id_Emp; //Si no ocupa empresa su catalogo se pone -1
                    ct.Id_Cd = -1; //Si no ocupa Centro de distribución se pone -1
                    ct.Id = Convert.ToInt32(hiddenCdi.Value); //Si no ocupa Id se pone -1
                    ct.Tabla = "CatCentroDistribucion"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Cd"; //Nombre de la columna del ID del catalogo
                    CN_Comun.Deshabilitar(ct, sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CentroDistribucion cd = new CentroDistribucion();
            CN_CatCentroDistribucion centro1 = new CN_CatCentroDistribucion();

            int verificador = 0;

            this.LlenarObjetoCentroDistribucion(ref cd);

            if (this.hiddenCdi.Value.Trim() == string.Empty)
            {
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                centro1.InsertarCentroDistribucion(ref cd, sesion.Emp_Cnx, ref verificador);
                centro1.AgregarCentroDistribucion_Cobranza(ListaCobranzaCentroDistribucion, verificador, sesion);
                centro1.AgregarCentroDistribucion_Rentabilidad(ListaRentabilidadCentroDistribucion, verificador, sesion);

                this.LimpiarCampos();
                this.txtCentroDistribucion.Text = this.Valor;
                this.txtCentroDistribucion.Enabled = true;

                //this.LlenarComboOficinas();

                //establecer siguiente clave de nuevo cliente del centro de dist.
                //this.EstablecerSigClaveCliente(ref cd, verificador);

                this.DisplayMensajeAlerta("Cd_insert_ok");
            }
            else
            {
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para actualizar");
                    return;
                }

                centro1.ActualizarCentroDistribucion(ref cd, sesion.Emp_Cnx, ref verificador);
                centro1.AgregarCentroDistribucion_Cobranza(ListaCobranzaCentroDistribucion, sesion.Id_Cd_Ver, sesion);
                centro1.AgregarCentroDistribucion_Rentabilidad(ListaRentabilidadCentroDistribucion, sesion.Id_Cd_Ver, sesion);

                StringBuilder zonas = new StringBuilder();
                foreach (RadListBoxItem rlbi in listZonas.Items)
                {

                    if (rlbi.Checked)
                    {
                        if (zonas.Length > 0)
                        {
                            zonas.Append(",");
                        }
                        zonas.Append(rlbi.Value);
                    }
                }
                centro1.AgregarZona(zonas.ToString(), sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                //establecer siguiente clave de nuevo cliente del centro de dist.
                //this.EstablecerSigClaveCliente(ref cd, cd.Id_Cd);

                this.DisplayMensajeAlerta("Cd_update_ok");
            }
            this.LlenarComboOficinas();

            //habilita botones
            if (sesion.U_MultiOfi)
                this.HabilitarBotones(true, true, false, false, false, false);
            else
                this.HabilitarBotones(true, true, false, false, false, false);
        }
        private void LimpiarCampos()
        {
            //Controles de la pestaña 'Valuacion de proyectos' 
            txtProyecto.Text = string.Empty;
            txtCetesCd.Text = string.Empty;
            txtIvaCd.Text = string.Empty;
            txtCuentasCd.Text = string.Empty;
            txtFleteCd.Text = string.Empty;
            txtDiasCd.Text = string.Empty;
            txtComisionCd.Text = string.Empty;
            txtInventarioCd.Text = string.Empty;
            txtOtrosCd.Text = string.Empty;
            txtFactorInvCd.Text = string.Empty;
            txtGastofijoCd.Text = string.Empty;
            txtFactorConCd.Text = string.Empty;
            txtGastofijopapelCd.Text = string.Empty;
            txtFinanciamientoCd.Text = string.Empty;
            txtIsrCd.Text = string.Empty;
            txtTasaCd.Text = string.Empty;
            txtCargoCd.Text = string.Empty;
            //Controles de los datos generales
            cmbEmpresaID.SelectedIndex = cmbEmpresaID.FindItemIndexByValue("-1");
            cmbRegion.SelectedIndex = cmbRegion.FindItemIndexByValue("-1");
            txtEmpresaID.Text = string.Empty;
            txtRegion.Text = string.Empty;
            txtCentroDistribucion.Text = string.Empty;
            chkActivo.Checked = true;
            txtNombreCD.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtNumeroCalle.Text = string.Empty;
            txtColonia.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtMunicipio.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtPais.Text = string.Empty;
            txtCp.Text = string.Empty;
            txtRfc.Text = string.Empty;
            txtTel.Text = string.Empty;
            cmbTipo.Text = string.Empty;
            //Controles de datos de Pedidos y facturación
            txtPartidaPedidos.Text = string.Empty;
            txtIvaPedidosFacturacion.Text = string.Empty;
            txtClienteClaveSig.Text = string.Empty;
            txtMaximoTerritoriosSegmentos.Text = string.Empty;
            chkFormatoFacturaRetIva.Checked = false;
            chkDeshabilitarReglaCons.Checked = false;
            chkActivaCapPedRep.Checked = false;
            //Controles de datos de info de Inventarios
            txtPartidaRemisiones.Text = string.Empty;
            txtPartidaEntradas.Text = string.Empty;
            txtAjusteFromatoRengInventario.Text = string.Empty;
            //Controles de datos de info de Cobranza
            txtRelacionCobranza.Text = string.Empty;
            txtInteresMoratorio.Text = string.Empty;
            txtContribucionBruta.Text = string.Empty;
            txtAmortizacion.Text = string.Empty;
            txtSaldosMenores.Text = string.Empty;
            txtPersonaFormula.Text = string.Empty;
            txtPersonaAutoriza.Text = string.Empty;
            //Controles de info de administración de inventarios
            txtTiempoEntrega.Text = string.Empty;
            txtTiempoTransportacion.Text = string.Empty;
            //Controles de info de compras locales
            chkActualiza.Checked = false;
            txtFactorCosto.Text = string.Empty;
            //Controles Totalizadores
            txtProyecto.Text = "0";
            txtContadorPedidos.Text = "0";
            txtRemisiones.Text = "0";
            txtEntradas.Text = "0";
            txtSalidas.Text = "0";
            txtDevoluciones.Text = "0";
            txtContratoComodato.Text = "0";
            txtEmbarques.Text = "0";
            txtPagos.Text = "0";
            txtOrdenesCompra.Text = "0";
            txtReclamaciones.Text = "0";

            ListaCobranzaCentroDistribucion = new List<CentroDistribucion>();
            rgCobranza.Rebind();
            ListaRentabilidadCentroDistribucion = new List<CentroDistribucion>();
            rgRentabilidad.Rebind();
        }
        private void HabilitarControlesAdminitrador()
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //habilitar/deshabilitar controles de administrador
            if (Sesion.Id_TU == 1)
                this.chkDeshabilitarReglaCons.Enabled = true; //solo habilitada para administrador           
            else
                this.chkDeshabilitarReglaCons.Enabled = false; //solo habilitada para administrador           
        }
        private void CargaDatosCentroDistribucion()
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            this.LlenarComboOficinas();
            //si el usuario es multioficina, llena combo de centos de dist.
            if (Sesion.U_MultiOfi)
            {
                this.HabilitarBotones(true, true, false, false, false, false); //estado de botones para la primer pestaña
                this.hiddenCdi.Value = string.Empty;
                this.hiddenMultiUSer.Value = "1";
            }
            else
            {
                this.HabilitarBotones(true, true, false, false, false, false); //estado de botones para la primer pestaña
                this.hiddenCdi.Value = Sesion.Id_Cd.ToString();
                this.hiddenMultiUSer.Value = "0";
            }
        }
        private void LlenarComboOficinas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
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
        private void LlenarObjetoCentroDistribucion(ref CentroDistribucion cd)
        {
            cd.Id_Emp = Convert.ToInt32(txtEmpresaID.Text);
            cd.Id_Reg = Convert.ToInt32(txtRegion.Text);
            cd.Id_Cd = Convert.ToInt32(txtCentroDistribucion.Text);
            cd.Cd_Activo = chkActivo.Checked;
            cd.Cd_Descripcion = txtNombreCD.Text;
            cd.Cd_Calle = txtCalle.Text;
            cd.Cd_Numero = txtNumeroCalle.Text;
            cd.Cd_Colonia = txtColonia.Text;
            cd.Cd_Ciudad = txtCiudad.Text;
            cd.Cd_Municipio = txtMunicipio.Text;
            cd.Cd_Estado = txtEstado.Text;
            cd.Cd_Pais = txtPais.Text;
            cd.Cd_CP = txtCp.Text;
            cd.Cd_Rfc = txtRfc.Text;
            cd.Cd_Tel = txtTel.Text;
            cd.Id_TipoCD = Convert.ToInt32(cmbTipo.SelectedValue);
            cd.Cd_Activo = chkActivo.Checked;
            cd.Cd_CreditoKey = txtCreditoProveedor.Value;
            cd.Cd_CreditoPapel = txtCreditoProveedorPapel.Value;

            cd.Cd_TasaCetes = txtCetesCd.Text == string.Empty ? txtCetesEstandar.Text == "" ? 0 : Convert.ToDouble(txtCetesEstandar.Text) : Convert.ToDouble(txtCetesCd.Text);
            cd.Cd_DiasCuentasPorCobrar = txtCuentasCd.Text == string.Empty ? txtCuentasEstandar.Text == "" ? 0 : Convert.ToInt32(txtCuentasEstandar.Text) : Convert.ToInt32(txtCuentasCd.Text);
            cd.Cd_Dias = txtDiasCd.Text == string.Empty ? txtDiasEstandar.Text == "" ? 0 : Convert.ToInt32(txtDiasEstandar.Text) : Convert.ToInt32(txtDiasCd.Text);
            cd.Cd_DiasInv = txtInventarioCd.Text == string.Empty ? txtInventarioEstandar.Text == "" ? 0 : Convert.ToInt32(txtInventarioEstandar.Text) : Convert.ToInt32(txtInventarioCd.Text);
            cd.Cd_FactorInvComodato = txtFactorInvCd.Text == string.Empty ? txtFactorInvEstandar.Text == "" ? 0 : Convert.ToDouble(txtFactorInvEstandar.Text) : Convert.ToDouble(txtFactorInvCd.Text);
            cd.Cd_FactorConvActFijo = txtFactorConCd.Text == string.Empty ? txtFactorConEstandar.Text == "" ? 0 : Convert.ToDouble(txtFactorConEstandar.Text) : Convert.ToDouble(txtFactorConCd.Text);
            cd.Cd_DiasFinanciaProv = txtFinanciamientoCd.Text == string.Empty ? txtfinanciamientoEstandar.Text == "" ? 0 : Convert.ToInt32(txtfinanciamientoEstandar.Text) : Convert.ToInt32(txtFinanciamientoCd.Text);
            cd.Cd_TasaIncCostoCapital = txtTasaCd.Text == string.Empty ? txtTasaEstandar.Text == "" ? 0 : Convert.ToDouble(txtTasaEstandar.Text) : Convert.ToDouble(txtTasaCd.Text);
            cd.Cd_Iva = txtIvaCd.Text == string.Empty ? txtIvaEstandar.Text == "" ? 0 : Convert.ToDouble(txtIvaEstandar.Text) : Convert.ToDouble(txtIvaCd.Text);
            cd.Cd_Flete = txtFleteCd.Text == string.Empty ? txtFleteEstandar.Text == "" ? 0 : Convert.ToDouble(txtFleteEstandar.Text) : Convert.ToDouble(txtFleteCd.Text);
            cd.Cd_ComisionRik = txtComisionCd.Text == string.Empty ? txtComisionEstandar.Text == "" ? 0 : Convert.ToDouble(txtComisionEstandar.Text) : Convert.ToDouble(txtComisionCd.Text);
            cd.Cd_OtrosGastosVar = txtOtrosCd.Text == string.Empty ? txtOtrosEstandar.Text == "" ? 0 : Convert.ToDouble(txtOtrosEstandar.Text) : Convert.ToDouble(txtOtrosCd.Text);
            cd.Cd_ContribucionGastosFijosOtros = txtGastofijoCd.Text == string.Empty ? txtGastofijoEstandar.Text == "" ? 0 : Convert.ToDouble(txtGastofijoEstandar.Text) : Convert.ToDouble(txtGastofijoCd.Text);
            cd.Cd_ContribucionGastosFijosPapel = txtGastofijopapelCd.Text == string.Empty ? txtGastofijopapelEstandar.Text == "" ? 0 : Convert.ToDouble(txtGastofijopapelEstandar.Text) : Convert.ToDouble(txtGastofijopapelCd.Text);
            cd.Cd_ISRyPTU = txtIsrCd.Text == string.Empty ? txtIsrEstandar.Text == "" ? 0 : Convert.ToDouble(txtIsrEstandar.Text) : Convert.ToDouble(txtIsrCd.Text);
            cd.Cd_CargoUCS = txtCargoCd.Text == string.Empty ? txtCargoEstandar.Text == "" ? 0 : Convert.ToDouble(txtCargoEstandar.Text) : Convert.ToDouble(txtCargoCd.Text);

            //cd.Cd_FacturasRangoInicio = txtFacturasRangoInicio.Text == string.Empty ? 0 : Convert.ToInt32(txtFacturasRangoInicio.Text);
            //cd.Cd_PartidaFacturas = txtPartidaFacturas.Text == string.Empty ? 0 : Convert.ToInt32(txtPartidaFacturas.Text);
            //cd.Cd_FacturasRangoFin = txtFacturasRangoFin.Text == string.Empty ? 0 : Convert.ToInt32(txtFacturasRangoFin.Text);
            cd.Cd_PartidaPedidos = txtPartidaPedidos.Text == string.Empty ? 0 : Convert.ToInt32(txtPartidaPedidos.Text);
            cd.Cd_IvaPedidosFacturacion = txtIvaPedidosFacturacion.Text == string.Empty ? 0 : Convert.ToInt32(txtIvaPedidosFacturacion.Text);
            //cd.Cd_ClientesRangoInicio = txtClientesRangoInicio.Text == string.Empty ? 0 : Convert.ToInt32(txtClientesRangoInicio.Text);
            //cd.Cd_ClientesRangoFin = txtClientesRangoFin.Text == string.Empty ? 0 : Convert.ToInt32(txtClientesRangoFin.Text);
            //cd.Cd_AjusteFromatoReng = txtAjusteFromatoReng.Text == string.Empty ? 0 : Convert.ToInt32(txtAjusteFromatoReng.Text);
            cd.Cd_MaximoTerritoriosSegmentos = txtMaximoTerritoriosSegmentos.Text == string.Empty ? 0 : Convert.ToInt32(txtMaximoTerritoriosSegmentos.Text);
            cd.Cd_FormatoFacturaRetIva = chkFormatoFacturaRetIva.Checked;
            cd.Cd_DeshabilitarReglaCons = chkDeshabilitarReglaCons.Checked;
            cd.Cd_ActivaCapPedRep = chkActivaCapPedRep.Checked;

            cd.Cd_PartidaRemisiones = txtPartidaRemisiones.Text == string.Empty ? 0 : Convert.ToInt32(txtPartidaRemisiones.Text);
            cd.Cd_PartidaEntradas = txtPartidaEntradas.Text == string.Empty ? 0 : Convert.ToInt32(txtPartidaEntradas.Text);
            cd.Cd_AjusteFromatoRengInventario = txtAjusteFromatoRengInventario.Text == string.Empty ? 0 : Convert.ToInt32(txtAjusteFromatoRengInventario.Text);

            cd.Cd_RelacionCobranza = txtRelacionCobranza.Text == string.Empty ? 0 : Convert.ToInt32(txtRelacionCobranza.Text);
            cd.Cd_InteresMoratorio = txtInteresMoratorio.Text == string.Empty ? 0 : Convert.ToInt32(txtInteresMoratorio.Text);
            cd.Cd_ContribucionBruta = txtContribucionBruta.Text == string.Empty ? 0 : Convert.ToDecimal(txtContribucionBruta.Text);
            cd.Cd_Amortizacion = txtAmortizacion.Text == string.Empty ? 0 : Convert.ToDecimal(txtAmortizacion.Text);
            cd.Cd_SaldosMenores = txtSaldosMenores.Text == string.Empty ? 0 : Convert.ToDecimal(txtSaldosMenores.Text);
            cd.Cd_CobranzaPersonaFormula = txtPersonaFormula.Text;
            cd.Cd_CobranzaPersonaAutoriza = txtPersonaAutoriza.Text;

            cd.Cd_TiempoEntrega = txtTiempoEntrega.Text == string.Empty ? 0 : Convert.ToInt32(txtTiempoEntrega.Text);
            cd.Cd_TiempoTransportacion = txtTiempoTransportacion.Text == string.Empty ? 0 : Convert.ToInt32(txtTiempoTransportacion.Text);
            if (string.IsNullOrEmpty(txtNumeroMacola.Text))
                cd.Cd_NumMacola = null;
            else
                cd.Cd_NumMacola = Convert.ToInt32(txtNumeroMacola.Text);

            cd.Cd_ActualizaEntradaAuto = chkActualiza.Checked;
            cd.Cd_FactorCosto = txtFactorCosto.Text == string.Empty ? 0 : Convert.ToInt32(txtFactorCosto.Text);
            cd.Cd_FactorCostoFinanciero = txtFactorCostoFinanciero.Value.HasValue ? txtFactorCostoFinanciero.Value.Value : 0;
            cd.CD_NuevoEsquemaCom = this.ChkCD_NuevoEsquemaCom.Checked;
            cd.CD_Gasto = this.TxtCD_Gasto.Text == "" ? 0 : double.Parse(this.TxtCD_Gasto.Text);
            cd.Cd_MargenDiferenciaDocs = txtMargenDiferenciaDocs.Text == string.Empty ? 0 : Convert.ToDouble(txtMargenDiferenciaDocs.Text);

            //Edsg26062017
            cd.CD_PermiteTerrMismaUEN = chkVariasUEN.Checked;



        }
        private void HabilitarBotones(bool nuevo, bool guardar, bool regresar, bool eliminar, bool imprimir, bool mail)
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            nuevo = _PermisoGuardar;

            if (guardar)
            {
                if (_PermisoGuardar || _PermisoModificar)
                    guardar = true;
                else
                    guardar = false;
            }

            if (eliminar)
                eliminar = _PermisoEliminar;

            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = nuevo;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = guardar;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("undo")).Visible = regresar;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = eliminar;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = imprimir;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("mail")).Visible = mail;
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CD_PermisosU CN_PermisosU = new CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Sesion.U_MultiOfi)
                    {
                        this.RadToolBar1.Items[6].Visible = _PermisoGuardar; //new
                        if (_PermisoGuardar || _PermisoModificar)
                            this.RadToolBar1.Items[5].Visible = true; //save                       
                        this.RadToolBar1.Items[4].Visible = false; //Regresar
                        this.RadToolBar1.Items[3].Visible = _PermisoEliminar; //Eliminar
                        this.RadToolBar1.Items[2].Visible = false; //Imprimir
                        this.RadToolBar1.Items[1].Visible = false; //Correo
                    }
                    else //usuario No multi-CentroDistribucion
                    {
                        this.RadToolBar1.Items[6].Visible = false; //new
                        if (_PermisoGuardar || _PermisoModificar)
                            this.RadToolBar1.Items[5].Visible = true; //save                       
                        this.RadToolBar1.Items[4].Visible = false; //Regresar
                        this.RadToolBar1.Items[3].Visible = _PermisoEliminar; //Eliminar
                        this.RadToolBar1.Items[2].Visible = false; //Imprimir
                        this.RadToolBar1.Items[1].Visible = false; //Correo
                    }
                }
                else
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EstablecerSigClaveCliente(int Id_Emp, int Id_Cd_Ver)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //establecer siguiente clave de nuevo cliente del centro de dist.
            int claveClienteSig = 0;
            new CN_CatCliente().ConsultarClienteSigCentroDist(ref claveClienteSig, Id_Emp, Id_Cd_Ver, sesion.Emp_Cnx);
            txtClienteClaveSig.Text = claveClienteSig.ToString();
        }


        private void LlenarFormularioCentroDistribucion(int Id_Cd)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            //Datos del centro de distribución
            CentroDistribucion cd = new CentroDistribucion();
            new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, Id_Cd, sesion.Id_Emp, sesion.Emp_Cnx);

            //establecer siguiente clave de nuevo cliente del centro de dist.
            this.EstablecerSigClaveCliente(sesion.Id_Emp, Id_Cd);

            //Datos de valuación de proyectos del C. de Dist.
            CentroDistribucion cdValProy = new CentroDistribucion();
            new CN_CatCentroDistribucion().ConsultaCentroDistribucion_DatosValProyecto(ref cdValProy, sesion.Emp_Cnx);

            //*****Llenar Datos Genrales del centro de distribucion******
            #region Llenar Datos Genrales del centro de distribucion

            txtEmpresaID.Text = cd.Id_Emp.ToString();
            cmbEmpresaID.Text = "";
            cmbEmpresaID.ClearSelection();
            if (cd.Id_Emp != 0)
            {
                cmbEmpresaID.SelectedValue = cd.Id_Emp.ToString();
            }
            LlenarComboRegiones();
            txtRegion.Text = cd.Id_Reg.ToString();
            cmbRegion.Text = "";
            cmbRegion.ClearSelection();
            if (cd.Id_Reg != 0)
            {
                cmbRegion.SelectedValue = cd.Id_Reg.ToString();
            }

            txtCentroDistribucion.Text = cd.Id_Cd.ToString();
            chkActivo.Checked = cd.Cd_Activo;
            txtNombreCD.Text = cd.Cd_Descripcion;
            txtCalle.Text = cd.Cd_Calle;
            txtNumeroCalle.Text = cd.Cd_Numero;
            txtColonia.Text = cd.Cd_Colonia.ToString();
            txtCiudad.Text = cd.Cd_Ciudad;
            txtMunicipio.Text = cd.Cd_Municipio;
            txtEstado.Text = cd.Cd_Estado;
            txtPais.Text = cd.Cd_Pais;
            txtCp.Text = cd.Cd_CP;

            txtRfc.Text = cd.Cd_Rfc;
            cmbTipo.Text = "";
            cmbTipo.ClearSelection();
            if (cd.Id_TipoCD != -1)
            {
                cmbTipo.SelectedValue = cd.Id_TipoCD.ToString();
            }

            txtTel.Text = cd.Cd_Tel;
            //cmbFormato.Text = "";
            //cmbFormato.ClearSelection(); 
            //if (cd.Cd_Formato != 0)
            //{
            //    cmbFormato.SelectedValue = cd.Cd_Formato.ToString();
            //}

            #endregion

            //Llenar Datos de valuación de proyecto del centro de distribucion
            #region Llenar Datos de valuación de proyecto del centro de distribucion

            txtCetesCd.Text = cd.Cd_TasaCetes == 0 ? cdValProy.Cd_TasaCetes.ToString() : cd.Cd_TasaCetes.ToString();
            txtCetesEstandar.Text = cdValProy.Cd_TasaCetes.ToString();

            txtIvaCd.Text = cd.Cd_Iva == 0 ? cdValProy.Cd_Iva.ToString() : cd.Cd_Iva.ToString();
            txtIvaEstandar.Text = cdValProy.Cd_Iva.ToString();

            txtCuentasCd.Text = cd.Cd_DiasCuentasPorCobrar == 0 ? cdValProy.Cd_DiasCuentasPorCobrar.ToString() : cd.Cd_DiasCuentasPorCobrar.ToString();
            txtCuentasEstandar.Text = cdValProy.Cd_DiasCuentasPorCobrar.ToString();

            txtFleteCd.Text = cd.Cd_Flete == 0 ? cdValProy.Cd_Flete.ToString() : cd.Cd_Flete.ToString();
            txtFleteEstandar.Text = cdValProy.Cd_Flete.ToString();

            txtDiasCd.Text = cd.Cd_Dias == 0 ? cdValProy.Cd_Dias.ToString() : cd.Cd_Dias.ToString();
            txtDiasEstandar.Text = cdValProy.Cd_Dias.ToString();

            txtComisionCd.Text = cd.Cd_ComisionRik == 0 ? cdValProy.Cd_ComisionRik.ToString() : cd.Cd_ComisionRik.ToString();
            txtComisionEstandar.Text = cdValProy.Cd_ComisionRik.ToString();

            txtInventarioCd.Text = cd.Cd_DiasInv == 0 ? cdValProy.Cd_DiasInv.ToString() : cd.Cd_DiasInv.ToString();
            txtInventarioEstandar.Text = cdValProy.Cd_DiasInv.ToString();

            txtOtrosCd.Text = cd.Cd_OtrosGastosVar == 0 ? cdValProy.Cd_OtrosGastosVar.ToString() : cd.Cd_OtrosGastosVar.ToString();
            txtOtrosEstandar.Text = cdValProy.Cd_OtrosGastosVar.ToString();

            txtFactorInvCd.Text = cd.Cd_FactorInvComodato == 0 ? cdValProy.Cd_FactorInvComodato.ToString() : cd.Cd_FactorInvComodato.ToString();
            txtFactorInvEstandar.Text = cdValProy.Cd_FactorInvComodato.ToString();

            txtGastofijoCd.Text = cd.Cd_ContribucionGastosFijosOtros == 0 ? cdValProy.Cd_ContribucionGastosFijosOtros.ToString() : cd.Cd_ContribucionGastosFijosOtros.ToString();
            txtGastofijoEstandar.Text = cdValProy.Cd_ContribucionGastosFijosOtros.ToString();

            txtFactorConCd.Text = cd.Cd_FactorConvActFijo == 0 ? cdValProy.Cd_FactorConvActFijo.ToString() : cd.Cd_FactorConvActFijo.ToString();
            txtFactorConEstandar.Text = cdValProy.Cd_FactorConvActFijo.ToString();

            txtGastofijopapelCd.Text = cd.Cd_ContribucionGastosFijosPapel == 0 ? cdValProy.Cd_ContribucionGastosFijosPapel.ToString() : cd.Cd_ContribucionGastosFijosPapel.ToString();
            txtGastofijopapelEstandar.Text = cdValProy.Cd_ContribucionGastosFijosPapel.ToString();

            txtFinanciamientoCd.Text = cd.Cd_DiasFinanciaProv == 0 ? cdValProy.Cd_DiasFinanciaProv.ToString() : cd.Cd_DiasFinanciaProv.ToString();
            txtfinanciamientoEstandar.Text = cdValProy.Cd_DiasFinanciaProv.ToString();

            txtIsrCd.Text = cd.Cd_ISRyPTU == 0 ? cdValProy.Cd_ISRyPTU.ToString() : cd.Cd_ISRyPTU.ToString();
            txtIsrEstandar.Text = cdValProy.Cd_ISRyPTU.ToString();

            txtTasaCd.Text = cd.Cd_TasaIncCostoCapital == 0 ? cdValProy.Cd_TasaIncCostoCapital.ToString() : cd.Cd_TasaIncCostoCapital.ToString();
            txtTasaEstandar.Text = cdValProy.Cd_TasaIncCostoCapital.ToString();

            txtCargoCd.Text = cd.Cd_CargoUCS == 0 ? cdValProy.Cd_CargoUCS.ToString() : cd.Cd_CargoUCS.ToString();
            txtCargoEstandar.Text = cdValProy.Cd_CargoUCS.ToString();

            txtCreditoProveedor.DbValue = cd.Cd_CreditoKey;
            txtCreditoProveedorPapel.DbValue = cd.Cd_CreditoPapel;

            #endregion

            //Llenar datos de Pedidos y facturación
            #region Llenar datos de Pedidos y facturación

            //txtFacturasRangoInicio.Text = cd.Cd_FacturasRangoInicio.ToString();
            //txtPartidaFacturas.Text = cd.Cd_PartidaFacturas.ToString();
            //txtFacturasRangoFin.Text = cd.Cd_FacturasRangoFin.ToString();
            txtPartidaPedidos.Text = cd.Cd_PartidaPedidos.ToString();
            txtIvaPedidosFacturacion.Text = cd.Cd_IvaPedidosFacturacion.ToString();
            //txtClientesRangoInicio.Text = cd.Cd_ClientesRangoInicio.ToString();
            //txtClientesRangoFin.Text = cd.Cd_ClientesRangoFin.ToString();
            //txtAjusteFromatoReng.Text = cd.Cd_AjusteFromatoReng.ToString();
            txtMaximoTerritoriosSegmentos.Text = cd.Cd_MaximoTerritoriosSegmentos.ToString();
            chkFormatoFacturaRetIva.Checked = cd.Cd_FormatoFacturaRetIva;
            chkDeshabilitarReglaCons.Checked = cd.Cd_DeshabilitarReglaCons;
            chkActivaCapPedRep.Checked = cd.Cd_ActivaCapPedRep;
            txtMargenDiferenciaDocs.Text = cd.Cd_MargenDiferenciaDocs.ToString();
            #endregion

            //Llenar datos de info de Inventarios
            #region Llenar datos de info de Inventarios

            txtPartidaRemisiones.Text = cd.Cd_PartidaRemisiones.ToString();
            txtPartidaEntradas.Text = cd.Cd_PartidaEntradas.ToString();
            txtAjusteFromatoRengInventario.Text = cd.Cd_AjusteFromatoRengInventario.ToString();

            #endregion

            //Llenar datos de info de Cobranza
            #region Llenar datos de info de Cobranza

            txtRelacionCobranza.Text = cd.Cd_RelacionCobranza.ToString();
            txtInteresMoratorio.Text = cd.Cd_InteresMoratorio.ToString();
            txtContribucionBruta.Text = cd.Cd_ContribucionBruta.ToString();
            txtAmortizacion.Text = cd.Cd_Amortizacion.ToString();
            txtSaldosMenores.Text = cd.Cd_SaldosMenores.ToString();
            txtPersonaFormula.Text = cd.Cd_CobranzaPersonaFormula;
            txtPersonaAutoriza.Text = cd.Cd_CobranzaPersonaAutoriza;
            txtFactorCostoFinanciero.Value = cd.Cd_FactorCostoFinanciero;
            #endregion

            //Llenar info de administración de inventarios
            #region Llenar info de administración de inventarios

            txtTiempoEntrega.Text = cd.Cd_TiempoEntrega.ToString();
            txtTiempoTransportacion.Text = cd.Cd_TiempoTransportacion.ToString();
            txtNumeroMacola.Text = cd.Cd_NumMacola != null ? cd.Cd_NumMacola.ToString() : string.Empty;

            #endregion

            //Llenar info de compras locales
            #region Llenar info de compras locales

            chkActualiza.Checked = cd.Cd_ActualizaEntradaAuto;
            txtFactorCosto.Text = cd.Cd_FactorCosto.ToString();

            #endregion

            #region Totalizadores

            //Establecer ultimo proyecto de valuación de proyetos de la Sucursal
            txtProyecto.Text = cd.ConsecutivoValProyCD.ToString();

            //Establecer total de pedidos de la Sucursal
            txtContadorPedidos.Text = cd.CantidadPedidosCD.ToString();

            //Establecer total de remisiones de la Sucursal
            txtRemisiones.Text = cd.CantidadRemisionesCD.ToString();

            //Establecer total de Entradas de la Sucursal
            txtEntradas.Text = cd.CantidadEntradasCD.ToString();

            //Establecer total de Salidas de la Sucursal
            txtSalidas.Text = cd.CantidadSalidasCD.ToString();

            //Establecer total de Devoluciones de la Sucursal
            txtDevoluciones.Text = cd.CantidadDevolucionesCD.ToString();

            //Establecer total de Contratos de comodato de la Sucursal
            txtContratoComodato.Text = cd.CantidadContratosComCD.ToString();

            //Establecer total de Embarques de la Sucursal
            txtEmbarques.Text = cd.CantidadEmbarquesCD.ToString();

            //Establecer total de Embarques de la Sucursal
            //txtNotasCargo.Text = cd.CantidadNotaCargoCD.ToString();

            //Establecer total de Embarques de la Sucursal
            //txtNotasCredito.Text = cd.CantidadNotaCreditoCD.ToString();

            //Establecer total de Embarques de la Sucursal
            txtPagos.Text = cd.CantidadPagosCD.ToString();

            //Establecer total de Ordenes de compra de la Sucursal
            txtOrdenesCompra.Text = cd.CantidadOrdenesCompraCD.ToString();

            //Establecer Rerclamaciones de compra de la Sucursal
            txtReclamaciones.Text = cd.CantidadReclamacionesCD.ToString();

            #endregion

            #region Comisiones

            this.TxtCD_Gasto.Text = cd.CD_Gasto.ToString();
            this.ChkCD_NuevoEsquemaCom.Checked = cd.CD_NuevoEsquemaCom;
            //this.rgCategorias.Rebind();

            #endregion

            //Edsg28062017

            chkVariasUEN.Checked = cd.CD_PermiteTerrMismaUEN;

        }
        private void LlenarControlesDatosCentroDistValuacionProyectos(ref CentroDistribucion cdValProy)
        {
            txtCetesEstandar.Text = cdValProy.Cd_TasaCetes.ToString();
            txtIvaEstandar.Text = cdValProy.Cd_Iva.ToString();
            txtCuentasEstandar.Text = cdValProy.Cd_DiasCuentasPorCobrar.ToString();
            txtFleteEstandar.Text = cdValProy.Cd_Flete.ToString();
            txtDiasEstandar.Text = cdValProy.Cd_Dias.ToString();
            txtComisionEstandar.Text = cdValProy.Cd_ComisionRik.ToString();
            txtInventarioEstandar.Text = cdValProy.Cd_DiasInv.ToString();
            txtOtrosEstandar.Text = cdValProy.Cd_OtrosGastosVar.ToString();
            txtFactorInvEstandar.Text = cdValProy.Cd_FactorInvComodato.ToString();
            txtGastofijoEstandar.Text = cdValProy.Cd_OtrosGastosVar.ToString();
            txtFactorConEstandar.Text = cdValProy.Cd_FactorConvActFijo.ToString();
            txtGastofijopapelEstandar.Text = cdValProy.Cd_ContribucionGastosFijosPapel.ToString();
            txtfinanciamientoEstandar.Text = cdValProy.Cd_DiasFinanciaProv.ToString();
            txtIsrEstandar.Text = cdValProy.Cd_ISRyPTU.ToString();
            txtTasaEstandar.Text = cdValProy.Cd_TasaIncCostoCapital.ToString();
            txtCargoEstandar.Text = cdValProy.Cd_CargoUCS.ToString();
        }
        private void LlenarComboEmpresas()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Emp_Cnx, "spCatEmpresaCombo", ref cmbEmpresaID);
        }
        private void LlenarComboRegiones()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatRegion_Combo", ref cmbRegion);
            this.cmbRegion.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgCobranza_insert_error"))
                    Alerta("Error al momento de agregar un registro a la tabla de cobranza");
                else
                    if (mensaje.Contains("rgCobranza_insert_repetida"))
                        Alerta("Este registro ya ha sido capturado");
                    else
                        if (mensaje.Contains("rgCobranza_delete_item_error"))
                            Alerta("Error al momento de eliminar el registro de la tabla de cobranza");
                        else
                            if (mensaje.Contains("radGrid_PageIndexChanged"))
                                Alerta("Error al cambiar de página");
                            else
                                if (mensaje.Contains("rgRentabilidad_insert_error"))
                                    Alerta("Error al momento de agregar un registro a la tabla de rentabilidad");
                                else
                                    if (mensaje.Contains("rgRentabilidad_insert_repetida"))
                                        Alerta("Este registro ya ha sido capturado");
                                    else
                                        if (mensaje.Contains("rgRentabilidad_delete_item_error"))
                                            Alerta("Error al momento de eliminar el registro de la tabla de rentabilidad");
                                        else
                                            if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                                Alerta("Error al cargar el centro de distribución");
                                            else
                                                if (mensaje.Contains("PermisoGuardarNo"))
                                                    Alerta("No tiene permisos para grabar");
                                                else
                                                    if (mensaje.Contains("PermisoModificarNo"))
                                                        Alerta("No tiene permisos para actualizar");
                                                    else
                                                        if (mensaje.Contains("PermisoEliminarNo"))
                                                            Alerta("No tiene permisos para eliminar");
                                                        else
                                                            if (mensaje.Contains("CatCdRelEmpReg_error"))
                                                                Alerta(string.Concat("No se pudo guardar los datos del centro de distribuci&oacute;n"
                                                                    , "<br/>"
                                                                    , "La regi&oacute;n ", cmbRegion.SelectedItem.Text
                                                                    , " no está asociada a la empresa ", cmbEmpresaID.SelectedItem.Text));
                                                            else
                                                                if (mensaje.Contains("CatCdIdRepetida_error"))
                                                                    Alerta("La clave ya existe");
                                                                else
                                                                    if (mensaje.Contains("CatCdDescripcionRepetida_error"))
                                                                        Alerta("La descripción ya existe");
                                                                    else
                                                                        if (mensaje.Contains("Cd_insert_ok"))
                                                                            Alerta("Los datos se guardaron correctamente");
                                                                        else
                                                                            if (mensaje.Contains("Cd_insert_error"))
                                                                                Alerta("No se pudo guardar los datos del centro de distribuci&oacute;n");
                                                                            else
                                                                                if (mensaje.Contains("Cd_update_ok"))
                                                                                    Alerta("Los datos se modificaron correctamente");
                                                                                else
                                                                                    if (mensaje.Contains("Cd_update_error"))
                                                                                        Alerta("No se pudo actualizar los datos del centro de distribuci&oacute;n");
                                                                                    else
                                                                                        if (mensaje.Contains("Cd_delete_ok"))
                                                                                            Alerta("Los datos se eliminaron correctamente");
                                                                                        else
                                                                                            if (mensaje.Contains("Cd_delete_error"))
                                                                                                Alerta("No se pudo eliminar los datos del centro de distribuci&oacute;n");
                                                                                            else
                                                                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        protected bool ListaCobranza_Agregar(CentroDistribucion centro_cobranza)
        {
            try
            {

                //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //CN_CatCentroDistribucion centro1 = new CN_CatCentroDistribucion();
                //List<CentroDistribucion> ListaCentroDistribucion = new List<CentroDistribucion>();
                //centro1.ConsultarCobranzaCentroDistribucion(ref ListaCentroDistribucion, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                //ListaCobranzaCentroDistribucion = ListaCentroDistribucion;
                List<CentroDistribucion> lista = ListaCobranzaCentroDistribucion;
                //buscar el registro de cobranza en la lista para ver si ya existe
                for (int i = 0; i < lista.Count; i++)
                {
                    CentroDistribucion cobranza = lista[i];
                    if (cobranza.Cob_DiaInicio == centro_cobranza.Cob_DiaInicio && cobranza.Cob_DiaLimite == centro_cobranza.Cob_DiaLimite && cobranza.Cob_Multiplicador == centro_cobranza.Cob_Multiplicador)
                    {//si el producto es el mismo

                        Alerta("Este registro ya ha sido capturado");
                        return true;
                    }
                    if (centro_cobranza.Cob_DiaLimite < centro_cobranza.Cob_DiaInicio)
                    {
                        Alerta("El día inicio no debe ser mayor al día límite");
                        return true;
                    }
                    if (cobranza.Cob_DiaInicio <= centro_cobranza.Cob_DiaInicio & centro_cobranza.Cob_DiaInicio <= cobranza.Cob_DiaLimite)
                    {
                        Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                        return true;
                    }
                    if (cobranza.Cob_DiaInicio <= centro_cobranza.Cob_DiaLimite & centro_cobranza.Cob_DiaLimite <= cobranza.Cob_DiaLimite)
                    {
                        Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                        return true;
                    }
                    if (cobranza.Cob_DiaInicio >= centro_cobranza.Cob_DiaInicio & centro_cobranza.Cob_DiaLimite >= cobranza.Cob_DiaLimite)
                    {
                        Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                        return true;
                    }
                    if (centro_cobranza.Cob_DiaLimite < 0 || centro_cobranza.Cob_DiaInicio < 0 || centro_cobranza.Cob_Multiplicador < 0)
                    {
                        Alerta("Los días o el multiplicador no deben ser menor a cero");
                        return true;
                    }
                }

                ListaCobranzaCentroDistribucion.Add(centro_cobranza);
                //centro1.AgregarCentroDistribucion_Cobranza(ref centro_cobranza, sesion);
                //lista.Add(centro_cobranza);
                //this.ListaCobranzaCentroDistribucion = lista;

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            return false;

        }
        private void ObtenerCobranza()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCentroDistribucion centro = new CN_CatCentroDistribucion();
                List<CentroDistribucion> ListaCentroDistribucion = new List<CentroDistribucion>();
                centro.ConsultarCobranzaCentroDistribucion(ref ListaCentroDistribucion, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                ListaCobranzaCentroDistribucion = ListaCentroDistribucion;
                rgCobranza.Rebind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ObtenerRentabilidad()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCentroDistribucion centro = new CN_CatCentroDistribucion();
                List<CentroDistribucion> ListaCentroDistribucion = new List<CentroDistribucion>();
                centro.ConsultarRentabilidadCentroDistribucion(ref ListaCentroDistribucion, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                ListaRentabilidadCentroDistribucion = ListaCentroDistribucion;
                rgRentabilidad.Rebind();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //private void ObtenerCategorias()
        //{
        //    try
        //    {
        //        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        CN_CategoriaParticipacion cn_cat = new CN_CategoriaParticipacion();
        //        List<CategoriaParticipacion> List = new List<CategoriaParticipacion>();

        //        cn_cat.CatCategoriaParticipacion_ConsultaLista(sesion, ref List);

        //        ListaCategoriasCentroDistribucion = List;
        //        this.rgCategorias.Rebind();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        protected bool ListaRentabilidad_Agregar(CentroDistribucion centro_Rentabilidad)
        {
            try
            {
                //int validador = 0;
                //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //CN_CatCentroDistribucion centro1 = new CN_CatCentroDistribucion();
                //List<CentroDistribucion> ListaCentroDistribucion = new List<CentroDistribucion>();
                //centro1.ConsultarRentabilidadCentroDistribucion(ref ListaCentroDistribucion, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                //ListaRentabilidadCentroDistribucion = ListaCentroDistribucion;
                List<CentroDistribucion> lista = this.ListaRentabilidadCentroDistribucion;
                //buscar el registro de cobranza en la lista para ver si ya existe
                for (int i = 0; i < lista.Count; i++)
                {
                    CentroDistribucion cobranza = lista[i];
                    if (cobranza.Rent_LInferior == centro_Rentabilidad.Rent_LInferior && cobranza.Rent_LSuperior == centro_Rentabilidad.Rent_LSuperior && cobranza.Rent_Multiplicador == centro_Rentabilidad.Rent_Multiplicador)
                    {//si el producto es el mismo
                        Alerta("Este registro ya ha sido capturado");  //throw new Exception("rgCobranza_insert_repetida");
                        return true;
                    }
                    if (centro_Rentabilidad.Rent_LSuperior < centro_Rentabilidad.Rent_LInferior)
                    {
                        Alerta("El límite inferior no debe ser mayor al límite superior");
                        return true;
                    }
                    if (cobranza.Rent_LInferior <= centro_Rentabilidad.Rent_LInferior & centro_Rentabilidad.Rent_LInferior <= cobranza.Rent_LSuperior)
                    {
                        Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                        return true;
                    }
                    if (cobranza.Rent_LInferior <= centro_Rentabilidad.Rent_LSuperior & centro_Rentabilidad.Rent_LSuperior <= cobranza.Rent_LSuperior)
                    {
                        Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                        return true;
                    }
                    if (cobranza.Rent_LInferior >= centro_Rentabilidad.Rent_LInferior & centro_Rentabilidad.Rent_LSuperior >= cobranza.Rent_LSuperior)
                    {
                        Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                        return true;
                    }
                }
                if (centro_Rentabilidad.Rent_LInferior < 0 || centro_Rentabilidad.Rent_LSuperior < 0 || centro_Rentabilidad.Rent_Multiplicador < 0)
                {
                    Alerta("Los límites o el multiplicador no deben ser menor a cero");
                    return true;
                }
                //centro1.AgregarCentroDistribucion_Rentabilidad(ref centro_Rentabilidad, sesion);
                ListaRentabilidadCentroDistribucion.Add(centro_Rentabilidad);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRentabilidad_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
            return false;
        }
        protected void ListaRentabilidad_Modificar(CentroDistribucion centro_Rentabilidad, int index)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCentroDistribucion centro1 = new CN_CatCentroDistribucion();
                List<CentroDistribucion> ListaCentroDistribucion = new List<CentroDistribucion>();
                centro1.ConsultarRentabilidadCentroDistribucion(ref ListaCentroDistribucion, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                ListaRentabilidadCentroDistribucion = ListaCentroDistribucion;
                List<CentroDistribucion> lista = this.ListaRentabilidadCentroDistribucion;

                //buscar el registro de cobranza en la lista  
                if (lista.Count > 0)
                {
                    CentroDistribucion centro = lista[index];
                    if (centro.Id_Rent == centro_Rentabilidad.Id_Rent)
                    {  //  lista[index] = centro_cobranza;
                        int verificador = 0;
                        int validador = 0;
                        lista.RemoveAt(index);
                        for (int i = 0; i < lista.Count; i++)
                        {
                            CentroDistribucion cobranza = lista[i];
                            if (cobranza.Rent_LInferior <= centro_Rentabilidad.Rent_LInferior & centro_Rentabilidad.Rent_LInferior <= cobranza.Rent_LSuperior)
                            {
                                validador = 1;
                                Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                            }
                            if (cobranza.Rent_LInferior <= centro_Rentabilidad.Rent_LSuperior & centro_Rentabilidad.Rent_LSuperior <= cobranza.Rent_LSuperior)
                            {
                                validador = 1;
                                Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                            }
                            if (cobranza.Rent_LInferior >= centro_Rentabilidad.Rent_LInferior & centro_Rentabilidad.Rent_LSuperior >= cobranza.Rent_LSuperior)
                            {
                                validador = 1;
                                Alerta("El registro no debe ser igual ni estar dentro del rango de otro registro");
                            }
                        }
                        if (centro_Rentabilidad.Rent_LSuperior < centro_Rentabilidad.Rent_LInferior)
                        {
                            validador = 1;
                            Alerta("El límite inferior no debe ser mayor al límite superior");
                        }
                        if (centro_Rentabilidad.Rent_LSuperior < 0 || centro_Rentabilidad.Rent_LInferior < 0 || centro_Rentabilidad.Rent_Multiplicador < 0)
                        {
                            validador = 1;
                            Alerta("Los límites o el multiplicador no deben ser menor a cero");
                        }
                        if (validador == 0)
                            centro1.ModificarCentroDistribucion_Rentabilidad(ref centro_Rentabilidad, sesion, verificador);
                    }
                }
                else
                {
                    string mensaje = string.Concat("rgRentabilidad_insert_error");
                    this.DisplayMensajeAlerta(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRentabilidad_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        private void LlenarZonas()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatZonas cn_zonas = new CN_CatZonas();
            List<CentroDistribucion> list = new List<CentroDistribucion>();
            try
            {

                cn_zonas.Consultar(sesion.Id_Emp, CmbCentro.Visible ? Convert.ToInt32(CmbCentro.SelectedValue) : sesion.Id_Cd_Ver, ref list, sesion.Emp_Cnx);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PERTENECE@@"))
                {
                    lblZonas.Text = ex.Message.Replace("PERTENECE@@", "");
                    lblZonas1.Text = "";
                    listZonas.Visible = false;
                    return;
                }
                else
                {
                    throw ex;
                }
            }
            lblZonas.Text = "Centros que pertenecen a la zona de";
            lblZonas1.Text = sesion.Id_Cd_Ver.ToString() + "-" + sesion.Cd_Nombre;
            listZonas.Visible = true;
            listZonas.Items.Clear();
            foreach (CentroDistribucion c in list)
            {
                RadListBoxItem rlbi = new RadListBoxItem();
                rlbi.Value = c.Id_Cd.ToString();
                rlbi.Text = c.Cd_Descripcion;
                rlbi.Checked = c.Generico;
                listZonas.Items.Add(rlbi);
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
        private void Alerta(string mensaje)
        {
            try
            {
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
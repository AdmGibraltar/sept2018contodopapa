using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Configuration;

namespace SIANWEB
{
    public partial class CapGestionRentabilidadSimulador : System.Web.UI.Page
    {
        #region Variables

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.Inicializar();


                //rgGestionRentabilidadSimuladorResultados.ClientSettings.Scrolling.AllowScroll = true;
                //rgGestionRentabilidadSimuladorResultados.ClientSettings.Scrolling.UseStaticHeaders = true;



                CN_CatCalendario cn_calenda = new CN_CatCalendario();
                Calendario c = new Calendario();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];


                TxtNumeroCliente.Text = Convert.ToString(Request.QueryString["Id_Cte"]);
                TxtNombreCliente.Text = Convert.ToString(Request.QueryString["TxtNombreCliente"]);
                txtTerritorio.Text = Convert.ToString(Request.QueryString["Id_Ter"]);

                if (Convert.ToInt32(Request.QueryString["txtMesInicial"]) <= 9)
                {
                    txtMesInicial.SelectedValue = "0" + Convert.ToString(Request.QueryString["txtMesInicial"]);
                }
                else
                {
                    txtMesInicial.SelectedValue = Convert.ToString(Request.QueryString["txtMesInicial"]);
                }



                if (Convert.ToInt32(Request.QueryString["txtMesFinal"]) <= 9)
                {
                    txtMesFinal.SelectedValue = "0" + Convert.ToString(Request.QueryString["txtMesFinal"]);
                }
                else
                {
                    txtMesFinal.SelectedValue = Convert.ToString(Request.QueryString["txtMesFinal"]);
                }

                TxtAnioInicial.Text = Convert.ToString(Request.QueryString["TxtAnioInicial"]);
                TxtAnioFinal.Text = Convert.ToString(Request.QueryString["TxtAnioFinal"]);



                ///txtDondeViene aqui me quede regitrando de donde viene
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


                Session["Sesion" + Session.SessionID] = sesion;


                rgGestionRentabilidadSimulador.Rebind();
                rgGestionRentabilidadResultados.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion
        #region Funciones
        protected void rgGestionRentabilidadSimulador_InsertCommand(object sender, GridCommandEventArgs e)
        {

        }
        protected void rgGestionRentabilidadResultados_InsertCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void rgGestionRentabilidadSimulador_UpdateCommand(object sender, GridCommandEventArgs e)
        {
        }
        protected void rgGestionRentabilidadResultados_UpdateCommand(object sender, GridCommandEventArgs e)
        {
        }
        protected void rgGestionRentabilidadSimulador_DeleteCommand(object sender, GridCommandEventArgs e)
        {
           



            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];


            string EId_PrdP = "";
            string CDI = "";
            string Terr = "";
            string Cte = "";
            string Eventa = "";
            string ECosto = "";
            string EUtilidadBruta = "";
            string EPorcUBReal = "";
            string MesInicial = "";
            string AnioInicial = "";
            string MesFinal = "";
            string AnioFinal = "";

            string Ecantidad = "";
            string EPrecioVenta = "";
            string EPrecioDistribuidor = "";



            GridItem editItem = e.Item;
            EId_PrdP = (rgGestionRentabilidadSimulador.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("NEId_Prd") as Label).Text;
            CDI = Sesion.Id_Cd_Ver.ToString();
            Terr = txtTerritorio.Text;
            Cte = TxtNumeroCliente.Text;


            Eventa = (rgGestionRentabilidadSimulador.MasterTableView.Items[e.Item.ItemIndex]["venta"].FindControl("venta") as Label).Text;
            ECosto = (rgGestionRentabilidadSimulador.MasterTableView.Items[e.Item.ItemIndex]["Costo"].FindControl("Costo") as Label).Text;
            EUtilidadBruta = (rgGestionRentabilidadSimulador.MasterTableView.Items[e.Item.ItemIndex]["UtilidadBruta"].FindControl("UtilidadBruta") as Label).Text;
            EPorcUBReal = (rgGestionRentabilidadSimulador.MasterTableView.Items[e.Item.ItemIndex]["PorcUBReal"].FindControl("PorcUBReal") as Label).Text;
            MesInicial = txtMesInicial.SelectedValue;
            AnioInicial = TxtAnioInicial.Text;
            MesFinal = txtMesFinal.SelectedValue;
            AnioFinal = TxtAnioFinal.Text;

            Ecantidad = (rgGestionRentabilidadSimulador.MasterTableView.Items[e.Item.ItemIndex]["cantidad"].FindControl("cantidad") as Label).Text;
            EPrecioVenta = (rgGestionRentabilidadSimulador.MasterTableView.Items[e.Item.ItemIndex]["PrecioVenta"].FindControl("PrecioVenta") as Label).Text;
            EPrecioDistribuidor = (rgGestionRentabilidadSimulador.MasterTableView.Items[e.Item.ItemIndex]["PrecioDistribuidor"].FindControl("PrecioDistribuidor") as Label).Text;






            new CN_GestionRentabilidadSimulador().EliminaGestionRentabilidadSimulador(
            CDI,
            Terr,
            Cte,
            EId_PrdP,
            Sesion.Emp_Cnx
                     , Eventa
                      , ECosto
                      , EUtilidadBruta
                      , EPorcUBReal
                      , MesInicial
                      , AnioInicial
                      , MesFinal
                      , AnioFinal
                      , Ecantidad
                      , EPrecioVenta
                      , EPrecioDistribuidor
 
                );

            List<GestionRentabilidadSimulador> listGestionRentabilidadSimulador = new List<GestionRentabilidadSimulador>();
            GestionRentabilidadSimulador gestionRentabilidadSimulador = new GestionRentabilidadSimulador();


            new CN_GestionRentabilidadSimulador().ConsultaGestionRentabilidadSimulador_Buscar(gestionRentabilidadSimulador
                            , Sesion.Emp_Cnx
                            , ref listGestionRentabilidadSimulador
                            , Sesion.Id_Emp
                            , Sesion.Id_Cd_Ver
                            , this.TxtNumeroCliente.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Cte"]) : this.TxtNumeroCliente.Text
                            , this.txtTerritorio.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Ter"]) : this.txtTerritorio.Text
                            , this.TxtNombreCliente.Text
                            , Convert.ToInt32(Request.QueryString["txtMesInicial"])
                            , Convert.ToInt32(Request.QueryString["TxtAnioInicial"])
                            , Convert.ToInt32(Request.QueryString["txtMesFinal"])
                            , Convert.ToInt32(Request.QueryString["TxtAnioFinal"])
                            , Sesion.Id_U
                            , "S"
                            );


            rgGestionRentabilidadResultados.DataSource = listGestionRentabilidadSimulador;
            rgGestionRentabilidadResultados.Rebind();
            ActualizaTotales();
        }
        protected void rgGestionRentabilidadResultados_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            Alerta("Prueba");
        }
        protected void rgGestionRentabilidadResultados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                {

                }
                else
                {

                }

            }
        }
        protected void rgGestionRentabilidadSimulador_ItemDataBound(object sender, GridItemEventArgs e){

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                {

                    
                    RadNumericTextBox NId_PrdP = (RadNumericTextBox)editItem.FindControl("NId_PrdP");
                    NId_PrdP.Enabled = false;
                    RadTextBox NPrd_DescripcionP = (RadTextBox)editItem.FindControl("NPrd_DescripcionP");
                    NPrd_DescripcionP.Enabled = false;

                    RadNumericTextBox Ncantidad = (RadNumericTextBox)editItem.FindControl("Ncantidad");
                    Ncantidad.Enabled = false;

                    RadNumericTextBox NPrecioVenta = (RadNumericTextBox)editItem.FindControl("NPrecioVenta");
                    NPrecioVenta.Enabled = false;

                    RadNumericTextBox NPrecioDistribuidor = (RadNumericTextBox)editItem.FindControl("NPrecioDistribuidor");
                    NPrecioDistribuidor.Enabled = false;

                    RadNumericTextBox Nventa = (RadNumericTextBox)editItem.FindControl("Nventa");
                    Nventa.Enabled = false;
                    RadNumericTextBox NCosto = (RadNumericTextBox)editItem.FindControl("NCosto");
                    NCosto.Enabled = false;
                    RadNumericTextBox NUtilidadBruta = (RadNumericTextBox)editItem.FindControl("NUtilidadBruta");
                    NUtilidadBruta.Enabled = false;
                    RadNumericTextBox NPorcUBReal = (RadNumericTextBox)editItem.FindControl("NPorcUBReal");
                    NPorcUBReal.Enabled = false;
                    RadComboBox comboAccion = (RadComboBox)editItem.FindControl("ComboAccion");
                    comboAccion.SelectedValue = "Producto Adicional";
                    comboAccion.Enabled = false;
                    RadNumericTextBox EId_PrdP = (RadNumericTextBox)editItem.FindControl("EId_PrdP");
                    EId_PrdP.Focus();                    
                }
                else
                {
                   RadComboBox comboAccion = (RadComboBox)editItem.FindControl("ComboAccion");
                    foreach (RadComboBoxItem ComboItem in comboAccion.Items)
                    {
                        if (ComboItem.Value == "Producto Adicional" || ComboItem.Value == "Sin Acción" || ComboItem.Value == "Cancelar Consumo")
                        {
                            ComboItem.Enabled = false;
                        }
                        else
                        {
                            ComboItem.Enabled = true;
                        }
                    }
                    comboAccion.SelectedValue = "Cambio de Producto";
                    
                    RadNumericTextBox EId_PrdP = (RadNumericTextBox)editItem.FindControl("EId_PrdP");
                    RadNumericTextBox NId_PrdP = (RadNumericTextBox)editItem.FindControl("NId_PrdP");

                    RadTextBox NPrd_DescripcionP = (RadTextBox)editItem.FindControl("NPrd_DescripcionP");
                    RadTextBox EPrd_DescripcionP = (RadTextBox)editItem.FindControl("EPrd_DescripcionP");

                    RadNumericTextBox EcantidadP = (RadNumericTextBox)editItem.FindControl("EcantidadP");
                    RadNumericTextBox Ncantidad = (RadNumericTextBox)editItem.FindControl("Ncantidad");

                    RadNumericTextBox EPrecioVentaP = (RadNumericTextBox)editItem.FindControl("EPrecioVentaP");
                    RadNumericTextBox NPrecioVenta = (RadNumericTextBox)editItem.FindControl("NPrecioVenta");

                    RadNumericTextBox EPrecioDistribuidorP = (RadNumericTextBox)editItem.FindControl("EPrecioDistribuidorP");
                    RadNumericTextBox NPrecioDistribuidor = (RadNumericTextBox)editItem.FindControl("NPrecioDistribuidor");

                    RadNumericTextBox EventaP = (RadNumericTextBox)editItem.FindControl("EventaP");
                    RadNumericTextBox Nventa = (RadNumericTextBox)editItem.FindControl("Nventa");

                    RadNumericTextBox ECostoP = (RadNumericTextBox)editItem.FindControl("ECostoP");
                    RadNumericTextBox NCosto = (RadNumericTextBox)editItem.FindControl("NCosto");

                    RadNumericTextBox EUtilidadBrutaP = (RadNumericTextBox)editItem.FindControl("EUtilidadBrutaP");
                    RadNumericTextBox NUtilidadBruta = (RadNumericTextBox)editItem.FindControl("NUtilidadBruta");

                    RadNumericTextBox EPorcUBRealP = (RadNumericTextBox)editItem.FindControl("EPorcUBRealP");
                    RadNumericTextBox NPorcUBReal = (RadNumericTextBox)editItem.FindControl("NPorcUBReal");

                    NId_PrdP.Enabled = false;
                    NPrd_DescripcionP.Enabled = false;
                    Ncantidad.Enabled = false;
                    NPrecioVenta.Enabled = false;
                    NPrecioDistribuidor.Enabled = false;
                    Nventa.Enabled = false;
                    NCosto.Enabled = false;
                    NUtilidadBruta.Enabled = false;
                    NPorcUBReal.Enabled = false;
                   /* 
                    EId_PrdP.Text = NId_PrdP.Text;
                    EPrd_DescripcionP.Text = NPrd_DescripcionP.Text;
                    EcantidadP.Text = Ncantidad.Text;
                    EPrecioVentaP.Text = NPrecioVenta.Text;
                    EPrecioDistribuidorP.Text = NPrecioDistribuidor.Text;
                    EventaP.Text = Nventa.Text;
                    ECostoP.Text = NCosto.Text;
                    EUtilidadBrutaP.Text = NUtilidadBruta.Text;
                    EPorcUBRealP.Text = NPorcUBReal.Text;
                    */

                    EId_PrdP.Text = string.Empty;
                    EPrd_DescripcionP.Text = string.Empty;
                    EcantidadP.Text = string.Empty;
                    EPrecioVentaP.Text = string.Empty;
                    EPrecioDistribuidorP.Text = string.Empty;
                    EventaP.Text = string.Empty;
                    ECostoP.Text = string.Empty;
                    EUtilidadBrutaP.Text = string.Empty;
                    EPorcUBRealP.Text = string.Empty;

                    EId_PrdP.Focus();
                }
            }
           

        }

        protected void rgGestionRentabilidadSimulador_ItemCreated(object sender, GridItemEventArgs e)
        {
        }
        protected void rgGestionRentabilidadResultados_ItemCreated(object sender, GridItemEventArgs e)
        {
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        Session["ListaRemisionesFactura"] = new List<Remision>();// null;


                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgGestionRentabilidadSimulador_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    rgGestionRentabilidadSimulador.DataSource = this.GetList("N");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgGestionRentabilidadResultados_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    rgGestionRentabilidadResultados.DataSource = this.GetList("S");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgGestionRentabilidadSimulador_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgGestionRentabilidadSimulador.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgGestionRentabilidadResultados_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgGestionRentabilidadSimulador.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgGestionRentabilidadSimulador_ItemCommand(object source, GridCommandEventArgs e)
        {

            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //Sesion Sesion = new Sesion();

            string NId_PrdP = "";
            string NPrd_DescripcionP = "";
            string Ncantidad = "";
            string NPrecioVenta = "";
            string NPrecioDistribuidor = "";
            string Nventa = "";
            string NCosto = "";
            string NUtilidadBruta = "";
            string NPorcUBReal = "";
            string comboAccion = "";

            string EId_PrdP = "";
            string EcantidadP = "";

            string EPrd_DescripcionP = "";
            string EPrecioVentaP = "";
            string EPrecioDistribuidorP = "";
            string EventaP = "";
            string ECostoP = "";
            string EUtilidadBrutaP = "";
            string EPorcUBRealP = "";
            string CDI = "";
            string Terr = "";
            string Cte = "";

            string Eventa = "";
            string ECosto = "";
            string EUtilidadBruta = "";
            string EPorcUBReal="";
            string MesInicial="";
            string AnioInicial="";
            string MesFinal="";
            string AnioFinal = "";

            string Ecantidad = "";
            string EPrecioVenta = "";
            string EPrecioDistribuidor = "";


            //CN_DevParcialDetalle clsDevParcial = new CN_DevParcialDetalle();

            //rgDevParcial.DataSource = clsDevParcial.ConsultaDetalleFactura(sesion, Factura, 0)
            List<GestionRentabilidadSimulador> listGestionRentabilidadSimulador = new List<GestionRentabilidadSimulador>();
            GestionRentabilidadSimulador gestionRentabilidadSimulador = new GestionRentabilidadSimulador();


            GridItem editItem = e.Item;
           
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        
                      break;
                    case "PerformInsert":
                      


                      NId_PrdP = "0";
                      NPrd_DescripcionP = ((RadTextBox)editItem.FindControl("NPrd_DescripcionP")).Text;
                      Ncantidad = ((RadNumericTextBox)editItem.FindControl("Ncantidad")).Text;
                      NPrecioVenta = ((RadNumericTextBox)editItem.FindControl("NPrecioVenta")).Text;
                      NPrecioDistribuidor = ((RadNumericTextBox)editItem.FindControl("NPrecioDistribuidor")).Text;
                      Nventa = ((RadNumericTextBox)editItem.FindControl("Nventa")).Text;
                      NCosto = ((RadNumericTextBox)editItem.FindControl("NCosto")).Text;
                      NUtilidadBruta = ((RadNumericTextBox)editItem.FindControl("NUtilidadBruta")).Text;
                      NPorcUBReal = ((RadNumericTextBox)editItem.FindControl("NPorcUBReal")).Text;
                      comboAccion = ((RadComboBox)editItem.FindControl("comboAccion")).Text;

                      EId_PrdP = ((RadNumericTextBox)editItem.FindControl("EId_PrdP")).Text;
                      EcantidadP = ((RadNumericTextBox)editItem.FindControl("EcantidadP")).Text;

                      EPrd_DescripcionP = ((RadTextBox)editItem.FindControl("EPrd_DescripcionP")).Text;
                      EPrecioVentaP = ((RadNumericTextBox)editItem.FindControl("EPrecioVentaP")).Text;
                      EPrecioDistribuidorP = ((RadNumericTextBox)editItem.FindControl("EPrecioDistribuidorP")).Text;
                      EventaP = ((RadNumericTextBox)editItem.FindControl("EventaP")).Text;
                      ECostoP = ((RadNumericTextBox)editItem.FindControl("ECostoP")).Text;
                      EUtilidadBrutaP = ((RadNumericTextBox)editItem.FindControl("EUtilidadBrutaP")).Text;
                      EPorcUBRealP = ((RadNumericTextBox)editItem.FindControl("EPorcUBRealP")).Text;
                      CDI = Sesion.Id_Cd_Ver.ToString();
                      Terr = txtTerritorio.Text;
                      Cte = TxtNumeroCliente.Text;

                      Eventa = "0";
                      ECosto = "0";
                      EUtilidadBruta = "0";
                      EPorcUBReal = "0";
                      MesInicial = txtMesInicial.SelectedValue;
                      AnioInicial = TxtAnioInicial.Text;
                      MesFinal = txtMesFinal.SelectedValue;
                      AnioFinal = TxtAnioFinal.Text;

                      Ecantidad = "0";
                      EPrecioVenta = "0";
                      EPrecioDistribuidor = "0";


                      new CN_GestionRentabilidadSimulador().ActualizaGestionRentabilidadSimulador(
                      NId_PrdP
                      ,EId_PrdP
                      ,EcantidadP
                      ,EPrd_DescripcionP
                      ,EPrecioVentaP
                      ,EPrecioDistribuidorP
                      ,EventaP
                      ,ECostoP
                      ,EUtilidadBrutaP
                      ,EPorcUBRealP
                      ,CDI
                      ,Terr
                      ,Cte
                      ,Sesion.Emp_Cnx
                      ,comboAccion
                      ,Eventa
                      ,ECosto
                      ,EUtilidadBruta
                      ,EPorcUBReal
                      ,MesInicial
                      ,AnioInicial
                      ,MesFinal
                      ,AnioFinal
                      ,Ecantidad
                      ,EPrecioVenta
                      ,EPrecioDistribuidor


                          );




                      new CN_GestionRentabilidadSimulador().ConsultaGestionRentabilidadSimulador_Buscar(gestionRentabilidadSimulador
                                      , Sesion.Emp_Cnx
                                      , ref listGestionRentabilidadSimulador
                                      , Sesion.Id_Emp
                                      , Sesion.Id_Cd_Ver
                                      , this.TxtNumeroCliente.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Cte"]) : this.TxtNumeroCliente.Text
                                      , this.txtTerritorio.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Ter"]) : this.txtTerritorio.Text
                                      , this.TxtNombreCliente.Text
                                      , Convert.ToInt32(Request.QueryString["txtMesInicial"])
                                      , Convert.ToInt32(Request.QueryString["TxtAnioInicial"])
                                      , Convert.ToInt32(Request.QueryString["txtMesFinal"])
                                      , Convert.ToInt32(Request.QueryString["TxtAnioFinal"])
                                      , Sesion.Id_U
                                      , "S"
                                      );

                        


                      rgGestionRentabilidadResultados.DataSource = listGestionRentabilidadSimulador;
                      rgGestionRentabilidadResultados.Rebind();
                      ActualizaTotales();

                        break;
                    case "Update":

                      NId_PrdP = ((RadNumericTextBox)editItem.FindControl("NId_PrdP")).Text;
                      NPrd_DescripcionP = ((RadTextBox)editItem.FindControl("NPrd_DescripcionP")).Text;
                      Ncantidad = ((RadNumericTextBox)editItem.FindControl("Ncantidad")).Text;
                      NPrecioVenta = ((RadNumericTextBox)editItem.FindControl("NPrecioVenta")).Text;
                      NPrecioDistribuidor = ((RadNumericTextBox)editItem.FindControl("NPrecioDistribuidor")).Text;
                      Nventa = ((RadNumericTextBox)editItem.FindControl("Nventa")).Text;
                      NCosto = ((RadNumericTextBox)editItem.FindControl("NCosto")).Text;
                      NUtilidadBruta = ((RadNumericTextBox)editItem.FindControl("NUtilidadBruta")).Text;
                      NPorcUBReal = ((RadNumericTextBox)editItem.FindControl("NPorcUBReal")).Text;
                      comboAccion = ((RadComboBox)editItem.FindControl("comboAccion")).Text;

                      EId_PrdP = ((RadNumericTextBox)editItem.FindControl("EId_PrdP")).Text;
                      EcantidadP = ((RadNumericTextBox)editItem.FindControl("EcantidadP")).Text;

                      EPrd_DescripcionP = ((RadTextBox)editItem.FindControl("EPrd_DescripcionP")).Text;
                      EPrecioVentaP = ((RadNumericTextBox)editItem.FindControl("EPrecioVentaP")).Text;
                      EPrecioDistribuidorP = ((RadNumericTextBox)editItem.FindControl("EPrecioDistribuidorP")).Text;
                      EventaP = ((RadNumericTextBox)editItem.FindControl("EventaP")).Text;
                      ECostoP = ((RadNumericTextBox)editItem.FindControl("ECostoP")).Text;
                      EUtilidadBrutaP = ((RadNumericTextBox)editItem.FindControl("EUtilidadBrutaP")).Text;
                      EPorcUBRealP = ((RadNumericTextBox)editItem.FindControl("EPorcUBRealP")).Text;
                      CDI = Sesion.Id_Cd_Ver.ToString();
                      Terr = txtTerritorio.Text;
                      Cte = TxtNumeroCliente.Text;


                      Eventa = ((RadNumericTextBox)editItem.FindControl("Nventa")).Text;
                      ECosto = ((RadNumericTextBox)editItem.FindControl("NCosto")).Text;
                      EUtilidadBruta = ((RadNumericTextBox)editItem.FindControl("NUtilidadBruta")).Text;
                      EPorcUBReal = ((RadNumericTextBox)editItem.FindControl("NPorcUBReal")).Text;
                      MesInicial = txtMesInicial.SelectedValue;
                      AnioInicial = TxtAnioInicial.Text;
                      MesFinal = txtMesFinal.SelectedValue;
                      AnioFinal = TxtAnioFinal.Text;

                      Ecantidad = ((RadNumericTextBox)editItem.FindControl("Ncantidad")).Text;
                      EPrecioVenta = ((RadNumericTextBox)editItem.FindControl("NPrecioVenta")).Text;
                      EPrecioDistribuidor = ((RadNumericTextBox)editItem.FindControl("NPrecioDistribuidor")).Text;



                      new CN_GestionRentabilidadSimulador().ActualizaGestionRentabilidadSimulador(
                      NId_PrdP
                      ,EId_PrdP
                      ,EcantidadP
                      ,EPrd_DescripcionP
                      ,EPrecioVentaP
                      ,EPrecioDistribuidorP
                      ,EventaP
                      ,ECostoP
                      ,EUtilidadBrutaP
                      ,EPorcUBRealP
                      ,CDI
                      ,Terr
                      ,Cte
                      , Sesion.Emp_Cnx
                      , comboAccion
                      , Eventa
                      , ECosto
                      , EUtilidadBruta
                      , EPorcUBReal
                      , MesInicial
                      , AnioInicial
                      , MesFinal
                      , AnioFinal
                      ,Ecantidad
                      ,EPrecioVenta
                      ,EPrecioDistribuidor
                          );


                      new CN_GestionRentabilidadSimulador().ConsultaGestionRentabilidadSimulador_Buscar(gestionRentabilidadSimulador
                                      , Sesion.Emp_Cnx
                                      , ref listGestionRentabilidadSimulador
                                      , Sesion.Id_Emp
                                      , Sesion.Id_Cd_Ver
                                      , this.TxtNumeroCliente.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Cte"]) : this.TxtNumeroCliente.Text
                                      , this.txtTerritorio.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Ter"]) : this.txtTerritorio.Text
                                      , this.TxtNombreCliente.Text
                                      , Convert.ToInt32(Request.QueryString["txtMesInicial"])
                                      , Convert.ToInt32(Request.QueryString["TxtAnioInicial"])
                                      , Convert.ToInt32(Request.QueryString["txtMesFinal"])
                                      , Convert.ToInt32(Request.QueryString["TxtAnioFinal"])
                                      , Sesion.Id_U
                                      , "S"
                                      );


                      rgGestionRentabilidadResultados.DataSource = listGestionRentabilidadSimulador;
                      rgGestionRentabilidadResultados.Rebind();
                      ActualizaTotales();



                        break;

                    case "Delete":

                      //EId_PrdP = ((RadNumericTextBox)editItem.FindControl("EId_PrdP")).Text;
                        //CDI = Sesion.Id_Cd_Ver.ToString();
                        //Terr = txtTerritorio.Text;
                        //Cte = TxtNumeroCliente.Text;

                      //new CN_GestionRentabilidadSimulador().EliminaGestionRentabilidadSimulador(
                      //CDI,
                      //Terr,
                      //Cte,
                     // EId_PrdP,
                      //Sesion.Emp_Cnx
                        //    );

                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        
        protected void rgGestionRentabilidadResultados_ItemCommand(object source, GridCommandEventArgs e)
        {

            string NId_PrdP = "";
            string AnioAccion = "";
            string MesAccion = "";
            string CDI = "";
            string Terr = "";
            string Cte = "";
            string Accion = "";

            GridItem editItem = e.Item;
           
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        
                      break;
                    case "PerformInsert":

                      break;
                    case "Update":

                      Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                      NId_PrdP = ((RadNumericTextBox)editItem.FindControl("NEId_Prdx")).Text;
                      MesAccion = ((RadComboBox)editItem.FindControl("MesAccionP")).SelectedValue;
                      AnioAccion = ((RadNumericTextBox)editItem.FindControl("AnioAccionP")).Text;
                      Accion = ((RadTextBox)editItem.FindControl("PAccion")).Text;

                      if (AnioAccion == "")
                      {
                          Alerta("Favor de capturar el Año de Compromiso");
                          break;

                      }


                      CDI = sesion.Id_Cd_Ver.ToString();
                      Terr = txtTerritorio.Text;
                      Cte = TxtNumeroCliente.Text;

                      new CN_GestionRentabilidadSimulador().ActualizaGestionRentabilidadSimuladorAcciones(
                      NId_PrdP
                      ,CDI
                      ,Terr
                      ,Cte
                      , sesion.Emp_Cnx
                      , MesAccion
                      , AnioAccion
                      , Accion
                          );
                        break;

                    case "Delete":
                      break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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
        #region Funciones
        private void ActualizaTotales()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            GestionRentabilidadSimulador gestionRentabilidadSimulador = new GestionRentabilidadSimulador();

            Decimal VentaActual = 0;
            Decimal CostoActual = 0;
            Decimal UtilidadBrutaActual = 0;
            Decimal VentaPlanteada = 0;
            Decimal CostoPlanteada = 0;
            Decimal UtilidadBrutaPlanteada = 0;
            Decimal UtilidadBrutaPorcentajeActual = 0;
            Decimal ComisionRIKActual = 0;
            Decimal UtilidadBrutaPorcentajePlanteada = 0;
            Decimal ComisionRIKPlanteada = 0;
            Decimal AhorroClientesPesos = 0;
            Decimal AhorroClientesPorcentaje = 0;
            Decimal UtilidadBrutaMejoraPesos = 0;
            Decimal UtilidadBrutaMejoraPorcentaje = 0;

            new CN_GestionRentabilidadSimulador().ConsultaGestionRentabilidadSimulador_Totales(
                                 sesion.Emp_Cnx
                                , sesion.Id_Emp
                                , sesion.Id_Cd_Ver
                                , this.TxtNumeroCliente.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Cte"]) : this.TxtNumeroCliente.Text
                                , this.txtTerritorio.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Ter"]) : this.txtTerritorio.Text
                                , this.TxtNombreCliente.Text
                                , Convert.ToInt32(Request.QueryString["txtMesInicial"])
                                , Convert.ToInt32(Request.QueryString["TxtAnioInicial"])
                                , Convert.ToInt32(Request.QueryString["txtMesFinal"])
                                , Convert.ToInt32(Request.QueryString["TxtAnioFinal"])
                                , sesion.Id_U
                                , "N"
                                , ref VentaActual
                                , ref CostoActual
                                , ref UtilidadBrutaActual
                                , ref VentaPlanteada
                                , ref CostoPlanteada
                                , ref UtilidadBrutaPlanteada
                                , ref UtilidadBrutaPorcentajeActual
                                , ref ComisionRIKActual
                                , ref UtilidadBrutaPorcentajePlanteada
                                , ref ComisionRIKPlanteada
                                , ref AhorroClientesPesos
                                , ref AhorroClientesPorcentaje
                                , ref UtilidadBrutaMejoraPesos
                                , ref UtilidadBrutaMejoraPorcentaje
                                );

            SA_Ventas_Pesos.Text = Convert.ToString(VentaActual);
            SA_Costo_Pesos.Text = Convert.ToString(CostoActual);
            SA_UB_Pesos.Text = Convert.ToString(UtilidadBrutaActual);
            SA_UB_Porcentaje.Text = Convert.ToString(UtilidadBrutaPorcentajeActual);
            SA_ComisionRIK_Pesos.Text = Convert.ToString(ComisionRIKActual);

            SP_Ventas_Pesos.Text = Convert.ToString(VentaPlanteada);
            SP_Costo_Pesos.Text = Convert.ToString(CostoPlanteada);
            SP_UB_Pesos.Text = Convert.ToString(UtilidadBrutaPlanteada);
            SP_UB_Porcentaje.Text = Convert.ToString(UtilidadBrutaPorcentajePlanteada);
            SP_ComisionRIK_Pesos.Text = Convert.ToString(ComisionRIKPlanteada);

            M_AhorroMensualClientes_Pesos.Text = Convert.ToString(AhorroClientesPesos);
            M_AhorroMensualClientes_Porcentaje.Text = Convert.ToString(AhorroClientesPorcentaje);
            M_MejoraUB_Pesos.Text = Convert.ToString(UtilidadBrutaMejoraPesos);
            M_MejoraUB_Porcentaje.Text = Convert.ToString(UtilidadBrutaMejoraPorcentaje);


        }

        private void CargarCentros()
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

        private void Inicializar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                this.CargarCentros();                                           
                rgGestionRentabilidadSimulador.Rebind();

                rgGestionRentabilidadResultados.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<GestionRentabilidadSimulador> GetList(string ConAccion)
        {            
            
           try
            {


                   
             //foreach (GridColumn col in rgGestionRentabilidad.MasterTableView.DetailTables[0].Columns) 
             //   { 
             //       if (col.UniqueName == "Cte_NomComercial") 
             //       { 
             //           col.Visible = false; 
             //       } 
             //   }


                
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<GestionRentabilidadSimulador> listGestionRentabilidadSimulador = new List<GestionRentabilidadSimulador>();
                GestionRentabilidadSimulador gestionRentabilidadSimulador = new GestionRentabilidadSimulador();
               
                new CN_GestionRentabilidadSimulador().ConsultaGestionRentabilidadSimulador_Buscar(gestionRentabilidadSimulador
                                    , sesion.Emp_Cnx
                                    , ref listGestionRentabilidadSimulador
                                    , sesion.Id_Emp
                                    , sesion.Id_Cd_Ver
                                    , this.TxtNumeroCliente.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Cte"]) : this.TxtNumeroCliente.Text
                                    , this.txtTerritorio.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Ter"]) : this.txtTerritorio.Text
                                    , this.TxtNombreCliente.Text
                                    , Convert.ToInt32(Request.QueryString["txtMesInicial"])
                                    , Convert.ToInt32(Request.QueryString["TxtAnioInicial"])
                                    , Convert.ToInt32(Request.QueryString["txtMesFinal"])
                                    , Convert.ToInt32(Request.QueryString["TxtAnioFinal"])
                                    , sesion.Id_U
                                    , ConAccion
                                    );


                ActualizaTotales();

               

                               return listGestionRentabilidadSimulador;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private List<GestionRentabilidadSimulador> GetListResultados(string ConAccion)
        {

            try
            {



                //foreach (GridColumn col in rgGestionRentabilidad.MasterTableView.DetailTables[0].Columns) 
                //   { 
                //       if (col.UniqueName == "Cte_NomComercial") 
                //       { 
                //           col.Visible = false; 
                //       } 
                //   }



                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<GestionRentabilidadSimulador> listGestionRentabilidadSimulador = new List<GestionRentabilidadSimulador>();
                GestionRentabilidadSimulador gestionRentabilidadSimulador = new GestionRentabilidadSimulador();

                new CN_GestionRentabilidadSimulador().ConsultaGestionRentabilidadSimulador_Buscar(gestionRentabilidadSimulador
                                    , sesion.Emp_Cnx
                                    , ref listGestionRentabilidadSimulador
                                    , sesion.Id_Emp
                                    , sesion.Id_Cd_Ver
                                    , this.TxtNumeroCliente.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Cte"]) : this.TxtNumeroCliente.Text
                                    , this.txtTerritorio.Text == string.Empty ? Convert.ToString(Request.QueryString["Id_Ter"]) : this.txtTerritorio.Text
                                    , this.TxtNombreCliente.Text
                                    , Convert.ToInt32(Request.QueryString["txtMesInicial"])
                                    , Convert.ToInt32(Request.QueryString["TxtAnioInicial"])
                                    , Convert.ToInt32(Request.QueryString["txtMesFinal"])
                                    , Convert.ToInt32(Request.QueryString["TxtAnioFinal"])
                                    , sesion.Id_U
                                    , ConAccion
                                    );

                return listGestionRentabilidadSimulador;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int id_Cd_Prod = sesion.Id_Cd_Ver;

            Producto producto = new Producto();
            CN_CatProducto clsProducto = new CN_CatProducto();
            RadNumericTextBox combo = (RadNumericTextBox)sender;

            RadTextBox rgEPrd_DescripcionP = combo.Parent.FindControl("EPrd_DescripcionP") as RadTextBox;
            RadNumericTextBox rgEPrecioDistribuidorP = combo.Parent.FindControl("EPrecioDistribuidorP") as RadNumericTextBox;
            RadNumericTextBox rgEPrecioVentaP = combo.Parent.FindControl("EPrecioVentaP") as RadNumericTextBox;

            try
            {
                clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, Convert.ToInt32(combo.Value),0);
            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, combo.ClientID);
                return;
            }

            float precioPublico = 0;
            float precioAAA = 0;
            new CN_ProductoPrecios().ConsultaListaProductoPrecioPUBLICO(ref precioPublico, sesion, Convert.ToInt32(combo.Value));

            new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precioAAA, sesion, Convert.ToInt32(combo.Value));


            rgEPrd_DescripcionP.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
            rgEPrecioDistribuidorP.Value = Convert.ToDouble( precioAAA == null ? 0 : precioAAA );
            rgEPrecioVentaP.Value = Convert.ToDouble(precioPublico == null ? 0 : precioPublico);

        }
        protected void txtProductoCantidad_TextChanged(object sender, EventArgs e)
        {
            RadNumericTextBox combo = (RadNumericTextBox)sender;
            RadNumericTextBox rgEPrecioVentaP = combo.Parent.FindControl("EPrecioVentaP") as RadNumericTextBox;
            RadNumericTextBox rgEventaP = combo.Parent.FindControl("EventaP") as RadNumericTextBox;
            RadNumericTextBox rgEPrecioDistribuidorP = combo.Parent.FindControl("EPrecioDistribuidorP") as RadNumericTextBox;
            RadNumericTextBox rgECostoP = combo.Parent.FindControl("ECostoP") as RadNumericTextBox;
            RadNumericTextBox rgEUtilidadBrutaP = combo.Parent.FindControl("EUtilidadBrutaP") as RadNumericTextBox;
            RadNumericTextBox rgEPorcUBRealP = combo.Parent.FindControl("EPorcUBRealP") as RadNumericTextBox;
            
            rgEventaP.Value = Convert.ToDouble(combo.Value * rgEPrecioVentaP.Value);
            rgECostoP.Value = Convert.ToDouble(combo.Value * rgEPrecioDistribuidorP.Value);

            if (rgEventaP.Value > 0)
            {
                rgEUtilidadBrutaP.Value = Convert.ToDouble(rgEventaP.Value - rgECostoP.Value);
                rgEPorcUBRealP.Value = Convert.ToDouble((rgEUtilidadBrutaP.Value / rgEventaP.Value) * 100);
            }
            else
            {
                rgEUtilidadBrutaP.Value = Convert.ToDouble(0);
                rgEPorcUBRealP.Value = Convert.ToDouble(0);
            }
        }
        protected void txtProductoPrecio_TextChanged(object sender, EventArgs e)
        {
            RadNumericTextBox combo = (RadNumericTextBox)sender;
            RadNumericTextBox rgEcantidadP = combo.Parent.FindControl("EcantidadP") as RadNumericTextBox;
            RadNumericTextBox rgEPrecioVentaP = combo.Parent.FindControl("EPrecioVentaP") as RadNumericTextBox;
            RadNumericTextBox rgEventaP = combo.Parent.FindControl("EventaP") as RadNumericTextBox;
            RadNumericTextBox rgEPrecioDistribuidorP = combo.Parent.FindControl("EPrecioDistribuidorP") as RadNumericTextBox;
            RadNumericTextBox rgECostoP = combo.Parent.FindControl("ECostoP") as RadNumericTextBox;
            RadNumericTextBox rgEUtilidadBrutaP = combo.Parent.FindControl("EUtilidadBrutaP") as RadNumericTextBox;
            RadNumericTextBox rgEPorcUBRealP = combo.Parent.FindControl("EPorcUBRealP") as RadNumericTextBox;

            rgEventaP.Value = Convert.ToDouble(rgEcantidadP.Value * rgEPrecioVentaP.Value);
            rgECostoP.Value = Convert.ToDouble(rgEcantidadP.Value * rgEPrecioDistribuidorP.Value);

            if (rgEventaP.Value > 0)
            {
                if (rgEPrecioVentaP.Value <= rgEPrecioDistribuidorP.Value)
                {
                    Alerta("El Precio de Venta tiene que ser Mayor al Costo");
                    rgEPrecioVentaP.Focus();
                }
                else
                {
                    rgEUtilidadBrutaP.Value = Convert.ToDouble(rgEventaP.Value - rgECostoP.Value);
                    rgEPorcUBRealP.Value = Convert.ToDouble((rgEUtilidadBrutaP.Value / rgEventaP.Value) * 100);
                }
            }
            else
            {
                rgEUtilidadBrutaP.Value = Convert.ToDouble(0);
                rgEPorcUBRealP.Value = Convert.ToDouble(0);
            }
        }

        protected void cmbAccion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {


            RadComboBox combo = sender as RadComboBox;
            RadNumericTextBox rgEId_PrdP = combo.Parent.FindControl("EId_PrdP") as RadNumericTextBox;
            RadNumericTextBox rgNId_PrdP = combo.Parent.FindControl("NId_PrdP") as RadNumericTextBox;
            RadNumericTextBox rgEcantidadP = combo.Parent.FindControl("EcantidadP") as RadNumericTextBox;
            RadNumericTextBox rgNcantidadP = combo.Parent.FindControl("Ncantidad") as RadNumericTextBox;
            RadTextBox rgEPrd_DescripcionP = combo.Parent.FindControl("EPrd_DescripcionP") as RadTextBox;
            RadTextBox rgNPrd_DescripcionP = combo.Parent.FindControl("NPrd_DescripcionP") as RadTextBox;
            RadNumericTextBox rgEPrecioVentaP = combo.Parent.FindControl("EPrecioVentaP") as RadNumericTextBox;
            RadNumericTextBox rgNPrecioVentaP = combo.Parent.FindControl("NPrecioVenta") as RadNumericTextBox;
            RadNumericTextBox rgEPrecioDistribuidorP = combo.Parent.FindControl("EPrecioDistribuidorP") as RadNumericTextBox;
            RadNumericTextBox rgNPrecioDistribuidorP = combo.Parent.FindControl("NPrecioDistribuidor") as RadNumericTextBox;
            RadNumericTextBox rgEventaP = combo.Parent.FindControl("EventaP") as RadNumericTextBox;
            RadNumericTextBox rgNventaP = combo.Parent.FindControl("Nventa") as RadNumericTextBox;
            RadNumericTextBox rgECostoP = combo.Parent.FindControl("ECostoP") as RadNumericTextBox;
            RadNumericTextBox rgNCostoP = combo.Parent.FindControl("NCosto") as RadNumericTextBox;
            RadNumericTextBox rgEUtilidadBrutaP = combo.Parent.FindControl("EUtilidadBrutaP") as RadNumericTextBox;
            RadNumericTextBox rgNUtilidadBrutaP = combo.Parent.FindControl("NUtilidadBruta") as RadNumericTextBox;
            RadNumericTextBox rgEPorcUBRealP = combo.Parent.FindControl("EPorcUBRealP") as RadNumericTextBox;
            RadNumericTextBox rgNPorcUBRealP = combo.Parent.FindControl("NPorcUBReal") as RadNumericTextBox;


            if (e.Value == "Incremento en Precio" || e.Value=="Disminuir Precio" ||  e.Value=="Disminuir Consumo")
            {
                rgEId_PrdP.Value = rgNId_PrdP.Value;
                rgEcantidadP.Value = rgNcantidadP.Value;
                rgEPrd_DescripcionP.Text = rgNPrd_DescripcionP.Text;
                rgEPrecioVentaP.Value = rgNPrecioVentaP.Value;
                rgEPrecioDistribuidorP.Value = rgNPrecioDistribuidorP.Value;
                rgEventaP.Value = rgNventaP.Value;
                rgECostoP.Value = rgNCostoP.Value;
                rgEUtilidadBrutaP.Value = rgNUtilidadBrutaP.Value;
                rgEPorcUBRealP.Value = rgNPorcUBRealP.Value;
                rgEId_PrdP.Enabled = false;
                rgEPrd_DescripcionP.Enabled = false;
                rgEPrecioDistribuidorP.Enabled = false;
                rgEventaP.Enabled = false;
                rgECostoP.Enabled = false;
                rgEUtilidadBrutaP.Enabled = false;
                rgEPorcUBRealP.Enabled = false;
/*                rgEPrecioVentaP.Text = string.Empty;
                rgEventaP.Text = string.Empty;
                rgEUtilidadBrutaP.Text = string.Empty;
                rgEPorcUBRealP.Text = string.Empty;
                rgEId_PrdP.Enabled = false;
                rgEcantidadP.Enabled = false; */
            }
            else
            {
                rgEId_PrdP.Enabled = true;
                rgEcantidadP.Enabled = true;

                rgEcantidadP.Text = string.Empty;
                rgEId_PrdP.Text = string.Empty;
                rgEPrd_DescripcionP.Text = string.Empty;
                rgEPrecioVentaP.Text = string.Empty;
                rgEventaP.Text = string.Empty;
                rgEUtilidadBrutaP.Text = string.Empty;
                rgEPorcUBRealP.Text = string.Empty;

                rgEPrecioDistribuidorP.Text = string.Empty;
                rgECostoP.Text = string.Empty;


            }
            
        }
        #endregion

        protected void z_btnAceptar_Click(object sender, EventArgs e)
        {

            Alerta("prueba");
            
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                rgGestionRentabilidadSimulador.Rebind();
                rgGestionRentabilidadResultados.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

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

    }

}
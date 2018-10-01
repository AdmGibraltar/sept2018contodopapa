    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Telerik.Web.UI;
    using Telerik.Web.UI.Common;

    using CapaEntidad;
    using CapaNegocios;
    using System.Collections;
    using System.Net;
    using System.IO;
    using System.Xml;
    using System.Text;
    using System.Configuration;
    using System.Security;
    using System.Net.Mail;
    using System.Net.Mime;

    namespace SIANWEB
    {
        public partial class CapOrdenCompra_Lista : System.Web.UI.Page
        {
                                #region Variables

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        #endregion

                                #region Propiedades

        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }

        #endregion

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        Session["_IdProducto"] = 0;
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                            return;
                        }
                        this.CargarCentros();
                        //Cargar grid de ordenes de compra
                        if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                        {
                            txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                        }
                        if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                        {
                            txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                        }
                        rgOrdCompra.DataSource = this.GetList();
                        rgOrdCompra.DataBind();
                        txtFolio1.Focus();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                    //menu de filtros de Grid
                    GridFilterMenu menu = rgOrdCompra.FilterMenu;
                    foreach (RadMenuItem item in menu.Items)
                    {
                        switch (item.Text)
                        {
                            case "NoFilter": item.Text = "Quitar filtro"; break;
                            case "Contains": item.Text = "Que contenga"; break;
                            case "DoesNotContain": item.Text = "Que no contenga"; break;
                            case "StartsWith": item.Text = "Inicia con"; break;
                            case "EndsWith": item.Text = "Termina con"; break;
                            case "EqualTo": item.Text = "Igual a"; break;
                            case "NotEqualTo": item.Text = "No igual a"; break;
                            case "GreaterThan": item.Text = "Mayor que"; break;
                            case "LessThan": item.Text = "Menor que"; break;
                            case "GreaterThanOrEqualTo": item.Text = "Mayor o igual que"; break;
                            case "LessThanOrEqualTo": item.Text = "Menor o igual que"; break;
                            case "Between": item.Text = "Esta dentro de"; break;
                            case "NotBetween": item.Text = "No esta dentro de"; break;
                            case "IsEmpty": item.Text = "Es vac&iacute;o"; break;
                            case "NotIsEmpty": item.Text = "No es vac&iacute,o"; break;
                            case "IsNull": item.Text = "Es nulo"; break;
                            case "NotIsNull": item.Text = "No es nulo"; break;
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
            try
            {
                string cmd = e.Argument.ToString();
                if (cmd == "RebindGrid")
                {
                    rgOrdCompra.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgOrdCompra_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {//Llenar Grid
                    rgOrdCompra.DataSource = this.GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgOrdCompra_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgOrdCompra.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgOrdCompra_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                ImageButton imgButton = (ImageButton)item.FindControl("ImageButton1");
                imgButton.OnClientClick = "return AbrirVentana_OrdenCompra_Edicion('" + item.ItemIndex + "')";
            }
        }
        protected void rgOrdCompra_ItemCommand(object source, GridCommandEventArgs e)
        {
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                int porautorizar = 0;
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgOrdCompra.Items[item]["Id_Emp"].Text);
                    int Id_Cd = Convert.ToInt32(rgOrdCompra.Items[item]["Id_Cd"].Text);
                    int Id_Ord = Convert.ToInt32(rgOrdCompra.Items[item]["Id_Ord"].Text);
                    string Ord_EstatusStr = rgOrdCompra.Items[item]["Ord_EstatusStr"].Text;
                    string[] datePart = rgOrdCompra.Items[item]["Ord_Fecha"].Text.Split(new char[] { '/' });
                    DateTime fechaOrden = new DateTime(Convert.ToInt32(datePart[2]), Convert.ToInt32(datePart[1]), Convert.ToInt32(datePart[0]));
                    string Id_Pvd = ((Label)(rgOrdCompra.Items[item]["Id_Pvd"].FindControl("lblId_Pvd"))).Text;
                    string Ord_EstatusEmision = rgOrdCompra.Items[item]["Ord_EstatusEmisionStr"].Text;


                    if (Ord_EstatusEmision == "Orden de compra pendiente de autorización")
                        porautorizar = 1;

                    switch (e.CommandName.ToString())
                    {
                        case "Eliminar":

                            mensajeError = "CapOrdCompra_delete_error";
                            if (_PermisoEliminar)
                            {
                                if (rgOrdCompra.Items[item]["Ord_EstatusStr"].Text.ToUpper() == "CAPTURADO" || rgOrdCompra.Items[item]["Ord_EstatusStr"].Text.ToUpper() == "IMPRESO")
                                {
                                    this.CancelarOrdenCompra(Id_Emp, Id_Cd, Id_Ord, Ord_EstatusStr);
                                    rgOrdCompra.Rebind();
                                }
                                else
                                {
                                    Alerta("La orden de compra se encuentra en estatus no válido para realizar la baja");
                                }
                            }
                            else
                                this.DisplayMensajeAlerta("PermisoEliminarDenegado");

                            break;

                        case "Imprimir":
                            mensajeError = "CapOrdCompra_print_error";
                            if (_PermisoImprimir)
                            {
                                if (rgOrdCompra.Items[item]["Ord_EstatusStr"].Text.ToUpper() != "BAJA" && porautorizar != 1)
                                {
                                    HD_GridRebind.Value = "0";
                                    this.OrdenCompra_Impresion(Id_Emp, Id_Cd, Id_Ord, fechaOrden, Ord_EstatusStr);
                                    rgOrdCompra.Rebind();
                                }
                                else
                                {
                                    Alerta("La orden de compra se encuentra en estatus no válido para realizar la impresion");
                                }
                            }
                            else
                                this.DisplayMensajeAlerta("PermisoImprimirDenegado");
                            break;

                        case "Enviar":
                            mensajeError = "CapOrdCompra_mail_error";
                            if (Id_Pvd != "100")
                                this.DisplayMensajeAlerta("EnvioInternet_ProveedorNoAlmacen");
                            else
                            {
                                string habilitaenviodirecto = ConfigurationManager.AppSettings["OrdenCompraEnvioDirecto"].ToString();
                                if (habilitaenviodirecto == "0")
                                {

                                    List<string> arregloURL = this.OrdenCompra_CrearURL_EnvioInternet(Id_Emp, Id_Cd, Id_Ord);
                                    this.OrdenCompra_EnvioPorInternet(Id_Emp, Id_Cd, Id_Ord, arregloURL, Ord_EstatusStr);
                                }
                                else
                                {
                                    this.OrdenCompra_CrearEnviaXML(Id_Emp, Id_Cd, Id_Ord, fechaOrden, Ord_EstatusStr);

                                }
                                rgOrdCompra.Rebind();
                            }
                            break;
                        case "Autorizar":
                            Autorizar(Id_Ord);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }

                Session["Sesion" + Session.SessionID] = sesion;

                rgOrdCompra.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                rgOrdCompra.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                #region Funciones
        private void Autorizar(int Id_Ord)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapOrdenCompra cn_capordencompra = new CN_CapOrdenCompra();
                String Resultado = "";
                cn_capordencompra.SP_Autoriza_Saldo_OC(sesion.Id_Emp, sesion.Id_Cd, Id_Ord, sesion.Id_U, sesion.Emp_Cnx, ref Resultado);
                if (Resultado == "")
                {
                    Alerta("Se autorizó correctamente la Orden de Compra #" + Id_Ord);
                }
                else
                {
                    Alerta(Resultado);
                }
                rgOrdCompra.Rebind();

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
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
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

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[6].Visible = false;
                    this.RadToolBar1.Items[5].Visible = false;
                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OrdenCompra_EnvioPorInternet(int Id_Emp, int Id_Cd, int Id_Ord, List<string> arregloURL, string Ord_EstatusStr)
        {
            try
            {
                if (Ord_EstatusStr.Contains("Baja"))
                    throw new Exception("OrdCompra_estatus_incorrecto");
                else
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    string jsArregloURL = string.Empty;
                    int verificador = 0;
                    string urlFinal = "";
                    foreach (string url in arregloURL)
                    {
                        jsArregloURL = string.Concat(jsArregloURL, "'", url, "',");

                        if (url.Contains("SubeFinalSuc.asp"))
                        {
                            urlFinal = url;
                        }
                        else
                        {
                            this.HTTPrequest_ResponseText(url);
                        }
                    }
                    jsArregloURL = jsArregloURL.Substring(0, jsArregloURL.Length - 1);
                    jsArregloURL = string.Concat("arregloURL = new Array(", jsArregloURL, ");");

                    //actualiza estatus de orden de compra a Impreso (I)
                    OrdenCompra ordCompra = new OrdenCompra();
                    ordCompra.Id_Emp = Id_Emp;
                    ordCompra.Id_Cd = Id_Cd;
                    ordCompra.Id_Ord = Id_Ord;
                    ordCompra.Ord_Estatus = "I";
                    new CN_CapOrdenCompra().ModificarOrdenCompra_Estatus(ordCompra, sesion.Emp_Cnx, ref verificador);
                    //this.DisplayMensajeAlerta("CapOrdCompra_envioInternet_ok");
                    RAM1.ResponseScripts.Add("AbrirResultado('" + urlFinal + "')");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HTTPrequest_ResponseText(string strURI)
        {
            try
            {
                Uri objURI = new Uri(strURI);
                WebRequest objWebRequest = WebRequest.Create(objURI);
                WebResponse objWebResponse = objWebRequest.GetResponse();
                Stream objStream = objWebResponse.GetResponseStream();
                StreamReader objStreamReader = new StreamReader(objStream);
                string responseText = objStreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getEstatusEmision(int? estatus)
        {
            try
            {

                estatus = (estatus == null) ? -1 : estatus;
                string respuesta = "";

                
                switch (estatus)
                {
                    case -1:
                        respuesta = "";
                        break;
                    case 0:
                        respuesta = "La orden no se ha enviado";
                        break;
                    case 1:
                        respuesta = "Orden enviada";
                        break;
                    case 2:
                        respuesta = "Orden ya ha sido enviada";
                        break;
                    case 3:
                        respuesta = "Error al recibir Información, Vuelva a intentar enviar ";
                        break;
                    case 4:
                        respuesta = "Orden de compra pendiente de autorización";
                        break;
                    default:
                        respuesta = "No hay Conexión con la planta";
                        break;
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<string> OrdenCompra_CrearURL_EnvioInternet(int Id_Emp, int Id_Cd, int Id_Ord)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Consulta encabezado de la orden de compra
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = Id_Emp;
                ordCompra.Id_Cd = Id_Cd;
                ordCompra.Id_Ord = Id_Ord;
                new CN_CapOrdenCompra().ConsultaOrdenCompra(ref ordCompra, sesion.Emp_Cnx);
                //Consulta detalle de la orden de compra
                List<OrdenCompraDet> listaOrdCompraDet = new List<OrdenCompraDet>();
                OrdenCompraDet ordenCompraDet = new OrdenCompraDet();

                ordenCompraDet.Id_Emp = Id_Emp;
                ordenCompraDet.Id_Cd = Id_Cd;
                ordenCompraDet.Id_Ord = Id_Ord;
                new CN_CapOrdenCompraDet().ConsultaOrdenCompraDetalle_Lista(ordenCompraDet, sesion.Emp_Cnx, ref listaOrdCompraDet);

                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, ordCompra.Id_Cd, ordCompra.Id_Emp, sesion.Emp_Cnx);

                //construir URL
                List<string> arregloURL = new List<string>();
                System.Text.StringBuilder URL = new System.Text.StringBuilder();
                foreach (OrdenCompraDet ordComDet in listaOrdCompraDet)
                {
                    URL.Clear();
                    URL.Append(string.Concat("http://148.244.244.141/oc/SubeDetalleOC.asp?oczonnumsian=", Cd.Id_Cd.ToString()));
                    URL.Append(string.Concat("&ocnumsian=", ordCompra.Id_Ord.ToString()));
                    URL.Append(string.Concat("&ocpronumsian=", ordComDet.Producto.Id_Prd));
                    URL.Append(string.Concat("&ocprodescsian=", ordComDet.Producto.Prd_Descripcion));
                    URL.Append(string.Concat("&ocpropresensian=", ordComDet.Producto.Prd_Presentacion));
                    URL.Append(string.Concat("&ocprounidadsian=", ordComDet.Producto.Prd_UniNe));
                    URL.Append(string.Concat("&ocprocostosian=", ordComDet.ProductoPrecio.Prd_Pesos.ToString()));
                    URL.Append(string.Concat("&ocantidadsian=", ordComDet.Ord_Cantidad.ToString()));
                    URL.Append(string.Concat("&ocimportesian=", (ordComDet.Ord_Cantidad * ordComDet.ProductoPrecio.Prd_Pesos).ToString()));
                    arregloURL.Add(URL.ToString());
                }
                //Crear URL de datos de encabezado
                URL.Clear();
                URL.Append(string.Concat("http://148.244.244.141/oc/SubeFinalSuc.asp?oczonnumsian=", Cd.Id_Cd.ToString()));
                URL.Append(string.Concat("&oczondescsian=", Cd.Cd_Descripcion));
                URL.Append(string.Concat("&ocnumsian=", ordCompra.Id_Ord.ToString()));
                URL.Append(string.Concat("&ocusunumsian=", Cd.Cd_NumMacola != null ? Cd.Cd_NumMacola.ToString() : "0")); //"27868"
                URL.Append(string.Concat("&ocusunomsian=", Cd.Cd_Descripcion));
                URL.Append(string.Concat("&oczoncallesian=", Cd.Cd_Calle));
                URL.Append(string.Concat("&oczoncolsian=", Cd.Cd_Colonia));
                URL.Append(string.Concat("&oczoncd=", Cd.Cd_Ciudad));
                URL.Append(string.Concat("&oczonedo=", Cd.Cd_Estado));
                URL.Append(string.Concat("&oczonlnum=", Cd.Cd_Numero));
                URL.Append(string.Concat("&oczoncpsian=", Cd.Cd_CP));
                URL.Append(string.Concat("&octotalsian=0"));
                URL.Append(string.Concat("&ocinvsian=0"));
                URL.Append(string.Concat("&octransian=0"));
                arregloURL.Add(URL.ToString());

                return arregloURL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void OrdenCompra_CrearEnviaXML(int Id_Emp, int Id_Cd, int Id_Ord, DateTime fechaOrden, string Ord_EstatusStr)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Consulta encabezado de la orden de compra
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = Id_Emp;
                ordCompra.Id_Cd = Id_Cd;
                ordCompra.Id_Ord = Id_Ord;
                new CN_CapOrdenCompra().ConsultaOrdenCompra(ref ordCompra, sesion.Emp_Cnx);
                //Consulta detalle de la orden de compra
                List<OrdenCompraDet> listaOrdCompraDet = new List<OrdenCompraDet>();
                OrdenCompraDet ordenCompraDet = new OrdenCompraDet();

                ordenCompraDet.Id_Emp = Id_Emp;
                ordenCompraDet.Id_Cd = Id_Cd;
                ordenCompraDet.Id_Ord = Id_Ord;
                new CN_CapOrdenCompraDet().ConsultaOrdenCompraDetalle_Lista(ordenCompraDet, sesion.Emp_Cnx, ref listaOrdCompraDet);

                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, ordCompra.Id_Cd, ordCompra.Id_Emp, sesion.Emp_Cnx);

                StringBuilder XML_Enviar = new StringBuilder();
                XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>");
                XML_Enviar.Append("<OrdenCompra");
                XML_Enviar.Append(" fecha=\"\"");
                XML_Enviar.Append(" ocnumsian=\"\"");
                XML_Enviar.Append(" oczonnumsian=\"\" >");

                XML_Enviar.Append(" <Encabezado");
                XML_Enviar.Append(" ocnumsian=\"\"");
                XML_Enviar.Append(" oczonnumsian=\"\"");
                XML_Enviar.Append(" octransian=\"\"");
                XML_Enviar.Append(" ocinvsian=\"\"");
                XML_Enviar.Append(" octotalsian=\"\"");
                XML_Enviar.Append(" oczoncpsian=\"\"");
                XML_Enviar.Append(" oczonlnum=\"\"");
                XML_Enviar.Append(" oczonedo=\"\"");
                XML_Enviar.Append(" oczoncd=\"\"");
                XML_Enviar.Append(" oczoncolsian=\"\"");
                XML_Enviar.Append(" oczoncallesian=\"\"");
                XML_Enviar.Append(" ocusunommacola=\"\"");
                XML_Enviar.Append(" ocusunummacola=\"\"");
                XML_Enviar.Append(" ocfechamacola=\"\"");

                XML_Enviar.Append(" ocfechasian=\"\"");
                XML_Enviar.Append(" ocusunomsian=\"\"");
                XML_Enviar.Append(" ocusunumsian=\"\"");
                XML_Enviar.Append(" oczondescsian=\"\"/>");
                XML_Enviar.Append("<Detalle>");

                var importe = 0.0;
                //PARTIDA DETALLE
                if (listaOrdCompraDet.Count() > 0)
                {
                    foreach (OrdenCompraDet ocd in listaOrdCompraDet)
                    {
                        importe = Math.Round(ocd.Ord_Cantidad * ocd.ProductoPrecio.Prd_Pesos, 2);
                        XML_Enviar.Append(" <Partida");
                        XML_Enviar.Append(" ocnumsian=\"" + ocd.Id_Ord + "\"");
                        XML_Enviar.Append(" oczonnumsian=\"" + ocd.Id_Cd + "\"");
                        XML_Enviar.Append(" ocimportesian=\"" + importe + "\"");
                        XML_Enviar.Append(" occantidadsian=\"" + ocd.Ord_Cantidad + "\"");
                        XML_Enviar.Append(" ocprocostosian=\"" + ocd.ProductoPrecio.Prd_Pesos + "\"");
                        XML_Enviar.Append(" ocprounidadsian=\"" + ocd.Producto.Prd_UniNe + "\"");
                        XML_Enviar.Append(" ocpropresensian=\"" + ocd.Producto.Prd_Presentacion + "\"");
                        XML_Enviar.Append(" ocprodescsian=\"" + ocd.Producto.Prd_Descripcion.ToString().Replace("\"", "").Replace("'", "").Replace("&", "")
                            + "\"");
                        XML_Enviar.Append(" ocpronumsian=\"" + ocd.Id_Prd + "\"");
                        XML_Enviar.Append(" />");
                    }
                }



                XML_Enviar.Append(" </Detalle>");
                XML_Enviar.Append(" </OrdenCompra>");

                XmlDocument xml = new XmlDocument();

                xml.LoadXml(XML_Enviar.ToString());

                XmlNode OrdenCompra = xml.SelectSingleNode("OrdenCompra");
                OrdenCompra.Attributes["fecha"].Value = string.Format("{0:s}", ordCompra.Ord_Fecha);
                OrdenCompra.Attributes["ocnumsian"].Value = ordCompra.Id_Ord.ToString();
                OrdenCompra.Attributes["oczonnumsian"].Value = ordCompra.Id_Cd.ToString();


                XmlNode Cabecera = OrdenCompra.SelectSingleNode("Encabezado");

                Cabecera.Attributes["ocnumsian"].Value = ordCompra.Id_Ord.ToString();
                Cabecera.Attributes["oczonnumsian"].Value = Cd.Id_Cd.ToString();
                Cabecera.Attributes["octransian"].Value = Cd.Cd_NumMacola != null ? Cd.Cd_NumMacola.ToString() : Cd.Id_Cd.ToString();
                Cabecera.Attributes["ocinvsian"].Value = ordCompra.Id_Ord.ToString();
                Cabecera.Attributes["octotalsian"].Value = "0";
                Cabecera.Attributes["oczoncpsian"].Value = Cd.Cd_CP.ToString();
                Cabecera.Attributes["oczonlnum"].Value = Cd.Cd_Numero.ToString();
                Cabecera.Attributes["oczonedo"].Value = Cd.Cd_Descripcion;
                Cabecera.Attributes["oczoncd"].Value = Cd.Cd_Descripcion.ToString();
                Cabecera.Attributes["oczoncolsian"].Value = Cd.Cd_Colonia.ToString();
                Cabecera.Attributes["oczoncallesian"].Value = Cd.Cd_Calle.ToString();
                Cabecera.Attributes["ocusunommacola"].Value = "100";
                Cabecera.Attributes["ocusunummacola"].Value = "0";
                Cabecera.Attributes["ocfechamacola"].Value = string.Format("{0:s}", ordCompra.Ord_Fecha); ;
                Cabecera.Attributes["ocfechasian"].Value = string.Format("{0:s}", ordCompra.Ord_Fecha); ;
                Cabecera.Attributes["ocusunomsian"].Value = Cd.Cd_Descripcion.ToString();
                Cabecera.Attributes["ocusunumsian"].Value = ordCompra.Id_Pvd.ToString();
                Cabecera.Attributes["oczondescsian"].Value = Cd.Cd_Descripcion.ToString();



                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xml.WriteTo(tx);
                string xmlString = sw.ToString();

                //throw new Exception(sw.ToString());



                XmlDocument xmlOrdenCompra = new XmlDocument();


                OrdendeCompra.Service1 sianEnvioOrdendeCompra = new OrdendeCompra.Service1();


                object sianEnvioOrdendeCompraResult = sianEnvioOrdendeCompra.OrdenCompra(xmlString);

                // xmlOrdenCompra.LoadXml(sianEnvioOrdendeCompraResult.ToString());


                int verificador = 0;
                ordCompra.Id_Emp = Id_Emp;
                ordCompra.Id_Cd = Id_Cd;
                ordCompra.Id_Ord = Id_Ord;
                ordCompra.Ord_Estatus = "I";
                new CN_CapOrdenCompra().ModificarOrdenCompra_Estatus(ordCompra, sesion.Emp_Cnx, ref verificador);

                try
                {
                    ordCompra.Ord_EstatusEmision = Convert.ToInt32(sianEnvioOrdendeCompraResult);
                }
                catch (FormatException e)
                {
                    ordCompra.Ord_EstatusEmision = 5;
                }


                ordCompra.Ord_EstatusEmisionStr = getEstatusEmision(ordCompra.Ord_EstatusEmision);

                new CN_CapOrdenCompra().ModificarOrdenCompra_EstatusEmision(ordCompra, sesion.Emp_Cnx, ref verificador);

                Alerta(getEstatusEmision(ordCompra.Ord_EstatusEmision));

                if (ordCompra.Ord_EstatusEmision == 1)
                {
                    this.OrdenCompra_Impresion(Id_Emp, Id_Cd, Id_Ord, fechaOrden, Ord_EstatusStr);
                }
                // RAM1.ResponseScripts.Add("Alert('" + sianEnvioOrdendeCompraResult.ToString() + "')");

            }
            catch (Exception ex)
            {
                this.EnviaEmail(ex.ToString());
                throw ex;
            }
        }

        private void OrdenCompra_Impresion(int Id_Emp, int Id_Cd, int Id_Ord, DateTime fechaOrden, string Ord_EstatusStr)
        {
            try
            {
                if (Ord_EstatusStr.Contains("Baja"))
                    throw new Exception("OrdCompra_estatus_incorrecto");
                else
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    ArrayList ALValorParametrosInternos = new ArrayList();
                    //Consulta centro de distribución
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                    ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                    ALValorParametrosInternos.Add(sesion.Id_Emp);
                    ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                    ALValorParametrosInternos.Add(Id_Ord);
                    ALValorParametrosInternos.Add(Cd.Cd_Descripcion);
                    ALValorParametrosInternos.Add(Cd.Cd_Calle);
                    ALValorParametrosInternos.Add(Cd.Cd_Numero.ToString());
                    ALValorParametrosInternos.Add(Cd.Cd_CP);
                    ALValorParametrosInternos.Add(Cd.Cd_Ciudad);
                    ALValorParametrosInternos.Add(Cd.Cd_Estado);
                    ALValorParametrosInternos.Add(fechaOrden.Day.ToString());
                    ALValorParametrosInternos.Add(fechaOrden.Month.ToString());
                    ALValorParametrosInternos.Add(fechaOrden.Year.ToString());
                    Type instance = null;
                    instance = typeof(LibreriaReportes.OrdenCompraImpresion);
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                    //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CancelarOrdenCompra(int Id_Emp, int Id_Cd, int Id_Ord, string Ord_EstatusStr)
        {
            try
            {
                if (Ord_EstatusStr.Contains("Baja"))
                    this.DisplayMensajeAlerta("rgOrdCompra_delete_error_cancelacion");
                else
                    if (Ord_EstatusStr.Contains("Impreso"))
                        this.DisplayMensajeAlerta("OrdCompra_estatus_incorrecto");
                    else
                    {
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        OrdenCompra ordCompra = new OrdenCompra();
                        ordCompra.Id_Emp = Id_Emp;
                        ordCompra.Id_Cd = Id_Cd;
                        ordCompra.Id_Ord = Id_Ord;

                        String Resultado = "";
                        new CN_CapOrdenCompra().SP_Consulta_Entradas_OC(Id_Emp, Id_Cd, Id_Ord, sesion.Emp_Cnx, ref Resultado);

                        if (Resultado.Trim() == "")
                        {
                            int verificador = 0;
                            new CN_CapOrdenCompra().EliminarOrdenCompra(ordCompra, sesion.Emp_Cnx, ref verificador);
                            this.DisplayMensajeAlerta("CapOrdCompra_delete_ok");

                        }
                        else
                        {
                            Alerta(Resultado.Trim());
                        }
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        private List<OrdenCompra> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<OrdenCompra> listOrdenCompra = new List<OrdenCompra>();
                List<OrdenCompra> listOrdenCompraFinal = new List<OrdenCompra>();
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = sesion.Id_Emp;
                ordCompra.Id_Cd = sesion.Id_Cd_Ver;
                ordCompra.Id_U = sesion.Propia ? sesion.Id_U : -1;

                new CN_CapOrdenCompra().ConsultaOrdenCompra_Lista(ordCompra, sesion.Emp_Cnx, ref listOrdenCompra
                , this.txtFolio1.Text == string.Empty ? -1 : Convert.ToInt32(this.txtFolio1.Text)
                , this.txtFolio2.Text == string.Empty ? -1 : Convert.ToInt32(this.txtFolio2.Text)
                , this.txtFecha1.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha1.SelectedDate)
                , this.txtFecha2.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha2.SelectedDate)
                , cmbEstatus.SelectedValue
                );


                foreach (OrdenCompra oCompra in listOrdenCompra)
                {
                    oCompra.Ord_EstatusEmisionStr = getEstatusEmision(oCompra.Ord_EstatusEmision);
                    listOrdenCompraFinal.Add(oCompra);
                }

                return listOrdenCompraFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("btnBuscar_error"))
                    Alerta("Error al momento de filtrar la información");
                else
                    if (mensaje.Contains("PermisoEliminarDenegado"))
                        Alerta("Operación denegada, no tiene permisos para dar de baja ordenes de compra");
                    else
                        if (mensaje.Contains("PermisoImprimirDenegado"))
                            Alerta("Operación denegada, no tiene permisos para imprimir ordenes de compra");
                        else
                            if (mensaje.Contains("EnvioInternet_ProveedorNoAlmacen"))
                                Alerta("Operación denegada, solo se permite enviar órdenes de compra con el proveedor \"Almacen central\" (clave 100)");
                            else
                                if (mensaje.Contains("OrdCompra_estatus_incorrecto"))
                                    Alerta("Estatus de orden de compra incorrecto");
                                else
                                    if (mensaje.Contains("rgOrdCompra_delete_error_cancelacion"))
                                        Alerta("Esta orden de compra ya está cancelada");
                                    else
                                        if (mensaje.Contains("CapOrdCompra_envioInternet_ok"))
                                            Alerta("La orden de compra se ha enviado correctamente");
                                        else
                                            if (mensaje.Contains("CapOrdCompra_delete_error"))
                                                Alerta("Error al momento de dar de baja la orden de compra");
                                            else
                                                if (mensaje.Contains("CapOrdCompra_print_error"))
                                                    Alerta("Error al momento de generar el reporte de impresión de orden de compra");
                                                else
                                                    if (mensaje.Contains("CapOrdCompra_mail_error"))
                                                        Alerta("Error al momento de enviar la orden de compra por internet");
                                                    else
                                                        if (mensaje.Contains("CapOrdCompra_delete_ok"))
                                                            Alerta("La orden de compra se ha dado de baja (estatus \"B\") correctamente");
                                                        else
                                                            if (mensaje.Contains("RAM1_AjaxRequest"))
                                                                Alerta("Error al momento de actualizar el grid de ordenes de compra");
                                                            else
                                                                if (mensaje.Contains("rgOrdCompra_NeedDataSource"))
                                                                    Alerta("Error al cargar el grid de detalle de orden de compra");
                                                                else
                                                                    if (mensaje.Contains("rgOrdCompra_ItemCommand"))
                                                                        Alerta("Error al ejecutar un evento (rgOrdCompra_ItemCommand) al cargar el grid de orden de compra");
                                                                    else
                                                                        if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                            Alerta("Error al cambiar de página");
                                                                        else
                                                                            Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }


        private void EnviaEmail(string error)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append(" <table>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td><img src=\"cid:companylogo\"></td>");
                cuerpo_correo.Append("   <td valign='middle' style='text-decoration: underline'><b><font face= 'Tahoma' size = '4'>Error Envio Orden de Compra SUCURSAL#" + session.Id_Cd_Ver + " </font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><b><font face= 'Tahoma' size = '2'>Error</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td align='left' colspan='2'><b><font face= 'Tahoma' size = '2' color='#777777'>" + error);
                cuerpo_correo.Append("</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append(" </table>");
                cuerpo_correo.Append("</div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                string To = "rafael.garcia@gibraltar.com.mx";
                m.To.Add(new MailAddress(To));
                m.Subject = "Error Envio Orden de Compra #" + session.Id_Cd_Ver;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }
                m.AlternateViews.Add(vistaHtml);
                sm.Send(m);
            }
            catch (Exception)
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
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
        }
    }
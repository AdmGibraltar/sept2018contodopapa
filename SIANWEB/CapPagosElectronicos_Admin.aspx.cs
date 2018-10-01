using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using CapaDatos;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Telerik.Web.UI.Calendar;
using System.Data;

namespace SIANWEB
{
    public partial class CapPagosElectronicos_Admin : System.Web.UI.Page
    {
        private string CryptoPassIn = "k3y9u1m1c4";
        private string CryptoPassOut = "k3y9u1m1c4";
        private string BuzonKey
        {
            get
            {
                if (Session["TimeBuzonServer"] == null || Session["TimeBuzonServer"].ToString() == "")
                {
                    wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
                    DateTime FechaServer = Convert.ToDateTime(CapaDatos.clsCrypto.BlowFish.Decrypt(wsBuzon.GetKey(), CryptoPassIn)).AddSeconds(-20);
                    Session["TimeBuzonServer"] = (FechaServer - DateTime.Now).TotalSeconds;
                }

                DateTime TimeServer = DateTime.Now.AddSeconds(Convert.ToDouble(Session["TimeBuzonServer"]));

                string key = String.Format(
                    "{0}|&|{1}|&|{2}",
                    "SIANWEB",
                    TimeServer.ToString("yyyy-MM-dd HH:mm:ss"),
                    TimeServer.AddSeconds(80).ToString("yyyy-MM-dd HH:mm:ss")
                );

                return CapaDatos.clsCrypto.BlowFish.Encrypt(key, CryptoPassOut);
            }
        }
        
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        //SAUL GUERRA 20150716  BEGIN
        protected string Emp_RFC
        {
            get
            {
                return (string)(Session["Emp_RFC"] != null ? Session["Emp_RFC"] : GetEmp_RFC());
            }
        }

        private string GetEmp_RFC()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Session["Emp_RFC"] = (new CN_CapPagoElectronico().ConsultaEmpRFC(Sesion.Id_Emp, Sesion.Emp_Cnx));

            return (string)Session["Emp_RFC"];
        }
        //SAUL GUERRA 20150716  END

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);

                //string str = Context.Items["href"].ToString();
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);Context.Items.Add("href", pag[pag.Length-1]);Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
            }
            else
            {
                if (!IsPostBack)
                {
                    
                    ValidarPermisos();
                    CargarCentros();
                    CargarAcreedores();
                    CargarEstatus();
                    //JFCV cambie el inicializar a abajo porque necesito que ya estén cargados los combos cuando cargue el grid 
                    //para poder filtrar por estatus = creado 
                    CmbEstatus.SelectedIndex = 1;
                    Inicializar();

                }
                CargarCtaGastos();
            }
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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

                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                rgPagoElectronico.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        //protected void CmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    CargarConceptos();
        //}


        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    this.lblMensaje.Text = string.Empty;
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        RAM1.ResponseScripts.Add("return AbrirVentana_Gastos('-1', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')");
                        //Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        protected void rgPagoElectronico_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //rgPagoElectronico.DataSource = GetList();
                    int? tipo;
                    int? cuenta;
                    int? acreedor;
                    int? estatus;
                    //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                    int? id_pagoElectronico;

                    if (CmbTipo.SelectedValue == "")
                    {
                        tipo = null;
                    }
                    else
                    {
                        tipo = Int32.Parse(CmbTipo.SelectedValue);
                    }

                    if (CmbAcreedor.SelectedValue == "")
                    {
                        acreedor = null;
                    }
                    else
                    {
                        acreedor = Int32.Parse(CmbAcreedor.SelectedValue);
                    }

                    if (cmdCtaGastos.SelectedValue == "")
                    {
                        cuenta = null;
                    }
                    else
                    {
                        cuenta = Int32.Parse(cmdCtaGastos.SelectedValue);
                    }

                    if (CmbEstatus.SelectedValue == "")
                    {
                        estatus = null;
                    }
                    else
                    {
                        estatus = Int32.Parse(CmbEstatus.SelectedValue);
                    }

                    if (txtidPagoElectronico.Text  == "")
                    {
                        id_pagoElectronico = -1;
                    }
                    else
                    {
                        id_pagoElectronico = Int32.Parse(txtidPagoElectronico.Text);
                    }


                    rgPagoElectronico.DataSource = GetList(tipo, acreedor, cuenta, estatus, id_pagoElectronico);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoElectronico_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Int32 item = default(Int32);
                item = e.Item.ItemIndex;

                int Id_PagElec = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_PagElec"].Text);
                int id_PagElecStatus = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_PagElecEstatus"].Text);
                //JFCV 17 dic 2015 , que solo se puedan editar y cancelar Solicitudes que no estén autorizadas.

                switch (e.CommandName.ToString())
                {
                    case "XML":
                        descargarXML(Id_PagElec);
                        break;
                    case "PDF":
                        descargarPDF(Id_PagElec);
                        break;
                    case "Soporte":
                        descargarSOPORTE(Id_PagElec);
                        break;
                    case "Comprobantes":
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantes('", Id_PagElec, "')"));
                        break;
                    case "Delete":
                        if (id_PagElecStatus == 2 || id_PagElecStatus == 5 || id_PagElecStatus == 4 )
                        {
                            Alerta("Solo se pueden Cancelar Solicitudes que estén en estatus creado o rechazado.");
                        }
                        else
                        {
                            Cancelar(Id_PagElec);
                        }
                        break;
                    case "Modificar":
                        if (id_PagElecStatus == 2 || id_PagElecStatus == 5 || id_PagElecStatus == 4)
                        {
                            Alerta("Solo se pueden Editar Solicitudes que estén en estatus creado o rechazado.");
                        }
                        else
                        {
                            RAM1.ResponseScripts.Add("return AbrirVentana_GastosModificar(" + Id_PagElec.ToString() + ")");
                        }
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoElectronico_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                ImageButton imgBtnSoporte = ((ImageButton)((Telerik.Web.UI.GridDataItem)(e.Item)).FindControl("imgSoporte"));
                ImageButton imgBtnComprobantes = ((ImageButton)((Telerik.Web.UI.GridDataItem)(e.Item)).FindControl("imgComprobantes"));
                //ahora puede ser que tenga dos tipos de archivos , asi que el que tenga soporte no quiere 
                //decir que no tenga xml y pdf 
                if (((PagoElectronico)(e.Item.DataItem)).PagElec_Soporte != null)
                {
                    imgBtnSoporte.Enabled = true;
                    imgBtnSoporte.Attributes["class"] = "edit";
                    imgBtnSoporte.Attributes["title"] = "Archivo de Soporte";
                 }
                else
                {
                    //lo desabilito porque ahora siempre debe tener comprobantes 
                    //imgBtnSoporte.Enabled = false;
                    //imgBtnSoporte.Attributes.Remove("class");
                    //imgBtnSoporte.Attributes["title"] = "Sin Archivos";
                    //imgBtnComprobantes.Enabled = true;
                    //imgBtnComprobantes.Attributes["class"] = "edit";
                    //imgBtnComprobantes.Attributes["title"] = "Comprobantes PDF y XML";
                }
                //lo desabilito porque ahora siempre debe tener comprobantes 
                //if (((PagoElectronico)(e.Item.DataItem)).PagElecArchivo.Count > 0)
                //{
                    GridDataItem dataItem = (GridDataItem)e.Item;
                    //////if (Convert.ToInt32(dataItem["Id_PagElecTipo"].Text) == 2)
                    //////    dataItem["Acr_Nombre"].Text = "[Varios]";

                    imgBtnComprobantes.Visible = true;
                    imgBtnComprobantes.Enabled = true;
                    imgBtnComprobantes.Attributes["class"] = "edit";
                    imgBtnComprobantes.Attributes["title"] = "Comprobantes PDF y XML";

                //}
                //    //JFCV si no tiene enonces el icono es no disponible
                //else
                //{
                //    imgBtnComprobantes.Visible = false;
                //    imgBtnComprobantes.Enabled = false;
                //    imgBtnComprobantes.Attributes.Remove("class");
                //    imgBtnSoporte.Attributes["title"] = "Sin Archivos";
                //}
                     
            }
        }

        protected void CmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //CargarConceptos();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            rgPagoElectronico.Rebind();
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            rgPagoElectronico.Rebind();
        }

        private void Inicializar()
        {
            rgPagoElectronico.Rebind();
        }

        //JFCV 28 EditorSeparator 2015
        protected void rgPagoElectronico_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgPagoElectronico.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //protected void CargarConceptos()
        //{
        //    //Sesion Sesion = new Sesion();
        //    //Sesion = (Sesion)Session["Sesion" + Session.SessionID];

        //    //int Id_PagElecTipo = 0;

        //    //if (CmbTipo.SelectedValue != "")
        //    //{
        //    //    Id_PagElecTipo = Int32.Parse(CmbTipo.SelectedValue);
        //    //}

        //    //CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //    //CN_Comun.LlenaCombo(1, Id_PagElecTipo, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatPagoElectronicoConcepto_Combo", ref CmbCuenta);
        //}

        protected void CargarCtaGastos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<PagoElectronicoCuenta> CtaGastos = new List<PagoElectronicoCuenta>();

            (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                new PagoElectronicoCuenta() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd },
                Sesion.Emp_Cnx,
                ref CtaGastos
            );

            cmdCtaGastos.Items.Clear();
            if (CtaGastos.Count > 0)
            {
                //JFCV 18 noviembre 2016 que se pueda buscar por la subcuenta  punto 5
                var datasource = from x in CtaGastos
                                 select new
                                 {
                                     x.Id_Emp,
                                     x.Id_Cd,
                                     x.Id_PagElecCuenta,
                                     x.PagElecCuenta_CC,
                                     x.PagElecCuenta_CuentaPago,
                                     x.PagElecCuenta_Descripcion,
                                     x.PagElecCuenta_Numero,
                                     x.PagElecCuenta_SubCuenta,
                                     x.PagElecCuenta_SubSubCuenta,
                                     DisplayField = String.Format("{0} ({1})", x.PagElecCuenta_Descripcion, x.PagElecCuenta_SubCuenta)
                                 };
 
                cmdCtaGastos.DataSource = datasource;
                cmdCtaGastos.DataValueField = "Id_PagElecCuenta";
                cmdCtaGastos.DataTextField = "DisplayField";
                //cmdCtaGastos.DataTextField = "PagElecCuenta_Descripcion";
                cmdCtaGastos.DataBind();
            }
        }

        protected void CargarAcreedores()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatAcreedor_Combo", ref CmbAcreedor);
        }

        private void CargarEstatus()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatPagoElectronicoEstatus_Combo", ref CmbEstatus);
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
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }

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
                    {
                        this.rtb1.Items[6].Visible = false;
                    }
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.rtb1.Items[5].Visible = false;
                    }
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = false;
                    //Correo
                    this.rtb1.Items[1].Visible = false;
                    //Grabar se pone desabilitado JFCV 
                    this.rtb1.Items[5].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Guardar()
        {

        }

        protected void Nuevo()
        {
        }

        protected void Cancelar(int id_PagElec)
        {
            try
            {
                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                PagoElectronico pagoElectronico = new PagoElectronico();
                pagoElectronico.Id_Emp = session.Id_Emp;
                pagoElectronico.Id_Cd = session.Id_Cd_Ver;
                pagoElectronico.Id_PagElec = id_PagElec;

                int verificador = 0;

                CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(session.Emp_Cnx);
                oDB.BeginTransaction();

             
                clsPagoElectronico.CancelarPagoElectronico(pagoElectronico, session.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, session.Emp_Cnx);

                    wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
                    foreach (PagoElectronicoArchivo factura in pagoElectronico.PagElecArchivo)
                    {
                        DataTable DT = ObtenerDatos(factura.PagElec_XMLStream.Length > 0 ? factura.PagElec_XMLStream : null);

                        if (DT == null)
                        {
                            //si marca error , quitar caracteres raros y addenda para que se suba el archivo y no falle
                            var archivoxml = System.Text.Encoding.ASCII.GetString(factura.PagElec_XMLStream);

                            string xmlantes = antes(archivoxml);
                            string xmldespues = despues(archivoxml);
                            archivoxml = xmlantes + xmldespues;
                            
                            //JFCV 1 abr 2016 marcaba error porque tenia " en descripcion 
                            archivoxml = corregirDescripcion(archivoxml);

                            archivoxml = SanitizeXmlString(archivoxml);
                            archivoxml = archivoxml.Replace("'", Convert.ToString(Convert.ToChar(34)));

                            // Este es si eligen que se grabe la addenda en el xml por eso esta comentarizado si no hay que quitarlo
                            //byte[] facturaxml = Encoding.ASCII.GetBytes(archivoxml);
                            //DT = ObtenerDatos(facturaxml.Length > 0 ? facturaxml : null);

                            factura.PagElec_XMLStream = Encoding.ASCII.GetBytes(archivoxml);

                            DT = ObtenerDatos(factura.PagElec_XMLStream.Length > 0 ? factura.PagElec_XMLStream : null);
                            if (DT == null)
                            {
                                Alerta("Error al cancelar el folio . ");
                                return;
                            }
                        }

                        if (!wsBuzon.DelAsigFacGastos(
                             BuzonKey,
                             (string)DT.Rows[0]["rfc"],
                             (string)DT.Rows[0]["serie"],
                             (string)DT.Rows[0]["folio"],
                             session.Id_Emp,
                             session.Id_Cd_Ver,
                             session.Id_U,
                             Emp_RFC
                         ))
                        {
                            Alerta("El pago no pudo ser cancelado.");
                            oDB.RollBack();
                        }

                    }

                    oDB.Commit();
                    Alerta("El pago fue cancelado.");
                    rgPagoElectronico.Rebind();
                }
                else
                {
                    oDB.RollBack();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        #region leerelxml

        public static string antes(string s)
        {
            int l = s.IndexOf("<cfdi:Addenda>");
            if (l > 0)
            {
                return s.Substring(0, l);
            }
            // si no encuentra adenda regreso el valor de s completo
            return s;

        }

        public static string despues(string s)
        {
            int l = s.IndexOf("</cfdi:Addenda>");
            if (l > 0)
            {
                //le sumo 15 para que  tome de donde termina "</cfdi:Addenda>"
                return s.Substring(l + 15, s.Length - l - 15);
            }
            //si no tiene adenda regreso espacio vacio para que al juntar el antes y despues no se duplique 
            return "";

        }

        //JFCV 1 abr 2016 
        public static string corregirDescripcion(string s)
        {
            int seguir = 0;
            int inicial = -1;
            do
            {
                int l = s.IndexOf("descripcion=", inicial + 1);
                if (l > 0)
                {
                    inicial = l;
                    int lv = s.IndexOf("valorUnitario=", inicial + 1);
                    if (lv > 0)
                    {
                        string sdescripcion = "";
                        sdescripcion = s.Substring(l + 13, (lv - 2 - (l + 13)));
                        //string corregido = sdescripcion.Replace("\"", "");
                        //corregido = corregido.Replace("&", "");
                        string corregido = sdescripcion.Replace("&", "&amp;");
                        corregido = corregido.Replace("\"", "&quot;"); 
                        s = s.Substring(0, l + 13) + corregido + s.Substring(lv - 2, s.Length - lv + 2);
                    }
                    //JFCV 01 abril 2016 factura[0].XMLFile.(" Descipcion=\"" + d.Prd_Descripcion.ToString().Replace("\"", "").Replace("'", "").Replace("&", "")
                }
                else
                {
                    seguir = 1;
                }


            } while (seguir == 0);


            // si no encuentra adenda regreso el valor de s completo
            return s;

        }



        /// <summary>
        /// Remove illegal XML characters from a string.
        /// </summary>
        public string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            StringBuilder buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {

                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        public bool IsLegalXmlChar(int character)
        {

            if (character == 0x27)
                return false;

            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                 character == 0x27 ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

        #endregion leerelxml



        //SAUL GUERRA 20150803  BEGIN
        private DataTable ObtenerDatos(byte[] xmlObject)
        {
            DataTable dtable = new DataTable();
            dtable.Columns.Add("rfc", typeof(string));
            dtable.Columns.Add("fecha", typeof(DateTime));
            dtable.Columns.Add("serie", typeof(string));
            dtable.Columns.Add("folio", typeof(string));
            dtable.Columns.Add("importe", typeof(string));
            //jfcv 24nov2015 Agregar 3 valores de impuestos
            dtable.Columns.Add("subtotal", typeof(string));
            dtable.Columns.Add("ivaretenido", typeof(string));
            dtable.Columns.Add("impuestoretenido", typeof(string));
            dtable.Columns.Add("iva", typeof(string));
            dtable.Columns.Add("uuid", typeof(string));


            if (xmlObject != null && xmlObject.Length > 0)
            {
                try
                {
                    MemoryStream xmlStream = new MemoryStream(xmlObject);
                    string rfc = null;
                    string serie = null;
                    string folio = null;
                    DateTime? fecha = null;
                    decimal? importe = null;
                    //jfcv 24nov2015 Agregar 3 valores de impuestos
                    decimal? subtotal = null;
                    decimal? ivaretenido = null;
                    decimal? impuestoretenido = null;
                    decimal? iva = null;
                    string uuid = "";

                    XmlDocument xmldoc = new XmlDocument();
                    XmlNodeList xmlnode;
                    try
                    {
                        xmldoc.Load(xmlStream);
                        Session["xml"] = xmlStream;
                    }
                    catch (Exception ex)
                    {
                        dtable = null;
                        //Alerta("Problemas al leer el XML de la factura" + ex.Message);
                        return dtable;
                    }
                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Comprobante");
                    try
                    {
                        serie = xmlnode[0].Attributes["serie"].Value;
                    }
                    catch
                    {
                        try
                        {
                            serie = xmlnode[0].Attributes["Serie"].Value;
                        }
                        catch
                        {
                            serie = "";
                        }
                    }
                    //JFCV 04 ene 2016 algunas facturas pueden no traer serie ni folio
                    try
                    {
                        folio = xmlnode[0].Attributes["folio"].Value;
                    }
                    catch
                    {
                        try
                        {
                            folio = xmlnode[0].Attributes["Folio"].Value;
                        }
                        catch
                        {
                            folio = "";
                        }
                    }
                    //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                    try
                    {
                        fecha = Convert.ToDateTime(xmlnode[0].Attributes["fecha"].Value);
                        importe = decimal.Parse("0" + xmlnode[0].Attributes["total"].Value);
                        //jfcv 24nov2015 Agregar 3 valores de impuestos
                        subtotal = decimal.Parse("0" + xmlnode[0].Attributes["subTotal"].Value);
                        //jfcv fin
                    }
                    catch
                    {
                        fecha = Convert.ToDateTime(xmlnode[0].Attributes["Fecha"].Value);
                        importe = decimal.Parse("0" + xmlnode[0].Attributes["Total"].Value);
                        //jfcv 24nov2015 Agregar 3 valores de impuestos
                        subtotal = decimal.Parse("0" + xmlnode[0].Attributes["SubTotal"].Value);
                        //jfcv fin
                    }
                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Emisor");
                    //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                    try
                    {
                        rfc = xmlnode[0].Attributes["rfc"].Value;
                    }
                    catch
                    {
                        rfc = xmlnode[0].Attributes["Rfc"].Value;
                    }

                    xmlnode = xmldoc.GetElementsByTagName("tfd:TimbreFiscalDigital");
                    uuid = xmlnode[0].Attributes["UUID"].Value;
                    if (folio == "")
                    {
                        folio = uuid.Substring(0, 10);
                    }

                    //jfcv 24nov2015 Agregar 3 valores de impuestos
                    iva = 0;
                    try
                    {
                        xmlnode = xmldoc.GetElementsByTagName("cfdi:Traslados");
                        if (xmlnode[0].ChildNodes.Count > 0)
                        {
                            if (xmlnode[0].ChildNodes.Count == 1)
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                }
                            }
                            else
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                    if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IVA")
                                        iva = iva + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    if (xmlnode[0].ChildNodes[1].Attributes["Impuesto"].Value == "002")
                                        iva = iva + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    ivaretenido = 0;
                    impuestoretenido = 0;
                    try
                    {
                        xmlnode = xmldoc.GetElementsByTagName("cfdi:Retenciones");
                        if (xmlnode[0].ChildNodes.Count > 0)
                        {
                            if (xmlnode[0].ChildNodes.Count == 1)
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                }
                            }
                            else
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                    if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    if (xmlnode[0].ChildNodes[1].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    //jfcv fin 24nov2015 Agregar 3 valores de impuestos
                    //jfcv 24nov2015 Agregar 3 valores de impuestos
                    //dtable.Rows.Add(rfc, fecha, serie, folio, importe);
                    dtable.Rows.Add(rfc, fecha, serie, folio, importe, subtotal, ivaretenido, impuestoretenido, iva, uuid);
                }
                catch (Exception ex)
                {
                    dtable = null;
                }
            }
            else
            {
                dtable = null;
            }


            return dtable != null && dtable.Rows.Count > 0 ? dtable : null;
        }
       

        //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
        private List<PagoElectronico> GetList(int? tipo, int? acreedor, int? cuenta, int? estatus, int? id_pagoelectronico)
        {
            try
            {
                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
                List<PagoElectronico> list = new List<PagoElectronico>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                PagoElectronico pagoElectronico = new PagoElectronico();
                pagoElectronico.Id_Emp = session.Id_Emp;
                pagoElectronico.Id_Cd = session.Id_Cd_Ver;
                pagoElectronico.Id_Acr_Filtro = acreedor;
                pagoElectronico.Id_PagElecTipo_Filtro = tipo;
                pagoElectronico.Id_PagElecCuenta_Filtro = cuenta;
                // agregue filtro por estatus ya que no estaba funcionando
                pagoElectronico.Id_PagElecEstatus_Filtro = estatus;
                //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                pagoElectronico.Id_PagElec = Convert.ToInt32(id_pagoelectronico);
                clsPagoElectronico.ConsultaPagoElectronicoAdmin(pagoElectronico, session.Emp_Cnx, ref list);

                return list;

                //list = list.Where(x => x.Id_PagElecEstatus != 4).ToList();

                //if (CmbEstatus.SelectedValue == "")
                //{
                //    return list;
                //}
                //else
                //{
                //    return list.Where(x => x.Id_PagElecEstatus == Int32.Parse(CmbEstatus.SelectedValue)).ToList();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void descargarXML(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            if (pagoElectronico.PagElec_Xml != string.Empty)
            {
                string ruta = null;
                System.IO.StreamWriter sw = null;
                 // en el servidor , no se estan abriendo los XML
                ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".txt";

                if (File.Exists(ruta))
                    File.Delete(ruta);
                if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml"))
                    File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
                sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                sw.WriteLine(pagoElectronico.PagElec_Xml.ToString());
                sw.Close();
                File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
                RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", id_PagElec.ToString(), ".xml')"));
            }
            else
            {
                Alerta("El registro no tiene un archivo XML.");
            }
        }

        private void descargarPDF(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            byte[] archivoPdf = pagoElectronico.PagElec_Pdf;

            if (archivoPdf != null)
            {
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("GASTO_"
                             , Sesion.Id_Emp.ToString()
                             , "_", Sesion.Id_Cd.ToString()
                             , "_", id_PagElec.ToString()
                             , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDFGastos, "')"));
                }
            }
            else
            {
                Alerta("El registro no tiene un archivo PDF.");
            }
        }

        private void descargarSOPORTE(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            byte[] archivoSoporte = pagoElectronico.PagElec_Soporte;

            if (archivoSoporte != null)
            {
                if (archivoSoporte.Length > 0)
                {
                    string tempPDFname = //pagoElectronico.PagElec_Soporte_Nombre; 
                    string.Concat(
                        "GASTO_Soporte_",
                        Sesion.Id_Emp.ToString(),
                        "_", Sesion.Id_Cd.ToString(),
                        "_", id_PagElec.ToString(),
                        "_", pagoElectronico.PagElec_Soporte_Nombre +".pdf"
                    );

                    //jfcv prueba abrir un archivo pdf 
                    //Byte[] data = archivoSoporte;
                     

                    ////// Send the file to the browser
                    //Response.AddHeader("Content-type", tempPDFname);
                    //Response.AddHeader("sdaadj_nombre", tempPDFname);
                    ////Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
                    //Response.AddHeader("Content-Disposition", "inline; filename=" + tempPDFname);
                    //Response.BinaryWrite(data); 

                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                    this.ByteToTempJPG(URLtempPDF, archivoSoporte);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDFGastos, "')"));
                }
            }
            else
            {
                Alerta("El registro no tiene un archivo de Soporte.");
            }
        }

        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        private void ByteToTempJPG(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }

            //System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(new MemoryStream(filebytes));

            //myBitmap.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg); 

            FileStream fs = new FileStream(
                tempPath,
                FileMode.CreateNew,
                FileAccess.Write
            );
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();

                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    return "";
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapPagoElectronico", "Id_PagElec", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
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

    }
}
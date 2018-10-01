using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.Xml;
using System.Text;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SIANWEB
{
    public partial class CapDevolucionRemision_Admin : System.Web.UI.Page
    {
        private bool Tm_ReqSpo
        {
            set
            {
                Session["Tm_ReqSpoREM" + Session.SessionID] = value;
            }
            get
            {
                return (bool)Session["Tm_ReqSpoREM" + Session.SessionID];
            }
        }

        public DataTable dt { get; set; }

        private void CargarTipoMovimiento()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatMovimiento_ComboParaRemisionesDevolucion", ref cmbTipoMovimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cmbTipoMovimiento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if (cmbTipoMovimiento.SelectedValue != "" && cmbTipoMovimiento.SelectedValue != "-1")
                {
                    Sesion sesion = new Sesion();
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    bool Tm_ReqSpo2 = false;
                    new CN_CatMovimientos().ConsultarTmovimientoReqSpo(sesion, int.Parse(cmbTipoMovimiento.SelectedValue), ref Tm_ReqSpo2);
                    Tm_ReqSpo = Tm_ReqSpo2;

                    hf_spo.Value = Tm_ReqSpo.ToString();
                }

                int tipo = Convert.ToInt32(cmbTipoMovimiento.SelectedValue);
                //txtClienteId.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);                  
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        CargarTipoMovimiento();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();
                        CargarEstatus();
                       
                        PoblarTablaDT();

                        dpFechaIni.DbSelectedDate = Sesion.CalendarioIni;
                        dpFechaFin.DbSelectedDate = Sesion.CalendarioFin;

                        double ancho = 0;
                        foreach (GridColumn gc in rg1.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rg1.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rg1.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rg1.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                //this.Nuevo();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rg1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
               // if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rg1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //DataRow[] dr = dt.Select(GenerarQry());
                //foreach (DataRow d in dr)
                //{
                //    dt.ImportRow(d);
                //}
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DateTime fechaPeriodoInicio = session2.CalendarioIni;
                DateTime fechaPeriodoFinal = session2.CalendarioFin;
                GridDataItem gdi;
                
                if (e.Item.ItemIndex > -1)
                {
                    gdi = rg1.Items[e.Item.ItemIndex];
                    //List<string> statusPosibles;

                    switch (e.CommandName.ToLower())
                    { 
                        case "editar":                       
                            editar(gdi);
                            break;
                        case "eliminar":
                            eliminar(gdi);
                            break;      
                        case "imprimir":
                            imprimir(gdi);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void imprimir(GridDataItem gi)
        {
            Sesion vSession = new Sesion();
            vSession = (Sesion)Session["Sesion" + Session.SessionID];

            Dictionary<string, object> vParametros = new Dictionary<string, object>();
            string vEstatus = gi["DevRem_Estatus"].Text.Trim();
            int vIdFact = 0;
            int vFolio = 0;
            string vNoEntradas = string.Empty;
                        
            vIdFact = Convert.ToInt32(gi["Id_Fac"].Text);
            vFolio = Convert.ToInt32(gi["Id_DevRem"].Text);
            vNoEntradas = gi["NumEntradas"].Text;

            if (vIdFact > 0)
            {
                vParametros.Add("IdFac", vIdFact);
                Session["Parametros" + Session.SessionID] = vParametros;
                Response.Redirect("CapFactura_Lista.aspx", false);
            }
            else
            {
                vParametros.Add("NumFin", vNoEntradas.Split(',').Select(x => Convert.ToInt32(x)).OrderBy(x => x).Max());
                vParametros.Add("NumIni", vNoEntradas.Split(',').Select(x => Convert.ToInt32(x)).OrderBy(x => x).Min());
                
                Session["Parametros" + Session.SessionID] = vParametros;                
                Response.Redirect("CapEntradaSalida_Lista.aspx", false);
            }
           
        }

        private void eliminar(GridDataItem gi)
        {
            Sesion session2 = new Sesion();
            session2 = (Sesion)Session["Sesion" + Session.SessionID];

            int vIdFact = 0;
            int vFolio = 0;
            string vEstatus = gi["DevRem_Estatus"].Text.Trim();
            CN_CapDevolucionRemision cnDevRem = new CN_CapDevolucionRemision();
            List<int> vListaEntradas = new List<int>();

            if (vEstatus.Equals("s", StringComparison.OrdinalIgnoreCase) || vEstatus.Equals("c", StringComparison.OrdinalIgnoreCase))            
            {                
                int verificador = 0;                

                vIdFact = Convert.ToInt32(gi["Id_Fac"].Text);
                vFolio = Convert.ToInt32(gi["Id_DevRem"].Text);
               
                if (vIdFact > 0)
                {
                    Alerta("No se puede cancelar una devolución con Factura, por favor cancelar desde el modulo de Facturas.");
                    return;
                }
                else
                {
                    cancelarEntradaSalida(session2, session2.Id_Emp, session2.Id_Cd_Ver, vFolio);                    
                    verificador = 1;
                }

                if (verificador == 1)
                {
                    Alerta("La solicitud se cancelo exitosamente");
                }
                else if (verificador == 2)
                {
                    Alerta("La solicitud se encuentra en estatus no valido para la cancelación");
                }
                else
                {
                    Alerta("Ocurrio un error al intentar cancelar la solicitud");
                }

                rg1.Rebind();

            }
            else
            {
                Alerta("La solicitud se encuentra en estatus no valido para la cancelación");
            }
        }

        private void cancelarEntradaSalida(Sesion pSession, int pIdEmp, int pIdCd, int pIdDevRem)
        {
            try
            {
                CN_CapDevolucionRemision cnDevRem = new CN_CapDevolucionRemision();
                CN_CapEntradaSalida cnEntSal = new CN_CapEntradaSalida();

                DevolucionRemision enDevRem = new DevolucionRemision();
                EntradaSalida enEntSal = new EntradaSalida();

                int vVerificador = 0;
                List<string> vEntradas = new List<string>();

                cnDevRem.ConsultaDetalle(ref enDevRem, pSession.Emp_Cnx, pIdEmp, pIdCd, pIdDevRem);

                enDevRem.ListDevolucionRemisionDet.Select(x => x.Id_Es)
                                                    .Distinct().ToList()
                                                    .ForEach(ent =>
                                                    {
                                                        cnEntSal.ConsultarEntradaSalida(pSession, pIdEmp, pIdCd, ent, enDevRem.Es_NatInv, ref enEntSal);

                                                        enEntSal.Es_Estatus = "B";
                                                        enEntSal.Id_U = pSession.Id_U;

                                                        List<EntradaSalidaDetalle> enEntSalDetList = new List<EntradaSalidaDetalle>();

                                                        cnEntSal.ConsultarEntradaSalidaDetalles(pSession, enEntSal, ref enEntSalDetList);

                                                        cnEntSal.BajaEntradaSalida(ref enEntSal, ref enEntSalDetList, pSession, ref vVerificador, 1, (enEntSal.Es_Naturaleza == 0), false);

                                                        vEntradas.Add(ent.ToString());
                                                    });

                cnDevRem.CancelaEntradas(pSession.Emp_Cnx, enDevRem.Id_Emp, enDevRem.Id_Cd, enDevRem.Id_DevRem);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cancelar las entradas.", ex);
            }                        
        }

        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                Sesion vSession = new Sesion();
                vSession = (Sesion)Session["Sesion" + Session.SessionID];

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = "";

                    DateTime remDate = Convert.ToDateTime(item["DevRem_Fecha"].Text);
                    DateTime sDate = vSession.CalendarioIni;
                    DateTime eDate = vSession.CalendarioFin;
                    string vEstatus = item["DevRem_Estatus"].Text.Trim();
                    
                    if (!(remDate >= sDate && remDate <= eDate) || vEstatus.Equals("B", StringComparison.OrdinalIgnoreCase))
                    {
                        TableCell cell = item["Eliminar"];
                        foreach (Control c in cell.Controls)
                        {
                            c.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemDataBound");
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
                        rg1.Rebind();
                        Session["ListaDevolucionRemisionesFactura"] = new List<Remision>();
                        Session["DetalleSaldo"] = new List<RemisionDet>();
                        Session["DetalleHistorico"] = new List<DevolucionRemisionDet>();  
                        break;
                    case "FacturacionVarialesSesionDestruir":                      
                        Session["ListaDevolucionRemisionesFactura" + Session.SessionID] = null;
                        Session["ListaDevolucionRemisionesFactura"] = new List<Remision>();
                        Session["DetalleSaldo" + Session.SessionID] = null;
                        Session["DetalleSaldo"] = new List<RemisionDet>();
                        Session["DetalleHistorico" + Session.SessionID] = null;
                        Session["DetalleHistorico"] = new List<DevolucionRemisionDet>();  

                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rg1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        

        #region Funciones
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

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                  /*  _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;*/

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");


              
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

        private void CargarEstatus()
        {
            cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));
            cmbEstatus.Items.Add(new RadComboBoxItem("Capturada", "C"));            
            cmbEstatus.Items.Add(new RadComboBoxItem("Cancelada", "B"));
        }

        private DataTable GetList()
        {
            try
            {
                List<DevolucionRemision> List = new List<DevolucionRemision>();
                CD_CapDevolucionRemision clsCN_CapDevolRem = new CD_CapDevolucionRemision();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DevolucionRemision DevolRemision = new DevolucionRemision();
                DevolRemision.Id_Emp = session2.Id_Emp;
                DevolRemision.Id_Cd = session2.Id_Cd_Ver;
                if (txtFolioIni.Text != "") DevolRemision.Folio1 = Convert.ToInt32(txtFolioIni.Text);
                if (txtFolioFin.Text != "") DevolRemision.Folio2 = Convert.ToInt32(txtFolioFin.Text);
                if (dpFechaIni.SelectedDate != null) DevolRemision.Fecha1 = dpFechaIni.SelectedDate;
                if (dpFechaFin.SelectedDate != null) DevolRemision.Fecha2 = Convert.ToDateTime(dpFechaFin.SelectedDate.Value.ToString("dd/MM/yyyy")).AddDays(1).AddSeconds(-1);
                DevolRemision.Estatus = cmbEstatus.SelectedValue;
                if (txtClienteIni.Text != "") DevolRemision.Id_CteFiltro1 = Convert.ToInt32(txtClienteIni.Text);
                if (txtClienteFin.Text != "") DevolRemision.Id_CteFiltro2 = Convert.ToInt32(txtClienteFin.Text);
                if (txtTipoId.Text != "") DevolRemision.Id_Tm = Convert.ToInt32(txtTipoId.Text);
               // if (string.IsNullOrEmpty(txtSolicitud.Text)) AutEntradaVirtual.Solicitud = Convert.ToInt32(txtSolicitud.Text);
                
                clsCN_CapDevolRem.Consulta_Lista(DevolRemision, session2.Emp_Cnx, ref List);
                dt = new DataTable();
                dt.Columns.Add("Id_Cd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dt.Columns.Add("NumEntradas", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_DevRem", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_U", System.Type.GetType("System.String"));
                dt.Columns.Add("U_Nombre", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dt.Columns.Add("DevRem_CteNombre", System.Type.GetType("System.String"));              
                dt.Columns.Add("Id_Fac", System.Type.GetType("System.Int32"));                
                dt.Columns.Add("DevRem_Fecha", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Id_Tm", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Tm_nombre", System.Type.GetType("System.String"));
                dt.Columns.Add("DevRem_Estatus", System.Type.GetType("System.String"));
                dt.Columns.Add("DevRem_EstatusStr", System.Type.GetType("System.String"));
                dt.Columns.Add("DevRem_Tipo", System.Type.GetType("System.String"));                
                dt.Clear();

                foreach (DevolucionRemision devolRemision in List)
                {
                    DataRow row = dt.NewRow();
                    row["Id_Emp"] = session2.Id_Emp;
                    row["Id_Cd"] = session2.Id_Cd;                    
                    row["Id_DevRem"] = devolRemision.Id_DevRem;
                    row["Id_U"] = devolRemision.Id_U;
                    row["U_Nombre"] = devolRemision.DevRem_UNombre;
                    row["Id_Cte"] = devolRemision.Id_Cte;
                    row["DevRem_CteNombre"] = devolRemision.DevRem_CteNombre;                 
                    row["Id_Fac"] = devolRemision.Id_Fac;
                    row["DevRem_Fecha"] = devolRemision.DevRem_Fecha;
                    row["Id_Tm"] = devolRemision.Id_Tm;
                    row["NumEntradas"] = devolRemision.NumEntradas;
                    row["Tm_nombre"] = devolRemision.DevRem_TmNombre;
                    row["DevRem_Estatus"] = devolRemision.Estatus;
                    row["DevRem_EstatusStr"] = devolRemision.EstatusStr;
                    row["DevRem_Tipo"] = devolRemision.DevRem_Tipo;

                    dt.Rows.Add(row);
                }
                
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PoblarTablaDT()
        {
            try
            { //poblar la tabla virtual con columnas vacias:
                dt = new DataTable();
                dt.Columns.Add("Id_Cd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dt.Columns.Add("NumEntradas", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_DevRem", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_U", System.Type.GetType("System.String"));
                dt.Columns.Add("U_Nombre", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dt.Columns.Add("DevRem_CteNombre", System.Type.GetType("System.String"));              
                dt.Columns.Add("Id_Fac", System.Type.GetType("System.Int32"));                
                dt.Columns.Add("DevRem_Fecha", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Id_Tm", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Tm_nombre", System.Type.GetType("System.String"));
                dt.Columns.Add("DevRem_Estatus", System.Type.GetType("System.String"));
                dt.Columns.Add("DevRem_EstatusStr", System.Type.GetType("System.String"));              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
              
        private void editar(GridDataItem gi)
        {
            try
            {
               /* if (_PermisoModificar)*/
                string script = string.Format("return AbrirVentana_DevolucionRemision('{0}')", gi["Id_DevRem"].Text);
                RAM1.ResponseScripts.Add(script);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }               

        #endregion

        #region Cancelación de Factura

        private bool cancelarFactura(int Id_Emp, int Id_Cd, int Id_Fac)
        {
            bool cancelado = false;

            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Factura factura = new Factura();
                factura.Id_Emp = Id_Emp;
                factura.Id_Cd = Id_Cd;
                factura.Id_Fac = Id_Fac;
                factura.Id_U = sesion.Id_U;
                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                EntradaSalida entSal = new EntradaSalida();
                List<EntradaSalidaDetalle> listaEntSal = new List<EntradaSalidaDetalle>();
                double importeTotalFactura = 0;
                double importeTotalFacturaIVA = 0;
                double importeTotalFactura_ProdNoDevolucion = 0;
                double importeTotalFacturaIVA_ProdNoDevolucion = 0;

                //Consultar factura
                new CN_CapFactura().ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                string RFC = string.Empty;
                string UUID = string.Empty;
                

                XmlDocument xmlBD = new XmlDocument();
                int TSATCANCELACION = 1;
                /*
                if (factura.Fac_Xml != null)
                {
                    xmlBD.LoadXml(factura.Fac_Xml.ToString());

                    foreach (XmlNode nodo in xmlBD.ChildNodes)
                    {
                        if (nodo.Name == "Comprobante")
                        {
                            TSATCANCELACION = 1;
                        }
                        else if (nodo.Name == "cfdi:Comprobante")
                        {
                            TSATCANCELACION = 2;
                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    XmlNode Nodo_nivel3;
                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                    UUID = Nodo_nivel3.Attributes["UUID"].Value;
                                }

                                if (Nodo_nivel2.Name == "cfdi:Emisor")
                                {
                                    RFC = Nodo_nivel2.Attributes["rfc"].Value;
                                }

                            }
                        }
                    }
                }
                */

                if (TSATCANCELACION == 2)
                {
                    /* string valorResultadoCancelacion = "0";
                     WS_CFDICancelacion.Service1 ws = new WS_CFDICancelacion.Service1();
                     ws.Url = ConfigurationManager.AppSettings["WS_CFDICancelacion"].ToString();
                     String respuestaCancelacion = ws.CancelacionWS("" + RFC + "," + UUID + "");
                     XmlDocument XmlCancelacion = new XmlDocument();
                     XmlCancelacion.LoadXml(respuestaCancelacion);


                     foreach (XmlNode nodo in XmlCancelacion.ChildNodes)
                     {
                         if (nodo.Name == "Acuse")
                         {
                             foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                             {
                                 if (Nodo_nivel2.Name == "Folios")
                                 {
                                     foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                     {
                                         if (Nodo_nivel3.Name == "EstatusUUID")
                                         {
                                             valorResultadoCancelacion = Nodo_nivel3.InnerText;
                                         }

                                     }

                                 }
                             }

                         }
                     }
                     string valorResultadoCancelacionTexto = string.Empty;
                     switch (valorResultadoCancelacion)
                     {
                         case "202":
                             valorResultadoCancelacionTexto = "Documento Previamente Cancelado";
                             break;
                         case "203":
                             valorResultadoCancelacionTexto = "Documento No corresponda al emisor";
                             break;
                         case "204":
                             valorResultadoCancelacionTexto = "Documento No Aplicable para cancelación";
                             break;
                         case "205":
                             valorResultadoCancelacionTexto = "Documento No Existe emisión";
                             break;
                         default:
                             valorResultadoCancelacionTexto = "No se hizo conexión con el servicio de cancelación";
                             break;
                     }

                     if (valorResultadoCancelacion != "201")
                     {
                         this.Alerta(valorResultadoCancelacionTexto);
                         return;
                     }
                     */
                }


                if (factura.Fac_Estatus != "B")
                {
                    //crear objeto entrada-salida y su detalle
                    foreach (FacturaDet facturaDet in listaFacturaDet)
                    {
                        if (factura.Id_Tm == 51)
                        {
                            CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                            int saldo = 0;
                            CN_CatProducto cn_producto = new CN_CatProducto();
                            Producto prod = new Producto();
                            cn_producto.ConsultaProducto(ref prod, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, facturaDet.Id_Prd);

                            if (((bool)prod.Prd_AparatoSisProp) && prod.Id_Ptp != 2)
                            {
                                CNentrada.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, facturaDet.Id_Prd.ToString(), facturaDet.Id_Ter.ToString(), factura.Id_Cte.ToString(), sesion.Emp_Cnx, ref saldo, "14");
                                /*
                                if (saldo - facturaDet.Fac_Cant < 0)
                                {
                                    Alerta("El producto " + facturaDet.Id_Prd.ToString() + " no cuenta con el saldo suficiente");
                                    return;
                                }*/
                            }
                        }

                        if (!facturaDet.Fac_Devolucion)
                        { //Crear item de lista de entrada-salida mov. 7
                            EntradaSalidaDetalle entSalDetalle = new EntradaSalidaDetalle();
                            entSalDetalle.Id_Emp = Id_Emp;
                            entSalDetalle.Id_Cd = Id_Cd;
                            entSalDetalle.Id_Es = 0;//se reasigna al insertar la entSal de encabezado
                            entSalDetalle.Id_EsDet = 0;//se reasigna al insertar la entSalDetalle
                            entSalDetalle.Id_Ter = facturaDet.Id_Ter;
                            entSalDetalle.Id_Prd = facturaDet.Id_Prd;
                            entSalDetalle.EsDet_Naturaleza = 0; //entrada
                            entSalDetalle.Es_Cantidad = facturaDet.Fac_Cant;
                            entSalDetalle.Es_Costo = facturaDet.Fac_Precio;
                            entSalDetalle.Es_BuenEstado = true;
                            entSalDetalle.Afct_OrdCompra = false;
                            listaEntSal.Add(entSalDetalle);
                            // ir sumando la cantidad de importe de factura por productos que no se les
                            // a aplicado una devolución
                            importeTotalFactura_ProdNoDevolucion += facturaDet.Fac_Importe;
                        }
                    }
                    /*
                     * Calcular cantidad de Iva en base al porcentaje que representa el importe de la factura a 
                     * cancelar calculado de los productos a los que no se ha aplicado una devolución
                     */
                    importeTotalFactura = factura.Fac_SubTotal != null ? Convert.ToSingle(factura.Fac_SubTotal) : 0;
                    importeTotalFacturaIVA = factura.Fac_ImporteIva != null ? Convert.ToSingle(factura.Fac_ImporteIva) : 0;
                    double porcentaje = 0;
                    if (importeTotalFactura > 0)
                        porcentaje = importeTotalFactura_ProdNoDevolucion / importeTotalFactura;
                    if (porcentaje > 0 && importeTotalFacturaIVA > 0)
                        importeTotalFacturaIVA_ProdNoDevolucion = importeTotalFacturaIVA * porcentaje;

                    CapaDatos.Funciones funciones = new CapaDatos.Funciones();

                    //llenar objeto de entrada-salida, movimiento 7 (cancelación de factura)
                    entSal.Id_Emp = Id_Emp;
                    entSal.Id_Cd = Id_Cd;
                    entSal.Id_U = sesion.Id_U;
                    entSal.Id_Tm = 8; //mov. de entrada por cancelacion de factura, el prod. vuvlve al almacén de la sucursal
                    entSal.Id_Cte = factura.Id_Cte;
                    entSal.Id_Pvd = -1;
                    entSal.Es_Naturaleza = 0;//entrada
                    entSal.Es_Fecha = funciones.GetLocalDateTime(sesion.Minutos);
                    entSal.Es_Referencia = string.Concat("Canc. F-", factura.Id_Fac.ToString());
                    entSal.Es_Notas = string.Concat("Movimiento automático generado por cancelación de factura ", factura.Id_Fac.ToString());
                    entSal.Es_SubTotal = importeTotalFactura_ProdNoDevolucion;
                    entSal.Es_Iva = importeTotalFacturaIVA_ProdNoDevolucion;
                    entSal.Es_Total = importeTotalFactura_ProdNoDevolucion + importeTotalFacturaIVA_ProdNoDevolucion;
                    entSal.Es_Estatus = "I";
                    int verificador = 0;
                    new CN_CapFactura().EliminarFactura(ref factura, sesion.Emp_Cnx, ref verificador, ref entSal, ref listaEntSal);//, ref notaCredito, ref listaNotaCreditoDetalle);
                }

                ImprimirFactura(sesion.Id_Emp, sesion.Id_Cd, factura.Id_Fac, "CANCELACION", string.Concat("Canc. F-", factura.Id_Fac.ToString()), false);

                cancelado = true;                
            }
            catch (Exception e)
            {
                cancelado = false;
                throw e;
            }

            return cancelado;
        }

        private void ImprimirFactura(int Id_Emp, int Id_Cd, int Id_Fac, string movimiento, string agregado_nota_cancelacion, bool tienePDF = false)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                int verificador = 0;

                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                CN_CapFactura cn_factura = new CN_CapFactura();
                Factura factura = new Factura();
                Factura facturaNacional = new Factura();
                factura.Id_Emp = sesion.Id_Emp;
                factura.Id_Cd = sesion.Id_Cd_Ver;
                factura.Id_Fac = Id_Fac;

                facturaNacional.Id_Emp = sesion.Id_Emp;
                facturaNacional.Id_Cd = sesion.Id_Cd_Ver;
                facturaNacional.Id_Fac = Id_Fac;

                cn_factura.ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                cn_factura.ConsultaFacturaNacional(ref facturaNacional, sesion.Emp_Cnx);

                // Validar si la Remisión es Válida o no en base a la suma de los montos en las partidas de la remisión y la remisión especial.
                bool bDocumentoValido = false;
                new CN_CapFactura().ValidaMontosImpresion(factura, sesion.Id_Cd_Ver, sesion.Id_Emp, 2, sesion.Emp_Cnx, ref bDocumentoValido);

                if (bDocumentoValido)
                {
                    List<AdendaDet> listCabT = new List<AdendaDet>();
                    List<AdendaDet> listDetT = new List<AdendaDet>();
                    List<AdendaDet> listCabR = new List<AdendaDet>();
                    List<AdendaDet> listDetR = new List<AdendaDet>();
                    List<AdendaDet> listCabNacionalT = new List<AdendaDet>();
                    List<AdendaDet> listDetNacionalT = new List<AdendaDet>();
                    new CN_CapFactura().ConsultarAdenda(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                    new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);
                    new CN_CapFactura().ConsultarAdendaNacional(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabNacionalT, ref listDetNacionalT, sesion.Emp_Cnx);

                    // -------------------------------------------------------------------------------------------
                    // Consulta productos de factura especial de la tabla 'CapFacturaEspecialDet' si esque la factura especial existe
                    // esto es si es una actualización de factura --> si el parametro Folio trae un Id de factura
                    // -------------------------------------------------------------------------------------------
                    List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
                    new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                        , sesion.Emp_Cnx
                        , Id_Emp
                        , Id_Cd
                        , Id_Fac
                        , factura.Id_Cte);
                    // -------------------------------------------------------------------------------------------

                    #region variable XML a enviar
                    StringBuilder XML_Enviar = new StringBuilder();
                    XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    XML_Enviar.Append("<Comprobante");
                    XML_Enviar.Append(" serie=\"\"");
                    XML_Enviar.Append(" folio=\"\"");
                    XML_Enviar.Append(" fecha=\"\"");
                    XML_Enviar.Append(" formaDePago=\"\"");
                    XML_Enviar.Append(" subTotal=\"\"");
                    XML_Enviar.Append(" total=\"\"");

                    XML_Enviar.Append(" tipoDeComprobante=\"\"");
                    XML_Enviar.Append(" Sustituye=\"\"");
                    XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CARGO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION NOTA DE CARGO
                    XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                    XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                    XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                    XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CARGO
                    XML_Enviar.Append(" ConceptoDescuento1=\"\"");
                    XML_Enviar.Append(" TasaDescuento1=\"\"");
                    XML_Enviar.Append(" ConceptoDescuento2=\"\"");
                    XML_Enviar.Append(" TasaDescuento2=\"\"");
                    XML_Enviar.Append(" Notas=\"\"");
                    XML_Enviar.Append(" Correo=\"\"");
                    XML_Enviar.Append(" CliNum=\"\"");

                    XML_Enviar.Append(" MetodoPago=\"\"");
                    XML_Enviar.Append(" CuentaBancaria=\"\"");
                    XML_Enviar.Append(" Referencia=\"\"");
                    XML_Enviar.Append(">");
                    XML_Enviar.Append(" <Emisor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" numero=\"\" />");
                    XML_Enviar.Append(" <Receptor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" nombre=\"\">");
                    XML_Enviar.Append(" <Domicilio");
                    XML_Enviar.Append(" calle=\"\"");
                    XML_Enviar.Append(" noExterior=\"\"");
                    XML_Enviar.Append(" noInterior=\"\"");
                    XML_Enviar.Append(" colonia=\"\"");
                    XML_Enviar.Append(" municipio=\"\"");
                    XML_Enviar.Append(" estado=\"\"");
                    XML_Enviar.Append(" pais=\"\"");
                    XML_Enviar.Append(" codigoPostal=\"\" />");
                    XML_Enviar.Append(" </Receptor>");
                    XML_Enviar.Append(" <Conceptos>");
                    XML_Enviar.Append(" <Concepto");
                    XML_Enviar.Append(" cantidad=\"\"");
                    XML_Enviar.Append(" noIdentificacion=\"\"");
                    XML_Enviar.Append(" descripcion=\"\"");
                    XML_Enviar.Append(" valorUnitario=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Conceptos>");
                    XML_Enviar.Append(" <Impuestos");
                    XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                    XML_Enviar.Append(" <Traslados>");
                    XML_Enviar.Append(" <Traslado");
                    XML_Enviar.Append(" impuesto=\"\"");
                    XML_Enviar.Append(" tasa=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Traslados>");

                    if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                    {
                        XML_Enviar.Append(" <Retenidos>");
                        XML_Enviar.Append(" <Retenido");
                        XML_Enviar.Append(" importe=\"\"");
                        XML_Enviar.Append(" impuesto=\"\" />");
                        XML_Enviar.Append(" </Retenidos>");
                    }
                    XML_Enviar.Append(" </Impuestos>");

                    XML_Enviar.Append(" <Addenda>");

                    //ADENDA CABECERA
                    XML_Enviar.Append(" <cabecera");
                    XML_Enviar.Append(" Pedido=\"\"");
                    XML_Enviar.Append(" Requisicion=\"\"");
                    XML_Enviar.Append(" consignarRenglon1=\"\"");
                    XML_Enviar.Append(" consignarRenglon2=\"\"");
                    XML_Enviar.Append(" consignarRenglon3=\"\"");
                    XML_Enviar.Append(" consignarRenglon4=\"\"");
                    XML_Enviar.Append(" consignarRenglon5=\"\"");
                    XML_Enviar.Append(" Conducto=\"\"");
                    XML_Enviar.Append(" CondicionesPago=\"\"");
                    XML_Enviar.Append(" NumeroGuia=\"\"");
                    XML_Enviar.Append(" ControlPedido=\"\"");
                    XML_Enviar.Append(" OrdenEmbarque=\"\"");
                    XML_Enviar.Append(" Zona=\"\"");
                    XML_Enviar.Append(" Territorio=\"\"");
                    XML_Enviar.Append(" Agente=\"\"");
                    XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                    XML_Enviar.Append(" Formulo=\"\"");
                    XML_Enviar.Append(" Autorizo=\"\"");

                    XML_Enviar.Append(" NombreAddenda=\"\"");
                    foreach (AdendaDet det in listCabT)
                    {
                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    }
                    foreach (AdendaDet det in listCabR)
                    {
                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    }
                    XML_Enviar.Append("/>");




                    //ADENDA DETALLE
                    if (listaProdFacturaEspecialFinal.Count > 0)
                    {
                        foreach (FacturaDet fd in listaProdFacturaEspecialFinal)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                            string primerNodo = "";
                            int primerfila = 0;
                            foreach (AdendaDet det in listDetT)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            primerNodo = "";
                            primerfila = 0;
                            foreach (AdendaDet det in listDetR)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            XML_Enviar.Append("/>");
                        }
                    }
                    else
                    {
                        //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                        //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                            string primerNodo = "";
                            int primerfila = 0;
                            foreach (AdendaDet det in listDetT)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            primerNodo = "";
                            primerfila = 0;
                            foreach (AdendaDet det in listDetR)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            XML_Enviar.Append("/>");
                        }

                    }
                    XML_Enviar.Append(" </Addenda>");
                    if (facturaNacional != null)
                    {
                        if (movimiento != "CANCELACION")
                        {
                            //COMPROBANTE NACIONAL
                            XML_Enviar.Append(" <ComprobanteCN");
                            XML_Enviar.Append(" CliNum=\"\"");
                            XML_Enviar.Append(">");
                            XML_Enviar.Append(" <Conceptos>");
                            XML_Enviar.Append(" <Concepto");
                            XML_Enviar.Append(" cantidad=\"\"");
                            XML_Enviar.Append(" noIdentificacion=\"\"");
                            XML_Enviar.Append(" descripcion=\"\"");
                            XML_Enviar.Append(" valorUnitario=\"\"");
                            XML_Enviar.Append(" importe=\"\" />");
                            XML_Enviar.Append(" </Conceptos>");

                            //ADENDA NACIONAL
                            XML_Enviar.Append(" <AddendaCN>");

                            //ADENDA NACIONAL CABECERA
                            XML_Enviar.Append(" <CabeceraCN");
                            XML_Enviar.Append(" Pedido=\"\"");
                            XML_Enviar.Append(" Requisicion=\"\"");
                            XML_Enviar.Append(" consignarRenglon1=\"\"");
                            XML_Enviar.Append(" consignarRenglon2=\"\"");
                            XML_Enviar.Append(" consignarRenglon3=\"\"");
                            XML_Enviar.Append(" consignarRenglon4=\"\"");
                            XML_Enviar.Append(" consignarRenglon5=\"\"");
                            XML_Enviar.Append(" Conducto=\"\"");
                            XML_Enviar.Append(" CondicionesPago=\"\"");
                            XML_Enviar.Append(" NumeroGuia=\"\"");
                            XML_Enviar.Append(" ControlPedido=\"\"");
                            XML_Enviar.Append(" OrdenEmbarque=\"\"");
                            XML_Enviar.Append(" Zona=\"\"");
                            XML_Enviar.Append(" Territorio=\"\"");
                            XML_Enviar.Append(" Agente=\"\"");
                            XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                            XML_Enviar.Append(" Formulo=\"\"");
                            XML_Enviar.Append(" Autorizo=\"\"");

                            XML_Enviar.Append(" NombreAddenda=\"\"");
                            foreach (AdendaDet det in listCabNacionalT)
                            {
                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                            }
                            XML_Enviar.Append("/>");


                            //ADENDA NACIONAL DETALLE

                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            foreach (FacturaDet fd in listaFacturaDet)
                            {
                                XML_Enviar.Append(" <Detalle");
                                XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                                string primerNodo = "";
                                int primerfila = 0;
                                foreach (AdendaDet det in listDetNacionalT)
                                {

                                    if (fd.Id_Prd == det.Id_Prd)
                                    {
                                        if (primerfila == 0)
                                        { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                            primerNodo = det.Nodo;
                                        }
                                        if (primerfila > 0 && det.Nodo == primerNodo)
                                        {
                                            XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                            // ABRIMOS UNA NUEVA ADENDA
                                            XML_Enviar.Append(" <Detalle");
                                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                        }

                                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        primerfila++;
                                    }
                                }

                                XML_Enviar.Append("/>");
                            }

                            XML_Enviar.Append(" </AddendaCN>");

                            XML_Enviar.Append(" </ComprobanteCN>");
                        }
                        else
                        {
                            XML_Enviar.Append("<ComprobanteCN UUID=\"" + factura.Fac_FolioFiscalCN + "\" Folio=\"" + factura.Fac_FolioCN.ToString() + "\" Serie=\"" + factura.Serie + "\" />");
                            facturaNacional = null;
                        }
                    }
                    XML_Enviar.Append(" </Comprobante>");

                    //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                    //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA

                    //foreach (FacturaDet fd in listaFacturaDet)
                    //{
                    //    XML_Enviar.Append(" <Detalle");
                    //    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                    //    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                    //    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\""); 
                    //    foreach (AdendaDet det in listDetT)
                    //    {
                    //        if (fd.Id_Prd == det.Id_Prd)
                    //        {
                    //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    //        }
                    //    }
                    //    foreach (AdendaDet det in listDetR)
                    //    {
                    //        if (fd.Id_Prd == det.Id_Prd)
                    //        {
                    //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    //        }
                    //    }
                    //    XML_Enviar.Append("/>");
                    //}






                    #endregion

                    #region Codigo pruebas

                    //PruebaServicio.Service1 servicio = new PruebaServicio.Service1();
                    //float suma = servicio.Suma(Convert.ToSingle(txtNumero1.Text), Convert.ToSingle(txtNumero2.Text));
                    //this.Alerta(suma.ToString());

                    //Uri objURI = new Uri("");
                    //WebRequest objWebRequest = WebRequest.Create(objURI);
                    //WebResponse objWebResponse = objWebRequest.GetResponse();
                    //Stream objStream = objWebResponse.GetResponseStream();
                    //StreamReader objStreamReader = new StreamReader(objStream);
                    //string responseText = objStreamReader.ReadToEnd();

                    #endregion

                    // --------------------------------------
                    // Consulta centro de distribución
                    // --------------------------------------
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    // --------------------------------------------------------------------
                    // Consulta detalle de factura para generar lista de productos
                    // --------------------------------------------------------------------
                    //if (factura.Fac_Sello != "" && factura.Fac_Sello != null && movimiento == "FACTURA")
                    //{
                    //    //Abre el XML y carga el PDF de la factura
                    //    object resultado = null;
                    //    cn_factura.ConsultarFacturaSAT(ref factura, sesion.Emp_Cnx, ref resultado);
                    //    byte[] archivoPdf = (byte[])resultado;
                    //    if (archivoPdf.Length > 0)
                    //    {
                    //        string tempPDFname = string.Concat("FACTURA_"
                    //                 , factura.Id_Emp.ToString()
                    //                 , "_", factura.Id_Cd.ToString()
                    //                 , "_", factura.Id_U.ToString()
                    //                 , ".pdf");
                    //        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    //        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                    //        this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    //        // ------------------------------------------------------------------------------------------------
                    //        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    //        // ------------------------------------------------------------------------------------------------

                    //        RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                    //    }
                    //    else
                    //        this.DisplayMensajeAlerta("TempPDFNoData");
                    //}
                    //else
                    //{
                    // --------------------------------------
                    // cargar xml de factura que se envia a SAT
                    // --------------------------------------
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XML_Enviar.ToString());

                    // --------------------------------------//
                    // --------------------------------------//
                    //         LLENAR DATOS DEL XML          //
                    // --------------------------------------//
                    // --------------------------------------//
                    #region Llenar datos factura a Enviar
                    //encabezado
                    XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = factura.Id_Emp;
                    cliente.Id_Cd = factura.Id_Cd;
                    cliente.Id_Cte = factura.Id_Cte;
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    Comprobante.Attributes["serie"].Value = factura.Serie;
                    Comprobante.Attributes["folio"].Value = factura.Folio_cancelacion > 0 ? factura.Folio_cancelacion.ToString() : factura.Id_Fac.ToString();
                    Comprobante.Attributes["fecha"].Value = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", factura.Fac_FechaHr);
                    Comprobante.Attributes["formaDePago"].Value = "PAGO EN UNA SOLA EXHIBICION";
                    Comprobante.Attributes["subTotal"].Value = factura.Fac_SubTotal == null ? "0" : Math.Round((double)factura.Fac_SubTotal, 2).ToString();
                    Comprobante.Attributes["total"].Value = (Math.Round((double)factura.Fac_SubTotal, 2) + Math.Round((double)factura.Fac_ImporteIva, 2)).ToString();
                    Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                    Comprobante.Attributes["Sustituye"].Value = factura.Fac_Refactura;
                    Comprobante.Attributes["tipoMovimiento"].Value = movimiento;
                    Comprobante.Attributes["tipoMoneda"].Value = factura.Mon_Unidad;
                    Comprobante.Attributes["tipoCambio"].Value = factura.Mon_TipCambio.ToString();
                    Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                    Comprobante.Attributes["movimientoacancelar"].Value = ""; //

                    Comprobante.Attributes["ConceptoDescuento1"].Value = factura.Fac_Desc1;
                    Comprobante.Attributes["TasaDescuento1"].Value = factura.Fac_DescPorcen1 == null ? string.Empty : factura.Fac_DescPorcen1.ToString();
                    Comprobante.Attributes["ConceptoDescuento2"].Value = factura.Fac_Desc2;
                    Comprobante.Attributes["TasaDescuento2"].Value = factura.Fac_DescPorcen2 == null ? string.Empty : factura.Fac_DescPorcen2.ToString();
                    Comprobante.Attributes["Correo"].Value = factura.Cte_Email;
                    Comprobante.Attributes["CliNum"].Value = factura.Id_Cte.ToString();

                    Comprobante.Attributes["MetodoPago"].Value = FormaPagoNombre(factura.Fac_FPago);
                    Comprobante.Attributes["CuentaBancaria"].Value = factura.Fac_UDigitos.ToString();
                    Comprobante.Attributes["Referencia"].Value = cliente.Cte_Referencia;

                    XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                    Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                    Emisor.Attributes["numero"].Value = Cd.Id_Cd.ToString();

                    //receptor
                    XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                    Receptor.Attributes["rfc"].Value = factura.Fac_CteRfc;
                    Receptor.Attributes["nombre"].Value = factura.Cte_NomComercial;

                    //Domicilio
                    XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                    Domicilio.Attributes["calle"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacCalle); // factura.Fac_CteCalle.Replace("\"", "");
                    Domicilio.Attributes["noExterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumero);// factura.Fac_CteNumero;
                    Domicilio.Attributes["noInterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumeroInterior);// factura.Fac_CteNumero;
                    Domicilio.Attributes["colonia"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacColonia);// factura.Fac_CteColonia;
                    Domicilio.Attributes["municipio"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacMunicipio);// factura.Fac_CteMunicipio;
                    Domicilio.Attributes["estado"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacEstado);// factura.Fac_CteEstado;
                    Domicilio.Attributes["pais"].Value = "México";
                    Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;// factura.Fac_CteCp;
                    // ---------------------
                    // Conceptos --> partidas = producto
                    // Detalle --> productoDetalle
                    // ---------------------         
                    XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                    XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                    XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");

                    XmlNode ComprobanteCN = Comprobante.SelectNodes("ComprobanteCN").Count > 0 ? Comprobante.SelectSingleNode("ComprobanteCN") : null;
                    XmlNode AddendaCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("AddendaCN") : null;
                    XmlNode ConceptosCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("Conceptos") : null;
                    XmlNode productoCN = ConceptosCN != null ? ConceptosCN.SelectSingleNode("Concepto") : null;

                    if (facturaNacional != null)
                    {
                        ComprobanteCN.Attributes["CliNum"].Value = facturaNacional != null ? facturaNacional.Id_Cte.ToString() : "0";
                    }


                    //Si existe una factura especial, en los nodos de conceptos del producto se pone
                    //los productos de la factura especial
                    //si no, se pone los datos de productos de la factura original
                    StringBuilder NotaProductosOriginales = new StringBuilder();
                    if (listaProdFacturaEspecialFinal.Count > 0)
                    {
                        foreach (FacturaDet facturaDet in listaProdFacturaEspecialFinal)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = facturaDet.Producto.Id_PrdEsp;
                            prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                            prd.Attributes["cantidad"].Value = facturaDet.Fac_CantE.ToString();
                            prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_ImporteE, 2).ToString();
                            producto.ParentNode.AppendChild(prd);
                        }

                        foreach (FacturaDet facturaDet in listaFacturaDet)
                        {
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(facturaDet.Id_Prd.ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(Math.Round(facturaDet.Fac_Precio, 2).ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(facturaDet.Fac_Cant.ToString());
                        }
                    }
                    else
                    {
                        foreach (FacturaDet facturaDet in listaFacturaDet)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                            prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                            prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                            prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                            producto.ParentNode.AppendChild(prd);


                            if (facturaNacional != null)
                            {
                                XmlNode prdCN = productoCN.Clone();
                                prdCN.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                                prdCN.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                                prdCN.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                                prdCN.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                                prdCN.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                                productoCN.ParentNode.AppendChild(prdCN);
                            }
                        }
                    }
                    producto.ParentNode.RemoveChild(xml.SelectNodes("//Concepto").Item(0));

                    if (facturaNacional != null)
                    {
                        productoCN.ParentNode.RemoveChild(xml.SelectNodes("//ComprobanteCN//Conceptos//Concepto").Item(0));
                    }


                    //Impuestos
                    XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                    Impuestos.Attributes["totalImpuestosTrasladados"].Value = factura.Fac_ImporteIva == null ? "0" : factura.Fac_ImporteIva.ToString();

                    //Traslado (impuestos desgloce)
                    XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                    XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                    Traslado.Attributes["impuesto"].Value = "IVA";
                    if (cliente.BPorcientoIVA == true)
                        Traslado.Attributes["tasa"].Value = cliente.PorcientoIVA.ToString();
                    else
                        Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();
                    Traslado.Attributes["importe"].Value = factura.Fac_ImporteIva == null ? "0" : Math.Round((double)factura.Fac_ImporteIva, 2).ToString();

                    if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                    {
                        XmlNode Retenidos = Impuestos.SelectSingleNode("Retenidos");
                        XmlNode Retenido = Retenidos.SelectSingleNode("Retenido");
                        Retenido.Attributes["importe"].Value = factura.Fac_ImporteRetencion == null ? "0" : Math.Round((double)factura.Fac_ImporteRetencion, 2).ToString();
                        Retenido.Attributes["impuesto"].Value = "IVA";
                    }

                    //Addenda
                    XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                    cabecera.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabecera.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    //consulta datos cliente                 
                    cabecera.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                    cabecera.Attributes["consignarRenglon2"].Value = string.Concat(factura.Fac_CteCalle.Replace("\"", ""), " ", factura.Fac_CteNumero);
                    cabecera.Attributes["consignarRenglon3"].Value = factura.Fac_CteColonia;
                    cabecera.Attributes["consignarRenglon4"].Value = string.Concat(factura.Fac_CteMunicipio, " ", factura.Fac_CteEstado, " ", factura.Fac_CteCp);
                    cabecera.Attributes["consignarRenglon5"].Value = "México";
                    cabecera.Attributes["Conducto"].Value = factura.Fac_Conducto;
                    cabecera.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                    cabecera.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                    cabecera.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabecera.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                    cabecera.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                    cabecera.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                    cabecera.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                    cabecera.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    cabecera.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                    cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                    cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;


                    //Addenda Nacional
                    if (facturaNacional != null)
                    {
                        XmlNode cabeceraCN = AddendaCN.SelectSingleNode("CabeceraCN");
                        cabeceraCN.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                        cabeceraCN.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                        //consulta datos cliente                 
                        cabeceraCN.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                        cabeceraCN.Attributes["consignarRenglon2"].Value = string.Concat(facturaNacional.Fac_CteCalle.Replace("\"", ""), " ", facturaNacional.Fac_CteNumero);
                        cabeceraCN.Attributes["consignarRenglon3"].Value = facturaNacional.Fac_CteColonia;
                        cabeceraCN.Attributes["consignarRenglon4"].Value = string.Concat(facturaNacional.Fac_CteMunicipio, " ", facturaNacional.Fac_CteEstado, " ", facturaNacional.Fac_CteCp).Replace('É', 'E');
                        cabeceraCN.Attributes["consignarRenglon5"].Value = "Mexico";
                        cabeceraCN.Attributes["Conducto"].Value = factura.Fac_Conducto;
                        cabeceraCN.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                        cabeceraCN.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                        cabeceraCN.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                        cabeceraCN.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                        cabeceraCN.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                        cabeceraCN.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                        cabeceraCN.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                        cabeceraCN.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                        cabeceraCN.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                        cabeceraCN.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                        cabeceraCN.Attributes["NombreAddenda"].Value = facturaNacional.Fac_CteAdeNombre;//cliente.Ade_Nombre;
                    }


                    Factura factura_remision = new Factura();
                    factura_remision.Id_Emp = factura.Id_Emp;
                    factura_remision.Id_Cd = factura.Id_Cd;
                    factura_remision.Id_Fac = factura.Id_Fac;
                    string agregado_nota = "";
                    cn_factura.FacturaRemision_Nota(factura_remision, sesion.Emp_Cnx, ref agregado_nota);
                    StringBuilder NotaCompleta = new StringBuilder();

                    NotaCompleta.Append(agregado_nota + "//");
                    NotaCompleta.Append(NotaProductosOriginales.ToString() + "//");
                    NotaCompleta.Append(factura.Fac_Notas + "//");
                    NotaCompleta.Append(agregado_nota_cancelacion);
                    Comprobante.Attributes["Notas"].Value = NotaCompleta.ToString();

                    /*
                    if (!ValidaImpresionFactura(xml)) 
                    {
                        Alerta("No se puede Imprimir Documento: Detalle de factura no coincide con total, Revise factura");
                        return;
                    
                    }*/

                    #endregion
                    // --------------------------------------
                    // convertir XML a string
                    // --------------------------------------
                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    xml.WriteTo(tx);
                    string xmlString = sw.ToString();
                    // ------------------------------------------------------   
                    // ENVIAR XML al servicio de la aplicacion de KEY
                    // -------- ----------------------------------------------
                    XmlDocument xmlSAT = new XmlDocument();

                    int TSAT = 1;

                    XmlDocument xmlBD = new XmlDocument();

                    if (factura.Fac_Xml != null && factura.Fac_Xml != "")
                    {
                        xmlBD.LoadXml(factura.Fac_Xml.ToString());

                        foreach (XmlNode nodo in xmlBD.ChildNodes)
                        {
                            if (nodo.Name == "Comprobante")
                            {
                                TSAT = 1;
                            }
                            else if (nodo.Name == "cfdi:Comprobante")
                            {
                                TSAT = 2;

                            }
                        }
                    }


                    //sian_cfd.Service1 sianFacturacionElectronica = new sian_cfd.Service1();

                    if (TSAT == 2 && tienePDF)
                    {
                        descargarPDF(Id_Fac);
                        return;
                    }

                    WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                    sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                    object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                    if (movimiento == "CANCELACION")
                    {
                        string XmLRegex = string.Empty;
                        XmLRegex = Regex.Replace(sianFacturacionElectronicaResult.ToString(), @"(?s)(?<=<cfdi:Addenda>).+?(?=</cfdi:Addenda>)", "");
                        XmLRegex = XmLRegex.Replace("<cfdi:Addenda>", "");
                        XmLRegex = XmLRegex.Replace("</cfdi:Addenda>", "");
                        xmlSAT.LoadXml(XmLRegex);
                    }
                    else
                    {
                        xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                    }





                    //*********************************************//
                    //* Procesar XML recibido de servicio de SAT  *//
                    //*********************************************//
                    string stringPDF = string.Empty;
                    string stringPDFCN = string.Empty;
                    string selloSAT = string.Empty;
                    string selloSATCN = string.Empty;
                    string folioFiscal = string.Empty;
                    string folioFiscalCN = string.Empty;
                    string errorNum = string.Empty;
                    string errorText = string.Empty;
                    string errorNumCN = string.Empty;
                    string errorTextCN = string.Empty;

                    TSAT = 1;

                    foreach (XmlNode nodoSistemaFacturacion in xmlSAT.ChildNodes)
                    {
                        if (nodoSistemaFacturacion.Name == "Comprobante")
                        {
                            TSAT = 1;
                            selloSAT = nodoSistemaFacturacion.Attributes["sello"].Value;

                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                                }


                            }
                        }
                        else if (nodoSistemaFacturacion.Name == "cfdi:Comprobante")
                        {
                            TSAT = 2;
                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                                }

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "tfd:TimbreFiscalDigital")
                                        {
                                            selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                            folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                        }
                                    }

                                }

                            }

                        }
                        if (nodoSistemaFacturacion.Name == "SistemaFacturacion")
                        {
                            foreach (XmlNode nodoXmlSAT in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (nodoXmlSAT.Name == "ComprobanteCDIK")
                                {
                                    foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                    {
                                        if (nodo.Name == "Comprobante")
                                        {
                                            TSAT = 1;
                                            selloSAT = nodo.Attributes["sello"].Value;

                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }


                                            }
                                        }
                                        else if (nodo.Name == "cfdi:Comprobante")
                                        {
                                            TSAT = 2;
                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }

                                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                {
                                                    XmlNode Nodo_nivel3;
                                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                                    selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                                    folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                                }

                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    if (nodoXmlSAT.Name == "ComprobanteKSL")
                                    {
                                        foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                        {
                                            if (nodo.Name == "Comprobante")
                                            {
                                                TSAT = 1;
                                                selloSATCN = nodo.Attributes["sello"].Value;

                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            if (Nodo_nivel3.Name == "ERROR")
                                                            {
                                                                errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                                errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                            }
                                                        }

                                                        nodo.RemoveChild(Nodo_nivel2);
                                                    }


                                                }
                                            }
                                            else if (nodo.Name == "cfdi:Comprobante")
                                            {
                                                TSAT = 2;
                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            if (Nodo_nivel3.Name == "ERROR")
                                                            {
                                                                errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                                errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                            }
                                                        }

                                                        nodo.RemoveChild(Nodo_nivel2);
                                                    }

                                                    if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                    {
                                                        XmlNode Nodo_nivel3;
                                                        Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                                        selloSATCN = Nodo_nivel3.Attributes["selloSAT"].Value;
                                                        folioFiscalCN = Nodo_nivel3.Attributes["UUID"].Value;
                                                    }

                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }

                    }



                    if (errorNum != "0")
                    {
                        this.Alerta(string.Concat(errorText.Replace("'", "\"")));

                        /* factura.Fac_Sello = selloSAT;
                         System.Data.SqlTypes.SqlXml sqlXml
                             = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                         factura.Fac_Xml = sqlXml;
                         factura.Fac_Pdf = this.Base64ToByte(stringPDF);

                         verificador = 0;

                         new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);*/
                    }
                    else
                    {
                        //ComprobanteSAT.RemoveChild(AddendaSAT);

                        if ((facturaNacional != null) && (errorNumCN != "0"))
                        {
                            this.Alerta(string.Concat(errorTextCN.Replace("'", "\"")));
                        }
                        else
                        {
                            factura.Fac_Sello = selloSAT;
                            factura.Fac_SelloCN = selloSATCN;

                            System.Data.SqlTypes.SqlXml sqlXml;
                            System.Data.SqlTypes.SqlXml sqlXmlCN;

                            if (xmlSAT.SelectNodes("SistemaFacturacion").Count > 0)
                            {
                                //sqlXml = sqlXml.Value.Replace("ComprobanteCDIK", "").;
                                sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteCDIK").OuterXml.Replace("<ComprobanteCDIK>", "").Replace("</ComprobanteCDIK>", ""), XmlNodeType.Document, null));
                                sqlXmlCN = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").OuterXml.Replace("<ComprobanteKSL>", "").Replace("</ComprobanteKSL>", ""), XmlNodeType.Document, null));
                                factura.Fac_FolioCN = int.Parse(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["folio"].Value == string.Empty ? "0" : xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["folio"].Value);
                            }
                            else
                            {
                                sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                                sqlXmlCN = null;
                                factura.Fac_FolioCN = null;
                            }


                            if (movimiento != "CANCELACION")
                            {

                                factura.Fac_Xml = sqlXml;
                                factura.Fac_XmlCN = sqlXmlCN;
                                factura.Fac_FolioFiscal = folioFiscal;
                                factura.Fac_FolioFiscalCN = folioFiscalCN;
                            }

                            factura.Fac_Pdf = this.Base64ToByte(stringPDF);
                            factura.Fac_PdfCN = this.Base64ToByte(stringPDFCN);

                            #region reporte factura


                            #endregion

                            // ---------------------------------------------------------------------------------------------
                            // Se actualiza el estatus de la factura a Impreso (I)
                            // ---------------------------------------------------------------------------------------------
                            if (movimiento != "CANCELACION")
                            {
                                factura.Fac_Estatus = "I";
                                new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);
                            }
                            else
                            {
                                factura.Fac_Estatus = "B";
                            }
                            verificador = 0;


                            // -----------------------
                            // Abrir PDF de factura
                            // -----------------------
                            string tempPDFname = string.Concat("FACTURA_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_Fac.ToString(), ".pdf");
                            string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                            string tempPDFCNname = string.Concat("FACTURACN_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_Fac.ToString(), ".pdf");
                            string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                            string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);

                            this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                            if (facturaNacional != null)
                            {
                                this.ByteToTempPDF(URLtempPDFCN, this.Base64ToByte(stringPDFCN));
                                // ------------------------------------------------------------------------------------------------
                                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                                // ------------------------------------------------------------------------------------------------
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','", WebURLtempPDFCN, "')"));
                            }
                            else
                            {
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','')"));
                            }
                        }
                        //}
                    }
                }
                else
                {
                    RAM1.ResponseScripts.Add("OpenAlert('Los montos de la Factura y la Factura Especial no coinciden','" + Id_Emp + "','" + Id_Cd + "','" + Id_Fac + "','" + 1 + "')");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    filebytes = Convert.FromBase64String(data);
                }
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
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
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        private string FormaPagoNombre(string Id_Fpa)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatFormaPago cncatformapago = new CN_CatFormaPago();
                FormaPago fpago = new FormaPago();
                fpago.Id_Emp = sesion.Id_Emp;
                if (Id_Fpa != "")
                {
                    fpago.Id_Fpa = Convert.ToInt32(Id_Fpa == "" ? "1" : Id_Fpa);
                    cncatformapago.ConsultaFormaPago(ref fpago, sesion.Emp_Cnx);
                }
                else
                {
                    fpago.Descripcion = "";
                }
                return fpago.Descripcion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void descargarPDF(int Id_Fac)
        {
            object resultado = null;
            object resultadoCN = null;
            Factura fac = new Factura();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            fac.Id_Emp = Sesion.Id_Emp;
            fac.Id_Cd = Sesion.Id_Cd_Ver;
            fac.Id_Fac = Id_Fac;
            CN_CapFactura factura = new CN_CapFactura();
            factura.ConsultarFacturaSAT(ref fac, Sesion.Emp_Cnx, ref resultado, ref resultadoCN);
            byte[] archivoPdf = (byte[])resultado;
            byte[] archivoPdfCN = resultadoCN != System.DBNull.Value ? (byte[])resultadoCN : new byte[0];
            if (archivoPdf.Length > 0)
            {
                string tempPDFname = string.Concat("FACTURA_"
                         , Sesion.Id_Emp.ToString()
                         , "_", Sesion.Id_Cd.ToString()
                         , "_", Id_Fac.ToString()
                         , ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, archivoPdf);

                if (archivoPdfCN.Length > 0)
                {
                    string tempPDFCNname = string.Concat("FACTURACN_", Sesion.Id_Emp.ToString(), "_", Sesion.Id_Cd.ToString(), "_", Id_Fac.ToString(), ".pdf");
                    string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                    string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);

                    this.ByteToTempPDF(URLtempPDFCN, archivoPdfCN);

                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','" + WebURLtempPDFCN + "')"));
                }
                else
                {
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','')"));
                }
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
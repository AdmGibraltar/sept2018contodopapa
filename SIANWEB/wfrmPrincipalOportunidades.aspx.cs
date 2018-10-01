using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Data;

namespace SIANWEB
{
    public partial class wfrmPrincipaloportunidades : System.Web.UI.Page
    {
        #region Variables
        private static bool _PermisoGuardar;
        private static bool _PermisoModificar;
        private static bool _PermisoEliminar;
        private static bool _PermisoImprimir;
        public List<CrmPromociones> list = new List<CrmPromociones>();
        public double sum = 0;
        public double a = 0;
        public double p = 0;
        public double n = 0;
        public double c = 0;
        public double x = 0;
        public int avance = 0;
        public int valorCancelacion = 0;
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;
            }
        }
        private int cliente = 0;
        public int Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public int refrescar
        {
            get
            {
                int ret = Session["refrescarOp" + Session.SessionID] != null ? (int)Session["refrescarOp" + Session.SessionID] : 1;
                Session["refrescarOp" + Session.SessionID] = 2;
                return ret;

            }
            set { Session["refrescarOp" + Session.SessionID] = value; }
        }
        #endregion

        #region Eventos
        //protected override void OnLoad(EventArgs e)
        //{

        //    if (!ValidarSesion())
        //    {
        //        Response.Redirect("login.aspx", false);
        //    }
        //    else if (Page.IsPostBack)
        //    {
        //        //NO HACE NADA
        //    }
        //    else if (Session["refreshPage" + Session.SessionID] == null || (bool)Session["refreshPage" + Session.SessionID])
        //    {
        //        //string Url = "";
        //        //string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
        //        //if (pag.Length > 1)
        //        //    Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
        //        //else
        //        //    Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
        //        Session["refreshPage" + Session.SessionID] = false;

        //        //Response.Redirect(Url);
        //    }
        //    else
        //    {
        //        Session["refreshPage" + Session.SessionID] = true;
        //        Page_Load(null, null);
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                parametros();
                if (ValidarSesion())
                    if (!Page.IsPostBack)
                    {
                        //session.Id_U = 108;
                        ValidarPermisos();
                        if (session.Cu_Modif_Pass_Voluntario == false)
                            return;
                        //Inicializar();
                        //dgControlPromocion.Rebind();
                        //Totales(list);
                        eliminar();

                        if (session.Id_TU != 2)
                            ibtnNuevaOportunidad.Visible = false;

                        //if (Session["refreshPage" + Session.SessionID] == null || (bool)Session["refreshPage" + Session.SessionID])
                        //{
                        //    Session["refreshPage" + Session.SessionID] = false;
                        RadAjaxManager1.ResponseScripts.Add("refreshGrid();");
                        //}
                        //else
                        //{
                        //    Session["refreshPage" + Session.SessionID] = true;
                        //}
                    }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.Item is GridDataItem)
                {//solo falta campo de busqueda en filtro clientes                          
                    GridDataItem dataItem = (GridDataItem)e.Item;
                    Totales(list);
                    string link = "wfrmDetalleCliente.aspx?" +
                           "ID=" + ((dataItem["Id_Cte"].Text).ToString()) +
                           "&Seg=" + ((dataItem["Segmento"].Text).ToString()) +
                           "&Ter=" + ((dataItem["Id_Ter"].Text).ToString());
                    (dataItem.FindControl("lnkNombre") as LinkButton).PostBackUrl = link;

                    string link2 = "wfrmSeguimientoOportunidad.aspx?" +
                           "id=" + ((dataItem["Ids"].Text).ToString()) +
                           "&cd=" + ((dataItem["Cds"].Text).ToString());
                    (dataItem.FindControl("lnkID") as LinkButton).PostBackUrl = link2;
                }
                if (e.Item is GridPagerItem)
                    eliminar();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void rg1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {//botones del grid
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Delete")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);
                        Label LblId = new Label();
                        LblId = (Label)this.dgControlPromocion.Items[item].Cells[0].FindControl("lblOp");
                        int promocion = !string.IsNullOrEmpty(LblId.Text) ? Convert.ToInt32(LblId.Text) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void dgControlPromocion_SortCommand(object source, GridSortCommandEventArgs e)
        {
            try
            {
                dgControlPromocion.Rebind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    list = GetList();
                    dgControlPromocion.DataSource = list;
                    parametros();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rg1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {//paginador
            try
            {
                ErrorManager();
                dgControlPromocion.Rebind();
                if (IsPostBack)
                {
                    Totales(list);
                    eliminar();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlCDS_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int representante = 0;
            cdsCambio();
            int cds = !string.IsNullOrEmpty(ddlCDS.SelectedValue) ? Convert.ToInt32(ddlCDS.SelectedValue) : 0;
            CargarRepresentante(cds, ref representante);
            ddlRik.SelectedValue = representante.ToString();
            CargarUEN();

        }
        protected void ddlRik_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //int uen = 0;
            //int segmento = 0;
            //int area = 0;
            //int solucion = 0;
            //int aplicacion = 0;
            //int cds = !string.IsNullOrEmpty(ddlCDS.SelectedValue) ? Convert.ToInt32(ddlCDS.SelectedValue) : 0;
            //int representante = !string.IsNullOrEmpty(ddlRik.SelectedValue) ? Convert.ToInt32(ddlRik.SelectedValue) : 0;
            CargarUEN();
            //ddlUENS.SelectedValue = uen.ToString();
            //CargarSegmentos(uen, ref segmento);
            //ddlSegmento.SelectedValue = segmento.ToString();
            //CargarTerritorio(cds);
            ////filtro2
            //CargarAreas(segmento, ref area);
            //ddlArea.SelectedValue = area.ToString();
            //CargarSolucion(area, ref solucion);
            //ddlSolucion.SelectedValue = solucion.ToString();
            //CargarAplicacion(solucion, ref aplicacion);
            //ddlAplicacion.SelectedValue = aplicacion.ToString();
            ////dgControlPromocion.Rebind();
        }
        protected void ddlUENS_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //int segmento = 0;
            //int area = 0;
            //int solucion = 0;
            //int aplicacion = 0;
            //int cds = !string.IsNullOrEmpty(ddlCDS.SelectedValue) ? Convert.ToInt32(ddlCDS.SelectedValue) : 0;
            //int representante = !string.IsNullOrEmpty(ddlRik.SelectedValue) ? Convert.ToInt32(ddlRik.SelectedValue) : 0;
            //int uen = !string.IsNullOrEmpty(ddlUENS.SelectedValue) ? Convert.ToInt32(ddlUENS.SelectedValue) : 0;
            //CargarSegmentos(uen, ref segmento);
            //ddlSegmento.SelectedValue = segmento.ToString();
            //CargarTerritorio(cds);
            ////filtro2
            //CargarAreas(segmento, ref area);
            //ddlArea.SelectedValue = area.ToString();
            //CargarSolucion(area, ref solucion);
            //ddlSolucion.SelectedValue = solucion.ToString();
            //CargarAplicacion(solucion, ref aplicacion);
            //ddlAplicacion.SelectedValue = aplicacion.ToString();
            //dgControlPromocion.Rebind();
            CargarSegmentos();
        }
        protected void ddlSegmento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //int area = 0;
            //int solucion = 0;
            //int aplicacion = 0;
            //int cds = !string.IsNullOrEmpty(ddlCDS.SelectedValue) ? Convert.ToInt32(ddlCDS.SelectedValue) : 0;
            //int representante = !string.IsNullOrEmpty(ddlRik.SelectedValue) ? Convert.ToInt32(ddlRik.SelectedValue) : 0;
            //int uen = !string.IsNullOrEmpty(ddlUENS.SelectedValue) ? Convert.ToInt32(ddlUENS.SelectedValue) : 0;
            //int segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
            //CargarTerritorio(cds);
            ////filtro2
            //CargarAreas(segmento, ref area);
            //ddlArea.SelectedValue = area.ToString();
            //CargarSolucion(area, ref solucion);
            //ddlSolucion.SelectedValue = solucion.ToString();
            //CargarAplicacion(solucion, ref aplicacion);
            //ddlAplicacion.SelectedValue = aplicacion.ToString();
            //// dgControlPromocion.Rebind();
            txtCliente.Text = "";
            ibtnBuscarCliente.ImageUrl = "img/lupa.jpg";
            HiddenField1.Value = "";
            CargarTerritorio();
            CargarAreas();
        }
        protected void ddlTerritorio_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtCliente.Text = "";
            ibtnBuscarCliente.ImageUrl = "img/lupa.jpg";
            HiddenField1.Value = "";

            CargarAreas();
        }
        protected void ddlArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //int solucion = 0;
            //int aplicacion = 0;
            //int area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
            //CargarSolucion(area, ref solucion);
            //ddlSolucion.SelectedValue = solucion.ToString();
            //CargarAplicacion(solucion, ref aplicacion);
            //ddlAplicacion.SelectedValue = aplicacion.ToString();
            ////dgControlPromocion.Rebind();
            CargarSolucion();
        }
        protected void ddlSolucion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //int aplicacion = 0;
            //int area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
            //int solucion = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : 0;
            //CargarAplicacion(solucion, ref aplicacion);
            //ddlAplicacion.SelectedValue = aplicacion.ToString();
            //dgControlPromocion.Rebind();
            CargarAplicacion();
        }
        protected void ddlAplicacion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //dgControlPromocion.Rebind();
        }
        protected void ddlEstatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // dgControlPromocion.Rebind();
        }
        protected void ibtnFiltro_Click(object sender, EventArgs e)
        {//imagen filtrar
            try
            {
                dgControlPromocion.Rebind();
                Totales(list);
                MensajeFiltro();
                eliminar();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ibtnBuscarCliente_Click(object sender, ImageClickEventArgs e)
        {//imagen buscar
            try
            {
                if (ddlTerritorio.SelectedIndex == 0)
                {
                    lblMensajeFiltro.Text = "Antes de seleccionar el cliente, debe especificar el territorio";
                    return;
                }
                lblMensajeFiltro.Text = "";
                if (HiddenField1.Value == "")
                {
                    int cte = !string.IsNullOrEmpty(HiddenField1.Value) ? Convert.ToInt32(HiddenField1.Value) : 0;
                    int ter = !string.IsNullOrEmpty(ddlTerritorio.SelectedValue) ? Convert.ToInt32(ddlTerritorio.SelectedValue) : 0;
                    int uen = !string.IsNullOrEmpty(ddlUENS.SelectedValue) ? Convert.ToInt32(ddlUENS.SelectedValue) : 0;
                    int rik = !string.IsNullOrEmpty(ddlRik.SelectedValue) ? Convert.ToInt32(ddlRik.SelectedValue) : 0;
                    int seg = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;//solo saltillo
                    RadAjaxManager1.ResponseScripts.Add("return AbrirVentana(" + cte + "," + ter + "," + uen + "," + rik + "," + seg + ");");
                }
                else
                {
                    HiddenField1.Value = "";
                    txtCliente.Text = "";
                    txtCliente.Enabled = true;
                    ibtnBuscarCliente.ImageUrl = "img/lupa.jpg";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void txtNoCliente_TextChanged(object sender, EventArgs e)
        {
            clientes();
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            Inicializar();
            dgControlPromocion.Rebind();
            Totales(list);
        }
        #endregion

        #region Funciones
        private void parametros()
        {
            try
            {
                if (Request.Params["__EVENTTARGET"] == "opcionA")
                {
                    if (Session["NumCliente" + Session.SessionID] != null)
                        if (!string.IsNullOrEmpty(Session["NumCliente" + Session.SessionID].ToString()))
                        {
                            int.TryParse(Session["NumCliente" + Session.SessionID].ToString(), out cliente);
                            //txtNoCliente.Text = cliente.ToString();
                            txtCliente.Text = Session["NombreCliente" + Session.SessionID].ToString();
                            HiddenField1.Value = cliente.ToString();
                            //RadNumericTextBox btn = new RadNumericTextBox();
                            //btn.TextChanged += new EventHandler(txtNoCliente_TextChanged);       
                            txtCliente.Enabled = false;
                            ibtnBuscarCliente.ImageUrl = "img/quitar5.png";
                        }
                    dgControlPromocion.Rebind();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ValidarPermisos()
        {
            try
            {
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla
                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);
                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
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
                int cds = 0;
                int representante = 0;
                //filtro1                
                CargarCDS(ref cds);
                if (Session["CDS" + Session.SessionID] != null)
                {
                    if (!string.IsNullOrEmpty(Session["CDS" + Session.SessionID].ToString()))
                    {
                        ddlCDS.SelectedValue = Session["CDS" + Session.SessionID].ToString();
                        CargarRepresentante(Convert.ToInt32(ddlCDS.SelectedValue), ref representante);
                    }
                }
                else
                    if (session.Id_TU == 2)
                    {
                        ddlCDS.SelectedValue = session.Id_Cd_Ver.ToString();
                        CargarRepresentante(cds, ref representante);
                    }
                    else
                    {
                        ddlCDS.SelectedValue = cds.ToString();
                        CargarRepresentante(cds, ref representante);
                    }
                if (session.Id_TU == 2)
                    ddlRik.SelectedValue = session.Id_Rik.ToString();

                string tipoUsuario = string.Empty;
                CN_CrmOportunidad oportunidad = new CN_CrmOportunidad();
                oportunidad.tipoUsuario(session, ref tipoUsuario);
                tipoUsuario = tipoUsuario.ToLower();
                if (session.Id_TU == 1)
                {
                    ddlCDS.Enabled = true;
                    ddlRik.Enabled = true;
                    ddlCDS.Visible = true;
                    ddlRik.Visible = true;
                    labelcd.Visible = true;
                    labelrik.Visible = true;
                }
                else
                {
                    if (tipoUsuario.Contains("gerente"))
                    {
                        ddlRik.Enabled = true;
                        ddlRik.Visible = true;
                        labelrik.Visible = true;
                    }
                    else
                    {
                        ddlCDS.Enabled = false;
                        ddlRik.Enabled = false;
                        ddlCDS.Visible = false;
                        ddlRik.Visible = false;
                        labelcd.Visible = false;
                        labelrik.Visible = false;
                    }
                }
                CargarUEN();
                // dgControlPromocion.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void clientes()
        {
            try
            {
                int cliente = 0;
                if (Session["NumCliente" + Session.SessionID] != null)
                    if (!string.IsNullOrEmpty(Session["NumCliente" + Session.SessionID].ToString()))
                    {
                        int.TryParse(Session["NumCliente" + Session.SessionID].ToString(), out cliente);
                        HiddenField1.Value = cliente.ToString();
                        Session["NumCliente" + Session.SessionID] = null;
                    }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Totales(List<CrmPromociones> list)
        {
            try
            {
                sum = 0;
                a = 0;
                p = 0;
                n = 0;
                c = 0;
                x = 0;
                avance = 0;
                int totalPagina = dgControlPromocion.PageCount;
                int registroPagina = dgControlPromocion.PageSize;
                int pagina = dgControlPromocion.CurrentPageIndex;
                int totalRegistros = list.Count;
                int valorInicial = (pagina) * registroPagina;
                int valorFinal = (pagina + 1) * registroPagina;
                if (valorFinal > totalRegistros)
                    valorFinal = totalRegistros;

                foreach (CrmPromociones cp in list)
                {
                    double venta = cp.Cli_VPObservado;
                    sum += venta;
                    if (cp.Estatus == 1)//total analisis
                        a += venta;
                    if (cp.Estatus == 2)//total promocion
                        p += venta;
                    if (cp.Estatus == 3)//total negociacion
                        n += venta;
                    if (cp.Estatus == 4)//total cierre
                        c += venta;
                    if (cp.Estatus == 5)//total cancelacion                   
                        x += venta;
                    avance += cp.MesModificacion != "--" ? Convert.ToInt32(cp.MesModificacion) : 0;//total avance  
                }
                //for (int i = valorInicial; i < valorFinal; i++)
                //{
                //    double venta = list[i].Cli_VPObservado;
                //    sum += venta;
                //    if (list[i].Avances == 1)//total analisis
                //        a += venta;
                //    if (list[i].Avances == 2)//total promocion
                //        p += venta;
                //    if (list[i].Avances == 3)//total negociacion
                //        n += venta;
                //    if (list[i].Avances == 4)//total cierre
                //        c += venta;
                //    if (list[i].Avances == 5)//total cancelacion                   
                //        x += venta;
                //    avance += list[i].Avances;//total avance  
                //}

                txtTot.Text = sum.ToString("C");
                txtA.Text = a.ToString("C");
                txtP.Text = p.ToString("C");
                txtN.Text = n.ToString("C");
                txtC.Text = c.ToString("C");
                txtX.Text = x.ToString("C");
                txtAvance.Text = avance.ToString();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void eliminar()
        {
            //try
            //{
            //    if (ddlEstatus.SelectedValue == "5" || ddlEstatus.SelectedValue == "-1")
            //    {
            //        if (dgControlPromocion.Items.Count > 0)
            //            for (int i = 0; i < dgControlPromocion.Items.Count; i++)
            //            {
            //                if (dgControlPromocion.Items[i]["Avances"].Text == "5")
            //                {
            //                    Image img = new Image();
            //                    img = (Image)dgControlPromocion.Items[i].FindControl("btnCancelacion");
            //                    img.ImageUrl = "img/x.gif";
            //                }
            //                else
            //                {
            //                    Image img = new Image();
            //                    img = (Image)dgControlPromocion.Items[i].FindControl("btnCancelacion");
            //                    img.ToolTip = "";
            //                }
            //            }
            //    }
            //    else
            //        if (dgControlPromocion.Items.Count > 0)
            //            for (int i = 0; i < dgControlPromocion.Items.Count; i++)
            //            {
            //                Image img = new Image();
            //                img = (Image)dgControlPromocion.Items[i].FindControl("btnCancelacion");
            //                img.ToolTip = "";
            //            }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
        private void MensajeFiltro()
        {
            try
            {
                string criterio = string.Empty;
                string etapas = string.Empty;
                //lblCriterios
                CrmPromociones promocion = new CrmPromociones();
                promocion.Cds = !string.IsNullOrEmpty(ddlCDS.SelectedValue) ? Convert.ToInt32(ddlCDS.SelectedValue) : session.Id_Cd_Ver;
                if (promocion.Cds == -1)
                    promocion.Cds = session.Id_Cd_Ver;
                promocion.Representante = !string.IsNullOrEmpty(ddlRik.SelectedValue) ? Convert.ToInt32(ddlRik.SelectedValue) : 0;
                promocion.Uen = !string.IsNullOrEmpty(ddlUENS.SelectedValue) ? Convert.ToInt32(ddlUENS.SelectedValue) : 0;
                promocion.Segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                promocion.Territorio = !string.IsNullOrEmpty(ddlTerritorio.SelectedValue) ? Convert.ToInt32(ddlTerritorio.SelectedValue) : 0;
                //filtro2
                promocion.Area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
                promocion.Solucion = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : 0;
                promocion.Aplicacion = !string.IsNullOrEmpty(ddlAplicacion.SelectedValue) ? Convert.ToInt32(ddlAplicacion.SelectedValue) : 0;
                promocion.Estatus = !string.IsNullOrEmpty(ddlEstatus.SelectedValue) ? Convert.ToInt32(ddlEstatus.SelectedValue) : -1;

                criterio = "<b>Filtros especificados:</b>  ";

                criterio = "<b>Representante:</b> " + ddlRik.SelectedItem.Text.Replace("-", "");

                if (promocion.Uen > 0)
                    criterio += "<b> / UEN:</b> " + ddlUENS.SelectedItem.Text;
                if (promocion.Segmento > 0)
                    criterio += "<b> / Segmento:</b> " + ddlSegmento.SelectedItem.Text;
                if (promocion.Territorio > 0)
                    criterio += "<b> / Territorio:</b> " + ddlTerritorio.SelectedItem.Text;
                if (promocion.Area > 0)
                    criterio += "<b> / Área:</b> " + ddlArea.SelectedItem.Text;
                if (promocion.Solucion > 0)
                    criterio += "<b> / Solución:</b> " + ddlSolucion.SelectedItem.Text;
                if (promocion.Aplicacion > 0)
                    criterio += "<b> / Aplicación:</b> " + ddlAplicacion.SelectedItem.Text;
                lblCriterios.Text = criterio;
                if (promocion.Estatus > 0)
                    lblEtapa.Text = "<b> / Etapa de los proyectos:</b> " + ddlEstatus.SelectedItem.Text;
                else
                    lblEtapa.Text = "";
            }
            catch (Exception)
            {
                throw;
            }
        }
        //filtro 1
        private void CargarCDS(ref int cds)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmPromociones> list = new List<CrmPromociones>();
                CN_CrmPromocion clscrmCat = new CN_CrmPromocion();
                clscrmCat.ComboCds(sesion, ref list);
                ddlCDS.Items.Clear();
                if (list.Count > 0)
                {
                    cds = list[0].Id;
                    for (int i = 0; i < list.Count; i++)
                        ddlCDS.Items.Add(new RadComboBoxItem(list[i].Descripcion, list[i].Id.ToString()));
                    ddlCDS.SelectedValue = cds.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRepresentante(int CDS, ref int valorRepresentante)
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, session.Id_Cd_Ver, session.Id_U, session.Emp_Cnx, "spCatRik_Combo", ref ddlRik);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                ddlRik.Items.Remove(0);

                RadComboBoxItem rcb = new RadComboBoxItem();
                rcb.Value = "-1";
                rcb.Text = "-- Todos --";
                ddlRik.Items.Insert(0, rcb);
                ddlRik.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUEN()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, session.Id_U, session.Emp_Cnx, "spCatRikUen_Combo", ref ddlUENS);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                ddlUENS.Items.Remove(0);

                RadComboBoxItem rcb = new RadComboBoxItem();
                rcb.Value = "-1";
                rcb.Text = "-- Todos --";
                ddlUENS.Items.Insert(0, rcb);
                ddlUENS.SelectedIndex = 0;
                CargarSegmentos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSegmentos()
        {
            try
            {
                if (ddlUENS.SelectedValue == "")
                    ddlUENS.SelectedIndex = 0;

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaComboUEN(1, session.Id_Emp, Convert.ToInt32(ddlUENS.SelectedValue), Convert.ToInt32(session.Id_U), session.Emp_Cnx, "spCatSegmentosUen_ComboCRM", ref ddlSegmento, session.Id_Cd_Ver);
                ddlSegmento.Items.Remove(0);

                RadComboBoxItem rcb = new RadComboBoxItem();
                rcb.Value = "-1";
                rcb.Text = "-- Todos --";
                ddlSegmento.Items.Insert(0, rcb);
                ddlSegmento.SelectedIndex = 0;
                CargarTerritorio();
                CargarAreas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorio()
        {
            try
            {
                if (ddlSegmento.SelectedValue == "")
                    ddlSegmento.SelectedIndex = 0;

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaComboCRM(session.Id_Emp, session.Id_Cd, Convert.ToInt32(ddlSegmento.SelectedValue), ddlRik.SelectedIndex == 0 ? (int?)null : Convert.ToInt32(ddlRik.SelectedValue), session.Emp_Cnx, "spCatTerritorioSegmento_ComboCRM", ref ddlTerritorio, session.Id_Cd_Ver);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAreas()
        {
            try
            {
                if (ddlSegmento.SelectedValue == "")
                {
                    ddlSegmento.SelectedIndex = 0;
                }

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlSegmento.SelectedValue), session.Emp_Cnx, "spCatAreaSegmento_Combo", ref ddlArea);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                ddlArea.Items.Remove(0);

                RadComboBoxItem rcb = new RadComboBoxItem();
                rcb.Value = "-1";
                rcb.Text = "-- Todos --";
                ddlArea.Items.Insert(0, rcb);
                ddlArea.SelectedIndex = 0;
                this.CargarSolucion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSolucion()
        {
            try
            {
                if (ddlArea.SelectedValue == "")
                    ddlArea.SelectedIndex = 0;

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlArea.SelectedValue), session.Emp_Cnx, "spCatSolucionArea_Combo", ref ddlSolucion);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                ddlSolucion.Items.Remove(0);

                RadComboBoxItem rcb = new RadComboBoxItem();
                rcb.Value = "-1";
                rcb.Text = "-- Todos --";
                ddlSolucion.Items.Insert(0, rcb);
                ddlSolucion.SelectedIndex = 0;
                CargarAplicacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAplicacion()
        {
            try
            {
                if (ddlSolucion.SelectedValue == "")
                    ddlSolucion.SelectedIndex = 0;

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlSolucion.SelectedValue), session.Emp_Cnx, "spCatAplicacionSolucion_Combo", ref ddlAplicacion);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                ddlAplicacion.Items.Remove(0);

                RadComboBoxItem rcb = new RadComboBoxItem();
                rcb.Value = "-1";
                rcb.Text = "-- Todos --";
                ddlAplicacion.Items.Insert(0, rcb);
                ddlAplicacion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
        private List<CrmPromociones> GetList()
        {
            try
            {
                CrmPromociones promocion = new CrmPromociones();
                //filtro1
                promocion.Cds = ddlCDS.Visible ? Convert.ToInt32(ddlCDS.SelectedValue) : session.Id_Cd_Ver;
                if (promocion.Cds == -1)
                    promocion.Cds = session.Id_Cd_Ver;

                promocion.Representante = session.Id_U; 
                promocion.Uen = !string.IsNullOrEmpty(ddlUENS.SelectedValue) ? Convert.ToInt32(ddlUENS.SelectedValue) : 0;
                promocion.Segmento = !string.IsNullOrEmpty(ddlSegmento.SelectedValue) ? Convert.ToInt32(ddlSegmento.SelectedValue) : 0;
                promocion.Territorio = !string.IsNullOrEmpty(ddlTerritorio.SelectedValue) ? Convert.ToInt32(ddlTerritorio.SelectedValue) : 0;
                //filtro2
                promocion.Area = !string.IsNullOrEmpty(ddlArea.SelectedValue) ? Convert.ToInt32(ddlArea.SelectedValue) : 0;
                promocion.Solucion = !string.IsNullOrEmpty(ddlSolucion.SelectedValue) ? Convert.ToInt32(ddlSolucion.SelectedValue) : 0;
                promocion.Aplicacion = !string.IsNullOrEmpty(ddlAplicacion.SelectedValue) ? Convert.ToInt32(ddlAplicacion.SelectedValue) : 0;
                promocion.Estatus = !string.IsNullOrEmpty(ddlEstatus.SelectedValue) ? Convert.ToInt32(ddlEstatus.SelectedValue) : -1;
                promocion.Cliente = !string.IsNullOrEmpty(HiddenField1.Value) ? Convert.ToInt32(HiddenField1.Value) : ((Request.Form["HiddenField1"] != null) ? (!string.IsNullOrEmpty(Request.Form["HiddenField1"].ToString()) ? Convert.ToInt32(Request.Form["HiddenField1"].ToString()) : 0) : 0);
                promocion.Id_Rik = ddlRik.SelectedValue;
                Session["CDS" + Session.SessionID] = promocion.Cds;
                Session["RIK"] = promocion.Representante;
                Session["UEN"] = promocion.Uen;
                Session["SEG"] = promocion.Segmento;
                Session["TER"] = promocion.Territorio;
                Session["AREA"] = promocion.Area;
                Session["SOL"] = promocion.Solucion;
                Session["APLIC"] = promocion.Aplicacion;
                Session["STAT"] = promocion.Estatus;

                List<CrmPromociones> List = new List<CrmPromociones>();
                CN_CrmPromocion cls = new CN_CrmPromocion();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                cls.ConsultaCatPromocion(session2, promocion, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cdsCambio()
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
                comun.CambiarCdVer(ddlCDS.SelectedItem, ref sesion);
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
                RadAjaxManager1.ResponseScripts.Add("alert('" + mensaje + "', 330, 150);");
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
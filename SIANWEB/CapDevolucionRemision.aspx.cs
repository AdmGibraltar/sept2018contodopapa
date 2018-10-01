using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Xml;
using System.Xml.Linq;



namespace SIANWEB
{
    public partial class CapDevolucionRemision : PaginaBase
    {
        public void ChkDetalleRemisionHdr_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            IEnumerable<GridDataItem> gridItems;

            gridItems = rgDevParcial.MasterTableView.Items
                                                   .OfType<GridDataItem>();

            foreach (GridDataItem gi in gridItems)
            {
                CheckBox cbItem = ((CheckBox)(gi["BDetalleRemision"].FindControl("ChkDetalleRemision")));
                cbItem.Checked = cb.Checked;
            }
        }

        public void grDetalleDevoluciones_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid rd = sender as RadGrid;

            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rd.DataSource = List_Historico;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private bool _PermisoGuardar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private List<RemisionDet> List_Saldo
        {
            get { return (List<RemisionDet>)Session["DetalleSaldo" + Session.SessionID]; }
            set { Session["DetalleSaldo" + Session.SessionID] = value; }
        }


        private List<DevolucionRemisionDet> List_Historico
        {
            get { return (List<DevolucionRemisionDet>)Session["DetalleHistorico" + Session.SessionID]; }
            set { Session["DetalleHistorico" + Session.SessionID] = value; }
        }

        private List<Remision> ListaRemisionesFactura
        {
            get { return (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID]; }
            set { Session["ListaDevolucionRemisionesFactura" + Session.SessionID] = value; }
        }

        public DataTable dt { get; set; }


        protected override void OnInit(EventArgs e)
        {
            this.GlobalRAM = RAM1;
            this.LabelMensaje = lblMensaje;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //this.ValidarPermisos();
                    if (gSession.Cu_Modif_Pass_Voluntario == false)
                    {
                        RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        return;
                    }

                    CargarEstatus();
                    CargarTipoMovimiento();
                    CargarComboTiposGarantias();

                    string Modulo = Request.QueryString["Modulo"] == null ? "1" : Request.QueryString["Modulo"];
                    hiddenId.Value = Request.QueryString["Id_Folio"] == null ? "-1" : Request.QueryString["Id_Folio"];

                    if (Modulo == "1")
                    {
                        BtnFacturar.Visible = false;
                    }
                    else
                    {
                        BtnDevolucion.Visible = false;

                    }

                    if (hiddenId.Value != "-1")
                    {
                        hiddenId.Value = Request.QueryString["Id_Folio"];

                        this.Inicializar(Convert.ToInt32(hiddenId.Value));
                        List_Historico = GetListDetalleHistorico();
                        DeshabilitarControles();

                        List_Saldo = GetListSaldo();
                        rgDevParcial.Rebind();
                        grDetalleDevoluciones.Rebind();

                        RadTabStrip1.Tabs[3].Visible = false;
                        RadTabStrip1.Tabs[2].Enabled = true;
                        RadTabStrip1.Tabs[1].Enabled = true;

                    }
                    else
                    {
                        txtSolicitud.Enabled = false;
                        txtSolicitud.Text = MaximoId("CapDevolucionRemision", "Id_DevRem");
                        RadTabStrip1.Tabs[3].Visible = true;
                        RadTabStrip1.Tabs[3].Enabled = true;
                        RadTabStrip1.Tabs[2].Visible = false;
                        RadTabStrip1.Tabs[1].Enabled = true;
                        dpFecha1.DbSelectedDate = DateTime.Now;
                    }

                    double ancho = 0;
                    foreach (GridColumn gc in rgDetalleMov.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    rgDetalleMov.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgDetalleMov.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgDetalleMov.Rebind();
                    Random randObj = new Random(DateTime.Now.Millisecond);
                    HF_ClvPag.Value = randObj.Next().ToString();

                    RadPageViewDGenerales.Selected = true;
                    RadTabStrip1.Tabs[0].Selected = true;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        private void DeshabilitarControles()
        {
            txtSolicitud.Enabled = false;
            dpFecha1.Enabled = false;
            cmbEstatus.Enabled = false;
            cmbTipoMov.Enabled = false;
            txtTipoId.Enabled = false;
            txtNumCliente.Enabled = false;
            txtCliente.Enabled = false;
            txtTerritorio.Enabled = false;
            cmbTerritorio.Enabled = false;
            RblRealizar.Enabled = false;
            rgDetalleMov.Enabled = false;
            BtnDetalleRemisiones.Enabled = false;
            rgDevParcial.Enabled = false;
        }

        protected void BtnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                int vCantidadT = 0;
                int cantidadRemision = 0;

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<Remision> lRemisiones = new List<Remision>();
                if (txtNumCliente.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Cliente");
                    return;
                }

                if (txtTerritorio.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Territorio");
                    return;
                }

                if (txtTipoId.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Tipo de Movimiento");
                    return;
                }

                List<RemisionDet> lcompare = new List<RemisionDet>();

                lcompare = GetListSaldo();
                bool flagDif = false;
                if (rgDevParcial.MasterTableView.Items.Count != lcompare.Count())
                {
                    flagDif = true;
                }

                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    foreach (RemisionDet compare in lcompare)
                    {
                        if (Convert.ToInt32(item["Id_Prd"].Text) == compare.Id_Prd)
                        {
                            if (Convert.ToInt32(item["Rem_Cant"].Text) != compare.Rem_Cant)
                            {
                                flagDif = true;
                            }
                        }
                    }
                }

                if (flagDif)
                {
                    Alerta("El Saldo ha sido actualizado debido a que otro usuario esta realizando una transacción en este momento para este cliente y tipo de movimiento, es necesario volver a capturar la devolución");
                    List_Saldo = GetListSaldo();
                    rgDevParcial.Rebind();
                    return;

                }

                bool ProductosSeleccionados = false;

                IEnumerable<GridDataItem> gridItems = rgDetalleRemision.MasterTableView.Items.Cast<GridDataItem>().Where(x =>
                                                           ((RadNumericTextBox)x.FindControl("NumCantRemisionDevuelta")).Text.Trim() != "" &&
                                                           ((RadNumericTextBox)x.FindControl("NumCantRemisionDevuelta")).Text.Trim() != "0");

                gridItems.ToList().ForEach(gi =>
                {
                    vCantidadT = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("NumCantRemisionDevuelta")).Text);
                    cantidadRemision = Convert.ToInt32(gi["Rem_Cant"].Text);

                    Remision remision = new Remision();
                    remision.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                    remision.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    remision.Cte_NomComercial = txtCliente.Text;
                    remision.Id_Prd = Convert.ToInt32(gi["Id_Prd"].Text);
                    remision.Id_Tm = Convert.ToInt32(txtTipoId.Text);
                    remision.Cant = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("NumCantDevuelta")).Text.Trim());
                    lRemisiones.Add(remision);

                    if (vCantidadT > cantidadRemision)
                    {
                        flagDif = true;
                    }
                });

                ProductosSeleccionados = gridItems.Any();

                if (!ProductosSeleccionados)
                {
                    Alerta("Es necesario agregar la cantidad a facturar en al menos un producto");
                    return;
                }

                if (flagDif)
                {
                    Alerta("La cantidad debe ser menor a la de la remisión.");
                    return;
                }

                ListaRemisionesFactura = lRemisiones;
                Session["DevolucionRemision" + Session.SessionID] = true;
                if (this.cmbTipoGarantia.SelectedValue != "")
                    Session["Id_TG_Fac" + Session.SessionID] = Convert.ToInt32(this.cmbTipoGarantia.SelectedValue);
                string PermisoGuardar = "1";
                string PermisoModificar = "1";
                string PermisoEliminar = "1";
                string PermisoImprimir = "1";

                RAM1.ResponseScripts.Add("return CloseWindowRem()");

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }

        protected IEnumerable<RemisionesVencidas> ObtieneListaRemisionesProducto(IEnumerable<GridDataItem> grdItems)
        {
            foreach (GridDataItem item in grdItems)
            {
                RemisionesVencidas rem = new RemisionesVencidas();
                rem.Id_Ter = Convert.ToInt32(item["Id_Ter"].Text);
                rem.Id_Rem = Convert.ToInt32(item["Id_Rem"].Text);
                rem.Id_Prd = Convert.ToInt32(item["Id_Prd"].Text);

                yield return rem;
            }
        }

        private List<RemisionDet> ObtieneRemisionDetalle()
        {
            List<RemisionDet> lRemisionDet = new List<RemisionDet>();
            List<RemisionesVencidas> lRemVencidas = new List<RemisionesVencidas>();

            CN_CapDevolucionRemision clsDR = new CN_CapDevolucionRemision();
            IEnumerable<GridDataItem> grdItems;
            IEnumerable<string> idProds;

            if (!string.IsNullOrEmpty(cmbTerritorio.SelectedValue))
            {
                DevolucionRemision dr = new DevolucionRemision();
                dr.Id_Emp = gSession.Id_Emp;
                dr.Id_Cd = gSession.Id_Cd_Ver;
                dr.Id_Ter = Int32.Parse(cmbTerritorio.SelectedValue);
                dr.Id_Cte = Int32.Parse(txtNumCliente.Text);
                dr.Id_Tm = Int32.Parse(txtTipoId.Text);
                dr.DevRem_Tipo = RblRealizar.Items[0].Selected ? "D" : "F";

                grdItems = rgDevParcial.MasterTableView.Items
                                                       .OfType<GridDataItem>()
                                                       .Where(gi =>
                                                                    ((CheckBox)(gi["BDetalleRemision"].FindControl("ChkDetalleRemision"))).Checked
                                                              );

                idProds = grdItems.Select(prods => prods["Id_Prd"].Text);
                dr.Id_Prd = string.Join(",", idProds);

                clsDR.ConsultaRemisionProductoSaldoDetalle(dr, gSession.Emp_Cnx, ref lRemisionDet);

                lRemVencidas = ObtieneListaRemisionesProducto(grdItems).ToList();

                lRemisionDet = lRemisionDet.Where(rem => rem.Rem_Estatus != "C")
                                           .Join(lRemVencidas, rem => new { Id_Ter = rem.Id_Ter.Value, Id_Prd = rem.Id_Prd },
                                                               grd => new { Id_Ter = grd.Id_Ter, Id_Prd = grd.Id_Prd }, (rem, grd) => new { rem, grd })
                                           .Select(s => s.rem).ToList();
            }

            return lRemisionDet;
        }

        protected void BtnDetalleRemisiones_Click(object sender, EventArgs e)
        {
            List<RemisionDet> lRemisionDet = new List<RemisionDet>();
            List<RemisionesVencidas> lRemVencidas = new List<RemisionesVencidas>();

            CN_CapDevolucionRemision clsDR = new CN_CapDevolucionRemision();
            IEnumerable<GridDataItem> grdItems;
            IEnumerable<string> idProds;

            DevolucionRemision dr = new DevolucionRemision();
            dr.Id_Emp = gSession.Id_Emp;
            dr.Id_Cd = gSession.Id_Cd_Ver;
            dr.Id_Ter = Int32.Parse(cmbTerritorio.SelectedValue);
            dr.Id_Cte = Int32.Parse(txtNumCliente.Text);
            dr.Id_Tm = Int32.Parse(txtTipoId.Text);
            dr.DevRem_Tipo = RblRealizar.Items[0].Selected ? "D" : "F";

            if (this.cmbTipoGarantia.SelectedValue != "")
                dr.Id_TG = Convert.ToInt32(this.cmbTipoGarantia.SelectedValue);
            grdItems = rgDevParcial.MasterTableView.Items
                                                   .OfType<GridDataItem>()
                                                   .Where(gi =>
                                                                ((CheckBox)(gi["BDetalleRemision"].FindControl("ChkDetalleRemision"))).Checked
                                                          );

            if (!grdItems.Any())
            {
                Alerta("Por favor seleccione un producto de la lista.");
                return;
            }

            idProds = grdItems.Select(prods => prods["Id_Prd"].Text);
            dr.Id_Prd = string.Join(",", idProds);

            clsDR.ConsultaRemisionProductoSaldoDetalle(dr, gSession.Emp_Cnx, ref lRemisionDet);

            lRemVencidas = ObtieneListaRemisionesProducto(grdItems).ToList();

            lRemisionDet = lRemisionDet.Where(rem => rem.Rem_Estatus != "C")
                                       .Join(lRemVencidas, rem => new { Id_Ter = rem.Id_Ter.Value, Id_Prd = rem.Id_Prd },
                                                           grd => new { Id_Ter = grd.Id_Ter, Id_Prd = grd.Id_Prd }, (rem, grd) => new { rem, grd })
                                       .Select(s => s.rem).ToList();

            if (!lRemisionDet.Any())
            {
                Alerta("No hay remisiones impresas con los productos seleccionados.");
                return;
            }

            RadTabStrip1.Tabs[0].Enabled = false;
            RadTabStrip1.Tabs[2].Enabled = false;
            RadTabStrip1.Tabs[3].Visible = true;
            RadTabStrip1.Tabs[3].Selected = true;
            RadPageViewDetalleRemisiones.Selected = true;
            string headerText = string.Empty;

            if (RblRealizar.SelectedValue == "1")
            {
                headerText = "Devolución Almacén";
                BtnFacturarRemision.Visible = false;
                BtnEntradaRemision.Visible = true;
                PnlEntradaRemision.Visible = true;
            }
            else
            {
                headerText = "Facturar";
                BtnFacturarRemision.Visible = true;
                BtnEntradaRemision.Visible = false;
                PnlEntradaRemision.Visible = false;
            }

            GridColumn gc = rgDetalleRemision.Columns.FindByDataField("Rem_CantE");
            gc.HeaderText = headerText;

            rgDetalleRemision.DataSource = lRemisionDet;
            rgDetalleRemision.DataBind();
            Valida_NumCantRemisionDevuelta(sender, e);

        }

        protected void BtnFacturarRemision_Click(object sender, EventArgs e)
        {
            int vCantidadT = 0;
            int cantidadRemision = 0;
            bool flagDif = false;
            bool ProductosSeleccionados = false;

            List<Remision> lRemisiones = new List<Remision>();
            if (txtNumCliente.Text.Trim() == "")
            {
                Alerta("Es necesario seleccionar Cliente");
                return;
            }

            if (txtTerritorio.Text.Trim() == "")
            {
                Alerta("Es necesario seleccionar Territorio");
                return;
            }

            if (txtTipoId.Text.Trim() == "")
            {
                Alerta("Es necesario seleccionar Tipo de Movimiento");
                return;
            }

            IEnumerable<GridDataItem> gridItems = rgDetalleRemision.MasterTableView.Items.Cast<GridDataItem>().Where(x =>
                                                           ((RadNumericTextBox)x.FindControl("NumCantRemisionDevuelta")).Text.Trim() != "" &&
                                                           ((RadNumericTextBox)x.FindControl("NumCantRemisionDevuelta")).Text.Trim() != "0");

            gridItems.ToList().ForEach(gi =>
            {
                vCantidadT = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("NumCantRemisionDevuelta")).Text);
                cantidadRemision = Convert.ToInt32(gi["Rem_Cant"].Text);

                Remision remision = new Remision();
                remision.Id_Emp = gSession.Id_Emp;
                remision.Id_Cd = gSession.Id_Cd_Ver;
                remision.Id_Rem = Convert.ToInt32(gi["Id_Rem"].Text);
                remision.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                remision.Id_Ter = Convert.ToInt32(gi["Id_Ter"].Text);
                remision.Cte_NomComercial = txtCliente.Text;
                remision.Id_Prd = Convert.ToInt32(gi["Id_Prd"].Text);
                remision.Id_Tm = Convert.ToInt32(txtTipoId.Text);
                remision.Cant = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("NumCantRemisionDevuelta")).Text.Trim());
                lRemisiones.Add(remision);

                if (vCantidadT > cantidadRemision)
                {
                    flagDif = true;
                }
            });

            ProductosSeleccionados = gridItems.Any();

            if (!ProductosSeleccionados)
            {
                Alerta("Es necesario agregar la cantidad a facturar en al menos un producto");
                return;
            }

            if (flagDif)
            {
                Alerta("La cantidad debe ser menor a la de la remisión.");
                return;
            }

            if (ValidaRemisionFacturaMismoPrecio(lRemisiones))
            {
                Alerta("Solo se pueden facturar productos con el mismo precio.");
                return;
            }



            ListaRemisionesFactura = lRemisiones;
            Session["DevolucionRemision" + Session.SessionID] = true;
            if (this.cmbTipoGarantia.SelectedValue != "")
                Session["Id_TG_Fac" + Session.SessionID] = Convert.ToInt32(this.cmbTipoGarantia.SelectedValue);
            string permisoGuardar = "1";
            string permisoModificar = "1";
            string permisoEliminar = "1";
            string permisoImprimir = "1";

            string cmdScript = string.Format("AbrirVentana_Factura({0},{1},{2},{3},{4},{5},{6},{7});return CloseWindowRem();", gSession.Id_Emp, gSession.Id_Cd_Ver, 0, 1, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
            RAM1.ResponseScripts.Add(cmdScript);

        }

        private bool ValidaRemisionFacturaMismoPrecio(List<Remision> listRemisiones)
        {
            List<RemisionDet> listRemisionDet = new List<RemisionDet>();
            CD_CapRemision capRemision = new CD_CapRemision();
            listRemisiones.ForEach(x =>
            {
                capRemision.ConsultarRemisionesDetalle(gSession, x, ref listRemisionDet);
            });

            var joinRemision = listRemisionDet.Join(listRemisiones, rDet => new { rDet.Id_Prd, rDet.Id_Rem }
                                                                  , rRem => new { rRem.Id_Prd, rRem.Id_Rem }
                                                                  , (rDet, rRem) => new { DevRemDet = rDet });

            var groupRemision = joinRemision.GroupBy(x => new { x.DevRemDet.Id_Prd, x.DevRemDet.Rem_Precio })
                                               .GroupBy(g => g.Key.Id_Prd)
                                               .Select(s => new { Id_Prd = s.Key, Prd_Count = s.Count() });

            return groupRemision.Where(x => x.Prd_Count > 1).Any();
        }

        protected void BtnEntradaRemision_Click(object sender, EventArgs e)
        {
            try
            {
                DevolucionRemision dR = new DevolucionRemision();
                List<RemisionDet> lRemision = new List<RemisionDet>();
                List<RemisionDet> lRemisionFinal = new List<RemisionDet>();
                List<RemisionDet> lcompare = new List<RemisionDet>();

                int vCantidadT = 0;
                int cantidadRemision = 0;
                bool flagDif = false;
                bool ProductosSeleccionados = false;

                IEnumerable<GridDataItem> gridItems = rgDetalleRemision.MasterTableView.Items.Cast<GridDataItem>().Where(x =>
                                                           ((RadNumericTextBox)x.FindControl("NumCantRemisionDevuelta")).Text.Trim() != "" &&
                                                           ((RadNumericTextBox)x.FindControl("NumCantRemisionDevuelta")).Text.Trim() != "0");

                gridItems.ToList().ForEach(gi =>
                {
                    vCantidadT = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("NumCantRemisionDevuelta")).Text);
                    cantidadRemision = Convert.ToInt32(gi["Rem_Cant"].Text);

                    if (vCantidadT > cantidadRemision)
                    {
                        flagDif = true;
                    }
                });

                ProductosSeleccionados = gridItems.Any();

                if (!ProductosSeleccionados)
                {
                    Alerta("Es necesario agregar la cantidad a devolver en al menos un producto");
                    return;
                }

                if (flagDif)
                {
                    Alerta("La cantidad debe ser menor a la de la remisión.");
                    return;
                }

                if (txtNumCliente.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Cliente");
                    return;
                }

                if (txtTerritorio.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Territorio");
                    return;
                }

                if (txtTipoId.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Tipo de Movimiento");
                    return;
                }

                lcompare = GetListSaldo();

                CN_CapDevolucionRemision CN_DevolucionRemision = new CN_CapDevolucionRemision();
                dR.Id_Emp = gSession.Id_Emp;
                dR.Id_Cd = gSession.Id_Cd_Ver;
                dR.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                dR.Id_Tm = Convert.ToInt32(txtTipoId.Text);
                dR.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                dR.DevRem_Tipo = RblRealizar.Items[0].Selected ? "D" : "F";

                DevolucionRemision devrem = new DevolucionRemision();
                devrem.Id_Emp = gSession.Id_Emp;
                devrem.Id_Cd = gSession.Id_Cd_Ver;
                devrem.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                devrem.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                devrem.DevRem_CteNombre = txtCliente.Text;
                devrem.DevRem_Fecha = Convert.ToDateTime(dpFecha1.SelectedDate);
                devrem.Id_U = gSession.Id_U;
                devrem.Id_Tm = Convert.ToInt32(txtTipoId.Text);
                devrem.Estatus = "C";
                devrem.DevRem_Tipo = RblRealizar.Items[0].Selected ? "D" : "F";

                IEnumerable<string> enumProds = gridItems.Select(s => s["Id_Prd"].Text);
                dR.Id_Prd = string.Join(",", enumProds);

                CN_DevolucionRemision.ConsultaRemisionProductoSaldoDetalle(dR, gSession.Emp_Cnx, ref lRemision);
                int saldo = 0;

                vCantidadT = 0;
                int val;
                int IdProd = 0;
                int IdRem = 0;
                int IdTer = 0;

                foreach (GridDataItem item in rgDetalleRemision.MasterTableView.Items)
                {
                    saldo = 0;
                    vCantidadT = ((RadNumericTextBox)item.FindControl("NumCantRemisionDevuelta")).Text.Trim() == "" ? 0 : Convert.ToInt32(((RadNumericTextBox)item.FindControl("NumCantRemisionDevuelta")).Text);
                    int cant = 0;
                    bool flag = false;
                    IdProd = Convert.ToInt32(item["Id_Prd"].Text);
                    IdRem = Convert.ToInt32(item["Id_Rem"].Text);
                    IdTer = Convert.ToInt32(item["Id_Ter"].Text);

                    foreach (RemisionDet rd in lRemision.Where(x => x.Id_Prd == IdProd && x.Id_Rem == IdRem && x.Id_Ter == IdTer))
                    {
                        if (vCantidadT != 0)
                        {
                            cant = (vCantidadT - rd.Rem_Cant) < 0 ? vCantidadT : rd.Rem_Cant;
                            flag = vCantidadT != 0 ? true : false;
                            vCantidadT = vCantidadT - rd.Rem_Cant;
                            vCantidadT = vCantidadT < 0 ? 0 : vCantidadT;

                            Producto producto = null;
                            CN_CatProducto clsProducto = new CN_CatProducto();
                            clsProducto.ConsultaProducto(ref producto, gSession.Emp_Cnx, gSession.Id_Emp, gSession.Id_Cd_Ver, gSession.Id_Cd_Ver, rd.Id_Prd, 3);

                            if (flag)
                            {
                                RemisionDet RemDet = new RemisionDet();
                                RemDet.Id_Emp = gSession.Id_Emp;
                                RemDet.Id_Cd = gSession.Id_Cd_Ver;
                                RemDet.Id_Rem = rd.Id_Rem;
                                RemDet.Id_Prd = rd.Id_Prd;
                                RemDet.Id_Ter = rd.Id_Ter;
                                RemDet.Rem_Cant = cant;
                                RemDet.Rem_Precio = double.Parse(producto.Prd_AAACadena == "" ? "0" : producto.Prd_AAACadena);
                                lRemisionFinal.Add(RemDet);
                            }
                        }

                    }
                }
                CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
                CentroDistribucion centroDistribucion = new CentroDistribucion();
                double icd = 0;
                cd.ConsultarIva(gSession.Id_Emp, gSession.Id_Cd_Ver, ref icd, gSession.Emp_Cnx);

                int verificador = 0;
                string mensaje = string.Empty;

                CN_CapEntradaSalida cn_capEntradaSalida = new CN_CapEntradaSalida();

                IEnumerable<int> lRemisionFinalDistinct = lRemisionFinal.Select(x => x.Id_Rem).Distinct();
                List<DevolucionRemisionDet> lDevRemisionDet = new List<DevolucionRemisionDet>();
                List<EntradaSalida> lEntradaSalida = new List<EntradaSalida>();

                foreach (int rd in lRemisionFinalDistinct)
                {
                    EntradaSalida entsal = new EntradaSalida();
                    entsal.Id_Emp = gSession.Id_Emp;
                    entsal.Id_Cd = gSession.Id_Cd_Ver;
                    entsal.Id_U = gSession.Id_U;
                    entsal.Id_Es = Int32.Parse(MaximoId("CapEntsal", "Id_Es"));
                    entsal.Es_Naturaleza = 0;
                    entsal.Es_Fecha = DateTime.Now;
                    entsal.Id_Tm = CN_DevolucionRemision.ConsultaMovInverso(gSession.Id_Emp, Convert.ToInt32(txtTipoId.Text), gSession.Emp_Cnx);
                    entsal.Id_Pvd = -1;
                    entsal.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                    entsal.Es_Referencia = rd.ToString();
                    entsal.Es_Notas = "Movimiento por proceso de devolución de remisiones, número de devolución:" + txtSolicitud.Text;
                    entsal.Es_Estatus = "C";
                    entsal.ManAut = true;
                    entsal.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    entsal.Es_CteCuentaNacional = -1;
                    entsal.Es_CteCuentaContNacional = 0;
                    entsal.Id_Cte = Convert.ToInt32(txtNumCliente.Text);

                    double TotalEs = 0;
                    bool CantMayor = false;
                    string Cantidad;

                    List<EntradaSalidaDetalle> listaDetalle = new List<EntradaSalidaDetalle>();

                    foreach (RemisionDet rf in lRemisionFinal.FindAll(x => x.Id_Rem == rd))
                    {
                        EntradaSalidaDetalle vpep = new EntradaSalidaDetalle();
                        vpep.Id_Emp = gSession.Id_Emp;
                        vpep.Id_Cd = gSession.Id_Cd_Ver;
                        vpep.Id_Es = entsal.Id_Es;
                        vpep.Id_Prd = rf.Id_Prd;
                        vpep.Es_Cantidad = rf.Rem_Cant;
                        vpep.Es_Costo = rf.Rem_Precio;
                        vpep.Id_Rem = rf.Id_Rem;
                        vpep.Id_Ter = rf.Id_Ter.Value;

                        vpep.EsDet_Naturaleza = 1;
                        if (vpep.Es_Cantidad != 0)
                        {
                            TotalEs = TotalEs + (vpep.Es_Costo * vpep.Es_Cantidad);
                            listaDetalle.Add(vpep);

                            DevolucionRemisionDet drd = new DevolucionRemisionDet();
                            drd.Id_Cd = gSession.Id_Emp;
                            drd.Id_Emp = gSession.Id_Cd_Ver;
                            drd.Id_Rem = rd;
                            drd.Id_Prd = rf.Id_Prd;
                            drd.Id_Cte = entsal.Id_Cte;
                            drd.Id_Ter = rf.Id_Ter.Value;
                            drd.Cant = vpep.Es_Cantidad;
                            drd.Id_Es = entsal.Id_Es;
                            lDevRemisionDet.Add(drd);
                        }
                    }
                    entsal.Es_SubTotal = TotalEs;
                    entsal.Es_Iva = TotalEs * (icd / 100);
                    entsal.Es_Total = TotalEs * (1 + (icd / 100));
                    entsal.ListEntradaSalidaDetalle = new List<EntradaSalidaDetalle>(listaDetalle);
                    lEntradaSalida.Add(entsal);
                }


                devrem.ListEntradaSalida = lEntradaSalida;
                devrem.ListDevolucionRemisionDet = lDevRemisionDet;

                int strEmp = gSession.Id_Emp;
                string verificadorStr = "0";
                int id_DevRem = 0;
                if (Request.QueryString["id"] == "-1" || Request.QueryString["id"] == null)
                {
                    CN_DevolucionRemision.GuardarEntradaSalida(devrem, ref verificadorStr, strEmp, gSession.Emp_Cnx, ref id_DevRem);
                }

                if (verificadorStr == "0" && id_DevRem != 0)
                {
                    mensaje = "El devolución # " + id_DevRem + " ha sido Aplicada";
                    List_Historico = GetListDetalleHistorico();
                    List_Saldo = GetListSaldo();
                    //rgDetalleMov.Rebind();
                    rgDetalleRemision.Rebind();
                    RadTabStrip1.Tabs[2].Enabled = false;
                    BtnDevolucion.Enabled = false;
                    RAM1.ResponseScripts.Add(string.Concat(@"alertClose('", mensaje, "')"));
                }
                else
                {
                    Alerta("Error al aplicar devolución");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }

        protected void BtnCancelarRemision_Click(object sender, EventArgs e)
        {
            RadTabStrip1.Tabs[3].Visible = true;
            RadTabStrip1.Tabs[3].Enabled = true;
            RadTabStrip1.Tabs[2].Visible = false;
            RadTabStrip1.Tabs[0].Enabled = true;

            RadPageViewDevoluciones.Selected = true;
        }

        protected void NumCantRemisionDevuelta_TextChanged(object sender, EventArgs e)
        {
            Valida_NumCantRemisionDevuelta(sender, e);
        }

        public void Valida_NumCantRemisionDevuelta(object sender, EventArgs e)
        {
            CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();

            double vIva = 0;
            float subtotal = 0;
            float iva = 0;
            float total = 0;

            List<Producto> lPrd = new List<Producto>();
            cd.ConsultarIva(gSession.Id_Emp, gSession.Id_Cd_Ver, ref vIva, gSession.Emp_Cnx);

            foreach (GridDataItem item in rgDetalleRemision.MasterTableView.Items)
            {
                RadNumericTextBox txtNumCanDev = (RadNumericTextBox)item.FindControl("NumCantRemisionDevuelta");
                string cantidad = txtNumCanDev.Text;
                string cantidadRemision = item["Rem_Cant"].Text;

                if (!string.IsNullOrEmpty(cantidad) && !string.IsNullOrEmpty(cantidadRemision))
                {
                    if (Convert.ToInt32(cantidad) > Convert.ToInt32(cantidadRemision))
                    {
                        Alerta("La cantidad debe ser menor a la de la remisión.");
                        return;
                    }
                    else
                    {
                        if ((cantidad != "") && (cantidad != "0"))
                        {
                            if (RblRealizar.SelectedValue == "1")
                            {
                                //Revisar el parse, Id_Prd está recibiendo valores incorrectos...
                                int Id_Prd = Int32.Parse(item["Id_Prd"].Text);
                                float precio = 1;

                                CN_ProductoPrecios prdPrecios = new CN_ProductoPrecios();
                                prdPrecios.ConsultaListaProductoPrecioAAA(ref precio, gSession.Id_Emp, gSession.Id_Cd_Ver, Id_Prd, gSession.Emp_Cnx);

                                Producto prd = new Producto();

                                prd.Id_Prd = Id_Prd;
                                prd.Prd_Precio = precio.ToString("###,###,##0.00");
                                prd.Prd_Descripcion = item["Prd_Descripcion"].Text;
                                prd.CantFact = Int32.Parse(cantidad);
                                prd.Prd_Ordenado = Int32.Parse(cantidad);

                                subtotal += precio * prd.Prd_Ordenado;

                                lPrd.Add(prd);

                                total = subtotal * (1 + ((float)vIva / 100));
                                iva = total - subtotal;

                                TxtRemisionDevolucionSubtotal.Text = String.Format("{0:###,###,##0.00}", subtotal);
                                TxtRemisionDevolucionIVA.Text = String.Format("{0:###,###,##0.00}", iva);
                                TxtRemisionDevolucionTotal.Text = String.Format("{0:###,###,##0.00}", total);
                            }
                        }
                    }
                }
                if (this.txtTipoId.Text == "92")
                {
                    txtNumCanDev.Text = cantidadRemision;
                    txtNumCanDev.Enabled = false;
                }
            }

            lPrd = lPrd.GroupBy(p => new { p.Id_Prd, p.Prd_Descripcion })
                                      .Select(p => new Producto
                                      {
                                          Id_Prd = p.Key.Id_Prd,
                                          Prd_Descripcion = p.Key.Prd_Descripcion,
                                          Prd_Precio = p.Max(a => a.Prd_Precio),
                                          CantFact = p.Sum(a => a.CantFact),
                                          Prd_Ordenado = p.Sum(a => a.Prd_Ordenado)
                                      }).ToList();

            if (lPrd.Any())
            {
                rgDetalleRemisionDevolucion.DataSource = lPrd;
                rgDetalleRemisionDevolucion.DataBind();
            }
        }

        protected void BtnDevolucion_Click(object sender, EventArgs e)
        {
            try
            {
                DevolucionRemision dR = new DevolucionRemision();
                List<RemisionDet> lRemision = new List<RemisionDet>();
                List<RemisionDet> lRemisionFinal = new List<RemisionDet>();

                if (txtNumCliente.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Cliente");
                    return;
                }

                if (txtTerritorio.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Territorio");
                    return;
                }

                if (txtTipoId.Text.Trim() == "")
                {
                    Alerta("Es necesario seleccionar Tipo de Movimiento");
                    return;
                }

                List<RemisionDet> lcompare = new List<RemisionDet>();

                lcompare = GetListSaldo();
                bool flagDif = false;
                if (rgDevParcial.MasterTableView.Items.Count != lcompare.Count())
                {

                    flagDif = true;
                }

                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    foreach (RemisionDet compare in lcompare)
                    {
                        if (Convert.ToInt32(item["Id_Prd"].Text) == compare.Id_Prd)
                        {
                            if (Convert.ToInt32(item["Rem_Cant"].Text) != compare.Rem_Cant)
                            {
                                flagDif = true;
                            }
                        }
                    }
                }

                if (flagDif)
                {
                    Alerta("El Saldo ha sido actualizado debido a que otro usuario esta realizando una transacción en este momento para este cliente y tipo de movimiento, es necesario volver a capturar la devolución");
                    List_Saldo = GetListSaldo();
                    rgDevParcial.Rebind();
                    return;
                }


                bool ProductosSeleccionados = false;
                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    if (((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "" &&
                        ((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "0")
                    {
                        ProductosSeleccionados = true;
                    }
                }

                if (!ProductosSeleccionados)
                {
                    Alerta("Es necesario agregar la cantidad a devolver en al menos un producto");
                    return;
                }

                CN_CapDevolucionRemision CN_DevolucionRemision = new CN_CapDevolucionRemision();
                dR.Id_Emp = gSession.Id_Emp;
                dR.Id_Cd = gSession.Id_Cd_Ver;
                dR.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                dR.Id_Tm = Convert.ToInt32(txtTipoId.Text);
                dR.Id_Ter = Convert.ToInt32(txtTerritorio.Text);


                DevolucionRemision devrem = new DevolucionRemision();
                devrem.Id_Emp = gSession.Id_Emp;
                devrem.Id_Cd = gSession.Id_Cd_Ver;
                devrem.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                devrem.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                devrem.DevRem_CteNombre = txtCliente.Text;
                devrem.DevRem_Fecha = Convert.ToDateTime(dpFecha1.SelectedDate);
                devrem.Id_U = gSession.Id_U;
                devrem.Id_Tm = Convert.ToInt32(txtTipoId.Text);
                devrem.Estatus = "C";

                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    if (((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "" &&
                        ((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() != "0")
                    {
                        dR.Id_Prd += item["Id_Prd"].Text + ",";
                    }
                }


                CN_DevolucionRemision.ConsultaRemisionProductoSaldoDetalle(dR, gSession.Emp_Cnx, ref lRemision);
                int saldo = 0;

                int CantidadT = 0;
                int val;
                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    saldo = 0;
                    CantidadT = ((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text.Trim() == "" ? 0 : Convert.ToInt32(((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text);
                    int cant = 0;
                    int primera = 0;
                    bool flag = false;

                    foreach (RemisionDet rd in lRemision)
                    {
                        if (CantidadT != 0)
                        {
                            if (Convert.ToInt32(item["Id_Prd"].Text) == rd.Id_Prd)
                            {
                                cant = (CantidadT - rd.Rem_Cant) < 0 ? CantidadT : rd.Rem_Cant;
                                flag = CantidadT != 0 ? true : false;
                                CantidadT = CantidadT - rd.Rem_Cant;
                                CantidadT = CantidadT < 0 ? 0 : CantidadT;

                                Producto producto = null;
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                clsProducto.ConsultaProducto(ref producto, gSession.Emp_Cnx, gSession.Id_Emp, gSession.Id_Cd_Ver, gSession.Id_Cd_Ver, rd.Id_Prd, 3);

                                if (flag)
                                {
                                    RemisionDet RemDet = new RemisionDet();
                                    RemDet.Id_Emp = gSession.Id_Emp;
                                    RemDet.Id_Cd = gSession.Id_Cd_Ver;
                                    RemDet.Id_Rem = rd.Id_Rem;
                                    RemDet.Id_Prd = rd.Id_Prd;
                                    RemDet.Rem_Cant = cant;
                                    RemDet.Rem_Precio = double.Parse(producto.Prd_Precio == "" ? "0" : producto.Prd_Precio);
                                    lRemisionFinal.Add(RemDet);
                                }
                            }
                        }

                    }
                }

                CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
                CentroDistribucion centroDistribucion = new CentroDistribucion();
                double icd = 0;
                cd.ConsultarIva(gSession.Id_Emp, gSession.Id_Cd_Ver, ref icd, gSession.Emp_Cnx);

                int verificador = 0;
                string mensaje = string.Empty;

                CN_CapEntradaSalida cn_capEntradaSalida = new CN_CapEntradaSalida();

                IEnumerable<int> lRemisionFinalDistinct = lRemisionFinal.Select(x => x.Id_Rem).Distinct();
                List<DevolucionRemisionDet> lDevRemisionDet = new List<DevolucionRemisionDet>();
                List<EntradaSalida> lEntradaSalida = new List<EntradaSalida>();

                foreach (int rd in lRemisionFinalDistinct)
                {
                    EntradaSalida entsal = new EntradaSalida();
                    entsal.Id_Emp = gSession.Id_Emp;
                    entsal.Id_Cd = gSession.Id_Cd_Ver;
                    entsal.Id_U = gSession.Id_U;
                    entsal.Id_Es = Int32.Parse(MaximoId("CapEntsal", "Id_Es"));
                    entsal.Es_Naturaleza = 0;
                    entsal.Es_Fecha = DateTime.Now;
                    entsal.Id_Tm = CN_DevolucionRemision.ConsultaMovInverso(gSession.Id_Emp, Convert.ToInt32(txtTipoId.Text), gSession.Emp_Cnx);
                    entsal.Id_Pvd = -1;
                    entsal.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                    entsal.Es_Referencia = rd.ToString();
                    entsal.Es_Notas = "Movimiento por proceso de devolución de remisiones, número de devolución:" + txtSolicitud.Text;
                    entsal.Es_Estatus = "C";
                    entsal.ManAut = true;
                    entsal.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    entsal.Es_CteCuentaNacional = -1;
                    entsal.Es_CteCuentaContNacional = 0;
                    entsal.Id_Cte = Convert.ToInt32(txtNumCliente.Text);

                    double TotalEs = 0;
                    bool CantMayor = false;
                    string Cantidad;

                    List<EntradaSalidaDetalle> listaDetalle = new List<EntradaSalidaDetalle>();

                    foreach (RemisionDet rf in lRemisionFinal.FindAll(x => x.Id_Rem == rd))
                    {
                        EntradaSalidaDetalle vpep = new EntradaSalidaDetalle();
                        vpep.Id_Emp = gSession.Id_Emp;
                        vpep.Id_Cd = gSession.Id_Cd_Ver;
                        vpep.Id_Es = entsal.Id_Es;
                        vpep.Id_Prd = rf.Id_Prd;
                        vpep.Es_Cantidad = rf.Rem_Cant;
                        vpep.Es_Costo = rf.Rem_Precio;

                        vpep.EsDet_Naturaleza = 1;
                        if (vpep.Es_Cantidad != 0)
                        {
                            TotalEs = TotalEs + (vpep.Es_Costo * vpep.Es_Cantidad);
                            listaDetalle.Add(vpep);


                            DevolucionRemisionDet drd = new DevolucionRemisionDet();
                            drd.Id_Cd = gSession.Id_Emp;
                            drd.Id_Emp = gSession.Id_Cd_Ver;
                            drd.Id_Rem = rd;
                            drd.Id_Prd = rf.Id_Prd;
                            drd.Id_Cte = entsal.Id_Cte;
                            drd.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                            lDevRemisionDet.Add(drd);
                        }
                    }
                    entsal.Es_SubTotal = TotalEs;
                    entsal.Es_Iva = TotalEs * (icd / 100);
                    entsal.Es_Total = TotalEs * (1 + (icd / 100));
                    entsal.ListEntradaSalidaDetalle = listaDetalle;
                    lEntradaSalida.Add(entsal);
                }

                devrem.ListEntradaSalida = lEntradaSalida;
                devrem.ListDevolucionRemisionDet = lDevRemisionDet;
                int strEmp = gSession.Id_Emp;
                string verificadorStr = "0";
                int id_DevRem = 0;
                if (Request.QueryString["id"] == "-1" || Request.QueryString["id"] == null)
                {
                    CN_DevolucionRemision.GuardarEntradaSalida(devrem, ref verificadorStr, strEmp, gSession.Emp_Cnx, ref id_DevRem);
                }

                if (verificadorStr == "0" && id_DevRem != 0)
                {
                    mensaje = "El devolución # " + id_DevRem + " ha sido Aplicada";
                    List_Historico = GetListDetalleHistorico();
                    List_Saldo = GetListSaldo();
                    rgDetalleMov.Rebind();
                    rgDevParcial.Rebind();
                    RadTabStrip1.Tabs[2].Enabled = false;
                    BtnDevolucion.Enabled = false;
                    RAM1.ResponseScripts.Add(string.Concat(@"alertClose('", mensaje, "')"));
                }
                else
                {
                    Alerta("Error al aplicar devolución");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }


        private void CargarComboTerritorios()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int cliente = !string.IsNullOrEmpty(txtNumCliente.Text) ? Convert.ToInt32(txtNumCliente.Text.ToString()) : -1;

                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTerritoriosDelCliente(cliente, gSession, ref listaTerritorios);
                cmbTerritorio.DataTextField = "Descripcion";
                cmbTerritorio.DataValueField = "Id_Ter";
                cmbTerritorio.DataSource = listaTerritorios;
                cmbTerritorio.DataBind();

                if (cmbTerritorio.Items.Count > 1)
                {
                    cmbTerritorio.SelectedIndex = 1;
                    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    txtTerritorio.Text = cmbTerritorio.Items[1].Value;

                    CN_CatTerritorios territorio = new CN_CatTerritorios();
                    Territorios ter = new Territorios();
                    ter.Id_Emp = gSession.Id_Emp;
                    ter.Id_Cd = gSession.Id_Cd_Ver;
                    ter.Id_Ter = Convert.ToInt32(cmbTerritorio.Items[1].Value);
                    territorio.ConsultaTerritoriosCombo(ref ter, gSession.Emp_Cnx);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbTipoGarantiaChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                if (txtCliente.Text != "")
                {
                    List_Saldo = GetListSaldo();
                    rgDevParcial.Rebind();
                    txtTerritorio.Focus();
                }

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                txtCliente.Text = "";
                txtNumCliente.Text = "";
                txtTerritorio.Text = "";
                cmbTerritorio.SelectedIndex = -1;

            }
        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                Clientes cliente = new Clientes();
                cliente.Id_Emp = gSession.Id_Emp;
                cliente.Id_Cd = gSession.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value.Value : -1);
                new CN_CatCliente().ConsultaClientes(ref cliente, gSession.Emp_Cnx);
                txtCliente.Text = cliente.Cte_NomComercial;
                CargarComboTerritorios();


                List_Saldo = GetListSaldo();
                rgDevParcial.Rebind();
                txtTerritorio.Focus();

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                txtCliente.Text = "";
                txtNumCliente.Text = "";
                txtTerritorio.Text = "";
                cmbTerritorio.SelectedIndex = -1;

            }
        }

        protected void txtTipoId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (txtNumCliente.Text != string.Empty && (cmbTerritorio.SelectedValue != "-1" && cmbTerritorio.SelectedValue != string.Empty))
                {
                    List_Saldo = GetListSaldo();
                    rgDevParcial.Rebind();
                    txtNumCliente.Focus();
                    this.rgDevParcial.Enabled = true;
                }

                var val = ((Telerik.Web.UI.RadNumericTextBox)(sender)).Value;

                if (val == 92)
                {
                    this.cmbTipoGarantia.Visible = true;
                    this.lblTipoGarantia.Visible = true;
                }
                else
                {
                    this.cmbTipoGarantia.Visible = false;
                    this.lblTipoGarantia.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        private void CargarTipoMovimiento()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(gSession.Id_Emp, gSession.Emp_Cnx, "spCatMovimiento_ComboParaRemisionesDevolucion", ref cmbTipoMov);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboTiposGarantias()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(gSession.Id_Emp, gSession.Emp_Cnx, "spComboTipoGarantias", ref this.cmbTipoGarantia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbTerritorio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((txtCliente.Text != string.Empty) && (this.cmbTipoMov.SelectedValue != "-1" && this.cmbTipoMov.SelectedValue != string.Empty)
                        && (cmbTerritorio.SelectedValue != "-1" && cmbTerritorio.SelectedValue != string.Empty))
                {
                    this.rgDevParcial.Enabled = true;

                }
                else
                {
                    this.rgDevParcial.Enabled = false;
                }

                CN_CatTerritorios territorio = new CN_CatTerritorios();
                Territorios ter = new Territorios();
                ter.Id_Emp = gSession.Id_Emp;
                ter.Id_Cd = gSession.Id_Cd_Ver;
                ter.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                territorio.ConsultaTerritoriosCombo(ref ter, gSession.Emp_Cnx);


                List_Saldo = GetListSaldo();
                rgDevParcial.Rebind();

            }
            catch (Exception ex)
            {
                this.Alerta(string.Concat(ex.Message));
            }
        }

        protected void NumCantDevuelta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                int CantidadDevolver = combo.Value.HasValue ? Int32.Parse(combo.Text) : 0;

                GridDataItem dataItem = combo.Parent.Parent as GridDataItem;

                if (Int32.Parse(dataItem["Rem_Cant"].Text) < CantidadDevolver)
                {
                    combo.Value = 0;
                    Alerta("No se devolver una cantidad Mayor al saldo del producto ");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbMov_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                rgDevParcial.Rebind();

            }
            catch (Exception ex)
            {
                this.Alerta(ex.Message);
            }
        }


        private void Inicializar(int Id_Folio)
        {
            try
            {
                DevolucionRemision v = new DevolucionRemision();
                bool selected = false;
                v.Id_Emp = gSession.Id_Emp;
                v.Id_Cd = gSession.Id_Cd;
                v.Id_DevRem = Id_Folio;

                new CN_CapDevolucionRemision().Consulta(ref v, gSession.Emp_Cnx, gSession.Id_Emp, gSession.Id_Cd_Ver, Id_Folio);

                string Estatus = v.Estatus.Trim();

                if (Estatus != "C")
                {
                    RadTabStrip1.Tabs[2].Enabled = false;
                    RadTabStrip1.Tabs[1].Enabled = true;
                }

                dpFecha1.SelectedDate = v.DevRem_Fecha;
                cmbEstatus.SelectedValue = v.Estatus.Trim();

                txtNumCliente.Text = v.Id_Cte.ToString();
                txtCliente.Text = v.DevRem_CteNombre;
                txtTerritorio.Text = v.Id_Ter.ToString();
                txtSolicitud.Text = v.Id_DevRem.ToString();
                txtTipoId.Text = v.Id_Tm.ToString();

                selected = v.DevRem_Tipo.Trim().Equals("F", StringComparison.OrdinalIgnoreCase);

                RblRealizar.Items[0].Selected = !selected;
                RblRealizar.Items[1].Selected = selected;

                CargarComboTerritorios();
                try
                {
                    cmbTipoMov.SelectedIndex = cmbTipoMov.FindItemIndexByValue(v.Id_Tm.ToString());
                    cmbTipoMov.Text = cmbTipoMov.FindItemByValue(v.Id_Tm.ToString()).Text;
                }
                catch
                {
                }

                List_Historico = GetListDetalleHistorico();
                rgDetalleMov.Rebind();
                RadTabStrip1.Tabs[2].Enabled = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private List<RemisionDet> GetListSaldo()
        {
            try
            {
                List<RemisionDet> List = new List<RemisionDet>();
                CN_CapDevolucionRemision clsCapDevolucionRemision = new CN_CapDevolucionRemision();
                DevolucionRemision rd = new DevolucionRemision();
                rd.Id_Emp = gSession.Id_Emp;
                rd.Id_Cd = gSession.Id_Cd_Ver;
                rd.Id_Cte = Convert.ToInt32(txtNumCliente.Text);
                rd.Id_Ter = txtTerritorio.Value.HasValue ? (int)txtTerritorio.Value.Value : -1;
                rd.Id_Tm = Convert.ToInt32(cmbTipoMov.SelectedValue);

                if (rd.Id_Tm == 92 && this.cmbTipoGarantia.SelectedValue != "")
                    rd.Id_TG = Convert.ToInt32(this.cmbTipoGarantia.SelectedValue);
                clsCapDevolucionRemision.ConsultaRemisionSaldo(rd, gSession.Emp_Cnx, ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<DevolucionRemisionDet> GetListDetalleHistorico()
        {
            try
            {
                List<DevolucionRemisionDet> List = new List<DevolucionRemisionDet>();
                CN_CapDevolucionRemision clsCapDevolucionRemision = new CN_CapDevolucionRemision();
                DevolucionRemision dr = new DevolucionRemision();

                dr.Id_Emp = gSession.Id_Emp;
                dr.Id_Cd = gSession.Id_Cd_Ver;
                dr.Id_DevRem = (int)txtSolicitud.Value.Value;

                clsCapDevolucionRemision.ConsultaDevolucionHistorico(dr, gSession.Emp_Cnx, ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                DateTime fechaPeriodoInicio = gSession.CalendarioIni;
                DateTime fechaPeriodoFinal = gSession.CalendarioFin;
                GridItem gi = e.Item;

                switch (e.CommandName.ToLower())
                {
                    case "editar":

                        editar(gi);
                        break;

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
                switch (cmd)
                {
                    case "RebindGrid":
                        rgDetalleMov.Rebind();
                        break;
                    case "RebindGridMain":
                        RAM1.ResponseScripts.Add("return CloseAndRebind()");
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 50);
                        RadPageViewDevoluciones.Height = altura;
                        RadSplitterDevol.Height = altura;
                        RadPaneDevol.Height = altura;
                        RadPageViewDetalleRemisiones.Height = altura;
                        RadSplitterDetalleRemisiones.Height = altura;
                        RadPanelDetalleRemisiones.Height = altura;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }


        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            Telerik.Web.UI.RadTab TabClicked = e.Tab;
            // Label1.Text = TabClicked.Value;
            IEnumerable<GridDataItem> gridItems;

            gridItems = rgDevParcial.MasterTableView.Items
                                                   .OfType<GridDataItem>();



            if (cmbTipoMov.SelectedValue == "92" && this.RblRealizar.SelectedValue == "2")
            {

                foreach (GridDataItem gi in gridItems)
                {
                    CheckBox cbItem = ((CheckBox)(gi["BDetalleRemision"].FindControl("ChkDetalleRemision")));
                    cbItem.Checked = true;
                    cbItem.Enabled = false;
                }

            }
            else
            {
                foreach (GridDataItem gi in gridItems)
                {
                    CheckBox cbItem = ((CheckBox)(gi["BDetalleRemision"].FindControl("ChkDetalleRemision")));
                    cbItem.Checked = false;
                    cbItem.Enabled = true;
                }
            }
        }


        #region Funciones
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
                CapaNegocio.PaginaConsultar(ref pagina, gSession.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = gSession.Id_U;
                Permiso.Id_Cd = gSession.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, gSession.Emp_Cnx);

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

        private void CargarEstatus()
        {
            cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));
            cmbEstatus.Items.Add(new RadComboBoxItem("Capturada", "C"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Cancelada", "B"));
        }

        protected void rgDevParcial_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDevParcial.DataSource = List_Saldo;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgDetalleMov_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDetalleMov.DataSource = List_Historico;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private int CambiarEstatus(int Id_Env, string estatus)
        {
            try
            {
                CN_EntradaVirtual CN_EntradaVirtual = new CN_EntradaVirtual();
                EntradaVirtual ape = new EntradaVirtual();
                int verificador = -1;

                ape.Id_Emp = gSession.Id_Emp;
                ape.Id_Cd = gSession.Id_Cd_Ver;
                ape.Id_Env = Id_Env;
                ape.Env_Estatus = estatus;

                CN_EntradaVirtual.EnviarEntradaVirtual(ape, gSession.Emp_Cnx, ref verificador);

                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void editar(GridItem gi)
        {
            try
            {
                RAM1.ResponseScripts.Add("return AbrirVentana_EntradaVirtual('" + gi.Cells[2].Text + "', 3)");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MaximoId(string nomTabla, string nomColumna)
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(gSession.Id_Emp, gSession.Id_Cd_Ver, nomTabla, nomColumna, gSession.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
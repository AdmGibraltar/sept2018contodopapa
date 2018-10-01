using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using CapaDatos;
using System.Globalization;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class ProAdminCapPedido_VentInst : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        //public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        //public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        //public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        //public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }
        public List<PedidoVtaInst> ListPedidoVtaInst
        {
            get { return (List<PedidoVtaInst>)Session[Session.SessionID + "ListPedidoVtaInst"]; }
            set { Session[Session.SessionID + "ListPedidoVtaInst"] = value; }
        }
        public IList<Pedido_Internet> ListPedidoInternet
        {
            get { return (IList<Pedido_Internet>)Session[Session.SessionID + "ListPedidoInternet"]; }
            set { Session[Session.SessionID + "ListPedidoInternet"] = value; }
        }
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
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (session.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                    //rg1.Rebind();
                    //ListPedidoVtaInst = GetList();
                    //rg1.DataSource = ListPedidoVtaInst;
                    //rg1.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


                Session["Sesion" + Session.SessionID] = sesion;
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "new")
                {
                    VentaNueva();
                }
                else if (btn.CommandName == "print")
                {
                    Imprimir(0);
                }
                else if (btn.CommandName == "excel")
                {
                    Imprimir(1);
                }
                else if (btn.CommandName == "rechazar")
                {
                    RechazarLista();
                }

                else if (btn.CommandName == "question")
                {
                    RAM1.ResponseScripts.Add("return AbrirProtocolos()");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                //if (e.RebindReason == GridRebindReason.ExplicitRebind)
                //{
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    rg1.DataSource = ListPedidoVtaInst;
                }
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgInternet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                //if (e.RebindReason == GridRebindReason.ExplicitRebind)
                //{
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    ListPedidoInternet = GetListInternet();
                    rgInternet.DataSource = this.ListPedidoInternet;
                }
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //rgInternet


        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgInternet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgInternet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgInternet_PreRender(object sender, EventArgs e)
        {
            // Edsg 07042015 Desactiva boton agregar pedido
            if (CmbEstatus.SelectedValue != "P")
            {
                rgInternet.MasterTableView.GetColumn("Captar").Display = false;
                rgInternet.MasterTableView.GetColumn("Cancelar").Display = false;

            }
            else
            {
                rgInternet.MasterTableView.GetColumn("Captar").Display = true;
                rgInternet.MasterTableView.GetColumn("Cancelar").Display = true;
            }



        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (TxtAnioIni.Value > TxtAnioFin.Value)
                {
                    Alerta("El Año inicial no puede ser mayor al año final");
                    return;
                }
                else if (TxtAnioIni.Value != null && TxtAnioFin.Value != null)
                {
                    if (this.TxtAnioIni.Value == this.TxtAnioFin.Value)
                    {
                        if (this.TxtSemIni.Value > this.TxtSemFin.Value)
                        {
                            Alerta("El la semana inicial no puede ser mayor a la semana final");
                            return;

                        }
                    }

                }

                if (txtCteIni.Value > txtCteFin.Value)
                {
                    Alerta("El número de cliente inicial no puede ser mayor al número de cliente final");
                    return;
                }
                if (txtTerIni.Value > txtTerFin.Value)
                {
                    Alerta("El número de territorio inicial no puede ser mayor al número de territorio final");
                    return;
                }
                ListPedidoVtaInst = GetList();
                rg1.DataSource = ListPedidoVtaInst;
                rg1.DataBind();
                rgInternet.Rebind();
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
                        rg1.Rebind();
                        rgInternet.Rebind();


                        break;
                }
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
                GridItem gi = e.Item;
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                string Estatus = this.rg1.Items[item]["Acs_Estatus"].Text;
                switch (e.CommandName)
                {
                    case "captar":
                        if (Estatus != "P")
                        {
                            Alerta("Solo se pueden captar pedidos con estatus pendiente");
                        }
                        else
                        {
                            Session["Borrados" + Session.SessionID] = new ArrayList();
                            Session["dtPedVI" + Session.SessionID] = new DataTable();
                            Session["Borrados" + Session.SessionID] = null;
                            Captar(ref e, gi);
                        }
                        break;
                    case "Cancelar":
                        int Id_Acs = Convert.ToInt32(this.rg1.Items[item]["Id_Acs"].Text);
                        int Acs_Anio = Convert.ToInt32(rg1.Items[item]["Acs_Anio"].Text);
                        int Acs_Semana = Convert.ToInt32(rg1.Items[item]["Acs_Semana"].Text);
                        if (Estatus == "X")
                        {
                            Alerta("Ya se encuentra en estatus rechazado");
                        }
                        else if (Estatus == "C")
                        {
                            Alerta("El pedido ya fue captado, no se puede rechazar");
                        }
                        else
                        {
                            RechazarPedidoVI(Id_Acs, Acs_Anio, Acs_Semana);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void rgInternet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                try
                {
                    GridItem gi = e.Item;
                    Int32 item = default(Int32);
                    if (e.Item == null) return;
                    item = e.Item.ItemIndex;
                    // string Estatus = this.rgInternet.Items[item]["Estatus_Id"].Text;

                    switch (e.CommandName)
                    {
                        case "captar":
                            //if (Estatus != "P")
                            //{
                            //    Alerta("Solo se pueden captar pedidos con estatus pendiente");
                            //}
                            //else
                            //{
                            Session["Borrados" + Session.SessionID] = new ArrayList();
                            Session["dtPedVI" + Session.SessionID] = new DataTable();
                            Session["Borrados" + Session.SessionID] = null;
                            CaptarInternet(ref e, gi);
                            //}
                            break;
                        case "Cancelar":

                            int res = 0;
                            int num_pedido = int.Parse(this.rgInternet.Items[item]["Num_Pedido"].Text);

                            var clsCatPedidosInternet = new CN_CapPedido_Internet();
                            clsCatPedidosInternet.Rechazar_Pedido(session.Emp_Cnx, num_pedido, ref res);

                            this.rgInternet.Rebind();

                            break;
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                {
                    GridDataItem dataItem = (GridDataItem)e.Item;
                    int Vigencia = Convert.ToInt32(dataItem["Acs_Vigencia"].Text);

                    if (Vigencia == 0)
                    {
                        dataItem["Id_Cte"].ForeColor = Color.Red;
                        dataItem["Cte_Nom"].ForeColor = Color.Red;
                        dataItem["Id_Ter"].ForeColor = Color.Red;
                        dataItem["Acs_Cantidad"].ForeColor = Color.Red;
                        dataItem["Acs_Semana"].ForeColor = Color.Red;
                        dataItem["Acs_Anio"].ForeColor = Color.Red;
                        dataItem["Acs_EstatusStr"].ForeColor = Color.Red;
                        dataItem["Acs_VigenciaStr"].ForeColor = Color.Red;
                        dataItem["Cte_CreditoLetra"].ForeColor = Color.Red;


                        dataItem["Id_Cte"].Font.Bold = true;
                        dataItem["Cte_Nom"].Font.Bold = true;
                        dataItem["Id_Ter"].Font.Bold = true;
                        dataItem["Acs_Cantidad"].Font.Bold = true;
                        dataItem["Acs_Semana"].Font.Bold = true;
                        dataItem["Acs_Anio"].Font.Bold = true;
                        dataItem["Acs_EstatusStr"].Font.Bold = true;
                        dataItem["Acs_VigenciaStr"].Font.Bold = true;
                        dataItem["Cte_CreditoLetra"].Font.Bold = true;
                    }
                    else if (Vigencia == 1)
                    {
                        dataItem["Id_Cte"].ForeColor = Color.Orange;
                        dataItem["Cte_Nom"].ForeColor = Color.Orange;
                        dataItem["Id_Ter"].ForeColor = Color.Orange;
                        dataItem["Acs_Cantidad"].ForeColor = Color.Orange;
                        dataItem["Acs_Semana"].ForeColor = Color.Orange;
                        dataItem["Acs_Anio"].ForeColor = Color.Orange;
                        dataItem["Acs_EstatusStr"].ForeColor = Color.Orange;
                        dataItem["Acs_VigenciaStr"].ForeColor = Color.Orange;
                        dataItem["Cte_CreditoLetra"].ForeColor = Color.Orange;


                        dataItem["Id_Cte"].Font.Bold = true;
                        dataItem["Cte_Nom"].Font.Bold = true;
                        dataItem["Id_Ter"].Font.Bold = true;
                        dataItem["Acs_Cantidad"].Font.Bold = true;
                        dataItem["Acs_Semana"].Font.Bold = true;
                        dataItem["Acs_Anio"].Font.Bold = true;
                        dataItem["Acs_EstatusStr"].Font.Bold = true;
                        dataItem["Acs_VigenciaStr"].Font.Bold = true;
                        dataItem["Cte_CreditoLetra"].Font.Bold = true;

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }


        protected void rgInternet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //if (e.Item.ItemType == GridItemType.Item)
                //    if (e.Item.FindControl("CaptarImgInternet") != null)
                //    {
                //        if (e.Item.Cells[3].Text == "P")
                //        {
                //            ((ImageButton)e.Item.FindControl("CaptarImgInternet")).Visible = true;
                //            ((ImageButton)e.Item.FindControl("gbcCancelar")).Visible = true;
                //        }
                //        else
                //        {
                //            ((ImageButton)e.Item.FindControl("CaptarImgInternet")).Visible = false;
                //            ((ImageButton)e.Item.FindControl("gbcCancelar")).Visible = false;

                //        }
                //    }
            }
            catch (Exception ex)
            {

            }
        }


        protected void Seleccionar(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem dataItem in rg1.MasterTableView.Items)
                {
                    int Id_Acs = Convert.ToInt32(dataItem["Id_Acs"].Text);
                    int Acs_Anio = Convert.ToInt32(dataItem["Acs_Anio"].Text);
                    int Acs_Semana = Convert.ToInt32(dataItem["Acs_Semana"].Text);
                    bool Estatus = (dataItem.FindControl("chkIncluir") as System.Web.UI.WebControls.CheckBox).Checked;
                    ListPedidoVtaInst.Where(i => (i.Id_Acs == Id_Acs) && (i.Acs_Anio == Acs_Anio) && (i.Acs_Semana == Acs_Semana)).ToList()[0].Seleccionado = Estatus;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void SeleccionarTodos(object sender, EventArgs e)
        {
            try
            {
                System.Web.UI.WebControls.CheckBox headerCheckBox = (sender as System.Web.UI.WebControls.CheckBox);
                int Total1 = ListPedidoVtaInst.Count;
                int Total2 = ListPedidoVtaInst.Where(i => i.Seleccionado == true).Count();
                bool Estatus;
                if (Total1 == Total2) { Estatus = false; } else { Estatus = true; }

                //Si los dos totales son iguales significa que estan todos seleccionados por lo que hay que deseleccionarlos
                foreach (PedidoVtaInst ped in ListPedidoVtaInst)
                {
                    ped.Seleccionado = Estatus;
                }

                rg1.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        #endregion
        #region Funciones
        private void Inicializar()
        {

            Funciones funcion = new Funciones();
            //txtAnio.Value = funcion.GetLocalDateTime(session.Minutos).Year;
            Semana semana = new Semana();
            semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
            semana.Id_Emp = session.Id_Emp;
            semana.Id_Cd = session.Id_Cd_Ver;
            CN_CatSemana cn_semana = new CN_CatSemana();
            TxtAnioIni.Value = funcion.GetLocalDateTime(session.Minutos).Year;
            TxtAnioFin.Value = funcion.GetLocalDateTime(session.Minutos).Year;
            int verificador = 0;
            cn_semana.ConsultaSemanaActual(ref semana, session.Emp_Cnx, ref verificador);
            CargarFiltroGarantias();
            if (verificador > 0 && semana.Id_Sem != 0)
            {
                HD_Anio.Value = funcion.GetLocalDateTime(session.Minutos).Year.ToString();
                HD_Semana.Value = semana.Id_Sem.ToString();
                ListPedidoVtaInst = GetList();
                rg1.DataBind();
                rgInternet.Rebind();
            }
            else
                Alerta("Aun no se han configurado las semanas del periodo actual");

        }
        private void Imprimir(int excel)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                PedidoVtaInst pedido = new PedidoVtaInst();
                double Total = 0;
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Estatus = this.CmbEstatus.SelectedValue == "" ? (string)null : CmbEstatus.SelectedValue;
                pedido.Filtro_Vigencia = this.CmbVigencia.SelectedValue == "" ? (string)null : CmbVigencia.SelectedValue;
                pedido.Filtro_CteIni = txtCteIni.Value.ToString();
                pedido.Filtro_CteFin = txtCteFin.Value.ToString();
                pedido.Filtro_SemIni = TxtSemIni.Value.ToString();
                pedido.Filtro_SemFin = TxtSemFin.Value.ToString();
                pedido.Filtro_AnioIni = TxtAnioIni.Value.ToString();
                pedido.Filtro_AnioFin = TxtAnioFin.Value.ToString();
                pedido.Filtro_TerIni = txtTerIni.Value;
                pedido.Filtro_TerFin = txtTerFin.Value;
                pedido.Filtro_usuario = session.Propia ? session.Id_U.ToString() : "";
                pedido.Id_U = session.Id_Rik == -1 ? (int?)null : session.Id_Rik;

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(pedido.Id_Emp);
                ALValorParametrosInternos.Add(pedido.Id_Cd);
                ALValorParametrosInternos.Add(pedido.Estatus == "" ? "T" : pedido.Estatus);
                ALValorParametrosInternos.Add(pedido.Filtro_Vigencia == null ? "0" : pedido.Filtro_Vigencia);
                ALValorParametrosInternos.Add(pedido.Filtro_CteIni == "" ? "0" : pedido.Filtro_CteIni);
                ALValorParametrosInternos.Add(pedido.Filtro_CteFin == "" ? "0" : pedido.Filtro_CteFin);
                ALValorParametrosInternos.Add(pedido.Filtro_SemIni == "" ? "0" : pedido.Filtro_SemIni);
                ALValorParametrosInternos.Add(pedido.Filtro_SemFin == "" ? "0" : pedido.Filtro_SemFin);
                ALValorParametrosInternos.Add(pedido.Filtro_AnioIni == "" ? "0" : pedido.Filtro_AnioIni);
                ALValorParametrosInternos.Add(pedido.Filtro_AnioFin == "" ? "0" : pedido.Filtro_AnioFin);
                ALValorParametrosInternos.Add(pedido.Filtro_TerIni == null ? 0 : pedido.Filtro_TerIni);
                ALValorParametrosInternos.Add(pedido.Filtro_TerFin == null ? 0 : pedido.Filtro_TerFin);
                ALValorParametrosInternos.Add(pedido.Id_U == null ? 0 : pedido.Id_U);
                ALValorParametrosInternos.Add(this.CmbEstatus.Text);
                ALValorParametrosInternos.Add(this.CmbVigencia.Text);



                Type instance = null;
                instance = typeof(LibreriaReportes.RepPedidoVI);

                if (excel == 0)
                {
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                    //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
                else
                {
                    ImprimirXLS(ALValorParametrosInternos, instance);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ImprimirXLS(ArrayList ALValorParametrosInternos, Type instance)
        {
            try
            {
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", report1, null);
                string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                fs.Flush();
                fs.Close();
                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
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

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                //this.rtb1.Items[1].Visible = false;

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    this.rtb1.Items[7].Visible = false;
                    //Regresar
                    this.rtb1.Items[6].Visible = false;
                    //Eliminar
                    this.rtb1.Items[5].Visible = false;
                    ////Correo
                    //this.rtb1.Items[2].Visible = false;

                    if (session.Id_Rik != -1 || session.Id_TU == 2)
                    { //Captura de pedidos por parte del representante
                        CN_CatCentroDistribucion catcentro = new CN_CatCentroDistribucion();
                        CentroDistribucion cd = new CentroDistribucion();
                        catcentro.ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);

                        if (!cd.Cd_ActivaCapPedRep)
                        {
                            this.rtb1.Items[8].Visible = false;
                            rg1.Columns[9].Visible = false;
                        }
                    }
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<PedidoVtaInst> GetList()
        {
            try
            {
                Funciones Funcion = new Funciones();
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                CN_CapPedidoVtaInst clsCatArea = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst();
                double Total = 0;
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Estatus = this.CmbEstatus.SelectedValue == "" ? (string)null : CmbEstatus.SelectedValue;
                pedido.Filtro_Vigencia = this.CmbVigencia.SelectedValue == "3" ? (string)null : CmbVigencia.SelectedValue;
                pedido.Filtro_CteIni = txtCteIni.Value.ToString();
                pedido.Filtro_CteFin = txtCteFin.Value.ToString();
                pedido.Filtro_SemIni = TxtSemIni.Value.ToString();
                pedido.Filtro_SemFin = TxtSemFin.Value.ToString();
                pedido.Filtro_AnioIni = TxtAnioIni.Value.ToString();
                pedido.Filtro_AnioFin = TxtAnioFin.Value.ToString();
                pedido.Filtro_TerIni = txtTerIni.Value;
                pedido.Filtro_TerFin = txtTerFin.Value;
                pedido.Filtro_Nombre = txtNombre.Text;
                pedido.Filtro_usuario = session.Propia ? session.Id_U.ToString() : "";
                if (this.CmbCredito.SelectedValue == "True")
                {
                    pedido.Filtro_Credito = "0";
                }
                else if (this.CmbCredito.SelectedValue == "False")
                {
                    pedido.Filtro_Credito = "1";
                }


                pedido.Id_U = session.Id_Rik == -1 ? (int?)null : session.Id_Rik;
                string modalidadVenta = null;
                modalidadVenta = rcbTipoGarantia.SelectedValue;
                clsCatArea.Lista(pedido, session.Emp_Cnx, ref List, modalidadVenta);
                CN_CatTipoVenta cnTv = new CN_CatTipoVenta(session);
                foreach (PedidoVtaInst ped in List)
                {
                    Total += ped.Acs_Cantidad;

                    if (ped.Id_TG == 0)
                    {
                        ped.Id_TG = 0;
                        ped.ModalidadGarantia = "Regular"; //cnTv.Tradicional.TV_Nombre;
                    }
                }
                txtTotal.Text = Total.ToString("C2");
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IList<Pedido_Internet> GetListInternet()
        {
            try
            {
                Funciones Funcion = new Funciones();
                IList<Pedido_Internet> List = new List<Pedido_Internet>();
                CN_CapPedido_Internet clsCatPedidosInternet = new CN_CapPedido_Internet();
                Pedido_Internet pedido = new Pedido_Internet();

                if (txtCteIni.Text != "") pedido.P_Cliente_Inicio = Int32.Parse(txtCteIni.Text);
                if (txtCteFin.Text != "") pedido.P_Cliente_Final = Int32.Parse(this.txtCteFin.Text);
                if (txtTerIni.Text != "") pedido.P_Terr_Inicio = Int32.Parse(this.txtTerIni.Text);
                if (txtTerFin.Text != "") pedido.P_Terr_Final = Int32.Parse(this.txtTerFin.Text);

                if (TxtAnioIni.Text != "") pedido.P_Anio_Inicio = Int32.Parse(this.TxtAnioIni.Text);
                if (TxtAnioFin.Text != "") pedido.P_Anio_Final = Int32.Parse(this.TxtAnioFin.Text);

                pedido.P_Estatus = this.CmbEstatus.SelectedValue;
                pedido.P_Nom_Cliente = this.txtNombre.Text;

                double Total = 0;
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;

                clsCatPedidosInternet.ConsultarPedidos(ref List, session.Emp_Cnx, pedido);

                txtTotal.Text = Total.ToString("C2");
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void RechazarPedidoVI(int Id_Acs, int Acs_Anio, int Acs_Semana)
        {
            try
            {
                int verificador = -1;
                CN_CapPedidoVtaInst clsPedidovi = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = Id_Acs;
                pedido.Acs_Anio = Acs_Anio;
                pedido.Acs_Semana = Acs_Semana;
                clsPedidovi.RechazarPedidoVI(pedido, session.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    rg1.Rebind();
                    Alerta("Pedido rechazado exitosamente");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void VentaNueva()
        {
            try
            {
                RAM1.ResponseScripts.Add("return AbrirVentana_ProPedidoVI('', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "', '" + HD_Anio.Value + "'," + HD_Semana.Value + ")");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void CaptarInternet(ref GridCommandEventArgs e, GridItem gi)
        {
            try
            {
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;

                bool CreditoSusp = Convert.ToBoolean(rgInternet.Items[item]["Cte_Credito"].Text);
                if (CreditoSusp == true)
                {
                    Alerta("El cliente tiene crédito suspendido, favor de hacer las gestiones correspondientes para poder captar");
                    e.Canceled = true;
                }
                else
                {
                    ////Edsg 09042015
                    RAM1.ResponseScripts.Add("return AbrirVentana_ProPedido_Internet('" + this.rgInternet.Items[item]["Num_Pedido"].Text + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + rg1.Items[item]["Acs_Anio"].Text + ", '" + rg1.Items[item]["Acs_Semana"].Text + "')");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void Captar(ref GridCommandEventArgs e, GridItem gi)
        {
            try
            {
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                bool CreditoSusp = Convert.ToBoolean(rg1.Items[item]["Cte_Credito"].Text);


                if (CreditoSusp == true)
                {
                    Alerta("El cliente tiene crédito suspendido, favor de hacer las gestiones correspondientes para poder captar");
                    e.Canceled = true;
                }
                else
                {


                    CN_CapAcys cn_capacys = new CN_CapAcys();
                    Acys acys = new Acys();
                    acys.Id_Emp = session.Id_Emp;
                    acys.Id_Cd = session.Id_Cd_Ver;
                    acys.Id_Acs = Convert.ToInt32(this.rg1.Items[item]["Id_Acs"].Text);
                    cn_capacys.Consultar(ref acys, session.Emp_Cnx);
                    if (acys.Acs_Estatus == "B")
                    {
                        Alerta("No se puede captar el pedido, el Acuerdo esta dado de baja");
                        rg1.Rebind();
                        return;
                    }

                    //(gi as GridDataItem).GetDataKeyValue("Id_TG")
                    PedidoVtaInst pedido = gi.DataItem as PedidoVtaInst;
                    int? idTG = (int?)(gi as GridDataItem).GetDataKeyValue("Id_TG");
                    string idTGComponent = "";
                    if (idTG.HasValue)
                    {
                        if (idTG.Value != 0)
                        {
                            idTGComponent = "', '" + idTG.Value;
                        }
                    }
                    RAM1.ResponseScripts.Add("return AbrirVentana_ProPedidoVI('" + this.rg1.Items[item]["Id_Acs"].Text + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + rg1.Items[item]["Acs_Anio"].Text + ", '" + rg1.Items[item]["Acs_Semana"].Text + idTGComponent + "')");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RechazarLista()
        {
            try
            {
                int Verificador = 0;
                CN_CapPedidoVtaInst cn_clsPedidovi;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (ListPedidoVtaInst.Where(i => i.Seleccionado == true).Count() == 0)
                {
                    Alerta("Debe seleccionar al menos un elemento");
                    return;
                }

                foreach (PedidoVtaInst ped in ListPedidoVtaInst)
                {
                    if (ped.Seleccionado == true)
                    {
                        cn_clsPedidovi = new CN_CapPedidoVtaInst();
                        ped.Id_Emp = sesion.Id_Emp;
                        ped.Id_Cd = sesion.Id_Cd_Ver;
                        cn_clsPedidovi.RechazarPedidoVI(ped, sesion.Emp_Cnx, ref Verificador);

                        if (Verificador != 1)
                        {
                            Alerta("Error al tratar de rechazar los pedidos");
                        }

                    }
                }


                if (Verificador == 1)
                {
                    ListPedidoVtaInst = GetList();
                    rg1.Rebind();
                    Alerta("Pedidos rechazados exitosamente");
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GetWeekNumber(DateTime dtPassed)
        {
            try
            {
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                return weekNum.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarFiltroGarantias()
        {
            CN_TipoGarantia cnTg = new CN_TipoGarantia(session);
            var garantias = cnTg.ObtenerTodas();
            rcbTipoGarantia.Items.Clear();
            rcbTipoGarantia.Items.Add(new RadComboBoxItem("--Todos--", "-1"));
            rcbTipoGarantia.Items.Add(new RadComboBoxItem("Tradicional", "0"));
            foreach (var g in garantias)
            {
                rcbTipoGarantia.Items.Add(new RadComboBoxItem(g.TG_Nombre, g.Id_TG.ToString()));
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
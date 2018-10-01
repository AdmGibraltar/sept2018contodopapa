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

namespace SIANWEB
{
    public partial class ProPlaneacionReparto_Admin : System.Web.UI.Page
    {
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

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

                        CargarCentros();
                        Inicializar();
                        ValidarPermisos();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                    }
                }
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                //rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAsignacion.MasterTableView.SortExpressions.AllowMultiColumnSorting = true;
                    GridSortExpression sortExp = new GridSortExpression();
                    sortExp.FieldName = CmbOrdenarPor.SelectedValue;
                    sortExp.SortOrder = GridSortOrder.Ascending;
                    rgAsignacion.MasterTableView.SortExpressions.Clear();
                    rgAsignacion.MasterTableView.SortExpressions.AddSortExpression(sortExp);
                    
                    rgAsignacion.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }

        protected void rgAsignacion_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;

                switch (e.CommandName)
                {
                    case "Editar":

                        RAM1.ResponseScripts.Add("return AbrirVentana_PlaneacionReparto('" + gi.Cells[2].Text + "','" + gi.Cells[6].Text + "','" + gi.Cells[7].Text + "','" + gi.Cells[3].Text + "','" + gi.Cells[5].Text + "','" + gi.Cells[9].Text + "','" + ((RadNumericTextBox)(gi.Cells[21].Controls[1])).Text + "','" + ((RadNumericTextBox)(gi.Cells[20].Controls[1])).Text + "','" + "')");
                        break;
                    case "Delete":
                        CancelarPedido(Int32.Parse(gi.Cells[2].Text));
                        break;
                    case "Guardar":
                        string id_Ped = rgAsignacion.Items[gi.ItemIndex]["Id_Ped"].Text;
                        string ruta = ((RadNumericTextBox)(rgAsignacion.Items[gi.ItemIndex]["Ruta"].FindControl("TxtRuta"))).Text;
                        string sector = ((RadNumericTextBox)(rgAsignacion.Items[gi.ItemIndex]["Sector"].FindControl("TxtSector"))).Text;
                        int secuencia = Convert.ToInt32(((RadNumericTextBox)(rgAsignacion.Items[gi.ItemIndex]["Secuencia"].FindControl("TxtSecuenciaEntrega"))).Text == "" ? "0" : ((RadNumericTextBox)(rgAsignacion.Items[gi.ItemIndex]["Secuencia"].FindControl("TxtSecuenciaEntrega"))).Text);

                        GuardarRuta(id_Ped, ruta, secuencia, sector);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ImbGuardarRutaSector_Click(object sender, EventArgs e)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            for (int x = 0; x < rgAsignacion.Items.Count; x++)
            {

                string id_Ped = rgAsignacion.Items[x]["Id_Ped"].Text;
                string ruta = ((RadNumericTextBox)(rgAsignacion.Items[x]["Ruta"].FindControl("TxtRuta"))).Text;
                string sector = ((RadNumericTextBox)(rgAsignacion.Items[x]["Sector"].FindControl("TxtSector"))).Text;
                int secuencia = Convert.ToInt32(((RadNumericTextBox)(rgAsignacion.Items[x]["Secuencia"].FindControl("TxtSecuenciaEntrega"))).Text == "" ? "0" : ((RadNumericTextBox)(rgAsignacion.Items[x]["Secuencia"].FindControl("TxtSecuenciaEntrega"))).Text);

                int verificador = 0;
                CN_CapPedido pedido = new CN_CapPedido();
                pedido.AsignarRuta(Int32.Parse(id_Ped), sector, ruta, secuencia, session.Emp_Cnx, ref verificador);

                //if (((CheckBox)(rgAsignacion.Items[x]["Seleccionar"].FindControl("ChkSeleccionar"))).Checked)
                //{
                //    int verificador = 0;
                //    CN_CapPedido pedido = new CN_CapPedido();
                //    pedido.AsignarRuta(Int32.Parse(id_Ped), sector, ruta, secuencia, session.Emp_Cnx, ref verificador);
                //}
            }

            Alerta("Se guardo correctamente la Ruta, Sector y Secuencia.");

            rgAsignacion.Rebind();
        }

        protected void ImbAsignar_Click(object sender, EventArgs e)
        {

            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            Funciones funcion = new Funciones();
            CN_ProDesasignaPedido_Aut cn_desasigna = new CN_ProDesasignaPedido_Aut();

            for (int x = 0; x < rgAsignacion.Items.Count; x++)
            {
                
                string id_Ped = rgAsignacion.Items[x]["Id_Ped"].Text;
                string ruta = ((RadNumericTextBox)(rgAsignacion.Items[x]["Ruta"].FindControl("TxtRuta"))).Text;
                string sector = ((RadNumericTextBox)(rgAsignacion.Items[x]["Sector"].FindControl("TxtSector"))).Text;
                int secuencia = Convert.ToInt32(((RadNumericTextBox)(rgAsignacion.Items[x]["Secuencia"].FindControl("TxtSecuenciaEntrega"))).Text == "" ? "0" : ((RadNumericTextBox)(rgAsignacion.Items[x]["Secuencia"].FindControl("TxtSecuenciaEntrega"))).Text);

                if (((CheckBox)(rgAsignacion.Items[x]["Seleccionar"].FindControl("ChkSeleccionar"))).Checked)
                {
                    int verificador = 0;
                    CN_CapPedido pedido = new CN_CapPedido();
                    pedido.AsignarRuta(Int32.Parse(id_Ped), sector, ruta, secuencia, session.Emp_Cnx, ref verificador);
                    cn_desasigna.AsignacionPedido_Aut(session.Id_Emp, session.Id_Cd_Ver, funcion.GetLocalDateTime(session.Minutos), session.Id_U, id_Ped, ref verificador, session.Emp_Cnx);
                }
            }

            rgAsignacion.Rebind();
        }

        protected void ImbDesasingar_Click(object sender, EventArgs e)
        {
            try
            {

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                int verificador = -1;
                CN_ProDesasignaPedido_Aut cn_desasigna = new CN_ProDesasignaPedido_Aut();

                for (int x = 0; x < rgAsignacion.Items.Count; x++)
                {
                    string id_Ped = rgAsignacion.Items[x]["Id_Ped"].Text;

                    if (((CheckBox)(rgAsignacion.Items[x]["Seleccionar"].FindControl("ChkSeleccionar"))).Checked)
                    {
                        cn_desasigna.DesasignacionPedido_Aut(session.Id_Emp, session.Id_Cd_Ver, id_Ped, ref verificador, session.Emp_Cnx);
                    }
                }

                if (verificador != -1)
                {
                    Alerta("Desasignación de pedidos correcta");
                }
                else
                {
                    Alerta("Ocurrió un error al intentar desasignar los pedidos");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            rgAsignacion.Rebind();
        }

        protected void ImbBuscar_Click(object sender, EventArgs e)
        {
            if (TxtFechaInicial.SelectedDate > TxtFechaFinal.SelectedDate)
            {
                Alerta("La fecha de fin debe ser mayor a la fecha de inicio");
                return;
            }

            if (TxtClienteInicial.Value > TxtClienteFinal.Value)
            {
                Alerta("El cliente final debe ser mayor al cliente de inicio");
                return;
            }

            if (TxtPedidoInicial.Value > TxtPedidoFinal.Value)
            {
                Alerta("El pedido de fin debe ser mayor al pedido de inicio");
                return;
            }

            if (TxtRutaInicial.Value > TxtRutaFinal.Value)
            {
                Alerta("La ruta de fin debe ser mayor a la ruta de inicio");
                return;
            }

            if (TxtSectorInicial.Value > TxtSectorFinal.Value)
            {
                Alerta("El sector de fin debe ser mayor al sector de inicio");
                return;
            }

            try
            {

                this.rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ImbProgramaReparto_Click(object sender, EventArgs e)
        {
            PnlRuta.Visible = true;
            ImbProgramaReparto.Visible = false;
        }

        protected void ImbProgramaRepartoPrint_Click(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ArrayList ALValorParametrosInternos = new ArrayList();

            string ids = "";

            for (int x = 0; x < this.rgAsignacion.Items.Count; x++)
            {
                if ((rgAsignacion.Items[x].FindControl("ChkSeleccionar") as CheckBox).Checked)
                {
                    ids = ids + rgAsignacion.Items[x]["Id_Ped"].Text + ",";
                }
            }

            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            ALValorParametrosInternos.Add(ParametroInt(TxtPedidoInicial.Text.Trim()));
            ALValorParametrosInternos.Add(ParametroInt(TxtPedidoFinal.Text.Trim()));
            ALValorParametrosInternos.Add(TxtFechaInicial.SelectedDate);
            ALValorParametrosInternos.Add(TxtFechaFinal.SelectedDate);
            ALValorParametrosInternos.Add(ParametroInt(TxtClienteInicial.Text.Trim()));
            ALValorParametrosInternos.Add(ParametroInt(TxtClienteFinal.Text.Trim()));
            ALValorParametrosInternos.Add(ParametroInt(TxtSectorInicial.Text.Trim()));
            ALValorParametrosInternos.Add(ParametroInt(TxtSectorFinal.Text.Trim()));
            if (TxtRutaFiltro.Text.Trim() != string.Empty)
            {
                ALValorParametrosInternos.Add(TxtRutaFiltro.Text.Trim());
                ALValorParametrosInternos.Add(TxtRutaFiltro.Text.Trim());
                //TxtRutaInicial.Text = TxtRutaFiltro.Text.Trim();
                //TxtRutaFinal.Text = TxtRutaFiltro.Text.Trim();
            }
            else
            {
                ALValorParametrosInternos.Add(ParametroInt(TxtRutaInicial.Text.Trim()));
                ALValorParametrosInternos.Add(ParametroInt(TxtRutaFinal.Text.Trim()));
            }
            ALValorParametrosInternos.Add(ids == string.Empty ? null : ids);

            Type instance = typeof(LibreriaReportes.Rep_ProgramaReparto);

            Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
            Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
            RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");

            PnlRuta.Visible = false;
            ImbProgramaReparto.Visible = true;
        }

        protected int? ParametroInt(string valor)
        {
            if (valor == string.Empty)
            {
                return null;
            }
            else
            {
                return Int32.Parse(valor);
            }
        }

        protected void ImbPickingList_Click(object sender, EventArgs e)
        {
            PnlPickingOpciones.Visible = true;
            ImbPickingList.Visible = false;
        }

        protected void ImbPickingListPrint_Click(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ArrayList ALValorParametrosInternos = new ArrayList();

            string ids = "";

            for (int x = 0; x < this.rgAsignacion.Items.Count; x++)
            {
                if ((rgAsignacion.Items[x].FindControl("ChkSeleccionar") as CheckBox).Checked)
                {
                    ids = ids + rgAsignacion.Items[x]["Id_Ped"].Text + ",";
                }
            }

            if (ids == "")
            {
                Alerta("Debe selecciona al menos un pedido para imprimir.");
                return;
            }
            

            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            ALValorParametrosInternos.Add(ParametroInt(TxtPedidoInicial.Text.Trim()));
            ALValorParametrosInternos.Add(ParametroInt(TxtPedidoFinal.Text.Trim()));
            ALValorParametrosInternos.Add(TxtFechaInicial.SelectedDate);
            ALValorParametrosInternos.Add(TxtFechaFinal.SelectedDate);
            ALValorParametrosInternos.Add(ParametroInt(TxtClienteInicial.Text.Trim()));
            ALValorParametrosInternos.Add(ParametroInt(TxtClienteFinal.Text.Trim()));
            ALValorParametrosInternos.Add(ParametroInt(TxtSectorInicial.Text.Trim()));
            ALValorParametrosInternos.Add(ParametroInt(TxtSectorFinal.Text.Trim()));
            //ALValorParametrosInternos.Add(ParametroInt(TxtRutaInicial.Text.Trim()));
            //ALValorParametrosInternos.Add(ParametroInt(TxtRutaFinal.Text.Trim()));
            if (TxtRuta2Filtro.Text.Trim() != string.Empty)
            {
                ALValorParametrosInternos.Add(TxtRuta2Filtro.Text.Trim());
                ALValorParametrosInternos.Add(TxtRuta2Filtro.Text.Trim());
            }
            else
            {
                ALValorParametrosInternos.Add(ParametroInt(TxtRutaInicial.Text.Trim()));
                ALValorParametrosInternos.Add(ParametroInt(TxtRutaFinal.Text.Trim()));
            }
            ALValorParametrosInternos.Add(ids == string.Empty ? null : ids);

            Type instance;
            if (RblOpcionPrint.SelectedValue == "1")
            {
                instance = typeof(LibreriaReportes.Rep_PickingListCompleto);
            }
            else if (RblOpcionPrint.SelectedValue == "2")
            {
                instance = typeof(LibreriaReportes.Rep_PickingListCliente);
            }
            else
            {
                instance = typeof(LibreriaReportes.Rep_PickingListConcentrado);
            }

            Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
            Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
            RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");

            PnlPickingOpciones.Visible = false;
            ImbPickingList.Visible = true;
        }

        protected void ChkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            for (int x = 0; x < this.rgAsignacion.Items.Count; x++)
            {
                (rgAsignacion.Items[x].FindControl("ChkSeleccionar") as CheckBox).Checked = (sender as CheckBox).Checked;
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
                        //Inicializar();
                        ImbBuscar_Click(null, null);
                        break;
                    case "ok":
                        string status = Session["Ped_Accion" + Session.SessionID].ToString();
                        if (status == "I")
                        {
                            //Imprimir();
                        }
                        else
                        {
                            //Baja();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
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
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Inicializar()
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                TxtFechaInicial.DbSelectedDate = session2.CalendarioIni;
                TxtFechaFinal.DbSelectedDate = session2.CalendarioFin;
                rgAsignacion.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }

        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                this.rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }

        public void CancelarPedido(int id_Ped)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<PedidoDet> list = new List<PedidoDet>();
                PedidoDet det;
                //double? cancelado;
                //foreach (GridDataItem gdi in rgPedido.Items)
                //{
                //    det = new PedidoDet();
                //    det.Id_Prd = Convert.ToInt32(gdi["Id_Prd"].Text);
                //    det.Id_Ter = Convert.ToInt32(gdi["Id_Ter"].Text);
                //    cancelado = (gdi["Cant_cancelado"].FindControl("RadNumericTextBox1") as RadNumericTextBox).Value;
                //    det.Cancelado = cancelado != null ? Convert.ToInt32(cancelado) : 0;
                //    list.Add(det);
                //}

                list = GetListDet(id_Ped);

                foreach (PedidoDet item in list)
                {
                    item.Cancelado = item.Prd_Ord;
                }

                CN_CapPedido cn_cappedido = new CN_CapPedido();
                
                Pedido ped = new Pedido();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Ped = id_Ped;

                int verificador = 0;

                cn_cappedido.BajaParcial(ped, list, sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    Alerta("Las cantidades fueron actualizadas correctamente.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GuardarRuta(string id_Ped, string ruta, int secuencia, string sector)
        {
            Sesion session2 = new Sesion();
            session2 = (Sesion)Session["Sesion" + Session.SessionID];

            int verificador = 0;
            CN_CapPedido pedido = new CN_CapPedido();
            pedido.AsignarRuta(Int32.Parse(id_Ped), sector, ruta, secuencia, session2.Emp_Cnx, ref verificador);

            Alerta("La Ruta, Sector y Secuencia de entrega se guardarón correctamente.");
        }

        private List<Pedido> GetList()
        {
            try
            {
                List<Pedido> List = new List<Pedido>();
                CN_CapPedido clsPedido = new CN_CapPedido();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session2.Id_Emp;
                pedido.Id_Cd = session2.Id_Cd_Ver;
                pedido.Filtro_CteIni = TxtClienteInicial.Value.ToString();
                pedido.Filtro_CteFin = TxtClienteFinal.Value.ToString();
                pedido.Filtro_FecIni = TxtFechaInicial.SelectedDate;
                pedido.Filtro_FecFin = TxtFechaFinal.SelectedDate;
                pedido.Filtro_PedIni = TxtPedidoInicial.Value;
                pedido.Filtro_PedFin = TxtPedidoFinal.Value;
                pedido.Filtro_RutaInicial = TxtRutaInicial.Value;
                pedido.Filtro_RutaFinal = TxtRutaFinal.Value;
                pedido.Filtro_SectorInicial = TxtSectorInicial.Value;
                pedido.Filtro_SectorFinal = TxtSectorFinal.Value;
                pedido.Filtro_Credito = Boolean.Parse(CmbCredito.SelectedValue);
                clsPedido.ConsultaPedidoAsig_Admin(pedido, session2.Emp_Cnx, ref List);

                if (CmbEstatusPedido.SelectedValue == "1")
                {
                    return List.Where(x => x.Ped_Cantidad != x.Ped_Asignado).ToList();
                }
                else if (CmbEstatusPedido.SelectedValue == "2")
                {
                    return List.Where(x => x.Ped_Cantidad == x.Ped_Asignado).ToList();
                }
                else
                {
                    return List;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<PedidoDet> GetListDet(int id_Ped)
        {
            try
            {
                List<PedidoDet> List = new List<PedidoDet>();
                CN_CapPedido cn_CapPedido = new CN_CapPedido();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session2.Id_Emp;
                pedido.Id_Cd = session2.Id_Cd_Ver;
                pedido.Id_Ped = id_Ped;

                cn_CapPedido.ConsultaPedidoAsig(pedido, session2.Emp_Cnx, ref List);
                return List;
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
    }
}
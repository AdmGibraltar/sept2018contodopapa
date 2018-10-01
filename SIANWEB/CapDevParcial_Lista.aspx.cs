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
using System.IO;
using System.Runtime.Remoting;
using System.Collections;

namespace SIANWEB
{
    public partial class CapDevParcial_Lista : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
        #region Eventos
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
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();


                        double ancho = 0;
                        foreach (GridColumn gc in rgDevolucion.Columns)
                        {
                            if (gc.Display && gc.Visible)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgDevolucion.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDevolucion.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {//combos de menu de pantalla
            try
            {

                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= dpFechaini.MinDate && sesion.CalendarioIni <= dpFechaini.MaxDate)
                {
                    dpFechaini.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= dpFechafin.MinDate && sesion.CalendarioFin <= dpFechafin.MaxDate)
                {
                    dpFechafin.DbSelectedDate = sesion.CalendarioFin;
                }

                Session["Sesion" + Session.SessionID] = sesion;
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDevolucion_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {//fuente del grid
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgDevolucion.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDevolucion_ItemCommand(object source, GridCommandEventArgs e)
        {//botones del grid
            try
            {
                GridItem item = e.Item;
                string mensajeError = string.Empty;
                List<string> statusPosibles = new List<string>();
                int validador = 0;
                switch (e.CommandName)
                {
                    case "Eliminar":
                        baja(ref e, ref statusPosibles, ref item);
                        validador = 1;
                        //}
                        //else
                        //    this.Alerta("No tiene permiso para dar de baja los registros");
                        break;
                    case "Imprimir":
                        mensajeError = "CapFactura_print_error";
                        if (_PermisoImprimir)
                            Imprimir(ref e, ref statusPosibles);
                        else
                            this.Alerta("No tiene permisos para imprimir");
                        break;

                    case "Modificar":
                        ///El movimiento solo se podrá modificar siempre y cuando se encuentre en estatus C capturado y 
                        ///la entrada sea tipo de movimiento que no sea automático.
                        Modificar(ref e, ref statusPosibles, ref item);
                        validador = 1;
                        break;
                }
                if (validador == 1)
                    this.rgDevolucion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgDevolucion_ItemCommand");
            }
        }
        protected void rgDevolucion_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {//cambio de paginas
            try
            {
                ErrorManager();
                this.rgDevolucion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDevolucion_ItemDataBound(object sender, GridItemEventArgs e)
        {//boton de grid agregar
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string clickHandler = string.Empty;
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {//barra de botones
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "Modificar":
                        Sesion Sesion = new Sesion();
                        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        DateTime fecha = DateTime.Now;
                        if ((fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))//validar fecha dentro del periodo
                        {  //solo se muestra la info del movimiento pero no se permite modificar     
                            RAM1.ResponseScripts.Add("return AbrirVentana_Pagos('0','-1','" + /*permisoGuardar*/_PermisoGuardar
                                + "','" + /*permisoModificar*/ _PermisoModificar + "','" + /*permisoEliminar*/ _PermisoEliminar + "','"
                                + /*permisoImprimir*/_PermisoImprimir + "')");
                        }
                        else
                            Alerta("La fecha se encuentra fuera del periodo");
                        break;

                    case "Buscar":
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
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
                        rgDevolucion.Rebind();
                        break;
                    default:
                        rgDevolucion.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtClienteini.Value > txtClientefin.Value)
                {
                    Alerta("El número de cliente inicial no debe ser mayor al número de cliente final");
                    return;
                }
                if (dpFechaini.SelectedDate > dpFechafin.SelectedDate)
                {
                    Alerta("La fecha inicial no debe ser mayor a la fecha final");
                    return;
                }
                if (txtDevini.Value > txtDevfin.Value)
                {
                    Alerta("El número de devolución inicial no debe ser mayor al número de devolución final");
                    return;
                }
                try
                {
                    this.rgDevolucion.Rebind();
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
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion.CalendarioIni >= dpFechaini.MinDate && sesion.CalendarioIni <= dpFechaini.MaxDate)
                {
                    dpFechaini.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= dpFechafin.MinDate && sesion.CalendarioFin <= dpFechafin.MaxDate)
                {
                    dpFechafin.DbSelectedDate = sesion.CalendarioFin;
                }
                rgDevolucion.Rebind();
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
        private void Nuevo()
        {
            try
            {
                txtClienteini.Text = string.Empty;
                txtClientefin.Text = string.Empty;
                txtNombre.Text = string.Empty;
                dpFechaini.DateInput.Text = string.Empty;
                dpFechafin.DateInput.Text = string.Empty;
                dpFechaini.Clear();
                dpFechafin.Clear();
                txtDevini.Text = string.Empty;
                txtDevfin.Text = string.Empty;
                cmbEstatus.SelectedIndex = 0;
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
        private List<DevParcial> GetList()
        {
            try
            {
                List<DevParcial> List = new List<DevParcial>();
                CN_DevParcial_Lista clsDevParcial = new CN_DevParcial_Lista();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DevParcial devParcial = new DevParcial();

                devParcial.Filtro_Nombre = txtNombre.Text;
                devParcial.Filtro_Id_Cte = txtClienteini.Text;
                devParcial.Filtro_Id_Cte2 = txtClientefin.Text;
                devParcial.Filtro_Id_Devini = txtDevini.Text;
                devParcial.Filtro_Id_Devfin = txtDevfin.Text;
                devParcial.Filtro_FecIni = dpFechaini.SelectedDate.HasValue ? dpFechaini.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                devParcial.Filtro_FecFin = dpFechafin.SelectedDate.HasValue ? dpFechafin.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                devParcial.Filtro_Estatus = cmbEstatus.SelectedValue;
                if (!string.IsNullOrEmpty(devParcial.Filtro_Estatus))
                    if (devParcial.Filtro_Estatus == "-1" || devParcial.Filtro_Estatus == "1")
                        devParcial.Filtro_Estatus = string.Empty;
                clsDevParcial.ConsultaDevParcial(session2.Id_Emp, session2.Id_Cd_Ver, session2.Emp_Cnx, devParcial, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Modificar(ref GridCommandEventArgs e, ref List<string> statusPosibles, ref GridItem item)
        {
            statusPosibles = new List<string>() { "C" };
            List<string> statusPosibles2 = new List<string>() { "Sí" };
            if (!statusPosibles.Contains(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Es_Estatus"].Text.ToUpper()))
            {
                int id = Convert.ToInt32(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Id_Nca"].Text);
                int fac = Convert.ToInt32(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Factura"].Text);
                //Alerta("La devolución parcial se encuentra en estatus no válido para realizar la modificación");
                RAM1.ResponseScripts.Add("OpenAlert('La devolución parcial se encuentra en estatus no válido para realizar la modificación','" + id + "','" + fac + "'," + 1 + ");");
                e.Canceled = true;
                return;
            }//Nca_Pagos
            if (statusPosibles2.Contains(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Nca_Pagos"].Text.ToUpper()))
            {
                int id = Convert.ToInt32(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Id_Nca"].Text);
                int fac = Convert.ToInt32(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Factura"].Text);
                string mensaje = "La factura de la devolución seleccionada ya tiene pagos realizados no es posible realizar una modificación";
                RAM1.ResponseScripts.Add("OpenAlert('" + mensaje + "','" + id + "','" + fac + "'," + 1 + ");");
                e.Canceled = true;
                return;
            }
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DateTime fecha = new DateTime();
            fecha = Convert.ToDateTime(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Fecha"].Text);
            if ((fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))
            {//solo se muestra la info del movimiento pero no se permite modificar                   
                int id = Convert.ToInt32(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Id_Nca"].Text);
                int fac = Convert.ToInt32(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Factura"].Text);
                RAM1.ResponseScripts.Add("return AbrirVentana_Pagos('" + id + "','" + fac + "','" + /*permisoGuardar*/_PermisoGuardar
                    + "','" + /*permisoModificar*/ _PermisoModificar + "','" + /*permisoEliminar*/ _PermisoEliminar + "','"
                    + /*permisoImprimir*/_PermisoImprimir + "'," + 1 + ")");
            }
            else
            {
                int id = Convert.ToInt32(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Id_Nca"].Text);
                int fac = Convert.ToInt32(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Factura"].Text);
                //Alerta("La devolución parcial se encuentra en estatus no válido para realizar la modificación");
                RAM1.ResponseScripts.Add("OpenAlert('La fecha se encuentra fuera del período','" + id + "','" + fac + "'," + 1 + ");");
                //Alerta("La fecha se encuentra fuera del período");
                e.Canceled = true;
            }
        }
        private void Imprimir(ref GridCommandEventArgs e, ref List<string> statusPosibles)
        {
            try
            {   ///El movimiento solo se podrá imprimir siempre y cuando se encuentre en estatus C capturado, e I de impreso.
                ///Lo manda a imprimir, y se cambia el estatus a impreso.
                statusPosibles = new List<string>() { "C", "I" };
                if (!statusPosibles.Contains(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Es_Estatus"].Text.ToUpper()))
                {
                    Alerta("El pedido se encuentra en estatus no válido");
                    e.Canceled = true;
                    return;
                }
                GridItem gi = Session["Pedido" + Session.SessionID] as GridItem;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                DevParcial devParcial = new DevParcial();
                List<DevParcial> List = new List<DevParcial>();
                DevParcial devParcial2 = new DevParcial();
                ArrayList ALValorParametrosInternos = new ArrayList();
                CN_DevParcialDetalle cls = new CN_DevParcialDetalle();

                devParcial.Id_Nca = int.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Id_Nca"].Text);
                devParcial.Factura = int.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Factura"].Text);
                cls.ConsultarEncabezadoImprimir(sesion, devParcial, ref devParcial2);

                ALValorParametrosInternos.Add(devParcial2.TipoMov);
                ALValorParametrosInternos.Add(devParcial2.Estatus);
                ALValorParametrosInternos.Add(devParcial2.Id_Nca);
                ALValorParametrosInternos.Add(devParcial2.Id_Nca2);
                ALValorParametrosInternos.Add(devParcial2.Id_Ter);
                ALValorParametrosInternos.Add(devParcial2.Id_Rik);
                ALValorParametrosInternos.Add(devParcial2.Fecha);
                ALValorParametrosInternos.Add(devParcial2.Factura);
                ALValorParametrosInternos.Add(devParcial2.Num_Cliente);
                ALValorParametrosInternos.Add(devParcial2.Cliente);
                ALValorParametrosInternos.Add(devParcial2.Cte_FacCalle1);
                ALValorParametrosInternos.Add(devParcial2.Nca_Subtotal);
                ALValorParametrosInternos.Add(devParcial2.Nca_Iva);
                ALValorParametrosInternos.Add(devParcial2.Nca_Total);
                ALValorParametrosInternos.Add(devParcial2.DatoFactura);
                //parametros para el cuerpo del reporte
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd);
                //conexion
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                Type instance = null;
                instance = typeof(LibreriaReportes.DevParcial_Imprimir);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void baja(ref GridCommandEventArgs e, ref List<string> statusPosibles, ref GridItem item)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DateTime fecha = new DateTime();
                fecha = Convert.ToDateTime(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Fecha"].Text);
                //validar que sea tipo impreso o capturado
                statusPosibles = new List<string>() { "I", "C" };
                if (!statusPosibles.Contains(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Es_Estatus"].Text.ToUpper()))
                {
                    Alerta("La devolución parcial se encuentra en estatus no válido para realizar la baja");
                    e.Canceled = true;
                    return;
                }
                //validar fecha dentro del periodo
                if (!(Sesion.CalendarioIni <= fecha && fecha <= Sesion.CalendarioFin))
                {
                    Alerta("La fecha se encuentra fuera del período");
                    e.Canceled = true;
                    return;
                }
                //Si es un movimiento de entrada va a checar si se tiene disponible 
                //suficiente (inventario final menos asignado).            
                DevParcial devParcial = new DevParcial();
                devParcial.Num_Cliente = int.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Num_Cliente"].Text);
                devParcial.Id_Nca = int.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Id_Nca"].Text);
                devParcial.Id_Nca2 = int.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Id_Nca2"].Text);
                devParcial.Es_Estatus = rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Es_Estatus"].Text;
                devParcial.Fecha = DateTime.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Fecha"].Text);
                devParcial.Tipo = int.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Tipo"].Text);
                devParcial.Factura = int.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Factura"].Text);
                devParcial.Nca_Subtotal = double.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Nca_Subtotal"].Text);
                devParcial.Nca_Iva = double.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Nca_Iva"].Text);
                devParcial.Nca_Total = double.Parse(rgDevolucion.MasterTableView.Items[e.Item.ItemIndex]["Nca_Total"].Text);

                int verificador = 0;
                new CN_DevParcialDetalle().EliminarDevParcial(Sesion, devParcial, ref verificador);

                if (verificador == 1)
                    Alerta("La devolución parcial se dio de baja correctamente");
                else
                    Alerta("No se pudo de dar de baja la devolución parcial");

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
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
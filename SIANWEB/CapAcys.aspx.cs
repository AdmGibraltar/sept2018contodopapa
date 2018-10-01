using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using System.Globalization;
using Telerik.Web.UI;
using System.Collections;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Telerik.Reporting.Processing;
using System.IO;
using System.Web.UI.HtmlControls;
using CapaModelo;

namespace SIANWEB
{
    public partial class CapAcys : System.Web.UI.Page
    {
        #region Variables

        private Acys Acys_Or
        {
            get
            {
                return (Acys)Session["dtAcys_Or" + Session.SessionID];
            }
            set
            {
                Session["dtAcys_Or" + Session.SessionID] = value;
            }
        }

        private DataTable dtAcuerdos_Or
        {
            get
            {
                return (DataTable)Session["dtAcuerdos_Or" + Session.SessionID];
            }
            set
            {
                Session["dtAcuerdos_Or" + Session.SessionID] = value;
            }
        }


        private List<Asesoria> List_Asesoria_Or
        {
            get { return (List<Asesoria>)Session["Servicios" + Session.SessionID]; }
            set { Session["Servicios" + Session.SessionID] = value; }
        }

        private List<Producto> List_ServicioTec_Or
        {
            get { return (List<Producto>)Session["List_ServicioTec_Or" + Session.SessionID]; }
            set { Session["List_ServicioTec_Or" + Session.SessionID] = value; }
        }


        private List<Producto> List_ServMtto_Or
        {
            get { return (List<Producto>)Session["List_ServMtto_Or" + Session.SessionID]; }
            set { Session["List_ServMtto_Or" + Session.SessionID] = value; }
        }

        private string _UsuarioLog
        {
            get
            {
                return (string)Session[_UsuarioLog];
            }
            set
            {
                Session["_UsuarioLog" + Session.SessionID] = value;
            }

        }

        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }

        private DataTable dtAcuerdos
        {
            get
            {
                return (DataTable)Session["dtAcuerdos" + Session.SessionID];
            }
            set
            {
                Session["dtAcuerdos" + Session.SessionID] = value;
            }
        }

        private DataTable dtAcuerdos_Kilo
        {
            get
            {
                return (DataTable)Session["dtAcuerdos_Kilo" + Session.SessionID];
            }
            set
            {
                Session["dtAcuerdos_Kilo" + Session.SessionID] = value;
            }
        }

        private DataTable dtAcuerdos_Comensal
        {
            get
            {
                return (DataTable)Session["dtAcuerdos_Comensal" + Session.SessionID];
            }
            set
            {
                Session["dtAcuerdos_Comensal" + Session.SessionID] = value;
            }
        }

        private DataTable dtAcuerdos_Habitacion
        {
            get
            {
                return (DataTable)Session["dtAcuerdos_Habitacion" + Session.SessionID];
            }
            set
            {
                Session["dtAcuerdos_Habitacion" + Session.SessionID] = value;
            }
        }

        private DataTable dtAcuerdos_Iguala
        {
            get
            {
                return (DataTable)Session["dtAcuerdos_Iguala" + Session.SessionID];
            }
            set
            {
                Session["dtAcuerdos_Iguala" + Session.SessionID] = value;
            }
        }



        private int _Accion
        {
            get
            {
                return (int)Session["SesionAccion" + Session.SessionID];
            }
            set
            {
                Session["SesionAccion" + Session.SessionID] = value;
            }

        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        bool producto = false;
        private DataTable Seleccionados
        {
            get { return (DataTable)Session["SeleccionadosAcys" + Session.SessionID]; }
            set { Session["SeleccionadosAcys" + Session.SessionID] = value; }
        }
        private List<Producto> List_Productos
        {
            //get { return (List<Producto>)Session["Servicios" + Session.SessionID]; }
            //set { Session["Servicios" + Session.SessionID] = value; }

            get { return (List<Producto>)Session["List_Productos" + Session.SessionID]; }
            set { Session["List_Productos" + Session.SessionID] = value; }
        }

        private List<Producto> List_ProductosMantenimiento
        {
            get { return (List<Producto>)Session["ServiciosMantenimiento" + Session.SessionID]; }
            set { Session["ServiciosMantenimiento" + Session.SessionID] = value; }
        }

        public List<ClienteDetGarantia> listaGarantia
        {
            get
            {
                if (HttpContext.Current.Session["listaGarantia"] == null)
                {
                    HttpContext.Current.Session["listaGarantia"] = new List<ClienteDetGarantia>();
                }
                return HttpContext.Current.Session["listaGarantia"] as List<ClienteDetGarantia>;
            }
            set
            {
                HttpContext.Current.Session["listaGarantia"] = value;
            }
        }


        public List<Semana> listSemana
        {
            get
            {
                if (HttpContext.Current.Session["listSemana"] == null)
                {
                    HttpContext.Current.Session["listSemana"] = new List<Semana>();
                }
                return HttpContext.Current.Session["listSemana"] as List<Semana>;
            }
            set
            {
                HttpContext.Current.Session["listSemana"] = value;
            }
        }

        private bool EsInicio { get; set; }

        private bool IniciaCalendario { get; set; }

        private bool modoEdicion0 { get; set; }
        private bool modoEdicion1 { get; set; }
        private bool modoEdicion2 { get; set; }
        private bool modoEdicion3 { get; set; }
        private bool modoEdicion4 { get; set; }
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
                    CerrarVentana();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);

                }
                else
                    if (!Page.IsPostBack)
                    {
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();

                        if (Request.QueryString["Id"].ToString() != "-1") ObtieneGarantias();
                        rdFecha.DbSelectedDate = DateTime.Now;
                        rdFechaInicioDocumento.Enabled = true;
                        rdFechaFinDocumento.Enabled = false;

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();


                        if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(divGenerales.Controls);
                            deshabilitarcontroles(divAcuerdosE.Controls);
                            GridCommandItem cmdItem = (GridCommandItem)rgAcuerdos.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 
                            ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  

                            rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 1].Display = false;
                            rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 2].Display = false;

                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgAcuerdos.Columns)
                        {
                            if (gc.Display && gc.Visible)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        if (rgAcuerdos.Items.Count > 6)
                        {
                            ancho += 17;
                        }
                        rgAcuerdos.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgAcuerdos.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        if (txtCliente.Text != "")
                        {
                            List_Productos = GetListServicios();
                            rgServicios.Rebind();
                            List_ProductosMantenimiento = GetListServiciosMantenimiento();

                            rgMantPrevRev.Rebind();
                        }


                        if (List_ServicioTec_Or == null)
                        {
                            List_ServicioTec_Or = GetListServicios();
                        }

                        if (List_ServMtto_Or == null)
                        {
                            List_ServMtto_Or = GetListServiciosMantenimiento();
                        }


                        rgAsesoria.Rebind();
                    }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = false;
                        break;
                }
                if (Type.Contains("System.Web.UI.WebControls.CheckBox"))
                {
                    (controles_contenidos[x] as System.Web.UI.WebControls.CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }
        }



        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                /* foreach (object o in Validators)
                 {
                     if (!((IValidator)o).IsValid)
                     {
                         Alerta(((Control)o).ID);
                     }

                 }*/

                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                        Guardar();
                }
                if (btn.CommandName == "Bitacora")
                {
                    if (Page.IsValid)
                        Bitacora();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Bitacora()
        {

            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                int Id_Acys = Convert.ToInt32(txtFolio.Value);
                int Id_Cd = sesion.Id_Cd;
                string Pantalla = "Acuerdos Comerciales y Servicios";



                RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Bitacora('", Id_Cd, "','", Id_Acys, "','", Pantalla, "')"));

                //RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Bitacora('", Id_Emp, "','", Id_Cd, "')"));

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void cmbRepresentante_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                AcysPrevio();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirBuscarDireccionEntrega();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void imgKilo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirCalendario(1);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void imgComensal_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirCalendario(2);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void imgHabitacion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirCalendario(3);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void imgIguala_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirCalendario(4);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }




        protected void RadDatePicker2_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                RadDatePicker objFecha = sender as RadDatePicker;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DateTime fechaInicioPeriodo = sesion.CalendarioIni;
                DateTime fechaFinPeriodo = sesion.CalendarioFin;

                if (fechaInicioPeriodo > Convert.ToDateTime(objFecha.SelectedDate))
                {
                    RAM1.ResponseScripts.Add("radalert('La fecha inicio de Vigencia no puede ser menor al periodo actual', 330, 150);");
                    objFecha.DbSelectedDate = null;
                    return;
                }

                if (Convert.ToDateTime(rdFechaInicioDocumento.SelectedDate) > Convert.ToDateTime(objFecha.SelectedDate))
                {
                    RAM1.ResponseScripts.Add("radalert('La fecha inicio de Vigencia no puede ser a la fecha de inicio del Acuerdo', 330, 150);");
                    objFecha.DbSelectedDate = null;
                    return;
                }

                if (Convert.ToInt32(Convert.ToDateTime(objFecha.SelectedDate).DayOfWeek) != 1)
                {
                    RAM1.ResponseScripts.Add("radalert('La fecha inicio de Vigencia tiene que ser lunes', 330, 150);");
                    objFecha.DbSelectedDate = null;
                    return;
                }

                DateTime nextweek = GetNextWeekday(Convert.ToDateTime(objFecha.SelectedDate), DayOfWeek.Monday);

                if (Convert.ToDateTime(objFecha.SelectedDate) < fechaFinPeriodo && _Accion == 4)
                {
                    RAM1.ResponseScripts.Add("radalert('La fecha inicio de Vigencia tiene que ser lunes del próximo periodo', 330, 150);");
                    objFecha.DbSelectedDate = null;
                    return;
                }
                int verificador = 0;
                //txtSemana.Text = GetWeekNumber(rdVigenciaIni.SelectedDate.Value);
                //se cambia la manera de obtener la semana desde la tabla catsemana
                Semana semana = new Semana();
                semana.Sem_FechaAct = rdVigenciaIni.SelectedDate.Value;
                semana.Id_Emp = 1;
                semana.Id_Cd = sesion.Id_Cd_Ver;
                CN_CatSemana CNSem = new CN_CatSemana();
                CNSem.ConsultaSemanaActual(ref semana, sesion.Emp_Cnx, ref verificador);
                txtSemana.Text = semana.Id_Sem.ToString();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
        protected void cmbTer_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                txtTerritorio.Text = cmbTerritorio.SelectedValue;
                AcysPrevio();
                CargarRik();
                CargarSegmento();
                //CargarClientes();

                this.ObtieneGarantias();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Boolean EsGarantia = false;

                RadNumericTextBox cmbProd = sender as RadNumericTextBox;
                Producto prd = new Producto();

                if (cmbProd.Parent.ClientID.Contains("Kilo") || cmbProd.Parent.ClientID.Contains("Iguala") || cmbProd.Parent.ClientID.Contains("Habitacion") || cmbProd.Parent.ClientID.Contains("Comensal"))
                    EsGarantia = true;


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatProducto cnProducto = new CN_CatProducto();
                try
                {
                    cnProducto.ConsultaProducto(ref prd, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, Convert.ToInt32(cmbProd.Value.HasValue ? cmbProd.Value.Value : -1), 0);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, (sender as RadNumericTextBox).ClientID);
                    return;
                }
                (cmbProd.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = prd.Prd_Descripcion;
                (cmbProd.Parent.FindControl("lblUniEd") as Label).Text = prd.Prd_UniNs;//(cmbProd.SelectedItem.FindControl("lblUni") as Label).Text;
                (cmbProd.Parent.FindControl("lblPresentacionEd") as Label).Text = prd.Prd_Presentacion;//(cmbProd.SelectedItem.FindControl("lblPre") as Label).Text;
                if (!EsGarantia)
                {
                    (cmbProd.Parent.FindControl("txtPrecio") as RadNumericTextBox).DbValue = prd.Prd_Precio; //(cmbProd.SelectedItem.FindControl("lblPrecio") as Label).Text;
                    (cmbProd.Parent.FindControl("txtPrecio") as RadNumericTextBox).Focus();
                }
                else
                {
                    (cmbProd.Parent.FindControl("txtPrecio") as RadNumericTextBox).DbValue = 0.00;
                    (cmbProd.Parent.FindControl("txtCantidad") as RadNumericTextBox).Focus();
                }


                if (prd.Prd_Descripcion == null)
                {

                    AlertaFocus("El producto no existe o esta deshabilitado", (sender as RadNumericTextBox).ClientID);
                    return;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region EventosGrid

        // Acuerdos
        protected void rgAcuerdos_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgAcuerdos.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro.");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        EsInicio = true;

                        break;
                    case "Update":
                        Update(e);
                        EsInicio = true;
                        break;
                    case "Delete":
                        Delete(e);
                        EsInicio = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAcuerdos_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    LinkButton imgButton = (LinkButton)item.FindControl("hlEquivalencias");
                    string id_Prd = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"].ToString();
                    string Id_Acs = txtFolio.Text == "" ? "-1" : txtFolio.Text;
                    // imgButton.OnClientClick = "return popup2('" + id_Prd + "', '" + Id_Acs + "')";
                }
                catch (Exception)
                {

                }
            }
        }

        protected void rgAcuerdos_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgAcuerdos.DataSource = dtAcuerdos;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Enabled = false;
                }
            }

            if (e.Item is GridEditableItem)
            {
                Label lblSubtotal = ((Label)((GridDataItem)e.Item)["SubTotal"].FindControl("lblSubtotal"));

                if (lblSubtotal.Text != "")
                    sumaSub += double.Parse(lblSubtotal.Text);
            }


            if (e.Item is GridFooterItem)
            {
                ((Label)((GridFooterItem)e.Item)["SubTotal"].FindControl("lblPiePrecio")).Text = "Total: " + sumaSub.ToString("N2");
            }
        }

        protected void rgAcuerdos_PreRender(object sender, EventArgs e)
        {
            if (EsInicio)
                if (modoEdicion0)
                    this.PintarCalendario((RadGrid)sender);
        }
        // Acuerdos Kilo

        protected void rgAcuerdos_Kilo_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgAcuerdos.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro.");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        EsInicio = true;

                        break;
                    case "Update":
                        Update(e);
                        EsInicio = true;
                        break;
                    case "Delete":
                        Delete(e);
                        EsInicio = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Kilo_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    LinkButton imgButton = (LinkButton)item.FindControl("hlEquivalencias");

                    //if (EsGarantia())
                    //    ((RadNumericTextBox)item.FindControl("txtPrecio")).Visible = false;
                    //else
                    //    ((RadNumericTextBox)item.FindControl("txtPrecio")).Visible = true;

                    string id_Prd = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"].ToString();
                    string Id_Acs = txtFolio.Text == "" ? "-1" : txtFolio.Text;
                    imgButton.OnClientClick = "return popup2('" + id_Prd + "', '" + Id_Acs + "')";


                }
                catch (Exception)
                {

                }
            }
        }

        protected void rgAcuerdos_Kilo_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    this.rgAcuerdos_Kilo.DataSource = dtAcuerdos_Kilo;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Kilo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Enabled = false;
                }
                ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Focus();
            }

            if (e.Item is GridEditableItem)
            {
                //Label lblSubtotal = ((Label)((GridDataItem)e.Item)["SubTotal"].FindControl("lblSubtotal"));

                //if (lblSubtotal.Text != "")
                //    sumaSub += double.Parse(lblSubtotal.Text);
            }


            if (e.Item is GridFooterItem)
            {
                // ((Label)((GridFooterItem)e.Item)["SubTotal"].FindControl("lblPiePrecio")).Text = "Total: " + sumaSub.ToString("N2");
            }
        }

        protected void rgAcuerdos_Kilo_PreRender(object sender, EventArgs e)
        {
            if (EsInicio)
                if (modoEdicion1)
                    this.PintarCalendario((RadGrid)sender);
        }



        protected void rgAcuerdos_Comensal_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgAcuerdos.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro.");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        EsInicio = true;

                        break;
                    case "Update":
                        Update(e);
                        EsInicio = true;
                        break;
                    case "Delete":
                        Delete(e);
                        EsInicio = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Comensal_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    LinkButton imgButton = (LinkButton)item.FindControl("hlEquivalencias");

                    //if (EsGarantia())
                    //    ((RadNumericTextBox)item.FindControl("txtPrecio")).Visible = false;
                    //else
                    //    ((RadNumericTextBox)item.FindControl("txtPrecio")).Visible = true;

                    string id_Prd = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"].ToString();
                    string Id_Acs = txtFolio.Text == "" ? "-1" : txtFolio.Text;
                    imgButton.OnClientClick = "return popup2('" + id_Prd + "', '" + Id_Acs + "')";


                }
                catch (Exception)
                {

                }
            }
        }

        protected void rgAcuerdos_Comensal_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    this.rgAcuerdos_Comensal.DataSource = dtAcuerdos_Comensal;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Comensal_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Enabled = false;
                }
                ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Focus();
            }

            if (e.Item is GridEditableItem)
            {
                Label lblSubtotal = ((Label)((GridDataItem)e.Item)["SubTotal"].FindControl("lblSubtotal"));

                if (lblSubtotal.Text != "")
                    sumaSub += double.Parse(lblSubtotal.Text);
            }


            if (e.Item is GridFooterItem)
            {
                // ((Label)((GridFooterItem)e.Item)["SubTotal"].FindControl("lblPiePrecio")).Text = "Total: " + sumaSub.ToString("N2");
            }

        }

        protected void rgAcuerdos_Comensal_PreRender(object sender, EventArgs e)
        {
            if (EsInicio)
                if (modoEdicion2)
                    this.PintarCalendario((RadGrid)sender);
        }

        protected void rgAcuerdos_Habitacion_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgAcuerdos.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro.");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        EsInicio = true;

                        break;
                    case "Update":
                        Update(e);
                        EsInicio = true;
                        break;
                    case "Delete":
                        Delete(e);
                        EsInicio = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgAcuerdos_Habitacion_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    LinkButton imgButton = (LinkButton)item.FindControl("hlEquivalencias");



                    //if (EsGarantia())
                    //    ((RadNumericTextBox)item.FindControl("txtPrecio")).Visible = false;
                    //else
                    //    ((RadNumericTextBox)item.FindControl("txtPrecio")).Visible = true;



                    string id_Prd = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"].ToString();
                    string Id_Acs = txtFolio.Text == "" ? "-1" : txtFolio.Text;
                    imgButton.OnClientClick = "return popup2('" + id_Prd + "', '" + Id_Acs + "')";






                }
                catch (Exception)
                {

                }
            }
        }









        protected void rgAcuerdos_Habitacion_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    this.rgAcuerdos_Habitacion.DataSource = dtAcuerdos_Habitacion;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Habitacion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Enabled = false;
                }
                ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Focus();
            }

            if (e.Item is GridEditableItem)
            {
                Label lblSubtotal = ((Label)((GridDataItem)e.Item)["SubTotal"].FindControl("lblSubtotal"));

                if (lblSubtotal.Text != "")
                    sumaSub += double.Parse(lblSubtotal.Text);
            }


            if (e.Item is GridFooterItem)
            {
                // ((Label)((GridFooterItem)e.Item)["SubTotal"].FindControl("lblPiePrecio")).Text = "Total: " + sumaSub.ToString("N2");
            }
        }

        protected void rgAcuerdos_Habitacion_PreRender(object sender, EventArgs e)
        {
            if (EsInicio)
                if (modoEdicion3)
                    this.PintarCalendario((RadGrid)sender);
        }

        protected void rgAcuerdos_Iguala_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgAcuerdos.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro.");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        EsInicio = true;

                        break;
                    case "Update":
                        Update(e);
                        EsInicio = true;
                        break;
                    case "Delete":
                        Delete(e);
                        EsInicio = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Iguala_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    LinkButton imgButton = (LinkButton)item.FindControl("hlEquivalencias");

                    //if (EsGarantia())
                    //    ((RadNumericTextBox)item.FindControl("txtPrecio")).Visible = false;
                    //else
                    //    ((RadNumericTextBox)item.FindControl("txtPrecio")).Visible = true;

                    string id_Prd = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"].ToString();
                    string Id_Acs = txtFolio.Text == "" ? "-1" : txtFolio.Text;
                    imgButton.OnClientClick = "return popup2('" + id_Prd + "', '" + Id_Acs + "')";


                }
                catch (Exception)
                {

                }
            }
        }

        protected void rgAcuerdos_Iguala_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    this.rgAcuerdos_Iguala.DataSource = dtAcuerdos_Iguala;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Iguala_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Enabled = false;
                }
                ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Focus();
            }

            if (e.Item is GridEditableItem)
            {
                Label lblSubtotal = ((Label)((GridDataItem)e.Item)["SubTotal"].FindControl("lblSubtotal"));

                if (lblSubtotal.Text != "")
                    sumaSub += double.Parse(lblSubtotal.Text);
            }


            if (e.Item is GridFooterItem)
            {
                // ((Label)((GridFooterItem)e.Item)["SubTotal"].FindControl("lblPiePrecio")).Text = "Total: " + sumaSub.ToString("N2");
            }
        }

        protected void rgAcuerdos_Iguala_PreRender(object sender, EventArgs e)
        {
            if (EsInicio)
                if (modoEdicion4)
                    this.PintarCalendario((RadGrid)sender);
        }




        #endregion
        protected void rgServicios_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgServicios.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsertService(e);
                        break;
                    case "Update":
                        UpdateService(e);
                        break;
                    case "Delete":
                        DeleteService(e);
                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgMantPrevRev_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgMantPrevRev.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsertMantenimiento(e);
                        break;
                    case "Update":
                        UpdateMantenimiento(e);
                        break;
                    case "Delete":
                        DeleteMantenimiento(e);
                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void cmbProducto_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (producto)
                    return;
                producto = true;
                RadComboBox cmbProducto = sender as RadComboBox;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto cn_catproducto = new CN_CatProducto();
                List<Producto> list = new List<Producto>();
                Producto prd = new Producto();
                prd.Id_Cd = sesion.Id_Cd_Ver;
                prd.Id_Emp = sesion.Id_Emp;
                int Id_Acs = HF_ID.Value == "" ? -1 : Convert.ToInt32(HF_ID.Value);
                cn_catproducto.ConsultaProductoCte_Lista(prd, sesion.Emp_Cnx, Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1), Id_Acs, ref list);

                cmbProducto.DataSource = list;
                cmbProducto.DataValueField = "Id_Prd";
                cmbProducto.DataTextField = "Prd_Descripcion";
                cmbProducto.DataBind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cte = new Clientes();
                cte.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1);
                cte.Id_Emp = sesion.Id_Emp;
                cte.Id_Cd = sesion.Id_Cd_Ver;
                cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    txtClienteNombre.Text = "";
                    txtCliente.Text = "";
                    if (cmbTerritorio.Items.Count > 0)
                    {
                        cmbTerritorio.SelectedIndex = 1;
                        cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                        txtTerritorio.Text = cmbTerritorio.Items[1].Text;

                    } //txtTerritorio.Value = null;
                    Limpiar();
                    return;
                }

                txtClienteNombre.Text = cte.Cte_NomComercial;
                txtClienteDireccion.Text = cte.Cte_FacCalle;
                txtClienteColonia.Text = cte.Cte_FacColonia;
                txtClienteMunicipio.Text = cte.Cte_FacMunicipio;
                txtClienteEstado.Text = cte.Cte_FacEstado;
                txtClienteRFC.Text = cte.Cte_FacRfc;
                txtClienteCodPost.Text = cte.Cte_FacCp;

                CheckCuentaCorporativa.Checked = cte.Id_Corp > 1 ? true : false;
                ChkbAdendaSI.Checked = cte.Id_Ade > 0 ? true : false;


                //Información Comercial  
                txtComercial.Text = cte.Cte_NomComercial;
                txtDireccionEntrega.Text = cte.Cte_Calle;
                txtClienteColoniaE.Text = cte.Cte_Colonia;
                txtClienteMunicipioE.Text = cte.Cte_Municipio;
                txtClienteEstadoE.Text = cte.Cte_Estado;
                txtClienteCPE.Text = cte.Cte_Cp;
                //txtProveedor.Text = cte.;
                txtContacto.Text = cte.Cte_Contacto;
                //txtPuesto.Text = cte.ct;
                txtTelefono.Text = cte.Cte_Telefono;
                txtEmail.Text = cte.Cte_Email;

                this.IdCte_DirEntrega.Value = "0";


                cmbAsignacion.SelectedIndex = cmbAsignacion.FindItemIndexByValue(cte.Cte_AsignacionPed.ToString());
                cmbAsignacion.Text = cmbAsignacion.FindItemByValue(cte.Cte_AsignacionPed.ToString()) == null ? "" : cmbAsignacion.FindItemByValue(cte.Cte_AsignacionPed.ToString()).Text;

                if (cte.Id_TCte <= 0)
                {
                    txtIdTipoCliente.Text = "";
                }
                else
                {
                    txtIdTipoCliente.Text = cte.Id_TCte.ToString();
                    //cmbTipoCliente_SelectedIndexChanged(null, null);
                }

                cmbTipoCliente.SelectedIndex = cmbTipoCliente.FindItemByValue(cte.Id_TCte.ToString()) == null ? 0 : cmbTipoCliente.FindItemIndexByValue(cte.Id_TCte.ToString());
                cmbTipoCliente.Text = cmbTipoCliente.FindItemByValue(cte.Id_TCte.ToString()) == null ? cmbTipoCliente.Items[0].Text : cmbTipoCliente.FindItemByValue(cte.Id_TCte.ToString()).Text;


                CargarTerritorios();
                if (txtCliente.Text != "")
                {
                    CargarCondiciones();
                    List_Productos = GetListServicios();
                    rgServicios.Rebind();
                    //aki
                    List_ProductosMantenimiento = GetListServiciosMantenimiento();
                    rgMantPrevRev.Rebind();

                }

                AcysPrevio();
                cte.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1);
                cte.Id_Emp = sesion.Id_Emp;
                cte.Id_Cd = sesion.Id_Cd_Ver;


                //EDSG 14102015
                this.ObtieneGarantias();

                //if (EsGarantia())
                //{
                //    lblModOp.Visible = true;
                //    cboModalidadOPAcys.Visible = true;

                //    lblFactorGarantía.Visible = true;
                //    txtfactorGarantia.Visible = true;

                //    lblUltFechaCorte.Visible = true;
                //    rdFechaUltCorte.Visible = true;
                //}
                //else
                //{
                //    lblModOp.Visible = false;
                //    cboModalidadOPAcys.Visible = false;

                //    lblFactorGarantía.Visible = false;
                //    txtfactorGarantia.Visible = false;

                //    lblUltFechaCorte.Visible = false;
                //    rdFechaUltCorte.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void ObtieneGarantias()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            ClienteDet clienteDet = new ClienteDet();
            clienteDet.Id_Emp = sesion.Id_Emp;
            clienteDet.Id_Cd = sesion.Id_Cd;
            clienteDet.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1);
            clienteDet.Id_Ter = Convert.ToInt32(this.txtTerritorio.Value.HasValue ? txtTerritorio.Value : -1);

            CN_CatCliente cntCliente = new CN_CatCliente();
            listaGarantia = cntCliente.ConsultarClienteTerr_EsGarantia(clienteDet, sesion.Emp_Cnx);

            MostrarGrids();
        }
        double sumaSub = 0;

        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("popup();");
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
                ErrorManager();
                switch (e.Argument.ToString())
                {
                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                    case "direccion":
                        //txtIdCte.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        //txtClienteNombre.Text = Session["ClienteNombre_Bucsar" + Session.SessionID].ToString();
                        //txtIdCte_TextChanged(null, null);
                        CN_CatCliente clsCliente = new CN_CatCliente();
                        ClienteDirEntrega cliente = new ClienteDirEntrega();
                        Sesion session2 = new Sesion();
                        session2 = (Sesion)Session["Sesion" + Session.SessionID];
                        cliente.Id_Emp = session2.Id_Emp;
                        cliente.Id_Cd = session2.Id_Cd_Ver;
                        cliente.Id_CteDirEntrega = Int32.Parse(Session["Id_Buscar" + Session.SessionID].ToString()) - 1;
                        cliente.Id_Cte = Int32.Parse(Session["Descripcion_Buscar" + Session.SessionID].ToString());
                        clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx);
                        this.txtDireccionEntrega.Text = cliente.Cte_Calle;
                        this.txtClienteCPE.Text = cliente.Cte_Cp.Trim();
                        this.txtClienteColoniaE.Text = cliente.Cte_Colonia;
                        this.txtClienteMunicipioE.Text = cliente.Cte_Municipio;
                        this.txtClienteEstadoE.Text = cliente.Cte_Estado;
                        IdCte_DirEntrega.Value = cliente.Id_CteDirEntrega.ToString();
                        break;

                    case "productos":
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 90);


                        RadPageCliente.Height = altura;
                        RadSplitter1.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageCliente.Width;


                        RPVRecepcionPedido.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RPVRecepcionPedido.Width;

                        RPVAcuerdosEconomicos.Height = altura;
                        RadSplitter3.Height = altura;
                        RadPane3.Height = altura;
                        RadPane3.Width = RPVAcuerdosEconomicos.Width;


                        RPVCondicionesPago.Height = altura;
                        RadSplitter4.Height = altura;
                        RadPane4.Height = altura;
                        RadPane4.Width = RPVCondicionesPago.Width;



                        RPVServicio.Height = altura;
                        RadSplitter5.Height = altura;
                        RadPane5.Height = altura;
                        RadPane5.Width = RPVServicio.Width;

                        RPVOtrosApoyos.Height = altura;
                        RadSplitter6.Height = altura;
                        RadPane6.Height = altura;
                        RadPane6.Width = RPVOtrosApoyos.Width;


                        txtCliente.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAsesoria_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAsesoria.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgServicios_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgServicios.DataSource = List_Productos;
                    //
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgServicios_ItemDeleted(object source, GridDeletedEventArgs e)
        {

        }

        protected void rgMantPrevRev_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //List_ServiciosMantenimiento_Or = List_ProductosMantenimiento;

                    rgMantPrevRev.DataSource = List_ProductosMantenimiento;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void cargarCboUsuarios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (_Accion == 0 || _Accion == 1)
                {
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("%ASESOR%"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoAseServ);
                    cboUsuario_SelectedIndexChanged(this.ContactoAseServ, new RadComboBoxSelectedIndexChangedEventArgs(null, null, null, null));

                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("Almacen"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCAlmRep);
                    cboUsuario_SelectedIndexChanged(this.ContactoCAlmRep, new RadComboBoxSelectedIndexChangedEventArgs(null, null, null, null));
                    //Cobranza
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("Cobranza"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCCreCob);
                    cboUsuario_SelectedIndexChanged(this.ContactoCCreCob, new RadComboBoxSelectedIndexChangedEventArgs(null, null, null, null));

                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("Servicios de Valor"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCServTec);
                    cboUsuario_SelectedIndexChanged(this.ContactoCServTec, new RadComboBoxSelectedIndexChangedEventArgs(null, null, null, null));

                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("Servicio y operaciones"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoJefOper);
                    cboUsuario_SelectedIndexChanged(this.ContactoJefOper, new RadComboBoxSelectedIndexChangedEventArgs(null, null, null, null));

                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("Jefe de Servicio al Cliente"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoJefServ);
                    cboUsuario_SelectedIndexChanged(this.ContactoJefServ, new RadComboBoxSelectedIndexChangedEventArgs(null, null, null, null));

                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("Representante de Servicio al Cliente"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoRepServ);
                    cboUsuario_SelectedIndexChanged(this.ContactoRepServ, new RadComboBoxSelectedIndexChangedEventArgs(null, null, null, null));

                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("%RIK%"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoRepVenta);
                    cboUsuario_SelectedIndexChanged(this.ContactoRepVenta, new RadComboBoxSelectedIndexChangedEventArgs(null, null, null, null));

                }
                else
                {
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoAseServ);
                    this.ContactoAseServ.SelectedValue = "0";
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCAlmRep);
                    this.ContactoCAlmRep.SelectedValue = "0";
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCCreCob);
                    this.ContactoCCreCob.SelectedValue = "0";
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCServTec);
                    this.ContactoCServTec.SelectedValue = "0";
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoJefOper);
                    this.ContactoJefOper.SelectedValue = "0";
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoJefServ);
                    this.ContactoJefServ.SelectedValue = "0";
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoRepServ);
                    this.ContactoRepServ.SelectedValue = "0";
                    CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, GetIdTipoUsuarioXNombre("RSC/ASESOR/RIK"), Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoRepVenta);
                    this.ContactoRepVenta.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cboUsuario_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatUsuario clsCatUsuario = new CN_CatUsuario();
                string Correo = "";
                Usuario usuario = new Usuario();
                RadComboBox objUsuario = o as RadComboBox;
                usuario.Id_Cd_Ver = sesion.Id_Cd_Ver;
                usuario.Id_Cd = sesion.Id_Cd_Ver;
                usuario.Id_Emp = sesion.Id_Emp;

                RadTextBox emailTextBox = (objUsuario.Parent.FindControl(objUsuario.ID + "Email") as RadTextBox);

                if (objUsuario.SelectedValue != string.Empty)
                {
                    if (Convert.ToInt32(objUsuario.SelectedValue) > 0)
                    {
                        usuario.Id_U = Convert.ToInt32(objUsuario.SelectedValue);
                        clsCatUsuario.ConsultaCorreoUsuario(usuario, sesion.Emp_Cnx, ref Correo);

                    }
                }
                emailTextBox.Text = Correo;

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        private void EnviaEmail(int Id_Acys)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = -1;
                Acys acys = new Acys();
                acys.Id_Emp = session.Id_Emp;
                acys.Id_Cd = session.Id_Cd_Ver;
                acys.Id_Acs = Id_Acys;

                CN_CapAcys cn_acys = new CN_CapAcys();
                cn_acys.ConsultaUltimaVersion(ref acys, sesion.Emp_Cnx);
                cn_acys.Consultar(ref acys, sesion.Emp_Cnx);

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("La solicitud  de acuerdo comercial con el número #" + Id_Acys + " ha sido atendida.");

                cuerpo_correo.Append(", de la sucursal " + session.Id_Cd_Ver);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "CapAcys_Admin.aspx'" + ">");
                cuerpo_correo.Append("Solicitud de autorización de acuerdos comerciales</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(ConsultarEmail(acys.Id_U)));
                m.Subject = "Confirmación de autorización de Acuerdo Comercial #" + Id_Acys + " del centro " + session.Id_Cd_Ver;
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
                try
                {
                    sm.Send(m);
                }
                catch (Exception)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void ExportToPDF(int Id_Acs)
        {
            Type instance = null;
            instance = typeof(LibreriaReportes.AcuerdoImpresion);
            Funciones funcion = new Funciones();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_CapAcys clsCapAcys = new CN_CapAcys();
            Acys acys = new Acys();
            acys.Id_Emp = sesion.Id_Emp;
            acys.Id_Cd = sesion.Id_Cd_Ver;
            acys.Id_Acs = Convert.ToInt32(Id_Acs);
            clsCapAcys.ConsultaUltimaVersion(ref acys, sesion.Emp_Cnx);

            Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
            CultureInfo cultura = CultureInfo.CurrentCulture;

            ArrayList ALValorParametrosInternos = new ArrayList();

            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
            ALValorParametrosInternos.Add(Id_Acs);

            ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos).ToString("dd"));
            ALValorParametrosInternos.Add(cultura.TextInfo.ToTitleCase(funcion.GetLocalDateTime(sesion.Minutos).ToString("MMMM")));
            ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos).ToString("yyyy"));
            ALValorParametrosInternos.Add(acys.Id_AcsVersion);

            for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
            {
                report1.ReportParameters[i].AllowNull = true;
                report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
            }
            ReportProcessor reportProcessor = new ReportProcessor();
            RenderingResult result = reportProcessor.RenderReport("PDF", report1, null);
            string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + "_" + Id_Acs + ".pdf";
            if (File.Exists(ruta))
                File.Delete(ruta);
            FileStream fs = new FileStream(ruta, FileMode.Create);
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);

            fs.Flush();
            fs.Close();

        }




        private string ConsultarEmail(int id_u)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario u = new Usuario();
            u.Id_Emp = session.Id_Emp;
            u.Id_Cd = session.Id_Cd_Ver;
            u.Id_U = id_u;
            string correo = "";
            cn_catusuario.ConsultaCorreoUsuario(u, session.Emp_Cnx, ref correo);
            return correo;
        }

        protected void Rb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rdb = sender as RadioButton;
                RadDatePicker dpGeneric = (rdb.Parent.FindControl(rdb.ID + "fechaIni") as RadDatePicker);
                dpGeneric.Enabled = true;
                string type = "";

                ControlCollection td = rdb.Parent.Controls as ControlCollection;

                foreach (Control control in td)
                {
                    if (control.GetType().ToString() == "Telerik.Web.UI.RadDatePicker")
                    {
                        if (control.ID != rdb.ID + "fechaIni")
                        {
                            dpGeneric = (rdb.Parent.FindControl(control.ID) as RadDatePicker);
                            dpGeneric.Enabled = false;
                            dpGeneric.DbSelectedDate = null;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void RbModalidad_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rdb = sender as RadioButton;
                switch (rdb.ID)
                {
                    case "rdModFrencuenciaEstablecida":
                        showColumsAcuerdoEconomico("A");
                        break;

                    case "rdModOrdenAbierta":
                        showColumsAcuerdoEconomico("B");
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void showColumsAcuerdoEconomico(string Modalidad)
        {

            switch (Modalidad)
            {
                case "A":
                    rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 5].Display = false;
                    rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 6].Display = false;
                    rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 7].Display = false;
                    lAnexo.Text = "ANEXO A";


                    break;

                case "B":
                    rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 5].Display = true;
                    rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 6].Display = true;
                    rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 7].Display = true;
                    lAnexo.Text = "ANEXO B";

                    break;
            }


        }


        protected void ChkServAsesoria_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AsesoriaListado.Visible = ChkServAsesoria.Checked;

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ChkServTecnicoRelleno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EquipoRellenoListado.Visible = ChkServTecnicoRelleno.Checked;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void ChkServMantenimiento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MantenimientoPreventivoListado.Visible = ChkServMantenimiento.Checked;


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ValidarFechaInicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

            RadDatePicker objFecha = sender as RadDatePicker;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DateTime fechaInicioPeriodo = sesion.CalendarioIni;

            if (fechaInicioPeriodo > Convert.ToDateTime(objFecha.SelectedDate))
            {
                RAM1.ResponseScripts.Add("radalert('La fecha inicio no puede ser menor al periodo actual', 330, 150);");
                objFecha.DbSelectedDate = null;
                return;
            }



        }



        protected void DeleteMantenimiento(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
            int Prd_AgrupadoSpo = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_AgrupadoSpo"]);
            List_ProductosMantenimiento.Remove(List_ProductosMantenimiento.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray()[0]);
        }

        protected void DeleteService(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
            int Prd_AgrupadoSpo = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_AgrupadoSpo"]);
            List_Productos.Remove(List_Productos.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray()[0]);
        }




        protected void PerformInsertService(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32((item.FindControl("txtCodigoEdit") as RadNumericTextBox).Value);
            int Prd_AgrupadoSpo = Convert.ToInt32((item.FindControl("txtAgrupadoSpoEdit") as RadNumericTextBox).Value);
            string Prd_Descripcion = (item.FindControl("txtProductoEdit") as RadTextBox).Text;
            int Cantidad = Convert.ToInt32((item.FindControl("txtCantidadEdit") as RadNumericTextBox).Value);

            DateTime? FechaInicioMensual;
            DateTime? FechaInicioBimestral;
            DateTime? FechaInicioTrimestral;

            bool Mensual = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoMensualEdit") as RadioButton).Checked);
            FechaInicioMensual = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoMensualEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateMensual = (item.FindControl("ServTecnicoRellenoMensualEditfechaIni") as RadDatePicker);


            bool Bimestral = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoBimestralEdit") as RadioButton).Checked);
            FechaInicioBimestral = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateBimestral = (item.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker);

            bool Trimestral = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoTrimestralEdit") as RadioButton).Checked);
            FechaInicioTrimestral = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateTrimestral = (item.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker);



            if (Cantidad < 1)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar un numero mayor a 0 en cantidad");
                return;
            }

            if (!DateBimestral.SelectedDate.HasValue && !DateTrimestral.SelectedDate.HasValue && !DateMensual.SelectedDate.HasValue)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar una fecha");
                return;

            }

            if (List_Productos.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray().Length > 0)
            {
                Alerta("El producto ya ha sido agregado");
                return;
            }
            else
            {
                Producto prd = new Producto();
                prd.Id_Prd = Id_Prd;
                prd.Prd_AgrupadoSpo = Prd_AgrupadoSpo;
                prd.Prd_Descripcion = Prd_Descripcion;
                prd.Prd_InvInicial = Cantidad;
                prd.ServTecnicoRellenoMensual = Mensual;
                prd.ServTecnicoRellenoBimestral = Bimestral;
                prd.ServTecnicoRellenoTrimestral = Trimestral;

                if (Mensual)
                {
                    DateTrimestral.DbSelectedDate = null;
                    DateBimestral.DbSelectedDate = null;
                    prd.ServTecnicoRellenoMensualfechaIni = FechaInicioMensual;
                }

                if (Bimestral)
                {
                    DateTrimestral.DbSelectedDate = null;
                    DateMensual.DbSelectedDate = null;

                    prd.ServTecnicoRellenoBimestralfechaIni = FechaInicioBimestral;
                }


                if (Trimestral)
                {
                    DateBimestral.DbSelectedDate = null;
                    DateMensual.DbSelectedDate = null;
                    prd.ServTecnicoRellenoTrimestralfechaIni = FechaInicioTrimestral;
                }
                List_Productos.Add(prd);
            }

        }


        protected void PerformInsertMantenimiento(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32((item.FindControl("txtCodigoEdit") as RadNumericTextBox).Value);
            int Prd_AgrupadoSpo = Convert.ToInt32((item.FindControl("txtAgrupadoSpoEdit") as RadNumericTextBox).Value);
            string Prd_Descripcion = (item.FindControl("txtProductoEdit") as RadTextBox).Text;
            int Cantidad = Convert.ToInt32((item.FindControl("txtCantidadEdit") as RadNumericTextBox).Value);

            DateTime? FechaInicioMensual;
            DateTime? FechaInicioBimestral;
            DateTime? FechaInicioTrimestral;

            bool Mensual = Convert.ToBoolean((item.FindControl("ServMantenimientoMensualEdit") as RadioButton).Checked);
            FechaInicioMensual = Convert.ToDateTime((item.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateMensual = (item.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker);
            bool Bimestral = Convert.ToBoolean((item.FindControl("ServMantenimientoBimestralEdit") as RadioButton).Checked);
            FechaInicioBimestral = Convert.ToDateTime((item.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateBimestral = (item.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker);
            bool Trimestral = Convert.ToBoolean((item.FindControl("ServMantenimientoTrimestralEdit") as RadioButton).Checked);
            FechaInicioTrimestral = Convert.ToDateTime((item.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateTrimestral = (item.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker);

            if (Cantidad < 1)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar un numero mayor a 0 en cantidad");
                return;
            }

            if (!DateMensual.SelectedDate.HasValue && !DateBimestral.SelectedDate.HasValue && !DateTrimestral.SelectedDate.HasValue)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar una fecha");
                return;
            }


            if (List_ProductosMantenimiento.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray().Length > 0)
            {
                Alerta("El producto ya ha sido agregado");
                return;
            }
            else
            {
                Producto prd = new Producto();
                prd.Id_Prd = Id_Prd;
                prd.Prd_AgrupadoSpo = Prd_AgrupadoSpo;
                prd.Prd_Descripcion = Prd_Descripcion;
                prd.Prd_InvInicial = Cantidad;


                prd.ServMantenimientoMensual = Mensual;
                prd.ServMantenimientoBimestral = Bimestral;
                prd.ServMantenimientoTrimestral = Trimestral;


                if (Mensual)
                {
                    DateTrimestral.DbSelectedDate = null;
                    DateBimestral.DbSelectedDate = null;
                    prd.ServMantenimientoMensualfechaIni = FechaInicioMensual;
                }

                if (Bimestral)
                {
                    DateMensual.DbSelectedDate = null;
                    DateTrimestral.DbSelectedDate = null;
                    prd.ServMantenimientoBimestralfechaIni = FechaInicioBimestral;
                }


                if (Trimestral)
                {
                    DateMensual.DbSelectedDate = null;
                    DateBimestral.DbSelectedDate = null;
                    prd.ServMantenimientoTrimestralfechaIni = FechaInicioTrimestral;
                }

                List_ProductosMantenimiento.Add(prd);
            }

        }


        protected void UpdateMantenimiento(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32((item.FindControl("txtCodigoEdit") as RadNumericTextBox).Value);
            int Prd_AgrupadoSpo = Convert.ToInt32((item.FindControl("txtAgrupadoSpoEdit") as RadNumericTextBox).Value);
            string Prd_Descripcion = (item.FindControl("txtProductoEdit") as RadTextBox).Text;
            int Cantidad = Convert.ToInt32((item.FindControl("txtCantidadEdit") as RadNumericTextBox).Value);
            DateTime? FechaInicioMensual;
            DateTime? FechaInicioBimestral;
            DateTime? FechaInicioTrimestral;

            bool Mensual = Convert.ToBoolean((item.FindControl("ServMantenimientoMensualEdit") as RadioButton).Checked);
            FechaInicioMensual = Convert.ToDateTime((item.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateMensual = (item.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker);
            bool Bimestral = Convert.ToBoolean((item.FindControl("ServMantenimientoBimestralEdit") as RadioButton).Checked);
            FechaInicioBimestral = Convert.ToDateTime((item.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateBimestral = (item.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker);

            bool Trimestral = Convert.ToBoolean((item.FindControl("ServMantenimientoTrimestralEdit") as RadioButton).Checked);
            FechaInicioTrimestral = Convert.ToDateTime((item.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateTrimestral = (item.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker);

            if (Cantidad < 1)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar un numero mayor a 0 en cantidad");
                return;
            }

            if (!DateMensual.SelectedDate.HasValue && !DateBimestral.SelectedDate.HasValue && !DateTrimestral.SelectedDate.HasValue)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar una fecha");
                return;
            }

            if (List_ProductosMantenimiento.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray().Length > 0)
            {
                List_ProductosMantenimiento.Remove(List_ProductosMantenimiento.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray()[0]);
            }

            Producto prd = new Producto();
            prd.Id_Prd = Id_Prd;
            prd.Prd_AgrupadoSpo = Prd_AgrupadoSpo;
            prd.Prd_Descripcion = Prd_Descripcion;

            prd.Prd_InvInicial = Cantidad;
            prd.ServMantenimientoMensual = Mensual;
            prd.ServMantenimientoBimestral = Bimestral;
            prd.ServMantenimientoTrimestral = Trimestral;


            if (Mensual)
            {
                DateTrimestral.DbSelectedDate = null;
                DateBimestral.DbSelectedDate = null;
                prd.ServMantenimientoMensualfechaIni = FechaInicioMensual;
            }

            if (Bimestral)
            {
                DateMensual.DbSelectedDate = null;
                DateTrimestral.DbSelectedDate = null;
                prd.ServMantenimientoBimestralfechaIni = FechaInicioBimestral;
            }


            if (Trimestral)
            {
                DateMensual.DbSelectedDate = null;
                DateBimestral.DbSelectedDate = null;
                prd.ServMantenimientoTrimestralfechaIni = FechaInicioTrimestral;
            }

            List_ProductosMantenimiento.Add(prd);
        }




        protected void UpdateService(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32((item.FindControl("txtCodigoEdit") as RadNumericTextBox).Value);
            int Prd_AgrupadoSpo = Convert.ToInt32((item.FindControl("txtAgrupadoSpoEdit") as RadNumericTextBox).Value);
            string Prd_Descripcion = (item.FindControl("txtProductoEdit") as RadTextBox).Text;
            int Cantidad = Convert.ToInt32((item.FindControl("txtCantidadEdit") as RadNumericTextBox).Value);
            //int Revision = Convert.ToInt32((item.FindControl("txtRevisionEdit") as RadNumericTextBox).Value);

            DateTime? FechaInicioMensual;
            DateTime? FechaInicioBimestral;
            DateTime? FechaInicioTrimestral;

            bool Mensual = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoMensualEdit") as RadioButton).Checked);
            FechaInicioMensual = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoMensualEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateMensual = (item.FindControl("ServTecnicoRellenoMensualEditfechaIni") as RadDatePicker);

            bool Bimestral = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoBimestralEdit") as RadioButton).Checked);
            FechaInicioBimestral = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateBimestral = (item.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker);


            bool Trimestral = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoTrimestralEdit") as RadioButton).Checked);
            FechaInicioTrimestral = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateTrimestral = (item.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker);

            if (Cantidad < 1)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar un numero mayor a 0 en cantidad");
                return;
            }

            if (!DateBimestral.SelectedDate.HasValue && !DateTrimestral.SelectedDate.HasValue && !DateMensual.SelectedDate.HasValue)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar una fecha");
                return;
            }

            if (List_Productos.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray().Length > 0)
            {
                List_Productos.Remove(List_Productos.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray()[0]);
            }

            Producto prd = new Producto();
            prd.Id_Prd = Id_Prd;
            prd.Prd_AgrupadoSpo = Prd_AgrupadoSpo;
            prd.Prd_Descripcion = Prd_Descripcion;
            prd.Prd_InvInicial = Cantidad;
            prd.ServTecnicoRellenoMensual = Mensual;
            prd.ServTecnicoRellenoBimestral = Bimestral;
            prd.ServTecnicoRellenoTrimestral = Trimestral;


            if (Mensual)
            {
                DateTrimestral.DbSelectedDate = null;
                DateBimestral.DbSelectedDate = null;
                prd.ServTecnicoRellenoMensualfechaIni = FechaInicioMensual;
            }

            if (Bimestral)
            {
                DateTrimestral.DbSelectedDate = null;
                DateMensual.DbSelectedDate = null;
                prd.ServTecnicoRellenoBimestralfechaIni = FechaInicioBimestral;
            }


            if (Trimestral)
            {
                DateBimestral.DbSelectedDate = null;
                DateMensual.DbSelectedDate = null;
                prd.ServTecnicoRellenoTrimestralfechaIni = FechaInicioTrimestral;
            }
            List_Productos.Add(prd);
        }
        protected void rgServicios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("txtCodigoEdit")).Enabled = false;

                    bool Mensual = Convert.ToBoolean((editItem.FindControl("ServTecnicoRellenoMensualEdit") as RadioButton).Checked);
                    RadDatePicker DateMensual = (editItem.FindControl("ServTecnicoRellenoMensualEditfechaIni") as RadDatePicker);

                    bool Bimestral = Convert.ToBoolean((editItem.FindControl("ServTecnicoRellenoBimestralEdit") as RadioButton).Checked);
                    RadDatePicker DateBimestral = (editItem.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker);

                    bool Trimestral = Convert.ToBoolean((editItem.FindControl("ServTecnicoRellenoTrimestralEdit") as RadioButton).Checked);
                    RadDatePicker DateTrimestral = (editItem.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker);


                    if (Mensual)
                    {
                        DateMensual.Enabled = true;
                        DateTrimestral.DbSelectedDate = null;
                        DateBimestral.DbSelectedDate = null;

                    }

                    if (Bimestral)
                    {
                        DateBimestral.Enabled = true;
                        DateTrimestral.DbSelectedDate = null;
                        DateMensual.DbSelectedDate = null;

                    }


                    if (Trimestral)
                    {
                        DateTrimestral.Enabled = true;
                        DateBimestral.DbSelectedDate = null;
                        DateMensual.DbSelectedDate = null;
                    }

                }
            }
        }


        protected void rgMantPrevRev_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("txtCodigoEdit")).Enabled = false;
                    bool Mensual = Convert.ToBoolean((editItem.FindControl("ServMantenimientoMensualEdit") as RadioButton).Checked);
                    RadDatePicker DateMensual = (editItem.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker);

                    bool Bimestral = Convert.ToBoolean((editItem.FindControl("ServMantenimientoBimestralEdit") as RadioButton).Checked);
                    RadDatePicker DateBimestral = (editItem.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker);
                    bool Trimestral = Convert.ToBoolean((editItem.FindControl("ServMantenimientoTrimestralEdit") as RadioButton).Checked);
                    RadDatePicker DateTrimestral = (editItem.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker);

                    if (Mensual)
                    {
                        DateMensual.Enabled = true;
                        DateBimestral.DbSelectedDate = null;
                        DateTrimestral.DbSelectedDate = null;
                    }

                    if (Bimestral)
                    {
                        DateBimestral.Enabled = true;
                        DateMensual.DbSelectedDate = null;
                        DateTrimestral.DbSelectedDate = null;
                    }


                    if (Trimestral)
                    {
                        DateTrimestral.Enabled = true;
                        DateBimestral.DbSelectedDate = null;
                        DateMensual.DbSelectedDate = null;
                    }

                }
            }
        }
        #endregion
        #region Funciones
        private List<Asesoria> GetList()
        {
            try
            {
                List<Asesoria> List = new List<Asesoria>();
                CN_CapAcys clsCapAcys = new CN_CapAcys();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Acys acys = new Acys();
                acys.Id_Emp = session2.Id_Emp;
                acys.Id_Cd = session2.Id_Cd_Ver;
                acys.Id_Acs = (int)txtFolio.Value.Value;
                if (Session["Id_Buscar" + Session.SessionID] != null)
                {
                    acys.Id_AcsVersion = Convert.ToInt32(Session["IdVersion_Buscar" + Session.SessionID]);
                }
                else
                {
                    clsCapAcys.ConsultaUltimaVersion(ref acys, session2.Emp_Cnx);
                }
                clsCapAcys.ConsultaAsesorias(acys, session2.Emp_Cnx, ref List);


                List_Asesoria_Or = List;

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // SAUL GUERRA 20150515 BEGIN
        /// <summary>
        /// Trae el primer id_TU de la lista en coicidencia. 
        /// </summary>
        /// <param name="Id_Emp">id_Emp: 0 = SISTEMA, 1 = Empresa Base</param>
        /// <param name="Nombre">Nombre del Tipo e Usuario (Tu_Descripcion)</param>
        /// <returns>Id del Tipo de Usuario (id_TU)</returns>
        private int GetIdTipoUsuarioXNombre(string Nombre)
        {
            CapaEntidad.TipoUsuario Result = null;

            try
            {
                CapaNegocios.CN_CatTiposUsuario TU = new CapaNegocios.CN_CatTiposUsuario();

                List<CapaEntidad.TipoUsuario> returnUsuario = null;
                CapaEntidad.TipoUsuario tUsuario = new CapaEntidad.TipoUsuario();

                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                tUsuario.Id_Emp = session.Id_Emp;
                tUsuario.TU_Descripcion = Nombre;

                TU.ConsultaTiposDeUsuarioPorNombre(tUsuario, session.Emp_Cnx, ref returnUsuario);

                if (returnUsuario.Count > 0)
                {
                    Result = returnUsuario[0];
                }
                else
                {
                    Result = null;
                }
            }
            catch
            {
                Result = null;
            }

            return Result == null ? 0 : Result.Id_TU;
        }
        // SAUL GUERRA 20150515 END

        private List<Producto> GetListServicios()
        {
            try
            {
                List<Producto> List = new List<Producto>();
                CN_CapAcys clsCapAcys = new CN_CapAcys();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Acys acys = new Acys();
                acys.Id_Emp = session2.Id_Emp;
                acys.Id_Cd = session2.Id_Cd_Ver;
                acys.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);   //(int)txtCliente.Value.Value;
                acys.Id_Ter = Convert.ToInt32(txtTerritorio.Value.HasValue ? txtTerritorio.Value.Value : -1);   //txtTerritorio.Value.HasValue ? (int)txtTerritorio.Value.Value : -1;
                acys.Id_Acs = Convert.ToInt32(txtFolio.Value.HasValue ? txtFolio.Value.Value : -1); //(int)txtFolio.Value.Value;

                if (Session["Id_Buscar" + Session.SessionID] != null)
                {
                    acys.Id_AcsVersion = Convert.ToInt32(Session["IdVersion_Buscar" + Session.SessionID]);
                }
                else
                {
                    clsCapAcys.ConsultaUltimaVersion(ref acys, session2.Emp_Cnx);
                }

                clsCapAcys.ConsultaEstBi(acys, session2.Emp_Cnx, ref List);

                //List_ServicioTec_Or = List;
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<Producto> GetListServiciosMantenimiento()
        {
            try
            {
                List<Producto> List = new List<Producto>();
                CN_CapAcys clsCapAcys = new CN_CapAcys();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Acys acys = new Acys();
                acys.Id_Emp = session2.Id_Emp;
                acys.Id_Cd = session2.Id_Cd_Ver;

                //acys.Id_Cte = (int)txtCliente.Value.Value;
                //acys.Id_Ter = txtTerritorio.Value.HasValue ? (int)txtTerritorio.Value.Value : -1;
                //acys.Id_Acs = (int)txtFolio.Value.Value;

                acys.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);   //(int)txtCliente.Value.Value;
                acys.Id_Ter = Convert.ToInt32(txtTerritorio.Value.HasValue ? txtTerritorio.Value.Value : -1);   //txtTerritorio.Value.HasValue ? (int)txtTerritorio.Value.Value : -1;
                acys.Id_Acs = Convert.ToInt32(txtFolio.Value.HasValue ? txtFolio.Value.Value : -1); //(int)txtFolio.Value.Value;

                if (Session["Id_Buscar" + Session.SessionID] != null)
                {
                    acys.Id_AcsVersion = Convert.ToInt32(Session["IdVersion_Buscar" + Session.SessionID]);
                }
                else
                {
                    clsCapAcys.ConsultaUltimaVersion(ref acys, session2.Emp_Cnx);
                }

                clsCapAcys.ConsultaEstBiMantenimiento(acys, session2.Emp_Cnx, ref List);


                //if (List_ServMtto_Or == null)
                //{
                //    List_ServMtto_Or = List;
                //}

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCondiciones()
        {
            CN_CatCliente cn_catcliente = new CN_CatCliente();
            Clientes cte = new Clientes();
            cte.Id_Emp = sesion.Id_Emp;
            cte.Id_Cd = sesion.Id_Cd_Ver;
            cte.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
            try
            {
                cn_catcliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);
                /* if (_PermisoGuardar == false)
                     this.rtb1.Items[5].Visible = false;
                 else
                     this.rtb1.Items[5].Visible = true;
                 if (_PermisoGuardar == false & _PermisoModificar == false)
                     this.rtb1.Items[5].Visible = false;
                 else
                     this.rtb1.Items[5].Visible = true;*/
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                this.rtb1.Items[5].Visible = false;
            }

            /** CONDICIONES DE PAGO *****/
            chkCredito.Checked = cte.Cte_Credito;
            txtDias.Text = cte.Cte_CondPago.ToString();
            txtLimite.Text = cte.Cte_LimCobr.ToString();
            chkContado.Checked = cte.Cte_Contado;


            //FORMAS DE PAGO
            chkEfectivo.Checked = cte.Cte_Efectivo;
            chkFactoraje.Checked = cte.Cte_Factoraje;
            chkTransferencia.Checked = cte.Cte_Transferencia;
            chkCheque.Checked = cte.Cte_Cheque;
            chkTarjetaDebito.Checked = cte.Cte_TarjetaDebito;
            ChkTarjetaCredito.Checked = cte.Cte_TarjetaDebito;
            ChkDeposito.Checked = cte.Cte_Deposito;


            //REVISION
            chkRevisionLunes.Checked = cte.Cte_RLunes;
            chkRevisionMartes.Checked = cte.Cte_RMartes;
            chkRevisionMiercoles.Checked = cte.Cte_RMiercoles;
            chkRevisionJueves.Checked = cte.Cte_RJueves;
            chkRevisionViernes.Checked = cte.Cte_RViernes;
            chkRevisionSabado.Checked = cte.Cte_RSabado;
            tpRevisionMañanaInicio.DbSelectedDate = cte.Cte_RHoraam1;
            tpRevisionMañanaFin.DbSelectedDate = cte.Cte_RHoraam2;
            tpRevisionTardeInicio.DbSelectedDate = cte.Cte_RHorapm1;
            tpRevisionTardeFin.DbSelectedDate = cte.Cte_RHorapm2;
            //PAGO
            chkPagoLunes.Checked = cte.Cte_CPLunes;
            chkPagoMartes.Checked = cte.Cte_CPMartes;
            chkPagoMiercoles.Checked = cte.Cte_CPMiercoles;
            chkPagoJueves.Checked = cte.Cte_CPJueves;
            chkPagoViernes.Checked = cte.Cte_CPViernes;
            chkPagoSabado.Checked = cte.Cte_CPSabado;
            tpPagoMañanaInicio.DbSelectedDate = cte.Cte_PHoraam1;
            tpPagoMañanaFin.DbSelectedDate = cte.Cte_PHoraam2;
            tpPagoTardeInicio.DbSelectedDate = cte.Cte_PHorapm1;
            tpPagoTardeFin.DbSelectedDate = cte.Cte_PHorapm2;



            // chkOrden.Checked = cte.Cte_ReqOrdenCompra;
            // txtDocumentos.Text = cte.Cte_Documentos;
        }

        private void Guardar()
        {

            try
            {
                //if (dtAcuerdos.Rows.Count == 0)
                //{
                //    Alerta("Favor de capturar por lo menos un acuerdo económico");
                //    return;
                //}
                Acys acys = new Acys();
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                acys.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                acys.Id_Rik = Convert.ToInt32(cmbRepresentante.SelectedValue);
                acys.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                acys.Cte_Nombre = txtComercial.Text;

                acys.Id_AcsVersion = Convert.ToInt32(txtVersion.Text);
                if (_Accion == 1)
                {
                    acys.Id_AcsVersion = 1;

                }

                Funciones funcion = new Funciones();
                acys.Acs_Fecha = Convert.ToDateTime(rdFecha.SelectedDate.Value.ToShortDateString() + " " + funcion.GetLocalDateTime(sesion.Minutos).ToShortTimeString());
                acys.Acs_FechaInicioDocumento = Convert.ToDateTime(rdFechaInicioDocumento.SelectedDate.Value.ToShortDateString() + " " + funcion.GetLocalDateTime(sesion.Minutos).ToShortTimeString());
                acys.Acs_FechaFinDocumento = Convert.ToDateTime(rdFechaFinDocumento.SelectedDate.Value.ToShortDateString() + " " + funcion.GetLocalDateTime(sesion.Minutos).ToShortTimeString());

                acys.Acs_Proveedor = txtProveedor.Text;
                acys.Acs_RutaEntrega = !string.IsNullOrEmpty(cmbRutaEntrega.SelectedValue) ? Convert.ToInt32(cmbRutaEntrega.SelectedValue) : 0;
                acys.Acs_RutaServicio = !string.IsNullOrEmpty(cmbRutaServicio.SelectedValue) ? Convert.ToInt32(cmbRutaServicio.SelectedValue) : 0;

                //acys.Acs_VigenciaIni = rdVigenciaIni.FocusedDate;
                acys.Acs_VigenciaIni = Convert.ToDateTime(rdFechaInicioDocumento.SelectedDate.Value.ToShortDateString());
                acys.Acs_Semana = txtSemana.Value.HasValue ? Convert.ToInt32(txtSemana.Text) : 0;


                acys.Acs_RecPedCorreo = ChkbEmail.Checked;
                acys.Acs_RecPedFax = ChkbFax.Checked;
                acys.Acs_RecPedTel = ChkbTelefono.Checked;
                acys.Acs_RecPedRep = CheckRepVenta.Checked;
                acys.Acs_RecPedOtroStr = txtPedidoOtro.Text;

                acys.Acs_PedidoEncargadoEnviar = txtPedidoEncargadoEnviar.Text;
                acys.Acs_PedidoPuesto = txtpedidoPuesto.Text;
                acys.Acs_PedidoTelefono = txtpedidotelefono.Text;
                acys.Acs_PedidoEmail = txtpedidoEmail.Text;


                // acys.Acs_ReqOrdenCompra = chkRecDocOrdenCompra.Checked;
                //acys.Acs_RecDocReposicion = chkRecDocReposicion.Checked;
                //acys.Acs_RecDocFolio = ChkRecDocFolio.Checked;
                acys.Acs_RecDocOtro = txtRecDocOtro.Text;

                //if (acys.Acs_RecDocOtro == "") {

                //    if (!acys.Acs_RecDocReposicion && !acys.Acs_RecDocFolio && !acys.Acs_ReqOrdenCompra) {
                //        Alerta("Por Favor seleccione una opción en documentación requerida para entrega");
                //        return;

                //    }

                //}

                acys.Id_U = sesion.Id_U;

                //VISITAS
                acys.Vis_Frecuencia = Vis_Frecuencia.Value.HasValue ? Vis_Frecuencia.Value : 0;
                acys.Acs_VisitaOtro = txtVisitaOtro.Text;

                acys.Acs_ReqServAsesoria = ChkServAsesoria.Checked;
                acys.Acs_ReqServTecnicoRelleno = ChkServTecnicoRelleno.Checked;
                acys.Acs_ReqServMantenimiento = ChkServMantenimiento.Checked;

                string Modalidad = "A";

                if (rdModFrencuenciaEstablecida.Checked)
                {
                    Modalidad = "A";
                }
                else /*if (rdModOrdenAbierta.Checked)
                {
                    Modalidad = "B";
                }
                else*/
                    if (rdModConsignacion.Checked)
                    {
                        Modalidad = "C";
                    }

                acys.Acs_Modalidad = Modalidad;

                // EDSG
                acys.IdCte_DirEntrega = Convert.ToInt32(this.IdCte_DirEntrega.Value);


                if (this.IdCte_DirEntrega.Value == "")
                    acys.IdCte_DirEntrega = 0;
                else
                    acys.IdCte_DirEntrega = Convert.ToInt32(this.IdCte_DirEntrega.Value);
                //ASESORIAS


                List<Asesoria> asesorias = new List<Asesoria>();
                Asesoria asesoria;
                bool contieneAsesoria = false;


                if (acys.Acs_ReqServAsesoria)
                {

                    for (int i = 0; i < rgAsesoria.Items.Count; i++)
                    {

                        bool ServAsesoriaMensual = (rgAsesoria.Items[i].FindControl("ServAsesoriaMensual") as RadioButton).Checked;
                        DateTime? ServAsesoriaMensualfechaIni = (rgAsesoria.Items[i].FindControl("ServAsesoriaMensualfechaIni") as RadDatePicker).SelectedDate;

                        bool ServAsesoriaBimestral = (rgAsesoria.Items[i].FindControl("ServAsesoriaBimestral") as RadioButton).Checked;
                        DateTime? ServAsesoriaBimestralfechaIni = (rgAsesoria.Items[i].FindControl("ServAsesoriaBimestralfechaIni") as RadDatePicker).SelectedDate;

                        bool ServAsesoriaTrimestral = (rgAsesoria.Items[i].FindControl("ServAsesoriaTrimestral") as RadioButton).Checked;
                        DateTime? ServAsesoriaTrimestralfechaIni = (rgAsesoria.Items[i].FindControl("ServAsesoriaTrimestralfechaIni") as RadDatePicker).SelectedDate;


                        asesoria = new Asesoria();
                        asesoria.Id_Ase = Convert.ToInt32(rgAsesoria.Items[i]["Id_Ase"].Text);
                        asesoria.Ase_ServAsesoriaMensual = ServAsesoriaMensual == null ? false : (bool)ServAsesoriaMensual;

                        asesoria.Ase_ServAsesoriaMensualfechaIni = ServAsesoriaMensualfechaIni;

                        asesoria.Ase_ServAsesoriaBimestral = ServAsesoriaBimestral == null ? false : (bool)ServAsesoriaBimestral;
                        asesoria.Ase_ServAsesoriaBimestralfechaIni = ServAsesoriaBimestralfechaIni;

                        asesoria.Ase_ServAsesoriaTrimestral = ServAsesoriaTrimestral == null ? false : (bool)ServAsesoriaTrimestral;
                        asesoria.Ase_ServAsesoriaTrimestralfechaIni = ServAsesoriaTrimestralfechaIni;

                        if (asesoria.Ase_ServAsesoriaMensual || asesoria.Ase_ServAsesoriaBimestral || asesoria.Ase_ServAsesoriaTrimestral)
                        {
                            contieneAsesoria = true;
                        }

                        asesorias.Add(asesoria);
                    }

                }

                //if (acys.Acs_ReqServAsesoria && !contieneAsesoria) {
                //    Alerta("Por Favor seleccione periodo de Asesoria");
                //    return;

                //}


                //SERVICIO
                int contieneServicioRelleno = 0;


                List<Producto> Lista_Producto = new List<Producto>();
                Producto prod;

                if (acys.Acs_ReqServTecnicoRelleno)
                {

                    for (int i = 0; i < rgServicios.Items.Count; i++)
                    {
                        int cant = Convert.ToInt32((rgServicios.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Text);


                        bool ServTecnicoRellenoMensual = (rgServicios.Items[i].FindControl("ServTecnicoRellenoMensual") as RadioButton).Checked;
                        DateTime? ServTecnicoRellenoMensualfechaIni = (rgServicios.Items[i].FindControl("ServTecnicoRellenoMensualfechaIni") as RadDatePicker).SelectedDate;


                        bool ServTecnicoRellenoBimestral = (rgServicios.Items[i].FindControl("ServTecnicoRellenoBimestral") as RadioButton).Checked;
                        DateTime? ServTecnicoRellenoBimestralfechaIni = (rgServicios.Items[i].FindControl("ServTecnicoRellenoBimestralfechaIni") as RadDatePicker).SelectedDate;

                        bool ServTecnicoRellenoTrimestral = (rgServicios.Items[i].FindControl("ServTecnicoRellenoTrimestral") as RadioButton).Checked;
                        DateTime? ServTecnicoRellenoTrimestralfechaIni = (rgServicios.Items[i].FindControl("ServTecnicoRellenoTrimestralfechaIni") as RadDatePicker).SelectedDate;

                        prod = new Producto();
                        prod.Id_Prd = Convert.ToInt32((rgServicios.Items[i].FindControl("lblCodigo") as Label).Text);

                        prod.Prd_InvFinal = cant;
                        prod.Prd_InvInicial = cant;
                        prod.ServTecnicoRellenoMensual = ServTecnicoRellenoMensual == null ? false : (bool)ServTecnicoRellenoMensual;
                        prod.ServTecnicoRellenoMensualfechaIni = ServTecnicoRellenoMensualfechaIni;

                        prod.ServTecnicoRellenoBimestral = ServTecnicoRellenoBimestral == null ? false : (bool)ServTecnicoRellenoBimestral;
                        prod.ServTecnicoRellenoBimestralfechaIni = ServTecnicoRellenoBimestralfechaIni;

                        prod.ServTecnicoRellenoTrimestral = ServTecnicoRellenoTrimestral == null ? false : (bool)ServTecnicoRellenoTrimestral;
                        prod.ServTecnicoRellenoTrimestralfechaIni = ServTecnicoRellenoTrimestralfechaIni;

                        if (prod.ServTecnicoRellenoBimestral || prod.ServTecnicoRellenoTrimestral || prod.ServTecnicoRellenoMensual)
                        {
                            contieneServicioRelleno = contieneServicioRelleno + 1;
                        }

                        Lista_Producto.Add(prod);
                    }


                }

                //if (acys.Acs_ReqServTecnicoRelleno && rgServicios.Items.Count != contieneServicioRelleno)
                //{
                //    Alerta("Por Favor seleccione periodo para el Servicio Tecnico de Relleno de todos los equipos");
                //    return;
                //}


                int contieneServicioMantenimiento = 0;

                List<Producto> Lista_ProductosManteniento = new List<Producto>();
                Producto productoMantenimiento;

                if (acys.Acs_ReqServMantenimiento)
                {
                    for (int i = 0; i < rgMantPrevRev.Items.Count; i++)
                    {
                        int cant = Convert.ToInt32((rgMantPrevRev.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Text);

                        bool ServMantenimientoMensual = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoMensual") as RadioButton).Checked;
                        DateTime? ServMantenimientoMensualfechaIni = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoMensualfechaIni") as RadDatePicker).SelectedDate;

                        bool ServMantenimientoBimestral = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoBimestral") as RadioButton).Checked;
                        DateTime? ServMantenimientoBimestralfechaIni = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoBimestralfechaIni") as RadDatePicker).SelectedDate;

                        bool ServMantenimientoTrimestral = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoTrimestral") as RadioButton).Checked;
                        DateTime? ServMantenimientoTrimestralfechaIni = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoTrimestralfechaIni") as RadDatePicker).SelectedDate;

                        productoMantenimiento = new Producto();
                        productoMantenimiento.Id_Prd = Convert.ToInt32((rgMantPrevRev.Items[i].FindControl("lblCodigo") as Label).Text);
                        productoMantenimiento.Prd_InvFinal = cant;
                        productoMantenimiento.Prd_InvInicial = cant;
                        productoMantenimiento.ServMantenimientoMensual = ServMantenimientoMensual == null ? false : (bool)ServMantenimientoMensual;
                        productoMantenimiento.ServMantenimientoMensualfechaIni = ServMantenimientoMensualfechaIni;

                        productoMantenimiento.ServMantenimientoBimestral = ServMantenimientoBimestral == null ? false : (bool)ServMantenimientoBimestral;
                        productoMantenimiento.ServMantenimientoBimestralfechaIni = ServMantenimientoBimestralfechaIni;

                        productoMantenimiento.ServMantenimientoTrimestral = ServMantenimientoTrimestral == null ? false : (bool)ServMantenimientoTrimestral;
                        productoMantenimiento.ServMantenimientoTrimestralfechaIni = ServMantenimientoTrimestralfechaIni;

                        if (productoMantenimiento.ServMantenimientoMensual || productoMantenimiento.ServMantenimientoBimestral || productoMantenimiento.ServMantenimientoTrimestral)
                        {
                            contieneServicioMantenimiento = contieneServicioMantenimiento + 1;
                        }
                        Lista_ProductosManteniento.Add(productoMantenimiento);
                    }
                }

                //if (acys.Acs_ReqServMantenimiento && rgMantPrevRev.Items.Count != contieneServicioMantenimiento)
                //{
                //    Alerta("Por Favor seleccione periodo para el Servicio Tecnico de Mantenimiento de todos los equipos");
                //    return;
                //}


                //
                List<AcysPrd> list = new List<AcysPrd>();

                //AcysPrd prd;
                //for (int x = 0; x < dtAcuerdos.Rows.Count; x++)
                //{
                //    prd = new AcysPrd();
                //    prd.Id_Prd = Convert.ToInt32(dtAcuerdos.Rows[x]["Id_Prd"]);
                //    prd.Prd_Precio = Convert.ToDouble(dtAcuerdos.Rows[x]["Prd_Precio"]);
                //    prd.Acys_Cantidad = Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_Cantidad"]);
                //    prd.Acys_Frecuencia = Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_Frecuencia"]);
                //    prd.Acys_Lunes = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Lunes"]);
                //    prd.Acys_Martes = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Martes"]);
                //    prd.Acys_Miercoles = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Miercoles"]);
                //    prd.Acys_Jueves = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Jueves"]);
                //    prd.Acys_Viernes = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Viernes"]);
                //    prd.Acys_Sabado = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Sabado"]);
                //    prd.Acs_Doc = dtAcuerdos.Rows[x]["Acs_Doc"].ToString();
                //    prd.Acys_FechaInicio = dtAcuerdos.Rows[x]["Acys_ConsigFechaInicio"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dtAcuerdos.Rows[x]["Acys_ConsigFechaInicio"]);
                //    prd.Acys_FechaFin = dtAcuerdos.Rows[x]["Acys_ConsigFechaFin"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dtAcuerdos.Rows[x]["Acys_ConsigFechaFin"]);
                //    prd.Acys_CantTotal = dtAcuerdos.Rows[x]["Acys_cantTotal"] == DBNull.Value ? -1 : Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_cantTotal"]);
                //    prd.Acys_UltSCtp = dtAcuerdos.Rows[x]["Acys_UltSCtp"] == DBNull.Value ? -1 : Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_UltSCtp"]);
                //    prd.Acys_UltACtp = dtAcuerdos.Rows[x]["Acys_UltACtp"] == DBNull.Value ? -1 : Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_UltACtp"]);

                //    list.Add(prd);
                //}


                list.AddRange(this.LLenarDatosGrid(dtAcuerdos, 0));
                list.AddRange(this.LLenarDatosGrid(dtAcuerdos_Kilo, 1));
                list.AddRange(this.LLenarDatosGrid(dtAcuerdos_Iguala, 4));
                list.AddRange(this.LLenarDatosGrid(dtAcuerdos_Comensal, 2));
                list.AddRange(this.LLenarDatosGrid(dtAcuerdos_Habitacion, 3));


                acys.Acs_Notas = txtNotas.Text;

                acys.Acs_ContactoRepVenta = !string.IsNullOrEmpty(ContactoRepVenta.SelectedValue) ? Convert.ToInt32(ContactoRepVenta.SelectedValue) : 0;
                //   acys.Acs_ContactoRepVentaTel = ContactoRepVentaTel.Text;
                acys.Acs_ContactoRepVentaEmail = ContactoRepVentaEmail.Text;

                acys.Acs_ContactoRepServ = !string.IsNullOrEmpty(ContactoRepServ.SelectedValue) ? Convert.ToInt32(ContactoRepServ.SelectedValue) : 0;
                // acys.Acs_ContactoRepServTel = ContactoRepServTel.Text;
                acys.Acs_ContactoRepServEmail = ContactoRepServEmail.Text;


                acys.Acs_ContactoJefServ = !string.IsNullOrEmpty(ContactoJefServ.SelectedValue) ? Convert.ToInt32(ContactoJefServ.SelectedValue) : 0;
                //acys.Acs_ContactoJefServTel = ContactoJefServTel.Text;
                acys.Acs_ContactoJefServEmail = ContactoJefServEmail.Text;


                acys.Acs_ContactoAseServ = !string.IsNullOrEmpty(ContactoAseServ.SelectedValue) ? Convert.ToInt32(ContactoAseServ.SelectedValue) : 0;
                //acys.Acs_ContactoAseServTel = ContactoAseServTel.Text;
                acys.Acs_ContactoAseServEmail = ContactoAseServEmail.Text;

                acys.Acs_ContactoJefOper = !string.IsNullOrEmpty(ContactoJefOper.SelectedValue) ? Convert.ToInt32(ContactoJefOper.SelectedValue) : 0;
                //acys.Acs_ContactoJefOperTel = ContactoJefOperTel.Text;
                acys.Acs_ContactoJefOperEmail = ContactoJefOperEmail.Text;


                acys.Acs_ContactoCAlmRep = !string.IsNullOrEmpty(ContactoCAlmRep.SelectedValue) ? Convert.ToInt32(ContactoCAlmRep.SelectedValue) : 0;
                //acys.Acs_ContactoCAlmRepTel = ContactoCAlmRepTel.Text;
                acys.Acs_ContactoCAlmRepEmail = ContactoCAlmRepEmail.Text;

                acys.Acs_ContactoCServTec = !string.IsNullOrEmpty(ContactoCServTec.SelectedValue) ? Convert.ToInt32(ContactoCServTec.SelectedValue) : 0;
                //acys.Acs_ContactoCServTecTel = ContactoCServTecTel.Text;
                acys.Acs_ContactoCServTecEmail = ContactoCServTecEmail.Text;

                acys.Acs_ContactoCCreCob = !string.IsNullOrEmpty(ContactoCCreCob.SelectedValue) ? Convert.ToInt32(ContactoCCreCob.SelectedValue) : 0;
                acys.Acs_ContactoCCreCobTel = ContactoCCreCobTelA.Text;
                acys.Acs_ContactoCCreCobEmail = ContactoCCreCobEmail.Text;


                acys.Acs_Contacto2 = txtContactoClientecompra.Text;
                //acys.Acs_Telefono2 = txtContactoClientecompraTelA.Text;
                acys.Acs_Correo2 = txtContactoClientecompraEmail.Text;

                acys.Acs_Contacto3 = txtContactoClientealmacen.Text;
                //acys.Acs_Telefono3 = txtContactoClientealmacenTel.Value.HasValue ? Convert.ToInt32(txtContactoClientealmacenTel.Value) : 0;
                acys.Acs_Correo3 = txtContactoClientealmacenEmail.Text;

                acys.Acs_Contacto4 = txtContactoClienteMantenimiento.Text;
                //acys.Acs_Telefono4 = txtContactoClienteMantenimientoTel.Value.HasValue ? Convert.ToInt32(txtContactoClienteMantenimientoTel.Value) : 0;
                acys.Acs_Correo4 = txtContactoClienteMantenimientoEmail.Text;

                acys.Acs_Contacto5 = txtContactoClientePagos.Text;
                //acys.Acs_Telefono5 = txtContactoClientePagosTel.Value.HasValue ? Convert.ToInt32(txtContactoClientePagosTel.Value) : 0;
                acys.Acs_Correo5 = txtContactoClientePagosEmail.Text;

                acys.Acs_Contacto6 = txtContactoClienteOtro.Text;
                //acys.Acs_Telefono6 = txtContactoClienteOtroTel.Value.HasValue ? Convert.ToInt32(txtContactoClienteOtroTel.Value) : 0;
                acys.Acs_Correo6 = txtContactoClienteOtroEmail.Text;


                acys.Acs_Sucursal = txtSucursal.Text;
                acys.Acs_ParcialidadesSi = CheckParcialidadesSi.Checked ? 1 : 0;
                acys.Acs_ParcialidadesNo = CheckParcialidadesNo.Checked ? 1 : 0;
                acys.Acs_ConfirmacionPedidosSI = CheckConfirmacionPedidosSI.Checked ? 1 : 0;
                acys.Acs_ConfirmacionPedidosnO = CheckConfirmacionPedidosNO.Checked ? 1 : 0;
                acys.Acs_chkRecRevLunes = chkRecRevLunes.Checked ? 1 : 0;
                acys.Acs_RecRevMartes = chkRecRevMartes.Checked ? 1 : 0;
                acys.Acs_RecRevMiercoles = chkRecRevMiercoles.Checked ? 1 : 0;
                acys.Acs_RecRevJueves = chkRecRevJueves.Checked ? 1 : 0;
                acys.Acs_RecRevViernes = chkRecRevViernes.Checked ? 1 : 0;
                acys.Acs_RecRevSabado = chkRecRevSabado.Checked ? 1 : 0;
                acys.Acs_TimePicker1 = RadTimePicker1.DateInput.SelectedDate == null ? "": RadTimePicker1.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.Acs_TimePicker2 = RadTimePicker2.DateInput.SelectedDate == null ? "" : RadTimePicker2.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.Acs_TimePicker3 = RadTimePicker3.DateInput.SelectedDate == null ? "" : RadTimePicker3.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.Acs_TimePicker4 = RadTimePicker4.DateInput.SelectedDate == null ? "" : RadTimePicker4.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.Acs_RecPersonaRecibe = txtRecPersonaRecibe.Text;
                acys.Acs_RecPuesto = txtRecPuesto.Text;
                acys.Acs_RecCitaMismoDia = chkRecCitaMismoDia.Checked ? 1 : 0;
                acys.Acs_RecCitaSinCita = chkRecCitaSinCita.Checked ? 1 : 0;
                acys.Acs_RecCitaPrevia = chkRecCitaPrevia.Checked ? 1 : 0;
                acys.Acs_RecCitaContacto = txtRecCitaContacto.Text;
                acys.Acs_RecCitaTelefono = txtRecCitaTelefono.Text;
                acys.Acs_RecCitaDiasdeAnticipacion = Convert.ToInt32(txtRecCitaDiasdeAnticipacion.Text.Trim() == "" ? "0" : txtRecCitaDiasdeAnticipacion.Text);
                acys.Acs_RecAreaPropia = chkRecAreaPropia.Checked ? 1 : 0;
                acys.Acs_RecAreaPlaza = chkRecAreaPlaza.Checked ? 1 : 0;
                acys.Acs_RecAreaCalle = chkRecAreaCalle.Checked ? 1 : 0;
                acys.Acs_RecAreaAvTransitada = chkRecAreaAvTransitada.Checked ? 1 : 0;
                acys.Acs_RecEstCortesia = chkRecEstCortesia.Checked ? 1 : 0;
                acys.Acs_RecEstCosto = chkRecEstCosto.Checked ? 1 : 0;
                acys.Acs_RecEstMonto = Convert.ToInt32(txtRecEstMonto.Text.Trim() == "" ? "0" : txtRecEstMonto.Text);
                acys.Acs_RecDocFactFranquiciaEnt = chkRecDocFactFranquiciaEnt.Checked ? 1 : 0;
                acys.Acs_RecDocFactFranquiciaEntCop = Convert.ToInt32(txtRecDocFactFranquiciaEntCop.Text.Trim() == "" ? "0" : txtRecDocFactFranquiciaEntCop.Text);
                acys.Acs_RecDocFactFranquiciaRec = chkRecDocFactFranquiciaRec.Checked ? 1 : 0;
                acys.Acs_RecDocFactFranquiciaRecCop = Convert.ToInt32(txtRecDocFactFranquiciaRecCop.Text.Trim() == "" ? "0" : txtRecDocFactFranquiciaRecCop.Text);
                acys.Acs_RecDocFactKeyEnt = chkRecDocFactKeyEnt.Checked ? 1 : 0;
                acys.Acs_RecDocFactKeyEntCop = Convert.ToInt32(txtRecDocFactKeyEntCop.Text.Trim() == "" ? "0" : txtRecDocFactKeyEntCop.Text);
                acys.Acs_RecDocFactKeyRec = chkRecDocFactKeyRec.Checked ? 1 : 0;
                acys.Acs_RecDocFactKeyRecCop = Convert.ToInt32(txtRecDocFactKeyRecCop.Text.Trim() == "" ? "0" : txtRecDocFactKeyRecCop.Text);
                acys.Acs_RecDocOrdCompraEnt = chkRecDocOrdCompraEnt.Checked ? 1 : 0;
                acys.Acs_RecDocOrdCompraEntCop = Convert.ToInt32(txtRecDocOrdCompraEntCop.Text.Trim() == "" ? "0" : txtRecDocOrdCompraEntCop.Text);
                acys.Acs_RecDocOrdCompraRec = chkRecDocOrdCompraRec.Checked ? 1 : 0;
                acys.Acs_RecDocOrdCompraRecCop = Convert.ToInt32(txtRecDocOrdCompraRecCop.Text.Trim() == "" ? "0" : txtRecDocOrdCompraRecCop.Text);
                acys.Acs_RecDocOrdReposEnt = chkRecDocOrdReposEnt.Checked ? 1 : 0;
                acys.Acs_RecDocOrdReposEntCop = Convert.ToInt32(txtRecDocOrdReposEntCop.Text.Trim() == "" ? "0" : txtRecDocOrdReposEntCop.Text);
                acys.Acs_RecDocOrdReposRec = chkRecDocOrdReposRec.Checked ? 1 : 0;
                acys.Acs_RecDocOrdReposRecCop = Convert.ToInt32(txtRecDocOrdReposRecCop.Text.Trim() == "" ? "0" : txtRecDocOrdReposRecCop.Text);
                acys.Acs_RecDocCopPedidoEnt = chkRecDocCopPedidoEnt.Checked ? 1 : 0;
                acys.Acs_RecDocCopPedidoEntCop = Convert.ToInt32(txtRecDocCopPedidoEntCop.Text.Trim() == "" ? "0" : txtRecDocCopPedidoEntCop.Text);
                acys.Acs_RecDocCopPedidoRec = chkRecDocCopPedidoRec.Checked ? 1 : 0;
                acys.Acs_RecDocCopPedidoRecCop = Convert.ToInt32(txtRecDocCopPedidoRecCop.Text.Trim() == "" ? "0" : txtRecDocCopPedidoRecCop.Text);
                acys.ACS_RecDocRemisionEnt = chkRecDocRemisionEnt.Checked ? 1 : 0;
                acys.ACS_RecDocRemisionEntCop = Convert.ToInt32(txtRecDocRemisionEntCop.Text.Trim() == "" ? "0" : txtRecDocRemisionEntCop.Text);
                acys.ACS_RecDocRemisionRec = chkRecDocRemisionRec.Checked ? 1 : 0;
                acys.ACS_RecDocRemisionRecCop = Convert.ToInt32(txtRecDocRemisionRecCop.Text.Trim() == "" ? "0" : txtRecDocRemisionRecCop.Text);
                acys.ACS_RecDocFolioEnt = chkRecDocFolioEnt.Checked ? 1 : 0;
                acys.ACS_RecDocFolioEntCop = Convert.ToInt32(txtRecDocFolioEntCop.Text.Trim() == "" ? "0" : txtRecDocFolioEntCop.Text);
                acys.ACS_RecDocFolioRec = chkRecDocFolioRec.Checked ? 1 : 0;
                acys.ACS_RecDocFolioRecCop = Convert.ToInt32(txtRecDocFolioRecCop.Text.Trim() == "" ? "0" : txtRecDocFolioRecCop.Text);
                acys.ACS_RecDocContraRecEnt = chkRecDocContraRecEnt.Checked ? 1 : 0;
                acys.ACS_RecDocContraRecEntCop = Convert.ToInt32(txtRecDocContraRecEntCop.Text.Trim() == "" ? "0" : txtRecDocContraRecEntCop.Text);
                acys.ACS_RecDocContraRecRec = chkRecDocContraRecRec.Checked ? 1 : 0;
                acys.ACS_RecDocContraRecRecCop = Convert.ToInt32(txtRecDocContraRecRecCop.Text.Trim() == "" ? "0" : txtRecDocContraRecRecCop.Text);
                acys.ACS_RecDocEntAlmacenEnt = chkRecDocEntAlmacenEnt.Checked ? 1 : 0;
                acys.ACS_RecDocEntAlmacenEntCop = Convert.ToInt32(txtRecDocEntAlmacenEntCop.Text.Trim() == "" ? "0" : txtRecDocEntAlmacenEntCop.Text);
                acys.ACS_RecDocEntAlmacenRec = chkRecDocEntAlmacenRec.Checked ? 1 : 0;
                acys.ACS_RecDocEntAlmacenRecCop = Convert.ToInt32(txtRecDocEntAlmacenRecCop.Text.Trim() == "" ? "0" : txtRecDocEntAlmacenRecCop.Text);
                acys.ACS_RecDocSopServicioEnt = chkRecDocSopServicioEnt.Checked ? 1 : 0;
                acys.ACS_RecDocSopServicioEntCop = Convert.ToInt32(txtRecDocSopServicioEntCop.Text.Trim() == "" ? "0" : txtRecDocSopServicioEntCop.Text);
                acys.ACS_RecDocSopServicioRec = chkRecDocSopServicioRec.Checked ? 1 : 0;
                acys.ACS_RecDocSopServicioRecCop = Convert.ToInt32(txtRecDocSopServicioRecCop.Text.Trim() == "" ? "0" : txtRecDocSopServicioRecCop.Text);
                acys.ACS_RecDocNomFirmaEnt = chkRecDocNomFirmaEnt.Checked ? 1 : 0;
                acys.ACS_RecDocNomFirmaEntCop = Convert.ToInt32(txtRecDocNomFirmaEntCop.Text.Trim() == "" ? "0" : txtRecDocNomFirmaEntCop.Text);
                acys.ACS_RecDocNomFirmaoRec = chkRecDocNomFirmaoRec.Checked ? 1 : 0;
                acys.ACS_RecDocNomFirmaRecCop = Convert.ToInt32(txtRecDocNomFirmaRecCop.Text.Trim() == "" ? "0" : txtRecDocNomFirmaRecCop.Text);
                acys.ACS_RecCitaEnt = chkRecCitaEnt.Checked ? 1 : 0;
                acys.ACS_RecCitaEntCop = Convert.ToInt32(txtRecCitaEntCop.Text.Trim() == "" ? "0" : txtRecCitaEntCop.Text);
                acys.ACS_RecCitaRec = chkRecCitaRec.Checked ? 1 : 0;
                acys.ACS_RecCitaRecCop = Convert.ToInt32(txtRecCitaRecCop.Text.Trim() == "" ? "0" : txtRecCitaRecCop.Text);
                acys.ACS_RecOtroRec = txtRecOtro.Text;


                acys.ACS_chk62Lunes = chk62Lunes.Checked ? 1 : 0;
                acys.ACS_chk62Martes = chk62Martes.Checked ? 1 : 0;
                acys.ACS_chk62Miercoles = chk62Miercoles.Checked ? 1 : 0;
                acys.ACS_chk62Jueves = chk62Jueves.Checked ? 1 : 0;
                acys.ACS_chk62Viernes = chk62Viernes.Checked ? 1 : 0;
                acys.ACS_chk62Sabado = chk62Sabado.Checked ? 1 : 0;
                acys.ACS_RadTimePicker162 = RadTimePicker162.DateInput.SelectedDate == null ? "" : RadTimePicker162.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.ACS_RadTimePicker262 = RadTimePicker262.DateInput.SelectedDate == null ? "" : RadTimePicker262.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.ACS_RadTimePicker362 = RadTimePicker362.DateInput.SelectedDate == null ? "" : RadTimePicker362.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.ACS_RadTimePicker462 = RadTimePicker462.DateInput.SelectedDate == null ? "" : RadTimePicker462.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.ACS_txtRecPersonaRecibe62 = txtRecPersonaRecibe62.Text;
                acys.ACS_txtRecPuesto62 = txtRecPuesto62.Text;
                acys.ACS_Chk62Mismodia = Chk62Mismodia.Checked ? 1 : 0;
                acys.ACS_Chk62Sincita = Chk62Sincita.Checked ? 1 : 0;
                acys.ACS_Chk62Previa = Chk62Previa.Checked ? 1 : 0;
                acys.ACS_txt62CitaContacto = txt62CitaContacto.Text;
                acys.ACS_txt62CitaTelefono = txt62CitaTelefono.Text;
                acys.ACS_txt62CitaDiasdeAnticipacion = Convert.ToInt32(txt62CitaDiasdeAnticipacion.Text.Trim() == "" ? "0" : txt62CitaDiasdeAnticipacion.Text);
                acys.ACS_chk62AreaPropia = chk62AreaPropia.Checked ? 1 : 0;
                acys.ACS_chk62AreaPlaza = chk62AreaPlaza.Checked ? 1 : 0;
                acys.ACS_chk62AreaCalle = chk62AreaCalle.Checked ? 1 : 0;
                acys.ACS_chk62AreaAvTransitada = chk62AreaAvTransitada.Checked ? 1 : 0;
                acys.ACS_chk62EstCortesia = chk62EstCortesia.Checked ? 1 : 0;
                acys.ACS_chk62EstCosto = chk62EstCosto.Checked ? 1 : 0;
                acys.ACS_txt62EstMonto = Convert.ToInt32(txt62EstMonto.Text.Trim() == "" ? "0" : txt62EstMonto.Text);
                acys.ACS_txt62ClienteDireccion = txt62ClienteDireccion.Text;
                acys.ACS_txt62ClienteColonia = txt62ClienteColonia.Text;
                acys.ACS_txt62ClienteMunicipio = txt62ClienteMunicipio.Text;
                acys.ACS_txt62ClienteEstado = txt62ClienteEstado.Text;
                acys.ACS_txt62ClienteCodPost = txt62ClienteCodPost.Text;
                acys.ACS_chk62DocFactFranquiciaEnt = chk62DocFactFranquiciaEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocFactFranquiciaEntCop = Convert.ToInt32(txt62DocFactFranquiciaEntCop.Text.Trim() == "" ? "0" : txt62DocFactFranquiciaEntCop.Text);
                acys.ACS_chk62DocFactFranquiciaRec = chk62DocFactFranquiciaRec.Checked ? 1 : 0;
                acys.ACS_txt62DocFactFranquiciaRecCop = Convert.ToInt32(txt62DocFactFranquiciaRecCop.Text.Trim() == "" ? "0" : txt62DocFactFranquiciaRecCop.Text);
                acys.ACS_chk62DocFactKeyEnt = chk62DocFactKeyEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocFactKeyEntCop = Convert.ToInt32(txt62DocFactKeyEntCop.Text.Trim() == "" ? "0" : txt62DocFactKeyEntCop.Text);
                acys.ACS_chk62DocFactKeyRec = chk62DocFactKeyRec.Checked ? 1 : 0;
                acys.ACS_txt62DocFactKeyRecCop = Convert.ToInt32(txt62DocFactKeyRecCop.Text.Trim() == "" ? "0" : txt62DocFactKeyRecCop.Text);
                acys.ACS_chk62DocOrdCompraEnt = chk62DocOrdCompraEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocOrdCompraEntCop = Convert.ToInt32(txt62DocOrdCompraEntCop.Text.Trim() == "" ? "0" : txt62DocOrdCompraEntCop.Text);
                acys.ACS_chk62DocOrdCompraRec = chk62DocOrdCompraRec.Checked ? 1 : 0;
                acys.ACS_txt62DocOrdCompraRecCop = Convert.ToInt32(txt62DocOrdCompraRecCop.Text.Trim() == "" ? "0" : txt62DocOrdCompraRecCop.Text);
                acys.ACS_chk62DocOrdReposEnt = chk62DocOrdReposEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocOrdReposEntCop = Convert.ToInt32(txt62DocOrdReposEntCop.Text.Trim() == "" ? "0" : txt62DocOrdReposEntCop.Text);
                acys.ACS_chk62DocOrdReposRec = chk62DocOrdReposRec.Checked ? 1 : 0;
                acys.ACS_txt62DocOrdReposRecCop = Convert.ToInt32(txt62DocOrdReposRecCop.Text.Trim() == "" ? "0" : txt62DocOrdReposRecCop.Text);
                acys.ACS_chk62DocCopPedidoEnt = chk62DocCopPedidoEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocCopPedidoEntCop = Convert.ToInt32(txt62DocCopPedidoEntCop.Text.Trim() == "" ? "0" : txt62DocCopPedidoEntCop.Text);
                acys.ACS_chk62DocCopPedidoRec = chk62DocCopPedidoRec.Checked ? 1 : 0;
                acys.ACS_txt62DocCopPedidoRecCop = Convert.ToInt32(txt62DocCopPedidoRecCop.Text.Trim() == "" ? "0" : txt62DocCopPedidoRecCop.Text);
                acys.ACS_chk62DocRemisionEnt = chk62DocRemisionEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocRemisionEntCop = Convert.ToInt32(txt62DocRemisionEntCop.Text.Trim() == "" ? "0" : txt62DocRemisionEntCop.Text);
                acys.ACS_chk62DocRemisionRec = chk62DocRemisionRec.Checked ? 1 : 0;
                acys.ACS_txt62DocRemisionRecCop = Convert.ToInt32(txt62DocRemisionRecCop.Text.Trim() == "" ? "0" : txt62DocRemisionRecCop.Text);
                acys.ACS_chk62DocFolioEnt = chk62DocFolioEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocFolioEntCop = Convert.ToInt32(txt62DocFolioEntCop.Text.Trim() == "" ? "0" : txt62DocFolioEntCop.Text);
                acys.ACS_chk62DocFolioRec = chk62DocFolioRec.Checked ? 1 : 0;
                acys.ACS_txt62DocFolioRecCop = Convert.ToInt32(txt62DocFolioRecCop.Text.Trim() == "" ? "0" : txt62DocFolioRecCop.Text);
                acys.ACS_chk62DocContraRecEnt = chk62DocContraRecEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocContraRecEntCop = Convert.ToInt32(txt62DocContraRecEntCop.Text.Trim() == "" ? "0" : txt62DocContraRecEntCop.Text);
                acys.ACS_chk62DocContraRecRec = chk62DocContraRecRec.Checked ? 1 : 0;
                acys.ACS_txt62DocContraRecRecCop = Convert.ToInt32(txt62DocContraRecRecCop.Text.Trim() == "" ? "0" : txt62DocContraRecRecCop.Text);
                acys.ACS_chk62DocEntAlmacenEnt = chk62DocEntAlmacenEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocEntAlmacenEntCop = Convert.ToInt32(txt62DocEntAlmacenEntCop.Text.Trim() == "" ? "0" : txt62DocEntAlmacenEntCop.Text);
                acys.ACS_chk62DocEntAlmacenRec = chk62DocEntAlmacenRec.Checked ? 1 : 0;
                acys.ACS_txt62DocEntAlmacenRecCop = Convert.ToInt32(txt62DocEntAlmacenRecCop.Text.Trim() == "" ? "0" : txt62DocEntAlmacenRecCop.Text);
                acys.ACS_chk62DocSopServicioEnt = chk62DocSopServicioEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocSopServicioEntCop = Convert.ToInt32(txt62DocSopServicioEntCop.Text.Trim() == "" ? "0" : txt62DocSopServicioEntCop.Text);
                acys.ACS_chk62DocSopServicioRec = chk62DocSopServicioRec.Checked ? 1 : 0;
                acys.ACS_txt62DocSopServicioRecCop = Convert.ToInt32(txt62DocSopServicioRecCop.Text.Trim() == "" ? "0" : txt62DocSopServicioRecCop.Text);
                acys.ACS_chk62DocNomFirmaEnt = chk62DocNomFirmaEnt.Checked ? 1 : 0;
                acys.ACS_txt62DocNomFirmaEntCop = Convert.ToInt32(txt62DocNomFirmaEntCop.Text.Trim() == "" ? "0" : txt62DocNomFirmaEntCop.Text);
                acys.ACS_chk62DocNomFirmaoRec = chk62DocNomFirmaoRec.Checked ? 1 : 0;
                acys.ACS_txt62DocNomFirmaRecCop = Convert.ToInt32(txt62DocNomFirmaRecCop.Text.Trim() == "" ? "0" : txt62DocNomFirmaRecCop.Text);
                acys.ACS_chk62CitaEnt = chk62CitaEnt.Checked ? 1 : 0;
                acys.ACS_txt62CitaEntCop = Convert.ToInt32(txt62CitaEntCop.Text.Trim() == "" ? "0" : txt62CitaEntCop.Text);
                acys.ACS_chk62CitaRec = chk62CitaRec.Checked ? 1 : 0;
                acys.ACS_txt62CitaRecCop = Convert.ToInt32(txt62CitaRecCop.Text.Trim() == "" ? "0" : txt62CitaRecCop.Text);
                acys.ACS_chk63Lunes = chk63Lunes.Checked ? 1 : 0;
                acys.ACS_chk63Martes = chk63Martes.Checked ? 1 : 0;
                acys.ACS_chk63Miercoles = chk63Miercoles.Checked ? 1 : 0;
                acys.ACS_chk63Jueves = chk63Jueves.Checked ? 1 : 0;
                acys.ACS_chk63Viernes = chk63Viernes.Checked ? 1 : 0;
                acys.ACS_chk63Sabado = chk63Sabado.Checked ? 1 : 0;
                acys.ACS_Rad63TimePicker163 = Rad63TimePicker163.DateInput.SelectedDate == null ? "" : Rad63TimePicker163.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.ACS_Rad63TimePicker263 = Rad63TimePicker263.DateInput.SelectedDate == null ? "" : Rad63TimePicker263.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.ACS_Rad63TimePicker363 = Rad63TimePicker363.DateInput.SelectedDate == null ? "" : Rad63TimePicker363.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.ACS_Rad63TimePicker463 = Rad63TimePicker463.DateInput.SelectedDate == null ? "" : Rad63TimePicker463.DateInput.SelectedDate.Value.TimeOfDay.ToString("c");
                acys.ACS_txtRecPersonaRecibe63 = txtRecPersonaRecibe63.Text;
                acys.ACS_txtRecPuesto63 = txtRecPuesto63.Text;
                acys.ACS_Chk63Mismodia = Chk63Mismodia.Checked ? 1 : 0;
                acys.ACS_Chk63Sincita = Chk63Sincita.Checked ? 1 : 0;
                acys.ACS_Chk63Previa = Chk63Previa.Checked ? 1 : 0;
                acys.ACS_txt63CitaContacto = txt63CitaContacto.Text;
                acys.ACS_txt63CitaTelefono = txt63CitaTelefono.Text;
                acys.ACS_txt63CitaDiasdeAnticipacion = Convert.ToInt32(txt63CitaDiasdeAnticipacion.Text.Trim() == "" ? "0" : txt63CitaDiasdeAnticipacion.Text);
                acys.ACS_chk63AreaPropia = chk63AreaPropia.Checked ? 1 : 0;
                acys.ACS_chk63AreaPlaza = chk63AreaPlaza.Checked ? 1 : 0;
                acys.ACS_chk63AreaCalle = chk63AreaCalle.Checked ? 1 : 0;
                acys.ACS_chk63AreaAvTransitada = chk63AreaAvTransitada.Checked ? 1 : 0;
                acys.ACS_chk63EstCortesia = chk63EstCortesia.Checked ? 1 : 0;
                acys.ACS_chk63EstCosto = chk63EstCosto.Checked ? 1 : 0;
                acys.ACS_txt63EstMonto = Convert.ToInt32(txt63EstMonto.Text.Trim() == "" ? "0" : txt63EstMonto.Text);
                acys.ACS_txt63ClienteDireccion = txt63ClienteDireccion.Text;
                acys.ACS_txt63ClienteColonia = txt63ClienteColonia.Text;
                acys.ACS_txt63ClienteMunicipio = txt63ClienteMunicipio.Text;
                acys.ACS_txt63ClienteEstado = txt63ClienteEstado.Text;
                acys.ACS_txt63ClienteCodPost = txt63ClienteCodPost.Text;
                acys.ACS_chk63DocFactFranquiciaEnt = chk63DocFactFranquiciaEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocFactFranquiciaEntCop = Convert.ToInt32(txt63DocFactFranquiciaEntCop.Text.Trim() == "" ? "0" : txt63DocFactFranquiciaEntCop.Text);
                acys.ACS_chk63DocFactFranquiciaRec = chk63DocFactFranquiciaRec.Checked ? 1 : 0;
                acys.ACS_txt63DocFactFranquiciaRecCop = Convert.ToInt32(txt63DocFactFranquiciaRecCop.Text.Trim() == "" ? "0" : txt63DocFactFranquiciaRecCop.Text);
                acys.ACS_chk63DocFactKeyEnt = chk63DocFactKeyEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocFactKeyEntCop = Convert.ToInt32(txt63DocFactKeyEntCop.Text.Trim() == "" ? "0" : txt63DocFactKeyEntCop.Text);
                acys.ACS_chk63DocFactKeyRec = chk63DocFactKeyRec.Checked ? 1 : 0;
                acys.ACS_txt63DocFactKeyRecCop = Convert.ToInt32(txt63DocFactKeyRecCop.Text.Trim() == "" ? "0" : txt63DocFactKeyRecCop.Text);
                acys.ACS_chk63DocOrdCompraEnt = chk63DocOrdCompraEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocOrdCompraEntCop = Convert.ToInt32(txt63DocOrdCompraEntCop.Text.Trim() == "" ? "0" : txt63DocOrdCompraEntCop.Text);
                acys.ACS_chk63DocOrdCompraRec = chk63DocOrdCompraRec.Checked ? 1 : 0;
                acys.ACS_txt63DocOrdCompraRecCop = Convert.ToInt32(txt63DocOrdCompraRecCop.Text.Trim() == "" ? "0" : txt63DocOrdCompraRecCop.Text);
                acys.ACS_chk63DocOrdReposEnt = chk63DocOrdReposEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocOrdReposEntCop = Convert.ToInt32(txt63DocOrdReposEntCop.Text.Trim() == "" ? "0" : txt63DocOrdReposEntCop.Text);
                acys.ACS_chk63DocOrdReposRec = chk63DocOrdReposRec.Checked ? 1 : 0;
                acys.ACS_txt63DocOrdReposRecCop = Convert.ToInt32(txt63DocOrdReposRecCop.Text.Trim() == "" ? "0" : txt63DocOrdReposRecCop.Text);
                acys.ACS_chk63DocCopPedidoEnt = chk63DocCopPedidoEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocCopPedidoEntCop = Convert.ToInt32(txt63DocCopPedidoEntCop.Text.Trim() == "" ? "0" : txt63DocCopPedidoEntCop.Text);
                acys.ACS_chk63DocCopPedidoRec = chk63DocCopPedidoRec.Checked ? 1 : 0;
                acys.ACS_txt63DocCopPedidoRecCop = Convert.ToInt32(txt63DocCopPedidoRecCop.Text.Trim() == "" ? "0" : txt63DocCopPedidoRecCop.Text);
                acys.ACS_chk63DocRemisionEnt = chk63DocRemisionEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocRemisionEntCop = Convert.ToInt32(txt63DocRemisionEntCop.Text.Trim() == "" ? "0" : txt63DocRemisionEntCop.Text);
                acys.ACS_chk63DocRemisionRec = chk63DocRemisionRec.Checked ? 1 : 0;
                acys.ACS_txt63DocRemisionRecCop = Convert.ToInt32(txt63DocRemisionRecCop.Text.Trim() == "" ? "0" : txt63DocRemisionRecCop.Text);
                acys.ACS_chk63DocFolioEnt = chk63DocFolioEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocFolioEntCop = Convert.ToInt32(txt63DocFolioEntCop.Text.Trim() == "" ? "0" : txt63DocFolioEntCop.Text);
                acys.ACS_chk63DocFolioRec = chk63DocFolioRec.Checked ? 1 : 0;
                acys.ACS_txt63DocFolioRecCop = Convert.ToInt32(txt63DocFolioRecCop.Text.Trim() == "" ? "0" : txt63DocFolioRecCop.Text);
                acys.ACS_chk63DocContraRecEnt = chk63DocContraRecEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocContraRecEntCop = Convert.ToInt32(txt63DocContraRecEntCop.Text.Trim() == "" ? "0" : txt63DocContraRecEntCop.Text);
                acys.ACS_chk63DocContraRecRec = chk63DocContraRecRec.Checked ? 1 : 0;
                acys.ACS_txt63DocContraRecRecCop = Convert.ToInt32(txt63DocContraRecRecCop.Text.Trim() == "" ? "0" : txt63DocContraRecRecCop.Text);
                acys.ACS_chk63DocEntAlmacenEnt = chk63DocEntAlmacenEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocEntAlmacenEntCop = Convert.ToInt32(txt63DocEntAlmacenEntCop.Text.Trim() == "" ? "0" : txt63DocEntAlmacenEntCop.Text);
                acys.ACS_chk63DocEntAlmacenRec = chk63DocEntAlmacenRec.Checked ? 1 : 0;
                acys.ACS_txt63DocEntAlmacenRecCop = Convert.ToInt32(txt63DocEntAlmacenRecCop.Text.Trim() == "" ? "0" : txt63DocEntAlmacenRecCop.Text);
                acys.ACS_chk63DocSopServicioEnt = chk63DocSopServicioEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocSopServicioEntCop = Convert.ToInt32(txt63DocSopServicioEntCop.Text.Trim() == "" ? "0" : txt63DocSopServicioEntCop.Text);
                acys.ACS_chk63DocSopServicioRec = chk63DocSopServicioRec.Checked ? 1 : 0;
                acys.ACS_txt63DocSopServicioRecCop = Convert.ToInt32(txt63DocSopServicioRecCop.Text.Trim() == "" ? "0" : txt63DocSopServicioRecCop.Text);
                acys.ACS_chk63DocNomFirmaEnt = chk63DocNomFirmaEnt.Checked ? 1 : 0;
                acys.ACS_txt63DocNomFirmaEntCop = Convert.ToInt32(txt63DocNomFirmaEntCop.Text.Trim() == "" ? "0" : txt63DocNomFirmaEntCop.Text);
                acys.ACS_chk63DocNomFirmaoRec = chk63DocNomFirmaoRec.Checked ? 1 : 0;
                acys.ACS_txt63DocNomFirmaRecCop = Convert.ToInt32(txt63DocNomFirmaRecCop.Text.Trim() == "" ? "0" : txt63DocNomFirmaRecCop.Text);
                acys.ACS_chk63CitaEnt = chk63CitaEnt.Checked ? 1 : 0;
                acys.ACS_txt63CitaEntCop = Convert.ToInt32(txt63CitaEntCop.Text.Trim() == "" ? "0" : txt63CitaEntCop.Text);
                acys.ACS_chk63CitaRec = chk63CitaRec.Checked ? 1 : 0;
                acys.ACS_txt63CitaRecCop = Convert.ToInt32(txt63CitaRecCop.Text.Trim() == "" ? "0" : txt63CitaRecCop.Text);




                acys.Acs_ContactoRepVentaTel = ContactoRepVentaTelA.Text;
                acys.Acs_ContactoRepServTel = ContactoRepServTelA.Text;
                acys.Acs_ContactoJefServTel = ContactoJefServTelA.Text;
                acys.Acs_ContactoAseServTel = ContactoAseServTelA.Text;
                acys.Acs_ContactoJefOperTel = ContactoJefOperTelA.Text;
                acys.Acs_ContactoCAlmRepTel = ContactoCAlmRepTelA.Text;
                acys.Acs_ContactoCServTecTel = ContactoCServTecTelA.Text;
                acys.Acs_ContactoCCreCobTel = ContactoCCreCobTelA.Text;


                if (HF_ID.Value != "" && _Accion != 1)
                {
                    acys.Id_Acs = Convert.ToInt32(HF_ID.Value);
                }

                List<AcysDatosGarantia> listDatosGar = LlenaDatosGarantia(acys);
                CN_CapAcys cn_capacys = new CN_CapAcys();
                int verificador = -1;

                foreach (AcysDatosGarantia a in listDatosGar)
                {

                    if (a.Id_TG == 1 && this.rgAcuerdos_Kilo.Items.Count == 0)
                    {
                        Alerta("Faltan capturar productos para la garantia de Kilo");
                        return;
                    }
                    if (a.Id_TG == 2 && this.rgAcuerdos_Comensal.Items.Count == 0)
                    {
                        Alerta("Faltan capturar productos para la garantia de Comensal");
                        return;
                    }
                    if (a.Id_TG == 3 && this.rgAcuerdos_Habitacion.Items.Count == 0)
                    {
                        Alerta("Faltan capturar productos para la garantia de Habitación");
                        return;
                    }
                    if (a.Id_TG == 4 && this.rgAcuerdos_Iguala.Items.Count == 0)
                    {
                        Alerta("Faltan capturar productos para la garantia de Iguala");
                        return;
                    }

                    if (a.Fechas_Corte == null)
                    {
                        Alerta("Faltan capturar las fechas de corte para las garantías");
                        return;
                    }

                }


                //Inicio- Vamos a obtener el Estatus con el que se cuenta Actualmente                
                acys.Acs_Estatus = txtEstatus.Text;


                acys.Acs_Sucursal = txtSucursal.Text;

                //Fin- Vamos a obtener el Estatus con el que se cuenta Actualmente
                // Por aqui va lo del actualizar el calendario

                //ActualizaControlCalendario(acys);
                //acys.Acs_Estatus = gi.Cells[4].Text;
                //EDSG - 28022017 - Actualiza Datos Adicionales ACYS
                CapaModelo.CapAcy acysCN = new CapaModelo.CapAcy();


                acysCN.Id_Emp = acys.Id_Emp;
                acysCN.Id_Cd = acys.Id_Cd;
                acysCN.Id_Acs = acys.Id_Acs;
                acysCN.Id_AcsVersion = 1;






                if (HF_ID.Value == "" || HF_Sustituye.Value != "" || _Accion == 4)
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    cn_capacys.Insertar(acys, list, sesion.Emp_Cnx, Seleccionados, ref verificador, asesorias, Lista_Producto, Lista_ProductosManteniento, listDatosGar, this.txtValoresCalendario.Value);
                    if (verificador > 0)
                        RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente en el acuerdo #" + acys.Id_Acs + "');");
                    else
                        Alerta("Ocurrió un error al intentar guardar el acuerdo");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    cn_capacys.Modificar(acys, list, sesion.Emp_Cnx, Seleccionados, ref verificador, asesorias, Lista_Producto, Lista_ProductosManteniento, listDatosGar, this.txtValoresCalendario.Value);
                    /*
                    cn_capacys.Modificar_Log(List_ServMtto_Or,
                                            List_ServicioTec_Or,
                                            List_Asesoria_Or,
                                             dtAcuerdos_Or,
                                             Acys_Or,
                                             acys,
                                             list,
                                             sesion.Emp_Cnx,
                                             Seleccionados,
                                             ref verificador,
                                             asesorias,
                                             Lista_Producto,
                                             Lista_ProductosManteniento,
                                             sesion.Cu_User,
                                             "Acuerdos Comerciales y Servicios");
                 
                    */

                    verificador = 1;

                    if (verificador > 0)
                        RAM1.ResponseScripts.Add("CloseAlert('Los datos se modificaron correctamente en el acuerdo #" + acys.Id_Acs + "');");
                    else
                        Alerta("Ocurrió un error al intentar guardar los cambios");
                }

                List_ServMtto_Or = null;

                List_ServicioTec_Or = null;
                List_Asesoria_Or = null;
                dtAcuerdos_Or = null;

                RadTabStrip1.Tabs[0].Selected = true;
                RadPageCliente.Selected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<AcysDatosGarantia> LlenaDatosGarantia(Acys acys)
        {
            List<AcysDatosGarantia> listDatosGar = new List<AcysDatosGarantia>();


            if (listaGarantia.Exists(x => x.id_TG == 1))
            {
                var datosGar = new AcysDatosGarantia();
                datosGar.Id_Emp = acys.Id_Emp;
                datosGar.Id_Cd = acys.Id_Cd;
                datosGar.Id_Acs = acys.Id_Acs;
                datosGar.Id_TG = 1;
                datosGar.FactorGarantia = double.Parse(this.Fac_Kilo.Text);
                datosGar.UPrimaNeta = double.Parse(this.PNeta_Kilo.Text);
                //datosGar.FechaCorte = this.FCorte_Kilo.SelectedDate.Value;

                if (Session["Fechas_1"] != null) datosGar.Fechas_Corte = (Dictionary<int, DateTime>)Session["Fechas_1"];

                listDatosGar.Add(datosGar);
            }
            if (listaGarantia.Exists(x => x.id_TG == 2))
            {
                var datosGar = new AcysDatosGarantia();
                datosGar.Id_Emp = acys.Id_Emp;
                datosGar.Id_Cd = acys.Id_Cd;
                datosGar.Id_Acs = acys.Id_Acs;
                datosGar.Id_TG = 2;
                datosGar.FactorGarantia = double.Parse(this.Fac_Comensal.Text);
                datosGar.UPrimaNeta = double.Parse(this.PNeta_Comensal.Text);
                //datosGar.FechaCorte = this.FCorte_Comensal.SelectedDate.Value;

                if (Session["Fechas_2"] != null) datosGar.Fechas_Corte = (Dictionary<int, DateTime>)Session["Fechas_2"];

                listDatosGar.Add(datosGar);
            }
            if (listaGarantia.Exists(x => x.id_TG == 3))
            {
                var datosGar = new AcysDatosGarantia();
                datosGar.Id_Emp = acys.Id_Emp;
                datosGar.Id_Cd = acys.Id_Cd;
                datosGar.Id_Acs = acys.Id_Acs;
                datosGar.Id_TG = 3;
                datosGar.FactorGarantia = double.Parse(this.Fac_Habitacion.Text);
                datosGar.UPrimaNeta = double.Parse(this.PNeta_Habitacion.Text);
                //datosGar.FechaCorte = this.FCorte_Habitacion.SelectedDate.Value;

                if (Session["Fechas_3"] != null) datosGar.Fechas_Corte = (Dictionary<int, DateTime>)Session["Fechas_3"];

                listDatosGar.Add(datosGar);
            }

            if (listaGarantia.Exists(x => x.id_TG == 4))
            {
                var datosGar = new AcysDatosGarantia();
                datosGar.Id_Emp = acys.Id_Emp;
                datosGar.Id_Cd = acys.Id_Cd;
                datosGar.Id_Acs = acys.Id_Acs;
                datosGar.Id_TG = 4;
                datosGar.FactorGarantia = double.Parse(this.Fac_Iguala.Text);
                datosGar.UPrimaNeta = double.Parse(this.PNeta_Iguala.Text);
                // datosGar.FechaCorte = this.FCorte_Iguala.SelectedDate.Value;

                if (Session["Fechas_4"] != null) datosGar.Fechas_Corte = (Dictionary<int, DateTime>)Session["Fechas_4"];

                listDatosGar.Add(datosGar);
            }

            return listDatosGar;
        }


        public List<AcysPrd> LLenarDatosGrid(DataTable dt, int Id_TG)
        {
            //
            List<AcysPrd> list = new List<AcysPrd>();
            AcysPrd prd;
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                prd = new AcysPrd();
                prd.Id_Prd = Convert.ToInt32(dt.Rows[x]["Id_Prd"]);
                prd.Prd_Precio = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);
                prd.Acys_Cantidad = Convert.ToInt32(dt.Rows[x]["Acys_Cantidad"]);
                prd.Acys_Frecuencia = Convert.ToInt32(dt.Rows[x]["Acys_Frecuencia"]);
                prd.Acys_Lunes = Convert.ToBoolean(dt.Rows[x]["Acys_Lunes"]);
                prd.Acys_Martes = Convert.ToBoolean(dt.Rows[x]["Acys_Martes"]);
                prd.Acys_Miercoles = Convert.ToBoolean(dt.Rows[x]["Acys_Miercoles"]);
                prd.Acys_Jueves = Convert.ToBoolean(dt.Rows[x]["Acys_Jueves"]);
                prd.Acys_Viernes = Convert.ToBoolean(dt.Rows[x]["Acys_Viernes"]);
                prd.Acys_Sabado = Convert.ToBoolean(dt.Rows[x]["Acys_Sabado"]);
                prd.Acs_Doc = dt.Rows[x]["Acs_Doc"].ToString();
                prd.Acys_FechaInicio = dt.Rows[x]["Acys_ConsigFechaInicio"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dt.Rows[x]["Acys_ConsigFechaInicio"]);
                prd.Acys_FechaFin = dt.Rows[x]["Acys_ConsigFechaFin"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dt.Rows[x]["Acys_ConsigFechaFin"]);
                prd.Acys_CantTotal = dt.Rows[x]["Acys_cantTotal"] == DBNull.Value ? -1 : Convert.ToInt32(dt.Rows[x]["Acys_cantTotal"]);
                prd.Acys_UltSCtp = dt.Rows[x]["Acys_UltSCtp"] == DBNull.Value ? -1 : Convert.ToInt32(dt.Rows[x]["Acys_UltSCtp"]);
                prd.Acys_UltACtp = dt.Rows[x]["Acys_UltACtp"] == DBNull.Value ? -1 : Convert.ToInt32(dt.Rows[x]["Acys_UltACtp"]);
                prd.Id_TG = Id_TG;


                list.Add(prd);

            }
            return list;
        }

        private void Update()
        {
            throw new NotImplementedException();
        }


        private void CargarTipoCliente() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = new RadComboBox();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatTCliente_Combo", ref cmbTipoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarAsignar()
        {
            cmbAsignacion.Items.Clear();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");
            Dt.Rows.Add(new object[] { "-- Seleccionar --", "-1" });
            Dt.Rows.Add(new object[] { "Dependiendo de existencia", "0" });
            Dt.Rows.Add(new object[] { "Sólo partidas completas", "1" });

            cmbAsignacion.DataSource = Dt;
            cmbAsignacion.DataValueField = "Id";
            cmbAsignacion.DataTextField = "Descripcion";
            cmbAsignacion.DataBind();
        }
        private void Inicializar()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                ////Edsg Pone una fecha final al calendario
                //DateTime SemanaFinal= DateTime.MinValue;
                //CN_CatSemana CNSem = new CN_CatSemana();
                //CNSem.ConsultaSemanaMaxCalendario(Convert.ToDateTime(rdFechaInicioDocumento.SelectedDate), ref SemanaFinal, sesion.Emp_Cnx);
                Session.Remove("Fechas_1");
                Session.Remove("Fechas_2");
                Session.Remove("Fechas_3");
                Session.Remove("Fechas_4");

                //Edsg Saca las semanas Key para pintar el calendario
                var CN_Semana = new CN_CatSemana();
                var listSemanaKey = new List<Semana>();
                //  Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Semana.ConsultaCalendarioKey(DateTime.Now.Year, sesion.Id_Cd_Ver, sesion.Emp_Cnx, listSemanaKey);
                this.listSemana = listSemanaKey;



                //rdFechaFinDocumento.MaxDate = SemanaFinal;

                _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
                _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
                _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
                _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
                _Accion = Convert.ToInt32(Request.QueryString["Accion"]);

                ValidarPermisos();
                if (_Accion == 2)
                {
                    BtnAutorizar.Visible = true;
                    BtnRechazar.Visible = true;
                }

                this.cargarCboUsuarios();


                this.CargarAsignar();

                this.CargarTipoCliente();

                if (Request.QueryString["Id"].ToString() != "-1")
                {
                    if (_Accion == 0 || _Accion == 2 || _Accion == 4)
                    {
                        txtFolio.Text = Request.QueryString["Id"].ToString();
                        HF_ID.Value = Request.QueryString["Id"].ToString();
                        txtEstatus.Text = /*Request.QueryString["Estatus"].ToString()*/"S";
                        if (_Accion == 4) EsInicio = true;
                    }
                    else
                    {
                        DateTime SemanaFinal = DateTime.MinValue;
                        CN_CatSemana CNSem = new CN_CatSemana();
                        Funciones funcion = new Funciones();
                        txtFolio.Text = MaximoId();
                        HF_ID.Value = Request.QueryString["Id"].ToString();
                        HF_Sustituye.Value = Request.QueryString["Id"].ToString();
                        rdFecha.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                        rdFechaInicioDocumento.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                        DateTime fechafin = funcion.GetLocalDateTime(sesion.Minutos);
                        CNSem.ConsultaSemanaMaxCalendario(Convert.ToDateTime(rdFechaInicioDocumento.SelectedDate), ref SemanaFinal, sesion.Emp_Cnx);
                        rdFechaFinDocumento.MaxDate = SemanaFinal;
                        rdFechaFinDocumento.SelectedDate = SemanaFinal;
                        rdVigenciaIni.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                    }


                    txtTerritorio.Enabled = false;
                    cmbTerritorio.Enabled = false;
                    txtRepresentante.Enabled = false;
                    cmbRepresentante.Enabled = false;
                    CargarAcys();
                    CargarAcysDet();
                    CargarCondiciones();
                    //CargarDatosAdicionalesACYS();
                    //NO SE PUEDE MODIFICAR FECHA, TERRITORIO, REPRESENTANTE NI CLIENTE EN LA EDICION

                }
                else
                {
                    txtFolio.Text = MaximoId();
                    txtVersion.Value = 1;

                }


                rdFecha.Enabled = false;

                //txtCliente.Enabled = false;
                txtClienteNombre.Enabled = false;
                txtComercial.Enabled = false;
                txtEmail.Enabled = false;
                CheckCuentaCorporativa.Enabled = false;
                txtDireccionEntrega.Enabled = false;
                txtClienteColoniaE.Enabled = false;
                txtClienteMunicipioE.Enabled = false;
                txtClienteEstadoE.Enabled = false;
                txtClienteCPE.Enabled = false;
                txtClienteMunicipio.Enabled = false;
                txtClienteColonia.Enabled = false;
                txtClienteDireccion.Enabled = false;
                txtClienteEstado.Enabled = false;
                txtClienteRFC.Enabled = false;
                txtRutaEntrega.Enabled = false;
                txtRutaServicio.Enabled = false;
                txtTelefono.Enabled = false;
                cmbRutaEntrega.Enabled = false;
                cmbRutaServicio.Enabled = false;
                txtPuesto.Enabled = false;
                txtContacto.Enabled = false;
                txtClienteCodPost.Enabled = false;
                ChkbAdendaSI.Enabled = false;

                cmbAsignacion.Enabled = false;
                txtIdTipoCliente.Enabled = false;
                cmbTipoCliente.Enabled = false;

                ChkbGarantiaSI.Enabled = false;
                ChkbServiciosSI.Enabled = false;

                div_Regular.Visible = false;
                div_Kilo.Visible = false;
                div_Comensal.Visible = false;
                div_Habitacion.Visible = false;
                div_iguala.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAcysDet()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GetListGrl();
                CN_CapAcys cn_capacys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                acys.Id_Acs = Convert.ToInt32(HF_ID.Value);
                if (Session["Id_Buscar" + Session.SessionID] != null)
                {
                    acys.Id_AcsVersion = Convert.ToInt32(Session["IdVersion_Buscar" + Session.SessionID]);
                }
                else
                {
                    cn_capacys.ConsultaUltimaVersion(ref acys, sesion.Emp_Cnx);
                }

                DataTable dt = dtAcuerdos.Clone();
                cn_capacys.ConsultarDet(acys, ref dt, sesion.Emp_Cnx);
                // dtAcuerdos = dt;
                foreach (var row in dt.Select("Id_TG = 0 OR Id_TG is null"))
                {
                    dtAcuerdos.ImportRow(row);
                }
                foreach (var row in dt.Select("Id_TG = 1"))
                {
                    this.dtAcuerdos_Kilo.ImportRow(row);
                }
                foreach (var row in dt.Select("Id_TG = 2"))
                {
                    dtAcuerdos_Comensal.ImportRow(row);
                }
                foreach (var row in dt.Select("Id_TG = 3"))
                {
                    dtAcuerdos_Habitacion.ImportRow(row);
                }
                foreach (var row in dt.Select("Id_TG = 4"))
                {
                    dtAcuerdos_Iguala.ImportRow(row);
                }
                rgAcuerdos.Rebind();
                rgAcuerdos_Comensal.Rebind();
                rgAcuerdos_Habitacion.Rebind();
                rgAcuerdos_Iguala.Rebind();
                rgAcuerdos_Kilo.Rebind();
                /// Se realiza copia de como se
                /// 
                if (dtAcuerdos_Or == null)
                { dtAcuerdos_Or = dt; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarAcys()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapAcys cn_capacys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                acys.Id_Acs = Convert.ToInt32(HF_ID.Value);

                if (Session["Id_Buscar" + Session.SessionID] != null)
                {
                    acys.Id_AcsVersion = Convert.ToInt32(Session["IdVersion_Buscar" + Session.SessionID]);
                }
                else
                {
                    cn_capacys.ConsultaUltimaVersion(ref acys, sesion.Emp_Cnx);
                }
                cn_capacys.Consultar(ref acys, sesion.Emp_Cnx);



                if (acys.Acs_Estatus != "C" && _Accion == 0 || _Accion == 2)
                {
                    if (sesion.Id_TU == 2) //|| sesion.Id_TU == 12)
                        this.rtb1.Items[5].Visible = true;
                    else
                        this.rtb1.Items[5].Visible = false;
                }
                //Cabecera
                DateTime fec = Convert.ToDateTime("1900/01/01");
                Funciones funcion = new Funciones();
                txtVersion.Value = acys.Acs_Version;
                if (_Accion == 0 || _Accion == 2)
                {

                    rdFecha.DbSelectedDate = acys.Acs_Fecha;
                    rdFechaFinDocumento.DbSelectedDate = acys.Acs_FechaFinDocumento;
                    rdFechaInicioDocumento.DbSelectedDate = acys.Acs_FechaInicioDocumento;
                }
                else if (_Accion == 4)
                {
                    rdFecha.DbSelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                    rdFechaFinDocumento.DbSelectedDate = acys.Acs_FechaFinDocumento;
                    rdFechaInicioDocumento.DbSelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                    txtVersion.Value = acys.Acs_Version + 1;
                }

                /***Order to Cash****/

                txtSucursal.Text = acys.Acs_Sucursal;



                /****CLIENTE ***/
                //Información Fiscal
                txtCliente.Text = acys.Id_Cte.ToString();
                txtClienteNombre.Text = acys.Cte_Nombre;
                txtClienteDireccion.Text = acys.ClienteDireccion;
                txtClienteColonia.Text = acys.ClienteColonia;
                txtClienteMunicipio.Text = acys.ClienteMunicipio;
                txtClienteEstado.Text = acys.ClienteEstado;
                txtClienteRFC.Text = acys.ClienteRFC;
                txtClienteCodPost.Text = acys.ClienteCodPost;
                txtEmail.Enabled = false; ;
                CheckCuentaCorporativa.Checked = acys.CuentaCorporativa;
                ChkbAdendaSI.Checked = acys.AddendaSI;


                //Información Comercial  
                txtComercial.Text = acys.Cte_Nombre;
                txtDireccionEntrega.Text = acys.DireccionEntrega;
                txtClienteColoniaE.Text = acys.ClienteColoniaE;
                txtClienteMunicipioE.Text = acys.ClienteMunicipioE;
                txtClienteEstadoE.Text = acys.ClienteEstadoE;
                txtClienteCPE.Text = acys.ClienteCPE;
                txtProveedor.Text = acys.Acs_Proveedor;
                txtContacto.Text = acys.Acs_Contacto;
                txtPuesto.Text = acys.Acs_Puesto;
                txtTelefono.Text = acys.Acs_Telefono.ToString();
                txtEmail.Text = acys.Acs_Correo;

                CN_CatCliente clsCliente = new CN_CatCliente();
                ClienteDirEntrega cliente = new ClienteDirEntrega();

                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_CteDirEntrega = acys.IdCte_DirEntrega;
                cliente.Id_Cte = acys.Id_Cte;
                clsCliente.ConsultaClienteDirEntrega(cliente, sesion.Emp_Cnx);

                this.txtDireccionEntrega.Text = cliente.Cte_Calle;
                this.txtClienteCPE.Text = cliente.Cte_Cp.Trim();
                this.txtClienteColoniaE.Text = cliente.Cte_Colonia;
                this.txtClienteMunicipioE.Text = cliente.Cte_Municipio;
                this.txtClienteEstadoE.Text = cliente.Cte_Estado;
                this.IdCte_DirEntrega.Value = acys.IdCte_DirEntrega.ToString();

                CargarTerritorios();
                txtTerritorio.Text = acys.Id_Ter.ToString();
                if (cmbTerritorio.FindItemIndexByValue(acys.Id_Ter.ToString()) > 0)
                {
                    cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(acys.Id_Ter.ToString());
                    cmbTerritorio.Text = cmbTerritorio.FindItemByValue(acys.Id_Ter.ToString()).Text;
                }


                if (cmbRutaServicio.FindItemIndexByValue(acys.Acs_RutaServicio.ToString()) > 0)
                {
                    txtRutaServicio.DbValue = acys.Acs_RutaServicio <= 0 ? (object)null : acys.Acs_RutaServicio;
                    cmbRutaServicio.SelectedIndex = cmbRutaServicio.FindItemIndexByValue(acys.Acs_RutaServicio.ToString());
                    cmbRutaServicio.Text = cmbRutaServicio.FindItemByValue(acys.Acs_RutaServicio.ToString()).Text;
                }


                CargarRik();
                CargarSegmento();
                //CargarClientes();
                if (cmbRepresentante.FindItemIndexByValue(acys.Id_Rik.ToString()) > 0)
                {
                    txtRepresentante.Text = acys.Id_Rik.ToString();
                    cmbRepresentante.SelectedIndex = cmbRepresentante.FindItemIndexByValue(acys.Id_Rik.ToString());
                    cmbRepresentante.Text = cmbRepresentante.FindItemByValue(acys.Id_Rik.ToString()).Text;
                }

                CargarRutaEntrega();


                if (cmbRutaEntrega.FindItemIndexByValue(acys.Acs_RutaEntrega.ToString()) > 0)
                {
                    txtRutaEntrega.DbValue = acys.Acs_RutaEntrega <= 0 ? (object)null : acys.Acs_RutaEntrega;
                    cmbRutaEntrega.SelectedIndex = cmbRutaEntrega.FindItemIndexByValue(acys.Acs_RutaEntrega.ToString());
                    cmbRutaEntrega.Text = cmbRutaEntrega.FindItemByValue(acys.Acs_RutaEntrega.ToString()).Text;
                }


                /*  RECEPCION DE PEDIDOS****/
                rdModFrencuenciaEstablecida.Checked = acys.Acs_Modalidad.Trim() == "A" ? true : false;
                /*rdModOrdenAbierta.Checked = acys.Acs_Modalidad.Trim() == "B" ? true : false;*/
                showColumsAcuerdoEconomico(acys.Acs_Modalidad.Trim());
                ChkbEmail.Checked = acys.Acs_RecPedCorreo;
                ChkbFax.Checked = acys.Acs_RecPedFax;
                ChkbTelefono.Checked = acys.Acs_RecPedTel;
                CheckRepVenta.Checked = acys.Acs_RecPedRep;
                txtPedidoOtro.Text = acys.Acs_RecPedOtroStr;

                txtPedidoEncargadoEnviar.Text = acys.Acs_PedidoEncargadoEnviar;
                txtpedidoPuesto.Text = acys.Acs_PedidoPuesto;
                txtpedidotelefono.Text = acys.Acs_PedidoTelefono;
                txtpedidoEmail.Text = acys.Acs_PedidoEmail;

                //chkRecDocOrdenCompra.Checked = acys.Acs_ReqOrdenCompra;
                /* chkRecDocReposicion.Checked = acys.Acs_RecDocReposicion;*/
                //ChkRecDocFolio.Checked = acys.Acs_RecDocFolio;
                txtRecDocOtro.Text = acys.Acs_RecDocOtro;

                /************** ACUERDOS ENONOMICOS ******/
                if (_Accion == 0 || _Accion == 2)
                {
                    rdVigenciaIni.DbSelectedDate = (acys.Acs_VigenciaIni == fec ? null : acys.Acs_VigenciaIni);
                }
                txtSemana.DbValue = acys.Acs_Semana;

                /******* SERVICIOS DE VALOR ******/
                Vis_Frecuencia.DbValue = acys.Vis_Frecuencia;
                txtVisitaOtro.Text = acys.Acs_VisitaOtro;
                ChkServAsesoria.Checked = acys.Acs_ReqServAsesoria;
                ChkServTecnicoRelleno.Checked = acys.Acs_ReqServTecnicoRelleno;
                ChkServMantenimiento.Checked = acys.Acs_ReqServMantenimiento;
                AsesoriaListado.Visible = acys.Acs_ReqServAsesoria;
                EquipoRellenoListado.Visible = acys.Acs_ReqServTecnicoRelleno;
                MantenimientoPreventivoListado.Visible = acys.Acs_ReqServMantenimiento;

                /**** OTROS APOYOS *******/


                txtSucursal.Text = acys.Acs_Sucursal;
                CheckParcialidadesSi.Checked = acys.Acs_ParcialidadesSi == 1 ? true : false;
                CheckParcialidadesNo.Checked = acys.Acs_ParcialidadesNo == 1 ? true : false;
                CheckConfirmacionPedidosSI.Checked = acys.Acs_ConfirmacionPedidosSI == 1 ? true : false;
                CheckConfirmacionPedidosNO.Checked = acys.Acs_ConfirmacionPedidosnO == 1 ? true : false;
                chkRecRevLunes.Checked = acys.Acs_chkRecRevLunes == 1 ? true : false;
                chkRecRevMartes.Checked = acys.Acs_RecRevMartes == 1 ? true : false;
                chkRecRevMiercoles.Checked = acys.Acs_RecRevMiercoles == 1 ? true : false;
                chkRecRevJueves.Checked = acys.Acs_RecRevJueves == 1 ? true : false;
                chkRecRevViernes.Checked = acys.Acs_RecRevViernes == 1 ? true : false;
                chkRecRevSabado.Checked = acys.Acs_RecRevSabado == 1 ? true : false;
                RadTimePicker1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.Acs_TimePicker1);
                RadTimePicker2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.Acs_TimePicker2);
                RadTimePicker3.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.Acs_TimePicker3);
                RadTimePicker4.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.Acs_TimePicker4);
                txtRecPersonaRecibe.Text = acys.Acs_RecPersonaRecibe;
                txtRecPuesto.Text = acys.Acs_RecPuesto;
                chkRecCitaMismoDia.Checked = acys.Acs_RecCitaMismoDia == 1 ? true : false;
                chkRecCitaSinCita.Checked = acys.Acs_RecCitaSinCita == 1 ? true : false;
                chkRecCitaPrevia.Checked = acys.Acs_RecCitaPrevia == 1 ? true : false;
                txtRecCitaContacto.Text = acys.Acs_RecCitaContacto;
                txtRecCitaTelefono.Text = acys.Acs_RecCitaTelefono;
                txtRecCitaDiasdeAnticipacion.Value = acys.Acs_RecCitaDiasdeAnticipacion;
                chkRecAreaPropia.Checked = acys.Acs_RecAreaPropia == 1 ? true : false;
                chkRecAreaPlaza.Checked = acys.Acs_RecAreaPlaza == 1 ? true : false;
                chkRecAreaCalle.Checked = acys.Acs_RecAreaCalle == 1 ? true : false;
                chkRecAreaAvTransitada.Checked = acys.Acs_RecAreaAvTransitada == 1 ? true : false;
                chkRecEstCortesia.Checked = acys.Acs_RecEstCortesia == 1 ? true : false;
                chkRecEstCosto.Checked = acys.Acs_RecEstCosto == 1 ? true : false;
                txtRecEstMonto.Value = acys.Acs_RecEstMonto;
                chkRecDocFactFranquiciaEnt.Checked = acys.Acs_RecDocFactFranquiciaEnt == 1 ? true : false;
                txtRecDocFactFranquiciaEntCop.Value = acys.Acs_RecDocFactFranquiciaEntCop;
                chkRecDocFactFranquiciaRec.Checked = acys.Acs_RecDocFactFranquiciaRec == 1 ? true : false;
                txtRecDocFactFranquiciaRecCop.Value = acys.Acs_RecDocFactFranquiciaRecCop;
                chkRecDocFactKeyEnt.Checked = acys.Acs_RecDocFactKeyEnt == 1 ? true : false;
                txtRecDocFactKeyEntCop.Value = acys.Acs_RecDocFactKeyEntCop;
                chkRecDocFactKeyRec.Checked = acys.Acs_RecDocFactKeyRec == 1 ? true : false;
                txtRecDocFactKeyRecCop.Value = acys.Acs_RecDocFactKeyRecCop;
                chkRecDocOrdCompraEnt.Checked = acys.Acs_RecDocOrdCompraEnt == 1 ? true : false;
                txtRecDocOrdCompraEntCop.Value = acys.Acs_RecDocOrdCompraEntCop;
                chkRecDocOrdCompraRec.Checked = acys.Acs_RecDocOrdCompraRec == 1 ? true : false;
                txtRecDocOrdCompraRecCop.Value = acys.Acs_RecDocOrdCompraRecCop;
                chkRecDocOrdReposEnt.Checked = acys.Acs_RecDocOrdReposEnt == 1 ? true : false;
                txtRecDocOrdReposEntCop.Value = acys.Acs_RecDocOrdReposEntCop;
                chkRecDocOrdReposRec.Checked = acys.Acs_RecDocOrdReposRec == 1 ? true : false;
                txtRecDocOrdReposRecCop.Value = acys.Acs_RecDocOrdReposRecCop;
                chkRecDocCopPedidoEnt.Checked = acys.Acs_RecDocCopPedidoEnt == 1 ? true : false;
                txtRecDocCopPedidoEntCop.Value = acys.Acs_RecDocCopPedidoEntCop;
                chkRecDocCopPedidoRec.Checked = acys.Acs_RecDocCopPedidoRec == 1 ? true : false;
                txtRecDocCopPedidoRecCop.Value = acys.Acs_RecDocCopPedidoRecCop;
                chkRecDocRemisionEnt.Checked = acys.ACS_RecDocRemisionEnt == 1 ? true : false;
                txtRecDocRemisionEntCop.Value = acys.ACS_RecDocRemisionEntCop;
                chkRecDocRemisionRec.Checked = acys.ACS_RecDocRemisionRec == 1 ? true : false;
                txtRecDocRemisionRecCop.Value = acys.ACS_RecDocRemisionRecCop;
                chkRecDocFolioEnt.Checked = acys.ACS_RecDocFolioEnt == 1 ? true : false;
                txtRecDocFolioEntCop.Value = acys.ACS_RecDocFolioEntCop;
                chkRecDocFolioRec.Checked = acys.ACS_RecDocFolioRec == 1 ? true : false;
                txtRecDocFolioRecCop.Value = acys.ACS_RecDocFolioRecCop;
                chkRecDocContraRecEnt.Checked = acys.ACS_RecDocContraRecEnt == 1 ? true : false;
                txtRecDocContraRecEntCop.Value = acys.ACS_RecDocContraRecEntCop;
                chkRecDocContraRecRec.Checked = acys.ACS_RecDocContraRecRec == 1 ? true : false;
                txtRecDocContraRecRecCop.Value = acys.ACS_RecDocContraRecRecCop;
                chkRecDocEntAlmacenEnt.Checked = acys.ACS_RecDocEntAlmacenEnt == 1 ? true : false;
                txtRecDocEntAlmacenEntCop.Value = acys.ACS_RecDocEntAlmacenEntCop;
                chkRecDocEntAlmacenRec.Checked = acys.ACS_RecDocEntAlmacenRec == 1 ? true : false;
                txtRecDocEntAlmacenRecCop.Value = acys.ACS_RecDocEntAlmacenRecCop;
                chkRecDocSopServicioEnt.Checked = acys.ACS_RecDocSopServicioEnt == 1 ? true : false;
                txtRecDocSopServicioEntCop.Value = acys.ACS_RecDocSopServicioEntCop;
                chkRecDocSopServicioRec.Checked = acys.ACS_RecDocSopServicioRec == 1 ? true : false;
                txtRecDocSopServicioRecCop.Value = acys.ACS_RecDocSopServicioRecCop;
                chkRecDocNomFirmaEnt.Checked = acys.ACS_RecDocNomFirmaEnt == 1 ? true : false;
                txtRecDocNomFirmaEntCop.Value = acys.ACS_RecDocNomFirmaEntCop;
                chkRecDocNomFirmaoRec.Checked = acys.ACS_RecDocNomFirmaoRec == 1 ? true : false;
                txtRecDocNomFirmaRecCop.Value = acys.ACS_RecDocNomFirmaRecCop;
                chkRecCitaEnt.Checked = acys.ACS_RecCitaEnt == 1 ? true : false;
                txtRecCitaEntCop.Value = acys.ACS_RecCitaEntCop;
                chkRecCitaRec.Checked = acys.ACS_RecCitaRec == 1 ? true : false;
                txtRecCitaRecCop.Value = acys.ACS_RecCitaRecCop;
                txtRecOtro.Text = acys.ACS_RecOtroRec;


                chk62Lunes.Checked = acys.ACS_chk62Lunes == 1 ? true : false;
                chk62Martes.Checked = acys.ACS_chk62Martes == 1 ? true : false;
                chk62Miercoles.Checked = acys.ACS_chk62Miercoles == 1 ? true : false;
                chk62Jueves.Checked = acys.ACS_chk62Jueves == 1 ? true : false;
                chk62Viernes.Checked = acys.ACS_chk62Viernes == 1 ? true : false;
                chk62Sabado.Checked = acys.ACS_chk62Sabado == 1 ? true : false;
                RadTimePicker162.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.ACS_RadTimePicker162); ;
                RadTimePicker262.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.ACS_RadTimePicker262); ;
                RadTimePicker362.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.ACS_RadTimePicker362); ;
                RadTimePicker462.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.ACS_RadTimePicker462); ;
                txtRecPersonaRecibe62.Text = acys.ACS_txtRecPersonaRecibe62;
                txtRecPuesto62.Text = acys.ACS_txtRecPuesto62;
                Chk62Mismodia.Checked = acys.ACS_Chk62Mismodia == 1 ? true : false;
                Chk62Sincita.Checked = acys.ACS_Chk62Sincita == 1 ? true : false;
                Chk62Previa.Checked = acys.ACS_Chk62Previa == 1 ? true : false;
                txt62CitaContacto.Text = acys.ACS_txt62CitaContacto;
                txt62CitaTelefono.Text = acys.ACS_txt62CitaTelefono;
                txt62CitaDiasdeAnticipacion.Value = acys.ACS_txt62CitaDiasdeAnticipacion;
                chk62AreaPropia.Checked = acys.ACS_chk62AreaPropia == 1 ? true : false;
                chk62AreaPlaza.Checked = acys.ACS_chk62AreaPlaza == 1 ? true : false;
                chk62AreaCalle.Checked = acys.ACS_chk62AreaCalle == 1 ? true : false;
                chk62AreaAvTransitada.Checked = acys.ACS_chk62AreaAvTransitada == 1 ? true : false;
                chk62EstCortesia.Checked = acys.ACS_chk62EstCortesia == 1 ? true : false;
                chk62EstCosto.Checked = acys.ACS_chk62EstCosto == 1 ? true : false;
                txt62EstMonto.Value = acys.ACS_txt62EstMonto;
                txt62ClienteDireccion.Text = acys.ACS_txt62ClienteDireccion;
                txt62ClienteColonia.Text = acys.ACS_txt62ClienteColonia;
                txt62ClienteMunicipio.Text = acys.ACS_txt62ClienteMunicipio;
                txt62ClienteEstado.Text = acys.ACS_txt62ClienteEstado;
                txt62ClienteCodPost.Text = acys.ACS_txt62ClienteCodPost;
                chk62DocFactFranquiciaEnt.Checked = acys.ACS_chk62DocFactFranquiciaEnt == 1 ? true : false;
                txt62DocFactFranquiciaEntCop.Value = acys.ACS_txt62DocFactFranquiciaEntCop;
                chk62DocFactFranquiciaRec.Checked = acys.ACS_chk62DocFactFranquiciaRec == 1 ? true : false;
                txt62DocFactFranquiciaRecCop.Value = acys.ACS_txt62DocFactFranquiciaRecCop;
                chk62DocFactKeyEnt.Checked = acys.ACS_chk62DocFactKeyEnt == 1 ? true : false;
                txt62DocFactKeyEntCop.Value = acys.ACS_txt62DocFactKeyEntCop;
                chk62DocFactKeyRec.Checked = acys.ACS_chk62DocFactKeyRec == 1 ? true : false;
                txt62DocFactKeyRecCop.Value = acys.ACS_txt62DocFactKeyRecCop;
                chk62DocOrdCompraEnt.Checked = acys.ACS_chk62DocOrdCompraEnt == 1 ? true : false;
                txt62DocOrdCompraEntCop.Value = acys.ACS_txt62DocOrdCompraEntCop;
                chk62DocOrdCompraRec.Checked = acys.ACS_chk62DocOrdCompraRec == 1 ? true : false;
                txt62DocOrdCompraRecCop.Value = acys.ACS_txt62DocOrdCompraRecCop;
                chk62DocOrdReposEnt.Checked = acys.ACS_chk62DocOrdReposEnt == 1 ? true : false;
                txt62DocOrdReposEntCop.Value = acys.ACS_txt62DocOrdReposEntCop;
                chk62DocOrdReposRec.Checked = acys.ACS_chk62DocOrdReposRec == 1 ? true : false;
                txt62DocOrdReposRecCop.Value = acys.ACS_txt62DocOrdReposRecCop;
                chk62DocCopPedidoEnt.Checked = acys.ACS_chk62DocCopPedidoEnt == 1 ? true : false;
                txt62DocCopPedidoEntCop.Value = acys.ACS_txt62DocCopPedidoEntCop;
                chk62DocCopPedidoRec.Checked = acys.ACS_chk62DocCopPedidoRec == 1 ? true : false;
                txt62DocCopPedidoRecCop.Value = acys.ACS_txt62DocCopPedidoRecCop;
                chk62DocRemisionEnt.Checked = acys.ACS_chk62DocRemisionEnt == 1 ? true : false;
                txt62DocRemisionEntCop.Value = acys.ACS_txt62DocRemisionEntCop;
                chk62DocRemisionRec.Checked = acys.ACS_chk62DocRemisionRec == 1 ? true : false;
                txt62DocRemisionRecCop.Value = acys.ACS_txt62DocRemisionRecCop;
                chk62DocFolioEnt.Checked = acys.ACS_chk62DocFolioEnt == 1 ? true : false;
                txt62DocFolioEntCop.Value = acys.ACS_txt62DocFolioEntCop;
                chk62DocFolioRec.Checked = acys.ACS_chk62DocFolioRec == 1 ? true : false;
                txt62DocFolioRecCop.Value = acys.ACS_txt62DocFolioRecCop;
                chk62DocContraRecEnt.Checked = acys.ACS_chk62DocContraRecEnt == 1 ? true : false;
                txt62DocContraRecEntCop.Value = acys.ACS_txt62DocContraRecEntCop;
                chk62DocContraRecRec.Checked = acys.ACS_chk62DocContraRecRec == 1 ? true : false;
                txt62DocContraRecRecCop.Value = acys.ACS_txt62DocContraRecRecCop;
                chk62DocEntAlmacenEnt.Checked = acys.ACS_chk62DocEntAlmacenEnt == 1 ? true : false;
                txt62DocEntAlmacenEntCop.Value = acys.ACS_txt62DocEntAlmacenEntCop;
                chk62DocEntAlmacenRec.Checked = acys.ACS_chk62DocEntAlmacenRec == 1 ? true : false;
                txt62DocEntAlmacenRecCop.Value = acys.ACS_txt62DocEntAlmacenRecCop;
                chk62DocSopServicioEnt.Checked = acys.ACS_chk62DocSopServicioEnt == 1 ? true : false;
                txt62DocSopServicioEntCop.Value = acys.ACS_txt62DocSopServicioEntCop;
                chk62DocSopServicioRec.Checked = acys.ACS_chk62DocSopServicioRec == 1 ? true : false;
                txt62DocSopServicioRecCop.Value = acys.ACS_txt62DocSopServicioRecCop;
                chk62DocNomFirmaEnt.Checked = acys.ACS_chk62DocNomFirmaEnt == 1 ? true : false;
                txt62DocNomFirmaEntCop.Value = acys.ACS_txt62DocNomFirmaEntCop;
                chk62DocNomFirmaoRec.Checked = acys.ACS_chk62DocNomFirmaoRec == 1 ? true : false;
                txt62DocNomFirmaRecCop.Value = acys.ACS_txt62DocNomFirmaRecCop;
                chk62CitaEnt.Checked = acys.ACS_chk62CitaEnt == 1 ? true : false;
                txt62CitaEntCop.Value = acys.ACS_txt62CitaEntCop;
                chk62CitaRec.Checked = acys.ACS_chk62CitaRec == 1 ? true : false;
                txt62CitaRecCop.Value = acys.ACS_txt62CitaRecCop;
                chk63Lunes.Checked = acys.ACS_chk63Lunes == 1 ? true : false;
                chk63Martes.Checked = acys.ACS_chk63Martes == 1 ? true : false;
                chk63Miercoles.Checked = acys.ACS_chk63Miercoles == 1 ? true : false;
                chk63Jueves.Checked = acys.ACS_chk63Jueves == 1 ? true : false;
                chk63Viernes.Checked = acys.ACS_chk63Viernes == 1 ? true : false;
                chk63Sabado.Checked = acys.ACS_chk63Sabado == 1 ? true : false;
                Rad63TimePicker163.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.ACS_Rad63TimePicker163); ;
                Rad63TimePicker263.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.ACS_Rad63TimePicker263); ;
                Rad63TimePicker363.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.ACS_Rad63TimePicker363); ;
                Rad63TimePicker463.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + acys.ACS_Rad63TimePicker463); ;
                txtRecPersonaRecibe63.Text = acys.ACS_txtRecPersonaRecibe63;
                txtRecPuesto63.Text = acys.ACS_txtRecPuesto63;
                Chk63Mismodia.Checked = acys.ACS_Chk63Mismodia == 1 ? true : false;
                Chk63Sincita.Checked = acys.ACS_Chk63Sincita == 1 ? true : false;
                Chk63Previa.Checked = acys.ACS_Chk63Previa == 1 ? true : false;
                txt63CitaContacto.Text = acys.ACS_txt63CitaContacto;
                txt63CitaTelefono.Text = acys.ACS_txt63CitaTelefono;
                txt63CitaDiasdeAnticipacion.Value = acys.ACS_txt63CitaDiasdeAnticipacion;
                chk63AreaPropia.Checked = acys.ACS_chk63AreaPropia == 1 ? true : false;
                chk63AreaPlaza.Checked = acys.ACS_chk63AreaPlaza == 1 ? true : false;
                chk63AreaCalle.Checked = acys.ACS_chk63AreaCalle == 1 ? true : false;
                chk63AreaAvTransitada.Checked = acys.ACS_chk63AreaAvTransitada == 1 ? true : false;
                chk63EstCortesia.Checked = acys.ACS_chk63EstCortesia == 1 ? true : false;
                chk63EstCosto.Checked = acys.ACS_chk63EstCosto == 1 ? true : false;
                txt63EstMonto.Value = acys.ACS_txt63EstMonto;
                txt63ClienteDireccion.Text = acys.ACS_txt63ClienteDireccion;
                txt63ClienteColonia.Text = acys.ACS_txt63ClienteColonia;
                txt63ClienteMunicipio.Text = acys.ACS_txt63ClienteMunicipio;
                txt63ClienteEstado.Text = acys.ACS_txt63ClienteEstado;
                txt63ClienteCodPost.Text = acys.ACS_txt63ClienteCodPost;
                chk63DocFactFranquiciaEnt.Checked = acys.ACS_chk63DocFactFranquiciaEnt == 1 ? true : false;
                txt63DocFactFranquiciaEntCop.Value = acys.ACS_txt63DocFactFranquiciaEntCop;
                chk63DocFactFranquiciaRec.Checked = acys.ACS_chk63DocFactFranquiciaRec == 1 ? true : false;
                txt63DocFactFranquiciaRecCop.Value = acys.ACS_txt63DocFactFranquiciaRecCop;
                chk63DocFactKeyEnt.Checked = acys.ACS_chk63DocFactKeyEnt == 1 ? true : false;
                txt63DocFactKeyEntCop.Value = acys.ACS_txt63DocFactKeyEntCop;
                chk63DocFactKeyRec.Checked = acys.ACS_chk63DocFactKeyRec == 1 ? true : false;
                txt63DocFactKeyRecCop.Value = acys.ACS_txt63DocFactKeyRecCop;
                chk63DocOrdCompraEnt.Checked = acys.ACS_chk63DocOrdCompraEnt == 1 ? true : false;
                txt63DocOrdCompraEntCop.Value = acys.ACS_txt63DocOrdCompraEntCop;
                chk63DocOrdCompraRec.Checked = acys.ACS_chk63DocOrdCompraRec == 1 ? true : false;
                txt63DocOrdCompraRecCop.Value = acys.ACS_txt63DocOrdCompraRecCop;
                chk63DocOrdReposEnt.Checked = acys.ACS_chk63DocOrdReposEnt == 1 ? true : false;
                txt63DocOrdReposEntCop.Value = acys.ACS_txt63DocOrdReposEntCop;
                chk63DocOrdReposRec.Checked = acys.ACS_chk63DocOrdReposRec == 1 ? true : false;
                txt63DocOrdReposRecCop.Value = acys.ACS_txt63DocOrdReposRecCop;
                chk63DocCopPedidoEnt.Checked = acys.ACS_chk63DocCopPedidoEnt == 1 ? true : false;
                txt63DocCopPedidoEntCop.Value = acys.ACS_txt63DocCopPedidoEntCop;
                chk63DocCopPedidoRec.Checked = acys.ACS_chk63DocCopPedidoRec == 1 ? true : false;
                txt63DocCopPedidoRecCop.Value = acys.ACS_txt63DocCopPedidoRecCop;
                chk63DocRemisionEnt.Checked = acys.ACS_chk63DocRemisionEnt == 1 ? true : false;
                txt63DocRemisionEntCop.Value = acys.ACS_txt63DocRemisionEntCop;
                chk63DocRemisionRec.Checked = acys.ACS_chk63DocRemisionRec == 1 ? true : false;
                txt63DocRemisionRecCop.Value = acys.ACS_txt63DocRemisionRecCop;
                chk63DocFolioEnt.Checked = acys.ACS_chk63DocFolioEnt == 1 ? true : false;
                txt63DocFolioEntCop.Value = acys.ACS_txt63DocFolioEntCop;
                chk63DocFolioRec.Checked = acys.ACS_chk63DocFolioRec == 1 ? true : false;
                txt63DocFolioRecCop.Value = acys.ACS_txt63DocFolioRecCop;
                chk63DocContraRecEnt.Checked = acys.ACS_chk63DocContraRecEnt == 1 ? true : false;
                txt63DocContraRecEntCop.Value = acys.ACS_txt63DocContraRecEntCop;
                chk63DocContraRecRec.Checked = acys.ACS_chk63DocContraRecRec == 1 ? true : false;
                txt63DocContraRecRecCop.Value = acys.ACS_txt63DocContraRecRecCop;
                chk63DocEntAlmacenEnt.Checked = acys.ACS_chk63DocEntAlmacenEnt == 1 ? true : false;
                txt63DocEntAlmacenEntCop.Value = acys.ACS_txt63DocEntAlmacenEntCop;
                chk63DocEntAlmacenRec.Checked = acys.ACS_chk63DocEntAlmacenRec == 1 ? true : false;
                txt63DocEntAlmacenRecCop.Value = acys.ACS_txt63DocEntAlmacenRecCop;
                chk63DocSopServicioEnt.Checked = acys.ACS_chk63DocSopServicioEnt == 1 ? true : false;
                txt63DocSopServicioEntCop.Value = acys.ACS_txt63DocSopServicioEntCop;
                chk63DocSopServicioRec.Checked = acys.ACS_chk63DocSopServicioRec == 1 ? true : false;
                txt63DocSopServicioRecCop.Value = acys.ACS_txt63DocSopServicioRecCop;
                chk63DocNomFirmaEnt.Checked = acys.ACS_chk63DocNomFirmaEnt == 1 ? true : false;
                txt63DocNomFirmaEntCop.Value = acys.ACS_txt63DocNomFirmaEntCop;
                chk63DocNomFirmaoRec.Checked = acys.ACS_chk63DocNomFirmaoRec == 1 ? true : false;
                txt63DocNomFirmaRecCop.Value = acys.ACS_txt63DocNomFirmaRecCop;
                chk63CitaEnt.Checked = acys.ACS_chk63CitaEnt == 1 ? true : false;
                txt63CitaEntCop.Value = acys.ACS_txt63CitaEntCop;
                chk63CitaRec.Checked = acys.ACS_chk63CitaRec == 1 ? true : false;
                txt63CitaRecCop.Value = acys.ACS_txt63CitaRecCop;






                ContactoRepVentaTelA.Text = acys.Acs_ContactoRepVentaTel;
                ContactoRepServTelA.Text = acys.Acs_ContactoRepServTel;
                ContactoJefServTelA.Text = acys.Acs_ContactoJefServTel;
                ContactoAseServTelA.Text = acys.Acs_ContactoAseServTel;
                ContactoJefOperTelA.Text = acys.Acs_ContactoJefOperTel;
                ContactoCAlmRepTelA.Text = acys.Acs_ContactoCAlmRepTel;
                ContactoCServTecTelA.Text = acys.Acs_ContactoCServTecTel;
                ContactoCCreCobTelA.Text = acys.Acs_ContactoCCreCobTel;

                txtNotas.Text = acys.Acs_Notas;

                // Personal de KEY


                if (ContactoRepVenta.FindItemIndexByValue(acys.Acs_ContactoRepVenta.ToString()) > 0)
                {
                    ContactoRepVenta.SelectedIndex = ContactoRepVenta.FindItemIndexByValue(acys.Acs_ContactoRepVenta.ToString());
                    ContactoRepVenta.Text = ContactoRepVenta.FindItemByValue(acys.Acs_ContactoRepVenta.ToString()).Text;
                }

                //ContactoRepVentaTel.Text = acys.Acs_ContactoRepVentaTel;
                ContactoRepVentaEmail.Text = acys.Acs_ContactoRepVentaEmail;

                if (ContactoRepServ.FindItemIndexByValue(acys.Acs_ContactoRepServ.ToString()) > 0)
                {
                    ContactoRepServ.SelectedIndex = ContactoRepServ.FindItemIndexByValue(acys.Acs_ContactoRepServ.ToString());
                    ContactoRepServ.Text = ContactoRepServ.FindItemByValue(acys.Acs_ContactoRepServ.ToString()).Text;
                }

                //ContactoRepServTel.Text = acys.Acs_ContactoRepServTel;
                ContactoRepServEmail.Text = acys.Acs_ContactoRepServEmail;


                if (ContactoJefServ.FindItemIndexByValue(acys.Acs_ContactoJefServ.ToString()) > 0)
                {
                    ContactoJefServ.SelectedIndex = ContactoJefServ.FindItemIndexByValue(acys.Acs_ContactoJefServ.ToString());
                    ContactoJefServ.Text = ContactoJefServ.FindItemByValue(acys.Acs_ContactoJefServ.ToString()).Text;
                }

                //ContactoJefServTel.Text = acys.Acs_ContactoJefServTel;
                ContactoJefServEmail.Text = acys.Acs_ContactoJefServEmail;


                if (ContactoAseServ.FindItemIndexByValue(acys.Acs_ContactoAseServ.ToString()) > 0)
                {
                    ContactoAseServ.SelectedIndex = ContactoAseServ.FindItemIndexByValue(acys.Acs_ContactoAseServ.ToString());
                    ContactoAseServ.Text = ContactoAseServ.FindItemByValue(acys.Acs_ContactoAseServ.ToString()).Text;
                }

                //ContactoAseServTel.Text = acys.Acs_ContactoAseServTel;
                ContactoAseServEmail.Text = acys.Acs_ContactoAseServEmail;


                if (ContactoJefOper.FindItemIndexByValue(acys.Acs_ContactoJefOper.ToString()) > 0)
                {
                    ContactoJefOper.SelectedIndex = ContactoJefOper.FindItemIndexByValue(acys.Acs_ContactoJefOper.ToString());
                    ContactoJefOper.Text = ContactoJefOper.FindItemByValue(acys.Acs_ContactoJefOper.ToString()).Text;
                }

                //ContactoJefOperTel.Text = acys.Acs_ContactoJefOperTel;
                ContactoJefOperEmail.Text = acys.Acs_ContactoJefOperEmail;

                if (ContactoCAlmRep.FindItemIndexByValue(acys.Acs_ContactoCAlmRep.ToString()) > 0)
                {
                    ContactoCAlmRep.SelectedIndex = ContactoCAlmRep.FindItemIndexByValue(acys.Acs_ContactoCAlmRep.ToString());
                    ContactoCAlmRep.Text = ContactoCAlmRep.FindItemByValue(acys.Acs_ContactoCAlmRep.ToString()).Text;
                }

                //ContactoCAlmRepTel.Text = acys.Acs_ContactoCAlmRepTel;
                ContactoCAlmRepEmail.Text = acys.Acs_ContactoCAlmRepEmail;


                if (ContactoCServTec.FindItemIndexByValue(acys.Acs_ContactoCServTec.ToString()) > 0)
                {
                    ContactoCServTec.SelectedIndex = ContactoCServTec.FindItemIndexByValue(acys.Acs_ContactoCServTec.ToString());
                    ContactoCServTec.Text = ContactoCServTec.FindItemByValue(acys.Acs_ContactoCServTec.ToString()).Text;
                }

                //ContactoCServTecTel.Text = acys.Acs_ContactoCServTecTel;
                ContactoCServTecEmail.Text = acys.Acs_ContactoCServTecEmail;

                //acys.Acs_ContactoCCreCob.ToString()
                // if (ContactoCCreCob.FindItemIndexByValue(acys.Acs_ContactoCCreCob.ToString())>0)
                if (acys.Acs_ContactoCCreCob > 0)
                {
                    ContactoCCreCob.SelectedIndex = ContactoCCreCob.FindItemIndexByValue(acys.Acs_ContactoCCreCob.ToString());
                    if (ContactoCCreCob.FindItemIndexByValue(acys.Acs_ContactoCCreCob.ToString()) == -1)
                    {
                        ContactoCCreCob.Text = "";
                    }
                    else
                    {
                        ContactoCCreCob.Text = ContactoCCreCob.FindItemByValue(acys.Acs_ContactoCCreCob.ToString()).Text;
                    }
                }

                //ContactoCCreCobTel.Text = acys.Acs_ContactoCCreCobTel;
                ContactoCCreCobEmail.Text = acys.Acs_ContactoCCreCobEmail;




                //  Contactos del Cliente

                txtContactoClientecompra.Text = acys.Acs_Contacto2;
                //txtContactoClientecompraTel.DbValue = acys.Acs_Telefono2 <= 0 ? (object)null : acys.Acs_Telefono2;
                txtContactoClientecompraEmail.Text = acys.Acs_Correo2;

                txtContactoClientealmacen.Text = acys.Acs_Contacto3;
                //txtContactoClientealmacenTel.DbValue = acys.Acs_Telefono3 <= 0 ? (object)null : acys.Acs_Telefono3;
                txtContactoClientealmacenEmail.Text = acys.Acs_Correo3;

                txtContactoClienteMantenimiento.Text = acys.Acs_Contacto4;
                //txtContactoClienteMantenimientoTel.DbValue = acys.Acs_Telefono4 <= 0 ? (object)null : acys.Acs_Telefono4;
                txtContactoClienteMantenimientoEmail.Text = acys.Acs_Correo4;

                txtContactoClientePagos.Text = acys.Acs_Contacto5;
                //txtContactoClientePagosTel.DbValue = acys.Acs_Telefono5 <= 0 ? (object)null : acys.Acs_Telefono5;
                txtContactoClientePagosEmail.Text = acys.Acs_Correo5;

                txtContactoClienteOtro.Text = acys.Acs_Contacto6;
                //txtContactoClienteOtroTel.DbValue = acys.Acs_Telefono6 <= 0 ? (object)null : acys.Acs_Telefono6;
                txtContactoClienteOtroEmail.Text = acys.Acs_Correo6;





                //Se crea una copia de la Informacion como se encontraba Originalmente 
                //para Registo de Log en caso de alguna Actualizacion
                Acys_Or = acys;

                CN_CatCalendarioControl CN_Cal = new CN_CatCalendarioControl();
                CalendarioControl cale = new CalendarioControl();
                List<CalendarioControl> lista = new List<CalendarioControl>();
                cale.Id_Emp = sesion.Id_Emp;
                cale.Id_Cd = sesion.Id_Cd;
                cale.Id_Acs = acys.Id_Acs;
                cale.Id_AcsVersion = acys.Acs_Version;
                cale.Cal_Año = DateTime.Now.Year;

                CN_Cal.ConsultaCalendarioControl(ref cale, sesion.Emp_Cnx, ref lista);


                ConsultaCalendarioBD(listSemana, lista.Where(x => x.Id_TG == 0).ToList(), this.rgAcuerdos.ID);
                ConsultaCalendarioBD(listSemana, lista.Where(x => x.Id_TG == 1).ToList(), this.rgAcuerdos_Kilo.ID);
                ConsultaCalendarioBD(listSemana, lista.Where(x => x.Id_TG == 2).ToList(), this.rgAcuerdos_Comensal.ID);
                ConsultaCalendarioBD(listSemana, lista.Where(x => x.Id_TG == 3).ToList(), this.rgAcuerdos_Habitacion.ID);
                ConsultaCalendarioBD(listSemana, lista.Where(x => x.Id_TG == 4).ToList(), this.rgAcuerdos_Iguala.ID);


                if (lista.Where(x => x.Id_TG == 0).ToList().Count == 0)
                {
                    modoEdicion0 = true;
                }
                else
                {
                    modoEdicion0 = false;
                }

                if (lista.Where(x => x.Id_TG == 1).ToList().Count == 0)
                {
                    modoEdicion1 = true;
                }
                else
                {
                    modoEdicion1 = false;
                }

                if (lista.Where(x => x.Id_TG == 2).ToList().Count == 0)
                {
                    modoEdicion2 = true;
                }
                else
                {
                    modoEdicion2 = false;
                }

                if (lista.Where(x => x.Id_TG == 3).ToList().Count == 0)
                {
                    modoEdicion3 = true;
                }
                else
                {
                    modoEdicion3 = false;
                }

                if (lista.Where(x => x.Id_TG == 4).ToList().Count == 0)
                {
                    modoEdicion4 = true;
                }
                else
                {
                    modoEdicion4 = false;
                }


                //Edsg 21102015
                AcysDatosGarantia datosGar = new AcysDatosGarantia();
                datosGar.Id_Emp = acys.Id_Emp;
                datosGar.Id_Cd = acys.Id_Cd;
                datosGar.Id_Acs = acys.Id_Acs;
                datosGar.Id_AcsVersion = acys.Id_AcsVersion;
                datosGar.Id_TG = -1;

                List<AcysDatosGarantia> listaDatosGar = cn_capacys.DatosGarantia_Consulta(sesion.Emp_Cnx, datosGar);

                if (listaDatosGar.Exists(x => x.Id_TG == 1))
                {
                    var datos = listaDatosGar.FirstOrDefault(x => x.Id_TG == 1);
                    this.Fac_Kilo.Text = datos.FactorGarantia.ToString();
                    this.PNeta_Kilo.Text = datos.UPrimaNeta.ToString();
                    //  this.FCorte_Kilo.SelectedDate = datos.FechaCorte;

                    Session["Fechas_1"] = datos.Fechas_Corte;
                }

                if (listaDatosGar.Exists(x => x.Id_TG == 2))
                {
                    var datos = listaDatosGar.FirstOrDefault(x => x.Id_TG == 2);
                    this.Fac_Comensal.Text = datos.FactorGarantia.ToString();
                    this.PNeta_Comensal.Text = datos.UPrimaNeta.ToString();
                    // this.FCorte_Comensal.SelectedDate = datos.FechaCorte;

                    Session["Fechas_2"] = datos.Fechas_Corte;
                }

                if (listaDatosGar.Exists(x => x.Id_TG == 3))
                {
                    var datos = listaDatosGar.FirstOrDefault(x => x.Id_TG == 3);
                    this.Fac_Habitacion.Text = datos.FactorGarantia.ToString();
                    this.PNeta_Habitacion.Text = datos.UPrimaNeta.ToString();
                    //this.FCorte_Habitacion.SelectedDate = datos.FechaCorte;

                    Session["Fechas_3"] = datos.Fechas_Corte;
                }

                if (listaDatosGar.Exists(x => x.Id_TG == 4))
                {
                    var datos = listaDatosGar.FirstOrDefault(x => x.Id_TG == 4);
                    this.Fac_Iguala.Text = datos.FactorGarantia.ToString();
                    this.PNeta_Iguala.Text = datos.UPrimaNeta.ToString();
                    //   this.FCorte_Iguala.SelectedDate = datos.FechaCorte;

                    Session["Fechas_4"] = datos.Fechas_Corte;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CerrarVentana()
        {
            try
            {
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSegmento() //Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN_CatSegmentos CN_CatSegmentos = new CapaNegocios.CN_CatSegmentos();
                int terr = !string.IsNullOrEmpty(cmbTerritorio.SelectedValue) ? Convert.ToInt32(cmbTerritorio.SelectedValue) : 0;
                Segmentos segmento = new Segmentos();

                CN_CatSegmentos.ConsultaSegmentoTer(Sesion.Id_Emp, Sesion.Id_Cd_Ver, terr, Sesion.Emp_Cnx, ref segmento);

                txtSegmento.Text = segmento.Descripcion;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarRik() //Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int terr = !string.IsNullOrEmpty(cmbTerritorio.SelectedValue) ? Convert.ToInt32(cmbTerritorio.SelectedValue) : 0;
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, terr, 1, Sesion.Emp_Cnx, "spCatRikTerr_Combo", ref cmbRepresentante);
                if (cmbRepresentante.Items.Count > 1)
                {
                    cmbRepresentante.SelectedIndex = 1;
                    txtRepresentante.Text = cmbRepresentante.Items[1].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRutaEntrega() //Local
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRutaEnt_Combo", ref cmbRutaEntrega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRutaServicio() //Local
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRutaSer_Combo", ref cmbRutaServicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1), Sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmbTerritorio);
                try
                {
                    if (cmbTerritorio.Items.Count > 1)
                    {
                        cmbTerritorio.SelectedIndex = 1;
                        txtTerritorio.Text = cmbTerritorio.Items[1].Value;
                        CargarRik();
                        CargarSegmento();
                    }
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapAcys", "Id_Acs", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AcysPrevio()
        {
            try
            {
                Nuevo();
                if (!txtCliente.Value.HasValue || cmbTerritorio.SelectedIndex == 0)
                    return;

                CN_CapAcys cn_capacys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Ter = cmbTerritorio.SelectedValue != "" ? Convert.ToInt32(cmbTerritorio.SelectedValue) : -2;
                acys.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                acys.Filtro_Estatus = "TEB";
                acys.Id_Rik = -1;
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                acys.Acs_Vencido = "No";
                List<Acys> list = new List<Acys>();
                cn_capacys.ConsultarAcys_Lista(acys, sesion.Emp_Cnx, ref list);
                if (list.Count > 0)
                {
                    Alerta("Ya existe un acuerdo para el cliente <b>" + txtClienteNombre.Text + "</b> con el territorio <b>#" + cmbTerritorio.SelectedValue + "</b>");
                    txtTerritorio.Text = "";
                    cmbTerritorio.SelectedIndex = 0;
                    cmbTerritorio.Text = "";
                    txtRepresentante.Text = "";
                    cmbRepresentante.SelectedIndex = 0;
                    cmbRepresentante.Text = "";
                    cmbRepresentante.Items.Clear();
                    txtCliente.Focus();
                }
                else
                {
                    if (Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1) != -1)
                    {
                        CN_CatCliente cn_catcliente = new CN_CatCliente();
                        Clientes cc = new Clientes();
                        cc.Id_Emp = sesion.Id_Emp;
                        cc.Id_Cd = sesion.Id_Cd_Ver;
                        cc.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                        try
                        {
                            cn_catcliente.ConsultaClientes(ref cc, sesion.Emp_Cnx);
                            txtComercial.Text = cc.Cte_NomComercial;
                            txtContacto.Text = cc.Cte_Contacto;
                            txtEmail.Text = cc.Cte_Email;
                        }
                        catch (Exception ex)
                        {
                            AlertaFocus(ex.Message, txtCliente.ClientID);
                            txtComercial.Text = "";
                            txtContacto.Text = "";
                            txtEmail.Text = "";
                            return;
                        }
                        Funciones funcion = new Funciones();
                        rdFecha.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                        rdFecha.Focus();
                        GetListGrl();
                        rgAcuerdos.Rebind();
                        rgAcuerdos_Kilo.Rebind();
                        rgAcuerdos_Comensal.Rebind();
                        rgAcuerdos_Habitacion.Rebind();
                        rgAcuerdos_Iguala.Rebind();
                    }
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
                txtComercial.Text = string.Empty;
                txtContacto.Text = string.Empty;
                txtEmail.Text = string.Empty;
                HF_Sustituye.Value = "";
                HF_ID.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void MostrarGrids()
        {
            if (!listaGarantia.Exists(x => x.id_TG == 0))
                this.div_Regular.Visible = false;
            else
                div_Regular.Visible = true;

            if (!listaGarantia.Exists(x => x.id_TG == 1))
                div_Kilo.Visible = false;
            else
                div_Kilo.Visible = true;

            if (!listaGarantia.Exists(x => x.id_TG == 2))
                div_Comensal.Visible = false;
            else
                div_Comensal.Visible = true;

            if (!listaGarantia.Exists(x => x.id_TG == 3))
                div_Habitacion.Visible = false;
            else
                div_Habitacion.Visible = true;

            if (!listaGarantia.Exists(x => x.id_TG == 4))
                this.div_iguala.Visible = false;
            else
                div_iguala.Visible = true;

        }
        private void Limpiar()
        {
            txtComercial.Text = string.Empty;
            txtContacto.Text = string.Empty;
            txtEmail.Text = string.Empty;

            if (cmbRepresentante.Items.Count > 0)
            {
                cmbRepresentante.SelectedIndex = 0;
                cmbRepresentante.Text = cmbRepresentante.Items[0].Text;

            }
            txtRepresentante.Value = null;
            //txtClienteNombre.Text = "";
            //txtCliente.Value = null;

            GetListGrl();
            rgAcuerdos.Rebind();
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
        private void ValidarPermisos()
        {
            try
            {
                if (_PermisoGuardar == false)
                    this.rtb1.Items[5].Visible = false;
                if (_PermisoGuardar == false & _PermisoModificar == false)
                    this.rtb1.Items[5].Visible = false;
                //Regresar
                this.rtb1.Items[4].Visible = false;
                //Eliminar
                this.rtb1.Items[3].Visible = false;
                //Imprimir
                this.rtb1.Items[2].Visible = false;
                //Correo
                this.rtb1.Items[1].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetListGrl_Detalle(DataTable dt)
        {
            dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
            dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
            dt.Columns.Add("Prd_UniNom", System.Type.GetType("System.String"));
            dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
            dt.Columns.Add("Acys_Cantidad", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Acys_Frecuencia", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Acys_Lunes", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Acys_Martes", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Acys_Miercoles", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Acys_Jueves", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Acys_Viernes", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Acys_Sabado", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
            dt.Columns.Add("Acs_DocStr", System.Type.GetType("System.String"));
            dt.Columns.Add("Acys_ConsigFechaInicio", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Acys_ConsigFechaFin", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Acys_CantTotal", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Acys_UltSCtp", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Acys_UltACtp", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Id_TG", System.Type.GetType("System.Int32"));
        }
        private void GetListGrl()
        {
            try
            {
                /*
                                dtAcuerdos = new DataTable();
                                dtAcuerdos.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                                dtAcuerdos.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                                dtAcuerdos.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                                dtAcuerdos.Columns.Add("Prd_UniNom", System.Type.GetType("System.String"));
                                dtAcuerdos.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                                dtAcuerdos.Columns.Add("Acys_Cantidad", System.Type.GetType("System.Int32"));
                                dtAcuerdos.Columns.Add("Acys_Frecuencia", System.Type.GetType("System.Int32"));
                                dtAcuerdos.Columns.Add("Acys_Lunes", System.Type.GetType("System.Boolean"));
                                dtAcuerdos.Columns.Add("Acys_Martes", System.Type.GetType("System.Boolean"));
                                dtAcuerdos.Columns.Add("Acys_Miercoles", System.Type.GetType("System.Boolean"));
                                dtAcuerdos.Columns.Add("Acys_Jueves", System.Type.GetType("System.Boolean"));
                                dtAcuerdos.Columns.Add("Acys_Viernes", System.Type.GetType("System.Boolean"));
                                dtAcuerdos.Columns.Add("Acys_Sabado", System.Type.GetType("System.Boolean"));
                                dtAcuerdos.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                                dtAcuerdos.Columns.Add("Acs_DocStr", System.Type.GetType("System.String"));
                                dtAcuerdos.Columns.Add("Acys_ConsigFechaInicio", System.Type.GetType("System.DateTime"));
                                dtAcuerdos.Columns.Add("Acys_ConsigFechaFin", System.Type.GetType("System.DateTime"));
                                dtAcuerdos.Columns.Add("Acys_CantTotal", System.Type.GetType("System.Int32"));
                                dtAcuerdos.Columns.Add("Acys_UltSCtp", System.Type.GetType("System.Int32"));
                                dtAcuerdos.Columns.Add("Acys_UltACtp", System.Type.GetType("System.Int32"));*/

                dtAcuerdos = new DataTable(); this.GetListGrl_Detalle(dtAcuerdos);
                dtAcuerdos_Kilo = new DataTable(); this.GetListGrl_Detalle(dtAcuerdos_Kilo);
                dtAcuerdos_Comensal = new DataTable(); this.GetListGrl_Detalle(dtAcuerdos_Comensal);
                dtAcuerdos_Habitacion = new DataTable(); this.GetListGrl_Detalle(dtAcuerdos_Habitacion);
                dtAcuerdos_Iguala = new DataTable(); this.GetListGrl_Detalle(dtAcuerdos_Iguala);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete(GridCommandEventArgs e)
        {
            DataTable dt = dtAcuerdos;

            if (e.Item.OwnerGridID.Contains("Kilo")) dt = dtAcuerdos_Kilo;
            else if (e.Item.OwnerGridID.Contains("Habitacion")) dt = dtAcuerdos_Habitacion;
            else if (e.Item.OwnerGridID.Contains("Iguala")) dt = dtAcuerdos_Iguala;
            else if (e.Item.OwnerGridID.Contains("Comensal")) dt = dtAcuerdos_Comensal;
            GridItem gi = e.Item;
            int Id_Prd = Convert.ToInt32(((Label)gi.FindControl("lblIdPrd")).Text);
            DataRow[] Ar_dr = dtAcuerdos.Select("Id_Prd='" + Id_Prd + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dtAcuerdos.AcceptChanges();
            }
        }
        private void PerformInsert(GridCommandEventArgs e)
        {
            try
            {
                Boolean EsGarantia = false;
                DataTable dtInsertar = dtAcuerdos;

                if (e.Item.OwnerGridID.Contains("Kilo")) { dtInsertar = dtAcuerdos_Kilo; EsGarantia = true; }
                else if (e.Item.OwnerGridID.Contains("Habitacion")) { dtInsertar = dtAcuerdos_Habitacion; EsGarantia = true; }
                else if (e.Item.OwnerGridID.Contains("Iguala")) { dtInsertar = dtAcuerdos_Iguala; EsGarantia = true; }
                else if (e.Item.OwnerGridID.Contains("Comensal")) { dtInsertar = dtAcuerdos_Comensal; EsGarantia = true; }
                int Id_Prd = 0;
                string Prd_Descripcion = "";
                string Prd_Presentacion = "";
                string Prd_UniNom = "";
                double Prd_Precio = 0;
                int Acys_Cantidad = 0;
                int Acys_Frecuencia = 0;
                bool Acys_Lunes = false;
                bool Acys_Martes = false;
                bool Acys_Miercoles = false;
                bool Acys_Jueves = false;
                bool Acys_Viernes = false;
                bool Acys_Sabado = false;
                string Acs_Doc = "";
                string Acs_DocStr = "";
                DateTime? Acys_FechaInicio;
                DateTime? Acys_FechaFin;
                int Acys_cantTotal = 0;
                GridItem gi = e.Item;

                if (
                    Convert.ToInt32(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.Value : -1) == -1 ||
                    (((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" && !EsGarantia) ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text == "" ||
                    ((RadComboBox)gi.FindControl("txtDocumento")).Text == "" ||
                    rdModConsignacion.Checked && ((RadDatePicker)gi.FindControl("tpConsigFechaInicio")).SelectedDate == null ||
                    rdModConsignacion.Checked && ((RadDatePicker)gi.FindControl("tpConsigFechaFin")).SelectedDate == null ||
                    rdModConsignacion.Checked && !((RadNumericTextBox)gi.FindControl("txtCantTotal")).Value.HasValue

                    )
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return;
                }
                if (!(((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkLunes")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkMartes")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkMiercoles")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkJueves")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkViernes")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkSabado")).Checked))
                {
                    e.Canceled = true;
                    this.Alerta("Seleccione por lo menos un día de entrega");
                    return;
                }

                Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.Value : -1);
                Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                Prd_Presentacion = ((Label)gi.FindControl("lblPresentacionEd")).Text;
                Prd_UniNom = ((Label)gi.FindControl("lblUniEd")).Text;

                if (((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "") Prd_Precio = 0;
                else Prd_Precio = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text);

                Acys_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                Acys_Frecuencia = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text);
                Acys_Lunes = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkLunes")).Checked;
                Acys_Martes = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkMartes")).Checked;
                Acys_Miercoles = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkMiercoles")).Checked;
                Acys_Jueves = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkJueves")).Checked;
                Acys_Viernes = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkViernes")).Checked;
                Acys_Sabado = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkSabado")).Checked;
                Acs_Doc = ((RadComboBox)gi.FindControl("txtDocumento")).SelectedValue;
                Acs_DocStr = ((RadComboBox)gi.FindControl("txtDocumento")).Text;
                Acys_FechaInicio = DateTime.Now;
                if (((RadDatePicker)gi.FindControl("tpConsigFechaInicio")).SelectedDate != null)
                {
                    Acys_FechaInicio = ((RadDatePicker)gi.FindControl("tpConsigFechaInicio")).SelectedDate;

                }
                Acys_FechaFin = DateTime.Now;
                if (((RadDatePicker)gi.FindControl("tpConsigFechaFin")).SelectedDate != null)
                {
                    Acys_FechaFin = ((RadDatePicker)gi.FindControl("tpConsigFechaFin")).SelectedDate;

                }


                Acys_cantTotal = 0;
                if (((RadNumericTextBox)gi.FindControl("txtCantTotal")).Value.HasValue)
                {
                    Acys_cantTotal = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantTotal")).Text);

                }

                if (Prd_Precio <= 0 && !EsGarantia)
                {
                    e.Canceled = true;
                    Alerta("El precio de venta debe ser mayor a cero");
                    return;
                }


                DataRow[] Ar_Dr = dtAcuerdos.Select("Id_Prd='" + Id_Prd + "'");
                if (Ar_Dr.Length > 0)
                {
                    e.Canceled = true;
                    this.Alerta("El producto ya fue incluido en este acuerdo");
                    return;
                }
                //dtAcuerdos.Rows.Add(new object[] { 
                dtInsertar.Rows.Add(new object[] {
                Id_Prd, 
                Prd_Descripcion, 
                Prd_Presentacion, 
                Prd_UniNom, 
                Prd_Precio, 
                Acys_Cantidad,
                Acys_Frecuencia, 
                Acys_Lunes,
                Acys_Martes,
                Acys_Miercoles,
                Acys_Jueves,
                Acys_Viernes,
                Acys_Sabado,
                Acs_Doc,
                Acs_DocStr,              
                Acys_FechaInicio,
                Acys_FechaFin,
                Acys_cantTotal
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void rdFechaInicioDocumento_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                RadDatePicker dpIni = sender as RadDatePicker;
                RadDatePicker dpFin = (dpIni.Parent.FindControl("rdFechaFinDocumento") as RadDatePicker);


                if (dpIni.SelectedDate.HasValue && dpFin.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate > dpFin.SelectedDate)
                    {
                        AlertaFocus("La fecha de fin de Vigencia debe ser mayor a la fecha de inicio", dpFin.DateInput.ClientID);
                        dpFin.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        dpFin.DateInput.Focus();
                    }

                }
                else if (dpIni.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate < Sesion.CalendarioIni)
                    {
                        AlertaFocus("La fecha de inicio debe ser mayor o igual a la fecha de inicio del periodo actual", dpIni.DateInput.ClientID);
                        dpIni.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        //Edsg Pone una fecha final al calendario
                        DateTime SemanaFinal = DateTime.MinValue;
                        CN_CatSemana CNSem = new CN_CatSemana();
                        CNSem.ConsultaSemanaMaxCalendario(Convert.ToDateTime(rdFechaInicioDocumento.SelectedDate), ref SemanaFinal, sesion.Emp_Cnx);

                        rdFechaFinDocumento.MaxDate = SemanaFinal;
                        rdFechaFinDocumento.SelectedDate = SemanaFinal;
                        //rdFechaFinDocumento.Enabled = true;
                        //dpFin.DateInput.Focus();
                    }
                }
                else
                {
                    dpIni.DateInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rdFechaFinDocumento_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                if (rdFechaInicioDocumento.IsEmpty)
                {
                    AlertaFocus("Debe capturar una fecha de inicio de Vigencia, antes de la fecha de fin.", null);
                    rdFechaFinDocumento.SelectedDate = null;
                    return;
                }

                RadDatePicker dpFin = sender as RadDatePicker;
                RadDatePicker dpIni = (dpFin.Parent.FindControl("rdFechaInicioDocumento") as RadDatePicker);
                DateTime? oldDate;
                DateTime? newDate;

                if (dpIni.SelectedDate.HasValue && dpFin.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate > dpFin.SelectedDate)
                    {
                        AlertaFocus("La fecha de fin de Vigencia no debe ser mayor a la fecha de inicio", dpFin.DateInput.ClientID);
                        dpFin.DbSelectedDate = null;
                        return;
                    }

                    oldDate = dpIni.SelectedDate;
                    newDate = dpFin.SelectedDate;
                    if (oldDate != null && newDate != null)
                    {
                        DateTime FechaIni = oldDate.Value;
                        DateTime FechaFin = newDate.Value;

                        TimeSpan ts = FechaFin - FechaIni;


                        int DiferenciaDias = ts.Days;

                        if (DiferenciaDias > 365)
                        {
                            AlertaFocus("La Vigencia del Acuerdo no puede ser mayor a un Año", dpFin.DateInput.ClientID);
                            dpFin.DbSelectedDate = null;
                            return;
                        }
                    }

                }
                else if (dpIni.SelectedDate.HasValue)
                {
                    dpFin.DateInput.Focus();
                }
                else
                {
                    dpIni.DateInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void BtnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string mensaje = string.Empty;

                int verificador = 0;


                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                CN_CapAcys cn_acys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = Sesion.Id_Emp;
                acys.Id_Cd = Sesion.Id_Cd_Ver;
                acys.Id_Acs = Convert.ToInt32(HF_ID.Value);
                acys.Id_AcsVersion = Convert.ToInt32(txtVersion.Text);


                cn_acys.AutorizarAcys(acys, Sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    verificador = CambiarEstatus(Convert.ToInt32(HF_ID.Value), Convert.ToInt32(txtVersion.Text), "A");
                    Alerta("La solicitud de Autorización ha sido atendida");
                    EnviaEmail(Convert.ToInt32(HF_ID.Value));
                    ExportToPDF(Convert.ToInt32(HF_ID.Value));
                    RAM1.ResponseScripts.Add("return AbrirVentana_EnviarCorreo('" + Convert.ToInt32(HF_ID.Value) + "')");
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }



        protected void BtnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string mensaje = string.Empty;

                int verificador = 0;


                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }


                verificador = CambiarEstatus(Convert.ToInt32(HF_ID.Value), Convert.ToInt32(txtVersion.Text), "C");



                if (verificador == 1)
                {
                    EnviaEmail(Convert.ToInt32(HF_ID.Value));
                    Alerta("La solicitud de Autorización ha sido atendida");
                }



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }


        private void Update(GridCommandEventArgs e)
        {
            try
            {
                int Id_Prd = 0;
                string Prd_Descripcion = "";
                string Prd_Presentacion = "";
                string Prd_UniNom = "";
                double Prd_Precio = 0;
                int Acys_Cantidad = 0;
                int Acys_Frecuencia = 0;
                bool Acys_Lunes = false;
                bool Acys_Martes = false;
                bool Acys_Miercoles = false;
                bool Acys_Jueves = false;
                bool Acys_Viernes = false;
                bool Acys_Sabado = false;
                string Acs_Doc = "";
                string Acs_DocStr = "";
                DateTime? Acys_FechaInicio;
                DateTime? Acys_FechaFin;
                int Acys_cantTotal = 0;

                GridItem gi = e.Item;

                Boolean EsGarantia = false;

                if (gi.OwnerID.Contains("Kilo") || gi.OwnerID.Contains("Iguala") || gi.OwnerID.Contains("Habitacion") || gi.OwnerID.Contains("Comensal"))
                    EsGarantia = true;
                if (
                    Convert.ToInt32(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.Value : -1) == -1 ||
                    (((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "") ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text == "" ||
                    ((RadComboBox)gi.FindControl("txtDocumento")).Text == "" ||
                    rdModConsignacion.Checked && ((RadDatePicker)gi.FindControl("tpConsigFechaInicio")).SelectedDate == null ||
                    rdModConsignacion.Checked && ((RadDatePicker)gi.FindControl("tpConsigFechaFin")).SelectedDate == null ||
                    rdModConsignacion.Checked && !((RadNumericTextBox)gi.FindControl("txtCantTotal")).Value.HasValue

                    )
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return;
                }


                if (!(((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkLunes")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkMartes")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkMiercoles")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkJueves")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkViernes")).Checked ||
                    ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkSabado")).Checked))
                {
                    //e.Canceled = true;
                    this.Alerta("Seleccione por lo menos un día de entrega");
                    return;
                }

                Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.Value : -1);
                Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                Prd_Presentacion = ((Label)gi.FindControl("lblPresentacionEd")).Text;
                Prd_UniNom = ((Label)gi.FindControl("lblUniEd")).Text;
                Prd_Precio = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text);
                Acys_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                Acys_Frecuencia = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text);
                Acys_Lunes = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkLunes")).Checked;
                Acys_Martes = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkMartes")).Checked;
                Acys_Miercoles = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkMiercoles")).Checked;
                Acys_Jueves = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkJueves")).Checked;
                Acys_Viernes = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkViernes")).Checked;
                Acys_Sabado = ((System.Web.UI.WebControls.CheckBox)gi.FindControl("chkSabado")).Checked;
                Acs_Doc = ((RadComboBox)gi.FindControl("txtDocumento")).SelectedValue;
                Acs_DocStr = ((RadComboBox)gi.FindControl("txtDocumento")).Text;
                Acys_FechaInicio = DateTime.Now;
                if (((RadDatePicker)gi.FindControl("tpConsigFechaInicio")).SelectedDate != null)
                {
                    Acys_FechaInicio = ((RadDatePicker)gi.FindControl("tpConsigFechaInicio")).SelectedDate;

                }
                Acys_FechaFin = DateTime.Now;
                if (((RadDatePicker)gi.FindControl("tpConsigFechaFin")).SelectedDate != null)
                {
                    Acys_FechaFin = ((RadDatePicker)gi.FindControl("tpConsigFechaFin")).SelectedDate;

                }


                Acys_cantTotal = 0;
                if (((RadNumericTextBox)gi.FindControl("txtCantTotal")).Value.HasValue)
                {
                    Acys_cantTotal = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantTotal")).Text);

                }

                if (Prd_Precio <= 0 && !EsGarantia)
                {
                    e.Canceled = true;
                    Alerta("El precio de venta debe ser mayor a cero");
                    return;
                }

                DataTable dt = dtAcuerdos;
                if (e.Item.OwnerGridID.Contains("Kilo")) dt = dtAcuerdos_Kilo;
                else if (e.Item.OwnerGridID.Contains("Habitacion")) dt = dtAcuerdos_Habitacion;
                else if (e.Item.OwnerGridID.Contains("Iguala")) dt = dtAcuerdos_Iguala;
                else if (e.Item.OwnerGridID.Contains("Comensal")) dt = dtAcuerdos_Comensal;

                DataRow[] Ar_dr = dt.Select("Id_Prd='" + Id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Prd"] = Id_Prd;
                    Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;
                    Ar_dr[0]["Prd_UniNom"] = Prd_UniNom;
                    Ar_dr[0]["Prd_Precio"] = Prd_Precio;
                    Ar_dr[0]["Acys_Cantidad"] = Acys_Cantidad;
                    Ar_dr[0]["Acys_Frecuencia"] = Acys_Frecuencia;
                    Ar_dr[0]["Acys_Lunes"] = Acys_Lunes;
                    Ar_dr[0]["Acys_Martes"] = Acys_Martes;
                    Ar_dr[0]["Acys_Miercoles"] = Acys_Miercoles;
                    Ar_dr[0]["Acys_Jueves"] = Acys_Jueves;
                    Ar_dr[0]["Acys_Viernes"] = Acys_Viernes;
                    Ar_dr[0]["Acys_Sabado"] = Acys_Sabado;
                    Ar_dr[0]["Acs_Doc"] = Acs_Doc;
                    Ar_dr[0]["Acs_DocStr"] = Acs_DocStr;
                    Ar_dr[0]["Acys_ConsigFechaInicio"] = Acys_FechaInicio;
                    Ar_dr[0]["Acys_ConsigFechaFin"] = Acys_FechaFin;
                    Ar_dr[0]["Acys_cantTotal"] = Acys_cantTotal;
                    Ar_dr[0].AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CambiarEstatus(int Id_Acs, int id_AcsVersion, string estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapAcys cn_acys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = session.Id_Emp;
                acys.Id_Cd = session.Id_Cd_Ver;
                acys.Id_Acs = Id_Acs;
                acys.Id_AcsVersion = id_AcsVersion;
                acys.Acs_Estatus = estatus;
                int verificador = -1;
                cn_acys.actualizarEstatus(acys, session.Emp_Cnx, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void InitInsert(GridCommandEventArgs e)
        {

        }
        #endregion
        #region ErrorManager
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
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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
        protected double CalculaSubtotal(double x, double y)
        {
            return x * y;
        }











        private void PintarCalendario(RadGrid rg)
        {


            int m = 1;
            int itemi = 0;

            foreach (GridDataItem item in rg.Items)
            {

                int prod = Convert.ToInt32(((Label)(item["Id_Prd"].Controls[1])).Text);
                int frec = Convert.ToInt32(((Label)(item["Acys_Frecuencia"].Controls[1])).Text);
                int semInicio = Convert.ToInt32(this.txtSemana.Text);

                int semCurso = 1;
                int frecCont = frec;

                int ia = 0;
                for (m = 1; m <= 12; m++)
                {
                    HtmlTable cal = (HtmlTable)RPCalendario.FindControl("tblCalendario_" + m.ToString());
                    int i = 1;

                    if (itemi == 0)
                        foreach (HtmlTableRow r in cal.Rows)
                            foreach (HtmlTableCell cel in r.Cells)
                            {
                                if (r.ID == null || !r.ID.Contains("encabezado"))
                                {
                                    if (!IniciaCalendario) cel.InnerText = "";
                                    cel.InnerHtml += "<div  id='div_Cal_Main_" + ia.ToString() + "_" + rg.ID + "' class='divItemsCalendario_" + rg.ID + "' ondrop='drop(event)' ondragover='allowDrop(event)' ondragenter='dragEnter(event)'><img  src='Imagenes/flecha.jpg' ondragstart='drag(event)' draggable='true'";
                                    // if (!IniciaCalendario) cel.InnerText = "";
                                    cel.InnerHtml += ">";
                                }
                                ia++;
                            }


                    foreach (HtmlTableRow r in cal.Rows)
                    {
                        int semMesCurso = 1;


                        if (r.ID == null || !r.ID.Contains("encabezado"))
                            foreach (HtmlTableCell cel in r.Cells)
                            {
                                //if (semInicio == semCurso)
                                //{

                                //    HtmlTable calControl = (HtmlTable)RPCalendario.FindControl("tCal" + ((m / 2) + 1).ToString());
                                //    calControl.Style.Add("display", "block");
                                //}

                                if (semMesCurso == listSemana[semCurso - 1].Id_SemxMes)
                                {
                                    if (semInicio <= semCurso)
                                    {
                                        if (frecCont == frec)
                                        {
                                            // Valida 4 semanas
                                            if (frec == 4 && semMesCurso == 5)
                                            {
                                                frecCont--;
                                            }
                                            else
                                            {
                                                cel.InnerHtml = cel.InnerHtml + "<div id='divimg_" + semCurso.ToString() + "_" + prod.ToString() + "'><img  src='Imagenes/Inventarios20.png' ondragstart='drag(event)' draggable='true'><label>"
                                                    + prod.ToString() + "</label>&nbsp;<img src='Imagenes/delete2.png' style='cursor:pointer' onclick='$(this).prev().prev().remove();$(this).prev().remove();$(this).remove(); LlenaDatosCalendario(); '/><br></div>";

                                                frecCont = 0;
                                            }
                                        }

                                        frecCont++;
                                    }

                                    cal.Rows[1].Cells[semMesCurso - 1].InnerText = "Semana " + semCurso.ToString();
                                }
                                else
                                {
                                    semCurso--;
                                    //r.Cells.Remove(cel);
                                    //cal.Rows[1].Cells.RemoveAt(semMesCurso - 1);

                                    cel.Visible = false;
                                    cal.Rows[1].Cells[semMesCurso - 1].Visible = false;
                                }
                                semMesCurso++;
                                semCurso++;
                            }


                    }


                }

                itemi++;

                IniciaCalendario = true;
            }


            // Cerrar el div
            for (m = 1; m <= 12; m++)
            {
                HtmlTable cal = (HtmlTable)RPCalendario.FindControl("tblCalendario_" + m.ToString());

                foreach (HtmlTableRow r in cal.Rows)
                    foreach (HtmlTableCell cel in r.Cells)
                    {
                        if (r.ID == null || !r.ID.Contains("encabezado"))
                            cel.InnerHtml += "</div>";

                    }
            }

        }














        private void ConsultaCalendarioBD(List<Semana> listSemana, List<CalendarioControl> lista, String GridId)
        {
            int m = 1;
            int itemi = 0;


            if (lista.Count == 0)
            {
                EsInicio = true;
                return;
            }



            /*******/

            int semInicio = Convert.ToInt32(this.txtSemana.Text);
            int semCurso = 1;


            int ia = 0;


            for (m = 1; m <= 12; m++)
            {
                HtmlTable cal = (HtmlTable)RPCalendario.FindControl("tblCalendario_" + m.ToString());
                int i = 1;



                foreach (HtmlTableRow r in cal.Rows)
                    foreach (HtmlTableCell cel in r.Cells)
                    {
                        if (r.ID == null || !r.ID.Contains("encabezado"))
                        {
                            if (!IniciaCalendario) cel.InnerText = "";
                            cel.InnerHtml += "<div  id='div_Cal_Main_" + ia.ToString() + "_" + GridId + "' class='divItemsCalendario_" + GridId + "' ondrop='drop(event)' ondragover='allowDrop(event)' ondragenter='dragEnter(event)'><img  src='Imagenes/flecha.jpg' ondragstart='drag(event)' draggable='true'>";


                        }
                        ia++;
                    }


                foreach (HtmlTableRow r in cal.Rows)
                {
                    int semMesCurso = 1;

                    if (r.ID == null || !r.ID.Contains("encabezado"))
                        foreach (HtmlTableCell cel in r.Cells)
                        {

                            var itemssel = lista.Where(x => x.Semana == semCurso).ToList();


                            if (semMesCurso == listSemana[semCurso - 1].Id_SemxMes)
                            {
                                foreach (CalendarioControl item in itemssel)
                                {
                                    cel.InnerHtml = cel.InnerHtml + "<div  id='divimg_" + semCurso.ToString() + "_" + item.IdProd.ToString() + "'><img  src='Imagenes/Inventarios20.png' ondragstart='drag(event)' draggable='true'><label>"
                                     + item.IdProd.ToString() + "</label>&nbsp;<img src='Imagenes/delete2.png' style='cursor:pointer' onclick='$(this).prev().prev().remove();$(this).prev().remove();$(this).remove(); LlenaDatosCalendario(); '/><br></div>";
                                }

                                cal.Rows[1].Cells[semMesCurso - 1].InnerText = "Semana " + semCurso.ToString();
                            }
                            else
                            {
                                semCurso--;
                                cel.Visible = false;
                                cal.Rows[1].Cells[semMesCurso - 1].Visible = false;
                            }

                            semMesCurso++;
                            semCurso++;
                        }
                }



                itemi++;
            }


            // Cerrar el div
            for (m = 1; m <= 12; m++)
            {
                HtmlTable cal = (HtmlTable)RPCalendario.FindControl("tblCalendario_" + m.ToString());

                foreach (HtmlTableRow r in cal.Rows)
                    foreach (HtmlTableCell cel in r.Cells)
                    {
                        if (r.ID == null || !r.ID.Contains("encabezado"))
                            cel.InnerHtml += "</div>";
                    }
            }

            IniciaCalendario = true;
        }



















        //private void ActualizaControlCalendario(Acys acys, List<CalendarioControl> listaCalendario)
        //{
        //    int ver = 0;
        //    CN_CatCalendarioControl CN = new CN_CatCalendarioControl();
        //    //List<CalendarioControl> listaCalendario = new List<CalendarioControl>();

        //    string[] items = this.txtValoresCalendario.Value.Split(',');

        //    foreach (string it in items)
        //    {
        //        if (it != "")
        //        {
        //            CalendarioControl calItem = new CalendarioControl();
        //            calItem.Id_Emp = acys.Id_Emp;
        //            calItem.Id_Acs = acys.Id_Acs;
        //            calItem.Id_AcsVersion = acys.Id_AcsVersion;
        //            calItem.Id_Cd = acys.Id_Cd;
        //            calItem.Cal_Año = DateTime.Now.Year;
        //            calItem.Semana = Convert.ToInt32(it.Split('_')[0]);
        //            calItem.IdProd = Convert.ToInt32(it.Split('_')[1]);
        //            calItem.Id_TG  =  Convert.ToInt32(it.Split('_')[2]);

        //            listaCalendario.Add(calItem);
        //        }
        //    }

        //    CN.GuardarCalendarioControl(ref listaCalendario, sesion.Emp_Cnx, ref ver);
        //}


    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using CapaNegocios;
using System.IO;

namespace SIANWEB
{
    public partial class CatCalendario : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        static int mes_siguiente = 1;
        //ultima fecha es la fecha final del ultimo periodo encontrado en la base de datos
        static DateTime ultima_fecha;
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

                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (session.Cu_Modif_Pass_Voluntario == false)
                        {
                            RadAjaxManager1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();
                    }
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
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    List<Calendario> lista = new List<Calendario>();
                    lista = GetList();
                    if (lista.Count > 0)
                    {
                        //Button1.Enabled = false; 
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = false;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = false;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = _PermisoEliminar;
                    }
                    else
                    {
                        //Button1.Enabled = true;    
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = _PermisoGuardar;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = _PermisoEliminar;
                    }
                    this.rgCalendario.DataSource = lista;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void rgGuardar_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgGuardar.DataSource = dt;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void rgGuardar_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                if (mes_siguiente > 12)
                {
                    Alerta("Ya existen 12 periodos del año");
                    e.Canceled = true;
                }
            }
        }
        protected void rgGuardar_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                //Telerik.Web.UI.GridLinkButton
                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                {
                    //
                    rgGuardar.Columns.FindByUniqueName("Cal_Año").Visible = true;
                    rgGuardar.Columns.FindByUniqueName("EditCommandColumn").Visible = true;

                    (editItem.FindControl("Cal_AñoTextBox0") as RadNumericTextBox).Text = RadComboBoxAño.SelectedValue;
                    (editItem.FindControl("RadComboBox2") as RadComboBox).SelectedValue = mes_siguiente.ToString();
                    (editItem.FindControl("Cal_AñoTextBox0") as RadNumericTextBox).Enabled = false;
                    (editItem.FindControl("RadComboBox2") as RadComboBox).Enabled = false;

                    if (ultima_fecha == DateTime.Parse("01/01/0001"))
                    {
                        (editItem.FindControl("Cal_fechainiRadDatePicker0") as RadDatePicker).SelectedDate = DateTime.Parse("01/" + mes_siguiente.ToString() + "/" + RadComboBoxAño.SelectedValue);
                        (editItem.FindControl("Cal_fechainiRadDatePicker0") as RadDatePicker).Enabled = true;
                    }
                    else
                    {
                        (editItem.FindControl("Cal_fechainiRadDatePicker0") as RadDatePicker).SelectedDate = ultima_fecha.AddDays(1);
                        (editItem.FindControl("Cal_fechainiRadDatePicker0") as RadDatePicker).Enabled = false;
                    }
                    if ((editItem.FindControl("Cal_fechainiRadDatePicker0") as RadDatePicker).SelectedDate >= DateTime.Parse("01/" + mes_siguiente.ToString() + "/" + RadComboBoxAño.SelectedValue))
                    {
                        (editItem.FindControl("Cal_FechafinRadDatePicker0") as RadDatePicker).SelectedDate = (editItem.FindControl("Cal_fechainiRadDatePicker0") as RadDatePicker).SelectedDate;
                    }
                    else
                    {
                        (editItem.FindControl("Cal_FechafinRadDatePicker0") as RadDatePicker).SelectedDate = DateTime.Parse("01/" + mes_siguiente.ToString() + "/" + RadComboBoxAño.SelectedValue);
                    }
                    (editItem["Cal_Activo"].Controls[0] as CheckBox).Checked = true;
                    (editItem["Cal_Activo"].Controls[0] as CheckBox).Enabled = false;
                }
            }
            else
            {
                if (e.Item.IsDataBound)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    item["EditCommandColumn"].Controls[0].Visible = false;
                    rgGuardar.Columns.FindByUniqueName("Cal_Año").Visible = false;
                    rgGuardar.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
                }
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        //Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        rgCalendario.Visible = false;
                        rgGuardar.Visible = true;                    
                    }
                    else if (btn.CommandName == "undo")
                    {
                        rgCalendario.Visible = true;
                        rgGuardar.Visible = false;                    
                    }                   
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rgCalendario_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                CapaNegocios.CN_CatCalendario cn_catCalendario = new CapaNegocios.CN_CatCalendario();
                Calendario calendario = new Calendario();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                calendario.Id_Emp = session.Id_Emp;
                //calendario.Id_Cal = Convert.ToInt32((editedItem["Id_Cal"].FindControl("Id_CalLabel") as Label).Text);
                calendario.Cal_Año = Convert.ToInt32((editedItem["Cal_Año"].FindControl("Cal_AñoTextBox") as RadNumericTextBox).Text);
                calendario.Cal_Mes = Convert.ToInt32((editedItem["Cal_Mes"].FindControl("RadComboBox1") as RadComboBox).SelectedValue);
                calendario.Cal_FechaIni = Convert.ToDateTime((editedItem["Cal_fechaini"].FindControl("Cal_fechainiRadDatePicker") as RadDatePicker).SelectedDate);
                calendario.Cal_FechaFin = Convert.ToDateTime((editedItem["Cal_Fechafin"].FindControl("Cal_FechafinRadDatePicker") as RadDatePicker).SelectedDate);
                calendario.Cal_Actual = Convert.ToBoolean((editedItem["Cal_Actual"].Controls[0] as CheckBox).Checked);
                calendario.Cal_Activo = Convert.ToBoolean((editedItem["Cal_Activo"].Controls[0] as CheckBox).Checked);


                //cn_catCalendario.GuardarCalendario(ref calendario, session.Emp_Cnx, ref verificador, false);
                //rgCalendario.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>","Los datos se guardaron correctamente")));
                Alerta("Los datos se guardaron correctamente");

            }
            catch (Exception ex)
            {
                //rgCalendario.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>","No se pudieron guardar los datos. " + ex.Message)));
                Alerta("No se pudieron guardar los datos. " + msgerror(ex));
                e.Canceled = true;
            }
        }
        protected void rgGuardar_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                int gId_Emp = session.Id_Emp;
                int gCal_Año = Convert.ToInt32((editedItem["Cal_Año"].FindControl("Cal_AñoTextBox0") as RadNumericTextBox).Text);
                int gCal_Mes = Convert.ToInt32((editedItem["Cal_Mes"].FindControl("RadComboBox2") as RadComboBox).SelectedValue);
                DateTime gCal_FechaIni = Convert.ToDateTime((editedItem["Cal_fechaini"].FindControl("Cal_fechainiRadDatePicker0") as RadDatePicker).SelectedDate);
                DateTime gCal_FechaFin = Convert.ToDateTime((editedItem["Cal_Fechafin"].FindControl("Cal_FechafinRadDatePicker0") as RadDatePicker).SelectedDate);
                bool gCal_Actual = Convert.ToBoolean((editedItem["Cal_Actual"].Controls[0] as CheckBox).Checked);
                bool gCal_Activo = Convert.ToBoolean((editedItem["Cal_Activo"].Controls[0] as CheckBox).Checked);

                DataRow[] dr;
                dr = dt.Select("gCal_Año='" + gCal_Año + "' and gCal_Mes='" + gCal_Mes + "'");
                if (dr.Length == 0)
                {
                    if (gCal_FechaIni < gCal_FechaFin)
                    {
                        if ((gCal_FechaIni == ultima_fecha.AddDays(1)) || (ultima_fecha == DateTime.Parse("01/01/0001")))
                        {
                            TimeSpan ts = gCal_FechaFin - gCal_FechaIni;
                            if ((ts.Days) < 45)
                            {
                                dt.Rows.Add(new object[] { gId_Emp, gCal_Año, gCal_Mes, gCal_FechaIni, gCal_FechaFin, gCal_Actual, gCal_Activo });
                                ultima_fecha = gCal_FechaFin;
                                mes_siguiente++;
                            }
                            else
                            {
                                e.Canceled = true;
                                Alerta("El máximo para un periodo de calendario es de 45 días");
                            }
                        }
                        else
                        {
                            e.Canceled = true;
                            Alerta("El periodo actual debe empezar donde terminó el ultimo periodo");
                        }
                    }
                    else
                    {
                        e.Canceled = true;
                        Alerta("Fecha fin debe ser mayor a fecha inicio");
                    }
                }
                else
                {
                    e.Canceled = true;
                    Alerta("Ya existe el periodo del mes " + gCal_Mes + " y año " + gCal_Año);
                }


                //Alerta("Los datos se guardaron correctamente");

            }
            catch (Exception ex)
            {
                Alerta("No se pudieron guardar los datos. " + msgerror(ex));
                e.Canceled = true;
            }
        }
        protected void rgCalendario_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                CapaNegocios.CN_CatCalendario cn_catCalendario = new CapaNegocios.CN_CatCalendario();
                Calendario calendario = new Calendario();
                GridEditableItem editedItem = e.Item as GridEditableItem;

                calendario.Id_Emp = Convert.ToInt32((editedItem["Id_Emp"].FindControl("Id_EmpLabel") as Label).Text);
                calendario.Id_Cal = Convert.ToInt32((editedItem["Id_Cal"].FindControl("Id_CalLabel") as Label).Text);
                calendario.Cal_Año = Convert.ToInt32((editedItem["Cal_Año"].FindControl("Cal_AñoTextBox") as RadNumericTextBox).Text);
                calendario.Cal_Mes = Convert.ToInt32((editedItem["Cal_Mes"].FindControl("RadComboBox1") as RadComboBox).SelectedValue);
                calendario.Cal_FechaIni = Convert.ToDateTime((editedItem["Cal_fechaini"].FindControl("Cal_fechainiRadDatePicker") as RadDatePicker).SelectedDate);
                calendario.Cal_FechaFin = Convert.ToDateTime((editedItem["Cal_Fechafin"].FindControl("Cal_FechafinRadDatePicker") as RadDatePicker).SelectedDate);
                calendario.Cal_Actual = Convert.ToBoolean((editedItem["Cal_Actual"].Controls[0] as CheckBox).Checked);
                calendario.Cal_Activo = Convert.ToBoolean((editedItem["Cal_Activo"].Controls[0] as CheckBox).Checked);

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Alerta("Los datos se modificaron correctamente.");

            }
            catch (Exception ex)
            {
                //rgCalendario.Controls.Add(new LiteralControl("No se pudo actualizar el Catalago. " + ex.Message));
                Alerta("No se pudieron modificar los datos. " + msgerror(ex));
                e.Canceled = true;
            }
        }
        protected void rgCalendario_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                CapaNegocios.CN_CatCalendario cn_catCalendario = new CapaNegocios.CN_CatCalendario();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                int verificador = 0;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                int id_cal = Convert.ToInt32((editedItem["Id_Cal"].FindControl("Id_CalLabel") as Label).Text);
                if (_PermisoEliminar)
                {
                    cn_catCalendario.EliminarCalendario(id_cal, session.Emp_Cnx, ref verificador);
                    Alerta("Los datos se eliminaron correctamente");
                }
                else
                    Alerta("No tiene permiso para eliminar este registro");
            }
            catch (Exception ex)
            {
                Alerta("No se pudieron eliminar los datos. " + msgerror(ex));
                e.Canceled = true;
            }
        }
        protected void txtAnyo_TextChanged(object sender, EventArgs e)
        {
            rgCalendario.Rebind();
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                rgCalendario.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RadComboBoxAño_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                rgCalendario.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "delete":
                        Borrar();
                        break;

                    case "save":
                        this.Guardar();
                        break;

                    case "undo":
                        Undo();
                        break;

                    case "new":
                        Nuevo();
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        #endregion
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
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar)
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;

                    if (Permiso.PModificar)
                    {
                        //columna editar
                    }
                    //columna borrar
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = _PermisoEliminar;
                    //Regresar
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("undo")).Visible = true;
                    //
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = false;
                    //Imprimir
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = false;
                    //Correo
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("mail")).Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
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
                crearDT();
                int x = DateTime.Now.AddYears(-5).Year;
                RadComboBoxItem item = new RadComboBoxItem();
                for (int i = 0; i < 11; i++)
                {
                    item = new RadComboBoxItem();
                    item.Text = x.ToString();
                    item.Value = x.ToString();
                    x++;
                    RadComboBoxAño.Items.Add(item);
                }
                CargarCentros();
                Funciones funcion = new Funciones();
                RadComboBoxAño.SelectedValue = funcion.GetLocalDateTime(session.Minutos).Year.ToString();
                rgCalendario.Rebind();
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
        private string msgerror(Exception exception)
        {
            switch (exception.Message)
            {
                case "msg01":
                    { return "Ya existe periodo con este Año - Mes"; }

                case "msg02":
                    { return "Fecha final debe ser mayor a la fecha inicial"; }

                case "msg03":
                    { return "El periodo seleccionado se empalma con uno existente"; }
                default:
                    { return exception.Message; }
            }
        }
        private List<Calendario> GetList()
        {
            int año;
            int.TryParse(RadComboBoxAño.SelectedValue, out año);
            if (año == 0)
            {
                año = DateTime.Now.Year;
                RadComboBoxAño.SelectedValue = año.ToString();
            }
            try
            {
                List<Calendario> List = new List<Calendario>();
                CapaNegocios.CN_CatCalendario _catCalendario = new CapaNegocios.CN_CatCalendario();
                Calendario _calendario = new Calendario();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                _catCalendario.ConsultaCalendario(ref _calendario, año, session2, ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void crearDT()
        {
            dt = new DataTable();
            dt.Columns.Add("gId_Emp");
            dt.Columns.Add("gCal_Año");
            dt.Columns.Add("gCal_Mes");
            dt.Columns.Add("gCal_FechaIni", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("gCal_FechaFin", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("gCal_Actual");
            dt.Columns.Add("gCal_Activo");
        }
        private void Borrar()
        {
            try
            {
                if (!string.IsNullOrEmpty(RadComboBoxAño.SelectedValue))
                {
                    if (GetList().Count == 0)
                        Alerta("No hay calendarios para este año");
                    else
                    {
                        CN_CatCalendario cn_catCalendario = new CN_CatCalendario();
                        Sesion session = new Sesion();
                        int verificador = 0;
                        session = (Sesion)Session["Sesion" + Session.SessionID];
                        if (_PermisoEliminar)
                        {
                            cn_catCalendario.EliminarCalendarioAño(int.Parse(RadComboBoxAño.SelectedValue), session, ref verificador);
                            Alerta("Calendario " + RadComboBoxAño.SelectedValue + " borrado");
                        }
                        else
                            Alerta("No tiene permiso para eliminar este calendario");
                        rgCalendario.Rebind();
                        rgGuardar.Rebind();
                    }
                }
                else
                    Alerta("Seleccione un año");               
            }
            catch (Exception)
            {
                Alerta("No se pudo borrar el calendario");
            }
        }
        private void Guardar()
        {
            if (mes_siguiente > 12)
            {
                try
                {
                    CapaNegocios.CN_CatCalendario cn_catCalendario = new CapaNegocios.CN_CatCalendario();
                    Calendario calendario;
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    List<Calendario> calendarios = new List<Calendario>();
                    foreach (DataRow row in dt.Rows)
                    {
                        calendario = new Calendario();
                        calendario.Id_Emp = session.Id_Emp;
                        calendario.Cal_Año = int.Parse(row["gCal_Año"].ToString());
                        calendario.Cal_Mes = int.Parse(row["gCal_Mes"].ToString());
                        calendario.Cal_FechaIni = Convert.ToDateTime(row["gCal_FechaIni"].ToString());
                        calendario.Cal_FechaFin = Convert.ToDateTime(row["gCal_FechaFin"].ToString());
                        calendario.Cal_Actual = Convert.ToBoolean(row["gCal_Actual"].ToString());
                        calendario.Cal_Activo = Convert.ToBoolean(row["gCal_Activo"].ToString());
                        calendarios.Add(calendario);
                    }
                    int verificador = 0;
                    cn_catCalendario.GuardarCalendario(ref calendarios, session.Emp_Cnx, ref verificador, false);
                    Alerta("Calendario " + RadComboBoxAño.SelectedValue + " guardado");
                    rgGuardar.Visible = false;
                    rgCalendario.Visible = true;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Enabled = _PermisoEliminar;
                    RadComboBoxAño.Enabled = true;
                    rgCalendario.Rebind();
                    mes_siguiente = 1;
                    crearDT();
                    rgGuardar.Rebind();
                }
                catch (Exception ex)
                {
                    Alerta("No se pudieron guardar los datos: " + msgerror(ex));
                }
            }
            else
                Alerta("Favor de ingresar todos los periodos");
        }
        private void Undo()
        {
            rgCalendario.Visible = true;
            rgGuardar.Visible = false;
            RadComboBoxAño.Enabled = true;
            mes_siguiente = 1;
            ultima_fecha = new DateTime();
            crearDT();
        }
        private void Nuevo()
        {
            if (RadComboBoxAño.SelectedValue != "")
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Calendario calendario = new Calendario();
                CN_CatCalendario cn_calendario = new CN_CatCalendario();
                if (GetList().Count == 0)
                {
                    cn_calendario.ConsultaCalendarioUltimaFecha(ref calendario, int.Parse(RadComboBoxAño.SelectedValue), session);
                    ultima_fecha = calendario.Cal_FechaFin;
                    rgCalendario.Visible = false;
                    rgGuardar.Visible = true;
                    RadComboBoxAño.Enabled = false;
                   ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Enabled = false;
                    crearDT();
                    mes_siguiente = 1;
                    rgGuardar.Rebind();
                }
                else               
                    Alerta("Ya existe calendarios para este año");                           
            }
            else           
                Alerta("seleccione un año");           
        }
        #endregion
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
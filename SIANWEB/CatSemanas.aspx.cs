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
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class CatSemanas : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        static int Id_Calendario;
        static int Calendario_Año;
        static int Id_Semana = 1;
        static DateTime Fecha_Siguiente;
        //static DateTime Ultima_Fecha;

        DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)                
                    Response.Redirect("Login.aspx");               
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        CargarCentros();
                        crearDT();
                        RadGridGuardar.Rebind();
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
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
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
                this.rgSemana.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGridGuardar_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {           
            try
            {
                RadGridGuardar.DataSource = dt;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGridGuardar_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                int Id_Emp = session.Id_Emp; 
                int Id_Sem = Convert.ToInt32((editedItem["Id_SemG"].FindControl("RadNumericTextBox3G") as Label).Text);
                int Id_Cal = Id_Calendario;
                int Cal_Año = Convert.ToInt32((editedItem["Cal_AñoG"].FindControl("Cal_AñoTextBox") as RadNumericTextBox).Text);
                DateTime Sem_FechaIni = Convert.ToDateTime((editedItem["Sem_fechainiG"].FindControl("RadDatePicker3G") as RadDatePicker).SelectedDate);
                DateTime Sem_FechaFin = Convert.ToDateTime((editedItem["Sem_FechafinG"].FindControl("RadDatePicker4G") as RadDatePicker).SelectedDate);
                bool Sem_Activo = Convert.ToBoolean((editedItem["Sem_ActivoG"].Controls[0] as CheckBox).Checked);

                dt.Rows.Add(new object[] { Id_Emp, Id_Sem, Id_Cal, Cal_Año, Sem_FechaIni, Sem_FechaIni.ToString("dd/MM/yyyy"), Sem_FechaFin, Sem_FechaFin.ToString("dd/MM/yyyy"), Sem_Activo });
                Alerta("Los datos se modificaron correctamente.");
            }
            catch (Exception ex)
            {
                Alerta("No se pudieron modificar los datos. " + msgerror(ex));
                e.Canceled = true;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void ButtonBorrar_Click(object sender, EventArgs e)
        {

        }
        protected void RadNumericTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rgSemana.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbMes_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                rgSemana.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGridGuardar_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "InitInsert")               
                    if (Fecha_Siguiente >= RadDatePickerFin.SelectedDate)
                    {
                        Alerta("Ya se completó el periodo de calendario");
                        e.Canceled = true;
                    }               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGridGuardar_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;
                    Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        RadGridGuardar.Columns.FindByUniqueName("EditCommandColumn").Visible = true;
                        (editItem.FindControl("RadNumericTextBox3G") as RadNumericTextBox).Text = Id_Semana.ToString();
                        (editItem.FindControl("RadNumericTextBox3G") as RadNumericTextBox).Enabled = false;
                        if (Id_Semana == 1)
                        {
                            (editItem.FindControl("RadDatePicker3G") as RadDatePicker).SelectedDate = Fecha_Siguiente;
                            (editItem.FindControl("RadDatePicker4G") as RadDatePicker).SelectedDate = Fecha_Siguiente;
                            (editItem.FindControl("RadDatePicker3G") as RadDatePicker).Enabled = false;
                        }
                        else
                        {
                            (editItem.FindControl("RadDatePicker3G") as RadDatePicker).SelectedDate = Fecha_Siguiente.AddDays(1);
                            (editItem.FindControl("RadDatePicker4G") as RadDatePicker).SelectedDate = Fecha_Siguiente.AddDays(1);
                            (editItem.FindControl("RadDatePicker3G") as RadDatePicker).Enabled = false;
                        }

                        (editItem["Sem_ActivoG"].Controls[0] as CheckBox).Checked = true;
                        (editItem["Sem_ActivoG"].Controls[0] as CheckBox).Enabled = false;
                    }
                }
                else                
                    if (e.Item.IsDataBound)
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        item["EditCommandColumn"].Controls[0].Visible = false;
                        RadGridGuardar.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
                    }               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGridGuardar_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                int Id_Emp = session.Id_Emp;
                int Id_Sem = Convert.ToInt32((editedItem["Id_SemG"].FindControl("RadNumericTextBox3G") as RadNumericTextBox).Text);
                int Id_Cal = Id_Calendario;
                int Cal_Año = Calendario_Año;
                DateTime Sem_FechaIni = Convert.ToDateTime((editedItem["Sem_FechaIniG"].FindControl("RadDatePicker3G") as RadDatePicker).SelectedDate);
                DateTime Sem_FechaFin = Convert.ToDateTime((editedItem["Sem_FechaFinG"].FindControl("RadDatePicker4G") as RadDatePicker).SelectedDate);
                bool Sem_Activo = Convert.ToBoolean((editedItem["Sem_ActivoG"].Controls[0] as CheckBox).Checked);
                if (Sem_FechaIni < Sem_FechaFin)
                {
                    if (Sem_FechaFin > RadDatePickerFin.SelectedDate)
                    {
                        Alerta("La última fecha del periodo debe ser " + RadDatePickerFin.SelectedDate); // cambiado a peticion de norma Alerta("La ultima fecha del periodo es " + RadDatePickerFin.SelectedDate);
                        e.Canceled = true;
                    }
                    else
                    {
                        Id_Semana++;
                        Fecha_Siguiente = Sem_FechaFin;
                        dt.Rows.Add(new object[] { Id_Emp, Id_Sem, Id_Cal, Cal_Año, Sem_FechaIni, Sem_FechaIni.ToString("dd/MM/yyyy"), Sem_FechaFin, Sem_FechaFin.ToString("dd/MM/yyyy"), Sem_Activo });//Id_Cal, Cal_Año, 
                    }
                }
                else
                {
                    e.Canceled = true;
                    Alerta("La fecha inicial no debe ser mayor a la fecha final");
                }
            }
            catch (Exception ex)
            {
                Alerta("No se pudieron guardar los datos. " + msgerror(ex));
                e.Canceled = true;
            }
        }
        protected void RadComboBoxAño_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                rgSemana.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgServicio_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgSemana.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                    case "new":
                        rgSemana.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                rgSemana.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private List<Semana> GetList()
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
                List<Calendario> calendarios = new List<Calendario>();
                CN_CatCalendario cn_calendario = new CN_CatCalendario();
                Calendario calendario = new Calendario();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                cn_calendario.VerificaCalendario(ref calendario, año, cmbMes.SelectedIndex, session, ref calendarios);
                if (calendarios.Count >= 1)
                {
                    Id_Calendario = 0;
                    foreach (Calendario calen in calendarios)
                    {
                        RadDatePickerInicio.SelectedDate = calen.Cal_FechaIni;
                        Fecha_Siguiente = calen.Cal_FechaIni;
                        RadDatePickerFin.SelectedDate = calen.Cal_FechaFin;
                        Id_Calendario = calen.Id_Cal;
                        Calendario_Año = año;
                    }
                    List<Semana> List = new List<Semana>();
                    CN_CatSemana cn_catSemana = new CN_CatSemana();
                    Semana semana = new Semana();

                    cn_catSemana.ConsultaSemana(ref semana, año, cmbMes.SelectedIndex, session, ref List);

                    if (List.Count > 0)
                    {
                        RadGridGuardar.Visible = false;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = false;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = false;
                        rgSemana.Visible = true;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = _PermisoEliminar;
                    }
                    else
                    {
                        crearDT();
                        RadGridGuardar.Visible = true;
                        RadGridGuardar.Rebind();
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = _PermisoGuardar;
                        rgSemana.Visible = false;
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = false;
                    }
                    return List;
                }
                else
                {
                    RadDatePickerInicio.SelectedDate = null;
                    Fecha_Siguiente = new DateTime();
                    RadDatePickerFin.SelectedDate = null;
                    Id_Calendario = new int();
                    Calendario_Año = new int();
                    if ((cmbMes.SelectedIndex != 0) && (RadComboBoxAño.SelectedValue != ""))
                    {
                        Alerta("No se han dado de alta los calendarios " + RadComboBoxAño.SelectedValue);
                    }
                    List<Semana> List = new List<Semana>();
                    RadGridGuardar.Visible = false;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = false;
                    rgSemana.Visible = true;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = false;
                    return List;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

                    if (Permiso.PGrabar)
                    {
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                    }

                    if (Permiso.PModificar)
                    {
                        //columna editar

                    }

                    if (Permiso.PEliminar)
                    {
                        //columna borrar
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = _PermisoEliminar;
                    }

                    //Regresar
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("undo")).Visible = false;
                    //Eliminar
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = false;
                    //Imprimir
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = false;
                    //Correo
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("mail")).Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
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
        private void crearDT()
        {
            Id_Semana = 1;
            dt = new DataTable();
            dt.Columns.Add("Id_Emp");
            dt.Columns.Add("Id_Sem");
            dt.Columns.Add("Id_Cal");
            dt.Columns.Add("Cal_Año");
            dt.Columns.Add("Sem_FechaIni");
            dt.Columns.Add("Sem_FechaIniStr");
            dt.Columns.Add("Sem_FechaFin");
            dt.Columns.Add("Sem_FechaFinStr");
            dt.Columns.Add("Sem_Activo");
            //fecha sig ultima fecha...
        }
        private void Guardar()
        {
            if (Fecha_Siguiente == RadDatePickerFin.SelectedDate)
            {
                try
                {
                    CN_CatSemana cn_catSemana = new CN_CatSemana();
                    Semana semana;
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    List<Semana> semanas = new List<Semana>();
                    foreach (DataRow row in dt.Rows)
                    {
                        semana = new Semana();
                        semana.Id_Emp = session.Id_Emp;
                        semana.Id_Sem = int.Parse(row["Id_Sem"].ToString());
                        semana.Id_Cal = int.Parse(row["Id_Cal"].ToString());
                        semana.Cal_Año = int.Parse(row["Cal_Año"].ToString());
                        semana.Sem_FechaIni = Convert.ToDateTime(row["Sem_FechaIni"].ToString());
                        semana.Sem_FechaFin = Convert.ToDateTime(row["Sem_FechaFin"].ToString());
                        semana.Sem_Activo = Convert.ToBoolean(row["Sem_Activo"].ToString());
                        semanas.Add(semana);
                    }
                    int verificador = 0;
                    cn_catSemana.GuardarSemana(ref semanas, session.Emp_Cnx, ref verificador, false);
                    Alerta("Los datos se guardaron correctamente");
                    RadGridGuardar.Visible = false;
                    rgSemana.Visible = true;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Enabled = _PermisoEliminar;
                    rgSemana.Rebind();
                    crearDT();
                    RadGridGuardar.Rebind();
                }
                catch (Exception ex)
                {
                    Alerta("No se pudieron guardar los datos: " + msgerror(ex));
                }
            }
            else
            {
                Alerta("Hay días sin asignar");
            }
        }
        private void Borrar()
        {
            try
            {
                if ((cmbMes.SelectedIndex != 0) && (RadComboBoxAño.SelectedValue != "") && (Id_Calendario != 0))
                {
                    if (GetList().Count == 0)
                        Alerta("No hay semanas para borrar");
                    else
                    {
                        CN_CatSemana cn_catSemana = new CN_CatSemana();
                        Sesion session = new Sesion();
                        int verificador = 0;
                        session = (Sesion)Session["Sesion" + Session.SessionID];
                        cn_catSemana.EliminarSemana(Id_Calendario, session.Emp_Cnx, ref verificador);
                        Alerta("Periodo " + cmbMes.SelectedItem.Text + " " + RadComboBoxAño.SelectedValue + " borrado");
                        RadGridGuardar.Rebind();
                        rgSemana.Rebind();
                    }
                }
                else
                    Alerta("No existe periodo a borrar");
            }
            catch (Exception ex)
            {
                Alerta("No se pudo borrar el periodo: " + msgerror(ex));
            }
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
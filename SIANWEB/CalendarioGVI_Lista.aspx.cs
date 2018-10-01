using System;
using System.Globalization;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaDatos;
using CapaNegocios;
using CapaEntidad;
using System.IO;
using System.Runtime.Remoting;
using System.Text;

namespace SIANWEB
{
    public partial class CalendarioGVI_Lista : Page
    {   /// ver porq no identifdica los controles de telerik
        
        #region Variables

        private const string ProviderSessionKey = "SchedulerContextMenuCSharp";

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }

        public string strFiltroCliente;
        public string strFiltroRSC;

        #endregion

        #region Appoint

        private object EditedAppointmentID
        {
            get { return ViewState["EditedAppointmentID"]; }
            set { ViewState["EditedAppointmentID"] = value; }
        }

        Appointment EditedAppointment
        {
            get
            {
                return (EditedAppointmentID != null) ? RadScheduler1.Appointments.FindByID(EditedAppointmentID) : null;
            }
            set
            {
                EditedAppointmentID = value.ID;
                //  EditedAppointmentParentID = value.RecurrenceParentID;
            }
        }

        private void PopulateEditForm(Appointment editedAppointment)
        {
            Appointment appointmentToEdit = RadScheduler1.PrepareToEdit(editedAppointment, RadScheduler1.EditingRecurringSeries);
            if (appointmentToEdit.Subject != "")
            {
                RadDock1.Title = appointmentToEdit.Subject;
                DescriptionText.Text = appointmentToEdit.Description;
                StartTimeText.Text = appointmentToEdit.Start.ToShortDateString();
                TipoVisitaText.Text = appointmentToEdit.Resources[0].Text;

                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];

                CN_Citas.ObtieneDatosCriterioCita(Convert.ToInt32(appointmentToEdit.ID), ref RSCNameText, gSession.Emp_Cnx);

                CN_CatRequisitoCitas clsRequi = new CN_CatRequisitoCitas();
                List<RequisitoCita> ListadoReq = new List<RequisitoCita>();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                chklstPreRequisitos.Items.Clear();
                lblPreRequi.Visible = false;
                RadDock1.Width = 500;
                RadDock1.Height = 250;

                CN_Citas.ListadoPrerequisitosCita_Todos(gSession.Emp_Cnx, "spRequisitosUnaCita_Consulta", appointmentToEdit.ID.ToString(), ref this.chklstPreRequisitos);

                foreach (ListItem iteem in chklstPreRequisitos.Items)
                {
                    RadDock1.Width = 580;
                    RadDock1.Height = 350;
                    lblPreRequi.Visible = true;
                    iteem.Selected = true;
                    iteem.Enabled = false;
                }


                string Visita = appointmentToEdit.Resources[0].Key.ToString();
                if (Visita != null)
                {
                    if (Visita == "3")
                    {
                        //  this.lblDocto2.InnerText = "Ver Material de Apoyo";
                        //  this.txtAccion.Text = "acordeon para la llamada telefonica";
                        this.URLHelp.InnerText = "Ver Material de Apoyo";
                        this.URLHelp.HRef = "Apoyo/GuiaTelefonica.html";
                        this.URLHelp.Visible = true;
                        this.URLCorreo.Visible = false;
                    }

                    if (Visita == "2")
                    {

                        this.URLCorreo.InnerText = "Enviar Correo Institucional";
                        this.URLCorreo.HRef = "Apoyo/EnviaCorreoInstitucional.aspx?Cita=" + appointmentToEdit.ID.ToString();
                        this.URLCorreo.Visible = true;
                        this.URLHelp.Visible = false;
                        //  this.lblDocto2.InnerText = "Enviar Correo Institucional";
                        //  this.txtAccion.Text = "acciones para el correo electronico";
                    }

                    if (Visita == "1")
                    {
                        this.URLCorreo.Visible = false;
                        this.URLHelp.Visible = false;
                    }
                }
            }

            DescriptionText.Enabled = false;
            StartTimeText.Enabled = false;
            RSCNameText.Enabled = false;
            TipoVisitaText.Enabled = false;

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


            if (sesion == null)
            {
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                //Session["dir" + Session.SessionID] = pag[pag.Length - 1]; 
                //Response.Redirect("login.aspx", false);
                //      ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);
                //  this.lblMensaje.Text = "Su sesion ha caducado.";
                string script = "window.close();";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", script, true);

            }
            else
            {
                if (!Page.IsPostBack)
                {
                    ValidarPermisos();
                    if (sesion.Cu_Modif_Pass_Voluntario == false)
                    {
                        RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        return;
                    }
                    CargarCentros();
                    CargarCombos();
                    ConfiguraCalendario();

                    Random randObj = new Random(DateTime.Now.Millisecond);
                    HF_ClvPag.Value = randObj.Next().ToString();
                    this.HF_Usuario.Value = sesion.Id_U.ToString();
                }

                /*
                this.SqlDataSource1.SelectCommand = "SELECT * FROM [Appointments] WHERE [Activo] = 1" ;
                this.SqlDataSource1.InsertCommand = "INSERT INTO [Appointments] ([Subject], [Description], [Start], [End], [RecurrenceRule], [RecurrenceParentID], [Reminder], [Activo], [Usuario]) VALUES (@Subject, @Description, @Start, @End , @RecurrenceRule, @RecurrenceParentID, @Reminder, 1, " + sesion.Id_U.ToString() + " )";
                this.SqlDataSource1.UpdateCommand = "UPDATE [Appointments] SET [Start] = @Start, [End] = @End, [Usuario] = " + sesion.Id_U.ToString() + " WHERE (ID = @ID)";
                this.SqlDataSource1.DeleteCommand = "Exec BorraApooint @ID, " + sesion.Id_U.ToString();

                this.RequiDataSource.ConnectionString = sesion.Emp_Cnx;
                this.RequiDataSource.SelectCommand = "spCatPreRequisitos_Todos2";

                this.SqlDataSource1.ConnectionString = sesion.Emp_Cnx;
                */

                if (this.txtNumeroCliente.Text == "")
                {
                    strFiltroCliente = "null";
                }
                else
                {
                    strFiltroCliente = this.txtNumeroCliente.Text;
                }
                if (this.cmbRSC.SelectedValue == "0")
                {
                    strFiltroRSC = "null";
                }
                else
                {
                    strFiltroRSC = this.cmbRSC.SelectedValue.ToString();
                }

                this.SqlDataSource1.ConnectionString = sesion.Emp_Cnx;
                this.RSCDataSource.ConnectionString = sesion.Emp_Cnx;
                this.RSCDataSource.SelectCommand = "spRSC_Todos " + sesion.Id_Emp.ToString() + ", " + sesion.Id_Cd.ToString();

                this.VisitaDataSource.ConnectionString = sesion.Emp_Cnx;
                this.VisitaDataSource.SelectCommand = "spCatTipoVisita_Todos";

                this.RequiDataSource.ConnectionString = sesion.Emp_Cnx;
                this.RequiDataSource.SelectCommand = "spCatPreRequisitos_Todos2";

                this.SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.Text;
                this.SqlDataSource1.SelectCommand = "spCitaVisita_Todas " + sesion.Id_Emp.ToString() + "," + sesion.Id_Cd.ToString() + "," + sesion.Id_U.ToString() + "," + strFiltroCliente + ", " + strFiltroRSC;


                this.SqlDataSource1.UpdateCommandType = SqlDataSourceCommandType.Text;
                this.SqlDataSource1.UpdateCommand = "spCitaVisita_ActualizaFecha  @ID, " + sesion.Id_U.ToString() + ", @Start, @End";

                this.SqlDataSource1.DeleteCommandType = SqlDataSourceCommandType.Text;
                this.SqlDataSource1.DeleteCommand = "spCitaVisita_Eliminar  " + sesion.Id_U.ToString() + ", @ID";
            }

        }

        private void ConfiguraCalendario()
        {

            RadScheduler1.WeekView.UserSelectable = true;
            RadScheduler1.DayView.UserSelectable = true;
            RadScheduler1.MultiDayView.UserSelectable = false;
            RadScheduler1.TimelineView.UserSelectable = false;
            RadScheduler1.MonthView.UserSelectable = true;

            RadScheduler1.Skin = "Web20";
            RadScheduler1.ShowAllDayRow = false;
            RadScheduler1.MultiDayView.ShowDateHeaders = true;
            RadScheduler1.SelectedDate = DateTime.Today;

            ConfiguraIdiomaScheduler(RadScheduler1);
        }

        private void ConfiguraIdiomaScheduler(RadScheduler S)
        {

            S.Localization.AdvancedNewAppointment = "Nueva Cita";
            S.Localization.AdvancedSubject = "Cita";
            S.Localization.HeaderDay = "Diario";
            S.Localization.HeaderMonth = "Mensual";
            S.Localization.HeaderTimeline = "Agenda";
            S.Localization.HeaderToday = "hoy";
            S.Localization.HeaderWeek = "Semanal";
            S.Localization.Save = "Guardar";
            S.Localization.Show24Hours = "24 horas..";
            S.Localization.AdvancedAllDayEvent = "Todo el dia";
            S.Localization.AdvancedCalendarCancel = "Cancelar";
            S.Localization.AdvancedCalendarToday = "hoy";
            S.Localization.AdvancedClose = "Cerrar";
            S.Localization.AdvancedDaily = "Diario";
            S.Localization.AdvancedDay = "Dia";
            S.Localization.AdvancedDays = "Dias";
            S.Localization.AdvancedDescription = "Descripción";
            S.Localization.AdvancedDone = "Listo";
            S.Localization.AdvancedEditAppointment = "Editar Cita";
            S.Localization.AdvancedEndAfter = "Finalizar despues de";
            S.Localization.AdvancedEndByThisDate = "Terminar el";
            S.Localization.AdvancedEndDateRequired = "Fecha de Terminación es Requerida";
            S.Localization.AdvancedEndTimeRequired = "Hora de Terminación es Requerida";
            S.Localization.AdvancedEvery = "Cada";
            S.Localization.AdvancedEveryWeekday = "Cada Dia de la Semana";
            S.Localization.AdvancedFirst = "primero";
            S.Localization.AdvancedFourth = "cuarta";
            S.Localization.AdvancedFrom = "Hora de Inicio";
            S.Localization.AdvancedHourly = "Cada Hora";
            S.Localization.AdvancedHours = "horas";
            S.Localization.AdvancedInvalidNumber = "Número Inválido";
            S.Localization.AdvancedLast = "último";
            S.Localization.AdvancedMaskDay = "dia";
            S.Localization.AdvancedMaskWeekday = "día de la semana";
            S.Localization.AdvancedMaskWeekendDay = "día del fin de semana";
            S.Localization.AdvancedMonthly = "Mensual";
            S.Localization.AdvancedMonths = "meses";
            S.Localization.AdvancedNoEndDate = "Sin Fecha de Terminacion";
            S.Localization.AdvancedOccurrences = "ocurrencias";
            S.Localization.AdvancedOf = "de";
            S.Localization.AdvancedOfEvery = "de cada";
            S.Localization.AdvancedRecurEvery = "Recurrente cada";
            S.Localization.AdvancedRecurrence = "Recurrente";
            S.Localization.AdvancedReset = "Reiniciar Excepciones";
            S.Localization.AdvancedSecond = "segundos";
            S.Localization.AdvancedStartDateRequired = "Fecha inicial es requerida";
            S.Localization.AdvancedStartTimeBeforeEndTime = "La hora inicial debe ser previa a la fecha final";
            S.Localization.AdvancedStartTimeRequired = "Hora inicial es requerida";
            S.Localization.AdvancedSubjectRequired = "Favor de capturar el motivo de la cita";
            S.Localization.AdvancedThe = "El";
            S.Localization.AdvancedThird = "tercer";
            S.Localization.AdvancedTo = "Termina";
            S.Localization.AdvancedWeekly = "Semanal";
            S.Localization.AdvancedWeeks = "semanas en";
            S.Localization.AdvancedWorking = "Procesando...";
            S.Localization.AdvancedYearly = "Anual";
            S.Localization.AllDay = "todo el día";
            S.Localization.Cancel = "Cancelar";
            S.Localization.ConfirmCancel = "Cancelar";
            S.Localization.ConfirmDeleteTitle = "Confirma eliminación";
            S.Localization.ConfirmRecurrenceDeleteOccurrence = "Eliminar solo esta cita.";
            S.Localization.ConfirmRecurrenceDeleteSeries = "Eliminar la serie de citas.";
            S.Localization.ConfirmRecurrenceDeleteTitle = "Eliminar la cita recurrente.";
            S.Localization.ConfirmRecurrenceEditOccurrence = "Editar solo esta cita.";
            S.Localization.ConfirmRecurrenceEditSeries = "Editar la serie de citas.";
            S.Localization.ConfirmRecurrenceEditTitle = "Editar la cita recurrente.";
            S.Localization.ConfirmRecurrenceMoveOccurrence = "Mover sola esta cita.";
            S.Localization.ConfirmRecurrenceMoveSeries = "Mover la serie de citas.";
            S.Localization.ConfirmRecurrenceMoveTitle = "Mover la cita recurrente";
            S.Localization.ContextMenuAddAppointment = "Nueva cita";
            S.Localization.ContextMenuAddRecurringAppointment = "Nueva cita recurrente";
            S.Localization.ContextMenuDelete = "Eliminar";
            S.Localization.ContextMenuEdit = "Editar";
            S.Localization.ContextMenuGoToToday = "hoy";
            S.Localization.HeaderMultiDay = "Multiples dias";
            S.Localization.HeaderNextDay = "siguiente día";
            S.Localization.HeaderPrevDay = "día anterior";
            S.Localization.Reminder = "Recordatorio";
            S.Localization.ReminderBeforeStart = "antes de iniciar";
            S.Localization.ReminderDay = "día";
            S.Localization.ReminderDays = "días";
            S.Localization.ReminderDismiss = "Rechazar";
            S.Localization.ReminderDismissAll = "Rechazar todas";
            S.Localization.ReminderHour = "hora";
            S.Localization.ReminderHours = "horas";
            S.Localization.ReminderMinute = "minuto";
            S.Localization.ReminderMinutes = "minutos";
            S.Localization.ReminderNone = "Ninguno";
            S.Localization.ReminderOpenItem = "Abrir Item";
            S.Localization.Reminders = "Recordatorio";
            S.Localization.ReminderWeek = "semana";
            S.Localization.ReminderWeeks = "semanas";
            S.Localization.ShowAdvancedForm = "Opciones";
            S.Localization.ShowBusinessHours = "Mostrar horas de oficina...";
            S.Localization.ShowMore = "mas...";
        }

        #region ProcesosScheduler

        protected void RadScheduler1_FormCreating(object sender, SchedulerFormCreatingEventArgs e)
        {
            if (e.Mode != SchedulerFormMode.Hidden)
            {
                EditedAppointment = e.Appointment;
                e.Cancel = true;
            }
            // si no tiene cita, no debe levantar la pantallita

            var appointmentToEdit = RadScheduler1.PrepareToEdit(e.Appointment, RadScheduler1.EditingRecurringSeries);
            if (appointmentToEdit.ID != null)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "formScript", "Sys.Application.add_load(openForm);", true);
                PopulateEditForm(appointmentToEdit);
            }
        }

        protected void RadScheduler1_AppointmentDelete(object sender, SchedulerCancelEventArgs e)
        {
            hf_CitaVisitaModif.Value = e.Appointment.ID.ToString();
            this.lblCita2.Text = e.Appointment.ID.ToString();
            this.lblMotivo2.Text = "Motivo para eliminar la cita (" + e.Appointment.Subject + "):";
            SolicitaMotivo(2,".");
        }

        protected void RadScheduler1_AppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {
            hf_CitaVisitaModif.Value = e.ModifiedAppointment.ID.ToString();
            this.lblCita1.Text = e.ModifiedAppointment.ID.ToString();
            this.lblMotivo1.Text = "Motivo para reprogramar la cita (" + e.ModifiedAppointment.Subject + "):";
            SolicitaMotivo(1, e.ModifiedAppointment.Start.ToShortDateString());
            int DiaFes = 0;
            this.ValidaDiaFestivo(e.ModifiedAppointment.Start.ToShortDateString(), ref DiaFes);
            if (1 == DiaFes)
            {
                this.RAM1.Alert("La nueva fecha seleccionada es un dia inhabil.");
            }

        }

        #endregion


        #region ProcesosVarios

        public void txtNumCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];

                Clientes cliente = new Clientes();
                cliente.Id_Emp = gSession.Id_Emp;
                cliente.Id_Cd = gSession.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value.Value : -1);
                new CN_CatCliente().ConsultaClientes(ref cliente, gSession.Emp_Cnx);
                txtNombreCliente.Text = cliente.Cte_NomComercial;

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);

                txtNumeroCliente.Text = string.Empty;
                txtNombreCliente.Text = string.Empty;
            }
        }

        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
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
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void imgFiltros_Click(object sender, EventArgs e)
        {
            if (this.txtNumeroCliente.Text == "")
            {
                strFiltroCliente = null;
            }
            else
            {
                strFiltroCliente = this.txtNumeroCliente.Text;
            }
            if (this.cmbRSC.SelectedValue == "0")
            {
                strFiltroRSC = null;
            }
            else
            {
                strFiltroRSC = this.cmbRSC.SelectedValue.ToString();
            }
            RadScheduler1.Rebind();
        }

        protected void btnEliminaOK_OnClick(object sender, EventArgs e)
        {
            // mandar ejecutar el SP spCatMotivoCambioVisita_AgregarALog
            try
            {
                hf_CitaVisitaModif.Value = this.lblCita2.Text;
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                CN_Citas.GrabaMotivoModificacion(Convert.ToInt32(hf_CitaVisitaModif.Value), this.txtMotivoElimina.Text, gSession.Emp_Cnx);
                this.lblCita2.Text = "";
                hf_CitaVisitaModif.Value = "";
                RadScheduler1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnReprogramaOK_OnClick(object sender, EventArgs e)
        {
            // mandar ejecutar el SP spCatMotivoCambioVisita_AgregarALog
            try
            {
                hf_CitaVisitaModif.Value = this.lblCita1.Text;
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                CN_Citas.GrabaMotivoModificacion(Convert.ToInt32(hf_CitaVisitaModif.Value), this.txtMotivoRepro.Text, gSession.Emp_Cnx);
                this.lblCita1.Text = "";
                hf_CitaVisitaModif.Value = "";
                RadScheduler1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCerrar_OnClick(object sender, EventArgs e)
        {
            try
            {
                hf_CitaVisitaModif.Value = this.lblCita1.Text;
                if (hf_CitaVisitaModif.Value == string.Empty)
                {
                    hf_CitaVisitaModif.Value = this.lblCita1.Text;
                }

                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                CN_Citas.CancelaModificacion(Convert.ToInt32(hf_CitaVisitaModif.Value), gSession.Emp_Cnx);
                this.lblCita1.Text = "";
                RadScheduler1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelarCambio_OnClick(object sender, EventArgs e)
        {
            // mandar ejecutar el SP spCatMotivoCambioVisita_AgregarALog
            try
            {
                hf_CitaVisitaModif.Value = this.lblCita1.Text;
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                CN_Citas.CancelaModificacion(Convert.ToInt32(hf_CitaVisitaModif.Value), gSession.Emp_Cnx);
                hf_CitaVisitaModif.Value = "";
                RadScheduler1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelarCambio2_OnClick(object sender, EventArgs e)
        {
            // mandar ejecutar el SP spCatMotivoCambioVisita_AgregarALog
            //  limpiar variables para q no se queden con los valores anteriores
            try
            {
                hf_CitaVisitaModif.Value = this.lblCita2.Text;
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                CN_Citas.CancelaModificacion(Convert.ToInt32(hf_CitaVisitaModif.Value), gSession.Emp_Cnx);
                hf_CitaVisitaModif.Value = "";

                RadScheduler1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Funciones

        private void ValidaDiaFestivo(string Fecha, ref int resul)
        {

            CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
            Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];

            CN_Citas.EsDiaFestivo(gSession.Emp_Cnx, Fecha, ref resul);  //spEsDiaFestivo 
            
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

        private void CargarCombos()
        {
            try
            {
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun CN_Comun = new CN__Comun();
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();

                CN_Comun.LlenaCombo(gSession.Id_Emp, gSession.Id_Cd, gSession.Emp_Cnx, "spRSC_Todos", ref this.cmbRSC, false);
                this.cmbRSC.Items.Add(new RadComboBoxItem("-- Seleccione --", "0"));
                this.cmbRSC.SelectedValue = "0";

                CN_Comun.LlenaCombo(gSession.Id_Emp, gSession.Id_Cd, gSession.Emp_Cnx, "spTerritorioSvc_Todos", ref this.cmbTerritorioSvc, false);
                this.cmbTerritorioSvc.Items.Add(new RadComboBoxItem("-- Seleccione --", "0"));
                this.cmbTerritorioSvc.SelectedValue = "0";


                //  CN_Comun.LlenaCombo(gSession.Emp_Cnx, "spCatMotivoCambioVisita_ReAgendarTodos", ref this.cmbMotivoRepro);
                //  this.cmbMotivoRepro.Items.Add(new RadComboBoxItem("-- Seleccione --", "0"));
                //  this.cmbMotivoRepro.SelectedValue = "0";

                //  CN_Comun.LlenaCombo(gSession.Emp_Cnx, "spCatMotivoCambioVisita_EliminarTodos", ref this.cmbMotivoElimina);
                //  this.cmbMotivoElimina.Items.Add(new RadComboBoxItem("-- Seleccione --", "0"));
                //  this.cmbMotivoRepro.SelectedValue = "0";
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SolicitaMotivo(int tipo, string fech)
        {
            try
            {
                
                    string script = "";
                    if (tipo == 1)
                    {
                        int DiaFes = 0;
                        this.ValidaDiaFestivo(fech, ref DiaFes);
                        if (0 == DiaFes)
                        { 
                            script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        }
                        //else
                        //{
                        //    //hf_CitaVisitaModif.Value = this.lblCita1.Text;
                        //    //Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                        //    //CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                        //    //CN_Citas.CancelaModificacion(Convert.ToInt32(this.lblCita1.Text), gSession.Emp_Cnx);
                        //    //hf_CitaVisitaModif.Value = "";
                        //    //RadScheduler1.Rebind();
                        //    Alerta("La nueva fecha seleccionada es un dia inhabil.");
                        //}
                    }
                    if (tipo == 2)
                    {
                        script = "function f(){$find(\"" + RadWindow2.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    }
                    txtMotivoRepro.Text = "";
                    txtMotivoElimina.Text = "";
                    //lblCita1.Text = "";
                    //lblCita2.Text = "";

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                
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


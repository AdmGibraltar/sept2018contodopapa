<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="SIANWEB.WebForm3" Culture="es-MX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <%-- 

      
    DeleteCommand="DELETE FROM [Appointments] WHERE [ID] = @ID" UPDATE [Appointments] SET [Activo] = 0 WHERE [ID] = @ID 

    <telerik:RadScheduler ID="RadScheduler1" runat="server" Culture="es-MX" 
        EnableCustomAttributeEditing="True">
        <AdvancedForm EnableCustomAttributeEditing="True" />
        <Localization AdvancedNewAppointment="Nueva Cita" AdvancedSubject="Cita" 
            HeaderDay="Diario" HeaderMonth="Mensual" HeaderTimeline="Agenda" 
            HeaderToday="hoy" HeaderWeek="Semanal" Save="Guardar" 
            Show24Hours="24 horas..." AdvancedAllDayEvent="Todo el dia" 
            AdvancedCalendarCancel="Cancelar" AdvancedCalendarToday="hoy" 
            AdvancedClose="Cerrar" AdvancedDaily="Diario" AdvancedDay="Dia" 
            AdvancedDays="Dias" AdvancedDescription="Descripción" AdvancedDone="Listo" 
            AdvancedEditAppointment="Editar Cita" AdvancedEndAfter="Finalizar despues" 
            AdvancedEndByThisDate="Terminar el" 
            AdvancedEndDateRequired="Fecha de Terminación es Requerida" 
            AdvancedEndTimeRequired="Hora de Terminación es Requerida" AdvancedEvery="Cada" 
            AdvancedEveryWeekday="Cada Dia de la Semana" AdvancedFirst="primero" 
            AdvancedFourth="cuarta" AdvancedFrom="Hora de Inicio" 
            AdvancedHourly="Cada Hora" AdvancedHours="horas" 
            AdvancedInvalidNumber="Número Inválido" AdvancedLast="último" 
            AdvancedMaskDay="dia" AdvancedMaskWeekday="día de la semana" 
            AdvancedMaskWeekendDay="día del fin de semana" AdvancedMonthly="Mensual" 
            AdvancedMonths="meses" AdvancedNoEndDate="Sin Fecha de Terminacion" 
            AdvancedOccurrences="ocurrencias" AdvancedOf="de" AdvancedOfEvery="de cada" 
            AdvancedRecurEvery="Recurrente cada" AdvancedRecurrence="Recurrente" 
            AdvancedReset="Reiniciar Excepciones" AdvancedSecond="segundos" 
            AdvancedStartDateRequired="Fecha inicial es requerida" 
            AdvancedStartTimeBeforeEndTime="La hora inicial debe ser previa a la fecha final" 
            AdvancedStartTimeRequired="Hora inicial es requerida" 
            AdvancedSubjectRequired="Favor de capturar el motivo de la cita" 
            AdvancedThe="El" AdvancedThird="tercer" 
            AdvancedTo="Termina" AdvancedWeekly="Semanal" AdvancedWeeks="semanas en" 
            AdvancedWorking="Procesando..." AdvancedYearly="Anual" AllDay="todo el día" 
            Cancel="Cancelar" ConfirmCancel="Cancelar" 
            ConfirmDeleteTitle="Confirma eliminación" 
            ConfirmRecurrenceDeleteOccurrence="Eliminar solo esta cita." 
            ConfirmRecurrenceDeleteSeries="Eliminar la serie de citas." 
            ConfirmRecurrenceDeleteTitle="Eliminar la cita recurrente." 
            ConfirmRecurrenceEditOccurrence="Editar solo esta cita." 
            ConfirmRecurrenceEditSeries="Editar la serie de citas." 
            ConfirmRecurrenceEditTitle="Editar la cita recurrente." 
            ConfirmRecurrenceMoveOccurrence="Mover sola esta cita." 
            ConfirmRecurrenceMoveSeries="Mover la serie de citas." 
            ConfirmRecurrenceMoveTitle="Mover la cita recurrente" 
            ContextMenuAddAppointment="Nueva cita" 
            ContextMenuAddRecurringAppointment="Nueva cita recurrente" 
            ContextMenuDelete="Eliminar" ContextMenuEdit="Editar" 
            ContextMenuGoToToday="hoy" HeaderMultiDay="Multiples dias" 
            HeaderNextDay="siguiente día" HeaderPrevDay="día anterior" 
            Reminder="Recordatorio" ReminderBeforeStart="antes de iniciar" 
            ReminderDay="día" ReminderDays="días" ReminderDismiss="Rechazar" 
            ReminderDismissAll="Rechazar todas" ReminderHour="hora" ReminderHours="horas" 
            ReminderMinute="minuto" ReminderMinutes="minutos" ReminderNone="Ninguno" 
            ReminderOpenItem="Abrir Item" Reminders="Recordatorio" ReminderWeek="semana" 
            ReminderWeeks="semanas" ShowAdvancedForm="Opciones" 
            ShowBusinessHours="Mostrar horas de oficina..." ShowMore="mas..." />
    </telerik:RadScheduler>
    --%>
    </div>
        <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="form1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                                       
                </UpdatedControls>
            </telerik:AjaxSetting>            
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Vista">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />
    <telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
        <script type="text/javascript">
            Sys.Application.add_load(function () {
                demo.dock = $find("<%= RadDock1.ClientID %>");
            });
        </script>
        <script type="text/javascript" >
            (function () {
                var demo = window.demo = window.demo || {};

                window.openForm = function () {

                    // Center the RadDock on the screen
                    var viewPort = $telerik.getViewPortSize();
                    var xPos = Math.round((viewPort.width - parseInt(demo.dock.get_width())) / 2);
                    var yPos = Math.round((viewPort.height - parseInt(demo.dock.get_height())) / 2);
                    $telerik.setLocation(demo.dock.get_element(), { x: xPos, y: yPos });

                    demo.dock.set_closed(false);
                    Sys.Application.remove_load(openForm);
                }

                window.hideForm = function () {
                    demo.dock.set_closed(true);
                    return true;
                }

                window.OnClientClicked = function (sender, args) {
                    var validated = Page_ClientValidate('Val');
                    if (!validated) return;

                    demo.dock.set_closed(true);

                }

                window.OnClientCommand = function (sender, eventArgs) {
                    if (eventArgs.Command.get_name() === "Cerrar") {
                        closeDropdowns();
                    }
                }

                window.OnClientDragStart = function (sender, eventArgs) {
                    closeDropdowns();
                }

                window.closeDropdowns = function () {
                    var combos = Telerik.Web.UI.RadComboBox.ComboBoxes;

                    for (var i = 0, length = combos.length; i < length; i++) {
                        combos[i].hideDropDown();
                    }
                }
            } ());
        
        </script>
         <script type="text/javascript">
             // cdigo de JS para el funcionamiento de las pantallitas Modal
             // Get the modal 
             var modal = document.getElementById('myModal');

             // Get the button that opens the modal
             var btn = document.getElementById("myBtn");

             // Get the <span> element that closes the modal
             var span = document.getElementsByClassName("close")[0];

             // When the user clicks on the button, open the modal 
             btn.onclick = function () {
                 try {
                     alert('aquii');
                     modal.style.display = "block";
                 }
                 catch (err) {
                     alert(err);
                 }
             }

             function openMyDialog(url) {
                 var manageWindow = GetRadWindowManager();
                 try {
                     if (manageWindow) {
                         var radWindow = manageWindow.open(url, "<your_dialog_name>");
                         if (radWindow) {
                             radWindow.set_initialBehaviors(Telerik.Web.UI.WindowBehaviors.None);
                             radWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Resize);
                             radWindow.setActive(true);
                             radWindow.SetModal(true);
                             radWindow.center();
                             radWindow.set_visibleStatusbar(false);
                             radWindow.set_keepInScreenBounds(true);
                             //  radWindow.set_minWidth(640);
                             //  radWindow.set_minHeight(480);
                             radWindow.setSize(640, 480);
                             radWindow.set_destroyOnClose(true);
                             //radWindow.add_close(closeMyDialog); //after closing the RadWindow, closeMyDialog will be called
                             //  radWindow.argument = args; //you can pass the value from parent page to RadWindow dialog as this line
                         }
                     }
                 }
                 catch (err) {
                     alert(err);
                 }
             }

             function closeMoveProjectDialog(sender, args) {
                 var objArgs = args.get_argument();
                 //objArgs variable stored the values returned from the RadWindow
                 //you can use it for your purpose
             }

             // When the user clicks on <span> (x), close the modal
             span.onclick = function () {
                 try {
                     var modal2 = document.getElementById('myModal');
                     modal2.style.display = "none";
                 }
                 catch (err) {
                     alert(err);
                 }
             }

             // When the user clicks anywhere outside of the modal, close it
             window.onclick = function (event) {
                 if (event.target == modal) {

                     var modal4 = document.getElementById('myModal');
                     modal4.style.display = "none";
                 }
             }

             function showRadWindow() {
                 //Grab the RadWindow Object
                 var wnd = $find("<%=RadWindow1.ClientID %>");
                 //Display radwindow using the show method
                 wnd.show();
             }

             function CloseWindow1(sender, args) {
                 var window;
                 //Grab the RadWindow Object
                 window = $find('<%= RadWindow1.ClientID %>');
                 //Hide the radwindow
                 window.hide();
             }

             function CloseWindow2(sender, args) {
                 var window;
                 //Grab the RadWindow Object
                 window = $find('<%= RadWindow2.ClientID %>');
                 //Hide the radwindow
                 window.hide();
             }
             
             function OnClientCloseHandler(sender, args) {
                 try {
                     var btn = document.getElementById("<%= btnActualizaCancel.ClientID %>");
                     btn.click();
                 }
                 catch (err) {
                     alert(err);
                 }

             }

        </script>

        <script type="text/javascript" >
        function handleClickEvent(sender, eventArgs) {
                var key = eventArgs.get_keyCode();
                if (key && key == 13)
                    eventArgs.set_cancel(true);
            }

        </script>

        <style type="text/css">
        
        .rsCustomAppointmentContainer1
        {
            width: 100% !important;
            height: 100% !important;
            position: relative;
            z-index: 2;
            
            font: 10px Verdana, sans-serif;
        }

        .rsCustomAppointmentContainer2
        {
            width: 100% !important;
            height: 100% !important;
            position: relative;
            z-index: 2;
            
            font: 10px Verdana, sans-serif;
        }
        
        .rsCustomAppointmentContainer3
        {
            width: 100% !important;
            height: 100% !important;
            position: relative;
            z-index: 2;
            
            font: 10px Verdana, sans-serif;
        }
        </style>
    </telerik:RadScriptBlock>
    <%-- 
    background: url('Imagenes/fondoentrevista.jpg') no-repeat top right;
    background: url('Imagenes/fondocorreo.png') no-repeat top right;
    background: url('Imagenes/fondoentrevista.jeg') no-repeat top right;
    --%>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadDock RenderMode="Lightweight" runat="server" ID="RadDock1" Width="500px" Height="250px" Closed="true"
        Style="z-index: 2000;" Title="Editar Cita" OnClientCommand="OnClientCommand" OnClientDragStart="OnClientDragStart" Skin="Vista">
        <Commands>
            <telerik:DockCloseCommand></telerik:DockCloseCommand>
        </Commands>
        <ContentTemplate>
            <asp:Panel ID="PanelDock" runat="server">
                <div class="editForm">
                    <div class="header">
                        <asp:Label runat="server" ID="StatusLabel"></asp:Label>
                    </div>
                    <div class="content">
                    <table>
                        <tr><td colspan="2">&nbsp;</td></tr>
                        <tr>
                            <td style="width=150px; "><label>Descripcion</label></td>
                            <td><telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="DescriptionText" Height="61px" TextMode="MultiLine" Width="344px" />
                                <asp:RequiredFieldValidator runat="server" ID="DescriptionTextRequiredFieldValidator" ValidationGroup="Val"
                                    Display="Dynamic" ControlToValidate="DescriptionText" ErrorMessage="Descripcion es requerida"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td><label>Fecha</label></td>
                            <td><telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="StartTimeText" Width="100px"></telerik:RadTextBox>
                                <label id="lblDocto2" onclick="alert('abrir formato de documento 2');" style="cursor:pointer; font-style:italic; color:Blue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td><label>Tipo Visita</label></td>
                            <td><telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="TipoVisitaText" Width="140px"></telerik:RadTextBox>
                                &nbsp;&nbsp;
                                <a id="URLHelp" runat="server" target="_blank"  onclick="window.open(this.href, this.target, 'width=1000,height=400'); return false;" />
                                <a id="URLCorreo" runat="server" target="_blank"  onclick="window.open(this.href, this.target, 'width=1000,height=550'); return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td><label>RSC</label></td>
                            <td><telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="RSCNameText" Width="240px"></telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td colspan ="2"><asp:Label runat="server" ID="lblPreRequi" Text="No olvidarse de llevar los siguientes documentos/requisitos para la visita:"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan ="2">
                                <asp:CheckBoxList runat="server" ID="chklstPreRequisitos" AutoPostBack="false" 
                                            RepeatColumns="2" CellSpacing="2" CellPadding="2" Width="500px"/>
                            </td>
                            </tr>
                    </table>                    
                        <%-- 
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="StartTime" runat="server" SharedCalendarID="SharedCalendar"
                            SharedTimeViewID="SharedTimeView" Width="220px">
                        </telerik:RadDateTimePicker>
                        <asp:RequiredFieldValidator runat="server" ID="StartTimeRequiredFieldValidator" Display="Dynamic" ValidationGroup="Val"
                            ControlToValidate="StartTime" ErrorMessage="La fecha de la cita es requerida"></asp:RequiredFieldValidator>
                    
                    <telerik:RadTimeView ID="SharedTimeView" runat="server">
                    </telerik:RadTimeView>
                    <telerik:RadCalendar RenderMode="Lightweight" ID="SharedCalendar" runat="server" EnableMonthYearFastNavigation="False"
                        EnableMultiSelect="False" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                    </telerik:RadCalendar>
                     
                         --%>
                    </div>
                    <div class="footer" >
                            
                    </div>
                    
                    <asp:HiddenField runat="server" ID="_originalRecurrenceRule"></asp:HiddenField>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadDock>
    <%--     --%>
    
<telerik:RadWindowManager ID="AssetPreviewManager" Modal="true" 
EnableEmbeddedSkins="true" runat="server"  DestroyOnClose="true" Behavior="Close" EnableViewState="true"  
style="z-index:8000">
    <Windows>     
    <%-- raddock de captura motido de pateo     --%>
        <telerik:RadWindow runat="server" ID="RadWindow1" Width="300px" Height="270px" Modal="true" RenderMode="Lightweight" Behavior="Close" OnClientClose="OnClientCloseHandler"  Skin="Vista">
        <ContentTemplate>
           <table style="font-family: Verdana; font-size: 8pt;" border="1" >
                <tr>
                     <td width="250px">
                        <asp:Label ID="lblMotivo1" runat="server" Text="Motivo para reprogramar la cita " ></asp:Label>
                        <asp:Label ID="lblCita1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="250px" style="font-weight: bold" align="right">
                        <telerik:RadTextBox ID="txtMotivoRepro" MaxHeight="200px" runat="server"  AutoPostBack="false" TextMode="MultiLine" Height="100px" Resize="false"
                            RenderMode="Lightweight" Width="200px" ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td colspan="2" align="right">
                        <table>
                            <tr>
                                <td><asp:ImageButton id="btnActualizaOK" OnClick="btnReprogramaOK_OnClick" runat ="server" ImageUrl="imagenes/Button_OK.png" Height="20px" Width="20px" /> </td>
                                <td><asp:ImageButton id="btnActualizaCancel" OnClientClick="CloseWindow2(this,1); " OnClick="btnCancelarCambio_OnClick" runat ="server" ImageUrl="imagenes/Button_Delete.png" Height="20px" Width="20px" /> </td>
                            </tr>
                        </table>
                    </td>
                </tr>
           </table>
        </ContentTemplate>
    </telerik:RadWindow>
      <%--  raddock de captura motivo eliminacion   --%>
    <telerik:RadWindow runat="server" ID="RadWindow2" Width="300px" Height="270px" Modal="true" RenderMode="Lightweight" Behavior="Close" OnClientClose="OnClientCloseHandler" Skin="Vista">
        <ContentTemplate>
           <table style="font-family: Verdana; font-size: 8pt;" border="1" >
                <tr>
                     <td width="250px">
                        <asp:Label ID="lblMotivo2" runat="server"  Text="Motivo para eliminar la cita "></asp:Label>
                        <asp:Label ID="lblCita2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="250px" style="font-weight: bold"  align="right">
                        <telerik:RadTextBox ID="txtMotivoElimina" MaxHeight="200px" runat="server"  AutoPostBack="false" TextMode="MultiLine" Height="100px" Resize="false"
                            RenderMode="Lightweight" Width="200" ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td colspan="2" align="right">
                        <table>
                            <tr>
                                <td><asp:ImageButton id="btnEliminaOK" OnClick="btnEliminaOK_OnClick" runat ="server" ImageUrl="imagenes/Button_OK.png" Height="20px" Width="20px" /> </td>
                                <td><asp:ImageButton id="btnEliminaCancel" OnClientClick="CloseWindow2(this,1);" OnClick="btnCancelarCambio2_OnClick" runat ="server" ImageUrl="imagenes/Button_Delete.png" Height="20px" Width="20px" /> </td>
                            </tr>
                        </table>
                    </td>
                </tr>
           </table>
        </ContentTemplate>
    </telerik:RadWindow>
   </Windows>  
</telerik:RadWindowManager>



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ProviderName="System.Data.SqlClient" 
        DeleteCommandType="Text" >
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Start" Type="DateTime"></asp:Parameter>
            <asp:Parameter Name="End" Type="DateTime"></asp:Parameter>
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Subject" Type="String"></asp:Parameter>
            <asp:Parameter Name="Description" Type="String"></asp:Parameter>
            <asp:Parameter Name="Start" Type="DateTime"></asp:Parameter>
            <asp:Parameter Name="End" Type="DateTime"></asp:Parameter>
            <asp:Parameter Name="RecurrenceRule" Type="String"></asp:Parameter>
            <asp:Parameter Name="RecurrenceParentID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Reminder" Type="String"></asp:Parameter>
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="VisitaDataSource" runat="server"
        ProviderName="System.Data.SqlClient" >
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="RSCDataSource" runat="server"
        ProviderName="System.Data.SqlClient" >
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
        ProviderName="System.Data.SqlClient" >
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="RequiDataSource" runat="server"
        ProviderName="System.Data.SqlClient" >
    </asp:SqlDataSource>
            <%--
    <telerik:RadScheduler ID="RadScheduler1" runat="server" Culture="es-MX" 
            EnableCustomAttributeEditing="True"  SelectedView="MonthView" 
            DataSourceID="SqlDataSource1" OverflowBehavior="Expand"
            DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" 
            DataEndField="End" DataReminderField="Reminder" 
            DataRecurrenceField="RecurrenceRule" DataRecurrenceParentKeyField="RecurrenceParentID"
            CustomAttributeNames="Ejemplo" Visible="false">
            
            <AdvancedForm EnableCustomAttributeEditing="True" />
            <Reminders Enabled="false"></Reminders>
            <ResourceTypes>
                   <telerik:ResourceType KeyField="Id" Name="Requisitos" TextField="Descripcion" ForeignKeyField="ID" AllowMultipleValues="true"    
                        DataSourceID="RequiDataSource" ></telerik:ResourceType>
            </ResourceTypes>
    </telerik:RadScheduler>
    --%>
    <table style="font-family: Verdana; font-size: 8pt;" border="0" >
        <tr>
            <td colspan="5">&nbsp;
                <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                    width="99%">
                    <tr>
                        <td colspan="5">
                            <asp:Label ID="lblMensaje" runat="server" />
                        </td>
                        <td style="text-align: right" width="1000px">
                            <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                        </td>
                        <td width="150px" style="font-weight: bold">
                            <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                                Width="150px" AutoPostBack="True">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
                <asp:Label ID="LblCliente" runat="server" Text="Cliente"  />
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>                                        
                        <td style="padding-right:5px">
                            <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" Width="125px" MinValue="1" 
                                    MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                <ClientEvents OnKeyPress="handleClickEvent" />
                            </telerik:RadNumericTextBox>                                    
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false" Enabled="false" >
                                <ClientEvents OnKeyPress="handleClickEvent" />
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
               
            </td>
        </tr>
        <tr>
            <td>&nbsp;
                <asp:Label ID="LblRSC" runat="server" Text="RSC/ASESOR/RIK"  />
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cmbRSC" AutoPostBack="false"  Size="large" 
                    EnableVirtualScrolling="True" ItemsPerRequest="10" ShowMoreResultsBox="True"
                    ChangeTextOnKeyBoardNavigation="true" Filter="Contains"
                    MaxHeight="300px" EmptyMessage="-- Seleccionar --" Width="400px" >
                <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                    NoMatches="No hay coincidencias" />
                </telerik:RadComboBox> 
                &nbsp;&nbsp;
                <asp:Imagebutton ID="imgFiltros" runat="server" Height="15px" ImageUrl="~/Imagenes/view.png" AutoPostBack="true" onclick="imgFiltros_Click"  /> 
            </td>
        </tr>
        <tr><td colspan="3">&nbsp;<asp:HiddenField ID="HF_ClvPag" runat="server" /> <asp:HiddenField ID="hf_CitaVisitaModif" runat="server" /><asp:HiddenField ID="HF_Usuario" runat="server" /></td></tr>
    </table>
    <br />
    <telerik:RadScheduler ID="RadScheduler1"  StartEditingInAdvancedForm="false" Culture="es-MX" 
                FirstDayOfWeek="Monday" LastDayOfWeek="Friday" SelectedView="MonthView"  runat="server"
            DataSourceID="SqlDataSource1" 
            DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start"
            DataEndField="End" DataDescriptionField="Description"
             style="height:auto; min-height:auto"
             visible="false"
            OnAppointmentUpdate="RadScheduler1_AppointmentUpdate"
            OnAppointmentDelete="RadScheduler1_AppointmentDelete"   
            RenderMode="Lightweight"
            OnFormCreating="RadScheduler1_FormCreating" Reminders-Enabled="false"
            >
        <AdvancedForm  Modal="true" />
        <ResourceTypes>
            <telerik:ResourceType KeyField="Id" Name="Visita" TextField="Descripcion" ForeignKeyField="Id_TipoVisita" 
                DataSourceID="VisitaDataSource"></telerik:ResourceType>
        </ResourceTypes>
         <ResourceStyles>
            <telerik:ResourceStyleMapping Type="Visita" Text="VISITA PERSONAL" ApplyCssClass="rsCategoryBlue"></telerik:ResourceStyleMapping>
            <telerik:ResourceStyleMapping Type="Visita" Text="LLAMADA TELEFONICA" ApplyCssClass="rsCategoryGreen"></telerik:ResourceStyleMapping>
            <telerik:ResourceStyleMapping Type="Visita" Text="CORREO ELECTRONICO" ApplyCssClass="rsCategoryOrange"></telerik:ResourceStyleMapping>
        </ResourceStyles>
    </telerik:RadScheduler>


    <telerik:RadScheduler ID="RadScheduler2" runat="server" SelectedView="MonthView"  Culture="es-MX"  StartEditingInAdvancedForm="false"
            DataSourceID="SqlDataSource1"  FirstDayOfWeek="Monday" LastDayOfWeek="Friday" 
            DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start"
            DataEndField="End"  DataDescriptionField="Description"
            style="height:auto; min-height:auto" Skin="Vista"
            OnAppointmentUpdate="RadScheduler1_AppointmentUpdate"
            OnAppointmentDelete="RadScheduler1_AppointmentDelete"   

            OnFormCreating="RadScheduler1_FormCreating" Reminders-Enabled="false"
            RenderMode="Lightweight"  ShowAllDayRow="false"
            >
            <AdvancedForm  Modal="true" />
        <ResourceTypes>
            <telerik:ResourceType KeyField="Id" Name="Visita" TextField="Descripcion" ForeignKeyField="Id_TipoVisita" 
                DataSourceID="VisitaDataSource"></telerik:ResourceType>
        </ResourceTypes>
        <AppointmentTemplate>
           <div class="rsCustomAppointmentContainer<%# Eval("Id_TipoVisita") %>" > 
               <img src='Imagenes/<%# Eval("Id_TipoVisita") %>.png' height="20px" width="20px" /> &nbsp;<%# Eval("Subject") %>
           </div>
        </AppointmentTemplate>    
    </telerik:RadScheduler>


    </form>
</body>
</html>

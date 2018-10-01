<%@ Page Title="Relación de facturas enviadas de Servicio de Ventas a Almacén" Language="C#"
    MasterPageFile="~/MasterPage/MasterPage02.master" AutoEventWireup="true" CodeBehind="~/CapFacturaSvtas.aspx.cs"
    Inherits="SIANWEB.CapFacturaSvtas" %> 

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .MyImageButton
        {
            cursor: hand;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                                var ajaxManager = $find("<%= RAM1.ClientID %>");
                                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                                ajaxManager.ajaxRequest('panel');
            }
            //---------------------------------------------------------------------CloseWindow-----------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = '';

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de captura de Nota de cargo
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesOrdenCompra() {
                //debugger;

            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var lblVal_cmbTipoClientID = '';
            var cmbTipoClientID = '';
            //var lblVal_txtFva_DocClientID = '';
            var txtFva_DocClientID = '';
            var lblVal_cmbFva_EnviarAClientID = '';
            var cmbFva_EnviarAClientID = '';

            //Validación del formulario de insercion/edición de registro en un RadGrid.
            //param: accion --> indica que tipo de operación se esta realizando, puede traer los valores 'insertar' o 'actualizar'
            function ValidaFormEdit(accion) {

                var continuarAccion = true;

                //debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var lblVal_cmbTipo = document.getElementById(lblVal_cmbTipoClientID);
                var cmbTipo = $find(cmbTipoClientID);
                //var lblVal_txtFva_Doc = document.getElementById(lblVal_txtFva_DocClientID);
                var txtFva_Doc = $find(txtFva_DocClientID);
                var lblVal_cmbFva_EnviarA = document.getElementById(lblVal_cmbFva_EnviarAClientID);
                var cmbFva_EnviarA = $find(cmbFva_EnviarAClientID);

                //Limpiar contenedores de mensaje de validación
                lblVal_cmbTipo.innerHTML = '';
                //lblVal_txtFva_Doc.innerHTML = '';
                lblVal_cmbFva_EnviarA.innerHTML = '';

                if (cmbTipo != null)
                    if (cmbTipo.get_value() == '-1') {
                        //lblVal_cmbTipo.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (txtFva_Doc != null)
                    if (txtFva_Doc.get_textBoxValue() == '') {
                        //lblVal_txtFva_Doc.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (cmbFva_EnviarA != null)
                    if (cmbFva_EnviarA.get_value() == '-1') {
                        //lblVal_cmbFva_EnviarA.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (continuarAccion == false) {
                    var alertaRequedridosGrig = radalert('Los datos "Tipo F/N", "Doc." y "Enviar a" son requeridos', 330, 150, tituloMensajes);
                }

                return continuarAccion
            }


            function ValidacionesEspeciales() {
                var conntinuar = true;

                return conntinuar
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                //debugger;

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();


                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }

                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'new':
                        LimpiarControlesOrdenCompra();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenId = document.getElementById('<%= hiddenId.ClientID %>');
                        hiddenId.value = '';

                        var fechaActual = new Date('<%= ActualAnio %>', '<%= ActualMes %>', '<%= ActualDia %>');

                        continuarAccion = true;
                        break;

                    case 'save':
                        //select tab datos generales

                        //continuarAccion = ValidacionesEspeciales();


                        break;
                }

                if (continuarAccion == true) {
                    GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                }

                args.set_cancel(!continuarAccion);
            }



            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                //debugger;
                                //GetRadWindow().Close();
                                CloseAndRebind();
                            });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }
            function CheckAllIncluir(sender) {
                //debugger;
                var grid = $find('<%=rgFacturaSvtaAlmacenDet.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;

                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    if (row.findElement("chkIncluir") != null) {
                        (row.findElement("chkIncluir")).checked = sender.checked;
                    }
                    else if (row.findElement("chkIncluirEditar") != null) {
                        (row.findElement("chkIncluirEditar")).checked = sender.checked;
                    }
                }

                actualizar_tabla(-1, sender.checked);
            }

            var cabezera = '<%= CtrlCabezera %>';

            function actualizar_tabla(doc, sel) {
                var urlArchivo = 'CapFacturaSvtas_Seleccionar.aspx';
                parametros = "doc=" + doc;
                parametros = parametros + "&sel=" + sel;
                var a = obtenerrequest(urlArchivo, parametros);


                var CtrlCabezera = document.getElementById(cabezera);
                if (a == '1') {
                    CtrlCabezera.checked = true;
                }
                else if (a == '2') {
                    CtrlCabezera.checked = false;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"
        IsSticky="True">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tablaPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="formulario" />
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaSvtaAlmacenDet" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenId" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaSvtaAlmacenDet" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnConfirmarTodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaSvtaAlmacenDet" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFacturaSvtaAlmacenDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaSvtaAlmacenDet" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
        </Items>
    </telerik:RadToolBar>
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    <table id="tablaPrincipal" runat="server" style="font-family: Verdana; font-size: 8pt;
        height: 100%" width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
            <td valign="top">
                <div id="formulario" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblEntrego" runat="server" Text="Entregó"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtEntrego" runat="server" Width="250px" MaxLength="50">
                                    <ClientEvents OnKeyPress="SoloAlfabetico" />
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="val_txtEntrego" runat="server" ControlToValidate="txtEntrego"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblRecibio" runat="server" Text="Recibió"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtRecibio" runat="server" Width="250px" MaxLength="50">
                                    <ClientEvents OnKeyPress="SoloAlfabetico" />
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="val_txtRecibio" runat="server" ControlToValidate="txtRecibio"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblFecha" runat="server" Text="Fecha Inicio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha" runat="server" Width="155px">
                                    <Calendar ID="Calendar2" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput2" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_txtRecibio0" runat="server" ControlToValidate="dpFecha"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="buscar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &#160;&#160;
                                <asp:Label ID="Label1" runat="server" Text="Fecha Fin"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechaFin" runat="server" Width="155px">
                                    <Calendar ID="CalendarFin" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInputFin" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorFin" runat="server" ControlToValidate="dpFechaFin"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="buscar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click"
                                    ToolTip="Buscar" ValidationGroup="buscar" />
                            </td>
                            <td>
                                &#160;
                            </td>                             
                             <td>
                                &#160;
                            </td>                              
                            <td style="width:365px; text-align:right">
                            <asp:Button ID="BtnConfirmarTodos" runat="server" Text="Confirmar" 
                                ToolTip="Confirmar Todos" OnClick="BtnConfirmarTodos_Click"  />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--  <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="770px">--%>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="None"
                    BorderWidth="1px" ScrollBars="Hidden">
                    <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="300px" BorderStyle="None">
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="300px" ResizeMode="AdjacentPane"
                            ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                            <telerik:RadPane ID="RadPane1" runat="server" Height="300px" OnClientResized="onResize"
                                BorderStyle="None" Scrolling="Both">
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="rgFacturaSvtaAlmacenDet" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                OnNeedDataSource="rgFacturaSvtaAlmacenDet_NeedDataSource" OnInsertCommand="rgFacturaSvtaAlmacenDet_InsertCommand"
                                                OnUpdateCommand="rgFacturaSvtaAlmacenDet_UpdateCommand" OnDeleteCommand="rgFacturaSvtaAlmacenDet_DeleteCommand"
                                                OnItemDataBound="rgFacturaSvtaAlmacenDet_ItemDataBound" OnItemCommand="rgFacturaSvtaAlmacenDet_ItemCommand"
                                                OnPageIndexChanged="rgFacturaSvtaAlmacenDet_PageIndexChanged" PageSize="10" AllowPaging="True"
                                                DataMember="listaOrdCompraDet">
                                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_Fva,Id_FvaDet,Fva_Doc,Fva_Tipo, Fva_Confirmado, Fva_Seleccionado,Fva_EnviarA"
                                                    EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                                    NoMasterRecordsText="No se encontraron registros.">
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn DataField="Incluir" UniqueName="Incluir">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkIncluir" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="chkIncluirEditar" runat="server" Checked="true" />
                                                            </EditItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkIncluirCabezera2" runat="server" />
                                                            </HeaderTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Id_Fva" HeaderText="Id_Fva" UniqueName="Id_Fva"
                                                            ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Id_FvaDet" HeaderText="Id_FvaDet" UniqueName="Id_FvaDet"
                                                            ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Doc." DataField="Fva_Doc" UniqueName="Fva_Doc">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFva_Doc" runat="server" Text='<%# Eval("Fva_Doc").ToString() %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtFva_Doc" runat="server" Width="65px" MaxLength="9"
                                                                    MinValue="1" AutoPostBack="true" OnTextChanged="txtFva_Doc_TextChanged" Text='<%# Eval("Fva_Doc") %>'>
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                </telerik:RadNumericTextBox>
                                                                <asp:Label ID="lblVal_txtFva_Doc" runat="server" ForeColor="#FF0000" Text='<%# Eval("Fva_Doc").ToString() %>'
                                                                    Visible="false"></asp:Label>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Fva_Fecha" HeaderText="Fecha" UniqueName="Fva_Fecha">
                                                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFva_Fecha" runat="server" Text='<%# Convert.ToDateTime(Eval("Fva_Fecha")).ToShortDateString() %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadDatePicker ID="txtFva_Fecha" runat="server" DbSelectedDate='<%# Eval("Fva_Fecha") %>'
                                                                    Width="120px" Enabled="false">
                                                                </telerik:RadDatePicker>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Número Cliente" DataField="Id_Cte" UniqueName="Id_Cte"
                                                            Display="true">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_Cte" runat="server" Text='<%# Eval("Id_Cte").ToString() %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblId_CteEdit" runat="server" Text='<%# Eval("Id_Cte").ToString() %>' />
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Cliente" DataField="Id_Cte" UniqueName="Id_CteStr">
                                                            <HeaderStyle Width="400px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId_CteStr" runat="server" Text='<%# Eval("Cte_NomComercial") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblId_CteStrEdit" runat="server" Text='<%# Eval("Cte_NomComercial") %>' />
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Fva_Importe" HeaderText="Importe" UniqueName="Fva_Importe">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtFva_Importe" runat="server" Width="65px" MaxLength="9"
                                                                    MinValue="0" ReadOnly="true" BackColor="Transparent" BorderWidth="0" Text='<%# Eval("Fva_Importe") %>'>
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtFva_ImporteEdit" runat="server" Width="65px" MaxLength="9"
                                                                    MinValue="0" ReadOnly="true" BackColor="Transparent" BorderWidth="0" Text='<%# Eval("Fva_Importe") %>'>
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Revisión" DataField="Fva_DiaRev" UniqueName="Fva_DiaRev">
                                                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFva_DiaRev" runat="server" Text='<%# Eval("Fva_DiaRev") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblVal_Fva_DiaRev" runat="server" Text='<%# Eval("Fva_DiaRev") %>' />
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                            EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar"
                                                            HeaderText="Editar">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridButtonColumn CommandName="Confirmar" HeaderText="Confirmar" ConfirmDialogType="RadWindow"
                                                            ConfirmText="¿Desea confirmar la revisión de la factura <b>#[[ID]]</b> ? <br />"
                                                            Text="Confirmar" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" UniqueName="Confirmar"
                                                            Visible="true" ButtonType="PushButton">
                                                            <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridButtonColumn ConfirmText="¿Está seguro de quitar el documento de la lista?"
                                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow"
                                                            ButtonType="ImageButton" CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn"
                                                            HeaderText="Eliminar">
                                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                    ShowPagerText="True" PageButtonCount="10" />
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:HiddenField ID="hiddenId" runat="server" />
                <asp:HiddenField ID="HiddenHeight" runat="server" />
                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
            </td>
        </tr>
    </table>
</asp:Content>

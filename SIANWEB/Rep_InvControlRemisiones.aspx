<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="Rep_InvControlRemisiones.aspx.cs" Inherits="SIANWEB.Rep_InvControlRemisiones3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function refreshGrid() {

            }

            //Validaciones especiales
            function ValidacionesEspeciales(sender, args) {
                //debugger;

                var AA = true;

                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find('<%= dpFechaini.ClientID %>');
                var datePickerFechaFin = $find('<%= dpFechafin.ClientID %>');

                if (datePickerFechaInicio != null && datePickerFechaFin != null) {

                    //realizar validaciones
                    var fechaInicio = null;
                    var fechaFin = null;

                    fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                    fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                    //validar rango correcto de fechas.
                    if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                        var mensage = 'La fecha inicial, no debe ser mayor a la fecha final';
                        var alerta = radalert(mensage, 330, 150, tituloMensajes);

                        alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                        AA = false
                    }
                }
                args.set_cancel(!AA);
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio1.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbRemisiones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipoRem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNumeroCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTer" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtRepresentante" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRepresentante">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
        OnClientButtonClicking="ValidacionesEspeciales">
        <Items>
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ValidationGroup="Imprimir" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                ValidationGroup="Imprimir" ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
            </td>
            <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <div id="divPrincipal" runat="server">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td width="100">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblFiltroAnalisis" runat="server" Text="Filtro de análisis"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td width="110">
                                <asp:Label ID="lblRemisiones" runat="server" Text="Remisiones"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadComboBox ID="cmbRemisiones" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbRemisiones_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbRemisiones"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                    ValidationGroup="Imprimir"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td width="110">
                                <asp:Label ID="lblTipoRemision" runat="server" Text="Tipo de remisión"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadComboBox ID="cmbTipoRem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbTipoRem_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbTipoRem"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                    ValidationGroup="Imprimir"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="FechaIni" visible="false">
                            <td>
                            </td>
                            <td width="110">
                                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadDatePicker ID="dpFechaini" runat="server" Width="155px" Culture="es-MX"
                                    Enabled="False">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr runat="server" id="FechaFin" visible="false">
                            <td>
                            </td>
                            <td width="110">
                                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final "></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadDatePicker ID="dpFechafin" runat="server" Width="155px" Culture="es-MX"
                                    Enabled="False">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr runat="server" id="txtAnyo" visible="false">
                            <td>
                                &nbsp;
                            </td>
                            <td width="110">
                                <asp:Label ID="Label7" runat="server" Text="Año"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadNumericTextBox ID="NtxtAnyo" runat="server" MaxLength="4" MinValue="0">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="NtxtAnyo"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Imprimir"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="txtCliente" visible="false">
                            <td>
                            </td>
                            <td width="110">
                                <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" MinValue="1" MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>                                    
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false" Enabled="false">
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="txtTerritorio" visible="false">
                            <td>
                            </td>
                            <td width="110">
                                <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="txtTerritorio1" runat="server" MaxLength="5" onpaste="return false">                                        
                                    <ClientEvents OnBlur="txtTerritorio_OnBlur"  OnKeyPress="handleClickEvent" />
                                </telerik:RadTextBox>                                                     
                            </td>                    
                            <td>
                                <telerik:RadComboBox ID="cmbTer" runat="server" AutoPostBack="True" Width="300px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                        EnableLoadOnDemand="True" Filter="Contains" 
                                                        OnClientSelectedIndexChanged="cmbTer_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="cmbTer_SelectedIndexChanged">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 50px; text-align: center">
                                                                <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                            </td>
                                                            <td style="width: 200px; text-align: left">
                                                                <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>                                                                    
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr runat="server" id="txtRik" visible="false">
                            <td>
                            </td>
                            <td width="110">
                                <asp:Label ID="lblRik" runat="server" Text="RIK"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:radtextbox id="txtRepresentante" runat="server" AutoPostBack="true" maxlength="5"
                                        onpaste="return false" OnTextChanged="txtRep_TextChanged">
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radtextbox>
                                <asp:HiddenField runat="server" id="txtRepOld"/>                                                                            
                            </td>
                            <td>
                                <telerik:radtextbox id="txtRepresentanteStr" runat="server" width="300px" maxlength="5"
                                            onpaste="return false" enabled="false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radtextbox>
                            </td>
                        </tr>                        
                        <tr runat="server" id="txtProducto" visible="false">
                            <td>
                            </td>
                            <td width="110">
                                <asp:Label ID="lblProducto" runat="server" Text="Producto"></asp:Label>
                            </td>
                            <td colspan="2">
                                <%--<telerik:RadNumericTextBox ID="txtProductoId" Runat="server" Width="70px">
                                </telerik:RadNumericTextBox>--%>
                                <telerik:RadTextBox onpaste="return false" ID="RadTextBoxProducto" runat="server"
                                    MaxLength="256">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr runat="server" id="Detalle" visible="false">
                            <td>
                            </td>
                            <td width="110">
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkDetalle" runat="server" Text="Detalle" Visible="False" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="MostrarEnc" visible="false">
                            <td colspan="3">
                                Mostrar en
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="MostrarDet" visible="false">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblMostrar" runat="server">
                                    <asp:ListItem Selected="True" Value="x">Ambos</asp:ListItem>
                                    <asp:ListItem Value="b">Pesos</asp:ListItem>
                                    <asp:ListItem Value="a">Unidades</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="FiltroEnc" visible="false">
                            <td colspan="3">
                                <asp:Label ID="lblFiltroEstatus" runat="server" Text="Filtro de estatus"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="Estatus" visible="false">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2" rowspan="2">
                                <asp:RadioButtonList ID="rblEstatus" runat="server">
                                    <asp:ListItem Selected="True">General</asp:ListItem>
                                    <asp:ListItem>Vencidas</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="Estatus2Enc" visible="false">
                            <td colspan="3">
                                <asp:Label ID="lblFiltroEstatus0" runat="server" Text="Filtro de estatus"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="estatus2Det" visible="false">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblEstatus0" runat="server">
                                    <asp:ListItem Selected="True" Value="-1">Todas</asp:ListItem>
                                    <asp:ListItem Value="1">Vencidas</asp:ListItem>
                                    <asp:ListItem Value="0">No vencidas</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                          <tr runat="server" id="TrNivelEnc" visible="false">
                            <td colspan="3">
                                <asp:Label ID="Label1" runat="server" Text="Nivel"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="TrNivelDet" visible="false">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblNivel" runat="server">
                                    <asp:ListItem Selected="True" Value="D">Documento</asp:ListItem>
                                    <asp:ListItem Value="P">Producto</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

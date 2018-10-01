<%@ Page Title="Cumplimiento de rutas de servicio" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_SerRutasServicio.aspx.cs" Inherits="SIANWEB.Rep_SerRutasServicio" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Mostrar':
                        var txtRuta = $find("<%= txtRuta.ClientID %>");
                        if (txtRuta != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtRuta);
                        var txtTerr = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerr != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
                        var txtCliente = $find("<%= txtNumeroCliente.ClientID %>");
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        //Opcional, validaciones extras
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function refreshGrid() {
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
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
                </UpdatedControls>
            </telerik:AjaxSetting>            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Mostrar" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                        width="99%">
                        <tr>
                            <td colspan="4">
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
                <td>
                    <table>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="LblFechaini" runat="server" Text="Fecha inicial" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechaini" runat="server" Width="155px">
                                    <Calendar ID="Calendar1" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtFechaini" ValidationGroup="Mostrar"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="LblFechafin" runat="server" Text="Fecha final" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechafin" runat="server" Width="155px">
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtFechafin" ValidationGroup="Mostrar"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblRuta" runat="server" Text="Ruta" />
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtRuta" runat="server" MaxLength="19" onpaste="return false">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblCliente" runat="server" Text="Cliente" />
                            </td>
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>                                        
                                        <td style="padding-right:5px">
                                            <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" Width="125px" MinValue="1" MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
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
                                </table>
                            </td>                           
                        </tr>    
                        <tr>
                            <td>
                                <asp:Label ID="LblTerr" runat="server" Text="Territorio" />
                            </td>
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>                                        
                                        <td style="padding-right:5px">
                                            <telerik:RadTextBox ID="txtTerritorio" runat="server" MaxLength="19" onpaste="return false">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="txtTerritorio_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox ID="cmbTer" runat="server" AutoPostBack="True" Width="300px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
                                                                DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                                EnableLoadOnDemand="True" Filter="Contains" 
                                                                OnClientSelectedIndexChanged="cmbTer_ClientSelectedIndexChanged">
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
                                </table>
                            </td>
                        </tr>                    
                        <tr>
                            <td>
                                <asp:Label ID="LblOrden" runat="server" Text="Orden" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbOrden" runat="server" Width="125px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Fecha " Value="1" Owner="cmbOrden"
                                            Selected="true" />
                                        <telerik:RadComboBoxItem runat="server" Text="Ruta" Value="2" Owner="cmbOrden" />
                                        <telerik:RadComboBoxItem runat="server" Text="Territorio" Value="3" Owner="cmbOrden" />
                                        <telerik:RadComboBoxItem runat="server" Text="Cantidad de equipos" Value="4" Owner="cmbOrden" />
                                        <telerik:RadComboBoxItem runat="server" Text="% de cumplimiento" Value="5" Owner="cmbOrden" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkDetalle" runat="server" Text="Detalle" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

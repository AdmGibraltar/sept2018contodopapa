<%@ Page Title="Kardex de Inversión" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="RepKardexInversion.aspx.cs" Inherits="SIANWEB.RepKardexInversion" %>

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
                        var txtRepresentante = $find("<%= txtRepresentante.ClientID %>");
                        if (txtRepresentante != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtRepresentante);
                        var txtTerritorio = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerritorio != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerritorio);
                        var txtCliente = $find("<%= txtCliente.ClientID %>");
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        var txtEquipo = $find("<%= txtEquipo.ClientID %>");
                        if (txtEquipo != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtEquipo);
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
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbAño">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbMes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
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
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="Mostrar" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
                        font-size: 8pt">
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
                    <table>
                        <tr>
                            <td colspan="2" class="style1">
                                <asp:Label ID="LbllCliente" runat="server" Text="Cliente" />
                            </td>
                            <td class="style1">
                                <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="125px" MaxLength="19" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false" Enabled="false">
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblTerritorio" runat="server" Text="Territorio" />
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtTerritorio" runat="server" Width="125px" MaxLength="19"
                                    onpaste="return false">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
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
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblRepres" runat="server" Text="Representante" />
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtRepresentante" runat="server" Width="125px" MaxLength="19"
                                    onpaste="return false" OnTextChanged="txtRep_TextChanged">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                                <asp:HiddenField runat="server" id="txtRepOld"/>
                            </td>
                            <td>
                                <telerik:radtextbox id="txtRepresentanteStr" runat="server" width="300px" maxlength="5"
                                                onpaste="return false" enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radtextbox>
                            </td>
                        </tr>                      
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblEquipos" runat="server" Text="Equipos" />
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtEquipo" runat="server" Width="125px" MaxLength="19" onpaste="return false">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblAño" runat="server" Text="Año" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbAño" runat="server" Width="125px" AutoPostBack="true"
                                    MaxHeight="250px" OnSelectedIndexChanged="cmbAño_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="cmbAño" ValidationGroup="Mostrar"
                                    EnableTheming="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblMes" runat="server" Text="Mes" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbMes" runat="server" Width="125px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="--Seleccionar-- " Value="-1" Owner="cmbNivel"
                                            Selected="true" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="cmbMes" ValidationGroup="Mostrar"
                                    EnableTheming="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>

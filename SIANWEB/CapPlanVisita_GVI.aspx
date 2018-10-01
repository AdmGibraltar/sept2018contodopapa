<%@ Page Title="Programacion de Visita Inicial - Gestion Venta Instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapPlanVisita_GVI.aspx.cs" Inherits="SIANWEB.CapPlanVisita_GVI" %>

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
//                        var txtTerr = $find("= txtTerritorio.ClientID ");
//                        if (txtTerr != null)
//                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
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
                           
                        </tr>
                       

                        <tr><td colspan="3"></td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

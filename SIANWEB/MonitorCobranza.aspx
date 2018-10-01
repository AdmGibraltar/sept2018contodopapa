<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="MonitorCobranza.aspx.cs" Inherits="SIANWEB.MonitorCobranza" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"
        ZIndex="1500">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlDetalle" UpdatePanelHeight="" /> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbFiltroTCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tblFiltro" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divGraficas1" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="divGraficas2" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="PnlDetalle" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    &nbsp;
                </td>
                <td width="150px" style="font-weight: bold">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt;">
            <tr>
                <td width="10">
                </td>
                <td>
                    <table runat="server" id="tblFiltro">
                        <tr>
                            <td width="5">
                            </td>
                            <td width="90">
                                <asp:Label ID="lblTcentro" runat="server" Text="Tipo de centro"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbFiltroTCentro" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbFiltroTCentro_SelectedIndexChanged"
                                    Width="200px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td width="5">
                            </td>
                            <td>
                                <asp:Label ID="lblCentro" runat="server" Text="Centro"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbFiltroCentro" runat="server" Width="200px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbFiltroCentro"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Buscar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table runat="server" id="tblFiltro2">
                        <tr>
                            <td width="5">
                                &nbsp;
                            </td>
                            <td width="90">
                                <asp:Label ID="lblVer" runat="server" Text="Ver en"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbFiltroVer" runat="server" Width="200px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Selected="True" Text="Porcentajes" Value="%"
                                            Owner="cmbFiltroVer" />
                                        <telerik:RadComboBoxItem runat="server" Text="Pesos" Value="$" Owner="cmbFiltroVer" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                    ToolTip="Buscar" ValidationGroup="Buscar" />
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
                            <td>
                            </td>
                        </tr>
                    </table>
                    <div runat="server" id="divGraficas1" visible="True">
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
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
                                </td>
                                <td>
                                    <asp:Literal ID="LiteralEntrega" runat="server"></asp:Literal>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Literal ID="LiteralCobranza" runat="server"></asp:Literal>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Literal ID="LiteralRevision" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="LiteralVencidas" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr style="font-weight: bold; font-size: x-small;">
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Entrega de mercancía"></asp:Label>
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label2" runat="server" Text="Entrega a cobranza"></asp:Label>
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label3" runat="server" Text="Documnetos a revisión"></asp:Label>
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblGraficaVencidos" runat="server" Text="Saldo por recuperar"></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-weight: bold; font-size: x-small;">
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Literal ID="LiteralDiasVencidos" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="LiteralCartera" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="LiteralCosto" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr style="font-weight: bold; font-size: x-small;">
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    Dias Vencidos sobre venta
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    Rotación de cartera
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    Costo financiero de cartera no cobrado a tiempo 
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
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div runat="server" id="divGraficas2" align="center" visible="true">
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="LiteralAvance" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr style="font-weight: bold; font-size: x-small;">
                                <td>
                                    Avance de recuperación de cartera
                                </td>
                            </tr>
                            <tr style="font-weight: bold; font-size: x-small;">
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <telerik:RadPanelBar ID="PnlDetalle" runat="server" Width="900px" AllowCollapseAllItems="True"
                                    ExpandMode="SingleExpandedItem">
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Owner="PnlDetalle" PostBack="False">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td width="10px">
                                                        </td>
                                                        <td>
                                                            <div runat="server" id="divaTiempo">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Owner="PnlDetalle" PostBack="False">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td width="10px">
                                                        </td>
                                                        <td>
                                                            <div runat="server" id="divAtrasado">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Owner="PnlDetalle" PostBack="False">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td width="10px">
                                                        </td>
                                                        <td>
                                                            <div runat="server" id="divClientes">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </telerik:RadPanelItem>
                                    </Items>
                                    <ExpandAnimation Duration="100" />
                                    <CollapseAnimation Duration="100" />
                                </telerik:RadPanelBar>
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
                    </table>
                </td>
            </tr>
        </table>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                var boton;
                function Button_Click(param, control) {

                    boton = control;
                    var oWnd = radopen("Ventana_Acciones.aspx?Id='" + Math.random() + "'&" + param, "AbrirVentana_Acciones");
                    oWnd.center();
                    return false;
                }

                function refresh() {
                    boton.src = "Img\\Cerrado.png";
                    //var ajaxManager = $find("<%= RAM1.ClientID %>");
                    //ajaxManager.ajaxRequest();
                }

            </script>
        </telerik:RadCodeBlock>
    </div>
</asp:Content>

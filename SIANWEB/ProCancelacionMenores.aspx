<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProCancelacionMenores.aspx.cs" Inherits="SIANWEB.ProCancelacionMenores" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgAceptar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
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
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="100px">
                                    <Calendar runat="server"><%-- UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">--%>
                                       <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DatePopupButton ToolTip="Abrir calendario" />
                                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" 
                                        DateFormat="dd/MM/yyyy" MaxLength="10">
                                         <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                   <%-- <DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled rcCalPopup"
                                        Enabled="False">
                                    </DatePopupButton> --%>                                      
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dpFecha1"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label8" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha2" runat="server" Width="100px">
                                    <Calendar runat="server"><%-- UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">--%>
                                       <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                    </Calendar>
                                     <DatePopupButton ToolTip="Abrir calendario" />
                                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" 
                                        DateFormat="dd/MM/yyyy" MaxLength="10">
                                         <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <%--<DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled rcCalPopup"
                                        Enabled="False">
                                    </DatePopupButton>--%>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dpFecha2"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgAceptar" runat="server" CssClass="aceptar" ImageUrl="~/Imagenes/blank.png"
                                    OnClick="imgAceptar_Click" ToolTip="Aceptar" ValidationGroup="print" />
                            </td>
                        </tr>                    
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

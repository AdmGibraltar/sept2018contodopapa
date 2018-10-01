<%@ Page Title="Rastreo de documentos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProRastreoDocumentos.aspx.cs" Inherits="SIANWEB.ProRastreoDocumentos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipo">
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
                            <td width="65">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="20">
                                &nbsp;
                            </td>
                            <td width="100">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="50">
                                <asp:Label ID="Label7" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="cmbTipo" runat="server" OnSelectedIndexChanged="CmbTipo_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="50">
                                <asp:Label ID="Label16" runat="server" Text="Serie"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbSerie" runat="server" MaxHeight="250px" Width="120px">
                                </telerik:RadComboBox>
                            </td>
                            <td align="right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbSerie"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="buscar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
<%--                            <td width="65">
                                <asp:Label ID="Label15" runat="server" Text="Documento"></asp:Label>
                            </td>
--%>                            <td>
                                <telerik:RadComboBox ID="CmbTipoBusqueda" runat="server" OnSelectedIndexChanged="CmbTipoBusqueda_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </telerik:RadComboBox>
                            </td>
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="txtDocumento" runat="server" MaxLength="9" MinValue="1"
                                    Width="90px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>

                                <telerik:RadTextBox ID="txtDocumentoFolioFiscal" runat="server" width="280px" Visible="false">
                                </telerik:RadTextBox>

                            </td>
                            <td align="left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDocumento"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="buscar"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                    ToolTip="Buscar" ValidationGroup="buscar" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td width="50">
                                &nbsp;
                            </td>
                            <td width="70">
                                &nbsp;
                            </td>
                            <td width="30">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="250">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td width="70">
                                <telerik:RadNumericTextBox ID="txtClienteId" runat="server" Width="70px" Enabled="False">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtCliente" runat="server" Width="300px" Enabled="False">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Importe"></asp:Label>
                            </td>
                            <td width="70">
                                <telerik:RadNumericTextBox ID="txtImporte" runat="server" Width="70px" Enabled="False">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Saldo"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtSaldo" runat="server" Width="70px" Enabled="False">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="IVA"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIva" runat="server" Width="70px" Enabled="False">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Estatus"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtEstatus" runat="server" Width="150px" Enabled="False">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Total"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="70px" Enabled="False">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Fecha"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha" runat="server" Width="100px" Culture="es-MX"
                                    EnableTyping="False" Enabled="False">
                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                                        ReadOnly="True">
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled aspNetDisabled rcCalPopup"
                                        Enabled="False"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label1" runat="server" Text="Folio Fiscal"></asp:Label></td>
                            <td colspan ="4">
                                <telerik:RadTextBox ID="txtFolioFiscal" runat="server" Width="280px" Enabled="False">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="# Documento"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtDocumentoNumero" runat="server" Width="70px" Enabled="False">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
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
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgDocumentos" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rgDocumentos_NeedDataSource" PageSize="15" AllowPaging="True"
                                    OnPageIndexChanged="rgDocumentos_PageIndexChanged">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Cd_Externo" HeaderText="Centro ejecutor" UniqueName="Cd_Externo">
                                                <HeaderStyle Width="150px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_Nombre" HeaderText="Usuario" UniqueName="U_Nombre">
                                                <HeaderStyle Width="150px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Doc_TipoMov" HeaderText="Tipo mov." UniqueName="Doc_TipoMov">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Doc" HeaderText="Número" UniqueName="Id_Doc">
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Doc_Fecha" HeaderText="Fecha" UniqueName="Doc_Fecha"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Doc_Importe" HeaderText="Importe" UniqueName="Doc_Importe"
                                                DataFormatString="{0:N2}">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Doc_EstatusStr" HeaderText="Estatus" UniqueName="Doc_EstatusStr">
                                                <HeaderStyle Width="150px" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                     <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgLogDocumento" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rgLogDocumento_NeedDataSource" PageSize="15" AllowPaging="True"
                                    OnPageIndexChanged="rgLogDocumento_PageIndexChanged">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                                        <Columns>                                           
                                            <telerik:GridBoundColumn DataField="U_Nombre" HeaderText="Usuario" UniqueName="U_Nombre">
                                                <HeaderStyle Width="310px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_Actividad" HeaderText="Actividad" UniqueName="U_Actividad">
                                                <HeaderStyle Width="310px" />
                                            </telerik:GridBoundColumn>                                           
                                            <telerik:GridBoundColumn DataField="Doc_Fecha" HeaderText="Fecha" UniqueName="Doc_Fecha"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>                                            
                                            <telerik:GridBoundColumn DataField="Id_Relacion" HeaderText="Relación" UniqueName="Id_Relacion">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

﻿<%@ Page Title="Facturas en entrega" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProFactura_EntregaCobranza.aspx.cs" Inherits="SIANWEB.ProFactura_EntregaCobranza" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls> 
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFactura">
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
                <td style="text-align: right; width: 150px">
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
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td width="70">
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td width="50">
                                &nbsp;
                            </td>
                            <td width="90">
                                &nbsp;
                            </td>
                            <td width="50">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblNombre" runat="server" Text="Nombre del cliente"></asp:Label>
                            </td>
                            <td colspan="5">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="300px"
                                    MaxLength="140">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblClienteini" runat="server" Text="Cliente inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtClienteini" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 70px">
                                <asp:Label ID="LblClientefin" runat="server" Text="Cliente final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtClientefin" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblFechaini" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechaini" runat="server" Width="155px">
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
                                &nbsp;
                            </td>
                            <td style="width: 65px">
                                <asp:Label ID="LblFechafin" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechafin" runat="server" Width="155px">
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
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
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
                            <td style="width: 70px">
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
                    <asp:Panel ID="Panel1" runat="server" Width="950px">
                        <telerik:RadGrid ID="rgFactura" runat="server" AutoGenerateColumns="False" Width="900px"
                            GridLines="None" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="rgFactura_ItemDataBound"
                            OnItemCommand="rgFactura_ItemCommand" OnPageIndexChanged="rgFactura_PageIndexChanged"
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                            <ExportSettings ExportOnlyData="True" FileName="Entrega_cobranza" 
                                HideStructureColumns="True" IgnorePaging="True" OpenInNewWindow="True">
                            </ExportSettings>
                            <MasterTableView ClientDataKeyNames="Numero" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <CommandItemSettings ExportToExcelText="Exportar a Excel" ExportToPdfText="Export to Pdf"
                                    ShowAddNewRecordButton="False" ShowExportToExcelButton="True" ShowRefreshButton="False" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Fac" HeaderText="Id_Fac" UniqueName="Id_Fac"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Numero" HeaderText="Factura" UniqueName="Numero">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha de entrega" UniqueName="Fecha"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fecha2" HeaderText="Fecha2" UniqueName="Fecha2"
                                        DataFormatString="{0:dd/MM/yyyy}" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Pedido" HeaderText="Pedido" UniqueName="Pedido">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Num_Cliente" HeaderText="Núm. cte." UniqueName="Num_Cliente">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="Cliente">
                                        <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Recibido" ConfirmDialogType="RadWindow"
                                        ConfirmText="¿Desea confirmar la recepción de la factura <b>[[ID]]</b> ? <br />"
                                        Text="Autorizar recepción" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
                                        UniqueName="Autorizar" Visible="true" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                        ButtonCssClass="aceptar">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="HF_ID" runat="server" />
                    <asp:HiddenField ID="HF_PED" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClienteini.ClientID %>'));
                LimpiarTextBox($find('<%= txtClientefin.ClientID %>'));
                LimpiarTextBox($find('<%= txtNombre.ClientID %>'));
                LimpiarDatePicker($find('<%= dpFechaini.ClientID %>'));
                LimpiarDatePicker($find('<%= dpFechafin.ClientID %>'));
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {

                    args.set_enableAjax(false);
                }
            }       
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapPagosElectronicos_Listado.aspx.cs" Inherits="SIANWEB.CapPagosElectronicos_Listado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="./Styles/Toolbar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All" />
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" PersistenceMode="Session"
            Skin="Outlook">
        </telerik:RadSkinManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <%-- Factura (Impresion PDF) --%>
                <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                    Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                    Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                    OnClientClose="refreshGrid" ReloadOnShow="true">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

        <telerik:RadGrid ID="rgPagoElectronico" runat="server" AutoGenerateColumns="False" GridLines="None"
            OnNeedDataSource="rgPagoElectronico_NeedDataSource" OnItemCommand="rgPagoElectronico_ItemCommand" OnPageIndexChanged="rgPagoElectronico_PageIndexChanged"
            PageSize="20" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
             ShowFooter="True" EnableViewState = "true">
             <ClientSettings AllowGroupExpandCollapse="false"></ClientSettings>

            <MasterTableView ShowGroupFooter="true" GroupLoadMode="Client" >
                <Columns>
                    <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"  Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd" Visible="false"></telerik:GridBoundColumn>
                    
                    <telerik:GridBoundColumn DataField="PagElec_ConSoporte" HeaderText="Con/Sin" UniqueName="PagElec_ConSoporte"><HeaderStyle Width="50" /></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Id_PagElec" UniqueName="Id_PagElec" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="id_RowFactura" HeaderText="#" UniqueName="id_RowFactura"><HeaderStyle Width="20" /></telerik:GridBoundColumn>
                  
                    <telerik:GridTemplateColumn DataField="PagElec_XMLStream" HeaderText="Emisor" UniqueName="PagElec_XMLStream">
                        <ItemTemplate>
                            <%#ObtenerNombreAcredor((byte[])DataBinder.Eval(Container.DataItem, "PagElec_XMLStream")) %>
                        </ItemTemplate>
                        <HeaderStyle Width="160" />
                    </telerik:GridTemplateColumn>
                      <telerik:GridBoundColumn DataField="PagElec_Cuenta" HeaderText="Número" UniqueName="PagElec_Cuenta"  Visible="false"><HeaderStyle Width="30"/></telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="PagElec_Cc" HeaderText="C.C." UniqueName="PagElec_Cc"  Visible="true"><HeaderStyle Width="30"/></telerik:GridBoundColumn>
                       <telerik:GridBoundColumn DataField="PagElec_SubCuenta" HeaderText="Subcta" UniqueName="PagElec_SubCuenta"  Visible="true"><HeaderStyle Width="30"/></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PagElec_SubSubCuenta" HeaderText="Sub Subcta" UniqueName="PagElec_SubCuenta"  Visible="true"><HeaderStyle Width="30"/></telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="FechaFactura" HeaderText="Fecha Requiere" DataFormatString="{0:dd/MM/yy}" UniqueName="FechaFactura"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Serie" HeaderText="Serie" UniqueName="Serie"  Visible="true"><HeaderStyle Width="80"/></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Folio" HeaderText="Folio" UniqueName="Folio"  Visible="true"><HeaderStyle Width="80"/></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Aggregate="Sum" DataField="Importe" HeaderText="Importe" DataFormatString="{0:C}" UniqueName="Importe" FooterText="Total: " ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px" UniqueName="XML">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png" CssClass="edit" ToolTip="Descargar" CommandName="XML" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                        UniqueName="PDF">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                        ConfirmText="¿Desea cancelar la solicitud?" Text="Cancelar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                        ConfirmDialogWidth="350px" Visible="false">
                        <HeaderStyle Width="30px" />
                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                    </telerik:GridButtonColumn>
                </Columns>
               <%-- JFCV 19 NOV 2016 agrupar por tipo comprobante --%>
                   <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Tipo" FieldName='PagElec_ConSoporte' HeaderValueSeparator=": ">
                                                </telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="PagElec_ConSoporte" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>

            </MasterTableView>
            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                ShowPagerText="True" PageButtonCount="3" />
        </telerik:RadGrid>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow)
                        oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                    else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                    return oWindow;
                }
                //Cierra la venata actual y regresa el foco a la ventana padre
                function CloseWindow() {
                    GetRadWindow().Close();
                }
                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind(param) {
                    //debugger;
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.ClienteSeleccionado(param);
                }

                function refreshGrid() {
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    ajaxManager.ajaxRequest('RebindGrid');
                }

                function abrirArchivo(pagina) {
                    var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                    window.open(pagina, '', opciones);
                }

                function AbrirFacturaPDF(WebURL) {
                    var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                    oWnd.set_showOnTopWhenMaximized(true);
                    oWnd.center();
                }
            </script>
        </telerik:RadCodeBlock>
    </div>
    </form>
</body>
</html>
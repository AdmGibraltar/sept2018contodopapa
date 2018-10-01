<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentanaVersion_Buscar.aspx.cs"
    Inherits="SIANWEB.VentanaVersion_Buscar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                <telerik:AjaxSetting AjaxControlID="btnBuscar1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <table style="font-family: verdana; font-size: 8pt" runat="server">
            <tr>               
                <td>
                    &nbsp;
                </td>                
            </tr>
        </table>
        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            AllowFilteringByColumn="False" MasterTableView-NoMasterRecordsText="No se encontraron registros."
            OnNeedDataSource="RadGrid1_NeedDataSource" OnPageIndexChanged="RadGrid1_PageIndexChanged"
            GridLines="None" OnItemCommand="RadGrid1_ItemCommand">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="False" TableLayout="Auto" AllowMultiColumnSorting="False"
                AllowNaturalSort="true" AllowSorting="true">
                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="Id_Acs" UniqueName="Id_Acs" HeaderText="Id_Acs" SortExpression="Id_Acs"
                        CurrentFilterFunction="Contains" ShowFilterIcon="False" FilterControlWidth="50px"
                        HeaderTooltip="Introduzca una clave para su búsqueda">
                        <HeaderStyle Width="70px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Acs_Version" Visible="True" UniqueName="Acs_Version"
                        HeaderText="Versión" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="300px"
                        SortExpression="Descripcion" CurrentFilterFunction="Contains" ShowFilterIcon="False"
                        HeaderTooltip="Introduzca una descripción para su búsqueda">
                        <HeaderStyle HorizontalAlign="Center" Width="450px"></HeaderStyle>
                        <ItemStyle></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Acs_Fecha" UniqueName="Acs_Fecha" HeaderText="Fecha"
                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"  DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FechaInicioVersion" UniqueName="Acs_Fecha" HeaderText="Fecha Inicio"
                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                        <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FechaFinVersion" UniqueName="Acs_Fecha" HeaderText="Fecha Fin"
                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"  >
                        <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn CommandName="Select" HeaderText="" Text="Seleccionar">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
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
                    GetRadWindow().BrowserWindow.AbrirVentana_Acys(param,0);
                }
            </script>
        </telerik:RadCodeBlock>
    </div>
    </form>
</body>
</html>

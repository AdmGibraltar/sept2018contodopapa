<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentanaVersion.aspx.cs" Inherits="SIANWEB.VentanaVersion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <table id="Table1" style="font-family: verdana; font-size: 8pt" runat="server">
            <tr>               
                <td>
                    &nbsp;
                </td>                
            </tr>
        </table>
        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            AllowFilteringByColumn="False" MasterTableView-NoMasterRecordsText="No se encontraron registros."
            OnNeedDataSource="RadGrid1_NeedDataSource"  >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="False" TableLayout="Auto" AllowMultiColumnSorting="False"
                AllowNaturalSort="true" AllowSorting="true">
                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                <Columns>
                    
                    <telerik:GridBoundColumn DataField="Dll_Nombre" Visible="True" UniqueName="Dll_Nombre"
                        HeaderText="Archivo" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="60px"
                        SortExpression="Descripcion" CurrentFilterFunction="Contains" ShowFilterIcon="False">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Num_Version" UniqueName="Num_Version" HeaderText="Version" SortExpression="Num_Version"
                        CurrentFilterFunction="Contains" ShowFilterIcon="False" FilterControlWidth="100px"  >
                        <HeaderStyle Width="100px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Dll_Fecha" UniqueName="Dll_Fecha" HeaderText="Fecha"
                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"  DataFormatString="{0:yyyy-MM-dd hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </telerik:GridBoundColumn>
                  
                </Columns>
            </MasterTableView>
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
                    GetRadWindow().BrowserWindow.AbrirVentana_Acys(param, 0);
                }
            </script>
        </telerik:RadCodeBlock>
    </div>
    </form>
</body>
</html>

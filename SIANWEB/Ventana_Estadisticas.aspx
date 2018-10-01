<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ventana_Estadisticas.aspx.cs"
    Inherits="SIANWEB.Ventana_Estadisticas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family: verdana; font-size: 8pt">
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
                <telerik:AjaxSetting AjaxControlID="CmbEn">
                    <updatedcontrols>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                            LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    </updatedcontrols>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Cliente"></asp:Label>
                </td>
                <td>
                    <telerik:RadNumericTextBox runat="server" ID="txtId_Cte" Enabled="False" Width="50px">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtCte" Enabled="False" Width="300px">
                    </telerik:RadTextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="En"></asp:Label>
                </td>
                <td colspan="2">
                    <telerik:RadComboBox ID="CmbEn" MaxHeight="300px" runat="server" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="CmbEn_SelectedIndexChanged">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Unidades" Value="0" />
                            <telerik:RadComboBoxItem runat="server" Text="Pesos" Value="1" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
             <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Núm. producto"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="RadNumericTextBox1" Width="50px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Producto"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadTextBox runat="server" ID="RadTextBox1" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                    </td>
                </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        AllowFilteringByColumn="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnNeedDataSource="RadGrid1_NeedDataSource" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                        GridLines="None" OnItemCommand="RadGrid1_ItemCommand">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="false" TableLayout="Auto" AllowMultiColumnSorting="False"
                            AllowNaturalSort="true" AllowSorting="true">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="IdStr2" UniqueName="Id2" HeaderText="Territorio"
                                    SortExpression="Territorio" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="False" FilterControlWidth="50px" HeaderTooltip="Introduzca una clave para su búsqueda">
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IdStr" UniqueName="Id" HeaderText="Clave" SortExpression="Clave"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="False"
                                    FilterControlWidth="50px" HeaderTooltip="Introduzca una clave para su búsqueda">
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Descripcion" Visible="True" UniqueName="Descripcion"
                                    HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="300px"
                                    SortExpression="Descripcion" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="False" HeaderTooltip="Introduzca una descripción para su búsqueda">
                                    <HeaderStyle HorizontalAlign="Center" Width="450px"></HeaderStyle>
                                    <ItemStyle></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ValorDoble" UniqueName="ValorDoble1" HeaderText=""
                                    HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Display="true" DataFormatString="{0:N2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ValorDoble2" UniqueName="ValorDoble2" HeaderText=""
                                    HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Display="true" DataFormatString="{0:N2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ValorDoble3" UniqueName="ValorDoble3" HeaderText=""
                                    HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Display="true" DataFormatString="{0:N2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ValorInt3" UniqueName="ValorEntero3" HeaderText=""
                                    HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Display="false" DataFormatString="{0:N0}">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ValorInt2" UniqueName="ValorEntero2" HeaderText=""
                                    HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Display="false" DataFormatString="{0:N0}">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ValorInt" UniqueName="ValorEntero1" HeaderText=""
                                    HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Display="false" DataFormatString="{0:N0}">
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
                </td>
            </tr>
        </table>
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
            </script>
        </telerik:RadCodeBlock>
    </div>
    </form>
</body>
</html>

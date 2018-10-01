<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ventana_Indicadores.aspx.cs"
    Inherits="SIANWEB.Ventana_Indicadores" %>

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
        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div runat="server" id="divPrincipal" style="font-family: verdana; font-size: 8pt">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Núm. producto"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtId_Cte" Width="50px">
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
                        <asp:Label ID="Label2" runat="server" Text="Producto"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadTextBox runat="server" ID="txtCte" Width="300px">
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
                            GridLines="None" OnItemCommand="RadGrid1_ItemCommand" OnSelectedIndexChanged="RadGrid1_SelectedIndexChanged">
                            <GroupingSettings CaseSensitive="false" />
                            <ClientSettings EnablePostBackOnRowClick="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowFilteringByColumn="false" TableLayout="Auto" AllowMultiColumnSorting="False"
                                AllowNaturalSort="true" AllowSorting="true">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridNumericColumn DataField="Id_Prd" UniqueName="Id_Prd" HeaderText="Núm."
                                        SortExpression="Clave" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"
                                        ShowFilterIcon="False" FilterControlWidth="50px" HeaderTooltip="Introduzca una clave para su búsqueda">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridBoundColumn DataField="Prd_Descripcion" Visible="True" UniqueName="Prd_Descripcion"
                                        HeaderText="Producto" HeaderStyle-HorizontalAlign="Center" FilterControlWidth="300px"
                                        SortExpression="Prd_Descripcion" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="False" HeaderTooltip="Introduzca una descripción para su búsqueda">
                                        <HeaderStyle HorizontalAlign="Center" Width="450px"></HeaderStyle>
                                        <ItemStyle></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <%-- <telerik:GridTemplateColumn DataField="Prd_Descripcion" UniqueName="Prd_Descripcion" HeaderText="Producto"
                                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="False"                                       
                                        AllowFiltering="true" FilterControlWidth="300px" HeaderTooltip="Introduzca una descripción para su búsqueda">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Prd_Descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="98%" Text='<%# Eval("Prd_Descripcion") %>'
                                                ReadOnly="true">
                                            </telerik:RadTextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="450px" />
                                    </telerik:GridTemplateColumn>--%>
                                    <telerik:GridBoundColumn DataField="Prd_Presentacion" UniqueName="Presentacion" HeaderText="Presentación"
                                        AllowFiltering="false" Display="true">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Uni_Descripcion" UniqueName="Unidad" HeaderText="Unidad"
                                        AllowFiltering="false" Display="true">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Prd_InvInicial" UniqueName="Prd_InvInicial" HeaderText="Prd_InvInicial"
                                        AllowFiltering="false" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Prd_InvFinal" UniqueName="Prd_InvFinal" HeaderText="Prd_InvFinal"
                                        AllowFiltering="false" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Prd_Fisico" UniqueName="Prd_Fisico" HeaderText="Prd_Fisico"
                                        AllowFiltering="false" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Prd_Ordenado" UniqueName="Prd_Ordenado" HeaderText="Prd_Ordenado"
                                        AllowFiltering="false" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Prd_Asignado" UniqueName="Prd_Asignado" HeaderText="Prd_Asignado"
                                        AllowFiltering="false" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Prd_Transito" UniqueName="Prd_Transito" HeaderText="Prd_Transito"
                                        AllowFiltering="false" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="Select" HeaderText="" Text="Seleccionar">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Inventario inicial"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtInicial" Enabled="False" Width="50px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Asignado"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtAsignado" Enabled="False" Width="50px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Tránsito"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtTransito" Enabled="False" Width="50px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Inventario final"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtFinal" Enabled="False" Width="50px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Ordenado"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtOrdenado" Enabled="False" Width="50px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
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

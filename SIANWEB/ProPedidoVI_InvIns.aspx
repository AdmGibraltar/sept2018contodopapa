<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="ProPedidoVI_InvIns.aspx.cs" Inherits="SIANWEB.ProPedidoVI_InvIns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    &nbsp;
                </td>
                <td width="150px">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Fecha de factura"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha" runat="server" Width="100px" AutoPostBack="True">
                                    <Calendar ID="Calendar2" runat="server">
                                        <ClientEvents OnDateClick="Calendar_Click" />
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput AutoPostBack="True">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Orden de compra"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtOrden" runat="server" Enabled="False" Width="70px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Los siguientes productos no tienen suficiente disponible"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnItemDataBound="RadGrid1_ItemDataBound"
                                    OnItemCreated="RadGrid1_ItemCreated" Height="180px">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros." DataKeyNames="Id_Prd">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Código" UniqueName="Id_Prd">
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion">
                                                <HeaderStyle Width="300px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Cantidad" HeaderText="Cant. capt." UniqueName="Prd_Cantidad">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Asignado" HeaderText="Asignado" UniqueName="Prd_Asignado">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_InvFinal" HeaderText="Inv. final" UniqueName="Prd_InvFinal">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Disponible" HeaderText="Disponible" UniqueName="Prd_Disponible">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="" UniqueName="Equivalencias" Visible="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="hlEquivalencias" runat="server">Reemplazar</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle Width="90px" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="100px">
                                        </Scrolling>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="En los siguientes códigos no se está respetando los precios convenidos "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="RadGrid2" runat="server" OnNeedDataSource="RadGrid2_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" Height="150px">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Código" UniqueName="Id_Prd">
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion">
                                                <HeaderStyle Width="300px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Precio_Convenido" HeaderText="Precio convenido"
                                                UniqueName="Precio_Convenido" DataFormatString="{0:N2}">
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Precio_Captado" HeaderText="Precio captado" UniqueName="Precio_Captado"
                                                DataFormatString="{0:N2}">
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="100px">
                                        </Scrolling>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="¿Desea hacer alguna corrección?"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="SI" Width="80px" OnClick="Button1_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="NO" Width="80px" OnClick="Button2_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            /*CERRAR VENTANA*/
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
                GetRadWindow().BrowserWindow.confirmCallBackFnPrecio(false);
            }

            function CloseAndContinue() {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.confirmCallBackFnPrecio(true);
            }


            function popup2(Id_Prd, Id_Acs, Id_Cte) {
                var oWnd = radopen("Ventana_Equivalencias.aspx?Id_Prd=" + Id_Prd + "&Id_Acs=" + Id_Acs + "&Id_Cte=" + Id_Cte, "AbrirVentana_PrdAcys");
                oWnd.center();
            }

            function CambioProductos() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

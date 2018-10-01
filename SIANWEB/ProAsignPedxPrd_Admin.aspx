<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProAsignPedxPrd_Admin.aspx.cs" Inherits="SIANWEB.ProAsignPedxPrd_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <div id="divPrincipal" runat="server">
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
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table style="font-family: Verdana; font-size: 8pt">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Producto" />
                                        </td>
                                        <td colspan="6">
                                            <telerik:radtextbox onpaste="return false" id="txtProducto" runat="server" width="328px">
                                            </telerik:radtextbox>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Producto inicial" />
                                        </td>
                                        <td>
                                            <telerik:radnumerictextbox id="txtProducto1" runat="server" width="70px" minvalue="1"
                                                maxlength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:radnumerictextbox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Producto final" />
                                        </td>
                                        <td>
                                            <telerik:radnumerictextbox id="txtProducto2" runat="server" width="70px" minvalue="1"
                                                maxlength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:radnumerictextbox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                                ToolTip="Buscar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td width="10">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td width="100">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <telerik:radgrid id="rgPedido" runat="server" autogeneratecolumns="False" gridlines="None"
                                    onneeddatasource="RadGrid1_NeedDataSource" enablelinqexpressions="False" pagesize="15"
                                    allowpaging="true" mastertableview-nomasterrecordstext="No se encontraron registros."
                                    onpageindexchanged="rgPedido_PageIndexChanged" onitemcommand="rgPedido_ItemCommand">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView>
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Prod." UniqueName="Id_Prd">
                                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Descripción" UniqueName="Prd_Descripcion">
                                                <ItemStyle HorizontalAlign="Left" Width="320px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Asignado" HeaderText="Asig." UniqueName="Prd_Asignado">
                                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_InvFinal" HeaderText="Inventario" UniqueName="Prd_InvFinal">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                          
                                            <telerik:GridBoundColumn DataField="Prd_Presentacion" HeaderText="Presen." UniqueName="Prd_Presentacion">
                                                <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Ordenado" HeaderText="Ord." UniqueName="Prd_Ordenado">
                                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Sobrante" HeaderText="Disponible" UniqueName="Prd_Sobrante">
                                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Pendiente" HeaderText="Pendiente" UniqueName="Prd_Pendiente">
                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Asignar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Asignar" CommandName="Asignar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                        <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                                </telerik:radgrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function AbrirVentana_ProAsignPedxPrd_Asignar(Id_Prd, Prd_Nom) {
                AbrirVentana_AsigPrdxPed(Id_Prd, Prd_Nom);
                return false;
            }

            function AbrirVentana_ProAsignPedxPrd(Id_Prd, Prd_Nom) {

                var oWnd = radopen("ProAsignPedxPrd.aspx?Id_Prd=" + Id_Prd + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_ProAsignPedxPrd");
                oWnd.center();
                return false;
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        </script>
    </telerik:radcodeblock>
</asp:Content>

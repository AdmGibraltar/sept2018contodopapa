<%@ Page Title="Seguimiento de entrega de producto a sucursal" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProSeguimientoPrd.aspx.cs" Inherits="SIANWEB.ProSegEntProductoSucursal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgProductoSucursal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgProductoSucursal" LoadingPanelID="RadAjaxLoadingPanel1"
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
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
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
                            </td>
                            <td width="90">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="210">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Núm. producto
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtNum" runat="server" Width="90px" MinValue="1" MaxLenght="9"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" ValidationGroup="buscar" />
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <%--      <tr>
                            <td>
                                Producto
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtProducto" runat="server" Width="300px"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
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
                        </tr>--%>
                    </table>
                    <br />
                    <telerik:RadGrid ID="rgProductoSucursal" runat="server" AutoGenerateColumns="False"
                        GridLines="None" EnableLinqExpressions="False" PageSize="15" AllowPaging="True"
                        OnNeedDataSource="RadGrid1_NeedDataSource" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnPageIndexChanged="rgProductoSucursal_PageIndexChanged" OnItemCommand="rgProductoSucursal_ItemCommand">
                        <MasterTableView>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_Prd" HeaderText="Núm." UniqueName="ID_Prd">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="300px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Presentacion" HeaderText="Presentación" UniqueName="Prd_Presentacion">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_InvInicial" HeaderText="Inv. Inicial" UniqueName="Prd_InvInicial">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Asignado" HeaderText="Asignado" UniqueName="Prd_Asignado">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Transito" HeaderText="Transito" UniqueName="Prd_Transito">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_InvFinal" HeaderText="Inv. Final" UniqueName="Prd_InvFinal">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Ordenado" HeaderText="Ordenado" UniqueName="Prd_Ordenado">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TieneComentarios" HeaderText="Tiene Comentarios"
                                    UniqueName="TieneComentarios">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Agregar observaciones" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="add16" ToolTip="Agregar observaciones" CommandName="agregar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" /> 
                        </ClientSettings>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" ShowPagerText="True"
                            PageButtonCount="3" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >." />
                    </telerik:RadGrid>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //debugger;
            // ---------------------
            // Variables de permiso
            // ---------------------

            function AbrirVentana_Observaciones(Id) {
                //debugger;
                AbrirVentana(Id);
                return false;
            }

            function AbrirVentana(Id) {
                //debugger;
                var oWnd = radopen("ProSeguimientoPrd_Obs.aspx?Id=" + Id
                  + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>"
                  + "&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>"
                  , "AbrirVentana_Observaciones");
                oWnd.center();
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

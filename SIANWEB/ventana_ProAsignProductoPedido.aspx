<%@ Page Title="Asign. de producto por pedido" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="ventana_ProAsignProductoPedido.aspx.cs" Inherits="SIANWEB.ventana_ProAsignProductoPedido" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Aceptar">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4" width="290">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="50">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Pedido
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox onpaste="return false" ID="txtPedido" runat="server" Width="70px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cliente
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox onpaste="return false" ID="txtCliente1" runat="server" Width="70px">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox onpaste="return false" ID="txtCliente2" runat="server" Width="207px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Producto
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="285px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Producto inicial
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtCliente" runat="server" Width="70px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Producto final
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtCliente0" runat="server" Width="70px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgAsignacion" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="RadGrid1_NeedDataSource">
                        <MasterTableView>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Pedido" HeaderText="Terr." UniqueName="column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fecha" HeaderText="Prod." UniqueName="column1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Terr" HeaderText="Descripción" UniqueName="column3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte" HeaderText="Ord." UniqueName="column4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nom" HeaderText="Disp. ord." UniqueName="column5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Credito" HeaderText="Asig." UniqueName="column6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Credito" HeaderText="Faltante" UniqueName="column6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Credito" HeaderText="Existencia" UniqueName="column6">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

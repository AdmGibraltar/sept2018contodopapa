<%@ Page Title="Administración de pedidos no captados" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProVentInst_AdminPedidoNoCaptado.aspx.cs"
    Inherits="SIANWEB.ProVentInst_AdminPedidoNoCaptado" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Nuevo">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton Text="Modificar">
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
                            </td>
                            <td width="70">
                            </td>
                            <td width="10">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td width="10">
                            </td>
                            <td width="80">
                            </td>
                            <td width="50">
                            </td>
                            <td width="70">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cliente
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" Width="70px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Territorio
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" Width="70px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Usuario
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox1" runat="server" Width="218px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
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
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None"
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
                                <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Semana" HeaderText="Semana entrega" UniqueName="column1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Cod. cte." UniqueName="column2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="column3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Terr" HeaderText="Terr." UniqueName="column4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Causa" HeaderText="Causa" UniqueName="column5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="column6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="vta" HeaderText="Vta. inst." UniqueName="column7">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<%@ Page Title="Captura de órdenes de compra de venta instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProOrdCompra_Gen.aspx.cs" Inherits="SIANWEB.ProOrdCompra_Gen" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Aceptar">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton runat="server" Text="Exportar">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgProveedor" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="RadGrid1_NeedDataSource" Width="400px" Height="150px">
                                    <MasterTableView>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" UniqueName="column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Proveedor" HeaderText="Proveedor" UniqueName="column1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Seleccion" UniqueName="column12">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Seleccion")) %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                            <td valign="bottom">
                                <asp:Button ID="btnTodo" runat="server" Text="Todo" />
                            </td>
                            <td valign="bottom">
                                <asp:Button ID="btnInvertir" runat="server" Text="Invertir" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <telerik:RadGrid ID="rgOrden" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="RadGrid2_NeedDataSource" Width="800px" Height="200px">
                        <MasterTableView>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" UniqueName="column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Producto" HeaderText="Producto" UniqueName="column1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Presen" HeaderText="Presen." UniqueName="column2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Unc" HeaderText="Unc" UniqueName="column3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Vta" HeaderText="Vta. inst." UniqueName="column4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="prom" HeaderText="Ve. prom." UniqueName="column5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="var" HeaderText="Ve. var." UniqueName="column6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="disp" HeaderText="Inv. disp" UniqueName="column7">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="mes1" HeaderText="Vta. mes1" UniqueName="column8">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="mes2" HeaderText="Vta. mes2" UniqueName="column9">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="mes3" HeaderText="Vta. mes3" UniqueName="column10">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="sug" HeaderText="OC. sug" UniqueName="column11">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Sel." UniqueName="column12">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sel")) %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="sol" HeaderText="OC sol" UniqueName="column13">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="uni" HeaderText="Cto. uni" UniqueName="column14">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" UniqueName="column15">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

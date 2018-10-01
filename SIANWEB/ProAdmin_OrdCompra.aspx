<%@ Page Title="Administrador de ordenes de compra de venta instalada" Language="C#"
    MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true" CodeBehind="ProAdmin_OrdCompra.aspx.cs"
    Inherits="SIANWEB.ProAdmin_OrdCompra" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Modificar">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton runat="server" Text="Imprimir">
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
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Proveedor</td>
                            <td colspan="7">
                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox1" Runat="server" Width="405px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Proveedor</td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox1" Runat="server" Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Final</td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox2" Runat="server" Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Fecha</td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker2" Runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Fecha</td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker1" Runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Estatus</td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox1" Runat="server" Width="100px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" onneeddatasource="RadGrid1_NeedDataSource">
<MasterTableView>
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
    <Columns>
        <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" 
            UniqueName="column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Orden" HeaderText="Orden" 
            UniqueName="column1">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" 
            UniqueName="column2">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Num" HeaderText="Num." UniqueName="column3">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Proveedor" HeaderText="Proveedor" 
            UniqueName="column4">
        </telerik:GridBoundColumn>
    </Columns>
</MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

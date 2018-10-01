<%@ Page Title="Asiganción manual detalle" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProPedido_ManualDetalle.aspx.cs" Inherits="SIANWEB.ProPedido_AsignacionManual" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
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
                            <td width="70">
                            </td>
                            <td width="10">
                            </td>
                            <td>
                            </td>
                            <td width="100">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Inventario
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" Width="70px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Asignado
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" Width="70px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                SAP
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="100px">
                                </telerik:RadComboBox>
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
                                <telerik:GridBoundColumn DataField="Pedido" HeaderText="Pedido" UniqueName="column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="column1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Sap" HeaderText="SAP" UniqueName="column2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Terr" HeaderText="Terr." UniqueName="column3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte" HeaderText="Cte" UniqueName="column4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nom" HeaderText="Nom. cte." UniqueName="column5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Credito" HeaderText="Crédito" UniqueName="column6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ord" HeaderText="Ord." UniqueName="column7">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Disp" HeaderText="Disp." UniqueName="column8">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Asig" HeaderText="Asig." UniqueName="column9">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Faltante" HeaderText="Faltante" UniqueName="column10">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

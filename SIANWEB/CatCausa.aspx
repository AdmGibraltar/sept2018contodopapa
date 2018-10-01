<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatCausa.aspx.cs" Inherits="SIANWEB.CatCausa" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="80">
                                &nbsp;
                            </td>
                            <td width="70">
                                &nbsp;
                            </td>
                            <td width="250">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Código
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox3" runat="server" Width="70px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Descripción
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox1" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None"
                        Height="200px" OnNeedDataSource="RadGrid1_NeedDataSource" Width="600px">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Empresa" HeaderText="Empresa" UniqueName="column">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cau" HeaderText="Cau" UniqueName="column1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="desc" HeaderText="Cau desc" UniqueName="column2">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

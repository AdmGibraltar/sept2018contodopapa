<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true" CodeBehind="CatUsuario_Lista.aspx.cs" Inherits="SIANWEB.CatUsuario_Lista" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<div>
    <table>
        <tr>
            <td>
            </td>
            <td>
            <table>
            <tr>
            <td height="10">&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            </tr>
            <tr>
            <td>&nbsp;</td>
            <td>Nombre</td>
            <td>
                <telerik:RadTextBox onpaste="return false" ID="RadTextBox1" runat="server" Width="300px">
                </telerik:RadTextBox></td>
            </tr>
            <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            </tr>
            </table>
                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" 
                    GridLines="None" onneeddatasource="RadGrid1_NeedDataSource">
                    <MasterTableView>
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Modulos" HeaderText="Numero" 
                                UniqueName="column">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Usuario" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Derecho")) %>'/>
                            </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

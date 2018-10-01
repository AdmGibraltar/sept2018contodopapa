<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true"
    CodeBehind="CatCliente_Tipo.aspx.cs" Inherits="SIANWEB.CatCliente_Tipo" %>

  
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
                            <td width="50">
                                &nbsp;
                            </td>
                            <td width="250">
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
                    </table>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None"
                        Height="200px" OnNeedDataSource="RadGrid1_NeedDataSource" Width="600px">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" UniqueName="column">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="column1">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CapPago_Dif.aspx.cs" Inherits="SIANWEB.CapPago_Dif" %>
     
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <table style="font-family: verdana; font-size: 8pt">
        <tr>
            <td>
            </td>
            <td>
                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" Height="200px"
                    Width="345px"  MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Ficha" HeaderText="Ficha" UniqueName="Ficha">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Datos generales" UniqueName="Gral">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtGral" runat="server" Text='<%# Bind("Gral") %>'
                                        Width="90px" ReadOnly="true">
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Detalles" UniqueName="Det">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtDet" runat="server" Text='<%# Bind("Det") %>' Width="90px"
                                        ReadOnly="true">
                                        <ClientEvents OnValueChanging="" />
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn HeaderText="" DataField="Valido" UniqueName="Valido">
                                <HeaderStyle Width="30px" />     
                                 <ItemStyle HorizontalAlign="Left" Font-Bold="true" ForeColor="Red" />                                 
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="200px">
                        </Scrolling>
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label1" runat="server" Text="CUIDADO: El detalle de las fichas no cuadra <br />con los datos generales. Favor de hacer <br />los ajustes necesarios."
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

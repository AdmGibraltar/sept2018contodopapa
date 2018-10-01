<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProGenOrdenCompra.aspx.cs" Inherits="SIANWEB.ProGenOrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div id="divPrincipal" runat="server">
        <table>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Semana"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox4" Runat="server" Width="70px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                    <asp:Label ID="Label2" runat="server" Text="Consecutivo orden"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox3" Runat="server" Width="70px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                    <asp:Label ID="Label3" runat="server" Text="Mes"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox1" Runat="server" Width="70px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                    <asp:Label ID="Label4" runat="server" Text="Año"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="RadTextBox2" Runat="server" Width="70px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                    <asp:Label ID="Label5" runat="server" Text="a procesar"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    </div>
</asp:Content>

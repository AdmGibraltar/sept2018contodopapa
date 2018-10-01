<%@ Page Title="Abrir pedido" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProVentInst_AbrirPedido.aspx.cs" Inherits="SIANWEB.ProPedido_Abrir" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Nuevo" CommandName="Nuevo">
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
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Pedido
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<%@ Page Title="Análisis de inventario físico" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true" CodeBehind="Rep_InvInventarioFisico.aspx.cs" Inherits="SIANWEB.Rep_InvInventarioFisico" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
  <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Mostrar">
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
                            <td width="10">
                                </td>
                            <td width="80">
                                &nbsp;</td>
                            <td>
                                </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Producto</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtProductoini" Runat="server" Width="125px">
                                </telerik:RadNumericTextBox>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Inventario</td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox1" Runat="server" Width="130px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" />
                                        <telerik:RadComboBoxItem runat="server" Text="Faltante" Value="Faltante" />
                                        <telerik:RadComboBoxItem runat="server" Text="Sobrante" Value="Sobrante" />
                                        <telerik:RadComboBoxItem runat="server" Text="Todos" Value="Todos" />
                                    </Items>
                                </telerik:RadComboBox>
                                </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Precio</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td>
                                <asp:RadioButton ID="rbAaa" runat="server" GroupName="precio" 
                                    Text="AAA" />
                                </td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td>
                                <asp:RadioButton ID="rbLista" runat="server" GroupName="precio" Text="Lista" />
                                </td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        </table>
                </td>
            </tr>
        </table>
         <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

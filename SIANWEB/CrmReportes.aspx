<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CrmReportes.aspx.cs" Inherits="SIANWEB.CrmReportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="40">
                            </td>
                            <td width="250">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButton ID="rbClientesContacto" runat="server" Text="Reporte de contactos por clientes" />
                                &nbsp;
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButton ID="rbControlProm" runat="server" Text="Reporte de control de promociones" />
                                &nbsp;
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                 
                                 
                                Rango de monto del proyecto 
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                De</td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox1" Runat="server" Width="70px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                A</td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox2" Runat="server" Width="70px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

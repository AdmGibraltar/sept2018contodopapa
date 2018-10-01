<%@ Page Title="Planeación de requerimientos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProPlanRequerimiento.aspx.cs" Inherits="SIANWEB.ProPlanRequerimiento" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
            <Items>
                <telerik:RadToolBarButton Text="Aceptar">
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
                        <td></td>
                        <td width="70">&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td width="10">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td width="150">&nbsp;</td>
                        </tr>
                        <tr>
                        <td>Usuario</td>
                        <td>
                            <telerik:RadNumericTextBox ID="RadNumericTextBox1" Runat="server" Width="70px">
                            </telerik:RadNumericTextBox>
                            </td>
                        <td colspan="5">
                            <telerik:RadTextBox onpaste="return false" ID="RadTextBox1" Runat="server" Width="300px">
                            </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        </tr>
                        <tr>
                        <td>Rango de </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        </tr>
                        <tr>
                        <td>Inicial</td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="RadComboBox1" Runat="server" Width="100px">
                            </telerik:RadComboBox>
                            </td>
                        <td>&nbsp;</td>
                        <td>Final</td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBox4" Runat="server" Width="100px">
                            </telerik:RadComboBox>
                            </td>
                        <td>&nbsp;</td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        </tr>
                        <tr>
                        <td>Rango de</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        </tr>
                        <tr>
                        <td>Inicial</td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="RadComboBox2" Runat="server" Width="100px">
                            </telerik:RadComboBox>
                            </td>
                        <td>&nbsp;</td>
                        <td>Final</td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBox3" Runat="server" Width="100px">
                            </telerik:RadComboBox>
                            </td>
                        <td>&nbsp;</td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

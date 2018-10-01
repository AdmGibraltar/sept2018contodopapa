<%@ Page Title="Listado de notas de credito" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true"
    CodeBehind="CapNotaCredito_Impresion.aspx.cs" Inherits="SIANWEB.CapNotaCredito_Impresion" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
        <Items>
            <telerik:RadToolBarButton Text="Previsualizar" CommandName="Previsualizar">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton Text="Enviar por e-mail" CommandName="Enviar">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton Text="Exportar" CommandName="Exportar">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton Text="Imprimir" CommandName="Imprimir">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>
    <table>
        <tr>
            <td>
            </td>
            <td>
                <table>
                    <tr>
                        <td width="60">
                            Archivo
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="200px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            E-Mail
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="200px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CrmCampania.aspx.cs" Inherits="SIANWEB.CrmCampania" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                Enabled="false" ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <div>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                Nombre de la campaña
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtCampania" runat="server">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                UEN
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbUen" runat="server" Width="130px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Segmento
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbSegmento" runat="server" Width="130px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Aplicaciones&nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Jabon de manos" />
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox5" runat="server" Text="Tratamiento de pisos" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox2" runat="server" Text="Toalla de Manos y papel sanitario" />
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox6" runat="server" Text="Bolsa de Basura" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox3" runat="server" Text="Control de Olores" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox7" runat="server" Text="Wipers" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox4" runat="server" Text="Quimicos del Aseo" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox8" runat="server" Text="Suplementos del Afanador" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

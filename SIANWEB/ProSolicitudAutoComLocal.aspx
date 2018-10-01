f<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProSolicitudAutoComLocal.aspx.cs" Inherits="SIANWEB.ProSolicitudAutoComLocal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <br />
    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenActualiza" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    Solicitud
                </td>
                <td colspan="4">
                    <telerik:RadComboBox ID="cmbSolicitud" runat="server" Width="200px" Filter="Contains"
                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true">
                    </telerik:RadComboBox>
                </td>
                <td rowspan="2">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" />
                    <br />
                    <asp:Button ID="btnQuitar" runat="server" Text="Quitar" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Producto
                </td>
                <td>
                    <telerik:RadNumericTextBox ID="txtProducto" runat="server" Width="70px" MaxLength="9">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbProducto" runat="server" Width="200px" Filter="Contains"
                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true">
                    </telerik:RadComboBox>
                </td>
                <td>
                    Costo
                </td>
                <td>
                    <telerik:RadNumericTextBox ID="txtCosto" runat="server" Width="70px" MaxLength="12">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadGrid ID="rgSolicitud" runat="server" AutoGenerateColumns="False" GridLines="None"
                        PageSize="15" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                        <MasterTableView DataKeyNames="Id_Sol" DataMember="list">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Num." UniqueName="Id" DataField="Id">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Descripción" UniqueName="Descripcion" DataField="Descripcion">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Costo" UniqueName="Costo" DataField="Costo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Estatus" UniqueName="Estatus" DataField="Estatus">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Autoriza" UniqueName="Autoriza" DataField="Autoriza">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

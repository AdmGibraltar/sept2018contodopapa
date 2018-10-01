<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CatPermisoCtrlU.aspx.cs" Inherits="SIANWEB.CatPermisoCtrlU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
 <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridPermisos" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridPermisos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPermisos" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--   <telerik:AjaxSetting AjaxControlID="RadToolbar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" BackgroundPosition="Top">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar runat="server" ID="rtb1" AutoPostBack="true" dir="rtl" Width="100%"
        OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" Owner="rtb1" />
            <telerik:RadToolBarButton ToolTip="Correo" CommandName="mail" CssClass="mail" ImageUrl="Imagenes/blank.png"
                Owner="rtb1" />
            <telerik:RadToolBarButton ToolTip="Imprimir" CommandName="print" CssClass="print"
                ImageUrl="Imagenes/blank.png" Owner="rtb1" />
            <telerik:RadToolBarButton ToolTip="Eliminar" CommandName="delete" CssClass="delete"
                ImageUrl="Imagenes/blank.png" Owner="rtb1" />
            <telerik:RadToolBarButton ToolTip="Regresar" CommandName="undo" CssClass="undo" ImageUrl="Imagenes/blank.png"
                Owner="rtb1" />
            <telerik:RadToolBarButton ToolTip="Guardar" CommandName="save" CssClass="save" ImageUrl="Imagenes/blank.png"
                ValidationGroup="guardar" Owner="rtb1" />
            <telerik:RadToolBarButton ToolTip="Nuevo" CommandName="new" CssClass="new" ImageUrl="Imagenes/blank.png"
                Owner="rtb1" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                Centro de distribucion
            </td>
            <td width="150px">
                <telerik:RadComboBox ID="CmbCentro" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <div runat="server" id="divPrincipal" style="font-family: verdana; font-size: 8pt">
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbUsuario" runat="server" Width="200px" 
                                    LoadingMessage="Cargando..." onclientblur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbUsuario"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                    ValidationGroup="Permisos"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Pantalla"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbPantalla" runat="server" Width="200px" 
                                    Height="400px" LoadingMessage="Cargando..." onclientblur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbPantalla"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                    ValidationGroup="Permisos"></asp:RequiredFieldValidator>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    Style="height: 16px" ValidationGroup="Permisos" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <telerik:RadGrid ID="RadGridPermisos" runat="server" AutoGenerateColumns="False"
            Width="700px" GridLines="None" Visible="False">
            <MasterTableView NoMasterRecordsText="No existen registros que mostrar">
                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                <Columns>
                    <telerik:GridBoundColumn UniqueName="Menu" HeaderText="Control" DataField="Menu"
                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="210px">
                        <HeaderStyle Width="210px" />
                        <ItemStyle HorizontalAlign="Left" Width="210px"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="MenuCve" HeaderText="Menú" DataField="Id_Ctrl"
                        Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="Accesar">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ChkAccesarHeader" Text="Accesar" runat="server" Style="cursor: hand"
                                ToolTip="Seleccionar todos" AutoPostBack="true" OnCheckedChanged="ChkAccesarHeader_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkAccesar" runat="server" Style="cursor: hand" Checked='<%# DataBinder.Eval(Container, "DataItem.PAccesar") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">
                </Scrolling>
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</asp:Content>

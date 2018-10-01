<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatPerfiles.aspx.cs" Inherits="SIANWEB.CatPerfiles" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="C1" ContentPlaceHolderID="CPH" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" BackgroundPosition="Top">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="Panel1" runat="server">
        <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="true" dir="rtl"
            Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" CssClass="mail" ToolTip="Correo" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="save" ToolTip="Guardar" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" ToolTip="Nuevo" CssClass="new" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <input id="HiddenId_TU" type="hidden" runat="server" visible="false" />
                    <input id="HiddenId_Ofi" type="hidden" runat="server" visible="False" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
              <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div>
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table style="font-family: Verdana; font-size: 8pt" align="left">
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Nombre"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox onpaste="return false" ID="txtTipoNombre" runat="server" Width="200px"
                                        MaxLength="80" TabIndex="2">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTipoNombre"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" Style="cursor: hand" TabIndex="3"
                                        Text="Activo" ToolTip="Marcar el tipo de usuario como activo" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkPropia" runat="server" Text="Sólo mostrar información propia" />
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" Width="590px"
                                        AllowPaging="True" PageSize="5" AllowFilteringByColumn="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                        OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemCommand"
                                        OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand"
                                        GridLines="None">
                                        <GroupingSettings CaseSensitive="false" />
                                        <GroupingSettings CaseSensitive="False" />
                                        <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente"
                                            SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
                                        <MasterTableView AllowFilteringByColumn="True" AllowMultiColumnSorting="False" AllowNaturalSort="False"
                                            AllowSorting="false" TableLayout="Auto">
                                            <Columns>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    DataField="TU_Descripcion" FilterControlWidth="200px" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="Nombre" HeaderTooltip="Introduzca un tipo de usuario para su búsqueda"
                                                    ItemStyle-Width="200px" ShowFilterIcon="False" SortExpression="TU_Descripcion"
                                                    UniqueName="Perfil" Visible="True">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="200px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AllowSorting="false" DataField="TU_ActivoStr"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="80px" UniqueName="Activo_string" Visible="True">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TU_Activo" UniqueName="TU_Activo" Visible="False">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_TU" UniqueName="IDPerfil" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Ofi" UniqueName="Id_Ofi" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TU_Propia" UniqueName="TU_Propia" Visible="False">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Modificar" CssClass="edit"
                                                            ImageUrl="~/Imagenes/blank.png" TabIndex="4" ToolTip="Editar tipo de usuario" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente"
                                            SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                            ShowPagerText="True" PageButtonCount="3" />
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <table style="font-family: Verdana; font-size: 8pt; position: relative;" align="left">
                                <tr>
                                    <td colspan="6" align="center ">
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadGrid ID="RadGrid2" AllowPaging="false" ShowFooter="True" runat="server"
                                                        AutoGenerateColumns="False" AllowSorting="false" PageSize="15" GridLines="Horizontal"
                                                        CellPadding="0" AllowMultiRowSelection="false" VirtualItemCount="15">
                                                        <MasterTableView ShowFooter="True">
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="Menu" HeaderText="Menú" DataField="Menu" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="350px">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="MenuCve" HeaderText="Menú" DataField="Sm_Cve"
                                                                    Visible="False">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Id_TU" UniqueName="Id_TU" Visible="False">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Accesar" UniqueName="Accesar" ItemStyle-Width="60px"
                                                                    HeaderStyle-HorizontalAlign="Justify">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="Chk_AccesarAll" runat="server" AutoPostBack="True" Style="cursor: hand"
                                                                            Text="Accesar" TextAlign="Left" TabIndex="5" OnCheckedChanged="chkAccesar_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkAccesar" runat="server" Style="cursor: hand" Checked='<%# DataBinder.Eval(Container, "DataItem.Sp_PAccesar") %>'
                                                                            TabIndex="10" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Grabar" UniqueName="grabar" ItemStyle-Width="60px"
                                                                    HeaderStyle-HorizontalAlign="Justify">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="Chk_GrabarAll" runat="server" AutoPostBack="True" Style="cursor: hand"
                                                                            Text="Grabar" TextAlign="Left" TabIndex="6" OnCheckedChanged="chkGrabar_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkGrabar" runat="server" Style="cursor: hand" Checked='<%# DataBinder.Eval(Container, "DataItem.Sp_PGrabar") %>'
                                                                            TabIndex="10" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Modificar" UniqueName="modificar" ItemStyle-Width="60px"
                                                                    HeaderStyle-HorizontalAlign="Justify">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="Chk_ModificarAll" runat="server" AutoPostBack="True" Style="cursor: hand"
                                                                            Text="Modificar" TextAlign="Left" TabIndex="7" OnCheckedChanged="chkModificar_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkModificar" runat="server" Style="cursor: hand" Checked='<%# DataBinder.Eval(Container, "DataItem.Sp_PModificar") %>'
                                                                            TabIndex="10" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="eliminar" ItemStyle-Width="60px"
                                                                    HeaderStyle-HorizontalAlign="Justify">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="Chk_EliminarAll" runat="server" AutoPostBack="True" Style="cursor: hand"
                                                                            Text="Eliminar" TextAlign="Left" TabIndex="8" OnCheckedChanged="chkEliminar_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkEliminar" runat="server" Style="cursor: hand" Checked='<%# DataBinder.Eval(Container, "DataItem.Sp_PEliminar") %>'
                                                                            TabIndex="10" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Imprimir" UniqueName="imprimir" ItemStyle-Width="60px"
                                                                    HeaderStyle-HorizontalAlign="Justify">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="Chk_ImprimirAll" runat="server" AutoPostBack="True" Style="cursor: hand"
                                                                            Text="Imprimir" TextAlign="Left" TabIndex="9" OnCheckedChanged="chkImprimir_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkImprimir" runat="server" Style="cursor: hand" Checked='<%# DataBinder.Eval(Container, "DataItem.Sp_PImprimir") %>'
                                                                            TabIndex="10" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="Sp_PAccesar" Visible="False" UniqueName="SpTu_PAccesar">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Sp_PGrabar" Visible="False" UniqueName="SpTu_PGrabar">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Sp_PModificar" Visible="False" UniqueName="SpTu_PModificar">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Sp_PEliminar" Visible="False" UniqueName="SpTu_PEliminar">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Sp_PImprimir" Visible="False" UniqueName="SpTu_PImprimir">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>

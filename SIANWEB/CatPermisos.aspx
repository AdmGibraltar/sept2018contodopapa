<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CatPermisos.aspx.cs"
    Inherits="SIANWEB.CatPermisos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting  AjaxControlID="CmbCentro">
            <updatedcontrols>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </updatedcontrols>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" BackgroundPosition="Top">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="true" dir="rtl"
            Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton ToolTip="Correo" CommandName="mail" CssClass="mail" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Imprimir" CommandName="print" CssClass="print"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Eliminar" CommandName="delete" CssClass="delete"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Regresar" CommandName="undo" CssClass="undo" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Guardar" CommandName="save" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
                <telerik:RadToolBarButton ToolTip="Nuevo" CommandName="new" CssClass="new" ImageUrl="~/Imagenes/blank.png" />
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
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                Usuario
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cboUsuario" runat="server" Width="250px" AutoPostBack="True"
                                    Filter="Contains" Style="cursor: hand" TabIndex="2" OnSelectedIndexChanged="cboUsuario_SelectedIndexChanged"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="300px">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="cboUsuario"
                                    InitialValue="-- Seleccionar --" ErrorMessage="*Requerido" ValidationGroup="guardar"
                                    Display="Dynamic" SetFocusOnError="true" ForeColor="Red" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
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
                    <%--<asp:Panel ID="Panel2" runat="server" Visible="False">--%>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="RadGridPermisos" runat="server" AutoGenerateColumns="False"
                                    GridLines="None" Width="900px" Height="500px" Visible="false">
                                    <ClientSettings EnableRowHoverStyle="false">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    </ClientSettings>
                                    <MasterTableView NoMasterRecordsText="No existen registros que mostrar">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Menu" HeaderText="Menú" ItemStyle-HorizontalAlign="Left"
                                                UniqueName="Menu">
                                                <HeaderStyle Width="420px" />
                                                <ItemStyle HorizontalAlign="Left" Width="420px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sm_Cve" HeaderText="Menú" UniqueName="MenuCve"
                                                Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="Accesar">
                                                <HeaderTemplate>
                                                    <input onclick="CheckAllAccesar(this);" type="checkbox" id="ChkAccesarHeader" runat="server" />Accesar
                                                    <%--<asp:CheckBox ID="ChkAccesarHeader" runat="server" AutoPostBack="true" OnCheckedChanged="ChkAccesarHeader_CheckedChanged"
                                                            Style="cursor: hand" Text="Accesar" TextAlign="left" ToolTip="Seleccionar todos" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkAccesar" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PAccesar") %>'
                                                        Style="cursor: hand" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="grabar">
                                                <HeaderTemplate>
                                                    <input onclick="CheckAllGrabar(this);" type="checkbox" id="ChkGrabarHeader" runat="server" />Grabar
                                                    <%--<asp:CheckBox ID="ChkGrabarHeader" runat="server" AutoPostBack="true" OnCheckedChanged="ChkGrabarHeader_CheckedChanged"
                                                            Style="cursor: hand" Text="Grabar" TextAlign="left" ToolTip="Seleccionar todos" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkGrabar" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PGrabar") %>'
                                                        Style="cursor: hand" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="modificar">
                                                <HeaderTemplate>
                                                    <input onclick="CheckAllModificar(this);" type="checkbox" id="ChkModificarHeader"
                                                        runat="server" />Modificar
                                                    <%--<asp:CheckBox ID="ChkModificarHeader" runat="server" AutoPostBack="true" OnCheckedChanged="ChkModificarHeader_CheckedChanged"
                                                            Style="cursor: hand" Text="Modificar" TextAlign="left" ToolTip="Seleccionar todos" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkModificar" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PModificar") %>'
                                                        Style="cursor: hand" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="eliminar">
                                                <HeaderTemplate>
                                                    <input onclick="CheckAllEliminar(this);" type="checkbox" id="ChkEliminarHeader" runat="server" />Eliminar
                                                    <%--   <asp:CheckBox ID="ChkEliminarHeader" runat="server" AutoPostBack="true" OnCheckedChanged="ChkEliminarHeader_CheckedChanged"
                                                            Style="cursor: hand" Text="Eliminar" TextAlign="left" ToolTip="Seleccionar todos" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkEliminar" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PEliminar") %>'
                                                        Style="cursor: hand" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="imprimir">
                                                <HeaderTemplate>
                                                    <input onclick="CheckAllImprimir(this);" type="checkbox" id="ChkImprimirHeader" runat="server" />Imprimir
                                                    <%-- <asp:CheckBox ID="ChkImprimirHeader" runat="server" AutoPostBack="true" OnCheckedChanged="ChkImprimirHeader_CheckedChanged"
                                                            Style="cursor: hand" Text="Imprimir" TextAlign="left" ToolTip="Seleccionar todos" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkImprimir" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PImprimir") %>'
                                                        Style="cursor: hand" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Sp_PAccesar" UniqueName="SpTu_PAccesar" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sp_PGrabar" UniqueName="SpTu_PGrabar" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sp_PModificar" UniqueName="SpTu_PModificar" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sp_PEliminar" UniqueName="SpTu_PEliminar" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sp_PImprimir" UniqueName="SpTu_PImprimir" Visible="False">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                    <%--                    </asp:Panel>--%>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function CheckAllAccesar(sender) {
                //debugger;
                var grid = $find('<%=RadGridPermisos.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;

                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    (row.findElement("ChkAccesar")).checked = sender.checked;
                }
            }

            function CheckAllGrabar(sender) {
                //debugger;
                var grid = $find('<%=RadGridPermisos.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;

                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    (row.findElement("ChkGrabar")).checked = sender.checked;
                }
            }
            function CheckAllModificar(sender) {
                //debugger;
                var grid = $find('<%=RadGridPermisos.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;

                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    (row.findElement("ChkModificar")).checked = sender.checked;
                }
            }
            function CheckAllEliminar(sender) {
                //debugger;
                var grid = $find('<%=RadGridPermisos.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;

                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    (row.findElement("ChkEliminar")).checked = sender.checked;
                }
            }
            function CheckAllImprimir(sender) {
                //debugger;
                var grid = $find('<%=RadGridPermisos.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;

                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    (row.findElement("ChkImprimir")).checked = sender.checked;
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

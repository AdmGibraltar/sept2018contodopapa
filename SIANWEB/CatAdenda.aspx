<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatAdenda.aspx.cs" Inherits="SIANWEB.CatAdenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFacturacioncabecera">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgFacturacioncabecera" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFacturacionDetalle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgFacturacionDetalle" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCargocabecera">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgCargocabecera" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCargoDetalle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgCargoDetalle" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCreditocabecera">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgCreditocabecera" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCreditoDetalle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgCreditoDetalle" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgRefacturacioncabecera">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgRefacturacioncabecera" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgRefacturacionDetalle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgRefacturacionDetalle" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAdendas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server" style="font-family: Verdana; font-size: 8pt">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
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
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenActualiza" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label3" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <!-- Tabla principal--->
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;<asp:Label ID="Label1" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtId" runat="server" Width="70px" MaxLength="9" MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_txtId" runat="server" ControlToValidate="txtId"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Descripción "></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox onpaste="return false" ID="txtDescripcion" runat="server" Width="300px"
                                    MaxLength="200">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_txtDescripcion" runat="server" ControlToValidate="txtDescripcion"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                    AutoPostBack="True" Checked="True" />
                            </td>
                            <td>
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
                            <td width="70px">
                                &nbsp;
                            </td>
                            <td width="230">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Facturación" PageViewID="rpvFacturacion" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Nota de cargo" PageViewID="rpvCargo">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Nota de crédito" PageViewID="rpvCredito">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Refacturación" PageViewID="rpvRefacturacion">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderWidth="1px">
                        <telerik:RadPageView ID="rpvFacturacion" runat="server" Selected="True">
                            <table>
                                <tr>
                                    <td width="200">
                                        &nbsp;
                                    </td>
                                    <td width="50">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Addenda para la cabecera</b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgFacturacioncabecera" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnNeedDataSource="rgFacturacioncabecera_NeedDataSource" OnInsertCommand="rg_InsertCommand"
                                            OnUpdateCommand="rg_UpdateCommand" OnDeleteCommand="rg_DeleteCommand" OnItemDataBound="rg_ItemDataBound">
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                EditMode="InPlace" DataKeyNames="Requerido">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Det" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditTipo" runat="server" Text="1"></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Nombre del nodo" UniqueName="Nodo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNodo" runat="server" Text='<%# Bind("Nodo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditNodo" runat="server" Text='<%# Bind("Nodo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SoloAlfanumericoyGuiones" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Campo" UniqueName="Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditCampo" runat="server" Text='<%# Bind("Campo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SinComilla" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Longitud" UniqueName="Longitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongitud" runat="server" Text='<%# Bind("Longitud") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="EditLongitud" runat="server" Width="50px" Text='<%# Bind("Longitud") %>'
                                                                MaxLength="3" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Requerido" HeaderText="Requerido" DataField="Requerido">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido" Checked="true" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido2" Checked="true" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="29px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td width="200">
                                        &nbsp;
                                    </td>
                                    <td width="50">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Addenda para el detalle</b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgFacturacionDetalle" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnNeedDataSource="rgFacturacionDetalle_NeedDataSource" OnInsertCommand="rg_InsertCommand"
                                            OnUpdateCommand="rg_UpdateCommand" OnDeleteCommand="rg_DeleteCommand" OnItemDataBound="rg_ItemDataBound">
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                EditMode="InPlace" DataKeyNames="Requerido">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Det" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditTipo" runat="server" Text="2"></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Nombre del nodo" UniqueName="Nodo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNodo" runat="server" Text='<%# Bind("Nodo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditNodo" runat="server" Text='<%# Bind("Nodo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SoloAlfanumericoyGuiones" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Campo" UniqueName="Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditCampo" runat="server" Text='<%# Bind("Campo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SinComilla" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Longitud" UniqueName="Longitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongitud" runat="server" Text='<%# Bind("Longitud") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="EditLongitud" runat="server" Width="50px" Text='<%# Bind("Longitud") %>'
                                                                MaxLength="3" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Requerido" HeaderText="Requerido" DataField="Requerido">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido" Checked="true" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido2" Checked="true" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="29px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCargo" runat="server">
                            <table>
                                <tr>
                                    <td width="200">
                                        &nbsp;
                                    </td>
                                    <td width="50">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Addenda para la cabecera</b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgCargocabecera" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnNeedDataSource="rgCargocabecera_NeedDataSource" OnInsertCommand="rg_InsertCommand"
                                            OnUpdateCommand="rg_UpdateCommand" OnDeleteCommand="rg_DeleteCommand" OnItemDataBound="rg_ItemDataBound">
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                EditMode="InPlace" DataKeyNames="Requerido">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Det" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditTipo" runat="server" Text="3"></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Nombre del nodo" UniqueName="Nodo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNodo" runat="server" Text='<%# Bind("Nodo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditNodo" runat="server" Text='<%# Bind("Nodo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SoloAlfanumericoyGuiones" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Campo" UniqueName="Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditCampo" runat="server" Text='<%# Bind("Campo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SinComilla" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Longitud" UniqueName="Longitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongitud" runat="server" Text='<%# Bind("Longitud") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="EditLongitud" runat="server" Width="50px" Text='<%# Bind("Longitud") %>'
                                                                MaxLength="3" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Requerido" HeaderText="Requerido" DataField="Requerido">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido" Checked="true" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido2" Checked="true" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="29px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td width="200">
                                        &nbsp;
                                    </td>
                                    <td width="50">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Addenda para el detalle</b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgCargoDetalle" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            OnNeedDataSource="rgCargoDetalle_NeedDataSource" OnInsertCommand="rg_InsertCommand"
                                            OnUpdateCommand="rg_UpdateCommand" OnDeleteCommand="rg_DeleteCommand" OnItemDataBound="rg_ItemDataBound">
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                EditMode="InPlace" DataKeyNames="Requerido">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Det" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditTipo" runat="server" Text="4"></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Nombre del nodo" UniqueName="Nodo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNodo" runat="server" Text='<%# Bind("Nodo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditNodo" runat="server" Text='<%# Bind("Nodo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SoloAlfanumericoyGuiones" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Campo" UniqueName="Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditCampo" runat="server" Text='<%# Bind("Campo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SinComilla" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Longitud" UniqueName="Longitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongitud" runat="server" Text='<%# Bind("Longitud") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="EditLongitud" runat="server" Width="50px" Text='<%# Bind("Longitud") %>'
                                                                MaxLength="3" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Requerido" HeaderText="Requerido" DataField="Requerido">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido" Checked="true" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido2" Checked="true" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="29px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCredito" runat="server">
                            <table>
                                <tr>
                                    <td width="200">
                                        &nbsp;
                                    </td>
                                    <td width="50">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Addenda para la cabecera</b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgCreditocabecera" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnNeedDataSource="rgCreditocabecera_NeedDataSource" OnInsertCommand="rg_InsertCommand"
                                            OnUpdateCommand="rg_UpdateCommand" OnDeleteCommand="rg_DeleteCommand" OnItemDataBound="rg_ItemDataBound">
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                EditMode="InPlace" DataKeyNames="Requerido">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Det" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditTipo" runat="server" Text="5"></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Nombre del nodo" UniqueName="Nodo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNodo" runat="server" Text='<%# Bind("Nodo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditNodo" runat="server" Text='<%# Bind("Nodo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SoloAlfanumericoyGuiones" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Campo" UniqueName="Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditCampo" runat="server" Text='<%# Bind("Campo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SinComilla" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Longitud" UniqueName="Longitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongitud" runat="server" Text='<%# Bind("Longitud") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="EditLongitud" runat="server" Width="50px" Text='<%# Bind("Longitud") %>'
                                                                MaxLength="3" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Requerido" HeaderText="Requerido" DataField="Requerido">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido" Checked="true" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido2" Checked="true" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="29px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td width="200">
                                        &nbsp;
                                    </td>
                                    <td width="50">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Addenda para el detalle</b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgCreditoDetalle" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnNeedDataSource="rgCreditoDetalle_NeedDataSource" OnInsertCommand="rg_InsertCommand"
                                            OnUpdateCommand="rg_UpdateCommand" OnDeleteCommand="rg_DeleteCommand" OnItemDataBound="rg_ItemDataBound">
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                EditMode="InPlace" DataKeyNames="Requerido">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Det" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditTipo" runat="server" Text="6"></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Nombre del nodo" UniqueName="Nodo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNodo" runat="server" Text='<%# Bind("Nodo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditNodo" runat="server" Text='<%# Bind("Nodo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SoloAlfanumericoyGuiones" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Campo" UniqueName="Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditCampo" runat="server" Text='<%# Bind("Campo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SinComilla" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Longitud" UniqueName="Longitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongitud" runat="server" Text='<%# Bind("Longitud") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="EditLongitud" runat="server" Width="50px" Text='<%# Bind("Longitud") %>'
                                                                MaxLength="3" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Requerido" HeaderText="Requerido" DataField="Requerido">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido" Checked="true" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido2" Checked="true" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="29px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvRefacturacion" runat="server">
                            <table>
                                <tr>
                                    <td width="200">
                                        &nbsp;
                                    </td>
                                    <td width="50">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Addenda para la cabecera</b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgRefacturacioncabecera" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnNeedDataSource="rgRefacturacioncabecera_NeedDataSource" OnInsertCommand="rg_InsertCommand"
                                            OnUpdateCommand="rg_UpdateCommand" OnDeleteCommand="rg_DeleteCommand" OnItemDataBound="rg_ItemDataBound">
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                EditMode="InPlace" DataKeyNames="Requerido">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Det" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditTipo" runat="server" Text="7"></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Nombre del nodo" UniqueName="Nodo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNodo" runat="server" Text='<%# Bind("Nodo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditNodo" runat="server" Text='<%# Bind("Nodo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SoloAlfanumericoyGuiones" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Campo" UniqueName="Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditCampo" runat="server" Text='<%# Bind("Campo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SinComilla" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Longitud" UniqueName="Longitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongitud" runat="server" Text='<%# Bind("Longitud") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="EditLongitud" runat="server" Width="50px" Text='<%# Bind("Longitud") %>'
                                                                MaxLength="3" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Requerido" HeaderText="Requerido" DataField="Requerido">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido" Checked="true" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido2" Checked="true" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="29px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td width="200">
                                        &nbsp;
                                    </td>
                                    <td width="50">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Addenda para el detalle</b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgRefacturacionDetalle" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnNeedDataSource="rgRefacturacionDetalle_NeedDataSource" OnInsertCommand="rg_InsertCommand"
                                            OnUpdateCommand="rg_UpdateCommand" OnDeleteCommand="rg_DeleteCommand" OnItemDataBound="rg_ItemDataBound">
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                EditMode="InPlace" DataKeyNames="Requerido">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Det" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditDet" runat="server" Text='<%# Bind("Det") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="EditTipo" runat="server" Text="8"></asp:Label>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Nombre del nodo" UniqueName="Nodo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNodo" runat="server" Text='<%# Bind("Nodo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditNodo" runat="server" Text='<%# Bind("Nodo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SoloAlfanumericoyGuiones" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Campo" UniqueName="Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="EditCampo" runat="server" Text='<%# Bind("Campo") %>' MaxLength="200"
                                                                Width="250px">
                                                                <ClientEvents OnKeyPress="SinComilla" />
                                                            </telerik:RadTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="270px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Longitud" UniqueName="Longitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongitud" runat="server" Text='<%# Bind("Longitud") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="EditLongitud" runat="server" Width="50px" Text='<%# Bind("Longitud") %>'
                                                                MaxLength="3" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="Requerido" HeaderText="Requerido" DataField="Requerido">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido" Checked="true" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkRequerido2" Checked="true" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                        InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="29px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <table>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgAdendas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    PageSize="15" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                    OnItemCommand="rgAdendas_ItemCommand" OnNeedDataSource="rgAdendas_NeedDataSource"
                                    OnPageIndexChanged="rgAdendas_PageIndexChanged">
                                    <MasterTableView DataKeyNames="Id_Ade" DataMember="listTipoCostos">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Clave" UniqueName="Id_Ade" DataField="Id_Ade">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Descripción" UniqueName="Tco_Descripcion" DataField="Tco_Descripcion">
                                                <ItemStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tco_Activo" HeaderText="Estatus" UniqueName="Tco_Activo"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tco_ActivoStr" HeaderText="Estatus" UniqueName="Tco_ActivoStr">
                                                <ItemStyle Width="90px" />
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
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

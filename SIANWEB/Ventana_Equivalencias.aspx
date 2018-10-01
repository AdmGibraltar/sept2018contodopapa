<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ventana_Equivalencias.aspx.cs"
    MasterPageFile="~/MasterPage/MasterPage03.Master" Inherits="SIANWEB.Ventana_Equivalencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rtb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="aceptarToolbar"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
            </Items>
        </telerik:RadToolBar>
        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="false"
            AllowFilteringByColumn="false" MasterTableView-NoMasterRecordsText="No se encontraron registros."
            OnNeedDataSource="RadGrid1_NeedDataSource" OnPageIndexChanged="RadGrid1_PageIndexChanged"
            GridLines="None" OnItemCommand="RadGrid1_ItemCommand" OnInsertCommand="RadGrid1_InsertCommand"
            OnItemDataBound="RadGrid1_ItemDataBound">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="false" TableLayout="Auto" AllowMultiColumnSorting="False"
                AllowNaturalSort="true" AllowSorting="true" EditMode="InPlace" CommandItemDisplay="Top">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="true"
                    ShowRefreshButton="false" AddNewRecordText="Agregar"></CommandItemSettings>
                <Columns>
                    <telerik:GridTemplateColumn DataField="Id" UniqueName="Id" HeaderText="Clave" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" ShowFilterIcon="False" FilterControlWidth="50px"
                        AllowFiltering="false" HeaderTooltip="Introduzca una clave para su búsqueda">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadNumericTextBox ID="txtId" runat="server" Width="50px" Text='<%# Eval("Id") %>'
                                OnTextChanged="txtId_TextChanged" AutoPostBack="true">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="70px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Descripcion" UniqueName="Descripcion" HeaderText="Nombre"
                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="False"
                        AllowFiltering="false" FilterControlWidth="98%" HeaderTooltip="Introduzca una descripción para su búsqueda">
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="98%" Text='<%# Eval("Descripcion") %>'
                                ReadOnly="true">
                            </telerik:RadTextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="450px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Disponible" UniqueName="Disponible" HeaderText="Disponible"
                        AllowFiltering="false">
                        <EditItemTemplate>
                            <asp:Label ID="lblDisponibleEdit" runat="server" Text='<%# Eval("Disponible") %>'
                                ReadOnly="true"></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDisponible" runat="server" Text='<%# Eval("Disponible") %>' ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Cantidad" UniqueName="Cantidad" HeaderText="Cantidad"
                        AllowFiltering="false">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Width="50px" Text='<%# Eval("Cantidad") %>'>
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Precio" UniqueName="Precio" HeaderText="Precio"
                        AllowFiltering="false">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtPrecio" runat="server" Width="50px" Text='<%# Eval("Precio") %>'>
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                        InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                        <HeaderStyle Width="70px" />
                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                    </telerik:GridEditCommandColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
        </telerik:RadGrid>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow)
                        oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                    else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                    return oWindow;
                }
                //Cierra la venata actual y regresa el foco a la ventana padre
                function CloseWindow() {
                    GetRadWindow().Close();
                }
                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind(param) {
                    //debugger;
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.CambioProductos();
                }
            </script>
        </telerik:RadCodeBlock>
    </div>
</asp:Content>

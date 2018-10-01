<%@ Page Title="Generación de órdenes de compra" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProOrdCompra_GenMax.aspx.cs" Inherits="SIANWEB.ProOrdCompra_GenMax" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de captura de Orden de Compra
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesOrdenCompra() {
                //debugger;
                var txtProveedor = $find('<%= txtProveedor.ClientID %>');
                var cmbProveedor = $find('<%= cmbProveedor.ClientID %>');
                var txtId_PrdInicial = $find('<%= txtId_PrdInicial.ClientID %>');
                var txtId_PrdFinal = $find('<%= txtId_PrdFinal.ClientID %>');
                var chkTransito = document.getElementById('chkTransito');
                LimpiarTextBox(txtProveedor);
                LimpiarComboSelectIndex0(cmbProveedor);
                LimpiarTextBox(txtId_PrdInicial);
                LimpiarTextBox(txtId_PrdFinal);
                LimpiarTextBox(cmbProductoFinal);
                LimpiarCheckBox(chkTransito);
            }

            function ValidacionesEspeciales() {
                var productoInicio = 0;
                if (txtId_PrdInicial.get_textBoxValue() != '') {
                    productoInicio = parseFloat(txtId_PrdInicial.get_textBoxValue());
                }

                var productoFin = 0;
                if (txtId_PrdFinal.get_textBoxValue() != '') {
                    productoFin = parseFloat(txtId_PrdFinal.get_textBoxValue());
                }

                if (productoInicio > 0 && productoFin > 0 && (productoInicio > productoFin)) {
                    var alertaMsg = radalert('El producto inicial no debe ser mayor al producto final', 330, 150, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtId_PrdInicial.focus();
                    });
                    return false;
                }
                return true;
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }

                switch (button.get_value()) {
                    case 'new':
                        continuarAccion = false;
                        //AbrirVentana_OrdenCompra(0, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            //cuando el campo de texto pirde el foco
            function txtProveedor_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbProveedor.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProveedor.ClientID %>'));
            }

            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de Partidas dispara evento de edición
            //--------------------------------------------------------------------------------------------------
            function rgOrdenCompra_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //----------- Datos producto inicial --------------
            var txtId_PrdInicial;

            function txtId_PrdInicial_OnLoad(sender, args) {
                txtId_PrdInicial = sender;
            }

            //----------- Datos producto final --------------
            var txtId_PrdFinal;


            function txtId_PrdFinal_OnLoad(sender, args) {
                txtId_PrdFinal = sender;
            }

            function ObtenerNombre(prd) {
                var urlArchivo = 'ObtenerNombre.aspx';
                parametros = "prd=" + prd;
                return obtenerrequest(urlArchivo, parametros);
            }

            function ObtenerNombreInicio() {
                //debugger;
                var txtPrd = $find('<%= txtId_PrdInicial.ClientID %>');
                var PrdIni = $find('<%= txtProductoIni.ClientID %>');
                var actual;

                if (txtPrd.get_value() == '') {
                    actual = '';
                }
                else {
                    actual = ObtenerNombre(txtPrd.get_value());
                    if (actual == "-0") {
                        window.location.href("Login.aspx");
                    }
                }
                PrdIni.set_value(actual);
            }

            function ObtenerNombreFinal() {
                //debugger;
                var txtPrd = $find('<%= txtId_PrdFinal.ClientID %>');
                var PrdIni = $find('<%= txtProductoFin.ClientID %>');
                var actual;

                if (txtPrd.get_value() == '') {
                    actual = '';
                }
                else {
                    actual = ObtenerNombre(txtPrd.get_value());
                    if (actual == "-0") {
                        window.location.href("Login.aspx");
                    }
                }
                PrdIni.set_value(actual);
            }
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0) {
                    args.set_enableAjax(false);
                }
                else {
                    args.set_enableAjax(true);
                }
            }
            //--------------------------------------------------------------------------------------------------
            //Abre la ventana para subir archivos de Excel
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_Excel(Id_Emp, Id_Cd, proveedor, productoInicial, productoFinal, aplicaTransito) {
                //debugger;
                var oWnd = radopen("ProOrdenCompra_GenExcel.aspx?"
                    + "&Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&proveedor=" + proveedor
                    + "&productoInicial=" + productoInicial
                    + "&productoFinal=" + productoFinal
                    + "&aplicaTransito=" + aplicaTransito                   
                    , "AbrirVentana_vExcel");
                oWnd.center();
            }
            function CerrarWindow_ClientEvent(sender, eventArgs) {
                //debugger;
                   refreshGrid('RebindGrid');
               }
            
            function LimpiarBanderaRebind(sender, eventArgs) {
            }
            function ActivarBanderaRebind_Excel() {
            }
            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------
            function refreshGrid(accion) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(accion);
            }
        </script>
    </telerik:RadCodeBlock>  
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtId_PrdInicial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtId_PrdFinal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBusqueda">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgOrdenCompra" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgOrdenCompra">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgOrdenCompra" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>           
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip=""
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Cancelar"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="arcExcel" Value="arcExcel" Text="" CssClass="descExcel"
                ToolTip="Subir archivo Excel" ImageUrl="Imagenes/blank.png" />            
        </Items>
    </telerik:RadToolBar>
    <br />
    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%" border="0">
            <tr>
                <td align="left">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
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
                    <asp:Panel ID="panelFrom" runat="server">
                        <table>
                            <tr>
                                <td width="100">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblProveedor" runat="server" Text="Proveedor"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtProveedor" runat="server" MaxLength="9" MinValue="1"
                                        Width="70px">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnBlur="txtProveedor_OnBlur" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbProveedor" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                        LoadingMessage="Cargando..." MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                        OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged" Width="300px"
                                        MaxHeight="250px">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                        <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                            Width="50px" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_txtProveedor" runat="server" ControlToValidate="txtProveedor"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="buscar">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblProductoInicial" runat="server" Text="Producto inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtId_PrdInicial" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1" Text='<%# Eval("Id_Prd") %>'>
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtId_PrdInicial_OnLoad" OnBlur="ObtenerNombreInicio" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtProductoIni" runat="server" Width="298px" ReadOnly="True">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblProductoFinal" runat="server" Text="Producto final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtId_PrdFinal" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1" Text='<%# Eval("Id_Prd") %>' ClientEvents-OnLoad="txtId_PrdFinal_OnLoad">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" OnBlur="ObtenerNombreFinal" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtProductoFin" runat="server" Width="298px" ReadOnly="True">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkTransito" runat="server" Text="Tránsito" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnBusqueda" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBusqueda_Click"
                                        ToolTip="Buscar" ValidationGroup="buscar" OnClientClick="return ValidacionesEspeciales()" />
                                </td>
                                <td>
                                </td>
                            </tr>                           
                            <tr>
                                <td colspan="2">
                                  <%--  <asp:ImageButton ID="botonExcel" runat="server" 
                                        ImageUrl="~/Imagenes/excel.gif" onclick="botonExcel_Click" 
                                        ToolTip="Subir archivo Excel" />--%>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hiddenId" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <telerik:RadGrid ID="RadGrid1" runat="server">
                    </telerik:RadGrid>
                    <telerik:RadGrid ID="rgOrdenCompra" runat="server" GridLines="None" HeaderStyle-HorizontalAlign="Center"
                        AllowPaging="true" AutoGenerateColumns="False" AllowMultiRowSelection="false"
                        OnNeedDataSource="rgOrdenCompra_NeedDataSource" OnUpdateCommand="rgOrdenCompra_UpdateCommand"
                        OnItemDataBound="rgOrdenCompra_ItemDataBound" OnPageIndexChanged="rgOrdenCompra_PageIndexChanged"
                        OnItemCommand="rgOrdenCompra_ItemCommand">
                        <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="OrdenCompra"
                            HideStructureColumns="true" ExportOnlyData="True" 
                            Excel-FileExtension="xls" >
                        </ExportSettings>
                        <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Prd,existencia,Prd_MaxExistencia, Prd_UniEmp"
                            EditMode="InPlace" HorizontalAlign="NotSet" PageSize="10" AutoGenerateColumns="False"
                            NoMasterRecordsText="No hay registro para mostrar.">
                            <ExpandCollapseColumn Visible="True">
                            </ExpandCollapseColumn>
                            <CommandItemSettings ShowExportToExcelButton="true" ExportToExcelText="Exportar a Excel" 
                             AddNewRecordText="Agregar" RefreshText="Actualizar" ></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd"
                                    ReadOnly="true">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion"
                                    ReadOnly="true">
                                    <HeaderStyle Width="210px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Presentacion" HeaderText="Presentación" UniqueName="Prd_Presentacion"
                                    ReadOnly="true">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ventaMes3" HeaderText="Venta mes 3" UniqueName="ventaMes3"
                                    ReadOnly="true" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ventaMes2" HeaderText="Venta mes 2" UniqueName="ventaMes2"
                                    ReadOnly="true" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ventaMes1" HeaderText="Venta mes 1" UniqueName="ventaMes1"
                                    ReadOnly="true" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ventaMes0" HeaderText="Venta mes actual" UniqueName="ventaMes0"
                                    ReadOnly="true" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ventaPromedio" HeaderText="Promedio" UniqueName="ventaPromedio"
                                    ReadOnly="true" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                               <%-- <telerik:GridTemplateColumn HeaderText="Promedio" DataField="ventaPromedio" UniqueName="ventaPromedio"
                                    ReadOnly="true">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtPromedio" runat="server" Width="50px" MaxLength="9"
                                            ReadOnly="true" BackColor="Transparent" BorderWidth="0" Text='<%# Eval("ventaPromedio") %>'>
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <EnabledStyle HorizontalAlign="Right" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_MaxExistencia" HeaderText="Máximo" UniqueName="Prd_MaxExistencia"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Ordenado" DataField="ordenado" UniqueName="ordenado">
                                    <ItemTemplate>
                                        <div style="text-align: right">
                                            <asp:Label ID="lblordenado" runat="server" Text='<%# Eval("ordenado") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtordenado" runat="server" Width="50px" MaxLength="9"
                                            MinValue="0" Text='<%# Eval("ordenado") %>'>
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Prd_UniEmp" HeaderText="Prd_UniEmp" UniqueName="Prd_UniEmp"
                                    ReadOnly="true" Display="false">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                    EditText="Editar" CancelText="Cancelar" UpdateText="Actualizar" HeaderText="Editar">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </telerik:GridEditCommandColumn>
                            </Columns>
                            <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de partida con el producto {0}"
                                InsertCaption="Agregar nueva partida">
                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                    BorderColor="#000000" BorderWidth="1" />
                                <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                    EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                </EditColumn>
                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                            </EditFormSettings>
                        </MasterTableView>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                            PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                            PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings>
                            <ClientEvents OnRowDblClick="rgOrdenCompra_ClientRowDblClick" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

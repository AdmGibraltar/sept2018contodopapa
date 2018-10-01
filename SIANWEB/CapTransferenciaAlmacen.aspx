<%@ Page Title="Transferencia de Almacen" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapTransferenciaAlmacen.aspx.cs" Inherits="SIANWEB.CapTransferenciaAlmacen" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .MyImageButton
        {
            cursor: hand;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;              
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                    ajaxManager.ajaxRequest('panel');            
            }
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = '';           
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de captura de Orden de Compra
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesTransferenciaAlmacen() {
                //debugger;
                var txtFolio = $find('<%= txtFolio.ClientID %>');
                var txtFecha = $find('<%= txtFecha.ClientID %>');
               
               
                var txtNotas = $find('<%= txtNotas.ClientID %>');

                LimpiarTextBox(txtFolio);
                LimpiarDatePicker(txtFecha);
              
                LimpiarTextBox(txtProveedor);
                LimpiarComboSelectIndex0(cmbProveedor);
                LimpiarTextBox(txtNotas);
            }

            //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un producto
            //y selecciona la Tab donde esta el control
            function ValidaObjetoRequerido(textBox, label, indiceTab) {
                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                if (textBox.get_textBoxValue() == '') {
                    label.innerHTML = '*Requerido';
                    radTabStrip.get_allTabs()[indiceTab].select();
                    return false;
                }
                return true;
            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var lbl_cmbProductoClientId = '';
            var txtOrd_CantidadClientId = '';
            var lblVal_txtOrd_CantidadClientId = '';
            var HD_Prd_UniEmpClientId = '';

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }

                switch (button.get_value()) {
                    case 'new':
                        LimpiarControlesTransferenciaAlmacen();

                        //select tab datos generales
                        var RadTabStripPrincipal = $find('<%= RadTabStrip1.ClientID %>');
                        RadTabStripPrincipal.get_allTabs()[0].select();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenId = document.getElementById('<%= hiddenId.ClientID %>');
                        hiddenId.value = '';


                        //establecer datos por default
                      

                        var fechaActual = new Date('<%= ActualAnio %>', '<%= ActualMes %>', '<%= ActualDia %>');
                        var txtFecha = $find('<%= txtFecha.ClientID %>');
                        txtFecha.set_selectedDate(fechaActual);


                        //establecer consecitivo de folio de proveedor
                        var txtFolio = $find('<%= txtFolio.ClientID %>');
                        txtFolio.set_value('<%= Valor %>');
                        txtFolio.disable();

                        //poner foco en txtProveedor
                       

                        continuarAccion = true;
                        break;

                    case 'save':
                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        radTabStrip.get_allTabs()[0].select();

                        continuarAccion = _ValidarFechaEnPeriodo();
                        break;

                   
                }

                if (continuarAccion == true) {
                    GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                }

                args.set_cancel(!continuarAccion);
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= txtFecha.ClientID %>');
                return txtFecha._dateInput;
            }

            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
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
            function CloseAndRebind() {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            //--------------------------------------------------------------------------------------------------
            //Setea variable de pestaña del TabStrip es clickeada
            //--------------------------------------------------------------------------------------------------
            function OnClientTabSelectingHandler(sender, args) {
                tabSeleccionada = args.get_tab().get_text();
            }

            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de Precios dispara evento de edición
            //--------------------------------------------------------------------------------------------------
            function rgTransferenciaAlmacen_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //cuando el campo de texto pirde el foco
           

            //Para el combo de Productos dentro del Grid
            var txtId_Prd;
            var cmbProducto;

            function txtId_Prd_OnLoad(sender, args) {
                txtId_Prd = sender;
            }

            function txtPrd_Descripcion(sender, args) {//cmbProducto_OnLoad
                cmbProducto = sender;
            }

            //cuando el campo de texto de edición del Grid de clave de producto pierde el foco
            function txtId_Prd_OnBlur(sender, args) {
            }
            function popup(pvd) {

                var oWnd = radopen("Ventana_Buscar.aspx?pvd=" + pvd, "AbrirVentana_Buscar");
                oWnd.center();
            }

            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="hiddenId" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="rgTransferenciaAlmacen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
           
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                ImageUrl="Imagenes/blank.png" />
          
        </Items>
    </telerik:RadToolBar>
    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenId" runat="server" />
                </td>
            </tr>
        </table>
        <table style="font-family: verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RPVGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="t" PageViewID="RPVDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden" Width="98%"><%-- Height="300px" Width="765px">--%>
                        <telerik:RadPageView ID="RPVGenerales" runat="server" Heigth="250px">
                         <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="250px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%" >
                            <telerik:RadPane ID="RadPane2" runat="server" Height="250px" OnClientResized="onResize"
                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                            <table><%-- width="500px">--%>
                                <tr>
                                    <td colspan="10">
                                        <asp:Literal ID="literal" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFolio" runat="server" Text="Folio"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFolio" runat="server" Width="70px" MaxLength="9"
                                            MinValue="1" Enabled="false">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                      <asp:Label ID="Label5" runat="server" Text="Num. Remisión"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtId_RemOrigen" runat="server" Width="70px" MaxLength="9"
                                            MinValue="1" Enabled="false">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                   
                                    <td>
                                        <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFecha" runat="server" Width="100px" Enabled="false">
                                            <DatePopupButton ToolTip="Abrir calendario" />
                                            <Calendar ID="cal_dpFecha" runat="server">
                                                <ClientEvents OnDateClick="Calendar_Click" />
                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                    TodayButtonCaption="Hoy" />
                                            </Calendar>
                                            <DateInput runat="server" MaxLength="10">
                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="val_txtFecha" runat="server" ControlToValidate="txtFecha"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                                                     
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCdI" runat="server" Text="CDI"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCdIOrigen" runat="server" Width="70px" MaxLength="9"
                                            MinValue="1" Enabled="false">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                          
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="CDI Nombre"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtCdIOrigenStr" runat="server" Width="200px" MaxLength="9"
                                            MinValue="1" Enabled="false">
                                           
                                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                     <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                     <asp:Label ID="Label6" runat="server" Text="Usuario Creador ID"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="Id_UOrigen" runat="server" Width="70px" MaxLength="9"
                                            MinValue="1" Enabled="false">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                          
                                        </telerik:RadNumericTextBox>
                                    </td>
                            
                              
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Usuario Creador"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="U_NombreOrigen" runat="server" Width="200px" MaxLength="9"
                                            Enabled="false">
                                            
                                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lblNotas" runat="server" Text="Notas"></asp:Label>
                                    </td>
                                    <td colspan="7">
                                        <telerik:RadTextBox ID="txtNotas" runat="server" Rows="4" TextMode="MultiLine" Width="99%" Enabled="false" >
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" OnFocus="_ValidarFechaEnPeriodo" />
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <asp:HiddenField ID="HD_TransferenciaAlmacenEstatus" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            </telerik:RadPane>
                         </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVDetalles" runat="server">
                          <%--  <asp:Panel ID="Panel" runat="server" Width="765px" ScrollBars="auto">--%>
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="300px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%">
                               <telerik:RadPane ID="RadPane1" runat="server" Height="300px" OnClientResized="onResize" BorderStyle="None" Scrolling="Both"> 
                                <telerik:RadGrid ID="rgTransferenciaAlmacen" runat="server" GridLines="None" DataMember="listaTransferenciaAlmacenDet"
                                    AllowPaging="False" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                                    BorderColor="White" BorderStyle="Solid" OnNeedDataSource="rgTransferenciaAlmacen_NeedDataSource" 
                                    OnDeleteCommand="rgTransferenciaAlmacen_DeleteCommand"
                                    OnInsertCommand="rgTransferenciaAlmacen_InsertCommand" OnUpdateCommand="rgTransferenciaAlmacen_UpdateCommand"
                                    OnItemDataBound="rgTransferenciaAlmacen_ItemDataBound" OnPageIndexChanged="rgTransferenciaAlmacen_PageIndexChanged">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="ListaOrdenesDeCompra"
                                        HideStructureColumns="true" ExportOnlyData="true">
                                    </ExportSettings>
                                    <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Trans, Id_TransDet, Id_Prd"
                                        EditMode="InPlace" DataMember="listaTransferenciaAlmacenDet" HorizontalAlign="NotSet" PageSize="6"
                                        AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros.">
                                        <ExpandCollapseColumn Visible="True">
                                        </ExpandCollapseColumn>
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Trans" HeaderText="Id_Trans" UniqueName="Id_Trans"
                                                ReadOnly="true" Display="false">
                                                <HeaderStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_TransDet" HeaderText="Id_TransDet" UniqueName="Id_TransDet"
                                                ReadOnly="true" Display="false">
                                                <HeaderStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                        MinValue="1" Text='<%# Eval("Id_Prd") %>' OnTextChanged="txtProducto_TextChanged"
                                                        OnLoad="txtProducto_Load" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtId_Prd_OnBlur" OnLoad="txtId_Prd_OnLoad" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd"
                                                Visible="false">
                                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_Prd" runat="server" Text='<%DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <table border="0">
                                                        <tr>
                                                            <td style="border-color: transparent">
                                                            </td>
                                                            <td style="border-color: transparent">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Producto.Prd_Descripcion"
                                                UniqueName="Prd_Descripcion">
                                                <HeaderStyle Width="367px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_Descripcion" runat="server" Text='<%# ObtenerDescripcion(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="border-bottom-style: none">
                                                                <telerik:RadTextBox ID="txtPrd_Descripcion" runat="server" Width="320px" ReadOnly="true"
                                                                    Text='<%# ObtenerDescripcion(Container.DataItem) %>'>
                                                                </telerik:RadTextBox>
                                                            </td>
                                                            <td style="border-bottom-style: none">
                                                                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                                    ToolTip="Buscar" ValidationGroup="buscar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Presen." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_Presentacion" runat="server" Text='<%# ObtenerPresentacion(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtPrd_Presentacion" runat="server" Width="50px" ReadOnly="true"
                                                        Text='<%# ObtenerPresentacion(Container.DataItem) %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Unid." DataField="Prd_UniNe" UniqueName="Prd_UniNe">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_UniNe" runat="server" Text='<%# ObtenerUnidades(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtPrd_UniNe" runat="server" Width="50px" ReadOnly="true"
                                                        Text='<%# ObtenerUnidades(Container.DataItem) %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                             <telerik:GridTemplateColumn HeaderText="Cantidad" DataField="Trans_Cant" UniqueName="Trans_Cant">
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrans_Cant" runat="server" Text='<%# Eval("Trans_Cant") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtTrans_Cantidad" runat="server" Width="80px" MaxLength="9"
                                                        Text='<%# Eval("Trans_Cantidad") %>' OnTextChanged="txtCantidad_TextChanged" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />                                                       
                                                    </telerik:RadNumericTextBox>
                                                    <asp:Label ID="lblVal_txtTrans_Cant" runat="server" ForeColor="#FF0000"></asp:Label>
                                                    <asp:HiddenField ID="HD_Prd_UniEmp" runat="server" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                             <telerik:GridTemplateColumn HeaderText="Precio" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>                                                    
                                                    <asp:Label ID="Label2" runat="server" Text='<%# ObtenerPrecio(Container.DataItem) %>' />                                                
                                                </ItemTemplate>
                                                <EditItemTemplate>                                                   
                                                    <asp:HiddenField ID="HD_Prd_PrecioAAA" runat="server" Value='<%# ObtenerPrecio(Container.DataItem) %>' />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                                        PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                        PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                        ShowPagerText="True" PageButtonCount="3" />
                                    <GroupingSettings CaseSensitive="False" />
                                    <%--<ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="300px">
                                        </Scrolling>
                                    </ClientSettings>--%>
                                </telerik:RadGrid>
                          <%--  </asp:Panel>--%>
                          </telerik:RadPane>
                         </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTotal" runat="server" ReadOnly="true">
                                    <NumberFormat DecimalDigits="2" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                           <td>
                               <asp:HiddenField ID="HiddenHeight" runat="server" />
                               <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                           </td>
                       </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<%@ Page Title="Embarque de facturas por ruta" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="ProFacturaRuta.aspx.cs" Inherits="SIANWEB.ProFacturaRuta" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dpFecha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDetalles" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" Height="24" dir="rtl"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="T" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                            <%-- Width="670px" Height="250px"--%>
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="250px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="250px" OnClientResized="onResize"
                                   BorderColor="White" BorderStyle="Solid" BorderWidth="1px">
                                   <div id="formularioDatosGenerales" runat="server">
                                    <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td width="70">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    Embarque
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtEmbarque" runat="server" Width="70px" Enabled="false"
                                                        MaxLength="9" MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    Fecha
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="dpFecha" runat="server" Width="100px" DateInput-MaxLength="10">
                                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario" />
                                                        <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy">
                                                            </FastNavigationSettings>
                                                        </Calendar>
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="70" valign="middle">
                                                    Chofer
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtChofer" runat="server" MaxLength="50" onpaste="return false"
                                                        Width="340px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtChofer"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td width="70" valign="middle">
                                                    Día
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDia" runat="server" DateInput-MaxLength="10" Width="100px">
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy">
                                                            </FastNavigationSettings>
                                                        </Calendar>
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDia"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10" valign="middle">
                                                </td>
                                                <td width="70">
                                                    Camioneta
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCamioneta" runat="server" MaxLength="50" onpaste="return false"
                                                        Width="340px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCamioneta"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <asp:HiddenField ID="HF_ID" runat="server" />
                                                <asp:HiddenField ID="HiddenHeight" runat="server" />
                                                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                                            </tr>
                                        </table>                                 
                                   </div>      
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <%--<asp:Panel ID="Panel1" runat="server" Width="670px" Height="250px">--%>
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="270px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="250px" OnClientResized="onResize"
                                    BorderStyle="None">
                                    <telerik:RadGrid ID="rgDetalles" runat="server" GridLines="None" AllowPaging="False"
                                        AutoGenerateColumns="False" OnNeedDataSource="RadGrid1_NeedDataSource" OnInsertCommand="rgDetalles_InsertCommand"
                                        OnDeleteCommand="rgDetalles_DeleteCommand" OnItemCommand="rgDetalles_ItemCommand"
                                        OnItemDataBound="rgDetalles_ItemDataBound">
                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_Fac, Id_FacSerie" NoMasterRecordsText="No se encontraron registros."
                                            EditMode="InPlace" DataMember="listaFacDet" HorizontalAlign="NotSet" AutoGenerateColumns="False">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                            <RowIndicatorColumn>
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn>
                                                <HeaderStyle Width="20px" />
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Fac" HeaderText="Id_Fac" UniqueName="Id_Fac"
                                                    ReadOnly="true" Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn UniqueName="Serie" HeaderText="Serie">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FacSerieLabel" runat="server" Text='<%# Eval("Id_FacSerie") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="rcbSerie" runat="server" Width="80px">
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="115px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Factura" HeaderText="Factura" UniqueName="Factura">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtFact" runat="server" MaxLength="9" MinValue="1"
                                                            Text='<%# Eval("Id_Fac") %>' Width="55px" OnTextChanged="txtFac_TextChanged"
                                                            AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFact"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="FacLabel" runat="server" Text='<%# Eval("Id_Fac") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCte" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Cte_NomComercial") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtCte" runat="server" ReadOnly="true" Text='<%# ObtenerCte("Cte_NomComercial") %>'
                                                            Width="250px">
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Fac_Importe" HeaderText="Importe" UniqueName="Fac_Importe">
                                                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtImporte" runat="server" MaxLength="9" MinValue="0"
                                                            ReadOnly="true" Width="70px" Text='<%# ObtenerImporte("Fac_Importe") %>'>
                                                        </telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtImporte"
                                                            Display="Dynamic" ErrorMessage="RequiredFieldValidator" ForeColor="red">* Requerido</asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblImporte" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Fac_Importe")).ToString("N") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                    EditText="Editar" CancelText="Cancelar" InsertText="Aceptar">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Width="70px" HorizontalAlign="Center" Wrap="False" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn ConfirmText="¿Desea quitar esta factura de la lista?" ConfirmDialogType="RadWindow"
                                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmTitle="" ButtonType="ImageButton"
                                                    CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn">
                                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                            ShowPagerText="True" PageButtonCount="3" />
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                            <%--</asp:Panel>--%>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function LimpiarBanderaRebind() {
            }

            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;              
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                    ajaxManager.ajaxRequest('panel');             
            }
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            function ClientTabSelecting(sender, args) {
                continuarAccion = _ValidarFechaEnPeriodo();
                args.set_cancel(!continuarAccion);
            }
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
            }
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
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
                        LimpiarControles();
                        //registro nuevo -> se limpia bandera de actualización
                        var txtId = $find('<%= txtEmbarque.ClientID %>');
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CapReclamaciones";
                        parametros = parametros + "&sp=spCatLocal_Maximo";
                        parametros = parametros + "&columna=Id_Pag";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        continuarAccion = false;
                        break;
                }
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= dpFecha.ClientID %>');
                return txtFecha._dateInput;
            }

            function TabSelected(sender, args) {
            }

            //Variables necesarias el campo de facturas
            var txtFact;
            function rcmbFac_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtFact);
            }

            /*CERRAR VENTANA*/
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                CloseAndRebind();
                            });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                //debugger;
                GetRadWindow().BrowserWindow.AbrirReporte();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

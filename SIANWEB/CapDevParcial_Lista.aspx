<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapDevParcial_Lista.aspx.cs" Inherits="SIANWEB.CapDevParcial_Lista" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
            EnablePageHeadUpdate="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RAM1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgDevolucion" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="CmbCentro">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ImageButton2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Agregar" Value="Agregar" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div runat="server" id="divPrincipal">
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    </td>
                    <td style="text-align: right; width: 150px">
                        <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table style="font-family: verdana; font-size: 8pt">
                            <tr>
                                <td width="110">
                                </td>
                                <td width="100">
                                </td>
                                <td width="50">
                                </td>
                                <td width="100">
                                </td>
                                <td width="50">
                                </td>
                                <td width="150">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblNombre" runat="server" Text="Nombre de cliente"></asp:Label>
                                </td>
                                <td colspan="5">
                                    <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="465px"
                                        MaxLength="140">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblClienteini" runat="server" Text="Cliente inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtClienteini" runat="server" Width="125px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="SoloNumerico" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td style="width: 70px">
                                    <asp:Label ID="LblClientefin" runat="server" Text="Cliente final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtClientefin" runat="server" Width="125px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="SoloNumerico" />
                                    </telerik:RadNumericTextBox>
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
                                    <asp:Label ID="LblFechaini" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dpFechaini" runat="server" Width="155px">
                                        <Calendar ID="Calendar1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                            runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 65px">
                                    <asp:Label ID="LblFechafin" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dpFechafin" runat="server" Width="155px">
                                        <Calendar ID="Calendar2" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput2" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                            runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:Label ID="LblEstatus" runat="server" Text="Estatus"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Todos --" Value="1" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Capturado" Value="C" />
                                            <telerik:RadComboBoxItem Text="Impreso" Value="I" />
                                            <telerik:RadComboBoxItem Text="Baja" Value="B" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblDevIni" runat="server" Text="Dev. inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDevini" runat="server" Width="125px" MinValue="0"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="SoloNumerico" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LblDevFin" runat="server" Text="Dev. final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDevfin" runat="server" Width="125px" MinValue="0"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="SoloNumerico" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td align="left">
                                    <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton1_Click" ToolTip="Buscar"
                                        ImageUrl="~/Img/find16.png" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <telerik:RadGrid ID="rgDevolucion" runat="server" GridLines="None" 
                PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                AutoGenerateColumns="False" AllowPaging="True" HeaderStyle-HorizontalAlign="Center"
                OnNeedDataSource="rgDevolucion_NeedDataSource" OnPageIndexChanged="rgDevolucion_PageIndexChanged"
                OnItemCommand="rgDevolucion_ItemCommand" OnItemDataBound="rgDevolucion_ItemDataBound">
                <MasterTableView ClientDataKeyNames="Id_Nca">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Id_Nca" HeaderText="Devolución" UniqueName="Id_Nca">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" UniqueName="Tipo">
                            <HeaderStyle HorizontalAlign="Center" Width="70px"  />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Es_Estatus" HeaderText="EstatusiId" UniqueName="Es_Estatus"
                            Visible="false">
                            <HeaderStyle HorizontalAlign="Center"  Width="70px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus">
                            <HeaderStyle HorizontalAlign="Center"  Width="70px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Id_Nca2" HeaderText="Nota de crédito" UniqueName="Id_Nca2">
                            <HeaderStyle HorizontalAlign="Center"  Width="70px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha"
                            DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center"  Width="70px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Num_Cliente" HeaderText="Cliente" UniqueName="Num_Cliente">
                            <HeaderStyle HorizontalAlign="Center"  Width="50px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Cliente" HeaderText="Nombre" UniqueName="Cliente">
                            <HeaderStyle HorizontalAlign="Center"  Width="250px"/>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nca_Subtotal" HeaderText="Subtotal" DataFormatString="{0:N2}"
                            UniqueName="Nca_Subtotal">
                            <HeaderStyle HorizontalAlign="Center"  Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nca_Iva" HeaderText="I.V.A." DataFormatString="{0:N2}"
                            UniqueName="Nca_Iva">
                            <HeaderStyle HorizontalAlign="Center" Width="70px"  />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nca_Total" HeaderText="Total" DataFormatString="{0:N2}"
                            UniqueName="Nca_Total">
                            <HeaderStyle HorizontalAlign="Center" Width="70px"  />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nca_Pagos" HeaderText="Tiene pagos" UniqueName="Nca_Pagos">
                            <HeaderStyle HorizontalAlign="Center" Width="70px"  />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Factura" HeaderText="Factura" UniqueName="Factura"
                            Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="70px"  />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                    CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="50px" ></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Baja" ConfirmDialogType="RadWindow"
                            ConfirmText="¿Está seguro de dar de baja la devolución?" ConfirmDialogHeight="150px"
                            ConfirmDialogWidth="350px" Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton"
                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="50px" ></HeaderStyle>
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                            ConfirmText="Se imprimirá la devolución, tenga listo el formato en la impresora"
                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                            ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="50px" ></HeaderStyle>
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                    ShowPagerText="True" PageButtonCount="3" />
            </telerik:RadGrid>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------            
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Agregar':
                        AbrirVentana_Pagos(0, -1, true, true, true, true, 0);
                        args.set_cancel(true);
                        break;
                }
            }
            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent(sender, eventArgs) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                refreshGrid_Nca('RebindGrid');
            }

            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------

            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function refreshGrid_Nca(accion) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function LimpiarBanderaRebind(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind('0');
            }

            function ActivarBanderaRebind() {
                //debugger;
                ModificaBanderaRebind('1');
            }

            function ModificaBanderaRebind(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición, cuando se edita seleccionandola del grid
            //--------------------------------------------------------------------------------------------------
            function OpenAlert(mensaje, id, factura, editable) {
                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_Pagos(id, factura, false, false, false, false, editable);
                    });
                }

                function AbrirVentana_Pagos(id, factura, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, editable) {
                    AbrirVentana_Pagos(id, factura, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, editable);
            }

            function AbrirVentana_Pagos(id, factura, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, editable) {
                var oWnd = radopen("CapDevParcial.aspx?id=" + id + "&fac=" + factura + "&PermisoGuardar=" + permisoGuardar + "&PermisoModificar=" + permisoModificar + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir=" + permisoImprimir + "&editar=" + editable, "AbrirVentana_devoluciones");
                oWnd.center();
                oWnd.Maximize();
            }
            //----------------------------------------------------------------------------------------------------
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

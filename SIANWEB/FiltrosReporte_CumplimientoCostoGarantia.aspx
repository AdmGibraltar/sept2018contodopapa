<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="FiltrosReporte_CumplimientoCostoGarantia.aspx.cs" Inherits="SIANWEB.FiltrosReporte_CumplimientoCostoGarantia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            $telerik.$(document).ready(function () {
                $telerik.$('#<%=rbDetalleRemisiones.ClientID %>').click(function () {
                    if ($telerik.$(this).is(':checked')) {
                        /*$telerik.$('[data-filtergroup="fecha-monitoreo-desviaciones"]').slideUp({ complete: function () {
                            $telerik.$('[data-filtergroup="fecha-detalle-remisiones"]').slideDown();
                        }
                        });*/
                    }
                });

                $telerik.$('#<%=rbMonitoreoDesviaciones.ClientID %>').click(function () {
                    if ($telerik.$(this).is(':checked')) {
                        /*$telerik.$('[data-filtergroup="fecha-detalle-remisiones"]').slideUp({ complete: function () {
                            $telerik.$('[data-filtergroup="fecha-monitoreo-desviaciones"]').slideDown();
                        } 
                        });*/
                    }
                });
            });
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Mostrar':
                        var txtCliente = null;
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        var txtTerr = null;
                        if (txtTerr != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
                        break;
                    case 'excel':
                        var txtCliente = null;
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        var txtTerr = null;
                        if (txtTerr != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
                        break;
                }
                args.set_cancel(!continuarAccion);
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
                //debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }

            function TabSelected(sender, args) {
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                GetRadWindow().BrowserWindow.AbrirReporte();
            }

            function refreshGrid() {
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), null);
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, null);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:ScriptManagerProxy ID="rsmMainProxy" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl"
            TabIndex="10" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

        
    </div>

    <table border="0" style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <asp:Label runat="server" Text="Territorio" ID="lblTerritorio"></asp:Label>
                </td>
                <td colspan="3">
                    <telerik:RadTextBox runat="server" id="rtbTerritorio">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblNoCliente" Text="No. Cliente"></asp:Label>
                </td>
                <td colspan="3">
                    <telerik:RadTextBox runat="server" id="rtbCliente"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblNombreCliente" Text="Nombre del Cliente"></asp:Label>
                </td>
                <td colspan="3">
                    <telerik:RadTextBox runat="server" id="rbtNombreCliente"></telerik:RadTextBox>
                </td>
            </tr>
            <tr id="trFechaDetalleRemisionesAno">
                <td>
                    <div data-filtergroup="fecha-detalle-remisiones">
                        <asp:Label runat="server" ID="lblAno" Text="Año"></asp:Label>
                    </div>
                </td>
                <td colspan="3">
                    <div data-filtergroup="fecha-detalle-remisiones">
                        <telerik:RadComboBox runat="server" id="rcbFechaDetalleRemisionesAno"></telerik:RadComboBox>
                    </div>
                </td>
            </tr>
            <tr id="trFechaDetalleRemisionesMes">
                <td>
                    <div data-filtergroup="fecha-detalle-remisiones">
                        <asp:Label runat="server" ID="lblMes" Text="Mes"></asp:Label>
                    </div>
                </td>
                <td colspan="3">
                    <div data-filtergroup="fecha-detalle-remisiones">
                        <telerik:RadComboBox runat="server" id="rcbFechaDetalleRemisionesMes"></telerik:RadComboBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblAsesor" Text="RIK/ RSC / Asesor"></asp:Label>
                </td>
                <td colspan="3">
                    <telerik:RadComboBox runat="server" id="rcbAsesor"></telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:Label runat="server" ID="lblTipoGarantia" Text="Tipo de Garantía"></asp:Label>
                    </div>
                </td>
                <td colspan="3">
                    <div id="dvGarantias">
                        <asp:CheckBoxList runat="server" ID="chklstGarantias" DataTextField="TG_Nombre" DataValueField="Id_TG">
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function rcbTipoVenta_clientSelectedIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                if (item.get_value() == 1) {
                    $telerik.$('[seccionTipoGarantias]').slideUp();
                }
                if (item.get_value() == 2) {
                    $telerik.$('[seccionTipoGarantias]').slideDown();
                }
            }
        </script>
        <br />
        <table>
            <tr>
                <td id="tdDetalleRemisionesChk">
                    <asp:RadioButton runat="server" ID="rbDetalleRemisiones" GroupName="OpcionesReporte" Text="Detalle Remisiones" />
                </td>
            </tr>
            <tr>
                <td id="tdMonitoreoDesviacionesChk">
                    <asp:RadioButton runat="server" ID="rbMonitoreoDesviaciones" GroupName="OpcionesReporte" Text="Monitoreo Desviaciones" />
                </td>
            </tr>
        </table>

    <asp:HiddenField ID="HF_ClvPag" runat="server" />
</asp:Content>

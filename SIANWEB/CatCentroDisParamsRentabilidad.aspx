<%@ Page Title="Parámetros de rentabilidad" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CatCentroDisParamsRentabilidad.aspx.cs" Inherits="SIANWEB.CatCentroDisParamsRentabilidad" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
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

            function CloseWindow(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                //debugger;
                                //GetRadWindow().Close();
                                CloseAndRebind();
                            });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }
        </script>
    </telerik:radcodeblock>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <telerik:radajaxmanager id="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="formularioMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radtoolbar id="RadToolBar1" runat="server" width="100%" dir="rtl" onbuttonclick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
        </Items>
    </telerik:radtoolbar>
    <div class="formulario">
        <div runat="server" id="divPrincipal" style="margin-left: 10px; margin-right: 10px;
            margin-top: 10px;">
            <telerik:radajaxpanel id="ajaxFormPanel" runat="server" loadingpanelid="RadAjaxLoadingPanel1">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                <asp:HiddenField ID="HD_Folio" runat="server" Value="" />
                <table width="600px">
                    <tr style="width:50%">
                        <td>
                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label>
                        </td>
                        <td height="25px">
                            <asp:Label ID="lblEmpresaNombre" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal"></asp:Label>
                        </td>
                        <td height="25px">
                            <asp:Label ID="lblSucursalNombre" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr style="width:50%">
                        <td>
                            <asp:Label ID="lblRegion" runat="server" Text="Región"></asp:Label>
                        </td>
                        <td height="25px">
                            <asp:Label ID="lblRegionNombre" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                        </td>
                        <td height="25px">
                            <asp:Label ID="lblClienteNombre" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td height="25px" style="background-color:#F2F2F2; text-align:center">
                            <asp:Label ID="lblParams" runat="server" Font-Bold="true" Text="Parámetros de rentabilidad generales"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="formularioMain" runat="server">
                                <table border="0">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label64" runat="server" Text="Valor CD" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Valor STD" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Valor CD" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Valor STD" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Tasa de cetes" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCetesCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCetesEstandar" runat="server" Width="70px" MaxLength="4"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="I.V.A. %" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtIvaCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtIvaEstandar" runat="server" Width="70px" MaxLength="2"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Días de cuentas por cobrar" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCuentasCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCuentasEstandar" runat="server" Width="70px" MaxLength="3"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Flete %" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFleteCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFleteEstandar" runat="server" Width="70px" MaxLength="2"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Días" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtDiasCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtDiasEstandar" runat="server" Width="70px" MaxLength="2"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Comisión RIK %" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtComisionCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtComisionEstandar" runat="server" Width="70px" MaxLength="2"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="D&iacute;as de inventario en consignaci&oacute;n" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtInventarioCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtInventarioEstandar" runat="server" Width="70px"
                                                MaxLength="2" NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Otros gastos variables %" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtOtrosCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtOtrosEstandar" runat="server" Width="70px" MaxLength="2"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="Factor de inventario en comodatos" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFactorInvCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFactorInvEstandar" runat="server" Width="70px"
                                                MaxLength="10" NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="Contribución a gastos fijos otros %" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtGastofijoCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtGastofijoEstandar" runat="server" Width="70px"
                                                MaxLength="2" NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Factor de inversión en activos fijos" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFactorConCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFactorConEstandar" runat="server" Width="70px"
                                                MaxLength="9" NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Contribución a gastos fijos de papel %" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtGastofijopapelCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtGastofijopapelEstandar" runat="server" Width="70px"
                                                MaxLength="2" NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Días financiamiento a proveedores" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFinanciamientoCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtfinanciamientoEstandar" runat="server" Width="70px"
                                                MaxLength="3" NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="ISR y PTU %" />
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtIsrCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtIsrEstandar" runat="server" Width="70px" MaxLength="2"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Tasa incremental al costo de capital" />
                                        </td>
                                        <td style="margin-left: 40px">
                                            <telerik:RadNumericTextBox ID="txtTasaCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTasaEstandar" runat="server" Width="70px" MaxLength="2"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="Cargo de UCS's %" />
                                        </td>
                                        <td style="margin-left: 40px">
                                            <telerik:RadNumericTextBox ID="txtCargoCd" runat="server" Width="70px" MaxLength="6"
                                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCargoEstandar" runat="server" Width="70px" MaxLength="2"
                                                NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                                MinValue="0">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </telerik:radajaxpanel>
        </div>
    </div>
</asp:Content>

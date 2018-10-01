<%@ Page Title="Parámetros de rentabilidad" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CatCentroDisParamsValuacion.aspx.cs" Inherits="SIANWEB.CatCentroDisParamsValuacion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
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
                var cerrarWindow = radalert(mensaje, 300, 10, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                CloseAndRebind();
                            });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                GetRadWindow().Close();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }
            function SoloNumerico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if (c && c == 13)
                    eventArgs.set_cancel(true);
                if (c < 48 || c > 57) //si no es numero
                    eventArgs.set_cancel(true);
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
                <table><tr>
                <td align="center" colspan="5">
                    <asp:Label ID="Label67" runat="server" 
                        Text="DATOS ACERCA DE LA PART. DE UTILIDADES Y DEL ACYS" Font-Bold="True"></asp:Label>
                    </td></tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label66" runat="server" Text="Vigencia del ACYS (en años)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtVigencia" Runat="server" MaxLength="4" 
                                MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label65" runat="server" 
                                Text="Proporción de la participación de utilidades reconocida al RIK (%)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtComision" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5" class="style1">
                            <asp:Label ID="Label68" runat="server" 
                                Text="GASTOS EXTRAS PARA PODER SERVIR AL CLIENTE" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label70" runat="server" Text="Mano de obra en proyectos"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtManoObra" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label71" runat="server" 
                                Text="Amortización de equipos arrendados"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtAmortizacion" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label93" runat="server" Text="Número de entregas"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtNumEntregas" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label94" runat="server" Text="Costo de entregas"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtCostoEntregas" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label72" runat="server" Text="% de comisión por factoraje"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtComisionFactoraje" Runat="server" 
                                MaxLength="9" MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label73" runat="server" 
                                Text="% de comisión por cruce de andén (por surtir a través de un CEDIS)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtComisionCruce" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label69" runat="server" 
                                Text="Gasto de flete entregas locales (% s/costo)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtFleteLocales" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <asp:Label ID="Label75" runat="server" 
                                Text="INFORMACIÓN PARA DETERMINAR ACTIVOS NETOS DE OP'N." Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label76" runat="server" 
                                Text="IVA con el cual factura el CDS-CDC (%)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtIva" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label82" runat="server" 
                                Text="Plazo de pago del cliente (en días)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtPlazoPago" Runat="server" MaxLength="4" 
                                MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                 <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label77" runat="server" Text="Inventarios KEY (en días)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtInventarioKey" Runat="server" MaxLength="4" 
                                MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                 <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label87" runat="server" 
                                Text="Inventarios a consignación KEY (en días)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtInventarioKeyConsignacion" Runat="server" 
                                MaxLength="4" MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                 <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label78" runat="server" 
                                Text="Inventarios PROVEEDORES DE PAPEL (en días)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtProveedorPapel" Runat="server" MaxLength="4" 
                                MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                 <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label83" runat="server" 
                                Text="Inventarios PROVEEDORES DE PAPEL a consignación (en días)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtProveedorPapelConsignacion" Runat="server" 
                                MaxLength="4" MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                 <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label79" runat="server" 
                                Text="Crédito del proveedor KEY (en días)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtCreditoProveedor" Runat="server" 
                                MaxLength="4" MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                 <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label84" runat="server" 
                                Text="Crédito del proveedor de PAPEL (en días)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtCreditoProveedorPapel" Runat="server" 
                                MaxLength="4" MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                 <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label80" runat="server" Text="ISR y PTU (%)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtIsr" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label85" runat="server" Text="UCS's (%)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtUcs" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label81" runat="server" Text="Cetes (%)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtCetes" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label86" runat="server" Text="% Adicional a cetes"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtAdicionalCetes" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <asp:Label ID="Label88" runat="server" Text="GASTOS FIJOS" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label89" runat="server" 
                                Text="Contribución a los costos fijos (%  sobre venta NO PAPEL)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtCostosFijosNoPapel" Runat="server" 
                                MaxLength="9" MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label90" runat="server" 
                                Text="Contribución a los costos fijos (%  sobre venta de PAPEL)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtCostosFijosPapel" Runat="server" 
                                MaxLength="9" MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label95" runat="server" Text="Gastos administrativos"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtGAdmitivos" Runat="server" MaxLength="9" 
                                MinValue="0" Width="80px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <asp:Label ID="Label91" runat="server" Text="INVERSIÓN EN ACTIVOS" 
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label92" runat="server" 
                                Text="Inversión en activos fijos (días de venta en promedio)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtInversionActivosFijos" Runat="server" 
                                MaxLength="4" MinValue="0" Width="80px">
                                <numberformat decimaldigits="0" groupseparator="" />
                                 <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="300">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td width="250">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </telerik:radajaxpanel>
        </div>
    </div>
</asp:Content>

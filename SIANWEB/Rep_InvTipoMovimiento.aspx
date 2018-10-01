<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="Rep_InvTipoMovimiento.aspx.cs" Inherits="SIANWEB.Rep_InvTipoMovimiento" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
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
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
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
                            <td width="100">
                            </td>
                            <td width="75">
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="75">
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRel0" Text="Fecha inicial" runat="server"></asp:Label>
                            </td>
                            <td width="75">
                                <telerik:RadDatePicker ID="txtFechaIni" runat="server" Width="120px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="10">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechaIni"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblRel1" Text="Fecha final" runat="server"></asp:Label>
                            </td>
                            <td width="100">
                                <telerik:RadDatePicker ID="txtFechaFin" runat="server" Width="120px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="10">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFechaFin"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRel2" Text="T. movimiento" runat="server"></asp:Label>
                            </td>
                            <td width="75" colspan="4" style="width: 85px">
                                <telerik:RadComboBox ID="cmbTmovimiento" runat="server" Width="150px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="-- Todos --" Value="0" Owner="cmbTmovimiento" />
                                        <telerik:RadComboBoxItem runat="server" Text="Entradas de almacén" Value="1" Owner="cmbTmovimiento" />
                                        <telerik:RadComboBoxItem runat="server" Text="Salidas de almacén" Value="2" Owner="cmbTmovimiento" />
                                        <telerik:RadComboBoxItem runat="server" Text="Facturas" Value="3" Owner="cmbTmovimiento" />
                                        <telerik:RadComboBoxItem runat="server" Text="Remisiones" Value="4" Owner="cmbTmovimiento" />
                                        <telerik:RadComboBoxItem runat="server" Text="Devoluciones parciales" Value="5" Owner="cmbTmovimiento" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="75" style="width: 85px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRel3" Text="Movimiento" runat="server"></asp:Label>
                            </td>
                            <td width="75">
                                <telerik:RadTextBox ID="txtMovimiento" runat="server" Width="117px" MaxLength="200">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRel4" Text="Estatus" runat="server"></asp:Label>
                            </td>
                            <td width="75">
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="120px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="-- Todos --" Owner="cmbEstatus" />
                                        <telerik:RadComboBoxItem runat="server" Text="Capturado" Value="C" Owner="cmbEstatus" />
                                        <telerik:RadComboBoxItem runat="server" Text="Impreso" Value="I" Owner="cmbEstatus" />
                                        <telerik:RadComboBoxItem runat="server" Text="Baja" Value="B" Owner="cmbEstatus" />
                                        <telerik:RadComboBoxItem runat="server" Text="Embarque" Value="E" Owner="cmbEstatus" />
                                        <telerik:RadComboBoxItem runat="server" Text="Entregado" Value="N" Owner="cmbEstatus" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRel5" Text="Tipo de producto" runat="server"></asp:Label>
                            </td>
                            <td width="75">
                                <telerik:RadComboBox ID="cmbTproducto" runat="server" Width="120px">
                                    <Items>
                                        <%--<telerik:RadComboBoxItem runat="server" Text="-- Todos --" Value="0" Owner="cmbTproducto" />
                                        <telerik:RadComboBoxItem runat="server" Text="Accesorios" Value="1" Owner="cmbTproducto" />
                                        <telerik:RadComboBoxItem runat="server" Text="Otros" Value="3" Owner="cmbTproducto" />
                                        <telerik:RadComboBoxItem runat="server" Text="Productos" Value="2" Owner="cmbTproducto" />--%>
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRel" Text="Productos" runat="server"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtProductos" runat="server" Width="117px" MaxLength="200">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                            </td>
                            <td>
                                &nbsp;
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
                        <tr  runat="server" id="Nivel" >
                            <td colspan="2">
                                <asp:Label ID="LblNivel" runat="server" Text="Nivel"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr  runat="server" id="Nivel_Producto">
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbProducto" runat="server" Text="Producto" Checked="true" GroupName="Nivel" OnCheckedChanged= "rbProducto_CheckedChanged" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr  runat="server" id="Nivel_Cliente">
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbDocumento" runat="server" Text="Documento" GroupName="Nivel"  OnCheckedChanged= "rbDocumento_CheckedChanged" AutoPostBack="true" />
                            </td>
                        </tr>                                                               
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            function refreshGrid() {
            }

            function OnClientClicked(button, args) {
                if (window.confirm("Are you sure you want to submit the page?")) {
                    button.set_autoPostBack(true);
                }
                else {
                    button.set_autoPostBack(false);
                }
            }

                     
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

<%@ Page Title="Productividad sistemas propietarios" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_SerProductividadSistemas.aspx.cs" Inherits="SIANWEB.Rep_SerProductividadSistemas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;

                var conntinuar = true;

                var HD_tipoPeriodo = document.getElementById('<%= HD_tipoPeriodo.ClientID %>');
                var val_txtFecha = document.getElementById('<%= val_txtFecha.ClientID %>');
                var val_cmbMesInicio = document.getElementById('<%= val_cmbMesInicio.ClientID %>');
                var cmbMesInicio = $find('<%= cmbMesInicio.ClientID %>');
                var val_cmbAnioFin = document.getElementById('<%= val_cmbAnioFin.ClientID %>');
                var cmbAnioFin = $find('<%= cmbAnioFin.ClientID %>');
                var val_cmbMesFin = document.getElementById('<%= val_cmbMesFin.ClientID %>');
                var cmbMesFin = $find('<%= cmbMesFin.ClientID %>');
                var val_cmbAnioInicio = document.getElementById('<%= val_cmbAnioInicio.ClientID %>');
                var cmbAnioInicio = $find('<%= cmbAnioInicio.ClientID %>');

                //                if (HD_tipoPeriodo.value == 'CALENDARIO') {
                //                    var txtFecha = $find('<%= txtFecha.ClientID %>');

                //                    //realizar validaciones
                //                    var fechaInicio = null;

                //                    fechaInicio = txtFecha._dateInput.get_selectedDate();

                //                    //validar rango correcto de fechas.
                //                    if (fechaInicio == null) {
                //                        val_txtFecha.innerHTML = '*Requerido';
                //                        conntinuar = false
                //                    }
                //                }
                //                else {
                if (cmbMesInicio != null) {
                    if (cmbMesInicio.get_value() == '-1') {
                        val_cmbMesInicio.innerHTML = '*Requerido';
                        conntinuar = false
                    }
                    else {
                        val_cmbMesInicio.innerHTML = ''
                    }
                }
                //                    if (cmbAnioFin != null) {
                //                        if (cmbAnioFin.get_value() == '-1') {
                //                            val_cmbAnioFin.innerHTML = '*Requerido';
                //                            conntinuar = false
                //                        }
                //                        else {
                //                            val_cmbAnioFin.innerHTML = ''
                //                        }
                //                    }
                //                    if (cmbMesFin != null) {
                //                        if (cmbMesFin.get_value() == '-1') {
                //                            val_cmbMesFin.innerHTML = '*Requerido';
                //                            conntinuar = false
                //                        }
                //                        else {
                //                            val_cmbMesFin.innerHTML = ''
                //                        }
                //                    }
                if (cmbAnioInicio != null) {
                    if (cmbAnioInicio.get_value() == '-1') {
                        val_cmbAnioInicio.innerHTML = '*Requerido';
                        conntinuar = false
                    }
                    else {
                        val_cmbAnioInicio.innerHTML = ''
                    }
                    //                    }

                }

                return conntinuar;
            }

            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }


            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="filtros" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" ValidationGroup="print"
                    CssClass="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HF_ClvPag" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div id="filtros" runat="server">
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table border="0">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label7" runat="server" Text="Grupo" visible = "true"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtGrupo" onpaste="return false" runat="server" Width="200px" visible = "true"
                                        MaxLength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label1" runat="server" Text="Representante" visible = "true"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtRepresentante" onpaste="return false" runat="server" Width="200px" visible = "true"
                                        MaxLength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label3" runat="server" Text="Cliente" visible = "true"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtCliente" onpaste="return false" runat="server" Width="200px" visible = "true"
                                        MaxLength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <table runat="server" visible="false">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="rbFecha" runat="server" AutoPostBack="true">
                                        <asp:ListItem Text="Fecha" Value="CALENDARIO"></asp:ListItem>
                                        <asp:ListItem Text="Mensual" Value="MENSUAL" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:HiddenField ID="HD_tipoPeriodo" runat="server" Value="CALENDARIO" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr runat="server" visible="false">
                                <td colspan="2">
                                    <asp:Label ID="Label4" runat="server" Text="Fecha"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha" runat="server" Width="100px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:Label ID="val_txtFecha" runat="server" ForeColor="#FF0000"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Año"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Mes"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label8" runat="server" Text="Periodo"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbAnioInicio" runat="server" Width="120px" Height="150px">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="val_cmbAnioInicio" runat="server" ForeColor="#FF0000"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbMesInicio" runat="server" Width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                            <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                            <telerik:RadComboBoxItem Text="Marzo" Value="3" />
                                            <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                            <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                            <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                            <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                            <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                            <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                            <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                            <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                            <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="val_cmbMesInicio" runat="server" ForeColor="#FF0000"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td colspan="2">
                                    <asp:Label ID="Label9" runat="server" Text="Periodo final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbAnioFin" runat="server" Width="120px" Height="150px">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="val_cmbAnioFin" runat="server" ForeColor="#FF0000"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbMesFin" runat="server" Width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                            <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                            <telerik:RadComboBoxItem Text="Marzo" Value="3" />
                                            <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                            <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                            <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                            <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                            <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                            <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                            <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                            <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                            <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="val_cmbMesFin" runat="server" ForeColor="#FF0000"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label10" runat="server" Text="Orden"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadComboBox ID="cmbOrden" runat="server" Width="300px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-- Seleccionar --" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Grupo de sistemas propietarios" Value="1" visible = "true" />
                                            <telerik:RadComboBoxItem Text="Equipos instalados" Value="2" visible = "true" />
                                            <telerik:RadComboBoxItem Text="Productividad" Value="3" visible = "true"/>
                                            <telerik:RadComboBoxItem Text="Variación en pesos de unidades  no facturadas" Value="4" visible = "true"/>
                                            <telerik:RadComboBoxItem Text="PAP" Value="5" visible = "true"/>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="val_cmbPrecio" ControlToValidate="cmbOrden"
                                        ErrorMessage="*Requerido" InitialValue="-- Seleccionar --" ValidationGroup="print"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Agrupar por sistema propietario" Checked ="true" Enabled="true" />
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
                                    <%-- <asp:CheckBox ID="chkGrupo" runat="server" Text="Grupo" />--%>
                                </td>
                                <td colspan="2">
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
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td colspan="3">
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

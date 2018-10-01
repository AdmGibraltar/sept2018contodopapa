<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_VenRentabilidad.aspx.cs" Inherits="SIANWEB.Rep_VenRentabilidad" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;
                var conntinuar = true;

                var lbl_ValCliente = document.getElementById('<%= lbl_ValCliente.ClientID %>');

                var lbl_ValTerritorio = document.getElementById('<%= lbl_ValTerritorio.ClientID %>');
                

                //                if (cmbTerritorio != null)
                //                    if (cmbTerritorio.get_value() == '-1') {
                //                        lbl_ValTerritorio.innerHTML = '*Requerido';
                //                        conntinuar = false
                //                    }
                //                    else {
                //                        lbl_ValTerritorio.innerHTML = ''
                //                    }

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



                if (continuarAccion == true) {
                    continuarAccion = ValidacionesEspeciales();
                }


                args.set_cancel(!continuarAccion);
            }



            //cuando el campo de texto pirde el foco
            function txtCliente_OnBlur(sender, args) {

            }

            //cuando se selecciona un Item del combo
            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }




            //cuando se selecciona un Item del combo
            function cmbTerritorio_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }


            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de parametros de resntabilidad
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_CentroDisParamsRentabilidad(Id_Emp, Id_Cd, Id_Vap_Editar, Id_Cte, Cte_NomComercial) {
                //AbrirVentana_CentroDisParamsRentabilidad_Open(Id_Emp, Id_Cd, Id_Vap_Editar, Id_Cte, Cte_NomComercial, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                AbrirVentana_CentroDisParamsRentabilidad_Open(Id_Emp, Id_Cd, Id_Vap_Editar, Id_Cte, Cte_NomComercial, 1, 1, 1, 1);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de valuacion de proyectos
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_CentroDisParamsRentabilidad_Open(Id_Emp, Id_Cd, Id_Vap, Id_Cte, Cte_NomComercial, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                //debugger;
                var oWnd = radopen("CatCentroDisParamsRentabilidad.aspx?Id_Vap=" + Id_Vap
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&Id_Cte=" + Id_Cte
                    + "&Cte_NomComercial=" + Cte_NomComercial
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    , "AbrirVentana_CentroDistParametrosRentabilidad");
                oWnd.center();
            }

            function popup() {

                var window_dimensions = "toolbar=no,menubar=no,directories=no,location=no,scrollbars=1,resizable=1,status=no,width=900,height=400"
                window.open("CapMonitoreoGestionRentabilidad.aspx"
                , "_blank", window_dimensions);
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divResumen" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" 
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" CssClass="print"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
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
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td width="70">
                        <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MinValue="1"
                            MaxLength="9" AutoPostBack="True" OnTextChanged="txtCliente_TextChanged">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            <ClientEvents OnBlur="txtCliente_OnBlur" OnKeyPress="handleClickEvent" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_ValCliente" runat="server" ForeColor="#FF0000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Territorio"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" Width="70px" MaxLength="9"
                            MinValue="1">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />            
                            <ClientEvents OnBlur="txtTerritorio_OnBlur"  OnKeyPress="handleClickEvent" />                
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbTer" runat="server" AutoPostBack="True" Width="300px" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            EnableLoadOnDemand="True" Filter="Contains" 
                                                            OnClientSelectedIndexChanged="cmbTer_ClientSelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_ValTerritorio" runat="server" ForeColor="#FF0000"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td width="70">
                        <asp:Label ID="Label4" runat="server" Text="Periodo"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbPeriodo" runat="server" Width="130px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-- Seleccionar --" />
                                <telerik:RadComboBoxItem runat="server" Text="Mes anterior" Value="Mensual anterior" />
                                <telerik:RadComboBoxItem runat="server" Text="Promedio trimestral anterior" Value="Trimestral anterior" />
                                <telerik:RadComboBoxItem runat="server" Text="Promedio semestral anterior" Value="Semestral anterior" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator runat="server" ID="val_cmbPeriodo" ControlToValidate="cmbPeriodo"
                            ErrorMessage="*Requerido" InitialValue="-- Seleccionar --" ValidationGroup="print"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Ventas"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbVentas" runat="server" Width="130px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-- Seleccionar --" />
                                <telerik:RadComboBoxItem runat="server" Text="Sistemas propietarios" Value="Sistemas propietarios" />
                                <telerik:RadComboBoxItem runat="server" Text="Integral" Value="Integral" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator runat="server" ID="val_cmbVentas" ControlToValidate="cmbVentas"
                            ErrorMessage="*Requerido" InitialValue="-- Seleccionar --" ValidationGroup="print"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Periodo de Cierre"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbPeriodoCierre" runat="server" Width="130px" OnSelectedIndexChanged="cmbPeriodoCierre_SelectedIndexChanged" AutoPostBack="True">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="" />
                            </Items>
                        </telerik:RadComboBox>&nbsp;<asp:Label ID="MesesConsiderados" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="cmbPeriodoCierre"
                            ErrorMessage="*Requerido" InitialValue="" ValidationGroup="print"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

            </table>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
    </div>
    <div id="divResumen" runat="server">
    </div>
</asp:Content>

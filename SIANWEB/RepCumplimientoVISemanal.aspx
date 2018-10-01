<%@ Page Title="Cumplimiento de Venta Instalada Semanal" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="RepCumplimientoVISemanal.aspx.cs" Inherits="SIANWEB.RepCumplimientoVISemanal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
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

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$RadToolBar1") != -1)
                    args.set_enableAjax(false);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="cmbanio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="cmbmes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                 <telerik:AjaxSetting AjaxControlID="RblTipoCd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="cmbCDI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
         <ClientEvents OnRequestStart="onRequestStart" />
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
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
                            <td>
                                <asp:Label ID="Label3" Text="Año" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox runat ="server" ID="TxtAnio" 
                                     Width="70px" MinValue="2011" MaxLength="4" >
                                      <NumberFormat GroupSeparator="" DecimalDigits="0"></NumberFormat>
                                </telerik:RadNumericTextBox>
                                <telerik:RadComboBox ID="RadComboBoxAño" runat="server" OnSelectedIndexChanged="RadComboBoxAño_SelectedIndexChanged"
                                    AutoPostBack="True" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" visible="false">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelMes" runat="server" Text="Mes"></asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox runat ="server" ID="TxtMes" Visible="false"
                                        Width="70px" MaxValue="12" MinValue="1" MaxLength="2">
                                         <NumberFormat GroupSeparator="" DecimalDigits="0"></NumberFormat>
                                </telerik:RadNumericTextBox>
                                <telerik:RadComboBox ID="cmbMes" runat="server" AllowCustomText="False" SelectedValue='<%# Bind("Cal_Mes") %>'
                                    Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbMes_SelectedIndexChanged"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" />
                                        <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" />
                                        <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" />
                                        <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" />
                                        <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" />
                                        <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" />
                                        <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" />
                                        <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" />
                                        <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" />
                                        <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" />
                                        <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label4" Text="Selecione las Semanas" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <asp:CheckBoxList runat="server" ID="chklstSemanas" AutoPostBack="false" 
                                            CellSpacing="2" CellPadding="2" Width="600px"/>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <%--
                        <tr>
                            <td>
                                <asp:Label ID="Label5" Text="UEN" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbUEN" runat="server" Width="200px" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                                <asp:Label ID="Label7" Text="Segmento" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbSegmento" runat="server" Width="200px" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr> --%>
                            <tr>
                            <td>
                                <asp:Label ID="Label8" Text="RIK/ RSC / Asesor" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbRIK" runat="server" Width="200px" EnableLoadOnDemand="True" Filter="Contains" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                            <tr>
                            <td>
                                <asp:Label ID="Label9" Text="Territorio" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTer" runat="server" Width="200px" 
                                    EnableLoadOnDemand="True" Filter="Contains" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" Text="Cliente" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                           <td colspan="5">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>                                        
                                        <td style="padding-right:5px">
                                            <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" Width="125px" MinValue="1" MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>                                    
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false" Enabled="false">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>                                    
                                </table>
                            </td>
                        </tr>
                              <tr>
                            <td>
                              
                            </td>
                            <td>
                               &nbsp;<telerik:RadComboBox ID="cmbCte" runat="server" Width="10px" EnableLoadOnDemand="True" Filter="Contains" Visible="false"></telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                                <asp:Label ID="Label2" Text="Tipo de Reporte:" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                               <asp:RadioButtonList runat="server" ID="RblTipo" RepeatDirection="Horizontal" 
                                    Width="150px">
                                   <asp:ListItem selected="true" Text = "General" Value ="1"></asp:ListItem>
                                   <asp:ListItem  Text = "Detalle" Value ="2"></asp:ListItem>                             
                               </asp:RadioButtonList>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                              
                            </td>
                            <td>
                               &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                           <tr>
                            <td>
                              
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>
               

                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="HF_Cve" runat="server" />
                </td>
            </tr>
        </table>
         
          <table style="font-family: Verdana; font-size: 8pt; position:absolute; top:23px;left:45px;">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
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
                            <td>
                                <asp:Label ID="Label1" Text="Aguascalientes" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                       <%--     <td>
                                <telerik:RadNumericTextBox runat ="server" ID="TxtAnio" 
                                     Width="70px" MinValue="2011" MaxLength="4">
                                      <NumberFormat GroupSeparator="" DecimalDigits="0"></NumberFormat>
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                               <tr>
                            <td>
                                <asp:Label ID="Label2" Text="Venta Mes" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                   <telerik:RadNumericTextBox runat ="server" ID="TxtMes" 
                                        Width="70px" MaxValue="12" MinValue="1" MaxLength="2">
                                         <NumberFormat GroupSeparator="" DecimalDigits="0"></NumberFormat>
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
              
              
                              <tr>
                            <td>
                                <asp:Label ID="Label11" Text="UEN" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbUEN" runat="server" Width="200px" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                                <asp:Label ID="Label12" Text="Segmento" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbSegmento" runat="server" Width="200px" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                            <tr>
                            <td>
                                <asp:Label ID="Label13" Text="RIK/ RSC / Asesor" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbRIK" runat="server" Width="200px" EnableLoadOnDemand="True" Filter="Contains" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                            <tr>
                            <td>
                                <asp:Label ID="Label14" Text="Territorio" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTer" runat="server" Width="200px" 
                                    EnableLoadOnDemand="True" Filter="Contains" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                            <tr>
                            <td>
                                <asp:Label ID="Label15" Text="Cliente" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbCte" runat="server" Width="200px" EnableLoadOnDemand="True" Filter="Contains" >
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                              
                            </td>
                            <td>
                               &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                              
                            </td>
                            <td>
                               <asp:RadioButtonList runat="server" ID="RadioButtonList1" RepeatDirection="Horizontal" 
                                    Width="150px">
                               <asp:ListItem selected="true" Text = "General" Value ="1"></asp:ListItem>
                               <asp:ListItem  Text = "Detalle" Value ="2"></asp:ListItem>
                             
                               </asp:RadioButtonList>
                            </td>
                            <td>
                            </td>
                        </tr>
                              <tr>
                            <td>
                              
                            </td>
                            <td>
                               &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                           <tr>
                            <td>
                              
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>
               
               --%>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

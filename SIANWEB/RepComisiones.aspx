<%@ Page Title="Reporte comisiones" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="RepComisiones.aspx.cs" Inherits="SIANWEB.RepComisiones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "");
                oWnd.center();
            }
            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }


            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

            function TxtId_Rik_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRik.ClientID %>'));
            }

            function CmbId_Rik_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Rik.ClientID %>'));
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
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
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
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
             <telerik:RadToolBarButton CommandName="DwExcel" Value="DwExcel" Text="" CssClass="Excel"
                    ToolTip="Descargar Excel" ImageUrl="Imagenes/blank.png" />
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
                                <asp:Label ID="Label1" Text="Tipo de Reporte" runat="server">
                                
                                </asp:Label>&nbsp;
                            </td>
                            <td colspan= "2">
                                <telerik:RadComboBox ID="cmbTipoReporte" runat="server" Width="150px" >
                                   <Items>   
                                       <telerik:RadComboBoxItem runat="server" Text="Comisiones" Value = "" />   
                                       <telerik:RadComboBoxItem runat="server" Text="Venta Incremental" Value = "_venta_incremental" />   

                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                               <tr>
                            <td>
                                <asp:Label ID="Label3" Text="Año" runat="server">
                                
                                </asp:Label>&nbsp;
                            </td>
                            <td colspan= "2">
                                <telerik:RadComboBox ID="cmbanio" runat="server" Width="150px" >
                                   <Items>   
                                       <telerik:RadComboBoxItem runat="server" Text="2014" Value = "2014" />   
                                       <telerik:RadComboBoxItem runat="server" Text="2015" Value = "2015" />   
                                       <telerik:RadComboBoxItem runat="server" Text="2016" Value = "2016" />  
                                       <telerik:RadComboBoxItem runat="server" Text="2017" Value = "2017" />  
                                       <telerik:RadComboBoxItem runat="server" Text="2018" Value = "2018" />  
                                       <telerik:RadComboBoxItem runat="server" Text="2019" Value = "2019" />  
                                       <telerik:RadComboBoxItem runat="server" Text="2020" Value = "2020" />  
                                       <telerik:RadComboBoxItem runat="server" Text="2021" Value = "2021" />  
                                       <telerik:RadComboBoxItem runat="server" Text="2022" Value = "2022" />  
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                               <tr>
                            <td>
                                <asp:Label ID="Label4" Text="Mes" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbmes" runat="server" Width="150px" >
                                       <Items>   
                                       <telerik:RadComboBoxItem runat="server" Text="Enero" Value = "01" />   
                                       <telerik:RadComboBoxItem runat="server" Text="Febrero" Value = "02" />   
                                       <telerik:RadComboBoxItem runat="server" Text="Marzo" Value = "03" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Abril" Value = "04" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Mayo" Value = "05" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Junio" Value = "06" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Julio" Value = "07" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Agosto" Value = "08" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value = "09" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Octubre" Value = "10" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value = "11" />  
                                       <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value = "12" />  
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                                  <tr runat ="server" id="trRik">
                            <td>
                               <asp:Label ID="LblId_Rik" Text="Representante" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                             <telerik:RadNumericTextBox ID="TxtId_Rik" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px" >
                                                       <ClientEvents OnBlur="TxtId_Rik_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                
                                </telerik:RadNumericTextBox>
                        </td>
                        <td>
                          <telerik:RadComboBox ID="cmbRik" runat="server" Width="250px" OnClientBlur="Combo_ClientBlur" 
                                                         OnClientSelectedIndexChanged="CmbId_Rik_ClientSelectedIndexChanged" >
                                </telerik:RadComboBox>
                         
                            </td>
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
    </div>
</asp:Content>

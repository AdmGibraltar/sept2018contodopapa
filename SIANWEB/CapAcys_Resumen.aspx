<%@ Page Title="Cumplimiento de Venta Instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapAcys_Resumen.aspx.cs" Inherits="SIANWEB.CapAcys_Resumen" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RcGrafica_SeriesClicked(sender, eventArgs) {
                var tipo = eventArgs.get_category();
                var seleccion = tipo.substr(0, 7);
                alert("You clicked on a series item with value '" + eventArgs.get_value() + "' from category '" + eventArgs.get_category() + "'.");
//                if (seleccion == "Cobrado")
//                    window.location.href = "Rep_ClienteCuentasPagadas.aspx";
//                else
//                    window.location.href = "Rep_ClientesCuentasPorPagar.aspx";
            }
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
                if (args.get_eventTarget().indexOf("CPH_ctl00$CPH$lblMensajePanel") != -1)
                    args.set_enableAjax(false);
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("CPH_ctl00$CPH$divPrincipalPanel") != -1)
                    args.set_enableAjax(false);
            }




            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("CPH_ctl00$CPH$RadToolBar1") != -1)
                    args.set_enableAjax(false);
            }


            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("CPH_ctl00$CPH$div_ResumenPanel") != -1)
                    args.set_enableAjax(false);     
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
            }


            function TxtId_Rik_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRep.ClientID %>'));
            }

            function CmbRep_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtId_Rik.ClientID %>'));
            }

            function TxtCte_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbCte.ClientID %>'));
            }

            function CmbCte_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= TxtCte.ClientID %>'));
            }

            
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
                   
        
            
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
            <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="div_Resumen" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="div_Resumen" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                <telerik:RadToolBarButton CommandName="Consultar" Value="Consultar" CssClass="print" ToolTip="Buscar"
                    ImageUrl="~/Img/find16.png" />
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
                                <asp:Label ID="Label3" Text="Territorio" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                                <telerik:radtextbox id="txtTerritorio" onpaste="return false" runat="server" width="100px"
                                        maxlength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="txtTerritorio_OnBlur" />
                                    </telerik:radtextbox>    

                                <telerik:RadComboBox ID="cmbTer" runat="server" Width="200px" EnableLoadOnDemand="True" Filter="Contains" 
                                OnClientSelectedIndexChanged="cmbTer_ClientSelectedIndexChanged"                               
                                ></telerik:RadComboBox>
                             </td>                        
                            <td>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" Text="Representante" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>
                            
                                <telerik:RadNumericTextBox ID="TxtId_Rik" runat="server" MaxLength="9" MinValue="0"
                                                       Width="100px" >
                                                       <ClientEvents OnBlur="TxtId_Rik_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>


                                <telerik:RadComboBox ID="cmbRep" runat="server" Width="200px" EnableLoadOnDemand="True" Filter="Contains" 
                                OnClientSelectedIndexChanged="CmbRep_ClientSelectedIndexChanged" 
                                ></telerik:RadComboBox>
                            </td>  
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" Text="Cliente" runat="server">
                                </asp:Label>&nbsp;
                            </td>
                            <td>        
                                    <telerik:RadNumericTextBox ID="TxtCte" runat="server" MaxLength="9" MinValue="0"
                                                       Width="100px" >
                                                       <ClientEvents OnBlur="TxtCte_OnBlur"  OnKeyPress="handleClickEvent" />
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                 </telerik:RadNumericTextBox>  
                                
                                <telerik:RadComboBox ID="cmbCte" runat="server" Width="200px" EnableLoadOnDemand="True" Filter="Contains" 
                                 OnClientSelectedIndexChanged="CmbCte_ClientSelectedIndexChanged"
                                ></telerik:RadComboBox>                          
                                
                            </td>  
                        </tr>
                        <tr>
                                <td >
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="120px">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                               <%-- <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="RadDatePicker1" Display="Dynamic" ErrorMessage="*Requerido" 
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>--%>
                           </tr>
                             <tr>
                                <td> 
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Width="120px">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>&nbsp;&nbsp;
                                    <%--  <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                        ToolTip="Buscar" onclick="btnBuscar_Click"  />--%>
                                </td>
                                
                                
                                        </td>
                              
    <%--                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="RadDatePicker2" Display="Dynamic" ErrorMessage="*Requerido" 
                                        ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                       
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

    </div>

    <asp:Label left="550px" ID="LblSitio" Text="" runat="server" font-size="25pt" Font-Bold="true" align="CENTER" text-align= "Center" Visible="false"> </asp:Label>                    
    <div runat="server"  id="div_Resumen" style="position:absolute; top: 180PX; left:500PX; width:400px">     
    
            <table id="Table2" align="center">
             <td colspan=5 align=center ><font size="6">RESUMEN </font> </td> 
             <tr>
              <td style="text-align:center;">
                                <asp:Label ID="lblAutorizados_Valor" runat="server" Text="" font-size="14pt" ></asp:Label>
                            </td>
                            <td style="text-align:center;">
                                <asp:Label ID="lblCancelados_Valor" runat="server" Text="" font-size="14pt" align="CENTER" ></asp:Label>
                            </td>
                            <td  style="text-align:center;">
                                <asp:Label ID="lblCapturados_Valor" runat="server" Text="" font-size="14pt" align="CENTER" ></asp:Label>
                            </td>
                               <td  style="text-align:center;">
                                <asp:Label ID="lblRechazado_Valor" runat="server" Text="" font-size="14pt" align="CENTER" ></asp:Label>
                            </td>
                             <td style="text-align:center;">
                                <asp:Label ID="lblSolicitado_Valor" runat="server" Text="" font-size="14pt" align="CENTER" ></asp:Label>
                            </td>
            </tr>
             <tr>
            <td>
                                <asp:Label ID="lblAutorizados" runat="server" Text="Autorizados" font-size="14pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCancelados" runat="server" Text="Cancelados" font-size="14pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCapturados" runat="server" Text="Capturados" font-size="14pt"></asp:Label>
                            </td>
                               <td>
                                <asp:Label ID="lblRechazado" runat="server" Text="Rechazado" font-size="14pt"></asp:Label>
                            </td>
                             <td>
                                <asp:Label ID="lblSolicitado" runat="server" Text="Solicitado" font-size="14pt"></asp:Label>
                            </td>
                        </tr>
                       

            </table>
               
    </div>

</asp:Content>

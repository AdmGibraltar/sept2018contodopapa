<%@ Page Title="Cumplimiento de Venta Instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapAcys_Cumplimiento.aspx.cs" Inherits="SIANWEB.CapAcys_Cumplimiento" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RcGrafica_SeriesClicked(sender, eventArgs) {
                var tipo = eventArgs.get_category();
                var seleccion = tipo.substr(0, 7);
                alert("You clicked on a series item with value '" + eventArgs.get_value() + "' from category '" + eventArgs.get_category() + "'.");
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
                if (args.get_eventTarget().indexOf("ctl00$CPH$RadToolBar1") != -1)
                    args.set_enableAjax(false);
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("CPH_ctl00$CPH$div_GraficaPanel") != -1)
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
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
                               <telerik:AjaxSetting AjaxControlID="btnBuscar">
            <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="div_Grafica" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="div_Grafica" LoadingPanelID="RadAjaxLoadingPanel1" />

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

        <table style="font-family: Verdana; font-size: 8pt" width="1000">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
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
                                <td >
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Año inicial"></asp:Label>
                                </td>
                               <td>
                                    <telerik:radcombobox id="cmbAnioInicio" runat="server" width="150px">
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbAnioInicio" runat="server" ControlToValidate="cmbAnioInicio"
                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>


                                <td>
                               <%-- <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="RadDatePicker1" Display="Dynamic" ErrorMessage="*Requerido" 
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>--%>
                           </tr>
                           <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label5" Text="Mes inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radcombobox id="cmbMesInicio" runat="server" width="150px">
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
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbMesInicio" runat="server" ControlToValidate="cmbMesInicio"
                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label7" Text="Año final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radcombobox id="cmbAnioFin" runat="server" width="150px">
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbAnioFin" runat="server" ControlToValidate="cmbAnioFin"
                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>


                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label8" Text="Mes final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radcombobox id="cmbMesFin" runat="server" width="150px">
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
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbMesFin" runat="server" ControlToValidate="cmbMesFin"
                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                                          <%-- <td>  <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                        ToolTip="Buscar" onclick="btnBuscar_Click"  /></td>  --%>               
                            <td>
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
    
    <div runat="server" id="div_Grafica" style="position:absolute; top: 180PX; left:500PX; width:400px">         
            <table id="Table2" align="center">
               <tr>
                    <td>
                        <%=GeneraGraficaActividad()%>
                    </td>
                </tr> 
            </table>
       <table id="TblResumen" align="center" border="1" font-size="15pt" ridge(2,8,1) >
       <tr>
             <td  style="text-align:center "> <asp:Label ID="lblVentaMes_Valor" Text="" runat="server" font-size="11pt"   top= "30px"> </asp:Label></td>               
             <td  colspan="3" style="text-align:center" > <asp:Label ID="Label1" Text="CIERRE EN" runat="server" font-size="11pt"   top= "30px"> </asp:Label>  </td>
             <td style="text-align:center"><asp:Label ID="lblVentaInstalada_Valor" Text="" runat="server" font-size="11pt" top= "30px"></asp:Label></td>
        </tr>  
      <tr>
         <td  rowspan="2" style="text-align:center" > <asp:Label ID="lblVentaMes" Text="VENTA MES      " runat="server" font-size="11pt" top= "30px"></asp:Label> </td>         
         <td style="text-align:center"> <asp:Label ID="lblCierreDia" Text="" runat="server" font-size="11pt"  top= "30px"> </asp:Label></td>
         <td style="text-align:center"> <asp:Label ID="lblCierreHora" Text="" runat="server" font-size="11pt"  top= "30px"> </asp:Label> </td>
         <td style="text-align:center"> <asp:Label ID="lblCierreMinutos" Text="" runat="server" font-size="11pt"  top= "30px"> </asp:Label>    </td>
         <td  rowspan="2" style="text-align:center"> <asp:Label ID="lblVentaInstalada" Text="VENTA INSTALADA: " runat="server" font-size="11pt" top= "30px"></asp:Label></td>  
         
      </tr>    
       <tr >
         <td><asp:Label ID="Label2" Text="DIA(S)" runat="server" font-size="11pt" top= "30px"></asp:Label></td>
        <td><asp:Label ID="Label11" Text="HORA(S)" runat="server" font-size="11pt" top= "30px"></asp:Label></td>
        <td><asp:Label ID="Label12" Text="MINUTOS(S)" runat="server" font-size="11pt" top= "30px"></asp:Label></td>               
       
      </tr>    
         
      </table>           
    </div>

</asp:Content>

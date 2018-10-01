<%@ Page Title="Gestión precios" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapGestionPreciosP.aspx.cs" Inherits="SIANWEB.CapGestionPreciosP" %>

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

            function OpenAlert(mensaje, Id_PC, TipoOp, arg) {
                var abrirWindow = radconfirm(mensaje, 330,270, arg);

                abrirWindow.add_close(
                    function () {
                        AbrirVentana_Detalle(Id_PC, TipoOp, arg);
                    });
            }


            function callConfirm(mensaje) {
                radconfirm(mensaje, confirmCallBackFn);
            }

            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%=RAM1.ClientID%>");
                if (arg) {
                    ajaxManager.ajaxRequest('VerConvenio');
                }
                else {
                    ajaxManager.ajaxRequest('cancel');
                }
            }


            function OpenWindow(Id_PC,TipoOp) {

                AbrirVentana_Detalle(Id_PC, TipoOp);
            }
            function AbrirVentana_Detalle(Id_PC, TipoOp) {
                //debugger;
                var oWnd = radopen("CapGestionPreciosD.aspx?Id_PC=" + Id_PC + "&TipoOp=" + TipoOp, "AbrirVentana_GestionPreciosD");
                oWnd.center();
                oWnd.Maximize();
            }

            function OpenWindowDet(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr) {

                AbrirVentana_Cancelacion(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr);
            }
            function AbrirVentana_Cancelacion(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr) {
                //debugger;
                var oWnd = radopen("Ventana_BajaConvenio.aspx?Id_PC=" + Id_PC + "&PC_NoConvenio=" + PC_NoConvenio + "&PC_Nombre=" + PC_Nombre + "&Id_CatStr=" + Id_CatStr, "Ventana_BajaConvenio");
                oWnd.center();

            }
            function OpenWindowVincularSuc(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr) {

                AbrirVentana_VincularSuc(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr);
            }
            function AbrirVentana_VincularSuc(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr) {
                //debugger;
                var oWnd = radopen("Ventana_VinculaSucursal.aspx?Id_PC=" + Id_PC + "&PC_NoConvenio=" + PC_NoConvenio + "&PC_Nombre=" + PC_Nombre + "&Id_CatStr=" + Id_CatStr, "Ventana_VincularSucursal");
                oWnd.center();

            }


            function OpenWindowSolicitud(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr, Id_Sol) {

                AbrirVentana_Solicitud(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr,Id_Sol);
            }
            function AbrirVentana_Solicitud(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr, Id_Sol) {
                //debugger;
                var oWnd = radopen("CapGestionPrecios_SolicitudDet.aspx?Id_PC=" + Id_PC + "&PC_NoConvenio=" + PC_NoConvenio + "&PC_Nombre=" + PC_Nombre + "&Id_CatStr=" + Id_CatStr + "&Id_Sol=" + Id_Sol, "AbrirVentana_GPSolicitud");
                oWnd.center();
               
            }

            function OpenWindowVinculados(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr, Id_Cd) {

                AbrirVentana_Vinculados(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr, Id_Cd);
            }
            function AbrirVentana_Vinculados(Id_PC, PC_NoConvenio, PC_Nombre, Id_CatStr, Id_Cd) {
                //debugger;
                var oWnd = radopen("CapGestionPrecios_Vinculados.aspx?Id_PC=" + Id_PC + "&PC_NoConvenio=" + PC_NoConvenio + "&PC_Nombre=" + PC_Nombre + "&Id_CatStr=" + Id_CatStr + "&Id_Cd=" + Id_Cd, "AbrirVentana_Vinculados");
                oWnd.center();

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

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$RadToolBar1") != -1)
                    args.set_enableAjax(false);
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent(sender, eventArgs) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                refreshGrid_Nca('RebindGrid');
            }

            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"  ClientEvents-OnRequestStart="onRequestStart"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
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
             <telerik:AjaxSetting AjaxControlID="BtnBuscar">
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
            <telerik:RadToolBarButton CommandName="expconvenios" Value="expconvenios" ToolTip="Exportar convenios" CssClass="Excel"
                        ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="expclientes" Value="expclientes" ToolTip="Exportar clientes" CssClass="Excel"
                        ImageUrl="~/Imagenes/blank.png" />
                 <%--   <telerik:RadToolBarButton CommandName="imprimir" Value="imprimir" ToolTip="Imprimir" CssClass="print"
                ImageUrl="~/Imagenes/blank.png" />
                 <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="~/Imagenes/blank.png" />--%>
          
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
                           
                                <asp:Label ID="Label1" Text="Tipo de búsqueda" runat="server"> </asp:Label>
                           
                            </td>
                            <td>
                       
                               <asp:Label ID="Label3" Text="Vencido" runat="server"> </asp:Label>
                       
                            </td>
                            <td>
                              
                               <asp:Label ID="Label4" Text="Categoría" runat="server"> </asp:Label>
                            </td>
                            <td>
                               &nbsp;
                               <asp:Label ID="Label5" Text="Valor" runat="server"> </asp:Label>
                            </td>
                            <td>
                       
                            </td>
                        </tr>


                      <tr>
                            <td>
                                       <telerik:RadComboBox ID="CmbTipoB" MaxHeight="300px" runat="server" 
                        Width="150px">
                                           <Items>
                                               <telerik:RadComboBoxItem runat="server" Selected="True"  Text="-- Seleccionar --" Value="-1" />
                                                <telerik:RadComboBoxItem runat="server"  Text="Convenio" Value="1" />
                                                <telerik:RadComboBoxItem runat="server"  Text="No. Cliente" Value="2" />
                                                <telerik:RadComboBoxItem runat="server"  Text="Clave Key" Value="3" />
                                                <telerik:RadComboBoxItem runat="server"  Text="Clave proveedor" Value="4" />
                                           </Items>
                    </telerik:RadComboBox></td>
                            <td>
                                 <telerik:RadComboBox ID="CmbVencido" MaxHeight="300px" runat="server" 
                        Width="150px">
                        <Items>
                         <telerik:RadComboBoxItem runat="server" Selected="True"  Text="-- Seleccionar --" Value="-1" />
                                                <telerik:RadComboBoxItem runat="server"  Text="Si" Value="1" />
                                                <telerik:RadComboBoxItem runat="server"  Text="No" Value="2" />
                                                <telerik:RadComboBoxItem runat="server"  Text="Parcialmente" Value="3" />
                                                <telerik:RadComboBoxItem runat="server"  Text="Cancelado" Value="4" />
                                                         </Items>
                    </telerik:RadComboBox></td>
                            <td>
                                &nbsp;
                                 <telerik:RadComboBox ID="CmbCategoria" MaxHeight="300px" runat="server" 
                        Width="150px" >
                    </telerik:RadComboBox>
                            </td>
                            <td>
                            <telerik:RadTextbox runat="server"  ID="TxtValorCategoria" MaxHeight="300px"   Width="150px" >
                            </telerik:RadTextbox>
                              </td>
                            
                            <td>
                               <asp:ImageButton ID="BtnBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                        ToolTip="Buscar" onclick="BtnBuscar_Click"  />
                              </td>
                   <td></td>
                            
                        </tr>
                                <tr>
                            <td>
                               </td>
                            <td>
                                   &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                          <tr>
                            <td style="font-weight: bold" colspan="2">
                            Convenios utilizados:
                              </td>
                            <td>
                                &nbsp;</td>
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
                            <td colspan="4">
                               <telerik:RadGrid ID="rgUtilizados" runat="server" AutoGenerateColumns="False" GridLines="None"  
                                 EnableLinqExpressions="False"  OnItemCommand="rgUtilizado_ItemCommand"  OnPageIndexChanged="rgUtilizado_PageIndexChanged"
                                OnNeedDataSource="rgUtilizado_NeedDataSource" PageSize="5"  onItemDataBound = "rgUtilizado_ItemDataBound" 
                                AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."  
                               >
                                <MasterTableView ClientDataKeyNames="Id_PC" >
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                       <telerik:GridBoundColumn DataField="Id_PC" HeaderText="Id_PC" UniqueName="Id_PC" Display= "false">
                                            <HeaderStyle Width="150px" />
                                        </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="PC_NoConvenio" HeaderText="No. Convenio" UniqueName="PC_NoConvenio">
                                            <HeaderStyle Width="80px" />
                                             <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PC_Nombre" HeaderText="Nombre" UniqueName="PC_Nombre">
                                            <HeaderStyle Width="150px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Cat_DescCorta" HeaderText="Categoría" UniqueName="Cat_DescCorta">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Estatus" HeaderText="Vencido" UniqueName="Estatus">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PC_FechaCreo" HeaderText="Fecha apertura"  UniqueName="PC_FechaCreo"
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PCD_Usar" HeaderText="Usar" UniqueName="PCD_Usar" Display="False">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Editar" HeaderText="Ver" Text="Ver" 
                                            UniqueName="Editar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>
                                   <%--      <telerik:GridButtonColumn CommandName="Sustituir" HeaderText="Sustituir" Text="Sustituir" 
                                            UniqueName="sustituir" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>--%>
                                       <%--    <telerik:GridButtonColumn CommandName="VincularSuc" HeaderText="Vincular sucursal" Text="Vincular sucursal" 
                                            UniqueName="VincularSuc" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="Vincular">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>--%>
                                          <telerik:GridButtonColumn CommandName="VincularCte" HeaderText="Vincular cliente" Text="Vincular cliente" 
                                            UniqueName="VincularCte" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>
                                             <telerik:GridButtonColumn CommandName="Vinculados" HeaderText="Clientes vinculados" Text="Clientes vinculados" 
                                            UniqueName="CtesVinculados" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>
                                        <%--<telerik:GridButtonColumn CommandName="Baja" HeaderText="Cancelar" 
                                          
                                           Text="Cancelar" UniqueName="Cancelar" Visible="True" ButtonType="ImageButton" 
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>

                                          <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" 
                                           Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton" 
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>--%>
                                   
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <ClientSettings>
                                 <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                            </telerik:RadGrid>
                            </td>
                            <td>
                                     &nbsp;</td>
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
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
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
                            <td style="font-weight: bold" colspan="2">
                                Convenios disponibles (no utilizados):
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="4">
                               <telerik:RadGrid ID="rgNoUtilizados" runat="server" AutoGenerateColumns="False" GridLines="None"
                                 EnableLinqExpressions="False" OnItemCommand="rgNoUtilizado_ItemCommand"  OnPageIndexChanged="rgNoUtilizado_PageIndexChanged"
                                 OnNeedDataSource="rgNoUtilizado_NeedDataSource" onItemDataBound = "rgNoUtilizado_ItemDataBound" 
                                PageSize="5"  
                                AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                               >
                                <MasterTableView ClientDataKeyNames="Id_PC">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>

                                      <telerik:GridBoundColumn DataField="Id_PC" HeaderText="Id_PC" UniqueName="Id_PC" Display = "false">
                                            <HeaderStyle Width="150px" />
                                        </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="PC_NoConvenio" HeaderText="No. Convenio" UniqueName="PC_NoConvenio">
                                            <HeaderStyle Width="80px" />
                                             <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PC_Nombre" HeaderText="Nombre" UniqueName="PC_Nombre">
                                            <HeaderStyle Width="150px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Cat_DescCorta" HeaderText="Categoría" UniqueName="Cat_DescCorta">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Estatus" HeaderText="Vencido" UniqueName="Estatus">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PC_FechaCreo" HeaderText="Fecha apertura"  UniqueName="PC_FechaCreo"
                                           DataFormatString="{0:dd/MM/yy}" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="PCD_Usar" HeaderText="Usar" UniqueName="PCD_Usar" Display="False">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Editar" HeaderText="Ver" Text="Ver" 
                                            UniqueName="Editar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>
                                  <%--            <telerik:GridButtonColumn CommandName="Sustituir" HeaderText="Sustituir" Text="Sustituir" 
                                            UniqueName="sustituir" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>
                                                 <telerik:GridButtonColumn CommandName="VincularSuc" HeaderText="Vincular sucursal" Text="Vincular sucursal" 
                                            UniqueName="VincularSuc" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="Vincular">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>--%>
                                          <telerik:GridButtonColumn CommandName="VincularCte" HeaderText="Vincular cliente" Text="Vincular cliente" 
                                            UniqueName="VincularCte" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>
                                                  <telerik:GridButtonColumn CommandName="Vinculados" HeaderText="Clientes vinculados" Text=" Ver clientes vinculados" 
                                            UniqueName="CtesVinculados" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>

<%--
                                          <telerik:GridButtonColumn CommandName="Baja" HeaderText="Cancelar" 
                                          
                                           Text="Cancelar" UniqueName="Cancelar" Visible="True" ButtonType="ImageButton" 
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>

                                          <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" 
                                           Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton" 
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>--%>
                                   
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                            </telerik:RadGrid>
                            </td>
                            <td>
                                     &nbsp;</td>
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
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HF_Cve" runat="server" />
                    <asp:HiddenField ID="HFId_PC" runat="server" />
                     <asp:HiddenField ID="HFTipoOP" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

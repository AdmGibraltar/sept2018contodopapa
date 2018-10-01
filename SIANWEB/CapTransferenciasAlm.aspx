<%@ Page Title="Transferencias almacén" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapTransferenciasAlm.aspx.cs" Inherits="SIANWEB.CapTransferenciasAlm" %>

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
                    case 'new':
                        AbrirVentana_Remision(1, -1, true, true, true, true);
                        args.set_cancel(true);
                        break;

                }
            }


            function OpenWindow(tdm, Id_Rem, PermisoGuardar, mensaje) {

                if (mensaje != "") {
                    var cerrarWindow = radalert(mensaje, 350, 150, tituloMensajes);
                    cerrarWindow.add_close(
                    function () {
                        AbrirVentana_Remision(tdm, Id_Rem, PermisoGuardar);
                    });
                }
                else {
                    AbrirVentana_Remision(tdm, Id_Rem, PermisoGuardar);
                }
            }

            function AbrirVentana_Remision(tdm, Id_Rem, PermisoGuardar) {
                //debugger;
                var oWnd = radopen("CapRemisiones.aspx?tdm=" + tdm + "&Id_Rem=" + Id_Rem + "&PermisoGuardar=" + PermisoGuardar + "&Trans=1", "AbrirVentana_Remision");
                oWnd.center();
                oWnd.Maximize();
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


            function AbrirVentana_Recepcion(Id_Emp, Id_Cd,Id_Tra,Modificar) {
                //debugger;
                var oWnd = radopen("CapTransferenciasAlmRec.aspx?Id_Emp=" + Id_Emp + "&Id_Cd=" + Id_Cd + "&Id_Tra=" + Id_Tra + "&Mod=" + Modificar, "AbrirVentana_Recepcion");
                oWnd.center();
                oWnd.Maximize();
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
               <telerik:AjaxSetting AjaxControlID="rgEnviados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEnviados" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="rgRecibidos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecibidos" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                   <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecibidos" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rgEnviados" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick" >
            <Items>
     <%--       <telerik:RadToolBarButton CommandName="expconvenios" Value="expconvenios" ToolTip="Exportar convenios" CssClass="Excel"
                        ImageUrl="~/Imagenes/blank.png" />--%>
            <telerik:RadToolBarButton CommandName="Excel" Value="Excel" ToolTip="Exportar listado" CssClass="Excel"
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
 
                    <table>



                      <tr>
                            <td width="100">
                                       <asp:Label ID="Label2" Text="Sucursal inicial" runat="server"> </asp:Label></td>
                            <td>
                            <telerik:RadNumericTextBox runat="server"  ID="TxtSucIni"  
                                    Width="70px" MaxLength="5" MaxValue="999999" MinValue="0"  >
                                 <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                         </telerik:RadNumericTextBox>
                           <%-- <telerik:RadTextbox runat="server"  ID="TxtSucIni" MaxHeight="150px"   
                                    Width="100px"  >
                            </telerik:RadTextbox>--%>
                              </td>
                            <td>
                                &nbsp;
                                 </td>
                            <td width="100">
                              <asp:Label ID="Label1" Text="Sucursal final" runat="server"> </asp:Label></td>
                            
                            <td>
                                <telerik:RadNumericTextBox runat="server"  ID="TxtSucFin" 
                                    Width="70px" MaxLength="5" MaxValue="999999" MinValue="0"  >
                                 <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                         </telerik:RadNumericTextBox>
                   
                              </td>
                              <td></td>
                                <td width="100">
                              <asp:Label ID="Label3" Text="Remisión" runat="server"> </asp:Label></td>
                            
                           
                              <td>

                            <telerik:RadNumericTextBox runat="server"  ID="TxtId_Rem" 
                                    Width="70px" MaxLength="5" MaxValue="999999" MinValue="0"  >
                                 <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                         </telerik:RadNumericTextBox>
                        
                              </td>
                              <td>
                             
                              </td>
                            
                        </tr>
                                <tr>
                                   <td >
                                    Fecha inicial
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput1" runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Fecha final
                            </td>
                            <td>
                                          <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px">
                                        <DatePopupButton ToolTip="Abrir calendario" />
                                        <Calendar ID="calTxtFecha2" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput2" runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                    </telerik:RadDatePicker></td>

                            <td></td>
                            <td>
                            Estatus
                            </td>
                            <td>
                            <telerik:RadComboBox runat="server" ID="CmbEstatus" >
                            <Items>
                            <telerik:RadComboBoxItem  runat="server" Selected="true" Value="T" Text="-- Todos --"/>
                            <telerik:RadComboBoxItem  runat="server"  Value="C" Text="Capturado"/>
                            <telerik:RadComboBoxItem  runat="server"  Value="I" Text="Impreso"/>
                            <telerik:RadComboBoxItem  runat="server"  Value="B" Text="Cancelado por remitente"/>
                            <telerik:RadComboBoxItem  runat="server"  Value="X" Text="Cancelado por destinatario"/>
                            <telerik:RadComboBoxItem  runat="server"  Value="N" Text="Enviado"/>
                            <telerik:RadComboBoxItem  runat="server"  Value="R" Text="Recibido"/>
                            </Items>
                            
                            </telerik:RadComboBox>
                            </td>
                            <td>
                              <asp:ImageButton ID="BtnBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                        ToolTip="Buscar" onclick="BtnBuscar_Click"  />
                            </td>
                        </tr>
                        </table>
                        <br />
             
                       <table width="900px">
                        <tr>
                        <td>
                  <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" >
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Enviados" 
                                PageViewID="RadPageEnviados" Selected="True" Value="Enviados">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Recibido" 
                                PageViewID="RadPageRecibido" Value="Enviados">
                            </telerik:RadTab>



                        </Tabs>
                    </telerik:RadTabStrip>
                       <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <telerik:RadPageView  ID="RadPageEnviados" runat="server">
                           <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="900px" 
                                 BorderSize="0" Width="100%">
                        <telerik:RadPane ID="RadPane1" runat="server" Height="900px" Width="900px"  
                                    BorderStyle="None">
                               <table width="100%">
                                    <tbody>
                                    <tr>
                                    <td>
                         <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" dir="rtl"  OnClientButtonClicking="ToolBar_ClientClick">
                         <Items>
                                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                                      ImageUrl="~/Imagenes/blank.png" />
          
                         </Items>
                         </telerik:RadToolBar>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                
                                <telerik:RadGrid ID="rgEnviados" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnNeedDataSource="rgEnviados_NeedDataSource"  OnPageIndexChanged="rgEnviados_PageIndexChanged" EnableLinqExpressions="False" PageSize="15" OnItemCommand="rgEnviados_ItemCommand" OnItemDataBound="rgEnviados_ItemDataBound"
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                                <MasterTableView ClientDataKeyNames="Id_Rem">
                               
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Id_Cte" UniqueName="Id_Cte"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="Rem_ManAut" HeaderText="Rem_ManAut" UniqueName="Rem_ManAut"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Rem" UniqueName="Id_Rem"
                                          HeaderText="Remisión">
                                              <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Sucursal" UniqueName="Cte_NomComercial">
                                            <HeaderStyle Width="350px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="Rem_Fecha" HeaderText="Fecha" UniqueName="Rem_Fecha"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="Rem_Total" HeaderText="Total" UniqueName="Rem_Total"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="60px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Estatus" HeaderText="Rem_Estatus" UniqueName="Rem_Estatus"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_EstatusStr" HeaderText="Estatus" UniqueName="Rem_EstatusStr">
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Editar" HeaderText="Editar" Text="Editar"
                                            UniqueName="Editar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="Se imprimirá la remisión, tenga listo el formato en la impresora<br /><br />"
                                            ConfirmTitle="" Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="Baja" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="¿Estás seguro que desea cancelar la remisión <b>#[[ID]]</b>?<br /><br />"
                                            ConfirmTitle="" Text="Baja" UniqueName="Baja" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                       
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
                                    </tr>
                            
                                    </tbody>
                                    </table>
                        
                        </telerik:RadPane>
                        </telerik:RadSplitter>
                             
                        </telerik:RadPageView>
                      <telerik:RadPageView  ID="RadPageRecibido" runat="server">
                               <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="900px" 
                               BorderSize="0" Width="100%">
                                   <telerik:RadPane ID="RadPane2" runat="server"  Height="900px" Width="900px"  
                                    BorderStyle="None">
                            <table width="100%">
                                    <tbody>
                                    <tr>
                                    <td>
                         <telerik:RadToolBar ID="RadToolBar3" runat="server" Width="100%" dir="rtl"  OnClientButtonClicking="ToolBar_ClientClick">
                         <Items>
                                <telerik:RadToolBarButton CommandName="" Value="n" ToolTip="" 
                                      ImageUrl="~/Imagenes/blank.png" />
          
                         </Items>
                         </telerik:RadToolBar>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                
                                <telerik:RadGrid ID="rgRecibidos" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnNeedDataSource="rgRecibidos_NeedDataSource"  OnPageIndexChanged="rgRecibidos_PageIndexChanged" 
                                 EnableLinqExpressions="False" PageSize="15"  OnItemCommand="rgRecibidos_ItemCommand"
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                                <MasterTableView ClientDataKeyNames="Id_Rem">
                               
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Tra" HeaderText="Id_Tra" UniqueName="Id_Tra"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="Id_CdOrigen" HeaderText="Id_CdOrigen" UniqueName="Id_CdOrigen"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_RemOrigen" UniqueName="Id_RemOrigen"
                                          HeaderText="Remisión">
                                              <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cd_Nombre" HeaderText="Sucursal" UniqueName="Cd_Nombre">
                                            <HeaderStyle Width="350px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="Tra_RemFecha" HeaderText="Fecha remisión" UniqueName="Tra_RemFecha"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="Tra_FechaEnvio" HeaderText="Fecha envió" UniqueName="Tra_FechaEnvio"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn DataField="Tra_FechaRecepcion" HeaderText="Fecha recepción" UniqueName="Tra_FechaRecepcion"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="Id_Es" HeaderText="Entrada" UniqueName="Id_Es">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="Tra_Importe" HeaderText="Total" UniqueName="Tra_Importe"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="60px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Tra_Estatus" HeaderText="Tra_Estatus" UniqueName="Tra_Estatus"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Tra_EstatusStr" HeaderText="Estatus" UniqueName="Tra_EstatusStr">
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </telerik:GridBoundColumn>

                                        <telerik:GridButtonColumn CommandName="Recibir" HeaderText="Recibir" Text="Recibir"
                                            UniqueName="Recibir" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="Se imprimirá la remisión, tenga listo el formato en la impresora<br /><br />"
                                            ConfirmTitle="" Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="Baja" HeaderText="Baja" ConfirmDialogType="RadWindow" Display="false"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="¿Estás seguro que deseas cancelar la remisión <b>#[[ID]]</b>?<br /><br />"
                                            ConfirmTitle="" Text="Baja" UniqueName="Baja" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                      
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
                                    </tr>
                            
                                    </tbody>
                                    </table>
                                  </telerik:RadPane>
                        </telerik:RadSplitter>
                        </telerik:RadPageView>

                        </telerik:RadMultiPage>
                              <asp:Panel ID="Panel1" runat="server">
                                <asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="Timer1_Tick">
                                 </asp:Timer>
                           </asp:Panel>
                        </td>
                        </tr>
                        
                        </table>
<asp:HiddenField runat="server" ID="clientSideIsPostBack"/>
<asp:HiddenField runat="server" ID="HiddenHeight"/>
<asp:HiddenField runat="server" ID="HD_GridRebind"/>
<asp:HiddenField runat="server" ID="HF_Cve" />

    </div>









</asp:Content>

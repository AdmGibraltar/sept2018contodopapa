<%@ Page Title="Autorización de Gasto de Viaje" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="ProAutorizacion_GastoViaje.aspx.cs" Inherits="SIANWEB.ProAutorizacion_GastoViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPago" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <%-- Agregar pantalla para soporte de archivos --%>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_LstComprobantes" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="900px" Height="600px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Listado de Comprobantes" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgPago" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="rgPago_NeedDataSource" OnItemCommand="rgPago_ItemCommand" OnPageIndexChanged="rgPago_PageIndexChanged"
                    PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Núm. Solicitud" UniqueName="Id_PagElec"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                             <%--jfcv agregar sub tipo Id_PagElecSubTipo--%>
                            <telerik:GridBoundColumn DataField="Id_PagElecSubTipo" HeaderText="Id_PagElecSubTipo" UniqueName="Id_PagElecSubTipo" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_GV" HeaderText="Id" UniqueName="Id_GV" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_GVEst" HeaderText="Tipo" UniqueName="Id_GVEst" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVEst_Descripcion" HeaderText="Estatus" UniqueName="GVEst_Descripcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Motivo" HeaderText="Motivo" UniqueName="GV_Motivo"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Solicitante" HeaderText="Solicitante" UniqueName="GV_Solicitante"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                             <%--JFCV 29ene2016 agregar nombre del acreedor , observaciones y columna de fecha de elaboracion cuando sea pago acreedores --%>
                            <telerik:GridBoundColumn DataField="GV_Acr_Nombre" HeaderText="Acreedor" UniqueName="GV_Acr_Nombre"><HeaderStyle Width="130" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_PagElec_Observaciones" HeaderText="Observaciones" UniqueName="GV_PagElec_Observaciones"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_FechaElaboracion" HeaderText="Fecha Elaboración" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaElaboracion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                         

                            <telerik:GridBoundColumn DataField="GV_FechaSalida" HeaderText="Fecha Salida" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaSalida"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_FechaRegreso" HeaderText="Fecha Regreso" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaRegreso"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_PagElec_Destino" HeaderText="Destino" UniqueName="GV_PagElec_Destino"><HeaderStyle Width="100" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Importe" HeaderText="Importe Solicitado" UniqueName="GV_Importe"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Saldo_Comprobar" HeaderText="Saldo Comprobar" UniqueName="GV_Saldo_Comprobar"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Acr_NumeroGenerado" HeaderText="Cta. Pago" UniqueName="Acr_NumeroGenerado" Visible="false"><HeaderStyle Width="60" /></telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn HeaderText="Número Referencia" UniqueName="Acr_NumeroGenerado">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="TxtNumeroAcreedor" runat="server" MaxLength="30"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                              <%-- jfcv 5 oct 2015 Agregue la columna de soporte y validación si no tiene docs soporte no muestra el icono y el de comprobantes , si no tiene de soporte muestra el icono de comproante--%>
                          
                           <telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px"
                                UniqueName="Comprobantes">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes"  />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn> 

                            <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Desea autorizar el gasto?</br></br>" Text="Autorizar"
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" UniqueName="Autorizar"
                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>
                            <%-- JFCV 12 ene 2015 agregar botón de rechazar y Motivo de Rechazo --%>
                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Desea rechazar la solicitud?" Text="Cancelar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                ConfirmDialogWidth="350px" HeaderText="Rechazar">
                                <HeaderStyle Width="25px" />
                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="25px" />
                            </telerik:GridButtonColumn>
                              <telerik:GridTemplateColumn HeaderText="Motivo Rechazo" UniqueName="Acr_MotivoRechazo">
                                <ItemTemplate>
                                     <telerik:RadTextBox ID="TxtMotivoRechazo" runat="server" MaxLength="100"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                             <%-- JFCV 02 feb 2016 agregar campos de cuenta subcuenta etc ocultos para enviar al movimiento de CXP --%>
                            <telerik:GridBoundColumn DataField="GV_Cuenta" HeaderText="GV_Cuenta" UniqueName="GV_Cuenta" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Cc" HeaderText="GV_Cc" UniqueName="GV_Cc" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Numero" HeaderText="GV_Numero" UniqueName="GV_Numero" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_SubCuenta" HeaderText="GV_SubCuenta" UniqueName="GV_SubCuenta" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_SubSubCuenta" HeaderText="GV_SubSubCuenta" UniqueName="GV_SubSubCuenta" Visible="false"></telerik:GridBoundColumn>
                           <%-- <telerik:GridBoundColumn DataField="GV_UUID" HeaderText="GV_UUID" UniqueName="GV_UUID" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Subtotal" HeaderText="GV_Subtotal" UniqueName="GV_Subtotal" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Iva" HeaderText="GV_Iva" UniqueName="GV_Iva" Visible="false"></telerik:GridBoundColumn>--%>
                           </Columns>
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:RadGrid>
                </td>
            </tr>
           </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }
            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function AbrirVentana_LstComprobantes(Id) {
                var oWnd = radopen("CapGastosViajeComprobantes_Listado.aspx?Id=" + Id, "AbrirVentana_LstComprobantes");
                oWnd.center();
            }
        </script>
 
    </telerik:RadCodeBlock>
</asp:Content>

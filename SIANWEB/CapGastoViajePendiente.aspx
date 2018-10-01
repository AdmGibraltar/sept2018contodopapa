<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CapGastoViajePendiente.aspx.cs" Inherits="SIANWEB.CapGastoViajePendiente" %>

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

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
        <%--jfcv 18oct2016 que pregunte antes de salir control de cambio 9 OnClientBeforeClose="winDetailClosing" --%>
            <telerik:RadWindow ID="AbrirVentana_ComprobacionGastos" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="940px" Height="645px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Captura de Comprobantes" Modal="True" Localization-Maximize="Maximizar"
                OnClientClose="refreshGrid" ReloadOnShow="true" OnClientBeforeClose="winDetailClosing">
            </telerik:RadWindow>
            <%-- Factura (Impresion PDF) --%>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

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
                    OnItemDataBound="rgPago_ItemDataBound" PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Núm. Solicitud" UniqueName="Id_PagElec"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_GV" HeaderText="Núm. Solicitud" UniqueName="Id_GV" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_GVEst" HeaderText="Tipo" UniqueName="Id_GVEst" Visible="false"></telerik:GridBoundColumn>
                            <%--agregar columnas de cc, subcuenta y subsubcuenta--%>
                            <telerik:GridBoundColumn DataField="PagElec_Cc" HeaderText="C.C." UniqueName="PagElec_Cc"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubCuenta" HeaderText="Sub Cuenta" UniqueName="PagElec_SubCuenta"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubSubCuenta" HeaderText="Sub Sub-Cta" UniqueName="PagElec_SubSubCuenta"><HeaderStyle Width="80" /></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="GVEst_Descripcion" HeaderText="Estatus" UniqueName="GVEst_Descripcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Motivo" HeaderText="Motivo de Viaje" UniqueName="GV_Motivo"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Solicitante" HeaderText="Solicitante" UniqueName="GV_Solicitante"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <%--JFCV 29ene2016 agregar nombre del acreedor , observaciones y columna de fecha de elaboracion cuando sea pago acreedores --%>
                            <telerik:GridBoundColumn DataField="GV_Acr_Nombre" HeaderText="Acreedor" UniqueName="GV_Acr_Nombre"><HeaderStyle Width="130" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_PagElec_Observaciones" HeaderText="Observaciones" UniqueName="GV_PagElec_Observaciones"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_FechaElaboracion" HeaderText="Fecha Elaboración" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaElaboracion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                              
                            <telerik:GridBoundColumn DataField="GV_FechaSalida" HeaderText="Fecha Salida" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaSalida"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_FechaRegreso" HeaderText="Fecha Regreso" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaRegreso"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_PagElec_Destino" HeaderText="Destino" UniqueName="GV_PagElec_Destino"><HeaderStyle Width="100" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Saldo_Comprobar" HeaderText="Saldo Comprobar" UniqueName="GV_Saldo_Comprobar"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_MotivoRechazo" HeaderText="Motivo Rechazo" UniqueName="GV_MotivoRechazo"><HeaderStyle Width="180" /></telerik:GridBoundColumn>
                            
                            <telerik:GridTemplateColumn HeaderText="Comprobar" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgEditar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Comprobar" CommandName="Comprobar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle Width="50px" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:RadGrid>
                </td>
            </tr>
            <tr>
            <td>
                <asp:CheckBox ID="chkGastoViaje" runat="server" Text="es gasto viaje" Visible="false" />
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

            //jfcv 18oct2016 que pregunte antes de salir control de cambio 9
            function winDetailClosing(sender, arg) {
                arg.set_cancel(true);
                function confirmCallback(args) {
                    if (args) {
                        sender.remove_beforeClose(winDetailClosing);
                        sender.close();
                        sender.add_beforeClose(winDetailClosing);
                    }
                }
                radconfirm("¿Seguro que desea cerrar sin guardar?", confirmCallback);
            }

            function AbrirVentana_ComprobacionGastos(id) {
               
                var oWnd = radopen("CapGastosViajeComprobacion.aspx?Id=" + id + "&ref=" + $_GET("ref"), "AbrirVentana_ComprobacionGastos");
                oWnd.center();
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
            function $_GET(param) {
                /* Obtener la url completa */
                url = document.URL;
                /* Buscar a partir del signo de interrogación ? */
                url = String(url.match(/\?+.+/));
                /* limpiar la cadena quitándole el signo ? */
                url = url.replace("?", "");
                /* Crear un array con parametro=valor */
                url = url.split("&");

                /* 
                Recorrer el array url
                obtener el valor y dividirlo en dos partes a través del signo = 
                0 = parametro
                1 = valor
                Si el parámetro existe devolver su valor
                */
                x = 0;
                while (x < url.length) {
                    p = url[x].split("=");
                    if (p[0] == param) {
                        return decodeURIComponent(p[1]);
                    }
                    x++;
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

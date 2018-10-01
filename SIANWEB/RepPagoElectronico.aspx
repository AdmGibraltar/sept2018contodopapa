<%@ Page Title="Gastos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="RepPagoElectronico.aspx.cs" Inherits="SIANWEB.RepPagoElectronico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
  <style>
        .RadInput input[readonly]
        {
            background-color: #F7F7F7 !important;
        }
        #PopUpBackground
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: gray;
            filter: alpha(opacity=50);
            opacity: 0.5;
            z-index: 100000;
        }
        #PopUpProgress
        {
            position: fixed;
            font-size: 120%;
            top: 40%;
            left: 40%;
            height: 20%;
            width: 20%;
            z-index: 100001;
            background-color: #FFFFFF;
            border: 1px solid Gray;
            background-image: url('images/loading1.gif');
            background-repeat: no-repeat;
            background-position: center;
            border-radius: .6em;
        }
}
    </style>
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPagoElectronico" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
   <%-- JFCV 06 dic 2016 1ue habra los archivos de listado de comprobantes --%>
     <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_Gastos" runat="server" Opacity="100" Behaviors="Move, Close, Maximize"
                VisibleStatusbar="False" Width="900px" Height="600px" Animation="Fade" KeepInScreenBounds="True"
                Overlay="True" Title="Captura Gastos" Modal="True" OnClientClose="refreshGrid"
                ReloadOnShow="true">
            </telerik:RadWindow>
            <%-- Factura (Impresion PDF) --%>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_LstComprobantes" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="900px" Height="600px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Listado de Comprobantes"
                Modal="True" OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
                <telerik:RadWindow ID="AbrirVentana_GastosConsultar" runat="server" Opacity="100"
                Behaviors="Resize, Close,Move, Maximize" VisibleStatusbar="False" Width="1120px" Height="690px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Consultar Gastos" Modal="True" Localization-Maximize="Maximizar"
                OnClientClose="refreshGrid" ReloadOnShow="true" >
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <div id="divPrincipal" runat="server">
        <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:radtoolbar>
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
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblid" runat="server" Text="Núm. Solicitud"></asp:Label>
                </td>
                <td>
                <telerik:RadTextBox ID="txtidPagoElectronico" runat="server"  Width="70px">
                                                        </telerik:RadTextBox>
                
                </td>
                <td><asp:Label ID="LblTipo" runat="server" Text="Tipo"></asp:Label></td>
                <td>
                    <telerik:RadCombobox id="CmbTipo" runat="server" onselectedindexchanged="CmbTipo_SelectedIndexChanged" autopostback="True">
                        <Items><telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Solicitud de Cheque" Value="1" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Reposicion de Caja" Value="2" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Gastos de Viaje" Value="3" /></Items>
                    </telerik:RadCombobox>
                </td>
                <td><asp:Label ID="LblAcreedor" runat="server" Text="Acreedor"></asp:Label></td>
                <td><telerik:RadCombobox ID="CmbAcreedor" runat="server" ></telerik:RadCombobox></td>
                <td><asp:Label ID="LblEstatus" runat="server" Text="Estatus"></asp:Label></td>
                <td>
                    <telerik:RadCombobox ID="CmbEstatus" runat="server">
                        <Items><telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Creado" Value="1" /></Items>
                         <Items><telerik:RadComboBoxItem runat="server" Text="Autorizado" Value="2" /></Items>
                         <Items><telerik:RadComboBoxItem runat="server" Text="Rechazado" Value="3" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Cancelado" Value="4" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Solicitado" Value="5" /></Items>
     
                    </telerik:RadCombobox>
                </td>
                <td><asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" /></td>
            </tr>

        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgPagoElectronico" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="rgPagoElectronico_NeedDataSource" OnItemCommand="rgPagoElectronico_ItemCommand"
                    PageSize="13" AllowPaging="True" 
                    mastertableview-nomasterrecordstext="No se encontraron registros." onpageindexchanged="rgPagoElectronico_PageIndexChanged">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Núm. Solicitud" UniqueName="Id_PagElec"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecTipo_Descrpcion" HeaderText="Tipo" UniqueName="PagElecTipo_Descrpcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecCuenta_Descripcion" HeaderText="Cuenta" UniqueName="PagElecCuenta_Descripcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Nombre" HeaderText="Acreedor" UniqueName="Acr_Nombre"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_NumeroGenerado" HeaderText="Cta. Pago" UniqueName="Acr_NumeroGenerado"><HeaderStyle Width="60" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Solicitante" HeaderText="Solicitante" UniqueName="PagElec_Solicitante"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_FechaRequiere" HeaderText="Fecha" DataFormatString="{0:dd/MM/yy}" UniqueName="PagElec_FechaRequiere"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Cuenta" HeaderText="Número" UniqueName="PagElec_Cuenta" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Cc" HeaderText="C.C." UniqueName="PagElec_Cc"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubCuenta" HeaderText="Sub Cuenta" UniqueName="PagElec_SubCuenta"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubSubCuenta" HeaderText="Sub Sub-Cta" UniqueName="PagElec_SubSubCuenta"><HeaderStyle Width="35" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_CuentaPago" HeaderText="Cta Pago." UniqueName="PagElec_CuentaPago" Visible="false" ></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Importe" HeaderText="Monto" DataFormatString="{0:C}" UniqueName="PagElec_Importe" ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElecEstatus" HeaderText="Id_PagElecEstatus" UniqueName="Id_PagElecEstatus" Visible="false"></telerik:GridBoundColumn>
			                <telerik:GridBoundColumn DataField="PagElecEstatus_Descripcion" HeaderText="Estatus" UniqueName="PagElecEstatus_Descripcion"><HeaderStyle Width="50" /></telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn HeaderText="Estatus Aut." Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="LblRGEstatus" runat="server" Text='<%# Eval("PagElec_Autorizado").ToString().ToUpper() == "TRUE" ? "Autorizado" : "Creado" %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PagElec_MotivoRechazo" HeaderText="Motivo Rechazo" UniqueName="PagElec_MotivoRechazo"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                       
                            <telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px" UniqueName="Comprobantes">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderText="Consultar" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgEditar" runat="server" ImageUrl="~/Imagenes/view.png"
                                         ToolTip="Consultar" CommandName="Consultar"    />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="35px" />
                                <HeaderStyle Width="35px" />
                            </telerik:GridTemplateColumn>

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
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
        <script type="text/javascript">


            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }

            function AbrirVentana_LstComprobantes(Id) {
                var oWnd = radopen("CapPagosElectronicos_Listado.aspx?Id=" + Id, "AbrirVentana_LstComprobantes");
                oWnd.center();
            }

            function AbrirFacturaPDF(WebURL) {
                //                alert(WebURL);
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function AbrirVentana_GastosConsultar(id) {
                var oWnd = radopen("CapPagosElectronicosConsultar.aspx?Id=" + id, "AbrirVentana_GastosConsultar");
                oWnd.center();
            }

            $("document").ready(function () {
                //if ($("#TblConceptos").length > 0) {
                if ($("#TblConceptos").length) {

                    //                    alert('');
                    $("input[name*='Importe']").val('0');
                    $("input[name*='IVA']").val('0');
                    $("input[name*='Total']").val('0');

                    $("#TblConceptos input[name*='Importe']").blur(function () {
                        var total = 0;
                        $("#TblConceptos input[name*='Importe'][name*='text']").each(function () {
                            total = total + parseFloat($(this).val().replace(',', ''));
                        });

                        $("#TblTotales input[name*='Importe']").val(total);
                    });

                    $("#TblConceptos input[name*='IVA']").blur(function () {
                        var total = 0;
                        $("#TblConceptos input[name*='IVA'][name*='text']").each(function () {
                            total = total + parseFloat($(this).val().replace(',', ''));
                        });

                        $("#TblTotales input[name*='IVA']").val(total);
                    });

                    $("#TblConceptos input[name*='Total']").blur(function () {
                        var total = 0;
                        $("#TblConceptos input[name*='Total'][name*='text']").each(function () {
                            total = total + parseFloat($(this).val().replace(',', ''));
                        });

                        //$("#TblTotales input[name*='TotalTotal']").val(total);
                    });
                }
            });
        </script>
    </telerik:radcodeblock>

</asp:Content>

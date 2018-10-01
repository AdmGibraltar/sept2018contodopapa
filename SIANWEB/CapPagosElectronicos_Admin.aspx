<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapPagosElectronicos_Admin.aspx.cs" Inherits="SIANWEB.CapPagosElectronicos_Admin" %>


   <asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
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

    <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPagoElectronico" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>        
            <telerik:AjaxSetting AjaxControlID="CmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmdCtaGastos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divCtaGastos" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbAcreedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>

            <telerik:AjaxSetting AjaxControlID="BtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
       
           <telerik:AjaxSetting AjaxControlID="rgPagoElectronico">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPagoElectronico" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radwindowmanager id="RadWindowManager1" runat="server">
        <Windows>
            <%--jfcv 18oct2016 que pregunte antes de salir control de cambio 9 OnClientBeforeClose="winDetailClosing" --%>
            <telerik:RadWindow ID="AbrirVentana_Gastos" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="1120px" Height="720px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Captura Gastos" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true" OnClientBeforeClose="winDetailClosing">
            </telerik:RadWindow>
            <%--Tenia 940 x 670 al abrir la de gastos --%>
            <%-- Factura (Impresion PDF) --%>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_LstComprobantes" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="900px" Height="600px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Listado de Comprobantes" Modal="True"
                 ReloadOnShow="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_GastosModificar" runat="server" Opacity="100"
                Behaviors="Resize, Close,Move, Maximize" VisibleStatusbar="False" Width="1120px" Height="690px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Editar Gastos" Modal="True" Localization-Maximize="Maximizar"
                OnClientClose="refreshGrid" ReloadOnShow="true" OnClientBeforeClose="winDetailClosing">
            </telerik:RadWindow>
        </Windows>
    </telerik:radwindowmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server">
        <div id="PopUpBackground"></div>
        <div id="PopUpProgress">
            <h6><p style="text-align:center;"><b>Favor de Esperar...</b></p></h6>
        </div>
    </telerik:radajaxloadingpanel>
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
            
                <td>
                    <asp:Label ID="LblTipo" runat="server" Text="Tipo"></asp:Label>
                </td>
                <td>
                    <telerik:radcombobox id="CmbTipo" runat="server" onselectedindexchanged="CmbTipo_SelectedIndexChanged"
                        autopostback="True">
                        <Items><telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Solicitud de Cheque" Value="1" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Reposicion de Caja" Value="2" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Gastos de Viaje" Value="3" /></Items>
                    </telerik:radcombobox>
                </td>
                <td>
                    <asp:Label ID="LblAcreedor" runat="server" Text="Acreedor"></asp:Label>
                </td>
                <td>
                    <telerik:radcombobox id="CmbAcreedor" runat="server"></telerik:radcombobox>
                </td>
                <td>
                    <asp:Label ID="LblCuenta" runat="server" Text="Cuenta"></asp:Label>
                </td>
                <td>
                    <asp:Panel ID="divCtaGastos" runat="server">
                        <telerik:radcombobox id="cmdCtaGastos" runat="server" width="425px" onclientfocus="cmdClient_Focus"
                            filter="Contains" changetextonkeyboardnavigation="true" markfirstmatch="true"
                            enableloadondemand="true" highlighttemplateditems="true" loadingmessage="Cargando..."
                            enableautomaticloadondemand="True" enablevirtualscrolling="True" itemsperrequest="12"
                            showmoreresultsbox="True" maxheight="300px" emptymessage="-- Seleccionar --"
                            autopostback="True">
                            <HeaderTemplate>
                                <table style="width:100%">
                                    <tr>
                                        <td valign="middle" style="width: 263px; text-align: left">
                                            <b>Descripcion</b>
                                        </td>
                                        <td valign="middle" style="width: 80px; text-align: left">
                                            <b>Sub Cuenta</b>
                                        </td>
                                        <td valign="middle" style="width: 80px; text-align: left">
                                            <b>SubSub Cuenta</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table style="width:100%">
                                    <tr>
                                        <td valign="middle" style="width: 263px; text-align: left">
                                            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_Descripcion") %>' />
                                        </td>
                                        <td valign="middle" style="width: 80px; text-align: left">
                                            <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_SubCuenta") %>' />
                                        </td>
                                        <td valign="middle" style="width: 80px; text-align: left">
                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PagElecCuenta_SubSubCuenta") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                NoMatches="No hay coincidencias" />
                        </telerik:radcombobox>
                    </asp:Panel>
                </td>
                <td>
                    <asp:Label ID="LblEstatus" runat="server" Text="Estatus"></asp:Label>
                </td>
                <td>
                    <telerik:radcombobox id="CmbEstatus" runat="server">
                        <Items><telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Creado" Value="False" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Autorizado" Value="True" /></Items>
                    </telerik:radcombobox>
                </td>
                <td>
                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="rgPagoElectronico" runat="server" autogeneratecolumns="False"
                        gridlines="None" onneeddatasource="rgPagoElectronico_NeedDataSource" onitemcommand="rgPagoElectronico_ItemCommand"
                        onitemdatabound="rgPagoElectronico_ItemDataBound" pagesize="15" allowpaging="True"
                        mastertableview-nomasterrecordstext="No se encontraron registros." onpageindexchanged="rgPagoElectronico_PageIndexChanged">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Núm. Solicitud" UniqueName="Id_PagElec"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElecTipo" HeaderText="Id_PagElecTipo" UniqueName="Id_PagElecTipo" Visible="false"><HeaderStyle/></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecTipo_Descrpcion" HeaderText="Tipo" UniqueName="PagElecTipo_Descrpcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <%--jfcv 21 oct 2016 mejoras Ocultar columna de cuenta y de número , mostrar la de nombre_acr punto 12 --%>
                            <telerik:GridBoundColumn DataField="PagElecCuenta_Descripcion" HeaderText="Cuenta" UniqueName="PagElecCuenta_Descripcion" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <%--se comenta la columna de acreedor y dirá cta pago y será la clave del proveedor --%>
                            <telerik:GridBoundColumn DataField="Acr_NumeroGenerado" HeaderText="Cta. Pago" UniqueName="Acr_NumeroGenerado"><HeaderStyle Width="60" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Nombre" HeaderText="Proveedor/Acreedor" UniqueName="Acr_Nombre"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Solicitante" HeaderText="Solicitante" UniqueName="PagElec_Solicitante"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_FechaRequiere" HeaderText="Fecha" DataFormatString="{0:dd/MM/yy}" UniqueName="PagElec_FechaRequiere"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Cuenta" HeaderText="Número" UniqueName="PagElec_Cuenta" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Cc" HeaderText="C.C." UniqueName="PagElec_Cc"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubCuenta" HeaderText="Sub Cuenta" UniqueName="PagElec_SubCuenta"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubSubCuenta" HeaderText="Sub Sub-Cta" UniqueName="PagElec_SubSubCuenta"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                          <%-- ocultar la columna de cuenta pago y poner en su lugar la de Monto que tendrá el importe de la solicitud --%>
                            <telerik:GridBoundColumn DataField="PagElec_Id_PagElecCuenta" HeaderText="PagElec_Id_PagElecCuenta" UniqueName="PagElec_Id_PagElecCuenta" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_RFC" HeaderText="PagElec_RFC" UniqueName="PagElec_RFC" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_CuentaPago" HeaderText="Cta Pago." UniqueName="PagElec_CuentaPago" Visible="false" ><HeaderStyle Width="10"/></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Importe" HeaderText="Monto" DataFormatString="{0:C}" UniqueName="PagElec_Importe" ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElecEstatus_Descripcion" HeaderText="Estatus" UniqueName="PagElecEstatus_Descripcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_MotivoRechazo" HeaderText="Motivo Rechazo" UniqueName="PagElec_MotivoRechazo"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElecEstatus" HeaderText="cveEstatus" UniqueName="Id_PagElecEstatus" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                              <%-- jfcv 02 feb 2016--%>
                            <telerik:GridBoundColumn DataField="PagElec_UUID" HeaderText="PagElec_UUID" UniqueName="PagElec_UUID" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Subtotal" HeaderText="PagElec_Subtotal" UniqueName="PagElec_Subtotal" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Iva" HeaderText="PagElec_Iva" UniqueName="PagElec_Iva" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_ImpRetenido" HeaderText="PagElec_ImpRetenido" UniqueName="PagElec_ImpRetenido" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_IvaRetenido" HeaderText="PagElec_IvaRetenido" UniqueName="PagElec_IvaRetenido" Visible="false"></telerik:GridBoundColumn>
                         

                             <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                UniqueName="XML" Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Descargar" CommandName="XML" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                UniqueName="PDF" Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                           <%-- jfcv 5 oct 2015 Agregue la columna de soporte y validación si no tiene docs soporte no muestra el icono y el de comprobantes , si no tiene de soporte muestra el icono de comproante--%>
                            <telerik:GridTemplateColumn HeaderText="Soporte" DataField="PagElec_Soporte" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                UniqueName="Soporte" Visible= "true"  >
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSoporte" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Archivo de Soporte" CommandName="Soporte" Enabled="true" Visible ='<%#Eval("PagElec_Soporte") != null ? true : false  %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                           <telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"
                                UniqueName="Comprobantes">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes"  />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <%--jfcv 11 dic 2015 agregar botón para editar 17 dic cambiar icono a botón editar por lapiz --%>
                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgEditar" runat="server" ImageUrl="~/Imagenes/lapiz.gif"
                                         ToolTip="Editar" CommandName="Modificar"    />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="35px" />
                                <HeaderStyle Width="35px" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Desea cancelar la solicitud?" Text="Cancelar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                ConfirmDialogWidth="350px" HeaderText="Cancelar">
                                <HeaderStyle Width="25px" />
                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="25px" />
                            </telerik:GridButtonColumn>
                            

                        </Columns>
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
        <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
        <script type="text/javascript">
            function AbrirVentana_Gastos(Id, guardar, modificar, eliminar, imprimir) {
                var oWnd = radopen("CapPagosElectronicos.aspx?Id=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir, "AbrirVentana_Gastos");
                oWnd.center();
            }

            function AbrirVentana_LstComprobantes(Id) {
                var oWnd = radopen("CapPagosElectronicos_Listado.aspx?Id=" + Id, "AbrirVentana_LstComprobantes");
                oWnd.center();
            }

            function AbrirVentana_GastosModificar(id) {
                var oWnd = radopen("CapPagosElectronicosModifica.aspx?Id=" + id, "AbrirVentana_GastosModificar");
                oWnd.center();
            }

            function ObtenerImporte() {
                alert('ddd');
            }


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

            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }
            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(true);
                oWnd.center();
            }
            function cmdClient_Focus(sender, args) {
                var input = sender.get_inputDomElement();
                sender.highlightAllMatches(input.defaultValue);
                sender.showDropDown();
            }
        </script>
    </telerik:radcodeblock>
</asp:Content>

<%@ Page Title="Autorización Gasto" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="ProAutorizacion_PagoElectronico.aspx.cs" Inherits="SIANWEB.ProAutorizacion_PagoElectronico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsignacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
           
             <telerik:AjaxSetting AjaxControlID="CmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="CmbSubTipo">
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

        </AjaxSettings>
    </telerik:radajaxmanager>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_Gastos" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="900px" Height="600px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Captura Gastos" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
            <%-- Factura (Impresion PDF) --%>
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
        <%--JFCV 24 oct que pueda filtrar punto 13--%>
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
                    <asp:Label ID="LblSubTipo" runat="server" Text="SubTipo"></asp:Label>
                </td>
                <td>
                     <telerik:radcombobox id="CmbSubTipo" runat="server" autopostback="True" >
                            <Items>   
                                <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" />  
                                <telerik:RadComboBoxItem runat="server" Text="Flete" Value="1" />
                                <telerik:RadComboBoxItem runat="server" Text="No Inventariable" Value="2" />
                                <telerik:RadComboBoxItem runat="server" Text="Compra Local" Value="3" />
                                <telerik:RadComboBoxItem runat="server" Text="Pagos de Servicios" Value="4" />
                                <telerik:RadComboBoxItem runat="server" Text="Otros" Value="5" /> 
                                <telerik:RadComboBoxItem runat="server" Text="Honorarios" Value="6" /> 
                                <telerik:RadComboBoxItem runat="server" Text="Arrendamientos" Value="7" /> 
                            </Items>    
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
                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" />
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
                            <telerik:GridBoundColumn DataField="PagElecTipo_Descrpcion" HeaderText="Tipo" UniqueName="PagElecTipo_Descrpcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElecTipo" HeaderText="Id_PagElecTipo" UniqueName="Id_PagElecTipo" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <%--jfcv agregar sub tipo Id_PagElecSubTipo--%>
                            <telerik:GridBoundColumn DataField="Id_PagElecSubTipo" HeaderText="Id_PagElecSubTipo" UniqueName="Id_PagElecSubTipo" Visible="false" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubTipoDescripcion" HeaderText="SubTipo" UniqueName="PagElec_SubTipoDescripcion" Visible="true" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="PagElecConcepto_Descripcion" HeaderText="Concepto" UniqueName="PagElecConcepto_Descripcion" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Acr" HeaderText="Id_Acr" UniqueName="Id_Acr" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <%--se comenta la columna de acreedor y dirá cta pago y será la clave del proveedor --%>
                             <%--//jfcv 24oct2016  inicio punto 13--%>
                            <telerik:GridBoundColumn DataField="Acr_NumeroGenerado" HeaderText="Cta. Pago" UniqueName="Acr_NumeroGenerado"><HeaderStyle Width="60" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_Nombre" HeaderText="Proveedor/Acreedor" UniqueName="Acr_Nombre"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Solicitante" HeaderText="Solicitante" UniqueName="PagElec_Solicitante" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <%--//jfcv 24oct2016  fin punto 13--%>
                            <telerik:GridBoundColumn DataField="PagElec_FechaRequiere" HeaderText="Fecha" DataFormatString="{0:dd/MM/yy}" UniqueName="PagElec_FechaRequiere"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Cuenta" HeaderText="Número" UniqueName="PagElec_Cuenta" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Cc" HeaderText="C.C." UniqueName="PagElec_Cc" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubCuenta" HeaderText="Sub Cuenta" UniqueName="PagElec_SubCuenta" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_SubSubCuenta" HeaderText="Sub Sub-Cta" UniqueName="PagElec_SubSubCuenta" Visible="false"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                             <%-- ocultar la columna de cuenta pago y poner en su lugar la de Monto que tendrá el importe de la solicitud --%>
                            <telerik:GridBoundColumn DataField="PagElec_CuentaPago" HeaderText="Cta Pago." UniqueName="PagElec_CuentaPago" Visible="false" ><HeaderStyle Width="10"/></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Importe" HeaderText="Monto" DataFormatString="{0:C}" UniqueName="PagElec_Importe" ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <%--JFCV 30 oct para poder enviar el archivo de soporte a macola necesito agregar estas dos columnas ocultas --%>
                            <telerik:GridBoundColumn DataField="PagElec_Importe" HeaderText="montocalcular" UniqueName="PagElec_ImporteSumar" Visible="false" ><HeaderStyle Width="10"/></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PagElec_Soporte" HeaderText="soporte" UniqueName="PagElec_Soporte" Visible="false" ><HeaderStyle Width="10"/></telerik:GridBoundColumn>
                            
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
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px"
                                UniqueName="Comprobantes">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes"  />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                             <%--<telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px"
                                UniqueName="Comprobantes">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes" Visible ='<%#Eval("PagElec_Soporte") != null ? false : true  %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>--%>

                            <telerik:GridTemplateColumn HeaderText="Número Referencia" UniqueName="Acr_NumeroGenerado">
                                <ItemTemplate>
                                    <%-- JFCV cambiar en lugar numerico que sea texto<telerik:RadNumericTextBox ID="TxtNumeroAcreedor" runat="server"><NumberFormat DecimalDigits="0" AllowRounding="false" /></telerik:RadNumericTextBox>--%>
                                    <telerik:RadTextBox ID="TxtNumeroAcreedor" runat="server" MaxLength="30"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Desea autorizar el gasto?</br></br>" Text="Autorizar"
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" UniqueName="Autorizar"
                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>
                            <%-- JFCV 18 dic 2015 agregar botón de rechazar y Motivo de Rechazo --%>
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
                var oWnd = radopen("CapPagosElectronicos_Listado.aspx?Id=" + Id, "AbrirVentana_LstComprobantes");
                oWnd.center();
            }
            //jfcv 24oct2016  punto 13
            function cmdClient_Focus(sender, args) {
                var input = sender.get_inputDomElement();
                sender.highlightAllMatches(input.defaultValue);
                sender.showDropDown();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

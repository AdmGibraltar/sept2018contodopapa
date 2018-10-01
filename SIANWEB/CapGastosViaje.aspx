<%@ Page Title="Gastos de Viaje" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CapGastosViaje.aspx.cs" Inherits="SIANWEB.CapGastosViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsignacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbDesasingar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbProgramaReparto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbPickingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbAsignar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbPickingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
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
                <td><asp:Label ID="LblSolicitante" runat="server" Text="Persona que Viaja"></asp:Label></td>
                <td colspan="2"><telerik:RadTextbox ID="TxtSolicitante" runat="server"></telerik:RadTextbox></td>
            </tr>
            <tr>
                <td><asp:Label ID="LblMotivo" runat="server" Text="Motivo del Viaje"></asp:Label></td>
                <td><telerik:RadTextbox ID="TxtMotivo" runat="server"></telerik:RadTextbox></td>
                <td><asp:Label ID="LblFechaSalida" runat="server" Text="Fecha Salida"></asp:Label></td>
                <td><telerik:RadDatePicker ID="TxtFechaSalida" runat="server" Culture="es-MX" Width="100px" AutoPostBack="true" OnSelectedDateChanged="TxtFechaSalida_SelectedDateChanged"></telerik:RadDatePicker></td>
                <td><asp:Label ID="LblFechaRegreso" runat="server" Text="Fecha Regreso"></asp:Label></td>
                <td><telerik:RadDatePicker ID="TxtFechaRegreso" runat="server" Culture="es-MX" Width="100px" AutoPostBack="true" OnSelectedDateChanged="TxtFechaRegreso_SelectedDateChanged"></telerik:RadDatePicker></td>
                <td><asp:Label ID="LblCantidadDias" runat="server" Text="Dias de Viaje"></asp:Label></td>
                <td><telerik:RadTextBox ID="TxtCantidadDias" runat="server" Enabled="false"></telerik:RadTextBox></td>
            </tr>
        </table>
<%--        <table style="font-family: Verdana; font-size: 8pt" style="display:none">
            <tr>
                <td><asp:Label ID="LblConcepto" runat="server" Text="Concepto"></asp:Label></td>
                <td>
                    <telerik:RadCombobox ID="CmbConcepto" runat="server" OnSelectedIndexChanged="CmbConcepto_SelectedIndexChanged" autopostback="True">
                    </telerik:RadCombobox>
                </td>
                <td>
                    <telerik:RadCombobox ID="CmbTipoComprobante" runat="server">
                        <Items><telerik:RadComboBoxItem runat="server" Text="Con Comprobante" Value="1" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Sin Comprobante" Value="2" /></Items>
                    </telerik:RadCombobox>
                </td>
            </tr>
            <tr>
                <td colspan="12">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="LblXml" runat="server" Text="XML"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadUpload ID="RadUpload1" runat="server" MaxFileInputsCount="1" OverwriteExistingFiles="false" ControlObjectsVisibility="none" localization-select="seleccione">
                                </telerik:RadUpload>
                            </td>
                            <td>
                                <asp:Button ID="BtnObtenerImporte" runat="server" Text="Importe" OnClick="BtnObtenerImporte_Click" />
                            </td>
                            <td>
                                <asp:Label ID="LblImporte" runat="server" Text="Importe"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextbox ID="TxtImporte" runat="server"></telerik:RadTextbox>
                                <asp:RequiredFieldValidator ID="RfvImporte" runat="server" ControlToValidate="TxtImporte" InitialValue="1" Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="12">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="LblPdf" runat="server" Text="PDF"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadUpload ID="RadUpload2" runat="server" MaxFileInputsCount="1" OverwriteExistingFiles="false" ControlObjectsVisibility="none" localization-select="seleccione">
                                </telerik:RadUpload>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                </td>
                <td colspan="5">
                    <telerik:RadTextbox ID="TxtObservaciones" runat="server" width="300px"></telerik:RadTextbox>
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="BtnAgregar" runat="server" Text="Agregar Comprobante" OnClick="BtnAgregar_Click" /></td>
            </tr>
        </table>
--%>
        <br />
        <table style="font-family: Verdana; font-size: 8pt" style="display:none">
            <tr>
                <td>
                    <asp:Label ID="lblTransporte" runat="server" Text="Tipo Transporte"></asp:Label>
                </td>
                <td>
                    <telerik:RadCombobox ID="CmbTipoComprobante" runat="server">
                        <Items><telerik:RadComboBoxItem runat="server" Text="Autobus" Value="1" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Avion" Value="2" /></Items>
                    </telerik:RadCombobox>
                </td>
                <td></td>
                <td>
                    <telerik:RadTextbox ID="TxtTransporteCuota" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
            </tr>
            <tr>
                <td><br /></td>
            </tr>
            <tr>
                <td></td>
                <td>NO. DIAS</td>
                <td>CUOTA DIARIA</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblHospedaje" runat="server" Text="Hospedaje"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtHospedajeDias" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtHospedajeCutoa" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtHospedajeImporte" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
            </tr>
            <tr>
                <td><br /></td>
            </tr>
            <tr>
                <td colspan="3">ALIMENTACION</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblDesayunos" runat="server" Text="Desayunos"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtDesayunosDias" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtDesayunosCutoa" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtDesayunosImporte" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblComidas" runat="server" Text="Comidas"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtComidasDias" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtComidasCutoa" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtComidasImporte" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblCenas" runat="server" Text="Cenas"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtCenasDias" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtCenasCutoa" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtCenasImporte" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblOtrosGastos" runat="server" Text="Otros Gastos"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtOtrosGastosCutoa" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtOtrosGastosDias" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtOtrosGastosImporte" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="LblTotal" runat="server" Text="Total"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextbox ID="TxtTotal" runat="server" Enabled="false"></telerik:RadTextbox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgPagoElectronico" runat="server" AutoGenerateColumns="False" GridLines="None" Visible="false"
                    OnNeedDataSource="rgPagoElectronico_NeedDataSource" OnItemCommand="rgPagoElectronico_ItemCommand"
                    PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Comprobante" HeaderText="Con/Sin" UniqueName="Id_PagElec"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Tipo" HeaderText="Concepto" UniqueName="PagElecTipo_Descrpcion"><HeaderStyle Width="180" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" UniqueName="PagElecConcepto_Descripcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" UniqueName="PagElec_Importe"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                UniqueName="XML">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Descargar" CommandName="XML" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                        UniqueName="PDF">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
<%--        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td colspan="5">
                    <table cellpadding="0" cellspacing="0" id="TblConceptos">
                        <tr>
                            <td>Concepto del Gasto</td>
                            <td>Importe</td>
                            <td>IVA</td>
                            <td>Total</td>
                        </tr>
                        <tr>
                            <td>TRANSPORTE (AVION, AUTOBUS)</td>
                            <td><telerik:RadNumericTextBox ID="TxtImporteTransporte" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtIVATransporte" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtTotalTransporte" runat="server"></telerik:RadNumericTextBox></td>
                        </tr>
                        <tr>
                            <td>HOSPEDAJE</td>
                            <td><telerik:RadNumericTextBox ID="TxtImporteHospedaje" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtIVAHospedaje" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtTotalHospedaje" runat="server"></telerik:RadNumericTextBox></td>
                        </tr>
                        <tr>
                            <td>ALIMENTACION</td>
                            <td><telerik:RadNumericTextBox ID="TxtImporteAlimentacion" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtIVAAlimentacion" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtTotalAlimentacion" runat="server"></telerik:RadNumericTextBox></td>
                        </tr>
                        <tr>
                            <td>TAXIS</td>
                            <td><telerik:RadNumericTextBox ID="TxtImporteTaxis" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtIVATaxis" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtTotalTaxis" runat="server"></telerik:RadNumericTextBox></td>
                        </tr>
                        <tr>
                            <td>LLAMADAS TELEFONICAS</td>
                            <td><telerik:RadNumericTextBox ID="TxtImporteTelefono" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtIVATelefono" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtTotalTelefono" runat="server"></telerik:RadNumericTextBox></td>
                        </tr>
                        <tr>
                            <td>CUOTAS DE CAMINOS</td>
                            <td><telerik:RadNumericTextBox ID="TxtImporteCuotas" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtIVACuotas" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtTotalCuotas" runat="server"></telerik:RadNumericTextBox></td>
                        </tr>
                        <tr>
                            <td>OTROS</td>
                            <td><telerik:RadNumericTextBox ID="TxtImporteOtros" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtIVAOtros" runat="server"></telerik:RadNumericTextBox></td>
                            <td><telerik:RadNumericTextBox ID="TxtTotalOtros" runat="server"></telerik:RadNumericTextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
--%>    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
        <script type="text/javascript">
            function ObtenerImporte() {
                alert('ddd');
            }

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


        </script>
    </telerik:radcodeblock>

</asp:Content>

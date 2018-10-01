<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatConsecutivoFacEle.aspx.cs" Inherits="SIANWEB.Cat_ConsecutivoFacEle" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbEmpresa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="cmbTipoMovimiento" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAcuse">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="rtb1_ButtonClick">
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
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label14" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px">
                    <telerik:RadComboBox ID="RadComboBox1" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
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
                                <asp:Label ID="Label5" runat="server" Text="Serie del consecutivo"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtAcuse" runat="server" TabIndex="1"
                                    Width="150px" MaxLength="10">
                                    <ClientEvents OnKeyPress="SoloAlfabetico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAcuse"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Año de aprobación"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtAñoAprobacion" runat="server" TabIndex="6" MaxLength="4"
                                    MinValue="0" Width="150px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAñoAprobacion"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Tipo de movimiento"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTipoMovimiento" runat="server" Width="153px" TabIndex="1"
                                    Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando...">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Facturaci&oacute;n" Value="1" />
                                        <telerik:RadComboBoxItem Text="Nota de cargo" Value="2" />
                                        <telerik:RadComboBoxItem Text="Nota de cr&eacute;dito" Value="3" />
                                        <telerik:RadComboBoxItem Text="Pago" Value="4" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ID="ValidatorComboEmp" ControlToValidate="cmbTipoMovimiento"
                                    ErrorMessage="*Requerido" InitialValue="-- Seleccionar --" ValidationGroup="guardar"
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Folio de aprobación"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFolioAprobacion" runat="server" TabIndex="7" MaxLength="9"
                                    MinValue="0" Width="150px">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFolioAprobacion"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Razón social"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtRazonSocial" runat="server" TabIndex="2"
                                    MaxLength="50" Width="150px">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRazonSocial"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Rango inicial de folio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFolioIni" runat="server" TabIndex="8" MaxLength="9"
                                    MinValue="0" Width="150px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" OnBlur="validarFolios" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFolioIni"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Número certificado de la razón social"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtCertificadoRS" runat="server" TabIndex="3" MaxLength="50"
                                    Width="150px">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCertificadoRS"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Rango final de folio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFolioFin" runat="server" TabIndex="9" MaxLength="9"
                                    MinValue="0" Width="150px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" OnBlur="validarFolios" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtFolioFin"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                <asp:Label ID="Label15" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Folio de aprobación del SAT-llave"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtLlaveSat" runat="server" TabIndex="4" MaxLength="10" Width="150px">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLlaveSat"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Último folio utilizado"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtUltimoFolio" runat="server" TabIndex="10" MaxLength="9"
                                    MinValue="0" Width="150px" Enabled="False">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                    <ClientEvents OnKeyPress="handleClickEvent" OnBlur="EvaluarRango" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label16" runat="server" ForeColor="Red"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Fecha de vigencia del certificado"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpVigencia" runat="server" Width="155px" TabIndex="5"
                                    Culture="es-MX">
                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                                        TabIndex="5">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" TabIndex="5" ToolTip="Abrir calendario">
                                    </DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="dpVigencia"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
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
                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" TabIndex="11"
                                    AutoPostBack="True" OnCheckedChanged="chkActivo_CheckedChanged" />
                            </td>
                            <td colspan="2">
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
                                &nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="HF_Id" runat="server" />
                                <asp:HiddenField ID="HF_Tipo" runat="server" />
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
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgAcuse" runat="server" AutoGenerateColumns="False" Width="650px"
                                    AllowPaging="True" PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                    OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemCommand"
                                    OnPageIndexChanged="RadGrid1_PageIndexChanged" TabIndex="12" GridLines="None">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="Id" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NombreAcuse" HeaderText="Serie del consecutivo"
                                                UniqueName="NombreAcuse">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FolioSAT" HeaderText="FolioSAT" UniqueName="FolioSAT"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Año" HeaderText="Año" UniqueName="Año" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RazonSocial" HeaderText="Razón social" UniqueName="RazonSocial">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NumRazonSocial" HeaderText="NumRazonSocial" UniqueName="NumRazonSocial"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UltimoFolio" HeaderText="UltimoFolio" UniqueName="UltimoFolio"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RangoInicial" HeaderText="RangoInicial" UniqueName="RangoInicial"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RangoFinal" HeaderText="RangoFinal" UniqueName="RangoFinal"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RangoFecha" HeaderText="RangoFecha" UniqueName="RangoFecha"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TipoMovimiento" HeaderText="TipoMovimiento" UniqueName="TipoMovimiento"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FolioAprovacion" HeaderText="FolioAprovacion"
                                                UniqueName="FolioAprovacion" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Empresa" HeaderText="Empresa" UniqueName="Empresa"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CentroDistribucion" HeaderText="Centro de distribución"
                                                UniqueName="CentroDistribucion" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                //--------------------------------------------------------------------------------------------------
                //Limpiar controles de formulario  
                //--------------------------------------------------------------------------------------------------
                function LimpiarControles() {
                    var ultimo = $find('<%= txtUltimoFolio.ClientID %>');
                    LimpiarTextBox($find('<%= txtAcuse.ClientID %>'));
                    LimpiarTextBox($find('<%= txtRazonSocial.ClientID %>'));
                    LimpiarTextBox($find('<%= txtCertificadoRS.ClientID %>'));
                    LimpiarTextBox($find('<%= txtLlaveSat.ClientID %>'));
                    LimpiarTextBox($find('<%= txtAñoAprobacion.ClientID %>'));
                    LimpiarTextBox($find('<%= txtFolioAprobacion.ClientID %>'));
                    LimpiarTextBox($find('<%= txtFolioIni.ClientID %>'));
                    LimpiarTextBox($find('<%= txtFolioFin.ClientID %>'));
                    LimpiarTextBox(ultimo);
                    LimpiarDatePicker($find('<%= dpVigencia.ClientID %>'));
                    LimpiarComboSelectIndex0($find('<%= cmbTipoMovimiento.ClientID %>'));
                    LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);
                    ultimo.disable();
                    var lbl = document.getElementById('<%= Label15.ClientID %>');
                    lbl.innerHTML = "";
                    var lbl2 = document.getElementById('<%= Label16.ClientID %>');
                    lbl2.innerHTML = "";
                }

                //--------------------------------------------------------------------------------------------------
                //Cuando un botón del toolBar es clickeado
                //--------------------------------------------------------------------------------------------------
                function ToolBar_ClientClick(sender, args) {
                    var continuarAccion = true;
                    var habilitaValidacion = false;
                    var button = args.get_item();
                    //habilitar/deshabilitar validators
                    if (button.get_value() == 'save')
                        habilitaValidacion = true;
                    else {
                        habilitaValidacion = false;
                    }
                    for (i = 0; i < Page_Validators.length; i++) {
                        ValidatorEnable(Page_Validators[i], habilitaValidacion);
                    }

                    switch (button.get_value()) {
                        case 'new':
                            //debugger;
                            LimpiarControles();
                            //registro nuevo -> se limpia bandera de actualización
                            var hiddenActualiza = document.getElementById('<%= HF_Id.ClientID %>');
                            hiddenActualiza.value = '';
                            var hiddenActualiza = document.getElementById('<%= HF_Tipo.ClientID %>');
                            hiddenActualiza.value = '';
                            continuarAccion = false;
                            break;
                        case 'save':
                            continuarAccion = validarFolios();
                            break;
                    }
                    args.set_cancel(!continuarAccion);
                }
                function validarFolios() {
                    var fin = $find('<%= txtFolioFin.ClientID %>');
                    var ini = $find('<%= txtFolioIni.ClientID %>');
                    var lbl = document.getElementById('<%= Label15.ClientID %>');
                    var ultimo = $find('<%= txtUltimoFolio.ClientID %>');
                    if (fin.get_value() < ultimo.get_value()) {
                        lbl.innerHTML = "*El folio final no puede ser menor al último folio utilizado";
                        return false;
                    }
                    if (ini.get_value() > fin.get_value()) {
                        lbl.innerHTML = "*El folio final no puede ser menor al folio inicial";
                        return false;
                    } else {
                        lbl.innerHTML = "";
                        return true;
                    }
                }
                function EvaluarRango() {
                    //debugger;
                    var fin = $find('<%= txtFolioFin.ClientID %>');
                    var ini = $find('<%= txtFolioIni.ClientID %>');
                    var ultimo = $find('<%= txtUltimoFolio.ClientID %>');
                    var lbl = document.getElementById('<%= Label16.ClientID %>');
                    if (ultimo.get_value() < ini.get_value() || ultimo.get_value() > fin.get_value()) {
                        lbl.innerHTML = "*El último folio debe estar entre el folio inicial y el folio final";
                        return false;
                    }
                    else {
                        lbl.innerHTML = "";
                        return true;
                    }
                }                
            </script>
        </telerik:RadCodeBlock>
    </div>
</asp:Content>

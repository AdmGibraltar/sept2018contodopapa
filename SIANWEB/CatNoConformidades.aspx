<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true"
    CodeBehind="CatNoConformidades.aspx.cs" Inherits="SIANWEB.CatNoConformidades" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescripcion.ClientID %>'));
                LimpiarTextBox($find('<%= txtAplica.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbTipo.ClientID %>'));
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
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
                        LimpiarControles();
                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                        hiddenActualiza.value = '';
                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtClave.ClientID %>');
                        txtId.enable();
                        txtId.focus();
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatNoConformidades";
                        parametros = parametros + "&sp=spCatCentral_Maximo";
                        parametros = parametros + "&columna=Id_Nco";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        continuarAccion = false;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
        </script>
    </telerik:RadCodeBlock>
    <div>
        <telerik:RadAjaxManager ID="RAM1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="chkActivo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rg1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
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
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" Width="150px"
                            AutoPostBack="True" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1">
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
                                    <asp:Label ID="lblClave" runat="server" Text="Clave"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave"
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
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox onpaste="return false" ID="txtDescripcion" runat="server" Width="300px"
                                        MaxLength="80">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcion"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbTipo" runat="server" Width="150px" Filter="Contains"
                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                        LoadingMessage="Cargando...">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />
                                            <telerik:RadComboBoxItem Text="1 - Producto" Value="1" />
                                            <telerik:RadComboBoxItem Text="2 - Servicio administrativo/operativo" Value="2" />
                                            <telerik:RadComboBoxItem Text="3 - Servicio de asesoría" Value="3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="val_cmbTipo" ControlToValidate="cmbTipo"
                                        ErrorMessage="*Requerido" InitialValue="-- Seleccionar --" ValidationGroup="guardar"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblAplica" runat="server" Text="Aplica"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox onpaste="return false" ID="txtAplica" runat="server" Rows="4"
                                        TextMode="MultiLine" Width="300px" MaxLength="250">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
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
                                    &nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" AutoPostBack="True" OnCheckedChanged="chkActivo_CheckedChanged"
                                        Checked="True" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:HiddenField ID="HF_ID" runat="server" />
                                    <asp:HiddenField ID="HF_Tipo" runat="server" />
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
                                <td width="70">
                                    &nbsp;
                                </td>
                                <td width="75">
                                    &nbsp;
                                </td>
                                <td width="150">
                                    &nbsp;
                                </td>
                                <td width="100">
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
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="rg1" runat="server" PageSize="15" AllowPaging="true" OnNeedDataSource="rg1_NeedDataSource"
                                        OnPageIndexChanged="rg1_PageIndexChanged" AutoGenerateColumns="False" GridLines="None"
                                        OnItemCommand="rg1_ItemCommand" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                                        <MasterTableView>
                                            <RowIndicatorColumn>
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn>
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Emp" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_Nco" HeaderText="Clave">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Descripción" DataField="Nco_Descripcion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nco_Tipo" HeaderText="Tipo" Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nco_TipoStr" HeaderText="Tipo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nco_Aplica" HeaderText="Aplica" Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nco_Activo" Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nco_ActivoStr" HeaderText="Estatus">
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
        </div>
</asp:Content>

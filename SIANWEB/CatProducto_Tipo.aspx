<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatProducto_Tipo.aspx.cs" Inherits="SIANWEB.CatProducto_Tipo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario de Tipo de productos
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesPrecios() {

                var txtIdTipoProducto = $find('<%= txtIdTipoProducto.ClientID %>');
                var cmbTipoProducto = $find('<%= cmbTipoProducto.ClientID %>');
                var txtDescripcionTipoProducto = $find('<%= txtDescripcionTipoProducto.ClientID %>');
                var chkActivoTipoProducto = document.getElementById('<%= chkActivoTipoProducto.ClientID %>');

                LimpiarTextBox(txtIdTipoProducto);
                LimpiarComboSelectIndex0(cmbTipoProducto);
                LimpiarTextBox(txtDescripcionTipoProducto);
                LimpiarCheckBox(chkActivoTipoProducto, true);
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
                        //debugger;

                        LimpiarControlesPrecios();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= hiddenActualiza.ClientID %>');
                        hiddenActualiza.value = '';

                        //poner el foco en campo Clave
                        var txtIdTipoProducto = $find('<%= txtIdTipoProducto.ClientID %>');
                        txtIdTipoProducto.enable();

                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatTProducto";
                        parametros = parametros + "&sp=spCatCentral_Maximo";
                        parametros = parametros + "&columna=Id_Ptp";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtIdTipoProducto.set_value(resultado);

                        txtIdTipoProducto.focus();

                        continuarAccion = true;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivoTipoProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgTipoProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div class="formulario" id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
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
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenActualiza" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribuci&oacute;n" />
                </td>
               <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <!-- Tabla principal--->
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 70px">
                                <asp:Label ID="Label2" runat="server" Text="Clave" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIdTipoProducto" runat="server" Width="50px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_txtIdTipoProducto" runat="server" ControlToValidate="txtIdTipoProducto"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 70px">
                                <asp:Label ID="Label3" runat="server" Text="Tipo" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTipoProducto" runat="server" Width="150px" Filter="Contains"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                    LoadingMessage="Cargando...">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Accesorios" Value="A" />
                                        <telerik:RadComboBoxItem Text="Aditamento" Value="D" />
                                        <telerik:RadComboBoxItem Text="Productos" Value="P" />
                                        <telerik:RadComboBoxItem Text="Otros" Value="O" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_cmbTipoProducto" runat="server" ControlToValidate="cmbTipoProducto"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                    ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 70px">
                                <asp:Label ID="Label4" runat="server" Text="Descripci&oacute;n" />
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtDescripcionTipoProducto" runat="server"
                                    Width="200px" MaxLength="40">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_txtDescripcionTipoProducto" runat="server" ControlToValidate="txtDescripcionTipoProducto"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 70px">
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivoTipoProducto" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                    AutoPostBack="True" Checked="True" />
                            </td>
                        </tr>
                        <tr>
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
        </table>
        <table>
            <tr>
                <td>
                </td>
                <td>
                <telerik:RadGrid ID="rgTipoProducto" runat="server" AutoGenerateColumns="False" GridLines="None"
                    PageSize="15" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                    OnItemCommand="rgTipoProducto_ItemCommand" OnNeedDataSource="rgTipoProducto_NeedDataSource"
                    OnPageIndexChanged="rgTipoProducto_PageIndexChanged">
                    <MasterTableView DataKeyNames="Id_Ptp" DataMember="listTipoProductos">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Clave" UniqueName="Id_Ptp" DataField="Id_Ptp">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Tipo" UniqueName="Ptp_Tipo" DataField="Ptp_Tipo"
                                Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Tipo" UniqueName="Ptp_Tipo_Str" DataField="Ptp_Tipo_Str">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Descripci&oacute;n" UniqueName="Ptp_Descripcion"
                                DataField="Ptp_Descripcion">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ptp_Activo" HeaderText="Estatus" UniqueName="Ptp_Activo"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ptp_ActivoStr" HeaderText="Estatus" UniqueName="Ptp_ActivoStr">
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
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

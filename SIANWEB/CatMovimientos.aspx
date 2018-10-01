<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatMovimientos.aspx.cs" Inherits="SIANWEB.CatMovimientos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbCobranza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cmbTipo" />
                    <telerik:AjaxUpdatedControl ControlID="cmbNaturaleza" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbInventario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cmbTipo" />
                    <telerik:AjaxUpdatedControl ControlID="cmbNaturaleza" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtInverso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="cmbInverso" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbInverso">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtInverso" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgMovimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="True" dir="rtl"
            Width="100%" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" CssClass="mail" ToolTip="Correo" ImageUrl="~/Imagenes/blank.png"
                    Enabled="false" />
                <telerik:RadToolBarButton CommandName="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" Enabled="false" />
                <telerik:RadToolBarButton CommandName="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" Enabled="false" />
                <telerik:RadToolBarButton CommandName="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png"
                    Enabled="false">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="save" ToolTip="Guardar" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
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
                    <asp:Label ID="Label7" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
               <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <table style="font-family: verdana; font-size: 8pt">
                        <tr>
                            <td colspan="5">
                                <asp:RadioButton ID="rbCobranza" runat="server" Checked="True" Text="Cobranza" AutoPostBack="True"
                                    OnCheckedChanged="rb_CheckedChanged" GroupName="MovNaturaleza" />
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="rbInventario" runat="server" Text="Inventario" AutoPostBack="True"
                                    OnCheckedChanged="rb_CheckedChanged" GroupName="MovNaturaleza" />
                                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
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
                                <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbTipo" runat="server" Width="155px" Filter="Contains"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                    LoadingMessage="Cargando...">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="cmbTipo"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="200px"
                                    MaxLength="40">
                                    <ClientEvents OnKeyPress="SoloAlfabetico" />
                                </telerik:RadTextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtNombre"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="txtNumero" runat="server" MinValue="1" Width="70px"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtNumero"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="1">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Naturaleza"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbNaturaleza" runat="server" AllowCustomText="False" Width="150px"
                                    Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    DataTextField="Descripcion" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="cmbNaturaleza"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Movimiento inverso"></asp:Label>
                            </td>
                            <td colspan="1">
                                <telerik:RadNumericTextBox ID="txtInverso" runat="server" MinValue="0" Width="70px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txt_OnBlur" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="cmbInverso" runat="server" OnClientSelectedIndexChanged="cmb_ClientSelectedIndexChanged"
                                    Width="300px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 50px; text-align: center">
                                                    <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                </td>
                                                <td style="width: 200px; text-align: left">
                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trAfecta" runat="server" visible="false">
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Afecta"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbAfecta" runat="server" Width="150px" EmptyMessage="-- Seleccionar --"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="cmbAfecta"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="trCobranza">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                <asp:CheckBox ID="chkVenta" runat="server" Text="Afecta venta" />
                                <br />
                                <asp:CheckBox ID="chkIva" runat="server" Text="Afecta IVA" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="trInventario" visible="false">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                <asp:CheckBox ID="chkOrden" runat="server" Text="Afecta orden de compra" />
                                <br />
                                <asp:CheckBox ID="chkReqRef" runat="server" Text="Requiere referencia" />
                                <br />
                                <asp:CheckBox ID="chkReqSpo" runat="server" Text="Requiere sistema de propietarios" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HFId_Mov" runat="server" />
                            </td>
                            <td width="70px">
                                &nbsp;
                            </td>
                            <td width="80px">
                                &nbsp;
                            </td>
                            <td width="250">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgMovimiento" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="rgMovimiento_ItemCommand"
                        PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnPageIndexChanged="rgMovimiento_PageIndexChanged">
                        <MasterTableView>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id" HeaderText="Clave" UniqueName="Id">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" UniqueName="Nombre">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" UniqueName="Tipo" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Naturaleza" HeaderText="Naturaleza" UniqueName="Naturaleza"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Inverso" HeaderText="Inverso" UniqueName="Inverso"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AfeIVA" HeaderText="AfeIVA" UniqueName="AfeIVA"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AfeVta" HeaderText="AfeVta" UniqueName="AfeVta"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AfeOrdComp" HeaderText="AfeOrdComp" UniqueName="AfeOrdComp"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Afecta" HeaderText="Afecta" UniqueName="Afecta"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ReqSispropietario" HeaderText="ReqSispropietario"
                                    UniqueName="ReqSispropietario" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ReqReferencia" HeaderText="ReqReferencia" UniqueName="ReqReferencia"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NatMov" HeaderText="NatMov" UniqueName="NatMov"
                                    Visible="false">
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
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function txt_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbInverso.ClientID %>'));
            }

            function cmb_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtInverso.ClientID %>'));
            }


            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtNombre.ClientID %>'));
                LimpiarTextBox($find('<%= txtNumero.ClientID %>'));
                LimpiarTextBox($find('<%= txtInverso.ClientID %>'));

                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);

                if (document.getElementById('<%= chkVenta.ClientID %>') != null) {
                    LimpiarCheckBox(document.getElementById('<%= chkVenta.ClientID %>'));
                    LimpiarCheckBox(document.getElementById('<%= chkIva.ClientID %>'));
                } else {
                    LimpiarCheckBox(document.getElementById('<%= chkOrden.ClientID %>'));
                    LimpiarCheckBox(document.getElementById('<%= chkReqRef.ClientID %>'));
                    LimpiarCheckBox(document.getElementById('<%= chkReqSpo.ClientID %>'));
                }

                LimpiarComboSelectIndex0($find('<%= cmbTipo.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbNaturaleza.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbInverso.ClientID %>'));

                if ($find('<%= cmbAfecta.ClientID %>') != null) {
                    LimpiarComboSelectIndex0($find('<%= cmbAfecta.ClientID %>'));
                }
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

                        LimpiarControles();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HFId_Mov.ClientID %>');
                        hiddenActualiza.value = '';

                        var radio = document.getElementById('<%= rbCobranza.ClientID %>');

                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtNumero.ClientID %>');
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatTMovimiento";
                        parametros = parametros + "&sp=spCatCentral_MaximoMov";
                        parametros = parametros + "&columna=Id_Tm";
                        parametros = parametros + "&naturaleza=" + radio.status;
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        txtId.enable();
                        txtId.focus();

                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

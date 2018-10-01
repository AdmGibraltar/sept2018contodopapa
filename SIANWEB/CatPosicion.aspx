<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CatPosicion.aspx.cs" Inherits="SIANWEB.CatPosicion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbUEN">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl"
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
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
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
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
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
                                <asp:Label ID="Label5" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" 
                                    MaxLength="9" MinValue="1">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="*Requerido" 
                                    ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Descripción
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtDescripcion" runat="server" 
                                    Width="320px" MaxLength="150">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="*Requerido" 
                                    ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Unidades estratégicas de negocio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtUen" runat="server" Width="70px" 
                                    MaxLength="9" MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txt1_OnBlur" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbUEN" runat="server" OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged"
                                    Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    DataTextField="Descripcion" DataValueField="Id" Width="250px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." 
                                    OnClientBlur="Combo_ClientBlur" 
                                    onselectedindexchanged="cmbUEN_SelectedIndexChanged" AutoPostBack="True">
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtUen" Display="Dynamic" ErrorMessage="*Requerido" 
                                    ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Segmento"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtSegmento" runat="server" Width="70px" 
                                    MaxLength="9" MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txt2_OnBlur" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbSegmento" runat="server" OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                    Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    DataTextField="Descripcion" DataValueField="Id" Width="250px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." 
                                    OnClientBlur="Combo_ClientBlur" EmptyMessage="Seleccione una uen">
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="txtSegmento" Display="Dynamic" ErrorMessage="*Requerido" 
                                    ForeColor="Red" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" Checked="True" 
                                    AutoPostBack="True" oncheckedchanged="chkActivo_CheckedChanged" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:HiddenField ID="HF_ID" runat="server" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="RadGrid1_NeedDataSource" OnPageIndexChanged="rg1_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" OnItemCommand="rg1_ItemCommand">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Area" HeaderText="Clave" UniqueName="Id_Area">
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Area_Descripcion" HeaderText="Descripción" UniqueName="Area_Descripcion">
                                                <HeaderStyle Width="300px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Seg" HeaderText="Segmento" UniqueName="Id_Seg" Display="false">
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Uen" HeaderText="Uen" UniqueName="Id_Uen" Display="false">
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Area_Potencial" HeaderText="Potencial (%)" UniqueName="Area_Potencial"
                                                DataFormatString="{0:N2}">
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr">
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle HorizontalAlign="Center" />
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
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescripcion.ClientID %>'));
                LimpiarTextBox($find('<%= txtUen.ClientID %>'));
                LimpiarTextBox($find('<%= txtSegmento.ClientID %>'));
                LimpiarTextBox($find('<%= txtPotencial.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbUEN.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbSegmento.ClientID %>'));
                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);
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

                        LimpiarControles();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                        hiddenActualiza.value = '';


                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtClave.ClientID %>');
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatArea";
                        parametros = parametros + "&sp=spCatCentral_Maximo";
                        parametros = parametros + "&columna=Id_Area";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        txtId.enable();
                        txtId.focus();

                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbUEN.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtUen.ClientID %>'));
            }
            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbSegmento.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtSegmento.ClientID %>'));
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

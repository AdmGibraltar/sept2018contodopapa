<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatRepresentantes.aspx.cs" Inherits="SIANWEB.CatRepresentantes" %>

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
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
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
                                <asp:Label ID="lblClave" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtClave"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                &nbsp;
                                <asp:CheckBox ID="chkPertenece" runat="server" Text="Este registro pertenece a un representante" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="310px"
                                    MaxLength="40">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNombre"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCalle" runat="server" Text="Calle"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtCalle" runat="server" Width="160px" MaxLength="20" onpaste="return false">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td colspan="3">
                                <asp:Label ID="lblNumero" runat="server" Text="Número"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtNumero" runat="server" Width="70px" MaxLength="5"
                                    MinValue="0">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblColonia" runat="server" Text="Colonia"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:RadTextBox ID="txtColonia" runat="server" Width="310px" MaxLength="40" onpaste="return false">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTelefono" runat="server" Text="Teléfono"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtTelefono" runat="server" MaxLength="20" onpaste="return false">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td colspan="3">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFecha" runat="server" Text="Fecha de alta"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha" runat="server" Width="100px" 
                                    Culture="es-MX" MinDate="1950-01-01">
                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Mostrar calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblContribucion" runat="server" Text="Contribución"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtContribucion" runat="server" Width="70px" MinValue="0"
                                    MaxLength="9">
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContribucion"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                &nbsp;</td>
                            <td align="left" colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCompensacion" runat="server" Text="Compensación"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCompensacion" runat="server" Width="70px" MinValue="0"
                                    MaxLength="9">
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCompensacion"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                &nbsp;</td>
                            <td align="left" colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                                    <td __designer:mapid="122">
                                        &nbsp;<asp:Label ID="lblFuncion" runat="server" Text="Tipo"></asp:Label>
                                    </td>
                                    <td __designer:mapid="123">
                                        <telerik:RadNumericTextBox ID="txtTipoRepresentante" runat="server" MaxLength="9" 
                                            MinValue="1" Width="70px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txtTipoRepresentante_OnBlur"/>
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td __designer:mapid="125">
                                        <telerik:RadComboBox ID="cmbTipoRepresentante" runat="server" 
                                            ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" 
                                            DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains" 
                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." 
                                            MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" 
                                            OnClientSelectedIndexChanged="cmbTipoRepresentanteSelectedIndexChanged" 
                                            Width="250px">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID3" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' 
                                                                Width="50px" />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC3" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td colspan="2" __designer:mapid="129">
                                        &nbsp;</td>
                                    <td __designer:mapid="132">
                                        &nbsp;
                                    </td>
                                </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                &nbsp;</td>
                            <td align="left" colspan="2">
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label2" runat="server" Text="UEN"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:RadListBox ID="lbUen" runat="server" CheckBoxes="True" Height="80px" 
                                    Width="320px">
                                </telerik:RadListBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td colspan="6">
                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" AutoPostBack="True"
                                    OnCheckedChanged="chkActivo_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                &nbsp;</td>
                            <td width="160">
                                &nbsp;</td>
                            <td width="10">
                                &nbsp;</td>
                            <td colspan="3" width="55">
                                &nbsp;</td>
                            <td width="200">
                                &nbsp;</td>
                        </tr>
                        </table>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rg1_NeedDataSource" OnItemCommand="rg1_ItemCommand" OnPageIndexChanged="rg1_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" 
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                    CellSpacing="0">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Rik" HeaderText="Clave" UniqueName="Id_Rik">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" UniqueName="Nombre">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Calle" HeaderText="Calle" 
                                                UniqueName="Calle">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Numero" HeaderText="Numero" 
                                                UniqueName="Numero">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Colonia" HeaderText="Colonia" 
                                                UniqueName="Colonia">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Telefono" HeaderText="Telefono" 
                                                UniqueName="Telefono">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Fecha_Alta" HeaderText="Fecha_Alta" UniqueName="Fecha_Alta"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Contribucion" HeaderText="Contribucion" 
                                                UniqueName="Contribucion">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Compensacion" HeaderText="Compensacion" 
                                                UniqueName="Compensacion">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Gte" HeaderText="Gte" UniqueName="Gte" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Pertenece" HeaderText="Pertenece" UniqueName="Pertenece"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" 
                                                UniqueName="Estatus" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TipoRep" 
                                                FilterControlAltText="Filter TipoRep column" 
                                                UniqueName="TipoRep" ShowSortIcon="False" Display="False" 
                                                ForceExtractValue="Always">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DescrRep" 
                                                FilterControlAltText="Filter DescrRep column" HeaderText="Representante" 
                                                UniqueName="DescrRep">
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
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                                <asp:HiddenField ID="HF_ID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">


            function cmbTipoRepresentanteSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoRepresentante.ClientID %>'));
            }

            function txtTipoRepresentante_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTipoRepresentante.ClientID %>'));
            }


            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= txtNombre.ClientID %>'));
                LimpiarTextBox($find('<%= txtCalle.ClientID %>'));
                LimpiarTextBox($find('<%= txtNumero.ClientID %>'));
                LimpiarTextBox($find('<%= txtColonia.ClientID %>'));
                LimpiarTextBox($find('<%= txtTelefono.ClientID %>'));
                LimpiarTextBox($find('<%= txtContribucion.ClientID %>'));
                LimpiarTextBox($find('<%= txtCompensacion.ClientID %>'));
                LimpiarDatePicker($find('<%= dpFecha.ClientID %>'));


                LimpiarCheckBox(document.getElementById('<%= chkPertenece.ClientID %>'));
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

                        //establecer valores por default en la alta (regla de negocio)
                        var txtContribucion = $find('<%= txtContribucion.ClientID %>');
                        var txtCompensacion = $find('<%= txtCompensacion.ClientID %>');
                        txtContribucion.set_value('0');
                        txtCompensacion.set_value('0');


                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtClave.ClientID %>');
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatRik";
                        parametros = parametros + "&sp=spCatLocal_Maximo";
                        parametros = parametros + "&columna=Id_Rik";
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
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

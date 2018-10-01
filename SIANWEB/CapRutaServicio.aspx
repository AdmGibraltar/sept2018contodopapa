<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapRutaServicio.aspx.cs" Inherits="SIANWEB.CapRutaServicio" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
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
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbRuta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgServicio">
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
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
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
                <td style="text-align: right; width: 150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div>
            <table style="font-family: verdana; font-size: 8pt;">
                <!-- Tabla principal--->
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <table style="width: auto">
                            <tr>
                                <td width="145">
                                    <asp:Label ID="LblCliente" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td style="width: auto">
                                    <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="50px" MinValue="1"
                                        MaxLength="9" OnTextChanged="txtCliente_TextChanged" AutoPostBack="True">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtClienteDescripcion" runat="server" Width="300px" 
                                        ReadOnly="True">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtCliente" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <table style="width: auto">
                            <tr>
                                <td width="145">
                                    <asp:Label ID="LblAparatos" runat="server" Text="Aparatos revisados"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtAparatos" runat="server" MaxLength="30">
                                        <ClientEvents OnKeyPress="SoloNumerico" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtAparatos" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <table style="width: auto">
                            <tr>
                                <td width="145">
                                    <asp:Label ID="LblFecha" runat="server" Text="Fecha de &uacute;ltima revisi&oacute;n" />
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFechaRev" runat="server" Width="100px">
                                        <Calendar ID="Calendar1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                            runat="server" MaxLength="10">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtFechaRev" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <table style="width: auto">
                            <tr>
                                <td valign="middle" width="145">
                                    <asp:Label ID="LblRuta" runat="server" Text="Ruta"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtRuta" runat="server" Width="50px">
                                        <ClientEvents OnBlur="txt_OnBlur" OnKeyPress="SoloNumerico" />
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="3">
                                    <telerik:RadComboBox ID="cmbRuta" runat="server" Width="300px" Filter="Contains"
                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                        LoadingMessage="Cargando..." 
                                        OnClientSelectedIndexChanged="cmbRuta_ClientSelectedIndexChanged" 
                                        MaxHeight="250px">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtRuta" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 25">
                                    &nbsp;
                                </td>
                                <td style="width: 25">
                                    &nbsp;
                                </td>
                                <td style="width: 100">
                                    <asp:HiddenField ID="HF_ID" runat="server" />
                                </td>
                                <td style="width: 186">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadGrid ID="rgServicio" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="rgServicio_ItemCommand"
                        OnPageIndexChanged="rgServicio_PageIndexChanged" PageSize="15" AllowPaging="True"
                        MasterTableView-NoMasterRecordsText="No se encontraron registros."  
                        GroupingSettings-RetainGroupFootersVisibility="true" ShowFooter="True">
                        <MasterTableView ShowGroupFooter="true">  
                                                 
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Cap" HeaderText="Clave" UniqueName="Id_Cap"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Num_Semana" HeaderText="Numero de Semana" UniqueName="Num_Semana"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fecha_InicioSemana" HeaderText="Inicio" UniqueName="Fecha_InicioSemana"
                                    Visible="false"  DataFormatString="{0:dd/MM/yyyy}" >
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fecha_FinSemana" HeaderText="Fin" UniqueName="Fecha_FinSemana"
                                    Visible="false"  DataFormatString="{0:dd/MM/yyyy}" >
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cliente" HeaderText="Cliente" UniqueName="Id_Cliente">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Nombre" UniqueName="Cte_NomComercial">
                                    <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Aggregate="Sum" DataField="Aparatos" HeaderText="Aparatos revisados" UniqueName="Aparatos"  
                                    FooterText="Total: ">
                                    <HeaderStyle Width="120" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha última revisión " UniqueName="Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Ruta" HeaderText="Id_Ruta" UniqueName="Id_Ruta"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Rut_Descripcion" HeaderText="Ruta" UniqueName="Rut_Descripcion">
                                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
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
                                <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="¿Está seguro de eliminar el Registro?"
                                Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                ButtonCssClass="baja">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </telerik:GridButtonColumn>                             
                            </Columns>
                            <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="Número" FieldName="Num_Semana" 
                                        HeaderValueSeparator=" de Semana: "></telerik:GridGroupByField>
                                        <telerik:GridGroupByField FieldAlias="Inicio" FieldName="Fecha_InicioSemana" 
                                        HeaderValueSeparator=": " FormatString="{0:dd/MM/yyyy}"></telerik:GridGroupByField>
                                        <telerik:GridGroupByField FieldAlias="Fin" FieldName="Fecha_FinSemana" 
                                        HeaderValueSeparator=": " FormatString="{0:dd/MM/yyyy}"></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Num_Semana" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                           </GroupByExpressions>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
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
                LimpiarTextBox($find('<%= txtCliente.ClientID %>'));
                LimpiarTextBox($find('<%= txtAparatos.ClientID %>'));

                LimpiarDatePicker($find('<%= txtFechaRev.ClientID %>'));

                LimpiarComboSelectIndex0($find('<%= cmbRuta.ClientID %>'));
                LimpiarTextBox($find('<%= txtRuta.ClientID %>'));
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
                        continuarAccion = false;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            

            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }

            function txt_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRuta.ClientID %>'));
            }

            function cmbRuta_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRuta.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatUsuario.aspx.cs" Inherits="SIANWEB.CatUsuario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings> 
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkMultiOficina">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadListBox1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboTipoUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
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
        <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="true" dir="rtl"
            Width="100%" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton ToolTip="Correo" CommandName="mail" CssClass="mail" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Imprimir" CommandName="print" CssClass="print"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Eliminar" CommandName="delete" CssClass="delete"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Regresar" CommandName="undo" CssClass="undo" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Guardar" CommandName="save" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
                <telerik:RadToolBarButton ToolTip="Nuevo" CommandName="new" CssClass="new" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton ToolTip="Configurar relación del gestor" Value="config"
                    CommandName="config" CssClass="config" ImageUrl="~/Imagenes/blank.png" Visible="false" />
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
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <table style="font-family: Verdana; font-size: 8pt">
                        <tr>
                            <td width="120">
                                <asp:Label ID="Label2" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="200px"
                                    MaxLength="50" TabIndex="1">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre"
                                    Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar"
                                    ForeColor="Red" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkMultiOficina" runat="server" Text="Multi-Centro" ToolTip="Permite visualizar la información de otros centros de distribución"
                                    Style="cursor: hand" TabIndex="11" AutoPostBack="True" OnCheckedChanged="chkMultiOficina_CheckedChanged" />
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblUen" runat="server" Text="UENs" Visible="False"></asp:Label>
                                <asp:Label ID="lblSegmento" runat="server" Text="Segmentos" Visible="False"></asp:Label>
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
                                Correo
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtCorreo" runat="server" Width="200px"
                                    MaxLength="50" Style="text-transform: lowercase;" TabIndex="2">
                                    <ClientEvents OnKeyPress="Email" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCorreo"
                                    Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="guardar"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCorreo"
                                    Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar"
                                    ForeColor="Red" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td rowspan="6" valign="top">
                                <telerik:RadListBox ID="RadListBox1" runat="server" CheckBoxes="True" Height="120px"
                                    Enabled="False" Width="160px">
                                </telerik:RadListBox>
                            </td>
                            <td rowspan="6" valign="top">
                                &nbsp;
                            </td>
                            <td rowspan="6" valign="top">
                                <telerik:RadListBox ID="ListUen" runat="server" CheckBoxes="True" Height="120px"
                                    Width="160px" Visible="False">
                                </telerik:RadListBox>
                                <telerik:RadListBox ID="ListSegmento" runat="server" CheckBoxes="True" Height="120px"
                                    Width="160px" Visible="False">
                                </telerik:RadListBox>
                            </td>
                            <td rowspan="6" valign="top">
                                &nbsp;
                            </td>
                            <td rowspan="6" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                F. nacimiento
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechaNac" runat="server" DateInput-MaxLength="10" DatePopupButton-ToolTip="Desplegar calendario"
                                    Calendar-CultureInfo="Spanish (Mexico)" Width="95px" MinDate="01/01/1940" MaxDate="2030-12-31"
                                    Calendar-FastNavigationSettings-TodayButtonCaption="Hoy" Calendar-FastNavigationSettings-OkButtonCaption="Aceptar"
                                    Calendar-FastNavigationSettings-CancelButtonCaption="Cancelar" TabIndex="3" Calendar-FastNavigationSettings-DateIsOutOfRangeMessage="La fecha no es válida">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings TodayButtonCaption="Hoy" OkButtonCaption="Aceptar" CancelButtonCaption="Cancelar"
                                            DateIsOutOfRangeMessage="La fecha no es v&#225;lida">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput TabIndex="3" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ToolTip="Desplegar calendario" ImageUrl="" HoverImageUrl="" TabIndex="3">
                                    </DatePopupButton>
                                </telerik:RadDatePicker>
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
                                Usuario
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtUsuario" runat="server" MaxLength="50"
                                    TabIndex="4" Width="125px">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtUsuario"
                                    ErrorMessage="*Requerido" ValidationGroup="guardar" Display="Dynamic" SetFocusOnError="true"
                                    ForeColor="Red" />
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tipo de usuario &nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cboTipoUsuario" runat="server" Width="203px" LoadingMessage="Cargando..."
                                    OnClientBlur="Combo_ClientBlur" AutoPostBack="True" OnSelectedIndexChanged="cboTipoUsuario_SelectedIndexChanged1"
                                    TabIndex="5">
                                </telerik:RadComboBox>
                            </td>
                            <td width="10">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboTipoUsuario"
                                    Display="Dynamic" ErrorMessage="*Requerido" InitialValue="-- Seleccionar --"
                                    SetFocusOnError="true" ValidationGroup="guardar" ForeColor="Red" />
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="TrRepresentante" visible="false">
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Representante"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbRepresentante" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                    Width="275px" MaxHeight="250px" TabIndex="6">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 25px; text-align: center; vertical-align: top">
                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                        Width="50px" />
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
                                &nbsp;
                            </td>
                        </tr>

                        <%--SAUL GUERRA 20150513 BEGIN--%>
                        <tr>
                            <td>
                                Teléfono&nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTelefono" runat="server" 
                                    MaxLength="10" MinValue="0" MaxValue="9999999999" Width="90px" TabIndex="7"> 
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                        </tr>
                        <%--SAUL GUERRA 20150513 END--%>

                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <%--SAUL GUERRA 20150513 BEGIN--%>
                                <asp:CheckBox ID="chkACYS" Checked="False" runat="server" Text="ACYS" ToolTip="Mostrar el usuario en ACYS"
                                    TabIndex="7" AutoPostBack="true" OnCheckedChanged="chkACYS_CheckedChanged" />&nbsp;
                                <%--SAUL GUERRA 20150513 END--%>
                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" ToolTip="Marcar el usuario como activo"
                                    TabIndex="8" />Activo
                                <asp:CheckBox ID="chkVerTodo" runat="server" Text="Ver todo " ToolTip="Permite visualizar la información de clientes de los cuales no es responsable"
                                    TabIndex="9" Visible="false" />
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td width="20">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table style="font-family: Verdana; font-size: 8pt; visibility: hidden;" >
                        <tr>
                            <td width="120">
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCredito" runat="server" TabIndex="10" Enabled="False"></asp:CheckBox>Permitir
                                modificar crédito suspendido hasta
                            </td>
                            <td rowspan="1" valign="middle">
                                <telerik:RadNumericTextBox ID="txtDias" runat="server" Width="50px" Enabled="False"
                                    MinValue="0" MaxLength="7" >
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td rowspan="1" valign="middle">
                                días después del vencimiento
                            </td>
                            <td rowspan="2" valign="top">
                                &nbsp;
                            </td>
                            <td rowspan="2" valign="top">
                                &nbsp;
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
            <tr>
                <td>
                </td>
                <td>
                    <table style="font-family: Verdana; font-size: 8pt">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="15" AllowFilteringByColumn="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                    OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemCommand"
                                    OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="True" TableLayout="Auto" AllowMultiColumnSorting="False"
                                        AllowNaturalSort="true" AllowSorting="true">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="U_Nombre" Visible="True" UniqueName="U_Nombre"
                                                HeaderText="Nombre" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Center"
                                                FilterControlWidth="195px" SortExpression="U_Nombre" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="False" HeaderTooltip="Introduzca un usuario para su búsqueda">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="250px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tu_Descripcion" HeaderText="Tipo de usuario"
                                                UniqueName="Tu_Descripcion" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"
                                                AllowFiltering="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="200px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Ofi_descripcion" Visible="True" UniqueName="Ofi_descripcion"
                                                HeaderText="Centro distribución" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                AllowFiltering="false">
                                                <HeaderStyle Width="120" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_ActivoStr" Visible="True" UniqueName="Activo_string"
                                                HeaderText="Estatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                AllowFiltering="false" ItemStyle-Width="80px" AllowSorting="false">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_u" UniqueName="Id_u" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_FNac" Visible="False" UniqueName="U_FNac">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_Activo" Visible="False" UniqueName="U_Activo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_VerTodo" Visible="False" UniqueName="U_VerTodo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_MultiCentro" Visible="False" UniqueName="U_MultiCentro">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_NombreCorto" Visible="False" UniqueName="U_NombreCorto">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_Correo" Visible="False" UniqueName="U_Correo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cu_User" Visible="False" UniqueName="Cu_User">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" Visible="False" UniqueName="Id_Cd">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Tu" Visible="False" UniqueName="Id_Tu">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Id_U" Visible="False" UniqueName="Id_Id_U">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Depto" Visible="False" UniqueName="Id_Depto">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Rik" Visible="False" UniqueName="Id_Rik">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_SusCredito" Visible="False" UniqueName="U_SusCredito">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_DiasVencimiento" Visible="False" UniqueName="U_DiasVencimiento">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_Telefono" Visible="False" UniqueName="U_Telefono">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="U_ACYS" Visible="False" UniqueName="U_ACYS">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="55px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Editar usuario" CommandName="Modificar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <SortingSettings EnableSkinSortStyles="False" SortToolTip="Ordenar ascendente/descendente"
                                        SortedAscToolTip="Ascendente" SortedDescToolTip="Descendente" />
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
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="HiddenId_Ofi" runat="server" Visible="False" />
                    <asp:HiddenField ID="HiddenId_TU" runat="server" Visible="False" />
                    <asp:HiddenField ID="HiddenId_U" runat="server" Visible="False" />
                    <asp:HiddenField ID="HiddenU_Usuario" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ToolBar_ClientClick(sender, args) {


            }
            function AbrirConf(dir) {
                var oWnd = radopen("ConfiguracionRelacionGestor.aspx?Id='" + Math.random() + "'", "AbrirRelacionGestor");
                oWnd.center();
                oWnd.Maximize();
            }
            function Habilita(sender) {

                var txt = $find('<%=txtDias.ClientID %>');

                if (sender.checked) {
                    txt.enable();
                }
                else {
                    txt.disable();
                    txt.set_value('');
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        
    </style>
</asp:Content>

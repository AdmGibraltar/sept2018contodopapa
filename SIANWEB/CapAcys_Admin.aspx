<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapAcys_Admin.aspx.cs" Inherits="SIANWEB.CapAcys_admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAcuerdo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGrid" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicked="ToolBar_ClientClick">
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
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
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
                <td style="margin-left: 40px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Territorio" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="7">
                                <telerik:RadComboBox ID="cmbTerritorio" runat="server" Width="300px" Filter="Contains"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged"
                                    MaxHeight="300px">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Representante" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txt2_OnBlur" OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="5">
                                <telerik:RadComboBox ID="cmbRepresentante" runat="server" Width="300px" Filter="Contains"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                    MaxHeight="300px">
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
                                Tipo de Modalidad
                            </td>
                            <td>
                                     <asp:DropDownList ID="cmbTipoModalidad" runat="server">
                                     </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Cliente" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1" AutoPostBack="True" OnTextChanged="txtCliente_TextChanged">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="5" style="margin-left: 40px">
                                <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                                <td>
                                    <asp:Label ID="lblVencido" runat="server" Text="Vencido"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbVencido" runat="server" Width="153px" LoadingMessage="Cargando..."
                                        OnClientBlur="Combo_ClientBlur">
                                    </telerik:RadComboBox>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Folio inicial" />
                            </td>
                            <td style="margin-left: 40px">
                                <telerik:RadNumericTextBox ID="txtFolio1" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Folio final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFolio2" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Estatus" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="100px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px" Culture="es-MX">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px" Culture="es-MX">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                                <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ToolTip="Buscar"
                                    ImageUrl="~/Img/find16.png" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="30">
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
                            <td>
                                &nbsp;
                            </td>
                            <td width="150">
                                <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="pnlGrid" Width="950px">
                                    <telerik:RadGrid ID="rgAcuerdo" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        OnNeedDataSource="rg_NeedDataSource" OnItemCommand="rg_ItemCommand" OnItemDataBound="rg_ItemDataBound"
                                        OnPageIndexChanged="rg_PageIndexChanged" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                        PageSize="15" AllowPaging="True">
                                        <MasterTableView ClientDataKeyNames="Id_Acs,Id_Cte,Id_Ter">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Acs" HeaderText="Folio" UniqueName="Id_Acs">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Acs_Version" HeaderText="Version" UniqueName="Acs_Version">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Acs_Estatus" HeaderText="Estatus" UniqueName="Acs_Estatus"
                                                    Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Acs_EstatusStr" HeaderText="Estatus" UniqueName="Acs_Estatus">
                                                    <HeaderStyle Width="80px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="Id_Cte">
                                                    <HeaderStyle Width="60px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cte_Nombre" HeaderText="Cliente" UniqueName="Cte_Nombre">
                                                    <HeaderStyle Width="250px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Terr." UniqueName="Id_Ter">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Rik" HeaderText="Rik" UniqueName="Id_Rik">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Acs_Fecha" HeaderText="Fecha" UniqueName="Acs_Fecha"
                                                    DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Acs_FechaInicioDocumento" HeaderText="Fecha Inicio" UniqueName="Acs_FechaInicioDocumento"
                                                    DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Acs_FechaFinDocumento" HeaderText="Fecha Fin" UniqueName="Acs_FechaFinDocumento"
                                                    DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Acs_Vencido" HeaderText="Vencido" UniqueName="Acs_Vencido">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Acs_Modalidad" HeaderText="Modalidad" UniqueName="Acs_Modalidad">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn CommandName="Editar" HeaderText="Editar" Text="Editar"
                                                    UniqueName="Editar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                                    ButtonCssClass="edit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                                    ConfirmText="[[MSG]]</br></br>" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
                                                    Text="Cancelar" UniqueName="Cancelar" Visible="True" ButtonType="ImageButton"
                                                    ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" Text="Imprimir"
                                                    UniqueName="Imprimir" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ButtonCssClass="imprimir">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="70px" />
                                                </telerik:GridButtonColumn>
                                                  <telerik:GridButtonColumn CommandName="Renovar" HeaderText="Renovar" ConfirmDialogType="RadWindow"
                                                    ConfirmText="¿Desea renovar  la solicitud <b>#[[ID]]</b>?"
                                                    Text="Renovar" UniqueName="Renovar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ButtonCssClass="edit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                </telerik:GridButtonColumn>
                                                <%-- <telerik:GridButtonColumn CommandName="Actualizar" HeaderText="Actualizar" ConfirmDialogType="RadWindow"
                                                    ConfirmText="¿Desea Actualizar la Versión de  la solicitud <b>#[[ID]]</b>?"
                                                    Text="Actualizar" UniqueName="Actualizar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ButtonCssClass="edit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                </telerik:GridButtonColumn>--%>
                                                <telerik:GridButtonColumn CommandName="Enviar" HeaderText="Enviar" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea enviar la solicitud <b>#[[ID]]</b>?" Text="Enviar" UniqueName="Enviar"
                                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="email_grid"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                            </telerik:GridButtonColumn>
                                              <telerik:GridButtonColumn CommandName="Ver_Versiones" HeaderText="Versiones" ConfirmDialogType="RadWindow"
                                                 Text="Ver Versiones" UniqueName="Ver_Versiones"
                                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridTemplateColumn HeaderText="Autorizar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Autorizar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Autorizar" CommandName="Autorizar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </MasterTableView>
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                            ShowPagerText="True" PageButtonCount="3" />
                                    </telerik:RadGrid>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRepresentante.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRepresentante.ClientID %>'));
            }

            function txt3_OnBlur(sender, args) {

            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }


            function ToolBar_ClientClick(sender, args) {

                AbrirVentana_Acys(-1, 0);


            }

            function enviarSolicitudPorCorreo() {

                if (RowFolio == null) {
                    radalert("Se debe seleccionar una solicitud del grid antes de presionar este botón.", 330, 150);
                    return false;
                }

                alert("(<enviar solicitud terminada por correo>)");

            }



            function abrirVersiones(Id) {
                // debugger;                 

                var oWnd = radopen("VentanaVersion_Buscar.aspx?Id_Acs=" + Id, "AbrirVentana_BuscarVersion");
                oWnd.center();

            }






            function AbrirVentana_Acys(Id, Accion, Estatus) {
                //debugger;
                var oWnd = radopen("CapAcys.aspx?Id=" + Id + "&Accion=" + Accion + "&Estatus=" + Estatus + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_Acys");
                oWnd.center();
                oWnd.Maximize();
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function OpenAlert(mensaje, Id, PermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir) {

                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_Acys2(Id, PermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir);
                    });
            }

            function AbrirVentana_Acys2(Id, ePermisoGuardar, PermisoModificar, PermisoEliminar, PermisoImprimir) {
                //debugger;
                var oWnd = radopen("CapAcys.aspx?Id=" + Id + "&PermisoGuardar=" + PermisoGuardar + "&PermisoModificar=" + PermisoModificar + "&PermisoEliminar=" + PermisoEliminar + "&PermisoImprimir=" + PermisoImprimir, "AbrirVentana_Acys");
                oWnd.center();
                oWnd.Maximize();
            }

            function LimpiarBanderaRebind(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind('0');
            }

            function ActivarBanderaRebind() {
                //debugger;
                ModificaBanderaRebind('1');
            }

            function ModificaBanderaRebind(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

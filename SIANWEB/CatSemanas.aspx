<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatSemanas.aspx.cs" Inherits="SIANWEB.CatSemanas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        break;
                    case 'delete':
                        continuarAccion = Confirma();
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="divPrincipal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadComboBoxAño">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                    <telerik:AjaxUpdatedControl ControlID="cmbMes" />
                    <telerik:AjaxUpdatedControl ControlID="RadDatePickerInicio" />
                    <telerik:AjaxUpdatedControl ControlID="RadDatePickerFin" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridGuardar" />
                    <telerik:AjaxUpdatedControl ControlID="rgSemana" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbMes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxAño" />
                    <telerik:AjaxUpdatedControl ControlID="RadDatePickerInicio" />
                    <telerik:AjaxUpdatedControl ControlID="RadDatePickerFin" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridGuardar" />
                    <telerik:AjaxUpdatedControl ControlID="rgSemana" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxAño" />
                    <telerik:AjaxUpdatedControl ControlID="cmbMes" />
                    <telerik:AjaxUpdatedControl ControlID="RadDatePickerInicio" />
                    <telerik:AjaxUpdatedControl ControlID="RadDatePickerFin" />
                    <telerik:AjaxUpdatedControl ControlID="rgSemana" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgSemana">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxAño" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="cmbMes" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadDatePickerInicio" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadDatePickerFin" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridGuardar" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlocke1" runat="server">
        <script type="text/javascript">

            function Confirma() {
                if (($find('<%= RadComboBoxAño.ClientID %>').get_value() == '') || ($find('<%= cmbMes.ClientID %>').get_value() == '-1')) {
                    radalert('Seleccione un periodo', 400, 10, 'Key Productos de limpieza');
                    var txt = $find('<%= RadComboBoxAño.ClientID %>');
                    txt.focus();
                    return false;
                }
                else {
                    if (confirm("¿Está seguro de eliminar el periodo " + $find('<%= cmbMes.ClientID %>').get_text() + " " + $find('<%= RadComboBoxAño.ClientID %>').get_value() + "?")) {

                        return true;
                    }
                    else {
                        return false;
                    }
                }

            }
        </script>
    </telerik:RadCodeBlock>
    <div runat="server" id="divPrincipal">
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
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución" />
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
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
                                &nbsp;
                            </td>
                            <td width="90">
                                &nbsp;
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="90">
                                &nbsp;
                            </td>
                            <td width="10">
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
                                <asp:Label ID="Label4" runat="server" Text="Año"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBoxAño" runat="server" OnSelectedIndexChanged="RadComboBoxAño_SelectedIndexChanged"
                                    AutoPostBack="True" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Mes"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbMes" runat="server" AllowCustomText="False" SelectedValue='<%# Bind("Cal_Mes") %>'
                                    Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbMes_SelectedIndexChanged"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" />
                                        <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" />
                                        <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" />
                                        <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" />
                                        <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" />
                                        <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" />
                                        <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" />
                                        <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" />
                                        <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" />
                                        <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" />
                                        <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Fecha inicio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePickerInicio" runat="server" Width="95px" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Fecha fin"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePickerFin" runat="server" Width="95px" Enabled="false">
                                </telerik:RadDatePicker>
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="RadGridGuardar" runat="server" AutoGenerateColumns="False" GridLines="None"
                        AllowMultiRowSelection="True" OnNeedDataSource="RadGridGuardar_NeedDataSource"
                        OnUpdateCommand="RadGridGuardar_UpdateCommand" OnInsertCommand="RadGridGuardar_InsertCommand"
                        OnItemDataBound="RadGridGuardar_ItemDataBound" OnItemCommand="RadGridGuardar_ItemCommand"
                        Visible="False">
                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" NoMasterRecordsText="No se encontraron registros.">
                            <CommandItemSettings AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf" RefreshText="Actualizar"
                                ShowRefreshButton="false"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Id_Sem" HeaderText="Semana" UniqueName="Id_SemG">
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="RadNumericTextBox3G" runat="server">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2G" runat="server" ControlToValidate="RadNumericTextBox3G"
                                            ErrorMessage="RequiredFieldValidator" Display="Dynamic">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1G" runat="server" Text='<%# Eval("Id_Sem") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Sem_FechaIni" HeaderText="Fecha inicial" UniqueName="Sem_FechaIniG">
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="RadDatePicker3G" runat="server">
                                            <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir calendario"></DatePopupButton>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3G" runat="server" ErrorMessage="RequiredFieldValidator"
                                            ControlToValidate="RadDatePicker3G" Display="Dynamic">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2G" runat="server" Text='<%# Eval("Sem_FechaIniStr") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Sem_FechaFin" HeaderText="Fecha final" UniqueName="Sem_FechaFinG">
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="RadDatePicker4G" runat="server">
                                            <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir calendario"></DatePopupButton>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4G" runat="server" ErrorMessage="RequiredFieldValidator"
                                            ControlToValidate="RadDatePicker4G" Display="Dynamic">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3G" runat="server" Text='<%# Eval("Sem_FechaFinStr") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn DataField="Sem_Activo" DataType="System.Boolean" HeaderText="Activo"
                                    display="false" SortExpression="Sem_Activo" UniqueName="Sem_ActivoG">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                    InsertText="Aceptar" UniqueName="EditCommandColumn" Visible="true">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                        Width="50px" />
                                </telerik:GridEditCommandColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn CancelText="Cancelar" InsertText="Añadir" UniqueName="EditCommandColumna1"
                                    UpdateText="Actualizar">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <br />
                    <telerik:RadGrid ID="rgSemana" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="RadGrid1_NeedDataSource" AllowMultiRowSelection="True" OnPageIndexChanged="rgServicio_PageIndexChanged"
                        PageSize="15" AllowPaging="True">
                        <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Id_Sem" HeaderText="Semana" UniqueName="column">
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="RadNumericTextBox3" runat="server">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadNumericTextBox3"
                                            ErrorMessage="RequiredFieldValidator">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Id_SemLabel" runat="server" Text='<%# Eval("Id_Sem") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Sem_FechaIni" HeaderText="Fecha inicial" UniqueName="column1">
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="RadDatePicker3" runat="server">
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Sem_FechaIniLabel" runat="server" Text='<%# Eval("Sem_FechaIni", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Sem_FechaFin" HeaderText="Fecha final" UniqueName="column2">
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="RadDatePicker4" runat="server">
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Sem_FechaFinLabel" runat="server" Text='<%# Eval("Sem_FechaFin", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn DataField="Sem_Activo" DataType="System.Boolean" HeaderText="Activo"
                                    SortExpression="Sem_Activo" UniqueName="Sem_Activo" display="false">
                                </telerik:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

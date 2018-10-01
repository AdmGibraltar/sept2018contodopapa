<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatCalendario.aspx.cs" Inherits="SIANWEB.CatCalendario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario de Precios
            //--------------------------------------------------------------------------------------------------

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
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rgCalendario" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadComboBoxAño">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxAño" />
                    <telerik:AjaxUpdatedControl ControlID="rgCalendario" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCalendario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxAño" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgGuardar" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">

            function Confirma() {
                if ($find('<%= RadComboBoxAño.ClientID %>').get_value() == '') {
                    radalert('Seleccione un año', 400, 10, 'Key Productos de limpieza');
                    var txt = $find('<%= RadComboBoxAño.ClientID %>');
                    txt.focus();
                    return false;
                }
                else {
                    if (confirm("¿Está seguro de eliminar el calendario " + $find('<%= RadComboBoxAño.ClientID %>').get_value() + "?")) {

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
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución" />
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
                            <td height="10">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="250">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Año"></asp:Label>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBoxAño" runat="server" OnSelectedIndexChanged="RadComboBoxAño_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </telerik:RadComboBox>
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
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgGuardar" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnItemCommand="rgGuardar_ItemCommand" Visible="False" OnInsertCommand="rgGuardar_InsertCommand"
                        OnItemDataBound="rgGuardar_ItemDataBound" OnNeedDataSource="rgGuardar_NeedDataSource">
                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" NoMasterRecordsText="No se encontraron registros.">
                            <CommandItemSettings AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf" RefreshText="Actualizar"
                                ShowRefreshButton="false" />
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Cal_Año" HeaderText="Año" SortExpression="Cal_Año"
                                    UniqueName="Cal_Año" Visible="false">
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="Cal_AñoTextBox0" runat="server" MaxLength="4" MinValue="2000"
                                            Text='<%# Bind("gCal_Año") %>' Width="70px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cal_AñoLabel0" runat="server" Text='<%# Eval("gCal_Año") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="90px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Cal_Mes" HeaderText="Mes" UniqueName="Cal_Mes">
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="RadComboBox2" runat="server" AllowCustomText="False" SelectedValue='<%# Bind("gCal_Mes") %>'
                                            Width="120px">
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
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cal_MesLabel0" runat="server" Text='<%# Eval("gCal_Mes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="140px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Cal_fechaini" HeaderText="Fecha inicial" UniqueName="Cal_fechaini">
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="Cal_fechainiRadDatePicker0" runat="server" DbSelectedDate='<%# Bind("gCal_fechaini") %>'
                                            Width="100px">
                                            <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir calendario"></DatePopupButton>
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cal_fechainiLabel0" runat="server" Text='<%# Eval("gCal_fechaini","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Cal_Fechafin" HeaderText="Fecha final" UniqueName="Cal_Fechafin">
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="Cal_FechafinRadDatePicker0" runat="server" DbSelectedDate='<%# Bind("gCal_Fechafin") %>'
                                            Width="100px">
                                            <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir calendario"></DatePopupButton>
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cal_FechafinLabel0" runat="server" Text='<%# Eval("gCal_Fechafin","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn DataField="gCal_Actual" DataType="System.Boolean" DefaultInsertValue="False"
                                    HeaderText="Actual" UniqueName="Cal_Actual" ReadOnly="True">
                                    <HeaderStyle Width="70px" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="gCal_Activo" DataType="System.Boolean" DefaultInsertValue="False"
                                    HeaderText="Activo" UniqueName="Cal_Activo" Visible="False">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                    InsertText="Aceptar" UniqueName="EditCommandColumn" Visible="true">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                        Width="50px" />
                                </telerik:GridEditCommandColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <AlternatingItemStyle HorizontalAlign="Center" />
                            <EditFormSettings>
                                <EditColumn CancelText="Cancelar" InsertText="Añadir" UniqueName="EditCommandColumn1"
                                    UpdateText="Actualizar">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <telerik:RadGrid ID="rgCalendario" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="RadGrid1_NeedDataSource" OnInsertCommand="rgCalendario_InsertCommand"
                        OnUpdateCommand="rgCalendario_UpdateCommand" OnDeleteCommand="rgCalendario_DeleteCommand">
                        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="No se encontraron registros.">
                            <CommandItemSettings RefreshText="Actualizar" AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf">
                            </CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Id_Emp" HeaderText="Id_Emp" SortExpression="Id_Emp"
                                    UniqueName="Id_Emp" ReadOnly="True" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Id_EmpTextBox" runat="server" Text='<%# Bind("Id_Emp") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Id_EmpTextBox"
                                            ErrorMessage="RequiredFieldValidator">* Requerido</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Id_EmpTextBox"
                                            ErrorMessage="Dato no valido" ValidationExpression="[0-9]{1,2}"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Id_EmpLabel" runat="server" Text='<%# Eval("Id_Emp") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Id_Cal" HeaderText="Id_Cal" SortExpression="Id_Cal"
                                    UniqueName="Id_Cal" ReadOnly="True" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Id_CalTextBox" runat="server" Text='<%# Bind("Id_Cal") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Id_CalTextBox"
                                            ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Id_CalTextBox"
                                            ErrorMessage="Dato no valido" ValidationExpression="[0-9]{1,2}"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Id_CalLabel" runat="server" Text='<%# Eval("Id_Cal") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Cal_Año" HeaderText="Año" SortExpression="Cal_Año"
                                    UniqueName="Cal_Año" Visible="False">
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="Cal_AñoTextBox" runat="server" Text='<%# Bind("Cal_Año") %>'
                                            MaxLength="4" MinValue="2000" MaxValue="2020" Width="140px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Cal_AñoTextBox"
                                            ErrorMessage="RequiredFieldValidator" Display="Dynamic">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cal_AñoLabel" runat="server" Text='<%# Eval("Cal_Año") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Cal_Mes" HeaderText="Mes" UniqueName="Cal_Mes">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="RadComboBox1" runat="server" AllowCustomText="False" SelectedValue='<%# Bind("Cal_Mes") %>'
                                            Width="145px">
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
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorMes" ControlToValidate="RadComboBox1"
                                            ErrorMessage="!" InitialValue="-- Seleccionar --">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cal_MesLabel" runat="server" Text='<%# Eval("Cal_Mes") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Cal_fechaini" HeaderText="Fecha inicial" UniqueName="Cal_fechaini">
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="Cal_fechainiRadDatePicker" runat="server" DbSelectedDate='<%# Bind("Cal_fechaini") %>'>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Cal_fechainiRadDatePicker"
                                            ErrorMessage="RequiredFieldValidator">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cal_fechainiLabel" runat="server" Text='<%# Eval("Cal_fechaini","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Cal_Fechafin" HeaderText="Fecha final" UniqueName="Cal_Fechafin">
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="Cal_FechafinRadDatePicker" runat="server" DbSelectedDate='<%# Bind("Cal_Fechafin") %>'>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Cal_FechafinRadDatePicker"
                                            ErrorMessage="RequiredFieldValidator">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cal_FechafinLabel" runat="server" Text='<%# Eval("Cal_Fechafin","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Cal_fechaExtemporaneo" DefaultInsertValue="False"
                                    HeaderText="Cierre pago extemporáneo" UniqueName="Cal_fechaExtemporaneo">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="Cal_Actual" DataType="System.Boolean" DefaultInsertValue="False"
                                    HeaderText="Actual" UniqueName="Cal_Actual">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="Cal_Activo" DataType="System.Boolean" DefaultInsertValue="False"
                                    HeaderText="Activo" UniqueName="Cal_Activo" Visible="False">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridEditCommandColumn HeaderText="Editar" EditText="Editar" Visible="False">
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn CommandName="Delete" HeaderText="Borrar" ConfirmDialogType="RadWindow"
                                    ConfirmText="Borrar este catalago?" Text="Borrar" UniqueName="DeleteColumn" Visible="False">
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <AlternatingItemStyle HorizontalAlign="Center" />
                            <EditFormSettings>
                                <EditColumn UniqueName="EditCommandColumn1" CancelText="Cancelar" InsertText="Añadir"
                                    UpdateText="Actualizar">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

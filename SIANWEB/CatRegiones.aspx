<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatRegiones.aspx.cs" Inherits="SIANWEB.CatRegiones" %>

<%--rm--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CheckBox1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgRegion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server" style="font-family: verdana; font-size: 8pt;">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
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
                        Width="150px">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hiddenActualiza" runat="server" />
        <table style="width: 518px">
            <!-- Tabla principal--->
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <!--Tab 1  Tabla 1-->
                        <tr>
                            <td style="width: 70px">
                                <asp:Label ID="Label2" runat="server" Text="Clave" />
                            </td>
                            <td class="style1">
                                <%--<asp:TextBox ID="txtRegion" Width="46px" runat="server" ></asp:TextBox>--%>
                                <telerik:RadNumericTextBox ID="txtRegion2" runat="server" Width="46px" MaxLength="5"
                                    MinValue="1" MaxValue="10000">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorReg1" runat="server" ErrorMessage="RequiredFieldValidator"
                                    ControlToValidate="txtRegion2" Display="static" ValidationGroup="guardar" ForeColor="#FF0000"
                                    Text="*Requerido">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 70px">
                                <asp:Label ID="Label3" runat="server" Text="Descripción" />
                            </td>
                            <td colspan="2">
                                <!-- Pendiente-->
                                <telerik:RadTextBox onpaste="return false" ID="txtDescripcion2" runat="server" Width="300px"
                                    AutoPostBack="false" MaxLength="20">
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorReg2" runat="server" ErrorMessage="RequiredFieldValidator"
                                    ControlToValidate="txtDescripcion2" Display="Static" ValidationGroup="guardar"
                                    ForeColor="#FF0000" Text="*Requerido">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                    AutoPostBack="true" Checked="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgRegion" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnInsertCommand="rgRegion_InsertCommand"
                        OnUpdateCommand="rgRegion_UpdateCommand" OnItemDataBound="rgTipoCosto_DataBound"
                        OnEditCommand="rgRegion_EditCommand" OnItemCommand="rgRegion_ItemCommand">
                        <MasterTableView NoMasterRecordsText="No hay registro para mostrar" CommandItemDisplay="None"
                            DataKeyNames="id_reg">
                            <CommandItemSettings RefreshText="Actualizar" AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf">
                            </CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="id_reg" HeaderText="Clave" UniqueName="id_reg">
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" Text='<%# Bind("id_reg") %>'
                                            MaxLength="6" MinValue="1" MaxValue="10000" Width="140px" Enabled="True">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                                            ControlToValidate="RadNumericTextBox1" Display="Dynamic">* Requerido
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="id_regLabel" runat="server" Text='<%# Eval("id_reg") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Reg_Descripcion" HeaderText="Descripción"
                                    UniqueName="Reg_Descripcion">
                                    <EditItemTemplate>
                                        <telerik:RadTextBox onpaste="return false" ID="RadTextBox2" runat="server" Text='<%# Bind("Reg_Descripcion") %>'>
                                            <ClientEvents OnKeyPress="SoloAlfabetico" />
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadTextBox2"
                                            Display="Dynamic" ErrorMessage="RequiredFieldValidator">* Requerido</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Reg_DescripcionLabel" runat="server" Text='<%# Eval("Reg_Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Reg_Activo" HeaderText="Estatus" UniqueName="Estatus">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="Reg_Activo" DataType="System.Boolean" DefaultInsertValue="False"
                                    HeaderText="Activo invi" UniqueName="Reg_Activo" Display="False">
                                </telerik:GridCheckBoxColumn>
                                <%--<telerik:GridEditCommandColumn HeaderText="Editar" EditText="Editar" Visible="False"
                                    UniqueName="ColumnaEditar">
                                </telerik:GridEditCommandColumn>--%>
                                <%--<telerik:GridTemplateColumn HeaderText="Editar" UniqueName="Editar" 
                                    Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonModificar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            ToolTip="Editar" CommandName="Modificar" CssClass="edit" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
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

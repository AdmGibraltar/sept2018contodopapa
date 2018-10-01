<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatProveedores.aspx.cs" Inherits="SIANWEB.CatProveedores" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
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
            <telerik:AjaxSetting AjaxControlID="rgProveedores">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <div runat="server" id="divPrincipal">
        <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="RadToolBar1_ButtonClick"
            onclientbuttonclicking="ToolBar_ClientClick">
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
        </telerik:radtoolbar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    Centro de distribucion
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:radtabstrip id="RadTabStrip1" runat="server" multipageid="RadMultiPage1"
                        selectedindex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos generales" AccessKey="G" PageViewID="RadPageViewDGenerales"
                                Selected="True">
                            </telerik:RadTab>                         
                        </Tabs>
                    </telerik:radtabstrip>
                    <telerik:radmultipage id="RadMultiPage1" runat="server" selectedindex="0" borderstyle="Solid"
                        borderwidth="1px">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" >
                          <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Text="Clave"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtClave" runat="server" MinValue="1" Width="70px"
                                                        MaxLength="8">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtClave"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
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
                                                <td colspan="1">
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
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Nombre"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" MaxLength="40"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="1">
                                                    <asp:CheckBox ID="chkActivo" Checked="True" runat="server" AutoPostBack="True" OnCheckedChanged="chkActivo_CheckedChanged"
                                                        Text="Activo" />
                                                </td>
                                                <td>
                                       
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="RFC"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtRfc" runat="server" MaxLength="20"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td colspan="2">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRfc"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRfc"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="^((([A-Z]|[a-z]){3})|(([A-Z]|[a-z]){4}))([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))$"
                                                        ValidationGroup="guardar"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="1">
                                                     <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                   <telerik:RadComboBox ID="cmbTipo" runat="server" Width="150px" AutoPostBack="True"
                                                        Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                        DataTextField="Descripcion" DataValueField="Id" 
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" CausesValidation="False"
                                                        OnSelectedIndexChanged="CmbTipo_SelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbTipo"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                    ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Calle"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtCalle" runat="server" MaxLength="40"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
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
                                                <td colspan="1">
                                                    <asp:Label ID="Label5" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtNumero" runat="server" MaxLength="15"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="C.P."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtCp" runat="server" MaxLength="6" MinValue="0" Width="200px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
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
                                                <td colspan="1">
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
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text="Colonia"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtColonia" runat="server" MaxLength="40"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
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
                                                <td colspan="1">
                                                    <asp:Label ID="Label8" runat="server" Text="Municipio"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtMunicipio" runat="server" MaxLength="40"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Text="Estado"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtEstado" runat="server" MaxLength="40"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
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
                                                <td colspan="1">
                                                    <asp:Label ID="Label10" runat="server" Text="País"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtPais" runat="server" MaxLength="40"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Teléfono"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTelefono" runat="server" MaxLength="20" Width="200px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadNumericTextBox>
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
                                                <td colspan="1">
                                                    <asp:Label ID="Label12" runat="server" Text="Fax"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtFax" runat="server" MaxLength="20" Width="200px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text="E-Mail"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtEmail" runat="server" MaxLength="40"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="Email" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td colspan="2">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                                        Display="Dynamic" ErrorMessage="*Formato incorrecto" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="guardar"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="1">
                                                    <asp:Label ID="Label14" runat="server" Text="Contacto"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtContacto" runat="server" MaxLength="40"
                                                        Width="200px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                         
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    <asp:HiddenField ID="HFId_Proveedor" runat="server" />
                                                      <asp:HiddenField ID="HF_Habilitar" runat="server" value="0"/>
                                                </td>
                                                <td colspan="2">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td colspan="1">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                     <td valign="top">
                                            <table runat="server" id="tbMovimientos">
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMovimientos" runat="server" Font-Bold="True" Text="Tipos de Movimientos"
                                                            Width="350px"></asp:Label>
                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadListBox ID="listMov" runat="server" CheckBoxes="True" Width="370px"
                                                            Height="302px">
                                                        </telerik:RadListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
 
                        </telerik:RadPageView>
                    </telerik:radmultipage>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:radgrid id="rgProveedores" runat="server" autogeneratecolumns="False" gridlines="None"
                                    onneeddatasource="rgProveedores_NeedDataSource" onitemcommand="rgProveedores_ItemCommand"
                                    onpageindexchanged="rgProveedores_PageIndexChanged" allowpaging="True" allowfilteringbycolumn="True"> 
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros.">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Empresa" HeaderText="Empresa" UniqueName="Empresa"
                                                Visible="false">
                                                <ItemStyle Width="70px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Clave" UniqueName="Id" AllowSorting="false" AllowFiltering="false">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="Descripcion"
                                             FilterControlWidth="300px" SortExpression="U_Nombre" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="False" HeaderTooltip="Introduzca un proveedor para su búsqueda">
                                                <HeaderStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Calle" HeaderText="Calle" UniqueName="Calle"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Numero" HeaderText="Numero" UniqueName="Numero"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CP" HeaderText="C.P." UniqueName="CP" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Colonia" HeaderText="Colonia" UniqueName="Colonia"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Municipio" HeaderText="Municipio" UniqueName="Municipio"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Ciudad" HeaderText="Ciudad" UniqueName="Ciudad"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Telefono" HeaderText="Telefono" UniqueName="Telefono"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFC" HeaderText="R.F.C." UniqueName="RFC" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Fax" HeaderText="Fax" UniqueName="Fax" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Correo" HeaderText="Correo" UniqueName="Correo"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" UniqueName="Estado"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Contacto" HeaderText="Contacto" UniqueName="Contacto"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" UniqueName="Tipo"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Pais" HeaderText="Pais" UniqueName="Pais" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus" 
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr" AllowSorting="false" AllowFiltering="false">
                                                <HeaderStyle Width="90px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Habilitar" UniqueName="Habilitar" 
                                                Visible="False">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </MasterTableView>
                                </telerik:radgrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <telerik:radscriptblock id="RadScriptBlock1" runat="server">
            <script type="text/jscript" language="jscript">
                function txtEmpresa_OnBlur(sender, args) {
                    var textBox = sender;


                    combo.clearSelection();
                    for (var i = 0; i < combo.get_items().get_count(); i++) {
                        var item = combo.get_items().getItem(i);
                        if (textBox.get_value() == item.get_value()) {
                            combo.get_items().getItem(i).select();
                            break;
                        }
                    }

                    if (combo.get_value() == '') {
                        var mens = 'El movimiento con Id ' + textBox.get_value() + ' no existe.';
                        RadAlert(mens, 600, 10);
                    }
                }

                //--------------------------------------------------------------------------------------------------
                //Limpiar controles de formulario  
                //--------------------------------------------------------------------------------------------------
                function LimpiarControles() {
                    LimpiarTextBox($find('<%= txtNombre.ClientID %>'));
                    LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                    LimpiarTextBox($find('<%= txtCalle.ClientID %>'));
                    LimpiarTextBox($find('<%= txtNumero.ClientID %>'));
                    LimpiarTextBox($find('<%= txtCp.ClientID %>'));
                    LimpiarTextBox($find('<%= txtColonia.ClientID %>'));
                    LimpiarTextBox($find('<%= txtMunicipio.ClientID %>'));
                    LimpiarTextBox($find('<%= txtTelefono.ClientID %>'));
                    LimpiarTextBox($find('<%= txtRfc.ClientID %>'));
                    LimpiarTextBox($find('<%= txtFax.ClientID %>'));
                    LimpiarTextBox($find('<%= txtEmail.ClientID %>'));
                    LimpiarTextBox($find('<%= txtEstado.ClientID %>'));
                    LimpiarTextBox($find('<%= txtContacto.ClientID %>'));
                    LimpiarTextBox($find('<%= txtPais.ClientID %>'));


                 


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
                            var hiddenActualiza = document.getElementById('<%= HFId_Proveedor.ClientID %>');
                            hiddenActualiza.value = '';

                            //Habilita, da el foco y establece el ID sugerido
                            var txtId = $find('<%= txtClave.ClientID %>');
                            txtId.enable();
                            txtId.focus();


                            var urlArchivo = 'ObtenerMaximo.aspx';
                            parametros = "Catalogo=CatProveedor";
                            parametros = parametros + "&sp=spCatCentral_Maximo";
                            parametros = parametros + "&columna=Id_Pvd";
                            var resultado = obtenerrequest(urlArchivo, parametros);
                            txtId.set_value(resultado);

                            continuarAccion = false;
                            break;
                    }

                    args.set_cancel(!continuarAccion);
                }
                
                 
            </script>
        </telerik:radscriptblock>
    </div>
</asp:Content>

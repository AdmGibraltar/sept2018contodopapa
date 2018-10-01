<%@ Page Title="Ajuste de base instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapAjusteBi.aspx.cs" Inherits="SIANWEB.CapAjusteBase" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
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
            <telerik:AjaxSetting AjaxControlID="cmbSolicitud">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtClienteOrigen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtClienteDestino">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProductoOrigen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProductoDestino">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAjuste">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
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
                <td>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Solicitud"></asp:Label>
                            </td>
                            <td style="margin-left: 40px">
                                <telerik:RadComboBox ID="cmbSolicitud" runat="server" Width="300px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbSolicitud_SelectedIndexChanged" MaxHeight="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Tipo de movimiento"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTipo" runat="server" Width="300px" OnClientSelectedIndexChanged="cmb0_ClientSelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbTipo"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                    ValidationGroup="add"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td width="0">
                                &nbsp;
                            </td>
                            <td align="center" colspan="6">
                                &nbsp;
                            </td>
                            <td align="center" colspan="6">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="0">
                                &nbsp;
                            </td>
                            <td align="center" colspan="6">
                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Text="Origen"></asp:Label>
                            </td>
                            <td align="center" colspan="6">
                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Text="Destino"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="0">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerritorioOrigen" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txt1_OnBlur" OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="4">
                                <telerik:RadComboBox ID="cmbTerritorioOrigen" runat="server" Width="300px" Filter="Contains"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged"
                                    MaxHeight="200px">
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
                                <asp:Label ID="Label14" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerritorioDestino" runat="server" Width="70px"
                                    MaxLength="9" MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txt4_OnBlur" OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="4">
                                <telerik:RadComboBox ID="cmbTerritorioDestino" runat="server" Width="300px" Filter="Contains"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmb4_ClientSelectedIndexChanged"
                                    MaxHeight="200px">
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
                            <td width="0">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtClienteOrigen" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1" AutoPostBack="True" OnTextChanged="txtClienteOrigen_TextChanged">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" OnBlur="ObtenerActOrigen" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txtClienteNombreOrigen" runat="server" Width="295px" ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtClienteDestino" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1" AutoPostBack="True" OnTextChanged="txtClienteDestino_TextChanged">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" OnBlur="ObtenerActDestino" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txtClienteNombreDestino" runat="server" Width="295px" ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="0">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Producto"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtProductoOrigen" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1" AutoPostBack="True" OnTextChanged="txtProductoOrigen_TextChanged">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" OnBlur="ObtenerActOrigen"/>
                                </telerik:RadNumericTextBox><%-- OnBlur="ObtenerActOrigen" OnBlur="txt9_OnBlur"--%>
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txtProductoNombreOrigen" runat="server" Width="295px" ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="Producto"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtProductoDestino" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1" AutoPostBack="True" OnTextChanged="txtProductoDestino_TextChanged">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent"  OnBlur="ObtenerActDestino"/>
                                </telerik:RadNumericTextBox><%--OnBlur="ObtenerActDestino"--%> 
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txtProductoNombreDestino" runat="server" Width="295px" ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="0">
                                &nbsp;
                            </td>
                            <td valign="middle" width="98">
                                <asp:Label ID="Label12" runat="server" Text="Cantidad actual"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCantActualOrigen" runat="server" Width="70px" MaxLength="9"
                                    Enabled="False">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="70">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCantActualOrigen"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
                            </td>
                            <td width="100">
                                <asp:Label ID="Label19" runat="server" Text="Cantidad a quitar"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCantQuitar" runat="server" Width="70px" MaxLength="9"
                                    MinValue="0" Enabled="False">
                                    <ClientEvents OnBlur="CantQuitarBlur" OnKeyPress="handleClickEvent" />
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td valign="middle" width="70">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCantQuitar"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
                            </td>
                            <td width="98">
                                <asp:Label ID="Label18" runat="server" Text="Cantidad actual"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCantActualDestino" runat="server" Width="70px"
                                    MaxLength="9" Enabled="False">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="70">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCantActualDestino"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
                            </td>
                            <td width="120">
                                <asp:Label ID="Label17" runat="server" Text="Cantidad modificada"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCantModificada" runat="server" Width="70px" MaxLength="9"
                                    MinValue="0" Enabled="False">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="65" valign="middle">
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCantModificada"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td width="0">
                                &nbsp;
                            </td>
                            <td valign="top">
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
                            <td width="0">
                                &nbsp;
                            </td>
                            <td valign="top">
                                <asp:Label ID="Label13" runat="server" Text="Explicación del caso"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtExplicacion" runat="server" Rows="3" TextMode="MultiLine"
                                    Width="440px" MaxLength="100">
                                    <ClientEvents OnKeyPress="ValidarMaxLenght" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="imgAgregar" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                                    OnClick="imgAgregar_Click" ToolTip="Agregar" ValidationGroup="add" OnClientClick="Add" />
                            </td>
                        </tr>
                        <tr>
                            <td width="0">
                                &nbsp;
                            </td>
                            <td valign="top">
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
                    <table>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="panel1" Width="900px" ScrollBars="Horizontal">
                                    <telerik:RadGrid ID="rgAjuste" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        OnNeedDataSource="rg_NeedDataSource" Height="200px" Width="1300px" OnItemCommand="rg_ItemCommand">
                                        <MasterTableView NoMasterRecordsText="No se encontraron registros." ClientDataKeyNames="Abi_Estatus">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Abi_Estatus" HeaderText="Estatus" UniqueName="Abi_Estatus"
                                                    Display="false">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Abi_EstatusStr" HeaderText="Estatus" UniqueName="Abi_EstatusStr">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Abi_Tipo" HeaderText="Tipo mov." UniqueName="Abi_Tipo"
                                                    Display="false">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Abi_TipoStr" HeaderText="Tipo mov." UniqueName="Abi_TipoStr">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Ter_Origen" HeaderText="Terr. origen" UniqueName="Id_Ter_Origen">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Cte_Origen" HeaderText="Cte. origen" UniqueName="Id_Cte_Origen">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Prd_Origen" HeaderText="Prod. origen" UniqueName="Id_Prd_Origen">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Abi_CantActual_Origen" HeaderText="Cant. actual origen"
                                                    UniqueName="Abi_CantActual_Origen">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Abi_CantQuitar_Origen" HeaderText="Cant. a quitar"
                                                    UniqueName="Abi_CantQuitar_Origen">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Ter_Destino" HeaderText="Terr. destino" UniqueName="Id_Ter_Destino">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Cte_Destino" HeaderText="Cte. destino" UniqueName="Id_Cte_Destino">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Prd_Destino" HeaderText="Prod. destino" UniqueName="Id_Prd_Destino">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Abi_CantActual_Destino" HeaderText="Cant. actual destino"
                                                    UniqueName="Abi_CantActual_Destino">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Abi_CantQuitar_Destino" HeaderText="Cant. modificada"
                                                    UniqueName="Abi_CantQuitar_Destino">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Abi_ExplicacionCaso" HeaderText="Explicación"
                                                    UniqueName="Abi_ExplicacionCaso" Display="false">
                                                    <HeaderStyle Width="100px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                    ConfirmText="¿Borrar este detalle?</br></br>" Text="Borrar" UniqueName="DeleteColumn"
                                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                Autorización
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtAutorizacion" runat="server" Width="500px" Enabled="False">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function Add(sender, args) {
                //debugger;
                if (txtCant.get_value() != 0) {
                    if (txtCant.get_value() < txtQuitar.get_value() && quitarblur) {
                        radalert("La cantidad a quitar no puede ser mayor a la cantidad actual", 330, 150);
                        txtQuitar.set_value(txtCant.get_value());
                        quitarblur = false;
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    return true;
                }
            }

            function CantQuitarBlur(sender, args) {
                //debugger;
                var txtCant = $find('<%= txtCantActualOrigen.ClientID %>');
                var txtQuitar = $find('<%= txtCantQuitar.ClientID %>');

                if (txtCant.get_value() < txtQuitar.get_value()) {
                    radalert("La cantidad a quitar no puede ser mayor a la cantidad actual", 330, 150);
                    txtQuitar.set_value(txtCant.get_value());

                }

                var txtMod = $find('<%= txtCantModificada.ClientID %>');
                var txtCantDest = $find('<%= txtCantActualDestino.ClientID %>');
                txtMod.set_value(txtCantDest.get_value() + txtQuitar.get_value());
            }

            function ObtenerActual(ter, cte, prd, mov) {
                var urlArchivo = 'ObtenerActual.aspx';
                parametros = "cte=" + cte;
                parametros = parametros + "&prd=" + prd;
                parametros = parametros + "&terr=" + ter;
                parametros = parametros + "&mov=" + mov;
                return obtenerrequest(urlArchivo, parametros);
            }

            function ObtenerActOrigen() {
                //debugger;
                var txtActual = $find('<%= txtCantActualOrigen.ClientID %>');
                var txtActualD = $find('<%= txtCantActualDestino.ClientID %>');
                var txtQuitar = $find('<%=  txtCantQuitar.ClientID %>');
                var txtCte = $find('<%= txtClienteOrigen.ClientID %>');
                var txtTer = $find('<%= txtTerritorioOrigen.ClientID %>');
                var txtPrd = $find('<%= txtProductoOrigen.ClientID %>');
                var cmbTipo = $find('<%= cmbTipo.ClientID %>');
                var txtMod = $find('<%= txtCantModificada.ClientID %>');
                var txtPrdD = $find('<%= txtProductoDestino.ClientID %>');
                var actual;

                if (txtCte.get_value() == '' && txtTer.get_value() == '' && txtPrd.get_value() == '') {
                    actual = '';
                }
                else {
                    actual = ObtenerActual(txtTer.get_value(), txtCte.get_value(), txtPrd.get_value(), cmbTipo.get_value());
                    if (actual == "-0") {
                        window.location.href("Login.aspx");
                    }
                }
                txtActual.set_value(actual);
                txtQuitar.set_value(actual);
                txtMod.set_value(txtActualD.get_value() + txtQuitar.get_value());

                if (txtPrd.get_value() != '' && txtPrdD.get_value() != '') {
                    txtQuitar.enable();
                }
                else {
                    txtQuitar.disable();
                }
            }

            function ObtenerActDestino() {
                //debugger;
                var txtActual = $find('<%= txtCantActualDestino.ClientID %>');
                var txtMod = $find('<%= txtCantModificada.ClientID %>');
                var txtCte = $find('<%= txtClienteDestino.ClientID %>');
                var txtTer = $find('<%= txtTerritorioDestino.ClientID %>');
                var txtPrd = $find('<%= txtProductoDestino.ClientID %>');
                var cmbTipo = $find('<%= cmbTipo.ClientID %>');
                var txtQuitar = $find('<%=  txtCantQuitar.ClientID %>');
                var txtPrdO = $find('<%= txtProductoOrigen.ClientID %>');

                var actual;
                if (txtCte.get_value() == '' && txtTer.get_value() == '' && txtPrd.get_value() == '') {
                    actual = '';
                }
                else {
                    actual = ObtenerActual(txtTer.get_value(), txtCte.get_value(), txtPrd.get_value(), cmbTipo.get_value());
                    if (actual == "-0") {
                        window.location.href("Login.aspx");
                    }
                }
                txtActual.set_value(actual);
                txtMod.set_value(txtActual.get_value() + txtQuitar.get_value());
                //debugger;
                if (txtPrdO.get_value() != '' && txtPrd.get_value() != '') {
                    txtQuitar.enable();
                }
                else {
                    txtQuitar.disable();
                }
            }

            function cmb0_ClientSelectedIndexChanged(sender, eventArgs) {
                ObtenerActOrigen();
                ObtenerActDestino();
            }

            function txt1_OnBlur(sender, args) {
                //debugger;
                var txt = sender;
                var cmb = $find('<%= cmbTerritorioOrigen.ClientID %>');
                var txtCte = $find('<%= txtClienteOrigen.ClientID %>');
                var txtCteDescr = $find('<%= txtClienteNombreOrigen.ClientID %>');

                txtCte.set_value('');
                txtCteDescr.set_value('');

                if (txt.get_value() != cmb.get_value()) {
                    OnBlur(sender, cmb);
                    ObtenerActOrigen();
                }
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {
                var txt = $find('<%= txtTerritorioOrigen.ClientID %>');
                var cmb = sender;

                var txtCte = $find('<%= txtClienteOrigen.ClientID %>');
                var txtCteDescr = $find('<%= txtClienteNombreOrigen.ClientID %>');

                txtCte.set_value('');
                txtCteDescr.set_value('');

                if (txt.get_value() != cmb.get_value()) {
                    ClientSelectedIndexChanged(eventArgs.get_item(), txt);
                    ObtenerActOrigen();
                }
            }



            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                var txt = $find('<%= txtClienteOrigen.ClientID %>');
                var cmb = sender;
                if (txt.get_value() != cmb.get_value()) {
                    ClientSelectedIndexChanged(eventArgs.get_item(), txt);
                    ObtenerActOrigen();
                }
            }

            function txt3_OnBlur(sender, args) {
                var txt = sender;

                var txtPrdO = $find('<%= txtProductoOrigen.ClientID %>');
                var txtPrdD = $find('<%= txtProductoDestino.ClientID %>');
                var txtQuitar = $find('<%= txtCantQuitar.ClientID %>');
                var txtActualOrigen = $find('<%= txtCantActualOrigen.ClientID %>');
                var txtActualDestino = $find('<%= txtCantActualDestino.ClientID %>');
                var txtMod = $find('<%= txtCantModificada.ClientID %>');

                if (txt.get_value() != txtPrdO.get_value()) {
                    OnBlur(sender, cmb);
                    ObtenerActOrigen();

                    if (txtPrdO.get_value() != '' && txtPrdD.get_value() != '') {
                        //debugger;
                        txtQuitar.enable();
                    }
                    else if (txtQuitar._enabled) {
                        txtQuitar.set_value(txtActualOrigen.get_value());
                        txtMod.set_value(txtActualDestino.get_value() + txtQuitar.get_value());
                        txtQuitar.disable();
                    }
                }
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
                var txt = $find('<%= txtProductoOrigen.ClientID %>');
                var txtPrdO = $find('<%= txtProductoOrigen.ClientID %>');
                var txtPrdD = $find('<%= txtProductoDestino.ClientID %>');
                var txtQuitar = $find('<%= txtCantQuitar.ClientID %>');
                var txtActualOrigen = $find('<%= txtCantActualOrigen.ClientID %>');
                var txtActualDestino = $find('<%= txtCantActualDestino.ClientID %>');
                var txtMod = $find('<%= txtCantModificada.ClientID %>');


                var cmb = sender;

                if (txt.get_value() != cmb.get_value()) {
                    ClientSelectedIndexChanged(eventArgs.get_item(), txt);
                    ObtenerActOrigen();

                    if (txtPrdO.get_value() != '' && txtPrdD.get_value() != '') {
                        //debugger;
                        txtQuitar.enable();
                    }
                    else if (txtQuitar._enabled) {
                        txtQuitar.set_value(txtActualOrigen.get_value());
                        txtMod.set_value(txtActualDestino.get_value() + txtQuitar.get_value());
                        txtQuitar.disable();
                    }
                }
            }

            function txt4_OnBlur(sender, args) {
                var txt = sender;
                var cmb = $find('<%= cmbTerritorioDestino.ClientID %>');
                
                var txtCte = $find('<%= txtClienteDestino.ClientID %>');
                var txtCteDescr = $find('<%= txtClienteNombreDestino.ClientID %>');

                txtCte.set_value('');
                txtCteDescr.set_value('');


                if (txt.get_value() != cmb.get_value()) {
                    OnBlur(sender, cmb);
                    ObtenerActDestino();
                }
            }

            function cmb4_ClientSelectedIndexChanged(sender, eventArgs) {
                var txt = $find('<%= txtTerritorioDestino.ClientID %>');
                var cmb = sender;

                var txtCte = $find('<%= txtClienteDestino.ClientID %>');
                var txtCteDescr = $find('<%= txtClienteNombreDestino.ClientID %>');

                txtCte.set_value('');
                txtCteDescr.set_value('');

                if (txt.get_value() != cmb.get_value()) {
                    ClientSelectedIndexChanged(eventArgs.get_item(), txt);
                    ObtenerActDestino();
                }
            }


            function txt9_OnBlur(sender, args) {
            }

            function txt6_OnBlur(sender, args) {
                var txt = sender;

                var txtPrdO = $find('<%= txtProductoOrigen.ClientID %>');
                var txtPrdD = $find('<%= txtProductoDestino.ClientID %>');
                var txtQuitar = $find('<%= txtCantQuitar.ClientID %>');
                var txtActualOrigen = $find('<%= txtCantActualOrigen.ClientID %>');
                var txtActualDestino = $find('<%= txtCantActualDestino.ClientID %>');
                var txtMod = $find('<%= txtCantModificada.ClientID %>');



                if (txt.get_value() != txtPrdD.get_value()) {
                    OnBlur(sender, cmb);
                    ObtenerActDestino();

                    if (txtPrdO.get_value() != '' && txtPrdD.get_value() != '') {
                        //debugger;
                        txtQuitar.enable();
                    }
                    else if (txtQuitar._enabled) {
                        txtQuitar.set_value(txtActualOrigen.get_value());
                        txtMod.set_value(txtActualDestino.get_value() + txtQuitar.get_value());
                        txtQuitar.disable();
                    }
                }
            }

            function cmb6_ClientSelectedIndexChanged(sender, eventArgs) {
                var txt = $find('<%= txtProductoDestino.ClientID %>');
                var txtPrdO = $find('<%= txtProductoOrigen.ClientID %>');
                var txtPrdD = $find('<%= txtProductoDestino.ClientID %>');
                var txtQuitar = $find('<%= txtCantQuitar.ClientID %>');
                var txtActualOrigen = $find('<%= txtCantActualOrigen.ClientID %>');
                var txtActualDestino = $find('<%= txtCantActualDestino.ClientID %>');
                var txtMod = $find('<%= txtCantModificada.ClientID %>');
                var cmb = sender;
                //debugger;

                if (txt.get_value() != cmb.get_value()) {
                    ClientSelectedIndexChanged(eventArgs.get_item(), txt);
                    ObtenerActDestino();

                    if (txtPrdO.get_value() != '' && txtPrdD.get_value() != '') {
                        //debugger;
                        txtQuitar.enable();
                    }
                    else if (txtQuitar._enabled) {
                        txtQuitar.set_value(txtActualOrigen.get_value());
                        txtMod.set_value(txtActualDestino.get_value() + txtQuitar.get_value());
                        txtQuitar.disable();
                    }
                }
            }
            function ValidarMaxLenght(sender, args) {
                //debugger;
                var maxlength = 200;
                if (sender.get_value().length > maxlength - 1) {
                    sender.set_value(sender.get_value.substring(0, maxlength - 1));
                    //alert("Debe ingresar hasta un maximo de "+maxlength+" caracteres");       
                }
                else {
                    SoloAlfanumerico(sender, args);
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

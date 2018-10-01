<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true"
    CodeBehind="CatCliente_Producto.aspx.cs" Inherits="SIANWEB.CatCliente_Producto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
 </telerik:RadAjaxLoadingPanel>
  <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
          <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" 
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
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
            <telerik:AjaxSetting AjaxControlID="txtClienteID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProductoID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbProducto">
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
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
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
                
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
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
                    <asp:Label ID="Label4" runat="server" Text="Centro de distribución" />
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
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Owner="RadTabStrip1" Text="Datos generales" PageViewID="RadPageView1"
                                Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Owner="RadTabStrip1" Text="Datos especiales" PageViewID="RadPageView2">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        SelectedIndex="0" Width="700px">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <table>
                                <tr>
                                    <td style="font-weight: bold">
                                        &nbsp;
                                    </td>
                                    <td style="font-weight: bold">
                                        &nbsp;
                                    </td>
                                    <td style="font-weight: bold" width="70">
                                        &nbsp;
                                    </td>
                                    <td width="350">
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
                                        <asp:Label ID="Label5" runat="server" Text="Cliente" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtClienteID" runat="server" MinValue="1" Width="70px"
                                            AutoPostBack="true" OnTextChanged="txtClave_TextChanged">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt2_OnBlur" OnKeyPress="handleClickEvent" />
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbCliente" runat="server" OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                            Width="350px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" MaxHeight="250px"
                                            AutoPostBack="True" OnSelectedIndexChanged="cmbCliente_SelectedIndexChanged"
                                            EnableAutomaticLoadOnDemand="True" EnableVirtualScrolling="True" ItemsPerRequest="10"
                                            ShowMoreResultsBox="True" OnClientDropDownOpening="Client_Focus">
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
                                            <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                                NoMatches="No hay coincidencias" />
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbCliente"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                            ValidationGroup="guardar">*Requerido</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Producto" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtProductoID" runat="server" MinValue="1" Width="70px"
                                            AutoPostBack="true" OnTextChanged="txtProducto_TextChanged">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt3_OnBlur" OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbProducto" runat="server" OnClientSelectedIndexChanged="cmb3_ClientSelectedIndexChanged"
                                            Width="350px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" AutoPostBack="True"
                                            OnSelectedIndexChanged="cmbProducto_SelectedIndexChanged" MaxHeight="250px" EnableAutomaticLoadOnDemand="True"
                                            EnableVirtualScrolling="True" ItemsPerRequest="10" ShowMoreResultsBox="True"
                                            OnClientDropDownOpening="Client_Focus2">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: left">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: 200px; text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                                NoMatches="No hay coincidencias" />
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbProducto"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                            ValidationGroup="guardar">*Requerido</asp:RequiredFieldValidator>
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
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <table>
                                <tr>
                                    <td style="font-weight: bold">
                                        &nbsp;
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Label ID="Label16" runat="server" Text="Cliente"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCliente" runat="server" Font-Bold="True"></asp:Label>
&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold">
                                        &nbsp;</td>
                                    <td style="font-weight: bold">
                                        <asp:Label ID="Label17" runat="server" Text="Producto"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProducto" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="font-weight: bold">
                                        &nbsp;</td>
                                    <td colspan="2" style="font-weight: bold">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; color: #FF0000;">
                                        &nbsp;
                                    </td>
                                    <td colspan="9" style="text-align: center; color: #FF0000;">
                                        <asp:Label ID="Label7" runat="server" 
                                            Text="Si requieres utilizar varios renglones en la descripción del producto, debes indicar en el texto el brinco de renglón &lt;br&gt; con el siguiente caracter &quot;|&quot; el código es alt+124." />
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
                                    <td>
                                        &nbsp;
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
                                        <asp:Label ID="Label8" runat="server" Text="Clave" />
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txtClave" runat="server" Width="70px" MaxLength="20">
                                            <%--MinValue="1" <NumberFormat DecimalDigits="0" GroupSeparator="" />--%>
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" ValidationGroup="guardar">*Requerido</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
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
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="Label9" runat="server" Text="Descripción" />
                                    </td>
                                    <td colspan="8">
                                        <telerik:RadTextBox ID="txtDescripcion" runat="server" MaxLength="500" 
                                            onpaste="return false" Rows="5" TextMode="MultiLine" Width="550px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                            ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" ValidationGroup="guardar">*Requerido</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="Unidades" />
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtUnidades" runat="server" Width="70px" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Presentación" />
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPresentacion" runat="server" MaxLength="15"
                                            Width="70px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico"/>
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Cantidad facturada" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCantFact" runat="server" MaxLength="9"
                                            Width="70px" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Fecha última vta." />
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="dpUltimaVta" runat="server" Culture="es-MX" Enabled="False"
                                            Width="100px">
                                            <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                ViewSelectorText="x">
                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                    TodayButtonCaption="Hoy" />
                                            </Calendar>
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </DateInput>
                                            <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Inventario final" />
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadNumericTextBox ID="txtInventarioFin" runat="server" Enabled="False" MaxLength="9"
                                            Width="70px">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="Asignado" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAsignado" runat="server" Enabled="False" MaxLength="9"
                                            Width="70px">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="HF_ID" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkActivo" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="chkActivo_CheckedChanged"
                                            Text="Activo" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
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
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="HF_ClvPag" runat="server" />
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
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="rgDet" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            OnNeedDataSource="rgDet_NeedDataSource" OnItemCommand="rgDet_ItemCommand" OnPageIndexChanged="rgDet_PageIndexChanged"
                                            PageSize="5" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                                            <MasterTableView CommandItemDisplay="Top" EditMode="InPlace">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Id_ClpDet" UniqueName="Id_ClpDet" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label0" runat="server" Text='<%# Bind("Id_ClpDet") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold0" runat="server" Text='<%# Bind("Id_ClpDet") %>' />
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="180px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Tipo de precio" UniqueName="Tprecio">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("TPrecioStr") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold1" runat="server" Text='<%# Bind("TPrecio") %>' Visible="false" />
                                                            <telerik:RadComboBox ID="RadComboBox1" runat="server" OnDataBinding="RadComboBox_DataBinding"
                                                                OnDataBound="RadComboBox_DataBound" Width="130px">
                                                            </telerik:RadComboBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Precio" UniqueName="Precio" Display="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Precio") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold2" runat="server" Text='<%# Bind("Precio") %>' />
                                                            <telerik:RadComboBox ID="RadComboBox2" runat="server" SelectedValue='<%# Bind("Tprecio") %>'
                                                                OnDataBinding="cmb2_DataBinding" Visible="false" />
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="180px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Pesos" UniqueName="Pesos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Pesos","{0:N2}") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblold3" runat="server" Text='<%# Bind("Pesos") %>' Visible="false" />
                                                            <telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" Text='<%# Bind("Pesos") %>'
                                                                MinValue="0" MaxLength="9" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic"
                                                                ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="RadNumericTextBox2">
                                                            </asp:RequiredFieldValidator>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="150px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" UniqueName="EditCommandColumn"
                                                        UpdateText="Aceptar" CancelText="Cancelar" InsertText="Aceptar">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="30px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <EditFormSettings>
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Client_Focus() {
                //debugger;
                var combo = $find("<%= cmbCliente.ClientID %>");
                combo.clearSelection();
            }
            function Client_Focus2() {
                //debugger;
                var combo = $find("<%= cmbProducto.ClientID %>");
                combo.clearSelection();
            }
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescripcion.ClientID %>'));
                LimpiarTextBox($find('<%= txtClienteID.ClientID %>'));
                LimpiarTextBox($find('<%= txtProductoID.ClientID %>'));
                LimpiarTextBox($find('<%= txtUnidades.ClientID %>'));
                LimpiarTextBox($find('<%= txtPresentacion.ClientID %>'));

                LimpiarTextBox($find('<%= txtCantFact.ClientID %>'));
                LimpiarTextBox($find('<%= txtInventarioFin.ClientID %>'));
                LimpiarTextBox($find('<%= txtAsignado.ClientID %>'));

                LimpiarComboSelectIndex0($find('<%= cmbCliente.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbProducto.ClientID %>'));

                LimpiarDatePicker($find('<%= dpUltimaVta.ClientID %>'));
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

                        //Selecciona la primera pestaña
                        var multiTab = $find('<%= RadTabStrip1.ClientID %>');
                        multiTab.get_tabs().getTab(0).set_selected(true);

                        var multiPage = $find('<%=RadMultiPage1.ClientID %>');
                        multiPage._pageViews.getPageView(0).set_selected(true);

                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtClave.ClientID %>');
                        txtId.enable();
                        //txtId.focus();

                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatClienteProducto";
                        parametros = parametros + "&sp=spCatLocal_Maximo";
                        parametros = parametros + "&columna=Id_Clp";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);


                        continuarAccion = false;
                        break;
                    case 'save':
                        var button = args.get_item();
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        if (button.get_value() == 'save') {
                            for (i = 0; i < Page_Validators.length; i++) {
                                if (!Page_Validators[i].isvalid) {
                                    if ('CPH_RequiredFieldValidator1' == Page_Validators[i].id || 'CPH_RequiredFieldValidator2' == Page_Validators[i].id) {
                                        radTabStrip.get_allTabs()[0].select()
                                    }
                                    else {
                                        radTabStrip.get_allTabs()[1].select()
                                    }
                                }
                            }
                        }
                        break;
                }

                args.set_cancel(!continuarAccion);
            }


            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbCliente.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtClienteID.ClientID %>'));
            }

            function txt3_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbProducto.ClientID %>'));
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProductoID.ClientID %>'));
            }

            function refreshGrid()
            { }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

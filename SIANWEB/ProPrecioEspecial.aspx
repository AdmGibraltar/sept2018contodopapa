<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="ProPrecioEspecial.aspx.cs" Inherits="SIANWEB.Ventana_PrecioEspecial" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="HF_Tipo" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgCliente" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgProductos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgProductos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Guardar y enviar"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="Imagenes/blank.png" Visible="false" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenId" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                </td>
                <td width="150px">
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt" width="100%">
            <tr>
                <td>                  
                    <table>
                        <tr>
                            <td width="85">
                                <asp:Label ID="lblSolicitud" runat="server" Text="Solicitud"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtSolicitud" runat="server" Width="70px" OnTextChanged="txtSolicitud_OnTextChanged"
                                    AutoPostBack="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="100px" Enabled="false">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput>
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </td>
                            <td colspan="1">
                                <asp:Label ID="lblEstatus" runat="server" Text="Estatus"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="150px" Enabled="false"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                    <%-- ReadOnly="true"--%>
                                </telerik:RadComboBox>
                            </td>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblSolicitudS" runat="server" Text="Solicitud que sustituye" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtSolSus" runat="server" Width="70px" MaxLength="9"
                                    Visible="False" Enabled="False">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td valign="middle" width="85">
                                <asp:Label ID="lblTipoVenta" runat="server" Text="Tipo de venta"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTipo" runat="server" Width="150px" LoadingMessage="Cargando..."
                                    AutoPostBack="true" OnSelectedIndexChanged="cmbTipo_SelectedIndexChanged" OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td valign="middle" style="margin-left: 40px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbTipo"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td valign="middle" width="85">
                                <asp:Label ID="lblSolicitar" runat="server" Text="Solicitar a"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbSolicitud" runat="server" Filter="Contains" Width="350px"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                    DataTextField="Nombre" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." MaxHeight="300px">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left">
                                                    <%# DataBinder.Eval(Container.DataItem, "Nombre") %>
                                                </td>
                                                <td style="visibility: hidden; width: 0px">
                                                    <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                </td>
                                                <td style="visibility: hidden; width: 0px">
                                                    <asp:Label ID="lblMail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td valign="middle">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbSolicitud"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                       <tr>
                            <td width="85>
                                    <asp:Label ID="lblProveedor" runat="server" Text="Proveedor"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbProveedor" runat="server" Width="150px" LoadingMessage="Cargando..."
                                    AutoPostBack="true" OnSelectedIndexChanged="cmbProveedor_SelectedIndexChanged" OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td valign="middle" style="margin-left: 40px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbProveedor"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"
                                    InitialValue="-- Seleccionar --"></asp:RequiredFieldValidator>
                                <br />
                            </td>
                        </tr>
                          
                    </table>

                     <table>
                        <tr>
                            <td width="85">
                                <asp:Label ID="Label3" runat="server" Text="Convenio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNumConvenio" runat="server" Width="70px">                                   
                                </telerik:RadTextBox>
                            </td>
                            <td valign="middle" style="margin-left: 40px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNumConvenio"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                    </asp:RequiredFieldValidator>
                                <br />
                              
                                                      
                            </td>
                        </tr>
                        <tr>
                            <td width="85">
                                <asp:Label ID="Label2" runat="server" Text="Numero de Usuario"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNumUsuario" runat="server" Width="70px">                                   
                                </telerik:RadTextBox>
                            </td>
                            <td valign="middle" style="margin-left: 40px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNumUsuario"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                    </asp:RequiredFieldValidator>
                                <br />                               
                                                            <br />
                            </td>
                        </tr>
                        <tr>
                        <td></td>
                            <td> <asp:ImageButton ID="ImageButton1" runat="server" CssClass="aceptar" ImageUrl="~/Imagenes/blank.png"
                                ToolTip="Actualizar Provedor Y Convenio" OnClick="ImageButton1_Click"  /></td>
                        </tr>
                    </table>                  

                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblClientes" runat="server" Text="Clientes" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgCliente" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgCliente_ItemCommand"
                                    OnNeedDataSource="rgCliente_NeedDataSource" PageSize="2" OnItemDataBound="rgCliente_ItemDataBound"
                                    Height="100px" Width="560">
                                    <MasterTableView CommandItemDisplay="Top" EditMode="InPlace">
                                        <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Cte">
                                                <HeaderStyle Width="70px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_Cte" runat="server" Text='<%# Bind("Id_Cte") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtNum" runat="server" Text='<%# Bind("Id_Cte") %>'
                                                        AutoPostBack="true" MaxLength="9" Width="50px" MinValue="1" OnTextChanged="txtCliente_TextChanged">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnLoad="OnIdNumLoad" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                                <HeaderStyle Width="370px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCte_NomComercial" runat="server" Text='<%# Bind("Cte_NomComercial") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True">
                                                    </telerik:RadTextBox>
                                                    <%--     <telerik:RadComboBox ID="CmbClientes" runat="server" Width="350px" Filter="Contains"
                                                        ChangeTextOnKeyBoardNavigation="true" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur"
                                                        MarkFirstMatch="true" OnClientSelectedIndexChanged="CmbNum_ClientSelectedIndexChanged"
                                                        OnClientLoad="OnDescripcionNumLoad" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                        MaxHeight="250px">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: right; vertical-align: top">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>--%>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" InsertText="Aceptar"
                                                CancelText="Cancelar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
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
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                        PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                        PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="215px">
                                        </Scrolling>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <table style="width:100%">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblNota" runat="server" Text="Nota"></asp:Label>
                                            <telerik:RadTextBox  ID="txtNota" runat="server" Rows="2" TextMode="MultiLine"
                                                Width="370px">
                                            </telerik:RadTextBox>
                                        </td>
                                      <td style="margin-left:30px">
                                        <asp:Label ID="Label1" runat="server" Text="Nota Respuesta"></asp:Label>                                       
                                        <telerik:RadTextBox  ID="txtNotaResp" runat="server" Rows="2" TextMode="MultiLine"
                                            Width="370px">
                                        </telerik:RadTextBox>
                                     </td>
                                   </tr>                                    
                                    <tr style="display:none">
                                    <td style="display:none">
                                            <asp:HiddenField ID="HF_Tipo" runat="server" />
                                            <asp:HiddenField ID="HiddenHeight" runat="server" />
                                            <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                                      </td> 
                                   </tr>
                                </table>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProductos" runat="server" Text="Productos" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="185px" ResizeMode="AdjacentPane"
                                    ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                    <telerik:RadPane ID="RadPane2" runat="server" Height="185px" Scrolling="None" 
                                        OnClientResized="onResize">
                                        <telerik:RadGrid ID="rgProductos" runat="server" AutoGenerateColumns="False" PageSize="3"
                                            GridLines="None" OnNeedDataSource="rgProductos_NeedDataSource" OnItemCommand="rgProductos_ItemCommand"                                          
                                            Height="180px" EnableLinqExpressions="False" 
                                            OnItemDataBound="rgProductos_ItemDataBound">
                                            <MasterTableView CommandItemDisplay="Top" EditMode="InPlace" NoMasterRecordsText="No se encontraron registros.">
                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Clave" UniqueName="Id_Prd" EditFormColumnIndex="0">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClave" runat="server" Text='<%# Bind("Id_Prd") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txtClave" runat="server" Text='<%# Bind("Id_Prd") %>'
                                                                AutoPostBack="true" MaxLength="9" Width="50px" MinValue="1" OnTextChanged="cmbProductoDet_TextChanged">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" OnLoad="OnIdPrdLoad" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="Prd_Descripcion" EditFormColumnIndex="0">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProducto" runat="server" Text='<%# Bind("Prd_Descripcion") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="150px"
                                                                Text='<%# Bind("Prd_Descripcion") %>'>
                                                            </telerik:RadTextBox>
                                                            <%--<telerik:RadComboBox ID="CmbPrd" runat="server" Width="220px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                        MarkFirstMatch="true" OnClientSelectedIndexChanged="CmbPrd_ClientSelectedIndexChanged"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" OnClientLoad="OnDescripcionPrdLoad"
                                                        OnDataBinding="cmbProducto_DataBinding" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                        Height="100px" Text='<%# Bind("Prd_Descripcion") %>'>
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
                                                    </telerik:RadComboBox>--%>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="170px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Vol." UniqueName="Ape_VolVta" EditFormColumnIndex="0">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVol" runat="server" Text='<%# Bind("Ape_VolVta") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txtVolVta" runat="server" Text='<%# Bind("Ape_VolVta") %>'
                                                                MaxLength="9" Width="50px" MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" OnLoad="volLoad" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Moneda" UniqueName="Id_Mon" EditFormColumnIndex="0">
                                                        <HeaderStyle Width="190px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMoneda" runat="server" Text='<%# Bind("Mon_Descripcion") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblMonedaEdit" runat="server" Text='<%# Bind("Id_Mon") %>' Visible="false" />
                                                            <asp:Label ID="lblMonedaEdit2" runat="server" Text='<%# Bind("Mon_Descripcion") %>'
                                                                Visible="false" />
                                                            <telerik:RadComboBox ID="cmbMoneda" runat="server" Width="170px" Filter="Contains"
                                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                                OnClientLoad="MonedaLoad" LoadingMessage="Cargando..." DataTextField="Descripcion"
                                                                DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                                OnDataBinding="cmbMoneda_DataBinding" Text='<%# Bind("Mon_Descripcion") %>'>
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
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Precio venta (unitario)" UniqueName="Ape_PreVta">
                                                        <HeaderStyle Width="80px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrecioVenta" runat="server" Text='<%# Bind("Ape_PreVta","{0:N2}") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txtPreVta" runat="server" Text='<%# Bind("Ape_PreVta") %>'
                                                                MaxLength="9" Width="60px" MinValue="1">
                                                                <ClientEvents OnKeyPress="handleClickEvent" OnLoad="PreVtaLoad" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Fec. inicio" UniqueName="Ape_FecInicio">
                                                        <HeaderStyle Width="120px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecIni" runat="server" Text='<%# Bind("Ape_FecInicio", "{0:MMM-yyyy}") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadDatePicker ID="rdp_FecIni" runat="server"  DbSelectedDate='<%# Bind("Ape_FecInicio") %>'
                                                                OnSelectedDateChanged="Ini_SelectedDateChanged" AutoPostBack="true" Width="100px" Enabled="False">
                                                                <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x">
                                                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                        TodayButtonCaption="Hoy" />
                                                                </Calendar>
                                                                <DateInput ID="DateInput1" runat="server" DateFormat="MMM-yyyy" DisplayDateFormat="MMM-yyyy">
                                                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                </DateInput>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" Enabled="False" />
                                                            </telerik:RadDatePicker>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Fec. fin" UniqueName="Ape_FecFin" DataField="Ape_FecFin">
                                                        <HeaderStyle Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecFin" runat="server" Text='<%# Bind("Ape_FecFin", "{0:MMM-yyyy}") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadDatePicker ID="rdp_FecFin" runat="server"  DbSelectedDate='<%# Bind("Ape_FecFin") %>'
                                                                Width="100px" OnSelectedDateChanged="fin_SelectedDateChanged" AutoPostBack="true" Calendar-FirstDayOfWeek="Default" DatePopupButton-Visible="True" DatePopupButton-Enabled="False">
                                                                <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x">
                                                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                        TodayButtonCaption="Hoy" />
                                                                </Calendar>
                                                                <DateInput ID="DateInput2" runat="server" DateFormat="MMM-yyyy" DisplayDateFormat="MMM-yyyy">
                                                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                                </DateInput>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                            </telerik:RadDatePicker>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Precio especial AAA" UniqueName="Ape_PreEsp">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPreEsp" runat="server" Text='<%# Bind("Ape_PreEsp","{0:N2}") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txtPreEsp" runat="server" Text='<%# Bind("Ape_PreEsp") %>'
                                                                MaxLength="9" Width="70px" Value="0" ReadOnly="true">
                                                                <ClientEvents OnKeyPress="handleClickEvent" OnLoad="EspLoad" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Autorizado" UniqueName="Ape_Autorizado" Display="false">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAutorizado" runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Ape_Estatus") is DBNull ? true : Convert.ToString(DataBinder.Eval(Container.DataItem, "Ape_Estatus"))=="A" ? true : false %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Rechazado" UniqueName="Ape_Rechazado" Display="false">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle Width="100px" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRechazado" runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Ape_Estatus") is DBNull ? true : Convert.ToString(DataBinder.Eval(Container.DataItem, "Ape_Estatus"))=="R" ? true : false %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn HeaderText="Fec. Aut." UniqueName="Ape_FecAut" DataField="Ape_FecAut"
                                                        DataFormatString="{0:dd/MM/yyyy}" Display="false">
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Hra. Aut." UniqueName="Ape_HrAut" DataField="Ape_FecAut"
                                                        DataFormatString="{0:hh:mm tt}" Display="false">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" InsertText="Aceptar"
                                                        CancelText="Cancelar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                        UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                            Width="30px" />
                                                    </telerik:GridButtonColumn>
                                                    <%--<telerik:GridTemplateColumn UniqueName="consecutivo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConsecutivoItem" runat="server" Text='<%# Bind("Id_ApePro") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblConsecutivoEdit" runat="server" Text='<%# Bind("Id_ApePro") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="200px">
                                                </Scrolling>
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </td>
                        </tr>
                       <tr>
                        <td>&#160;</td>
                       </tr>
                       <tr>
                        <td>&#160;</td>
                       </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var tipo = document.getElementById('<%= HF_Tipo.ClientID %>');
            var estatus = $find('<%= cmbEstatus.ClientID %>');
            var IdPrd1;
            var IdPrd2;
            var DescPrd1;
            var DescPrd2;


            function OnDescripcionNumLoad(sender, args) {
                DescPrd2 = sender;
            }
            function OnIdNumLoad(sender, args) {
                IdPrd2 = sender;

            }

            function OnIdPrdLoad(sender, args) {
                IdPrd1 = sender;

                if (tipo.value == '1' || tipo.value == '2') {
                    sender.disable();
                }
            }
            function OnDescripcionPrdLoad(sender, args) {
                DescPrd1 = sender;
                if (tipo.value == '1' || tipo.value == '2') {
                    sender.disable();
                }
            }
            function volLoad(sender, args) {
                if (tipo.value == '1' || tipo.value == '2') {
                    sender.disable();
                }
            }
            function PreVtaLoad(sender, args) {
                if (tipo.value == '1') {
                    sender.disable();
                }
            }
            function EspLoad(sender, args) {
                if (tipo.value == '1') {
                    sender.disable();
                }
            }
            function MonedaLoad(sender, args) {
                if (tipo.value == '1' || tipo.value == '2') {
                    sender.disable();
                }
            }

            function txtPrd_OnBlur(sender, eventArgs) {
                OnBlur(sender, DescPrd1);
            }
            function txtNum_OnBlur(sender, eventArgs) {
                OnBlur(sender, DescPrd2);
            }


            //cuando el campo de texto pirde el foco
            

           


            function CmbPrd_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), IdPrd1);
            }
            function CmbNum_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), IdPrd2);
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }

            function alertClose(str) {

                var oWnd = radalert(str, 330, 150);
                oWnd.add_close(function () {
                    CloseAndRebind();
                });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
                return false;
            }

            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                if (postback != 'N') {
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                    ajaxManager.ajaxRequest('panel');
                }
                else {
                    document.getElementById("<%=clientSideIsPostBack.ClientID %>").value = 'Y';
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

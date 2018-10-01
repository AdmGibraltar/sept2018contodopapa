<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="ProEntradaVirtual.aspx.cs" Inherits="SIANWEB.ProEntradaVirtual" %>

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
            <telerik:AjaxSetting AjaxControlID="rgProductos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgProductos" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtImporteFactura" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtImporteUb" UpdatePanelHeight="" />
                </UpdatedControls>

               </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="btnAutorizar">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgDevParcial" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgDetalleMov" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                  
                </UpdatedControls>
            </telerik:AjaxSetting>            
               <telerik:AjaxSetting AjaxControlID="btnDevolucion">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgDevParcial" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgDetalleMov" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                  
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">

        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick" SingleClick="Button" SingleClickText="Procesando...">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <%--<telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Guardar y enviar"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />--%>
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
                    <asp:HiddenField ID="hiddenGui" runat="server" />
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
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" >
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="D&lt;u&gt;e&lt;/u&gt;talle movimientos" AccessKey="E" PageViewID="RadPageViewDetalles" Enabled="False">
                            </telerik:RadTab> 
                            <telerik:RadTab runat="server" Text="Realizar D&lt;u&gt;e&lt;/u&gt;voluciones" AccessKey="E" PageViewID="RadPageViewDevoluciones" Enabled="False">
                            </telerik:RadTab>                            
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" heigth="370px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadSplitter ID="RadSplitter" runat="server" Height="370px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0" Width="100.5%">
                                <telerik:RadPane ID="RadPane" runat="server" BorderColor="White" BorderStyle="Solid"
                                    BorderWidth="1px" Height="370px" OnClientResized="onResize"  Scrolling="None">
                                    <div id="divGenerales" runat="server">                                   
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
                                                <td width="100">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnAutorizar" runat="server" Text="Autorizar" 
                                                    ToolTip="Autorizar" OnClick="BtnAutorizar_Click" Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" 
                                                    ToolTip="Rechazar" OnClick="BtnRechazar_Click" Visible="false" />
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
                                        </tr>
                                     </table>  
                                            
                                    <table>
                                         <tr>
                                            <td width="85">                            
                            
                                                <asp:Label ID="LabelProveedor" runat="server" Text="Proveedor"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtProveedorId" runat="server" MaxLength="9" MinValue="1"
                                                    Width="50px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />  
                                                     <ClientEvents OnBlur="txtProveedorId_OnBlur" OnKeyPress="handleClickEvent" />                                 
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbProveedor" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                    OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged"
                                                    Width="362px" MaxHeight="200px">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cmbProveedor"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                    ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &#160;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                     <table>
                                       <tr>                           
                                            <td width="85">
                                                <asp:Label ID="Label3" runat="server" Text="Cliente"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtNumCliente" runat="server" MinValue="1" Width="70px"
                                                    MaxLength="9" OnTextChanged="txtNumCliente_TextChanged" AutoPostBack="True">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCliente" runat="server" Width="296px" enabled="false">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>                
                                    </table> 
                                    <table>
                                       <tr>
                                            <td width="85">
                                                    <asp:Label ID="Label4" runat="server" Text="Días Crédito"></asp:Label>
                                            </td>
                                            <td>
                                               <telerik:RadTextBox ID="txtCredito" enabled="false" runat="server" Width="70px" DataFormatString="{0:N2}" onblur="x1 = Number(this.value) || 0; this.value=addCommas(this.value)">                                   
                                                </telerik:RadTextBox>
                                            </td>
                                            <td valign="middle" style="margin-left: 40px">
                              
                                                <br />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <asp:Label ID="lblSolicitante2" runat="server" Text="Solicitante"></asp:Label>
                                            </td>
                                             <td colspan="2">
                                                <asp:Label ID="lblSolicitanteId" runat="server"></asp:Label>
                                                   <telerik:RadComboBox ID="cmbSolicitante" runat="server" Width="150px" LoadingMessage="Cargando..."
                                                    AutoPostBack="true"  enabled="false" >
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>                              
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>                           
                                        </tr>                          
                                    </table>    
                                    <table width="100%">       
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblProductos" runat="server" Text="Productos" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="185px" ResizeMode="AdjacentPane"
                                                    ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                                    <telerik:RadPane ID="RadPane2" runat="server" Height="185px" Scrolling="Both" 
                                                        OnClientResized="onResize">
                                                        <telerik:RadGrid ID="rgProductos" runat="server" AutoGenerateColumns="False" PageSize="3"
                                                            GridLines="None" OnNeedDataSource="rgProductos_NeedDataSource" OnItemCommand="rgProductos_ItemCommand"                                          
                                                            Width="100%" Height="350"  EnableLinqExpressions="False" 
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
                                                        
                                                                        </EditItemTemplate>
                                                                        <HeaderStyle Width="170px" />
                                                                    </telerik:GridTemplateColumn>   
                                                                     <telerik:GridTemplateColumn HeaderText="Cantidad" UniqueName="Env_Cantidad">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("Env_Cantidad") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Env_Cantidad") %>'
                                                                                MaxLength="9" Width="60px" MinValue="1" OnTextChanged="txtProducto_TextChanged" AutoPostBack="true"> 
                                                                                 <ClientEvents OnLoad="txtCantidad_OnLoad"  />                                                         
                                                                            </telerik:RadNumericTextBox>
                                                                        </EditItemTemplate>
                                                                    </telerik:GridTemplateColumn>      
                                                    
                                                                    <telerik:GridTemplateColumn HeaderText="Costo" UniqueName="Env_Costo" Visible="False" >
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCosto" runat="server" Text='<%# Bind("Env_Costo","{0:N2}") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <telerik:RadNumericTextBox ID="txtCosto" runat="server" Text='<%# Bind("Env_Costo") %>'
                                                                                MaxLength="9" Width="60px" MinValue="1">
                                                                                <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtCosto_OnLoad"  />
                                                                            </telerik:RadNumericTextBox>
                                                                        </EditItemTemplate>
                                                                    </telerik:GridTemplateColumn>   
                                                                    <telerik:GridTemplateColumn HeaderText="Precio de Venta" UniqueName="Env_PreVta">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPrecioVenta" runat="server" Text='<%# Bind("Env_PreVta","{0:N2}") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <telerik:RadNumericTextBox ID="txtPreVta" runat="server" Text='<%# Bind("Env_PreVta") %>'
                                                                                MaxLength="9" Width="60px" MinValue="1"  OnTextChanged="txtProducto_TextChanged" AutoPostBack="true">
                                                                                <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtPreVta_OnLoad"  />
                                                                            </telerik:RadNumericTextBox>
                                                                        </EditItemTemplate>
                                                                    </telerik:GridTemplateColumn>                                                     
                                                                    <telerik:GridTemplateColumn  HeaderText="Importe" UniqueName="Env_Importe">
                                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEnv_Importe" runat="server" Text='<%# Bind("Env_Importe", "{0:N2}") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                          <telerik:RadNumericTextBox ID="txtEnvImporte" runat="server" MinValue="0" MaxLength="9"
                                                                            Width="50px" Text='<%# Bind("Env_Importe", "{0:N2}") %>' BackColor="Transparent" ReadOnly="true"
                                                                            CssClass="AlignRight">
                                                                            <ClientEvents OnLoad="txtEnvImportePartida_OnLoad" />
                                                                        </telerik:RadNumericTextBox>                                                        
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>    
                                                                 <telerik:GridTemplateColumn  HeaderText="Utilidad Bruta %" UniqueName="Env_UBruta">
                                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEnv_ImporteUBruta" runat="server" Text='<%# Bind("Env_UBruta", "{0:N2}") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                          <telerik:RadNumericTextBox ID="txtUBruta" runat="server"  MaxLength="9"
                                                                            Width="50px" Text='<%# Bind("Env_UBruta", "{0:N2}") %>' BackColor="Transparent" ReadOnly="true"
                                                                            CssClass="AlignRight">
                                                                            <ClientEvents OnLoad="txtEnvImportePartidaUBruta_OnLoad" />
                                                                        </telerik:RadNumericTextBox>                                                        
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>                                                                                                     
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
                                     </table>
                                   </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="370px">                           
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" BorderSize="0" Height="370px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" Width="101%">
                                <telerik:RadPane ID="RadPane1" runat="server" BorderStyle="None" Height="370px" OnClientResized="onResize">
                                        <table style="width:100%">
                                            <tr>
                                                <td>                                                
                                                    <telerik:RadGrid ID="rgDetalleMov" runat="server" AutoGenerateColumns="False" PageSize="3"
                                                            GridLines="None" OnNeedDataSource="rgDetalleMov_NeedDataSource"                                          
                                                            Width="100%" Height="350"  EnableLinqExpressions="False"
                                                            MasterTableView-NoMasterRecordsText="No se encontraron registros."  
                                                            GroupingSettings-RetainGroupFootersVisibility="true" ShowFooter="True">
                                                            <MasterTableView ShowGroupFooter="true" >
                                                                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                                <Columns>
                                                                    <telerik:GridBoundColumn HeaderText="# Producto" UniqueName="Id_Prd" DataField="Id_Prd">
                                                                        <HeaderStyle Width="70px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                   
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Folio Entrada" UniqueName="Id_Es" DataField="Id_Es">                                                                                                                              
                                                                        <HeaderStyle Width="80px" />
                                                                    </telerik:GridBoundColumn>   
                                                                    <telerik:GridBoundColumn  HeaderText="Tipo" UniqueName="Tipo" DataField="Tipo">
                                                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                   
                                                                    </telerik:GridBoundColumn>  
                                                                     <telerik:GridBoundColumn HeaderText="Tipo de Movimiento" UniqueName="Id_Tm" DataField="Id_Tm">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                          
                                                                    </telerik:GridBoundColumn>  
                                                                    <telerik:GridBoundColumn HeaderText="Fecha" UniqueName="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                            
                                                                    </telerik:GridBoundColumn>  
                                                                     <telerik:GridBoundColumn HeaderText="Cantidad" UniqueName="Cant" DataField="Cant" Aggregate="Sum" FooterText="Total: ">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                  
                                                                    </telerik:GridBoundColumn>   
                                                                </Columns>
                                                                 <GroupByExpressions>
                                                                    <telerik:GridGroupByExpression>
                                                                        <SelectFields>
                                                                            <telerik:GridGroupByField FieldAlias="Producto" FieldName="Id_Prd">
                                                                             </telerik:GridGroupByField>                                                                               
                                                                        </SelectFields>
                                                                        <GroupByFields>
                                                                            <telerik:GridGroupByField FieldName="Id_Prd" SortOrder="Ascending"></telerik:GridGroupByField>
                                                                        </GroupByFields>
                                                                    </telerik:GridGroupByExpression>
                                                                   </GroupByExpressions>
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

                                                </td>                                                
                                            </tr>
                                        </table>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDevoluciones" runat="server" heigth="380px">
                         <telerik:RadSplitter ID="RadSplitterDevol" runat="server" Height="300px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPaneDevol" runat="server" Height="300px" OnClientResized="onResize" BorderStyle="None"> 
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnDevolucion" runat="server" Text="Aplicar Devolución" 
                                            ToolTip="Autorizar" OnClick="BtnDevolucion_Click" Visible="False" />
                                        </td>
                                                
                                    </tr>
                                </table>
                                                          
                                <telerik:RadGrid ID="rgDevParcial" runat="server" GridLines="None" PageSize="15"
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." AutoGenerateColumns="False"
                                    AllowPaging="false" HeaderStyle-HorizontalAlign="Center" OnNeedDataSource="rgDevParcial_NeedDataSource"                                   
                                     Enabled="true">
                                    <MasterTableView>
                                        <Columns>                                           
                                           
                                            <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion">
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Env_Cantidad" HeaderText="Cantidad" UniqueName="Env_Cantidad">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>   
                                            <telerik:GridBoundColumn DataField="Env_Costo" HeaderText="Costo" UniqueName="Env_Costo" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                            
                                            <telerik:GridTemplateColumn HeaderText="Cant.Dev." DataField="Env_CantDevuelta" UniqueName="Env_CantDevuelta">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="NumCantDevuelta" runat="server" Width="50px" MaxLength="9"
                                                        MinValue="0" Text='<%# Eval("Env_CantDevuelta") %>'  OnTextChanged="NumCantDevuelta_TextChanged" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                                </telerik:RadPane>
                                </telerik:RadSplitter>
                                  </telerik:RadPageView>

                        </telerik:RadMultiPage>
                   </td>
            </tr>
        </table>
        <table width="60%" runat="server" id="divtotales" align="right" style="font-family: verdana; font-size: 8pt">
            <tr>                                    
                <td width="120">&#160;</td>
                    <td align="right">
                    <asp:Label ID="LabelSubtotal" runat="server" Text="Importe a Facturar"></asp:Label>
                </td>
                <td align="left" width="35">
                    <telerik:RadNumericTextBox ID="txtImporteFactura" runat="server" Enabled="false"
                        Value="0" CssClass="AlignRight">
                    </telerik:RadNumericTextBox>
                </td>
                <td align="right" >
                    <asp:Label ID="Label14" runat="server" Text="Total % UB"></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadNumericTextBox ID="txtImporteUb" runat="server" Enabled="false"
                        Value="0" CssClass="AlignRight">
                    </telerik:RadNumericTextBox>
                </td>                   
                    
                </tr>  
            </table>
                        
            <table  style="font-family: verdana; font-size: 8pt;width:100%">
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
                         <asp:HiddenField ID="HiddenRebind" runat="server" />
       
       
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

            var _prd;

            var _prd_precio;
            var _prd_costo;
            var _importe;
            var _importeUBruta;
            var _revisa_precio = true;
            var ultimo_precio;
            var ultima_cantidad;
            var ultimo_producto;

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

                _prd = sender;
                ultimo_producto = sender.get_value();
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

                sender.disable();

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




            function txtPreVta_Onblur() {



                if (_prd.get_value() == '') {
                    return;
                }

                if (_prd_precio.get_value() == '') {
                    _prd_precio.set_value('0');
                }

                if (ultimo_precio == _prd_precio.get_value() && _prd_precio.get_value() != '0') {
                    return;
                }
                else {
                    ultimo_precio = _prd_precio.get_value();
                }

                CalcularImporte(_importe, _prd_cantidad, _prd_precio);
                CalcularImporteUBruta(_importeUBruta, _prd_cantidad, _prd_costo, _prd_precio);

            }

            function txtProductoPartida_OnLoad(sender, args) {
                _prd = sender;
                ultimo_producto = sender.get_value();
            }


            function UpExcel() {

                var oWnd = radopen("Ventana_DocEVirtual.aspx", "AbrirVentana_vExcel");
                oWnd.setSize(550, 350);
                oWnd.center();
            }

            function txtPreVta_OnLoad(sender, args) {
                _prd_precio = sender;
                ultimo_precio = sender.get_value();
            }

            function txtCosto_OnLoad(sender, args) {
                _prd_costo = sender;

            }

            function txtCantidad_OnLoad(sender, args) {
                _prd_cantidad = sender;
                ultima_cantidad = sender.get_value();
            }

            function txtEnvImportePartida_OnLoad(sender, args) {
                _importe = sender;
            }


            function txtEnvImportePartidaUBruta_OnLoad(sender, args) {
                _importeUBruta = sender;
            }


            function CalcularImporte(lblImporte, txtCantidad, txtPrecio) {
                lblImporte.set_value(txtCantidad.get_value() * txtPrecio.get_value());
            }

            function CalcularImporteUBruta(lblImporteUBruta, txtCantidad, txtCosto, txtPrecio) {
                var importe = txtCantidad.get_value() * txtPrecio.get_value();
                var Costo = txtCantidad.get_value() * txtCosto.get_value();
                var uBruta = (Costo / importe) * 100;
                lblImporteUBruta.set_value(uBruta);
            }

            function txtCantidad_Onblur() {

                if (_prd.get_value() == '' || _prd_cantidad.get_value() == '') {
                    return;
                }
                if (ultima_cantidad == _prd_cantidad.get_value()) {
                    return;
                }
                else {
                    ultima_cantidad = _prd_cantidad.get_value();
                }
                CalcularImporte(_importe, _prd_cantidad, _prd_precio);
                CalcularImporteUBruta(_importeUBruta, _prd_cantidad, _prd_costo, _prd_precio);
            }


            //cuando el campo de texto pirde el foco


            function addCommas(nStr) {
                nStr = nStr.replace(/[^0-9\.]/g, "");
                nStr = Number(nStr).toFixed(2);   // remove excess decimals
                var rgx = /(\d+)(\d{3})/;
                while (rgx.test(nStr)) {
                    nStr = nStr.replace(rgx, '$1,$2');
                }
                if (nStr.indexOf('.') == -1) {  // if whole number add .00
                    nStr = nStr + ".00";
                }
                nStr = nStr.replace(/(\.\d)$/, "$10");  // if only one DP add another 0
                return nStr;
            }


            function txtProveedorId_OnBlur(sender, args) {
                var combo = $find('<%= cmbProveedor.ClientID %>');
                //debugger;
                OnBlur(sender, combo);
            }

            function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProveedorId.ClientID %>'));

            }



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
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }




            function ToolBar_ClientClick(sender, args) {

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        break;
                    case 'mail':
                        button.set_enabled(false);
                        continuarAccion = true;
                        break;
                    case 'save':
                        button.set_enabled(false);
                        continuarAccion = true;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }



            function createInput(inputType, inputID, inputName) {
                var input = document.createElement("input");

                input.setAttribute("type", inputType);
                input.setAttribute("id", inputID);
                input.setAttribute("name", inputName);

                return input;
            }

            function createLabel(forArrt) {
                var label = document.createElement("label");

                label.setAttribute("for", forArrt);
                label.innerHTML = "File info: ";

                return label;
            }

            function onClientFileUploaded(radAsyncUpload, args) {
                var row = args.get_row(),
                  inputName = radAsyncUpload.getAdditionalFieldID("TextBox"),
                  inputType = "text",
                  inputID = inputName,
                  input = createInput(inputType, inputID, inputName),
                  label = createLabel(inputID),
                   br = document.createElement("br");
                row.appendChild(br);
                row.appendChild(label);
                row.appendChild(input);
            }
            //<![CDATA[

            var fileList = null,
                    fileListUL = null;

            function fileUploaded(sender, args) {
                var name = args.get_fileName(),
                         li = document.createElement("li");

                if (fileList == null) {
                    fileList = document.getElementById("exFileList");
                    fileListUL = document.createElement("ul");
                    fileList.appendChild(fileListUL);

                    fileList.style.display = "block";
                }

                li.innerHTML = name;
                fileListUL.appendChild(li);
            }
            //]]>
            

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

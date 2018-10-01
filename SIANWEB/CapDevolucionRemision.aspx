<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPage02.master" CodeBehind="CapDevolucionRemision.aspx.cs" Inherits="SIANWEB.CapDevolucionRemision" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ChkDetalleRemisionHdr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDevParcial" LoadingPanelID="RadAjaxLoadingPanel1" 
                        UpdatePanelHeight="" />       
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="NumCantRemisionDevuelta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDetalleRemision" LoadingPanelID="RadAjaxLoadingPanel1" 
                        UpdatePanelHeight="" />       
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="txtNumCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipoMov">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="txtTipoId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtTerritorio">
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
            <telerik:AjaxSetting AjaxControlID="BtnFacturar">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgDevParcial" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgDetalleMov" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                  
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnDetalleRemisiones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight=""/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnEntradaRemision">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight=""/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <%-- Factura (Detalle de factura) --%>
            <telerik:RadWindow ID="AbrirVentana_Factura" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="940px" Height="645px" Animation="Fade"
                ShowContentDuringLoad="false" KeepInScreenBounds="True" Overlay="True" Title="Factura"
                Modal="True" OnClientClose="CerrarWindow_ClientEvent" OnClientPageLoad="LimpiarBanderaRebind"
                Localization-Restore="Restaurar" Localization-Maximize="Maximizar" Localization-Close="Cerrar"
                InitialBehaviors="Maximize">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" >
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Guardar y enviar"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" Visible="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save" 
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" Visible="false"  />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="Imagenes/blank.png" Visible="false" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenId" runat="server" Value="-1" />
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
                        SelectedIndex="2"  OnClientTabSelected="OnClientTabSelected" OnTabClick="RadTabStrip1_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="D&lt;u&gt;e&lt;/u&gt;talle movimientos" AccessKey="E" PageViewID="RadPageViewDetalles" visible="false"  enabled="False">
                            </telerik:RadTab> 
                            <telerik:RadTab runat="server" Text="Detalle Devoluciones" AccessKey="E" PageViewID="RadPageViewDetalleDevoluciones" enabled="False">
                            </telerik:RadTab>                            
                            <telerik:RadTab runat="server" Text="Saldo de R&lt;u&gt;e&lt;/u&gt;misiones" AccessKey="E" PageViewID="RadPageViewDevoluciones" enabled="False">
                            </telerik:RadTab>  
                            <telerik:RadTab runat="server" Text="Detalle de Remisiones" PageViewId="RadPageViewDetalleRemisiones" Visible="false"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="2" BorderStyle="Solid"
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
                                                    <asp:Label ID="lblSolicitud" runat="server" Text="Folio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtSolicitud" runat="server" Width="70px" 
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
                                                    <asp:Label ID="lblEstatus" runat="server" Text="Estatus" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="150px" Enabled="false" Visible="false"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                                        <%-- ReadOnly="true"--%>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="100">
                                                    &nbsp;
                                                </td>                                                                        
                                            </tr>
                                    </table>
                                     <table>
                                        <tr>
                                            <td valign="middle" width="85">
                                                <asp:Label ID="lblTipoMov" runat="server" Text="Tipo de Movimiento"></asp:Label>
                                            </td>
                                              <td>
                                                    <telerik:RadNumericTextBox ID="txtTipoId" runat="server" MaxLength="9" MinValue="0"
                                                        Width="50px"  OnTextChanged="txtTipoId_TextChanged" AutoPostBack="True">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTipoId_OnBlur" OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbTipoMov" runat="server" Filter="Contains" Width="350px"
                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                    DataTextField="Nombre" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                    LoadingMessage="Cargando..." MaxHeight="300px" OnClientSelectedIndexChanged="cmbTipoMov_ClientSelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion")%>
                                                                </td>
                                                                <td style="visibility: hidden; width: 0px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>                            
                                            <td>
                                                <asp:Label id="lblTipoGarantia" runat="server" Text="Tipo de Garantía" Visible="false"></asp:Label>    
                                            </td>      
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>

                                                <telerik:RadComboBox ID="cmbTipoGarantia" runat="server" Filter="Contains" Width="350px"
                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                    DataTextField="Nombre" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                    LoadingMessage="Cargando..." MaxHeight="300px" Visible="false" autopostback="True" onselectedindexchanged="cmbTipoGarantiaChanged">
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion")%>
                                                                </td>
                                                                <td style="visibility: hidden; width: 0px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>
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
                                                <asp:Label ID="Label3" runat="server" Text="Cliente"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtNumCliente" runat="server" MinValue="1" Width="70px"
                                                    MaxLength="9" OnTextChanged="txtCliente_TextChanged" AutoPostBack="True" OnFocus="_ValidarFechaEnPeriodo">
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
                                                <asp:Label ID="Label2" runat="server" Text="Territorio"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                                                Width="70px">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txtTerritorio_OnBlur"  OnKeyPress="handleClickEvent" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                                    LoadingMessage="Cargando..." MarkFirstMatch="true" MaxHeight="300px" OnClientBlur="Combo_ClientBlur"
                                                        OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged"
                                                    OnSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged" Width="300px">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                                    <div style="display: none">
                                                                        <asp:Label ID="lbl_Id_Rik" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Rik") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_Rik_Nombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rik_Nombre") %>'></asp:Label>
                                                                    </div>
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
                                            <td>
                                                <asp:Label ID="LblRealizar" runat="server" Text="Realizar:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="DevRemTipo_Value" runat="server"/>
                                                <asp:RadioButtonList ID="RblRealizar" runat="server" RepeatDirection="Horizontal" TextAlign="Right">
                                                    <asp:ListItem Text="Devolución Almacén" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Facturar al cliente" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <table>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="display:none">
                                        <tr>
<%--                                         <td width="85">
                                                <asp:Label ID="Label1" runat="server" Text="Territorio"></asp:Label>
                                            </td>
                                         <td>
                                                    <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTerritorio_OnBlur"  OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                                        LoadingMessage="Cargando..." MarkFirstMatch="true" MaxHeight="300px" OnClientBlur="Combo_ClientBlur"
                                                         OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged" Width="300px">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                                        <div style="display: none">
                                                                            <asp:Label ID="lbl_Id_Rik" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Rik") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Rik_Nombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rik_Nombre") %>'></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>                          
--%>                                        </tr>
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
                                                                 <telerik:GridBoundColumn  HeaderText="# Entrada " UniqueName="Id_Es" DataField="Id_Es">
                                                                        <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                   
                                                                    </telerik:GridBoundColumn> 
                                                                    <telerik:GridBoundColumn  HeaderText="# Remisión" UniqueName="Id_Rem" DataField="Id_Rem">
                                                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                   
                                                                    </telerik:GridBoundColumn>         
                                                                    <telerik:GridBoundColumn HeaderText="# Cliente" UniqueName="Id_Cte" DataField="Id_Cte">
                                                                        <HeaderStyle Width="70px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                   
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="# Territorio" UniqueName="Id_Ter" DataField="Id_Ter">                                                                                                                              
                                                                        <HeaderStyle Width="90px" />
                                                                    </telerik:GridBoundColumn>  
                                                                                                                                  
                                                                    <telerik:GridBoundColumn HeaderText="# Producto" UniqueName="Id_Prd" DataField="Id_Prd">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                            
                                                                    </telerik:GridBoundColumn>  
                                                                    <telerik:GridBoundColumn HeaderText="Producto Nombre" UniqueName="Prd_Descripcion" DataField="Prd_Descripcion">
                                                                        <HeaderStyle Width="280px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                            
                                                                    </telerik:GridBoundColumn> 
                                                                     <telerik:GridBoundColumn HeaderText="Cantidad" UniqueName="Cant" DataField="Cant">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                  
                                                                    </telerik:GridBoundColumn>   
                                                                </Columns>                                                                
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
                        <telerik:RadPageView ID="RadPageViewDevoluciones" runat="server" heigth="700px">
                         <telerik:RadSplitter ID="RadSplitterDevol" runat="server" Height="700px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPaneDevol" runat="server" Height="700px" OnClientResized="onResize" BorderStyle="None"> 
                               <table>
                                    <tr>
                                        <td><asp:Button ID="BtnDetalleRemisiones" runat="server" Text="Ver Detalle" OnClick="BtnDetalleRemisiones_Click" /></td>
                                        <td><asp:Button ID="BtnFacturarRemisiones" runat="server" Text="Facturar" OnClick="BtnFacturarRemision_Click" Visible="false" /></td>
                                        <td>
                                            <asp:Button ID="BtnDevolucion" runat="server" Text="Aplicar Devolución" 
                                            ToolTip="Aplicar Devolución" OnClick="BtnDevolucion_Click" Visible="false" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnFacturar" runat="server" Text="Facturar" 
                                            ToolTip="Facturar" OnClick="BtnFacturar_Click" />
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
                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="# Territorio" UniqueName="Id_Ter" Display="false" >
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="# Remisión" UniqueName="Id_Rem" Display="false" >
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Rem_Cant" HeaderText="Saldo Unidades" UniqueName="Rem_Cant">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>   
                                            <telerik:GridBoundColumn DataField="Rem_Precio" HeaderText="Saldo Pesos" UniqueName="Rem_Precio" Visible="false" >
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                            
                                            <telerik:GridTemplateColumn HeaderText="Cant.Dev." DataField="Rem_CantE" UniqueName="Rem_CantE" Visible="false">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="NumCantDevuelta" runat="server" Width="50px" MaxLength="9"
                                                        MinValue="0" Text='<%# Eval("Rem_CantE") %>' OnTextChanged="NumCantDevuelta_TextChanged" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Detalle de Remisiones" UniqueName="BDetalleRemision">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label ID="LblDetalleRemisiones" runat="server" Width="120px" Text="Detalle de Remisiones"></asp:Label>
                                                <asp:CheckBox ID="ChkDetalleRemisionHdr" runat="server" AutoPostBack="true" OnCheckedChanged="ChkDetalleRemisionHdr_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDetalleRemision" runat="server" />
                                            </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                         <GroupByExpressions> 
                                                 <telerik:GridGroupByExpression> 
                                                 <SelectFields> 
                                                    <telerik:GridGroupByField FieldName="Id_Ter" HeaderText=" " HeaderValueSeparator=""></telerik:GridGroupByField>
                                                    <telerik:GridGroupByField FieldName="Ter_Nombre" HeaderText=" " HeaderValueSeparator=""></telerik:GridGroupByField>
                                                 </SelectFields> 
                                                   <GroupByFields> 
                                                      <telerik:GridGroupByField  
                                                         FieldName="Id_Ter"/> 
                                                       </GroupByFields> 
                                                 </telerik:GridGroupByExpression> 
                                            </GroupByExpressions>                   
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                                </telerik:RadPane>
                                <table width="60%" runat="server" id="divtotales" align="right" style="font-family: verdana; font-size: 8pt">
                                <tr>                                    
                                    <td width="120">&#160;</td>
                                        <td align="right">
                                        <asp:Label ID="LabelSubtotal" runat="server" Text="Total Cantidad"></asp:Label>
                                    </td>
                                    <td align="left" width="35">
                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Enabled="false"
                                            Value="0" CssClass="AlignRight">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td align="right" >
                                        <asp:Label ID="Label14" runat="server" Text="Total Pesos"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <telerik:RadNumericTextBox ID="txtImporteUb" runat="server" Enabled="false"
                                            Value="0" CssClass="AlignRight">
                                        </telerik:RadNumericTextBox>
                                    </td>                   
                    
                                    </tr>  
                                </table>

                                </telerik:RadSplitter>
                                  </telerik:RadPageView>                        
                        <telerik:RadPageView ID="RadPageViewDetalleRemisiones" runat="server" heigth="680px">
                            <telerik:RadSplitter ID="RadSplitterDetalleRemisiones" runat="server" Height="700px" ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPanelDetalleRemisiones" runat="server" Height="700px" OnClientResized="onResize" BorderStyle="None">
                                <telerik:RadGrid ID="rgDetalleRemision" runat="server" GridLines="None" PageSize="15"
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." 
                                        AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" 
                                        CellSpacing="0">
                                    <MasterTableView ShowGroupFooter="true">                                        
                                        <RowIndicatorColumn Visible="false">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn >
                                        </ExpandCollapseColumn>
                                        <Columns>                                            
                                            <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Id_Prd" UniqueName="Id_Prd" Display="false" ReadOnly="true">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                           
                                            <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Prd_Descripcion" UniqueName="Prd_Descripcion" Display="false" ReadOnly="true">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>             
                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="# Territorio" UniqueName="Id_Ter" ReadOnly="true">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>   
                                            <telerik:GridBoundColumn DataField="Ter_Nombre" HeaderText="Territorio" UniqueName="Ter_Nombre" ReadOnly="true">
                                                <HeaderStyle HorizontalAlign="Center" Width="170px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                              
                                            <telerik:GridBoundColumn DataField="rem_fecha" HeaderText="Fecha" UniqueName="rem_fecha" DataFormatString="{0:dd/MM/yy}" ReadOnly="true">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                           
                                            <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Número Remisión" UniqueName="Id_Rem" ReadOnly="true">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Rem_Precio" HeaderText="Precio" Aggregate="Sum" UniqueName="Rem_Precio" FooterText="Total: $ " ReadOnly="true" DataFormatString="{0:C2}">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Rem_Cant" HeaderText="Cantidad" Aggregate="Sum" UniqueName="Rem_Cant" FooterText="Total: " ReadOnly="true">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                                                                                                             
                                            <telerik:GridTemplateColumn HeaderText="Facturar" DataField="Rem_CantE" UniqueName="Rem_CantE">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>                                                    
                                                    <telerik:RadNumericTextBox ID="NumCantRemisionDevuelta" runat="server" Width="50px" MaxLength="9" AutoPostBack="true"
                                                        MinValue="0" OnTextChanged="NumCantRemisionDevuelta_TextChanged" Text='<%# Eval("Rem_CantE") %>'> 
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent"/>
                                                    </telerik:RadNumericTextBox>                                                       
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <GroupByExpressions> 
                                                 <telerik:GridGroupByExpression> 
                                                 <SelectFields> 
                                                    <telerik:GridGroupByField FieldName="Id_Prd" HeaderText=" " HeaderValueSeparator=""></telerik:GridGroupByField>
                                                    <telerik:GridGroupByField FieldName="Prd_Descripcion" HeaderText=" " HeaderValueSeparator=""></telerik:GridGroupByField>
                                                 </SelectFields> 
                                                   <GroupByFields> 
                                                      <telerik:GridGroupByField  
                                                         FieldName="Id_Prd"/> 
                                                       </GroupByFields> 
                                                 </telerik:GridGroupByExpression> 
                                           </GroupByExpressions>                                                                            
                                    </MasterTableView>                                    
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                        ShowPagerText="True" PageButtonCount="3" />                                    
                                </telerik:RadGrid>                                
                                <asp:Panel ID="PnlEntradaRemision" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0" align="right">
                                        <tr>
                                            <td>
                                            <telerik:RadGrid ID="rgDetalleRemisionDevolucion" runat="server" GridLines="None" PageSize="15"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." AutoGenerateColumns="False"
                                                AllowPaging="false" HeaderStyle-HorizontalAlign="Center"                                   
                                                 Enabled="true">
                                                <MasterTableView>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Codigo" UniqueName="Id_Prd">
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>                                           
                                                        <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Nombre" UniqueName="Prd_Descripcion">
                                                            <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>                                           
                                                        <telerik:GridBoundColumn DataField="Prd_Ordenado" HeaderText="Cantidad" UniqueName="CantFact">
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>                                           
                                                        <telerik:GridBoundColumn DataField="Prd_Precio" HeaderText="Precio AAA" UniqueName="Prd_Precio">
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>                                           
                                                        <telerik:GridTemplateColumn HeaderText="Devolución" DataField="Monto" UniqueName="Rem_CantE">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblMonto" runat="server" Text='<%# (float.Parse(Eval("Prd_Precio").ToString())* Int32.Parse(Eval("Prd_Ordenado").ToString())).ToString("###,###,##0.00")  %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                    ShowPagerText="True" PageButtonCount="3" />
                                            </telerik:RadGrid>                                            
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table align="right" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width:70px">Subtotal</td>
                                                        <td style="width:70px"><asp:TextBox ID="TxtRemisionDevolucionSubtotal" runat="server" Width="70px" style="text-align:right"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>IVA</td>
                                                        <td><asp:TextBox ID="TxtRemisionDevolucionIVA" runat="server" Width="70px" style="text-align:right"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total</td>
                                                        <td><asp:TextBox ID="TxtRemisionDevolucionTotal" runat="server" Width="70px" style="text-align:right"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><asp:label ID="LblConfirmacion" runat="server" Text="¿Desea Guardar la Entrada?"></asp:label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BtnEntradaRemision" runat="server" Text="Entrada" OnClick="BtnEntradaRemision_Click" />
                                                <asp:Button ID="BtnCancelarRemision" runat="server" Text="Cancelar" OnClick="BtnCancelarRemision_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Button ID="BtnFacturarRemision" runat="server" Text="Facturar" OnClick="BtnFacturarRemision_Click" />
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalleDevoluciones" runat="server" heigth="370px">                           
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" BorderSize="0" Height="370px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" Width="101%">
                                <telerik:RadPane ID="RadPane2" runat="server" BorderStyle="None" Height="370px" OnClientResized="onResize">
                                        <table style="width:100%">
                                            <tr>
                                                <td>                                                
                                                    <telerik:RadGrid ID="grDetalleDevoluciones" runat="server" AutoGenerateColumns="False" PageSize="3"
                                                            GridLines="None" OnNeedDataSource="grDetalleDevoluciones_NeedDataSource"                                          
                                                            Width="50%" Height="350"  EnableLinqExpressions="False"
                                                            MasterTableView-NoMasterRecordsText="No se encontraron registros."  
                                                            GroupingSettings-RetainGroupFootersVisibility="true" ShowFooter="false" Enabled="true">
                                                            <MasterTableView ShowGroupFooter="true" >                                                                
                                                                <Columns>
                                                                    <telerik:GridBoundColumn HeaderText="# Remisión" UniqueName="Id_Rem" DataField="Id_Rem" Display="false">                                                                                                                              
                                                                        <HeaderStyle Width="90px" />
                                                                    </telerik:GridBoundColumn>                                                                    
                                                                    <telerik:GridBoundColumn HeaderText="# Territorio" UniqueName="Id_Ter" DataField="Id_Ter" SortExpression="Id_Ter">                                                                                                                              
                                                                        <HeaderStyle Width="90px" />
                                                                    </telerik:GridBoundColumn>                                                                                                                                                                                                      
                                                                    <telerik:GridBoundColumn HeaderText="# Producto" UniqueName="Id_Prd" DataField="Id_Prd">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                            
                                                                    </telerik:GridBoundColumn>  
                                                                    <telerik:GridBoundColumn HeaderText="Producto Nombre" UniqueName="Prd_Descripcion" DataField="Prd_Descripcion">
                                                                        <HeaderStyle Width="280px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                            
                                                                    </telerik:GridBoundColumn> 
                                                                     <telerik:GridBoundColumn HeaderText="Cantidad" UniqueName="Cant" DataField="Cant">
                                                                        <HeaderStyle Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Right" />                                                                                                                                  
                                                                    </telerik:GridBoundColumn>   
                                                                </Columns>      
                                                                <GroupByExpressions> 
                                                                     <telerik:GridGroupByExpression> 
                                                                     <SelectFields> 
                                                                        <telerik:GridGroupByField FieldName="Id_Rem" HeaderText="No. Remisión" HeaderValueSeparator=" - "></telerik:GridGroupByField>
                                                                     </SelectFields> 
                                                                       <GroupByFields> 
                                                                          <telerik:GridGroupByField  
                                                                             FieldName="Id_Rem"/> 
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
                        </telerik:RadMultiPage>
                   </td>
            </tr>
        </table>
        
                        
            <table  style="font-family: verdana; font-size: 8pt;width:100%">                                
                <tr style="display:none">
                <td style="display:none">
                        <asp:HiddenField ID="HF_Tipo" runat="server" />
                        <asp:HiddenField ID="HiddenHeight" runat="server" />
                        <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                         <asp:HiddenField ID="HiddenRebind" runat="server" />
                         <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
       <asp:HiddenField ID="HF_ClvPag" runat="server" />
       
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

            function ValidarCantidad(sender, args) {

                var rgGrid = $find("<%=rgDetalleRemision.ClientID %>");
                var cell = sender.get_element().parentNode.parentNode;
                var index = cell.parentNode.rowIndex;
                var MasterTable = rgGrid.get_masterTableView();
                var row = MasterTable.get_dataItems()[0];

                var cantidad = sender.get_value();
                //var disponible = MasterTable.getCellByColumnUniqueName(row, "Id_Rem").innerHTML;
                var disponible = row.get_cell('Id_Rem').innerText;

                alert(disponible);

            }

            function OnDescripcionNumLoad(sender, args) {
                DescPrd2 = sender;
            }
            function OnIdNumLoad(sender, args) {
                IdPrd2 = sender;

            }
            function cmbTerritorio_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
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

            function txtTipoId_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTipoMov.ClientID %>'));
            }
            function cmbTipoMov_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoId.ClientID %>'));
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


            function CloseWindowRem() {

                // GetRadWindow_FacturaRemisiones().BrowserWindow.ActivarBanderaRebind_FacturaRemisiones();
                //CloseAndRebind_FacturaRemisiones();

                /*
                //debugger;
                var oWnd = radopen("CapFactura.aspx?Id_Fac=" + Id_Fac
                + "&Id_Cd=" + Id_Cd
                + "&Id_Emp=" + Id_Emp
                + "&facModificable=1"
                + "&permisoGuardar=" + permisoGuardar
                + "&permisoModificar=" + permisoModificar
                + "&permisoEliminar=" + permisoEliminar
                + "&permisoImprimir=" + permisoImprimir
                + "&tipo=vi"
                , "AbrirVentana_FacturacionVi");
                oWnd.center();
                oWnd.Maximize();*/
            }


            function LimpiarBanderaRebind(sender, eventArgs) {
                ModificaBanderaRebind('0');
            }

            function ActivarBanderaRebind() {
                ModificaBanderaRebind('1');
            }

            function ModificaBanderaRebind(valor) {
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }


            //cuando el campo de texto pirde el foco


            function GetRadWindow_FacturaRemisiones() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow_FacturaRemisiones(mensaje) {
                //debugger;
                //                GetRadWindow_FacturaRemisiones().BrowserWindow.ActivarBanderaRebind_FacturaRemisiones();
                //                CloseAndRebind_FacturaPedido();

                var cerrarWindow = radalert(mensaje, 600, 10, tituloMensajes);
                cerrarWindow.add_close(
                                    function () {
                                        //debugger;
                                        //GetRadWindow().Close();
                                        CloseAndRebind_FacturaRemisiones();
                                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind_FacturaRemisiones() {
                //debugger;
                GetRadWindow_FacturaRemisiones().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage_FacturaRemisiones() {
                GetRadWindow_FacturaRemisiones().BrowserWindow.location.reload();
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

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }


            function ObtenerControlFecha() {
                //debugger;
                var txtFecha = $find('<%= dpFecha1.ClientID %>');
                return txtFecha._dateInput;
            }


            function AbrirVentana_Factura_Edicion(Id_Emp, Id_Cd, Id_Fac_Editar, facModificable) {

                AbrirVentana_Factura(Id_Emp, Id_Cd, Id_Fac_Editar, facModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de factura
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_Factura(Id_Emp, Id_Cd, Id_Fac, facModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                var oWnd = radopen("CapFactura.aspx?Id_Fac=" + Id_Fac
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&facModificable=" + facModificable
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    , "AbrirVentana_Factura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.maximize();
                oWnd.center();


            }

            function CerrarWindow_ClientEvent(sender, eventArgs) {
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                if (HD_GridRebind.value == '1') {
                    refreshGrid_Fac('RebindGridMain');
                }
                else {
                    refreshGrid_Fac('FacturacionVarialesSesionDestruir');
                }
            }

            function refreshGrid_Fac(accion) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(accion);
            }



            function OnClientTabSelected(sender, eventArgs) {
                //var tab = eventArgs.get_tab();
                //alert(tab.get_text());
            }
            
            

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
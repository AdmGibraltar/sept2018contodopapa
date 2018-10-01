<%@ Page Language="C#"AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPage02.master" CodeBehind="CapAjusteRemisiones.aspx.cs" Inherits="SIANWEB.CapAjusteRemisiones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
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
            <telerik:AjaxSetting AjaxControlID="btnDevolucion">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                               
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
        </AjaxSettings>
    </telerik:RadAjaxManager>
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
                <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnDevolucion" runat="server" Text="Aplicar Devolución" 
                                ToolTip="Aplicar Devolución" OnClick="BtnDevolucion_Click" />
                            </td>              
                        </tr>
                    </table> 
            </td>
        </tr>
            <tr>
                <td>  
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>                           
                            <telerik:RadTab runat="server" Text="Realizar D&lt;u&gt;e&lt;/u&gt;voluciones" AccessKey="D" PageViewID="RadPageViewDevoluciones" >
                            </telerik:RadTab>                            
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" heigth="770px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadSplitter ID="RadSplitter" runat="server" Height="570px"
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
                                        </tr>
                                     </table>  
                                   </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>                        
                        <telerik:RadPageView ID="RadPageViewDevoluciones" runat="server" heigth="1000px">
                         <telerik:RadSplitter ID="RadSplitterDevol" runat="server" Height="900px" ResizeMode="AdjacentPane"
                                 BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPaneDevol" runat="server" Height="900px" OnClientResized="onResize" BorderStyle="None">
                               <telerik:RadGrid ID="rgDevParcial" runat="server" GridLines="None"   AllowPaging="True" AllowSorting="true"
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." AutoGenerateColumns="False"
                                    HeaderStyle-HorizontalAlign="Center" OnNeedDataSource="rgDevParcial_NeedDataSource"                                   
                                     Enabled="true"   PageSize="550" AllowAutomaticUpdates="True" GroupingSettings-RetainGroupFootersVisibility="true" >
                                     <ClientSettings>
                                       <Scrolling AllowScroll="True" UseStaticHeaders="true" ScrollHeight="480px" />
                                   </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" >
                                        <Columns>        
                                        <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="# Remision." UniqueName="Id_Rem">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>          
                                            <telerik:GridBoundColumn DataField="Rem_Fecha" HeaderText="Fecha" UniqueName="Rem_Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                               
                                            <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Prd_Descripcion">
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Rem_Cant" HeaderText="Saldo Unidades" UniqueName="Rem_Cant" Aggregate="Sum" FooterText="Total: ">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>   
                                            <telerik:GridBoundColumn DataField="Rem_Precio" HeaderText="Saldo Pesos" UniqueName="Rem_Precio" >
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                            
                                            <telerik:GridTemplateColumn HeaderText="Cant.Dev." DataField="Rem_CantE" UniqueName="Rem_CantE">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="NumCantDevuelta" runat="server" Width="50px" MaxLength="9"
                                                        Text='<%#   Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Rem_Cant")) < 0 ? DataBinder.Eval(Container.DataItem, "Rem_Cant") : 0  %>' OnTextChanged="NumCantDevuelta_TextChanged" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                         <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="Producto" FieldName="Id_Prd"></telerik:GridGroupByField> 
                                                                      
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Id_Prd" SortOrder="Ascending"></telerik:GridGroupByField>
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

                GetRadWindow_FacturaRemisiones().BrowserWindow.ActivarBanderaRebind_FacturaRemisiones();
                CloseAndRebind_FacturaRemisiones();

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

           

           

          
            

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
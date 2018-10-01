<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatCampana.aspx.cs" Inherits="SIANWEB.CatCampana" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
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
            <telerik:AjaxSetting AjaxControlID="cmbUen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        />
                </UpdatedControls>
            </telerik:AjaxSetting>           
           
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                       />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="txtId_Prd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rgProductos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
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
                    <asp:Label ID="Label5" runat="server" Text="Centro de distribución"></asp:Label>
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
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Productos" PageViewID="RPProductos">
                            </telerik:RadTab>
                           
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="330px"
                        BorderStyle="Solid" BorderWidth="1px" Width="610px">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td width="70">
                                    </td>
                                    <td width="123">
                                        &nbsp;
                                    </td>
                                    <td width="75">
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
                                        <asp:Label ID="Label6" runat="server" Text="Clave"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
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
                                        <asp:Label ID="Label7" runat="server" Text="Descripción"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox onpaste="return false" ID="txtDescripcion" runat="server" Width="197px"
                                            MaxLength="50">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcion"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
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
                                        <asp:Label ID="Label15" runat="server" Text="Aplicación"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAplicacion" runat="server" Width="70px" >
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />                                          
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbAplicacion" runat="server"  OnClientSelectedIndexChanged="cmb5_ClientSelectedIndexChanged"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="false"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="false" HighlightTemplatedItems="true" 
                                            LoadingMessage="Cargando...">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="Label3" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
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
                                        <asp:Label ID="Label8" runat="server" Text="UEN"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtUen" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt1_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbUen" runat="server" OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." 
                                            AutoPostBack="True" onselectedindexchanged="cmbUen_SelectedIndexChanged">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbUen"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                            ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>                               
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Segmento"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtSegmento" runat="server" Width="70px" MinValue="1" MaxLength="9" ReadOnly = "True">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt2_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbSegmento" runat="server"  OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="false" OnClientLoad="cmbSegmento_OnLoad"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="false" HighlightTemplatedItems="true" Enabled="False"
                                            LoadingMessage="Cargando...">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
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
                                        <asp:Label ID="Label9" runat="server" Text="Area"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtArea" runat="server" Width="70px" MinValue="1" MaxLength="9" ReadOnly = "True">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt3_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbArea" runat="server" OnClientSelectedIndexChanged="cmb3_ClientSelectedIndexChanged" ReadOnly = "True"
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientLoad="cmbArea_OnLoad"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." Enabled="False" >
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="Label9" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
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
                                        <asp:Label ID="Label12" runat="server" Text="Solución"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtSolucion" runat="server" Width="70px" MinValue="1" MaxLength="9" ReadOnly = "True">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txt4_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="cmbSolucion" runat="server" OnClientSelectedIndexChanged="cmb4_ClientSelectedIndexChanged" 
                                            Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="False" MarkFirstMatch="false" OnClientLoad="cmbSolucion_OnLoad" 
                                            DataTextField="Descripcion" Enabled="False" DataValueField="Id" EnableLoadOnDemand="False" AllowCustomText="False"  HighlightTemplatedItems="true">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px; text-align: center">
                                                            <asp:Label ID="Label13" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="width: auto; text-align: left">
                                                            <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
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
                                    <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio"></asp:Label>
                                 </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFechaInicio" runat="server" Width="100px">
                                            <DatePopupButton ToolTip="Abrir calendario" />
                                            <Calendar ID="cal_txtFechaInicio" runat="server">
                                                <ClientEvents OnDateClick="Calendar_Click" />
                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                    TodayButtonCaption="Hoy" />
                                            </Calendar>
                                            <DateInput MaxLength="10" runat="server">
                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="val_txtFechaInicio" runat="server" ControlToValidate="txtFechaInicio"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                        </asp:RequiredFieldValidator>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                        &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LabelFechaFin" runat="server" Text="Fecha Fin"></asp:Label>
                                 </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFechaFin" runat="server" Width="100px" OnSelectedDateChanged="fin_SelectedDateChanged" AutoPostBack="true">
                                            <DatePopupButton ToolTip="Abrir calendario" />
                                            <Calendar ID="cal_txtFechaFin" runat="server">
                                                <ClientEvents OnDateClick="Calendar_Click" />
                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                    TodayButtonCaption="Hoy" />
                                            </Calendar>
                                            <DateInput MaxLength="10" runat="server">
                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFechaFIn"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                        </asp:RequiredFieldValidator>
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
                                        <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" AutoPostBack="True"
                                            OnCheckedChanged="chkActivo_CheckedChanged" />
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
                        <telerik:RadPageView ID="RPProductos" runat="server">  
                         <table>
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
                                </tr>
                             <tr>
                                 
                                  <td>
                                        &nbsp;
                                    </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Producto" />
                                </td>
                                <td>                            
                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1"  OnTextChanged="txtIdPrd_TextChanged"
                                        AutoPostBack="true">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />                                    
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td colspan="2" >
                                    <telerik:RadTextBox ID="txtProducto" runat="server" Width="200px" ReadOnly="True">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>                                
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Cuota"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadNumericTextBox  ID="txtCantidad" runat="server" Width="70px"
                                            MaxLength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>                                        
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgAgregarProducto" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                                            OnClick="imgAgregarProducto_Click" ToolTip="Agregar" ValidationGroup="AgregaProducto" />
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
                                </tr>
                                <tr> 
                                        <td width="10">
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadGrid ID="rgProductos" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgProductos_ItemCommand"
                                                OnNeedDataSource="rgProductos_NeedDataSource">
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="True" ScrollHeight="180px" UseStaticHeaders="true" />
                                                </ClientSettings>
                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="None"
                                                    TableLayout="Auto">
                                                    <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="SKU" UniqueName="Id_Prd" DataField="Id_Prd" Display="True">
                                                            <HeaderStyle Width="130px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Descripción" UniqueName="Prd_Descripcion"
                                                            DataField="Prd_Descripcion" Display="True">
                                                            <HeaderStyle Width="150" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Cuota" UniqueName="Prd_Cuota"
                                                            DataField="Prd_Cuota" Display="True">
                                                            <HeaderStyle Width="150" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="DeleteColumn"
                                                            HeaderText="Borrar" Text="Borrar">
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            </telerik:RadGrid>
                                        </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>                      
                    </telerik:RadMultiPage>
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
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="rg1_NeedDataSource" OnItemCommand="rg1_ItemCommand" OnPageIndexChanged="rg1_PageIndexChanged"
                                    PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cam" HeaderText="Clave" UniqueName="Id_Cam">
                                                <HeaderStyle Width="80" />
                                            </telerik:GridBoundColumn>                                            
                                            <telerik:GridBoundColumn DataField="Id_UEN" HeaderText="Id_UEN" UniqueName="Id_UEN"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Uen" HeaderText="Uen" UniqueName="Uen"
                                                Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Seg" HeaderText="Id_Segmento" UniqueName="Id_Seg"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="Segmento" HeaderText="Segmento" UniqueName="Segmento"
                                                Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Area" HeaderText="Id_Area" UniqueName="Id_Area"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Area" HeaderText="Area" UniqueName="Area"
                                                Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Sol" HeaderText="Id_Sol" UniqueName="Id_Sol"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Solucion" HeaderText="Solucion" UniqueName="Solucion"
                                                Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Aplicacion" HeaderText="Id_Aplicacion" UniqueName="Id_Aplicacion"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                           
                                            <telerik:GridBoundColumn DataField="Aplicacion" HeaderText="Aplicacion" UniqueName="Aplicacion"
                                                Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cam_Nombre" HeaderText="Descripción" UniqueName="Cam_Nombre">
                                                <HeaderStyle Width="200" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="Cam_FechaInicio" HeaderText="Cam_FechaInicio" UniqueName="Cam_FechaInicio"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="Cam_FechaFin" HeaderText="Cam_FechaFin" UniqueName="Cam_FechaFin"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cam_Activo" HeaderText="Cam_Activo" UniqueName="Cam_Activo"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="EstatusStr">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
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
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescripcion.ClientID %>'));
                LimpiarTextBox($find('<%= txtUen.ClientID %>'));
                LimpiarTextBox($find('<%= txtArea.ClientID %>'));
                LimpiarTextBox($find('<%= txtSegmento.ClientID %>'));
                LimpiarTextBox($find('<%= txtAplicacion.ClientID %>'));
                LimpiarTextBox($find('<%= txtSolucion.ClientID %>'));

                LimpiarComboSelectIndex0($find('<%= cmbUen.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbArea.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbSegmento.ClientID %>'));
                //LimpiarComboSelectIndex0($find('<%= cmbAplicacion.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbSolucion.ClientID %>'));

                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);
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
                        //debugger;

                        LimpiarControles();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                        hiddenActualiza.value = '';


                        var txtIdPrecio = $find('<%= txtClave.ClientID %>');
                        txtIdPrecio.enable();
                        txtIdPrecio.focus();
                        txtIdPrecio.set_value('<%= Valor %>');
                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbUen.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtUen.ClientID %>'));
            }

            function txt2_OnBlur(sender, args) {
              //  OnBlur(sender, $find('<%= cmbSegmento.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
               // ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtSegmento.ClientID %>'));
            }

            function txt3_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbArea.ClientID %>'));
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
               // ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtArea.ClientID %>'));
            }


            function txt4_OnBlur(sender, args) {
               // OnBlur(sender, $find('<%= cmbSolucion.ClientID %>'));
            }

            function cmb4_ClientSelectedIndexChanged(sender, eventArgs) {
                //ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtSolucion.ClientID %>'));
            }


            function txt5_OnBlur(sender, args) {
               // OnBlur(sender, $find('<%= cmbAplicacion.ClientID %>'));
            }

            function cmb5_ClientSelectedIndexChanged(sender, eventArgs) {
               ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtAplicacion.ClientID %>'));
            }


            function cmbSolucion_OnLoad(sender, args) {
                cmbSolucion = sender;
                var input = cmbSolucion.get_inputDomElement();
                input.onkeydown = onKeyDownHandler;
            }


            function cmbArea_OnLoad(sender, args) {
                cmbArea = sender;
                var input = cmbArea.get_inputDomElement();
                input.onkeydown = onKeyDownHandler;
            }


            function cmbSegmento_OnLoad(sender, args) {
                cmbSegmento = sender;
                var input = cmbSegmento.get_inputDomElement();
                input.onkeydown = onKeyDownHandler;
            }


            function onKeyDownHandler(e) {
                if (!e)
                    e = window.event;
                var code = e.keyCode || e.which;
                //do not allow any of these chars to be entered: !@#$%^&*()    
                if (code != 9) {
                    e.returnValue = false;
                    if (e.preventDefault) {
                        e.preventDefault();
                    }
                }
            } 




            function _PreValidarFechaEnPeriodo() {
               
            }

            function ObtenerControlFecha() {
                var txtFechaInicio = $find('<%= txtFechaInicio.ClientID %>');
                return txtFechaInicio._dateInput;
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

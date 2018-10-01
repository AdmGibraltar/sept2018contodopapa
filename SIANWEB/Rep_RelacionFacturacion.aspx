<%@ Page Title="Facturación por cliente" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_RelacionFacturacion.aspx.cs" Inherits="SIANWEB.Rep_RelacionFacturacion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Mostrar':                      
                        //Opcional, validaciones extras
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function refreshGrid() {
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
            }

            function cmbTer1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio1.ClientID %>'));
            }

            function txtTerritorio1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer1.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Mostrar" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                        width="99%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMensaje" runat="server" />
                            </td>
                            <td style="text-align: right" width="1000px">
                                <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                            </td>
                            <td width="150px" style="font-weight: bold">
                                <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                                    Width="150px" AutoPostBack="True">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Lbl1" runat="server" Text="Territorio inicial" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="0" Width="125px">
                                    <NumberFormat DecimalDigits="0" />
                                    <ClientEvents OnBlur="txtTerritorio_OnBlur"  OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>  
                            <td> 
                                &nbsp;
                            </td>                         
                            <td>
                                <asp:Label ID="Lbl2" runat="server" Text="Territorio final" />
                            </td>
                             <td>
                                <telerik:RadNumericTextBox ID="txtTerritorio1" runat="server" MaxLength="9" MinValue="0" Width="125px">
                                    <NumberFormat DecimalDigits="0"  />
                                    <ClientEvents OnBlur="txtTerritorio1_OnBlur"  OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
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
                                <telerik:RadComboBox ID="cmbTer" runat="server" AutoPostBack="True" Width="300px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                        EnableLoadOnDemand="True" Filter="Contains" 
                                                        OnClientSelectedIndexChanged="cmbTer_ClientSelectedIndexChanged">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 50px; text-align: center">
                                                                <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                            </td>
                                                            <td style="width: 200px; text-align: left">
                                                                <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>                                                                    
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>  
                            <td> 
                                &nbsp;
                            </td>                         
                            <td>
                                &nbsp;
                            </td>
                             <td>
                                <telerik:RadComboBox ID="cmbTer1" runat="server" AutoPostBack="True" Width="300px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                        EnableLoadOnDemand="True" Filter="Contains" 
                                                        OnClientSelectedIndexChanged="cmbTer1_ClientSelectedIndexChanged">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 50px; text-align: center">
                                                                <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                            </td>
                                                            <td style="width: 200px; text-align: left">
                                                                <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>                                                                    
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
                            <td style="width: 100px">
                                <asp:Label ID="LblFechaini" runat="server" Text="Fecha inicial" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechaini" runat="server" Width="155px">
                                    <Calendar ID="Calendar1" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtFechaini" ValidationGroup="Mostrar"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="LblFechafin" runat="server" Text="Fecha final" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechafin" runat="server" Width="155px">
                                    <Calendar ID="Calendar2" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput2" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtFechafin" ValidationGroup="Mostrar"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>                       
                        <tr>
                            <td>
                                <asp:Label ID="LblCliente" runat="server" Text="Cliente inicial" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente" runat="server" MaxLength="9" MinValue="0" Width="125px">
                                     <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>  
                            <td> 
                                &nbsp;
                            </td>                         
                            <td>
                                <asp:Label ID="LblCte2" runat="server" Text="Cliente final" />
                            </td>
                             <td>
                                <telerik:RadNumericTextBox ID="txtCliente2" runat="server" MaxLength="9" MinValue="0" Width="125px">
                                     <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>     
                        <tr>
                            <td>
                                <asp:Label ID="Lbl5" runat="server" Text="Factura inicial" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtFactura1" runat="server" MaxLength="9" MinValue="0" Width="125px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </td>  
                            <td> 
                                &nbsp;
                            </td>                         
                            <td>
                                <asp:Label ID="Lbl6" runat="server" Text="Factura final" />
                            </td>
                             <td>
                                <telerik:RadNumericTextBox ID="TxtFactura2" runat="server" MaxLength="9" MinValue="0" Width="125px">
                                    <NumberFormat DecimalDigits="0"  />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>  
                             &nbsp;                             
                            </td>
                        </tr>              
                          <tr>
                            <td>
                                <asp:Label ID="Lbl7" runat="server" Text="Estatus" />
                            </td>
                            <td colspan="4">
                                 <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="250px">
                                  <Items>
                                    <telerik:RadComboBoxItem Text="--Todos--" Value="0" Selected="true" />
                                    <telerik:RadComboBoxItem Text="Capturado" Value="C" />
                                    <telerik:RadComboBoxItem Text="Impreso" Value="I" />
                                    <telerik:RadComboBoxItem Text="Cancelado" Value="B" />                                    
                                    <telerik:RadComboBoxItem Text="Embarque" Value="E" />
                                    <telerik:RadComboBoxItem Text="Entregado" Value="N" />             
                                    <telerik:RadComboBoxItem Text="Entregado, embarcado o impreso" Value="E,N,I" />  
                                  </Items>
                                </telerik:RadComboBox>
                            </td>  
                            <td>  
                             &nbsp;                             
                            </td>
                        </tr>            
                         <tr>
                            <td>
                                <asp:Label ID="Lbl8" runat="server" Text="Filtrar por" />
                            </td>
                            <td>
                                 <asp:RadioButtonList ID="rbList" runat="server">
                                    <asp:ListItem Text="Factura" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Territorio" Value="2"></asp:ListItem>
                                 </asp:RadioButtonList>
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
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

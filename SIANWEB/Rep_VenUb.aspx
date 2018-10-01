<%@ Page Title="Utilidad bruta" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_VenUb.aspx.cs" Inherits="SIANWEB.Rep_VenUb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;

                var conntinuar = true;

                var HD_CalculadoCon = document.getElementById('<%= HD_CalculadoCon.ClientID %>');
                var lbl_txtUtilidadMinima = document.getElementById('<%= lbl_txtUtilidadMinima.ClientID %>');
                var txtUtilidadMinima = $find('<%= txtUtilidadMinima.ClientID %>');

                if (HD_CalculadoCon.value == '2') {
                    //validar rango correcto de fechas.

                    lbl_txtUtilidadMinima.innerHTML = '';
                    if (txtUtilidadMinima != null) {
                        if (txtUtilidadMinima.get_textBoxValue() == '') {
                            lbl_txtUtilidadMinima.innerHTML = '*Requerido';
                            conntinuar = false
                        }
                    }
                }

                return conntinuar;
            }

            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }


            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
            }
        </script>
    </telerik:radcodeblock>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
        
    </telerik:radajaxloadingpanel>
    <telerik:radajaxmanager id="RAM1" runat="server" 
        onajaxrequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="filtros" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="filtros" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbAgruparPor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="filtros" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cmbTer" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbCalculadoCon">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="filtros" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNumeroCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTer" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                                       
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <div>
        <telerik:radtoolbar id="RadToolBar1" runat="server" width="100%" dir="rtl" onclientbuttonclicking="ToolBar_ClientClick"
            onbuttonclick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" ValidationGroup="print" CssClass="print" ImageUrl="~/Imagenes/blank.png" />
                 <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:radtoolbar>
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label8" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="cmbCentrosDist_SelectedIndexChanged"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <div id="filtros" runat="server">
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td width="90">
                                    <asp:Label runat="server" ID="lbl_rbAgruparPor" Text="Agrupar por"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:RadioButtonList ID="rbAgruparPor" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rbAgruparPor_SelectedIndexChanged">
                                        <asp:ListItem Text="Representante" Value="5" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Territorio" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Segmento" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Cliente" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Producto" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>                                
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblCliente" Text="No. Cliente"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" Width="150px" MinValue="1" MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                   <telerik:RadTextBox ID="txtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false" Enabled="false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblTerritorio" Text="Territorio"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radtextbox id="txtTerritorio" onpaste="return false" runat="server" width="150px"
                                        maxlength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="txtTerritorio_OnBlur" />
                                    </telerik:radtextbox>                                    
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
                            </tr>                            
                        </table>
                        <table>
                            <tr>
                                <td width="90">
                                    <asp:Label runat="server" ID="Label3" Text="Año inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radcombobox id="cmbAnioInicio" runat="server" width="150px">
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbAnioInicio" runat="server" ControlToValidate="cmbAnioInicio"
                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label4" Text="Mes inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radcombobox id="cmbMesInicio" runat="server" width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                            <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                            <telerik:RadComboBoxItem Text="Marzo" Value="3" />
                                            <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                            <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                            <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                            <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                            <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                            <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                            <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                            <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                            <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                        </Items>
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbMesInicio" runat="server" ControlToValidate="cmbMesInicio"
                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label5" Text="Año final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radcombobox id="cmbAnioFin" runat="server" width="150px">
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbAnioFin" runat="server" ControlToValidate="cmbAnioFin"
                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label6" Text="Mes final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:radcombobox id="cmbMesFin" runat="server" width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                            <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                            <telerik:RadComboBoxItem Text="Marzo" Value="3" />
                                            <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                            <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                            <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                            <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                            <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                            <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                            <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                            <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                            <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                        </Items>
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_cmbMesFin" runat="server" ControlToValidate="cmbMesFin"
                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td width="90">
                                    <asp:Label runat="server" ID="Label7" Text="Calculado con"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbCalculadoCon" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbCalculadoCon_SelectedIndexChanged">
                                        <asp:ListItem Text="Precio de lista" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Utilidad bruta mínima" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:HiddenField ID="HD_CalculadoCon" runat="server" Value="1" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td valign="middle">
                                    <asp:Label runat="server" ID="lblUtilidadBruta" Text="Utilidad bruta mínima"></asp:Label>
                                </td>
                                <td>
                                    <div style="margin-top: 15px">
                                        <telerik:radnumerictextbox id="txtUtilidadMinima" runat="server" width="70px" minvalue="0"
                                            value="45" maxlength="9">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:radnumerictextbox>
                                    </div>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbl_txtUtilidadMinima" ForeColor="#FF0000"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
    </div>
</asp:Content>

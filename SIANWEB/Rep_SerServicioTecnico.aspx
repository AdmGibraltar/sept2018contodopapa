<%@ Page Title="Cargo por servicio técnico" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_SerServicioTecnico.aspx.cs" Inherits="SIANWEB.Rep_SerServicioTecnico" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function refreshGrid() {

            }
            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
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
            <telerik:AjaxSetting AjaxControlID="txtNumeroCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTer" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtRepresentante" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRepresentante">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="imprimir" />
            <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="imprimir" />
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
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <div id="divPrincipal" runat="server">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="10">
                                &nbsp;
                            </td>
                            <td width="80">
                            </td>
                            <td width="100">
                                &nbsp;
                            </td>
                            <td width="100">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" Width="125px" MinValue="1" MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
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
                            <td colspan="2">
                                <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtTerritorio" runat="server" MaxLength="256">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="txtTerritorio_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTer" runat="server" AutoPostBack="True" Width="300px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            EnableLoadOnDemand="True" Filter="Contains" 
                                                            OnClientSelectedIndexChanged="cmbTer_ClientSelectedIndexChanged"
                                                            OnSelectedIndexChanged="cmbTer_SelectedIndexChanged">
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
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblRepresentante" runat="server" Text="Representante"></asp:Label>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:radtextbox id="txtRepresentante" runat="server" AutoPostBack="true" width="125px" maxlength="5"
                                        onpaste="return false" OnTextChanged="txtRep_TextChanged">
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radtextbox>
                                <asp:HiddenField runat="server" id="txtRepOld"/>                                                                            
                            </td>
                            <td>
                                <telerik:radtextbox id="txtRepresentanteStr" runat="server" width="300px" maxlength="5"
                                            onpaste="return false" enabled="false">
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radtextbox>
                            </td>
                        </tr>                                                
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblEquipo" runat="server" Text="Equipo"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtEquipo" runat="server" MaxLength="256">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMes" runat="server" Text="Mes"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbMes" runat="server" Width="130px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-- Seleccionar --" />
                                        <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" />
                                        <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" />
                                        <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" />
                                        <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" />
                                        <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" />
                                        <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" />
                                        <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" />
                                        <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" />
                                        <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" />
                                        <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" />
                                        <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbMes"
                                    ErrorMessage="* Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="imprimir"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblAño" runat="server" Text="Año"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbAño" runat="server" Width="130px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbAño"
                                    ErrorMessage="* Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="imprimir"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
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
                            <td colspan="2">
                                <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
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
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblTipo" runat="server">
                                    <asp:ListItem Selected="True" Value="1">General</asp:ListItem>
                                    <asp:ListItem Value="2">Detallado por cliente</asp:ListItem>
                                    <asp:ListItem Value="3">Detallado por equipo</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
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
                            <td colspan="2">
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

<%@ Page  Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProAsignPedidoAutomaticaTerr.aspx.cs" Inherits="SIANWEB.ProAsignPedidoAutomaticaTerr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbCredito">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
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
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
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
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Clientes con crédito"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbCredito" runat="server" Width="100px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbCredito_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="-- Todo --" Value="-- Todo --" />
                                        <telerik:RadComboBoxItem runat="server" Text="Si" Value="Si" />
                                        <telerik:RadComboBoxItem runat="server" Text="No" Value="No" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtClienteID" runat="server" MinValue="1" Width="70px"
                                    AutoPostBack="true" OnTextChanged="txtClave_TextChanged">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbCliente" runat="server" 
                                    Width="350px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" 
                                    MaxHeight="250px" EnableAutomaticLoadOnDemand="True" EnableVirtualScrolling="True"
                                    ItemsPerRequest="10" ShowMoreResultsBox="True" 
                                    OnClientDropDownOpening="Client_Focus" 
                                    onclientselectedindexchanged="cmb1_ClientSelectedIndexChanged">
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClienteID"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" Width="70px" MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTer" runat="server" AutoPostBack="True" Width="350px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
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
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTerritorio"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                <asp:ImageButton ID="imgAceptar" runat="server" CssClass="aceptar" ImageUrl="~/Imagenes/blank.png"
                                    OnClick="imgAceptar_Click" ToolTip="Aceptar" ValidationGroup="guardar" />
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
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbCliente.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtClienteID.ClientID %>'));
            }

            function Client_Focus() {
                //debugger;
                var combo = $find("<%= cmbCliente.ClientID %>");
                combo.clearSelection();
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

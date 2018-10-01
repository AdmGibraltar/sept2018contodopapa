<%@ Page Title="Generación de pólizas ContPaq" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProGenPolizaCompaq.aspx.cs" Inherits="SIANWEB.ProGenPolizaCompaq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadAjaxManager ID="RAM1" runat="server">
          <AjaxSettings>             
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div id="divPrincipal" runat="server">
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                            Width="150px" AutoPostBack="True" ValidationGroup="Centros" OnClientSelectedIndexChanged="CmbCentro_OnClientSelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table style="font-family: Verdana; font-size: 8pt">
                            <tr>
                                <td colspan="6">
                                    <asp:Label ID="LblRangos" runat="server" Text="Rangos" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="center" style="width: 50px">                             
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    <asp:Label ID="LblTipo" runat="server" Text="Tipo"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbTipo" runat="server" Width="100px" Enabled="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Ventas" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
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
                                    <asp:Label ID="LblAnhio" runat="server" Text="Año"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbAnhio" runat="server" Width="120px" Filter="Contains"
                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                        LoadingMessage="Cargando..." ValidationGroup="Confirmar"
                                        OnClientSelectedIndexChanged="cmbAnhio_ClientSelectedIndexChanged">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="cmbAnhio" ValidationGroup="Confirmar">
                                     </asp:RequiredFieldValidator>&nbsp;
                                    <asp:Label ID="LblMes" runat="server" Text="Mes"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbMes" runat="server" Width="120px">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="cmbMes" ValidationGroup="Confirmar"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <table style="font-family: Verdana; font-size: 8pt">
                            <tr>
                                <td width="100">
                                    <asp:Label ID="LblCuenta" runat="server" Text="Cuenta cabecera"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox onpaste="return false" ID="txtCuenta1" runat="server" Width="70px"
                                        MaxLength="50">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtCuenta1" ValidationGroup="Confirmar"></asp:RequiredFieldValidator>
                                    <telerik:RadTextBox onpaste="return false" ID="txtCuenta2" runat="server" Width="70px"
                                        MaxLength="50">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtCuenta2" ValidationGroup="Confirmar"></asp:RequiredFieldValidator>
                                    <telerik:RadTextBox onpaste="return false" ID="txtCuenta3" runat="server" Width="70px"
                                        MaxLength="50">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtCuenta3" ValidationGroup="Confirmar"></asp:RequiredFieldValidator>
                                </td>
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgAceptar" runat="server" CssClass="aceptar" ImageUrl="~/Imagenes/blank.png"
                                        OnClick="imgAceptar_Click" ToolTip="Aceptar" ValidationGroup="Confirmar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function cmbAnhio_ClientSelectedIndexChanged(sender, eventArgs) {
            }
            function CmbCentro_OnClientSelectedIndexChanged(sender, eventArgs) {
                LimpiarControles();
            }
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtCuenta1.ClientID %>'));
                LimpiarTextBox($find('<%= txtCuenta2.ClientID %>'));
                LimpiarTextBox($find('<%= txtCuenta3.ClientID %>'));

                LimpiarComboSelectIndex0($find('<%= cmbAnhio.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbMes.ClientID %>'));                
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

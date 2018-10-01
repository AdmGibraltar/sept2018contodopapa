<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="Rep_VenCfd.aspx.cs" Inherits="SIANWEB.Rep_VenCfd" %>

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
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblMes" runat="server" Text="Mes"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbMes" runat="server" Width="120px">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                        ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="cmbMes" ValidationGroup="Confirmar"></asp:RequiredFieldValidator>
                                    &nbsp;<asp:Label ID="LblAnhio" runat="server" Text="Año"></asp:Label>
                                &nbsp;</td>
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
                                     </asp:RequiredFieldValidator>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgAceptar" runat="server" CssClass="aceptar" ImageUrl="~/Imagenes/blank.png"
                                        OnClick="imgAceptar_Click" ToolTip="Aceptar" ValidationGroup="Confirmar" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LblMes0" runat="server" Text="Mostrar:"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:RadioButton ID="rbTodos" runat="server" Checked="True" GroupName="Orden" 
                                        Text="Todos" />
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:RadioButton ID="rbActivo" runat="server" GroupName="Orden" 
                                        Text="Activos" />
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:RadioButton ID="rbCancelados" runat="server" GroupName="orden" 
                                        Text="Cancelados" />
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
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
                
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

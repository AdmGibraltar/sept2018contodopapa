<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapPedidos_BajaParcial.aspx.cs" Inherits="SIANWEB.CapPedidos_BajaParcial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
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
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgBuscar">
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
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
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
                                <asp:Label ID="lblId" runat="server" Text="Pedido"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtId" runat="server" MaxLength="9" MinValue="0" Width="50px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnValueChanged="txtId_OnBlur" OnKeyPress="handleClickEventBaja" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtId"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                    ToolTip="Buscar" Visible="False" ValidationGroup="guardar" />
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIdCte" runat="server" Enabled="False" MaxLength="9"
                                    MinValue="0" Width="50px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNCte" runat="server" Enabled="False" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIdTer" runat="server" Enabled="False" MaxLength="9"
                                    MinValue="0" Width="50px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNTer" runat="server" Enabled="False" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRik" runat="server" Text="Representante"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIdRik" runat="server" Enabled="False" MaxLength="9"
                                    MinValue="0" Width="50px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNRik" runat="server" Enabled="False" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgPedido" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    OnNeedDataSource="RadGrid1_NeedDataSource">
                                    <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Núm. Terr." UniqueName="Id_Ter" DataField="Id_Ter">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="60px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Núm. Prd." UniqueName="Id_Prd" DataField="Id_Prd">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="60px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Descripcion" DataField="Prd_Desc">
                                                <HeaderStyle Width="300px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Cantidad original" UniqueName="Cant_original"
                                                DataField="Original">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Cantidad pendiente" UniqueName="Cant_pendiente"
                                                DataField="Pendiente">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Cantidad cancelado" UniqueName="Cant_cancelado"
                                                DataField="Cancelado">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" Text='<%# Bind("Cancelado") %>'
                                                        Width="80px" MinValue="0" MaxLength="9">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="Asig_OnBlur" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="80px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderText="Cantidad pendiente final" UniqueName="Cant_final"
                                                DataField="Final">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                        NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                        PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                        PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">

            var alertaenviada = false;

            function Asig_OnBlur(sender, args) {
                var rdgrid = $find("<%=rgPedido.ClientID %>");
                var cell = sender.get_element().parentNode.parentNode;
                var row = (rdgrid.get_masterTableView()).get_dataItems()[(cell.parentNode.rowIndex - 1)]; //getting row

                var Cancelado = sender.get_value();
                var Pendiente = row.get_cell('Cant_Pendiente').innerText;

                if (Cancelado > Pendiente) {
                    //if (!alertaenviada) {
                    sender.set_value(Pendiente);
                    //alertaenviada = true;
                    Cancelado = Pendiente;
                    radalert('Cantidad cancelada no puede ser mayor a la pendiente', 330, 150);
                    //}
                }
                else {
                    alertaenviada = false;
                }

                row.get_cell('Cant_final').innerHTML = Pendiente - Cancelado;

            }
            function txtId_OnBlur() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest();
            }

            function handleClickEventBaja(sender, eventArgs) {
                var key = eventArgs.get_keyCode();
                if (key && key == 13) {
                    eventArgs.set_cancel(true);
                    txtId_OnBlur()
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

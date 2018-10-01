<%@ Page Title="Solicitud de autorización de compras locales" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProCompraLocal.aspx.cs" Inherits="SIANWEB.ProSolCompraLocal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
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
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbSolicitud">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtb1" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCompraLocal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCompraLocal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal" style="font-family: Verdana; font-size: 8pt">
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
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Solicitud" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbSolicitud" runat="server" Width="200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbSolicitud_SelectedIndexChanged" 
                                    MaxHeight="300px">
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
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table id="tbProducto" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Producto" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtProducto" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1" ontextchanged="txtProducto_TextChanged" AutoPostBack="true">
                                    <ClientEvents OnBlur="txt_OnBlur" />
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_Prd" runat="server" Width="250px" ReadOnly="true"></telerik:RadTextBox>                               
                                <%--<telerik:RadComboBox ID="cmbProducto" runat="server" Width="250px" MarkFirstMatch="true"
                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnClientSelectedIndexChanged="cmb_ClientSelectedIndexChanged">
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 50px; text-align: center">
                                                    <asp:Label ID="Label1" runat="server" Text="Clave" />
                                                </td>
                                                <td style="width: 200px; text-align: center">
                                                    <asp:Label ID="Label2" runat="server" Text="Nombre" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 50px; text-align: center">
                                                    <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                </td>
                                                <td style="width: 200px; text-align: left">
                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>--%>
                            </td>
                            <td>
                                &nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProducto"
                                    ErrorMessage="*Requerido" ForeColor="Red" Display="Dynamic"
                                    ValidationGroup="Agregar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Costo" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCosto" runat="server" Width="70px" Value="0" MaxLength="9"
                                    MinValue="0">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCosto"
                                    ErrorMessage="*Requerido" ForeColor="Red" Display="Dynamic" ValidationGroup="Agregar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                               <asp:ImageButton ID="Button1" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                                    OnClick="Button1_Click" ToolTip="Agregar" ValidationGroup="Agregar" />
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
                    <telerik:RadGrid ID="rgCompraLocal" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="rgCompraLocal_NeedDataSource" PageSize="15" AllowPaging="True"
                        MasterTableView-NoMasterRecordsText="No se encontraron registros." OnPageIndexChanged="rgCompraLocal_PageIndexChanged"
                        OnItemCommand="rgCompraLocal_ItemCommand">
                        <MasterTableView>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Num" HeaderText="Clave" UniqueName="column">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcve" runat="server" Text='<%# Bind("Num") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="column1">
                                    <HeaderStyle Width="300px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Costo" HeaderText="Costo" UniqueName="column2">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="column3"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="column3">
                                    <HeaderStyle Width="80px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                    ConfirmDialogHeight="130px" ConfirmText="¿Está seguro de eliminar el registro?"
                                    Text="Eliminar" UniqueName="DeleteColumn">
                                    <HeaderStyle Width="29px" />
                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="29px" VerticalAlign="Top" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function txt_OnBlur(sender, args) {

            //    OnBlur(sender, $find('<%= txt_Prd.ClientID %>'));
            }

            function cmb_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProducto.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

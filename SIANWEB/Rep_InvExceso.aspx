<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_InvExceso.aspx.cs" Inherits="SIANWEB.Rep_InvExceso" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function popup(id_u, indicador, proveedor, sucursal, dias, tipoProd) {

                var window_dimensions = "toolbar=no,menubar=no,directories=no,location=no,scrollbars=1,resizable=1,status=no,width=900,height=400"
                window.open("Rep_InvExceso1.aspx?Id_U=" + id_u
                + "&Indicador=" + indicador
                + "&Proveedor=" + proveedor
                + "&Centro=" + sucursal
                + "&Dias=" + dias
                + "&Tproducto=" + tipoProd
                , "_blank", window_dimensions);
            }           
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana" runat="server" Behaviors="Move, Close" Opacity="100"
                VisibleStatusbar="False" Width="800px" Height="600px" Animation="Fade" KeepInScreenBounds="True"
                Overlay="True" Title="" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
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
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="97%" CommandName="Envio" Value="Envio" CssClass="print"
                    ToolTip="Envio" ImageUrl="Imagenes/blank.png" ValidationGroup="grupo" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="1000px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td colspan="2">
                    <asp:Label ID="LblIndicador" runat="server" Text="Indicador" />
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbIndicador" runat="server" Width="200px" Filter="Contains"
                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                        LoadingMessage="Cargando..." MaxHeight="250px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LblProveedor" runat="server" Text="Proveedor" />
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbProveedor" runat="server" Width="300px" Filter="Contains"
                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                        LoadingMessage="Cargando..." MaxHeight="250px">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                        <asp:Label ID="LabelID" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LblSucursal" runat="server" Text="Centro" />
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbSucursal" runat="server" Width="200px" Filter="Contains"
                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                        LoadingMessage="Cargando..." MaxHeight="250px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LblDias" runat="server" Text="Días" />
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbDias" runat="server" Width="200px" Filter="Contains"
                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                        LoadingMessage="Cargando..." MaxHeight="250px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LblTipoProducto" runat="server" Text="Tipo de Producto" />
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbTipoProducto" runat="server" Width="200px" Filter="Contains"
                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                        LoadingMessage="Cargando..." MaxHeight="250px">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                        <asp:Label ID="LabelID" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

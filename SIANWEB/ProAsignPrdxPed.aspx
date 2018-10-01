<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="ProAsignPrdxPed.aspx.cs" Inherits="SIANWEB.ProAsignPrdxPed" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
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
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    &nbsp;
                </td>
                <td width="150px">
                    &nbsp;
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
                                <asp:Label ID="Label4" runat="server" Text="Pedido"></asp:Label>
                            </td>
                            <td colspan="5">
                                <telerik:RadNumericTextBox ID="txtPedido" runat="server" Width="70px" MinValue="1"
                                    ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MinValue="1"
                                    ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <telerik:RadTextBox onpaste="return false" ID="txtClienteNombre" runat="server" Width="250px"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Producto" />
                            </td>
                            <td colspan="6">
                                <telerik:RadTextBox onpaste="return false" ID="txtProducto" runat="server" Width="328px"
                                    MaxLength="150">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Producto inicial" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtProducto1" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Producto final" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtProducto2" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td width="10">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td width="100">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgPedido" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="RadGrid1_NeedDataSource" EnableLinqExpressions="false" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView>
                            <Columns>
                             <telerik:GridBoundColumn DataField="Id_PedDet" HeaderText="Id_PedDet" UniqueName="Id_PedDet" Display="false">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Terr." UniqueName="Terr">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Prod." UniqueName="Id_Prd">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Desc" HeaderText="Descripción" UniqueName="Prd_Desc">
                                    <HeaderStyle Width="290px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Ord" HeaderText="Ord." UniqueName="Prd_Ord">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_OrdDisp" HeaderText="Disp. ord." UniqueName="Prd_OrdDisp">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Asig" HeaderText="Prd_Asig" UniqueName="Prd_AsigOld"
                                    Display="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="Prd_Asig" HeaderText="Asig." UniqueName="Prd_Asig">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtAsig" runat="server" Width="70px" MinValue="0"
                                            MaxLength="9" DbValue='<%# Bind("Prd_Asig") %>'>
                                            <NumberFormat DecimalDigits="0" AllowRounding="false" />
                                            <EnabledStyle HorizontalAlign="Right" />
                                            <ClientEvents OnBlur="Asig_OnBlur" />
                                        </telerik:RadNumericTextBox>
                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                            <script type="text/javascript">

                                                function Asig_OnBlur(sender, args) {
                                                    debugger;
                                                    var rdgrid = $find("<%=rgPedido.ClientID %>");
                                                    var cell = sender.get_element().parentNode.parentNode;
                                                    var index = cell.parentNode.rowIndex;
                                                    var MasterTable = rdgrid.get_masterTableView();
                                                    var row = MasterTable.get_dataItems()[index]; //getting row

                                                    var Asignado = sender.get_value();
                                                    var DispOrd = row.get_cell('Prd_OrdDisp').innerText;
                                                    var Exist = row.get_cell('Prd_Existencia').innerText;
                                                    var Old = row.get_cell('Prd_AsigOld').innerText;
                                                    var Disponible = row.get_cell('Prd_Disponible').innerText;

                                                    var HF = document.getElementById('<%= HF_Guardar.ClientID %>');
                                                    var HFRB = document.getElementById('<%= HiddenRebind.ClientID %>');
                                                    if (sender.get_value() > DispOrd) {
                                                        sender.set_value(Old);
                                                        radalert('Cantidad asignada es mayor que la disponible ordenada', 330, 150);
                                                        HF.value = 'false';
                                                    }
                                                    else if (sender.get_value() - Old > Disponible) {
                                                        sender.set_value(Old);
                                                        radalert('No se cuenta con el inventario suficiente', 330, 150);
                                                        HF.value = 'false';
                                                    }
                                                    else {
                                                        HF.value = 'true';
                                                        //row.get_cell('Prd_Faltante').innerHTML = DispOrd - Asignado;
                                                    }
                                                    if (Asignado == '') {
                                                        sender.set_value('0');
                                                    }
                                                    HFRB.value = 'true';
                                                }
                                            </script>
                                        </telerik:RadCodeBlock>
                                    </ItemTemplate>
                                    <HeaderStyle Width="90px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Prd_Faltante" HeaderText="Faltante" UniqueName="Prd_Faltante">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Existencia" HeaderText="Inventario" UniqueName="Prd_Existencia"
                                    Display="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Disponible" HeaderText="Disponible" UniqueName="Prd_Disponible">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="true" ScrollHeight="230" UseStaticHeaders="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HiddenRebind" runat="server" Value="false" />
    <asp:HiddenField ID="HF_Ped" runat="server" />
    <asp:HiddenField ID="HF_Guardar" runat="server" Value="true" />
     <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }
           </script>
    </telerik:radcodeblock>
</asp:Content>

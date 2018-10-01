<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="ProAsignPedxPrd.aspx.cs" Inherits="SIANWEB.ProAsignPedxPrd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"  />
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
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
    </telerik:radtoolbar>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    &nbsp; &nbsp;
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
                                <asp:Label ID="Label5" runat="server" Text="Producto"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:radnumerictextbox id="txtProducto" runat="server" width="70px" minvalue="1"
                                    readonly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                                <telerik:radtextbox onpaste="return false" id="txtProductoNombre" runat="server"
                                    width="250px" readonly="true">
                                </telerik:radtextbox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Inventario" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtInventario" runat="server" width="70px" minvalue="0"
                                    maxlength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Asignado" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtAsignado" runat="server" width="70px" minvalue="0"
                                    maxlength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
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
                    <telerik:radgrid id="rgPedido" runat="server" autogeneratecolumns="False" gridlines="None"
                        onneeddatasource="RadGrid1_NeedDataSource" enablelinqexpressions="false" mastertableview-nomasterrecordstext="No se encontraron registros.">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Pedido" UniqueName="Id_Ped">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_PedDet" HeaderText="Pedido Detalle" UniqueName="Id_PedDet" Display="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Fecha" HeaderText="Fecha" UniqueName="Ped_Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Terr." UniqueName="Id_Ter">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="40px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="235px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte_CreditoStr" HeaderText="Crédito" UniqueName="Cte_CreditoStr">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="55px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Ord" HeaderText="Ord." UniqueName="Ped_Ord">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Disp" HeaderText="Disp." UniqueName="Ped_Disp">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Asignar" HeaderText="Old." UniqueName="Ped_AsignarOld"
                                    Display="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="Ped_Asignar" HeaderText="Asig." UniqueName="Ped_Asignar">
                                    <HeaderStyle Width="70" />
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtAsig" runat="server" Width="50px" DbValue='<%# Bind("Ped_Asignar") %>'
                                            MinValue="0" MaxLength="9">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <EnabledStyle HorizontalAlign="Right" />
                                            <ClientEvents OnBlur="Asig_OnBlur" OnFocus="Asig_Focus" OnValueChanged="Asig_Changed"
                                                OnLoad="Asig_Load" />
                                        </telerik:RadNumericTextBox>
                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                            <script type="text/javascript">
                                                var evaluado = 0;
                                                var alertaenviada = false;
                                                function Asig_Load(sender) {

                                                    //evaluado = evaluado + sender.get_value();
                                                }
                                                function Asig_Changed(sender) {
                                                    alertaenviada = false;
                                                }
                                                function Asig_Focus(sender) {

                                                    var rdgrid = $find("<%=rgPedido.ClientID %>");
                                                    var cell = sender.get_element().parentNode.parentNode;
                                                    var row = (rdgrid.get_masterTableView()).get_dataItems()[(cell.parentNode.rowIndex)]; //getting row
                                                    var Old = row.get_cell('Ped_AsignarOld').innerText;
                                                    evaluado = evaluado - (sender.get_value() - Old);
                                                }
                                                function Asig_OnBlur(sender, args) {
                                                    var rdgrid = $find("<%=rgPedido.ClientID %>");
                                                    var cell = sender.get_element().parentNode.parentNode;
                                                    var row = (rdgrid.get_masterTableView()).get_dataItems()[(cell.parentNode.rowIndex)]; //getting row

                                                    var Asignado = sender.get_value();
                                                    var DispOrd = row.get_cell('Ped_Disp').innerText;
                                                    var Old = row.get_cell('Ped_AsignarOld').innerText;
                                                    var Disponible = row.get_cell('Prd_Disp').innerText;



                                                    var HF = document.getElementById('<%= HF_Guardar.ClientID %>');
                                                    var HFRB = document.getElementById('<%= HiddenRebind.ClientID %>');

                                                    if (Asignado > DispOrd) {
                                                        if (!alertaenviada) {
                                                            sender.set_value(Old);
                                                            Asignado = Old;
                                                            radalert('Cantidad asignada es mayor que la ordenada', 330, 150);
                                                        }
                                                        HF.value = 'false';
                                                    }
                                                    else if (Asignado - Old + evaluado > Disponible) {

                                                        if (!alertaenviada) {
                                                            sender.set_value(Old);
                                                            Asignado = Old;
                                                            alertaenviada = true;
                                                            radalert('No se cuenta con el inventario suficiente', 330, 150);
                                                        }
                                                        HF.value = 'false';
                                                    }
                                                    else {
                                                        HF.value = 'true';

                                                    }

                                                    row.get_cell('Ped_Faltante').innerHTML = DispOrd - Asignado;
                                                    evaluado = Asignado - Old + evaluado;
                                                    HFRB.value = 'true';
                                                }
                                            </script>
                                        </telerik:RadCodeBlock>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Ped_Faltante" HeaderText="Faltante" UniqueName="Ped_Faltante">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="60px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_InvFinal" HeaderText="Inventario" UniqueName="Prd_InvFinal"
                                    Display="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Disp" HeaderText="Disponible" UniqueName="Prd_Disp"
                                    Display="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="true" ScrollHeight="300" UseStaticHeaders="true" />
                        </ClientSettings>
                    </telerik:radgrid>
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

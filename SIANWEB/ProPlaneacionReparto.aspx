<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true" CodeBehind="ProPlaneacionReparto.aspx.cs" Inherits="SIANWEB.ProPlaneacionReparto" Title="Planeación de Reparto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbBuscar">
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

<%--    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
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
--%>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
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
                            <td>
                                <telerik:RadNumericTextBox ID="txtPedido" runat="server" Width="70px" MinValue="1"
                                    ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:Label ID="LableFecha" runat="server" Text="Fecha"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtFecha" runat="server" Width="60px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td><asp:Label ID="LblSector" runat="server" Text="Sector"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtSector" runat="server" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" Width="70px" MinValue="1"
                                    ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Credito"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtCredito" runat="server" Width="70px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td><asp:Label ID="Label8" runat="server" Text="Ruta"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtRuta" runat="server" Width="70px" ReadOnly="true"></telerik:RadTextBox>
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
                            <td>
                                Desasignar
                            </td>
                            <td>
                                <asp:ImageButton ID="ImbDesasingar" runat="server" ImageUrl="~/Imagenes/check2.png" Width="16px" Height="16px" OnClick="ImbDesasingar_Click" />
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
                                <asp:ImageButton ID="ImbBuscar" runat="server" ImageUrl="~/Img/find16.png" ToolTip="Buscar" OnClick="ImbBuscar_Click" />
                            </td>
                            <td>
                                Asignar
                            </td>
                            <td>
                                <asp:ImageButton ID="ImbAsignar" runat="server" ImageUrl="~/Imagenes/check2.png" Width="16px" Height="16px" OnClick="ImbAsignar_Click" />
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
                    </td>
                    </tr>
            <tr>
                <td colspan="8">
                
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
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Prod." UniqueName="Id_Prd">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Desc" HeaderText="Descripción" UniqueName="Prd_Desc">
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Ord" HeaderText="Pzas Ord" UniqueName="Prd_Ord">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_CantF" HeaderText="Pzas Fac" UniqueName="Ped_CantF">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_CantR" HeaderText="Pzas Rem" UniqueName="Ped_CantR">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_OrdDisp" HeaderText="Pzas Disp" UniqueName="Prd_OrdDisp" Display="false">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Asig" HeaderText="Prd_Asig" UniqueName="Prd_AsigOld" Display="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="Prd_Asig" HeaderText="Pzas Asig" UniqueName="Prd_Asig">
                                <HeaderStyle Width="40px" />
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtAsig" runat="server" Width="60px" MinValue="0" MaxLength="9" DbValue='<%# Bind("Prd_Asig") %>'>
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
                                                    //var index = cell.parentNode.rowIndex -1 ;
                                                    var MasterTable = rdgrid.get_masterTableView();
                                                    var row = MasterTable.get_dataItems()[index]; //getting row

                                                    var Asignado = sender.get_value();
                                                    var DispOrd = row.get_cell('Prd_Disponible').innerText;
                                                    var Exist = row.get_cell('Prd_Existencia').innerText;
                                                    var Old = row.get_cell('Prd_AsigOld').innerText;
                                                    var Disponible = row.get_cell('Prd_Disponible').innerText;
                                                    var Faltante = row.get_cell('Prd_Faltante').innerText;

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
                                                    else if (sender.get_value() > Faltante) {
                                                        sender.set_value(Old);
                                                        radalert('Cantidad asignada es mayor que lo que le falta al pedido', 330, 150);
                                                        HF.value = 'false';
                                                    }
                                                    else {
                                                        HF.value = 'true';
                                                        row.get_cell('PorcentajePiezasAsignadas').innerText = Math.round((sender.get_value() / DispOrd) * 100);
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
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Prd_PorcentajeAsignado" HeaderText="% Pzas Asign" UniqueName="PorcentajePiezasAsignadas" Display="false">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Faltante" HeaderText="Faltante" UniqueName="Prd_Faltante">
                                    <HeaderStyle Width="40px"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Existencia" HeaderText="Inventario" UniqueName="Prd_Existencia" Display="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Prd_Disponible" HeaderText="Disponible" UniqueName="Prd_Disponible" Display="true">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Pzas No Conf" DataField="Prd_NoConf" UniqueName="Prd_NoConf">
                                <HeaderStyle Width="40px" />
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="TxtPrd_NoConf" runat="server" Width="60px" MinValue="0" Text='<%# Bind("Prd_NoConf") %>'>
                                            <EnabledStyle HorizontalAlign="Right" />
                                        </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Pzas No Enc" DataField="Prd_NoEnc" UniqueName="Prd_NoEnc">
                                <HeaderStyle Width="40px" />
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="TxtPrd_NoEnc" runat="server" Width="60px" MinValue="0" Text='<%# Bind("Prd_NoEnc") %>'>
                                            <EnabledStyle HorizontalAlign="Right" />
                                        </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="Seleccionar" >
                                <ItemStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                    <asp:CheckBox ID="ChkSeleccionarTodos" runat="server" AutoPostBack="true" OnCheckedChanged="ChkSeleccionarTodos_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </telerik:GridTemplateColumn>
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
            function AbrirVentana_AsigPrdxPed_Asignar(Id, Id_Cte, Nom_Cte) {
                AbrirVentana_AsigPrdxPed(Id, Id_Cte, Nom_Cte);
                return false;
            }
            function AbrirVentana_PlaneacionReparto(Id, Id_Cte, Nom_Cte) {

                //                var oWnd = radopen("ProAsignPrdxPed.aspx?Id=" + Id + "&Id_Cte=" + Id_Cte + "&Nom_Cte=" + Nom_Cte + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_ProAsignPrdxPed");
                var oWnd = radopen("ProAsignPrdxPed.aspx", "AbrirVentana_PlaneacionReparto");
                oWnd.center();
            }
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

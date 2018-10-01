<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Crmpromocion_ventana.aspx.cs"
    Inherits="SIANWEB.Crmpromocion_ventana" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title></title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ibtnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlBusqueda" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="pnlBusqueda" runat="server" BackColor="AliceBlue" Width="99.5% " BorderColor="SteelBlue"
        BorderWidth="1px">
        <div class="emergente">
            <div align="center" class="ehcontainer">
                <div class="ehizq">
                </div>
                <div class="ehcentro">
                    <asp:Label ID="Label2" runat="server" Text="CATÁLOGO DE CLIENTES" Font-Bold="True"></asp:Label></div>
                <div class="ehder">
                </div>
                <table>
                    <tr>
                        <td style="width: 70px; height: 19px;" align="left">
                            <asp:Label ID="lblBuscaCliente" runat="server" Text="Cliente:"></asp:Label>
                        </td>
                        <td align="left" style="height: 19px">
                            <telerik:RadTextBox ID="txtBuscaCliente" runat="server" AutoPostBack="True" MaxLength="150"
                                Width="288px" TabIndex="1">
                                <ClientEvents OnKeyPress="SoloAlfabetico" />
                            </telerik:RadTextBox>
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="ibtnBuscar" runat="server" AlternateText="Buscar" CausesValidation="False"
                                ImageUrl="Imagenes/view.png" OnClick="ibtnBuscar_Click" TabIndex="3" ToolTip="Buscar cliente" />
                        </td>
                        <td rowspan="2" style="width: 100px">
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td style="width: 70px" align="left">
                            <asp:Label ID="Label3" runat="server" Text="Número de cliente:" Width="112px"></asp:Label>
                        </td>
                        <td align="left" style="width: 96px">
                            <telerik:RadNumericTextBox ID="txtNoCliente" runat="server" Width="125px" MinValue="0"
                                MaxLength="9" TabIndex="2">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                <ClientEvents OnKeyPress="SoloNumerico" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 96px">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </td>
                        <td align="left" style="width: 96px">
                        </td>
                        <td rowspan="1" style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="overflow: auto; width: 785px;">
                                <telerik:RadGrid ID="dgClientes" runat="server" AutoGenerateColumns="False" OnNeedDataSource="dgClientes_NeedDataSource"
                                    CssClass="tr_1" DataKeyField="Id_Cte" Width="750px" BorderColor="Navy" BorderWidth="1px"
                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." OnPageIndexChanged="dgClientes_PageIndexChanged"
                                    OnItemCommand="dgClientes_ItemCommand" PageSize="15" TabIndex="4" GridLines="None">
                                    <SelectedItemStyle BackColor="SteelBlue" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                                    <PagerStyle Mode="NumericPages" />
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    </ClientSettings>
                                    <AlternatingItemStyle CssClass="tr_2" />
                                    <MasterTableView>
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Clave">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NombreCte" HeaderText="Cliente">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Id_Ter" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Ter_Nombre" HeaderText="Territorio">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Uen" HeaderText="Id_Uen" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Uen_Descrip" HeaderText="UEN">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="Select" HeaderText="Seleccionar" Text="Seleccionar">
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        NextPagesToolTip="Páginas siguientes" PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente"
                                        PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:HiddenField ID="HF_ID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"
                                TabIndex="5" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                                TabIndex="6" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function SoloNumerico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if (c && c == 13)
                    eventArgs.set_cancel(true);
                if (c < 48 || c > 57) //si no es numero
                    eventArgs.set_cancel(true);
            }
            function SoloAlfabetico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if ((c < 32) || (c > 32 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 193) || (c > 193 && c < 201) || (c > 201 && c < 205) || (c > 205 && c < 209) || (c > 209 && c < 211) || (c > 211 && c < 218) || (c > 218 && c < 225) || (c > 225 && c < 233) || (c > 233 && c < 237) || (c > 237 && c < 241) || (c > 241 && c < 243) || (c > 243 && c < 250) || (c > 250)) {
                    if (c != 95 && c != 124) //el guion bajo  y '|' tambien son permitidos
                        eventArgs.set_cancel(true);
                }
            }
            function seleccion() {
                var ret = document.getElementById("txtNoCliente").value;
                window.opener.document.getElementById('txtNoCliente_text').value = ret;
                window.close();
            }
            function closer() {
                window.close();
            }
            function refreshGrid() {
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }
            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }
            function returnToParent(arg) {
                //create the argument that will be returned to the parent page
                var oArg = new Object();

                //get the city's name
                oArg.noCliente = arg; //document.getElementById("txtNoCliente").value;

                //get a reference to the current RadWindow
                var oWnd = GetRadWindow();
                //Close the RadWindow and send the argument to the parent page
                if (oArg.noCliente) {
                    oWnd.close(oArg);
                }
                else {
                    alert("Seleccione un cliente válido");
                }
            }

        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>

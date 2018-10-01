<%@ Page Title="Valorización de inventarios" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_InvValorizacionInventarios.aspx.cs" Inherits="SIANWEB.Rep_InvValorizacionInventarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;
                var conntinuar = true;

                var lbl_cmbPropietarios = document.getElementById('<%= lbl_cmbPropietarios.ClientID %>');
                var cmbPropietarios = $find('<%= cmbPropietarios.ClientID %>');

//                if (cmbPropietarios != null)
//                    if (cmbPropietarios.get_value() == '-1') {
//                        lbl_cmbPropietarios.innerHTML = '*Requerido';
//                        conntinuar = false
//                    }
//                    else {
//                        lbl_cmbPropietarios.innerHTML = ''
//                    }
                
                return conntinuar;
            }

            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }


            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        
                        var txtProducto = $find("<%= txtProducto.ClientID %>");
                        continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtProducto);
                        
                        if (continuarAccion == true) {
                            continuarAccion = ValidacionesEspeciales();
                        }
                        break;
                }

                args.set_cancel(!continuarAccion);
            }



            //cuando el campo de texto pirde el foco
            function txtTipoProducto_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTipoProducto.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbTipoProducto_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoProducto.ClientID %>'));
            }

        </script>
    </telerik:radcodeblock>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
        
    </telerik:radajaxloadingpanel>
    <telerik:radajaxmanager id="RAM1" runat="server" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
           <%-- <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="filtros" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="filtros" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <div>
        <telerik:radtoolbar id="RadToolBar1" runat="server" width="100%" dir="rtl" onclientbuttonclicking="ToolBar_ClientClick"
            onbuttonclick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" CssClass="print" ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="DwExcel" Value="DwExcel" Text="" CssClass="facPedido"
                    ToolTip="Descargar formato" ImageUrl="Imagenes/blank.png" />
            </Items>
        </telerik:radtoolbar>
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" onselectedindexchanged="cmbCentrosDist_SelectedIndexChanged"
                        width="150px" autopostback="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div id="filtros" runat="server">
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td valign="top" width="130">
                        <asp:Label ID="lblProd" runat="server" Text="Producto"></asp:Label>
                    </td>
                    <td>
                                    <telerik:radtextbox id="txtProducto" onpaste="return false" runat="server" Width="350px" MaxLength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                    </telerik:radtextbox>
                    </td>
                </tr>
                </table>
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td valign="top" width="130">
                        <asp:Label ID="Label1" runat="server" Text="Tipo de productos"></asp:Label>
                    </td>
                    <td>
                                    <telerik:radnumerictextbox id="txtTipoProducto" runat="server" width="50px" 
                                        minvalue="1" maxlength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnBlur="txtTipoProducto_OnBlur" OnKeyPress="handleClickEvent" />
                                    </telerik:radnumerictextbox>
                    </td>
                    <td>
                                    <telerik:radcombobox id="cmbTipoProducto" runat="server" width="250px" filter="Contains"
                                        changetextonkeyboardnavigation="true" markfirstmatch="true" loadingmessage="Cargando..."
                                        highlighttemplateditems="true" datatextfield="Descripcion" datavaluefield="Id"
                                        onclientselectedindexchanged="cmbTipoProducto_ClientSelectedIndexChanged" 
                                        onclientblur="Combo_ClientBlur" MaxHeight="250px">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 50px; text-align: center">
                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                    </td>
                                                    <td style="width: 200px; text-align: left">
                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:radcombobox>
                    </td>
                </tr>
                </table>
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td valign="top" width="130">
                        <asp:Label ID="Label3" runat="server" Text="Sistemas propietarios"></asp:Label>
                    </td>
                    <td>
                                    <telerik:radcombobox id="cmbPropietarios" runat="server" width="250px" filter="Contains"
                                        changetextonkeyboardnavigation="true" markfirstmatch="true" loadingmessage="Cargando..."
                                        highlighttemplateditems="true" datatextfield="Descripcion" datavaluefield="Id"
                                        onclientblur="Combo_ClientBlur" MaxHeight="250px">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 50px; text-align: center">
                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                    </td>
                                                    <td style="width: 200px; text-align: left">
                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "-- Todos --" : DataBinder.Eval(Container.DataItem, "Descripcion").ToString()%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:radcombobox>
                    </td>
                    <td>
                                    <asp:Label ID="lbl_cmbPropietarios" runat="server" ForeColor="#FF0000"></asp:Label>
                    </td>
                </tr>
                </table>
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td valign="top">
                        <asp:Label ID="Label4" runat="server" Text="Precio" Width="130px"></asp:Label>
                    </td>
                    <td>
                                    <telerik:radcombobox id="cmbPrecio" runat="server" width="130px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-- Seleccionar --" />
                                            <telerik:RadComboBoxItem runat="server" Text="Costo AAA" Value="P_AAA" />
                                            <telerik:RadComboBoxItem runat="server" Text="Precio de lista " Value="P_LISTA" />
                                        </Items>
                                    </telerik:radcombobox>
                    </td>
                    <td>
                                    <asp:RequiredFieldValidator runat="server" ID="val_cmbPrecio" ControlToValidate="cmbPrecio"
                                        ErrorMessage="*Requerido" InitialValue="-- Seleccionar --" ValidationGroup="print"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                    </td>
                </tr>
                </table>
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td valign="top" width="130">
                        <asp:Label ID="Label5" runat="server" Text="Ordenar por"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbOrdenar" runat="server">
                            <asp:ListItem Text="Código de producto" Value="CODIGOPRODUCTO" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Presentación" Value="PRESENTACION"></asp:ListItem>
                            <asp:ListItem Text="Familia" Value="FAMILIA"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            &nbsp;<asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
</asp:Content>

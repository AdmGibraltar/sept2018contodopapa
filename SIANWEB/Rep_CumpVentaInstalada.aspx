<%@ Page Title="Cumplimiento de venta instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_CumpVentaInstalada.aspx.cs" Inherits="SIANWEB.Rep_CumpVentaInstalada" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Mostrar':                       
                        var txtRik = $find("<%= txtRIK.ClientID %>");
                        if (txtRik != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtRik);
                        var txtTerr = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerr != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
                        var txtProducto = $find("<%= txtProducto.ClientID %>");
                        if (txtProducto != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtProducto);
                        //Opcional, validaciones extras
                        break;
                    case 'excel':
                        var txtRik = $find("<%= txtRIK.ClientID %>");
                        if (txtRik != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtRik);
                        var txtTerr = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerr != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
                        var txtProducto = $find("<%= txtProducto.ClientID %>");
                        if (txtProducto != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtProducto);
                        //Opcional, validaciones extras
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function refreshGrid() {
            }
     </script>
   </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>   
                 <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="RBVentaInstalada">
                <UpdatedControls>   
                 <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="RBVentaNueva">
                <UpdatedControls>   
                 <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="RBAnalisis">
                <UpdatedControls>   
                 <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbNivel">
                <UpdatedControls>   
                 <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>  
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar id="RadToolBar1" runat="server" width="100%" dir="rtl" onbuttonclick="RadToolBar1_ButtonClick"
            onclientbuttonclicking="ToolBar_ClientClick" tabindex="10">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir" 
                        ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png" />                
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                        width="99%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMensaje" runat="server" />
                            </td>
                            <td style="text-align: right" width="700px">
                                <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                            </td>
                            <td width="150px" style="font-weight: bold">
                                <telerik:RadComboBox id="CmbCentro" maxheight="250px" runat="server" onselectedindexchanged="cmbCentrosDist_SelectedIndexChanged"
                                    width="150px" autopostback="True">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblFormato" runat="server" Text="Formato" />
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="RBVentaInstalada" runat="server" AutoPostBack="true" Text="Venta instalada facturada "
                                    GroupName="venta" Checked="true" OnCheckedChanged="RBVentaInstalada_CheckedChanged" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="RBVentaNueva" runat="server" AutoPostBack="true" Text="Venta nueva facturada"
                                    GroupName="venta" OnCheckedChanged="RBVentaNueva_CheckedChanged" TabIndex="1" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="RBAnalisis" runat="server" AutoPostBack="true" Text="Análisis de venta instalada"
                                    GroupName="venta" OnCheckedChanged="RBAnalisis_CheckedChanged" TabIndex="2" />
                            </td>
                            <td>
                                <telerik:RadComboBox id="cmbAnalisis" runat="server" width="200px" enabled="false"
                                    tabindex="3">
                                     <Items>                                        
                                        <telerik:RadComboBoxItem runat="server" text="Venta pendiente por autorizar" value="1" owner="cmbAnalisis" selected="true" />
                                        <telerik:RadComboBoxItem runat="server" text="Venta autorizada" value="2" owner="cmbAnalisis" />
                                        <telerik:RadComboBoxItem runat="server" text="Integración de venta instalada" value="3" owner="cmbAnalisis" />                                       
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblSemana" runat="server" Text="Semana" />
                            </td>
                            <td>
                                <telerik:RadComboBox id="ComboSemana" runat="server" width="129px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                                <telerik:RadTextBox id="txtSemana1" runat="server" width="125px" maxlength="19" onpaste="return false"
                                    tabindex="4" Visible="False">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblRIK" runat="server" Text="RIK" />
                            </td>
                            <td>
                                <telerik:RadTextBox id="txtRIK" runat="server" width="125px" maxlength="19" onpaste="return false" tabindex="5">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblTerritorio" runat="server" Text="Territorio" />
                            </td>
                            <td>
                                <telerik:RadTextBox id="txtTerritorio" runat="server" width="125px" maxlength="19"
                                    onpaste="return false" tabindex="6">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LbllProducto" runat="server" Text="Producto" />
                            </td>
                            <td>
                                <telerik:RadTextBox id="txtProducto" runat="server" width="125px" maxlength="19"
                                    onpaste="return false" tabindex="7">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblNivel" runat="server" Text="Nivel" />
                            </td>
                            <td>
                                <telerik:RadComboBox id="cmbNivel" runat="server" width="129px" tabindex="8" 
                                    onselectedindexchanged="cmbNivel_SelectedIndexChanged" AutoPostBack="true">
                                    <Items>                                        
                                        <telerik:RadComboBoxItem runat="server" Text="General " Value="1" Owner="cmbNivel" Selected="true" />
                                        <telerik:RadComboBoxItem runat="server" Text="RIK" Value="2" Owner="cmbNivel" />
                                        <telerik:RadComboBoxItem runat="server" Text="Producto" Value="3" Owner="cmbNivel" />                                       
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="10">
                            </td>
                            <td width="110">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="chkDetalle" runat="server" Text="Detalle" TabIndex="9" Enabled="false" Visible="false"/>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            <asp:HiddenField ID="HF_ClvPag" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

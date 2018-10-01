<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_ListaPrecios.aspx.cs" Inherits="SIANWEB.Rep_ListaPrecios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;
                var conntinuar = true;


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



                if (continuarAccion == true) {
                    continuarAccion = ValidacionesEspeciales();
                }


                args.set_cancel(!continuarAccion);
            }



            //cuando el campo de texto pirde el foco
            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de valuacion de proyectos
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_CentroDisParamsRentabilidad_Open(Id_Emp, Id_Cd, Id_Vap, Id_Cte, Cte_NomComercial, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                //debugger;
                var oWnd = radopen("CatCentroDisParamsRentabilidad.aspx?Id_Vap=" + Id_Vap
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&Id_Cte=" + Id_Cte
                    + "&Cte_NomComercial=" + Cte_NomComercial
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    , "AbrirVentana_CentroDistParametrosRentabilidad");
                oWnd.center();
            }


        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" 
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" CssClass="print"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
            font-size: 8pt">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div id="filtros" runat="server">
            <table border="0" style="font-family: Verdana; font-size: 8pt">
               <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Lista de Precios"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbListaPrecios" runat="server" Width="130px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="2014" Value="2014" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
    </div>
</asp:Content>

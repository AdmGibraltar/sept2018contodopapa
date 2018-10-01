<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="ListadoValuaciones.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Valuaciones.ListadoValuaciones" %>
<%@ Register Src="~/js/ListControl/ListControlToolbar_js.ascx" TagPrefix="uc" TagName="ListControlToolbar" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Valuaciones/UCListadoValuaciones.ascx" TagPrefix="uc" TagName="UCListadoValuaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphToolbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBodyContent" runat="server">
    <script type="text/html" id="tplListadoValuacionesTabularHeader">
        <th>
            Clave
        </th>
        <th>
            Fecha
        </th>
        <th>
            Cliente
        </th>
        <th>
            Estado
        </th>
        <th>
            <!--Editar-->
        </th>
        <th>
            <!--Eliminar-->
        </th>
    </script>
    <script type="text/html" id="tplListadoValuacionesRow">
        <td data-content="Id_Vap">
        </td>
        <td data-content="Vap_FechaCorta">
        </td>
        <td data-content="Vap_NombreCte">
        </td>
        <td data-content="Vap_Estatus">
        </td>
        <td style="text-align: center;">
            <button class="btn btn-default"><i class="fa fa-pencil-square-o"></i></button>
        </td>
        <td style="text-align: center;">
            <button class="btn btn-default"><i class="fa fa-times"></i></button>
        </td>
    </script>

    <form runat="server" id="frmMain">
        <asp:ScriptManager runat="server" ID="smMaster">
        </asp:ScriptManager>
    </form>
    <div id="lct" runat="server">
        <div class="tb-head">
        </div>
        <div class="row">
            <div id="tbContent">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.min.js") %>"></script>
    <uc:ListControlToolbar ID="ucListControlToolbar" runat="server" />
    <uc:UCListadoValuaciones ID="ucListadoValuaciones" runat="server"></uc:UCListadoValuaciones>
    <script type="text/javascript">
        var _tabularListView = null;
        $(document).ready(function () {
            _tabularListView = new crm.ui.ListControlTabularView({ 
                detailView: new crm.ui.ListControlTabularTemplateDetailView($('#tplListadoValuacionesRow')), 
                dataSource: [], 
                container: $('#<%=lct.ClientID %> #tbContent'), 
                headerDefinition: new crm.ui.ListControlTabularDetailViewTemplateHeaderDefinition($('#tplListadoValuacionesTabularHeader')) 
            });
            $('#<%=lct.ClientID %>').crmlistadovaluaciones({ 
                views: [
                        new crm.ui.ListControlToolbarDetailView(null, 'fa fa-th', function () { }, _tabularListView)
                       ] 
            });
        });
    </script>
</asp:Content>

<%@ Page Title="Exceso de inventarios" Language="C#" AutoEventWireup="true" CodeBehind="Rep_InvExceso2.aspx.cs"
    Inherits="SIANWEB.Rep_InvExceso2" %>
   

    <%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Menu.Sian.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Toolbar.css" rel="stylesheet" type="text/css" />
</head>

<body Onresize="OnResizeDocument()">
    <form id="form2" runat="server">
    <%--<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />      <telerik:RadSkinManager ID="QsfSkinManager" runat="server" ShowChooser="true" />     <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" />--%>
 
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnableTheming="true">    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" PersistenceMode="Session" Skin="Outlook"> </telerik:RadSkinManager>  
              
    <telerik:RadAjaxManager ID="RAM1" runat="server" >
    <%--<telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">--%>
    <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                   <%-- <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />--%>
                      <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
        
<%--        <asp:CheckBox ID="CheckBox1" Text="Export only data" runat="server"></asp:CheckBox>
        <br />
        <asp:CheckBox ID="CheckBox2" Text="Ignore paging (exports all pages)" runat="server">
        </asp:CheckBox>
        <br />
        <asp:CheckBox ID="CheckBox3" Text="Open exported data in new browser window" runat="server">
        </asp:CheckBox>--%>
        <br />
        <br />

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    
    <div runat="server" id="divPrincipal">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 9pt;">
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="LblTitulo" runat="server" Text="EXCESO DE INVENTARIO <b>[Rota]</b> " Width="500px"></asp:Label>
                      <br/>  
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt;">
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblLeyenda" runat="server" Text="Costo de inventario del proveedor <b>[Proveedor]</b> del centro <b>[Sucursal]</b> en los días <b>[Dia]</b>.<br>Última actualización <b>[Fecha]</b>"
                        Width="500px"></asp:Label>
                        
                </td>
            </tr>
        </table>

        <table style="font-family: Verdana; font-size: 8pt;">
            <tr>
                <td>
                <asp:Button ID="Button1" Width="150px" Text="Exportar a Excel" OnClick="Button1_Click" runat="server"></asp:Button>                                
                </td>                
            </tr>
        </table>

        <div id="bar" style="overflow: auto; height: 450px">
        
            <table>
                <tr>
                    <td>
                    </td>
                    <td>                    
                        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="True"   PageSize="20"
                            OnNeedDataSource="RadGrid1_NeedDataSource" ShowFooter="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            Width="480px"  OnPageIndexChanged="RadGrid1_PageIndexChanged"
                            OnItemDataBound="RadGrid1_ItemDataBound" OnGridExporting="RadGrid1_GridExporting" >                            

                            <ExportSettings Excel-Format="Html" IgnorePaging="true" OpenInNewWindow="true" FileName="ExcesoDeInventarios"
                                HideStructureColumns="true">
                                <%--<ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="ListaNotasCargos"
                                HideStructureColumns="true" ExportOnlyData="true">--%>                                
                            </ExportSettings>
                            
                            <MasterTableView DataKeyNames="Id_Pvd" CommandItemDisplay="None">

                                <CommandItemSettings ShowExportToExcelButton="true"  ExportToExcelText="Exportar a Excel" 
                                ></CommandItemSettings>
                                   <%-- <CommandItemSettings ShowExportToPdfButton="true"  ExportToPdfText="Exportar a Pdf"
                                    ShowExportToExcelButton="true"  ExportToExcelText="Exportar a Excel" ShowExportToWordButton="true"
                                    ExportToWordText="Exportar a Word" ShowExportToCsvButton="false" ExportToCsvText="Exportar a Csv"
                                    AddNewRecordText="Agregar"></CommandItemSettings>--%>
                                <Columns>
                                    <telerik:GridHyperLinkColumn HeaderText="Número" UniqueName="Id_Pvd" DataTextField="Id_Pvd"
                                        Target="_blank">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" Font-Underline="true" CssClass="link" />
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridHyperLinkColumn HeaderText="Proveedor" UniqueName="Pvd_Nombre" DataTextField="Pvd_Nombre"
                                        FooterText="Totales:" Target="_blank">
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle Font-Underline="true" CssClass="link" />
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn HeaderText="Costo" UniqueName="Costo" DataField="Costo"
                                        DataFormatString="{0:N2}" Aggregate="Sum">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Cantidad" UniqueName="Exceso" DataField="Exceso"
                                        Aggregate="Sum" DataFormatString="{0:#0}">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Disponible" UniqueName="Disponible" DataField="Disponible"
                                        Aggregate="Sum" DataFormatString="{0:#0}">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                            </MasterTableView>
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" ShowPagerText="True"
                                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >." />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
       
        <script type="text/javascript">

//            function onRequestStart(sender, args) {
//                if (args.get_eventTarget().indexOf("Button1") >= 0)
//                    args.set_enableAjax(false);
//            }

           function onRequestStart(sender, args) {
       if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                   args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
            args.set_enableAjax(false);
    }
  }

            function myJS(var1, var2, var3, var4, var5, var6, var7) {
                var window_dimensions = "toolbars=no,menubar=no,directories=no,location=no,scrollbars=no,resizable=yes,status=no,width=600,height=550"
                window.open("Rep_InvExceso3.aspx?Proveedor=" + var1 + "&Centro=" + var2 + "&DiasVer=" + var3 + "&Tproducto=" + var4 + "&Indicador=" + var5 + "&Dias=" + var6 + "&ProveedorVer=" + var7, "_blank", window_dimensions);
            }
            function OnResizeDocument() {
                var div = document.getElementById("bar");
                div.style.height = document.documentElement.offsetHeight - 60;
            }
             
        </script>
    </telerik:RadScriptBlock>
    </form>
</body>
</html>

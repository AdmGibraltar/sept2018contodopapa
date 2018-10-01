<%@ Page Title="Exceso de inventarios" Language="C#" AutoEventWireup="true" CodeBehind="Rep_InvExceso3.aspx.cs"
    Inherits="SIANWEB.Rep_InvExceso3" %>


    <%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Menu.Sian.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Toolbar.css" rel="stylesheet" type="text/css" />
</head>

<body onresize="OnResizeDocument()">
    <form id="form2" runat="server">


    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnableTheming="true"> </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" PersistenceMode="Session" Skin="Outlook">    </telerik:RadSkinManager>

    <telerik:RadAjaxManager ID="RAM1" runat="server" >
    <%--<telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">--%>
    <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                    <%--<telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />--%> 
                    </UpdatedControls>
            </telerik:AjaxSetting>
           <%-- <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"   UpdatePanelHeight="" /> </UpdatedControls>
            </telerik:AjaxSetting>--%>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    &nbsp;
                </td>
                <td width="150px" style="font-weight: bold">
                    &nbsp;
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

        <div id="bar" style="overflow: auto; height: 480px">
            <table>
                <tr>
                <td>
                    
                                      
                        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="True"
                            OnNeedDataSource="RadGrid1_NeedDataSource" ShowFooter="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            Width="550px"  OnPageIndexChanged="RadGrid1_PageIndexChanged"
                             OnGridExporting="RadGrid1_GridExporting" >
                                                        

                            <ExportSettings Excel-Format="Html" IgnorePaging="true" OpenInNewWindow="true" FileName="ExcesoDeInventarios"
                                HideStructureColumns="true">                                
                            </ExportSettings>

                            <MasterTableView DataKeyNames="Id_Pvd" CommandItemDisplay="None">
                                <CommandItemSettings ShowExportToPdfButton="true" ExportToPdfText="Exportar a Pdf"
                                    ShowExportToExcelButton="true" ExportToExcelText="Exportar a Excel" ShowExportToWordButton="true"
                                    ExportToWordText="Exportar a Word" ShowExportToCsvButton="false" ExportToCsvText="Exportar a Csv"
                                    AddNewRecordText="Agregar"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Prov." UniqueName="Id_Pvd" DataField="Id_Pvd"
                                        DataFormatString="{0:N0}">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn HeaderText="Clave" UniqueName="Clave" DataTextField="Id_Prd"
                                        DataNavigateUrlFields="url" Target="_blank">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridHyperLinkColumn HeaderText="Artículo" UniqueName="Articulo" DataTextField="Prd_Descripcion"
                                        DataNavigateUrlFields="url" FooterText="Totales:" Target="_blank">
                                        <HeaderStyle Width="200px" />
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

            function OnResizeDocument() {
                var div = document.getElementById("bar");

                div.style.height = document.documentElement.offsetHeight - 70;
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                   args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                    args.set_enableAjax(false);
                }
            }
             
        </script>
    </telerik:RadScriptBlock>
    </form>
</body>
</html>

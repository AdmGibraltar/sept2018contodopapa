<%@ Page Title="Bitácora modificaciones" Language="C#" AutoEventWireup="true" CodeBehind="Ventana_Bitacora.aspx.cs"
    Inherits="SIANWEB.Ventana_Bitacora" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
      <link href="../Styles/Toolbar.css" rel="stylesheet" type="text/css" />
      <link href="../Styles/Menu.Sian.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

        .html, body, form
        {
            margin: 0px;
            padding: 0px;
            overflow: hidden;
            height: auto;
          
        }
        .menuPanes
        {
            overflow: visible !important;

        }
        .formulario
        {
            font-family: Arial;
            font-size: 12px;

        }
        .dvstyle
        {
            position: fixed;
            top: 0;
            bottom: 0;
            width: 100%;
            height: 100%;
        }

        .hideMe
        {
            display: none !important;
        }
        
        div.RadGrid .rgRefresh, div.RadGrid .rgRefresh + a
        {
            display: none;
        }
    </style>
</head> 
<body>
    <form id="form1" runat="server" width="500" >

      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      </telerik:RadScriptManager>
      <telerik:RadWindowManager ID="RWM1" runat="server" Skin="Office2007">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PnlLogin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="rgBitacora">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="PnlLogin" runat="server" >
        <table style="font-family: Verdana; font-size: 8pt" > 

            <tr>
            <td colspan="4">
             &nbsp; &nbsp;
                <asp:Label ID="LblMensaje" runat="server"></asp:Label>
            </td>
            </tr>

                       <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="left" colspan="3">
               <telerik:RadGrid ID="rgBitacora" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="7"
            AllowFilteringByColumn="False" MasterTableView-NoMasterRecordsText="No se encontraron registros."
            OnNeedDataSource="rgBitacora_NeedDataSource" OnPageIndexChanged="rgBitacora_PageIndexChanged"
            GridLines="None" >
            <MasterTableView >
                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="Id_Acs" UniqueName="CteBit_Fecha" 
                               HeaderText="Folio"   DataFormatString="{0:g}">             
                                       <HeaderStyle Width="200px" />
                                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="Pantalla" HeaderText="Pantalla" UniqueName="U_Nombre">
                                        <HeaderStyle Width="850px" />
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="Campo" HeaderText="Campo" UniqueName="Cambios">
                                        <HeaderStyle Width="300px" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Valor_Anterior" HeaderText="Valor Anterior" UniqueName="Cambios">
                                        <HeaderStyle Width="300px" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Valor_Actualizado" HeaderText="Valor Actual" UniqueName="Cambios">
                                        <HeaderStyle Width="300px" />
                     </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Cambios">
                                        <HeaderStyle Width="300px" />
                     </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Usuario" HeaderText="Usuario" UniqueName="Cambios">
                                        <HeaderStyle Width="300px" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripcion" UniqueName="Cambios">
                                        <HeaderStyle Width="300px" />
                     </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Codigo" HeaderText="Codigo" UniqueName="Cambios">
                                        <HeaderStyle Width="300px" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
        </telerik:RadGrid>
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>

            <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;
             
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
         
            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                CloseAndRebind();
                            });
            }
            

            function AlertaFocus(mensaje, control) {

                var oWnd = radalert(mensaje, 340, 150);
                //oWnd.add_close(foco(control));
                oWnd.add_close(function () {
                    var target = $find(control);
                    if (target != null) {
                        target.focus();
                    }
                });
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
            }
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }


        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>

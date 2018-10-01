<%@ Page Title="Subir excel Físicos" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="Ventana_Fisico.aspx.cs" Inherits="SIANWEB.Ventana_Fisico" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
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
            function CloseAndRebind(param) {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.FisicoTerminado(param);
            }

        </script>
        <style type="text/css">
            .ruBrowse
            {
                background-position: 0 -23px !important;
                width: 80px !important;
            }
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="buttonSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="formulario">
        <div runat="server" id="divPrincipal" style="margin-left: 10px; margin-right: 10px;
            margin-top: 10px;">
            <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="buttonSubmit">
                    <table>
                        <tr>
                            <td colspan="3">
                                <telerik:RadAsyncUpload runat="server" ID="RadUpload1" AllowedFileExtensions="xls,xlsx"
                                    Height="25px" Width="400px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                    ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30">
                                    <Localization Remove="Quitar" Select="Seleccionar" />
                                </telerik:RadAsyncUpload>
                                <asp:Panel ID="ValidFiles" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="buttonSubmit" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    Style="margin-top: 6px" OnClick="btnImportar_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </telerik:RadAjaxPanel>
        </div>
    </div>
</asp:Content>

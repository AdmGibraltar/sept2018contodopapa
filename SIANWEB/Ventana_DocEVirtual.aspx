<%@ Page Title="Subir Documento" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="Ventana_DocEVirtual.aspx.cs" Inherits="SIANWEB.Ventana_DocEVirtual" %>

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
                width: 250px !important;
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
                            <td><label id="lblAutorizacion">Documento de Autorizacion</label></td>
                            <td colspan="3">
                                <telerik:RadAsyncUpload runat="server" ID="RadUpload1" AllowedFileExtensions="pdf,jpg,xls,doc,docx,png,xlsx,ppt,pptx,bmp,jepg"
                                    Height="25px" Width="400px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                    ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30" MaxFileSize="524288" TargetFolder="~/App_Data/RadUploadTemp" >
                                    <Localization Remove="Quitar" Select="...." />
                                </telerik:RadAsyncUpload>
                                <asp:Panel ID="ValidFiles" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="buttonSubmit" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo "
                                    Style="margin-top: 6px" OnClick="btnImportar_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>

                            <td><label id="Label1">Contrato</label></td>
                            <td colspan="3">
                                <telerik:RadAsyncUpload runat="server" ID="RadUpload2" AllowedFileExtensions="pdf,jpg,xls,doc,docx,png,xlsx,ppt,pptx,bmp,jepg"
                                    Height="25px" Width="400px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                    ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30" MaxFileSize="524288" TargetFolder="~/App_Data/RadUploadTemp">
                                    <Localization Remove="Quitar" Select="..." />
                                </telerik:RadAsyncUpload>
                                <asp:Panel ID="Panel2" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="button1" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    Style="margin-top: 6px" OnClick="btnImportar_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                        <td><label id="Label3">Orden de Compra</label></td>
                            <td colspan="3">
                                <telerik:RadAsyncUpload runat="server" ID="RadUpload3" AllowedFileExtensions="pdf,jpg,xls,doc,docx,png,xlsx,ppt,pptx,bmp,jepg"
                                    Height="25px" Width="400px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                    ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30" MaxFileSize="524288" TargetFolder="~/App_Data/RadUploadTemp">
                                    <Localization Remove="Quitar" Select="..." />
                                </telerik:RadAsyncUpload>
                                <asp:Panel ID="Panel4" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="button3" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                    Style="margin-top: 6px" OnClick="btnImportar_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                        <td><label id="Label2">Comunicado del Cliente</label></td>
                            <td colspan="3">
                                <telerik:RadAsyncUpload runat="server" ID="RadUpload4" AllowedFileExtensions="pdf,jpg,xls,doc,docx,png,xlsx,ppt,pptx,bmp,jepg"
                                    Height="25px" Width="400px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                    ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30" MaxFileSize="524288" TargetFolder="~/App_Data/RadUploadTemp">
                                    <Localization Remove="Quitar" Select="..." />
                                </telerik:RadAsyncUpload>
                                <asp:Panel ID="Panel3" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="button2" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
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

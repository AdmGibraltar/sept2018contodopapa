<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="ventana_RechazaSolicitud.aspx.cs" Inherits="SIANWEB.ventana_RechazaSolicitud" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

 <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
         
 
            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow(mensaje) {
                ////debugger;
                var cerrarWindow = radalert(mensaje, 240, 130, tituloMensajes);
                  cerrarWindow.add_close(
                    function () {
                        ////debugger;
                        CloseAndRebind();
                        GetRadWindow().BrowserWindow.location.reload();
                    });
                    

            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                ////debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura especial se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
         
            function OnClientItemsRequestedHandler(sender, eventArgs) {
                debugger;
                //set the max allowed height of the combo  
                var MAX_ALLOWED_HEIGHT = 520;
                //this is the single item's height  
                var SINGLE_ITEM_HEIGHT = 22;

                var calculatedHeight = sender.get_items().get_count() * SINGLE_ITEM_HEIGHT;

                var dropDownDiv = sender.get_dropDownElement();

                if (calculatedHeight > MAX_ALLOWED_HEIGHT) {
                    setTimeout(function () { dropDownDiv.firstChild.style.height = MAX_ALLOWED_HEIGHT + "px"; }, 20);
                }
                else {
                    setTimeout(function () { dropDownDiv.firstChild.style.height = calculatedHeight + "px"; }, 20);
                }
            }  
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
       <telerik:RadAjaxManager ID="RAM1" runat="server"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div  class="formulario" id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl"
        OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
           <%--
                 <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="save" />--%>

            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td colspan ="7">
                                &nbsp;
                                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ></asp:Label>
                            </td>
                     
                        </tr>
                 
                        <tr>
                            <td>
                                <b> Folio: </b></td>
                            <td colspan="4">
                                <asp:Label ID="LblId_Fac" runat="server" Font-Bold="True" ></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="LblIdCdi" runat="server" Font-Bold="True" Visible="False" ></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                       
                        <%--<tr>
                            <td>
                                <b>Naturaleza :</b>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="LblId_Cte" runat="server" Font-Bold="True" Width="40px">
                                </asp:Label>
                              <asp:Label  ID="Lbl" runat="server" Font-Bold="True"  Text ="-" Width="10px"></asp:Label>
                                <asp:Label  ID="lblCte_Nombre" runat="server" Font-Bold="True" Width="207px">
                                </asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                               </td>
                            <td colspan="4">
                               
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                    
                            </td>
                            <td colspan="4">
                            
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Motivo</td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txtMotivo" runat="server" Width="285px" Height = "100px" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtMotivo" ErrorMessage="*Requerido" ForeColor="Red" 
                                    ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                               </td>
                            <td colspan="4">
                             
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td> &nbsp;
                            </td>
                            <td>
                               <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" 
                               ToolTip="Rechazar" OnClick="BtnRechazar_Click" Visible="true" />
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>  
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

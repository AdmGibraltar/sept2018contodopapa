    <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="True" CodeBehind="capAcysEnviarCorreo.aspx.cs" Inherits="SIANWEB.capAcysEnviarCorreo"  EnableEventValidation="false"%>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
<script type="text/javascript">
          //<![CDATA[
    function OnClientPasteHtml(sender, args) {
        var commandName = args.get_commandName();
        var value = args.get_value();

        if (commandName == "InsertDate" || commandName == "InsertTime") {
            //Set the inserted date / time string in a DIV with formatting
            var div = document.createElement("DIV");

            Telerik.Web.UI.Editor.Utils.setElementInnerHtml(div, value);

            //Set date/time information in a DIV element with applied color and background formatting                       
            args.set_value("<div style='margin: 1px; padding:2px; width:auto; display:inline;border:solid 2px black; color:black; background-color:#FFCC00;'>" + div.innerHTML + "</div>");
        }

        if (commandName == "ImageManager") {
            //See if an img has an alt attribute set                       
            var div = document.createElement("DIV");

            //Do not use div.innerHTML as in IE this would cause the image's src or the link's href to be converted to absolute path.
            //This is a severe IE quirk.
            Telerik.Web.UI.Editor.Utils.setElementInnerHtml(div, value);

            //Now check if there is alt attribute       
            var img = div.firstChild;
            if (!img.alt) {
                var alt = prompt("No alt tag specified. Please specify an alt attribute for the image", "");
                img.setAttribute("alt", alt);

                //Set new content to be pasted into the editor                       
                args.set_value(div.innerHTML);
            }
        }

        if (commandName == "DocumentManager") {
            //Set target="_blank" to the links inserted through the Document manager
            var div = document.createElement("DIV");

            Telerik.Web.UI.Editor.Utils.setElementInnerHtml(div, value);
            //Now check if there is target attribute
            var link = div.firstChild;
            if (!link.target) {
                alert("No target attribute specified. The editor will automatically set target='blank' to the link");
                link.setAttribute("target", "_blank");

                //Set new content to be pasted into the editor                       
                args.set_value(div.innerHTML);
            }
        }

        if (commandName == "InsertTable") {
            //Set border to the inserted table elements
            var div = document.createElement("DIV");

            //Remove extra spaces from begining and end of the tag
            value = value.trim();

            Telerik.Web.UI.Editor.Utils.setElementInnerHtml(div, value);
            var table = div.firstChild;

            if (!table.style.border) {
                table.style.border = "dashed 1px red";
                //Set new content to be pasted into the editor                       
                args.set_value(div.innerHTML);
            }
        }

        //Cancel the event if you want to prevent pasteHtml to execute   
        /*
        args.set_cancel(true);
        */
    }
          //]]>
     </script>
    <telerik:radajaxmanager id="RAM1" runat="server" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"  />
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                ImageUrl="~/Imagenes/blank.png" />            
        </Items>
    </telerik:radtoolbar>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    &nbsp; &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="120">
                                <asp:Label ID="Label5" runat="server" Text="Enviar a"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:radtextbox id="txtCorreos" runat="server" width="550px" >                                
                                </telerik:radtextbox> 
                                  <asp:RequiredFieldValidator ID="val_txtCorreos" runat="server" ControlToValidate="txtCorreos"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>                              
                            </td>
                        </tr>
                        <tr>
                            <td width="120">
                                <asp:Label ID="Label2" runat="server" Text="Asunto" />
                            </td>
                            <td>
                                <telerik:radtextbox id="txtAsunto" runat="server" width="550px"  Value="">                                    
                                </telerik:radtextbox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAsunto"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>  
                            </td>
                            <td>
                            </td>                            
                            <td>
                                &nbsp;
                            </td>                            
                        </tr>
                         <tr>
                            <td width="120">
                                <asp:Label ID="Label1" runat="server" Text="Archivo adjunto" />
                            </td>
                            <td>
                                
                               <table><tr><td>
                                   
                                <img alt="product logo" src="Imagenes/pdf.png" width="30" height="30" />
                               </td><td>
                               <asp:Label ID="Label3" runat="server" Text="Key- Acuerdo Comercial y de Servicios.pdf" />
                               </td></tr></table> 
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
                            </td>
                            <td width="10">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td width="100">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:RadEditor runat="server" OnClientPasteHtml="OnClientPasteHtml" ID="RadEditor1">
                      <Tools>
                           <telerik:EditorToolGroup Tag="FileManagers">
                                <telerik:EditorTool Name="InsertTime"></telerik:EditorTool>
                                <telerik:EditorTool Name="InsertDate"></telerik:EditorTool>
                                <telerik:EditorTool Name="InsertTable"></telerik:EditorTool>  
                           </telerik:EditorToolGroup>
                      </Tools>                                         
                      <Content>
                           Acuerdo Comercial  Autorizado, 
                           <ul>
                                
                               <li><em> </em></li>                               
                                <li><em>Saludos cordiales!</em></li>
                           </ul>
                      </Content>
                </telerik:RadEditor>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HiddenRebind" runat="server" Value="false" />
    <asp:HiddenField ID="HF_Ped" runat="server" />
    <asp:HiddenField ID="HF_Guardar" runat="server" Value="true" />
     <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
     <asp:HiddenField ID="HiddenHeight" runat="server" />
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }

            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }


            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;

                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

            
           </script>
    </telerik:radcodeblock>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatModulos.aspx.cs" Inherits="SIANWEB.CatModulos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                 
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }
            }


            function popupAyuda() {
            try {
                var testo = document.getElementById("<%= txtClave.ClientID %>");
                var testo2 = document.getElementById("<%= txtdescripcion.ClientID %>");

                var sUrl = "Ayuda/CatAyuda.aspx?IdPag=" + testo.value + "&DescPag=" + testo2.value;
                //  alert(sUrl);
                var oWnd = radopen(sUrl, "AbrirCatalogo_Ayuda");
                oWnd.center();
            }
            catch (err) {
                alert(err);
            }
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="tvMenu" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tvMenu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="tvMenu" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
            </td>
            <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <div runat="server" id="divPrincipal">
        <table border="0px" style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td valign="top">
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td>
                                <telerik:RadToolBar ID="RadToolBar1" runat="server" AutoPostBack="True" dir="rtl"
                                    meta:resourceKey="RadToolBar1Resource1"
                                    OnButtonClick="RadToolBar1_ButtonClick"
                                    Width="100%">
                                    <Items>
                                        <telerik:RadToolBarButton runat="server" Enabled="False" meta:resourceKey="RadToolBarButtonResource1"
                                            Width="20px" />
                                        <telerik:RadToolBarButton runat="server" CommandName="save" Value="save" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                                            meta:resourceKey="RadToolBarButtonResource2" ToolTip="Guardar"   />
                                        <telerik:RadToolBarButton runat="server" Enabled="False" meta:resourceKey="RadToolBarButtonResource3"
                                            Width="20px" />
                                        <telerik:RadToolBarButton runat="server" CommandName="right" ImageUrl="Imagenes/RadTree/right.png"
                                            meta:resourceKey="RadToolBarButtonResource4" ToolTip="Derecha" />
                                        <telerik:RadToolBarButton runat="server" CommandName="down" ImageUrl="Imagenes/RadTree/down.png"
                                            meta:resourceKey="RadToolBarButtonResource5" ToolTip="Bajar" />
                                        <telerik:RadToolBarButton runat="server" CommandName="up" ImageUrl="Imagenes/RadTree/up.png"
                                            meta:resourceKey="RadToolBarButtonResource6" ToolTip="Subir" />
                                        <telerik:RadToolBarButton runat="server" CommandName="left" ImageUrl="Imagenes/RadTree/left.png"
                                            meta:resourceKey="RadToolBarButtonResource7" ToolTip="Izquierda" />
                                    </Items>
                                </telerik:RadToolBar>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadTreeView ID="tvMenu" runat="server" Width="300px" OnNodeClick="tvMenu_NodeClick"
                                    OnNodeDrop="tvMenu_NodeDrop" EnableDragAndDrop="True" 
                                    EnableDragAndDropBetweenNodes="True">
                                </telerik:RadTreeView>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top">
                    <table  runat="server" id="table0">
                        <tr>
                            <td colspan="4">
                                <telerik:RadToolBar ID="RadToolBar2" runat="server" AutoPostBack="True" dir="rtl"
                                    Width="100%" meta:resourceKey="RadToolBar2Resource1" 
                                    OnClientButtonClicking="ToolBar_ClientClick" 
                                    OnButtonClick="RadToolBar2_ButtonClick">
                                    <Items>
                                        <telerik:RadToolBarButton Width="20px" Enabled="False" runat="server" meta:resourceKey="RadToolBarButtonResource8" />
                                        <telerik:RadToolBarButton CommandName="save" Value="save" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                                            ToolTip="Guardar" ValidationGroup="guardar" runat="server" meta:resourceKey="RadToolBarButtonResource9" />
                                        <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ImageUrl="~/Imagenes/blank.png"
                                            ToolTip="Eliminar" runat="server" meta:resourceKey="RadToolBarButtonResource10" />
                                        <telerik:RadToolBarButton CommandName="new" CssClass="new" ImageUrl="~/Imagenes/blank.png"
                                            ToolTip="Nuevo" runat="server" meta:resourceKey="RadToolBarButtonResource11" />
                                    </Items>
                                </telerik:RadToolBar>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Clave de pantalla" Visible="False"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            <div style=" visibility:hidden">
                                <telerik:RadTextBox onpaste="return false" ID="txtClave" Runat="server" ReadOnly="True" 
                                    Width="70px">
                                </telerik:RadTextBox>
                                </div>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Descripción"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtdescripcion" runat="server" Width="150px">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_txtdescripcion" runat="server" 
                                    ControlToValidate="txtdescripcion" Display="Dynamic" ErrorMessage="*Requerido" 
                                    ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Dirección URL"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txturl" runat="server" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20">
                            </td>
                            <td valign="middle">
                                <asp:Label ID="Label1" runat="server" Text="Imagen"></asp:Label>
                            </td>
                            <td valign="middle">
                                &nbsp;</td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtImagen" runat="server" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Tipo de pantalla"></asp:Label>
                            </td>
                            <td width="5">
                                &nbsp;</td>
                            <td>
                                <telerik:RadComboBox ID="cmbTipo" Runat="server" Width="155px" 
                                    LoadingMessage="Cargando..." onclientblur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <!-- Seccion para definir pantalla de ayuda -->
                         <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Pagina de Ayuda"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <telerik:RadTextBox enabled="false" ID="txthlp" runat="server" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="right" >
                                <a id="hlpURL" runat="server" href="#" onclick="popupAyuda();">Pagina de Ayuda URL</a>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <!-- EMC Ago 2018 -->
                        <tr>
                            <td width="20">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:HiddenField ID="HF_Modificar" runat="server" Value="0" Visible="False" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    </div>
</asp:Content>

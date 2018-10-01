<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatDesbloqueo.aspx.cs" Inherits="SIANWEB.CatDesbloqueo" %>

<asp:Content ID="C1" ContentPlaceHolderID="CPH" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="true" dir="rtl"
        Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton ToolTip="Correo" CommandName="mail" CssClass="mail" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton ToolTip="Imprimir" CommandName="print" CssClass="print"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton ToolTip="Eliminar" CommandName="delete" CssClass="delete"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton ToolTip="Regresar" CommandName="undo" CssClass="undo" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton ToolTip="Guardar" CommandName="save" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                ValidationGroup="guardar" />
            <telerik:RadToolBarButton ToolTip="Nuevo" CommandName="new" CssClass="new" ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                <input id="HiddenId_U" type="hidden" runat="Server" visible="false" />
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                &nbsp;
            </td>
           <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <div runat="server" id="divPrincipal">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table id="TblDesbloquear" runat="server">
                        <tr>
                            <td style="width: 85px">
                                <asp:Label ID="Label2" runat="server" Text="Cuenta"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtUsuario" runat="server">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtUsuario"
                                    ErrorMessage="*Requerido" ValidationGroup="Buscar" Display="Dynamic" SetFocusOnError="true"
                                    ForeColor="Red" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="Button1_Click1"
                                    ValidationGroup="Buscar" ToolTip="Buscar" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkBloqueado" runat="server" AutoPostBack="true" Text="Bloqueado"
                                    ToolTip="Indica si la cuenta está bloqueada" Style="cursor: hand" TabIndex="5" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Último bloqueo"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

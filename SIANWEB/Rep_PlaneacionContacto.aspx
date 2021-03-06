﻿<%@ Page Title="Reporte de Planeacion de Contacto de Venta Instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_PlaneacionContacto.aspx.cs" Inherits="SIANWEB.Rep_PlaneacionContacto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Mostrar':
//                        var txtTerr = $find(" = txtTerritorio.ClientID ");
//                        if (txtTerr != null)
//                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerr);
                        var txtCliente = $find("<%= txtNumeroCliente.ClientID %>");
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        //Opcional, validaciones extras
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function refreshGrid() {
            }

            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
//                ClientSelectedIndexChanged(eventArgs.get_item(), $find(' = txtTerritorio.ClientID  >'));
            }

            function txtTerritorio_OnBlur(sender, args) {
//                OnBlur(sender, $find('< = cmbTer.ClientID >'));
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                      
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNumeroCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTer" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />                                                       
                </UpdatedControls>
            </telerik:AjaxSetting>            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Mostrar" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                        width="99%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMensaje" runat="server" />
                            </td>
                            <td style="text-align: right" width="1000px">
                                <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                            </td>
                            <td width="150px" style="font-weight: bold">
                                <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                                    Width="150px" AutoPostBack="True">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblCliente" runat="server" Text="Cliente" />
                            </td>
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>                                        
                                        <td style="padding-right:5px">
                                            <telerik:RadNumericTextBox ID="txtNumeroCliente" runat="server" Width="125px" MinValue="1" MaxLenght="9" onpaste="return false" AutoPostBack="true" OnTextChanged="txtNumCliente_TextChanged">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>                                    
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtNombreCliente" runat="server" Width="300px" MaxLength="100" onpaste="return false" Enabled="false">
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>                                    
                                </table>
                            </td>                           
                        </tr>
                        <tr>
                             <td>&nbsp;
                                <asp:Label ID="Label1" runat="server" Text="RSC/ASESOR/RIK" />
                            </td>
                            <td colspan="5">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>   
                                        <td >
                                            <telerik:RadComboBox runat="server" ID="cmbRSC" AutoPostBack="false"  Size="large"
                                            EnableVirtualScrolling="True" ItemsPerRequest="10" ShowMoreResultsBox="True"
                                             ChangeTextOnKeyBoardNavigation="true" Filter="Contains"
                                               MaxHeight="300px" EmptyMessage="-- Seleccionar --" Width="400px" >
                                            <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                                NoMatches="No hay coincidencias" />
                                            </telerik:RadComboBox> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%-- 
                        <tr>
                            <td>
                                <asp:Label ID="LblTerr" runat="server" Text="Territorio" />
                            </td>
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>                                        
                                        <td style="padding-right:5px">
                                            <telerik:RadTextBox ID="txtTerritorio" runat="server" MaxLength="19" onpaste="return false" >
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="txtTerritorio_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadComboBox ID="cmbTer" runat="server" AutoPostBack="True" Width="300px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
                                                                DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                                EnableLoadOnDemand="True" Filter="Contains" 
                                                                OnClientSelectedIndexChanged="cmbTer_ClientSelectedIndexChanged">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>                                                                    
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                            </telerik:RadComboBox>       
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        --%>    
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="LblFechaini" runat="server" Text="A Partir de" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechaini" runat="server" Width="155px" Enabled="false">
                                    <Calendar ID="Calendar1" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" Enabled="false"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtFechaini" ValidationGroup="Mostrar"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr><td colspan="3"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList runat="server" ID="rdbTipoCliente" AutoPostBack="false" CssClass="content">
                                    <Items>
                                        <asp:ListItem Text="Clientes A" Value="1" />
                                        <asp:ListItem Text="Clientes B" Value="2" />
                                        <asp:ListItem Text="Clientes C" Value="3" />
                                        <asp:ListItem Text="Clientes D" Value="4" />
                                        <asp:ListItem Text="Todos" Value="5" />
                                    </Items>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

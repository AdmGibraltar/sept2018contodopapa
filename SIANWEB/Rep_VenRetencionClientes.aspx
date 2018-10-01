<%@ Page Title="Análisis de retencion de clientes" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_VenRetencionClientes.aspx.cs" Inherits="SIANWEB.Rep_VenRetencionClientes" %>

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
                        var txtTerritorio = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerritorio != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerritorio);
                        //Opcional, validaciones extras
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function numeroSiguiente() {
            }
            function refreshGrid() {
            }
            //            function cambio(id, value) {
            //                var txt1 = 0;
            //                var txt2 = 0;
            //                if (id = 1) {
            //                    txt1 = document.getElementById("txtColini1").value;
            //                    tx21 = document.getElementById("txtColfin1").value;
            //                    if (txt1 < txt2) {
            //                        document.getElementById("txtColini2").value = (txt2 + 1);
            //                    }
            //                }            
            //            }
            function cmbTer_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTer.ClientID %>'));
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
            <telerik:AjaxSetting AjaxControlID="txtColfin1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtColfin2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtColfin3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtColfin4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbGeneral">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
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
                    ValidationGroup="Mostrar" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
                        font-size: 8pt">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMensaje" runat="server" />
                            </td>
                            <td colspan="4" style="text-align: right">
                                <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                            </td>
                            <td width="150px" style="font-weight: bold">
                                <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                                    Width="150px" AutoPostBack="True">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblTerritorio" runat="server" Text="Territorio"></asp:Label>
                            </td>
                            <td>                                    
                                <telerik:RadTextBox ID="txtTerritorio" runat="server" MaxLength="5" onpaste="return false">                                        
                                    <ClientEvents OnBlur="txtTerritorio_OnBlur"  OnKeyPress="handleClickEvent" />
                                </telerik:RadTextBox>                                                     
                            </td>                    
                            <td colspan="3">
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
                            <td style="text-align: right" width="600px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblColini1" runat="server" Text="Columna 1 inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtColini1" runat="server" Text="1"
                                    MaxLength="30" Enabled="false">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblColfin1" runat="server" Text="Columna 1 final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtColfin1" runat="server" MaxLength="30"
                                    Text="20" OnTextChanged="txtColfin1_TextChanged" AutoPostBack="true">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <%--   <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*Columna final debe ser mayor a columna inicial"
                                 Display="Dynamic" ForeColor="Red" ControlToValidate="txtColfin1"  ValidationGroup="Mostrar"
                                    ControlToCompare="txtColini1" SetFocusOnError="True" 
                                    Operator="GreaterThan" Type="Integer"></asp:CompareValidator>          --%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblColini2" runat="server" Text="Columna 2 inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtColini2" runat="server" MaxLength="30"
                                    Text="21" Enabled="false">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblColfin2" runat="server" Text="Columna 2 final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtColfin2" runat="server" MaxLength="30"
                                    Text="50" OnTextChanged="txtColfin2_TextChanged" AutoPostBack="true">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <%--   <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*Columna final debe ser mayor a columna inicial"
                                 Display="Dynamic" ForeColor="Red" ControlToValidate="txtColfin2" ValidationGroup="Mostrar"
                                    ControlToCompare="txtColini2" SetFocusOnError="True" 
                                    Operator="GreaterThan" Type="Integer"></asp:CompareValidator>  --%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblColIni3" runat="server" Text="Columna 3 inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtColini3" runat="server" MaxLength="30"
                                    Text="51" Enabled="false">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblColfin3" runat="server" Text="Columna 3 final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtColfin3" runat="server" MaxLength="30"
                                    Text="99" OnTextChanged="txtColfin3_TextChanged" AutoPostBack="true">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <%--   <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*Columna final debe ser mayor a columna inicial"
                                 Display="Dynamic" ForeColor="Red" ControlToValidate="txtColfin3" ValidationGroup="Mostrar"
                                    ControlToCompare="txtColini3" SetFocusOnError="True" Operator="GreaterThan" 
                                     Type="Integer"></asp:CompareValidator>    --%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblColini4" runat="server" Text="Columna 4 inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtColini4" runat="server" MaxLength="30"
                                    Text="100" Enabled="false">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblColfin4" runat="server" Text="Columna 4 final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtColfin4" runat="server" MaxLength="30">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*Columna final debe ser mayor a columna inicial"
                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtColfin4" ValidationGroup="Mostrar"
                                    ControlToCompare="txtColini4" SetFocusOnError="True" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:CheckBox ID="chkTodos" runat="server" Enabled="false" Text="Todos los clientes" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblAgrupar" runat="server" Text="Agrupar por"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbTerritorio" runat="server" Text="Territorio" GroupName="agrupar"
                                    AutoPostBack="true" OnCheckedChanged="rbTerritorio_CheckedChanged" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbGeneral" runat="server" Text="General" AutoPostBack="true"
                                    GroupName="agrupar" Checked="true" OnCheckedChanged="rbGeneral_CheckedChanged" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>

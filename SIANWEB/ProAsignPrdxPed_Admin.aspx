<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProAsignPrdxPed_Admin.aspx.cs" Inherits="SIANWEB.ProAsignProductoPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsignacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <div id="divPrincipal" runat="server">
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
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
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
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="50">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Nombre del cliente"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:radtextbox onpaste="return false" id="txtNombre" runat="server" width="300px"
                                    maxlength="150">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:radtextbox>
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
                                <asp:Label ID="Label2" runat="server" Text="Cliente inicial" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox onpaste="return false" id="txtCliente1" runat="server"
                                    width="70px" maxlength="9" minvalue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Cliente final" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox onpaste="return false" id="txtCliente2" runat="server"
                                    width="70px" maxlength="9" minvalue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
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
                                <asp:Label ID="Label4" runat="server" Text="Fecha inicial" />
                            </td>
                            <td>
                                <telerik:raddatepicker id="txtFecha1" runat="server" width="100px">
                                    <Calendar ID="Calendar1" runat="server">
                                         
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput>
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:raddatepicker>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Fecha final" />
                            </td>
                            <td>
                                <telerik:raddatepicker id="txtFecha2" runat="server" width="100px">
                                    <Calendar ID="Calendar2" runat="server">
                                         
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput>
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:raddatepicker>
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
                                <asp:Label ID="Label7" runat="server" Text="Pedido inicial" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox onpaste="return false" id="txtPedido1" runat="server"
                                    width="70px" maxlength="9" minvalue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Pedido final" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox onpaste="return false" id="txtPedido2" runat="server"
                                    width="70px" maxlength="9" minvalue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:radgrid id="rgAsignacion" runat="server" autogeneratecolumns="False" gridlines="None"
                        onneeddatasource="rg_NeedDataSource" onitemcommand="rgPedido_ItemCommand" onitemcreated="rg_ItemCreated"
                        onpageindexchanged="rg_PageIndexChanged" mastertableview-nomasterrecordstext="No se encontraron registros."
                        pagesize="15" allowpaging="True">
                        <MasterTableView>
                             
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Pedido" UniqueName="column">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Fecha" HeaderText="Fecha" UniqueName="column1"
                                    DataFormatString="{0:dd/MM/yy}">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Terr." UniqueName="column3">
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="column4">
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="column5">
                                    <HeaderStyle Width="400px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn    DataField="Credito" HeaderText="Crédito" UniqueName="column6"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CreditoStr" HeaderText="Crédito" UniqueName="column6">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Asignar" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Asignar" CommandName="Asignar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                           
                        </MasterTableView>
                         <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                    </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function AbrirVentana_AsigPrdxPed_Asignar(Id, Id_Cte, Nom_Cte) {
                AbrirVentana_AsigPrdxPed(Id, Id_Cte, Nom_Cte);
                return false;
            }

            function AbrirVentana_AsigPrdxPed(Id, Id_Cte, Nom_Cte) {

                var oWnd = radopen("ProAsignPrdxPed.aspx?Id=" + Id + "&Id_Cte=" + Id_Cte + "&Nom_Cte=" + Nom_Cte + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_ProAsignPrdxPed");
                oWnd.center();
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        </script>
    </telerik:radcodeblock>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapGestionPrecios_Solicitud.aspx.cs" Inherits="SIANWEB.CapGestionPrecios_Solicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                function OpenWindowSolicitud( Id_Sol) {

                    AbrirVentana_Solicitud(Id_Sol);
                }
                function AbrirVentana_Solicitud(Id_Sol) {
                    //debugger;
                    var oWnd = radopen("CapGestionPrecios_SolicitudDet.aspx?Id_Sol=" + Id_Sol, "AbrirVentana_GPSolicitud");
                    oWnd.center();

                }

                function OpenWindowSolicitudAt(Unq, TipoOp) {

                        AbrirVentana_SolicitudAt(Unq, TipoOp);
                }
                function AbrirVentana_SolicitudAt(Unq, TipoOp) {
                    //debugger;
                    var oWnd = radopen("CapGestionPrecios_SolicitudAt.aspx?Unq=" + Unq + "&TipoOp=" + TipoOp , "AbrirVentana_AtSolicitud");
                    oWnd.center();

                }
                //--------------------------------------------------------------------------------------------------
                // Se ejecuata cuando el radWindow del detalle de factura se cierra,
                // Esta función es invocada por el evento 'radWindowClose'
                //--------------------------------------------------------------------------------------------------
                function CerrarWindow_ClientEvent(sender, eventArgs) {
                    //debugger;
                    var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                    refreshGrid_Nca('RebindGrid');
                }

                function refreshGrid() {
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    ajaxManager.ajaxRequest('RebindGrid');
                }

            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RAM1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtb1">
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
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rg1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <asp:HiddenField  runat ="server" ID="HD_GridRebind" />
        <div runat="server" id="divPrincipal">
            <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" >
                <Items>
                    <telerik:RadToolBarButton Width="20px" Enabled="False" />
             
                </Items>
            </telerik:RadToolBar>
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <asp:HiddenField ID="hiddenId" runat="server" />
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="lblCentro" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold" runat="server" id ="Centro" >
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
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
                                <td width="90">
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
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="middle">
                                    &nbsp;
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
                                    <asp:Label ID="Nosol" runat="server" Text="No. Solicitud"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="TxtId_Sol" runat="server" Width="90px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
   
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Convenio" runat="server" Text="Convenio"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="TxtNo_Convenio" runat="server" Width="90px" MaxLength="30"
                                        >
                             
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblEstatus" runat="server" Text="Estatus"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="153px" LoadingMessage="Cargando..." >
                                       <Items>
                                                 <telerik:RadComboBoxItem runat="server" Selected="True"  Text="-- Todos --" Value="-1" />
                                                 <telerik:RadComboBoxItem runat="server"  Text="Pendiente" Value="P" />
                                                 <telerik:RadComboBoxItem runat="server"  Text="Atendido" Value="A" />
                                                 <telerik:RadComboBoxItem runat="server"  Text="Cancelado" Value="C" />
                                      </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                        ToolTip="Buscar" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                          
                        </table>
                        <br />
                        
                        <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="True" OnNeedDataSource="rg1_NeedDataSource" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnItemCommand="rg1_ItemCommand"  OnPageIndexChanged="rg1_PageIndexChanged" OnItemDataBound="rg1_ItemDataBound">
                            <MasterTableView ClientDataKeyNames="Id_Sol">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                 <telerik:GridBoundColumn DataField="Sol_Unique" HeaderText="Sol_Unique" UniqueName="Sol_Unique" Display="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridBoundColumn>
                                
                                    <telerik:GridBoundColumn DataField="Id_Sol" HeaderText="No. Solicitud" UniqueName="Id_Sol">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridBoundColumn>
                                   
                                    <telerik:GridBoundColumn DataField="Sol_Fecha" HeaderText="Fecha" UniqueName="Sol_Fecha"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Sol_EstatusStr" HeaderText="Estatus" UniqueName="Sol_Estatus" >
                                    </telerik:GridBoundColumn>
                          
                                    <telerik:GridBoundColumn DataField="CD_Nombre" HeaderText="CDI" UniqueName="Cd_Nombre">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Sol_UNombre" HeaderText="Usuario" UniqueName="Sol_UNombre">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                     
                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px" UniqueName="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="Editar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                             
                                    <telerik:GridButtonColumn CommandName="Enviar" HeaderText="Enviar" ConfirmDialogType="RadWindow"
                                        ConfirmText="¿Desea enviar la solicitud?" Text="Enviar" UniqueName="Enviar"
                                        Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="email_grid"
                                        ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    </telerik:GridButtonColumn>

                                  <%--    <telerik:GridTemplateColumn HeaderText="Atender" HeaderStyle-HorizontalAlign="Center" UniqueName="Atender"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Atender" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Atender" CommandName="Atender" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridTemplateColumn>--%>
                                    <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Cancelar" ConfirmDialogType="RadWindow"
                                        ConfirmText="¿Está seguro de cancelar la solicitud?" ConfirmDialogHeight="150px"
                                        ConfirmDialogWidth="350px" Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton"
                                        ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

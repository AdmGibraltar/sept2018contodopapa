<%@ Page Title="Clientes vinculados" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapGestionPrecios_Vinculados.aspx.cs" Inherits="SIANWEB.CapGestionPrecios_Vinculados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
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
                    //debugger;
                    GetRadWindow().Close();
                }

                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind() {
                    //debugger;
                    GetRadWindow().Close();
                    //GetRadWindow().BrowserWindow.refreshGrid();
                }
                function CloseWindowA(mensaje) {
                    //debugger;
                    var cerrarWindow = radalert(mensaje, 330, 150);
                    cerrarWindow.add_close(
                            function () {
                                GetRadWindow().Close();
                            });
                }

                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf("ctl00$CPH$rtb1") != -1)
                        args.set_enableAjax(false);
                }

            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
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
        <div runat="server" id="divPrincipal">
            <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick" >
                <Items>
                    <telerik:RadToolBarButton Width="20px" Enabled="False" />
                    <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Exportar a excel" CssClass="excel"
                        ImageUrl="~/Imagenes/blank.png" />
                </Items>
            </telerik:RadToolBar>
            <table id="TblEncabezado" 
                style="font-family: verdana; font-size: 8pt; visibility: hidden;" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <asp:HiddenField ID="hiddenId" runat="server" />
                        <asp:HiddenField runat="server" ID="HFId_PC"/>
                         <asp:HiddenField runat="server" ID="HFId_Cd"/>
                    <%--    <asp:HiddenField runat="server" ID="HFCapUsuario"/>
                        <asp:HiddenField runat="server" ID="HFSol_Unique"/>--%>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="lblCentro" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server"
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
                                <td class="style1">
                                    </td>
                                <td class="style1">
                                    </td>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style1">
                                    </td>
                                <td class="style1">
                                    </td>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style1">
                                    </td>
                                <td class="style1">
                                    </td>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style1">
                                    &nbsp;
                                    </td>
                            </tr>
                       
                                        <tr>
                                <td>
                                   <asp:Label ID="Label5" runat="server" Text="No. Convenio:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblPC_NoConvenio" runat="server" ></asp:Label></td>
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
                                    <tr>
                                <td>
                                   <asp:Label ID="Label6" runat="server" Text="Nombre convenio:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblPC_Nombre" runat="server" ></asp:Label></td>
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
                                      <tr>
                                <td>
                                   <asp:Label ID="Label7" runat="server" Text="Categoría:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblId_CatStr" runat="server" ></asp:Label></td>
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
                        <telerik:RadSplitter ID="RadSplitter4" runat="server" Height="270px" BorderSize="0" >
                           <telerik:RadPane ID="RadPane3" runat="server"  Height="250px" width="850px"
                                                BorderStyle="None">
                        <telerik:RadGrid ID="rgVinculados" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                               OnNeedDataSource="rgVinculados_NeedDataSource" CellSpacing="0" OnItemDataBound="rgVinculados_ItemDataBound"
                               OnItemCommand="rgVinculados_ItemCommand">
                       
                              <MasterTableView >
                                 <CommandItemSettings ShowRefreshButton="false" />
                                <Columns>
                                  <telerik:GridBoundColumn DataField="Id_Sol"  UniqueName="Id_Sol" visible="false"  >
                                    </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="Id_PC"  UniqueName="Id_PC" visible="false"  >
                                    </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="Id_Cte"   >
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px"  />
                                    </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="Sol_CteNombre" HeaderText="Cliente" UniqueName="Sol_CteNombre"   >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="200px"  />
                                    </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="SolTer_Nombre" HeaderText="Territorio" UniqueName="SolTer_Nombre"   >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="200px"  />
                                    </telerik:GridBoundColumn>

                                      <telerik:GridBoundColumn DataField="Sol_UsuFinal" HeaderText="Usuario final" UniqueName="Sol_UsuFinal"   >
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px"  />
                                    </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="CDI" HeaderText="CDI (Zona)" UniqueName="CDI"   >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="150px"  />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Sol_UNombre" HeaderText="Usuario" UniqueName="Sol_UNombre"   >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="150px"  />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Cancelar"  ConfirmDialogType="RadWindow"
                                           ConfirmText="¿Está seguro que desea desvíncular al cliente?" ConfirmDialogHeight="150px"
                                            ConfirmDialogWidth="350px"
                                           Text="Cancelar" UniqueName="Cancelar" Visible="True" ButtonType="ImageButton" 
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="50px" />
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
                        </telerik:RadPane>
                          </telerik:RadSplitter>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </div>
</asp:Content>

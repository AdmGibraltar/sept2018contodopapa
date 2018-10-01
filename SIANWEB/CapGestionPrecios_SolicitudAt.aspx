<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapGestionPrecios_SolicitudAt.aspx.cs" Inherits="SIANWEB.CapGestionPrecios_SolicitudAt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                                RefreshParentPage();
                            });
                }
                function RefreshParentPage() {
                    GetRadWindow().BrowserWindow.location.reload();
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
        <div runat="server" id="divPrincipal">
            <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick" >
                <Items>
                    <telerik:RadToolBarButton Width="20px" Enabled="False" />
                    <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
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
                            <asp:HiddenField runat="server" ID="HFTipoOp"/>
                        <asp:HiddenField runat="server" ID="HFCapUsuario"/>
                        <asp:HiddenField runat="server" ID="HFSol_Unique"/>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="lblCentro" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
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
                                   <asp:Label ID="Label3" runat="server" Text="Folio:" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                  <asp:Label ID="LblId_Sol" runat="server" ></asp:Label></td>
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
                                    <asp:Label ID="Nosol" runat="server" Text="Sucursal:" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblCd_Nombre" runat="server" ></asp:Label></td>
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
                                   <asp:Label ID="Label1" runat="server" Text="Solicitante:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblU_Nombre" runat="server" ></asp:Label></td>
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
                                   <asp:Label ID="Label2" runat="server" Text="Correo:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblU_Correo" runat="server" ></asp:Label></td>
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
                                   <asp:Label ID="Label4" runat="server" Text="Fecha solicitud:" Font-Bold="True"></asp:Label></td>
                                <td>
                                  <asp:Label ID="LblSol_Fecha" runat="server" ></asp:Label></td>
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
                           <telerik:RadPane ID="RadPane3" runat="server"  Height="250px" width="750px"
                                                BorderStyle="None">
                        <telerik:RadGrid ID="rgSolicitudDet" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                               OnNeedDataSource="rgSolicitudDet_NeedDataSource"  OnItemDataBound="rgSolicitudDet_ItemDataBound" CellSpacing="0">
                       
                              <MasterTableView >
                                 <CommandItemSettings ShowRefreshButton="false" />
                                <Columns>
                                   <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="Id_Cte"   >
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px"  />
                                    </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="Sol_CteNombre" HeaderText="Cliente" UniqueName="Sol_CteNombre"   >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="200px"  />
                                    </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Núm." UniqueName="Id_Ter"   >
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px"  />
                                    </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="SolTer_Nombre" HeaderText="Territorio" UniqueName="SolTer_Nombre"   >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="200px"  />
                                    </telerik:GridBoundColumn>

                                      <telerik:GridBoundColumn DataField="Sol_UsuFinal" HeaderText="Usuario final" UniqueName="Sol_UsuFinal"   >
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px"  />
                                    </telerik:GridBoundColumn>

                                         <telerik:GridTemplateColumn HeaderText="Autorizar" UniqueName="Autorizar">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkAutorizar" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "SolD_Estatus")) == "A" ? true : false %>'
                                                GroupName="autoriza" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkAutorizarAll" runat="server" OnCheckedChanged="chkAutorizarAll_CheckedChanged"
                                                GroupName="autorizaAll" AutoPostBack="true" Text="Autorizar" />
                                        </HeaderTemplate>
                                    </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Rechazar" UniqueName="Rechazado">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkRechazar" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "SolD_Estatus")) == "R" ? true : false %>'
                                                GroupName="autoriza"/>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkRechazarAll" runat="server" OnCheckedChanged="chkRechazarAll_CheckedChanged"
                                                GroupName="autorizaAll" AutoPostBack="true" Text="Rechazar" />
                                        </HeaderTemplate>
                                    </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn HeaderText="Cancelado" UniqueName="Cancelado">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkCancelado" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "SolD_Estatus")) == "X" ? true : false %>'
                                                />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkCanceladoAll" runat="server" 
                                                 Text="Cancelado" />
                                        </HeaderTemplate>
                                    </telerik:GridTemplateColumn>
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

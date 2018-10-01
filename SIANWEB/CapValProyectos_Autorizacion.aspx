<%@ Page Title="Autorización de valuación de proyectos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapValProyectos_Autorizacion.aspx.cs" Inherits="SIANWEB.CapValProyectos_Autorizacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadToolBar runat="server" ID="rtb1" AutoPostBack="True" dir="rtl" Width="100%"
            OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" CssClass="mail" ToolTip="Correo" ImageUrl="~/Imagenes/blank.png"
                    Enabled="false" Owner="rtb1" />
                <telerik:RadToolBarButton CommandName="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" Enabled="false" />
                <telerik:RadToolBarButton CommandName="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" Enabled="false" />
                <telerik:RadToolBarButton CommandName="undo" CssClass="undo" ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png"
                    Enabled="false">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="save" ToolTip="Guardar" CssClass="save" ImageUrl="~/Imagenes/blank.png"
                    ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" ToolTip="Nuevo" CssClass="new" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rtb1">
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
                <telerik:AjaxSetting AjaxControlID="rg1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div id="divPrincipal" runat="server">
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <br />
            <table style="font-family: verdana; font-size: 8pt;">
                <!-- Tabla principal--->
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <!--Tab 1  Tabla 1-->
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Folio" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblFolio" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Sucursal" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblSucursal" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Usuario que solicita" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblSolicitaId" runat="server"></asp:Label>
                                    <asp:Label ID="lblSolicitaNombre" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Fecha de solicitud"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaSol" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Fecha de autorización" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblAutorizacion" runat="server" Text=""></asp:Label>
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
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="rg1" runat="server" OnNeedDataSource="rg1_NeedDataSource" AutoGenerateColumns="False"
                            GridLines="None" OnPageIndexChanged="rg1_PageIndexChanged" PageSize="15" AllowPaging="True"
                            MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                            <MasterTableView>
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Det" DataField="Id_VapDet" Visible="false" UniqueName="Id_VapDet">
                                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Vap_Tipo" HeaderText="Vap_Tipo" UniqueName="Vap_Tipo" Visible="false">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Vap_TipoStr" HeaderText="Tipo" UniqueName="Vap_TipoStr">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Clave" DataField="Id_Prd" UniqueName="Id_Prd">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Producto" DataField="Prd_Descripcion" UniqueName="Prd_Descripcion">
                                        <HeaderStyle Width="250px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Prd_Presentacion" HeaderText="Presen." UniqueName="Prd_Presentacion">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="250px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Unidad" DataField="Prd_UniNe" 
                                        UniqueName="Prd_UniNe">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Vap_Cantidad" HeaderText="Cantidad" UniqueName="Vap_Cantidad">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Vap_Costo" HeaderText="Costo" UniqueName="Vap_Costo">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Vap_Precio" HeaderText="Precio" UniqueName="Vap_Precio">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Costo" UniqueName="Autoriza">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkAutoriza" runat="server" GroupName="autoriza" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado")) %>'
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkAutoriza_All" runat="server" Text="Autorizar" GroupName="autorizaAll"
                                                TextAlign="Left" OnCheckedChanged="chkAutoriza_CheckedChanged" AutoPostBack="true"
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Precio" UniqueName="Rechaza">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkRechaza" runat="server" GroupName="autoriza" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) %>'
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkRechaza_All" runat="server" Text="Rechazar" GroupName="autorizaAll"
                                                TextAlign="Left" OnCheckedChanged="chkRechaza_CheckedChanged" AutoPostBack="true"
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Pendiente" UniqueName="Pendiente">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chkPendiente" runat="server" GroupName="autoriza" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>'
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:RadioButton ID="chkPendiente_All" runat="server" GroupName="autorizaAll" Text="Pendiente"
                                                TextAlign="Left" OnCheckedChanged="chkPendiente_CheckedChanged" AutoPostBack="true"
                                                Enabled='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Autorizado"))||Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Rechazado")) ? false : true %>' />
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

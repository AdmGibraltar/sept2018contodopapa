<%@ Page Title="Autorización de ajuste de base instalada" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapAjusteSolicitud.aspx.cs" Inherits="SIANWEB.CapAjusteSolicitud" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="HD_Guardar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAutBaseInstalada" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="HD_Guardar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAutBaseInstalada">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAutBaseInstalada" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="HD_Guardar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                    <telerik:AjaxUpdatedControl ControlID="HD_Guardar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
        font-size: 8pt">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="lblEtiquetaCentro" runat="server" Text="Centro de distribución"></asp:Label>
            </td>
            <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <br />
    <div runat="server" id="divPrincipal">
        <table style="font-family: verdana; font-size: 8pt;">
            <!-- Tabla principal--->
            <tr>
                <td>
                    <asp:HiddenField runat="server" ID="HD_Guardar" Value="0" />
                </td>
                <td>
                    <table>
                        <!--Tab 1  Tabla 1-->
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Sucursal"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSucursal" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Persona que solicita"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSolicita" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Autorización"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizacion" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" visible="false">
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Número de autorización"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNumAut" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Folio"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Día/mes/año"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFecha" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:HiddenField ID="HF_ID" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgAutBaseInstalada" runat="server" AutoGenerateColumns="False"
                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        AllowPaging="True" AllowSorting="False" HeaderStyle-HorizontalAlign="Center"
                        AllowMultiRowSelection="True" OnNeedDataSource="rgAutBaseInstalada_NeedDataSource"
                        OnPageIndexChanged="rgAutBaseInstalada_PageIndexChanged" OnItemDataBound="rgAutBaseInstalada_DataBound">
                        <SortingSettings SortedAscToolTip="Orden acendente" SortedDescToolTip="Orden decendente"
                            SortToolTip="Click para reordenar" />
                        <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="Lista" HideStructureColumns="true"
                            ExportOnlyData="true">
                            <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Lista" Title="Lista" />
                        </ExportSettings>
                        <MasterTableView Name="Master" CommandItemDisplay="none" DataKeyNames="Id_Emp,Id_Cd,Id_Abi,Id_AbiDet">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridClientSelectColumn UniqueName="selectColumn" Text="Autorizar">
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Abi" HeaderText="Id_Abi" Display="false" UniqueName="Id_Abi">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_AbiDet" HeaderText="Id_AbiDet" Display="false"
                                    UniqueName="Id_AbiDet">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Abi_Tipo" HeaderText="Abi_Tipo" Display="false"
                                    UniqueName="Abi_Tipo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Tipo mov." DataField="Abi_TipoStr">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Terr. origen" DataField="Id_Ter_Origen">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cte. origen" DataField="Id_Cte_Origen">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Prod. origen" DataField="Id_Prd_Origen">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cant. origen act." DataField="Abi_CantActual_Origen">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cant. origen quitar" DataField="Abi_CantQuitar_Origen">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Terr. destino" DataField="Id_Ter_Destino">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cte. destino" DataField="Id_Cte_Destino">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Prod. destino" DataField="Id_Prd_Destino">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cant. destino act." DataField="Abi_CantActual_Destino">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cant. destino mod." DataField="Abi_CantQuitar_Destino">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Explicación del caso" DataField="Abi_ExplicacionCaso">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Estatus" DataField="Abi_Estatus" Display="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstatus" runat="server" Text='<%# Bind("Abi_Estatus" ) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Estatus" DataField="Abi_EstatusStr">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

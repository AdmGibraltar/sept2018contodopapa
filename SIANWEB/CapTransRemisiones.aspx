<%@ Page Title="Transmisión de remisiones" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CapTransRemisiones.aspx.cs" Inherits="SIANWEB.CapTransRemisiones" %>
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
    <telerik:RadAjaxManager ID="RAM1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <br />
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td style="text-align: right" width="150px">
                Centro de distribucion
            </td>
          <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px"   runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
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
                                Sucursal
                            </td>
                            <td>
                                <asp:Label ID="lblSucursal" runat="server" Text="110-KEY QUIMICA, S.A. (MTY)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Folio
                            </td>
                            <td>
                                <asp:Label ID="lblFolio" runat="server" Text="10063355"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                dia/mes/año
                            </td>
                            <td>
                                <asp:Label ID="lblFecha2" runat="server" Text="31 7 2008"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:HiddenField ID="HF_ID" runat="server" /></td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rg1" runat="server" OnNeedDataSource="rg1_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnItemCommand="rg1_ItemCommand" 
                        OnPageIndexChanged="rg1_PageIndexChanged" PageSize="15" AllowPaging="true" AllowAutomaticUpdates="True"
                        MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                        <MasterTableView>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderText="Secuencia" DataField="x">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Orden de compra" DataField="x">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Num. prod." DataField="x">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Desc. prod." DataField="x">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Presen." DataField="x">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Unidades" DataField="x">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cantidad remisión" DataField="x">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cantidad recibida" DataField="x">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cantidad por recibir" DataField="x">
                                </telerik:GridBoundColumn>
<%--                                <telerik:GridBoundColumn DataField="x" Visible="false">
                                </telerik:GridBoundColumn>--%>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

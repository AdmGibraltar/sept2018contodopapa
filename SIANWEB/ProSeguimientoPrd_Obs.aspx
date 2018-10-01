<%@ Page Title="Seguimiento de productos" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="ProSeguimientoPrd_Obs.aspx.cs" Inherits="SIANWEB.CatSeguimientoProductos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgSeguimiento"> 
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSeguimiento" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="RadToolBar1_ButtonClick" Visible="false">
        <Items>
            <telerik:RadToolBarButton CommandName="Confirmar" Value="Confinmar" ToolTip="Confirmar"
                CssClass="aceptar" ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="Eliminar" Value="Eliminar" ToolTip="Eliminar"
                CssClass="baja" ImageUrl="Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server" style="width: 900; height: 700;">
        <table style="font-family: Verdana; font-size: 8pt; width: 900; height: 700">
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblProducto" runat="server" Text="Producto"></asp:Label>
                </td>
                <td>
                    <telerik:RadNumericTextBox ID="txtProducto" runat="server" MaxLength="9" MinValue="0"
                        Width="50px" ReadOnly="true">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        <ClientEvents OnKeyPress="SoloNumerico" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtPrdDescripcion" Runat="server" ReadOnly="True" 
                        Width="300px">
                    </telerik:RadTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120">
                    <asp:HiddenField ID="HiddenRebind" runat="server" />
                    <asp:HiddenField ID="HF_ID" runat="server" />
                </td>
            </tr>
            </table>
        <telerik:RadGrid ID="rgSeguimiento" runat="server" AutoGenerateColumns="False" GridLines="None"
            OnItemCommand="rgSeguimiento_ItemCommand" OnNeedDataSource="rgSeguimiento_NeedDataSource"
            OnPageIndexChanged="rgSeguimiento_PageIndexChanged" Width="550px" Height="278px"
            OnItemCreated="rgSeguimiento_ItemCreated" AllowPaging="True">
            <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                EditMode="InPlace" PageSize="8">
                <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridTemplateColumn DataField="Id_SegPrd" HeaderText="Id" UniqueName="Id_SegPrd">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSegId1" runat="server" Text='<%# Bind("Id_SegPrd") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblSegId2" runat="server" Text='<%# Bind("Id_SegPrd") %>'></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle Width="10px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Seg_fecha" HeaderText="Fecha" UniqueName="Seg_fecha">
                        <ItemTemplate>
                            <asp:Label ID="lblFecha" runat="server" Width="75px" Text='<%# Bind("Seg_fecha","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadDatePicker ID="rdFecha" runat="server" Width="100px" DbSelectedDate='<%# Bind("Seg_fecha") %>'>
                                <DatePopupButton ToolTip="Abrir calendario" />
                                <Calendar ID="Calendar1" runat="server">
                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                        TodayButtonCaption="Hoy">
                                    </FastNavigationSettings>
                                </Calendar>
                                <DateInput runat="server">
                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                </DateInput>
                            </telerik:RadDatePicker>
                        </EditItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Seg_Comentarios" HeaderText="Observaciones"
                        UniqueName="Seg_Comentarios">
                        <HeaderStyle Width="250px" HorizontalAlign="Center" />
                        <ItemStyle Width="250px" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblObservaciones" runat="server" OnKeyPress="SoloAlfanumerico" Text='<%# DataBinder.Eval(Container.DataItem, "Seg_Comentarios") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox ID="txtObservaciones" runat="server" Width="250px" Text='<%# Bind("Seg_Comentarios") %>'>
                                <ClientEvents OnKeyPress="SoloAlfanumerico"  />
                            </telerik:RadTextBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                        InsertText="Aceptar" UpdateText="Actualizar" UniqueName="EditCommandColumn">
                        <HeaderStyle Width="70px" />
                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                    </telerik:GridEditCommandColumn>--%>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                        EditText="Editar" CancelText="Cancelar" InsertText="Aceptar">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle Width="70px" HorizontalAlign="Center" Wrap="False" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                        ConfirmText="¿Borrar esta observación?" Text="Borrar" UniqueName="DeleteColumn">
                        <HeaderStyle Width="30px" />
                        <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
            <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
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
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                //debugger;

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }


                switch (button.get_value()) {
                    case 'new':

                        break;
                    case 'save':

                        break;
                }

                args.set_cancel(!continuarAccion);
            }

           
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

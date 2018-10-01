<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CapFacturaRemisiones.aspx.cs" Inherits="SIANWEB.CapFacturaRemisiones" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<%@ Import Namespace="System" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var lblVal_txtId_RemClientId = '';
            var txtId_RemClientId = '';

            //Validación del formulario de insercion/edición de registro en un RadGrid.
            //param: accion --> indica que tipo de operación se esta realizando, puede traer los valores 'insertar' o 'actualizar'
            function ValidaFormEdit(accion) {

                var continuarAccion = true;

                //debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var lblVal_txtId_Rem = document.getElementById(lblVal_txtId_RemClientId);
                var txtId_Rem = $find(txtId_RemClientId);

                //Limpiar contenedores de mensaje de validación
                lblVal_txtId_Rem.innerHTML = '';

                //validar producto
                if (txtId_Rem != null) {
                    if (txtId_Rem.get_textBoxValue() == '') {
                        lblVal_txtId_Rem.innerHTML = '*Campo requerido';
                        continuarAccion = false
                    }
                }
                else
                    continuarAccion = false

                return continuarAccion;
            }



            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'save':
                        var radGrid = $find('<%= rgRemisiones.ClientID %>');
                        var MasterTable = radGrid.get_masterTableView();
                        var length = MasterTable.get_dataItems().length;

                        if (length == '' && length == 0) {
                            var alertaFEsp = radalert('El sistema canceló el proceso de facturación de remisiones debido a que no capturó ninguna remisión', 330, 150, tituloMensajes);
                            alertaFEsp.add_close(
                                    function () {
                                        GetRadWindow_FacturaRemisiones().BrowserWindow.ActivarBanderaRebind_FacturaRemisiones();
                                        continuarAccion = false;
                                        args.set_cancel(true);
                                        CloseAndRebind_FacturaRemisiones();
                                    });
                        }
                        else {
                            GetRadWindow_FacturaRemisiones().BrowserWindow.ActivarBanderaRebind_FacturaRemisiones();
                            continuarAccion = false;
                            args.set_cancel(true);
                            CloseAndRebind_FacturaRemisiones();
                        }
                        break;
                }
            }

            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow_FacturaRemisiones() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow_FacturaRemisiones(mensaje) {
                //debugger;
                //                GetRadWindow_FacturaRemisiones().BrowserWindow.ActivarBanderaRebind_FacturaRemisiones();
                //                CloseAndRebind_FacturaPedido();

                var cerrarWindow = radalert(mensaje, 600, 10, tituloMensajes);
                cerrarWindow.add_close(
                                    function () {
                                        //debugger;
                                        //GetRadWindow().Close();
                                        CloseAndRebind_FacturaRemisiones();
                                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind_FacturaRemisiones() {
                //debugger;
                GetRadWindow_FacturaRemisiones().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage_FacturaRemisiones() {
                GetRadWindow_FacturaRemisiones().BrowserWindow.location.reload();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRemisiones" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="HD_Id_Rem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtId_Rem_TextChanged">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRemisiones" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="HD_Id_Rem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgRemisiones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRemisiones" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="HD_Id_Rem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
        </Items>
    </telerik:RadToolBar>
    <div class="formulario">
        <div runat="server" id="divPrincipal">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <asp:HiddenField runat="server" ID="HD_Id_Rem" Value="0" />
            <div style="display: none">
                <table width="98%">
                    <tr>
                        <td style="width: 50%">
                            <table>
                                <tr>
                                    <td style="width: 60px;">
                                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:"></asp:Label>
                                    </td>
                                    <td height="25px" style="background-color: #F2F2F2; width: 40px; text-align: center">
                                        <asp:Label ID="lblEmpresaId" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td height="25px" style="background-color: #F2F2F2; width: 150px;">
                                        <asp:Label ID="lblEmpresaNombre" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%">
                            <table>
                                <tr>
                                    <td style="width: 60px;">
                                        <asp:Label ID="lblRegion" runat="server" Text="Región:"></asp:Label>
                                    </td>
                                    <td height="25px" style="background-color: #F2F2F2; width: 40px; text-align: center">
                                        <asp:Label ID="lblRegionId" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td height="25px" style="background-color: #F2F2F2; width: 150px;">
                                        <asp:Label ID="lblRegionNombre" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <table>
                                <tr>
                                    <td style="width: 60px;">
                                        <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:"></asp:Label>
                                    </td>
                                    <td height="25px" style="background-color: #F2F2F2; width: 40px; text-align: center">
                                        <asp:Label ID="lblSucursalId" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td height="25px" style="background-color: #F2F2F2; width: 150px;">
                                        <asp:Label ID="lblSucursalNombre" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%">
                            <table>
                                <tr>
                                    <td style="width: 60px;">
                                        <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                                    </td>
                                    <td height="25px" style="background-color: #F2F2F2; width: 40px; text-align: center">
                                        <asp:Label ID="lblUsuarioId" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td height="25px" style="background-color: #F2F2F2; width: 150px;">
                                        <asp:Label ID="lblUsuarioNombre" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <telerik:RadGrid ID="rgRemisiones" runat="server" GridLines="None" AllowPaging="True"
                AutoGenerateColumns="False" Width="100%" OnNeedDataSource="rgRemisiones_NeedDataSource"
                OnDeleteCommand="rgRemisiones_DeleteCommand" OnInsertCommand="rgRemisiones_InsertCommand"
                OnUpdateCommand="rgRemisiones_UpdateCommand" OnItemDataBound="rgRemisiones_ItemDataBound"
                OnItemCommand="rgRemisiones_ItemCommand" OnPageIndexChanged="rgRemisiones_PageIndexChanged">
                <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Rem" EditMode="InPlace"
                    HorizontalAlign="NotSet" AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros.">
                    <ExpandCollapseColumn Visible="True">
                    </ExpandCollapseColumn>
                    <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" ReadOnly="true"
                            UniqueName="Id_Emp">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" ReadOnly="true"
                            UniqueName="Id_Cd">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Remisión" DataField="Id_Rem" UniqueName="Id_Rem">
                            <ItemTemplate>
                                <asp:Label ID="lblId_Rem" runat="server" Text='<%# Eval("Id_Rem") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadNumericTextBox ID="txtId_Rem" runat="server" Width="50px" MaxLength="9"
                                    Text='<%# Eval("Id_Rem") %>' OnTextChanged="txtId_Rem_TextChanged" AutoPostBack="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:Label ID="lblVal_txtId_Rem" runat="server" ForeColor="#FF0000"></asp:Label>
                            </EditItemTemplate>
                            <HeaderStyle Width="70px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Fecha" DataField="Rem_Fecha" UniqueName="Rem_Fecha">
                            <ItemTemplate>
                                <asp:Label ID="lblRem_Fecha" runat="server" Text='<%# Convert.ToDateTime(Eval("Rem_Fecha")).ToString("dd/MM/yyyy") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblRem_FechaEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rem_Fecha") is DBNull ? "" : Convert.ToDateTime(Eval("Rem_Fecha")).ToString("dd/MM/yyyy")  %>' />
                            </EditItemTemplate>
                            <HeaderStyle Width="80px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Cliente" DataField="Id_Cte" UniqueName="Id_Cte">
                            <ItemTemplate>
                                <asp:Label ID="lblId_Cte" runat="server" Text='<%# Eval("Id_Cte") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblId_CteEdit" runat="server" Text='<%# Eval("Id_Cte") %>' />
                            </EditItemTemplate>
                            <HeaderStyle Width="80px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Nombre" DataField="NombreCliente" UniqueName="NombreCliente">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreCliente" runat="server" Text='<%# Eval("NombreCliente") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblNombreClienteEdit" runat="server" Text='<%# Eval("NombreCliente") %>' />
                            </EditItemTemplate>
                            <HeaderStyle Width="152px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Estatus" DataField="Rem_Estatus" UniqueName="Rem_Estatus"
                            Display="False">
                            <ItemTemplate>
                                <asp:Label ID="lblRem_Estatus" runat="server" Text='<%# Eval("Rem_Estatus") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblRem_EstatusEdit" runat="server" Text='<%# Eval("Rem_Estatus") %>' />
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Estatus" DataField="Rem_EstatusStr" UniqueName="Rem_EstatusStr">
                            <ItemTemplate>
                                <asp:Label ID="lblRem_EstatusStr" runat="server" Text='<%# Eval("Rem_EstatusStr") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblRem_EstatusStrEdit" runat="server" Text='<%# Eval("Rem_EstatusStr") %>' />
                            </EditItemTemplate>
                            <HeaderStyle Width="80px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                            EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar">
                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ConfirmText="¿Desea quitar esta remisión de la lista?"
                            ConfirmDialogType="RadWindow" ButtonType="ImageButton" CommandName="Delete" Text="Eliminar"
                            UniqueName="DeleteColumn">
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </MasterTableView>
                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                    PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                    PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings>
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CapRemisiones_Especial.aspx.cs" Inherits="SIANWEB.CapRemisiones_Especial" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            } 

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var lblRem_CantidadClientId = '';
            var txtRem_CantidadClientId = '';
            var lbl_cmbProductoClientId = '';
            var txtId_PrdClientId = '';
            var lblVal_txtPrd_DescripcionClientId = '';
            var txtPrd_DescripcionClientId = '';
            var lblVal_txtPrd_PresentacionClientId = '';
            var txtPrd_PresentacionClientId = '';
            var lblVal_txtPrd_UniNeClientId = '';
            var txtPrd_UniNeClientId = '';
            var lblVal_txtRem_PrecioClientId = '';
            var txtRem_PrecioClientId = '';
            //Validación del formulario de insercion/edición de registro en un RadGrid.
            //param: accion --> indica que tipo de operación se esta realizando, puede traer los valores 'insertar' o 'actualizar'
            function ValidaFormEdit(accion) {
                var continuarAccion = true;
                //debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var lblRem_Cantidad = document.getElementById(lblRem_CantidadClientId);
                var txtRem_Cantidad = $find(txtRem_CantidadClientId);
                var lbl_cmbProducto = document.getElementById(lbl_cmbProductoClientId);
                var txtId_Prd = $find(txtId_PrdClientId);
                var lblVal_txtPrd_Descripcion = document.getElementById(lblVal_txtPrd_DescripcionClientId);
                var txtPrd_Descripcion = $find(txtPrd_DescripcionClientId);
                var lblVal_txtPrd_Presentacion = document.getElementById(lblVal_txtPrd_PresentacionClientId);
                var txtPrd_Presentacion = $find(txtPrd_PresentacionClientId);
                var lblVal_txtPrd_UniNe = document.getElementById(lblVal_txtPrd_UniNeClientId);
                var txtPrd_UniNe = $find(txtPrd_UniNeClientId);
                var lblVal_txtRem_Precio = document.getElementById(lblVal_txtRem_PrecioClientId);
                var txtRem_Precio = $find(txtRem_PrecioClientId);

                //Limpiar contenedores de mensaje de validación
                lblRem_Cantidad.innerHTML = '';
                lbl_cmbProducto.innerHTML = '';
                lblVal_txtPrd_Descripcion.innerHTML = '';
                lblVal_txtPrd_Presentacion.innerHTML = '';
                //lblVal_txtPrd_UniNe.innerHTML = '';
                lblVal_txtRem_Precio.innerHTML = '';

                //---------------------------------
                //inicia validaciones de formulario
                //---------------------------------
                //validar producto
                //                if (txtId_Prd != null) {
                //                    if (txtId_Prd.get_textBoxValue() == '') {
                //                        lbl_cmbProducto.innerHTML = '*Campo requerido';
                //                        continuarAccion = false
                //                    }
                //                }
                //                else
                //                    continuarAccion = false

                //validar descripcion
                //                if (txtPrd_Descripcion != null) {
                //                    if (txtPrd_Descripcion.get_textBoxValue() == '') {
                //                        lblVal_txtPrd_Descripcion.innerHTML = '*Campo requerido';
                //                        continuarAccion = false
                //                    }
                //                }
                //                else
                //                    continuarAccion = false

                //validar presentación
                //                if (txtPrd_Presentacion != null) {
                //                    if (txtPrd_Presentacion.get_textBoxValue() == '') {
                //                        lblVal_txtPrd_Presentacion.innerHTML = '*Campo requerido';
                //                        continuarAccion = false
                //                    }
                //                }
                //                else
                //                    continuarAccion = false

                //validar unidades
                //                if (txtPrd_UniNe != null) {
                //                    if (txtPrd_UniNe.get_textBoxValue() == '') {
                //                        lblVal_txtPrd_UniNe.innerHTML = '*Campo requerido';
                //                        continuarAccion = false
                //                    }
                //                }
                //                else
                //                    continuarAccion = false

                //validar precio
                //                if (txtRem_Precio != null) {
                //                    if (txtRem_Precio.get_textBoxValue() == '') {
                //                        lblVal_txtRem_Precio.innerHTML = '*Campo requerido';
                //                        continuarAccion = false
                //                    }
                //                }
                //                else
                //                    continuarAccion = false

                //validar cantidad
                //                if (txtRem_Cantidad != null) {
                //                    if (txtRem_Cantidad.get_textBoxValue() == '') {
                //                        lblRem_Cantidad.innerHTML = '*Campo requerido';
                //                        continuarAccion = false
                //                    }
                //                    else
                //                        if (parseInt(txtRem_Cantidad.get_textBoxValue()) < 0) {
                //                            lblRem_Cantidad.innerHTML = '*Campo requerido. El valor debe ser mayor o igual a 0';
                //                            continuarAccion = false
                //                        }
                //                }
                //                else
                //                    continuarAccion = false
                return continuarAccion
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                var continuarAccion = true;
                var button = args.get_item();

                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'save':
                        //validar que los montos de la factura especial y la factura original coincidan   
                        var HD_ImporteTotal = document.getElementById('<%= HD_ImporteTotal.ClientID %>');
                        var montoTotalRemsionOriginal = parseFloat(HD_ImporteTotal.value);
                        var montoTotalRemisionOriginalSuperior = montoTotalRemsionOriginal + 0.90;
                        var montoTotalRemisionOriginalInferior = montoTotalRemsionOriginal - 0.90;


                        var txtTotal = $find('<%= txtImporte.ClientID %>');
                        var montoTotalRemisionEspecial = parseFloat(txtTotal.get_value());
                        /*
                        if (montoTotalRemisionEspecial > montoTotalRemisionOriginalSuperior) {
                            var alertMontoDif = radalert('El importe de la remisión especial solo puede variar 90c con el importe de la remisión original', 330, 150, tituloMensajes);
                            continuarAccion = false;
                        }

                        if (montoTotalRemisionEspecial < montoTotalRemisionOriginalSuperior) {
                            var alertMontoDif = radalert('El importe de la remisión especial solo puede variar 90c con el importe de la remisión original', 330, 150, tituloMensajes);
                            continuarAccion = false;
                        }*/

                        break;
                }
                if (continuarAccion == true) {
                    GetRadWindow_RemisionEspecial().BrowserWindow.ActivarBanderaRebind_RemisionEspecial();
                }

                args.set_cancel(!continuarAccion);
            }

            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow_RemisionEspecial() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow_RemisionEspecial(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                    function () {
                        //debugger;
                        CloseAndRebind_RemisionEspecial();
                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind_RemisionEspecial() {
                //debugger;
                GetRadWindow_RemisionEspecial().Close();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage_RemisionEspecial() {
                GetRadWindow_RemisionEspecial().BrowserWindow.location.reload();
            }

            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de Precios dispara evento de edición
            //--------------------------------------------------------------------------------------------------
            function rgRemisionEspecialDet_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //Para el combo de Productos dentro del Grid
            var txtId_Prd;
            var cmbProducto;

            function txtId_Prd_OnLoad(sender, args) {
                txtId_Prd = sender;
            }
            function cmbProducto_OnLoad(sender, args) {
                cmbProducto = sender;
            }
            //cuando el campo de texto de edición del Grid de clave de producto pirde el foco
            function txtId_Prd_OnBlur(sender, args) {
                //debugger; 
                OnBlur(sender, cmbProducto);
            }
            //cuando el combo de edición del Grid de producto cambia de indice
            function cmbProducto_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtId_Prd);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        <%--<asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="662px" Height="325px"
            BorderStyle="Solid" BorderWidth="1px">--%>
        <table width="100%">
            <tr>
                <td>
                    <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="341px" ResizeMode="AdjacentPane"
                        ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                        <telerik:RadPane ID="RadPane1" runat="server" Height="341px" BorderStyle="None" OnClientResized="onResize">
                            <telerik:RadGrid ID="rgRemisionEspecialDet" runat="server" GridLines="None" AllowPaging="True"
                                AutoGenerateColumns="False" BorderStyle="None" OnNeedDataSource="rgRemisionEspecialDet_NeedDataSource"
                                OnInsertCommand="rgRemisionEspecialDet_InsertCommand" OnUpdateCommand="rgRemisionEspecialDet_UpdateCommand"
                                OnDeleteCommand="rgRemisionEspecialDet_DeleteCommand" OnItemDataBound="rgRemisionEspecialDet_ItemDataBound"
                                OnItemCommand="rgRemisionEspecialDet_ItemCommand" OnPageIndexChanged="rgRemisionEspecialDet_PageIndexChanged">
                                <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Rem,Id_RemDet,Id_Prd"
                                    EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                    NoMasterRecordsText="No se encontraron registros." PageSize="15">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Id_Rem" UniqueName="Id_Rem"
                                            ReadOnly="true" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_RemDet" HeaderText="Id_RemDet" UniqueName="Id_RemDet"
                                            ReadOnly="true" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Prod." DataField="Id_Prd" UniqueName="Id_Prd">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblId_Prd" runat="server" Text='<%# Eval("Id_Prd").ToString() %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="100%" MaxLength="9"
                                                    Text='<%# Eval("Id_Prd") %>'>
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtId_Prd_OnBlur" OnLoad="txtId_Prd_OnLoad" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                            <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblId_PrdStr" runat="server" Text='<%# ObtenerDescripcion(Container.DataItem)  %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadComboBox ID="cmbProducto" runat="server" Width="100%" Filter="Contains"
                                                    AutoPostBack="true" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                    OnClientBlur="Combo_ClientBlur" DataTextField="Prd_Descripcion" DataValueField="Id_Prd"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                    OnSelectedIndexChanged="cmbProducto_SelectedIndexChanged" OnClientSelectedIndexChanged="cmbProducto_ClientSelectedIndexChanged"
                                                    OnClientLoad="cmbProducto_OnLoad">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 25px; text-align: center; vertical-align: top">
                                                                    <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id_prd").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_prd").ToString() %>' />
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Clave Especial" DataField="Id_PrdEsp" UniqueName="Id_PrdEsp">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblId_PrdEsp" runat="server" Text='<%# ObtenerIdEspecial(Container.DataItem) %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtId_PrdEsp" runat="server" Width="100%" MaxLength="50"
                                                    Text='<%# ObtenerIdEspecial(Container.DataItem) %>' onpaste="return false">
                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                </telerik:RadTextBox>
                                                <asp:Label ID="lblId_PrdEsp" runat="server" ForeColor="#FF0000"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Prd_Descripcion"
                                            UniqueName="Prd_Descripcion">
                                            <HeaderStyle Width="170px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrd_Descripcion" runat="server" Text='<%# ObtenerDescripcionEspecial(Container.DataItem) %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtPrd_Descripcion" runat="server" onpaste="return false"
                                                    Text='<%# ObtenerDescripcionEspecial(Container.DataItem)  %>' Width="100%">
                                                    <ClientEvents OnKeyPress="SinComillas" />
                                                </telerik:RadTextBox>
                                                <asp:Label ID="lblVal_txtPrd_Descripcion" runat="server" ForeColor="#FF0000"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Pres." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrd_Presentacion" runat="server" Text='<%# ObtenerPresentacion(Container.DataItem) %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtPrd_Presentacion" runat="server" onpaste="return false"
                                                    Text='<%# ObtenerPresentacion(Container.DataItem) %>' Width="100%">
                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                </telerik:RadTextBox>
                                                <asp:Label ID="lblVal_txtPrd_Presentacion" runat="server" ForeColor="#FF0000"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Unidades" DataField="Prd_UniNe" UniqueName="Prd_UniNe">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrd_UniNe" runat="server" Text='<%# ObtenerUnidades(Container.DataItem) %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtPrd_UniNe" runat="server" onpaste="return false" Text='<%# ObtenerUnidades(Container.DataItem) %>'
                                                    Width="100%">
                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                </telerik:RadTextBox>
                                                <asp:Label ID="lblVal_txtPrd_UniNe" runat="server" ForeColor="#FF0000"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Cant." DataField="Rem_Cant" UniqueName="Rem_Cant">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrd_Cantidad" runat="server" Text='<%# Eval("Rem_Cant") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtRem_Cantidad" runat="server" Width="100%" MaxLength="9"
                                                    MinValue="0" Text='<%# Eval("Rem_Cant") %>'>
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="lblVal_txtRem_Cantidad" runat="server" ForeColor="#FF0000"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Rem_Precio" HeaderText="Precio" UniqueName="Rem_Precio">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRem_Precio" runat="server" Text='<%# Eval("Rem_Precio") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtRem_Precio" runat="server" Width="50px" MaxLength="9"
                                                    MinValue="0" Text='<%# Eval("Rem_Precio") %>'>
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="lblVal_txtRem_Precio" runat="server" ForeColor="#FF0000"></asp:Label>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Rem_Importe" HeaderText="Importe" UniqueName="Rem_Importe">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRem_Importe" runat="server" Text='<%# Eval("Rem_Importe") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblRem_ImporteEdit" runat="server" Text='<%# Eval("Rem_Importe") %>' />
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Clp_Release" HeaderText="Release" UniqueName="Clp_Release">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblClp_Release" runat="server" Text='<%# Eval("Clp_Release") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <telerik:RadTextBox ID="txtClp_ReleaseEdit" runat="server" onpaste="return false"
                                                    MaxLength="100" Text='<%# Eval("Clp_Release") %>' Width="100%">
                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                </telerik:RadTextBox>
                                            </EditItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                            EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow"
                                            ButtonType="ImageButton" CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn">
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                                    PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                    ShowPagerText="True" PageButtonCount="3" />
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </td>
            </tr>
        </table>
        <%--</asp:Panel>--%>
        <div id="formularioTotales" class="formulario" runat="server">
            <table width="100%">
                <tr>
                    <td>
                    </td>
                    <td width="50">
                        <asp:Label ID="lblImporte" runat="server" Text="Importe"></asp:Label>
                    </td>
                    <td width="70">
                        <telerik:RadNumericTextBox ID="txtImporte" runat="server" Width="70px" MaxLength="9"
                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="10">
                        &nbsp;
                    </td>
                    <td width="50">
                        <asp:Label ID="lblSubtotal" runat="server" Text="Subtotal"></asp:Label>
                    </td>
                    <td width="70">
                        <telerik:RadNumericTextBox ID="txtSubTotal" runat="server" Width="70px" MaxLength="9"
                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" />
                        </telerik:RadNumericTextBox>
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
                        <asp:Label ID="lblIVA" runat="server" Text="I.V.A."></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtIVA" runat="server" Width="70px" MaxLength="9"
                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hf_spo" runat="server" />
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
                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="70px" MaxLength="9"
                            Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="HiddenHeight" runat="server" />
        <asp:HiddenField ID="HD_Cliente" runat="server" />
        <asp:HiddenField ID="HD_Moneda" runat="server" />
        <asp:HiddenField ID="HD_ImporteTotal" runat="server" />
        <asp:HiddenField ID="HD_IVARemision" runat="server" />
        <asp:HiddenField ID="HD_Descuento1" runat="server" />
        <asp:HiddenField ID="HD_Descuento2" runat="server" />
    </div>
</asp:Content>

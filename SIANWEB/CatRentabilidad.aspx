<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatRentabilidad.aspx.cs" Inherits="SIANWEB.CatRentabilidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
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
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
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
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <table style="font-family: Verdana; font-size: 8pt">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" MinValue="1" MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtClave" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Nivel"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox onpaste="return false" ID="txtNivel" runat="server" MaxLength="5">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtNivel" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Descripción"></asp:Label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox onpaste="return false" ID="txtDescripcion" runat="server" Width="200px"
                                    MaxLength="50">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtDescripcion"
                                    ValidationGroup="guardar"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                                <asp:HiddenField ID="HF_ID" runat="server" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged" />
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
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td width="75">
                                &nbsp;
                            </td>
                            <td width="50">
                                &nbsp;
                            </td>
                            <td width="70">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    PageSize="15" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                    OnPageIndexChanged="rg1_PageIndexChanged" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    OnItemCommand="rg1_ItemCommand">
                                    <MasterTableView>
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="Id_Emp" HeaderText="Id_Emp" DataField="Id_Emp"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Id_Ren" HeaderText="Clave" DataField="Id_Ren">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Nivel" HeaderText="Nivel" DataField="Nivel">
                                                <HeaderStyle Width="100" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripción" DataField="Descripcion">
                                                <HeaderStyle Width="300px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Estatus" HeaderText="Estatus" DataField="Estatus"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="EstatusStr" HeaderText="Estatus" DataField="EstatusStr">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                        ShowPagerText="True" PageButtonCount="3" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtClave.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescripcion.ClientID %>'));
                LimpiarTextBox($find('<%= txtNivel.ClientID %>'));

                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);
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
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }


                switch (button.get_value()) {
                    case 'new':
                        //debugger;

                        LimpiarControles();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HF_ID.ClientID %>');
                        hiddenActualiza.value = '';


                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtClave.ClientID %>');
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatRentabilidad";
                        parametros = parametros + "&sp=spCatCentral_Maximo";
                        parametros = parametros + "&columna=Id_Ren";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        txtId.enable();
                        txtId.focus();


                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

            

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

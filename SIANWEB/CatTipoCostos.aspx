<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatTipoCostos.aspx.cs" Inherits="SIANWEB.CatTipoCostos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario de Precios
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesTipoCostos() {

                var txtId = $find('<%= txtId.ClientID %>');
                var txtDescripcion = $find('<%= txtDescripcion.ClientID %>');
                var chkActivo = document.getElementById('<%= chkActivo.ClientID %>');

                LimpiarTextBox(txtId);
                LimpiarTextBox(txtDescripcion);
                LimpiarCheckBox(chkActivo, true);
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

                        LimpiarControlesTipoCostos();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= hiddenActualiza.ClientID %>');
                        hiddenActualiza.value = '';

                        //poner el foco en Descripcion
                        var txtId = $find('<%= txtId.ClientID %>');
                        txtId.enable();

                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatTCosto";
                        parametros = parametros + "&sp=spCatCentral_Maximo";
                        parametros = parametros + "&columna=Id_Tco";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        txtId.focus();

                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgTipoCosto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server" style="font-family: Verdana; font-size: 8pt">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
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
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenActualiza" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    Centro de distribuci&oacute;n
                </td>
              <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <!-- Tabla principal--->
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;<asp:Label ID="Label1" runat="server" Text="Clave"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtId" runat="server" Width="50px" MaxLength="9" MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_txtId" runat="server" ControlToValidate="txtId"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Descripción "></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox onpaste="return false" ID="txtDescripcion" runat="server" Width="350px"
                                    MaxLength="250">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="val_txtDescripcion" runat="server" ControlToValidate="txtDescripcion"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                    AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadGrid ID="rgTipoCosto" runat="server" AutoGenerateColumns="False" GridLines="None"
                        PageSize="15" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnItemCommand="rgTipoCosto_ItemCommand" OnNeedDataSource="rgTipoCosto_NeedDataSource"
                        OnPageIndexChanged="rgTipoCosto_PageIndexChanged">
                        <MasterTableView DataKeyNames="Id_Tco" DataMember="listTipoCostos">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                    Display="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Clave" UniqueName="Id_Tco" DataField="Id_Tco">
                                <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Descripci&oacute;n" UniqueName="Tco_Descripcion"
                                    DataField="Tco_Descripcion">
                                    <HeaderStyle Width="300px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Tco_Activo" HeaderText="Estatus" UniqueName="Tco_Activo"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Tco_ActivoStr" HeaderText="Estatus" UniqueName="Tco_ActivoStr">
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
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

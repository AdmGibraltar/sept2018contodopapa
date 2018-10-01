<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master" AutoEventWireup="true"
    CodeBehind="CatEmpresa.aspx.cs" Inherits="SIANWEB.CatEmpresa" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbEmpresa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
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
        <telerik:RadToolBar runat="server" ID="RadToolBar1" AutoPostBack="True" dir="rtl"
            Width="100%" onbuttonclick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" CssClass="mail" ToolTip="Correo" ImageUrl="~/Imagenes/blank.png"
                    Enabled="false" />
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
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución" />
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
                                <asp:Label ID="Label1" runat="server" Text="Empresa" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEmpresa" runat="server" Width="300px" OnSelectedIndexChanged="cmbEmpresa_SelectedIndexChanged"
                                    AutoPostBack="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 50px; text-align: center">
                                                    <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                </td>
                                                <td style="width: 200px; text-align: left">
                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="HFId_Empresa" runat="server" Visible="False" />
                                </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" TabIndex="-1">
                        <Tabs>
                            <telerik:RadTab PageViewID="RadPageViewDGrales" 
                                Text="Datos &lt;u&gt;g&lt;/u&gt;enerales " AccessKey="G">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="C" 
                                Text="&lt;u&gt;C&lt;/u&gt;ondiciones" PageViewID="rpvCondiciones">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0"
                        BorderStyle="Solid" BorderWidth="1px" Width="450px">
                        <!-- Aqui empieza el contenido de los tabs--->
                        <telerik:RadPageView ID="RadPageViewDGrales" runat="server">
                            <table style="font-family: vernada; font-size: 8;">
                                <!-- Tabla principal--->
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <table>
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
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Nombre" />
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox onpaste="return false" ID="txtNombre" runat="server" Width="300px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                                        AutoPostBack="true" />
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
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCondiciones" runat="server">
                            <table  >
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label69" runat="server" 
                                            Text="Gasto de flete entregas locales (% s/costo)"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFleteLocales" runat="server" MaxLength="9" 
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label95" runat="server" Text="Gastos administrativos"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGAdmitivos" runat="server" MaxLength="9" 
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label90" runat="server" 
                                            Text="Contribución a los costos fijos (%  sobre venta de PAPEL)"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCostosFijosPapel" runat="server" 
                                            MaxLength="9" MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label89" runat="server" 
                                            Text="Contribución a los costos fijos (%  sobre venta NO PAPEL)"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCostosFijosNoPapel" runat="server" 
                                            MaxLength="9" MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label85" runat="server" Text="UCS's (%)"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtUcs" runat="server" MaxLength="9" 
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label80" runat="server" Text="ISR y PTU (%)"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIsr" runat="server" MaxLength="9" 
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label92" runat="server" 
                                            Text="Inversión en activos fijos (días de venta en promedio)"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtInversionActivosFijos" runat="server" 
                                            MaxLength="4" MinValue="0" Width="80px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label81" runat="server" Text="Cetes (%)"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCetes" runat="server" MaxLength="9" 
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label86" runat="server" Text="% Adicional a cetes"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAdicionalCetes" runat="server" MaxLength="9" 
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <br />
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
                LimpiarTextBox($find('<%= txtNombre.ClientID %>'));

                LimpiarTextBox($find('<%= txtFleteLocales.ClientID %>'));
                LimpiarTextBox($find('<%= txtGAdmitivos.ClientID %>'));
                LimpiarTextBox($find('<%= txtCostosFijosPapel.ClientID %>'));
                LimpiarTextBox($find('<%= txtCostosFijosNoPapel.ClientID %>'));
                LimpiarTextBox($find('<%= txtUcs.ClientID %>'));
                LimpiarTextBox($find('<%= txtIsr.ClientID %>'));
                LimpiarTextBox($find('<%= txtInversionActivosFijos.ClientID %>'));
                LimpiarTextBox($find('<%= txtCetes.ClientID %>'));
                LimpiarTextBox($find('<%= txtAdicionalCetes.ClientID %>'));

                LimpiarCheckBox(document.getElementById('<%= chkActivo.ClientID %>'), true);
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {


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

                        LimpiarControles();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenActualiza = document.getElementById('<%= HFId_Empresa.ClientID %>');
                        hiddenActualiza.value = '';

                        //Habilita, da el foco y establece el ID sugerido
                        var txtId = $find('<%= txtNombre.ClientID %>');
                        txtId.focus();

                        continuarAccion = false;
                        break;
                }

                args.set_cancel(!continuarAccion);
            }
 
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

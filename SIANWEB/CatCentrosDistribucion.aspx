<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CatCentrosDistribucion.aspx.cs" Inherits="SIANWEB.CatCentrosDistribucion" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Comun.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="Scripts/Comun.js" type="text/javascript"></script>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = 'Valuación de proyecto de ventas';

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de Centro de Distribución
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesCentroDistribucion() {
                //debugger;

                //Controles de la pestaña 'Valuacion de proyectos'                    
                var txtProyecto = $find('<%= txtProyecto.ClientID %>');
                var txtCetesCd = $find('<%= txtCetesCd.ClientID %>');
                var txtIvaCd = $find('<%= txtIvaCd.ClientID %>');
                var txtCuentasCd = $find('<%= txtCuentasCd.ClientID %>');
                var txtFleteCd = $find('<%= txtFleteCd.ClientID %>');
                var txtDiasCd = $find('<%= txtDiasCd.ClientID %>');
                var txtComisionCd = $find('<%= txtComisionCd.ClientID %>');
                var txtInventarioCd = $find('<%= txtInventarioCd.ClientID %>');
                var txtOtrosCd = $find('<%= txtOtrosCd.ClientID %>');
                var txtFactorInvCd = $find('<%= txtFactorInvCd.ClientID %>');
                var txtGastofijoCd = $find('<%= txtGastofijoCd.ClientID %>');
                var txtFactorConCd = $find('<%= txtFactorConCd.ClientID %>');
                var txtGastofijopapelCd = $find('<%= txtGastofijopapelCd.ClientID %>');
                var txtFinanciamientoCd = $find('<%= txtFinanciamientoCd.ClientID %>');
                var txtIsrCd = $find('<%= txtIsrCd.ClientID %>');
                var txtTasaCd = $find('<%= txtTasaCd.ClientID %>');
                var txtCargoCd = $find('<%= txtCargoCd.ClientID %>');

                LimpiarTextBox(txtProyecto);
                LimpiarTextBox(txtCetesCd);
                LimpiarTextBox(txtIvaCd);
                LimpiarTextBox(txtCuentasCd);
                LimpiarTextBox(txtFleteCd);
                LimpiarTextBox(txtDiasCd);
                LimpiarTextBox(txtComisionCd);
                LimpiarTextBox(txtInventarioCd);
                LimpiarTextBox(txtOtrosCd);
                LimpiarTextBox(txtFactorInvCd);
                LimpiarTextBox(txtGastofijoCd);
                LimpiarTextBox(txtFactorConCd);
                LimpiarTextBox(txtGastofijopapelCd);
                LimpiarTextBox(txtFinanciamientoCd);
                LimpiarTextBox(txtIsrCd);
                LimpiarTextBox(txtTasaCd);
                LimpiarTextBox(txtCargoCd);

                //Controles de los datos generales
                var cmbEmpresa = $find('<%= cmbEmpresaID.ClientID %>');
                var cmbRegion = $find('<%= cmbRegion.ClientID %>');
                var txtEmpresaID = $find('<%= txtEmpresaID.ClientID %>');
                var txtRegion = $find('<%= txtRegion.ClientID %>');
                var txtCentroDistribucion = $find('<%= txtCentroDistribucion.ClientID %>');
                var chkActivo = document.getElementById('<%= chkActivo.ClientID %>');
                var txtNombreCD = $find('<%= txtNombreCD.ClientID %>');
                var txtCalle = $find('<%= txtCalle.ClientID %>');
                var txtNumeroCalle = $find('<%= txtNumeroCalle.ClientID %>');
                var txtColonia = $find('<%= txtColonia.ClientID %>');
                var txtCiudad = $find('<%= txtCiudad.ClientID %>');
                var txtMunicipio = $find('<%= txtMunicipio.ClientID %>');
                var txtEstado = $find('<%= txtEstado.ClientID %>');
                var txtPais = $find('<%= txtPais.ClientID %>');
                var txtCp = $find('<%= txtCp.ClientID %>');
                var txtRfc = $find('<%= txtRfc.ClientID %>');
                var txtTel = $find('<%= txtTel.ClientID %>');
                var cmbTipo = $find('<%= cmbTipo.ClientID %>');

                LimpiarComboSelectIndex0(cmbEmpresa);
                LimpiarComboSelectIndex0(cmbRegion);
                LimpiarTextBox(txtEmpresaID);
                LimpiarTextBox(txtRegion);
                LimpiarTextBox(txtCentroDistribucion);
                LimpiarCheckBox(chkActivo, true);
                LimpiarTextBox(txtNombreCD);
                LimpiarTextBox(txtCalle);
                LimpiarTextBox(txtNumeroCalle);
                LimpiarTextBox(txtColonia);
                LimpiarTextBox(txtCiudad);
                LimpiarTextBox(txtMunicipio);
                LimpiarTextBox(txtEstado);
                LimpiarTextBox(txtPais);
                LimpiarTextBox(txtCp);
                LimpiarTextBox(txtRfc);
                LimpiarTextBox(txtTel);
                LimpiarComboSelectIndex0(cmbTipo);

                //Controles de datos de Pedidos y facturación
                var txtPartidaPedidos = $find('<%= txtPartidaPedidos.ClientID %>');
                var txtIvaPedidosFacturacion = $find('<%= txtIvaPedidosFacturacion.ClientID %>');
                var txtClienteClaveSig = $find('<%= txtClienteClaveSig.ClientID %>');
                var txtMaximoTerritoriosSegmentos = $find('<%= txtMaximoTerritoriosSegmentos.ClientID %>');
                var chkFormatoFacturaRetIva = document.getElementById('<%= chkFormatoFacturaRetIva.ClientID %>');
                var chkDeshabilitarReglaCons = document.getElementById('<%= chkDeshabilitarReglaCons.ClientID %>');
                var chkActivaCapPedRep = document.getElementById('<%= chkActivaCapPedRep.ClientID %>');

                LimpiarTextBox(txtPartidaPedidos);
                LimpiarTextBox(txtIvaPedidosFacturacion);
                LimpiarTextBox(txtClienteClaveSig);
                LimpiarTextBox(txtMaximoTerritoriosSegmentos);
                LimpiarCheckBox(chkFormatoFacturaRetIva, false);
                LimpiarCheckBox(chkDeshabilitarReglaCons, false);
                LimpiarCheckBox(chkActivaCapPedRep, false);

                //Controles de datos de info de Inventarios
                var txtPartidaRemisiones = $find('<%= txtPartidaRemisiones.ClientID %>');
                var txtPartidaEntradas = $find('<%= txtPartidaEntradas.ClientID %>');
                var txtAjusteFromatoRengInventario = $find('<%= txtAjusteFromatoRengInventario.ClientID %>');

                LimpiarTextBox(txtPartidaRemisiones);
                LimpiarTextBox(txtPartidaEntradas);
                LimpiarTextBox(txtAjusteFromatoRengInventario);

                //Controles de datos de info de Cobranza
                var txtRelacionCobranza = $find('<%= txtRelacionCobranza.ClientID %>');
                var txtInteresMoratorio = $find('<%= txtInteresMoratorio.ClientID %>');
                var txtContribucionBruta = $find('<%= txtContribucionBruta.ClientID %>');
                var txtAmortizacion = $find('<%= txtAmortizacion.ClientID %>');
                var txtSaldosMenores = $find('<%= txtSaldosMenores.ClientID %>');
                var txtPersonaFormula = $find('<%= txtPersonaFormula.ClientID %>');
                var txtPersonaAutoriza = $find('<%= txtPersonaAutoriza.ClientID %>');

                LimpiarTextBox(txtRelacionCobranza);
                LimpiarTextBox(txtInteresMoratorio);
                LimpiarTextBox(txtContribucionBruta);
                LimpiarTextBox(txtAmortizacion);
                LimpiarTextBox(txtSaldosMenores);
                LimpiarTextBox(txtPersonaFormula);
                LimpiarTextBox(txtPersonaAutoriza);

                //Controles de info de administración de inventarios
                var txtTiempoEntrega = $find('<%= txtTiempoEntrega.ClientID %>');
                var txtTiempoTransportacion = $find('<%= txtTiempoTransportacion.ClientID %>');
                var txtNumeroMacola = $find('<%= txtNumeroMacola.ClientID %>');

                LimpiarTextBox(txtTiempoEntrega);
                LimpiarTextBox(txtTiempoTransportacion);
                LimpiarTextBox(txtNumeroMacola);

                //Controles de info de compras locales
                var chkActualiza = document.getElementById('<%= chkActualiza.ClientID %>');
                var txtFactorCosto = $find('<%= txtFactorCosto.ClientID %>');

                LimpiarCheckBox(chkActualiza, false);
                LimpiarTextBox(txtFactorCosto);

                //Controles Totalizadores
                txtProyecto = $find('<%= txtProyecto.ClientID %>');
                txtContadorPedidos = $find('<%= txtContadorPedidos.ClientID %>');
                txtRemisiones = $find('<%= txtRemisiones.ClientID %>');
                txtEntradas = $find('<%= txtEntradas.ClientID %>');
                txtSalidas = $find('<%= txtSalidas.ClientID %>');
                txtDevoluciones = $find('<%= txtDevoluciones.ClientID %>');
                txtContratoComodato = $find('<%= txtContratoComodato.ClientID %>');
                txtEmbarques = $find('<%= txtEmbarques.ClientID %>');
                txtPagos = $find('<%= txtPagos.ClientID %>');
                txtOrdenesCompra = $find('<%= txtOrdenesCompra.ClientID %>');
                txtReclamaciones = $find('<%= txtReclamaciones.ClientID %>');

                txtProyecto.set_value('0');
                txtContadorPedidos.set_value('0');
                txtRemisiones.set_value('0');
                txtEntradas.set_value('0');
                txtSalidas.set_value('0');
                txtDevoluciones.set_value('0');
                txtContratoComodato.set_value('0');
                txtEmbarques.set_value('0');
                txtPagos.set_value('0');
                txtOrdenesCompra.set_value('0');
                txtReclamaciones.set_value('0');
            }

            //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un producto
            //y selecciona la Tab donde esta el control
            function ValidaObjetoRequerido(textBox, label, indiceTab) {

                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');

                if (textBox.get_textBoxValue() == '') {
                    label.innerHTML = '*Requerido';
                    radTabStrip.get_allTabs()[indiceTab].select();
                    return false;
                }
                return true;
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


                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'new':
                        LimpiarControlesCentroDistribucion();

                        var txtCDist = $find('<%= txtCentroDistribucion.ClientID %>');
                        txtCDist.enable();

                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatCentroDistribucion";
                        parametros = parametros + "&sp=spCatCentral_Maximo";
                        parametros = parametros + "&columna=Id_Cd";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtCDist.set_value(resultado);

                        //select tab datos valuación proyectos
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        radTabStrip.get_allTabs()[0].select();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenCdi = document.getElementById('<%= hiddenCdi.ClientID %>');
                        hiddenCdi.value = '';

                        //poner el doco en idEmpresa
                        var txtC = $find('<%= txtCetesCd.ClientID %>');
                        txtC.focus();

                        //habilitar boton guardar
                        var buttonSave = sender.findItemByValue("save");
                        buttonSave.enable();

                        continuarAccion = true;
                        break;

                    case 'save':
                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');

                        //validar datos requeridos del tab de 'Cobranza'
                        var lbl_Val_txtSaldosMenores = document.getElementById('<%= lbl_Val_txtSaldosMenores.ClientID %>');
                        var lbl_Val_txtPersonaFormula = document.getElementById('<%= lbl_Val_txtPersonaFormula.ClientID %>');
                        var lbl_Val_txtPersonaAutoriza = document.getElementById('<%= lbl_Val_txtPersonaAutoriza.ClientID %>');
                        var lbl_valTxtNumeroMacola = document.getElementById('<%= lbl_valTxtNumeroMacola.ClientID %>');

                        lbl_Val_txtSaldosMenores.innerHTML = '';
                        lbl_Val_txtPersonaFormula.innerHTML = '';
                        lbl_Val_txtPersonaAutoriza.innerHTML = '';
                        lbl_valTxtNumeroMacola.innerHTML = '';

                        //debugger;

                        if (ValidaObjetoRequerido($find('<%= txtNumeroMacola.ClientID %>'), lbl_valTxtNumeroMacola, 5) == false) continuarAccion = false

                        if (ValidaObjetoRequerido($find('<%= txtSaldosMenores.ClientID %>'), lbl_Val_txtSaldosMenores, 4) == false) continuarAccion = false
                        if (ValidaObjetoRequerido($find('<%= txtPersonaFormula.ClientID %>'), lbl_Val_txtPersonaFormula, 4) == false) continuarAccion = false
                        if (ValidaObjetoRequerido($find('<%= txtPersonaAutoriza.ClientID %>'), lbl_Val_txtPersonaAutoriza, 4) == false) continuarAccion = false

                        if (continuarAccion) radTabStrip.get_allTabs()[1].select();

                        //                                continuarAccion = ValidaForm_CDistribucionDatosGenerales();
                        //                                if (!continuarAccion) {
                        //                                    var tabStrip = $find("<%= RadTabStrip1.ClientID %>");
                        //                                    var tab = tabStrip.findTabByValue('Datos generales');
                        //                                    if (tab) {
                        //                                        tab.select();
                        //                                    }
                        //                                }
                        break;

                    //                            case 'delete':                    

                    //                                var txtCentroDistribucion = document.getElementById('<%= txtCentroDistribucion.ClientID %>');                    
                    //                                if (txtCentroDistribucion != null) {                    
                    //                                    if (txtCentroDistribucion.value == '') {                    
                    //                                        radalert('Favor de proporcionar el ID del Centro de Distribución a eliminar.', 600, 10, tituloMensajes);                    
                    //                                        var txtCD = $find('<%= txtCentroDistribucion.ClientID %>');                    
                    //                                        txtCD.focus();                    
                    //                                        continuarAccion = false;                    
                    //                                    }                    
                    //                                    else {                    
                    //                                        //ventana de confirmación de eliminación                    
                    //                                        if (confirm("Esta seguro de eliminar el centro de distribución actual?")) {                    
                    //                                        //radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oTitle);                    
                    //                                            continuarAccion = true;                    
                    //                                        }                    
                    //                                        else {                    
                    //                                            continuarAccion = false;                    
                    //                                        }                    
                    //                                    }                    
                    //                                }                    
                    //                                break;                    
                }

                args.set_cancel(!continuarAccion);
            }

            //--------------------------------------------------------------------------------------------------
            //Setea variable de pestaña del TabStrip es clickeada
            //--------------------------------------------------------------------------------------------------
            function OnClientTabSelectingHandler(sender, args) {
                tabSeleccionada = args.get_tab().get_text();
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del combo de centros de distribución
            //--------------------------------------------------------------------------------------------------
            function ClientSelectedIndexChanging(sender, args) {
                //LimpiarControlesCentroDistribucion();
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del combo de empresas
            //--------------------------------------------------------------------------------------------------
            function CmbEmpresa_ClientSelectedIndexChanged(sender, eventArgs) {

                //debugger;

                var item = eventArgs.get_item();
                var txtEmpresaID = $find('<%= txtEmpresaID.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1')
                        txtEmpresaID.set_value(item.get_value());
                    else
                        txtEmpresaID.set_value('');
            }

            function cmbEmpresa_ClientBlur(sender, args) {
                //debugger;
                var itemSelected = sender.findItemByText(sender.get_text())
                if (itemSelected == null) {
                    LimpiarComboSelectIndex0(sender);
                }
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del combo de regiones
            //--------------------------------------------------------------------------------------------------
            function CmbRegion_ClientSelectedIndexChanged(sender, eventArgs) {

                //debugger;

                var item = eventArgs.get_item();
                var txtRegion = $find('<%= txtRegion.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1')
                        txtRegion.set_value(item.get_value());
                    else
                        txtRegion.set_value('');
            }

            function CmbRegion_ClientBlur(sender, args) {
                //debugger;
                var itemSelected = sender.findItemByText(sender.get_text())
                if (itemSelected == null) {
                    LimpiarComboSelectIndex0(sender);
                }
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtEmpresa pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtEmpresaID_OnBlur(sender, args) {

                //debugger;

                var textBox = sender;
                var combo = $find("<%= cmbEmpresaID.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1') {
                    if (textBox.get_value() != '') {
                        var mens = 'La empresa con id ' + textBox.get_value() + ' no existe. <br/>Favor de introducir el id de empresa correcto o seleccionar una de la lista.';
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    combo.get_items().getItem(0).select()
            }


            //--------------------------------------------------------------------------------------------------
            //Cuando el txtRegion pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtRegion_OnBlur(sender, args) {

                //debugger;

                var textBox = sender;
                var combo = $find("<%= cmbRegion.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1') {
                    if (textBox.get_value() != '') {
                        var mens = 'La regi&oacute;n con id ' + textBox.get_value() + ' no existe. <br/>Favor de introducir el id de regi&oacute;n correcto o seleccionar una de la lista.';
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    combo.get_items().getItem(0).select()
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenCdi" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenCdi" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenMultiUSer" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadTabStrip1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenCdi" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCobranza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCobranza" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgRentabilidad">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRentabilidad" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div id="divPrincipal" runat="server" style="font-family: Verdana; font-size: 8pt">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                    ImageUrl="~/Imagenes/blank.png" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" CausesValidation="false" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenActualiza" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        OnClientTabSelecting="OnClientTabSelectingHandler" SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Value="Valuación de proyecto de ventas" Text="&lt;u&gt;V&lt;/u&gt;aluación de proyecto de ventas"
                                AccessKey="V" PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Value="Datos generales" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales"
                                AccessKey="G" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Value="Pedidos y facturación" Text="&lt;u&gt;P&lt;/u&gt;edidos y facturación"
                                PageViewID="RadPageViewPedidos" AccessKey="P">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Value="Inventarios" Text="&lt;u&gt;I&lt;/u&gt;nventarios"
                                PageViewID="RadPageViewInventario" AccessKey="I">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Value="Cobranza" Text="&lt;u&gt;C&lt;/u&gt;obranza"
                                PageViewID="RadPageViewCobranza" AccessKey="C">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Value="Administración de inv." Text="&lt;u&gt;A&lt;/u&gt;dministración de inv."
                                PageViewID="RadPageViewAdmin" AccessKey="A">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Value="Servicios" Text="&lt;u&gt;S&lt;/u&gt;ervicios"
                                PageViewID="RadPageViewServicios" AccessKey="S">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Value="Compras locales" Text="Compras &lt;u&gt;l&lt;/u&gt;ocales"
                                PageViewID="RadPageViewCompras" AccessKey="L">
                            </telerik:RadTab>
                             <telerik:RadTab runat="server" Value="Comisiones" Text="Comisiones"
                                PageViewID="RadPageViewComisiones" AccessKey="M">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td height="10">
                                    </td>
                                    <td width="70">
                                    </td>
                                    <td width="90">
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="N&uacute;mero de proyecto" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtProyecto" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProyecto" runat="server" CssClass="labelValidacionForm"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td width="50">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td width="50">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label64" runat="server" Text="Valor CD" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Valor estándar" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Valor CD" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Valor estándar" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Tasa de cetes" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCetesCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCetesEstandar" runat="server" Width="70px" MaxLength="4"
                                            NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="IVA %" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIvaCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIvaEstandar" runat="server" Width="70px" MaxLength="6"
                                            NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Días cuentas por cobrar" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCuentasCd" runat="server" Width="70px" MaxLength="3"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCuentasEstandar" runat="server" Width="70px" MaxLength="3"
                                            NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Flete %" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFleteCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFleteEstandar" runat="server" Width="70px" MaxLength="6"
                                            Enabled="false" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="2"
                                            MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Días" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDiasCd" runat="server" Width="70px" MaxLength="3"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDiasEstandar" runat="server" Width="70px" MaxLength="2"
                                            NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Comisión RIK %" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtComisionCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtComisionEstandar" runat="server" Width="70px" MaxLength="6"
                                            NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="D&iacute;as de inventario en consignaci&oacute;n" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtInventarioCd" runat="server" Width="70px" MaxLength="2"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtInventarioEstandar" runat="server" Width="70px"
                                            NumberFormat-DecimalDigits="0" MaxLength="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Otros gastos variables %" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtOtrosCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtOtrosEstandar" runat="server" Width="70px" MaxLength="6"
                                            NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="Factor de inventario comodatos" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFactorInvCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFactorInvEstandar" runat="server" Width="70px"
                                            MaxLength="10" NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="Contribución a gastos fijos otros %" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGastofijoCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGastofijoEstandar" runat="server" Width="70px"
                                            MaxLength="6" NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Factor conversión en activos fijos" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFactorConCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFactorConEstandar" runat="server" Width="70px"
                                            MaxLength="9" NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="Contribución a gastos fijos papel %" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGastofijopapelCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGastofijopapelEstandar" runat="server" Width="70px"
                                            MaxLength="6" NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Días financiamiento de proveedores" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFinanciamientoCd" runat="server" Width="70px" MaxLength="3"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtfinanciamientoEstandar" runat="server" Width="70px"
                                            MaxLength="3" NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="ISR y PTU %" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIsrCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIsrEstandar" runat="server" Width="70px" MaxLength="6"
                                            NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="Tasa incremental al costo capital" />
                                    </td>
                                    <td style="margin-left: 40px">
                                        <telerik:RadNumericTextBox ID="txtTasaCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTasaEstandar" runat="server" Width="70px" MaxLength="2"
                                            NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Cargo de UCS's %" />
                                    </td>
                                    <td style="margin-left: 40px">
                                        <telerik:RadNumericTextBox ID="txtCargoCd" runat="server" Width="70px" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSeparator="" MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCargoEstandar" runat="server" Width="70px" MaxLength="6"
                                            NumberFormat-DecimalDigits="2" Enabled="false" NumberFormat-GroupSeparator=""
                                            MinValue="0" MaxValue="100">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label79" runat="server" Text="Crédito del proveedor KEY (en días)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCreditoProveedor" runat="server" MaxLength="4"
                                            MinValue="0" Width="70px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label84" runat="server" Text="Crédito del proveedor de PAPEL (en días)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCreditoProveedorPapel" runat="server" MaxLength="4"
                                            MinValue="0" Width="70px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="panelCobranza" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td width="50">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Tabla de cobranza</b>
                                        </td>
                                        <td width="50">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <b>Tabla de ajuste por rentabilidad</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <telerik:RadGrid ID="rgCobranza" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                OnNeedDataSource="rgCobranza_NeedDataSource" OnInsertCommand="rgCobranza_InsertCommand"
                                                OnUpdateCommand="rgCobranza_UpdateCommand" OnDeleteCommand="rgCobranza_DeleteCommand"
                                                OnItemDataBound="rgCobranza_ItemDataBound" OnItemCommand="rgCobranza_ItemCommand"
                                                OnPageIndexChanged="rgCobranza_PageIndexChanged">
                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                    DataKeyNames="Id_Cob" EditMode="InPlace">
                                                    <ExpandCollapseColumn Visible="True">
                                                    </ExpandCollapseColumn>
                                                    <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Id_Cob" HeaderText="Id_Cob" UniqueName="Id_Cob"
                                                            ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Día inicio" UniqueName="Cob_DiaInicio" DataField="Cob_DiaInicio">
                                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiaInicio" runat="server" Text='<%# Eval("Cob_DiaInicio") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtDiaInicio" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Cob_DiaInicio") %>'>
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Día límite" UniqueName="Cob_DiaLimite" DataField="Cob_DiaLimite">
                                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiaLimite" runat="server" Text='<%# Eval("Cob_DiaLimite") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtDiaLimite" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Cob_DiaLimite") %>'>
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Multiplicador" UniqueName="Cob_Multiplicador"
                                                            DataField="Cob_Multiplicador">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCMultiplicador" runat="server" Text='<%# Eval("Cob_Multiplicador") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtCobMultiplicador" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Cob_Multiplicador") %>'>
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                            InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                            <HeaderStyle Width="70px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                            UniqueName="DeleteColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                                Width="29px" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            </telerik:RadGrid>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            <telerik:RadGrid ID="rgRentabilidad" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                OnNeedDataSource="rgRentabilidad_NeedDataSource" OnInsertCommand="rgRentabilidad_InsertCommand"
                                                OnUpdateCommand="rgRentabilidad_UpdateCommand" OnDeleteCommand="rgRentabilidad_DeleteCommand"
                                                OnItemDataBound="rgRentabilidad_ItemDataBound" OnItemCommand="rgRentabilidad_ItemCommand"
                                                OnPageIndexChanged="rgRentabilidad_PageIndexChanged">
                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                    DataKeyNames="Id_Rent" EditMode="InPlace">
                                                    <ExpandCollapseColumn Visible="True">
                                                    </ExpandCollapseColumn>
                                                    <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Id_Rent" HeaderText="Id_Rent" UniqueName="Id_Rent"
                                                            ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Límite inferior" UniqueName="Rent_LInferior"
                                                            DataField="Rent_LInferior">
                                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLInferior" runat="server" Text='<%# Eval("Rent_LInferior") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtLInferior" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Rent_LInferior") %>'>
                                                                    <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Límite superior" UniqueName="Rent_LSuperior"
                                                            DataField="Rent_LSuperior">
                                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLSuperior" runat="server" Text='<%# Eval("Rent_LSuperior") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtLSuperior" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Rent_LSuperior") %>'>
                                                                    <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Multiplicador" UniqueName="Rent_Multiplicador"
                                                            DataField="Rent_Multiplicador">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRMultiplicador" runat="server" Text='<%# Eval("Rent_Multiplicador") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtRentMultiplicador" runat="server" Width="90px"
                                                                    MaxLength="9" Text='<%# Eval("Rent_Multiplicador") %>'>
                                                                    <NumberFormat GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                            InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                            <HeaderStyle Width="70px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                            UniqueName="DeleteColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                                Width="29px" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td height="10" width="140">
                                                    </td>
                                                    <td width="70">
                                                    </td>
                                                    <td width="10">
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label22" runat="server" Text="Empresa" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtEmpresaID" runat="server" Width="100px" MaxLength="4"
                                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="False">
                                                            <ClientEvents OnBlur="txtEmpresaID_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpresaID"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbEmpresaID" runat="server" Width="200px" Filter="Contains"
                                                            ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                            DataValueField="Id" OnClientSelectedIndexChanged="CmbEmpresa_ClientSelectedIndexChanged"
                                                            OnClientBlur="cmbEmpresa_ClientBlur" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="cmbEmpresaID_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="ValidatorComboEmp" runat="server" ControlToValidate="cmbEmpresaID"
                                                            ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server" Text="Región" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtRegion" runat="server" Width="100px" MaxLength="4"
                                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                                                            <ClientEvents OnBlur="txtRegion_OnBlur" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegion"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbRegion" runat="server" Width="200px" Filter="Contains"
                                                            ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                            DataValueField="Id" OnClientSelectedIndexChanged="CmbRegion_ClientSelectedIndexChanged"
                                                            OnClientBlur="CmbRegion_ClientBlur">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="ValidatorCmbRegion" runat="server" ControlToValidate="cmbRegion"
                                                            ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label24" runat="server" Text="Centro de distribución" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtCentroDistribucion" runat="server" MaxLength="9"
                                                            MinValue="1" Width="100px">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCentroDistribucion" runat="server" CssClass="labelValidacionForm"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="val_txtCentroDistribucion" runat="server" ControlToValidate="txtCentroDistribucion"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:CheckBox ID="chkActivo" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="chkActivo_CheckedChanged"
                                                            Text="Activo" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label25" runat="server" Text="Nombre" />
                                                    </td>
                                                    <td colspan="5">
                                                        <telerik:RadTextBox ID="txtNombreCD" runat="server" MaxLength="50" onpaste="return false"
                                                            Width="350px">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNombreCD"
                                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNombreCD" runat="server" CssClass="labelValidacionForm" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label26" runat="server" Text="Calle" />
                                                    </td>
                                                    <td colspan="4">
                                                        <telerik:RadTextBox ID="txtCalle" runat="server" MaxLength="50" onpaste="return false"
                                                            Width="160px">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label27" runat="server" Text="Número" />
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtNumeroCalle" runat="server" MaxLength="5" MinValue="0"
                                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="70px">
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label28" runat="server" Text="Colonia" />
                                                    </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtColonia" runat="server" MaxLength="50" onpaste="return false"
                                                            Width="350px">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label29" runat="server" Text="Ciudad" />
                                                    </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtCiudad" runat="server" MaxLength="50" onpaste="return false"
                                                            Width="350px">
                                                            <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label30" runat="server" Text="Municipio" />
                                                    </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtMunicipio" runat="server" MaxLength="50" onpaste="return false"
                                                            Width="350px">
                                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label31" runat="server" Text="Estado" />
                                                    </td>
                                                    <td colspan="6">
                                                        <telerik:RadTextBox ID="txtEstado" runat="server" MaxLength="50" onpaste="return false"
                                                            Width="350px">
                                                            <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label32" runat="server" Text="País" />
                                                        </td>
                                                        <td colspan="6">
                                                            <telerik:RadTextBox ID="txtPais" runat="server" MaxLength="50" onpaste="return false"
                                                                Width="350px">
                                                                <ClientEvents OnKeyPress="SoloAlfabetico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label33" runat="server" Text="C.P." />
                                                            &nbsp;
                                                        </td>
                                                        <td colspan="2">
                                                            <telerik:RadNumericTextBox ID="txtCp" runat="server" MaxLength="5" NumberFormat-DecimalDigits="0"
                                                                NumberFormat-GroupSeparator="" Width="120px">
                                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label34" runat="server" Text="RFC" />
                                                        </td>
                                                        <td colspan="2">
                                                            <telerik:RadTextBox ID="txtRfc" runat="server" MaxLength="14" onpaste="return false">
                                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label35" runat="server" Text="Tel." />
                                                        </td>
                                                        <td colspan="2">
                                                            <telerik:RadTextBox ID="txtTel" runat="server" MaxLength="20" onpaste="return false"
                                                                Width="120px">
                                                                <ClientEvents OnKeyPress="SoloNumerico" />
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label36" runat="server" Text="Tipo" />
                                                        </td>
                                                        <td colspan="2">
                                                            <telerik:RadComboBox ID="cmbTipo" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                Filter="Contains" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" Width="200px">
                                                                <%-- <Items>
                                                                    <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />
                                                                    <telerik:RadComboBoxItem Text="1 - Sucursal" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="2 - Distribuidor" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="3 - Tienda" Value="3" />
                                                                </Items>--%>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td width="140">
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table runat="server" id="tbZonas">
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblZonas" runat="server" Font-Bold="True" Text="Centros que pertenecen a la zona de"
                                                            Width="350px"></asp:Label>
                                                        <asp:Label ID="lblZonas1" runat="server" Font-Bold="True" Width="350px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadListBox ID="listZonas" runat="server" CheckBoxes="True" Width="370px"
                                                            Height="302px">
                                                        </telerik:RadListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadAjaxPanel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewPedidos" runat="server">
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td width="60" height="10">
                                    </td>
                                    <td width="70">
                                    </td>
                                    <td width="15">
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style1">
                                    </td>
                                    <td width="35">
                                    </td>
                                    <td>
                                    </td>
                                    <td width="70">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style1">
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
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label37" runat="server" Text="Pedidos" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtContadorPedidos" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator="">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" Text="Partidas" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPartidaPedidos" runat="server" Width="70px" MaxLength="5"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVariasUEN" runat="server" Text="Permitir varios territorios con la misma UEN" />
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label39" runat="server" Text="I.V.A." />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIvaPedidosFacturacion" runat="server" Width="70px"
                                            MaxLength="9" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label40" runat="server" Text="%" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                          &nbsp;  
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label41" runat="server" Text="Cliente" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtClienteClaveSig" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" Enabled="false" NumberFormat-GroupSeparator="">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="7">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style1">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="7">
                                        <asp:Label ID="Label42" runat="server" Text="N&uacute;mero m&aacute;ximo de territorios-segmentos que pueden tener un cliente" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtMaximoTerritoriosSegmentos" runat="server" Width="70px"
                                            MaxLength="2" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="7">
                                        <asp:Label ID="Label67" runat="server" Text="Monto de diferencia entre documento y documento especial" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtMargenDiferenciaDocs" runat="server" Width="70px" MaxLength="5"
                                            NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="6">
                                        <asp:CheckBox ID="chkFormatoFacturaRetIva" runat="server" Text="Formato de factura con retención de IVA" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="6">
                                        <asp:CheckBox ID="chkDeshabilitarReglaCons" runat="server" Text="Deshabilitar regla de consecutivo" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="6">
                                        <asp:CheckBox ID="chkActivaCapPedRep" runat="server" Text="Activa captura de pedidos de representantes" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style1">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style1">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewInventario" runat="server">
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td height="10">
                                    </td>
                                    <td>
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label43" runat="server" Text="Remisiones" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtRemisiones" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label44" runat="server" Text="Partidas" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPartidaRemisiones" runat="server" Width="70px"
                                            MaxLength="4" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label45" runat="server" Text="Entradas" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtEntradas" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label46" runat="server" Text="Partidas" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPartidaEntradas" runat="server" Width="70px" MaxLength="4"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label47" runat="server" Text="Salidas" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtSalidas" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label48" runat="server" Text="Devoluciones" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDevoluciones" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label49" runat="server" Text="Contrato" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtContratoComodato" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label50" runat="server" Text="Embarques" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtEmbarques" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label51" runat="server" Text="Ajuste de formato renglones" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAjusteFromatoRengInventario" runat="server" Width="70px"
                                            MaxLength="2" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
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
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewCobranza" runat="server">
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td height="10" width="120">
                                    </td>
                                    <td>
                                    </td>
                                    <td width="10">
                                        &nbsp;</td>
                                    <td width="140">
                                    </td>
                                    <td width="100">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label65" runat="server" Text="Pagos" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPagos" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label52" runat="server" Text="Relación de cobranza" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtRelacionCobranza" runat="server" Width="70px" MaxLength="5"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label53" runat="server" Text="Interés moratorio" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtInteresMoratorio" runat="server" Width="70px" MaxLength="9"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label85" runat="server" Text="Factor costo financiero" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFactorCostoFinanciero" runat="server" 
                                            MaxLength="5" MinValue="0" NumberFormat-DecimalDigits="0" 
                                            NumberFormat-GroupSeparator="" Width="70px">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label54" runat="server" Text="Contribución bruta" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtContribucionBruta" runat="server" Width="70px"
                                            MaxLength="14" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""
                                            MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label55" runat="server" Text="Amortización" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAmortizacion" runat="server" Width="70px" MaxLength="10"
                                            NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td width="120">
                                        <asp:Label ID="Label56" runat="server" Text="Saldos menores" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtSaldosMenores" runat="server" MaxLength="14" 
                                            MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" 
                                            Width="70px">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Val_txtSaldosMenores" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label57" runat="server" Text="Formula" />
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txtPersonaFormula" runat="server" MaxLength="100" 
                                            onpaste="return false" Width="250px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Val_txtPersonaFormula" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label58" runat="server" Text="Autoriza" />
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txtPersonaAutoriza" runat="server" MaxLength="100" 
                                            onpaste="return false" Width="250px">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Val_txtPersonaAutoriza" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td width="120">
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
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewAdmin" runat="server">
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td height="10">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label59" runat="server" Text="Órdenes de compra" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtOrdenesCompra" runat="server" Width="70px" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label60" runat="server" Text="Tiempo de entrega" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTiempoEntrega" runat="server" Width="70px" MaxLength="3"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label61" runat="server" Text="Tiempo de transportación" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTiempoTransportacion" runat="server" Width="70px"
                                            MaxLength="3" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="0">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label66" runat="server" Text="Número de macola" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtNumeroMacola" runat="server" Width="70px" MaxLength="9"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="1">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_valTxtNumeroMacola" runat="server" ForeColor="#FF0000" />
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewServicios" runat="server">
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td width="90" height="10">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label62" runat="server" Text="Reclamaciones" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtReclamaciones" runat="server" Width="70px" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSeparator="" Enabled="false">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewCompras" runat="server">
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td width="90" height="10">
                                    </td>
                                    <td>
                                    </td>
                                    <td width="100">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkActualiza" runat="server" Text="Actualiza entrada en automatico" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label63" runat="server" Text="Factor de costo" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFactorCosto" runat="server" MaxLength="9" MinValue="0"
                                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="70px">
                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                         <telerik:RadPageView ID="RadPageComisiones" runat="server">
                         <table>
                         <tr>
                         <td></td>
                         <td>
                         </td>
                         </tr>
                         <tr>
                         <td></td>
                         <td>
                         <asp:CheckBox runat="server" ID="ChkCD_NuevoEsquemaCom" Text="Nuevo esquema" />
                         </td>
                         <td>
                         </td>
                         <td>
                         Gasto administrativo &nbsp;   
                         <telerik:RadNumericTextBox ID="TxtCD_Gasto" runat="server" MaxLength="3" MinValue="0"
                           NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" Width="40px">
                                        </telerik:RadNumericTextBox> %
                         </td>
                         </tr>
                         <tr>
                         <td>
                         
                         </td>
                         <td>
                        <%-- <asp:Label runat="server" ID="LblMultCobranza" Text="Tabla de multiplicador de cobranza"  Font-Bold="True" ></asp:Label>--%>
                         </td>
                         <td>
                         </td>
                         <td>
           <%--               <asp:Label runat="server" ID="Label67" Text="Tabla de participación por categoría"  Font-Bold="True" ></asp:Label>--%>
                         </td>
                         
                         </tr>
                      
                      <tr>
                      <td>
                      </td>
                      <td>
              <%--            <telerik:RadGrid ID="rgMult" runat="server" AutoGenerateColumns="False" GridLines="None"
                                               OnNeedDataSource="rgCobranza_NeedDataSource" OnInsertCommand="rgCobranza_InsertCommand"
                                                OnUpdateCommand="rgCobranza_UpdateCommand" OnDeleteCommand="rgCobranza_DeleteCommand"
                                                OnItemDataBound="rgCobranza_ItemDataBound" OnItemCommand="rgCobranza_ItemCommand"
                                                OnPageIndexChanged="rgCobranza_PageIndexChanged"
                                                >
                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                    DataKeyNames="Id_Cob" EditMode="InPlace">
                                                    <ExpandCollapseColumn Visible="True">
                                                    </ExpandCollapseColumn>
                                                    <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Id_Cob" HeaderText="Id_Cob" UniqueName="Id_Cob"
                                                            ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Día inicio" UniqueName="Cob_DiaInicio" DataField="Cob_DiaInicio">
                                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiaInicio" runat="server" Text='<%# Eval("Cob_DiaInicio") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtDiaInicio" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Cob_DiaInicio") %>'>
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Día límite" UniqueName="Cob_DiaLimite" DataField="Cob_DiaLimite">
                                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiaLimite" runat="server" Text='<%# Eval("Cob_DiaLimite") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtDiaLimite" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Cob_DiaLimite") %>'>
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Multiplicador" UniqueName="Cob_Multiplicador"
                                                            DataField="Cob_Multiplicador">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCMultiplicador" runat="server" Text='<%# Eval("Cob_Multiplicador") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtCobMultiplicador" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Cob_Multiplicador") %>'>
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                            InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                            <HeaderStyle Width="70px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                            UniqueName="DeleteColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                                Width="29px" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            </telerik:RadGrid>--%>
                      </td>
                      <td></td>
                      <td>
                      
                 <%--                   <telerik:RadGrid ID="rgCategorias" runat="server" AutoGenerateColumns="False" GridLines="None"
                                               OnNeedDataSource="rgCategorias_NeedDataSource" OnInsertCommand="rgCategorias_InsertCommand"
                                                OnUpdateCommand="rgCategorias_UpdateCommand" OnDeleteCommand="rgCategorias_DeleteCommand"
                                                OnItemDataBound="rgCategorias_ItemDataBound" OnItemCommand="rgCategorias_ItemCommand"
                                                OnPageIndexChanged="rgCategorias_PageIndexChanged"
                                                >
                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                    DataKeyNames="Id_Cat" EditMode="InPlace">
                                                    <ExpandCollapseColumn Visible="True">
                                                    </ExpandCollapseColumn>
                                                    <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Id_Cat" HeaderText="Id_Cat" UniqueName="Id_Cat"
                                                            ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Descripción" UniqueName="Cat_Descripcion" DataField="Cob_DiaInicio">
                                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCat_Descripcion" runat="server" Text='<%# Eval("Cat_Descripcion") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadTextBox ID="txtCat_Descripcion" runat="server" Width="150px" 
                                                                    Text='<%# Eval("Cat_Descripcion") %>'>
                                                           
                                                                </telerik:RadTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                
                                                        <telerik:GridTemplateColumn HeaderText="Participación" UniqueName="Cat_Participacion"
                                                            DataField="Cat_Participacion">
                                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCat_Participacion" runat="server" Text='<%# Eval("Cat_Participacion") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txtCat_Participacion" runat="server" Width="90px" MaxLength="9"
                                                                    Text='<%# Eval("Cat_Participacion") %>'>
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </EditItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                            InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                            <HeaderStyle Width="70px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                            UniqueName="DeleteColumn">
                                                            <HeaderStyle Width="29px" />
                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                                Width="29px" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                            </telerik:RadGrid>--%>
                      </td>
                      </tr>
                         </table>
                         </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="hiddenCdi" runat="server" />
                    <asp:HiddenField ID="hiddenMultiUSer" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

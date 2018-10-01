<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CatProductos.aspx.cs" Inherits="SIANWEB.CatProductos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Producto_Focus() {
                //debugger;
                var combo = $find("<%= cmbProductosLista.ClientID %>");
                combo.clearSelection();
            }

            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = '';
            var arregloSubFamilias = '';


            //--------------------------------------------------------------------------------------------------
            //Limpiar controles del catalogo de Producto
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesProducto() {
                //debugger;

                //Controles de la pestaña 'Valuacion de proyectos'                    
                var TextId_Prd = $find('<%= TextId_Prd.ClientID %>');
                var chkActivo = document.getElementById('<%= chkActivo.ClientID %>');
                var chkProductoNuevo = document.getElementById('<%= chkProductoNuevo.ClientID %>');
                var txtCodProd = $find('<%= txtCodProd.ClientID %>');
                var TextPrd_Descrpcion = $find('<%= TextPrd_Descrpcion.ClientID %>');
                var txtPresentacion = $find('<%= txtPresentacion.ClientID %>');
                var txtTipoProducto = $find('<%= txtTipoProducto.ClientID %>');
                var cmbTipoProducto = $find('<%= cmbTipoProducto.ClientID %>');
                var TextId_Spo = $find('<%= TextId_Spo.ClientID %>');
                var cmbSisProp = $find('<%= cmbSisProp.ClientID %>');
                var txtAgrupadoSpo = $find('<%= txtAgrupadoSpo.ClientID %>');
                var txtCategoria = $find('<%= txtCategoria.ClientID %>');
                var cmbCategoria = $find('<%= cmbCategoria.ClientID %>');

                var txtSubFam = $find('<%= txtSubFam.ClientID %>');
                var cmbSubFam = $find('<%= cmbSubFam.ClientID %>');
                var txtProveedor = $find('<%= txtProveedor.ClientID %>');
                var cmbProveedor = $find('<%= cmbProveedor.ClientID %>');
                var cmbUentrada = $find('<%= cmbUentrada.ClientID %>');
                var txtFactorConversion = $find('<%= txtFactorConversion.ClientID %>');
                var cmbUsalida = $find('<%= cmbUsalida.ClientID %>');
                var txtUempaque = $find('<%= txtUempaque.ClientID %>');

                LimpiarTextBox(TextId_Prd);
                LimpiarCheckBox(chkActivo, true);
                LimpiarCheckBox(chkProductoNuevo, true);
                LimpiarTextBox(txtCodProd);
                LimpiarTextBox(TextPrd_Descrpcion);
                LimpiarTextBox(txtPresentacion);
                LimpiarTextBox(txtTipoProducto);
                LimpiarComboSelectIndex0(cmbTipoProducto);
                LimpiarTextBox(TextId_Spo);
                LimpiarComboSelectIndex0(cmbSisProp);
                LimpiarTextBox(txtAgrupadoSpo);
                LimpiarTextBox(txtCategoria);
                LimpiarComboSelectIndex0(cmbCategoria);

                var txtFam = $find('<%= txtFam.ClientID %>');
                var cmbFam = $find('<%= cmbFam.ClientID %>');
                LimpiarTextBox(txtFam);
                LimpiarComboSelectIndex0(cmbFam);

                LimpiarTextBox(txtSubFam);
                LimpiarComboSelectIndex0(cmbSubFam);
                LimpiarTextBox(txtProveedor);
                LimpiarComboSelectIndex0(cmbProveedor);
                LimpiarComboSelectIndex0(cmbUentrada);
                LimpiarTextBox(txtFactorConversion);
                LimpiarComboSelectIndex0(cmbUsalida);
                LimpiarTextBox(txtUempaque);


                var txtInvSeguridad = $find('<%= txtInvSeguridad.ClientID %>');
                var chkSistProp = document.getElementById('<%= chkSistProp.ClientID %>');
                var txtTentrega = $find('<%= txtTentrega.ClientID %>');
                var txtTtransporte = $find('<%= txtTtransporte.ClientID %>');
                var txtRentabilidad = $find('<%= txtRentabilidad.ClientID %>');
                var chkComprasLocales = document.getElementById('<%= chkComprasLocales.ClientID %>');
                var txtAmortizacion = $find('<%= txtAmortizacion.ClientID %>');
                var txtPesos = $find('<%= txtPesos.ClientID %>');
                var txtExistencia = $find('<%= txtExistencia.ClientID %>');
                var txtUbicacion = $find('<%= txtUbicacion.ClientID %>');
                var txtContribucion = $find('<%= txtContribucion.ClientID %>');

                LimpiarTextBox(txtInvSeguridad);
                LimpiarCheckBox(chkSistProp);
                LimpiarTextBox(txtTentrega);
                LimpiarTextBox(txtTtransporte);
                LimpiarTextBox(txtRentabilidad);
                LimpiarCheckBox(chkComprasLocales);
                LimpiarTextBox(txtAmortizacion);
                LimpiarTextBox(txtPesos);
                LimpiarTextBox(txtExistencia);
                LimpiarTextBox(txtUbicacion);
                LimpiarTextBox(txtContribucion);

                var txtAsignado = $find('<%= txtAsignado.ClientID %>');
                var txtInicial = $find('<%= txtInicial.ClientID %>');
                var txtOrdenado = $find('<%= txtOrdenado.ClientID %>');
                var txtFinal = $find('<%= txtFinal.ClientID %>');
                var txtTransito = $find('<%= txtTransito.ClientID %>');
                var txtFisico = $find('<%= txtFisico.ClientID %>');

                LimpiarTextBox(txtAsignado);
                LimpiarTextBox(txtInicial);
                LimpiarTextBox(txtOrdenado);
                LimpiarTextBox(txtFinal);
                LimpiarTextBox(txtTransito);
                LimpiarTextBox(txtFisico);

                var txtFnombre = $find('<%= txtFnombre.ClientID %>');
                var txtFcodigo = $find('<%= txtFcodigo.ClientID %>');
                var txtFdescripcion = $find('<%= txtFdescripcion.ClientID %>');
                var txtFpresentacion = $find('<%= txtFpresentacion.ClientID %>');
                var txtPnombre = $find('<%= txtPnombre.ClientID %>');
                var txtPcodigo = $find('<%= txtPcodigo.ClientID %>');
                var txtPdescripcion = $find('<%= txtPdescripcion.ClientID %>');
                var txtPpresentacion = $find('<%= txtPpresentacion.ClientID %>');

                LimpiarTextBox(txtFnombre);
                LimpiarTextBox(txtFcodigo);
                LimpiarTextBox(txtFdescripcion);
                LimpiarTextBox(txtFpresentacion);
                LimpiarTextBox(txtPnombre);
                LimpiarTextBox(txtPcodigo);
                LimpiarTextBox(txtPdescripcion);
                LimpiarTextBox(txtPpresentacion);
            }

            //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un producto
            //y selecciona la Tab donde esta el control
            function ValidaObjetoRequerido(textBox, label, indiceTab) {

                var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');

                if (textBox.get_textBoxValue() == '') {
                    label.innerHTML = '*Requerido';
                    radTabStrip.get_allTabs()[indiceTab].select();
                    return false;
                }
                return true;
            }

            //Validar datos que son requeridos Siempre
            function ValidarControlesRequeridos() {
                //debugger;
                var validacionResult = true;

                lbl_Val_TextId_Prd = document.getElementById('<%= lbl_Val_TextId_Prd.ClientID %>');
                lbl_Val_TextPrd_Descrpcion = document.getElementById('<%= lbl_Val_TextPrd_Descrpcion.ClientID %>');
                lbl_Val_txtPresentacion = document.getElementById('<%= lbl_Val_txtPresentacion.ClientID %>');
                lbl_Val_txtTipoProducto = document.getElementById('<%= lbl_Val_txtTipoProducto.ClientID %>');
                lbl_Val_txtCategoria = document.getElementById('<%= lbl_Val_txtCategoria.ClientID %>');
                lbl_Val_txtProveedor = document.getElementById('<%= lbl_Val_txtProveedor.ClientID %>');
                lbl_Val_cmbUentrada = document.getElementById('<%= lbl_Val_cmbUentrada.ClientID %>');

                lbl_Val_TextId_Prd.innerHTML = '';
                lbl_Val_TextPrd_Descrpcion.innerHTML = '';
                lbl_Val_txtPresentacion.innerHTML = '';
                lbl_Val_txtTipoProducto.innerHTML = '';
                lbl_Val_txtCategoria.innerHTML = '';
                lbl_Val_txtProveedor.innerHTML = '';
                lbl_Val_cmbUentrada.innerHTML = '';

                if (ValidaObjetoRequerido($find('<%= TextId_Prd.ClientID %>'), lbl_Val_TextId_Prd, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= TextPrd_Descrpcion.ClientID %>'), lbl_Val_TextPrd_Descrpcion, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtPresentacion.ClientID %>'), lbl_Val_txtPresentacion, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtTipoProducto.ClientID %>'), lbl_Val_txtTipoProducto, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtCategoria.ClientID %>'), lbl_Val_txtCategoria, 0) == false) validacionResult = false
                if (ValidaObjetoRequerido($find('<%= txtProveedor.ClientID %>'), lbl_Val_txtProveedor, 0) == false) validacionResult = false

                cmbUentrada = $find('<%= cmbUentrada.ClientID %>');
                if (cmbUentrada.get_value() == '-1') {
                    lbl_Val_cmbUentrada.innerHTML = '*Requerido';
                    validacionResult = false
                }

                return validacionResult;
            }


            //Valida datos requeridos que dependen de la captura de otros datos al momento de insertar o actualizar un Producto
            function ValidarControlesEspeciales() {
                var validacionResult = true;

                //obtener objetos (Labels) para desplegar avisos de dato requerido...
                var lbl_Val_txtCodProd = document.getElementById('<%= lbl_Val_txtCodProd.ClientID %>');
                var lbl_val_TextId_Spo = document.getElementById('<%= lbl_val_TextId_Spo.ClientID %>');
                var lbl_val_txtSubFam = document.getElementById('<%= lbl_val_txtSubFam.ClientID %>');
                var lbl_Val_Rentabilidad = document.getElementById('<%= lbl_Val_Rentabilidad.ClientID %>');

                var lbl_Val_Amortizacion = document.getElementById('<%= lbl_Val_Amortizacion.ClientID %>');

                var lbl_Val_txtFnombre = document.getElementById('<%= lbl_Val_txtFnombre.ClientID %>');
                var lbl_Val_txtFcodigo = document.getElementById('<%= lbl_Val_txtFcodigo.ClientID %>');
                var lbl_Val_txtFdescripcion = document.getElementById('<%= lbl_Val_txtFdescripcion.ClientID %>');
                var lbl_Val_txtFpresentacion = document.getElementById('<%= lbl_Val_txtFpresentacion.ClientID %>');
                var lbl_Val_txtPnombre = document.getElementById('<%= lbl_Val_txtPnombre.ClientID %>');
                var lbl_Val_txtPcodigo = document.getElementById('<%= lbl_Val_txtPcodigo.ClientID %>');
                var lbl_Val_txtPdescripcion = document.getElementById('<%= lbl_Val_txtPdescripcion.ClientID %>');
                var lbl_Val_txtPpresentacion = document.getElementById('<%= lbl_Val_txtPpresentacion.ClientID %>');

                var lbl_val_txtAgrupadoSpo = document.getElementById('<%= lbl_val_txtAgrupadoSpo.ClientID %>');

                lbl_Val_txtCodProd.innerHTML = '';
                lbl_val_TextId_Spo.innerHTML = '';
                lbl_val_txtSubFam.innerHTML = '';
                lbl_Val_Rentabilidad.innerHTML = '';

                lbl_Val_txtFnombre.innerHTML = '';
                lbl_Val_txtFcodigo.innerHTML = '';
                lbl_Val_txtFdescripcion.innerHTML = '';
                lbl_Val_txtFpresentacion.innerHTML = '';
                lbl_Val_txtPnombre.innerHTML = '';
                lbl_Val_txtPcodigo.innerHTML = '';
                lbl_Val_txtPdescripcion.innerHTML = '';
                lbl_Val_txtPpresentacion.innerHTML = '';

                lbl_val_txtAgrupadoSpo.innerHTML = '';

                var txtTipoProducto = $find('<%= txtTipoProducto.ClientID %>');
                var TextId_Spo = $find('<%= TextId_Spo.ClientID %>');
                //si el tipo de producto es tipo accesorios (Id_Ptp == 1) y elige una opcion del combo de sistemas propietarios
                //el agrupado de equipos de sistemas propietarios es requerido
                if (txtTipoProducto.get_textBoxValue() == '1' && TextId_Spo.get_textBoxValue() != '') {
                    if (ValidaObjetoRequerido($find('<%= txtAgrupadoSpo.ClientID %>'), lbl_val_txtAgrupadoSpo, 0) == false) validacionResult = false
                }

                if (txtTipoProducto.get_textBoxValue() == '1') {
                    if (ValidaObjetoRequerido($find('<%= txtCodProd.ClientID %>'), lbl_Val_txtCodProd, 0) == false) validacionResult = false
                    if (ValidaObjetoRequerido($find('<%= txtAgrupadoSpo.ClientID %>'), lbl_val_txtAgrupadoSpo, 0) == false) validacionResult = false
                }

                //valida que se capture codigo unico si el producto pertenece a un sistema de propietario
                //valida que se capture meses de amortización
                var TextId_Spo = $find('<%= TextId_Spo.ClientID %>');
                if (TextId_Spo.get_textBoxValue() != '') {
                    if (ValidaObjetoRequerido($find('<%= txtAmortizacion.ClientID %>'), lbl_Val_Amortizacion, 1) == false) validacionResult = false
                }

                //valida que se capture una subfamilia cuando se captura una Familia
                var txtFam = $find('<%= txtFam.ClientID %>');
                if (txtFam.get_textBoxValue() != '') {
                    if (ValidaObjetoRequerido($find('<%= txtSubFam.ClientID %>'), lbl_val_txtSubFam, 0) == false) validacionResult = false
                }

                //valida que se capture la rentabilidad cuando no esta activado el check de producto para compra local
                var chkComprasLocales = document.getElementById('<%= chkComprasLocales.ClientID %>');

                if (chkComprasLocales.checked == false) {
                    if (ValidaObjetoRequerido($find('<%= txtRentabilidad.ClientID %>'), lbl_Val_Rentabilidad, 1) == false) validacionResult = false
                }
                else {
                    //si si esta activado --> es obligatorio el sistema propietario

                    if (txtTipoProducto.get_textBoxValue() == '1') {
                        if (ValidaObjetoRequerido($find('<%= TextId_Spo.ClientID %>'), lbl_val_TextId_Spo, 0) == false) validacionResult = false
                    }
                    //si si esta activado --> todos los datos de la pestañs de compras locales son obligatorios
                    if (ValidaObjetoRequerido($find('<%= txtFnombre.ClientID %>'), lbl_Val_txtFnombre, 4) == false) validacionResult = false
                    if (ValidaObjetoRequerido($find('<%= txtFcodigo.ClientID %>'), lbl_Val_txtFcodigo, 4) == false) validacionResult = false
                    if (ValidaObjetoRequerido($find('<%= txtFdescripcion.ClientID %>'), lbl_Val_txtFdescripcion, 4) == false) validacionResult = false
                    if (ValidaObjetoRequerido($find('<%= txtFpresentacion.ClientID %>'), lbl_Val_txtFpresentacion, 4) == false) validacionResult = false
                    if (ValidaObjetoRequerido($find('<%= txtPnombre.ClientID %>'), lbl_Val_txtPnombre, 4) == false) validacionResult = false
                    if (ValidaObjetoRequerido($find('<%= txtPcodigo.ClientID %>'), lbl_Val_txtPcodigo, 4) == false) validacionResult = false
                    if (ValidaObjetoRequerido($find('<%= txtPdescripcion.ClientID %>'), lbl_Val_txtPdescripcion, 4) == false) validacionResult = false
                    if (ValidaObjetoRequerido($find('<%= txtPpresentacion.ClientID %>'), lbl_Val_txtPpresentacion, 4) == false) validacionResult = false
                }

                //validar que tiempo de entrega y de transporte sean multiplos de 7
                var txtTentrega = $find('<%= txtTentrega.ClientID %>');
                var txtTtransporte = $find('<%= txtTtransporte.ClientID %>');
                var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');

                if (validacionResult == true) {
                    if (txtTentrega.get_textBoxValue() != '') {
                        var tiempoEntrega = parseFloat(txtTentrega.get_textBoxValue());
                        if ((tiempoEntrega % 7) != 0) {
                            var Alerta_tiempoEntrega = radalert('El tiempo de entrega debe estar en múltiplos de 7', 600, 10, tituloMensajes);
                            validacionResult = false
                            Alerta_tiempoEntrega.add_close(
                            function () {
                                radTabStrip.get_allTabs()[1].select();
                                txtTentrega.focus();
                            });
                        }
                    }
                }

                if (validacionResult == true) {
                    if (txtTtransporte.get_textBoxValue() != '') {
                        var tiempoTransporte = parseFloat(txtTtransporte.get_textBoxValue());
                        if ((tiempoTransporte % 7) != 0) {
                            var Alerta_tiempoTransporte = radalert('El tiempo de transporte debe estar en múltiplos de 7', 600, 10, tituloMensajes);
                            validacionResult = false
                            Alerta_tiempoTransporte.add_close(
                            function () {
                                radTabStrip.get_allTabs()[1].select();
                                txtTtransporte.focus();
                            });
                        }
                    }
                }

                return validacionResult;
            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid de precios de producto
            var datePickerFechaInicioClientId = '';
            var datePickerFechaFinClientId = '';
            var txtPrd_PesosClientId = '';

            //Validación del formulario de insercion/edición de registro en el RadGrid de precios de producto
            function ValidaFormGridPrecioProductos(accion) {
                //debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find(datePickerFechaInicioClientId);
                var datePickerFechaFin = $find(datePickerFechaFinClientId);
                var txtPrd_Pesos = $find(txtPrd_PesosClientId);

                //realizar validaciones
                var fechaInicio = null;
                var fechaFin = null;

                fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                //validar que se capture la la fecha inicio.
                if (fechaInicio == null) {
                    var mensage = 'Favor de capturar la fecha de inicio';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
                }

                //validar que se capture la la fecha fin.
                if (fechaFin == null) {
                    var mensage = 'Favor de capturar la fecha de fin';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaFin._dateInput.focus(); });
                    return false
                }

                //validar rango correcto de fechas.
                if (fechaInicio > fechaFin) {
                    var mensage = 'La fecha de inicio, no debe ser mayor a la fecha de fin';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaFin._dateInput.focus(); });
                    return false
                }

                //validar que se capture la cantidad de pesos
                if (txtPrd_Pesos.get_textBoxValue() == '') {
                    var mensage = 'Favor de capturar el precio de producto';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { txtPrd_Pesos.focus(); });
                    return false
                }

                if (parseFloat(txtPrd_Pesos.get_textBoxValue()) == 0) {
                    var mensage = 'El precio debe ser mayor a 0';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { txtPrd_Pesos.focus(); });
                    return false
                }
                return true
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
                //                for (i = 0; i < Page_Validators.length; i++) {
                //                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                //                }

                //debugger;
                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        LimpiarControlesProducto();

                        //select tab datos generales
                        var RadTabStripPrincipal = $find('<%= RadTabStripPrincipal.ClientID %>');
                        RadTabStripPrincipal.get_allTabs()[0].select();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenId = document.getElementById('<%= hiddenId.ClientID %>');
                        hiddenId.value = '';

                        //poner el doco en txtIdProducto
                        var TextId_Prd = $find('<%= TextId_Prd.ClientID %>');
                        TextId_Prd.enable();

                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CatProducto";
                        parametros = parametros + "&sp=spCatCentral_Maximo";
                        parametros = parametros + "&columna=Id_Prd";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        TextId_Prd.set_value(resultado);

                        TextId_Prd.focus();
                        continuarAccion = true;
                        break;
                    case 'save':
                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStripPrincipal.ClientID %>');
                        radTabStrip.get_allTabs()[0].select();

                        continuarAccion = ValidarControlesRequeridos();
                        if (continuarAccion == true) {
                            continuarAccion = ValidarControlesEspeciales();
                        }
                        break;
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
            //Doble click en un Row del Grid de Precios dispara evento de edición
            //--------------------------------------------------------------------------------------------------
            function rgPrecios_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //--------------------------------------------------------------------------------------------------
            //Actualiza el número de registros en combo de productos.
            //--------------------------------------------------------------------------------------------------
            function cmbProductosLista_UpdateItemCountField(sender, args) {
                //set the footer text
                sender.get_dropDownElement().lastChild.innerHTML = "Un total de " + (sender.get_items().get_count() - 1) + " productos";
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el combo cmbCentrosDist cambia el item seleccionado
            //--------------------------------------------------------------------------------------------------
            function cmbCentrosDist_ClientSelectedIndexChanged(sender, args) {
                LimpiarControlesProducto();

                //select tab datos generales
                var RadTabStripPrincipal = $find('<%= RadTabStripPrincipal.ClientID %>');
                RadTabStripPrincipal.get_allTabs()[0].select();

                //registro nuevo -> se limpia bandera de actualización
                var hiddenId = document.getElementById('<%= hiddenId.ClientID %>');
                hiddenId.value = '';

                //poner el doco en txtIdProducto
                var TextId_Prd = $find('<%= TextId_Prd.ClientID %>');
                TextId_Prd.focus();
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el combo cmbUentrada cambia el item seleccionado
            //--------------------------------------------------------------------------------------------------
            function cmbUentrada_OnClientSelectedIndexChanged(sender, args) {
                var item = args.get_item();
                var cmbUsalida = $find('<%= cmbUsalida.ClientID %>');
                var cmbUsalida_item = cmbUsalida.findItemByValue(item.get_value())
                cmbUsalida_item.select();
            }

            //--------------------------------------------------------------------------------------------------
            // Cuando TextId_Prd TextPrd_Descrpcion pierde el foco, establece el titulo del producto
            //--------------------------------------------------------------------------------------------------
            function TextId_Prd_OnBlur(sender, args) {
                EstablecerLabelTituloProducto();
            }

            function TextPrd_Descrpcion_OnBlur(sender, args) {
                EstablecerLabelTituloProducto();
            }

            function EstablecerLabelTituloProducto() {
                var label = document.getElementById('<%= lblTituloProducto.ClientID %>');
                var TextId_Prd = $find('<%= TextId_Prd.ClientID %>');
                var TextPrd_Descrpcion = $find('<%= TextPrd_Descrpcion.ClientID %>');

                var string_variable = TextPrd_Descrpcion.get_value()

                var intIndexOfMatch = string_variable.indexOf("'");
                while (intIndexOfMatch != -1) {
                    string_variable = string_variable.replace("'", "")
                    intIndexOfMatch = string_variable.indexOf("'");
                }


                TextPrd_Descrpcion.set_value(string_variable);
                label.innerHTML = TextId_Prd.get_textBoxValue() + ' - ' + TextPrd_Descrpcion.get_textBoxValue();
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del cmbTipoProducto
            //--------------------------------------------------------------------------------------------------
            function cmbTipoProducto_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                var item = eventArgs.get_item();
                var textBox = $find('<%= txtTipoProducto.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1')
                        textBox.set_value(item.get_value());
                    else
                        textBox.set_value('');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtTipoProducto pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtTipoProducto_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbTipoProducto.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'El tipo de producto con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }


            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del cmbSisProp
            //--------------------------------------------------------------------------------------------------
            function cmbSisProp_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                var item = eventArgs.get_item();
                var textBox = $find('<%= TextId_Spo.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1')
                        textBox.set_value(item.get_value());
                    else
                        textBox.set_value('');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el TextId_Spo pierde el foco
            //--------------------------------------------------------------------------------------------------
            function TextId_Spo_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbSisProp.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'El Sistema de propietario con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del cmbCategoria
            //--------------------------------------------------------------------------------------------------
            function cmbCategoria_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                var item = eventArgs.get_item();
                var textBox = $find('<%= txtCategoria.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1')
                        textBox.set_value(item.get_value());
                    else
                        textBox.set_value('');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtCategoria pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtCategoria_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbCategoria.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'La categoría con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }



            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del cmbFam
            //--------------------------------------------------------------------------------------------------
            function cmbFam_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;

                var item = eventArgs.get_item();
                var textBox = $find('<%= txtFam.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1') {
                        textBox.set_value(item.get_value());
                        //Llenar combo de subfamilias
                        var combo = $find("<%= cmbSubFam.ClientID %>");
                        combo.clearItems();

                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_value('-1');
                        comboItem.set_text('-- Seleccionar --');
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                        combo.commitChanges();
                        for (i = 0; i < arregloSubFamilias[0].length; i++) {
                            if (arregloSubFamilias[0][i] == item.get_value()) {//Sila subfamilia pertnece a la familia seleccionada
                                comboItem = new Telerik.Web.UI.RadComboBoxItem();
                                comboItem.set_value(arregloSubFamilias[1][i]);
                                comboItem.set_text(arregloSubFamilias[2][i]);
                                combo.trackChanges();
                                combo.get_items().add(comboItem);
                                combo.commitChanges();
                            }
                        }
                    }
                    else
                        textBox.set_value('');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtFam pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtFam_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbFam.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'La familia con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }



            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del cmbSubFam
            //--------------------------------------------------------------------------------------------------
            function cmbSubFam_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                var item = eventArgs.get_item();
                var textBox = $find('<%= txtSubFam.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1')
                        textBox.set_value(item.get_value());
                    else
                        textBox.set_value('');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtSubFam pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtSubFam_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbSubFam.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'La subfamilia con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }



            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del cmbProveedor
            //--------------------------------------------------------------------------------------------------
            function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                var item = eventArgs.get_item();
                var textBox = $find('<%= txtProveedor.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1') {
                        textBox.set_value(item.get_value());

                        //poner descripcion de proveedor en caja de texto de pestaña de compras locales
                        txtPnombre = $find("<%= txtPnombre.ClientID %>");
                        txtPnombre.set_value(item.get_text());
                    }
                    else
                        textBox.set_value('');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtProveedor pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtProveedor_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbProveedor.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;

                        //poner descripcion de proveedor en caja de texto de pestaña de compras locales
                        txtPnombre = $find("<%= txtPnombre.ClientID %>");
                        txtPnombre.set_value(item.get_text());

                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'El proveedor con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }


            //--------------------------------------------------------------------------------------------------
            //Cuando se selecciona un opcion del cmbRentabilidad
            //--------------------------------------------------------------------------------------------------
            function cmbRentabilidad_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                var item = eventArgs.get_item();
                var textBox = $find('<%= txtRentabilidad.ClientID %>');

                if (item != null)
                    if (item.get_value() != '-1') {
                        textBox.set_value(item.get_value());
                    }
                    else
                        textBox.set_value('');
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando el txtRentabilidad pierde el foco
            //--------------------------------------------------------------------------------------------------
            function txtRentabilidad_OnBlur(sender, args) {
                //debugger;
                var textBox = sender;
                var combo = $find("<%= cmbRentabilidad.ClientID %>");

                var itemSelect = false;
                for (var i = 0; i < combo.get_items().get_count(); i++) {
                    var item = combo.get_items().getItem(i);
                    if (textBox.get_value() == item.get_value()) {
                        itemSelect = true;
                        combo.get_items().getItem(i).select();
                        break;
                    }
                }

                if (combo.get_value() == '-1' || combo.get_value() == '') {
                    if (textBox.get_value() != '') {
                        var mens = 'La rentabilidad con clave ' + textBox.get_value() + ' no existe';
                        textBox.set_value('');
                        radalert(mens, 600, 10, tituloMensajes);
                    }
                }

                if (!itemSelect)
                    LimpiarComboSelectIndex0(combo)
            }


            function KeyPress(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                //debugger;
                //                if (c == 39)
                //                    eventArgs.set_cancel(true);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadTabStripPrincipal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPrecios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbProductosLista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkActivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="formulario" id="divPrincipal" runat="server">
        <asp:HiddenField ID="hiddenId" runat="server" />
        <asp:HiddenField ID="hiddenRefrescapagina" runat="server" />
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
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
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt; margin-left: 10px;
            margin-right: 10px;" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribucion"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table id="TblEncabezado11" runat="server" width="100%">
            <tr>
                <td style="width: 70%; text-align: center">
                    <asp:Label ID="lblTituloProducto" runat="server" CssClass="tituloProducto" Font-Size="28px"
                        ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="margin-left: 10px; margin-right: 10px;">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Producto "></asp:Label>
                </td>
                <td colspan="2">
               <%--     <telerik:RadComboBox runat="server" ID="cmbProductosLista" Width="400px" HighlightTemplatedItems="true"
                        MaxHeight="200px" EnableLoadOnDemand="true" AutoPostBack="true" DataTextField="Prd_Descripcion"
                        DataValueField="Id_Prd" OnClientItemsRequested="cmbProductosLista_UpdateItemCountField"
                        OnDataBound="cmbProductosLista_DataBound" OnSelectedIndexChanged=""
                        EnableAutomaticLoadOnDemand="True" EnableVirtualScrolling="True" ItemsPerRequest="10"
                        ShowMoreResultsBox="True" LoadingMessage="Cargando..." OnClientDropDownOpening="Producto_Focus">
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
                        <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                            NoMatches="No hay coincidencias" />
                    </telerik:RadComboBox>--%>
                    <telerik:RadComboBox ID="cmbProductosLista" runat="server" 
                        Width="350px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                        LoadingMessage="Cargando..." AutoPostBack="True"
                        OnSelectedIndexChanged="cmbProductosLista_SelectedIndexChanged" MaxHeight="250px" EnableAutomaticLoadOnDemand="True"
                        EnableVirtualScrolling="True" ItemsPerRequest="10" ShowMoreResultsBox="True"
                        OnClientDropDownOpening="Producto_Focus">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="width: 50px; text-align: left">
                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                    </td>
                                    <td style="width: 200px; text-align: left">
                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                            NoMatches="No hay coincidencias" />
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <br />
        <div runat="server" id="formularioProductos" style="margin-left: 10px; margin-right: 10px;">
            <telerik:RadTabStrip ID="RadTabStripPrincipal" runat="server" MultiPageID="RadMultiPagePrincipal"
                SelectedIndex="1" TabIndex="-1">
                <Tabs>
                    <telerik:RadTab PageViewID="RadPageViewDGrales" Text="Datos <u>g</u>enerales " AccessKey="G">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="RadPageViewParametros" Text="<u>P</u>arámetros" AccessKey="F"
                        Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="RadPageViewIndicadores" Text="<u>I</u>ndicadores" AccessKey="R">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="RadPageViewDetalles" Text="Det<u>a</u>lles" AccessKey="A">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="RadPageViewCompLocal" Text="<u>C</u>ompras locales" AccessKey="B">
                    </telerik:RadTab>
                    <telerik:RadTab PageViewID="RadPageViewSAT" Text="<u>S</u>at" AccessKey="S">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPagePrincipal" runat="server" SelectedIndex="1"
                Width="800px">
                <!-- Aqui empieza el contenido de los tabs--->
                <telerik:RadPageView ID="RadPageViewDGrales" runat="server" BorderStyle="Solid" BorderWidth="1px">
                    <table style="font-family: vernada; font-size: 8;">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <!--Tab 1  Tabla 1-->
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Código del producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="TextId_Prd" runat="server" Width="50px" MaxLength="9"
                                                MinValue="1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="TextId_Prd_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Código usado del producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCodProd" runat="server" Width="50px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkActivo" Checked="True" runat="server" Text="Activo" OnCheckedChanged="chkActivo_CheckedChanged"
                                                AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_TextId_Prd" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtCodProd" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Descripción"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="TextPrd_Descrpcion" runat="server" Width="306px" MaxLength="100">
                                                <ClientEvents OnKeyPress="SinComilla" OnBlur="TextPrd_Descrpcion_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkProductoNuevo" runat="server" Text="Producto nuevo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_TextPrd_Descrpcion" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Presentación"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtPresentacion" runat="server" Width="70px" MaxLength="5"
                                                MinValue="1">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtPresentacion" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Tipo de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTipoProducto" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txtTipoProducto_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbTipoProducto" runat="server" Width="250px" Filter="Contains"
                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbTipoProducto_ClientSelectedIndexChanged"
                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtTipoProducto" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Sistemas propietarios"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="TextId_Spo" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="TextId_Spo_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbSisProp" runat="server" Width="250px" Filter="Contains"
                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbSisProp_ClientSelectedIndexChanged"
                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_val_TextId_Spo" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label45" runat="server" Text="Agrupado de equipos de sistemas propietarios"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtAgrupadoSpo" runat="server" MaxLength="9" Width="70px"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_val_txtAgrupadoSpo" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Categoría de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCategoria" runat="server" MaxLength="9" MinValue="1"
                                                Width="70px">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txtCategoria_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbCategoria" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmbCategoria_ClientSelectedIndexChanged"
                                                Width="250px" LoadingMessage="Cargando..." MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtCategoria" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Familia de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFam" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txtFam_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbFam" runat="server" Width="250px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                MarkFirstMatch="true" DataTextField="Descripcion" DataValueField="Id" OnClientSelectedIndexChanged="cmbFam_ClientSelectedIndexChanged"
                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Sub-familia de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtSubFam" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txtSubFam_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbSubFam" runat="server" Width="250px" Filter="Contains"
                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbSubFam_ClientSelectedIndexChanged"
                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_val_txtSubFam" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Proveedor"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtProveedor" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txtProveedor_OnBlur" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbProveedor" runat="server" Width="250px" Filter="Contains"
                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged"
                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtProveedor" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <!--Tab 1 Tabla 3-->
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Unidad de entrada"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbUentrada" runat="server" Width="200px" Filter="Contains"
                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbUentrada_OnClientSelectedIndexChanged"
                                                OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_cmbUentrada" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="Factor de conversión"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFactorConversion" runat="server" Width="50px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="Unidad de salida"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbUsalida" runat="server" Width="200px" Filter="Contains"
                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                DataValueField="Id" OnClientBlur="Combo_ClientBlur" MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Unidades de empaque"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtUempaque" runat="server" Width="50px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageViewParametros" runat="server" BorderStyle="Solid"
                    BorderWidth="1px">
                    <table style="font-family: vernada; font-size: 8;">
                        <!-- Tabla principal--->
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <!--Tab 2 Tabla 1 -->
                                    <tr>
                                        <td colspan="4">
                                            <strong>
                                                <asp:Label ID="Label17" runat="server" Text="Inventarios de seguridad"></asp:Label></strong>
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Inv. Seguridad"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtInvSeguridad" runat="server" Width="50px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkSistProp" runat="server" Text="Aparato de sistema propietario" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Tiempo de entrega"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTentrega" runat="server" Width="50px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label46" runat="server" Text="Planeación de Abasto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtPlanAbasto" runat="server" Width="150px" MaxLength="20">                                                <ClientEvents OnKeyPress="SoloAlfabetico" />
                                             <ClientEvents OnKeyPress="SoloAlfanumerico"></ClientEvents>
                                             </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label47" runat="server" Text="Minimo de compra"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtMinCompra" runat="server" Width="50px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Tiempo de transporte"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTtransporte" runat="server" Width="50px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="Rentabilidad"></asp:Label>
                                            <telerik:RadTextBox onpaste="return false" ID="txtRentabilidad" runat="server" Width="20px"
                                                MaxLength="1">
                                                <ClientEvents OnKeyPress="SoloAlfabetico" OnBlur="txtRentabilidad_OnBlur" />
                                            </telerik:RadTextBox>
                                            <telerik:RadComboBox ID="cmbRentabilidad" runat="server" Width="200px" Filter="Contains"
                                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                DataValueField="Id" OnClientSelectedIndexChanged="cmbRentabilidad_ClientSelectedIndexChanged"
                                                OnClientBlur="Combo_ClientBlur" LoadingMessage="Cargando..." MaxHeight="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_Rentabilidad" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: right">
                                            <asp:CheckBox ID="chkComprasLocales" runat="server" Text="Compras locales" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <!--Tab 2 Tabla 1 -->
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text="Meses de amortización"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtAmortizacion" runat="server" Width="70px" MaxLength="3"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_Amortizacion" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" Text="Pesos por concepto técnico de servicio"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtPesos" runat="server" Width="70px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text="Máximo en existencia final"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtExistencia" runat="server" Width="70px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" Text="Ubicación"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtUbicacion" runat="server" Width="70px"
                                                MaxLength="5">
                                                <ClientEvents OnKeyPress="SoloAlfabetico" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label44" runat="server" Text="Contribuci&oacute;n"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtContribucion" runat="server" Width="70px" MaxLength="9"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPorUtilidades" runat="server" Text="Porcentaje de participaci&oacute;n de utilidades"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtPorUtilidades" runat="server" Width="70px" MaxLength="3"
                                                MinValue="0">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageViewIndicadores" runat="server" BorderStyle="Solid"
                    BorderWidth="1px">
                    <table style="font-family: vernada; font-size: 8;">
                        <!-- Tabla principal--->
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <strong>
                                                <asp:Label ID="Label26" runat="server" Text="Administración de inv."></asp:Label></strong>
                                            <hr />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td colspan="2" style="text-align: center">
                                            <strong>
                                                <asp:Label ID="Label27" runat="server" Text="Inventarios"></asp:Label></strong>
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label28" runat="server" Text="Asignado"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtAsignado" runat="server" Width="50px" Enabled="false"
                                                MaxLength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label29" runat="server" Text="Inicial"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtInicial" runat="server" Width="50px" Enabled="false"
                                                MaxLength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label30" runat="server" Text="Ordenado"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtOrdenado" runat="server" Width="50px" Enabled="false"
                                                MaxLength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label33" runat="server" Text="Final"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFinal" runat="server" Width="50px" Enabled="false"
                                                MaxLength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label31" runat="server" Text="Tr&aacute;nsito"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTransito" runat="server" Width="50px" Enabled="false"
                                                MaxLength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label32" runat="server" Text="F&iacute;sico"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFisico" runat="server" Width="50px" Enabled="false"
                                                MaxLength="9">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageViewDetalles" runat="server" BorderStyle="Solid"
                    BorderWidth="1px">
                    <table style="font-family: vernada; font-size: 8;">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                <telerik:RadGrid ID="rgPrecios" runat="server" GridLines="None" DataMember="listaPrecios"
                                                    PageSize="8" AllowPaging="True" AutoGenerateColumns="False" Width="95%" AllowMultiRowSelection="True"
                                                    OnNeedDataSource="grdPrecios_NeedDataSource" OnUpdateCommand="grdPrecios_UpdateCommand"
                                                    OnPreRender="grdPrecios_PreRender" OnItemDataBound="grdPrecios_ItemDataBound"
                                                    OnPageIndexChanged="grdPrecios_PageIndexChanged">
                                                    <MasterTableView Name="Master" CommandItemDisplay="None" DataKeyNames="Id_Emp,Id_Cd,Id_Prd,Id_Pre,Prd_Actual"
                                                        EditMode="EditForms" DataMember="listaPrecios" HorizontalAlign="NotSet" PageSize="8"
                                                        Width="100%" AutoGenerateColumns="False" NoMasterRecordsText="No hay registros para mostrar.">
                                                        <ExpandCollapseColumn Visible="True">
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderText="Empresa" UniqueName="Id_Emp" DataField="Id_Emp"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Cd" UniqueName="Id_Cd" DataField="Id_Cd" Display="false"
                                                                ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="Producto" UniqueName="Id_Prd" DataField="Id_Prd"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderText="TP" UniqueName="Id_Pre" DataField="Id_Pre" Display="false"
                                                                ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Prd_Actual" HeaderText="Prd_Actual" UniqueName="Prd_Actual"
                                                                Display="false" ReadOnly="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Fec. inicial" DataField="Prd_FechaInicio"
                                                                UniqueName="Prd_FechaInicio">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaInicio" runat="server" Text='<%# Bind("Prd_FechaInicio","{0:dd/MM/yyyy}") %>'
                                                                        Width="200px" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadDatePicker ID="datePickerFechaInicio" runat="server" MinDate="1900-01-01"
                                                                        DbSelectedDate='<%# Eval("Prd_FechaInicio") %>'>
                                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                                        <Calendar ID="dateCalendarFechaInicio" runat="server" RangeMinDate="1900-01-01">
                                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                TodayButtonCaption="Hoy" />
                                                                        </Calendar>
                                                                    </telerik:RadDatePicker>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Fec. final" DataField="Prd_FechaFin" UniqueName="Prd_FechaFin">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaFin" runat="server" Text='<%# Bind("Prd_FechaFin","{0:dd/MM/yyyy}") %>'
                                                                        Width="200px" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadDatePicker ID="datePickerFechaFin" runat="server" MinDate="1900-01-01"
                                                                        DbSelectedDate='<%# Eval("Prd_FechaFin") %>'>
                                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                                        <Calendar ID="dateCalendarFechaFin" runat="server" RangeMinDate="1900-01-01">
                                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                TodayButtonCaption="Hoy" />
                                                                        </Calendar>
                                                                    </telerik:RadDatePicker>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Tipo de precio" DataField="Pre_Descripcion"
                                                                UniqueName="Pre_Descripcion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoPrecio" runat="server" Text='<%# Eval("Pre_Descripcion") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblTipoPrecioEdit" runat="server" Text='<%# Eval("Pre_Descripcion") %>'
                                                                        Font-Bold="true" />
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Descripci&oacute;n" DataField="Prd_PreDescripcion"
                                                                UniqueName="Prd_PreDescripcion">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrd_PreDescripcion" runat="server" Text='<%# Eval("Prd_PreDescripcion") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox onpaste="return false" ID="txtPrd_PreDescripcion" runat="server"
                                                                        Text='<%# Eval("Prd_PreDescripcion") %>' MaxLength="20">
                                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                    </telerik:RadTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Pesos" DataField="Prd_Pesos" UniqueName="Prd_Pesos">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrd_Pesos" runat="server" Text='<%# Eval("Prd_Pesos") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtPrd_Pesos" runat="server" Width="100px" MaxLength="9"
                                                                        MinValue="0" Text='<%# Eval("Prd_Pesos") %>'>
                                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                                EditText="Editar" HeaderText="Editar">
                                                            </telerik:GridEditCommandColumn>
                                                        </Columns>
                                                        <EditFormSettings ColumnNumber="6" CaptionDataField="Id_Prd" CaptionFormatString="Editar datos de precio de producto con clave {0}"
                                                            InsertCaption="Agregar nuevo precio de producto">
                                                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" Width="95%"
                                                                BorderColor="#000000" BorderWidth="1" />
                                                            <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                                                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                                                            <EditColumn ButtonType="ImageButton" InsertText="Agregar" UpdateText="Actualizar"
                                                                EditText="Editar" UniqueName="EditCommandColumn1" CancelText="Cancelar">
                                                            </EditColumn>
                                                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                    <PagerStyle NextPagesToolTip="Páginas siguientes" FirstPageToolTip="Primera página"
                                                        LastPageToolTip="Última página" NextPageToolTip="Siguiente página" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                                        PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" />
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <ClientSettings>
                                                        <ClientEvents OnRowDblClick="rgPrecios_ClientRowDblClick" />
                                                        <Selecting AllowRowSelect="true" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageViewCompLocal" runat="server" BorderStyle="Solid"
                    BorderWidth="1px">
                    <table style="font-family: vernada; font-size: 8;">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label34" runat="server" Text="Fabricante"></asp:Label></strong>
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label35" runat="server" Text="Nombre"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtFnombre" runat="server" Width="150px"
                                                MaxLength="100">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtFnombre" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label36" runat="server" Text="Código de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtFcodigo" runat="server" Width="100px"
                                                MaxLength="30">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtFcodigo" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label37" runat="server" Text="Descripción de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtFdescripcion" runat="server" Width="150px"
                                                MaxLength="100">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtFdescripcion" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label38" runat="server" Text="Presentación de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtFpresentacion" runat="server" Width="100px"
                                                MaxLength="20">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtFpresentacion" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label39" runat="server" Text="Proveedor"></asp:Label></strong>
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label40" runat="server" Text="Nombre"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtPnombre" runat="server" Width="150px"
                                                MaxLength="100">
                                                <ClientEvents OnKeyPress="SoloAlfabetico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtPnombre" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label41" runat="server" Text="Código de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtPcodigo" runat="server" Width="100px"
                                                MaxLength="30">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtPcodigo" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label42" runat="server" Text="Descripción de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtPdescripcion" runat="server" Width="150px"
                                                MaxLength="100">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtPdescripcion" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label43" runat="server" Text="Presentación de producto"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox onpaste="return false" ID="txtPpresentacion" runat="server" Width="100px"
                                                MaxLength="20">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Val_txtPpresentacion" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageViewSAT" runat="server" BorderStyle="Solid"
                    BorderWidth="1px">
                    <table style="font-family: vernada; font-size: 8;">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label48" runat="server" Text="SAT"></asp:Label></strong>
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label49" runat="server" Text="Productos y Servicios"></asp:Label>
                                        </td>
                                        <td>
						 <telerik:RadComboBox ID="cmbClaveProductoServicio" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        EnableLoadOnDemand="true" Filter="Contains" MarkFirstMatch="true" Width="400px"
                                                        LoadingMessage="Cargando...">
                                                    </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label50" runat="server" Text="Unidad"></asp:Label>
                                        </td>
                                        <td>
						 <telerik:RadComboBox ID="cmbClaveUnidad" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        EnableLoadOnDemand="true" Filter="Contains" MarkFirstMatch="true" Width="400px"
                                                        LoadingMessage="Cargando...">
                                                    </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label51" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>

            </telerik:RadMultiPage>
        </div>
    </div>
</asp:Content>

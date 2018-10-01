/*

12 May 2018 
Proyectos_tablaAgrupada_PropuestaTE.js

*/

var MAX_CONTROLS = 0;
var VISUALIZANDO_REPORTE = 0;

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
    c = isNaN(c = Math.abs(c)) ? 2 : c,
    d = d == undefined ? "." : d,
    t = t == undefined ? "," : t,
    s = n < 0 ? "-" : "",
    i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
    j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Calcular_CostoEnUso(D) {
    var costoEnUso = 0.0;
    if (D.Vap_Cantidad != '') {
        if (D.COP_EsQuimico == true) {
            if (D.COP_DilucionConsecuente != '') {
                var precio = D.Vap_Precio; //ProductoActual.Prd_Pesos;
                var unidadesPresentacion = D.Prd_UniEmp;
                var consumoMensualEnLitrosDiluidos = ((unidadesPresentacion * D.Vap_Cantidad) * (parseInt(D.COP_DilucionConsecuente) + 1));
                if (consumoMensualEnLitrosDiluidos != 0.0) {
                    costoEnUso = (D.Vap_Cantidad  * precio) / consumoMensualEnLitrosDiluidos;
                }
            }
        }
    }
    return costoEnUso;
};

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Propuesta_ModoEdicion() {
    for (var i = 0; i < MAX_CONTROLS; i++) {
            
        var chbAplDilucion = $('#chbAplDilucion_' + i).is(":checked");

        $('#lbVap_Cantidad_' + i).css('display', 'none');
        $('#tbVap_Cantidad_' + i).css('display', 'block');

        $('#lbCOP_DilucionAntecedente_' + i).css('display', 'none');
        $('#lbCOP_DilucionConsecuente_' + i).css('display', 'none');

        if (chbAplDilucion == false) {
            // deshabilitar TextBoxs
            $('#tbCOP_DilucionAntecedente_' + i).css('display', 'block');
            $('#tbCOP_DilucionAntecedente_' + i).prop('disabled', true);

            $('#tbCOP_DilucionConsecuente_' + i).css('display', 'block');
            $('#tbCOP_DilucionConsecuente_' + i).prop('disabled', true);
        } else {            
            // habilitar TextBoxs
            $('#tbCOP_DilucionAntecedente_' + i).css('display', 'block');
            $('#tbCOP_DilucionAntecedente_' + i).prop('disabled', false);

            $('#tbCOP_DilucionConsecuente_' + i).css('display', 'block');
            $('#tbCOP_DilucionConsecuente_' + i).prop('disabled', false);
        }

        $('#chbAplDilucion_' + i).css('display', 'block');

        //  PROPUESTA TECNICA TAB        
        $('#lbProductoActual_' + i).css('display', 'none');
        $('#tbProductoActual_' + i).css('display', 'block');
        var lbProductoActual = $('#lbProductoActual_' + i).text();
        $('#tbProductoActual_' + i).val(lbProductoActual);        

        $('#lbSituacionActual_' + i).css('display', 'none');
        $('#tbSituacionActual_' + i).css('display', 'block');
        var lbSituacionActual = $('#lbSituacionActual_' + i).text();
        $('#tbSituacionActual_' + i).text(lbSituacionActual);        
        
        $('#lbVentajasKey_' + i).css('display', 'none');
        $('#tbVentajasKey_' + i).css('display', 'block');
        var lbVentajasKey = $('#lbVentajasKey_' + i).text();
        $('#lbVentajasKey_' + i).text(lbVentajasKey);        
        
        $('#btnImagenSolucionActual_' + i).css('display', 'block');
        $('#btnImagenSolucionKey_' + i).css('display', 'block');

        // Resplada las imagenes actuales
        var imgImagenProdActual = $('#imgImagenProdActual_'+i).attr('src');
        $('#hf_imgImagenProdActual_' + i).val(imgImagenProdActual);

        var imgImagenSolucionKey = $('#imgImagenSolucionKey_'+i).attr('src');
        $('#hf_imgImagenSolucionKey_' + i).val(imgImagenSolucionKey);
        
            
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cancelar_Edicion() {
    for (var i = 0; i < MAX_CONTROLS; i++) {

        var chbAplDilucion = $('#chbAplDilucion_' + i).is(":checked");

        $('#lbVap_Cantidad_' + i).css('display', 'block');
        var lbVap_Cantidad = $('#lbVap_Cantidad_' + i).text();
        $('#tbVap_Cantidad_' + i).css('display', 'none');
        $('#tbVap_Cantidad_' + i).val(lbVap_Cantidad);


        $('#lbCOP_DilucionAntecedente_' + i).css('display', 'none');
        $('#lbCOP_DilucionConsecuente_' + i).css('display', 'none');

        if (chbAplDilucion == false) {
            // deshabilitar TextBoxs
            $('#tbCOP_DilucionAntecedente_' + i).css('display', 'none');
            $('#tbCOP_DilucionAntecedente_' + i).prop('disabled', true);

            $('#tbCOP_DilucionConsecuente_' + i).css('display', 'none');
            $('#tbCOP_DilucionConsecuente_' + i).prop('disabled', true);
        } else {
            // habilitar TextBoxs
            $('#tbCOP_DilucionAntecedente_' + i).css('display', 'none');
            $('#tbCOP_DilucionAntecedente_' + i).prop('disabled', false);

            $('#tbCOP_DilucionConsecuente_' + i).css('display', 'none');                
            $('#tbCOP_DilucionConsecuente_' + i).prop('disabled', false);
        }
        
        // Contenido de label a textboc        
        lbCOP_DilucionA = $('#lbCOP_DilucionAntecedente_'+i).text();
        $('#tbCOP_DilucionAntecedente_' + i).val(lbCOP_DilucionA);
                              
        lbCOP_DilucionC = $('#lbCOP_DilucionConsecuente_' + i).text();
        $('#tbCOP_DilucionConsecuente_' + i).val(lbCOP_DilucionC);

        // Activa labels
        $('#lbCOP_DilucionAntecedente_' + i).css('display', 'block');
        $('#lbCOP_DilucionConsecuente_' + i).css('display', 'block');                

        // chb
        $('#chbAplDilucion_' + i).css('display', 'none');

        //  PROPUESTA TECNICA TAB

        $('#lbProductoActual_' + i).css('display', 'block');
        $('#tbProductoActual_' + i).css('display', 'none');
        var lbProductoActual = $('#lbProductoActual_' + i).text();
        $('#tbProductoActual_' + i).val(lbProductoActual);
        
        $('#lbSituacionActual_' + i).css('display', 'block');
        $('#tbSituacionActual_' + i).css('display', 'none');
        var lbSituacionActual = $('#lbSituacionActual_' + i).text();
        $('#tbSituacionActual_' + i).val(lbSituacionActual);
        
        $('#lbVentajasKey_' + i).css('display', 'block');
        $('#tbVentajasKey_' + i).css('display', 'none');
        var lbVentajasKey = $('#lbVentajasKey_' + i).text();
        $('#tbVentajasKey_' + i).val(lbVentajasKey);
        
        $('#btnImagenSolucionActual_' + i).css('display', 'none');
        $('#btnImagenSolucionKey_' + i).css('display', 'none');
        
        // Resplada las imagenes actuales        
                                      
        var imgImagenProdActual = $('#hf_imgImagenProdActual_' + i).val();
        $('#imgImagenProdActual_' + i).attr('src', imgImagenProdActual);
        
        var imgImagenSolucionKey = $('#hf_imgImagenSolucionKey_' + i).val();
        $('#imgImagenSolucionKey_' + i).attr('src', imgImagenSolucionKey);
        
        Calcular_Renglon_ByNo(i);

    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Propuesta_ModoLectura() {
    for (var i = 0; i < MAX_CONTROLS; i++) {
        AplDilucion = $('#hfId_AplDilucion_' + i).val();

        $('#lbVap_Cantidad_' + i).css('display', 'block');
        $('#tbVap_Cantidad_' + i).css('display', 'none');

        $('#tbCOP_DilucionAntecedente_' + i).css('display', 'none');
        $('#tbCOP_DilucionAntecedente_' + i).prop('disabled', false);

        $('#tbCOP_DilucionConsecuente_' + i).css('display', 'none');
        $('#tbCOP_DilucionConsecuente_' + i).prop('disabled', false);

        if (AplDilucion == 0) {
            // no visible 
            $('#lbCOP_DilucionAntecedente_' + i).css('display', 'none');
            $('#lbCOP_DilucionConsecuente_' + i).css('display', 'none');
        } else {
            // si visible 
            $('#lbCOP_DilucionAntecedente_' + i).css('display', 'block');
            $('#lbCOP_DilucionConsecuente_' + i).css('display', 'block');
        }

        $('#chbAplDilucion_' + i).css('display', 'none');

        //$('#chbAplDilucion_' + i).prop('disabled', );
        
        $('#lbProductoActual_' + i).css('display', 'block');
        $('#tbProductoActual_' + i).css('display', 'none');
        
        $('#lbSituacionActual_' + i).css('display', 'block');
        $('#tbSituacionActual_' + i).css('display', 'none');

        $('#lbVentajasKey_' + i).css('display', 'block');
        $('#tbVentajasKey_' + i).css('display', 'none');

        $('#btnImagenSolucionActual_' + i).css('display', 'none');
        $('#btnImagenSolucionKey_' + i).css('display', 'none');

    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Valuacion_Cargar(obj) {

    var idcte = $(obj).data('idcte');
    var idval = $(obj).data('idval');
    var idop = $(obj).data('idop');

    $('#hf_Id_Op').val(idop);
    $('#hf_Id_Val').val(idval);
    $('#hf_Id_Cte').val(idcte);
        
    //alert('Oportunidad: ' + idop);

    alertify.success('Propuesta: ' + idval + ', cte.:' + idcte);

    $('#dvModalPropuestaTE_ver2').modal('show');

    $('#tblPropuestaEconomica > tbody').empty();
    $('#tblPropuestaTecnica > tbody').empty();

    var modal_w = $('#dvModalPropuestaTE_ver2').width();
    modal_w = parseInt(modal_w);
    var MWEnt2 = (modal_w / 2) - 5;

    Cargar_PropuestaTecnoEconomica(_CRM_Usuario_Rik, 0, idcte, idval, function () {

        try {
            if (lst.length <= 0) {
                alertify.error('Error: El detalle del documento no contiene registro.');
                return;
            }
        } catch (err) {
            alertify.error('Error: Ocurrio un error al tratar de cargar la valuaci&oacute;n.');
            return;
        }

        for (var i = 0; i < lst.length; i++) {

            if (lst[i].CPT_RecursoImagenProductoActual == "") {
                lst[i].CPT_RecursoImagenProductoActual = _ApplicationUrl + '/imgupload/imagen_vacia.jpg'
            }

            if (lst[i].CPT_RecursoImagenSolucionKey == "") {
                lst[i].CPT_RecursoImagenSolucionKey = _ApplicationUrl + '/imgupload/imagen_vacia.jpg'
            }

            var CostoEnUso = Calcular_CostoEnUso(lst[i]).formatMoney(2, '.', ',');
            var GastoMensual = lst[i].GastoMensual.formatMoney(2, '.', ',');
            var Vap_Precio = lst[i].Vap_Precio.formatMoney(2, '.', ',');

            AplDilucion = lst[i].AplDilucion;

            if (AplDilucion == 1) {
                var lbDilucionAntecedente = '<label id="lbCOP_DilucionAntecedente_' + i + '" style="display:block;">' + lst[i].COP_DilucionAntecedente + '</label>';
                var DilucionAntecedente = '<input type="text" id="tbCOP_DilucionAntecedente_' + i + '" value="' + lst[i].COP_DilucionAntecedente + '" ' +
                    'data-no="' + i + '" ' +
                    'onblur="Calcular_Renglon(this);" style="display:none; width:30px;">';
                var lbDilucionConsecuente = '<label id="lbCOP_DilucionConsecuente_' + i + '" style="display:block;">' + lst[i].COP_DilucionConsecuente + '</label>';
                var DilucionConsecuente = '<input type="text" id="tbCOP_DilucionConsecuente_' + i + '" value="' + lst[i].COP_DilucionConsecuente + '" ' +
                    'data-no="' + i + '" ' +
                    'onblur="Calcular_Renglon(this);" style="display:none; width:30px;">';
            } else {
                var lbDilucionAntecedente = '<label id="lbCOP_DilucionAntecedente_' + i + '" style="display:none;">' + lst[i].COP_DilucionAntecedente + '</label>';
                var DilucionAntecedente = '<input type="text" id="tbCOP_DilucionAntecedente_' + i + '" value="' + lst[i].COP_DilucionAntecedente + '" ' +
                    'data-no="' + i + '" ' +
                    'onblur="Calcular_Renglon(this);" style="display:none; width:30px;">';
                var lbDilucionConsecuente = '<label id="lbCOP_DilucionConsecuente_' + i + '" style="display:none;">' + lst[i].COP_DilucionConsecuente + '</label>';
                var DilucionConsecuente = '<input type="text" id="tbCOP_DilucionConsecuente_' + i + '" value="' + lst[i].COP_DilucionConsecuente + '" ' +
                    'data-no="' + i + '" ' +
                    'onblur="Calcular_Renglon(this);" style="display:none; width:30px;">';
            }

            var row = $('<tr>');
            row.append($('<td>').append(
            			'<input type="hidden" id="hfId_VapDet_' + i + '" value="' + lst[i].Id_VapDet + '">' +
                        '<input type="hidden" id="hfId_Prd_' + i + '" value="' + lst[i].Id_Prd + '">' +
                        '<input type="hidden" id="hfId_AplDilucion_' + i + '" value="' + lst[i].AplDilucion + '">' +
                        '<input type="hidden" id="hfPrd_UniEmp_' + i + '" value="' + lst[i].Prd_UniEmp + '">' +
						'<p>' + lst[i].Id_Prd + '</p>'
        	));
            row.append($('<td>').append(
                '<label id="Prd_Descripcion_' + i + '">' + lst[i].Prd_Descripcion + '</label>'
            ));
            row.append($('<td style="text-align:right;">').append(
                '<label id="Vap_Precio_' + i + '">' + Vap_Precio + '</label>'
            ));
            row.append($('<td style="text-align:center;">').append(
                '<label id="Prd_Presentacion_' + i + '">' + lst[i].Prd_Presentacion + '</label>'
            ));
            row.append($('<td style="text-align:center;">').append(
                '<label id="lbVap_Cantidad_' + i + '">' + lst[i].Vap_Cantidad + '</label>' +
                '<input ' +
                'type="text" ' +
                'data-no="' + i + '" ' +
                'onblur="Calcular_Renglon(this);" ' +
                'id="tbVap_Cantidad_' + i + '" value=' + lst[i].Vap_Cantidad + ' style="display:none; width:40px;"/>'
            ));
            row.append($('<td style="text-align:center;">').append(
                '<label id="ConsumoMensualL_' + i + '">' + lst[i].ConsumoMensualL + '</label>'
            ));
            row.append($('<td style="text-align:right;">').append(
                '<label id="GastoMensual_' + i + '">' + GastoMensual + '</label>'
            ));
            row.append($('<td align="center">').append(
                '<table id="rblDilucionEditar">' +
                    '<tr>' +
                        '<td>' +
                            '<input ' +
                            'data-no="' + i + '" ' +
                            'data-vapdet="' + lst[i].Id_VapDet + '" ' +
                            'style="display:none" ' +
                            'onclick="chbAplDilucion_click(this);" ' +
                            'type="checkbox" ' +
                            'id="chbAplDilucion_' + i + '" name="chbAplDilucion" value=' + lst[i].AplDilucion + ' ' +
                            'style="width:30px;">' +
                        '</td><td>' +
                            lbDilucionAntecedente +
                            DilucionAntecedente +
                        '</td>' +
                        '<td>:</td>' +
                        '<td>' +
                            lbDilucionConsecuente +
                            DilucionConsecuente +
                        '</td>' +
                    '</tr>' +
                '</table>'
            ));

            row.append($('<td style="text-align:center;">').append(
                '<label id="ConsumoMensualLDiluidos_' + i + '">' + lst[i].ConsumoMensualLDiluidos + '</label>'
            ));
            row.append($('<td style="text-align:right;">').append(
                '<label id="CostoEnUso_' + i + '">' + CostoEnUso + '</label>'
            ));

            $('#tblPropuestaEconomica > tbody').append(row);

            //  |-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-
            var row_y = $('<tr>');

            row_y.append($('<td>').append(

                '<div class="panel panel-default" style="width:99%; height:350px;">' +
                '<div class="panel-heading" style="height: 50px;">' +
                    '<label id="lbProductoActual_' + i + '">' + lst[i].CPT_ProductoActual + '</label>' +
                    '<input type="text" id="tbProductoActual_' + i + '" value="' + lst[i].CPT_ProductoActual + '" style="display:none; width:100%;">' +
                '</div>' +
                '<div class="panel-body" style="height: 198px;">' +
                    '<table style="width:' + MWEnt2 + 'px; max-width:' + MWEnt2 + '">' +
                        '<tr>' +
                            '<td align="center">' +
                            '<input type="hidden" id="hf_imgImagenProdActual_' + i + '" value="' + lst[i].CPT_RecursoImagenProductoActual + '">' +
                            '<img class="img-rounded" style="width: 140px; height: 140px;" id="imgImagenProdActual_' + i + '" src="' + lst[i].CPT_RecursoImagenProductoActual + '">' +
                            '</td>' +
                        '</tr>' +
                        '<tr>' +
                            '<td align="center">' +
                            '<button ' +
                                'style="margin-top:4px; display:none;" ' +
                                'type="button" ' +
                                'data-"button" ' +
                                'data-contenedor="#imgImagenProdActual_' + i + '" ' +
                                'onclick="BuscarImagen(this);" ' +
                                'class="btn btn-default btn-sm" ' +
                                'id="btnImagenSolucionActual_' + i + '">Elegir imagen' +
                            '</button>' +
                            '</td>' +
                        '</tr>' +
                    '</table>' +
                '</div>' +
                '<div class="panel-footer" style="height: 99px;" >' +
                    '<p>Situaci&oacute;n actual</p>' +
                    '<label id="lbSituacionActual_' + i + '">' + lst[i].CPT_SituacionActual + '</label>' +
                    '<textarea style="display:none; width:100%" id="tbSituacionActual_' + i + '" maxlength="250">' + lst[i].CPT_SituacionActual + '</textarea>' +
                '</div>' +
                '</div>'
        	));

            row_y.append($('<td>').append(

                '<div class="panel panel-default" style="width:99%; height:350px;">' +
                '<div class="panel-heading" style="height: 50px;">' +
                    '<label>' + lst[i].ProductoKey + '</label>' +
                '</div>' +
                '<div class="panel-body" style="height: 198px;">' +
                       '<table style="width:' + MWEnt2 + 'px; max-width:' + MWEnt2 + '">' +
                        '<tr>' +
                            '<td align="center">' +
                            '<input type="hidden" id="hf_imgImagenSolucionKey_' + i + '" value="' + lst[i].CPT_RecursoImagenSolucionKey + '">' +
                            '<img class="img-rounded" style="width: 140px; height: 140px;" id="imgImagenSolucionKey_' + i + '" src="' + lst[i].CPT_RecursoImagenSolucionKey + '">' +
                            '</td>' +
                        '</tr>' +
                        '<tr>' +
                            '<td align="center">' +
                            '<button ' +
                                'style="margin-top:4px; display:none;" ' +
                            'type="button" ' +
                            'data-"button" ' +
                            'data-contenedor="#imgImagenSolucionKey_' + i + '" ' +
                            'onclick="BuscarImagen(this);" ' +
                            'class="btn btn-default btn-sm" id="btnImagenSolucionKey_' + i + '">Elegir imagen</button>' +
                            '</td>' +
                        '</tr>' +
                    '</table>' +
                '</div>' +
                '<div class="panel-footer" style="height: 99px;" >' +
                    '<p>Ventajas KEY</p>' +
                    '<label id="lbVentajasKey_' + i + '">' + lst[i].CPT_VentajasKey + '</label>' +
                    '<textarea style="display:none; width:100%" id="tbVentajasKey_' + i + '" maxlength="250">' + lst[i].CPT_VentajasKey + '</textarea>' +
                '</div>' +
                '</div>'

            ));

            $('#tblPropuestaTecnica > tbody').append(row_y);

            if (AplDilucion == 1) {
                $('#chbAplDilucion_' + i).prop('checked', true);
            }

            Calcular_Renglon_ByNo(i);
        }

        $('#rowError').css('display', 'none');
        $('#rowLoagin').css('display', 'none');
        $('#rowPropuestaAcciones').css('display', 'block');

        MAX_CONTROLS = lst.length;

        //
        Cargar_PropuestaTecnoEconomicaEnc(_CRM_Usuario_Rik, 0, idcte, idval, function (Vap_Estatus, Vap_Estatus2) {
            // Verifica estatus 
            //var Vap_Estatus = $('#hf_Vap_Estatus').val();
            //var Vap_Estatus2 = $('#hf_Vap_Estatus2').val();
            console.log(Vap_Estatus2);

            if (_Parametro_ControlesSoloLectura == 1) {
                Vap_Estatus2 = 0; // Para que no se pueda editar.
            }

            if (Vap_Estatus2 == 3) {
                $('#btnPropuestaEditar').prop('disabled', false);
                $('#btnPropuestaAceptar').prop('disabled', false);
            } else {
                $('#btnPropuestaEditar').prop('disabled', true);
                $('#btnPropuestaAceptar').prop('disabled', true);
            }



        });

    });

    window.resizeTo(screen.width - 300, screen.height);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Calcular_Renglon_ByNo(no) {

    is_checked = $('#chbAplDilucion_' + no).is(":checked");
    if (is_checked == true) {

        tbCOP_DilucionA = $('#tbCOP_DilucionAntecedente_' + no).val();        
        if (tbCOP_DilucionA == '') {
            $('#tbCOP_DilucionAntecedente_' + no).val(1);
        }

        tbCOP_DilucionD = $('#tbCOP_DilucionConsecuente_' + no).val();
        if (tbCOP_DilucionA == '') {
            $('#tbCOP_DilucionConsecuente_' + no).val(1);
        }        

    } else {

    }

    //var no = $(obj).data("no");

    var Precio = $('#Vap_Precio_' + no).text();
    Precio = Precio.replace(',', '');
                    
    Precio = parseFloat(Precio);
    if (isNaN(Precio)) {
        Precio = 0;
    }

    var Cantidad = $('#tbVap_Cantidad_' + no).val();
    Cantidad = Cantidad.replace(',', '');
                        
    Cantidad = parseFloat(Cantidad);
    if (isNaN(Cantidad)) {
        Cantidad = 0;
    }

    var GastoMensual = Precio * Cantidad;
    //GastoMensual = GastoMensual.replace(',', '');
        
    if (isNaN(GastoMensual)) {
        GastoMensual = 0;
    }
    //GastoMensual = GastoMensual.toFixed(2);
    var fGastoMensual = GastoMensual;
    var GastoMensual = GastoMensual.formatMoney(2, '.', ',');
    

    //var Vap_Precio = lst[i].Vap_Precio.formatMoney(2, '.', ',');

    //var CostoEnUso = Calcular_CostoEnUso(lst[i]).formatMoney(2, '.', ',');
    
    $('#GastoMensual_' + no).text(GastoMensual);

    var UniEmp = $('#hfPrd_UniEmp_' + no).val();

    var ConsumoLitros = Cantidad * UniEmp;
    ConsumoLitros = ConsumoLitros.toFixed(2);

    $('#ConsumoMensualL_' + no).text(ConsumoLitros);

    DilucionC = $('#tbCOP_DilucionConsecuente_' + no).val();
    DilucionC = parseFloat(DilucionC);
    if (isNaN(DilucionC)) {
        DilucionC = 0;
    }

    /*
    ConsumoMensualLDiluidos = ConsumoLitros * (DilucionC + 1);
    ConsumoMensualLDiluidos = parseFloat(ConsumoMensualLDiluidos);
    if (isNaN(ConsumoMensualLDiluidos)) {
        ConsumoMensualLDiluidos = 0;
    }
    ConsumoMensualLDiluidos = ConsumoMensualLDiluidos.toFixed(2);
    */

    ConsumoMensualLDiluidos = ConsumoLitros * (DilucionC + 1);
    ConsumoMensualLDiluidos = ConsumoMensualLDiluidos.toFixed(2);

    $('#ConsumoMensualLDiluidos_' + no).text(ConsumoMensualLDiluidos);

    CostoEnUso = fGastoMensual / ConsumoMensualLDiluidos;
    var CostoEnUso = CostoEnUso.formatMoney(2, '.', ',');
    $('#CostoEnUso_' + no).text(CostoEnUso);
        
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function PrevisualizarPropuesta(parameter) {

    VISUALIZANDO_REPORTE = 1;

    parameter = parseInt(parameter);

    IdOp = $('#hf_Id_Op').val();
    IdVal = $('#hf_Id_Val').val();
    IdCte = $('#hf_Id_Cte').val();
    
    $('#divPropuestaDetalle').css('display', 'none');
    $('#pnlVisorDeReporte').css('display', 'block');

    setTimeout(function () {
        $('#lbPreparandoReporte').css('display', 'none');
    }, 2000);

    // Hay que pasar el RIK aqui.
    $("#iframeVisorReporte").attr("src", 'Propuestas/VisorReportesPropuestaTecnoEconomica.aspx?IdRik=' + _CRM_Usuario_Rik + '&idTipoRep=' + parameter + '&idCte=' + IdCte + '&idVal=' + IdVal + '&idOp=' + IdOp);
    
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Calcular_Renglon(obj) {

    var no = $(obj).data("no");
    Calcular_Renglon_ByNo(no) 

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function chbAplDilucion_click(obj) {
    var is_checked = $(obj).is(":checked");
    var no = $(obj).data("no");

    if (is_checked == true) {
        //$('#tbCOP_DilucionAntecedente_' + no).css('display', 'block');
        $('#tbCOP_DilucionAntecedente_' + no).prop('disabled', false);
        //$('#tbCOP_DilucionConsecuente_' + no).css('display', 'block');
        $('#tbCOP_DilucionConsecuente_' + no).prop('disabled', false);
    } else {
        //$('#tbCOP_DilucionAntecedente_' + no).css('display', 'none');
        $('#tbCOP_DilucionAntecedente_' + no).prop('disabled', true);
        $('#tbCOP_DilucionAntecedente_' + no).val("");
        //$('#tbCOP_DilucionConsecuente_' + no).css('display', 'none');
        $('#tbCOP_DilucionConsecuente_' + no).prop('disabled', true);
        $('#tbCOP_DilucionConsecuente_' + no).val("");
    }
    Calcular_Renglon_ByNo(no);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Propuesta_Guardar() {
    var Op = $('#hf_Id_Op').val();
    Op = parseInt(Op);
    if (isNaN(Op)) {
        Op = 0;
    }
    var Cte = $('#hf_Id_Cte').val();
    Cte = parseInt(Cte);
    if (isNaN(Cte)) {
        Cte = 0;
    }
    var Id_Val = $('#hf_Id_Val').val();
    Id_Val = parseInt(Id_Val);
    if (isNaN(Id_Val)) {
        Id_Val = 0;
    }

    for (var i = 0; i < MAX_CONTROLS; i++) {

        var Prd = $('#hfId_Prd_' + i).val();
        var Cantidad = $('#tbVap_Cantidad_' + i).val();
        var AplDilucion = 0;

        var DilucionA = $('#tbCOP_DilucionAntecedente_' + i).val();
        var DilucionC = $('#tbCOP_DilucionConsecuente_' + i).val();

        var fCantidad = $('#tbVap_Cantidad_' + i).val();
        fCantidad = fCantidad.replace(',', '');
        fCantidad = parseFloat(Cantidad);
        if (isNaN(fCantidad)) {
            fCantidad = 0;
        }
        fCantidad = fCantidad.toFixed(2);
        
        var chbAplDilucion = $('#chbAplDilucion_' + i).is(":checked");
        if (chbAplDilucion == true) {
            // Aplica

            AplDilucion = 1;
            $('#lbCOP_DilucionAntecedente_' + i).text(DilucionA);
            $('#lbCOP_DilucionConsecuente_' + i).text(DilucionC);

            $('#lbCOP_DilucionAntecedente_' + i).css('display','block');
            $('#lbCOP_DilucionConsecuente_' + i).css('display', 'block');

            $('#tbCOP_DilucionAntecedente_' + i).val(DilucionA);
            $('#tbCOP_DilucionConsecuente_' + i).val(DilucionC);

            $('#tbCOP_DilucionAntecedente_' + i).prop('disabled', false);
            $('#tbCOP_DilucionConsecuente_' + i).prop('disabled', false);

        } else {
            // NO Aplica
            DilucionA = 0;
            DilucionC = 0;
            $('#lbCOP_DilucionAntecedente_' + i).text("");
            $('#lbCOP_DilucionConsecuente_' + i).text("");

            $('#lbCOP_DilucionAntecedente_' + i).css('display', 'none');
            $('#lbCOP_DilucionConsecuente_' + i).css('display', 'none');
            
            $('#tbCOP_DilucionAntecedente_' + i).val("");
            $('#tbCOP_DilucionConsecuente_' + i).val("");

            $('#tbCOP_DilucionAntecedente_' + i).prop('disabled', true);
            $('#tbCOP_DilucionConsecuente_' + i).prop('disabled', true);
        }

        var CPT_ProductoActual = $('#tbProductoActual_' + i).val();
        var CPT_SituacionActual = $('#tbSituacionActual_' + i).val();
        var CPT_VentajasKey = $('#tbVentajasKey_' + i).val();
        var CPT_RecursoImagenProductoActual = $('#imgImagenProdActual_' + i).attr('src');
        var CPT_RecursoImagensolucionKey = $('#imgImagenSolucionKey_' + i).attr('src');

        var COP_CostoEnUso = $('#CostoEnUso_' + i).text();
        COP_CostoEnUso = COP_CostoEnUso.replace(',', '');
        COP_CostoEnUso = parseFloat(COP_CostoEnUso);
        if (isNaN(COP_CostoEnUso)) {
            COP_CostoEnUso = 0;
        }
        COP_CostoEnUso = COP_CostoEnUso.toFixed(2);

        Update_OportunidadesProductos(
        Op, Id_Val, Cte, Prd, Cantidad, AplDilucion, DilucionA, DilucionC,
        CPT_ProductoActual, CPT_SituacionActual, CPT_VentajasKey, CPT_RecursoImagenProductoActual, CPT_RecursoImagensolucionKey,
        COP_CostoEnUso, function () {
             $('#lbVap_Cantidad_' + i).text(Cantidad);
         });
                  
         $('#lbProductoActual_' + i).text(CPT_ProductoActual);

         $('#lbSituacionActual_' + i).text(CPT_SituacionActual);
         $('#lbVentajasKey_' + i).text(CPT_VentajasKey);

        $('#lbVap_Cantidad_' + i).text(fCantidad);
        $('#lbVap_Cantidad_' + i).css('display', 'block');
        $('#tbVap_Cantidad_' + i).css('display', 'none');

        $('#tbCOP_DilucionAntecedente_' + i).css('display', 'none');
        $('#tbCOP_DilucionConsecuente_' + i).css('display', 'none');

        $('#chbAplDilucion_' + i).css('display', 'none');

        // Pasa los valores de los controles text a labels
        var tbProductoActual = $('#tbProductoActual_' + i).val();
        $('#lbProductoActual_'+i).text(tbProductoActual);

        var tbSituacionActual = $('#tbSituacionActual_' + i).val();
        $('#lbSituacionActual_' + i).text(tbSituacionActual);

        var tbVentajasKey = $('#tbVentajasKey' + i).val();
        $('#lbVentajasKey_' + i).text(tbVentajasKey);

        // Visualizar y ocultar
        $('#tbProductoActual_' + i).css('display', 'none');
        $('#tbSituacionActual_' + i).css('display', 'none');
        $('#tbVentajasKey_' + i).css('display', 'none');
            
        $('#lbProductoActual_'+i).css('display', 'block');
        $('#lbSituacionActual_' + i).css('display', 'block');
        $('#lbVentajasKey_' + i).css('display', 'block');

        $('#imgImagenProdActual_' + i).css('display', 'block');
        $('#imgImagenSolucionKey_' + i).css('display', 'block');

        $('#btnImagenSolucionActual_' + i).css('display', 'none');
        $('#btnImagenSolucionKey_' + i).css('display', 'none');        
    }
    
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function BuscarImagen(obj) {

    var contenedor = $(obj).data('contenedor');
    alertify.success(contenedor);
    $('#modalCargaImagen_Contenedor').val(contenedor);

    $('#lbmodalRecursoNombreArchivo').html('');
    $('#CampoURLId').val('');
    
    $('#modalCargaRecurso').modal('show');

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#dvModalPropuestaTE_ver2').on('show.bs.modal', function (event) {
        $('#divPropuestaDetalle').css('display', 'block');
        $('#pnlVisorDeReporte').css('display', 'none');
                
        $('#rowAceptarPropuesta').css('display', 'none');
        $('#rowPropuestaAcciones').css('display', 'none');
        $('#rowPropuestaEdicion').css('display', 'none');
        $('#rowLoagin').css('display', 'block');

        //Valuacion_CargarDetalle();

        $('#dvModalPropuestaTE_ver2').find('.modal-body').css({
            width: 'auto', //probably not needed
            height: '98%', // 'auto', //probably not needed 
            'max-height': '100%'
        });

        var maxh = screen.height;

        $('#divPropuestaTecnica').css({
            height: '98%',
            'max-height': maxh - 280
        });
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnPropuestaEditar').on('click', function (e) {
        if (VISUALIZANDO_REPORTE == 1) {
            $('#divPropuestaDetalle').css('display', 'block');
            $('#pnlVisorDeReporte').css('display', 'none');
            VISUALIZANDO_REPORTE = 0;
        } else {
            $('#rowPropuestaAcciones').css('display', 'none');
            $('#rowPropuestaEdicion').css('display', 'block');
            Propuesta_ModoEdicion();
        }
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnPropuestaCancelarEdicion').on('click', function (e) {
        $('#rowPropuestaAcciones').css('display', 'block');
        $('#rowPropuestaEdicion').css('display', 'none');
        Cancelar_Edicion();
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnPropuesta_Guardar').on('click', function (e) {
        $('#rowPropuestaAcciones').css('display', 'block');
        $('#rowPropuestaEdicion').css('display', 'none');
        Propuesta_Guardar();
        //Propuesta_ModoLectura();
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnPropuestaCerrar').on('click', function (e) {
        $('#dvModalPropuestaTE_ver2').modal('hide');
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnVisualizarPropuestaTE').click(function () {
        alert("xx");
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnSubitImagen').click(function () {
        var Op = $('#hf_Id_Op').val();
        var IdRep = 0;
        var status = 0;
        var NombreArchivo = "";
        //console.log(NombreArchivo);
        crearRecursoArchivoUsandoFormData(Op, NombreArchivo, IdRep, status);
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnBuscarArchivo').click(function () {

        $("#file").click();

    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#modalCagaRecurso_Aceptar').click(function () {
        CampoURLId = $('#CampoURLId').val();
        CampoURLId = CampoURLId.trim();

        if (CampoURLId.length > 0) {
            // por urls
            var Contenedor = $('#modalCargaImagen_Contenedor').val();
            $(Contenedor).attr("src", CampoURLId);
            $('#modalCargaRecurso').modal('hide');
        } else {
            // por archivo cargado
            $("#btnSubitImagen").click();
        }

    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $("#file").change(function () {
        var file_data = $('#file').prop('files')[0];

        $('#lbmodalRecursoNombreArchivo').html('Nombre de archivo:</br>' + file_data.name);

    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnAceptarPro_Aceptar').click(function () {

        var Id_Val = $('#hf_Id_Val').val();
        Id_Val = parseInt(Id_Val);
        if (isNaN(Id_Val)) {
            Id_Val = 0;
        }
        aceptarPropuestaTecnoEconomica(Id_Val);
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnPropuestaAceptar').click(function () {
        var Id_Val = $('#hf_Id_Val').val();
        Id_Val = parseInt(Id_Val);
        if (isNaN(Id_Val)) {
            Id_Val = 0;
        }
        $('#rowPropuestaAcciones').css('display', 'none');
        $('#rowAceptarPropuesta').css('display', 'block');

        /*
        BootstrapConfirm.showWarning('Aceptar Propuesta', 'Está a punto de aceptar esta propuesta y ' +
        'generar el ACYS correspondiente. ¿Desea continuar?', function () {
            
        });
        */

        /*
        alertify
        .okBtn("Si, Aceptar propuesta y crear ACYS.")
        .cancelBtn("No" )
        .confirm("<b>Aceptar Propuesta</b><br/>Está a punto de aceptar esta propuesta y generar el ACYS correspondiente. ¿Desea continuar?", function (ev) {
        ev.preventDefault();
        aceptarPropuestaTecnoEconomica(Id_Val);
        $('#dvModalPropuestaTE_ver2').modal('hide');
        }, function (ev) {
        ev.preventDefault();
        }).bringToFront();
        */
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnAceptarPro_Cancelar').click(function () {
        $('#rowPropuestaAcciones').css('display', 'block');
        $('#rowAceptarPropuesta').css('display', 'none');
    });

});


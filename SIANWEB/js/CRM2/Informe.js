
var _onLoginSuccessful = null;

//--------------------------------------------------------------------------------------------------
//Cuando el combo pierde el foco
//Si el usuario escribió y no eligió un item correcto limpia el combo
//--------------------------------------------------------------------------------------------------


$.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
    console.log(message);
};

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Combo_ClientBlur(sender, args) {
    //debugger;
    var itemSelected = sender.findItemByText(sender.get_text())
    if (itemSelected == null) {
        LimpiarComboSelectIndex0(sender, '-- Seleccionar --');
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Combo_ClientBlur2(sender, args) {
    //debugger;
    var itemSelected = sender.findItemByText(sender.get_text())
    if (itemSelected == null) {
        LimpiarComboSelectIndex0(sender, '-- Todos --');
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Combo_ClientBlur3(sender, args) {
    //debugger;
    var itemSelected = sender.findItemByText(sender.get_text())
    if (itemSelected == null) {
        LimpiarComboSelectIndex0(sender, '-- Todas --');
    }
}

if ((typeof (console) == undefined) || (typeof (console) == 'undefined')) {
    window.console = new Object();
    window.console.log = function () {
    };
}
        
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function login($) {
    $('#wrnDvDialogoInicioSesion').fadeOut();
    $.ajax({
        url: _ApplicationUrl + '/api/Login/',
        data: $('#frmDvDialogoInicioSesion').serialize(),
        cache: false,
        type: 'POST',
        statusCode: {
            506: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            507: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            508: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            509: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            510: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#dvDialogoInicioSesion').modal('hide');
        if (_onLoginSuccessful != null) {
            _onLoginSuccessful();
        }
    }).fail(function (jqXHR, textStatus, error) {
        //Mostrar el toast con el mensaje de error; retirar las llamadas para mostrar el toast en cada uno de los casos de código de respuesta, y solo manejar las acciones de los casos en particular por código.
        $('#wrnDvDialogoInicioSesion #msgWrnDvDialogoInicioSesion').html(jqXHR.responseJSON.Message);
        $('#wrnDvDialogoInicioSesion').fadeIn()
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function redireccionarALogin() {
    self.location = _ApplicationUrl + '/login.aspx';
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function mostrarToast(jqToastElement, jqParent) {
    $(jqToastElement).appendTo($(jqParent));
    $(jqToastElement).fadeIn();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function salirDelSistema() {
    window.location = _ApplicationUrl + '/Login.aspx?Id=1';
}

function dynamicSort(property) {
var sortOrder = 1;
if(property[0] === "-") {
    sortOrder = -1;
    property = property.substr(1);
}
return function (a,b) {
    var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
    return result * sortOrder;
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_Representante(idZona) {

    $.ajax({
        url: _ApplicationUrl + '/api/CrmRepresentante?IdCD=' + idZona,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
                           
        //var SortedResponse = response.sortBy( response, 'Descripcion' );

        var SortedResponse = response.sort(dynamicSort("Descripcion"));

        var ddl = $('#ddlRepresentantesComercial').empty();
        for (var i = 0; i < SortedResponse.length; i++) {
            $('#ddlRepresentantesComercial').append(
                $('<option>').val(SortedResponse[i].Id).text(SortedResponse[i].Descripcion)
            );
        }                

        $('#ddlRepresentantesComercial').selectpicker('val', "-1");
        $('#ddlRepresentantesComercial').selectpicker('refresh');
        
        //var Id_TU= $('#<%=Id_TU.ClientID %>').val(); 
        //var hfId_Rik = $('#<%=hfId_Rik.ClientID %>').val();                 

        if (hfId_Rik >0) {                                        
            $('#ddlRepresentantesComercial').val(hfId_Rik);
        }

        if (Id_TU==2 || Id_TU==3 || Id_TU==4 || Id_TU==5 || Id_TU==1) {  
            $('#ddlRepresentantesComercial').removeAttr('disabled');                  
            //$('#ddlRepresentantesComercial').prop('disabled',true);                                            
        } else {                    
            //$('#ddlRepresentantesComercial').prop('disabled',false);                        
            $('#ddlRepresentantesComercial').prop('disabled', 'disabled');
        }                
        $('#ddlRepresentantesComercial').selectpicker('refresh');
                
    }).fail(function (jqXHR, textStatus, error) {
        alertify.error('Error: Carga de representantes');                
    });
}

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function CargarCombo_Calendario(idZona) {
            
    if (typeof (idZona) == 'undefined' || idZona == null)
    {
        idZona = 0;
    }

    $.ajax({

        url: _ApplicationUrl + '/api/CrmCalendario?IdCD=' + idZona,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        var ddl = $('#ddPeriodo').empty();
        for (var i = 0; i < response.length; i++) {
            $('#ddPeriodo').append($('<option>').val(response[i].Id).text(response[i].Descripcion));
        }
        $('#ddPeriodo').selectpicker('refresh');

    }).fail(function (jqXHR, textStatus, error) {                
        alertify.error('Ocurrio un error al cargar los proyectos.');                
    });
}
    
        
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
/*
function CargarCombo_Centros() {
    
    $.ajax({
        url: _ApplicationUrl + '/api/CrmCentroDist?Param=0',
        cache: false,
        type: 'GET',
        async:false,
    }).done(function (response, textStatus, jqXHR) {               

        var ddl = $('#ddlZonas').empty();
        //var Id_TU= $('#<%=Id_TU.ClientID %>').val(); 
        //var hfId_CD = $('#<%=hfId_CD.ClientID %>').val();                 

        for (var i = 0; i < response.length; i++) {
            $('#ddlZonas').append(
                $('<option>').val(response[i].Id).text(response[i].Descripcion)
            );
        }     
                                
        if (Id_TU==2 || Id_TU==3 || Id_TU==4 || Id_TU==5 || Id_TU==1) {                                        
            $('#ddlZonas').removeAttr('disabled');
        } else {                                  
            $('#ddlZonas').prop('disabled', 'disabled');
        }
        $('#ddlZonas').val(hfId_CD);

        $('#ddlZonas').selectpicker('refresh');
                                                       
        CargarCombo_Calendario(hfId_CD);
        Cargar_Representante(hfId_CD);
                                
    }).fail(function (jqXHR, textStatus, error) {                
        alertify.error('Error: Carga de Centros');
    });
}
 
*/                       
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Export_Excel_Informe1(Data) {
    var Periodo = $('#ddPeriodo option:selected').text();

    var excel = $JExcel.new();                      
    var excel = $JExcel.new("Arial 9 #333333");    
    var excel = $JExcel.new("Arial 9 #333333");            

    var P=Periodo.replace(/-/g,'');

    excel.set( {sheet:0,value:P } );
    var evenRow=excel.addStyle( { border: "none,none,none,thin #333333"});                                                    
    var oddRow=excel.addStyle ( { fill: "#ECECEC" ,border: "none,none,none,thin #333333"}); 
            
    var formatTitulo=excel.addStyle ( {                
        border: "none,none,none,none",font: "Arial 9 #0000AA B"}
    );                                                         
                        
    var line = 0;                        
    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    
    //var rbTipo= $('#<%=rbTipo.ClientID %> option:selected').text();    
    var rbTipo= $('#cphBodyContent_rbTipo option:selected').text();    
    
    //var Zonas = $('#ddlZonas option:selected').text();
    var Zonas = 0;
    
    var dStyle = excel.addStyle ( {                       
        align: "L",                                                                                
        format: "d-mmm-yy",                                                                             
        border: "none,none,none,none",
        font: "Arial 9 #0000AA B"}                
    );           

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");
    excel.set(0,0,line,"Modulo CRM - "+rbTipo, formatTitulo);             
    excel.set(0,0,line+1,"", formatTitulo);             
    excel.set(0,0,line+1,"Representante: "+Representante, formatTitulo);             
    excel.set(0,0,line+2,"Fecha: "+Fecha, formatTitulo);             
    excel.set(0,0,line+3,"Periodo: "+Periodo, formatTitulo);             
    excel.set(0,0,line+4,"CDS : "+CDI_Nombre, formatTitulo);             
            
    line = 6;

    var formatHeader=excel.addStyle ({
        align: "C",                                                                                
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B"
    });                                                         

    var headers=["Proyecto","Cliente","Area","Solución","Aplicación","Producto",
    "VPT",
    "Analisis","Presentación","Negociación","Cierre",
    "Cancelación","MontoProyecto","Comentarios","Fecha Modificación","Estatus",
    "Rik", "Nombre"
    //,"ClienteSIANID","Oportunidad ID","Rik","Nombre","Causa"
    ];                            
            
    for (var i=0;i<headers.length;i++){                       // Loop headers
        excel.set(0,i,6,headers[i],formatHeader);             // Set CELL header text & header format
        excel.set(0,i,undefined,"auto");                      // Set COLUMN width to auto 
    }            
    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);                                                                       
            
    

    var formatCell=excel.addStyle ( {                        
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    );     
    var formatCell_C=excel.addStyle ( {                
        align: "C",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 
    var formatCell_L=excel.addStyle ( {                
        align: "L",
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 
            
    var format_Monto=excel.addStyle ( {
        //format: '#,##0.00',
        align: "C",
        format: '$#,##0',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 

    // renglon amarillo
    var formatCell_Amarillo=excel.addStyle ({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 
    var formatCell_Amarillo_L=excel.addStyle ({
        align: "L",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 
    var formatCell_Amarillo_C=excel.addStyle ({
        align: "C",
        fill: "#FFFF00",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 9 #fff B",
        format: '$#,##0'
    }); 

  
//            
//    var Inicio = line+1;
//    for (var i=0; i<Data.length; i++){
//        
//        var Este_Rik  = Data[i].Rik
//        
//        // Ultimo Rik Diferente de Este Rik
//        if (Ultimo_Rik != Este_Rik) 
//        {
//            if (Ultimo_Rik > 0) {
//                if (Ultimo_Rik != Este_Rik) {
//                    // Imprime la linea e incrementa
//                    line = line + 1;
//                    excel.set(0,0,line,Data[i].Proyecto, formatCell_C);                    
//                    excel.set(0,1,line,Data[i].Cliente, formatCell);                    
//                    excel.set(0,2,line,Data[i].Area, formatCell);                    
//                    excel.set(0,3,line,Data[i].Solucion, formatCell);                    
//                    excel.set(0,4,line,Data[i].Aplicacion, formatCell);                    
//                    excel.set(0,5,line,Data[i].Productos, formatCell_L);                    
//                    excel.set(0,6,line,Data[i].VPO, format_Monto);                    
//                    excel.set(0,7,line,Data[i].Analisis, formatCell_C);                    
//                    excel.set(0,8,line,Data[i].Presentacion, formatCell_C);                    
//                    excel.set(0,9,line,Data[i].Negociacion, formatCell_C);                    
//                    excel.set(0,10,line,Data[i].Cierre, formatCell_C);                    
//                    excel.set(0,11,line,Data[i].Cancelacion, formatCell);                    
//                    excel.set(0,12,line,Data[i].MontoProyecto, format_Monto);                    
//                    excel.set(0,13,line,Data[i].Comentarios, formatCell);  
//                    excel.set(0,14,line,Data[i].FechaModificacion, formatCell_C);  
//                    excel.set(0,15,line,Data[i].Estatus, formatCell_C);  
//                    excel.set(0,16,line,Data[i].Rik, formatCell_C);  
//                    excel.set(0,17,line,Data[i].Nombre, formatCell);                          
//                    /*
//                    excel.set(0,16,line,Data[i].ClienteSIANID, formatCell);  
//                    excel.set(0,17,line,Data[i].OportunidadID, formatCell);  
//                    excel.set(0,18,line,Data[i].Rik, formatCell);  
//                    excel.set(0,19,line,Data[i].Nombre, formatCell);                  
//                    excel.set(0,20,line,Data[i].Causa, formatCell);  
//                    */
//                    Total1 = Total1 + Data[i].VPO;
//                    Total2 = Total2 + Data[i].MontoProyecto;
//                    // -- 

//                    // Imprime TOTALES
//                    excel.set(0,0,Totales_Linea,"TOTAL"+Este_Rik, formatCell_Amarillo_L);                    
//                    excel.set(0,6,Totales_Linea,Total1, formatCell_Amarillo);                    
//                    excel.set(0,12,Totales_Linea,Total2, formatCell_Amarillo);       
//                    Total1 = 0;             
//                    Total2 = 0;             
//                }
//            }           
//            Totales_Linea = line;
//            Total1 = 0;
//            Total2 = 0;
//            Total3 = 0;
//            Total4 = 0;
//            Total5 = 0;    
//            // Imprim totales
//            excel.set(0,0,line,"", formatCell_Amarillo);                    
//            excel.set(0,1,line,"", formatCell_Amarillo);                    
//            excel.set(0,2,line,"", formatCell_Amarillo);                    
//            excel.set(0,3,line,"", formatCell_Amarillo);                    
//            excel.set(0,4,line,"", formatCell_Amarillo);                    
//            excel.set(0,5,line,"", formatCell_Amarillo);                    
//            excel.set(0,6,line,"", formatCell_Amarillo);                    
//            excel.set(0,7,line,"", formatCell_Amarillo);                    
//            excel.set(0,8,line,"", formatCell_Amarillo);                    
//            excel.set(0,9,line,"", formatCell_Amarillo);                    
//            excel.set(0,10,line,"", formatCell_Amarillo);                    
//            excel.set(0,11,line,"", formatCell_Amarillo);                    
//            excel.set(0,12,line,"", formatCell_Amarillo);                    
//            excel.set(0,13,line,"", formatCell_Amarillo);                    
//            excel.set(0,14,line,"", formatCell_Amarillo);                    
//            excel.set(0,15,line,"", formatCell_Amarillo);                    
//            excel.set(0,16,line,"", formatCell_Amarillo);                    
//            excel.set(0,17,line,"", formatCell_Amarillo);       
//            /*            
//            excel.set(0,18,line,"", formatCell_Amarillo);                    
//            excel.set(0,19,line,"", formatCell_Amarillo);                    
//            excel.set(0,20,line,"", formatCell_Amarillo); 
//            */
//            Ultimo_Rik = Data[i].Rik;
//        } else {

//        line = line + 1;
//        excel.set(0,0,line,Data[i].Proyecto, formatCell_C);                    
//        excel.set(0,1,line,Data[i].Cliente, formatCell);                    
//        excel.set(0,2,line,Data[i].Area, formatCell);                    
//        excel.set(0,3,line,Data[i].Solucion, formatCell);                    
//        excel.set(0,4,line,Data[i].Aplicacion, formatCell);                    
//        excel.set(0,5,line,Data[i].Productos, formatCell_L);                    
//        excel.set(0,6,line,Data[i].VPO, format_Monto);                    
//        excel.set(0,7,line,Data[i].Analisis, formatCell_C);                    
//        excel.set(0,8,line,Data[i].Presentacion, formatCell_C);                    
//        excel.set(0,9,line,Data[i].Negociacion, formatCell_C);                    
//        excel.set(0,10,line,Data[i].Cierre, formatCell_C);                    
//        excel.set(0,11,line,Data[i].Cancelacion, formatCell);                    
//        excel.set(0,12,line,Data[i].MontoProyecto, format_Monto);                    
//        excel.set(0,13,line,Data[i].Comentarios, formatCell);  
//        excel.set(0,14,line,Data[i].FechaModificacion, formatCell_C);  
//        excel.set(0,15,line,Data[i].Estatus, formatCell_C);  
//        excel.set(0,16,line,Data[i].Rik, formatCell_C);  
//        excel.set(0,17,line,Data[i].Nombre, formatCell);                  
//        
//        /*
//        excel.set(0,16,line,Data[i].ClienteSIANID, formatCell);  
//        excel.set(0,17,line,Data[i].OportunidadID, formatCell);  
//        excel.set(0,18,line,Data[i].Rik, formatCell);  
//        excel.set(0,19,line,Data[i].Nombre, formatCell);                  
//        excel.set(0,20,line,Data[i].Causa, formatCell);  
//        */
//        Total1 = Total1 + Data[i].VPO;
//        Total2 = Total2 + Data[i].MontoProyecto;

//        }

//    }

    line = 6;

    var Totales_Linea = 0;    
    var Ultimo_Rik = 0; 
    var Total1 = 0;
    var Total2 = 0;
    var Total3 = 0;
    var Total4 = 0;
    var Total5 = 0;
            
    var Inicio = line + 1; // Salta Renglon  

    for (var i=0; i<Data.length; i++) { 
           
        var Este_Rik  = Data[i].Rik
        
        if (Ultimo_Rik == 0 || Ultimo_Rik != Este_Rik) {
            var bImprimeTotales = 0;
            // Esto indica que el rik es diferente 

            // Si no hay line totales inicializa
            if (Totales_Linea == 0) {
                line = line + 1; // Salta Renglon 
                Totales_Linea = line;
                bImprimeTotales = 0;
            } else {
                // No salta el renglon                 
                line = line + 1; // Salta Renglon                 
                bImprimeTotales = 1;
            }

            if (bImprimeTotales==1) {
                // Imprime total y continua                     
                    excel.set(0,0,Totales_Linea,"TOTAL", formatCell_Amarillo_L);                    
                    excel.set(0,1,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,2,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,3,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,4,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,5,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,6,Totales_Linea,Total1, formatCell_Amarillo);                    
                    excel.set(0,7,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,8,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,9,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,10,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,11,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,12,Totales_Linea,Total2, formatCell_Amarillo);       
                    excel.set(0,13,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,14,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,15,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,16,Totales_Linea,"", formatCell_Amarillo);                    
                    excel.set(0,17,Totales_Linea,"", formatCell_Amarillo);       

                    // Inicia totales
                    Total1 = 0;             
                    Total2 = 0;                         
                    Totales_Linea = line;
            }
            
            Ultimo_Rik = Este_Rik;
        }
        line = line + 1; // Salta Renglon 

        // Imprime registro Normal
        excel.set(0,0,line,Data[i].Proyecto, formatCell_C);                    
        excel.set(0,1,line,Data[i].Cliente, formatCell);                    
        excel.set(0,2,line,Data[i].Area, formatCell);                    
        excel.set(0,3,line,Data[i].Solucion, formatCell);                    
        excel.set(0,4,line,Data[i].Aplicacion, formatCell);                    
        excel.set(0,5,line,Data[i].Productos, formatCell_L);                    
        excel.set(0,6,line,Data[i].VPO, format_Monto);                    
        excel.set(0,7,line,Data[i].Analisis, formatCell_C);                    
        excel.set(0,8,line,Data[i].Presentacion, formatCell_C);                    
        excel.set(0,9,line,Data[i].Negociacion, formatCell_C);                    
        excel.set(0,10,line,Data[i].Cierre, formatCell_C);                    
        excel.set(0,11,line,Data[i].Cancelacion, formatCell);                    
        excel.set(0,12,line,Data[i].MontoProyecto, format_Monto);                    
        excel.set(0,13,line,Data[i].Comentarios, formatCell);  
        excel.set(0,14,line,Data[i].FechaModificacion, formatCell_C);  
        excel.set(0,15,line,Data[i].Estatus, formatCell_C);  
        excel.set(0,16,line,Data[i].Rik, formatCell_C);  
        excel.set(0,17,line,Data[i].Nombre, formatCell);                  
                
        Total1 = Total1 + Data[i].VPO;
        Total2 = Total2 + Data[i].MontoProyecto;

    }

    if (Ultimo_Rik >0) {        
        // Actualiza Totales           
        
        excel.set(0,0,Totales_Linea,"TOTAL", formatCell_Amarillo_L);                    
        excel.set(0,1,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,2,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,3,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,4,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,5,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,6,Totales_Linea,Total1, formatCell_Amarillo);                    
        excel.set(0,7,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,8,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,9,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,10,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,11,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,12,Totales_Linea,Total2, formatCell_Amarillo);       
        excel.set(0,13,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,14,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,15,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,16,Totales_Linea,"", formatCell_Amarillo);                    
        excel.set(0,17,Totales_Linea,"", formatCell_Amarillo);       
                        
    }           
    
    var Fin = line+1;
    line = line+1;
    excel.set(0,0,line,"", formatCell);  
    excel.set(0,1,line,"", formatCell);  
    excel.set(0,2,line,"", formatCell);  
    excel.set(0,3,line,"", formatCell);  
    excel.set(0,4,line,"", formatCell);  
    excel.set(0,5,line,"", formatCell_L);  
    excel.set(0,6,line,"", formatCell);      
    excel.set(0,7,line,"", formatCell);  
    excel.set(0,8,line,"", formatCell);  
    excel.set(0,9,line,"", formatCell);  
    excel.set(0,10,line,"", formatCell);  
    excel.set(0,11,line,"", formatCell);  
    excel.set(0,12,line,"", format_Monto); 
    excel.set(0,13,line,"", formatCell);  
    excel.set(0,14,line,"", formatCell);  
    excel.set(0,15,line,"", formatCell);  
    excel.set(0,16,line,"", formatCell);  
    excel.set(0,17,line,"", formatCell);  
    
    Periodo=Periodo.replace(/-/g,'');
    excel.generate("Reporte "+ Periodo +" "+Fecha+".xlsx");
}
              
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\           
function Export_Excel_Informe2(Data) {

    var Periodo = $('#ddPeriodo option:selected').text();
    var excel = $JExcel.new();                      
    var excel = $JExcel.new("Arial 10 #333333");    
    var excel = $JExcel.new("Arial light 10 #333333");            

    excel.set( {sheet:0,value:"Reporte_"+Periodo } );
    var evenRow=excel.addStyle( { border: "none,none,none,thin #333333"});                                                    
    var oddRow=excel.addStyle ( { fill: "#ECECEC" ,border: "none,none,none,thin #333333"}); 
            
    var formatTitulo=excel.addStyle ( {
        //border: "none,none,none,thin #333333",font: "Arial 12 #0000AA B"}
        border: "none,none,none,none",font: "Arial 12 #0000AA B"}
    );                                                         
                        
    var line = 0;
                        
    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    
    //var rbTipo= $('#<%=rbTipo.ClientID %> option:selected').text();
    var rbTipo= $('#cphBodyContent_rbTipo').text();
    
    //var Zonas = $('#ddlZonas option:selected').text();
    var Zonas = 0;
    //alert(rbTipo);

    var dStyle = excel.addStyle ( {                       
        align: "L",                                                                                
        format: "d-mmm-yy",                                                                             
        border: "none,none,none,none",
        font: "Arial 12 #0000AA B"}                
    );           

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");

    excel.set(0,0,line,"Modulo CRM - "+rbTipo, formatTitulo);             
    excel.set(0,0,line+1,"", formatTitulo);             
    excel.set(0,0,line+1,"Representante: "+Representante, formatTitulo);             
    excel.set(0,0,line+2,"Fecha: "+Fecha, formatTitulo);             
    excel.set(0,0,line+3,"Periodo: "+Periodo, formatTitulo);             
    excel.set(0,0,line+4,"CDS : "+Zonas, formatTitulo);             
            
    line = 6;

    var formatHeader=excel.addStyle ( {                
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 12 #fff B"
    });                                                         

    var headers=["CDS","No. Rik","Representante","Fecha de entrada"];                            
            
    for (var i=0;i<headers.length;i++){                       // Loop headers
        excel.set(0,i,6,headers[i],formatHeader);             // Set CELL header text & header format
        excel.set(0,i,undefined,"auto");                      // Set COLUMN width to auto 
    }            

    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);                                                                                  
    line = 7;

    var formatCell=excel.addStyle ( {                
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 
            
    var format_Monto=excel.addStyle ( {
        format: '#,##0.00',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    );             

    var Inicio = line+1;
    for (var i=0; i<Data.length; i++){
        line = line + 1;
        excel.set(0,0,line,Data[i].Zona, formatCell);                    
        excel.set(0,1,line,Data[i].UsuarioID, formatCell);                    
        excel.set(0,2,line,Data[i].Representante, formatCell);                    
        excel.set(0,3,line,Data[i].Fecha, formatCell);                    
    }

    var Fin = line+1;
    line = line+1;
    excel.set(0,0,line,"", formatCell);  
    excel.set(0,1,line,"", formatCell);  
    excel.set(0,2,line,"", formatCell);  
    excel.set(0,3,line,"", formatCell);  
            
    excel.generate("Reporte_"+Fecha+".xlsx");
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\                                 
function Export_Excel_Informe3(Data) {

    var Periodo = $('#ddPeriodo option:selected').text();
    var excel = $JExcel.new();                      
    var excel = $JExcel.new("Arial 10 #333333");    
    var excel = $JExcel.new("Arial light 10 #333333");            

    excel.set( {sheet:0,value:"Reporte_"+Periodo } );
    var evenRow=excel.addStyle( { border: "none,none,none,thin #333333"});                                                    
    var oddRow=excel.addStyle ( { fill: "#ECECEC" ,border: "none,none,none,thin #333333"}); 
            
    var formatTitulo=excel.addStyle ( {
        //border: "none,none,none,thin #333333",font: "Arial 12 #0000AA B"}
        border: "none,none,none,none",font: "Arial 12 #0000AA B"}
    );                                                         
                        
    var line = 0;
                        
    var Representante = $('#ddlRepresentantesComercial option:selected').text();
    //var rbTipo= $('#<%=rbTipo.ClientID %> option:selected').text();
    var rbTipo= $('#cphBodyContent_rbTipo').text();
    
    //var Zonas = $('#ddlZonas option:selected').text();
    var Zonas = 0;
    //alert(rbTipo);

    var dStyle = excel.addStyle ( {                       
        align: "L",                                                                                
        format: "d-mmm-yy",                                                                             
        border: "none,none,none,none",
        font: "Arial 12 #0000AA B"}                
    );           

    var Fecha = new Date();
    Fecha = Fecha.format("dd/mm/yyyy");

    excel.set(0,0,line,"Modulo CRM - "+rbTipo, formatTitulo);             
    excel.set(0,0,line+1,"", formatTitulo);             
    excel.set(0,0,line+1,"Representante: "+Representante, formatTitulo);             
    excel.set(0,0,line+2,"Fecha: "+Fecha, formatTitulo);             
    excel.set(0,0,line+3,"Periodo: "+Periodo, formatTitulo);             
    excel.set(0,0,line+4,"CDS : "+Zonas, formatTitulo);             
            
    line = 6;

    var formatHeader=excel.addStyle ( {                
        fill: "#dadada",
        border: "thin #333333,thin #333333,thin #333333,thin #333333",
        font: "Arial 12 #fff B"
    });                                                         

    var headers=["CDS","No. Rik","Representante","Análisis","Presentación","Monto proyecto","Cierre",
    "Cancelación","Efectividad cierre"];                            
            
    for (var i=0;i<headers.length;i++){                       // Loop headers
        excel.set(0,i,6,headers[i],formatHeader);             // Set CELL header text & header format
        excel.set(0,i,undefined,"auto");                      // Set COLUMN width to auto 
    }            

    var initDate = new Date(2000, 0, 1);
    var endDate = new Date(2016, 0, 1);                                                                                  
    line = 7;

    var formatCell=excel.addStyle ( {                
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    ); 
            
    var format_Monto=excel.addStyle ( {
        format: '#,##0.00',
        border: "thin #333333,thin #333333,thin #333333,thin #333333"}
    );             

    var Inicio = line+1;
    for (var i=0; i<Data.length; i++){
        line = line + 1;
        excel.set(0, 0, line, Data[i].ZonaId, formatCell);                    
        excel.set(0, 1, line, Data[i].UsuarioID, formatCell);                    
        excel.set(0, 2, line, Data[i].Representante, formatCell);                                    
        excel.set(0, 3, line, Data[i].A, formatCell);
        excel.set(0, 4, line, Data[i].P, formatCell);                    
        excel.set(0, 5, line, Data[i].N, formatCell);                                    
        excel.set(0, 6, line, Data[i].Monto, formatCell);                
        excel.set(0, 7, line, Data[i].C, formatCell);    
    }

    var Fin = line+1;
    line = line+1;
    excel.set(0,0,line,"", formatCell);  
    excel.set(0,1,line,"", formatCell);  
    excel.set(0,2,line,"", formatCell);  
    excel.set(0,3,line,"", formatCell);  
            
    excel.generate("Reporte_1007_imp"+Fecha+".xlsx");
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\                                 
function Generar_Informe1(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo) {

    $('#imgProgreso').css('display','block');

    var Monto1 = $('#txtDe').val();            
    Monto1 = parseFloat(Monto1);
    if (isNaN(Monto1)) {
        Monto1=0;
    }          

    var Monto2 = $('#txtA').val();
    Monto2 = parseFloat(Monto2);
    if (isNaN(Monto2)) {
        Monto2=0;
    }         

    $('#tbInforme1 > tbody').empty();

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }
    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?'+
        'TipoReporte=' + rbTipoReporte + 
        '&Zona=' + ddlZonas +
        '&Representante=' + ddlRepresentantes + 
        '&Periodo=' + ddPeriodo +
        '&Monto1=' + Monto1 + 
        '&Monto2=' + Monto2,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {                

        Export_Excel_Informe1(response);
        $('#imgProgreso').css('display','none');

    }).fail(function (jqXHR, textStatus, error) {
        $('#imgProgreso').css('display','none');
        //$('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');
        alertify.error('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');
        /*$('#toastDanger').fadeIn();
        //deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }*/
    }); 
    //$('#imgProgreso').css('display','none');          
}
                         
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Generar_Informe2(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo) {
            
    $('#tbInforme1 > tbody').empty();

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }
    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?TipoReporte=' + rbTipoReporte + '&Zona=' + ddlZonas +
            '&Representante=' + ddlRepresentantes + '&Periodo=' + ddPeriodo,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        //$('#tbInforme1 > tbody').empty();

        Export_Excel_Informe2(response);

    }).fail(function (jqXHR, textStatus, error) {                
        alertify.error('Ocurrio un error al preparar el informe 2.');                
    });           
}
                                                 
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Generar_Informe3(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo) {
            
    $('#tbInforme1 > tbody').empty();

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }
    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?Zona=' + ddlZonas+ '&Periodo='+ ddPeriodo,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        Export_Excel_Informe3(response);

    }).fail(function (jqXHR, textStatus, error) {                
            
        if (jqXHR.responseJSON.ExceptionMessage != null && jqXHR.responseJSON.ExceptionMessage != '' ) {
            alertify.error(jqXHR.responseJSON.ExceptionMessage);                
        } else {
            alertify.error('Ocurrio un error al ejecutar la funcion [Generar_Informe3].');                
        }                                
    });           
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Initialize Datatables

$(document).ready(function () {

    $('#imgProgreso').css('display','block');

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#rbTipo').on('change', function () {                
        //$('#<%= rbTipo.ClientID %>').change(function() {                
        //var rbTipo = $('#<%=rbTipo.ClientID %> option:selected').val();                
        var rbTipo = $('#cphBodyContent_rbTipo').val();                        
        if (rbTipo == 3 || rbTipo == 4 || rbTipo == 5 || rbTipo == 6 || rbTipo == 7) {
            $('#txtDe').val('');
            $('#txtA').val('');
            $('#tbMonto').css('display', 'none');
        } else {
            $('#tbMonto').css('display', 'block');
        }
    });

    /*
    $('#ddlZonas').on('change', function () {                
        console.log('ddlZonas_on_change');
        idZona = $('#ddlZonas').find("option:selected").val();                
        //alert(idZona);
        Cargar_Representante(idZona);
        CargarCombo_Calendario(idZona);
    });
    */
                        
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('.datatable').dataTable({
        "fnDrawCallback": function (oSettings) {
            // if .sidebar-pf exists, call sidebar() after the data table is drawn
            if ($('.sidebar-pf').length > 0) {
                $(document).sidebar();
            }
        }
    });

    $('.tooltip-demo').tooltip({
        selector: '[data-toggle=tooltip]',
        container: 'body'
    });

    if (typeof (crmOnReady) != undefined && typeof (crmOnReady) != 'undefined') {
        crmOnReady($);
    }

    if (!Modernizr.input.placeholder) {
        createPlaceholders();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('#btnGenerarRepote').click(function (e) {        
        //var rbTipoReporte = $('#rbTipo option:selected').val();        
        //var rbTipoReporte = $('#<%=rbTipo.ClientID %> option:selected').val();        
        var rbTipoReporte = $('#cphBodyContent_rbTipo').val();
        //var ddlZonas = $('#ddlZonas option:selected').val();
        var ddlZonas = 0;
        var ddlRepresentantes = $('#ddlRepresentantesComercial option:selected').val();
        var ddPeriodo = $('#ddPeriodo option:selected').val();
                
        console.log(rbTipoReporte + ':' + ddlZonas + ':' + ddlRepresentantes + ':' + ddPeriodo);
        
        if (rbTipoReporte == 2) 
        {                    
            Generar_Informe1(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo);
        } 
        if (rbTipoReporte == 3) 
        { 
            Generar_Informe2(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo);
        }
        if (rbTipoReporte == 5) 
        { 
            Generar_Informe3(rbTipoReporte, ddlZonas, ddlRepresentantes, ddPeriodo);
        }        
    });

    //CargarCombo_Centros();
    
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $('input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });

    $('input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });

    $('#tblContenido').numeraljs();

    var tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
        return function (table, name) {
            if (!table.nodeType) table = document.getElementById(table)
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
            window.location.href = uri + base64(format(template, ctx))
        };
    })();

    Cargar_Representante(0);
    CargarCombo_Calendario(0);

    $('#imgProgreso').css('display','none');

});

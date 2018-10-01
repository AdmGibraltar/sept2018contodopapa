<%@ Page Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="FullDashboard.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.FullDashboard" %>

<%@ Register Src="~/PortalRIK/GestionPromocion/Valuaciones/UCAutorizacionValuaciones.ascx" TagPrefix="uc" TagName="UCAutorizacionValuaciones" %>
<%@ Register Src="~/Controles/Cliente/UCPatternflyToast.ascx" TagPrefix="uc" TagName="UCPatternflyToast" %>

<%--<%@ Register Src="~/js/crm/servicios/navegacion/UCNotificaciones_js.ascx" TagPrefix="uc" TagName="UCNotificaciones_js" %>
--%>

<%--<%@ Register Src="~/PortalRIK/Navegacion/Notificaciones/UCNotificaciones.ascx" TagPrefix="uc" TagName="UCNotificaciones" %>
<%@ Register Src="~/js/crm/ui/Notificaciones.ascx" TagPrefix="uc" TagName="UINotificaciones" %>
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%=Page.ResolveUrl("~/css/horizontal_selector.css")%>" rel="stylesheet">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/radios-to-slider.min.css")%>">        
    <link rel="shortcut icon" href="<%=Page.ResolveUrl("~/Img/favicon.ico")%>">               

    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly-additions.min.css")%>">
    <!--script src="<%=Page.ResolveUrl("~/js/patternfly/patternfly.min.js")%>"></script-->
        
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/radios-to-slider.min.css")%>">
    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>" rel="stylesheet">     
    <link href="<%=Page.ResolveUrl("~/css/bootstrap-treeview.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/gridstack/gridstack.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/gridstack/gridstack-extra.min.css")%>" rel="stylesheet">
    
    <link href="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.css")%>" rel="stylesheet">
            
    <%--<link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"/>--%>
    <%--<link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">--%>
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">

    <!--link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/animate.css")%>"-->
    <script src="<%= Page.ResolveUrl("~/js/swf.js") %>" type="text/javascript"></script>
    
    <style>
        .toast-pf-top-right-rel {
          left: 20px;
          position: relative;
          right: 20px;
          top: 12px;
          z-index: 1035;
          /* Medium devices (desktops, 992px and up) */
        }
    </style>
    <script src="//code.jquery.com/jquery-2.1.4.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">

    <div class="row" style="display: block;">

        <div class="col-sm-12 col-md-12">

            <div class="row">
                <!--div class="col-sm-12 col-md-12 ROWPAD">                        
                    <h3><strong>Gestión de la Promoción</strong></h3>
                </div-->                                
                <table style="width:100%;">
                    <tr>
                        <td>
                            <h3 style="display: inline-block;"><strong>Gestión de la Promoción</strong></h3> 
                        </td>
                        <td style="width:20px;" valign="middle">                            
                            <div style="display:none;" id="Gerente_Icono">
                            <!--i class="fas fa-user-tie"></i-->                            
                            </div>
                        </td>
                        <td style="width:30px;" valign="middle">                            
                            <label style="margin-top:5px; display:none;" id="Gerente_lbRik">Rik&nbsp;</label>
                        </td>
                        <td style="width:180px;">
                            <button 
                                id="Gerente_btnCambiarUsuario"
                                class="btn pull-right btnGerente" 
                                onclick="btnGerente_CambiarUsuario();" 
                                data-action="1" 
                                data-toggle="tooltip" 
                                title="Haga clic aqu&iacute; para seleccionar el Rik."
                                style="width:100%; display:none;">
                                &nbsp;
                            </button>                            
                        </td>                        
                    </tr>
                </table>

            </div>

            <div class="row">
                <div class="col-sm-12 col-md-12">
                    <h2 class="h4">Resultados</h2>
                </div>
            </div>

                <form id="Form1" runat="server">
                <!--RadScriptManager-->
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableScriptGlobalization="True"
                    EnableScriptLocalization="True" EnableTheming="true" AsyncPostBackTimeout="36000">
                </telerik:RadScriptManager>

                <%-- ROW PROPIEDAES DE LAS GRAFICA --%>
                <div class="row align-bottom" style="vertical-align: bottom!important;" >
                    <div class="text-center">

                    <table style="width: 90%;">
                        <tr>
                            <td style="padding:10px;" valign="bottom">
                                <asp:Label ID="Label2" runat="server" Text="Núm. proyectos"></asp:Label>         
                            </td>
                            <td style="padding:10px;" valign="bottom">
                                <asp:Label ID="Label3" runat="server" Text="Monto proyectos"></asp:Label>                                        
                            </td>
                            <td style="padding:10px;" valign="bottom">
                                <asp:Label ID="Label4" runat="server" Text="Avances mes"></asp:Label><br>        
                            </td>
                            <td style="padding:10px;" valign="bottom">
                                <asp:Label ID="Label6" runat="server" Text="Cantidad cerrados"></asp:Label>                                
                            </td>
                            <td style="padding:10px;" valign="bottom">
                                <asp:Label ID="Label5" runat="server" Text="Monto cerrados"></asp:Label>
                            </td>                            
                            <td style="padding:10px;" valign="bottom">
                                <asp:Label ID="lblcd" runat="server" Text="CD"></asp:Label>        
                            </td>                            
                            <td style="padding:10px;" valign="bottom">
                                <asp:Label ID="Label7" runat="server" Text="Vista de <br>gráficas"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <telerik:RadNumericTextBox ID="NumProyectos" runat="server" ReadOnly="true" size="5" Style="text-align: center" Width="80px"> 
                                <NumberFormat DecimalDigits="0" />
                               </telerik:RadNumericTextBox>         
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="MontoProyectos" runat="server" ReadOnly="true" size="5"
                                    Style="text-align: center" Width="80px">
                                </telerik:RadNumericTextBox>        
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="AvanceMes" runat="server" ReadOnly="true" size="5" Style="text-align: center" Width="80px">
                                <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>        
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="CantidadCerrados" runat="server" ReadOnly="true" size="5"
                                    Style="text-align: center" Width="80px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>    
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="MontoCerrados" runat="server" ReadOnly="true"
                                Style="text-align: center" Width="80px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                 <telerik:RadComboBox ID="ddlCDS" runat="server" AutoPostBack="True" CssClass="inp1" OnSelectedIndexChanged="ddlCDS_SelectedIndexChanged">
                                </telerik:RadComboBox>       
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddl" runat="server" AutoPostBack="True">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Importe de proyectos" Value="1" />
                                        <telerik:RadComboBoxItem Text="Número de proyectos" Value="2" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>

                    <!--div class="col-sm-4 col-md-4">

                            <table>
                                                        
                                                        <tr>
                                                            <td style="width: 100px" align="center"></td>
                                                            <td style="width: 100px" align="center"></td>
                                                            <td style="width: 100px" align="center"></td>
                                                            <td style="width: 100px" align="center"></td>
                                                            <td style="width: 100px" align="center"></td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkMetas" runat="server" PostBackUrl="wfrmMetasTerritorio.aspx">Establecer metas y cuotas</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>

                    
                    </div-->

                    </div>
                </div>

                <%--ROW GRAFICAS --%>
                <div class="row">
                    <div class="col-md-12" style="overflow:auto;">

                        <div style="display:block; width:60%; float:left; min-width:500px; text-align:center;" >
                            <%=GeneraGraficaDistribucion()%>
                        </div>

                        <div style="display:block; width:40%; float:left; text-align:center;">
                            <%=GeneraGraficaActividad()%>
                        </div>

                    </div>
                </div>
              
                <div class="row">
                    <div class="col-sm-12">
                        <div id="centrador">
                                    <!--Inicia header-->
                                    <!--Termina header-->
                                    <!--Inicia contenido-->
                                    <div class="contenido" runat="server" id="contenido">
                                        <table id="tblPrin" align="center">
                                            <tr>
                                                <td align="right">
                                                    <asp:Panel ID="pnlComercial" runat="server"></asp:Panel>
                                                    <asp:Panel ID="pnlGeneral" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="ddlMeses" runat="server" AutoPostBack="True" Visible="False">
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 81px">
                                                    <div id="Label1"></div>
                                                    <div>
                                                            <table align="center">
                                                                <tr>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <div runat="server" id="divImprimir" visible="false">
                                                                            &nbsp;<asp:LinkButton ID="ibtnImprimir" runat="server" CssClass="btn_imprimir">IMPRIMIR</asp:LinkButton>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!--Termina contenido-->
                                    <!--Inicia footer-->
        
                                    <!--Termina footer-->
                        </div>
                    </div>
                </div>
                

                <% if (TipoUsuario.Tu_Descripcion == "Gerente de Sucursal")
                {%>
    
                <div class="row">
                    <div class="col-sm-12 col-md-12">
                        <!--Partes del gerente de sucursal-->                        
                        <%--<hr />--%>
                        <%--<div class="container">--%>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Autorización de Valuaciones</h3>
                                </div>
                                <div class="panel-body">
                                <uc:UCAutorizacionValuaciones runat="server" ID="ucAutorizacionValuaciones" />
                                </div>
                            </div>
                        <%--</div>--%>                       

                    </div>
                </div>

                <% }%>
              
              </form>
        </div>        

    </div>

    <!--Toast messages-->
    <div class="toast-pf toast-pf-max-width toast-pf-top-right alert alert-danger alert-dismissable"
        style="display: none" id="toastDanger">
        <button type="button" class="close" aria-hidden="true" onclick="cerrarToastDanger(jQuery)">
            <span class="pficon pficon-close"></span>
        </button>
        <span class="pficon pficon-error-circle-o"></span>
        <div id="toastDangerMessage">
            Message
        </div>
    </div>

    <div class="toast-pf toast-pf-max-width toast-pf-top-right alert alert-success alert-dismissable"
        style="display: none" id="toastSuccess">
        <button type="button" class="close" aria-hidden="true" onclick="cerrarToastSuccess(jQuery)">
            <span class="pficon pficon-close"></span>
        </button>
        <span class="pficon pficon-ok"></span>
        <div id="toastSuccessMessage">
            Message
        </div>
    </div>

    <div class="toast-pf toast-pf-max-width toast-pf-top-right alert alert-warning alert-dismissable"
        style="display: none" id="toastWarning">
        <button type="button" class="close" aria-hidden="true" onclick="cerrarToastWarning(jQuery)">
            <span class="pficon pficon-close"></span>
        </button>
        <span class="pficon pficon-warning-triangle-o"></span>
        <div id="toastWarningMessage">
            Message
        </div>
    </div>
    <!--Toast messages-->

    
    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="dvModalListaRepresentantes" class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">                    
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="img2" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="h10">
                        <table>
                            <tr>
                                <td>Representantes disponibles&nbsp;</td>
                                <td>
                                    <img id="spinner_listarep" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></a>
                                </td>
                            </tr>
                        </table>                        
                    </h4>
                </div>
                <div class="modal-body">
                    
                    <table id="tblUsuariosRik" class="table table-hover" style="width:100%;">
                        <thead>
                            <tr>
                                <th>Rik</th>
                                <th>Nombre</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    
                </div>
                <div class="modal-footer">                    
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnListadoRik_Aplicar" onclick="btnListaRep_Seleccion();">Aplicar</button>                
                </div>
            </div>
        </div>
    </div>
    
    <!--Login dialog MODAL-->
    <div class="modal fade" id="dvDialogoInicioSesion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="btndvDialogoInicioSesionCerrar" type="button" class="close" data-dismiss="modal"
                        aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H3">
                        Iniciar sesion
                    </h4>
                </div>
                <div class="modal-body">
                    <form id="frmDvDialogoInicioSesion">
                    <div class="form-group">
                        <label for="Cu_User">
                            Usuario
                        </label>
                        <input type="text" id="Username" name="Username" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="Cu_pass">
                            Contraseña
                        </label>
                        <input type="password" id="Password" name="Password" class="form-control" />
                    </div>
                    </form>
                    <div id="wrnDvDialogoInicioSesion" class="alert alert-warning" style="display: none;">
                        <span class="pficon pficon-warning-triangle-o"></span>
                        <div id="msgWrnDvDialogoInicioSesion">
                            Mensaje
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnDvDialogoInicioSesionCerrar" type="button" class="btn btn-default"
                        onclick="redireccionarALogin()" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnDvDialogoInicioSesionLogin"
                        onclick="login(jQuery)">
                        Confirmar
                    </button>
                </div>
            </div>
        </div>
    </div>


    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">

    <!--Login dialog-->

    <script src="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/lodash.js") %>"></script>
    
    <script src="<%=Page.ResolveUrl("~/js/jquery.inputmask-3.x/dist/min/jquery.inputmask.bundle.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-validation/jquery.validate.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-validation/localization/messages_es.min.js") %>"></script>        
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>            

    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ekko-lightbox.min.js") %>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/jquery.blockUI.min.js") %>"></script>    
    <script src="<%=Page.ResolveUrl("~/Librerias/jquery.blockUI.js") %>"></script>    

    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    
    <script src="//cdn.datatables.net/1.10.7/js/jquery.dataTables.min.js"></script>
    <%--<script src="<%=Page.ResolveUrl("~/Librerias/DataTables/datatables.min.js")%>"></script>--%>

    <%--<script src="//cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.min.js"></script>        --%>
    
    
    <script src="<%=Page.ResolveUrl("~/js/ListControl/crm-ns.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ListControl/crm.ui-ns.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.navegacion.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.servicios.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.servicios.navegacion.js")%>"></script>
    <uc:UCPatternflyToast runat="server" ID="ucPatternflyToast1" />
    
    <script src="<%=Page.ResolveUrl("~/js/Modernizr-input.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery.placeholder.min.js")%>"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/servicios/navegacion/Notificaciones.js") %>"></script>

    <%--<uc:UCNotificaciones_js runat="server" ID="UCNotificaciones_js" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/ui/Notificaciones.js") %>"></script>--%>

    <%--<uc:UINotificaciones runat="server" ID="UINotificaciones1" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/navegacion/Notificaciones.js") %>"></script>
--%>
    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet">

    <script src="<%=Page.ResolveUrl("~/js/CRM2/Gerente.js")%>"></script>

    <script type="text/javascript">        
        var _ApplicationUrl = '<%=ApplicationUrl %>';    
        /*VARIABLE GERENTE */
        var _Parametro_IdTU = "<%=Parametro_IdTU %>";
        var _Parametro_IdRik = "<%=Parametro_IdRik %>";
        var _Parametro_Nombre = "<%=Parametro_Nombre %>";

        var _CRM_Gerente_Id = "<%=CRM_Gerente_Id %>";
        var _CRM_Gerente_Rik = "<%=CRM_Gerente_Rik %>";
        var _CRM_Gerente_Nombre = "<%=CRM_Gerente_Nombre %>";

        var _CRM_Usuario_Id = "<%=CRM_Usuario_Id %>";
        var _CRM_Usuario_Rik = "<%=CRM_Usuario_Rik %>";
        var _CRM_Usuario_Nombre = "<%=CRM_Usuario_Nombre %>";
        
    </script>

    <script src="<%=Page.ResolveUrl("~/js/CRM2/Config.js")%>"></script>
        
    <script type="text/javascript">
        if ((typeof (console) == undefined) || (typeof (console) == 'undefined')) {
            window.console = new Object();
            window.console.log = function () {
            };
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
            console.log(message);
        };

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function Alerta_IdRik() {
            alertify
            .okBtn("Ok")
            .confirm("<b>Error Identificador Rik </b><br/><p>El suario actual no tien un Rik asignado, " +
            "esto causara un mal funcionamiento en el CRM, debe comunicarse a soporte.<p>", function (ev) {
               ev.preventDefault();
            }, function (ev) {
                   ev.preventDefault();
            });
        }

        //var _esNotificacionFuenteDeComando = false;

        //        function retirarElemento(sender, menuItemId) {
        //            _esNotificacionFuenteDeComando = true;
        //            var $menuItem = $('#' + menuItemId);
        //            $menuItem.addClass('animated fadeOutLeft').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
        //                $menuItem.removeClass('animated fadeOutLeft');
        //                $menuItem.remove();
        //            });
        //        }


        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
        function eliminarNotificacionRIK(id, menuItemId) {
            //_esNotificacionFuenteDeComando = true;
            crm.servicios.navegacion.Notificacion.eliminarNotificacionRIK(id, function () {
                //retirarElemento(null, menuItemId);
            },
            function () {
                PatternflyToast.showError('Se ha presentado una complicación al eliminar la notificación', 6000);
            },
            function () {
                //always
            },
            {
                401: function (jqXHR, textStatus, errorThrown) {
                    //self.location='<%=ApplicationUrl %>' + '/login.aspx';
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(eliminarNotificacionRIK, null, id, menuItemId);
                }
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
        function redireccionarALogin() {
            self.location = _ApplicationUrl + '/login.aspx';
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
        function mostrarToast(jqToastElement, jqParent) {
            $(jqToastElement).appendTo($(jqParent));
            $(jqToastElement).fadeIn();
        }

        var _onLoginSuccessful = null;

        function salirDelSistema() {
            window.location = _ApplicationUrl + '/Login.aspx?Id=1';
        }
        
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Initialize Datatables
        // Document REDY
        //
        $(document).ready(function () {
            /*
            $('.datatable').dataTable({
                "fnDrawCallback": function (oSettings) {
                    // if .sidebar-pf exists, call sidebar() after the data table is drawn
                    if ($('.sidebar-pf').length > 0) {
                        $(document).sidebar();
                    }
                }
            });
            */

            $('[data-toggle="popover"]').popovers();

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

            //Carga los parametros 

            // Compara si el usuario actual es el gerente
            if (_CRM_Gerente_Nombre == _CRM_Usuario_Nombre) {
                $('#Gerente_btnCambiarUsuario').text(_CRM_Gerente_Nombre);
            } else {
                $('#Gerente_btnCambiarUsuario').text(_CRM_Usuario_Nombre);
            }
            // 

            Incializa_Gerente(3);
            //alertify.success(_Parametro_IdTU);

            $('#ddSesion').click(function () {
                $('#ddSesion').addClass('open');
            });
            $('#UCNotificaciones1_menuItemNotificaciones').click(function () {
                $('#UCNotificaciones1_menuItemNotificaciones').addClass('open');
            });


        });
        
    </script>
    <script src="<%=Page.ResolveUrl("~/js/placeholder-setup.js")%>"></script>
    <script type="text/javascript">

        function printpage() {
            window.print();
        }  
    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphToolbar" runat="server">
    
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="Informe.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Informe" %>

<%@ Register Src="~/js/crm/servicios/navegacion/UCNotificaciones_js.ascx" TagPrefix="uc" TagName="UCNotificaciones_js" %>
<%@ Register Src="~/PortalRIK/Navegacion/Notificaciones/UCNotificaciones.ascx" TagPrefix="uc" TagName="UCNotificaciones" %>
<%@ Register Src="~/js/crm/ui/Notificaciones.ascx" TagPrefix="uc" TagName="UINotificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly-additions.min.css")%>">
    <script src="<%=Page.ResolveUrl("~/js/patternfly/patternfly.min.js")%>"></script>
    
    <%--<link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"/>--%>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/font-awesome.min.css")%>">
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">

    <script src="//code.jquery.com/jquery-2.1.4.min.js"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    
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
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="smMainScriptManager">
        </asp:ScriptManager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <div class="row" style="display: block;">

        <div class="col-sm-9 col-md-12">

          <div class="row ROWPAD MARGIN_BT5">
                <div class="col-sm-12 col-md-12 ROWPAD">
                    <h3>
                        <strong>Informes </strong>                        
                    </h3>
                </div>
            </div>

            <div class="row ROWPAD MARGIN_BT5">
                <div class="col-sm-12 col-md-12 ROWPAD">
    
                    <div class="row mt5">
                        <div class="col-md-2">
                            <label>Tipo de reporte</label>
                        </div>
                        <div class="col-md-3">
                            <select id="rbTipo" runat="server" class="selectpicker form-control"></select>
                        </div>
                    </div>

                    <!--div class="row mt5">
                        <div class="col-md-2">
                            <label>Centro de distribución</label>
                        </div>
                        <div class="col-md-3">
                            <select id="ddlZonas" class="selectpicker form-control"></select>                        
                        </div>
                    </div-->

                      <div class="row mt5">
                        <div class="col-md-2">
                            <label>Representante</label>
                        </div>
                        <div class="col-md-3">
                            <select id="ddlRepresentantesComercial" 
                                class="selectpicker form-control" name="ddlRepresentantesComercial">                            
                            </select>
                        </div>
                    </div>

                    <div class="row mt5">
                        <div class="col-md-2">
                            <label>Periodo</label>                        
                        </div>
                        <div class="col-md-3">                            
                                <select id="ddPeriodo" class="selectpicker form-control" name="ddPeriodo">                            
                                </select>                        
                        </div>
                    </div>

                    <div class="row mt5">
                        <div class="col-md-2">
                            <asp:Label ID="lNuevo" runat="server" Text="Proyectos Nuevos"></asp:Label>&nbsp;
                        </div>
                        <div class="col-md-3">                            
                                <input type="checkbox" id="chkProyectoNuevo" name="chkProyectoNuevo" value="" />                            
                        </div>
                    </div>

                    <div class="row mt5">
                        <div class="col-md-2">
                            <asp:Label ID="Label5" runat="server" Text="Monto"></asp:Label>                
                        </div>                        
                        <div class="col-md-3">                            
                            <table>
                                <tr>    
                                    <td><asp:Label ID="Label1" runat="server" Text="De:"></asp:Label></td>
                                    <td><asp:Label ID="Label3" runat="server" Text="A:"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><input type="text" style="width:100px;"  id="txtDe" value="0" />&nbsp;</td>
                                    <td><input type="text" style="width:100px;" id="txtA" value="9999999" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div style="padding:10px;">
                        
                        <table class="mt5" width:"100%">
                            <tr>
                                <td>                                
                                    <button type="button" class="btn btn-primary" id="btnGenerarRepote" >Generar reporte</button> 
                                </td>
                                <td>
                                    <img id="imgProgreso" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" style="display:block;" />
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>

                <div class="col-sm-9 col-md-9 ROWPAD">
                </div>                
            </div>

            <div width:"100%" class="row ROWPAD">

                

<%--                <table style="cellspacing:10px">
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                          
                        </td>
                    </tr><tr>
                        <td></td>
                    </tr><tr>
                        <td>
                            <%--<telerik:RadComboBox ID="ddlZonas" runat="server" Width="224px" Filter="Contains"
                            ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                            DataValueField="Id" LoadingMessage="Cargando..." AutoPostBack="True" OnClientBlur="Combo_ClientBlur2"
                            OnSelectedIndexChanged="ddlZonas_SelectedIndexChanged">
                            </telerik:RadComboBox>--%
                        </td>
                    </tr><tr>
                        <td></td>
                    </tr><tr>
                        <td>                                                
                            <%--<telerik:RadComboBox ID="ddlRepresentantesComercial" runat="server" Width="224px"
                            Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                            DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..."
                            OnClientBlur="Combo_ClientBlur">
                            </telerik:RadComboBox>--%                            
                        </td>
                    </tr><tr>
                        <td></td>    
                    </tr><tr>
                        <td>                                                
                            <%--<telerik:RadComboBox ID="ddPeriodo" runat="server" ChangeTextOnKeyBoardNavigation="true"
                            Filter="Contains" LoadingMessage="Cargando..." MarkFirstMatch="true" HighlightTemplatedItems="true"
                            OnClientBlur="Combo_ClientBlur" Width="224px" MaxHeight="250">
                            <ItemTemplate>--%>
                            <%--                                                        
                            <table>
                                    <tr>
                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                            <asp:Label ID="LabelID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                            Width="50px" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                        </td>
                                    </tr>
                            </table>
                            </ItemTemplate>
                            </telerik:RadComboBox>
                            --%
                        </td>                    
                    </tr>
                    <tr style="margin-top:50px!important;">                    
                        <td>
                            
                        </td>                                        
                    </tr>
            </table>--%>




                                     
                                                                    
                                    
<%--
                                    <div class="btn-group" data-toggle="buttons" id="divRadioButtons">                                                                            
                                       <table>
                                        <tr><td>
                                            <%--<asp:RadioButton runat="server" ID="radControl" Text="Control de la promoción nivel solución"
                                            AutoPostBack="True" ValidationGroup="Opciones" GroupName="opcion" OnCheckedChanged="radControl_CheckedChanged" />                                            
                                            --%
                                            <div class="radio">
                                                <label><input type="radio" id="rbTipo" name="rbTipo" value="1"/>Control de la promoción nivel solución</label>
                                            </div>                                            
                                        </td></tr><tr><td>

                                            <%--<asp:RadioButton runat="server" ID="radControlAplicacion" Text="Control de la promoción nivel aplicación"
                                            ValidationGroup="Opciones" GroupName="opcion" AutoPostBack="True" OnCheckedChanged="radControlAplicacion_CheckedChanged" />                                        --%
                                            <div class="radio">
                                                <label><input type="radio" id="rbTipo" name="rbTipo" value="2"/>Control de la promoción nivel aplicación</label>
                                            </div>                                            
                                        </td></tr><tr><td>
                                            <div class="radio">
                                                <label><input type="radio" id="rbTipo" name="rbTipo" value="3"/>Control de Entradas</label>
                                            </div>        
                                            <%--<asp:RadioButton runat="server" ID="radControlEntrada" Text="Control de Entradas"
                                            ValidationGroup="Opciones" GroupName="opcion" AutoPostBack="True" OnCheckedChanged="radControlEntrada_CheckedChanged" />                                       --%
                                        </td></tr><tr><td>
                                            
                                            <div class="radio">
                                                <label><input type="radio" id="rbTipo" name="rbTipo" value="4"/>DII en número de proyectos</label>
                                            </div> 

                                            <%--<asp:RadioButton runat="server" ID="radDII" Text="DII en número de proyectos" AutoPostBack="True"
                                            GroupName="opcion" ValidationGroup="Opciones" OnCheckedChanged="radDII_CheckedChanged" />                                        --%
                                        </td></tr><tr><td>

                                            <div class="radio">
                                                <label><input type="radio" id="rbTipo" name="rbTipo" value="5"/>DII en importe de proyectos</label>
                                            </div> 


                                            <%--<asp:RadioButton runat="server" ID="radDIINumero" Text="DII en importe de proyectos"
                                            AutoPostBack="True" GroupName="opcion" ValidationGroup="Opciones" OnCheckedChanged="radDIINumero_CheckedChanged" />                                        --%
                                        </td></tr><tr><td>
                                            
                                            <div class="radio">
                                                <label><input type="radio" id="rbTipo" name="rbTipo" value="6" />Campañas</label>
                                            </div> 
                                            
                                            <%--<asp:RadioButton runat="server" ID="radCampania" Text="Campañas"
                                            AutoPostBack="True" GroupName="opcion" ValidationGroup="Opciones" OnCheckedChanged="radCampania_CheckedChanged" />                                        --%
                                        </td></tr><tr><td>

                                            <div class="radio">
                                                <label><input type="radio" id="rbTipo" name="rbTipo" value="7"/>Cierre de Mes</label>
                                            </div> 
                                                                                        
                                            <%--<asp:RadioButton runat="server" ID="radCierreMes" Text="Cierre de Mes"
                                                AutoPostBack="True" GroupName="opcion" ValidationGroup="Opciones" OnCheckedChanged="radCierreMes_CheckedChanged" />--%
                                        </td></tr>
                                       </table> 
                                    </div>--%>

                                </td>
                                <td>
                                  
                                    &nbsp;
                                    <asp:Panel ID="pnlComercial" runat="server">
                                        <table id="tablaComercial" runat="server" cellpadding="0" cellspacing="0">
                                                                                    
                                            <tr>
                                                <td colspan="3" style="height: 16px">
                                                    <asp:Label ID="lblRepComercial" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 230px">
                                    &nbsp;
                                </td>
                                <td align="right">                                   
                                    
                                </td>
                            </tr>
                        </table>
            </div>
        </div>            
    </div>  
    
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">
    <!--Toast messages-->
    <!--Login dialog-->
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
    <!--Login dialog-->
    <script type="text/javascript">
        var _ApplicationUrl = '<%=ApplicationUrl %>';
        var Id_TU = '<%=Id_TU1 %>';
        var hfId_CD = '<%=Id_CD %>';
        var hfId_Rik = '<%=Id_Rik %>';
        var CDI_Nombre = '<%=CDI_Nombre %>';
    </script>
            
    <script src="<%=Page.ResolveUrl("~/js/Modernizr-input.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery.placeholder.min.js")%>"></script>

    <%--exportar excel--%>
    <script src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>

    <%--date format --%>
    <script src="<%=Page.ResolveUrl("~/js/date.format.js")%>"></script>
        
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/servicios/navegacion/Notificaciones.js") %>"></script>
    <uc:UCNotificaciones_js runat="server" ID="UCNotificaciones_js" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/ui/Notificaciones.js") %>"></script>
    <uc:UINotificaciones runat="server" ID="UINotificaciones1" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/navegacion/Notificaciones.js") %>"></script>

    <%--alertify--%>
    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet">
   
    <script src="<%=Page.ResolveUrl("~/js/placeholder-setup.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>                
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/min/numeral.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/jquery-numeraljs.js")%>"></script>

    <script src="<%=Page.ResolveUrl("~/js/CRM2/Informe.js")%>"></script>

    $(document).ready(function () {



    });
       
</asp:Content>

